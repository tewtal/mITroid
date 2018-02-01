using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
BRRtools
by Bregalad. Special thanks to Kode54.
Minor post-3.11 fixes by Optiroc (David Lindecrantz).

BRRtools are currently the most evolved tools to convert between standard RIFF .wav format and SNES's built-in BRR sound format. 
They have many features never seen before in any other converter, and are open source.

Versions up to 2.1 used to be coded in Java, requiring a Java virtual machine to run. 
Because this was an useless layer of abstraction which is only useful when developing, the program was rewritten to not need Java any longer.

I heavily borrowed encoding algorithms from Kode54, which himself heavily borrowed code from some other ADPCM encoder. 
This is freeware, feel free to redistribute/improve but DON'T CLAIM IT IS YOUR OWN WORK THANK YOU.
*/

/* Messy conversion of the C code to C# by total */

namespace mITroid.NSPC
{
    class BRRSample
    {
        public byte[] Data { get; set; }
        public int LoopPoint { get; set; }
        public decimal ResampleRatio { get; set; }
    }

    class BRREncoder
    {

        private byte filter_at_loop = 0;
        private Int16 p1_at_loop, p2_at_loop;
        private bool[] FIRen;
        private uint[] FIRstats;
        private bool wrap_en = true;
        private char resample_type = 'b';                    // Resampling type (n = nearest neighboor, l = linear, c = cubic, s = sine, b = bandlimited)
        private double PI = Math.PI;
        private byte[] BRR;
        private Int16 p1, p2;

