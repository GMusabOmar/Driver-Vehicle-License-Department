using System;
using BusinessAccess;
using System.Windows.Forms;
using workSpace.Properties;
using System.IO;

namespace workSpace.Licenses.International_Licenses.Controls
{
    public partial class ctrlDriverInternationalLicenseInfo : UserControl
    {
        private int _InternationalID = -1;
        private clsInternationalLicense _InternationalLicenseInfo;
        private void _LoadImage()
        {
            if (_InternationalLicenseInfo.DriverInfo.PersonInfo.Gendor == 0)
                pbPerson.Image = Resources.Male_512;
            else
                pbPerson.Image = Resources.Female_512;
            string ImagePath = _InternationalLicenseInfo.DriverInfo.PersonInfo.ImagePath;
            if(ImagePath != null )
            {
                if(File.Exists(ImagePath))
                {
                    pbPerson.Load(ImagePath);
                }
            }
        }
        public ctrlDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }
        public void LoadInfo(int InternationalID)
        {
            _InternationalID = InternationalID;
            _InternationalLicenseInfo = clsInternationalLicense.Find(_InternationalID);
            if (_InternationalLicenseInfo == null)
            {
                MessageBox.Show("Error: not found license international with id = " + _InternationalID);
                return;
            }
            lblName.Text = _InternationalLicenseInfo.DriverInfo.PersonInfo.Name;
            lblILicenseID.Text = _InternationalLicenseInfo.InternationalLicenseID.ToString();
            lblLicenseID.Text = _InternationalLicenseInfo.IssuedUsingLocalLicenseID.ToString();
            lblApplicationID.Text = _InternationalLicenseInfo.ApplicationID.ToString();
            lblNationalNo.Text = _InternationalLicenseInfo.DriverInfo.PersonInfo.NationalNo.ToString();
            lblGendor.Text = _InternationalLicenseInfo.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
            lblIssueDate.Text = _InternationalLicenseInfo.IssueDate.ToShortDateString();
            lblIsActive.Text = _InternationalLicenseInfo.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = _InternationalLicenseInfo.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _InternationalLicenseInfo.DriverID.ToString();
            lblExpirationDate.Text = _InternationalLicenseInfo.ExpirationDate.ToShortDateString();
            _LoadImage();
        }
    }
}
