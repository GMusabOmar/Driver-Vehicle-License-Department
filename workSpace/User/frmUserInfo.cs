using System;
using System.Windows.Forms;

namespace workSpace.User
{
    public partial class frmUserInfo: Form
    {
        private int _UserID = -1;
        public frmUserInfo(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            ctrlUserCard1.loadUserInfo(_UserID);
        }

    }
}