        public BRREncoder()
        {
            this.FIRen = new bool[4] { true, true, true, true };  // Which BRR filters are enabled
            this.FIRstats = new uint[4] { 0, 0, 0, 0 };// Statistincs on BRR filter usage
            this.BRR = new byte[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        private double sinc(double x)
        {
            if (x == 0.0)
            {
                return 1.0;
            }
            else
            {
                return Math.Sin(PI * x) / (PI * x);
            }
        }

        private Int16 CLAMP_16(int n)
        {
            return (Int16)(((Int16)(n) != (n)) ? ((Int16)(0x7fff - ((n) >> 24))) : (n));
        }

        // Convert a block from PCM to BRR
        // Returns the squared error between original data and encoded data
        // If "is_end_point" is true, the predictions p1/p2 at loop are also used in caluclating the error (depending on filter at loop)

        private int get_brr_prediction(byte filter, Int16 p1, Int16 p2)
        {
            int p;
            switch (filter)                         //Different formulas for 4 filters
            {
                case 0:
                    return 0;

                case 1:
                    p = p1;
                    p -= p1 >> 4;
                    return p;

                case 2:
                    p = p1 << 1;
                    p += (-(p1 + (p1 << 1))) >> 5;
                    p -= p2;
                    p += p2 >> 4;
                    return p;

                case 3:
                    p = p1 << 1;
                    p += (-(p1 + (p1 << 2) + (p1 << 3))) >> 6;
                    p -= p2;
                    p += (p2 + (p2 << 1)) >> 4;
                    return p;

                default:
                    return 0;
            }
        }

        private double ADPCMMash(uint shiftamount, byte filter, Int16[] PCM_data, int pn, bool write, bool is_end_point) // PCM_data[16]
        {
            double d2 = 0.0;
            Int16 l1 = p1;
            Int16 l2 = p2;
            int step = 1 << (int)shiftamount;

            int vlin, d, da, dp, c;

            for (int i = 0; i < 16; ++i)
            {
                /* make linear prediction for next sample */
                /*      vlin = (v0 * iCoef[0] + v1 * iCoef[1]) >> 8; */
                vlin = get_brr_prediction(filter, l1, l2) >> 1;
                d = (PCM_data[pn + i] >> 1) - vlin;      /* difference between linear prediction and current sample */
                da = Math.Abs(d);
                if (wrap_en && da > 16384 && da < 32768)
                {
                    /* Take advantage of wrapping */
                    d = d - 32768 * (d >> 24);
                    //if (write) printf("Caution : Wrapping was used.\n");
                }
                dp = d + (step << 2) + (step >> 2);
                c = 0;
                if (dp > 0)
                {
                    if (step > 1)
                        c = dp / (step / 2);
                    else
                        c = dp * 2;
                    if (c > 15)
                        c = 15;
                }
                c -= 8;
                dp = (c << (int)shiftamount) >> 1;       /* quantized estimate of samp - vlin */
                                                         /* edge case, if caller even wants to use it */
                if (shiftamount > 12)
                    dp = (dp >> 14) & ~0x7FF;
                c &= 0x0f;      /* mask to 4 bits */

                l2 = l1;            /* shift history */
                l1 = (Int16)(CLAMP_16(vlin + dp) * 2);

                d = PCM_data[pn + i] - l1;
                d2 += (double)d * d;        /* update square-error */

                if (write)                  /* if we want output, put it in proper place */
                    BRR[1 + (i >> 1)] |= (byte)(((i & 1) == 1) ? c : c << 4);
            }

            if (is_end_point)
                switch (filter_at_loop)
                {   /* Also account for history points when looping is enabled & filters used */
                    case 0:
                        d2 /= 16.0;
                        break;

                    /* Filter 1 */
                    case 1:
                        d = l1 - p1_at_loop;
                        d2 += (double)d * d;
                        d2 /= 17.0;
                        break;

                    /* Filters 2 & 3 */
                    default:
                        d = l1 - p1_at_loop;
                        d2 += (double)d * d;
                        d = l2 - p2_at_loop;
                        d2 += (double)d * d;
                        d2 /= 18.0;
                        break;
                }
            else
                d2 /= 16.0;

            if (write)
            {   /* when generating real output, we want to return these */
                p1 = l1;
                p2 = l2;

                BRR[0] = (byte)(((int)shiftamount << 4) | (filter << 2));
                if (is_end_point)
                    BRR[0] |= 1;                        //Set the end bit if we're on the last block
            }
            return d2;
        }

        // Encode a ADPCM block using brute force over filters and shift amounts
        private void ADPCMBlockMash(Int16[] PCM_data, int pn, bool is_loop_point, bool is_end_point)
        {
            int smin = 0, kmin = 0;
            double dmin = System.Double.PositiveInfinity;
            for (int s = 0; s < 13; ++s)
                for (int k = 0; k < 4; ++k)
                    if (FIRen[k])
                    {
                        double d = ADPCMMash((uint)s, (byte)k, PCM_data, pn, false, is_end_point);
                        if (d < dmin)
                        {
                            kmin = k;       //Memorize the filter, shift values with smaller error
                            dmin = d;
                            smin = s;
                        }
                    }

            if (is_loop_point)
            {
                filter_at_loop = (byte)kmin;
                p1_at_loop = p1;
                p2_at_loop = p2;
            }
            ADPCMMash((uint)smin, (byte)kmin, PCM_data, pn, true, is_end_point);
            FIRstats[kmin]++;
        }

        private Int16[] resample(Int16[] samples, int samples_length, int out_length, char type)
        {
            double ratio = (double)samples_length / (double)out_length;
            //pcm_t *out = safe_malloc(2 * out_length);
            Int16[] outBuf = new Int16[out_length];


            //printf("Resampling by effective ratio of %f...\n", ratio);

            switch (type)
            {
                case 'n':                               //No interpolation
                    for (int i = 0; i < out_length; ++i)
                    {
                        outBuf[i] = samples[(int)Math.Floor(i * ratio)];
                    }
                    break;
                case 'l':                               //Linear interpolation
                    for (int i = 0; i < out_length; ++i)
                    {
                        int a = (int)(i * ratio);       //Whole part of index
                        double b = i * ratio - a;       //Fractional part of index
                        if ((a + 1) == samples_length)
                        {
                            outBuf[i] = samples[a]; //This used only for the last sample
                        }
                        else
                            outBuf[i] = (Int16)((1 - b) * samples[a] + b * samples[a + 1]);
                    }
                    break;
                case 's':                               //Sine interpolation
                    for (int i = 0; i < out_length; ++i)
                    {
                        int a = (int)(i * ratio);
                        double b = i * ratio - a;
                        double c = (1.0 - Math.Cos(b * PI)) / 2.0;
                        if ((a + 1) == samples_length)
                        {
                            outBuf[i] = samples[a]; //This used only for the last sample
                        }
                        else
                        {
                            outBuf[i] = (Int16)((1 - c) * samples[a] + c * samples[a + 1]);
                        }
                    }
                    break;
                case 'c':                                       //Cubic interpolation
                    for (int i = 0; i < out_length; ++i)
                    {
                        int a = (int)(i * ratio);

                        short s0 = (a == 0) ? samples[0] : samples[a - 1];
                        short s1 = samples[a];
                        short s2 = (a + 1 >= samples_length) ? samples[samples_length - 1] : samples[a + 1];
                        short s3 = (a + 2 >= samples_length) ? samples[samples_length - 1] : samples[a + 2];

                        double a0 = s3 - s2 - s0 + s1;
                        double a1 = s0 - s1 - a0;
                        double a2 = s2 - s0;
                        double b = i * ratio - a;
                        double b2 = b * b;
                        double b3 = b2 * b;
                        outBuf[i] = (Int16)(b3 * a0 + b2 * a1 + b * a2 + s1);
                    }
                    break;

                case 'b':                                   // Bandlimited interpolation
                    int FIR_ORDER = 15;
                    if (ratio > 1.0)
                    {
                        //signed short* samples_antialiased = safe_malloc(2 * samples_length);
                        Int16[] samples_antialiased = new Int16[samples_length];
                        double[] fir_coefs = new double[FIR_ORDER + 1];

                        // Compute FIR coefficients
                        for (int k = 0; k <= FIR_ORDER; ++k)
                            fir_coefs[k] = sinc(k / ratio) / ratio;

                        // Apply FIR filter to samples
                        for (int i = 0; i < samples_length; ++i)
                        {
                            double acc = samples[i] * fir_coefs[0];
                            for (int k = FIR_ORDER; k > 0; --k)
                            {
                                acc += fir_coefs[k] * ((i + k < samples_length) ? samples[i + k] : samples[samples_length - 1]);
                                acc += fir_coefs[k] * ((i - k >= 0) ? samples[i - k] : samples[0]);
                            }
                            samples_antialiased[i] = (Int16)acc;
                        }

                        //free(samples);
                        samples = samples_antialiased;
                    }
                    // Actual resampling using sinc interpolation
                    for (int i = 0; i < out_length; ++i)
                    {
                        double a = i * ratio;
                        double acc = 0.0;
                        for (int j = (int)(a - FIR_ORDER); j <= (int)(a + FIR_ORDER); ++j)
                        {
                            Int16 sample;
                            if (j >= 0)
                            {
                                if (j < samples_length)
                                {
                                    sample = samples[j];
                                }
                                else
                                {
                                    sample = samples[samples_length - 1];
                                }
                            }
                            else
                            {
                                sample = samples[0];
                            }

                            acc += sample * sinc(a - j);
                        }
                        outBuf[i] = (Int16)acc;
                    }
                    break;

                default:
                    break;
            }

            // No longer need the non-resampled version of the sample
            return outBuf;
        }

        // This function applies a treble boosting filter that compensates the gauss lowpass filter
        private Int16[] treble_boost_filter(Int16[] samples, int length)
        {   // Tepples' coefficient multiplied by 0.6 to avoid overflow in most cases
            double[] coefs = new double[8] { 0.912962, -0.16199, -0.0153283, 0.0426783, -0.0372004, 0.023436, -0.0105816, 0.00250474 };

            Int16[] outBuf = new Int16[length]; 
            for (int i = 0; i < length; ++i)
            {
                double acc = samples[i] * coefs[0];
                for (int k = 7; k > 0; --k)
                {
                    acc += coefs[k] * ((i + k < length) ? samples[i + k] : samples[length - 1]);
                    acc += coefs[k] * ((i - k >= 0) ? samples[i - k] : samples[0]);
                }
                outBuf[i] = (Int16)acc;
            }
            return outBuf;
        }

        public BRRSample Encode(IT.Sample smp, bool enhanceTreble, decimal resampleFactor)
        {
            decimal ratio = resampleFactor; // Resampling factor (range ]0..4])
            byte loop_flag = 0;             // = 0x02 if loop flag is active
            bool fix_loop_en = false;       // True if fixed loop is activated
            uint loop_start = 0;            // Starting point of loop
            uint truncate_len = 0;          // Point at which input wave will be truncated (if = 0, input wave is not truncated)
            bool treble_boost = enhanceTreble;
            BRRSample bs = new BRRSample();

            byte[] outData = new byte[smp.Length * 2];
            BinaryWriter bw = new BinaryWriter(new MemoryStream(outData, true));

            if (smp.UseLoop)
            {
                loop_flag = 0x02;
                loop_start = smp.LoopBeg;
                fix_loop_en = true;
            }

            // Output buffer
            uint samples_length = smp.Length;
            // Optional truncation of input sample
            if ((truncate_len != 0) && (truncate_len < samples_length))
            {
                samples_length = truncate_len;
            }

            Int16[] samples = smp.Data; 

            uint target_length = (uint)(samples_length / ratio);

            uint new_loopsize = 0;
            if(fix_loop_en)
            {
                long loopsize = ((long)(samples_length - loop_start) * target_length) / samples_length;
            	// New loopsize is the multiple of 16 that comes after loopsize
            	new_loopsize = (uint)(((loopsize + 15)/16)*16);
            	// Adjust resampling
            	target_length = (uint)(((Int64)target_length * new_loopsize) / loopsize);
            }

            bs.ResampleRatio = (decimal)samples_length / (decimal)target_length;
            samples = resample(samples, (int)samples_length, (int)target_length, resample_type);
            samples_length = target_length;


            // Apply trebble boost filter (gussian lowpass compensation) if requested by user
            if (treble_boost)
            {
                samples = treble_boost_filter(samples, (int)samples_length);
            }



            if ((samples_length % 16) != 0)
            {
            	int padding = (int)(16 - (samples_length % 16));

                Int16[] padSamples = new Int16[2 * (samples_length + padding)];
                samples.CopyTo(padSamples, padding);
                samples = padSamples;
                samples_length += (uint)padding;

            }


            bool initial_block = false;
            for (int i=0; i<16; ++i)					//Initialization needed if any of the first 16 samples isn't zero
            	initial_block |= samples[i]!=0;

            if (initial_block)
            {   //Write initial BRR block
                byte[] initial_block_b = new byte[9] { loop_flag, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

                bw.Write(initial_block_b, 0, 9);
            }

            p1 = 0;
            p2 = 0;

            for (int n = 0; n < samples_length; n += 16)
            {
                //Clear BRR buffer
                for (int bc = 0; bc < 9; bc++)
                {
                    BRR[bc] = 0;
                }

                //Encode BRR block, tell the encoder if we're at loop point (if loop is enabled), and if we're at end point
                ADPCMBlockMash(samples, n, fix_loop_en && (n == (samples_length - new_loopsize)), n == samples_length - 16);

                //Set the loop flag if needed
                BRR[0] |= loop_flag;
                bw.Write(BRR, 0, 9);
            }


            if (fix_loop_en)
            {
                uint k = samples_length - (initial_block ? new_loopsize - 16 : new_loopsize);
                bs.LoopPoint = (int)((k / 16) * 9);
            }

            long brrLength = bw.BaseStream.Position;
            bs.Data = new byte[brrLength];                
            bw.Close();
            Array.Copy(outData, bs.Data, brrLength);
            return bs;
        }
    }
}
