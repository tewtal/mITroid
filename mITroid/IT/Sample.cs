using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mITroid.IT
{
    class Sample
    {
        private int _flags;
        private uint _samplePointer;

        public string Name { get; set; }
        public int GlobalVolume { get; set; }
        public int Volume { get; set; }
        public bool UseLoop { get; set; }
        public uint Length { get; set; }
        public uint LoopBeg { get; set; }
        public uint LoopEnd { get; set; }
        public uint C5Speed { get; set; }
        public bool SixteenBit { get; set; }
        public int SampleIndex { get; set; }

        public Int16[] Data { get; set; }


        public Sample(BinaryReader file, uint offset, int index)
        {
            file.BaseStream.Seek(offset + 0x11, SeekOrigin.Begin);
            GlobalVolume = file.ReadByte();
            _flags = file.ReadByte();
            UseLoop = ((_flags & 16) != 0);
            SixteenBit = ((_flags & 2) != 0);
            SampleIndex = index;
            Volume = file.ReadByte();
            Name = new string(file.ReadChars(26)).Trim('\0');
            int conv = file.ReadByte();

            file.BaseStream.Seek(offset + 0x30, SeekOrigin.Begin);
            Length = file.ReadUInt32();
            LoopBeg = file.ReadUInt32();
            LoopEnd = file.ReadUInt32();
            C5Speed = file.ReadUInt32();

            if (UseLoop)
                Length = LoopEnd;

            file.BaseStream.Seek(offset + 0x48, SeekOrigin.Begin);
            _samplePointer = file.ReadUInt32();
            
            //byte[] sampleData = file.ReadBytes((int)Length * (SixteenBit ? 2 : 1));
            Data = new Int16[Length];
            file.BaseStream.Seek(_samplePointer, SeekOrigin.Begin);

            // subtract offset for unsigned samples
            int convOff = ((conv & 1)==1) ? 0 : (SixteenBit ? -32768 : -128);
            for (int i = 0; i < Length; i++)
            {
                int sample = 0;
                if(SixteenBit)
                {
                    sample = (int)(file.ReadUInt16() + convOff);
                }
                else
                {
                    sample = (int)((file.ReadByte() + convOff) << 8);
                }
                Data[i] = (Int16)sample;
            }

        }
    }
}
