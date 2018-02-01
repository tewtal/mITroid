using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace mITroid.NSPC
{
    class Sample
    {
        public int StartAddress { get; set; }
        public int LoopAddress { get; set; }
        public int LoopPoint { get; set; }
        public byte[] Data { get; set; }
        public int SampleIndex {get; set;}

        public decimal C5Speed { get; set; }
        
        public Sample(IT.Sample itSample, bool enhanceTreble, decimal resampleFactor)
        {
            SampleIndex = itSample.SampleIndex;

            if (itSample.Length == 0)
                return;

            var encoder = new BRREncoder();
            var bs = encoder.Encode(itSample, enhanceTreble, resampleFactor);
            Data = bs.Data;

            if (itSample.UseLoop)
            {
                LoopPoint = bs.LoopPoint;
            }
            else
            {
                LoopPoint = bs.Data.Length;
            }

            //if (itSample.UseLoop)
            //{
            C5Speed = (decimal)itSample.C5Speed * (1.0m / bs.ResampleRatio);
            //}
            //else
            //{
              //  C5Speed = itSample.C5Speed;
            //}
            //C5Speed = (int)itSample.C5Speed;
        }
    }
}
