using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using K4os.Hash.xxHash;

namespace mITroid.NSPC
{
    public record Subroutine
    {
        public int Offset;
        public int Length;
        public byte[] Data;
    }

    internal class Optimizer
    {
        private readonly Module _module;
        private Dictionary<ulong, List<Block>> _blocks;

        struct Block
        {
            public Instruction[] Instructions;
            public int Position;
            public Track Track;
            public int NextInstruction;
        }

        struct Instruction
        {
            public byte Inst;
            public byte[] Data;
            public int Position;
        }


        public Optimizer(Module module)
        {
            _module = module;
            _blocks = new Dictionary<ulong, List<Block>>();
        }

        private List<Instruction> GetInstructions(Track t)
        {
            var instructions = new List<Instruction>();
            int pos = 0;
            while (pos < t.Data.Length)
            {
                byte inst = t.Data[pos];
                byte[] data = inst switch
                {
                    0x00 => t.Data[pos..(pos + 1)],
                    (> 0x00) and (< 0x80) => t.Data[pos + 1] < 0x80 ? t.Data[pos..(pos + 3)] : t.Data[pos..(pos + 2)],
                    (> 0x7F) and (< 0xE0) => t.Data[pos..(pos + 1)],
                    0xE0 => t.Data[pos..(pos + 2)],
                    0xE1 => t.Data[pos..(pos + 2)],
                    0xE2 => t.Data[pos..(pos + 3)],
                    0xE3 => t.Data[pos..(pos + 4)],
                    0xE4 => t.Data[pos..(pos + 1)],
                    0xE5 => t.Data[pos..(pos + 2)],
                    0xE6 => t.Data[pos..(pos + 3)],
                    0xE7 => t.Data[pos..(pos + 2)],
                    0xE8 => t.Data[pos..(pos + 3)],
                    0xE9 => t.Data[pos..(pos + 2)],
                    0xEA => t.Data[pos..(pos + 2)],
                    0xEB => t.Data[pos..(pos + 4)],
                    0xEC => t.Data[pos..(pos + 1)],
                    0xED => t.Data[pos..(pos + 2)],
                    0xEE => t.Data[pos..(pos + 3)],
                    0xEF => t.Data[pos..(pos + 4)],
                    0xF0 => t.Data[pos..(pos + 2)],
                    0xF1 => t.Data[pos..(pos + 4)],
                    0xF2 => t.Data[pos..(pos + 4)],
                    0xF3 => t.Data[pos..(pos + 1)],
                    0xF4 => t.Data[pos..(pos + 2)],
                    0xF5 => t.Data[pos..(pos + 4)],
                    0xF6 => t.Data[pos..(pos + 1)],
                    0xF7 => t.Data[pos..(pos + 4)],
                    0xF8 => t.Data[pos..(pos + 4)],
                    0xF9 => t.Data[pos..(pos + 4)],
                    0xFA => t.Data[pos..(pos + 2)],
                    _ => t.Data[pos..(pos + 1)]
                };

                instructions.Add(new Instruction
                {
                    Inst = inst,
                    Data = data,
                    Position = pos,
                });

                pos += data.Length;
            }
            return instructions;
        }

        private void AddBlocks(Instruction[] instructions, Track t)
        {
            var fromBlocks = new List<Block>();

            for (int i = instructions.Length; i > 0; i--)
            {
                var newInstructions = instructions[0..i];
                var nextInstruction = (i == instructions.Length) ? 0 : instructions[i].Inst;
                var firstInstruction = newInstructions[0].Inst;

                fromBlocks.Add(new Block
                {
                    Instructions = newInstructions.ToArray(),
                    Position = instructions[0].Position,
                    Track = t,
                    NextInstruction = nextInstruction
                });

                if (newInstructions.Length > 1 && !newInstructions.Any(i => i.Inst == 0x00 || i.Inst == 0xEF) && nextInstruction != 0xF9 && firstInstruction != 0xF9)
                {
                    var data = newInstructions.SelectMany(i => i.Data).ToArray();
                    ulong hash = XXH64.DigestOf(data);
                    var newBlock = new Block
                    {
                        Instructions = newInstructions.ToArray(),
                        Position = instructions[0].Position,
                        Track = t,
                        NextInstruction = nextInstruction
                    };

                    if (_blocks.ContainsKey(hash))
                    {
                        if (!_blocks[hash].Any(b => b.Track.Guid == t.Guid && (b.Position + data.Length) > newBlock.Position))
                        {
                            _blocks[hash].Add(newBlock);
                        }
                    }
                    else
                    {
                        _blocks[hash] = new List<Block> { newBlock };
                    }
                }
            }

            while (fromBlocks.Count > 0) {
                for (int i = 0; i < fromBlocks.Count; i++)
                {
                    var newInstructions = fromBlocks[i].Instructions[1..];
                    var nextInstruction = fromBlocks[i].NextInstruction;
                    

                    if (newInstructions.Length > 0)
                    {
                        var firstInstruction = newInstructions[0].Inst;
                        fromBlocks[i] = new Block
                        {
                            Instructions = newInstructions,
                            Position = newInstructions[0].Position,
                            Track = t,
                            NextInstruction = nextInstruction
                        };

                        if(newInstructions.Length > 1 && !newInstructions.Any(i => i.Inst == 0x00 || i.Inst == 0xEF) && nextInstruction != 0xF9 && firstInstruction != 0xF9)
                        {
                            //var data = newInstructions.SelectMany(i => i.Data).ToArray();
                            var data = t.Data.AsSpan()[newInstructions[0].Position..((newInstructions[^1].Position + newInstructions[^1].Data.Length))];
                            var dataLength = data.Length;
                            ulong hash = XXH64.DigestOf(data);
                            var newBlock = new Block
                            {
                                Instructions = newInstructions,
                                Position = newInstructions[0].Position,
                                Track = t,
                                NextInstruction = nextInstruction
                            };

                            if (_blocks.ContainsKey(hash))
                            {
                                if (!_blocks[hash].Any(b => b.Track.Guid == t.Guid && (b.Position + dataLength) > newBlock.Position))
                                {
                                    _blocks[hash].Add(newBlock);
                                }
                            }
                            else
                            {
                                _blocks[hash] = new List<Block> { newBlock };
                            }
                        }

                    } else
                    {
                        fromBlocks.Remove(fromBlocks[i]);
                    }
                }
            }
        }

