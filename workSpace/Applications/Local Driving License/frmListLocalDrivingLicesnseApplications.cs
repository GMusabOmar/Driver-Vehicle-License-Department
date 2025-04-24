using System;
using BusinessAccess;
using System.Windows.Forms;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using workSpace.Tests;
using workSpace.Licenses.Local_Licenses;
using workSpace.Licenses;

namespace workSpace.Applications.Local_Driving_License
{
    public partial class frmListLocalDrivingLicesnseApplications: Form
    {
        DataTable _GetAllLocalApplication;
        public frmListLocalDrivingLicesnseApplications()
        {
            InitializeComponent();
        }
        private void frmListLocalDrivingLicesnseApplications_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _GetAllLocalApplication = clsLocalDrivingLicenseAppliction.GetAllDrivingLicense();
            dgvLocalApplication.DataSource = _GetAllLocalApplication;
            lblRecords.Text = dgvLocalApplication.RowCount.ToString();
            if(dgvLocalApplication.RowCount > 0)
            {
                dgvLocalApplication.Columns[0].HeaderText = "L.ID";
                dgvLocalApplication.Columns[0].Width = 50;

                dgvLocalApplication.Columns[1].HeaderText = "ClassName";
                dgvLocalApplication.Columns[1].Width = 260;

                dgvLocalApplication.Columns[2].HeaderText = "N.No";
                dgvLocalApplication.Columns[2].Width = 50;

                dgvLocalApplication.Columns[3].HeaderText = "Name";
                dgvLocalApplication.Columns[3].Width = 210;

                dgvLocalApplication.Columns[4].HeaderText = "A.Date";
                dgvLocalApplication.Columns[4].Width = 100;

                dgvLocalApplication.Columns[5].HeaderText = "Pass Test";
                dgvLocalApplication.Columns[5].Width = 60;

                dgvLocalApplication.Columns[6].HeaderText = "Status";
                dgvLocalApplication.Columns[6].Width = 98;

            }
        }
        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm =
                new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();
            frmListLocalDrivingLicesnseApplications_Load(null, null);
        }
        private void EditApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvLocalApplication.CurrentRow.Cells[0].Value;
            frmAddUpdateLocalDrivingLicesnseApplication frm =
                new frmAddUpdateLocalDrivingLicesnseApplication(ID);
            frm.ShowDialog();
            frmListLocalDrivingLicesnseApplications_Load(null, null);
        }
        private void dgvLocalApplication_DoubleClick(object sender, EventArgs e)
        {
            int ID = (int)dgvLocalApplication.CurrentRow.Cells[0].Value;
            frmLocalDrivingLicenseApplicationInfo frm =
                new frmLocalDrivingLicenseApplicationInfo(ID);
            frm.ShowDialog();
        }
        private void ShowApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvLocalApplication.CurrentRow.Cells[0].Value;
            frmLocalDrivingLicenseApplicationInfo frm =
                new frmLocalDrivingLicenseApplicationInfo(ID);
            frm.ShowDialog();
        }
        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvLocalApplication.CurrentRow.Cells[0].Value;
            if (MessageBox.Show("Are you sure to delete license id = " + ID, "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            clsLocalDrivingLicenseAppliction Find = clsLocalDrivingLicenseAppliction.FindByLocalDrivingAppLicenseID(ID);
            if (Find == null)
            {
                MessageBox.Show("Not found local licence diving id = " + ID);
                return;
            }
            if (Find.DeleteLocalDrivingLicense())
            {
                MessageBox.Show("Done delete id = " + ID);
                frmListLocalDrivingLicesnseApplications_Load(null, null);
                return;
            }
            else
                MessageBox.Show("Error not delete id " + ID + " !");
        }
        private void CancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvLocalApplication.CurrentRow.Cells[0].Value;
            if (MessageBox.Show("Are you sure to cancel application with id = " + ID + " ?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            clsLocalDrivingLicenseAppliction Find = clsLocalDrivingLicenseAppliction.FindByLocalDrivingAppLicenseID(ID);
            if(Find == null)
            {
                MessageBox.Show("Not found local driving application id = " + ID);
                return;
            }
            if (Find.Cancel())
            {
                MessageBox.Show("Done cancel.");
                frmListLocalDrivingLicesnseApplications_Load(null, null);
                return;
            }
            else
                MessageBox.Show("Error to cancel application");
        }
        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string ColName = "";
            switch(cbFilterBy.Text)
            {
                case "L.ID":
                    ColName = "LocalDrivingLicenseApplicationID";
                    break;
                case "ClassName":
                    ColName = "ClassName";
                    break;
                case "N.No":
                    ColName = "NationalNo";
                    break;
                case "Name":
                    ColName = "FullName";
                    break;
                case "Pass Test":
                    ColName = "PassedTestCount";
                    break;
                case "Status":
                    ColName = "Status";
                    break;
                default:
                    ColName = "None";
                    break;
            }
            if(ColName == "None" || txtFilterBy.Text == "")
            {
                _GetAllLocalApplication.DefaultView.RowFilter = "";
                lblRecords.Text = dgvLocalApplication.Rows.Count.ToString();
                return;
            }
            if (cbFilterBy.Text == "L.ID" || cbFilterBy.Text == "Pass Test")
                _GetAllLocalApplication.DefaultView.RowFilter = string.Format("{0} = {1}", ColName, txtFilterBy.Text);
            else
                _GetAllLocalApplication.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", ColName, txtFilterBy.Text);
            lblRecords.Text = dgvLocalApplication.Rows.Count.ToString();
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Visible = cbFilterBy.Text != "None";
        }
        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "L.ID" || cbFilterBy.Text == "Pass Test")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void _ScheduleTest(clsTestType.enTypeID TestType)
        {

            int LocalDrivingLicenseApplicationID = (int)dgvLocalApplication.CurrentRow.Cells[0].Value;
            frmListTestAppointments frm = new frmListTestAppointments(LocalDrivingLicenseApplicationID, TestType);
            frm.ShowDialog();
            //refresh
            frmListLocalDrivingLicesnseApplications_Load(null, null);

        }
        private void ScheduleVisonTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTypeID.Vision);
        }
        private void ScheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTypeID.Written);
        }
        private void ScheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTypeID.Practical);
        }
        private void IssueDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalApplication.CurrentRow.Cells[0].Value;
            frmIssueDriverLicenseFirstTime frm =
                new frmIssueDriverLicenseFirstTime(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
            frmListLocalDrivingLicesnseApplications_Load(null, null);
        }
        private void ShowLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalApplication.CurrentRow.Cells[0].Value;
            int LicenseID = clsLocalDrivingLicenseAppliction.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID).GetActiveLicenseID();
            if(LicenseID != -1)
            {
                frmShowLicenseInfo frm =
                new frmShowLicenseInfo(LicenseID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Error: not fount License ID = " + LicenseID);
            }
        }
        private void cmsApplications_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int ID = (int)dgvLocalApplication.CurrentRow.Cells[0].Value;
            int TotalTest = (int)dgvLocalApplication.CurrentRow.Cells[5].Value;
            clsLocalDrivingLicenseAppliction _Local = clsLocalDrivingLicenseAppliction.FindByLocalDrivingAppLicenseID(ID);
            bool IsLicenseExist = _Local.IsLicenseIssued();
            ShowApplicationDetailsToolStripMenuItem.Enabled = IsLicenseExist;
            IssueDrivingLicenseToolStripMenuItem.Enabled = (TotalTest == 3) && !IsLicenseExist;
            EditApplicationToolStripMenuItem.Enabled = !IsLicenseExist && _Local.ApplicationStatus == clsApplication.enApplicationStatus.New;
            CancelApplicationToolStripMenuItem.Enabled = _Local.ApplicationStatus == clsApplication.enApplicationStatus.New;
            DeleteApplicationToolStripMenuItem.Enabled = _Local.ApplicationStatus == clsApplication.enApplicationStatus.New;
            ShowLicenseToolStripMenuItem.Enabled = IsLicenseExist;
            bool VisionTest = _Local.DoesPassTestType(clsTestType.enTypeID.Vision);
            bool Written = _Local.DoesPassTestType(clsTestType.enTypeID.Written);
            bool StreetTest = _Local.DoesPassTestType(clsTestType.enTypeID.Practical);
            ScheduleTestsToolStripMenuItem.Enabled = (!VisionTest || !Written || !StreetTest) && _Local.ApplicationStatus == clsApplication.enApplicationStatus.New;
            if(ScheduleTestsToolStripMenuItem.Enabled)
            {
                ScheduleVisonTestToolStripMenuItem.Enabled = !VisionTest;
                ScheduleWrittenTestToolStripMenuItem.Enabled = VisionTest && !Written;
                ScheduleStreetTestToolStripMenuItem.Enabled = VisionTest && Written && !StreetTest;
            }
        }

        private void ShowPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseAppliction = (int)dgvLocalApplication.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseAppliction _Local = clsLocalDrivingLicenseAppliction.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseAppliction);
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_Local.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
