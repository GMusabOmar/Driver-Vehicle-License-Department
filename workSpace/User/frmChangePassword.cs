using BusinessAccess;
using System;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using workSpace.Global_Classes;

namespace workSpace.User
{
    public partial class frmChangePassword: Form
    {
        private int _UserID = -1;
        private clsUser _User;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void _RestDefaultValues()
        {
            txtOldPassword.Clear();
            txtNewPassword.Clear();
            txtConformPassword.Clear();
        }
        private void _FillValues()
        {
            _User = clsUser.GetUserByUserID(_UserID);
            if(_User == null)
            {
                MessageBox.Show("Error not found user with id = " + _UserID);
                this.Close();
                return;
            }
            ctrlUserCard1.loadUserInfo(_UserID);
        }
        private void frmUserChangePassword_Load(object sender, EventArgs e)
        {
            _RestDefaultValues();
            _FillValues();
        }

        private void CheckOlaPasswordMatch(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtOldPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtOldPassword, "This field must be not null");
                return;
            }
            else
                errorProvider1.SetError(txtOldPassword, null);
            if(clsGlobal.ComputeHash(txtOldPassword.Text.Trim()) != clsGlobal.ComputeHash(_User.Password))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtOldPassword, "Password not correct!, try again");
                return;
            }
            errorProvider1.SetError(txtOldPassword, null);
        }

        private void IsEmptyNewPassword(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "This field must be not null");
            }
            else
                errorProvider1.SetError(txtNewPassword, null);
        }

        private void IsMatchConformPassword(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtConformPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConformPassword, "Not Conform!, Please check passowrd!");
            }
            else
                errorProvider1.SetError(txtConformPassword, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                MessageBox.Show("Please put the mouse on the red flaq!");
                return;
            }
             string ComputeHash = clsGlobal.ComputeHash(txtNewPassword.Text.Trim());
             clsGlobal.GetUserAndPasswordByRegEdit(clsGlobal.CurrentUser.UserName, ComputeHash);
            _User.Password = ComputeHash;
            if (_User.Save())
            {
                MessageBox.Show("Done save successful.");
                _RestDefaultValues();
            }
            else
                MessageBox.Show("Error not save!!");
        }
    }
}
