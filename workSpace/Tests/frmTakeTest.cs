using System;
using BusinessAccess;
using System.Windows.Forms;
using workSpace.Global_Classes;

namespace workSpace.Tests
{
    public partial class frmTakeTest: Form
    {
        private int _TestAppointmentID = -1;
        private clsTestType.enTypeID _TestType;
        private clsTest _Test;
        private int _TestID = -1;
        public frmTakeTest(int TestAppointmentID, clsTestType.enTypeID TestType)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
            _TestType = TestType;
        }
        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlSecheduledTest1.LoadInfo(_TestAppointmentID);
            ctrlSecheduledTest1.TestType = _TestType;
            if(_TestAppointmentID == -1)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;
            _TestID = ctrlSecheduledTest1.TestID;
            if (_TestID != -1)
            {
                _Test = clsTest.GetTestByID(_TestID);
                if (_Test.TestResult)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = true;
                txtNote.Text = _Test.Notes;
                rbPass.Enabled = false;
                rbFail.Enabled = false;
                lblMessageChange.Enabled = true;
            }
            else
                _Test = new clsTest();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to save?", "Conform", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            _Test.TestAppointmentID = _TestAppointmentID;
            _Test.TestResult = rbPass.Checked;
            _Test.Notes = txtNote.Text.Trim();
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            if(_Test.Save())
            {
                MessageBox.Show("Done save successfuly.");
                btnSave.Enabled = false;
            }
            else
                MessageBox.Show("Error : Not save test!");
        }

    }
}
