using System;
using BusinessAccess;
using System.Windows.Forms;
using System.Data;
using workSpace.People;
using workSpace.Licenses;

namespace workSpace.Drivers
{
    public partial class frmListDrivers : Form
    {
        private DataTable _dt;
        public frmListDrivers()
        {
            InitializeComponent();
        }
        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _dt = clsDriver.GetAllDriver();
            dgvDrivers.DataSource = _dt;
            lblRecords.Text = dgvDrivers.RowCount.ToString();
            if(dgvDrivers.RowCount > 0)
            {
                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[0].Width = 100;

                dgvDrivers.Columns[1].HeaderText = "Person ID";
                dgvDrivers.Columns[1].Width = 110;

                dgvDrivers.Columns[2].HeaderText = "NationalNo";
                dgvDrivers.Columns[2].Width = 100;

                dgvDrivers.Columns[3].HeaderText = "Full Name";
                dgvDrivers.Columns[3].Width = 260;

                dgvDrivers.Columns[4].HeaderText = "Create Date";
                dgvDrivers.Columns[4].Width = 150;

                dgvDrivers.Columns[5].HeaderText = "No.License";
                dgvDrivers.Columns[5].Width = 110;

            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Visible = cbFilterBy.Text != "None";
            if(txtFilterBy.Visible)
            {
                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }
        }
        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string ColName = "";
            switch (cbFilterBy.Text)
            {
                case "Driver ID":
                    ColName = "DriverID";
                    break;
                case "Person ID":
                    ColName = "PersonID";
                    break;
                case "NationalNo":
                    ColName = "NationalNo";
                    break;
                case "Full Name":
                    ColName = "FullName";
                    break;
                case "No.License":
                    ColName = "NumberOfActiveLicenses";
                    break;
                default:
                    ColName = "None";
                    break;
            }
            if (txtFilterBy.Text == "" || ColName == "None")
            {
                _dt.DefaultView.RowFilter = "";
                lblRecords.Text = dgvDrivers.RowCount.ToString();
                return;
            }
            if (ColName != "DriverID" && ColName != "PersonID" && ColName != "NumberOfActiveLicenses")
                _dt.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", ColName, txtFilterBy.Text.Trim());
            else
                _dt.DefaultView.RowFilter = string.Format("{0} = {1}", ColName, txtFilterBy.Text.Trim());
            lblRecords.Text = dgvDrivers.RowCount.ToString();
        }
        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "Driver ID" || cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "No.License")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void ShowPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo((int)dgvDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }
        private void dgvDrivers_DoubleClick(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo((int)dgvDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void ShowPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory((int)dgvDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }
    }
}
