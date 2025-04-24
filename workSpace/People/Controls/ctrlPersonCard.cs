using System;
using BusinessAccess;
using System.Windows.Forms;
using workSpace.Properties;
using workSpace.People;
using System.IO;
using workSpace.Persons;

namespace workSpace.People.Controls
{
    public partial class ctrlPersonCard: UserControl
    {
        private int _PersonID = -1;
        public int PersonID 
        { 
            get 
            { 
                return _PersonID;
            } 
        }
        private clsPerson _clsPerson;
        public clsPerson SelectedPersonInfo 
        { 
            get 
            { 
                return _clsPerson;
            }
        }
        public ctrlPersonCard()
        {
            InitializeComponent();
        }
        public void ResetDefaultValues()
        {
            _PersonID = -1;
            lblPersonID.Text = "[????]";
            lblName.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblGendor.Text = "[????]";
            lblEmail.Text = "[????]";
            lblAddress.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblPhone.Text = "[????]";
            lblCountry.Text = "[????]";
            pbPathImage.ImageLocation = Resources.Male_512.ToString();
        }
        private void _FillImage()
        {
            if (_clsPerson.Gendor == 0)
                pbPathImage.ImageLocation = Resources.Male_512.ToString();
            else
                pbPathImage.ImageLocation = Resources.Female_512.ToString();
            if(_clsPerson.ImagePath != "")
            {
                if (File.Exists(_clsPerson.ImagePath))
                    pbPathImage.ImageLocation = _clsPerson.ImagePath;
                else
                    MessageBox.Show("Can't load Image, because not found the path!");
            }
        }
        private void _FillPersonInfo()
        {
            _PersonID = _clsPerson.PersonID;
            lblPersonID.Text = _clsPerson.PersonID.ToString();
            lblName.Text = _clsPerson.Name;
            lblNationalNo.Text = _clsPerson.NationalNo;
            if(_clsPerson.Gendor == 0)
                lblGendor.Text = "Male";
            else
                lblGendor.Text = "Female";
            lblEmail.Text = _clsPerson.Email;
            lblAddress.Text = _clsPerson.Address;
            lblDateOfBirth.Text = _clsPerson.DateOfBirth.ToString();
            lblPhone.Text = _clsPerson.Phone;
            lblCountry.Text = clsCountry.Find(_clsPerson.NationalityCountryID).CountryName;
            _FillImage();
        }
        public void LoadPersonInfo(int PersonID)
        {
            _clsPerson = clsPerson.FindPersonByPersonID(PersonID);
            if(_clsPerson == null)
            {
                ResetDefaultValues();
                MessageBox.Show("Error not found Person with ID = " + PersonID + " !");
                return;
            }
            _FillPersonInfo();
        }
        public void LoadPersonInfo(string NationalNo)
        {
            _clsPerson = clsPerson.FindPersonByNationalNo(NationalNo);
            if (_clsPerson == null)
            {
                ResetDefaultValues();
                clsEventLog _Log = clsEventLog.SetEvent("ctrlPersonCard", "_clsPerson == null", clsEventLog.enEntryType.Error);
                MessageBox.Show("Error not found Person with ID = " + NationalNo + " !");
                return;
            }
            _FillPersonInfo();
        }
        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmAddUpdatePerson(_PersonID);
            frm.ShowDialog();
            LoadPersonInfo(_PersonID);
        }

    }
}
