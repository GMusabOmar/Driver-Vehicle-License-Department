using System;
using BusinessAccess;
using System.Windows.Forms;
using System.Data;

namespace workSpace.Tests.Test_Type
{
    public partial class frmListTest: Form
    {
        DataTable _GetAllTest;
        public frmListTest()
        {
            InitializeComponent();
        }

        private void frmListTest_Load(object sender, EventArgs e)
        {
            _GetAllTest = clsTestType.GetAllTest();
            dgvTest.DataSource = _GetAllTest;
            lblRowsNumber.Text = dgvTest.RowCount.ToString();
            if(dgvTest.RowCount > 0)
            {
                dgvTest.Columns[0].HeaderText = "ID";
                dgvTest.Columns[0].Width = 60;

                dgvTest.Columns[1].HeaderText = "Title";
                dgvTest.Columns[1].Width = 170;

                dgvTest.Columns[2].HeaderText = "Description";
                dgvTest.Columns[2].Width = 170;

                dgvTest.Columns[3].HeaderText = "Fees";
                dgvTest.Columns[3].Width = 137;
            }
        }

        private void UpdateTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateTest frm = new frmUpdateTest((clsTestType.enTypeID)dgvTest.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListTest_Load(null, null);
        }

        private void dgvTest_DoubleClick(object sender, EventArgs e)
        {
            frmUpdateTest frm = new frmUpdateTest((clsTestType.enTypeID)dgvTest.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListTest_Load(null, null);
        }
    }
}
