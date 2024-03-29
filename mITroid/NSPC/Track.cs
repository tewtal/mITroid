﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mITroid.NSPC
{
    enum EventType
    {
        Instrument,
        Volume,
        Effect,
        Note,
    }

    enum ITEffect
    {
        Vibrato = 'H',
        Tremolo = 'R',
        Volume = 'M',
        Speed = 'A',
        Tempo = 'T',
        Pan = 'X',
        VolumeSlide = 'D',
        VolumeSlideVibrato = 'K',
        VolumeSlidePortamento = 'L',
        NotePortamento = 'G',
        Special = 'S',
        Zpecial = 'Z',
        PortamentoUp = 'F',
        PortamentoDown = 'E',
        PatternBreak = 'C'        
    }

    enum Effect
    {
        Volume = 0xED,
        VolumeSlide = 0xEE,
        Vibrato = 0xE3,
        VibratoOff = 0xE4,
        Tremolo = 0xEB,
        TremoloOff = 0xEC,
        Speed = 0xFF,
        Tempo = 0xE7,
        Pan = 0xE2,
        PortamentoUp = 0xF1,
        PortamentoDown = 0xF2,
        PortamentoOff = 0xF3,
        NotePortamento = 0xF9,
        Instrument = 0xE0,
        Special = 0xFE,
        Zpecial = 0xFD,
        PatternBreak = 0xFC
    }

    class Event
    {
        public EventType Type { get; set; }
        public int Value { get; set; }
        public List<int> Parameters { get; set; }
        public int Row { get; set; }
        public bool Processed { get; set; }

        public static readonly byte[] VolumeLUT = new byte[]
                                                  { 0, 10, 13, 16, 18, 20, 21, 23,
                                                   25, 26, 27, 28, 29, 30, 31, 32,
                                                   33, 34, 35, 36, 37, 38, 39, 40,
                                                   41, 42, 43, 44, 45, 46, 47, 48,
                                                   49, 50, 51, 52, 53, 54, 55, 56,
                                                   56, 57, 57, 58, 58, 59, 59, 60,
                                                   60, 61, 61, 62, 62, 62, 62, 63,
                                                   63, 63, 63, 64, 64, 64, 64, 64};
                                                



        public Event()
        {
            Processed = false;
            Row = 0;
        }

        public static List<Event> CreateEffect(IT.Row itRow)
        {
            List<Event> events = new List<Event>();

            var ev = new Event
            {
                Row = itRow.RowNum,
                Type = EventType.Effect
            };

            switch ((ITEffect)(itRow.Command + 64))
            {
                case ITEffect.Vibrato:
                    {
                        int rate = (itRow.Value >> 4);
                        int depth = (itRow.Value & 0x0f);
                        ev.Value = (int)Effect.Vibrato;
                        ev.Parameters = new List<int>() { rate * 4, depth * 16 };
                        break;
                    }

                case ITEffect.Tremolo:
                    {
                        int rate = (itRow.Value >> 4);
                        int depth = (itRow.Value & 0x0f);
                        ev.Value = (int)Effect.Tremolo;
                        ev.Parameters = new List<int>() { rate * 4, depth * 16 };
                        break;
                    }

                case ITEffect.Volume:
                    {
                        ev.Value = (int)Effect.Volume;
                        ev.Parameters = new List<int>() { itRow.Value };  //ConvertVolume(itRow.Value) };
                        break;
                    }

                case ITEffect.Speed:
                    {
                        ev.Value = (int)Effect.Speed;
                        ev.Parameters = new List<int>() { itRow.Value };
                        break;
                    }

                case ITEffect.Tempo:
                    {
                        ev.Value = (int)Effect.Tempo;
                        ev.Parameters = new List<int>() { itRow.Value };
                        break;
                    }

                case ITEffect.Pan:
                    {
                        ev.Value = (int)Effect.Pan;
                        ev.Parameters = new List<int>() { (int)(0x14 - (itRow.Value / 12.8)) };
                        break;
                    }

                case ITEffect.VolumeSlide:
                    {
                        ev.Value = (int)Effect.VolumeSlide;
                        ev.Parameters = new List<int>() { 1, itRow.Value };
                        break;
                    }
                case ITEffect.VolumeSlideVibrato:
                    {
                        ev.Value = (int)Effect.VolumeSlide;
                        ev.Parameters = new List<int>() { 1, itRow.Value };

                        var ev2 = new Event
                        {
                            Row = itRow.RowNum,
                            Type = EventType.Effect,
                            Value = (int)Effect.Vibrato,
                            Parameters = new List<int>() { 0, 0 }
                        };

                        events.Add(ev2);
                        break;
                    }
                case ITEffect.VolumeSlidePortamento:
                    {
                        ev.Value = (int)Effect.VolumeSlide;
                        ev.Parameters = new List<int>() { 1, itRow.Value };

                        var ev2 = new Event
                        {
                            Row = itRow.RowNum,
                            Type = EventType.Effect,
                            Value = (int)Effect.NotePortamento,
                            Parameters = new List<int>() { 0 }
                        };

                        events.Add(ev2);
                        break;
                    }
                case ITEffect.PortamentoDown:
                    {
                        ev.Value = (int)Effect.PortamentoDown;
                        ev.Parameters = new List<int>() { itRow.Value };
                        break;
                    }
                case ITEffect.PortamentoUp:
                    {
                        ev.Value = (int)Effect.PortamentoUp;
                        ev.Parameters = new List<int>() { itRow.Value };
                        break;
                    }
                case ITEffect.NotePortamento:
                    {
                        ev.Value = (int)Effect.NotePortamento;
                        ev.Parameters = new List<int>() { itRow.Value };
                        break;
                    }                
                case ITEffect.Special:
                    {
                        int sub_cmd = (itRow.Value >> 4);
                        int sub_val = (itRow.Value & 0xF);
                        ev.Value = (int)Effect.Special;
                        ev.Parameters = new List<int>() { sub_cmd, sub_val };
                        break;
                    }
                case ITEffect.Zpecial:
                    {
                        ev.Value = (int)Effect.Zpecial;
                        ev.Parameters = new List<int>() { itRow.Value };
                        break;
                    }
                case ITEffect.PatternBreak:
                    {
                        ev.Value = (int)Effect.PatternBreak;
                        ev.Parameters = new List<int>() { itRow.Value };
                        break;
                    }
            }

            events.Add(ev);
            return events;
        }

        public static int ConvertVolume(int val)
        {
            int vp = (val == 0) ? 0 : VolumeLUT[val - 1];
            return vp;
        }
    }

    class EffectMemory
    {
        public int Portamento { get; set; }
        public int InstrumentIndex { get; set; }
        public int NoteVolume { get; set; }
        public int Volume { get; set; }
        public int VolumeSlide { get; set; }
        public int VibratoRate { get; set; }
        public int VibratoDepth { get; set; }
        public int TremoloRate { get; set; }
        public int TremoloDepth { get; set; }
        public int PortamentoTarget { get; set; }

        public int Note { get; set; }

        public EffectMemory()
        {
            Volume = -1;
            NoteVolume = -1;
            InstrumentIndex = -1;
            Note = -1;
        }
    }


    class Track
    {
        public static EffectMemory[] Memory;

        public int Rows { get; set; }
        public int Pointer { get; set; }
        public List<Event> Events { get; set; }
        public byte[] Data { get; set; }
        public int Channel { get; set; }
        public Guid Guid { get; set; }

        public Track(List<IT.Row> itRows, int channel)
        {
            Guid = Guid.NewGuid();
            Channel = channel;
            Events = new List<Event>();
            foreach(var row in itRows)
            {
                if(row.Note != -1)
                {
                    var noteEvent = new Event
                    {
                        Type = EventType.Note,
                        Row = row.RowNum,
                    };

                    if (row.Note == 254)
                    {
                        noteEvent.Value = 0xC9;
                    }
                    else if (row.Note == 255)
                    {
                        noteEvent.Value = 0xFF;
                    }
                    else if (row.Note > 119)
                    {
                        noteEvent.Value = 0xC8;
                    }
                    else
                    {
                        noteEvent.Value = row.Note + (104);
                        if (noteEvent.Value > 199)
                            noteEvent.Value = 199;

                        if (noteEvent.Value < 128)
                            noteEvent.Value = 128;
                    }

                    Events.Add(noteEvent);
                }

                if(row.Instrument != -1)
                {
                    var instrumentEvent = new Event
                    {
                        Type = EventType.Instrument,
                        Value = row.Instrument - 1,
                        Row = row.RowNum
                    };
                    Events.Add(instrumentEvent);

                    if(row.Note == -1)
                    {
                        var noteEvent = new Event
                        {
                            Type = EventType.Note,
                            Row = row.RowNum,
                            Value = -1
                        };
                        Events.Add(noteEvent);
                    }
                }

                if(row.Volume != -1)
                {
                    if (row.Volume <= 64)
                    {
                        var volumeEvent = new Event
                        {
                            Type = EventType.Volume,
                            Value = row.Volume, //Event.ConvertVolume(row.Volume),
                            Row = row.RowNum
                        };
                        Events.Add(volumeEvent);
                    }
                    else
                    {
                        if(row.Volume >= 128 && row.Volume <= 192)
                        {
                            int panValue = row.Volume == 128 ? 0 : ((row.Volume - 128) * 4) - 1;
                            var ev = new Event
                            {
                                Row = row.RowNum,
                                Type = EventType.Effect,
                                Value = (int)Effect.Pan,
                                Parameters = new List<int>() { (int)(0x14 - (panValue / 12.8)) }
                            };

                            Events.Add(ev);
                        }
                        else if(row.Volume >= 65 && row.Volume <= 104)
                        {
                            int volValue = 0;

                            if(row.Volume <= 74)
                            {
                                volValue = ((row.Volume - 65) << 4) + 0xF;
                            }
                            else if (row.Volume >= 75 && row.Volume <= 84)
                            {
                                volValue = (row.Volume - 75) + 0xF0;
                            }
                            else if (row.Volume >= 85 && row.Volume <= 94)
                            {
                                volValue = ((row.Volume - 85) << 4);
                            }
                            else if (row.Volume >= 95 && row.Volume <= 104)
                            {
                                volValue = (row.Volume - 95);
                            }

                            var ev = new Event
                            {
                                Row = row.RowNum,
                                Type = EventType.Effect,
                                Value = (int)Effect.VolumeSlide,
                                Parameters = new List<int>() { 1, volValue }
                            };

                            Events.Add(ev);
                        }
                        else if(row.Volume >= 105 && row.Volume <= 124)
                        {
                            int portValue = 0;

                            if(row.Volume <= 114)
                            {
                                portValue = (row.Volume - 105) * 4;
                            }
                            else
                            {
                                portValue = (row.Volume - 115) * 4;
                            }

                            var ev = new Event
                            {
                                Row = row.RowNum,
                                Type = EventType.Effect,
                                Value = (row.Volume <= 114) ? (int)Effect.PortamentoDown : (int)Effect.PortamentoUp,
                                Parameters = new List<int>() { portValue }
                            };

                            Events.Add(ev);
                        }
                        else if(row.Volume >= 193 && row.Volume <= 202)
                        {
                            var portTable = new int[] { 0,  1, 4, 8, 16, 32, 64, 96, 128, 255 };
                            int portValue = portTable[row.Volume - 193];

                            var ev = new Event
                            {
                                Row = row.RowNum,
                                Type = EventType.Effect,
                                Value = (int)Effect.NotePortamento,
                                Parameters = new List<int>() { portValue }
                            };

                            Events.Add(ev);
                        }
                        else if(row.Volume >= 203)
                        {
                            var ev = new Event
                            {
                                Row = row.RowNum,
                                Type = EventType.Effect,
                                Value = (int)Effect.Vibrato,
                                Parameters = new List<int>() { 0, (row.Volume - 203) * 16 }
                            };

                            Events.Add(ev);
                        }
                    }
                }

                if(row.Command != -1)
                {                    
                    Events.AddRange(Event.CreateEffect(row));
                }
            }
        }

        private byte Vol(int vol, Instrument i, Module mod)
        {
            int v = 0;
            if (i != null)
            {
                v = (((vol * i.SampleVolume * i.InstrumentVolume * mod.ChannelVolume[Channel]) / 131072) - 1);
            }
            else
            {
                v = (((vol * 64 * 128 * mod.ChannelVolume[Channel]) / 131072) - 1);
            }

            v = (v != 0xFF) ? v + 1 : v;

            return (byte)Math.Round(Math.Sqrt(256 * v), 0);
        }

        /* This function takes a track and encodes it as the N-SPC output stream */
        public void GenerateData(Module module)
        {
            List<byte> byteList = new List<byte>();
            int noteLength = 0;
            int volumeSlide = 0;
            int volume = 0xFE;
            int instrument = -1;
            int novolumechange = 0;
            int notedelay = 0;
            int lastnote = 0;
            int patternLength = 0;
            int lastnotevol = 0;
            int portamentoTarget = 0;
            int vibrato = 0;
            int portamento = 0;

            Instrument nI = null;

            if(Memory[Channel].InstrumentIndex >= 0)
            {
                nI = module.Instruments[Memory[Channel].InstrumentIndex];
            }

            if(nI == null)
            {
                nI = new Instrument() { DefaultVolume = 64, InstrumentVolume = 128, SampleVolume = 64 };
            }

            lastnotevol = Memory[Channel].NoteVolume;
            volume = Memory[Channel].Volume;

            if (portamentoTarget > 0)
            {
                portamentoTarget = Memory[Channel].PortamentoTarget;
            }

            if (Memory[Channel].Note >= 0)
            {
                lastnote = Memory[Channel].Note;
            }

            if (Events.Count == 0)
            {
                Data = byteList.ToArray();
                return;
            }


            int firstRow = Events.Min(x => x.Row);

            /* Generate blank data until the first row of events */
            if (firstRow > 0)
            {
                int row = 0;
                while (true)
                {
                    noteLength = module.CurrentSpeed * (firstRow - row);
                    if(noteLength > 0x7F)
                    {
                        noteLength = ((int)Math.Floor((double)0x7F / (double)module.CurrentSpeed)) * module.CurrentSpeed;
                    }

                    patternLength += noteLength;
                    byteList.Add((byte)noteLength);
                    byteList.Add(0xC8);
                    if (row + (noteLength/module.CurrentSpeed) == firstRow)
                        break;

                    row += (noteLength / module.CurrentSpeed);
                }
            }

            for(int row = firstRow; row < Rows; row++)
            {
                var iEvent = Events.Where(x => x.Row == row && x.Type == EventType.Instrument && x.Processed == false).FirstOrDefault();
                var nEvent = Events.Where(x => x.Row == row && x.Type == EventType.Note && x.Processed == false).FirstOrDefault();
                var vEvent = Events.Where(x => x.Row == row && x.Type == EventType.Volume && x.Processed == false).FirstOrDefault();
                var eEvents = Events.Where(x => x.Row == row && x.Type == EventType.Effect && x.Processed == false).ToList();

                if (eEvents.Any(x => (Effect)x.Value == Effect.PatternBreak))
                {
                    Rows = row;
                    break;
                }

                if (nEvent != null)
                {
                    if (nEvent.Value == -1 && lastnote >= 0)
                    {
                        nEvent.Value = lastnote;
                    }
                }

                if (iEvent != null)
                {
                    if (instrument != iEvent.Value || nI == null)
                    {
                        byteList.Add((byte)Effect.Instrument);

                        try
                        {
                            nI = module.Instruments[iEvent.Value];
                        }
                        catch
                        {
                            nI = null;
                        }

                        if (nI != null)
                        {
                            byteList.Add((byte)nI.InstrumentIndex);
                            iEvent.Processed = true;
                            instrument = iEvent.Value;
                            Memory[Channel].InstrumentIndex = instrument;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                if(vEvent != null)
                {
                    if(nEvent != null)
                    {
                        if (vEvent.Value != volume)
                        {
                            if (eEvents.Count == 0 || eEvents.Any(x => (Effect)x.Value == Effect.NotePortamento) == false)
                            {
                                if (volumeSlide == 1)
                                {
                                    byteList.Add((byte)Effect.VolumeSlide);
                                    byteList.Add((byte)0x01);
                                    //byteList.Add((byte)vEvent.Value);
                                    byteList.Add(Vol(vEvent.Value, nI, module));
                                    vEvent.Processed = true;
                                    volume = vEvent.Value;
                                }
                                else
                                {
                                    byteList.Add((byte)Effect.Volume);
                                    byteList.Add(Vol(vEvent.Value, nI, module));
                                    vEvent.Processed = true;
                                    volume = vEvent.Value;
                                }
                            }
                        }
                    }
                    else
                    {
                        /* Treat this as a volume slide, try to merge with upcoming volume changes */
                        /* First we find the next note */
                        var nextNote = Events.OrderBy(x => x.Row).Where(x => x.Row > row && x.Type == EventType.Note).FirstOrDefault();
                        int nextNotePos = (nextNote != null ? nextNote.Row : Rows);
                        int searchLength = 8; /* Search at most 8 rows ahead before bailing out */
                        int targetRow = 0;
                        int targetVolume = vEvent.Value;
                        int slideDir = 0;

                        for(int search = (row + 1); search < nextNotePos; search++)
                        {
                            var vSearch = Events.OrderBy(x => x.Row).Where(x => x.Row == search && x.Type == EventType.Volume && x.Processed == false).FirstOrDefault();
                            if(vSearch != null)
                            {
                                if (slideDir == 0)
                                {
                                    if (vSearch.Value < targetVolume)
                                    {
                                        slideDir = 1;
                                    }
                                    else if (vSearch.Value > targetVolume)
                                    {
                                        slideDir = 2;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    if(vSearch.Value < targetVolume && slideDir == 2)
                                    {
                                        break;
                                    }
                                    else if(vSearch.Value > targetVolume && slideDir == 1)
                                    {
                                        break;
                                    }
                                }

                                targetRow = search;
                                targetVolume = vSearch.Value;
                                searchLength = 8;
                                vSearch.Processed = true;

                                if ((((targetRow+1) - row) * module.CurrentSpeed) > 0xFF)
                                    break;
                            }
                            else
                            {
                                searchLength--;
                                if (searchLength == 0)
                                    break;
                            }
                        }
                        if (targetVolume != volume)
                        {
                            if (targetRow > 0)
                            {
                                if(vEvent.Value != volume)
                                {
                                    byteList.Add((byte)Effect.Volume);
                                    byteList.Add(Vol(vEvent.Value, nI, module));
                                }

                                int fadeRows = (targetRow - row);
                                byteList.Add((byte)Effect.VolumeSlide);
                                byteList.Add((byte)(fadeRows * module.CurrentSpeed));
                                byteList.Add(Vol(targetVolume, nI, module));
                            }
                            else
                            {
                                byteList.Add((byte)Effect.VolumeSlide);
                                byteList.Add((byte)0x01);
                                byteList.Add(Vol(targetVolume, nI, module));
                            }
                            volume = targetVolume;
                        }
                        volumeSlide = 1;
                        vEvent.Processed = true;
                    }
                }

                List<byte> preEffectList = new List<byte>();
                List<byte> effectList = new List<byte>();

                foreach (var eEvent in eEvents)
                {
                    if (eEvent != null)
                    {
                        switch ((Effect)eEvent.Value)
                        {
                            case Effect.Pan:
                                {
                                    preEffectList.Add((byte)Effect.Pan);
                                    preEffectList.Add((byte)0x01);
                                    preEffectList.Add((byte)eEvent.Parameters[0]);
                                    break;
                                }

                            case Effect.Tempo:
                                {
                                    byteList.Add((byte)Effect.Tempo);
                                    byteList.Add((byte)Math.Round(eEvent.Parameters[0] / (4.8m / module.EngineSpeed), 0));
                                    module.CurrentTempo = (int)Math.Round((eEvent.Parameters[0] / (4.8m / module.EngineSpeed)), 0);
                                    break;
                                }

                            case Effect.Speed:
                                {
                                    int newSpeed = (int)(eEvent.Parameters[0] * module.EngineSpeed);
                                    double factor = ((double)newSpeed / (double)module.CurrentSpeed);
                                    int newTempo = (int)Math.Round(((double)module.CurrentTempo / factor));
                                    byteList.Add((byte)Effect.Tempo);
                                    byteList.Add((byte)newTempo);
                                    break;
                                }
                            case Effect.Volume:
                                {
                                    if (eEvent.Parameters[0] != module.ChannelVolume[Channel])
                                    {
                                        module.ChannelVolume[Channel] = eEvent.Parameters[0];
                                        if (volumeSlide == 1)
                                        {
                                            preEffectList.Add((byte)Effect.VolumeSlide);
                                            preEffectList.Add((byte)0x01);
                                            preEffectList.Add(Vol(volume, nI, module));
                                        }
                                        else
                                        {
                                            byteList.Add((byte)Effect.Volume);
                                            preEffectList.Add(Vol(volume, nI, module));
                                        }
                                    }
                                    break;
                                }
                            case Effect.NotePortamento:
                                {

                                    if (vEvent == null)
                                    {
                                        if (nEvent == null || (nEvent != null && iEvent == null))
                                        {
                                            if (volume != lastnotevol)
                                            {
                                                volume = lastnotevol;
                                                preEffectList.Add((byte)Effect.VolumeSlide);
                                                preEffectList.Add((byte)0x01);
                                                preEffectList.Add(Vol(volume, nI, module));
                                                volumeSlide = 0;
                                            }
                                        }
                                        else
                                        {
                                            if (volume != nI.DefaultVolume)
                                            {
                                                volume = nI.DefaultVolume;
                                                preEffectList.Add((byte)Effect.VolumeSlide);
                                                preEffectList.Add((byte)0x01);
                                                preEffectList.Add(Vol(nI.DefaultVolume, nI, module));
                                                volumeSlide = 0;
                                            }

                                        }
                                    }
                                    else
                                    {
                                        preEffectList.Add((byte)Effect.VolumeSlide);
                                        preEffectList.Add((byte)0x01);
                                        preEffectList.Add(Vol(vEvent.Value, nI, module));
                                        volumeSlide = 0;
                                        volume = vEvent.Value;
                                        lastnotevol = volume;
                                    }

                                    if (nEvent != null && nEvent.Value <= 199)
                                    {
                                        portamentoTarget = nEvent.Value;
                                        nEvent.Processed = true;
                                        nEvent = null;

                                    }
                                    else
                                    {
                                        if (portamentoTarget == 0)
                                        {
                                            break;
                                        }
                                    }

                                    int val = eEvent.Parameters[0];
                                    if (val == 0 && Memory[Channel].Portamento >= 0)
                                    {
                                        val = Memory[Channel].Portamento;
                                    }
                                    else
                                    {
                                        Memory[Channel].Portamento = val;
                                    }

                                    int effectRows = 1;

                                    /* Scan for future note portamento effects and process them */
                                    var nextNote = Events.OrderBy(x => x.Row).Where(x => x.Row > row && x.Type == EventType.Note).FirstOrDefault();
                                    int nextNotePos = (nextNote != null ? nextNote.Row : Rows);
                                    for (int search = (row + 1); search < nextNotePos; search++)
                                    {
                                        var vSearch = Events.OrderBy(x => x.Row).Where(x => x.Row == search && x.Type == EventType.Effect && x.Value == (int)Effect.NotePortamento && x.Processed == false).FirstOrDefault();
                                        if (vSearch != null)
                                        {
                                            if (vSearch.Parameters[0] == val || vSearch.Parameters[0] == 0x00)
                                            {
                                                vSearch.Processed = true;
                                                effectRows++;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                    double semitonesPerTick = (val * (1.0 / 16.0));
                                    int semitones = ((portamentoTarget > lastnote) ? (portamentoTarget - lastnote) : (lastnote - portamentoTarget));
                                    double semitoneTicks = Math.Floor(semitones / semitonesPerTick);

                                    int ticksPerRow = module.CurrentSpeed;
                                    int effectTicks = effectRows * ticksPerRow;

                                    int newNote = portamentoTarget;

                                    if(semitoneTicks > effectTicks)
                                    {
                                        int calcSemitones = (int)(semitonesPerTick * effectTicks);
                                        newNote = (int)((portamentoTarget > lastnote) ? (lastnote + calcSemitones) : (lastnote - calcSemitones));
                                        semitoneTicks = (effectTicks - 1);
                                    }

                                    if (portamentoTarget > lastnote && newNote > portamentoTarget)
                                    {
                                        newNote = portamentoTarget;
                                    }
                                    else if (portamentoTarget < lastnote && newNote < portamentoTarget)
                                    {
                                        newNote = portamentoTarget;
                                    }
                                        
                                    effectList.Add((byte)Effect.NotePortamento);
                                    effectList.Add((byte)0x00);
                                    effectList.Add((byte)(semitoneTicks + 1));
                                    effectList.Add((byte)newNote);
                                    lastnote = newNote;

                                    /* Vibrato special thing test */
                                    nextNote = Events.OrderBy(x => x.Row).Where(x => x.Row > row && x.Type == EventType.Note).FirstOrDefault();
                                    nextNotePos = (nextNote != null ? nextNote.Row : Rows);
                                    int startRow = 0;
                                    effectRows = 0;
                                    int firstEffect = 0;
                                    int rate = 0;
                                    int depth = 0;
                                    Event lastEvent = null;
                                    for (int search = (row + 1); search < nextNotePos; search++)
                                    {
                                        var curEvent = Events.OrderBy(x => x.Row).Where(x => x.Row == search && x.Type == EventType.Effect && (x.Value == (int)Effect.Vibrato) && x.Processed == false).FirstOrDefault();
                                        if (curEvent != null)
                                        {
                                            if (startRow == 0)
                                            {
                                                if (curEvent.Parameters[0] == 0)
                                                {
                                                    curEvent.Parameters[0] = Memory[Channel].VibratoRate;
                                                }
                                                else
                                                {
                                                    Memory[Channel].VibratoRate = curEvent.Parameters[0];
                                                }

                                                if (curEvent.Parameters[1] == 0)
                                                {
                                                    curEvent.Parameters[1] = Memory[Channel].VibratoDepth;
                                                }
                                                else
                                                {
                                                    Memory[Channel].VibratoDepth = curEvent.Parameters[1];
                                                }

                                                startRow = search;
                                                firstEffect = curEvent.Value;
                                                rate = curEvent.Parameters[0];
                                                depth = curEvent.Parameters[1];
                                            }

                                            effectRows++;
                                            curEvent.Processed = true;
                                            lastEvent = curEvent;
                                        }
                                        else
                                        {
                                            if (startRow > 0)
                                                break;
                                        }
                                    }

                                    if (startRow > 0)
                                    {
                                        preEffectList.Add((byte)Effect.Vibrato);
                                        preEffectList.Add((byte)((startRow - row) * module.CurrentSpeed));
                                        preEffectList.Add((byte)(rate / module.EngineSpeed));
                                        preEffectList.Add((byte)depth);

                                        lastEvent.Type = EventType.Effect;
                                        lastEvent.Processed = false;
                                        lastEvent.Value = (int)Effect.VibratoOff;
                                        lastEvent.Parameters = null;
                                    }


                                    break;
                                }
                            case Effect.VibratoOff:
                                {
                                    preEffectList.Add((byte)Effect.VibratoOff);
                                    break;
                                }
                            case Effect.Tremolo:
                                {
                                    if (eEvent.Parameters[0] != 0 || eEvent.Parameters[1] != 0)
                                    {
                                        effectList.Add((byte)Effect.Tremolo);
                                        effectList.Add((byte)0x00);
                                        effectList.Add((byte)eEvent.Parameters[0]);
                                        effectList.Add((byte)eEvent.Parameters[1]);
                                    }

                                    /* Scan for future note vibrato effects and process them */
                                    var nextNote = Events.OrderBy(x => x.Row).Where(x => x.Row > row && x.Type == EventType.Note).FirstOrDefault();
                                    int nextNotePos = (nextNote != null ? nextNote.Row : Rows);
                                    Event lastSearch = null;
                                    for (int search = (row + 1); search < nextNotePos; search++)
                                    {
                                        var curSearch = Events.OrderBy(x => x.Row).Where(x => x.Row == search && x.Type == EventType.Effect && x.Value == (int)Effect.Tremolo && x.Processed == false).FirstOrDefault();
                                        if (curSearch != null)
                                        {
                                            curSearch.Processed = true;
                                            lastSearch = curSearch;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                    /* Set the last effect back as processed and change it to a vibrato off, and push it one row further */
                                    if (lastSearch != null)
                                    {
                                        lastSearch.Processed = false;
                                        lastSearch.Type = EventType.Effect;
                                        lastSearch.Value = (int)Effect.TremoloOff;
                                        lastSearch.Row = lastSearch.Row + 1;
                                    }

                                    break;
                                }
                            case Effect.TremoloOff:
                                {
                                    preEffectList.Add((byte)Effect.TremoloOff);
                                    break;
                                }
                            case Effect.VolumeSlide:
                                {
                                    int val = eEvent.Parameters[1];
                                    if (val == 0)
                                    {
                                        val = Memory[Channel].VolumeSlide;
                                    }
                                    else
                                    {
                                        Memory[Channel].VolumeSlide = val;
                                    }

                                    int slideDirection, volumeChange, volumeChangePerRow;

                                    if (((val & 0x0F) == 0x0F && (val & 0xF0) != 0) || ((val & 0xF0) == 0xF0 && (val & 0x0F) != 0))
                                    {
                                        /* Fine volume slide */
                                        if ((val & 0xF0) == 0xF0)
                                        {
                                            /* Down slide */
                                            slideDirection = 0;
                                            volumeChange = val & 0x0F;
                                        }
                                        else
                                        {
                                            slideDirection = 1;
                                            volumeChange = (val >> 4) & 0x0F;
                                        }
                                        volumeChangePerRow = volumeChange;
                                    }
                                    else
                                    {
                                        /* Regular volume slide */
                                        if ((val & 0xF0) == 0)
                                        {
                                            /* Down slide */
                                            slideDirection = 0;
                                            volumeChange = val & 0x0F;
                                        }
                                        else
                                        {
                                            slideDirection = 1;
                                            volumeChange = (val >> 4) & 0x0F;
                                        }
                                        volumeChangePerRow = (volumeChange * (module.CurrentSpeed - 1));
                                    }

                                    int targetVolumeChange = (slideDirection == 0 ? -volumeChangePerRow : volumeChangePerRow);
                                    int targetVolumeChangeRows = 1;
                                    var nextNote = Events.OrderBy(x => x.Row).Where(x => x.Row > row && x.Type == EventType.Note).FirstOrDefault();
                                    int nextNotePos = (nextNote != null ? nextNote.Row : Rows);
                                    for (int search = (row + 1); search < nextNotePos; search++)
                                    {
                                        var lastSearch = Events.OrderBy(x => x.Row).Where(x => x.Row == search && x.Type == EventType.Effect && x.Value == (int)Effect.VolumeSlide && x.Processed == false).FirstOrDefault();
                                        if (lastSearch != null)
                                        {
                                            if (((targetVolumeChangeRows + 1) * module.CurrentSpeed) > 0xFF)
                                                break;

                                            if (lastSearch.Parameters[1] == 0)
                                            {
                                                lastSearch.Processed = true;
                                                targetVolumeChange += (slideDirection == 0 ? -volumeChangePerRow : volumeChangePerRow);
                                                targetVolumeChangeRows += 1;
                                            }
                                            else
                                            {
                                                int newVal = lastSearch.Parameters[1];

                                                int newSlideDirection, newVolumeChange, newVolumeChangePerRow;

                                                if (((newVal & 0x0F) == 0x0F && (newVal & 0xF0) != 0) || ((newVal & 0xF0) == 0xF0 && (newVal & 0x0F) != 0))
                                                {
                                                    /* Fine volume slide */
                                                    if ((newVal & 0xF0) == 0xF0)
                                                    {
                                                        /* Down slide */
                                                        newSlideDirection = 0;
                                                        newVolumeChange = newVal & 0x0F;
                                                    }
                                                    else
                                                    {
                                                        newSlideDirection = 1;
                                                        newVolumeChange = (newVal >> 4) & 0x0F;
                                                    }
                                                    newVolumeChangePerRow = newVolumeChange;
                                                }
                                                else
                                                {
                                                    /* Regular volume slide */
                                                    if ((newVal & 0xF0) == 0)
                                                    {
                                                        /* Down slide */
                                                        newSlideDirection = 0;
                                                        newVolumeChange = newVal & 0x0F;
                                                    }
                                                    else
                                                    {
                                                        newSlideDirection = 1;
                                                        newVolumeChange = (newVal >> 4) & 0x0F;
                                                    }
                                                    newVolumeChangePerRow = (newVolumeChange * (module.CurrentSpeed - 1));
                                                }

                                                if (newSlideDirection != slideDirection)
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    volumeChange = newVolumeChange;
                                                    volumeChangePerRow = newVolumeChangePerRow;

                                                    lastSearch.Processed = true;
                                                    targetVolumeChange += (slideDirection == 0 ? -volumeChangePerRow : volumeChangePerRow);
                                                    targetVolumeChangeRows += 1;

                                                    Memory[Channel].VolumeSlide = newVal;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        if ((targetVolumeChangeRows * module.CurrentSpeed) > 0xFF)
                                            break;

                                    }

                                    // Special case where a volume slide is only done for one row
                                    // We reduce it because it's normally supposed to start one tick after note-on
                                    // but we can't do that, so that causes notes to fade out too fast
                                    if (targetVolumeChangeRows == 1 && nextNotePos == (row + 1))
                                    {
                                        targetVolumeChange = (int)(targetVolumeChange * 0.5);
                                    }
                                    else if(targetVolumeChangeRows == 1 && nextNotePos != (row + 1))
                                    {
                                        targetVolumeChangeRows++;
                                    }

                                    if (nEvent != null && vEvent == null)
                                    {
                                        novolumechange = 1;
                                        byteList.Add((byte)Effect.Volume);
                                        byteList.Add(Vol(nI.DefaultVolume, nI, module));
                                        volume = nI.DefaultVolume;
                                    }

                                    int finalVolume = (volume + targetVolumeChange);

                                    if (finalVolume < 0)
                                        finalVolume = 0;

                                    if (finalVolume > 0x40)
                                        finalVolume = 0x40;

                                    preEffectList.Add((byte)Effect.VolumeSlide);
                                    preEffectList.Add((byte)((targetVolumeChangeRows * module.CurrentSpeed)));
                                    preEffectList.Add(Vol(finalVolume, nI, module));
                                    volume = finalVolume;
                                    //volumeSlide = 1;
                                    break;
                                }
                            case Effect.Special:
                                {
                                    int sub_cmd = eEvent.Parameters[0];
                                    int sub_val = eEvent.Parameters[1];

                                    switch (sub_cmd)
                                    {
                                        case 0x0D: /* note delay by sub_val ticks */
                                            {
                                                if (sub_val >= module.CurrentSpeed)
                                                {
                                                    sub_val = module.CurrentSpeed - 1;
                                                }

                                                notedelay = (int)(sub_val * module.EngineSpeed);
                                                preEffectList.Add((byte)notedelay);
                                                preEffectList.Add((byte)0xC8);
                                                break;
                                            }
                                        case 0x0C: /* note cut after sub_val ticks */
                                            {

                                                if (nEvent == null)
                                                {
                                                    if (sub_val >= module.CurrentSpeed)
                                                    {
                                                        sub_val = module.CurrentSpeed - 1;
                                                    }

                                                    notedelay = (int)(sub_val * module.EngineSpeed);
                                                    nEvent = new Event() { Type = EventType.Note, Processed = false, Row = row, Value = 0xC9 };
                                                }
                                                else
                                                {
                                                    sub_val++;

                                                    if (sub_val >= module.CurrentSpeed)
                                                    {
                                                        sub_val = module.CurrentSpeed - 1;
                                                    }

                                                    notedelay = -(int)(sub_val * module.EngineSpeed);
                                                }
                                                break;
                                            }
                                        case 0x00: /* Special commands */
                                            {
                                                switch (sub_val)
                                                {
                                                    case 0x05:
                                                        {
                                                            /* Echo bits / volume */
                                                            var zEchoBits = Events.Where(x => x.Row == row + 1 && x.Type == EventType.Effect && x.Value == (int)Effect.Zpecial).FirstOrDefault();
                                                            var zVolLeft = Events.Where(x => x.Row == row + 2 && x.Type == EventType.Effect && x.Value == (int)Effect.Zpecial).FirstOrDefault();
                                                            var zVolRight = Events.Where(x => x.Row == row + 3 && x.Type == EventType.Effect && x.Value == (int)Effect.Zpecial).FirstOrDefault();

                                                            if (zEchoBits != null && zVolLeft != null && zVolRight != null)
                                                            {
                                                                byteList.Add((byte)0xF5);
                                                                byteList.Add((byte)zEchoBits.Parameters[0]);
                                                                byteList.Add((byte)zVolLeft.Parameters[0]);
                                                                byteList.Add((byte)zVolRight.Parameters[0]);

                                                                zEchoBits.Processed = true;
                                                                zVolLeft.Processed = true;
                                                                zVolRight.Processed = true;
                                                            }

                                                            break;
                                                        }
                                                    case 0x06:
                                                        {
                                                            /* stop echo */
                                                            byteList.Add((byte)0xF6);
                                                            break;
                                                        }
                                                    case 0x07:
                                                        {
                                                            /* echo parameters */
                                                            var zEDL = Events.Where(x => x.Row == row + 1 && x.Type == EventType.Effect && x.Value == (int)Effect.Zpecial).FirstOrDefault();
                                                            var zEFB = Events.Where(x => x.Row == row + 2 && x.Type == EventType.Effect && x.Value == (int)Effect.Zpecial).FirstOrDefault();
                                                            var zFilter = Events.Where(x => x.Row == row + 3 && x.Type == EventType.Effect && x.Value == (int)Effect.Zpecial).FirstOrDefault();

                                                            if (zEDL != null && zEFB != null && zFilter != null)
                                                            {
                                                                byteList.Add((byte)0xF7);
                                                                byteList.Add((byte)zEDL.Parameters[0]);
                                                                byteList.Add((byte)zEFB.Parameters[0]);
                                                                byteList.Add((byte)zFilter.Parameters[0]);

                                                                zEDL.Processed = true;
                                                                zEFB.Processed = true;
                                                                zFilter.Processed = true;
                                                            }
                                                            break;
                                                        }
                                                    case 0x08:
                                                        {
                                                            /* echo fade */
                                                            var zFade = Events.Where(x => x.Row == row + 1 && x.Type == EventType.Effect && x.Value == (int)Effect.Zpecial).FirstOrDefault();
                                                            var zVolLeft = Events.Where(x => x.Row == row + 2 && x.Type == EventType.Effect && x.Value == (int)Effect.Zpecial).FirstOrDefault();
                                                            var zVolRight = Events.Where(x => x.Row == row + 3 && x.Type == EventType.Effect && x.Value == (int)Effect.Zpecial).FirstOrDefault();

                                                            if (zFade != null && zVolLeft != null && zVolRight != null)
                                                            {
                                                                byteList.Add((byte)0xF8);
                                                                byteList.Add((byte)zFade.Parameters[0]);
                                                                byteList.Add((byte)zVolLeft.Parameters[0]);
                                                                byteList.Add((byte)zVolRight.Parameters[0]);

                                                                zFade.Processed = true;
                                                                zVolLeft.Processed = true;
                                                                zVolRight.Processed = true;
                                                            }
                                                            break;
                                                        }
                                                }
                                                break;
                                            }
                                    }
                                    break;
                                }
                            default:
                                break;
                        }

                        eEvent.Processed = true;
                    }
                }

                /* Search for the next event */
                var nextEvent = Events.OrderBy(x => x.Row).Where(x => x.Row > row && x.Type != EventType.Instrument && x.Value != (int)Effect.PortamentoDown && x.Value != (int)Effect.PortamentoUp && x.Value != (int)Effect.Vibrato && x.Processed == false).FirstOrDefault();
                int nextRow = (nextEvent != null ? nextEvent.Row : Rows);

                //if(nEvent != null && nEvent.Value == 0xC9 && nextEvent == null && nextRow == Rows)
                //{
                //    nextRow--;
                //}

                int newLength = (nextRow - row) * module.CurrentSpeed;
                int fullLength = newLength;
                int cdAdded = 0;

                var nextCut = Events.Where(x => x.Row == nextRow && x.Type == EventType.Effect && x.Value == (int)Effect.Special && x.Parameters[0] == 0x0C).FirstOrDefault();
                if (nextCut != null)
                {
                    if (Events.Any(x => x.Row == nextCut.Row && x.Type == EventType.Note && x.Processed == false) == false)
                    {
                        if(nextCut.Parameters[1] >= module.CurrentSpeed)
                        {
                            nextCut.Parameters[1] = module.CurrentSpeed - 1;
                        }

                        cdAdded = (int)((nextCut.Parameters[1]) * module.EngineSpeed);
                        newLength += (int)((nextCut.Parameters[1]) * module.EngineSpeed);
                    }
                }

                //var nextDelay = Events.Where(x => x.Row == nextRow && x.Type == EventType.Effect && x.Value == (int)Effect.Special && x.Parameters[0] == 0x0D).FirstOrDefault();
                //if (nextDelay != null)
                //{
                //    if (nextDelay.Parameters[1] >= module.CurrentSpeed)
                //    {
                //        nextDelay.Parameters[1] = module.CurrentSpeed - 1;
                //    }

                //    cdAdded = (nextDelay.Parameters[1] * module.EngineSpeed);
                //    newLength += (nextDelay.Parameters[1] * module.EngineSpeed);
                //}

                if (newLength > 0x7F)
                {
                    newLength = ((int)Math.Floor((double)0x7F / (double)module.CurrentSpeed)) * module.CurrentSpeed;
                    cdAdded = 0;
                }

                if (nEvent != null)
                {
                    /* Special case here for the portamento up/down effects that only works on the _next_ note played */
                    var nextNote = Events.OrderBy(x => x.Row).Where(x => x.Row > row && x.Type == EventType.Note).FirstOrDefault();
                    int nextNotePos = (nextNote != null ? nextNote.Row : Rows);
                    bool lastNote = (nextNote == null);

                    if (nEvent.Value < 0xC8)
                    {

                        int startRow, effectRows, effectValue, firstEffect;
                        double semitones;

                        startRow = 0;
                        effectRows = 0;
                        effectValue = 0;
                        semitones = 0;
                        firstEffect = 0;
                        for (int search = (row + 1); search < nextNotePos; search++)
                        {
                            var vSearch = Events.OrderBy(x => x.Row).Where(x => x.Row == search && x.Type == EventType.Effect && (x.Value == (int)Effect.PortamentoDown || x.Value == (int)Effect.PortamentoUp) && x.Processed == false).FirstOrDefault();
                            if (vSearch != null)
                            {
                                if (startRow == 0)
                                {
                                    if (vSearch.Parameters[0] == 0)
                                    {
                                        vSearch.Parameters[0] = Memory[Channel].Portamento;
                                    }
                                    else
                                    {
                                        Memory[Channel].Portamento = vSearch.Parameters[0];
                                    }

                                    startRow = search;
                                    firstEffect = vSearch.Value;
                                }

                                if (vSearch.Value != firstEffect)
                                    break;

                                effectRows++;

                                if (vSearch.Parameters[0] != 0)
                                    effectValue = vSearch.Parameters[0];

                                if ((effectValue & 0xF0) == 0xF0)
                                {
                                    /* Fine portamento */
                                    semitones += ((effectValue & 0x0F) * (1.0 / 16.0));
                                }
                                else if ((effectValue & 0xE0) == 0xE0)
                                {
                                    /* extra fine portamento */
                                    semitones += ((effectValue & 0x0F) * (1.0 / 64.0));
                                }
                                else
                                {
                                    semitones += (effectValue * (1.0 / 16.0)) * (module.CurrentSpeed - 1);
                                }
                                vSearch.Processed = true;
                            }
                            else
                            {
                                if (startRow > 0)
                                    break;
                            }
                        }

                        if (startRow > 0 && startRow < Rows && ((int)Math.Floor(semitones)) != 0)
                        {
                            int real_semitones = (int)Math.Floor(semitones) * (firstEffect == (int)Effect.PortamentoUp ? 1 : -1);

                            if (nEvent.Value + real_semitones > 199)
                            {
                                real_semitones = (199 - nEvent.Value) * (firstEffect == (int)Effect.PortamentoUp ? 1 : -1);
                            }
                            else if (nEvent.Value + real_semitones < 128)
                            {
                                real_semitones = (nEvent.Value - 128) * (firstEffect == (int)Effect.PortamentoUp ? 1 : -1);
                            }

                            if (real_semitones != 0)
                            {
                                preEffectList.Add((byte)Effect.PortamentoUp);
                                preEffectList.Add((byte)((startRow - row) * module.CurrentSpeed));
                                preEffectList.Add((byte)((module.CurrentSpeed * effectRows) - module.CurrentSpeed));
                                preEffectList.Add((byte)(sbyte)(real_semitones));
                                portamento = 1;

                                if(lastNote)
                                {
                                    // If there are no more notes, disable the portamento effect on the last row
                                    Events.Add(new Event() { Row = Rows, Processed = false, Type = EventType.Effect, Value = (int)Effect.PortamentoOff });
                                }
                            }
                        }


                        /* Vibrato special thing test */
                        nextNote = Events.OrderBy(x => x.Row).Where(x => x.Row > row && x.Type == EventType.Note).FirstOrDefault();
                        nextNotePos = (nextNote != null ? nextNote.Row : Rows);
                        startRow = 0;
                        effectRows = 0;
                        effectValue = 0;
                        semitones = 0;
                        firstEffect = 0;
                        int rate = 0;
                        int depth = 0;
                        Event lastEvent = null;
                        for (int search = row; search < nextNotePos; search++)
                        {
                            var curEvent = Events.OrderBy(x => x.Row).Where(x => x.Row == search && x.Type == EventType.Effect && (x.Value == (int)Effect.Vibrato) && x.Processed == false).FirstOrDefault();
                            if (curEvent != null)
                            {
                                if (startRow == 0)
                                {
                                    if (curEvent.Parameters[0] == 0)
                                    {
                                        curEvent.Parameters[0] = Memory[Channel].VibratoRate;
                                    }
                                    else
                                    {
                                        Memory[Channel].VibratoRate = curEvent.Parameters[0];
                                    }

                                    if (curEvent.Parameters[1] == 0)
                                    {
                                        curEvent.Parameters[1] = Memory[Channel].VibratoDepth;
                                    }
                                    else
                                    {
                                        Memory[Channel].VibratoDepth = curEvent.Parameters[1];
                                    }

                                    startRow = search;
                                    firstEffect = curEvent.Value;
                                    rate = curEvent.Parameters[0];
                                    depth = curEvent.Parameters[1];
                                }

                                effectRows++;
                                curEvent.Processed = true;
                                lastEvent = curEvent;
                            }
                            else
                            {
                                if (startRow > 0)
                                    break;
                            }
                        }

                        if (startRow > 0)
                        {
                            preEffectList.Add((byte)Effect.Vibrato);
                            preEffectList.Add((byte)((startRow - row) * module.CurrentSpeed));
                            preEffectList.Add((byte)(rate / module.EngineSpeed));
                            preEffectList.Add((byte)depth);

                            lastEvent.Type = EventType.Effect;
                            lastEvent.Processed = false;
                            lastEvent.Value = (int)Effect.VibratoOff;
                            lastEvent.Parameters = null;
                            vibrato = 1;
                        }
                    }

                    /* Apply any effects before the note if we're playing a note */
                    byteList.AddRange(preEffectList);

                    if (nEvent.Value == 0xFF)
                    {
                        int fadeOutVal = (nI.FadeOut * 32);
                        if (fadeOutVal == 0)
                        {
                            if (newLength != noteLength || notedelay != 0)
                            {
                                noteLength = newLength;
                                if (notedelay != 0)
                                {
                                    if (notedelay > 0)
                                    {
                                        noteLength -= notedelay;
                                    }
                                    else
                                    {
                                        noteLength = Math.Abs(notedelay);
                                    }
                                }
                                byteList.Add((byte)noteLength);
                            }

                            patternLength += noteLength;
                            byteList.Add(0xC9);
                        }
                        else
                        {
                            fadeOutVal = (8192 / fadeOutVal) * module.CurrentSpeed;

                            if (fadeOutVal > 0xFF)
                                fadeOutVal = 0xFF;

                            byteList.Add((byte)Effect.VolumeSlide);
                            byteList.Add((byte)fadeOutVal);
                            byteList.Add((byte)0x00);
                            volumeSlide = 1;
                            volume = 0;

                            if (newLength != noteLength || notedelay != 0)
                            {
                                noteLength = newLength;
                                if (notedelay != 0)
                                {
                                    if (notedelay > 0)
                                    {
                                        noteLength -= notedelay;
                                    }
                                    else
                                    {
                                        noteLength = Math.Abs(notedelay);
                                    }
                                }
                                byteList.Add((byte)noteLength);
                            }

                            patternLength += noteLength;
                            byteList.Add(0xC8);
                        }

                        lastnote = nEvent.Value;
                        lastnotevol = volume;
                        nEvent.Processed = true;

                        byteList.AddRange(effectList);

                        if (notedelay < 0)
                        {
                            patternLength += (module.CurrentSpeed + notedelay);
                            byteList.Add((byte)(module.CurrentSpeed + notedelay));
                            byteList.Add((byte)0xC9);
                            notedelay = (module.CurrentSpeed + notedelay);
                        }

                        if (portamento == 1)
                        {
                            byteList.Add((byte)Effect.PortamentoOff);
                            portamento = 0;
                        }

                        if(vibrato == 1)
                        {
                            byteList.Add((byte)Effect.VibratoOff);
                            vibrato = 0;
                        }
                    }
                    else
                    {
                        if (novolumechange == 0)
                        {
                            if (nEvent.Value < 0xC8)
                            {
                                if (iEvent != null)
                                {
                                    if (volumeSlide == 1 && vEvent == null)
                                    {
                                        byteList.Add((byte)Effect.VolumeSlide);
                                        byteList.Add((byte)0x01);
                                        byteList.Add(Vol(nI.DefaultVolume, nI, module));
                                        volumeSlide = 0;
                                        volume = nI.DefaultVolume;
                                    }
                                    else if (vEvent == null && volume != nI.DefaultVolume)
                                    {
                                        byteList.Add((byte)Effect.Volume);
                                        byteList.Add(Vol(nI.DefaultVolume, nI, module));
                                        volume = nI.DefaultVolume;  
                                    }
                                }
                                else
                                {
                                    if (volumeSlide == 1 && vEvent == null)
                                    {
                                        byteList.Add((byte)Effect.VolumeSlide);
                                        byteList.Add((byte)0x01);
                                        byteList.Add(Vol(lastnotevol, nI, module));
                                        volumeSlide = 0;
                                        volume = lastnotevol;
                                    }
                                    else if (vEvent == null && volume != lastnotevol)
                                    {
                                        byteList.Add((byte)Effect.Volume);
                                        byteList.Add(Vol(lastnotevol, nI, module));
                                        volume = lastnotevol;
                                    }
                                }
                            }
                        }
                        else
                        {
                            novolumechange = 0;
                        }

                        if((portamento == 1 || vibrato == 1) && nextNotePos == Rows)
                        {
                            if(newLength != module.CurrentSpeed)
                            {
                                newLength -= module.CurrentSpeed;
                            }
                        }

                        if (newLength != noteLength || notedelay != 0)
                        {
                            noteLength = newLength;
                            if (notedelay != 0)
                            {
                                if (notedelay > 0)
                                {
                                    noteLength -= notedelay;
                                }
                                else
                                {
                                    noteLength = Math.Abs(notedelay);
                                }
                            }
                            byteList.Add((byte)noteLength);
                        }

                        patternLength += noteLength;
                        byteList.Add((byte)nEvent.Value);

                        lastnote = nEvent.Value;
                        lastnotevol = volume;
                        nEvent.Processed = true;

                        byteList.AddRange(effectList);

                        if (notedelay < 0)
                        {
                            patternLength += (module.CurrentSpeed + notedelay);
                            byteList.Add((byte)(module.CurrentSpeed + notedelay));
                            byteList.Add((byte)0xC9);
                            notedelay = (module.CurrentSpeed + notedelay);
                        }

                        /* Only stop portamento and vibrato if the next note is an actual note */
                        if (fullLength <= 0x7f)
                        {
                            if (portamento == 1)
                            {
                                byteList.Add((byte)Effect.PortamentoOff);
                                portamento = 0;
                            }

                            if (vibrato == 1)
                            {
                                byteList.Add((byte)Effect.VibratoOff);
                                vibrato = 0;
                            }
                        }

                    }
                }
                else
                {
                    if (preEffectList.Count > 0)
                    {
                        byteList.AddRange(preEffectList);
                    }

                    if (newLength != noteLength || notedelay != 0)
                    {
                        noteLength = newLength;
                        if (notedelay != 0)
                        {
                            noteLength -= notedelay;
                        }
                        byteList.Add((byte)noteLength);
                    }

                    patternLength += noteLength;

                    byteList.Add(0xC8);

                    /* If we're not on a note, apply effects after the rest to get proper timing on effects */
                    if (effectList.Count > 0)
                    {
                        byteList.AddRange(effectList);
                    }

                    if (eEvents.Count > 0 && eEvents.Any(x => (Effect)x.Value == Effect.VibratoOff))
                    {
                        if (Events.Any(x => x.Row == row + 1 && x.Type == EventType.Note && x.Processed == false) == false)
                        {
                            effectList = new List<byte>();
                            effectList.Add((byte)Effect.NotePortamento);
                            effectList.Add((byte)0x00);
                            effectList.Add((byte)(module.CurrentSpeed - 1));
                            effectList.Add((byte)lastnote);
                            byteList.AddRange(effectList);
                        }

                        vibrato = 0;
                    }

                    /* Only stop portamento and vibrato if the next note is an actual note */
                    if (fullLength <= 0x7f)
                    {
                        if (portamento == 1)
                        {
                            byteList.Add((byte)Effect.PortamentoOff);
                            portamento = 0;
                        }

                        if (vibrato == 1)
                        {
                            byteList.Add((byte)Effect.VibratoOff);
                            vibrato = 0;
                        }
                    }
                }

                row += (((noteLength - cdAdded) + notedelay) / module.CurrentSpeed) - 1;
                notedelay = 0;
            }

            Memory[Channel].NoteVolume = lastnotevol;
            Memory[Channel].Volume = volume;
            Memory[Channel].Note = lastnote;
            Memory[Channel].PortamentoTarget = portamentoTarget;

            if (Channel == 0 || module.Deduplicate || module.SetupPattern)
            {
                byteList.Add(0);
            }

            Data = byteList.ToArray();
        }
    }
}
