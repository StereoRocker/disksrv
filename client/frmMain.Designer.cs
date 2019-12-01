namespace disksrv_client
{
    partial class frmMain
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
            this.panelLayout = new System.Windows.Forms.TableLayoutPanel();
            this.grpLocalFile = new System.Windows.Forms.GroupBox();
            this.cmdBrowseLocal = new System.Windows.Forms.Button();
            this.txtLocalPath = new System.Windows.Forms.TextBox();
            this.grpServer = new System.Windows.Forms.GroupBox();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.cmdConnect = new System.Windows.Forms.Button();
            this.cmdDisconnect = new System.Windows.Forms.Button();
            this.grpDrives = new System.Windows.Forms.GroupBox();
            this.cmbFloppyType = new System.Windows.Forms.ComboBox();
            this.lblFloppyType = new System.Windows.Forms.Label();
            this.lstServerDisks = new System.Windows.Forms.ListBox();
            this.grpActions = new System.Windows.Forms.GroupBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdWriteFileToDisk = new System.Windows.Forms.Button();
            this.cmdReadDiskToFile = new System.Windows.Forms.Button();
            this.menuStatus = new System.Windows.Forms.StatusStrip();
            this.statLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statProg = new System.Windows.Forms.ToolStripProgressBar();
            this.workerReadDisk = new System.ComponentModel.BackgroundWorker();
            this.workerWriteDisk = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutDisksrvClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineDocumentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelLayout.SuspendLayout();
            this.grpLocalFile.SuspendLayout();
            this.grpServer.SuspendLayout();
            this.grpDrives.SuspendLayout();
            this.grpActions.SuspendLayout();
            this.menuStatus.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLayout
            // 
            this.panelLayout.ColumnCount = 2;
            this.panelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelLayout.Controls.Add(this.grpLocalFile, 0, 1);
            this.panelLayout.Controls.Add(this.grpDrives, 0, 2);
            this.panelLayout.Controls.Add(this.grpActions, 1, 2);
            this.panelLayout.Controls.Add(this.menuStatus, 0, 3);
            this.panelLayout.Controls.Add(this.grpServer, 0, 0);
            this.panelLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLayout.Location = new System.Drawing.Point(0, 24);
            this.panelLayout.Name = "panelLayout";
            this.panelLayout.RowCount = 4;
            this.panelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panelLayout.Size = new System.Drawing.Size(434, 262);
            this.panelLayout.TabIndex = 2;
            // 
            // grpLocalFile
            // 
            this.grpLocalFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLayout.SetColumnSpan(this.grpLocalFile, 2);
            this.grpLocalFile.Controls.Add(this.cmdBrowseLocal);
            this.grpLocalFile.Controls.Add(this.txtLocalPath);
            this.grpLocalFile.Location = new System.Drawing.Point(3, 59);
            this.grpLocalFile.Name = "grpLocalFile";
            this.grpLocalFile.Size = new System.Drawing.Size(428, 50);
            this.grpLocalFile.TabIndex = 5;
            this.grpLocalFile.TabStop = false;
            this.grpLocalFile.Text = "Local file";
            // 
            // cmdBrowseLocal
            // 
            this.cmdBrowseLocal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowseLocal.Location = new System.Drawing.Point(392, 19);
            this.cmdBrowseLocal.Name = "cmdBrowseLocal";
            this.cmdBrowseLocal.Size = new System.Drawing.Size(30, 20);
            this.cmdBrowseLocal.TabIndex = 1;
            this.cmdBrowseLocal.Text = "...";
            this.cmdBrowseLocal.UseVisualStyleBackColor = true;
            this.cmdBrowseLocal.Click += new System.EventHandler(this.cmdBrowseLocal_Click);
            // 
            // txtLocalPath
            // 
            this.txtLocalPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocalPath.Location = new System.Drawing.Point(6, 19);
            this.txtLocalPath.Name = "txtLocalPath";
            this.txtLocalPath.Size = new System.Drawing.Size(380, 20);
            this.txtLocalPath.TabIndex = 0;
            // 
            // grpServer
            // 
            this.grpServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLayout.SetColumnSpan(this.grpServer, 2);
            this.grpServer.Controls.Add(this.txtHost);
            this.grpServer.Controls.Add(this.cmdConnect);
            this.grpServer.Controls.Add(this.cmdDisconnect);
            this.grpServer.Location = new System.Drawing.Point(3, 3);
            this.grpServer.Name = "grpServer";
            this.grpServer.Size = new System.Drawing.Size(428, 50);
            this.grpServer.TabIndex = 1;
            this.grpServer.TabStop = false;
            this.grpServer.Text = "Server";
            // 
            // txtHost
            // 
            this.txtHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHost.Location = new System.Drawing.Point(6, 20);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(254, 20);
            this.txtHost.TabIndex = 0;
            this.txtHost.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHost_KeyDown);
            // 
            // cmdConnect
            // 
            this.cmdConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConnect.Location = new System.Drawing.Point(266, 20);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(75, 20);
            this.cmdConnect.TabIndex = 1;
            this.cmdConnect.Text = "Connect";
            this.cmdConnect.UseVisualStyleBackColor = true;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // cmdDisconnect
            // 
            this.cmdDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDisconnect.Enabled = false;
            this.cmdDisconnect.Location = new System.Drawing.Point(347, 20);
            this.cmdDisconnect.Name = "cmdDisconnect";
            this.cmdDisconnect.Size = new System.Drawing.Size(75, 20);
            this.cmdDisconnect.TabIndex = 2;
            this.cmdDisconnect.Text = "Disconnect";
            this.cmdDisconnect.UseVisualStyleBackColor = true;
            this.cmdDisconnect.Click += new System.EventHandler(this.cmdDisconnect_Click);
            // 
            // grpDrives
            // 
            this.grpDrives.Controls.Add(this.cmbFloppyType);
            this.grpDrives.Controls.Add(this.lblFloppyType);
            this.grpDrives.Controls.Add(this.lstServerDisks);
            this.grpDrives.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDrives.Location = new System.Drawing.Point(3, 115);
            this.grpDrives.Name = "grpDrives";
            this.grpDrives.Size = new System.Drawing.Size(211, 124);
            this.grpDrives.TabIndex = 6;
            this.grpDrives.TabStop = false;
            this.grpDrives.Text = "Disk select";
            // 
            // cmbFloppyType
            // 
            this.cmbFloppyType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFloppyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFloppyType.FormattingEnabled = true;
            this.cmbFloppyType.Location = new System.Drawing.Point(3, 94);
            this.cmbFloppyType.Name = "cmbFloppyType";
            this.cmbFloppyType.Size = new System.Drawing.Size(202, 21);
            this.cmbFloppyType.TabIndex = 2;
            // 
            // lblFloppyType
            // 
            this.lblFloppyType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFloppyType.AutoSize = true;
            this.lblFloppyType.Location = new System.Drawing.Point(9, 77);
            this.lblFloppyType.Name = "lblFloppyType";
            this.lblFloppyType.Size = new System.Drawing.Size(61, 13);
            this.lblFloppyType.TabIndex = 1;
            this.lblFloppyType.Text = "Floppy type";
            // 
            // lstServerDisks
            // 
            this.lstServerDisks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstServerDisks.FormattingEnabled = true;
            this.lstServerDisks.Location = new System.Drawing.Point(3, 16);
            this.lstServerDisks.Name = "lstServerDisks";
            this.lstServerDisks.ScrollAlwaysVisible = true;
            this.lstServerDisks.Size = new System.Drawing.Size(205, 56);
            this.lstServerDisks.TabIndex = 0;
            // 
            // grpActions
            // 
            this.grpActions.Controls.Add(this.cmdCancel);
            this.grpActions.Controls.Add(this.cmdWriteFileToDisk);
            this.grpActions.Controls.Add(this.cmdReadDiskToFile);
            this.grpActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpActions.Location = new System.Drawing.Point(220, 115);
            this.grpActions.Name = "grpActions";
            this.grpActions.Size = new System.Drawing.Size(211, 124);
            this.grpActions.TabIndex = 7;
            this.grpActions.TabStop = false;
            this.grpActions.Text = "Actions";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Enabled = false;
            this.cmdCancel.Location = new System.Drawing.Point(7, 80);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(195, 23);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdWriteFileToDisk
            // 
            this.cmdWriteFileToDisk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdWriteFileToDisk.Location = new System.Drawing.Point(7, 50);
            this.cmdWriteFileToDisk.Name = "cmdWriteFileToDisk";
            this.cmdWriteFileToDisk.Size = new System.Drawing.Size(195, 23);
            this.cmdWriteFileToDisk.TabIndex = 1;
            this.cmdWriteFileToDisk.Text = "Write local file to remote disk";
            this.cmdWriteFileToDisk.UseVisualStyleBackColor = true;
            this.cmdWriteFileToDisk.Click += new System.EventHandler(this.cmdWriteFileToDisk_Click);
            // 
            // cmdReadDiskToFile
            // 
            this.cmdReadDiskToFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdReadDiskToFile.Location = new System.Drawing.Point(6, 20);
            this.cmdReadDiskToFile.Name = "cmdReadDiskToFile";
            this.cmdReadDiskToFile.Size = new System.Drawing.Size(196, 23);
            this.cmdReadDiskToFile.TabIndex = 0;
            this.cmdReadDiskToFile.Text = "Read remote disk to local file";
            this.cmdReadDiskToFile.UseVisualStyleBackColor = true;
            this.cmdReadDiskToFile.Click += new System.EventHandler(this.cmdReadDiskToFile_Click);
            // 
            // menuStatus
            // 
            this.panelLayout.SetColumnSpan(this.menuStatus, 2);
            this.menuStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statLabel,
            this.statProg});
            this.menuStatus.Location = new System.Drawing.Point(0, 242);
            this.menuStatus.Name = "menuStatus";
            this.menuStatus.Size = new System.Drawing.Size(434, 20);
            this.menuStatus.TabIndex = 8;
            this.menuStatus.Text = "statusStrip1";
            // 
            // statLabel
            // 
            this.statLabel.AutoSize = false;
            this.statLabel.Name = "statLabel";
            this.statLabel.Size = new System.Drawing.Size(180, 15);
            this.statLabel.Text = "Ready to connect";
            this.statLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statProg
            // 
            this.statProg.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.statProg.Name = "statProg";
            this.statProg.Size = new System.Drawing.Size(150, 14);
            this.statProg.Visible = false;
            // 
            // workerReadDisk
            // 
            this.workerReadDisk.WorkerReportsProgress = true;
            this.workerReadDisk.WorkerSupportsCancellation = true;
            this.workerReadDisk.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerReadDisk_DoWork);
            this.workerReadDisk.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.workerReadDisk_ProgressChanged);
            this.workerReadDisk.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerReadDisk_RunWorkerCompleted);
            // 
            // workerWriteDisk
            // 
            this.workerWriteDisk.WorkerReportsProgress = true;
            this.workerWriteDisk.WorkerSupportsCancellation = true;
            this.workerWriteDisk.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerWriteDisk_DoWork);
            this.workerWriteDisk.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.workerWriteDisk_ProgressChanged);
            this.workerWriteDisk.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerWriteDisk_RunWorkerCompleted);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(434, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutDisksrvClientToolStripMenuItem,
            this.onlineDocumentationToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutDisksrvClientToolStripMenuItem
            // 
            this.aboutDisksrvClientToolStripMenuItem.Name = "aboutDisksrvClientToolStripMenuItem";
            this.aboutDisksrvClientToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.aboutDisksrvClientToolStripMenuItem.Text = "About disksrv client";
            this.aboutDisksrvClientToolStripMenuItem.Click += new System.EventHandler(this.aboutDisksrvClientToolStripMenuItem_Click);
            // 
            // onlineDocumentationToolStripMenuItem
            // 
            this.onlineDocumentationToolStripMenuItem.Name = "onlineDocumentationToolStripMenuItem";
            this.onlineDocumentationToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.onlineDocumentationToolStripMenuItem.Text = "Online documentation";
            this.onlineDocumentationToolStripMenuItem.Click += new System.EventHandler(this.onlineDocumentationToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 286);
            this.Controls.Add(this.panelLayout);
            this.Controls.Add(this.menuStrip1);
            this.MinimumSize = new System.Drawing.Size(360, 300);
            this.Name = "frmMain";
            this.Text = "disksrv client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.panelLayout.ResumeLayout(false);
            this.panelLayout.PerformLayout();
            this.grpLocalFile.ResumeLayout(false);
            this.grpLocalFile.PerformLayout();
            this.grpServer.ResumeLayout(false);
            this.grpServer.PerformLayout();
            this.grpDrives.ResumeLayout(false);
            this.grpDrives.PerformLayout();
            this.grpActions.ResumeLayout(false);
            this.menuStatus.ResumeLayout(false);
            this.menuStatus.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel panelLayout;
        private System.Windows.Forms.GroupBox grpLocalFile;
        private System.Windows.Forms.Button cmdBrowseLocal;
        private System.Windows.Forms.TextBox txtLocalPath;
        private System.Windows.Forms.GroupBox grpServer;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Button cmdConnect;
        private System.Windows.Forms.Button cmdDisconnect;
        private System.Windows.Forms.GroupBox grpDrives;
        private System.Windows.Forms.ListBox lstServerDisks;
        private System.Windows.Forms.GroupBox grpActions;
        private System.Windows.Forms.Button cmdWriteFileToDisk;
        private System.Windows.Forms.Button cmdReadDiskToFile;
        private System.Windows.Forms.StatusStrip menuStatus;
        private System.Windows.Forms.ToolStripStatusLabel statLabel;
        private System.Windows.Forms.ToolStripProgressBar statProg;
        private System.Windows.Forms.Button cmdCancel;
        private System.ComponentModel.BackgroundWorker workerReadDisk;
        private System.ComponentModel.BackgroundWorker workerWriteDisk;
        private System.Windows.Forms.ComboBox cmbFloppyType;
        private System.Windows.Forms.Label lblFloppyType;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutDisksrvClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlineDocumentationToolStripMenuItem;
    }
}

