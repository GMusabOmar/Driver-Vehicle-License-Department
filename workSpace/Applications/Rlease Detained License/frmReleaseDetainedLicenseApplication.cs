using BusinessAccess;
using System;
using System.Windows.Forms;
using workSpace.Global_Classes;
using workSpace.Licenses;
using workSpace.Licenses.Local_Licenses;

namespace workSpace.Applications.Rlease_Detained_License
{
    public partial class frmReleaseDetainedLicenseApplication : Form
    {
        private int _SelectedLicense = -1;
        public frmReleaseDetainedLicenseApplication()
        {
            InitializeComponent();
        }
        public frmReleaseDetainedLicenseApplication(int SelectedLicense)
        {
            InitializeComponent();
            _SelectedLicense = SelectedLicense;
            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_SelectedLicense);
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
        }
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicense = obj;
            lblLicenseID.Text = _SelectedLicense.ToString();
            llShowLicenseHistory.Enabled = _SelectedLicense != -1;
            if (_SelectedLicense == -1)
                return;
            if(!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetain())
            {
                MessageBox.Show("Error: this license is not detained!");
                return;
            }
            lblDetainID.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedLicenseInfo.DetainID.ToString();
            lblDetainDate.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedLicenseInfo.DetainDate.ToShortDateString();
            lblFineFees.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedLicenseInfo.FineFees.ToString();
            lblApplicationFees.Text = clsApplicationType.Found((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees.ToString();
            lblTotalFees .Text= (Convert.ToSingle(lblFineFees.Text) + Convert.ToSingle(lblApplicationFees.Text)).ToString();
            lblCreateBy.Text = clsGlobal.CurrentUser.UserID.ToString();
            btnRelease.Enabled = true;
        }
        private void btnCloes_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectedLicense);
            frm.ShowDialog();
        }
        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }
        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to release this detained  license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            int ApplicationID = -1;
            bool IsRelase = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsReleaseLisense(clsGlobal.CurrentUser.UserID, ref ApplicationID);
            lblApplicationID.Text = ApplicationID.ToString();
            if (!IsRelase)
            {
                MessageBox.Show("Faild to to release the Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Done release license :-)");
            btnRelease.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
        }
    }
}
