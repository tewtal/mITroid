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
    public partial class Advanced : Form
    {
        private Game _game;
        private Module _module;
        private List<Chunk> _chunks;

        public Advanced()
        {
            InitializeComponent();
            txtSampleHeaders.Text = "0000";
            txtInstrumentHeaders.Text = "0000";
            txtPatternData.Text = "0000";
            txtSamples.Text = "0000";
            txtSongIndex.Text = "1";
            txtMusicIndex.Text = "0000";
            txtEchoBuffer.Text = "0000";
            txtEchoBufferLen.Text = "0000";
            txtSampleIndexOffset.Text = "00";
            txtInstrumentIndexOffset.Text = "00";
            txtSPCTemplate.Text = "";
            chkRepointEchoBuffer.Checked = false;
            chkRepointMusicData.Checked = false;
            chkDisableKeyOffNotes.Checked = false;
            chkDisableKeyOffPatterns.Checked = false;
            chkRepointEchoBuffer.Enabled = false;
            chkRepointMusicData.Enabled = false;
            chkDisableKeyOffNotes.Enabled = false;
            chkDisableKeyOffPatterns.Enabled = false;
            cboGamePreset.SelectedItem = "Custom";
            _game = Game.Custom;            
        }

        private void cboGamePreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch((string)cboGamePreset.SelectedItem)
            {
                case "Super Metroid":
                    txtSampleHeaders.Text = "6D00";
                    txtInstrumentHeaders.Text = "6C00";
                    txtPatternData.Text = "5830";
                    txtSamples.Text = "B210";
                    txtSongIndex.Text = "5";
                    txtEchoBuffer.Text = "1500";
                    txtEchoBufferLen.Text = "1000";
                    txtSampleIndexOffset.Text = "18";
                    txtInstrumentIndexOffset.Text = "18";
                    txtSPCTemplate.Text = "smorg.spc";
                    txtMusicIndex.Text = "5820";
                    chkRepointEchoBuffer.Checked = false;
                    chkRepointMusicData.Checked = false;
                    chkRepointEchoBuffer.Enabled = false;
                    chkRepointMusicData.Enabled = false;
                    chkDisableKeyOffNotes.Enabled = true;
                    chkDisableKeyOffPatterns.Enabled = true;
                    txtEchoBuffer.Enabled = false;
                    txtEchoBufferLen.Enabled = false;
                    _game = Game.SM;
                    break;
                case "Super Metroid (overwrite)":
                    txtSampleHeaders.Text = "6D00";
                    txtInstrumentHeaders.Text = "6C00";
                    txtPatternData.Text = "5830";
                    txtSamples.Text = "6E00";
                    txtMusicIndex.Text = "5820";
                    txtSongIndex.Text = "5";
                    txtEchoBuffer.Text = "1500";
                    txtEchoBufferLen.Text = "1000";
                    txtSampleIndexOffset.Text = "00";
                    txtInstrumentIndexOffset.Text = "00";
                    txtSPCTemplate.Text = "smorg.spc";
                    chkRepointEchoBuffer.Checked = false;
                    chkRepointMusicData.Checked = false;
                    chkRepointEchoBuffer.Enabled = false;
                    chkRepointMusicData.Enabled = false;
                    chkDisableKeyOffNotes.Enabled = true;
                    chkDisableKeyOffPatterns.Enabled = true;
                    txtEchoBuffer.Enabled = false;
                    txtEchoBufferLen.Enabled = false;
                    _game = Game.SM;
                    break;
                case "A Link to the Past":
                    txtSampleHeaders.Text = "3C00";
                    txtInstrumentHeaders.Text = "3D00";
                    txtPatternData.Text = "2A10";
                    txtSamples.Text = "BAA0";
                    txtMusicIndex.Text = "2A00";
                    txtSongIndex.Text = "1";
                    txtEchoBuffer.Text = "FF00";
                    txtEchoBufferLen.Text = "1000";
                    txtSampleIndexOffset.Text = "19";
                    txtInstrumentIndexOffset.Text = "1D";
                    txtSPCTemplate.Text = "alttporg.spc";
                    chkRepointEchoBuffer.Checked = true;
                    chkRepointMusicData.Checked = true;
                    chkRepointEchoBuffer.Enabled = true;
                    chkRepointMusicData.Enabled = true;
                    chkDisableKeyOffNotes.Enabled = true;
                    chkDisableKeyOffPatterns.Enabled = true;
                    txtEchoBuffer.Enabled = true;
                    txtEchoBufferLen.Enabled = true;
                    _game = Game.ALTTP;
                    break;
                case "A Link to the Past (overwrite)":
                    txtSampleHeaders.Text = "3C00";
                    txtInstrumentHeaders.Text = "3D00";
                    txtPatternData.Text = "2910";
                    txtSamples.Text = "4000";
                    txtMusicIndex.Text = "2900";
                    txtSongIndex.Text = "1";
                    txtEchoBuffer.Text = "FF00";
                    txtEchoBufferLen.Text = "1000";
                    txtSampleIndexOffset.Text = "00";
                    txtInstrumentIndexOffset.Text = "00";
                    txtSPCTemplate.Text = "alttporg.spc";
                    chkRepointEchoBuffer.Checked = true;
                    chkRepointMusicData.Checked = true;
                    chkRepointEchoBuffer.Enabled = true;
                    chkRepointMusicData.Enabled = true;
                    chkDisableKeyOffNotes.Enabled = true;
                    chkDisableKeyOffPatterns.Enabled = true;
                    txtEchoBuffer.Enabled = true;
                    txtEchoBufferLen.Enabled = true;
                    _game = Game.ALTTP;
                    break;
                default:
                    txtSampleHeaders.Text = "0000";
                    txtInstrumentHeaders.Text = "0000";
                    txtPatternData.Text = "0000";
                    txtSamples.Text = "0000";
                    txtSongIndex.Text = "1";
                    txtMusicIndex.Text = "0000";
                    txtEchoBuffer.Text = "0000";
                    txtEchoBufferLen.Text = "0000";
                    txtSampleIndexOffset.Text = "00";
                    txtInstrumentIndexOffset.Text = "00";
                    txtSPCTemplate.Text = "";
                    chkRepointEchoBuffer.Checked = false;
                    chkRepointMusicData.Checked = false;
                    chkDisableKeyOffNotes.Checked = false;
                    chkDisableKeyOffPatterns.Checked = false;
                    chkRepointEchoBuffer.Enabled = false;
                    chkRepointMusicData.Enabled = false;
                    chkDisableKeyOffNotes.Enabled = false;
                    chkDisableKeyOffPatterns.Enabled = false;
                    txtEchoBuffer.Enabled = true;
                    txtEchoBufferLen.Enabled = true;
                    _game = Game.Custom;
                    break;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            lstInputFiles.Items.Clear();
            var ofd = new OpenFileDialog();
            ofd.Filter = "Impulse Tracker Files|*.it";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                lstInputFiles.Items.Add(ofd.FileName);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            lstInputFiles.Items.Remove(lstInputFiles.SelectedItem);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if(lstInputFiles.Items.Count == 0)
            {
                MessageBox.Show("You must add an input file first.");
                return;
            }

            if (lstInputFiles.Items.Count > 1)
            {
                MessageBox.Show("Only one input file is supported at this time.");
                return;
            }

            var br = new BinaryReader(File.OpenRead((string)lstInputFiles.Items[0]));
            var it = new IT.Module(br);
            br.Close();

            decimal resampleFactor = 1;
            try
            {
                resampleFactor = decimal.Parse(txtResample.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            }
            catch
            {

            }

            decimal engineSpeed = 1;
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
            else if (radio05x.Checked)
            {
                engineSpeed = 0.5m;
            }

            RAMMap ram = new RAMMap();
            ram.SampleHeaderOffset = Convert.ToInt32(txtSampleHeaders.Text, 16);
            ram.InstrumentOffset = Convert.ToInt32(txtInstrumentHeaders.Text, 16);
            ram.PatternOffset = Convert.ToInt32(txtPatternData.Text, 16);
            ram.SampleOffset = Convert.ToInt32(txtSamples.Text, 16);
            ram.InstrumentIndexOffset = Convert.ToInt32(txtInstrumentIndexOffset.Text, 16);
            ram.SampleIndexOffset = Convert.ToInt32(txtSampleIndexOffset.Text, 16);
            ram.EchoBufferOffset = Convert.ToInt32(txtEchoBuffer.Text, 16);
            ram.EchoBufferLength = Convert.ToInt32(txtEchoBufferLen.Text, 16);
            ram.PatternIndexOffset = Convert.ToInt32(txtMusicIndex.Text, 16);
            ram.SongIndex = Convert.ToInt32(txtSongIndex.Text, 16);

            _module = new Module(it, chkEnhanceTreble.Checked, resampleFactor, engineSpeed, true, chkOptimizePatterns.Checked, _game, ram);
            _module.Deduplicate = chkOptimizeTracks.Checked;
            _module.SetupPattern = chkSetupPattern.Checked;

            _chunks = _module.GenerateData();
            _chunks.Add(new Chunk() { Offset = ram.EchoBufferOffset - ram.EchoBufferLength, Length = ram.EchoBufferLength, Type = Chunk.ChunkType.Echo });

            var patches = new List<Patch>();

            if (chkDisableKeyOffNotes.Checked)
            {
                if (_module.Game == NSPC.Game.SM)
                {
                    patches.Add(new Patch() { Offset = 0x1CAF, Data = new byte[] { 0xC9, 0x90 }, OrigData = new byte[] { 0xC8, 0xF0 } });
                    patches.Add(new Patch() { Offset = 0x164E, Data = new byte[] { 0x3f, 0xf0, 0x57 }, OrigData = new byte[] { 0xd5, 0x61, 0x03 } });
                    _chunks.Add(new Chunk() { Data = new byte[] { 0x2d, 0xe4, 0x47, 0x8d, 0x5c, 0x3f, 0x26, 0x17, 0xae, 0xd5, 0x61, 0x03, 0x6f }, Offset = 0x57f0, Length = 13, Type = Chunk.ChunkType.Patches });
                }
                else if (_module.Game == NSPC.Game.ALTTP)
                {
                    patches.Add(new Patch() { Offset = 0x101D, Data = new byte[] { 0xC9, 0x90 }, OrigData = new byte[] { 0xC8, 0xF0 } });
                    patches.Add(new Patch() { Offset = 0x091F, Data = new byte[] { 0x3f, 0xf0, 0x3f }, OrigData = new byte[] { 0xd5, 0x61, 0x03 } });
                    _chunks.Add(new Chunk() { Data = new byte[] { 0x2d, 0xe4, 0x47, 0x8d, 0x5c, 0x3f, 0xf7, 0x09, 0xae, 0xd5, 0x61, 0x03, 0x6f }, Offset = 0x3ff0, Length = 13, Type = Chunk.ChunkType.Patches });
                }
            }

            if (chkDisableKeyOffPatterns.Checked)
            {
                if (_module.Game == NSPC.Game.SM)
                {
                    patches.Add(new Patch() { Offset = 0x1CC5, Data = new byte[] { 0xF0, 0x2A }, OrigData = new byte[] { 0xF0, 0x23 } });
                }
                else if (_module.Game == NSPC.Game.ALTTP)
                {
                    patches.Add(new Patch() { Offset = 0x1033, Data = new byte[] { 0xF0, 0x2A }, OrigData = new byte[] { 0xF0, 0x23 } });
                }
            }

            if (_module.Game == NSPC.Game.ALTTP)
            {
                /* Patch ALTTP music data pointer and echo buffer */
                if (chkRepointEchoBuffer.Checked)
                {
                    _chunks.Insert(0, new Chunk() { Data = new byte[] { 0x6d, (byte)((ram.EchoBufferOffset-ram.EchoBufferLength) >> 8) }, Length = 2, Offset = 0xf2, Type = Chunk.ChunkType.Patches });
                    patches.Add(new Patch() { Offset = 0x0e61, Data = new byte[] { 0x88, (byte)((ram.EchoBufferOffset >> 8)) }, OrigData = new byte[] { 0x88, 0xd0 } });
                }

                if (chkRepointMusicData.Checked)
                {
                    patches.Add(new Patch() { Offset = 0x0aaa, Data = new byte[] { 0xf5, (byte)((ram.PatternOffset & 0xff) - 0x11), (byte)((ram.PatternOffset >> 8) - 1) }, OrigData = new byte[] { 0xf5, 0xff, 0xcf } });
                    patches.Add(new Patch() { Offset = 0x0aae, Data = new byte[] { 0xf5, (byte)((ram.PatternOffset & 0xff) - 0x12), (byte)((ram.PatternOffset >> 8) - 1) }, OrigData = new byte[] { 0xf5, 0xfe, 0xcf } });
                }
            }

            if (patches.Count > 0)
            {
                _chunks.AddRange(Patch.GetPatchChunks(patches, _module.Game));
            }

            lstSPCData.Items.Clear();

            foreach(var chunk in _chunks.Where(x => x.Type != Chunk.ChunkType.Patches).OrderBy(x => x.Offset))
            {
                lstSPCData.Items.Add(String.Format("[{0:X4}-{1:X4}] {2}", chunk.Offset, chunk.Offset + chunk.Length - 1, chunk.Type.ToString()));
            }
        }

        private void btnCreateSPC_Click(object sender, EventArgs e)
        {
            if (_module != null)
            {
                var sfd = new SaveFileDialog
                {
                    FileName = ((string)lstInputFiles.Items[0]).Replace(".it", ".spc")
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    var spcData = File.OpenRead(Application.StartupPath + "\\" + txtSPCTemplate.Text);
                    byte[] fileData = new byte[spcData.Length];
                    spcData.Read(fileData, 0, (int)spcData.Length);

                    using (MemoryStream ms = new MemoryStream(fileData))
                    {
                        using (BinaryWriter bw = new BinaryWriter(ms))
                        {
                            foreach (var chunk in _chunks.Where(x => x.Type != Chunk.ChunkType.Echo))
                            {
                                bw.BaseStream.Seek(chunk.Offset + 0x100, SeekOrigin.Begin);
                                bw.Write(chunk.Data);
                            }

                            if(chkRepointEchoBuffer.Checked)
                            {
                                bw.BaseStream.Seek(0x1016D, SeekOrigin.Begin);
                                bw.Write((byte)((_module.Ram.EchoBufferOffset - _module.Ram.EchoBufferLength) >> 8));

                                bw.BaseStream.Seek(0x1017D, SeekOrigin.Begin);
                                bw.Write((byte)(_module.Ram.EchoBufferLength / 0x800));
                            }
                        }
                    }                   

                    File.WriteAllBytes(sfd.FileName, fileData);
                    MessageBox.Show("SPC music file written.");
                }
            }
            else
            {
                MessageBox.Show("You must press 'Generate N-SPC data' before creating the SPC file.");
            }
        }

        private void btnWriteNSPC_Click(object sender, EventArgs e)
        {
            if (_module != null)
            {
                var sfd = new SaveFileDialog
                {
                    FileName = ((string)lstInputFiles.Items[0]).Replace(".it", ".nspc")
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    List<byte> fileData = new List<byte>();
                    foreach (var chunk in _chunks)                        
                    {
                        if (chunk.Type == Chunk.ChunkType.Echo)
                            continue;

                        if (chunk.Type == Chunk.ChunkType.InstrumentHeaders && chkExportInstruments.Checked == false)
                            continue;

                        if (chunk.Type == Chunk.ChunkType.SampleHeaders && chkExportSampleHeaders.Checked == false)
                            continue;

                        if (chunk.Type == Chunk.ChunkType.Samples && chkExportSamples.Checked == false)
                            continue;

                        if (chunk.Type == Chunk.ChunkType.Patterns && chkExportMusicData.Checked == false)
                            continue;

                        if (chunk.Type == Chunk.ChunkType.PatternIndex && chkExportMusicIndex.Checked == false)
                            continue;

                        if (chunk.Type == Chunk.ChunkType.Patches && chkExportPatches.Checked == false)
                            continue;

                        fileData.Add((byte)(chunk.Length & 0xFF));
                        fileData.Add((byte)((chunk.Length >> 8) & 0xFF));

                        fileData.Add((byte)(chunk.Offset & 0xFF));
                        fileData.Add((byte)((chunk.Offset >> 8) & 0xFF));

                        fileData.AddRange(chunk.Data);
                    }

                    if (_module.Game == Game.SM)
                    {
                        fileData.Add((byte)0x00);
                        fileData.Add((byte)0x00);
                        fileData.Add((byte)0x00);
                        fileData.Add((byte)0x15);
                    }

                    File.WriteAllBytes(sfd.FileName, fileData.ToArray());
                    MessageBox.Show("N-SPC data file written.");
                }
            }
            else
            {
                MessageBox.Show("You must press 'Generate N-SPC data' before creating the N-SPC data file.");
            }
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void btnLoadInfoFromRom_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Super Nintendo ROM|*.sfc;*.smc";
            ofd.ShowDialog();
        }
    }
}
