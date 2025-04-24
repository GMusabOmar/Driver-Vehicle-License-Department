using System;
using BusinessAccess;
using System.Windows.Forms;
using System.Data;

namespace workSpace.User
{
    public partial class frmListUser: Form
    {
        private DataTable _GetAllUser;
        public frmListUser()
        {
            InitializeComponent();
        }
        private void frmListUser_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _GetAllUser = clsUser.GetAllUser();
            dgvUser.DataSource = _GetAllUser;
            lblRowsNumber.Text = dgvUser.RowCount.ToString();
            if(dgvUser.Rows.Count > 0)
            {
                dgvUser.Columns[0].HeaderText = "User ID";
                dgvUser.Columns[0].Width = 100;

                dgvUser.Columns[1].HeaderText = "Person ID";
                dgvUser.Columns[1].Width = 100;

                dgvUser.Columns[2].HeaderText = "Full Name";
                dgvUser.Columns[2].Width = 370;

                dgvUser.Columns[3].HeaderText = "User Name";
                dgvUser.Columns[3].Width = 220;

                dgvUser.Columns[4].HeaderText = "Is Active";
                dgvUser.Columns[4].Width = 101;

            }
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilterBy.Text == "IsActive")
            {
                txtFilter.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.SelectedIndex = 0;
                cbIsActive.Focus();
            }
            else
            {
                txtFilter.Visible = cbFilterBy.Text != "None";
                cbIsActive.Visible = false;
                txtFilter.Text = "";
                txtFilter.Focus();
            }
        }
        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ColumnName = "IsActive";
            string FilterValue = "";
            switch (cbIsActive.Text)
            {
                case "Yes":
                    FilterValue = "1";
                        break;
                case "No":
                    FilterValue = "0";
                    break;
                default:
                    FilterValue = "All";
                    break;
            }
            if (FilterValue != "All")
                _GetAllUser.DefaultView.RowFilter = string.Format("{0} = {1}", FilterValue, ColumnName);
            else
                _GetAllUser.DefaultView.RowFilter = "";
            lblRowsNumber.Text = dgvUser.Rows.Count.ToString();
        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string ColumnName = "";
            switch(cbFilterBy.Text)
            {
                case "User ID":
                    ColumnName = "UserID";
                    break;
                case "Person ID":
                    ColumnName = "PersonID";
                    break;
                case "Name":
                    ColumnName = "Name";
                    break;
                case "User Name":
                    ColumnName = "UserName";
                    break;
                case "IsActive":
                    ColumnName = "IsActive";
                    break;
                default:
                    ColumnName = "None";
                    break;
            }
            if(txtFilter.Text == "" || ColumnName == "None")
            {
                _GetAllUser.DefaultView.RowFilter = "";
                lblRowsNumber.Text = dgvUser.Rows.Count.ToString();
                return;
            }
            if(ColumnName != "Name" && ColumnName != "UserName")
            {
                _GetAllUser.DefaultView.RowFilter = string.Format("[{0}] = {1}", ColumnName, txtFilter.Text.Trim());
            }
            else
                _GetAllUser.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", ColumnName, txtFilter.Text.Trim());
            lblRowsNumber.Text = dgvUser.Rows.Count.ToString();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();
            frmListUser_Load(null, null);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dgvUser.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();
            frmListUser_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser((int)dgvUser.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListUser_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUser.CurrentRow.Cells[0].Value;
            if (MessageBox.Show("Are you sure to delete User ID = " + UserID, "Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsUser.DeleteUser(UserID))
                {
                    MessageBox.Show("Delete successful.");
                    frmListUser_Load(null, null);
                }
                else
                    MessageBox.Show("Error not delete!");
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword((int)dgvUser.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void dgvUser_DoubleClick(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dgvUser.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

    }
}
