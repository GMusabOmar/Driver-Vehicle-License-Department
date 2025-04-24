using BusinessAccess;
using System;
using System.Windows.Forms;
using workSpace.Persons;

namespace workSpace.Applications.Controls
{
    public partial class ctrlApplicationBasicInfo: UserControl
    {
        private clsApplication _clsApplication;
        private int _ApplicationID = -1;
        public int ApplicationID
        {
            get
            {
                return _ApplicationID;
            }
        }
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }
        public void ResetDefaultValues()
        {
            _ApplicationID = -1;
            lblID.Text = "[???]";
            lblStatus.Text = "[???]";
            lblFees.Text = "[???]";
            lblType.Text = "[???]";
            lblApplicant.Text = "[???]";
            lblDate.Text = "[???]";
            lblStatus.Text = "[??/??/????]";
            lblCreateBy.Text = "[???]";
        }
        public void _FillApplicationInfo()
        {
            _ApplicationID = _clsApplication.ApplicationID;
            lblID.Text = _clsApplication.ApplicationID.ToString();
            lblStatus.Text = _clsApplication.ApplicationStatus.ToString();
            lblFees.Text = _clsApplication.PaidFees.ToString();
            lblType.Text = _clsApplication.ApplicationTypeInfo.ApplicationTypeTitle;
            lblApplicant.Text = _clsApplication.ApplicantPersonID.ToString();
            lblDate.Text = Convert.ToDateTime(_clsApplication.ApplicationDate.ToString()).ToShortDateString();
            lblStatusDate.Text = Convert.ToDateTime(_clsApplication.LastStatusDate.ToString()).ToShortDateString();
            lblCreateBy.Text = _clsApplication.CreatedByUserInof.UserName;
        }
        public void LoadApplicationBaicInfo(int ApplicationID)
        {
            _clsApplication = clsApplication.FindBaseApplication(ApplicationID);
            if (_clsApplication == null)
            {
                ResetDefaultValues();
                MessageBox.Show("Application basic info not found with Id = " + _ApplicationID);
                return;
            }
            else
                _FillApplicationInfo();
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(_clsApplication.ApplicantPersonID);
            frm.ShowDialog();
            LoadApplicationBaicInfo(_ApplicationID);
        }

    }
}
