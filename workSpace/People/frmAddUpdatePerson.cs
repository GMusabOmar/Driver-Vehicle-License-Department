using System;
using BusinessAccess;
using workSpace.Global_Classes;
using System.Windows.Forms;
using System.Data;
using workSpace.Properties;
using System.IO;

namespace workSpace.Persons
{
    public partial class frmAddUpdatePerson: Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public event DataBackEventHandler DataBack;
        private int _PersonID = -1;
        private clsPerson _Person;
        public enum enMode { Add = 1, Update = 2};
        private enMode _Mode;
        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.Add;
            _PersonID = -1;
        }
        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _PersonID = PersonID;
        }
        private void _FillCountry()
        {
            DataTable dt = clsCountry.GetAllCountry();
            foreach (DataRow rows in dt.Rows)
                cbCountry.Items.Add(rows["CountryName"]);
        }
        private void _RestValues()
        {
            _FillCountry();
            if (_Mode == enMode.Add)
            {
                _Person = new clsPerson();
                lblTitle.Text = "Add New Person";
                this.Text = "Add New Person";
            }
            if (rbMale.Checked)
                pbPathImage.Image = Resources.Male_512;
            else
                pbPathImage.Image = Resources.Female_512;
            llRemoveImage.Visible = pbPathImage.ImageLocation != null;
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
            txtNationalNo.Text = "";
            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
            rbMale.Checked = true;
            txtEmail.Text = "";
            cbCountry.SelectedIndex = cbCountry.FindString("Jordan");
        }
        private void _LoadPersonData()
        {
            _Person = clsPerson.FindPersonByPersonID(_PersonID);
            if(_Person == null)
            {
                MessageBox.Show("Not Found Person With ID : " + _PersonID);
                this.Close();
                return;
            }
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _PersonID.ToString();
            this.Text = "Update Person";
            lblTitle.Text = "Update Person";
            txtNationalNo.Text = _Person.NationalNo;
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            if (_Person.Gendor == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;
            txtAddress.Text = _Person.Address;
            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;
            if (_Person.ImagePath != "")
                pbPathImage.ImageLocation = _Person.ImagePath;
            llRemoveImage.Visible = pbPathImage.ImageLocation != null;
            cbCountry.SelectedIndex = cbCountry.FindString(_Person.CountryInfo.CountryName);

        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _RestValues();
            if (_Mode == enMode.Update)
                _LoadPersonData();
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;";
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pbPathImage.ImageLocation = openFileDialog1.FileName;
                llRemoveImage.Visible = true;
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPathImage.ImageLocation = null;
            if (rbMale.Checked)
                pbPathImage.ImageLocation = Resources.Male_512.ToString();
            else
                pbPathImage.ImageLocation = Resources.Female_512.ToString();
            llRemoveImage.Visible = false;
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPathImage.ImageLocation == null)
                pbPathImage.Image = Resources.Male_512;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPathImage.ImageLocation == null)
                pbPathImage.Image = Resources.Female_512;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Validate_Empty(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox Tem = (TextBox)sender;
            if(string.IsNullOrEmpty(Tem.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Tem, "This field required!");
            }
            else
                errorProvider1.SetError(Tem, null);
        }

        private void Validate_NationalNo(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field required!");
            }
            else
                errorProvider1.SetError(txtNationalNo, null);

            if(txtNationalNo.Text.Trim() != _Person.NationalNo && clsPerson.IsPersonExist(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This National No. Is Exists!, try another one.");
            }
            else
                errorProvider1.SetError(txtNationalNo, null);

        }

        private void Validate_Email(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
                return;
            if(!clsValidatoin.CheckEmail(txtEmail.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Email not correct!, try again.");
            }
            else
                errorProvider1.SetError(txtEmail, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                MessageBox.Show("Please put the mouse on the red flag!");
                return;
            }
            if(!_HandleImage())
            {
                return;
            }
            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            if (rbMale.Checked)
                _Person.Gendor = 0;
            else
                _Person.Gendor = 1;
            _Person.Address = txtAddress.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.NationalityCountryID = clsCountry.Find(cbCountry.Text).CountryID;
            _Person.Email = txtEmail.Text.Trim();
            if (pbPathImage.ImageLocation != null)
                _Person.ImagePath = pbPathImage.ImageLocation;
            else
                _Person.ImagePath = "";
            if (_Person.Save())
            {
                lblPersonID.Text = _Person.PersonID.ToString();
                _Mode = enMode.Update;
                this.Text = "Update Person";
                lblTitle.Text = "Update Person";
                MessageBox.Show("Done Save", "Save..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (DataBack != null)
                    DataBack(this, _Person.PersonID);
            }
            else
                MessageBox.Show("Don't Save!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool _HandleImage()
        {
            if(pbPathImage.ImageLocation != _Person.ImagePath)
            {
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error : " + e.Message);
                    }
                }
                if (pbPathImage.ImageLocation != null)
                {
                    string ImagePath = pbPathImage.ImageLocation.ToString();
                    if (util.CopyImageToProjectImagesFolder(ref ImagePath))
                    {
                        pbPathImage.ImageLocation = ImagePath;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
