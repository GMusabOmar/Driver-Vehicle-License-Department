using System;
using System.Data;
using BusinessAccess;
using System.Windows.Forms;
using workSpace.People;
using workSpace.Licenses.Local_Licenses;
using workSpace.Licenses;

namespace workSpace.Applications.International_License
{
    public partial class frmListInternationalLicesnseApplications : Form
    {
        private DataTable _dt;
        private clsInternationalLicense _InternationlInfo;
        private int _InternationalID = -1;
        public frmListInternationalLicesnseApplications()
        {
            InitializeComponent();
        }
        private void frmListInternationalLicesnseApplications_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _dt = clsInternationalLicense.GetAllInternationalLicense();
            dgvInternational.DataSource = _dt;
            lblRecords.Text = dgvInternational.RowCount.ToString();
            if(dgvInternational.RowCount > 0)
            {
                dgvInternational.Columns[0].HeaderText = "International License ID";
                dgvInternational.Columns[0].Width = 120;

                dgvInternational.Columns[1].HeaderText = "Application ID";
                dgvInternational.Columns[1].Width = 110;

                dgvInternational.Columns[2].HeaderText = "Driver ID";
                dgvInternational.Columns[2].Width = 100;

                dgvInternational.Columns[3].HeaderText = "Local License ID";
                dgvInternational.Columns[3].Width = 120;

                dgvInternational.Columns[4].HeaderText = "Issue Date";
                dgvInternational.Columns[4].Width = 110;

                dgvInternational.Columns[5].HeaderText = "Expiration Date";
                dgvInternational.Columns[5].Width = 110;

                dgvInternational.Columns[6].HeaderText = "Is Active";
                dgvInternational.Columns[6].Width = 114;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = cbFilterBy.Text != "None";
            if(txtFilter.Visible)
            {
                cbIsActive.Visible = false;
                txtFilter.Clear();
                txtFilter.Focus();
            }
            cbIsActive.Visible = cbFilterBy.Text == "Is Active";
            if(cbIsActive.Visible)
            {
                txtFilter.Visible = false;
                cbIsActive.SelectedIndex = 0;
                cbIsActive.Focus();
            }
        }
        private void btnAddNewInterationalLicense_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm 
                = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
            frmListInternationalLicesnseApplications_Load(null, null);
        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string ColName = "";
            switch(cbFilterBy.Text)
            {
                case "International License ID":
                    ColName = "InternationalLicenseID";
                    break;
                case "Application ID":
                    ColName = "ApplicationID";
                    break;
                case "Driver ID":
                    ColName = "DriverID";
                    break;
                case "Local License ID":
                    ColName = "IssuedUsingLocalLicenseID";
                    break;
                case "Is Active":
                    ColName = "IsActive";
                    break;
                default:
                    ColName = "None";
                    break;
            }
            if(txtFilter.Text == "" || ColName == "None")
            {
                _dt.DefaultView.RowFilter = "";
                lblRecords.Text = dgvInternational.RowCount.ToString();
                return;
            }
            _dt.DefaultView.RowFilter = string.Format("{0} = {1}", ColName, txtFilter.Text.Trim());
            lblRecords.Text = dgvInternational.RowCount.ToString();
        }
        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ColName = cbIsActive.Text == "Yes" ? 1 : 0;
            if (cbIsActive.Text == "All")
            {
                _dt.DefaultView.RowFilter = "";
                lblRecords.Text = dgvInternational.RowCount.ToString();
                return;
            }
            if(cbIsActive.Text == "Yes")
                _dt.DefaultView.RowFilter = string.Format("{0} = {1}",ColName, "IsActive");
            else
                _dt.DefaultView.RowFilter = string.Format("{0} = {1}", ColName, "IsActive");
            lblRecords.Text = dgvInternational.RowCount.ToString();
        }
        private void ShowPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternational.CurrentRow.Cells[2].Value;
            frmShowPersonInfo frm = new frmShowPersonInfo(clsDriver.GetDriverInfoByID(DriverID).PersonID);
            frm.ShowDialog();
        }
        private void ShowLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo((int)dgvInternational.CurrentRow.Cells[3].Value);
            frm.ShowDialog();
        }
        private void ShowPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternational.CurrentRow.Cells[2].Value;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(clsDriver.GetDriverInfoByID(DriverID).PersonID);
            frm.ShowDialog();
        }
    }
}
