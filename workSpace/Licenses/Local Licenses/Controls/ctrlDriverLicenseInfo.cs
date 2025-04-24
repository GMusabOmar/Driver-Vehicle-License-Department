using System;
using BusinessAccess;
using System.Windows.Forms;
using workSpace.Global_Classes;
using workSpace.Properties;
using System.IO;

namespace workSpace.Licenses.Local_Licenses.Controls
{
    public partial class ctrlDriverLicenseInfo: UserControl
    {
        private int _LicenseID = 0;
        public int LicenseID
        {
            get
            {
                return _LicenseID;
            }
        }
        private clsLicense _LicenseInfo;
        public clsLicense SelectedLicenseInfo
        {
            get
            {
                return _LicenseInfo;
            }
        }
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }
        private void _LoadImage()
        {
            if (_LicenseInfo.DriverInfo.PersonInfo.Gendor == 0)
                pbPerson.Image = Resources.Male_512;
            else
                pbPerson.Image = Resources.Female_512;
            string ImagePath = _LicenseInfo.DriverInfo.PersonInfo.ImagePath;
            if(ImagePath != "")
            {
                if (File.Exists(ImagePath))
                {
                    pbPerson.Load(ImagePath);
                }
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void LoadInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _LicenseInfo = clsLicense.Find(LicenseID);
            if(_LicenseInfo == null)
            {
                _LicenseID = -1;
                MessageBox.Show("Error: not found license with id = " + _LicenseID);
                return;
            }
            lblClass.Text = _LicenseInfo.LicenseClassInfo.ClassName;
            lblName.Text = _LicenseInfo.DriverInfo.PersonInfo.Name;
            lblLicenseID.Text = _LicenseInfo.LicenseID.ToString();
            lblNationalNo.Text = _LicenseInfo.DriverInfo.PersonInfo.NationalNo.ToString();
            lblGendor.Text = _LicenseInfo.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
            lblIssueDate.Text = _LicenseInfo.IssueDate.ToShortDateString();
            lblNote.Text = _LicenseInfo.Notes != "" ? _LicenseInfo.Notes : "No Note";
            lblIsActive.Text = _LicenseInfo.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = _LicenseInfo.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _LicenseInfo.DriverID.ToString();
            lblExpirDate.Text = _LicenseInfo.ExpirationDate.ToShortDateString();
            lblIsDetained.Text = _LicenseInfo.IsDetain() ? "Yes" : "No";
            lblIsseReason.Text = _LicenseInfo.IssueReasonText;
            _LoadImage();
        }
    }
}
