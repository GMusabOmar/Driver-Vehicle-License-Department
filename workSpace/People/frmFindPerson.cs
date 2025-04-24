using System;
using System.Windows.Forms;

namespace workSpace.People
{
    public partial class frmFindPerson: Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public event DataBackEventHandler DataBack;
        public frmFindPerson()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DataBack(this, ctrlPersonCardWithFilter1.PersonID);
        }
    }
}
