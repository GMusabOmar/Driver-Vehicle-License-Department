using BusinessAccess;
using System;
using System.Windows.Forms;
using workSpace.Global_Classes;
using Microsoft.Win32;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace workSpace.Login
{
    public partial class frmLogin: Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser _User;
            string UserName = txtUserName.Text.Trim();
            string Password = txtPassword.Text.Trim();
            if(Password.Length > 30)
                _User = clsUser.GetUserByUserNameAndPassword(UserName, Password);
            else
                _User = clsUser.GetUserByUserNameAndPassword(UserName, clsGlobal.ComputeHash(Password));
            if (_User != null)
            {
                if (!_User.IsActive)
                {
                    MessageBox.Show("This user not active!, call the admin.");
                    return;
                }
                if (cbIsActive.Checked)
                {
                    string ConvertPasswordToSave = clsGlobal.ComputeHash(Password);
                    clsGlobal.GetUserAndPasswordByRegEdit(UserName, ConvertPasswordToSave);
                }
                else
                    clsGlobal.GetUserAndPasswordByRegEdit("", "");
                this.Hide();
                clsGlobal.CurrentUser = _User;
                frmMain frm = new frmMain(this);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid username and password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtUserName.Text = "";
                txtUserName.Focus();
            }
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            string Username = "", Password = "";
            if (clsGlobal.RememberUserAndPasswordByRegEdit(ref Username, ref Password))
            {
                txtUserName.Text = Username;
                txtPassword.Text = Password;
                cbIsActive.Checked = true;
            }
            else
                cbIsActive.Checked = false;
        }
        private void frmLogin_Activated(object sender, EventArgs e)
        {
            txtUserName.Focus();
        }
    }
}
