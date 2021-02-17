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

        private static readonly decimal[] DecayMult = { 0.724m, 0.518m, 0.378m, 0.263m, 0.181m, 0.110m, 0.052m, 0 };
        private static readonly decimal[] SustainMult = { 0.407m, 0.623m, 0.768m, 0.876m, 0.962m, 1.032m, 1.095m, 1.149m };

        private int FindClosest(int[] table, decimal value, decimal multiplier)
        {
            decimal diff = Int32.MaxValue;
            for(int i = 0; i < table.Length; i++)
            {
                decimal newDiff = Math.Abs((table[i] * multiplier) - (decimal)value);
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
                InstrumentVolume = itInstrument.GlobalVolume;
                SampleVolume = nSample.GlobalVolume;
                DefaultVolume = nSample.DefaultVolume;
                return;
            }
            else if (itInstrument.FileName.StartsWith("<"))
            {
                int targetInstrumentIndex = Convert.ToInt32(itInstrument.FileName.Substring(1));
                VirtualInstrumentIndex = targetInstrumentIndex;
                VirtualInstrumentType = 2;
                InstrumentVolume = itInstrument.GlobalVolume;
                SampleVolume = nSample.GlobalVolume;
                DefaultVolume = nSample.DefaultVolume;
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
                    decimal itTempo = nModule.InitialTempo * (4.8m / nModule.EngineSpeed);
                    decimal millisPerTick = (2500.0m / itTempo);

                    aDuration = (aDuration == 1) ? 0x0F : FindClosest(AttackTable, (aDuration * millisPerTick), 1.0m);

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
                //decimal div = 0x1064;
                decimal div = 4186;// / 2;
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
