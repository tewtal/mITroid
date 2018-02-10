using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mITroid.IT
{

    class Row
    {
        public int Note { get; set; }
        public int Volume { get; set; }
        public int Command { get; set; }
        public int Value { get; set; }
        public int Instrument { get; set; }
        public int RowNum { get; set; }
        public Row()
        {
            Note = -1;
            Volume = -1;
            Command = -1;
            Value = -1;
            Instrument = -1;
            RowNum = 0;
        }

    }

    class Channel
    {
        public int ChannelNum { get; set; }
        public List<Row> Rows { get; set; }
        public int LastMaskVariable { get; set; }
        public int LastNote { get; set; }
        public int LastInstrument { get; set; }
        public int LastCommand { get; set; }
        public int LastValue { get; set; }
        public int LastVolume { get; set; }
        public Channel()
        {
            Rows = new List<Row>();
            LastCommand = -1;
            LastMaskVariable = -1;
            LastNote = -1;
            LastValue = -1;
            LastVolume = -1;
            LastInstrument = -1;
        }
    }

    class Pattern
    {
        private int _length;
        public int Rows { get; set; }
        private List<Channel> _channels;
        public List<Channel> Channels { get { return _channels; } }
        public int PatternIndex { get; set; }

        public Pattern(BinaryReader file, uint offset, int index)
        {
            PatternIndex = index;
            _channels = new List<Channel>();
            for (int i = 0; i < 8; i++)
                _channels.Add(new Channel() { ChannelNum = i });

            if (offset == 0)
            {
                _length = 0;
                Rows = 0;
                return;
            }

            file.BaseStream.Seek(offset, SeekOrigin.Begin);
            _length = file.ReadUInt16();
            Rows = file.ReadUInt16();

            int rowNum = 0;
            file.BaseStream.Seek(offset + 0x08, SeekOrigin.Begin);
            while(true)
            {
                int chanVar = file.ReadByte();
                if(chanVar == 0)
                {
                    rowNum += 1;
                    if (rowNum == Rows)
                        break;
                }
                else
                {
                    var row = new Row() { RowNum = rowNum };
                    int curChan = (chanVar - 1) & 63;
                    int maskVariable = 0;
                    if((chanVar & 128) != 0)
                    {
                        maskVariable = file.ReadByte();
                        _channels[curChan].LastMaskVariable = maskVariable;
                    }
                    else
                    {
                        maskVariable = _channels[curChan].LastMaskVariable;
                    }

                    if((maskVariable & 1) != 0)
                    {
                        row.Note = file.ReadByte();
                        _channels[curChan].LastNote = row.Note;
                    }

                    if ((maskVariable & 2) != 0)
                    {
                        row.Instrument = file.ReadByte();
                        _channels[curChan].LastInstrument = row.Instrument;
                    }

                    if ((maskVariable & 4) != 0)
                    {
                        int volume = file.ReadByte();

                        if (volume <= 64)
                        {
                            row.Volume = volume;
                        }

                        _channels[curChan].LastVolume = volume;
                    }

                    if ((maskVariable & 8) != 0)
                    {
                        row.Command = file.ReadByte();
                        row.Value = file.ReadByte();
                        _channels[curChan].LastCommand = row.Command;
                        _channels[curChan].LastValue = row.Value;
                    }


                    if ((maskVariable & 16) != 0)
                    {
                        row.Note = _channels[curChan].LastNote;
                    }
                    if ((maskVariable & 32) != 0)
                    {
                        row.Instrument = _channels[curChan].LastInstrument;
                    }
                    if ((maskVariable & 64) != 0)
                    {
                        if (_channels[curChan].LastVolume <= 64)
                        {
                            row.Volume = _channels[curChan].LastVolume;
                        }
                    }
                    if ((maskVariable & 128) != 0)
                    {
                        row.Command = _channels[curChan].LastCommand;
                        row.Value = _channels[curChan].LastValue;
                    }
                    _channels[curChan].Rows.Add(row);
                }
            }
        }
    }
}
