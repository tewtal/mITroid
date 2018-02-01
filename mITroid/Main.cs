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
                
                _module = new NSPC.Module(it, chkTreble.Checked, resampleFactor);
                _chunks = _module.GenerateData();


                lblInstruments.Text = _module.Instruments.Count.ToString() + " instruments - " + _chunks[2].Length + " bytes (" + ((int)((_chunks[2].Length / (double)0x70) * 100)).ToString() + "%)";
                lblSamplesHeaders.Text = _module.Samples.Count.ToString() + " headers - " + _chunks[1].Length + " bytes (" + ((int)((_chunks[1].Length / (double)0xA0) * 100)).ToString() + "%)";
                lblSamples.Text = _module.Instruments.Count.ToString() + " samples - " + _chunks[0].Length + " bytes (" + ((int)((_chunks[0].Length / (double)0x4D7F) * 100)).ToString() + "%)";
                lblMusicData.Text = _module.Patterns.Where(x => x.Pointer < _chunks[2].Offset).Count().ToString() + " patterns - " + _chunks[3].Length + " bytes (" + ((int)((_chunks[3].Length / (double)0x13D8) * 100)).ToString() + "%)";
                if(_chunks.Count > 4)
                {
                    lblExtraMusicData.Text = _module.Patterns.Where(x => x.Pointer > _chunks[2].Offset).Count().ToString() + " extra patterns - " + _chunks[4].Length + " bytes (" + ((int)((_chunks[4].Length / (double)(0xFF8f - _chunks[4].Offset)) * 100)).ToString() + "%)";
                }
                else
                {
                    lblExtraMusicData.Text = "0 extra patterns";
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
                var sfd = new SaveFileDialog();
                sfd.FileName = _fileName.Replace(".it", ".spc");
                if(sfd.ShowDialog() == DialogResult.OK)
                {
                    var spcData = File.OpenRead(Application.StartupPath + "\\smorg.spc");
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
                var sfd = new SaveFileDialog();
                sfd.FileName = _fileName.Replace(".it", ".nspc");
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
    }
}
