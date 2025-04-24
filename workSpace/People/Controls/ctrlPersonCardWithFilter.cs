using BusinessAccess;
using workSpace.People;
using System;
using System.Windows.Forms;
using workSpace.Persons;

namespace workSpace.People.Controls
{
    public partial class ctrlPersonCardWithFilter: UserControl
    {
        public event Action<int> OnPersonSelected;
        private bool _ShowAddNewPerson = true;
        public bool ShowAddNewPerson
        {
            get
            {
                return _ShowAddNewPerson;
            }
            set
            {
                _ShowAddNewPerson = value;
                btnAddNewPerson.Visible = _ShowAddNewPerson;
            }
        }
        private bool _FilterEnable = true;
        public bool FilterEnable
        {
            get
            {
                return _FilterEnable;
            }
            set
            {
                _FilterEnable = value;
                gbFind.Enabled = _FilterEnable;
            }
        }
        public int PersonID
        {
            get
            {
                return ctrlPersonCard1.PersonID;
            }
        }
        public clsPerson SelectedPersonInfo 
        {
            get
            {
                return ctrlPersonCard1.SelectedPersonInfo;
            }
        }
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }
        private void _FindPersonInfo()
        {
            switch(cbFilterBy.Text)
            {
                case "National No":
                    ctrlPersonCard1.LoadPersonInfo(txtFilterBy.Text);
                    break;
                default:
                    ctrlPersonCard1.LoadPersonInfo(int.Parse(txtFilterBy.Text));
                    break;
            }
            if (OnPersonSelected != null && FilterEnable)
                OnPersonSelected(PersonID);
        }
        public void LoadPersonInfo(int PersonID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterBy.Text = PersonID.ToString();
            _FindPersonInfo();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Text = "";
            txtFilterBy.Focus();
        }

        private void txtFilterBy_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFilterBy.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterBy, "This field must not null");
            }
            errorProvider1.SetError(txtFilterBy, null);
        }

        private void btnFindPerson_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                MessageBox.Show("Please fill text, must be not null.");
                return;
            }
            _FindPersonInfo();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.DataBack += MyDataBack;
            frm.ShowDialog();
        }
        private void MyDataBack(object sender, int PersonID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterBy.Text = PersonID.ToString();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                btnFindPerson.PerformClick();
            }
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterBy.Focus();
        }

        public void FilterFocus()
        {
            txtFilterBy.Focus();
        }

    }
}
