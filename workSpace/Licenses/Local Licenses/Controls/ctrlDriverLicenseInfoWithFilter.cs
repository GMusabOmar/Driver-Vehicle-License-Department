using System;
using BusinessAccess;
using System.Windows.Forms;

namespace workSpace.Licenses.Local_Licenses.Controls
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        public event Action<int> OnLicenseSelected;
        protected void PersonSelected(int LicenseID)
        {
            if (OnLicenseSelected != null)
            {
                OnLicenseSelected(LicenseID);
            }
        }
        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }
        private int _LicenseID = -1;
        public int LicenseID
        {
            get { return ctrlDriverLicenseInfo1.LicenseID; }
        }
        public clsLicense SelectedLicenseInfo
        { get { return ctrlDriverLicenseInfo1.SelectedLicenseInfo; } }
        public void LoadLicenseInfo(int LicenseID)
        {
            txtFilter.Text = LicenseID.ToString();
            ctrlDriverLicenseInfo1.LoadInfo(LicenseID);
            _LicenseID = ctrlDriverLicenseInfo1.LicenseID;
            if (OnLicenseSelected != null && FilterEnabled)
                OnLicenseSelected(_LicenseID);
        }
        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }
        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnSearch.PerformClick();
            }
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFilter.Focus();
                return;
            }
            _LicenseID = int.Parse(txtFilter.Text);
            LoadLicenseInfo(_LicenseID);
        }
        public void txtLicenseIDFocus()
        {
            txtFilter.Focus();
        }
        private void txtFilter_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilter.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilter, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(txtFilter, null);
            }
        }

    }
}