        public List<Subroutine> Optimize(int offset, int patternDataEnd, int extendedPatternDataStart)
        {
            HashSet<Guid> processedTracks;
            List<Subroutine> subRoutines = new List<Subroutine>();
            int instructionCount = 0;

            /* Optimize repeated patterns into subroutines until it's no longer efficient */
            while (true)
            {
                instructionCount = 0;
                processedTracks = new HashSet<Guid>();
                _blocks = new Dictionary<ulong, List<Block>>();
                foreach (var p in _module.Patterns)
                {
                    foreach (var t in p.Tracks)
                    {
                        if (!processedTracks.Contains(t.Guid))
                        {
                            var instructions = GetInstructions(t);
                            instructionCount += instructions.Count;
                            AddBlocks(instructions.ToArray(), t);
                            processedTracks.Add(t.Guid);
                        }
                    }
                }

                try
                {
                    var best = _blocks.Where(b => b.Value.Count > 1 && b.Value.First().Instructions.Length > 2 && b.Value.First().Instructions.SelectMany(i => i.Data).Count() > 5).OrderByDescending(b => b.Value.Count * b.Value.Max(bb => bb.Instructions.Length)).First();
                    var data = best.Value.First().Instructions.SelectMany(i => i.Data).ToArray();
                    
                    if(offset < extendedPatternDataStart && (offset + data.Length + 1) >= patternDataEnd)
                    {
                        offset = extendedPatternDataStart;
                    }

                    subRoutines.Add(new Subroutine
                        {
                            Offset = offset,
                            Length = data.Length + 1,
                            Data = data.Concat(new List<byte> { 0 }).ToArray()
                        }
                    );

                    Guid prevTrackGuid = new Guid();
                    int trackOffset = 0;
                    foreach (var b in best.Value.OrderBy(b => b.Track.Guid).ThenBy(b => b.Position))
                    {
                        if(prevTrackGuid != b.Track.Guid)
                        {
                            trackOffset = 0;
                            prevTrackGuid = b.Track.Guid;
                        }

                        try
                        {
                            b.Track.Data = b.Track.Data[..(b.Position - trackOffset)].Concat(new byte[] { 0xEF, (byte)(offset & 0xFF), (byte)((offset >> 8) & 0xFF), 1 }).Concat(b.Track.Data[(b.Position + data.Length - trackOffset)..]).ToArray();
                        } catch
                        {
                            break;
                        }
                        trackOffset += data.Length - 4;
                    }
                    
                    offset += data.Length + 1;
                }
                catch
                {
                    break;
                }
            }

            /* Find repeated calls to subroutines and merge them */
            processedTracks = new HashSet<Guid>();
            foreach (var p in _module.Patterns)
            {
                foreach (var t in p.Tracks)
                {
                    if (!processedTracks.Contains(t.Guid))
                    {
                        var instructions = GetInstructions(t);
                        int startPos = 0;
                        int repCount = 0;
                        int repOffset = 0;
                        int repAddr = 0;
                        foreach (var instr in instructions)
                        {
                            if (instr.Inst == 0xEF && (repCount == 0 || (instr.Data[1] | (instr.Data[2] << 8)) == repAddr))
                            {
                                if (repCount == 0)
                                {
                                    startPos = instr.Position;
                                    repAddr = instr.Data[1] | (instr.Data[2] << 8);
                                }
                                repCount++;
                            }
                            else
                            {
                                if (repCount > 1)
                                {
                                    /* There's a repeated subroutine call */
                                    t.Data[startPos - repOffset + 3] = (byte)repCount;
                                    t.Data = t.Data[..(startPos - repOffset + 4)].Concat(t.Data[(startPos - repOffset + (repCount * 4))..]).ToArray();
                                    repOffset += ((repCount - 1) * 4);
                                }

                                repCount = 0;
                                startPos = 0;
                                repAddr = 0;
                            }
                        }
                    }
                }
            }

            return subRoutines;
        }

    }
}
