using System;
using BusinessAccess;
using System.Windows.Forms;
using System.Data;
using workSpace.Licenses.Detain_License;
using workSpace.People;
using workSpace.Licenses.Local_Licenses;
using workSpace.Licenses;

namespace workSpace.Applications.Rlease_Detained_License
{
    public partial class frmListDetainedLicenses : Form
    {
        private DataTable _dt;
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }
        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _dt = clsDetainedLicense.GetAllDetainedLicense();
            dgvListDetainLicense.DataSource = _dt;
            cbFilterBy.SelectedIndex = 0;
            lblRecords.Text = dgvListDetainLicense.RowCount.ToString();
            if(dgvListDetainLicense.RowCount > 0)
            {
                dgvListDetainLicense.Columns[0].HeaderText = "D.ID";
                dgvListDetainLicense.Columns[0].Width = 60;

                dgvListDetainLicense.Columns[1].HeaderText = "L.ID";
                dgvListDetainLicense.Columns[1].Width = 60;

                dgvListDetainLicense.Columns[2].HeaderText = "D.Date";
                dgvListDetainLicense.Columns[2].Width = 110;

                dgvListDetainLicense.Columns[3].HeaderText = "Is Released";
                dgvListDetainLicense.Columns[3].Width = 90;

                dgvListDetainLicense.Columns[4].HeaderText = "Fine Fees";
                dgvListDetainLicense.Columns[4].Width = 100;

                dgvListDetainLicense.Columns[5].HeaderText = "Rela.Date";
                dgvListDetainLicense.Columns[5].Width = 100;

                dgvListDetainLicense.Columns[6].HeaderText = "National No";
                dgvListDetainLicense.Columns[6].Width = 90;

                dgvListDetainLicense.Columns[7].HeaderText = "F.Name";
                dgvListDetainLicense.Columns[7].Width = 220;

                dgvListDetainLicense.Columns[8].HeaderText = "R.ApplicationID";
                dgvListDetainLicense.Columns[8].Width = 130;
            }
        }
        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ReleaseDetainLicenseToolStripMenuItem.Enabled = !(bool)dgvListDetainLicense.CurrentRow.Cells[3].Value;
        }
        private void ReleaseDetainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm =
                new frmReleaseDetainedLicenseApplication((int)dgvListDetainLicense.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm =
                    new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }
        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
        }
        private void ShowPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLicense _License = clsLicense.Find((int)dgvListDetainLicense.CurrentRow.Cells[1].Value);
            frmShowPersonInfo frm = new frmShowPersonInfo(_License.DriverInfo.PersonID);
            frm.ShowDialog();
        }
        private void ShowLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo((int)dgvListDetainLicense.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }
        private void ShowPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLicense _License = clsLicense.Find((int)dgvListDetainLicense.CurrentRow.Cells[1].Value);
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_License.DriverInfo.PersonID);
            frm.ShowDialog();
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = cbFilterBy.Text != "None";
            if(txtFilter.Visible)
            {
                cbIsRelease.Visible = false;
                txtFilter.Text = "";
                txtFilter.Focus();
            }
            cbIsRelease.Visible = (cbFilterBy.Text == "Is Released");
            if (cbIsRelease.Visible)
            {
                cbIsRelease.SelectedIndex = 0;
                txtFilter.Visible = false;
            }
        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string ColName = "";
            switch(cbFilterBy.Text)
            {
                case "D.ID":
                    ColName = "DetainID";
                    break;
                case "L.ID":
                    ColName = "LicenseID";
                    break;
                case "Is Released":
                    ColName = "IsReleased";
                    break;
                case "Fine Fees":
                    ColName = "FineFees";
                    break;
                case "National No":
                    ColName = "NationalNo";
                    break;
                case "F.Name":
                    ColName = "FullName";
                    break;
                case "R.ApplicationID":
                    ColName = "ReleaseApplicationID";
                    break;
                default:
                    ColName = "None";
                    break;
            }
            if(txtFilter.Text.Trim() == "" || ColName == "None")
            {
                _dt.DefaultView.RowFilter = "";
                lblRecords.Text = dgvListDetainLicense.RowCount.ToString();
                return;
            }
            if (ColName == "DetainID" || ColName == "LicenseID" || ColName == "FineFees" || ColName == "ReleaseApplicationID")
                _dt.DefaultView.RowFilter = string.Format("{0} = {1}", ColName, txtFilter.Text.Trim());
            else
                _dt.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", ColName, txtFilter.Text.Trim());
            lblRecords.Text = dgvListDetainLicense.RowCount.ToString();
        }
        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "D.ID" || cbFilterBy.Text == "L.ID" || cbFilterBy.Text == "Fine Fees" || cbFilterBy.Text == "R.ApplicationID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void cbIsRelease_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte IsRelease = 0;
            IsRelease = (cbIsRelease.Text == "Yes") ? IsRelease = 1 : IsRelease = 0;
            if (cbIsRelease.Text == "All")
                _dt.DefaultView.RowFilter = "";
            else
                _dt.DefaultView.RowFilter = string.Format("{0} = {1}", IsRelease, "IsReleased");
            lblRecords.Text = dgvListDetainLicense.RowCount.ToString();
        }
    }
}
