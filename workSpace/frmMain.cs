using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using workSpace.Applications.ApplicationType;
using workSpace.Applications.International_License;
using workSpace.Applications.Local_Driving_License;
using workSpace.Applications.Renew_Local_License;
using workSpace.Applications.ReplaceLostOrDamagedLicense;
using workSpace.Applications.Rlease_Detained_License;
using workSpace.Drivers;
using workSpace.Global_Classes;
using workSpace.Licenses.Detain_License;
using workSpace.Login;
using workSpace.People;
using workSpace.Tests;
using workSpace.Tests.Test_Type;
using workSpace.User;

namespace workSpace
{
    public partial class frmMain: Form
    {
        frmLogin _frmLogin;
        public frmMain(frmLogin Login)
        {
            InitializeComponent();
            _frmLogin = Login;
        }
        private void PeopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPeople frm = new frmListPeople();
            frm.ShowDialog();
        }
        private void UsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListUser frm = new frmListUser();
            frm.ShowDialog();
        }
        private void SignOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }
        private void CurrentUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }
        private void ChangePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }
        private void ManageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListApplicationType frm = new frmListApplicationType();
            frm.ShowDialog();
        }
        private void ManageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTest frm = new frmListTest();
            frm.ShowDialog();
        }
        private void LocalLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm = new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();
        }
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicesnseApplications frm =
                new frmListLocalDrivingLicesnseApplications();
            frm.ShowDialog();
        }
        private void RetakeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicesnseApplications frm =
                new frmListLocalDrivingLicesnseApplications();
            frm.ShowDialog();
        }
        private void RenewDrivingLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicenseApplication frm =
                new frmRenewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }
        private void ReplacementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceLostOrDamagedLicenseApplication frm =
                new frmReplaceLostOrDamagedLicenseApplication();
            frm.ShowDialog();
        }
        private void DriversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDrivers frm = new frmListDrivers();
            frm.ShowDialog();
        }
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm =
                new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }

        private void ReleaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm =
                new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frm = new frmListDetainedLicenses();
            frm.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            frmListInternationalLicesnseApplications frm 
                = new frmListInternationalLicesnseApplications();
            frm.ShowDialog();
        }
    }
}
