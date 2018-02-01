using System;
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
        VolumeSlide2 = 'K',
        NotePortamento = 'G',
        Special = 'S',
        Zpecial = 'Z'
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
        NotePortamento = 0xF9,
        Instrument = 0xE0,
        Special = 0xFE,
        Zpecial = 0xFD
    }

    class Event
    {
        public EventType Type { get; set; }
        public int Value { get; set; }
        public List<int> Parameters { get; set; }
        public int Row { get; set; }
        public bool Processed { get; set; }

        public Event()
        {
            Processed = false;
            Row = 0;
        }

        public static Event CreateEffect(IT.Row itRow)
        {
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
                        ev.Parameters = new List<int>() { rate * 8, depth * 8 };
                        break;
                    }

                case ITEffect.Tremolo:
                    {
                        int rate = (itRow.Value >> 4);
                        int depth = (itRow.Value & 0x0f);
                        ev.Value = (int)Effect.Tremolo;
                        ev.Parameters = new List<int>() { rate * 8, depth * 8 };
                        break;
                    }

                case ITEffect.Volume:
                    {
                        ev.Value = (int)Effect.Volume;
                        ev.Parameters = new List<int>() { ConvertVolume(itRow.Value) };
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
                        ev.Parameters = new List<int>() { (int)Math.Round(itRow.Value / 4.8, 0) };
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
                case ITEffect.VolumeSlide2:
                    {
                        ev.Value = (int)Effect.VolumeSlide;
                        ev.Parameters = new List<int>() { 1, itRow.Value };
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
            }

            return ev;
        }

        public static int ConvertVolume(int val)
        {
            double vp = val;

            if (vp < 10)
                vp = vp * 1.7;
            else if (vp < 15)
                vp = vp * 1.6;
            else if (vp < 20)
                vp = vp * 1.5;
            else if (vp < 30)
                vp = vp * 1.4;
            else if (vp < 40)
                vp = vp * 1.3;
            else if (vp < 50)
                vp = vp * 1.2;


            int volume = (int)((vp > 0) ? (vp * 4) - 1 : 0);
            return volume;
        }
    }

    class Track
    {
        public int Rows { get; set; }
        public int Pointer { get; set; }
        public List<Event> Events { get; set; }
        public byte[] Data { get; set; }

        public Track(List<IT.Row> itRows)
        {
            Events = new List<Event>();
            foreach(var row in itRows)
            {
                if(row.Note != 0)
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
                    else if(row.Note == 255)
                    {
                        noteEvent.Value = 0xFF;
                    }
                    else if(row.Note > 119)
                    {
                        noteEvent.Value = 0xC8;
                    }
                    else
                    {
                        noteEvent.Value = row.Note + 104;
                        if (noteEvent.Value > 199)
                            noteEvent.Value = 199;

                        if (noteEvent.Value < 128)
                            noteEvent.Value = 128;
                    }

                    Events.Add(noteEvent);
                }

                if(row.Instrument != 0)
                {
                    var instrumentEvent = new Event
                    {
                        Type = EventType.Instrument,
                        Value = row.Instrument + 0x17,
                        Row = row.RowNum
                    };
                    Events.Add(instrumentEvent);
                }

                if(row.Volume != 0)
                {
                    var volumeEvent = new Event
                    {
                        Type = EventType.Volume,
                        Value = Event.ConvertVolume(row.Volume),
                        Row = row.RowNum
                    };
                    Events.Add(volumeEvent);
                }

                if(row.Command != 0)
                {                    
                    Events.Add(Event.CreateEffect(row));
                }
            }
        }

        /* This function takes a track and encodes it as the N-SPC output stream */
        public void GenerateData(Module module)
        {
            List<byte> byteList = new List<byte>();
            int noteLength = 0;
            int volumeSlide = 0;
            int volume = 0xFE;
            int instrument = 0;
            int novolumechange = 0;
            int notedelay = 0;
            int lastnote = 0;
            int patternLength = 0;
            int norest = 0;

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
                var eEvent = Events.Where(x => x.Row == row && x.Type == EventType.Effect && x.Processed == false).FirstOrDefault();

                if (iEvent != null)
                {
                    if (instrument != iEvent.Value)
                    {
                        byteList.Add((byte)Effect.Instrument);
                        byteList.Add((byte)iEvent.Value);
                        iEvent.Processed = true;
                        instrument = iEvent.Value;
                    }
                }

                if(vEvent != null)
                {
                    if(nEvent != null)
                    {
                        if (vEvent.Value != volume)
                        {
                            if (volumeSlide == 1)
                            {
                                byteList.Add((byte)Effect.VolumeSlide);
                                byteList.Add((byte)0x01);
                                byteList.Add((byte)vEvent.Value);
                                vEvent.Processed = true;
                                volume = vEvent.Value;
                            }
                            else
                            {
                                byteList.Add((byte)Effect.Volume);
                                byteList.Add((byte)vEvent.Value);
                                vEvent.Processed = true;
                                volume = vEvent.Value;
                            }
                        }
                    }
                    else
                    {
                        /* Treat this as a volume slide, try to merge with upcoming volume changes */
                        /* First we find the next note */
                        var nextNote = Events.OrderBy(x => x.Row).Where(x => x.Row > row && x.Type == EventType.Note).FirstOrDefault();
                        int nextNotePos = (nextNote != null ? nextNote.Row : Rows);
                        int searchLength = 8; /* Search at most 5 rows ahead before bailing out */
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
                                    byteList.Add((byte)vEvent.Value);
                                }

                                int fadeRows = (targetRow - row);
                                byteList.Add((byte)Effect.VolumeSlide);
                                byteList.Add((byte)(fadeRows * module.CurrentSpeed));
                                byteList.Add((byte)targetVolume);
                            }
                            else
                            {
                                byteList.Add((byte)Effect.VolumeSlide);
                                byteList.Add((byte)0x01);
                                byteList.Add((byte)targetVolume);
                            }
                            volume = targetVolume;
                        }
                        volumeSlide = 1;
                        vEvent.Processed = true;
                    }
                }

                List<byte> effectList = new List<byte>();
                if(eEvent != null)
                {
                    switch((Effect)eEvent.Value)
                    {
                        case Effect.Pan:
                            {
                                effectList.Add((byte)Effect.Pan);
                                effectList.Add((byte)0x01);
                                effectList.Add((byte)eEvent.Parameters[0]);
                                break;
                            }

                        case Effect.Tempo:
                            {
                                byteList.Add((byte)Effect.Tempo);
                                byteList.Add((byte)eEvent.Parameters[0]);
                                module.CurrentTempo = eEvent.Parameters[0];
                                break;
                            }

                        case Effect.Speed:
                            {
                                int newSpeed = eEvent.Parameters[0];
                                double factor = ((double)newSpeed / (double)module.CurrentSpeed);
                                int newTempo = (int)Math.Round(((double)module.CurrentTempo / factor));
                                byteList.Add((byte)Effect.Tempo);
                                byteList.Add((byte)newTempo);
                                break;
                            }
                        case Effect.Volume:
                            {
                                if (eEvent.Parameters[0] != volume)
                                {
                                    if (volumeSlide == 1)
                                    {
                                        effectList.Add((byte)Effect.VolumeSlide);
                                        effectList.Add((byte)0x01);
                                        effectList.Add((byte)eEvent.Parameters[0]);
                                        volume = eEvent.Parameters[0];
                                    }
                                    else
                                    {
                                        byteList.Add((byte)Effect.Volume);
                                        byteList.Add((byte)eEvent.Parameters[0]);
                                        volume = eEvent.Parameters[0];
                                    }
                                }
                                break;
                            }
                        case Effect.NotePortamento:
                            {
                                if (nEvent != null)
                                {

                                    if (eEvent.Parameters[0] != 0)
                                    {

                                        int pitch_speed = 0x20 - ((eEvent.Parameters[0] * 4) < 0x19 ? (eEvent.Parameters[0] * 4) : 0x19);

                                        //int pitch_speed = ((0xFF/16) - (eEvent.Parameters[0]/16) + 1);

                                        effectList.Add((byte)Effect.NotePortamento);
                                        effectList.Add((byte)0x00);
                                        effectList.Add((byte)pitch_speed);
                                        effectList.Add((byte)nEvent.Value);

                                        if (vEvent == null)
                                        {
                                            if (volume != 0xFF)
                                            {
                                                effectList.Add((byte)Effect.VolumeSlide);
                                                effectList.Add((byte)0x01);
                                                effectList.Add((byte)0xFF);
                                                volume = 0xFF;
                                                volumeSlide = 0;
                                            }
                                        }

                                        /* Scan for future note portamento effects and process them */
                                        var nextNote = Events.OrderBy(x => x.Row).Where(x => x.Row > row && x.Type == EventType.Note).FirstOrDefault();
                                        int nextNotePos = (nextNote != null ? nextNote.Row : Rows);
                                        for (int search = (row + 1); search < nextNotePos; search++)
                                        {
                                            var vSearch = Events.OrderBy(x => x.Row).Where(x => x.Row == search && x.Type == EventType.Effect && x.Value == (int)Effect.NotePortamento && x.Processed == false).FirstOrDefault();
                                            if (vSearch != null)
                                            {
                                                if (vSearch.Parameters[0] == eEvent.Parameters[0] || vSearch.Parameters[0] == 0x00)
                                                {
                                                    vSearch.Processed = true;
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

                                        lastnote = nEvent.Value;
                                        nEvent.Processed = true;
                                        nEvent = null;
                                    }
                                }

                                break;
                            }
                        case Effect.Vibrato:
                            {
                                if (eEvent.Parameters[0] != 0 || eEvent.Parameters[1] != 0)
                                {
                                    effectList.Add((byte)Effect.Vibrato);
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
                                    var curSearch = Events.OrderBy(x => x.Row).Where(x => x.Row == search && x.Type == EventType.Effect && x.Value == (int)Effect.Vibrato && x.Processed == false).FirstOrDefault();
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
                                    lastSearch.Value = (int)Effect.VibratoOff;
                                    lastSearch.Row = lastSearch.Row + 1;
                                }

                                break;
                            }
                        case Effect.VibratoOff:
                            {
                                effectList.Add((byte)Effect.VibratoOff);
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
                                effectList.Add((byte)Effect.TremoloOff);
                                break;
                            }
                        case Effect.VolumeSlide:
                            {
                                int val = eEvent.Parameters[1];
                                int slideDirection = (val < 0x10 ? 0 : 1);
                                int volumeChange = (slideDirection == 0 ? (val & 0xF) : ((val >> 4) & 0xF));
                                int volumeChangePerRow = (volumeChange * (module.CurrentSpeed-1)) * 2;
                                int targetVolumeChange = (slideDirection == 0 ? -volumeChangePerRow : volumeChangePerRow);
                                int targetVolumeChangeRows = 1;
                                var nextNote = Events.OrderBy(x => x.Row).Where(x => x.Row > row && x.Type == EventType.Note).FirstOrDefault();
                                int nextNotePos = (nextNote != null ? nextNote.Row : Rows);
                                for (int search = (row + 1); search < nextNotePos; search++)
                                {
                                    var lastSearch = Events.OrderBy(x => x.Row).Where(x => x.Row == search && x.Type == EventType.Effect && x.Value == (int)Effect.VolumeSlide && x.Processed == false).FirstOrDefault();
                                    if (lastSearch != null)
                                    {
                                        lastSearch.Processed = true;
                                        targetVolumeChange += (slideDirection == 0 ? -volumeChangePerRow : volumeChangePerRow);
                                        targetVolumeChangeRows += 1;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                int finalVolume = (volume + targetVolumeChange);

                                if (finalVolume < 0)
                                    finalVolume = 0;

                                if (finalVolume > 0xFF)
                                    finalVolume = 0xFF;

                                if (nEvent != null)
                                {
                                    novolumechange = 1;
                                    byteList.Add((byte)Effect.Volume);
                                    byteList.Add((byte)0xFF);
                                }

                                effectList.Add((byte)Effect.VolumeSlide);
                                effectList.Add((byte)((targetVolumeChangeRows * module.CurrentSpeed)));
                                effectList.Add((byte)finalVolume);
                                volume = finalVolume;
                                volumeSlide = 1;
                                break;
                            }
                        case Effect.Special:
                            {
                                int sub_cmd = eEvent.Parameters[0];
                                int sub_val = eEvent.Parameters[1];

                                switch(sub_cmd)
                                {
                                    case 0x0D: /* note delay by sub_val ticks */
                                        {
                                            effectList.Add((byte)sub_val);
                                            effectList.Add((byte)0xC8);
                                            notedelay = sub_val;
                                            break;
                                        }
                                    case 0x00: /* Special commands */
                                        {
                                            switch(sub_val)
                                            {
                                                case 0x05:
                                                    {
                                                        /* Echo bits / volume */
                                                        var zEchoBits = Events.Where(x => x.Row == row + 1 && x.Type == EventType.Effect && x.Value == (int)Effect.Zpecial).FirstOrDefault();
                                                        var zVolLeft  = Events.Where(x => x.Row == row + 2 && x.Type == EventType.Effect && x.Value == (int)Effect.Zpecial).FirstOrDefault();
                                                        var zVolRight = Events.Where(x => x.Row == row + 3 && x.Type == EventType.Effect && x.Value == (int)Effect.Zpecial).FirstOrDefault();

                                                        if(zEchoBits != null && zVolLeft != null && zVolRight != null)
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
                                                        var zVolRight= Events.Where(x => x.Row == row + 3 && x.Type == EventType.Effect && x.Value == (int)Effect.Zpecial).FirstOrDefault();

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

                /* Search for the next event */
                var nextEvent = Events.OrderBy(x => x.Row).Where(x => x.Row > row && x.Type != EventType.Instrument && x.Processed == false).FirstOrDefault();
                int nextRow = (nextEvent != null ? nextEvent.Row : Rows);
                int newLength = (nextRow - row) * module.CurrentSpeed;

                if (newLength > 0x7F)
                {
                    newLength = ((int)Math.Floor((double)0x7F / (double)module.CurrentSpeed)) * module.CurrentSpeed;
                }

                if (nEvent != null)
                {
                    /* Apply any effects before the note if we're playing a note */
                    byteList.AddRange(effectList);

                    if (nEvent.Value == 0xFF)
                    {
                        byteList.Add((byte)Effect.VolumeSlide);
                        byteList.Add((byte)(10 * module.CurrentSpeed));
                        byteList.Add((byte)0x00);
                        volumeSlide = 1;
                        volume = 0;

                        if (newLength != noteLength)
                        {
                            noteLength = newLength;
                            byteList.Add((byte)noteLength);
                        }

                        patternLength += noteLength;
                        byteList.Add(0xC8);
                        nEvent.Processed = true;
                    }
                    else
                    {
                        if (novolumechange == 0)
                        {
                            if (nEvent.Value < 0xC8)
                            {
                                if (volumeSlide == 1 && vEvent == null)
                                {
                                    byteList.Add((byte)Effect.VolumeSlide);
                                    byteList.Add((byte)0x01);
                                    byteList.Add((byte)0xFF);
                                    volumeSlide = 0;
                                    volume = 0xFF;
                                }
                                else if (vEvent == null && volume != 0xFF)
                                {
                                    byteList.Add((byte)Effect.Volume);
                                    byteList.Add((byte)0xFF);
                                    volume = 0xFF;
                                }
                            }
                        }
                        else
                        {
                            novolumechange = 0;
                        }

                        if (newLength != noteLength)
                        {
                            noteLength = newLength;
                            if (notedelay > 0)
                            {
                                noteLength -= notedelay;
                                notedelay = 0;
                                   
                            }
                            byteList.Add((byte)noteLength);
                        }

                        patternLength += noteLength;
                        byteList.Add((byte)nEvent.Value);
                        lastnote = nEvent.Value;
                        nEvent.Processed = true;
                    }
                }
                else
                {
                    if (newLength != noteLength)
                    {
                        noteLength = newLength;
                        if (notedelay > 0)
                        {
                            noteLength -= notedelay;
                            notedelay = 0;

                        }
                        noteLength = newLength;
                        byteList.Add((byte)noteLength);
                    }
                    patternLength += noteLength;
                    byteList.Add(0xC8);

                    /* If we're not on a note, apply effects after the rest to get proper timing on effects */
                    byteList.AddRange(effectList);
                }

                row += (noteLength / module.CurrentSpeed) - 1;
            }

            byteList.Add(0);
            Data = byteList.ToArray();
        }
    }
}
