using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessing { 
    public partial class MainForm : Form
    {
        ProcessingParameters parameters;
        ImageProcess crawler;
        Dictionary<string, string> searchFilters;
        BackgroundWorker worker;

        public MainForm()
        {
            parameters = new ProcessingParameters();
            crawler = new ImageProcess(Application.StartupPath);
            //crawler.FilenameFilter = "P4170568";// "P4170539"; //"P4170568";

            parameters.SourceFolder = Properties.Settings.Default.LastSourceFolder;
            parameters.DestinationFolder = Properties.Settings.Default.LastDestinationFolder;
            parameters.RecurseSubfolders = Properties.Settings.Default.RecurseSubfolders;
            parameters.DestinationQuality = Properties.Settings.Default.DestinationQuality;
            parameters.OverwriteExisting = Properties.Settings.Default.OverwriteExisting;
            parameters.SourceImageType = Properties.Settings.Default.SourceImageType;
            parameters.DestinationImageType = Properties.Settings.Default.DestinationImageType;

            crawler.Parameters = parameters;

            InitializeComponent();

            searchFilters = new Dictionary<string, string>();
            searchFilters.Add("JPEG", "*.jpg");
            searchFilters.Add("PNG", "*.png");
            searchFilters.Add("TIFF", "*.tif");

            worker = new BackgroundWorker();
            worker.DoWork += crawler.ProcessFiles;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            crawler.Worker = worker;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            btnStart.Enabled = true;
            btnCancel.Enabled = false;

            pgBar.Value = 0;

            if (e.Cancelled)
            {
                lblCurrentFile.Text = "Processing of images cancelled";
            }
            else if (e.Error != null)
            {
                lblCurrentFile.Text = "There was an error during image processing. The thread aborted";
            }
            else
            {
                lblCurrentFile.Text = "Processing of images completed";
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pgBar.Value = e.ProgressPercentage;

            var currentFile = e.UserState as ImageProcess.CurrentImage;
            if (currentFile!=null)
            {
                lblCurrentFile.Text = currentFile.currentFile;

                if (chkDisplayImages.Checked)
                {
                    pictureBox1.ImageLocation = currentFile.inputFile;
                    pictureBox2.ImageLocation = currentFile.outputFile;
                }
            }
            else
            {
                var error = e.UserState as ImageProcess.ErrorMessage;
                if (error != null)
                {
                    MessageBox.Show(error.message + "\n" + error.stackTrace);
                }
            }
        }        

        private void MainForm_Load(object sender, EventArgs e)
        {
            bindingSource.DataSource = parameters;
            lblNbSelectedFiles.Text = "";
            lblCurrentFile.Text = "";
            btnCancel.Enabled = false;
        }

        private bool GenerateFileQueue()
        {

            if (Directory.Exists(parameters.SourceFolder))
            {
                string searchFilter = "*.*";
                searchFilters.TryGetValue(parameters.SourceImageType, out searchFilter);

                crawler.GenerateFileQueue(parameters.SourceFolder, searchFilter, parameters.RecurseSubfolders);

                lblNbSelectedFiles.Text = crawler.QueuedFiles.Count().ToString();

                return true;
            }
            else
            {
                MessageBox.Show("Source folder not found", "Invalid parameters", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(parameters.SourceFolder) && Directory.Exists(parameters.SourceFolder))
            {
                folderBrowserDialog.SelectedPath = parameters.SourceFolder;
            }
            else
                folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                parameters.SourceFolder = folderBrowserDialog.SelectedPath;
                bindingSource.ResetBindings(false);
                GenerateFileQueue();

                Properties.Settings.Default.LastSourceFolder = parameters.SourceFolder;
                Properties.Settings.Default.Save();

            }

        }

        private void btnBrowseDestination_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(parameters.DestinationFolder) && Directory.Exists(parameters.DestinationFolder))
            {
                folderBrowserDialog.SelectedPath = parameters.SourceFolder;
            }
            else
                folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                parameters.DestinationFolder = folderBrowserDialog.SelectedPath;
                bindingSource.ResetBindings(false);

                Properties.Settings.Default.LastDestinationFolder = parameters.DestinationFolder;
                Properties.Settings.Default.Save();

            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!GenerateFileQueue()) return;

            if (crawler.QueuedFiles.Count() == 0)
                MessageBox.Show("No image in queue");
            else
            {
                Properties.Settings.Default.LastSourceFolder = parameters.SourceFolder;
                Properties.Settings.Default.LastDestinationFolder = parameters.DestinationFolder;
                Properties.Settings.Default.RecurseSubfolders = parameters.RecurseSubfolders;
                Properties.Settings.Default.DestinationQuality = parameters.DestinationQuality;
                Properties.Settings.Default.OverwriteExisting = parameters.OverwriteExisting;
                Properties.Settings.Default.SourceImageType = parameters.SourceImageType;
                Properties.Settings.Default.DestinationImageType = parameters.DestinationImageType;

                Properties.Settings.Default.Save();

                btnStart.Enabled = false;
                btnCancel.Enabled = true;
                worker.RunWorkerAsync();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (worker.IsBusy)
            {
                worker.CancelAsync();
            }
        }
    }
}

