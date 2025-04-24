using System;
using BusinessAccess;
using System.Windows.Forms;
using workSpace.Global_Classes;

namespace workSpace.Licenses.Local_Licenses
{
    public partial class frmIssueDriverLicenseFirstTime: Form
    {
        private clsLocalDrivingLicenseAppliction _LocalDrivingLicense;
        private int _LocalDrivingLicenseID = -1;
        public frmIssueDriverLicenseFirstTime(int LocalDrivingLicenseID)
        {
            InitializeComponent();
            _LocalDrivingLicenseID = LocalDrivingLicenseID;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            int LicenseID = _LocalDrivingLicense.IssueDrivingLicenseForFirstTime(txtNote.Text, clsGlobal.CurrentUser.UserID);
            if(LicenseID != -1)
            {
                MessageBox.Show("Done Issue driving license for first time with id = " + LicenseID);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error not issue driving license");
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmIssueDriverLicenseFirstTime_Load(object sender, EventArgs e)    
        {
            txtNote.Focus();
            _LocalDrivingLicense = clsLocalDrivingLicenseAppliction.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseID);
            if (_LocalDrivingLicense == null)
            {
                MessageBox.Show("Error not found local driving license with id = " + _LocalDrivingLicenseID);
                this.Close();
                return;
            }
            if(!_LocalDrivingLicense.PassedAllTests())
            {
                MessageBox.Show("Shoud pass all test before issue license!");
                this.Close();
                return;
            }
            int LicenseID = _LocalDrivingLicense.GetActiveLicenseID();
            if(LicenseID != -1)
            {
                MessageBox.Show("Person already has License before with License ID = " + LicenseID);
                this.Close();
                return;
            }
            ctrlDrivingLicenseApplicationInfo1.LoadLocalDrivingLicsenseByID(_LocalDrivingLicenseID);
        }

    }
}
