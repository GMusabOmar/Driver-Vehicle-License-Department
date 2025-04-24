using System;
using BusinessAccess;
using System.Windows.Forms;
using workSpace.Properties;

namespace workSpace.Tests.Controls
{
    public partial class ctrlSecheduledTest: UserControl
    {
        private clsTestType.enTypeID _TestType = clsTestType.enTypeID.Vision;
        public clsTestType.enTypeID TestType
        {
            get
            {
                return _TestType;
            }
            set
            {
                _TestType = value;
                switch(_TestType)
                {
                    case clsTestType.enTypeID.Vision:
                        gbTestType.Text = "Vision Test";
                        pbTestType.Image = Resources.Vision_512;
                        break;
                    case clsTestType.enTypeID.Written:
                        gbTestType.Text = "Written Test";
                        pbTestType.Image = Resources.Written_Test_512;
                        break;
                    default:
                        gbTestType.Text = "Street Test";
                        pbTestType.Image = Resources.driving_test_512;
                        break;
                }
            }
        }
        private clsTestAppointment _TestAppointment;
        private int _TestAppointmentID = -1;
        private int _TestID = -1;
        public int TestID
        {
            get
            {
                return _TestID;
            }
        }
        public int TestAppointmentID
        {
            get
            {
                return _TestAppointmentID;
            }
        }
        private clsLocalDrivingLicenseAppliction _LocalDrivingLicense;
        private int _LocalDrivingLicenseID = -1;
        public ctrlSecheduledTest()
        {
            InitializeComponent();
        }
        public void LoadInfo(int TestAppointmentID)
        {
            _TestAppointmentID = TestAppointmentID;
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: Not found test appointment with id = " + _TestAppointmentID);
                return;
            }
            _TestID = _TestAppointment.TestID;
            _LocalDrivingLicenseID = _TestAppointment.LocalDrivingLicenseApplicationID;
            _LocalDrivingLicense = clsLocalDrivingLicenseAppliction.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseID);
            if(_LocalDrivingLicense == null)
            {
                MessageBox.Show("Error: Not found local driving license with id = " + _LocalDrivingLicenseID);
                return;
            }
            lblDLAppID.Text = _LocalDrivingLicense.LocalDrivingLicenseApplicationID.ToString();
            lblDClass.Text = "N/A";
            lblName.Text = _LocalDrivingLicense.PersonFullName;
            lblTrial.Text = _LocalDrivingLicense.TotalTrialsPerTest(_TestType).ToString();

            lblDate.Text = _TestAppointment.AppointmentDate.ToShortDateString();
            lblFees.Text = _TestAppointment.PaidFees.ToString();

            lblTestID.Text = (_TestID == -1) ? "Not Taken Yest" : _TestID.ToString();

        }
    }
}
