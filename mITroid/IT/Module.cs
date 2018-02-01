using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mITroid.IT
{
    class Module
    {
        private List<Pattern> _patterns;
        private List<Sample> _samples;
        private List<Instrument> _instruments;
        private List<byte> _sequence;

        private List<uint> _instrumentOffsets;
        private List<uint> _sampleOffsets;
        private List<uint> _patternOffsets;

        private int _patternNum;
        private int _sampleNum;
        private int _instrumentNum;
        private int _sequenceNum;

        public string Name { get; set; }
        public string Message { get; set; }
        public int GlobalVolume { get; set; }
        public int MixVolume { get; set; }
        public int InitialSpeed { get; set; }
        public int InitialTempo { get; set; }
        public int LoopSequence { get; set; }

        public List<Pattern> Patterns { get { return _patterns; } }
        public List<Sample> Samples { get { return _samples; } }
        public List<Instrument> Instruments { get { return _instruments; } }
        public List<byte> Sequence { get { return _sequence; } }

        public Module(BinaryReader file)
        {
            string fileId = new string(file.ReadChars(4));
            if(fileId != "IMPM")
            {
                throw new InvalidDataException("The selected file is not an Impulse Tracker file");
            }


            Name = new string(file.ReadChars(26)).Trim('\0');


            file.BaseStream.Seek(0x20, SeekOrigin.Begin);
            _sequenceNum = file.ReadUInt16();
            _instrumentNum = file.ReadUInt16();
            _sampleNum = file.ReadUInt16();
            _patternNum = file.ReadUInt16();

            file.BaseStream.Seek(0x30, SeekOrigin.Begin);
            GlobalVolume = file.ReadByte();
            MixVolume = file.ReadByte();
            InitialSpeed = file.ReadByte();
            InitialTempo = file.ReadByte();

            file.BaseStream.Seek(0x36, SeekOrigin.Begin);
            UInt16 messageLength = file.ReadUInt16();
            UInt32 messageOffset = file.ReadUInt32();

            if(messageLength > 0)
            {
                file.BaseStream.Seek(messageOffset, SeekOrigin.Begin);
                Message = new string(file.ReadChars(messageLength)).Trim('\0');
                try
                {
                    LoopSequence = Convert.ToInt32(Message);
                }
                catch
                {
                    LoopSequence = 0;
                }
            }

            file.BaseStream.Seek(0xC0, SeekOrigin.Begin);
            _sequence = new List<byte>(file.ReadBytes(_sequenceNum));

            _instrumentOffsets = new List<uint>();
            for (int i = 0; i < _instrumentNum; i++)
                _instrumentOffsets.Add(file.ReadUInt32());

            _sampleOffsets = new List<uint>();
            for (int i = 0; i < _sampleNum; i++)
                _sampleOffsets.Add(file.ReadUInt32());

            _patternOffsets = new List<uint>();
            for (int i = 0; i < _patternNum; i++)
                _patternOffsets.Add(file.ReadUInt32());

            _instruments = new List<Instrument>();
            foreach(uint offset in _instrumentOffsets)
            {
                Instrument i = new Instrument(file, offset);
                _instruments.Add(i);
            }

            _samples = new List<Sample>();
            foreach(uint offset in _sampleOffsets)
            {
                Sample s = new Sample(file, offset, _sampleOffsets.IndexOf(offset));
                _samples.Add(s);
            }

            _patterns = new List<Pattern>();
            foreach(uint offset in _patternOffsets)
            {
                Pattern p = new Pattern(file, offset);
                _patterns.Add(p);
            }

        }

    }
}
