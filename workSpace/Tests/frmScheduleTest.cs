using System;
using BusinessAccess;
using System.Windows.Forms;

namespace workSpace.Tests
{
    public partial class frmScheduleTest: Form
    {
        private int _LocalDrivingLicenseID = -1;
        private int _TestAppointmentID = -1;
        private clsTestType.enTypeID _TestType = clsTestType.enTypeID.Vision;
        public frmScheduleTest(int LocalDrivingLicenseID, clsTestType.enTypeID TestType, int TestAppointmentID = -1)
        {
            InitializeComponent();
            _LocalDrivingLicenseID = LocalDrivingLicenseID;
            _TestAppointmentID = TestAppointmentID;
            _TestType = TestType;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            crlScheduleTest1.TestType = _TestType;
            crlScheduleTest1.LoadInfo(_LocalDrivingLicenseID, _TestAppointmentID);
        }

    }
}
