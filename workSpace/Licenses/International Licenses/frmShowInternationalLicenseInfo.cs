using System;
using System.Windows.Forms;

namespace workSpace.Licenses.International_Licenses
{
    public partial class frmShowInternationalLicenseInfo : Form
    {
        private int _InternationalLicenseID = -1;
        public frmShowInternationalLicenseInfo(int InternationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID = InternationalLicenseID;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmShowInternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverInternationalLicenseInfo1.LoadInfo(_InternationalLicenseID);
        }
    }
}
