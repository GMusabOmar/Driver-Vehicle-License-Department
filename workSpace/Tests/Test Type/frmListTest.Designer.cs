namespace workSpace.Tests.Test_Type
{
    partial class frmListTest
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
            this.dgvTest = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRowsNumber = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmsTest = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.UpdateTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.cmsTest.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTest
            // 
            this.dgvTest.AllowUserToAddRows = false;
            this.dgvTest.AllowUserToDeleteRows = false;
            this.dgvTest.BackgroundColor = System.Drawing.Color.White;
            this.dgvTest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTest.Location = new System.Drawing.Point(12, 151);
            this.dgvTest.Name = "dgvTest";
            this.dgvTest.ReadOnly = true;
            this.dgvTest.Size = new System.Drawing.Size(582, 136);
            this.dgvTest.TabIndex = 0;
            this.dgvTest.DoubleClick += new System.EventHandler(this.dgvTest_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 298);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "#RowsNumber :";
            // 
            // lblRowsNumber
            // 
            this.lblRowsNumber.AutoSize = true;
            this.lblRowsNumber.Location = new System.Drawing.Point(153, 298);
            this.lblRowsNumber.Name = "lblRowsNumber";
            this.lblRowsNumber.Size = new System.Drawing.Size(36, 20);
            this.lblRowsNumber.TabIndex = 2;
            this.lblRowsNumber.Text = "???";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::workSpace.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(470, 293);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(124, 31);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::workSpace.Properties.Resources.TestType_512;
            this.pictureBox1.Location = new System.Drawing.Point(232, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(142, 92);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(162, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(282, 37);
            this.label2.TabIndex = 5;
            this.label2.Text = "Manage Test Type";
            // 
            // cmsTest
            // 
            this.cmsTest.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UpdateTestToolStripMenuItem});
            this.cmsTest.Name = "cmsTest";
            this.cmsTest.Size = new System.Drawing.Size(152, 42);
            // 
            // UpdateTestToolStripMenuItem
            // 
            this.UpdateTestToolStripMenuItem.Image = global::workSpace.Properties.Resources.edit_32;
            this.UpdateTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.UpdateTestToolStripMenuItem.Name = "UpdateTestToolStripMenuItem";
            this.UpdateTestToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.UpdateTestToolStripMenuItem.Text = "Update Test";
            this.UpdateTestToolStripMenuItem.Click += new System.EventHandler(this.UpdateTestToolStripMenuItem_Click);
            // 
            // frmListTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(606, 338);
            this.ContextMenuStrip = this.cmsTest;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRowsNumber);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvTest);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmListTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "List Test";
            this.Load += new System.EventHandler(this.frmListTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.cmsTest.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRowsNumber;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip cmsTest;
        private System.Windows.Forms.ToolStripMenuItem UpdateTestToolStripMenuItem;
    }
}