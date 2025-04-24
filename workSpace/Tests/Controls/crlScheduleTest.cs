using System;
using BusinessAccess;
using System.Windows.Forms;
using workSpace.Properties;
using workSpace.Global_Classes;

namespace workSpace.Tests.Controls
{
    public partial class crlScheduleTest: UserControl
    {
        public enum enMode { Add = 1, Update = 2 }
        private enMode _Mode = enMode.Add;
        public enum enCreateMode { FirstTimeTestSchedule = 1, RetakeTestSchedule = 2 };
        private enCreateMode _CreateMode = enCreateMode.FirstTimeTestSchedule;
        private clsLocalDrivingLicenseAppliction _LocalDrivingLicense;
        private int _LocalDrivingLicenseID = -1;
        private clsTestType.enTypeID _TestType = clsTestType.enTypeID.Vision;
        private clsTestAppointment _TestAppointment;
        private int _TestAppointmentID = -1;
        public clsTestType.enTypeID TestType
        {
            get
            {
                return _TestType;
            }
            set
            {
                _TestType = value;
                switch (_TestType) { 
                    case clsTestType.enTypeID.Vision:
                        gbTestType.Text = "Vision Test";
                        pbScheduleTest.Image = Resources.Vision_512;
                        break;
                    case clsTestType.enTypeID.Written:
                        gbTestType.Text = "Written Test";
                        pbScheduleTest.Image = Resources.Written_Test_512;
                        break;
                    default:
                        gbTestType.Text = "Street Test";
                        pbScheduleTest.Image = Resources.driving_test_512;
                        break;
                }
            }
        }
        public crlScheduleTest()
        {
            InitializeComponent();
        }
        private bool _LoadTestAppointmentInfo()
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if(_TestAppointment == null)
            {
                MessageBox.Show("Error: not found testAppointment with id = " + _TestAppointmentID);
                btnSave.Enabled = false;
                return false; 
            }
            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
                dtpDate.MinDate = DateTime.Now;
            else
                dtpDate.MinDate = _TestAppointment.AppointmentDate;
            dtpDate.Value = _TestAppointment.AppointmentDate;
            if(_TestAppointment.RetakeTestApplicationID == -1)
            {
                lblRAppFees.Text = "0";
                lblRTestAppID.Text = "N/A";
            }
            else
            {
                lblRAppFees.Text = _TestAppointment.PaidFees.ToString();
                lblRTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
            }
            return true;
        }
        public void LoadInfo(int LocalDrivingLicenseID, int TestAppointmentID = -1)
        {
            if (TestAppointmentID == -1)
                _Mode = enMode.Add;
            else
                _Mode = enMode.Update;
            _LocalDrivingLicenseID = LocalDrivingLicenseID;
            _TestAppointmentID = TestAppointmentID;
            _LocalDrivingLicense = clsLocalDrivingLicenseAppliction.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseID);
            if(_LocalDrivingLicense == null)
            {
                MessageBox.Show("Error : not found local driving license with id " + _LocalDrivingLicenseID);
                btnSave.Enabled = false;
                return;
            }
            if (_LocalDrivingLicense.DoesAttendTestType(_TestType))
                _CreateMode = enCreateMode.RetakeTestSchedule;
            else
                _CreateMode = enCreateMode.FirstTimeTestSchedule;
            if(_CreateMode == enCreateMode.RetakeTestSchedule)
            {
                lblRAppFees.Text = clsApplicationType.Found((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees.ToString();
                lblRTestAppID.Text = "0";
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
            }else
            {
                lblRAppFees.Text = "0";
                gbRetakeTestInfo.Enabled = false;
                lblRTestAppID.Text = "N/A";
                lblTitle.Text = "Schedule Test";
            }
            lblDLAppID.Text = _LocalDrivingLicense.LocalDrivingLicenseApplicationID.ToString();
            lblDClass.Text = _LocalDrivingLicense.LicenseClassID.ToString();
            lblName.Text = _LocalDrivingLicense.PersonFullName;
            lblTrial.Text = _LocalDrivingLicense.TotalTrialsPerTest(_TestType).ToString();
            if (_Mode == enMode.Add)
            {
                dtpDate.Value = DateTime.Now;
                lblFees.Text = clsTestType.Found(_TestType).TestTypeFees.ToString();
                lblRAppFees.Text = "0";
                lblRTestAppID.Text = "N/A";
                _TestAppointment = new clsTestAppointment();
            }
            else
            {
                if(!_LoadTestAppointmentInfo())
                    return;
            }
            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRAppFees.Text)).ToString();
            if (!_HandleActive())
                return;
            if (!_HandleIsLock())
                return;
            if (!_HandlePervesTest())
                return;
        }
        private bool _HandleActive()
        {
            if(_Mode == enMode.Add && clsLocalDrivingLicenseAppliction.IsThereAnActiveScheduledTest(_LocalDrivingLicenseID, (int)_TestType))    
            {
                MessageBox.Show("Error: there is already application active !");
                btnSave.Enabled = false;
                dtpDate.Enabled = false;
                return false;
            }
            return true;
        }
        private bool _HandleIsLock()
        {
            if(_TestAppointment.IsLocked)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Person already sat this test";
                dtpDate.Enabled = false;
                btnSave.Enabled = false;
                return false;
            }
            else
                lblMessage.Visible = false;
            return true;
        }
        private bool _HandlePervesTest()
        {
            switch(_TestType)
            {
                case clsTestType.enTypeID.Vision:
                    return true;
                case clsTestType.enTypeID.Written:
                    if(!_LocalDrivingLicense.DoesPassPreviousTest(clsTestType.enTypeID.Vision))
                    {
                        lblMessage.Enabled = true;
                        lblMessage.Text = "Should pass previous test(Vison).";
                        dtpDate.Enabled = false;
                        btnSave.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblMessage.Enabled = false;
                        dtpDate.Enabled = true;
                        btnSave.Enabled = true;
                        return true;
                    }
                default:
                    if (!_LocalDrivingLicense.DoesPassPreviousTest(clsTestType.enTypeID.Written))
                    {
                        lblMessage.Enabled = true;
                        lblMessage.Text = "Should pass previous test(Written).";
                        dtpDate.Enabled = false;
                        btnSave.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblMessage.Enabled = false;
                        dtpDate.Enabled = true;
                        btnSave.Enabled = true;
                        return true;
                    }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_RetakeTestApplication())
            {
                MessageBox.Show("Error not save!");
                return;
            }
            _TestAppointment.TestTypeID = _TestType;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseID;
            _TestAppointment.AppointmentDate = dtpDate.Value;
            _TestAppointment.PaidFees = Convert.ToDecimal(lblFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            if(_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Done save successfuly.");
            }
            else
                MessageBox.Show("Error: not save!!");
        }
        private bool _RetakeTestApplication()
        {
            if(_Mode == enMode.Add && _CreateMode == enCreateMode.RetakeTestSchedule)
            {
                clsApplication _Application = new clsApplication();
                _Application.ApplicantPersonID = _LocalDrivingLicense.ApplicantPersonID;
                _Application.ApplicationDate = DateTime.Now;
                _Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                _Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                _Application.LastStatusDate = DateTime.Now;
                _Application.PaidFees = clsApplicationType.Found((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees;
                _Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                if (!_Application.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Error: not save application!!!");
                    return false;
                }
                _TestAppointment.RetakeTestApplicationID = _Application.ApplicationID;
            }
            return true;
        }

    }
}
