using System;
using BusinessAccess;
using System.Windows.Forms;
using System.Data;

namespace workSpace.Applications.ApplicationType
{
    public partial class frmListApplicationType: Form
    {
        DataTable _GetAllApplication;
        public frmListApplicationType()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListApplicationType_Load(object sender, EventArgs e)
        {
            _GetAllApplication = clsApplicationType.GetAllApplication();
            dgvApplication.DataSource = _GetAllApplication;
            lblRowNumber.Text = dgvApplication.RowCount.ToString();
            if(dgvApplication.Rows.Count > 0)
            {
                dgvApplication.Columns[0].HeaderText = "ID";
                dgvApplication.Columns[0].Width = 90;

                dgvApplication.Columns[1].HeaderText = "Title";
                dgvApplication.Columns[1].Width = 378;

                dgvApplication.Columns[2].HeaderText = "Fees";
                dgvApplication.Columns[2].Width = 125;
            }
        }

        private void UpdateApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateApplicationType frm = new frmUpdateApplicationType((int)dgvApplication.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListApplicationType_Load(null, null);
        }

        private void dgvApplication_DoubleClick(object sender, EventArgs e)
        {
            frmUpdateApplicationType frm = new frmUpdateApplicationType((int)dgvApplication.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListApplicationType_Load(null, null);
        }
    }
}
