using System;
using System.Windows.Forms;

namespace workSpace.Applications.Local_Driving_License
{
    public partial class frmLocalDrivingLicenseApplicationInfo: Form
    {
        public int LocalDrivingLicsenseID = -1;
        public frmLocalDrivingLicenseApplicationInfo(int LocalDrivingLicsenseID)
        {
            InitializeComponent();
            this.LocalDrivingLicsenseID = LocalDrivingLicsenseID;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseApplicationInfo1.LoadLocalDrivingLicsenseByID(LocalDrivingLicsenseID);
        }

    }
}
