using BusinessAccess;
using System;
using System.Windows.Forms;
using workSpace.Global_Classes;

namespace workSpace.User
{
    public partial class frmAddUpdateUser: Form
    {
        public enum enTypeMode { Add = 0, Update };
        private enTypeMode _Mode = enTypeMode.Add;
        private int _UserID = -1;
        private clsUser _User;
        public frmAddUpdateUser()
        {
            InitializeComponent();
            _Mode = enTypeMode.Add;
            _UserID = -1;
        }
        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            _Mode = enTypeMode.Update;
            _UserID = UserID;
        }

        private void _ResetValues()
        {
            if(_Mode == enTypeMode.Add)
            {
                this.Text = "Add New User";
                _User = new clsUser();
                tpLoginInfo.Enabled = false;
                ctrlPersonCardWithFilter1.FilterFocus();
            }
            else
            {
                this.Text = "Update User";
            }
            lblUserID.Text = "???";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            cbIsActive.Checked = true;
        }
        private void _LoadData()
        {
            _User = clsUser.GetUserByUserID(_UserID);
            ctrlPersonCardWithFilter1.FilterEnable = false;
            if(_User == null)
            {
                MessageBox.Show("Not found user with id = " + _UserID);
                this.Close();
                return;
            }
            btnSave.Enabled = true;
            tpLoginInfo.Enabled = true;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            cbIsActive.Checked = _User.IsActive;
        }
        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetValues();
            if (_Mode == enTypeMode.Update)
                _LoadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(_Mode == enTypeMode.Update)
            {
                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                return;
            }

            if(ctrlPersonCardWithFilter1.PersonID != -1)
            {
                if(clsUser.IsUserExistsByPersonID(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("This user already exists!, try another.");
                    ctrlPersonCardWithFilter1.FilterFocus();
                    return;
                }
                else
                {
                    tpLoginInfo.Enabled = true;
                    btnSave.Enabled = true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                }
            }
            else
            {
                MessageBox.Show("Please Choice Person ID");
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                MessageBox.Show("Put the mouse on the red flaq!");
                return;
            }
            _User.UserName = txtUserName.Text.Trim();
            string ComputeHash = clsGlobal.ComputeHash(txtConfirmPassword.Text.Trim());

            if (_User.UserName == clsGlobal.CurrentUser.UserName)
            {
                clsGlobal.GetUserAndPasswordByRegEdit(_User.UserName, ComputeHash);
            }
            _User.Password = ComputeHash;
            _User.IsActive = cbIsActive.Checked;
            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;
            if(_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();
                _Mode = enTypeMode.Update;
                this.Text = "Update User";
                MessageBox.Show("Done Save.");
            }
            else
                MessageBox.Show("Note Save!");
        }

        private void IsPasswordEqual(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(txtConfirmPassword.Text != txtPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Not much password!");
            }
            errorProvider1.SetError(txtConfirmPassword, null);
        }

        private void IsPasswordEmpty(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "This field must be not null.");
            }
            errorProvider1.SetError(txtPassword, null);
        }

        private void IsUserNameEmpty(object sender, System.ComponentModel.CancelEventArgs e)
        {
            {
                if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "This field must be not null.");
                }
                else
                    errorProvider1.SetError(txtUserName, null);
                if (txtUserName.Text.Trim() != _User.UserName && clsUser.IsUserExistsByUserName(txtUserName.Text))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "This user name already exist, try another user name.");
                }
                else
                    errorProvider1.SetError(txtUserName, null);
            }
        }

        private void frmAddUpdateUser_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }

    }
}
