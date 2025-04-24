namespace workSpace.Applications.ApplicationType
{
    partial class frmListApplicationType
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvApplication = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRowNumber = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.cmsApplication = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.UpdateApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvApplication)).BeginInit();
            this.cmsApplication.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::workSpace.Properties.Resources.Application_Types_512;
            this.pictureBox1.Location = new System.Drawing.Point(271, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(118, 110);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(195, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(270, 33);
            this.label1.TabIndex = 1;
            this.label1.Text = "Manage Application";
            // 
            // dgvApplication
            // 
            this.dgvApplication.AllowUserToAddRows = false;
            this.dgvApplication.AllowUserToDeleteRows = false;
            this.dgvApplication.BackgroundColor = System.Drawing.Color.White;
            this.dgvApplication.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvApplication.Location = new System.Drawing.Point(12, 178);
            this.dgvApplication.Name = "dgvApplication";
            this.dgvApplication.ReadOnly = true;
            this.dgvApplication.Size = new System.Drawing.Size(637, 186);
            this.dgvApplication.TabIndex = 2;
            this.dgvApplication.DoubleClick += new System.EventHandler(this.dgvApplication_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 391);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "#RowNumber :";
            // 
            // lblRowNumber
            // 
            this.lblRowNumber.AutoSize = true;
            this.lblRowNumber.Location = new System.Drawing.Point(145, 391);
            this.lblRowNumber.Name = "lblRowNumber";
            this.lblRowNumber.Size = new System.Drawing.Size(36, 20);
            this.lblRowNumber.TabIndex = 4;
            this.lblRowNumber.Text = "???";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::workSpace.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(529, 382);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 38);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmsApplication
            // 
            this.cmsApplication.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UpdateApplicationToolStripMenuItem});
            this.cmsApplication.Name = "cmsApplication";
            this.cmsApplication.Size = new System.Drawing.Size(193, 42);
            // 
            // UpdateApplicationToolStripMenuItem
            // 
            this.UpdateApplicationToolStripMenuItem.Image = global::workSpace.Properties.Resources.edit_32;
            this.UpdateApplicationToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.UpdateApplicationToolStripMenuItem.Name = "UpdateApplicationToolStripMenuItem";
            this.UpdateApplicationToolStripMenuItem.Size = new System.Drawing.Size(192, 38);
            this.UpdateApplicationToolStripMenuItem.Text = "Update Application";
            this.UpdateApplicationToolStripMenuItem.Click += new System.EventHandler(this.UpdateApplicationToolStripMenuItem_Click);
            // 
            // frmListApplicationType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(661, 444);
            this.ContextMenuStrip = this.cmsApplication;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRowNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvApplication);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmListApplicationType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Application Type";
            this.Load += new System.EventHandler(this.frmListApplicationType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvApplication)).EndInit();
            this.cmsApplication.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvApplication;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRowNumber;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ContextMenuStrip cmsApplication;
        private System.Windows.Forms.ToolStripMenuItem UpdateApplicationToolStripMenuItem;
    }
}