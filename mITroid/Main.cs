using mITroid.NSPC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mITroid
{
    public partial class Main : Form
    {
        private NSPC.Module _module;
        private List<NSPC.Chunk> _chunks;
        private string _fileName;

        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Impulse Tracker Files|*.it";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                var br = new BinaryReader(ofd.OpenFile());
                var it = new IT.Module(br);
                br.Close();

                _fileName = ofd.FileName;

                decimal resampleFactor = 1;
                try
                {
                    resampleFactor = decimal.Parse(txtResample.Text.Replace(",","."), CultureInfo.InvariantCulture);
                }
                catch
                {

                }

                int engineSpeed = 1;
                if (radio2x.Checked)
                {
                    engineSpeed = 2;
                }
                else if (radio3x.Checked)
                {
                    engineSpeed = 3;
                }
                else if (radio4x.Checked)
                {
                    engineSpeed = 4;
                }

                bool newAdsr;
                if(radioNewADSR.Checked)
                {
                    newAdsr = true;
                }
                else
                {
                    newAdsr = false;
                }

                bool optimizePatterns;
                if(chkOptimizePatterns.Checked)
                {
                    optimizePatterns = true;
                } else
                {
                    optimizePatterns = false;
                }

                RAMMap ram = new RAMMap();
                Game game = (radioSM.Checked ? NSPC.Game.SM : NSPC.Game.ALTTP);

                if (game == NSPC.Game.SM)
                {
                    if (chkOverwrite.Checked)
                    {
                        ram.SampleHeaderOffset = 0x6d00;
                        ram.InstrumentOffset = 0x6c00;
                        ram.PatternOffset = 0x5830;
                        ram.SampleOffset = 0x6e00;
                        ram.SampleIndexOffset = 0x00;
                        ram.InstrumentIndexOffset = 0x00;
                        ram.PatternIndexOffset = 0x5820;
                        ram.EchoBufferOffset = 0x1500;
                        ram.EchoBufferLength = 0x1000;
                        ram.SongIndex = 0x05;
                    }
                    else
                    {
                        ram.SampleHeaderOffset = 0x6d00;
                        ram.InstrumentOffset = 0x6c00;
                        ram.PatternOffset = 0x5830;
                        ram.SampleOffset = 0xb210;
                        ram.SampleIndexOffset = 0x18;
                        ram.InstrumentIndexOffset = 0x18;
                        ram.PatternIndexOffset = 0x5820;
                        ram.EchoBufferOffset = 0x1500;
                        ram.EchoBufferLength = 0x1000;
                        ram.SongIndex = 0x05;
                    }
                }
                else if (game == NSPC.Game.ALTTP)
                {
                    if (chkOverwrite.Checked)
                    {
                        ram.SampleHeaderOffset = 0x3c00;
                        ram.InstrumentOffset = 0x3d00;
                        ram.SampleOffset = 0x4000;
                        ram.PatternOffset = 0x2910;
                        ram.SampleIndexOffset = 0x00;
                        ram.InstrumentIndexOffset = 0x00;
                        ram.PatternIndexOffset = 0x2900;
                        ram.EchoBufferOffset = 0xff00;
                        ram.EchoBufferLength = 0x1000;
                        ram.SongIndex = 0x01;
                    }
                    else
                    {
                        ram.SampleHeaderOffset = 0x3c00;
                        ram.InstrumentOffset = 0x3d00;
                        ram.SampleOffset = 0xbaa0;
                        ram.PatternOffset = 0x2910;
                        ram.SampleIndexOffset = 0x19;
                        ram.InstrumentIndexOffset = 0x1d;
                        ram.PatternIndexOffset = 0x2900;
                        ram.EchoBufferOffset = 0xff00;
                        ram.EchoBufferLength = 0x1000;
                        ram.SongIndex = 0x01;
                    }
                }

                _module = new Module(it, chkTreble.Checked, resampleFactor, engineSpeed, newAdsr, optimizePatterns, game, ram);
                _chunks = _module.GenerateData();

                var patches = new List<Patch>();

                if (chkPatchNoteOff.Checked)
                {
                    if (_module.Game == NSPC.Game.SM)
                    {
                        patches.Add(new Patch() { Offset = 0x1CAF, Data = new byte[] { 0xC9, 0x90 }, OrigData = new byte[] { 0xC8, 0xF0 } });
                        patches.Add(new Patch() { Offset = 0x164E, Data = new byte[] { 0x3f, 0xf0, 0x57 }, OrigData = new byte[] { 0xd5, 0x61, 0x03 } });
                        _chunks.Add(new Chunk() { Data = new byte[] { 0x2d, 0xe4, 0x47, 0x8d, 0x5c, 0x3f, 0x26, 0x17, 0xae, 0xd5, 0x61, 0x03, 0x6f }, Offset = 0x57f0, Length = 13, Type = Chunk.ChunkType.Patches });
                    }
                    else if(_module.Game == NSPC.Game.ALTTP)
                    {
                        patches.Add(new Patch() { Offset = 0x101D, Data = new byte[] { 0xC9, 0x90 }, OrigData = new byte[] { 0xC8, 0xF0 } });
                        patches.Add(new Patch() { Offset = 0x091F, Data = new byte[] { 0x3f, 0xf0, 0x3f }, OrigData = new byte[] { 0xd5, 0x61, 0x03 } });
                        _chunks.Add(new Chunk() { Data = new byte[] { 0x2d, 0xe4, 0x47, 0x8d, 0x5c, 0x3f, 0xf7, 0x09, 0xae, 0xd5, 0x61, 0x03, 0x6f }, Offset = 0x3ff0, Length = 13, Type = Chunk.ChunkType.Patches });
                    }
                }

                if (chkPatchPatternOff.Checked)
                {
                    if (_module.Game == NSPC.Game.SM)
                    {
                        patches.Add(new Patch() { Offset = 0x1CC5, Data = new byte[] { 0xF0, 0x2A }, OrigData = new byte[] { 0xF0, 0x23 } });
                    }
                    else if(_module.Game == NSPC.Game.ALTTP)
                    {
                        patches.Add(new Patch() { Offset = 0x1033, Data = new byte[] { 0xF0, 0x2A }, OrigData = new byte[] { 0xF0, 0x23 } });
                    }
                }

                if(_module.Game == NSPC.Game.ALTTP)
                {
                    /* Patch ALTTP music data pointer and echo buffer */
                    _chunks.Insert(0, new Chunk() { Data = new byte[] { 0x6d, 0xef }, Length = 2, Offset = 0xf2, Type = Chunk.ChunkType.Patches });
                    patches.Add(new Patch() { Offset = 0x0aaa, Data = new byte[] { 0xf5, 0xff, 0x28 }, OrigData = new byte[] { 0xf5, 0xff, 0xcf } });
                    patches.Add(new Patch() { Offset = 0x0aae, Data = new byte[] { 0xf5, 0xfe, 0x28 }, OrigData = new byte[] { 0xf5, 0xfe, 0xcf } });
                    patches.Add(new Patch() { Offset = 0x0e61, Data = new byte[] { 0x88, 0xff }, OrigData = new byte[] { 0x88, 0xd0 } });
                }

                if (patches.Count > 0)
                {
                    _chunks.AddRange(Patch.GetPatchChunks(patches, _module.Game));
                }

                int instrumentSize = (0x100 - (_module.Ram.InstrumentOffset & 0xFF));
                int sampleHeaderSize = (0x100 - (_module.Ram.SampleHeaderOffset & 0xFF));
                int musicDataSize = (_module.Ram.PatternEnd - _module.Ram.PatternOffset);
                int sampleSize = 0;

                if (_module.Game == NSPC.Game.SM)
                {
                    sampleSize = (0xFF8F - _chunks.Where(x => x.Type == Chunk.ChunkType.Samples).First().Offset);
                    lblInstruments.Text = _module.Instruments.Count.ToString() + " instruments - " + _chunks.Where(x => x.Type == Chunk.ChunkType.InstrumentHeaders).First().Length + " bytes (" + ((int)((_chunks.Where(x => x.Type == Chunk.ChunkType.InstrumentHeaders).First().Length / (double)instrumentSize) * 100)).ToString() + "%)";
                    lblSamplesHeaders.Text = _module.Samples.Where(x => x.VirtualSampleType == 0).Count().ToString() + " headers - " + _chunks.Where(x => x.Type == Chunk.ChunkType.SampleHeaders).First().Length + " bytes (" + ((int)((_chunks.Where(x => x.Type == Chunk.ChunkType.SampleHeaders).First().Length / (double)sampleHeaderSize) * 100)).ToString() + "%)";
                    lblSamples.Text = _module.Samples.Where(x => x.VirtualSampleType == 0).Count().ToString() + " samples - " + _chunks.Where(x => x.Type == Chunk.ChunkType.Samples).First().Length + " bytes (" + ((int)((_chunks.Where(x => x.Type == Chunk.ChunkType.Samples).First().Length / (double)sampleSize) * 100)).ToString() + "%)";
                    lblMusicData.Text = _module.Patterns.Where(x => x.Pointer < _module.Ram.PatternEnd).Count().ToString() + " patterns - " + _chunks.Where(x => x.Type == Chunk.ChunkType.Patterns && x.Offset < _module.Ram.PatternEnd).First().Length + " bytes (" + ((int)((_chunks.Where(x => x.Type == Chunk.ChunkType.Patterns && x.Offset < _module.Ram.PatternEnd).First().Length / (double)musicDataSize) * 100)).ToString() + "%)";
                    if (_chunks.Any(x => x.Type == Chunk.ChunkType.Patterns  && x.Offset > _module.Ram.PatternEnd))
                    {
                        lblExtraMusicData.Text = _module.Patterns.Where(x => x.Pointer > _module.Ram.PatternEnd).Count().ToString() + " extra patterns - " + _chunks.Where(x => x.Type == Chunk.ChunkType.Patterns && x.Offset > _module.Ram.PatternEnd).Sum(x => x.Length) + " bytes (" + ((int)((_chunks.Where(x => x.Type == Chunk.ChunkType.Patterns && x.Offset > _module.Ram.PatternEnd).Sum(x => x.Length) / (double)(0xFF8f - _chunks.Where(x => x.Type == Chunk.ChunkType.Patterns && x.Offset > _module.Ram.PatternEnd).First().Offset)) * 100)).ToString() + "%)";
                    }
                    else
                    {
                        lblExtraMusicData.Text = "0 extra patterns";
                    }
                }
                else
                {
                    sampleSize = (0xEFFF - _chunks.Where(x => x.Type == Chunk.ChunkType.Samples).First().Offset);
                    lblInstruments.Text = _module.Instruments.Count.ToString() + " instruments - " + _chunks.Where(x => x.Type == Chunk.ChunkType.InstrumentHeaders).First().Length + " bytes (" + ((int)((_chunks.Where(x => x.Type == Chunk.ChunkType.InstrumentHeaders).First().Length / (double)instrumentSize) * 100)).ToString() + "%)";
                    lblSamplesHeaders.Text = _module.Samples.Where(x => x.VirtualSampleType == 0).Count().ToString() + " headers - " + _chunks.Where(x => x.Type == Chunk.ChunkType.SampleHeaders).First().Length + " bytes (" + ((int)((_chunks.Where(x => x.Type == Chunk.ChunkType.SampleHeaders).First().Length / (double)sampleHeaderSize) * 100)).ToString() + "%)";
                    lblSamples.Text = _module.Samples.Where(x => x.VirtualSampleType == 0).Count().ToString() + " samples - " + _chunks.Where(x => x.Type == Chunk.ChunkType.Samples).First().Length + " bytes (" + ((int)((_chunks.Where(x => x.Type == Chunk.ChunkType.Samples).First().Length / (double)sampleSize) * 100)).ToString() + "%)";
                    lblMusicData.Text = _module.Patterns.Where(x => x.Pointer < _module.Ram.PatternEnd).Count().ToString() + " patterns - " + _chunks.Where(x => x.Type == Chunk.ChunkType.Patterns && x.Offset < _module.Ram.PatternEnd).First().Length + " bytes (" + ((int)((_chunks.Where(x => x.Type == Chunk.ChunkType.Patterns && x.Offset < _module.Ram.PatternEnd).First().Length / (double)musicDataSize) * 100)).ToString() + "%)";
                    if (_chunks.Any(x => x.Type == Chunk.ChunkType.Patterns && x.Offset > _module.Ram.PatternEnd))
                    {
                        lblExtraMusicData.Text = _module.Patterns.Where(x => x.Pointer > _module.Ram.PatternEnd).Count().ToString() + " extra patterns - " + _chunks.Where(x => x.Type == Chunk.ChunkType.Patterns && x.Offset > _module.Ram.PatternEnd).Sum(x => x.Length) + " bytes (" + ((int)((_chunks.Where(x => x.Type == Chunk.ChunkType.Patterns && x.Offset > _module.Ram.PatternEnd).Sum(x => x.Length) / (double)(0xEFFF - _chunks.Where(x => x.Type == Chunk.ChunkType.Patterns && x.Offset > _module.Ram.PatternEnd).First().Offset)) * 100)).ToString() + "%)";
                    }
                    else
                    {
                        lblExtraMusicData.Text = "0 extra patterns";
                    }
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        } 

        private void button2_Click(object sender, EventArgs e)
        {
            if(_module != null)
            {
                var sfd = new SaveFileDialog
                {
                    FileName = _fileName.Replace(".it", ".spc")
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    var spcData = File.OpenRead(Application.StartupPath + (_module.Game == NSPC.Game.SM ? "\\smorg.spc" : "\\alttporg.spc"));
                    byte[] fileData = new byte[spcData.Length];
                    spcData.Read(fileData, 0, (int)spcData.Length);

                    using (MemoryStream ms = new MemoryStream(fileData))
                    {
                        using (BinaryWriter bw = new BinaryWriter(ms))
                        {
                            foreach (var chunk in _chunks)
                            {
                                bw.BaseStream.Seek(chunk.Offset + 0x100, SeekOrigin.Begin);
                                bw.Write(chunk.Data);
                            }
                        }
                    }
                    File.WriteAllBytes(sfd.FileName, fileData);
                    MessageBox.Show("SPC music file written.");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_module != null)
            {
                var sfd = new SaveFileDialog
                {
                    FileName = _fileName.Replace(".it", ".nspc")
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    List<byte> fileData = new List<byte>();
                    foreach (var chunk in _chunks)
                    {
                        fileData.Add((byte)(chunk.Length & 0xFF));
                        fileData.Add((byte)((chunk.Length >> 8) & 0xFF));

                        fileData.Add((byte)(chunk.Offset & 0xFF));
                        fileData.Add((byte)((chunk.Offset >> 8) & 0xFF));

                        fileData.AddRange(chunk.Data);
                    }
                    fileData.Add((byte)0x00);
                    fileData.Add((byte)0x00);
                    fileData.Add((byte)0x00);
                    fileData.Add((byte)0x15);

                    File.WriteAllBytes(sfd.FileName, fileData.ToArray());
                    MessageBox.Show("N-SPC data file written.");
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            var frmAdvanced = new Advanced();
            frmAdvanced.Show();
        }
    }
}
