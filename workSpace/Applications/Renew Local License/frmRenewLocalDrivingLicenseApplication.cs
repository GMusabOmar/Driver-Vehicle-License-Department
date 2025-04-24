using BusinessAccess;
using System;
using System.Windows.Forms;
using workSpace.Global_Classes;
using workSpace.Licenses.Local_Licenses;

namespace workSpace.Applications.Renew_Local_License
{
    public partial class frmRenewLocalDrivingLicenseApplication : Form
    {
        private int _NewLicenseID = -1;
        public frmRenewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmRenewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = "???";
            lblApplicationFees.Text = clsApplicationType.Found((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees.ToString();
            lblCreateByUser.Text = clsGlobal.CurrentUser.UserName;
        }
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            lblOldLicenseID.Text = obj.ToString();
            llShowLicenseHistory.Enabled = (obj != -1);
            if (obj == -1)
                return;
            lblLicenseFees.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassInfo.ClassFees.ToString();
            txtNote.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Notes;
            int DefaultValidityLength = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassInfo.DefaultValidityLength;
            lblExpirationDate.Text = DateTime.Now.AddYears(DefaultValidityLength).ToShortDateString();
            lblTotalFees.Text = Convert.ToString(float.Parse(lblApplicationFees.Text) + float.Parse(lblLicenseFees.Text));
            if(!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show("License not expire yet!, will be exp = " + ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ExpirationDate);
                btnRenew.Enabled = false;
                return;
            }
            if(!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("License not active");
                btnRenew.Enabled = false;
                return;
            }
            btnRenew.Enabled = true;
        }
        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            clsLicense _NewLicene = 
                ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.RenewLicense(txtNote.Text.Trim(), clsGlobal.CurrentUser.UserID);
            if (_NewLicene == null)
                return;
            _NewLicenseID = _NewLicene.LicenseID;
            lblRenewLicenseID.Text = _NewLicenseID.ToString();
            lblRLApplicationID.Text = _NewLicene.ApplicationID.ToString();
            MessageBox.Show("Done Renew License with id = " + lblRenewLicenseID);
            llShowNewLicenseInfo.Enabled = true;
            btnRenew.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
        }
        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }
    }
}
