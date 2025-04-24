using System;
using BusinessAccess;
using System.Windows.Forms;
using System.Data;
using workSpace.Persons;

namespace workSpace.People
{
    public partial class frmListPeople: Form
    {
        private static DataTable _GetAllPeople = clsPerson.GetAllPeople();

        private DataTable _dtPeople = _GetAllPeople.DefaultView.ToTable(false, "PersonID",
            "NationalNo", "FirstName", "SecondName", "ThirdName", "LastName", "DateOfBirth",
            "TypeGendor", "Address", "Phone" , "Email");

        public frmListPeople()
        {
            InitializeComponent();
        }

        private void _RefreshPeoplList()
        {
            _GetAllPeople = clsPerson.GetAllPeople();
            _dtPeople = _GetAllPeople.DefaultView.ToTable(false, "PersonID",
            "NationalNo", "FirstName", "SecondName", "ThirdName", "LastName", "DateOfBirth",
            "TypeGendor", "Address", "Phone", "Email");
            dgvPeople.DataSource = _dtPeople;
            lblRowsNumber.Text = dgvPeople.RowCount.ToString();
        }

        private void frmListPeople_Load(object sender, EventArgs e)
        {
            dgvPeople.DataSource = _dtPeople;
            cbFilterBy.SelectedIndex = 0;
            lblRowsNumber.Text = dgvPeople.Rows.Count.ToString();
            if (dgvPeople.Rows.Count > 0)
            {
                dgvPeople.Columns[0].HeaderText = "Person ID";
                dgvPeople.Columns[0].Width = 70;

                dgvPeople.Columns[1].HeaderText = "National No";
                dgvPeople.Columns[1].Width = 70;

                dgvPeople.Columns[2].HeaderText = "First Name";
                dgvPeople.Columns[2].Width = 120;

                dgvPeople.Columns[3].HeaderText = "Second Name";
                dgvPeople.Columns[3].Width = 120;

                dgvPeople.Columns[4].HeaderText = "Third Name";
                dgvPeople.Columns[4].Width = 120;

                dgvPeople.Columns[5].HeaderText = "Last Name";
                dgvPeople.Columns[5].Width = 120;

                dgvPeople.Columns[6].HeaderText = "Date Of Birth";
                dgvPeople.Columns[6].Width = 120;

                dgvPeople.Columns[7].HeaderText = "Gendor";
                dgvPeople.Columns[7].Width = 70;

                dgvPeople.Columns[8].HeaderText = "Address";
                dgvPeople.Columns[8].Width = 120;

                dgvPeople.Columns[9].HeaderText = "Phone";
                dgvPeople.Columns[9].Width = 120;

                dgvPeople.Columns[10].HeaderText = "Email";
                dgvPeople.Columns[10].Width = 129;

            }
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Visible = cbFilterBy.Text != "None";
            if(txtFilterBy.Visible)
            {
                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string NameOfColumn = "";
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    NameOfColumn = "PersonID";
                    break;
                case "National No.":
                    NameOfColumn = "NationalNo";
                    break;
                case "First Name":
                    NameOfColumn = "FirstName";
                    break;
                case "Second Name":
                    NameOfColumn = "SecondName";
                    break;
                case "Third Name":
                    NameOfColumn = "ThirdName";
                    break;
                case "Last Name":
                    NameOfColumn = "LastName";
                    break;
                case "Address":
                    NameOfColumn = "Address";
                    break;
                case "Gendor":
                    NameOfColumn = "TypeGendor";
                    break;
                case "Phone":
                    NameOfColumn = "Phone";
                    break;
                case "Email":
                    NameOfColumn = "Email";
                    break;
                default:
                    NameOfColumn = "None";
                    break;
            }
            if (txtFilterBy.Text == "" || NameOfColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRowsNumber.Text = _dtPeople.Rows.Count.ToString();
                return;
            }
            if (NameOfColumn == "PersonID")
                _dtPeople.DefaultView.RowFilter = string.Format("{0} = {1}", NameOfColumn, txtFilterBy.Text.Trim());
            else
                _dtPeople.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", NameOfColumn, txtFilterBy.Text.Trim());
            lblRowsNumber.Text = dgvPeople.Rows.Count.ToString();
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            _RefreshPeoplList();
        }

        private void showPerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo((int)dgvPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeoplList();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            _RefreshPeoplList();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson((int)dgvPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeoplList();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Do you want delete person with id = " + (int)dgvPeople.CurrentRow.Cells[0].Value, "Delete" , MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if(clsPerson.DeletePerson((int)dgvPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Done Delete Person ID = " + (int)dgvPeople.CurrentRow.Cells[0].Value);
                    _RefreshPeoplList();
                }
                else
                    MessageBox.Show("Error : Not Delete Person ID = " + (int)dgvPeople.CurrentRow.Cells[0].Value);
            }
        }
    }
}
