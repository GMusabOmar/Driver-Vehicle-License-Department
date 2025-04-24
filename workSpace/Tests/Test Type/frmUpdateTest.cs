using BusinessAccess;
using System;
using System.Windows.Forms;

namespace workSpace.Tests.Test_Type
{
    public partial class frmUpdateTest: Form
    {
        private clsTestType.enTypeID _ID = clsTestType.enTypeID.Vision;
        private clsTestType _Test;
        public frmUpdateTest(clsTestType.enTypeID ID)
        {
            InitializeComponent();
            _ID = ID;
        }

        private void frmUpdateTest_Load(object sender, EventArgs e)
        {
            _Test = clsTestType.Found(_ID);
            if(_Test == null)
            {
                MessageBox.Show("Not found test id : " + _ID);
                return;
            }
            int ID = (int)_Test.TestTypeID;
            lblID.Text = ID.ToString();
            txtTitle.Text = _Test.TestTypeTitle;
            txtDescription.Text = _Test.TestTypeDescription;
            txtFees.Text = _Test.TestTypeFees.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                MessageBox.Show("Put the mouse on red flaq!");
                return;
            }
            _Test.TestTypeTitle = txtTitle.Text.Trim();
            _Test.TestTypeDescription = txtDescription.Text.Trim();
            _Test.TestTypeFees = Convert.ToDecimal(txtFees.Text.Trim());
            if(_Test.Save())
            {
                MessageBox.Show("Done save successful.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
                MessageBox.Show("Not Save!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtFees_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox Temp = (TextBox)sender;
            if(string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "should be not null this field!");
            }
            else
                errorProvider1.SetError(Temp, "should be not null this field!");
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
