using BusinessAccess;
using System;
using System.Windows.Forms;

namespace workSpace.User
{
    public partial class ctrlUserCard: UserControl
    {
        private int _UserID = -1;
        public int UserID { get { return _UserID; } }
        private clsUser _User;
        public ctrlUserCard()
        {
            InitializeComponent();
        }
        private void _RestValues()
        {
            ctrlPersonCard1.ResetDefaultValues();
            lblUserID.Text = "???";
            lblUserName.Text = "???";
            lblIsActive.Text = "???";
        }
        public void loadUserInfo(int UserID)
        {
            _UserID = UserID;
            _User = clsUser.GetUserByUserID(UserID);
            if(_User == null)
            {
                _RestValues();
                MessageBox.Show("Error not found user with : " + _UserID);
                return;
            }
            _FillValues();
        }
        private void _FillValues()
        {
            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;
            if (_User.IsActive)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";
        }
    }
}
