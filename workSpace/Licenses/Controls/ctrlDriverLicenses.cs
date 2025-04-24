using System;
using System.Data;
using BusinessAccess;
using System.Windows.Forms;
using workSpace.Licenses.Local_Licenses;

namespace workSpace.Licenses.Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {
        private int _DriverID = -1;
        private DataTable _LocalLicense;
        private DataTable _InternationalLicense;
        private clsDriver _Driver;
        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }
        private void _LoadLocalLicenseInfo()
        {
            _LocalLicense = clsDriver.GetLicenses(_DriverID);
            dgvLocal.DataSource = _LocalLicense;
            lblRecordsLocal.Text = dgvLocal.RowCount.ToString();
            if(dgvLocal.RowCount > 0)
            {
                dgvLocal.Columns[0].HeaderText = "Lic.ID";
                dgvLocal.Columns[0].Width = 80;

                dgvLocal.Columns[1].HeaderText = "App.ID";
                dgvLocal.Columns[1].Width = 80;

                dgvLocal.Columns[2].HeaderText = "Class Name";
                dgvLocal.Columns[2].Width = 260;

                dgvLocal.Columns[3].HeaderText = "Issue Date";
                dgvLocal.Columns[3].Width = 170;

                dgvLocal.Columns[4].HeaderText = "Exp. Date";
                dgvLocal.Columns[4].Width = 170;

                dgvLocal.Columns[5].HeaderText = "Is Active";
                dgvLocal.Columns[5].Width = 140;
            }
        }
        private void _LoadInternationalLicenseInfo()
        {
            _InternationalLicense = clsInternationalLicense.GetAllInternationalLicenseByDriverID(_DriverID);
            dgvInternational.DataSource = _InternationalLicense;
            lblRecordsLocal.Text = dgvInternational.RowCount.ToString();
            if (dgvInternational.RowCount > 0)
            {
                dgvInternational.Columns[0].HeaderText = "I.Lic.ID";
                dgvInternational.Columns[0].Width = 80;

                dgvInternational.Columns[1].HeaderText = "App.ID";
                dgvInternational.Columns[1].Width = 80;

                dgvInternational.Columns[2].HeaderText = "D.ID";
                dgvInternational.Columns[2].Width = 90;

                dgvInternational.Columns[3].HeaderText = "Issue Local License";
                dgvInternational.Columns[3].Width = 120;

                dgvInternational.Columns[4].HeaderText = "Exp. Date";
                dgvInternational.Columns[4].Width = 170;

                dgvInternational.Columns[5].HeaderText = "Is Active";
                dgvInternational.Columns[5].Width = 140;
            }
        }
        public void LoadInfoByDriverID(int DriverID)
        {
            _Driver = clsDriver.GetDriverInfoByID(DriverID);
            if (_Driver == null)
            {
                MessageBox.Show("Error: not found driver with DriverID = " + DriverID);
                return;
            }
            _DriverID = DriverID;
            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();
        }
        public void LoadInfoByPersonID(int PersonID)
        {
            _Driver = clsDriver.GetDriverInfoByPersonID(PersonID);
            if (_Driver == null)
            {
                MessageBox.Show("Error: not found driver with PersonID = " + PersonID);
                return;
            }
            _DriverID = _Driver.DriverID;
            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo((int)dgvLocal.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
        public void Clear()
        {
            _LocalLicense.Clear();
        }
    }
}
