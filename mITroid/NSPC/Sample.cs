﻿using System;
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
        public int GlobalVolume { get; set; }
        public int DefaultVolume { get; set; }
        public int VirtualSampleType { get; set; }
        public int VirtualSampleIndex { get; set; }
        
        public Sample(IT.Sample itSample, bool enhanceTreble, decimal resampleFactor)
        {
            SampleIndex = itSample.SampleIndex;
            GlobalVolume = itSample.GlobalVolume;
            DefaultVolume = itSample.Volume;

            if (itSample.Length == 0)
                return;

            if(itSample.FileName.StartsWith(">"))
            {
                int targetSampleIndex = Convert.ToInt32(itSample.FileName.Substring(1));
                VirtualSampleIndex = targetSampleIndex;
                VirtualSampleType = 1;
            }
            else if(itSample.FileName.StartsWith("<"))
            {
                int targetSampleIndex = Convert.ToInt32(itSample.FileName.Substring(1));
                VirtualSampleIndex = targetSampleIndex;
                VirtualSampleType = 2;
            }

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

            C5Speed = (decimal)itSample.C5Speed * (1.0m / bs.ResampleRatio);
            if (VirtualSampleType > 0)
            {
                Data = new byte[0];
            }

        }

        public static implicit operator Sample(short v)
        {
            throw new NotImplementedException();
        }
    }
}
