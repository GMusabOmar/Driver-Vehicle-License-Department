namespace workSpace.Applications.Local_Driving_License
{
    partial class frmListLocalDrivingLicesnseApplications
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
            this.dgvLocalApplication = new System.Windows.Forms.DataGridView();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilterBy = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRecords = new System.Windows.Forms.Label();
            this.btnAddNewApplication = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmsApplications = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowApplicationDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.EditApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CancelApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ScheduleTestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScheduleVisonTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScheduleWrittenTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScheduleStreetTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.IssueDrivingLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ShowLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.ShowPersonLicenseHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalApplication)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.cmsApplications.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvLocalApplication
            // 
            this.dgvLocalApplication.AllowUserToAddRows = false;
            this.dgvLocalApplication.AllowUserToDeleteRows = false;
            this.dgvLocalApplication.BackgroundColor = System.Drawing.Color.White;
            this.dgvLocalApplication.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalApplication.Location = new System.Drawing.Point(12, 231);
            this.dgvLocalApplication.Name = "dgvLocalApplication";
            this.dgvLocalApplication.ReadOnly = true;
            this.dgvLocalApplication.Size = new System.Drawing.Size(871, 228);
            this.dgvLocalApplication.TabIndex = 0;
            this.dgvLocalApplication.DoubleClick += new System.EventHandler(this.dgvLocalApplication_DoubleClick);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "L.ID",
            "ClassName",
            "N.No",
            "Name",
            "Pass Test",
            "Status"});
            this.cbFilterBy.Location = new System.Drawing.Point(97, 184);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(158, 28);
            this.cbFilterBy.TabIndex = 1;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Find By :";
            // 
            // txtFilterBy
            // 
            this.txtFilterBy.Location = new System.Drawing.Point(273, 185);
            this.txtFilterBy.Name = "txtFilterBy";
            this.txtFilterBy.Size = new System.Drawing.Size(221, 26);
            this.txtFilterBy.TabIndex = 3;
            this.txtFilterBy.TextChanged += new System.EventHandler(this.txtFilterBy_TextChanged);
            this.txtFilterBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterBy_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 475);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "#Records :";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Location = new System.Drawing.Point(115, 475);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(36, 20);
            this.lblRecords.TabIndex = 5;
            this.lblRecords.Text = "???";
            // 
            // btnAddNewApplication
            // 
            this.btnAddNewApplication.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAddNewApplication.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewApplication.Image = global::workSpace.Properties.Resources.New_Application_64;
            this.btnAddNewApplication.Location = new System.Drawing.Point(813, 147);
            this.btnAddNewApplication.Name = "btnAddNewApplication";
            this.btnAddNewApplication.Size = new System.Drawing.Size(70, 65);
            this.btnAddNewApplication.TabIndex = 7;
            this.btnAddNewApplication.UseVisualStyleBackColor = true;
            this.btnAddNewApplication.Click += new System.EventHandler(this.btnAddNewApplication_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::workSpace.Properties.Resources.Applications;
            this.pictureBox1.Location = new System.Drawing.Point(405, -3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(109, 99);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(222, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(475, 33);
            this.label4.TabIndex = 9;
            this.label4.Text = "Local Driving Licesnse Applications";
            // 
            // cmsApplications
            // 
            this.cmsApplications.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowApplicationDetailsToolStripMenuItem,
            this.toolStripSeparator1,
            this.EditApplicationToolStripMenuItem,
            this.DeleteApplicationToolStripMenuItem,
            this.toolStripSeparator2,
            this.CancelApplicationToolStripMenuItem,
            this.toolStripSeparator3,
            this.ScheduleTestsToolStripMenuItem,
            this.toolStripSeparator4,
            this.IssueDrivingLicenseToolStripMenuItem,
            this.toolStripSeparator5,
            this.ShowLicenseToolStripMenuItem,
            this.toolStripSeparator6,
            this.ShowPersonLicenseHistoryToolStripMenuItem});
            this.cmsApplications.Name = "cmsApplications";
            this.cmsApplications.Size = new System.Drawing.Size(262, 366);
            this.cmsApplications.Opening += new System.ComponentModel.CancelEventHandler(this.cmsApplications_Opening);
            // 
            // ShowApplicationDetailsToolStripMenuItem
            // 
            this.ShowApplicationDetailsToolStripMenuItem.Image = global::workSpace.Properties.Resources.PersonDetails_32;
            this.ShowApplicationDetailsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ShowApplicationDetailsToolStripMenuItem.Name = "ShowApplicationDetailsToolStripMenuItem";
            this.ShowApplicationDetailsToolStripMenuItem.Size = new System.Drawing.Size(261, 38);
            this.ShowApplicationDetailsToolStripMenuItem.Text = "Show Application Details";
            this.ShowApplicationDetailsToolStripMenuItem.Click += new System.EventHandler(this.ShowApplicationDetailsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(258, 6);
            // 
            // EditApplicationToolStripMenuItem
            // 
            this.EditApplicationToolStripMenuItem.Image = global::workSpace.Properties.Resources.edit_32;
            this.EditApplicationToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.EditApplicationToolStripMenuItem.Name = "EditApplicationToolStripMenuItem";
            this.EditApplicationToolStripMenuItem.Size = new System.Drawing.Size(261, 38);
            this.EditApplicationToolStripMenuItem.Text = "Edit Application";
            this.EditApplicationToolStripMenuItem.Click += new System.EventHandler(this.EditApplicationToolStripMenuItem_Click);
            // 
            // DeleteApplicationToolStripMenuItem
            // 
            this.DeleteApplicationToolStripMenuItem.Image = global::workSpace.Properties.Resources.Delete_32_2;
            this.DeleteApplicationToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.DeleteApplicationToolStripMenuItem.Name = "DeleteApplicationToolStripMenuItem";
            this.DeleteApplicationToolStripMenuItem.Size = new System.Drawing.Size(261, 38);
            this.DeleteApplicationToolStripMenuItem.Text = "Delete Application";
            this.DeleteApplicationToolStripMenuItem.Click += new System.EventHandler(this.DeleteApplicationToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(258, 6);
            // 
            // CancelApplicationToolStripMenuItem
            // 
            this.CancelApplicationToolStripMenuItem.Image = global::workSpace.Properties.Resources.Delete_32;
            this.CancelApplicationToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.CancelApplicationToolStripMenuItem.Name = "CancelApplicationToolStripMenuItem";
            this.CancelApplicationToolStripMenuItem.Size = new System.Drawing.Size(261, 38);
            this.CancelApplicationToolStripMenuItem.Text = "Cancel Application";
            this.CancelApplicationToolStripMenuItem.Click += new System.EventHandler(this.CancelApplicationToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(258, 6);
            // 
            // ScheduleTestsToolStripMenuItem
            // 
            this.ScheduleTestsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ScheduleVisonTestToolStripMenuItem,
            this.ScheduleWrittenTestToolStripMenuItem,
            this.ScheduleStreetTestToolStripMenuItem});
            this.ScheduleTestsToolStripMenuItem.Image = global::workSpace.Properties.Resources.Schedule_Test_32;
            this.ScheduleTestsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ScheduleTestsToolStripMenuItem.Name = "ScheduleTestsToolStripMenuItem";
            this.ScheduleTestsToolStripMenuItem.Size = new System.Drawing.Size(261, 38);
            this.ScheduleTestsToolStripMenuItem.Text = "Schedule Tests";
            // 
            // ScheduleVisonTestToolStripMenuItem
            // 
            this.ScheduleVisonTestToolStripMenuItem.Image = global::workSpace.Properties.Resources.Vision_Test_32;
            this.ScheduleVisonTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ScheduleVisonTestToolStripMenuItem.Name = "ScheduleVisonTestToolStripMenuItem";
            this.ScheduleVisonTestToolStripMenuItem.Size = new System.Drawing.Size(203, 38);
            this.ScheduleVisonTestToolStripMenuItem.Text = "Schedule Vision Test";
            this.ScheduleVisonTestToolStripMenuItem.Click += new System.EventHandler(this.ScheduleVisonTestToolStripMenuItem_Click);
            // 
            // ScheduleWrittenTestToolStripMenuItem
            // 
            this.ScheduleWrittenTestToolStripMenuItem.Image = global::workSpace.Properties.Resources.Written_Test_32;
            this.ScheduleWrittenTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ScheduleWrittenTestToolStripMenuItem.Name = "ScheduleWrittenTestToolStripMenuItem";
            this.ScheduleWrittenTestToolStripMenuItem.Size = new System.Drawing.Size(203, 38);
            this.ScheduleWrittenTestToolStripMenuItem.Text = "Schedule Written Test";
            this.ScheduleWrittenTestToolStripMenuItem.Click += new System.EventHandler(this.ScheduleWrittenTestToolStripMenuItem_Click);
            // 
            // ScheduleStreetTestToolStripMenuItem
            // 
            this.ScheduleStreetTestToolStripMenuItem.Image = global::workSpace.Properties.Resources.Street_Test_32;
            this.ScheduleStreetTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ScheduleStreetTestToolStripMenuItem.Name = "ScheduleStreetTestToolStripMenuItem";
            this.ScheduleStreetTestToolStripMenuItem.Size = new System.Drawing.Size(203, 38);
            this.ScheduleStreetTestToolStripMenuItem.Text = "Schedule Street Test";
            this.ScheduleStreetTestToolStripMenuItem.Click += new System.EventHandler(this.ScheduleStreetTestToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(258, 6);
            // 
            // IssueDrivingLicenseToolStripMenuItem
            // 
            this.IssueDrivingLicenseToolStripMenuItem.Image = global::workSpace.Properties.Resources.IssueDrivingLicense_32;
            this.IssueDrivingLicenseToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.IssueDrivingLicenseToolStripMenuItem.Name = "IssueDrivingLicenseToolStripMenuItem";
            this.IssueDrivingLicenseToolStripMenuItem.Size = new System.Drawing.Size(261, 38);
            this.IssueDrivingLicenseToolStripMenuItem.Text = "Issue Driving License (First Time)";
            this.IssueDrivingLicenseToolStripMenuItem.Click += new System.EventHandler(this.IssueDrivingLicenseToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(258, 6);
            // 
            // ShowLicenseToolStripMenuItem
            // 
            this.ShowLicenseToolStripMenuItem.Image = global::workSpace.Properties.Resources.License_View_32;
            this.ShowLicenseToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ShowLicenseToolStripMenuItem.Name = "ShowLicenseToolStripMenuItem";
            this.ShowLicenseToolStripMenuItem.Size = new System.Drawing.Size(261, 38);
            this.ShowLicenseToolStripMenuItem.Text = "Show License";
            this.ShowLicenseToolStripMenuItem.Click += new System.EventHandler(this.ShowLicenseToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(258, 6);
            // 
            // ShowPersonLicenseHistoryToolStripMenuItem
            // 
            this.ShowPersonLicenseHistoryToolStripMenuItem.Image = global::workSpace.Properties.Resources.PersonLicenseHistory_32;
            this.ShowPersonLicenseHistoryToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ShowPersonLicenseHistoryToolStripMenuItem.Name = "ShowPersonLicenseHistoryToolStripMenuItem";
            this.ShowPersonLicenseHistoryToolStripMenuItem.Size = new System.Drawing.Size(261, 38);
            this.ShowPersonLicenseHistoryToolStripMenuItem.Text = "Show Person License History";
            this.ShowPersonLicenseHistoryToolStripMenuItem.Click += new System.EventHandler(this.ShowPersonLicenseHistoryToolStripMenuItem_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::workSpace.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(762, 467);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(121, 36);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // frmListLocalDrivingLicesnseApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(900, 518);
            this.ContextMenuStrip = this.cmsApplications;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnAddNewApplication);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFilterBy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.dgvLocalApplication);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmListLocalDrivingLicesnseApplications";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmListLocalDrivingLicesnseApplications";
            this.Load += new System.EventHandler(this.frmListLocalDrivingLicesnseApplications_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalApplication)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.cmsApplications.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLocalApplication;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFilterBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.Button btnAddNewApplication;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip cmsApplications;
        private System.Windows.Forms.ToolStripMenuItem ShowApplicationDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem EditApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem CancelApplicationToolStripMenuItem;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ScheduleTestsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ScheduleVisonTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ScheduleWrittenTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ScheduleStreetTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem IssueDrivingLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem ShowLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem ShowPersonLicenseHistoryToolStripMenuItem;
    }
}