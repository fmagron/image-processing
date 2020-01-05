using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public abstract class Crawler
    {
        protected DateTime startTime;
        protected int nbActuallyProcessedFiles;
        protected int filesProcessed;
        protected int totalNumberFiles;
        public BackgroundWorker Worker { get; set; }

        private ProgressReport progressReport;

        private string folderFilter;

        public string FolderFilter
        {
            get { return folderFilter; }
            set { folderFilter = value; }
        }

        private string filenameFilter;

        public string FilenameFilter
        {
            get { return filenameFilter; }
            set { filenameFilter = value; }
        }

        public struct QueuedFile
        {
            private string filePath;
            public string FilePath
            {
                get { return filePath; }
            }

            private string folder;
            public string Folder
            {
                get { return folder; }
            }

            public QueuedFile(string filePath, string folder)
            {
                this.filePath = filePath;
                this.folder = folder;
            }
        }

        private Queue<QueuedFile> queuedFiles;

        public Queue<QueuedFile> QueuedFiles {  get { return queuedFiles; } }

        Regex regexFolderFilter;
        Regex regexFilenameFilter;

        public class ProgressReport
        {
            #region Private members

            private string currentFolder;
            private string currentFile;

            #endregion

            #region Properties

            public string CurrentFolder
            {
                get { return currentFolder; }
                set { currentFolder = value; }
            }

            public string CurrentFile
            {
                get { return currentFile; }
                set { currentFile = value; }
            }

            #endregion

            public ProgressReport()
            {
                currentFolder = "";
                currentFile = "";
            }

        }

        private DateTime lastTime;

        public string RootPath { get { return rootPath; } }

        private string rootPath;
        protected int progressPercentage;

        public Crawler(string rootPath)
        {
            progressReport = new ProgressReport();
            queuedFiles = new Queue<QueuedFile>();

            this.rootPath = rootPath;
            if (!rootPath.EndsWith("\\")) this.rootPath += "\\";
        }

        public void GenerateFileQueue(string rootPath, string searchPattern, bool recurse)
        {

            this.rootPath = rootPath;
            if (!rootPath.EndsWith("\\")) this.rootPath += "\\";

            queuedFiles.Clear();

            lastTime = DateTime.Now;

            if (folderFilter != null && folderFilter.Length > 0)
            {
                regexFolderFilter = new Regex(folderFilter);
            }

            if (filenameFilter != null && filenameFilter.Length > 0)
            {
                regexFilenameFilter = new Regex(filenameFilter);
            }

            string startPath = null;

            if (FolderFilter != null)
            {
                startPath = Path.Combine(this.rootPath, FolderFilter.Substring(1).Replace(@"\\", @"\"));
            }
            else
                startPath = this.rootPath;

            ProcessDir(startPath, searchPattern, recurse);
        }


        public void ProcessFiles(object sender, DoWorkEventArgs e)
        {
            startTime = DateTime.Now;

            totalNumberFiles = queuedFiles.Count;
            filesProcessed = 0;
            nbActuallyProcessedFiles = 0;

            progressPercentage = 0;

            while (queuedFiles.Count > 0 && (Worker==null || !Worker.CancellationPending))
            {
                QueuedFile file = queuedFiles.Dequeue();

                progressReport.CurrentFile = System.IO.Path.GetFileName(file.FilePath);

                int newProgressPercentage = Convert.ToInt32((filesProcessed * 100.0) / totalNumberFiles);
                if (newProgressPercentage > progressPercentage
                    || DateTime.Now.Subtract(lastTime).Seconds > 0)
                {
                    lastTime = DateTime.Now;
                    progressPercentage = newProgressPercentage;

                    if (Worker!=null)
                    Worker.ReportProgress(progressPercentage, progressReport);
                }

                ProcessFile(file);
                filesProcessed++;
            }

            if (Worker != null && Worker.CancellationPending)
            {
                e.Cancel = true;
                Worker.ReportProgress(0);
            }
        }

        abstract protected void ProcessFile(QueuedFile file);

        public void ProcessDir(string sourceDir, string searchPattern, bool recurse)
        {

            string[] fileEntries = Directory.GetFiles(sourceDir, searchPattern);

            string pathRoot = System.IO.Path.GetFullPath(sourceDir);
            string folder = pathRoot.Replace(rootPath, "");

            progressReport.CurrentFolder = folder;

            foreach (string filePath in fileEntries)
            {

                if (regexFilenameFilter == null || regexFilenameFilter.IsMatch(Path.GetFileName(filePath)))
                    queuedFiles.Enqueue(new QueuedFile(filePath, folder));
            }

            //Worker.ReportProgress(0, progressReport);


            // Recurse into subdirectories of this directory.

            if (recurse)
            {
                string[] subdirEntries = Directory.GetDirectories(sourceDir);
                foreach (string subdir in subdirEntries)
                {
                    if (regexFolderFilter == null || regexFolderFilter.IsMatch(subdir.Replace(rootPath, "")))
                    {
                        // Do not iterate through reparse points
                        if ((File.GetAttributes(subdir) &
                             FileAttributes.ReparsePoint) !=
                                 FileAttributes.ReparsePoint)

                            ProcessDir(subdir, searchPattern, recurse);
                    }
                    //if (Worker.CancellationPending) return;
                }
            }
        }
    }
}

