using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class ProcessingParameters
    {

        public ProcessingParameters()
        {
            SourceImageType = "JPEG";
            DestinationImageType = "JPEG";
            DestinationQuality = 90;
            RecurseSubfolders = true;

            //SourceFolder = @"C:\Data\In_Progress\Benthic Imagery Tonga 2019\Tongatapu\RBT 1";
            //DestinationFolder = @"C:\Data\In_Progress\Benthic Imagery Tonga 2019\Tongatapu_PL\RBT 1";
        }


        public string SourceFolder { get; set; }
        public bool RecurseSubfolders { get; set; }
        public string SourceImageType { get; set; }
        public string DestinationFolder { get; set; }
        public bool OverwriteExisting { get; set; }
        public string DestinationImageType { get; set; }
        public int DestinationQuality { get; set; }
    }
}
