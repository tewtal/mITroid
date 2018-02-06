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
        public int InternalInstrument { get; set; }

        private static readonly int[] AttackTable = { 4100, 2500, 1500, 1000, 640, 380, 260, 160, 96, 64, 40, 24, 16, 10, 6, 0 };
        private static readonly int[] DecayTable = { 1200, 740, 440, 290, 180, 110, 74, 37 };
        private static readonly int[] SustainTable = { 65535, 38000, 28000, 24000, 19000, 14000, 12000, 9400, 7100, 5900, 4700, 3500, 2900, 2400, 1800, 1500, 1200, 880, 740, 590, 440, 370, 290, 220, 180, 150, 110, 92, 74, 55, 37, 18 };

        private int FindClosest(int[] table, int value)
        {
            int diff = Int32.MaxValue;
            for(int i = 0; i < table.Length; i++)
            {
                int newDiff = Math.Abs(table[i] - value);
                if (newDiff < diff)
                {
                    diff = newDiff;
                }
                else
                {
                    return i - 1;
                }
            }
            return table.Length - 1;
        }

        public Instrument(IT.Instrument itInstrument, NSPC.Sample nSample, NSPC.Module nModule)
        {
            if (itInstrument.Name.StartsWith("&"))
            {
                try
                {
                    InternalInstrument = Convert.ToInt32(itInstrument.Name.Substring(1), 16);
                }
                catch
                {
                    InternalInstrument = -1;
                }
            }
            else
            {
                InternalInstrument = -1;
            }

            if (itInstrument.Name.StartsWith("$"))
            {
                try
                {
                    SampleIndex = Convert.ToInt32(itInstrument.Name.Substring(1), 16);
                } 
                catch
                {
                    SampleIndex = itInstrument.SampleIndex + 0x17;
                }
            }
            else
            {
                SampleIndex = itInstrument.SampleIndex + 0x17;
            }

            if (itInstrument.UseEnvelope && itInstrument.EnvelopeNodes.Count > 3)
            {                
                int aDuration = itInstrument.EnvelopeNodes[1].Ticks - itInstrument.EnvelopeNodes[0].Ticks;
                int dDuration = itInstrument.EnvelopeNodes[2].Ticks - itInstrument.EnvelopeNodes[1].Ticks;
                int sDuration = itInstrument.EnvelopeNodes[3].Ticks - itInstrument.EnvelopeNodes[2].Ticks;
                int sVol = itInstrument.EnvelopeNodes[2].Volume / 8;


                if (nModule.UseNewADSR == true)
                {
                    int itTempo = (int)Math.Round(nModule.InitialTempo * (4.8 / nModule.EngineSpeed), 0);
                    int millisPerTick = (2500 / itTempo);

                    aDuration = (aDuration == 1) ? 0x0F : FindClosest(AttackTable, (aDuration * millisPerTick));
                    dDuration = FindClosest(DecayTable, (dDuration * millisPerTick));
                    sDuration = (itInstrument.SustainLoop == true) ? 0x00 : FindClosest(SustainTable, (sDuration * millisPerTick));
                }
                else
                {
                    aDuration = 0xF - (aDuration > 0xF ? 0xF : aDuration);
                    dDuration = 0x7 - ((dDuration > 0xF ? 0xF : dDuration) >> 1);
                    sDuration = 0x1f - ((sDuration << 1) > 0x1F ? 0x1F : (sDuration << 1));
                }

                int ad = 0x80 + (dDuration << 4) + aDuration;
                int sr = ((sVol < 8 ? sVol : 7) << 5) + sDuration;

                ADSR = (sr << 8) + ad;
                Gain = 0;
            }
            else
            {
                ADSR = 0;
                Gain = (Event.ConvertVolume(itInstrument.GlobalVolume / 2) / 2) - 1;
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
