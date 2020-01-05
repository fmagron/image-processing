namespace ImageProcessing
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtSourceFolder = new System.Windows.Forms.TextBox();
            this.btnBrowseSource = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDestinationFolder = new System.Windows.Forms.TextBox();
            this.btnBrowseDestination = new System.Windows.Forms.Button();
            this.chkRecurse = new System.Windows.Forms.CheckBox();
            this.cbImageFormats = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtQuality = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbImageTypes = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblNbSelectedFiles = new System.Windows.Forms.Label();
            this.chkDisplayImages = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.rdFilter = new System.Windows.Forms.RadioButton();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblCurrentFile = new System.Windows.Forms.Label();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSourceFolder
            // 
            this.txtSourceFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "SourceFolder", true));
            this.txtSourceFolder.Location = new System.Drawing.Point(113, 12);
            this.txtSourceFolder.Name = "txtSourceFolder";
            this.txtSourceFolder.Size = new System.Drawing.Size(403, 20);
            this.txtSourceFolder.TabIndex = 3;
            // 
            // btnBrowseSource
            // 
            this.btnBrowseSource.Location = new System.Drawing.Point(522, 11);
            this.btnBrowseSource.Name = "btnBrowseSource";
            this.btnBrowseSource.Size = new System.Drawing.Size(79, 23);
            this.btnBrowseSource.TabIndex = 2;
            this.btnBrowseSource.Text = "Browse...";
            this.btnBrowseSource.UseVisualStyleBackColor = true;
            this.btnBrowseSource.Click += new System.EventHandler(this.btnBrowseSource_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Source folder :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Destination folder :";
            // 
            // txtDestinationFolder
            // 
            this.txtDestinationFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "DestinationFolder", true));
            this.txtDestinationFolder.Location = new System.Drawing.Point(113, 99);
            this.txtDestinationFolder.Name = "txtDestinationFolder";
            this.txtDestinationFolder.Size = new System.Drawing.Size(403, 20);
            this.txtDestinationFolder.TabIndex = 6;
            // 
            // btnBrowseDestination
            // 
            this.btnBrowseDestination.Location = new System.Drawing.Point(522, 97);
            this.btnBrowseDestination.Name = "btnBrowseDestination";
            this.btnBrowseDestination.Size = new System.Drawing.Size(79, 23);
            this.btnBrowseDestination.TabIndex = 7;
            this.btnBrowseDestination.Text = "Browse...";
            this.btnBrowseDestination.UseVisualStyleBackColor = true;
            this.btnBrowseDestination.Click += new System.EventHandler(this.btnBrowseDestination_Click);
            // 
            // chkRecurse
            // 
            this.chkRecurse.AutoSize = true;
            this.chkRecurse.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSource, "RecurseSubfolders", true));
            this.chkRecurse.Location = new System.Drawing.Point(608, 15);
            this.chkRecurse.Name = "chkRecurse";
            this.chkRecurse.Size = new System.Drawing.Size(117, 17);
            this.chkRecurse.TabIndex = 8;
            this.chkRecurse.Text = "Recurse subfolders";
            this.chkRecurse.UseVisualStyleBackColor = true;
            // 
            // cbImageFormats
            // 
            this.cbImageFormats.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.bindingSource, "DestinationImageType", true));
            this.cbImageFormats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImageFormats.FormattingEnabled = true;
            this.cbImageFormats.Items.AddRange(new object[] {
            "JPEG",
            "PNG",
            "TIFF"});
            this.cbImageFormats.Location = new System.Drawing.Point(113, 125);
            this.cbImageFormats.Name = "cbImageFormats";
            this.cbImageFormats.Size = new System.Drawing.Size(77, 21);
            this.cbImageFormats.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Destination format :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(198, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Quality :";
            // 
            // txtQuality
            // 
            this.txtQuality.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "DestinationQuality", true));
            this.txtQuality.Location = new System.Drawing.Point(249, 125);
            this.txtQuality.Name = "txtQuality";
            this.txtQuality.Size = new System.Drawing.Size(53, 20);
            this.txtQuality.TabIndex = 12;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(522, 152);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(79, 23);
            this.btnStart.TabIndex = 13;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // pgBar
            // 
            this.pgBar.Location = new System.Drawing.Point(113, 152);
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(403, 23);
            this.pgBar.TabIndex = 14;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(607, 152);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(79, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Progress :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Image type :";
            // 
            // cbImageTypes
            // 
            this.cbImageTypes.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.bindingSource, "SourceImageType", true));
            this.cbImageTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImageTypes.FormattingEnabled = true;
            this.cbImageTypes.Items.AddRange(new object[] {
            "JPEG",
            "PNG",
            "TIFF"});
            this.cbImageTypes.Location = new System.Drawing.Point(113, 37);
            this.cbImageTypes.Name = "cbImageTypes";
            this.cbImageTypes.Size = new System.Drawing.Size(77, 21);
            this.cbImageTypes.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(198, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Files selected :";
            // 
            // lblNbSelectedFiles
            // 
            this.lblNbSelectedFiles.AutoSize = true;
            this.lblNbSelectedFiles.Location = new System.Drawing.Point(281, 40);
            this.lblNbSelectedFiles.Name = "lblNbSelectedFiles";
            this.lblNbSelectedFiles.Size = new System.Drawing.Size(47, 13);
            this.lblNbSelectedFiles.TabIndex = 20;
            this.lblNbSelectedFiles.Text = "NNNNN";
            // 
            // chkDisplayImages
            // 
            this.chkDisplayImages.AutoSize = true;
            this.chkDisplayImages.Location = new System.Drawing.Point(12, 188);
            this.chkDisplayImages.Name = "chkDisplayImages";
            this.chkDisplayImages.Size = new System.Drawing.Size(96, 17);
            this.chkDisplayImages.TabIndex = 21;
            this.chkDisplayImages.Text = "Display images";
            this.chkDisplayImages.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Filter :";
            // 
            // rdFilter
            // 
            this.rdFilter.AutoSize = true;
            this.rdFilter.Checked = true;
            this.rdFilter.Location = new System.Drawing.Point(113, 68);
            this.rdFilter.Name = "rdFilter";
            this.rdFilter.Size = new System.Drawing.Size(130, 17);
            this.rdFilter.TabIndex = 23;
            this.rdFilter.TabStop = true;
            this.rdFilter.Text = "Underwater (Paralenz)";
            this.rdFilter.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSource, "OverwriteExisting", true));
            this.checkBox2.Location = new System.Drawing.Point(608, 102);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(109, 17);
            this.checkBox2.TabIndex = 24;
            this.checkBox2.Text = "Overwrite existing";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 219);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(378, 274);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 25;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(396, 219);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(378, 274);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 26;
            this.pictureBox2.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(117, 189);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Currently processing :";
            // 
            // lblCurrentFile
            // 
            this.lblCurrentFile.AutoSize = true;
            this.lblCurrentFile.Location = new System.Drawing.Point(231, 189);
            this.lblCurrentFile.Name = "lblCurrentFile";
            this.lblCurrentFile.Size = new System.Drawing.Size(65, 13);
            this.lblCurrentFile.TabIndex = 28;
            this.lblCurrentFile.Text = "PlaceHolder";
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(ImageProcessing.ProcessingParameters);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 505);
            this.Controls.Add(this.lblCurrentFile);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.rdFilter);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.chkDisplayImages);
            this.Controls.Add(this.lblNbSelectedFiles);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbImageTypes);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pgBar);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtQuality);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbImageFormats);
            this.Controls.Add(this.chkRecurse);
            this.Controls.Add(this.btnBrowseDestination);
            this.Controls.Add(this.txtDestinationFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSourceFolder);
            this.Controls.Add(this.btnBrowseSource);
            this.Name = "MainForm";
            this.Text = "Image processing";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSourceFolder;
        private System.Windows.Forms.Button btnBrowseSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDestinationFolder;
        private System.Windows.Forms.Button btnBrowseDestination;
        private System.Windows.Forms.CheckBox chkRecurse;
        private System.Windows.Forms.ComboBox cbImageFormats;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtQuality;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbImageTypes;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblNbSelectedFiles;
        private System.Windows.Forms.CheckBox chkDisplayImages;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rdFilter;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblCurrentFile;
    }
}

