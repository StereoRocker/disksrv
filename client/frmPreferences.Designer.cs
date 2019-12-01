namespace disksrv_client
{
    partial class frmPreferences
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
            this.panSaveCancel = new System.Windows.Forms.Panel();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grpDiskBuffers = new System.Windows.Forms.GroupBox();
            this.lblHddSecMax = new System.Windows.Forms.Label();
            this.valHDDSecMax = new System.Windows.Forms.NumericUpDown();
            this.lblFlopSecMax = new System.Windows.Forms.Label();
            this.valFloppySecMax = new System.Windows.Forms.NumericUpDown();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.propGeometry = new System.Windows.Forms.PropertyGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lstGeometries = new System.Windows.Forms.ListView();
            this.cmdRemoveGeometry = new System.Windows.Forms.Button();
            this.cmdAddGeometry = new System.Windows.Forms.Button();
            this.bs1 = new System.Windows.Forms.BindingSource(this.components);
            this.panSaveCancel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.grpDiskBuffers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.valHDDSecMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valFloppySecMax)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bs1)).BeginInit();
            this.SuspendLayout();
            // 
            // panSaveCancel
            // 
            this.panSaveCancel.Controls.Add(this.cmdCancel);
            this.panSaveCancel.Controls.Add(this.cmdSave);
            this.panSaveCancel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panSaveCancel.Location = new System.Drawing.Point(0, 225);
            this.panSaveCancel.Name = "panSaveCancel";
            this.panSaveCancel.Size = new System.Drawing.Size(356, 25);
            this.panSaveCancel.TabIndex = 1;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(81, 0);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 25);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(0, 0);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 25);
            this.cmdSave.TabIndex = 0;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(356, 225);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grpDiskBuffers);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(348, 199);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Global";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grpDiskBuffers
            // 
            this.grpDiskBuffers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDiskBuffers.Controls.Add(this.lblHddSecMax);
            this.grpDiskBuffers.Controls.Add(this.valHDDSecMax);
            this.grpDiskBuffers.Controls.Add(this.lblFlopSecMax);
            this.grpDiskBuffers.Controls.Add(this.valFloppySecMax);
            this.grpDiskBuffers.Location = new System.Drawing.Point(9, 7);
            this.grpDiskBuffers.Name = "grpDiskBuffers";
            this.grpDiskBuffers.Size = new System.Drawing.Size(331, 130);
            this.grpDiskBuffers.TabIndex = 0;
            this.grpDiskBuffers.TabStop = false;
            this.grpDiskBuffers.Text = "Disk buffer settings";
            // 
            // lblHddSecMax
            // 
            this.lblHddSecMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHddSecMax.Location = new System.Drawing.Point(6, 71);
            this.lblHddSecMax.Name = "lblHddSecMax";
            this.lblHddSecMax.Size = new System.Drawing.Size(319, 30);
            this.lblHddSecMax.TabIndex = 3;
            this.lblHddSecMax.Text = "Override the maximum number of sectors to read/write while operating on a hard di" +
    "sk. (default = 0, set to 0 for no override)";
            // 
            // valHDDSecMax
            // 
            this.valHDDSecMax.Location = new System.Drawing.Point(6, 104);
            this.valHDDSecMax.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.valHDDSecMax.Name = "valHDDSecMax";
            this.valHDDSecMax.Size = new System.Drawing.Size(55, 20);
            this.valHDDSecMax.TabIndex = 2;
            this.valHDDSecMax.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // lblFlopSecMax
            // 
            this.lblFlopSecMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFlopSecMax.Location = new System.Drawing.Point(6, 16);
            this.lblFlopSecMax.Name = "lblFlopSecMax";
            this.lblFlopSecMax.Size = new System.Drawing.Size(319, 29);
            this.lblFlopSecMax.TabIndex = 1;
            this.lblFlopSecMax.Text = "Override the maximum number of sectors to read/write while operating on a floppy " +
    "disk. (default = 4, set to 0 for no override)";
            // 
            // valFloppySecMax
            // 
            this.valFloppySecMax.Location = new System.Drawing.Point(6, 48);
            this.valFloppySecMax.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.valFloppySecMax.Name = "valFloppySecMax";
            this.valFloppySecMax.Size = new System.Drawing.Size(55, 20);
            this.valFloppySecMax.TabIndex = 0;
            this.valFloppySecMax.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.propGeometry);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(348, 199);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Floppy disk geometries";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // propGeometry
            // 
            this.propGeometry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propGeometry.Location = new System.Drawing.Point(152, 3);
            this.propGeometry.Name = "propGeometry";
            this.propGeometry.Size = new System.Drawing.Size(193, 193);
            this.propGeometry.TabIndex = 1;
            this.propGeometry.ToolbarVisible = false;
            this.propGeometry.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.ValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lstGeometries);
            this.panel1.Controls.Add(this.cmdRemoveGeometry);
            this.panel1.Controls.Add(this.cmdAddGeometry);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(149, 193);
            this.panel1.TabIndex = 0;
            // 
            // lstGeometries
            // 
            this.lstGeometries.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstGeometries.HideSelection = false;
            this.lstGeometries.Location = new System.Drawing.Point(1, 0);
            this.lstGeometries.MultiSelect = false;
            this.lstGeometries.Name = "lstGeometries";
            this.lstGeometries.Size = new System.Drawing.Size(148, 159);
            this.lstGeometries.TabIndex = 2;
            this.lstGeometries.UseCompatibleStateImageBehavior = false;
            this.lstGeometries.View = System.Windows.Forms.View.List;
            this.lstGeometries.SelectedIndexChanged += new System.EventHandler(this.lstGeometries_SelectedIndexChanged);
            // 
            // cmdRemoveGeometry
            // 
            this.cmdRemoveGeometry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdRemoveGeometry.Location = new System.Drawing.Point(36, 165);
            this.cmdRemoveGeometry.Name = "cmdRemoveGeometry";
            this.cmdRemoveGeometry.Size = new System.Drawing.Size(25, 25);
            this.cmdRemoveGeometry.TabIndex = 1;
            this.cmdRemoveGeometry.Text = "-";
            this.cmdRemoveGeometry.UseVisualStyleBackColor = true;
            this.cmdRemoveGeometry.Click += new System.EventHandler(this.cmdRemoveGeometry_Click);
            // 
            // cmdAddGeometry
            // 
            this.cmdAddGeometry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdAddGeometry.Location = new System.Drawing.Point(5, 165);
            this.cmdAddGeometry.Name = "cmdAddGeometry";
            this.cmdAddGeometry.Size = new System.Drawing.Size(25, 25);
            this.cmdAddGeometry.TabIndex = 0;
            this.cmdAddGeometry.Text = "+";
            this.cmdAddGeometry.UseVisualStyleBackColor = true;
            this.cmdAddGeometry.Click += new System.EventHandler(this.cmdAddGeometry_Click);
            // 
            // frmPreferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(356, 250);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panSaveCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPreferences";
            this.Text = "Preferences";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPreferences_FormClosing);
            this.Load += new System.EventHandler(this.frmPreferences_Load);
            this.panSaveCancel.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.grpDiskBuffers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.valHDDSecMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valFloppySecMax)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bs1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panSaveCancel;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox grpDiskBuffers;
        private System.Windows.Forms.Label lblHddSecMax;
        private System.Windows.Forms.NumericUpDown valHDDSecMax;
        private System.Windows.Forms.Label lblFlopSecMax;
        private System.Windows.Forms.NumericUpDown valFloppySecMax;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PropertyGrid propGeometry;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdRemoveGeometry;
        private System.Windows.Forms.Button cmdAddGeometry;
        private System.Windows.Forms.BindingSource bs1;
        private System.Windows.Forms.ListView lstGeometries;
    }
}