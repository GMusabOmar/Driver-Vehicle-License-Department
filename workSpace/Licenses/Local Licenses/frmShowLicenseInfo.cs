using System;
using System.Windows.Forms;

namespace workSpace.Licenses.Local_Licenses
{
    public partial class frmShowLicenseInfo : Form
    {
        private int _LicenseID = -1;
        public frmShowLicenseInfo(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
        }
        private void frmShowLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfo1.LoadInfo(_LicenseID);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
