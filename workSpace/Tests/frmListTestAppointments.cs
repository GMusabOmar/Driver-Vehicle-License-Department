using System;
using System.Data;
using BusinessAccess;
using System.Windows.Forms;
using workSpace.Properties;

namespace workSpace.Tests
{
    public partial class frmListTestAppointments: Form
    {
        DataTable _GetAllTestAppointments;
        private int _LocalDrivingLicenseID = -1;
        private clsTestType.enTypeID _TestType = clsTestType.enTypeID.Vision;
        public frmListTestAppointments(int LocalDrivingLicenseID, clsTestType.enTypeID TestType)
        {
            InitializeComponent();
            _LocalDrivingLicenseID = LocalDrivingLicenseID;
            _TestType = TestType;
        }
        private void _LoadImage()
        {
            switch(_TestType)
            {
                case clsTestType.enTypeID.Vision:
                    lblTestAppointments.Text = "Vison Test Appointments";
                    pbTestAppointments.Image = Resources.Vision_512;
                    this.Text = lblTestAppointments.Text;
                    break;
                case clsTestType.enTypeID.Written:
                    lblTestAppointments.Text = "Written Test Appointments";
                    pbTestAppointments.Image = Resources.Written_Test_512;
                    this.Text = lblTestAppointments.Text;
                    break;
                default:
                    lblTestAppointments.Text = "Street Test Appointments";
                    pbTestAppointments.Image = Resources.Written_Test_512;
                    this.Text = lblTestAppointments.Text;
                    break;
            }
        }
        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadImage();
            ctrlDrivingLicenseApplicationInfo1.LoadLocalDrivingLicsenseByID(_LocalDrivingLicenseID);
            _GetAllTestAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType((int)_TestType, _LocalDrivingLicenseID);
            dgvTestAppointment.DataSource = _GetAllTestAppointments;
            lblRecords.Text = dgvTestAppointment.RowCount.ToString();
            if(dgvTestAppointment.RowCount > 0)
            {
                dgvTestAppointment.Columns[0].HeaderText = "Appointment ID";
                dgvTestAppointment.Columns[0].Width = 150;

                dgvTestAppointment.Columns[1].HeaderText = "Appointment Date";
                dgvTestAppointment.Columns[1].Width = 200;

                dgvTestAppointment.Columns[2].HeaderText = "Paid";
                dgvTestAppointment.Columns[2].Width = 150;

                dgvTestAppointment.Columns[3].HeaderText = "Lock";
                dgvTestAppointment.Columns[3].Width = 200;
            }
        }
        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseAppliction _LocalDrivingLicense = clsLocalDrivingLicenseAppliction.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseID);
            if(_LocalDrivingLicense.IsThereAnActiveScheduledTest(_TestType))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            clsTest LastTest = _LocalDrivingLicense.GetLastTestPerTestType(_TestType);
            if (LastTest == null)
            {
                frmScheduleTest frm1 = new frmScheduleTest(_LocalDrivingLicenseID, _TestType);
                frm1.ShowDialog();
                frmListTestAppointments_Load(null, null);
                return;
            }
            if (LastTest.TestResult == true)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseID, _TestType);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvTestAppointment.CurrentRow.Cells[0].Value;
            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseID, _TestType, TestAppointmentID);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RetakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvTestAppointment.CurrentRow.Cells[0].Value;
            frmTakeTest frm = new frmTakeTest(TestAppointmentID, _TestType);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }
    }
}
