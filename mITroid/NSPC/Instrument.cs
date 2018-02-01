using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mITroid.NSPC
{
    class Instrument
    {
        public int SampleIndex { get; set; }
        public int ADSR { get; set; }
        public int Gain { get; set; }
        public int PitchAdjustment { get; set; }

        public Instrument(IT.Instrument itInstrument, NSPC.Sample nSample)
        {
            SampleIndex = itInstrument.SampleIndex + 0x17;

            if (itInstrument.UseEnvelope && itInstrument.EnvelopeNodes.Count > 3)
            {
                int aDuration = itInstrument.EnvelopeNodes[1].Ticks - itInstrument.EnvelopeNodes[0].Ticks - 1;
                int dDuration = itInstrument.EnvelopeNodes[2].Ticks - itInstrument.EnvelopeNodes[1].Ticks - 1;
                int sDuration = itInstrument.EnvelopeNodes[3].Ticks - itInstrument.EnvelopeNodes[2].Ticks - 1;
                int sVol = (itInstrument.EnvelopeNodes[2].Volume / 8);

                aDuration = 0xF - (aDuration > 0xF ? 0xF : aDuration);
                dDuration = 0x7 - ((dDuration > 0xF ? 0xF : dDuration) >> 1);
                sDuration = 0x1f - ((sDuration<<1) > 0x1F ? 0x1F : (sDuration<<1));

                int ad = 0x80 + (dDuration << 4) + aDuration;
                int sr = (sVol << 5) + sDuration;

                ADSR = (sr << 8) + ad;
                Gain = 0;
            }
            else
            {
                ADSR = 0;
                Gain = itInstrument.GlobalVolume - 1;

            }


            if (nSample.C5Speed > 0)
            {
                decimal div = 0x1064;
                int mult = (int)Math.Floor(nSample.C5Speed / div);
                int mod = (int)nSample.C5Speed % (int)div;
                int sub = (int)Math.Round((255 * ((decimal)mod / div)));

                PitchAdjustment = (sub << 8) + mult;
            }
            else
            {
                PitchAdjustment = 0;
            }
        }
    }
}
