using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mITroid.NSPC
{
    class Chunk
    {
        public int Length { get; set; }
        public int Offset { get; set; }
        public byte[] Data { get; set; }
    }

    class Module
    {
        private List<Pattern> _patterns;
        private List<Sample> _samples;
        private List<Instrument> _instruments;
        private List<Sequence> _sequences;

        public string Name { get; set; }
        public int GlobalVolume { get; set; }
        public int MixVolume { get; set; }
        public int InitialSpeed { get; set; }
        public int InitialTempo { get; set; }

        public int CurrentSpeed { get; set; }
        public int CurrentTempo { get; set; }
        public int LoopSequence { get; set; }

        public List<Pattern> Patterns { get { return _patterns; } }
        public List<Sample> Samples { get { return _samples; } }
        public List<Instrument> Instruments { get { return _instruments; } }
        public List<Sequence> Sequence { get { return _sequences; } }

        public int PatternOffset {get; set;}
        public int SampleHeaderOffset { get; set; }
        public int InstrumentOffset { get; set; }
        public int SampleOffset { get; set; }

        public decimal ResampleFactor { get; set; }
        public bool EnhanceTreble { get; set; }

        public Module(IT.Module itModule, bool enhanceTreble, decimal resampleFactor)
        {
            Name = itModule.Name;
            GlobalVolume = (itModule.GlobalVolume * 2) - 1;
            InitialTempo = (int)Math.Round(itModule.InitialTempo / 4.8);
            InitialSpeed = itModule.InitialSpeed;
            LoopSequence = itModule.LoopSequence;

            /* Set SM standard values */
            SampleHeaderOffset = 0x6d60;
            InstrumentOffset = 0x6c90;
            PatternOffset = 0x5828;
            SampleOffset = 0xb210;

            _samples = new List<Sample>();
            foreach (var itSample in itModule.Samples)
            {
                var sample = new Sample(itSample, enhanceTreble, resampleFactor);
                _samples.Add(sample);
            }

            _instruments = new List<Instrument>();
            foreach(var itInstrument in itModule.Instruments)
            {
                var instrument = new Instrument(itInstrument, _samples[((itInstrument.SampleIndex > 0 && itInstrument.SampleIndex <= _samples.Count) ? itInstrument.SampleIndex : 1) - 1]);
                _instruments.Add(instrument);
            }

            _patterns = new List<Pattern>();
            foreach (var itPattern in itModule.Patterns)
            {
                var pattern = new Pattern(itPattern);
                _patterns.Add(pattern);
            }

            _sequences = new List<Sequence>();
            foreach(var itSequence in itModule.Sequence)
            {
                if (itSequence != 0xFF)
                {
                    var sequence = new Sequence() { Pattern = itSequence, Pointer = 0 };
                    _sequences.Add(sequence);
                }
            }
        }

        public List<Chunk> GenerateData()
        {
            var chunks = new List<Chunk>();
            CurrentSpeed = InitialSpeed;
            CurrentTempo = InitialTempo;

            var sampleChunk = new Chunk();
            sampleChunk.Offset = SampleOffset;
            List<byte> sampleBytes = new List<byte>();

            int curOffset = SampleOffset;
            foreach (var s in _samples.OrderBy(x => x.SampleIndex))
            {
                s.StartAddress = curOffset;
                s.LoopAddress = curOffset + s.LoopPoint;
                if (s.Data != null)
                {
                    sampleBytes.AddRange(s.Data);
                    curOffset += s.Data.Length;
                }
            }
            sampleChunk.Data = sampleBytes.ToArray();
            sampleChunk.Length = sampleChunk.Data.Length;
            chunks.Add(sampleChunk);

            var sampleHeaderChunk = new Chunk();
            sampleHeaderChunk.Offset = SampleHeaderOffset;
            sampleHeaderChunk.Data = new byte[_samples.Count * 4];
            using (MemoryStream ms = new MemoryStream(sampleHeaderChunk.Data))
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    foreach (var s in _samples)
                    {
                        bw.Write((UInt16)s.StartAddress);
                        bw.Write((UInt16)s.LoopAddress);
                    }
                }
            }
            sampleHeaderChunk.Length = sampleHeaderChunk.Data.Length;
            chunks.Add(sampleHeaderChunk);

            var instrumentChunk = new Chunk();
            instrumentChunk.Offset = InstrumentOffset;
            instrumentChunk.Data = new byte[_instruments.Count * 6];
            using (MemoryStream ms = new MemoryStream(instrumentChunk.Data))
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    foreach (var i in _instruments)
                    {
                        bw.Write((byte)i.SampleIndex);
                        bw.Write((UInt16)i.ADSR);
                        bw.Write((byte)i.Gain);
                        bw.Write((UInt16)i.PitchAdjustment);
                    }
                }
            }
            instrumentChunk.Length = instrumentChunk.Data.Length;
            chunks.Add(instrumentChunk);


            /* Reserve space for sequence data and setup pattern*/
            int patternDataOffset = PatternOffset + (_sequences.Count * 2) + 8 + 32;
            int duplicateRows = 0;
            int chunkEnd = 0;
            curOffset = patternDataOffset;
            foreach (var p in _patterns)
            {
                p.Pointer = curOffset;
                curOffset += 16;
                foreach (var t in p.Tracks)
                {
                    t.GenerateData(this);
                    if (t.Data.Count() > 0)
                    {
                        /* Check if this track is a duplicate of a previous track */
                        foreach(var pp in _patterns)
                        {
                            foreach(var tt in pp.Tracks)
                            {
                                if(tt.Pointer != 0)
                                {
                                    if (t.Events.Count == tt.Events.Count && t.Data.SequenceEqual(tt.Data))
                                    {
                                        t.Pointer = tt.Pointer;
                                        t.Data = new byte[0];
                                        duplicateRows++;
                                        goto next;
                                    }
                                }
                            }
                        }

                        t.Pointer = curOffset;
                        curOffset += t.Data.Length;
                    }
                    else
                    {
                        t.Pointer = 0;
                    }
                    next:
                    continue;
                }

                if(curOffset > (InstrumentOffset - 0x90) && curOffset < sampleChunk.Offset)
                {
                    /* out of space, allocate more after samples if possible */
                    curOffset = sampleChunk.Offset + sampleChunk.Length;

                    chunkEnd = p.Pointer;

                    /* reallocate the current pattern */
                    p.Pointer = curOffset;
                    curOffset += 16;
                    foreach(var t in p.Tracks)
                    {
                        if (t.Data.Length > 0)
                        {
                            t.Pointer = curOffset;
                            curOffset += t.Data.Length;
                        }
                    }
                }
            }

            if (chunkEnd == 0)
                chunkEnd = curOffset;

            var patternChunk = new Chunk();
            patternChunk.Offset = PatternOffset;
            patternChunk.Data = new byte[chunkEnd - PatternOffset + 1];
            using (MemoryStream ms = new MemoryStream(patternChunk.Data))
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    int setupPattern = PatternOffset + (_sequences.Count * 2) + 8;
                    bw.Write((UInt16)(PatternOffset + 2));
                    bw.Write((UInt16)setupPattern);
                    foreach(var seq in _sequences)
                    {
                        bw.Write((UInt16)_patterns[seq.Pattern].Pointer);
                    }
                    bw.Write((UInt16)0x00FF);
                    bw.Write((UInt16)(0x582C + (2 * LoopSequence)));

                    /* Write setup pattern */
                    for(int i = 0; i < 8; i++)
                    {
                        bw.Write((UInt16)(setupPattern + 16));
                    }

                    bw.Write((byte)0xF5);
                    bw.Write((byte)0x00);
                    bw.Write((byte)0x00);
                    bw.Write((byte)0x00);
                    bw.Write((byte)0xE5);
                    bw.Write((byte)GlobalVolume);
                    bw.Write((byte)0xE7);
                    bw.Write((byte)InitialTempo);
                    bw.Write((byte)0xE1);
                    bw.Write((byte)0x0A);
                    bw.Write((byte)0xED);
                    bw.Write((byte)GlobalVolume);
                    bw.Write((byte)InitialSpeed);
                    bw.Write((byte)0x7F);
                    bw.Write((byte)0xC9);
                    bw.Write((byte)0x00);

                    foreach(var p in _patterns.Where(x => x.Pointer < (InstrumentOffset - 0x90)))
                    {
                        foreach(var t in p.Tracks)
                        {
                            bw.Write((UInt16)t.Pointer);
                        }

                        foreach (var t in p.Tracks)
                        {
                            if (t.Data.Length > 0)
                            {
                                bw.Write(t.Data);
                            }
                        }
                    }
                }
            }
            patternChunk.Length = patternChunk.Data.Length;
            chunks.Add(patternChunk);

            if (_patterns.Where(x => x.Pointer > sampleChunk.Offset).Any())
            {
                var extraPatternChunk = new Chunk();
                extraPatternChunk.Offset = (sampleChunk.Offset + sampleChunk.Length);
                extraPatternChunk.Length = (_patterns.Where(x => x.Pointer >= extraPatternChunk.Offset).Max(x => x.Tracks.Max(y => y.Pointer + y.Data.Length)) - extraPatternChunk.Offset) + 1;
                extraPatternChunk.Data = new byte[extraPatternChunk.Length];
                using (MemoryStream ms = new MemoryStream(extraPatternChunk.Data))
                {
                    using (BinaryWriter bw = new BinaryWriter(ms))
                    {
                        foreach (var p in _patterns.Where(x => x.Pointer >= extraPatternChunk.Offset).OrderBy(x => x.Pointer))
                        {
                            foreach (var t in p.Tracks)
                            {
                                bw.Write((UInt16)t.Pointer);
                            }

                            foreach (var t in p.Tracks)
                            {
                                if (t.Data.Length > 0)
                                {
                                    bw.Write(t.Data);
                                }
                            }
                        }
                    }
                }
                chunks.Add(extraPatternChunk);
            }

            return chunks;
        }
    }
}
