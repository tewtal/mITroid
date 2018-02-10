using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mITroid.NSPC
{
    class Instrument
    {
        public int ADSR { get; set; }
        public int Gain { get; set; }
        public int PitchAdjustment { get; set; }
        public int InstrumentVolume { get; set; }
        public int SampleVolume { get; set; }
        public int DefaultVolume { get; set; }
        public int FadeOut { get; set; }
        public int InstrumentIndex { get; set; }
        public int VirtualInstrumentType { get; set; }
        public int VirtualInstrumentIndex { get; set; }
        public int SampleIndex { get; set; }

        private static readonly int[] AttackTable = { 4100, 2500, 1500, 1000, 640, 380, 260, 160, 96, 64, 40, 24, 16, 10, 6, 0 };
        private static readonly int[] DecayTable = { 1200, 740, 440, 290, 180, 110, 74, 37 };
        private static readonly int[] SustainTable = { 65535, 38000, 28000, 24000, 19000, 14000, 12000, 9400, 7100, 5900, 4700, 3500, 2900, 2400, 1800, 1500, 1200, 880, 740, 590, 440, 370, 290, 220, 180, 150, 110, 92, 74, 55, 37, 18 };

        private static readonly double[] DecayMult = { 0.724, 0.518, 0.378, 0.263, 0.181, 0.110, 0.052, 0 };
        private static readonly double[] SustainMult = { 0.407, 0.623, 0.768, 0.876, 0.962, 1.032, 1.095, 1.149 };

        private int FindClosest(int[] table, double value, double multiplier)
        {
            double diff = Int32.MaxValue;
            for(int i = 0; i < table.Length; i++)
            {
                double newDiff = Math.Abs((table[i] * multiplier) - (double)value);
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

        public Instrument()
        {

        }

        public Instrument(IT.Instrument itInstrument, NSPC.Sample nSample, NSPC.Module nModule)
        {
            InstrumentIndex = itInstrument.InstrumentIndex;
            FadeOut = itInstrument.FadeOut;
            SampleIndex = nSample.SampleIndex;

            if (itInstrument.FileName.StartsWith(">"))
            {
                int targetInstrumentIndex = Convert.ToInt32(itInstrument.FileName.Substring(1));
                VirtualInstrumentIndex = targetInstrumentIndex;
                VirtualInstrumentType = 1;
                return;
            }
            else if (itInstrument.FileName.StartsWith("<"))
            {
                int targetInstrumentIndex = Convert.ToInt32(itInstrument.FileName.Substring(1));
                VirtualInstrumentIndex = targetInstrumentIndex;
                VirtualInstrumentType = 2;
                return;
            }

            if (itInstrument.UseEnvelope && itInstrument.EnvelopeNodes.Count > 3)
            {
                int aDuration = itInstrument.EnvelopeNodes[1].Ticks - itInstrument.EnvelopeNodes[0].Ticks;
                int dDuration = itInstrument.EnvelopeNodes[2].Ticks - itInstrument.EnvelopeNodes[1].Ticks;
                int sDuration = itInstrument.EnvelopeNodes[3].Ticks - itInstrument.EnvelopeNodes[2].Ticks;
                int sVol = (int)(Math.Round(itInstrument.EnvelopeNodes[2].Volume / 8.0, 0)) - 1;

                if (nModule.UseNewADSR == true)
                {
                    double itTempo = nModule.InitialTempo * (4.8 / nModule.EngineSpeed);
                    double millisPerTick = (2500.0 / itTempo);

                    aDuration = (aDuration == 1) ? 0x0F : FindClosest(AttackTable, (aDuration * millisPerTick), 1.0);

                    int origDecayTicks = dDuration;

                    dDuration = FindClosest(DecayTable, (dDuration * millisPerTick), DecayMult[sVol]);
                    int actualDecayTicks = (int)Math.Round(((DecayTable[dDuration] * DecayMult[sVol]) / millisPerTick), 0);

                    sDuration += (origDecayTicks - actualDecayTicks);
                    sDuration = (itInstrument.SustainLoop == true) ? 0x00 : FindClosest(SustainTable, (sDuration * millisPerTick), SustainMult[sVol]);
                }
                else
                {
                    aDuration += 1; dDuration += 1; sDuration += 1;
                    aDuration = 0xF - (aDuration > 0xF ? 0xF : aDuration);
                    dDuration = 0x7 - ((dDuration > 0xF ? 0xF : dDuration) >> 1);
                    sDuration = 0x1f - ((sDuration << 1) > 0x1F ? 0x1F : (sDuration << 1));
                }

                int ad = 0x80 + (dDuration << 4) + aDuration;
                int sr = (sVol << 5) + sDuration;

                ADSR = (sr << 8) + ad;
                Gain = 0;
            }
            else
            {
                ADSR = 0;
                //Gain = (Event.ConvertVolume(itInstrument.GlobalVolume / 2) / 2) - 1;
                Gain = 0x7F; // Set this to max gain always since we're using software volume calculation for instruments
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

            InstrumentVolume = itInstrument.GlobalVolume;
            SampleVolume = nSample.GlobalVolume;
            DefaultVolume = nSample.DefaultVolume;
        }
    }
}
