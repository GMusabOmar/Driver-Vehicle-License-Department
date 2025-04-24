using System;
using BusinessAccess;
using System.Windows.Forms;
using workSpace.Licenses.International_Licenses;
using workSpace.Licenses;
using workSpace.Global_Classes;

namespace workSpace.Applications.International_License
{
    public partial class frmNewInternationalLicenseApplication : Form
    {
        private int _InternationalLicenseID = -1;
        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;
            lblLocalLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = SelectedLicenseID != -1;
            if (SelectedLicenseID == -1)
                return;
            if(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClass != 3)
            {
                MessageBox.Show("Error: Application type must be 3");
                return;
            }
            bool IsLicenseExists = clsInternationalLicense.IsInternationalLicenseExists(clsLicense.Find(ctrlDriverLicenseInfoWithFilter1.LicenseID).DriverID);
            if (IsLicenseExists)
            {
                lblILLicenseID.Text = clsInternationalLicense.FindByDriverID(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.DriverID).InternationalLicenseID.ToString();
                MessageBox.Show("Person already have an active international license with ID = " + lblILLicenseID.Text, "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _InternationalLicenseID = Convert.ToInt32(lblILLicenseID.Text);
                lblLApplicationID.Text = clsInternationalLicense.FindByDriverID(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.DriverID).ApplicationID.ToString();
                llShowLicenseInfo.Enabled = true;
                btnIssue.Enabled = false;
                return;
            }
            btnIssue.Enabled = true;
        }
        private void btnCLose_Click(object sender, EventArgs e)
        {
            this.Close ();
        }
        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            clsInternationalLicense _InternationalLicenseInfo = new clsInternationalLicense();
            _InternationalLicenseInfo.ApplicantPersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            _InternationalLicenseInfo.ApplicationDate = DateTime.Now;
            _InternationalLicenseInfo.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            _InternationalLicenseInfo.LastStatusDate = DateTime.Now;
            _InternationalLicenseInfo.PaidFees = clsApplicationType.Found((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees;


            _InternationalLicenseInfo.DriverID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID;
            _InternationalLicenseInfo.IssuedUsingLocalLicenseID = ctrlDriverLicenseInfoWithFilter1.LicenseID;
            _InternationalLicenseInfo.IssueDate = DateTime.Now;
            _InternationalLicenseInfo.ExpirationDate = DateTime.Now.AddYears(clsSetting.Find(1).InternationalExpirFees);
            _InternationalLicenseInfo.CreatedByUserID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.CreatedByUserID;
            if(!_InternationalLicenseInfo.Save())
            {
                MessageBox.Show("Error to issue international license!");
                return;
            }
            _InternationalLicenseID = _InternationalLicenseInfo.InternationalLicenseID;
            lblILLicenseID.Text = _InternationalLicenseID.ToString();
            lblLocalLicenseID.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID.ToString();
            lblLApplicationID.Text = clsInternationalLicense.FindByDriverID(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.DriverID).ApplicationID.ToString();
            MessageBox.Show("International License Issued Successfully with ID=" + _InternationalLicenseID, "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssue.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
        }
        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicenseInfo frm 
                = new frmShowInternationalLicenseInfo(_InternationalLicenseID);
            frm.ShowDialog();
        }
        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm
                = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }
        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblFees.Text = clsApplicationType.Found((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees.ToString();
            lblCreateBy.Text = clsGlobal.CurrentUser.UserName;
            lblExpirationDate.Text = DateTime.Now.AddYears(clsSetting.Find(1).InternationalExpirFees).ToShortDateString();
        }
    }
}
