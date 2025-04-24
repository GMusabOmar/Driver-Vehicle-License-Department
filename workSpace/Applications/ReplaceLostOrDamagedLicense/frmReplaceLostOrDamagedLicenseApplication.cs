using System;
using BusinessAccess;
using static BusinessAccess.clsLicense;
using System.Windows.Forms;
using workSpace.Global_Classes;
using workSpace.Licenses.Local_Licenses;

namespace workSpace.Applications.ReplaceLostOrDamagedLicense
{
    public partial class frmReplaceLostOrDamagedLicenseApplication : Form
    {
        private int _NewLicenseID = -1;
        private int _GetApplicationType()
        {
            if (rbDamageLicsense.Checked)
                return (int)clsApplication.enApplicationType.ReplacementforaDamagedDrivingLicense;
            else
                return (int)clsApplication.enApplicationType.ReplacementforaLostDrivingLicense;
        }
        private enIssueReason _GetIssueLicenes()
        {
            if (rbDamageLicsense.Checked)
                return enIssueReason.Replacement_for_Damaged;
            else
                return enIssueReason.Replacement_for_Lost;
        }
        public frmReplaceLostOrDamagedLicenseApplication()
        {
            InitializeComponent();
        }
        private void frmReplaceLostOrDamagedLicenseApplication_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreateBy.Text = clsGlobal.CurrentUser.UserName;
            rbDamageLicsense.Checked = true;
        }
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            lblOldLicenseID.Text = obj.ToString();
            llShowLicenseHistory.Enabled = (obj != -1);
            if (obj == -1)
                return;
            if(!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Error: License not active!");
                btnIssueReplacement.Enabled = false;
                return;
            }
            btnIssueReplacement.Enabled = true;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void llShowNewLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }
        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Issue a Replacement for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            clsLicense _NewLicense = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ReplaceDamageOrLostLicense(_GetIssueLicenes(), clsGlobal.CurrentUser.UserID);
            if (_NewLicense == null)
            {
                MessageBox.Show("Error: not issue license");
                return;
            }
            _NewLicenseID = _NewLicense.LicenseID;
            lblLRApplicationID.Text = _NewLicense.ApplicationID.ToString();
            lblReplacementLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("Licensed Replaced Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnIssueReplacement.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            gbReplacementFor.Enabled = false;
            llShowNewLicensesInfo.Enabled = true;
        }
        private void rbDamageLicsense_CheckedChanged(object sender, EventArgs e)
        {
            lblApplicationFees.Text = clsApplicationType.Found(_GetApplicationType()).ApplicationFees.ToString();
        }
        private void rbLostLicesne_CheckedChanged(object sender, EventArgs e)
        {
            lblApplicationFees.Text = clsApplicationType.Found(_GetApplicationType()).ApplicationFees.ToString();
        }
    }
}
