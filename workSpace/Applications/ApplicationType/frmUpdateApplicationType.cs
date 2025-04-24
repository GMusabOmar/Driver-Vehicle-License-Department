using System;
using BusinessAccess;
using System.Windows.Forms;

namespace workSpace.Applications.ApplicationType
{
    public partial class frmUpdateApplicationType: Form
    {
        private int _ApplicationTypeID = -1;
        private clsApplicationType _AppType;
        public frmUpdateApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _AppType = clsApplicationType.Found(_ApplicationTypeID);
            if (_AppType == null)
                MessageBox.Show("This Application not found!");
            lblID.Text = _ApplicationTypeID.ToString();
            txtTitle.Text = _AppType.ApplicationTypeTitle;
            txtFees.Text = _AppType.ApplicationFees.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Put the mouse on red flaq!");
                return;
            }
            _AppType.ApplicationTypeTitle = txtTitle.Text.Trim();
            _AppType.ApplicationFees = float.Parse(txtFees.Text.Trim());
            if(_AppType.Save())
            {
                MessageBox.Show("Done Save");
                this.Close();
            }
            else
                MessageBox.Show("Error not save!!");
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void IsEmptyText(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox Temp = (TextBox)sender;
            if(string.IsNullOrEmpty(Temp.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field must be not null.");
            }
            else
                errorProvider1.SetError(Temp, null);
        }
    }
}
