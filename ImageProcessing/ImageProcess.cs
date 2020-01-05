using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class ImageProcess : Crawler
    {

        public class CurrentImage
        {
            public string currentFile { get; set; }
            public string inputFile { get; set; }
            public string outputFile { get; set; }
        }

        public class ErrorMessage
        {
            public string message { get; set; }
            public string stackTrace { get; set; }
        }

        public ProcessingParameters Parameters { get; set; }

        public ImageProcess(string rootPath)
            : base(rootPath)
        {

        }

        protected override void ProcessFile(QueuedFile file)
        {
            try
            {
                var destFolder = Path.Combine(Parameters.DestinationFolder, file.Folder);
                if (!Directory.Exists(destFolder)) Directory.CreateDirectory(destFolder);

                ImageFormat imageFormat;
                string extension;

                switch (Parameters.DestinationImageType)
                {
                    case "JPEG": imageFormat = ImageFormat.Jpeg; extension = ".jpg"; break;
                    case "TIFF": imageFormat = ImageFormat.Tiff; extension = ".tif"; break;
                    case "PNG": imageFormat = ImageFormat.Png; extension = ".png"; break;
                    default: imageFormat = ImageFormat.Jpeg; extension = ".jpg"; break;
                }

                var outFilePath = Path.Combine(destFolder, Path.GetFileNameWithoutExtension(file.FilePath) + extension);

                if (!Parameters.OverwriteExisting && File.Exists(outFilePath)) return;

                var encoder = ImageToolbox.Utils.GetEncoder(imageFormat);
                var qualityEncoder = System.Drawing.Imaging.Encoder.Quality;
                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(qualityEncoder, Parameters.DestinationQuality);


                ImageToolbox.UnderwaterImagery.Paralenz.ApplyFilter(file.FilePath, outFilePath, encoder, encoderParams);

                var currentImage = new CurrentImage()
                {
                    inputFile = file.FilePath,
                    outputFile = outFilePath
                };

                nbActuallyProcessedFiles++;

                var avgDuration = (DateTime.Now - startTime).Ticks / nbActuallyProcessedFiles;
                var remainingTime = new TimeSpan(avgDuration * (totalNumberFiles - filesProcessed));

                currentImage.currentFile = String.Format("{0}/{1} - Finishing in {2} - {3}",
                    filesProcessed, totalNumberFiles, remainingTime.ToString(@"dd\.hh\:mm\:ss"),
                    Path.Combine(file.Folder, Path.GetFileName(file.FilePath)));

                Worker.ReportProgress(progressPercentage, currentImage);

            }
            catch(Exception ex)
            {
                var error = new ErrorMessage() { message = ex.Message, stackTrace = ex.StackTrace };

                Worker.ReportProgress(progressPercentage, error);
                throw ex;
            }
        }
    }
}
