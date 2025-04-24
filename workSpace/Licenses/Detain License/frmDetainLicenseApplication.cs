using BusinessAccess;
using System;
using System.Windows.Forms;
using workSpace.Global_Classes;
using workSpace.Licenses.Local_Licenses;

namespace workSpace.Licenses.Detain_License
{
    public partial class frmDetainLicenseApplication : Form
    {
        private int _DetainID = -1;
        private int _SelectedLicesneID = -1;
        public frmDetainLicenseApplication()
        {
            InitializeComponent();
        }
        private void frmDetainLicenseApplication_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblCreateBy.Text = clsGlobal.CurrentUser.UserName;
        }
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicesneID = obj;
            lblLicenseID.Text = _SelectedLicesneID.ToString();
            llShowLicenseHistory.Enabled = _SelectedLicesneID != -1;
            if (_SelectedLicesneID == -1)
            {
                return;
            }
            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetain())
            {
                MessageBox.Show("Selected License i already detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtFineFees.Focus();
            btnDetain.Enabled = true;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectedLicesneID);
            frm.ShowDialog();
        }
        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to detain license ?", "Conform", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            _DetainID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Detain(Convert.ToSingle(txtFineFees.Text), clsGlobal.CurrentUser.UserID);
            if(_DetainID == -1)
            {
                MessageBox.Show("Error: not found license!");
                return;
            }
            lblDetainID.Text = _DetainID.ToString();
            MessageBox.Show("Done detain license with id = " + _DetainID);
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            btnDetain.Enabled = false;
            txtFineFees.Enabled = false;
            llShowLicenseInfo.Enabled = true;
        }
        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }
        private void txtFineFees_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "This field must be not empty.");
            }
            else
                errorProvider1.SetError(txtFineFees, null);
        }
        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
