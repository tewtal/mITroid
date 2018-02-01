using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mITroid.NSPC
{
    class Pattern
    {
        public int Pointer { get; set; }
        public int Rows { get; set; }
        public List<Track> Tracks { get; set; }

        public Pattern(IT.Pattern itPattern)
        {
            Tracks = new List<Track>();
            Rows = itPattern.Rows;
            foreach(var channel in itPattern.Channels)
            {
                var track = new Track(channel.Rows);
                track.Rows = itPattern.Rows;
                Tracks.Add(track);
            }
        }
    }
}
