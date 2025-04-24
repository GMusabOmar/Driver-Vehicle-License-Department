using System;
using BusinessAccess;
using System.Windows.Forms;

namespace workSpace.Applications.Local_Driving_License
{
    public partial class ctrlDrivingLicenseApplicationInfo: UserControl
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        public int LocalDrivingLicenseApplicationID
        {
            get
            {
                return _LocalDrivingLicenseApplicationID;
            }
        }
        private clsLocalDrivingLicenseAppliction _LocalDrivingLicense;
        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }
        public void ResetDefaultValue()
        {
            ctrlApplicationBasicInfo1.ResetDefaultValues();
            _LocalDrivingLicenseApplicationID = -1;
            lblDLApplicationID.Text = "[???]";
        }
        public void FillLocalDrivingLicenseInfo()
        {
            lblAppliedForLicense.Text = clsLicenseClass.Find(_LocalDrivingLicense.LicenseClassID).ClassName;
            ctrlApplicationBasicInfo1.LoadApplicationBaicInfo(_LocalDrivingLicense.ApplicationID);
            _LocalDrivingLicenseApplicationID = _LocalDrivingLicense.LocalDrivingLicenseApplicationID;
            lblDLApplicationID.Text = _LocalDrivingLicense.LocalDrivingLicenseApplicationID.ToString();
            lblPassedTests.Text = _LocalDrivingLicense.GetPassedTestCount().ToString() + "/3";
        }
        public void LoadLocalDrivingLicsenseByID(int LocalDrivingLicenseApplicationID)
        {
            _LocalDrivingLicense = clsLocalDrivingLicenseAppliction.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);
            if(_LocalDrivingLicense == null)
            {
                ResetDefaultValue();
                MessageBox.Show("Not found license driving application with id = " + LocalDrivingLicenseApplicationID + " !");
                return;
            }
            FillLocalDrivingLicenseInfo();
        }
        public void LoadLocalDrivingLicsenseByApplicationID(int ApplicationID)
        {
            _LocalDrivingLicense = clsLocalDrivingLicenseAppliction.FoundByApplicationID(ApplicationID);
            if (_LocalDrivingLicense == null)
            {
                ResetDefaultValue();
                MessageBox.Show("Not found license driving application with Applicant_id = " + ApplicationID + " !");
                return;
            }
            FillLocalDrivingLicenseInfo();
        }

    }
}
