using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace ImageToolbox.UnderwaterImagery
{
    /// <summary>
    /// Implementation in C# of Paralenz algorithm using Emgu.CV for performance
    /// Adapted from https://github.com/nikolajbech/underwater-image-color-correction
    /// </summary>
    public static class Paralenz
    {

        /// <summary>
        /// Apply Paralenz filter to input image file and save as output
        /// </summary>
        /// <param name="inputFilePath">File path of input image</param>
        /// <param name="outputFilePath">File path of output image</param>
        public static void ApplyFilter(string inputFilePath, string outputFilePath, 
            ImageCodecInfo imageCodec, EncoderParameters encoderParams)
        {
            /*
            var mat = CvInvoke.Imread(inputFilePath.Replace("P4170568", "P4170539"), LoadImageType.Color);
            var inputMat = mat.ToImage<Bgr, byte>(); mat.Dispose();
            var filterMatrix = GetFilterMatrix(inputMat, false);

            var mat2 = CvInvoke.Imread(inputFilePath, LoadImageType.Color);
            var inputMat2 = mat2.ToImage<Bgr, byte>(); mat2.Dispose();
            ApplyFilter(inputMat2, filterMatrix);

            inputMat2.Bitmap.Save(outputFilePath, imageCodec, encoderParams);
            inputMat.Dispose();
            inputMat2.Dispose();*/

            
            var mat = CvInvoke.Imread(inputFilePath, LoadImageType.Color);
            var inputMat = mat.ToImage<Bgr, byte>(); mat.Dispose();

            ApplyFilter(inputMat);

            inputMat.Bitmap.Save(outputFilePath, imageCodec, encoderParams);
            inputMat.Dispose();
            

            GC.Collect();
        }

        /// <summary>
        /// Returns the filter parameters calculated from input image
        /// 1. Calculate the average color for image.
        /// 2. Hueshift the colors into the red channel until a minimum red average value of 60.
        /// 3. Create RGB histogram with new red color.
        /// 4. Find low threshold level and high threshold level.
        /// 5. Normalize array so threshold level is equal to 0 and threshold high is equal to 255.
        /// 6. Create color filter matrix based on the new values.
        /// </summary>
        /// <param name="inputOutputImage">Input image</param>
        /// <param name="applyToImage">If true the filter is applied to the input image</param>
        /// <returns></returns>

        public static FilterMatrix GetFilterMatrix(Image<Bgr, byte> inputOutputImage, bool applyToImage)
        {
            var minAvgRed = 60;
            var maxHueShift = 120;
            var thresholdRatio = 2000;
            var blueMagicValue = 1.2;

            var channels = inputOutputImage.Split();

            // Using short for temporary layers instead of float to reduce memory footprint

            var redChannel = channels[2].Convert<Gray, short>();
            var greenChannel = channels[1].Convert<Gray, short>();
            var blueChannel = channels[0].Convert<Gray, short>();

            var average = CvInvoke.Mean(inputOutputImage); 

            var avgR = average.V2;
            var avgG = average.V1;
            var avgB = average.V0;

            var numOfPixels = inputOutputImage.Width * inputOutputImage.Height;
            var thresholdLevel = numOfPixels / thresholdRatio;

            // Calculate shift amount:
            var newAvgRed = avgR;
            double hueShift = 0;

            while (newAvgRed < minAvgRed)
            {
                newAvgRed = hueShiftRed(avgR, avgG, avgB, hueShift).Sum;
                hueShift++;
                if (hueShift > maxHueShift) newAvgRed = 60; // Max value
            }

            float[] hist_B, hist_G, hist_R;

            var singleImageTable = new Image<Gray, byte>[1];

            var denseHistogram = new DenseHistogram(256, new RangeF(0, 255));
            singleImageTable[0] = channels[0];

            denseHistogram.Calculate(singleImageTable, false, null);
            hist_B = denseHistogram.GetBinValues();

            denseHistogram.Clear();

            singleImageTable[0] = channels[1];
            denseHistogram.Calculate(singleImageTable, false, null);
            hist_G = denseHistogram.GetBinValues();

            for (int i = 0; i < 3; i++) channels[i].Dispose();
            GC.Collect();

            var U = Math.Cos(hueShift * Math.PI / 180);
            var W = Math.Sin(hueShift * Math.PI / 180);

            var rfactor = (0.299 + 0.701 * U + 0.168 * W);
            var gfactor = (0.587 - 0.587 * U + 0.330 * W);
            var bfactor = (0.114 - 0.114 * U - 0.497 * W);

            Image<Gray, short> t1, t2, t3, t4, t5, t6, t7;

            // Important : the initial code was a single line using image operators
            // The code is more readable and cleaner but it creates temporary images which
            // are never disposed and thus memory leaks that quickly use all available memory
            // and crash the application.

            t1 = redChannel * rfactor;
            t2 = greenChannel * gfactor;
            t3 = t1 + t2; t1.Dispose(); t2.Dispose();
            t4 = blueChannel * bfactor;
            t5 = t3 + t4; t3.Dispose(); t4.Dispose();
            t6 = t5.Max(0); t5.Dispose();
            t7 = t6.Min(255); t6.Dispose();

            var redChannelWithHueCorrection = t7.Convert<Gray, byte>(); t7.Dispose();

            denseHistogram.Clear();

            singleImageTable[0] = redChannelWithHueCorrection;
            denseHistogram.Calculate(singleImageTable, false, null);
            hist_R = denseHistogram.GetBinValues();

            redChannelWithHueCorrection.Dispose();

            denseHistogram.Dispose();
            GC.Collect();

            var normalize_R = new List<int>();
            var normalize_G = new List<int>();
            var normalize_B = new List<int>();

            // Push 0 as start value in normalize array:
            normalize_R.Add(0);
            normalize_G.Add(0);
            normalize_B.Add(0);

            // Find values under threshold:
            for (var i = 0; i < 255; i++)
            {
                // Note: ignoring missing values (PNG compression)
                // Doesn't work for deep photos
                //if (hist_R[i]>0 && hist_R[i] - thresholdLevel < 2) normalize_R.Add(i);
                //if (hist_G[i]>0 && hist_G[i] - thresholdLevel < 2) normalize_G.Add(i);
                //if (hist_B[i]>0 && hist_B[i] - thresholdLevel < 2) normalize_B.Add(i);

                if (hist_R[i] - thresholdLevel < 2) normalize_R.Add(i);
                if (hist_G[i] - thresholdLevel < 2) normalize_G.Add(i);
                if (hist_B[i] - thresholdLevel < 2) normalize_B.Add(i);
            }

            // Push 255 as end value in normalize array;
            normalize_R.Add(255);
            normalize_G.Add(255);
            normalize_B.Add(255);

            var adjust_R = normalizingInterval(normalize_R);
            var adjust_G = normalizingInterval(normalize_G);
            var adjust_B = normalizingInterval(normalize_B);

            // Make histogram:
            var shifted = hueShiftRed(1, 1, 1, hueShift);

            var filterMatrix = new FilterMatrix();

            filterMatrix.redGain = 256.0 / (adjust_R.high - adjust_R.low);
            filterMatrix.greenGain = 256.0 / (adjust_G.high - adjust_G.low);
            filterMatrix.blueGain = 256.0 / (adjust_B.high - adjust_B.low);

            filterMatrix.redOffset = (-adjust_R.low / 256.0) * filterMatrix.redGain;
            filterMatrix.greenOffset = (-adjust_G.low / 256.0) * filterMatrix.greenGain;
            filterMatrix.blueOffset = (-adjust_B.low / 256.0) * filterMatrix.blueGain;

            filterMatrix.adjstRed = shifted.R * filterMatrix.redGain;
            filterMatrix.adjstRedGreen = shifted.G * filterMatrix.redGain;
            filterMatrix.adjstRedBlue = shifted.B * filterMatrix.redGain * blueMagicValue;

            if (applyToImage)
                ApplyFilterToImage(inputOutputImage, redChannel, greenChannel, blueChannel, filterMatrix);

            redChannel.Dispose();
            blueChannel.Dispose();
            greenChannel.Dispose();

            // Force GC to release memory
            GC.Collect();

            return filterMatrix;

        }

        private static void ApplyFilterToImage(Image<Bgr, byte> inputOutputImage, 
            Image<Gray, short> redChannel, Image<Gray, short> greenChannel, 
            Image<Gray, short> blueChannel, FilterMatrix filterMatrix)
        {
            Image<Gray, short> t1, t2, t3, t4, t5, t6, t7, t8;

            t1 = redChannel * filterMatrix.adjstRed;
            t2 = greenChannel * filterMatrix.adjstRedGreen;
            t3 = t1 + t2; t1.Dispose(); t2.Dispose();
            t4 = blueChannel * filterMatrix.adjstRedBlue;
            t5 = t3 + t4; t3.Dispose(); t4.Dispose();
            t6 = t5 + filterMatrix.redOffset * 255; t5.Dispose();
            t7 = t6.Min(255); t6.Dispose();
            t8 = t7.Max(0); t7.Dispose();

            var chanR = t8.Convert<Gray, byte>(); t8.Dispose();

            t1 = greenChannel * filterMatrix.greenGain;
            t2 = t1 + filterMatrix.greenOffset * 255; t1.Dispose();
            t3 = t2.Min(255); t2.Dispose();
            t4 = t3.Max(0); t3.Dispose();

            var chanG = t4.Convert<Gray, byte>(); t4.Dispose();

            t1 = blueChannel * filterMatrix.blueGain;
            t2 = t1 + filterMatrix.blueOffset * 255; t1.Dispose();
            t3 = t2.Min(255); t2.Dispose();
            t4 = t3.Max(0); t3.Dispose();

            var chanB = t4.Convert<Gray, byte>(); t4.Dispose();

            var vector = new VectorOfMat(chanB.Mat, chanG.Mat, chanR.Mat);
            CvInvoke.Merge(vector, inputOutputImage);

            chanR.Dispose(); chanB.Dispose(); chanG.Dispose(); vector.Dispose();
        }

        public static void ApplyFilter(Image<Bgr, byte> inputOutputImage, FilterMatrix filterMatrix)
        {
            var channels = inputOutputImage.Split();

            // Using Int16 for temporary layers to reduce memory footprint

            var redChannel = channels[2].Convert<Gray, short>();
            var greenChannel = channels[1].Convert<Gray, short>();
            var blueChannel = channels[0].Convert<Gray, short>();

            for (int i = 0; i < 3; i++) channels[i].Dispose();

            ApplyFilterToImage(inputOutputImage, redChannel, greenChannel, blueChannel, filterMatrix);

            redChannel.Dispose();
            blueChannel.Dispose();
            greenChannel.Dispose();

            // Force GC to release memory
            GC.Collect();

        }

        public static void ApplyFilter(Image<Bgr, byte> inputOutputImage)
        {

            GetFilterMatrix(inputOutputImage, true);

            //var filterMatrix = GetFilterMatrix(inputOutputImage, false);
            //ApplyFilter(inputOutputImage, filterMatrix);
        }

        private static HueShift hueShiftRed(double r, double g, double b, double h)
        {
            var result = new HueShift();

            var U = Math.Cos(h * Math.PI / 180);
            var W = Math.Sin(h * Math.PI / 180);

            result.R = (0.299 + 0.701 * U + 0.168 * W) * r;
            result.G = (0.587 - 0.587 * U + 0.330 * W) * g;
            result.B = (0.114 - 0.114 * U - 0.497 * W) * b;

            return result;
        }
        private static HighLow normalizingInterval(List<int> normArray)
        {
            var result = new HighLow();

            var high = 255;
            var low = 0;
            var maxDist = 0;

            var array = normArray.ToArray();

            for (var i = 1; i < array.Length; i++)
            {
                var dist = normArray[i] - normArray[i - 1];
                if (dist > maxDist)
                {
                    maxDist = dist;
                    high = normArray[i];
                    low = normArray[i - 1];
                }
            }

            result.low = low;
            result.high = high;

            return result;
        }

        private struct HighLow
        {
            public int high;
            public int low;
        }

        private struct HueShift
        {
            public double R;
            public double G;
            public double B;

            public double Sum { get { return R + G + B; } }
        }

        public struct FilterMatrix
        {
            public double redGain;
            public double greenGain;
            public double blueGain;

            public double redOffset;
            public double greenOffset;
            public double blueOffset;

            public double adjstRed;
            public double adjstRedGreen;
            public double adjstRedBlue;
        }
    }
}
