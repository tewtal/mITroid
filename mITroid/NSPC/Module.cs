using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mITroid.NSPC
{
    class RAMMap
    {
        public int PatternIndexOffset { get; set; }
        public int SongIndex { get; set; }
        public int PatternOffset { get; set; }
        public int SampleHeaderOffset { get; set; }
        public int InstrumentOffset { get; set; }
        public int SampleOffset { get; set; }
        public int SampleIndexOffset { get; set; }
        public int InstrumentIndexOffset { get; set; }
        public int EchoBufferOffset { get; set; }
        public int EchoBufferLength { get; set; }

        public int PatternEnd { get
            {
                int p = 0xFFFF;
                p = (SampleHeaderOffset > PatternOffset && SampleHeaderOffset < p) ? SampleHeaderOffset : p;
                p = (InstrumentOffset > PatternOffset && InstrumentOffset < p) ? InstrumentOffset : p;
                p = (SampleOffset > PatternOffset && SampleOffset < p) ? SampleOffset : p;
                p = (EchoBufferOffset > PatternOffset && EchoBufferOffset < p) ? EchoBufferOffset : p;
                return p;
            }
        }
    }

    class Chunk
    {
        public enum ChunkType
        {
            SampleHeaders,
            InstrumentHeaders,
            Samples,
            PatternIndex,
            Patterns,
            Patches,
            Echo
        }

        public int Length { get; set; }
        public int Offset { get; set; }
        public byte[] Data { get; set; }
        public ChunkType Type { get; set; }
    }

    class Patch
    {
        public int Offset { get; set; }
        public byte[] Data { get; set; }
        public byte[] OrigData { get; set; }

        public byte[] GetUnpatchCode()
        {
            List<byte> patchCode = new List<byte>();
            int a = Offset;
            foreach(var b in OrigData)
            {
                patchCode.Add(0xE8);
                patchCode.Add(b);
                patchCode.Add(0xC5);
                patchCode.Add((byte)(a & 0xFF));
                patchCode.Add((byte)((a >> 8) & 0xFF));
                a++;
            }
            return patchCode.ToArray();
        }

        public static List<Chunk> GetPatchChunks(List<Patch> patches, Game game)
        {
            var chunks = new List<Chunk>();

            var patchCode = new List<byte>
            {
                0x2D
            };

            foreach (var p in patches)
            {
                chunks.Add(new Chunk() { Data = p.Data, Length = p.Data.Length, Offset = p.Offset, Type = Chunk.ChunkType.Patches });
                patchCode.AddRange(p.GetUnpatchCode());
            }

            patchCode.Add(0xAE);
            patchCode.Add(0xE8);
            patchCode.Add(0xAA);
            patchCode.Add(0xC5);
            patchCode.Add(0xF4);
            patchCode.Add(0x00);

            if (game == Game.ALTTP)
            {
                patchCode.Add(0x8F);
                patchCode.Add(0x6D);
                patchCode.Add(0xF2);

                patchCode.Add(0x8F);
                patchCode.Add(0xC0);
                patchCode.Add(0xF3);

                patchCode.Add(0x6F);
            }
            else
            {
                patchCode.Add(0x6F);
            }

            if (game == Game.SM)
            {
                chunks.Add(new Chunk() { Data = new byte[] { 0x3F, 0xF0, 0x56, 0x00, 0x00 }, Length = 5, Offset = 0x1E8B, Type = Chunk.ChunkType.Patches });
                chunks.Add(new Chunk() { Data = patchCode.ToArray(), Length = patchCode.Count(), Offset = 0x56F0, Type = Chunk.ChunkType.Patches });
            }
            else
            {
                chunks.Add(new Chunk() { Data = new byte[] { 0x3F, 0x80, 0x3f, 0x00, 0x00 }, Length = 5, Offset = 0x11E6, Type = Chunk.ChunkType.Patches });
                chunks.Add(new Chunk() { Data = patchCode.ToArray(), Length = patchCode.Count(), Offset = 0x3f80, Type = Chunk.ChunkType.Patches });
            }

            return chunks;
        }
    }

    enum Game
    {
        SM,
        ALTTP,
        Custom
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

        public decimal ResampleFactor { get; set; }
        public bool EnhanceTreble { get; set; }
        public Game Game { get; set; }
        public decimal EngineSpeed { get; set; }
        public bool UseNewADSR { get; set; }
        public int[] ChannelVolume { get; set; }
        public int[] ChannelPanning { get; set; }
        public RAMMap Ram { get; set; }
        public bool Deduplicate { get; set; }
        public bool SetupPattern { get; set; }

        public Dictionary<int,int> SampleIndexMap { get; set; }
        public Dictionary<int,int> InstrumentIndexMap { get; set; }

        public Module(IT.Module itModule, bool enhanceTreble, decimal resampleFactor, decimal engineSpeed, bool newAdsr, Game game, RAMMap ram)
        {
            EngineSpeed = engineSpeed;
            Name = itModule.Name;
            GlobalVolume = (itModule.GlobalVolume * 2) - 1;
            InitialTempo = (int)Math.Round(itModule.InitialTempo / (4.85m / EngineSpeed), 0);
            InitialSpeed = (int)Math.Round(itModule.InitialSpeed * EngineSpeed, 0);
            LoopSequence = itModule.LoopSequence;
            UseNewADSR = newAdsr;
            Game = game;
            Ram = ram;
            Deduplicate = true;
            SetupPattern = true;
            ChannelVolume = new int[] { 64, 64, 64, 64, 64, 64, 64, 64 };
            ChannelPanning = new int[] { 32, 32, 32, 32, 32, 32, 32, 32 };

            SampleIndexMap = new Dictionary<int, int>();
            InstrumentIndexMap = new Dictionary<int, int>();

            Track.Memory = new EffectMemory[8];
            for (int i = 0; i < 8; i++)
            {
                Track.Memory[i] = new EffectMemory();
            }

            for (int i = 0; i < itModule.InitialChannelVolume.Count; i++)
            {
                ChannelVolume[i] = itModule.InitialChannelVolume[i];
                ChannelPanning[i] = itModule.InitialChannelPanning[i];
            }

            _samples = new List<Sample>();
            int sidx = Ram.SampleIndexOffset;
            foreach (var itSample in itModule.Samples)
            {
                var sample = new Sample(itSample, enhanceTreble, resampleFactor);
                _samples.Add(sample);
                if (sample.VirtualSampleType == 1)
                {
                    SampleIndexMap.Add(sample.SampleIndex, sample.VirtualSampleIndex);
                }
                else if(sample.VirtualSampleType == 2)
                {
                    SampleIndexMap.Add(sample.SampleIndex, sample.VirtualSampleIndex);
                }
                else
                {
                    SampleIndexMap.Add(sample.SampleIndex, sidx);
                    sidx++;
                }
            }

            foreach(var nSample in _samples)
            {
                /* Update and correct sample indexes according to map */
                if(nSample.VirtualSampleType != 2)
                {
                    nSample.SampleIndex = SampleIndexMap[nSample.SampleIndex];
                }
                else
                {
                    nSample.SampleIndex = SampleIndexMap[nSample.VirtualSampleIndex - 1];
                }
            }

            int iidx = Ram.InstrumentIndexOffset;
            _instruments = new List<Instrument>();
            foreach(var itInstrument in itModule.Instruments)
            {
                var instrument = new Instrument(itInstrument, _samples[((itInstrument.SampleIndex > 0 && itInstrument.SampleIndex <= _samples.Count) ? itInstrument.SampleIndex : 1) - 1], this);
                _instruments.Add(instrument);
                if (instrument.VirtualInstrumentType == 1)
                {
                    InstrumentIndexMap.Add(instrument.InstrumentIndex, instrument.VirtualInstrumentIndex);
                }
                else if (instrument.VirtualInstrumentType == 2)
                {
                    InstrumentIndexMap.Add(instrument.InstrumentIndex, instrument.VirtualInstrumentIndex);
                }
                else
                {
                    InstrumentIndexMap.Add(instrument.InstrumentIndex, iidx);
                    iidx++;
                }
            }

            foreach (var nInstrument in _instruments)
            {
                /* Update and correct sample indexes according to map */
                if (nInstrument.VirtualInstrumentType != 2)
                {
                    nInstrument.InstrumentIndex = InstrumentIndexMap[nInstrument.InstrumentIndex];
                }
                else
                {
                    nInstrument.InstrumentIndex = InstrumentIndexMap[nInstrument.VirtualInstrumentIndex - 1];
                }
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

            var sampleChunk = new Chunk
            {
                Offset = Ram.SampleOffset,
                Type = Chunk.ChunkType.Samples
            };
            List<byte> sampleBytes = new List<byte>();

            int curOffset = Ram.SampleOffset;
            foreach (var s in _samples.OrderBy(x => x.SampleIndex))
            {
                if (s.VirtualSampleType == 0)
                {
                    s.StartAddress = curOffset;
                    s.LoopAddress = curOffset + s.LoopPoint;
                    if (s.Data != null)
                    {
                        sampleBytes.AddRange(s.Data);
                        curOffset += s.Data.Length;
                    }
                }
            }
            sampleChunk.Data = sampleBytes.ToArray();
            sampleChunk.Length = sampleChunk.Data.Length;
            chunks.Add(sampleChunk);

            var sampleHeaderChunk = new Chunk
            {
                Offset = Ram.SampleHeaderOffset + (Ram.SampleIndexOffset * 4),
                Data = new byte[_samples.Count * 4],
                Type = Chunk.ChunkType.SampleHeaders
            };
            using (MemoryStream ms = new MemoryStream(sampleHeaderChunk.Data))
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    foreach (var s in _samples.Where(x => x.VirtualSampleType == 0).OrderBy(x => x.SampleIndex))
                    {
                        bw.Write((UInt16)s.StartAddress);
                        bw.Write((UInt16)s.LoopAddress);
                    }
                }
            }

            sampleHeaderChunk.Length = sampleHeaderChunk.Data.Length;
            chunks.Add(sampleHeaderChunk);

            var instrumentChunk = new Chunk
            {
                Offset = Ram.InstrumentOffset + (Ram.InstrumentIndexOffset * 6),
                Data = new byte[_instruments.Count * 6],
                Type = Chunk.ChunkType.InstrumentHeaders
            };

            using (MemoryStream ms = new MemoryStream(instrumentChunk.Data))
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    foreach (var i in _instruments.Where(x => x.VirtualInstrumentType == 0).OrderBy(x => x.InstrumentIndex))
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
            int patternDataOffset = Ram.PatternOffset + (_sequences.Count * 2) + 8 + (SetupPattern ? (34 + (ChannelPanning.Any(x => x != 32) ? (7 * 8) : 0)) : -2);
            int trackDataOffset = patternDataOffset + (_patterns.Count * 16);
            int duplicateRows = 0;
            int chunkEnd = 0;
            curOffset = trackDataOffset;

            foreach (var p in _patterns)
            {
                p.Pointer = curOffset;
                //curOffset += 16;
                foreach (var t in p.Tracks)
                {
                    t.GenerateData(this);
                    if (t.Data.Count() > 0)
                    {
                        /* Check if this track is a duplicate of a previous track */
                        if (Deduplicate)
                        {
                            foreach (var pp in _patterns)
                            {
                                foreach (var tt in pp.Tracks)
                                {
                                    if (tt.Pointer != 0)
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
                        }

                        if ((curOffset + t.Data.Length) >= Ram.PatternEnd && curOffset < (sampleChunk.Offset + sampleChunk.Length))
                        {
                            /* out of space, allocate more after samples if possible */
                            chunkEnd = curOffset;
                            curOffset = sampleChunk.Offset + sampleChunk.Length;
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
            }

            if (chunkEnd == 0)
                chunkEnd = curOffset;

            var indexChunk = new Chunk
            {
                Offset = Ram.PatternIndexOffset + ((Ram.SongIndex - 1) * 2),
                Length = 2,
                Type = Chunk.ChunkType.PatternIndex,
                Data = new byte[] { (byte)(Ram.PatternOffset & 0xff), (byte)(Ram.PatternOffset >> 8) }
            };

            chunks.Add(indexChunk);

            var patternChunk = new Chunk
            {
                Offset = Ram.PatternOffset,
                Data = new byte[chunkEnd - Ram.PatternOffset + 1],
                Type = Chunk.ChunkType.Patterns
            };

            using (MemoryStream ms = new MemoryStream(patternChunk.Data))
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    int setupPattern = Ram.PatternOffset + (_sequences.Count * 2) + 8 - (SetupPattern ? 0 : 2);
                    //bw.Write((UInt16)(Ram.PatternOffset + 2));

                    if (SetupPattern)
                    {
                        bw.Write((UInt16)setupPattern);

                        foreach (var seq in _sequences)
                        {
                            //bw.Write((UInt16)_patterns[seq.Pattern].Pointer);

                            bw.Write((UInt16)(setupPattern + ((seq.Pattern + 1) * 16)));
                        }
                    }
                    else
                    {
                        foreach (var seq in _sequences)
                        {
                            //bw.Write((UInt16)_patterns[seq.Pattern].Pointer);

                            bw.Write((UInt16)(setupPattern + (seq.Pattern * 16)));
                        }
                    }

                    bw.Write((UInt16)0x00FF);
                    bw.Write((UInt16)((Ram.PatternOffset + (SetupPattern ? 2 : 0)) + (2 * LoopSequence)));
                    bw.Write((UInt16)0x0000);

                    if (SetupPattern)
                    {
                        if (ChannelPanning.Any(x => x != 32))
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                bw.Write((UInt16)(setupPattern + 16 + (i * 7) + (_patterns.Count * 16)));
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                bw.Write((UInt16)(setupPattern + 16 + (_patterns.Count * 16)));
                            }
                        }
                    }

                    foreach (var p in _patterns)
                    {
                        foreach (var t in p.Tracks)
                        {
                            bw.Write((UInt16)t.Pointer);
                        }
                    }

                    if (SetupPattern)
                    {
                        /* Write setup pattern pointers */
                        if (ChannelPanning.Any(x => x != 32))
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                bw.Write((byte)0xE1);
                                bw.Write((byte)(0x14 - (ChannelPanning[i] / 3.2)));
                                bw.Write((byte)0xEF);
                                bw.Write((ushort)(setupPattern + 16 + (8 * 7) + (_patterns.Count * 16)));
                                bw.Write((byte)0x00);
                                bw.Write((byte)0x00);
                            }

                            bw.Write((byte)0xC9);
                            bw.Write((byte)0xC9);
                        }
                        else
                        {
                            bw.Write((byte)0xE1);
                            bw.Write((byte)0x0A);
                        }

                        bw.Write((byte)0xF5);
                        bw.Write((byte)0x00);
                        bw.Write((byte)0x00);
                        bw.Write((byte)0x00);
                        bw.Write((byte)0xE5);
                        bw.Write((byte)GlobalVolume);
                        bw.Write((byte)0xE7);
                        bw.Write((byte)InitialTempo);
                        bw.Write((byte)0xED);
                        bw.Write((byte)GlobalVolume);
                        bw.Write((byte)InitialSpeed);
                        bw.Write((byte)0x7F);
                        bw.Write((byte)0xC9);
                        bw.Write((byte)0xF0);
                        bw.Write((byte)0x01);
                        bw.Write((byte)0x00);
                    }

                    foreach (var p in _patterns.Where(x => x.Pointer < Ram.PatternEnd))
                    {
                        //foreach(var t in p.Tracks)
                        //{
                        //    bw.Write((UInt16)t.Pointer);
                        //}

                        foreach (var t in p.Tracks.Where(x => x.Pointer < Ram.PatternEnd))
                        {
                            if (t.Data.Length > 0)
                            {
                                bw.Write(t.Data);
                            }
                        }
                    }

                    bw.Write((byte)0);
                }
            }
            patternChunk.Length = patternChunk.Data.Length;
            chunks.Add(patternChunk);

            //if (_patterns.Where(x => x.Pointer > Ram.PatternEnd).Any())
            if(_patterns.Any(x => x.Tracks.Any(y => y.Pointer > Ram.PatternEnd)))
            {
                var extraPatternChunk = new Chunk
                {
                    Offset = (sampleChunk.Offset + sampleChunk.Length),
                    Type = Chunk.ChunkType.Patterns
                };

                int extraLength = 0;
                //var lastPattern = _patterns.OrderByDescending(x => x.Pointer).First();
                var lastTrackLength = _patterns.Max(x => x.Tracks.Max(y => y.Pointer + y.Data.Length));

                //if (lastTrack.Pointer > lastPattern.Pointer)
                //{
                    extraLength = lastTrackLength - extraPatternChunk.Offset;
                //}
                //else
                //{
                //    extraLength = (lastPattern.Pointer + 16) - extraPatternChunk.Offset;
                //}

                extraPatternChunk.Length = extraLength; 
                extraPatternChunk.Data = new byte[extraPatternChunk.Length];
                using (MemoryStream ms = new MemoryStream(extraPatternChunk.Data))
                {
                    using (BinaryWriter bw = new BinaryWriter(ms))
                    {
                        //foreach (var p in _patterns.Where(x => x.Pointer >= extraPatternChunk.Offset).OrderBy(x => x.Pointer))
                        foreach(var p in _patterns.Where(x => x.Tracks.Any(y => y.Pointer >= extraPatternChunk.Offset)).OrderBy(x => x.Pointer))
                        {
                            //foreach (var t in p.Tracks)
                            //{
                            //    bw.Write((UInt16)t.Pointer);
                            //}

                            foreach (var t in p.Tracks.Where(x => x.Pointer >= extraPatternChunk.Offset))
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
