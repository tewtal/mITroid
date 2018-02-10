using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mITroid.IT
{

    class EnvelopeNode
    {
        public int Volume { get; set; }
        public int Ticks { get; set; }
    }

    class Instrument
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public int FadeOut { get; set; }
        public int GlobalVolume { get; set; }
        public int DefaultPan { get; set; }
        public int SampleIndex { get; set; }
        public bool SustainLoop { get; set; }
        public bool UseEnvelope { get; set; }
        public int InstrumentIndex { get; set; }

        public List<EnvelopeNode> EnvelopeNodes { get; set; }
        private int _nodeNum;

        public Instrument(BinaryReader file, uint offset, int index)
        {
            InstrumentIndex = index;

            file.BaseStream.Seek(offset + 0x04, SeekOrigin.Begin);
            FileName = new string(file.ReadChars(12)).Trim('\0');

            file.BaseStream.Seek(offset + 0x14, SeekOrigin.Begin);
            FadeOut = file.ReadUInt16();

            file.BaseStream.Seek(offset + 0x18, SeekOrigin.Begin);
            GlobalVolume = file.ReadByte();
            DefaultPan = file.ReadByte();

            file.BaseStream.Seek(offset + 0x20, SeekOrigin.Begin);
            Name = new string(file.ReadChars(26)).Trim('\0');

            file.BaseStream.Seek(offset + 0x40 + 121, SeekOrigin.Begin);
            SampleIndex = file.ReadByte();

            file.BaseStream.Seek(offset + 0x130, SeekOrigin.Begin);
            byte flags = file.ReadByte();
            UseEnvelope = ((flags & 1) == 1);
            SustainLoop = ((flags & 4) == 4);

            _nodeNum = file.ReadByte();

            file.BaseStream.Seek(offset + 0x136, SeekOrigin.Begin);
            EnvelopeNodes = new List<EnvelopeNode>();
            for(int i = 0; i < _nodeNum; i++)
            {
                var node = new EnvelopeNode
                {
                    Volume = file.ReadByte(),
                    Ticks = file.ReadUInt16()
                };

                EnvelopeNodes.Add(node);
            }

        }
    }
}
