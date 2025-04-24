using System;
using System.Data;
using BusinessAccess;
using System.Windows.Forms;
using workSpace.Global_Classes;

namespace workSpace.Applications.Local_Driving_License
{
    public partial class frmAddUpdateLocalDrivingLicesnseApplication: Form
    {
        public enum enTypeMode { Add = 1, Update = 2};
        private enTypeMode _Mode;
        private int _SelectedPersonID = -1;
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseAppliction _clsLDLA;
        public frmAddUpdateLocalDrivingLicesnseApplication()
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = -1;
            _Mode = enTypeMode.Add;
        }
        public frmAddUpdateLocalDrivingLicesnseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _Mode = enTypeMode.Update;
        }
        private void _LoadLicenseClass()
        {
            DataTable dt = clsLicenseClass.GetAllLicenseClass();
            foreach (DataRow row in dt.Rows)
                cbLicenseClass.Items.Add(row["ClassName"]);
        }

        private void _RestDefaultValues()
        {
            _LoadLicenseClass();
            if (_Mode == enTypeMode.Add)
            {

                this.Text = "New Local Driving License Application";
                _clsLDLA = new clsLocalDrivingLicenseAppliction();
                ctrlPersonCardWithFilter1.FilterFocus();
                tpApplicationInfo.Enabled = false;

                cbLicenseClass.SelectedIndex = 2;
                lblApplicationFees.Text = clsApplicationType.Found((int)clsApplication.enApplicationType.NewLocalDrivingLicense).ApplicationFees.ToString();
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                lblCreateBy.Text = clsGlobal.CurrentUser.UserName;
            }
            else
            {
                this.Text = "Update Local Driving License Application";

                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
            }
        }
        private void _LoadDataInfo()
        {
            _clsLDLA = clsLocalDrivingLicenseAppliction.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);
            if(_clsLDLA == null)
            {
                MessageBox.Show("This id local driving not found !");
                return;
            }
            _LoadLicenseClass();
            this.Text = "Update Local Driving License Application";
            tpApplicationInfo.Enabled = true;
            lblDLApplicationID.Text = _clsLDLA.ApplicationID.ToString();
            lblApplicationDate.Text = _clsLDLA.ApplicationDate.ToString();
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(clsLicenseClass.Find(_clsLDLA.LicenseClassID).ClassName);
            lblApplicationFees.Text = _clsLDLA.PaidFees.ToString();
            lblCreateBy.Text = _clsLDLA.CreatedByUserID.ToString();
            ctrlPersonCardWithFilter1.LoadPersonInfo(_clsLDLA.ApplicantPersonID);
            ctrlPersonCardWithFilter1.FilterEnable = false;
            btnSave.Enabled = true;
        }
        private void DataBackEvent(object sender, int PersonID)
        {
            _SelectedPersonID = PersonID;
            ctrlPersonCardWithFilter1.LoadPersonInfo(PersonID);
        }
        private void frmAddUpdateLocalDrivingLicesnseApplication_Load(object sender, EventArgs e)
        {
            _RestDefaultValues();
            if (_Mode == enTypeMode.Update)
                _LoadDataInfo();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(_Mode == enTypeMode.Update)
            {
                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
                tcLocalDriving.SelectedTab = tcLocalDriving.TabPages["tpApplicationInfo"];
                return;
            }
            else
            {
                if (ctrlPersonCardWithFilter1.PersonID != -1)
                {
                    
                        tpApplicationInfo.Enabled = true;
                        btnSave.Enabled = true;
                        tcLocalDriving.SelectedTab = tcLocalDriving.TabPages["tpApplicationInfo"];
                }
                else
                {
                    MessageBox.Show("Please selecte person!");
                    ctrlPersonCardWithFilter1.FilterFocus();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int LicenseClassID = clsLicenseClass.Find(cbLicenseClass.Text).LicenseClassID;
            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_SelectedPersonID, clsApplication.enApplicationType.NewLocalDrivingLicense, LicenseClassID);
            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;
            }

            //check if user already have issued license of the same driving  class.
            if (clsLicense.IsLicenseExistByPersonID(ctrlPersonCardWithFilter1.PersonID, LicenseClassID))
            {

                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _clsLDLA.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            _clsLDLA.ApplicationDate = DateTime.Now;
            _clsLDLA.ApplicationTypeID = 1;
            _clsLDLA.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _clsLDLA.LastStatusDate = DateTime.Now;
            _clsLDLA.PaidFees = Convert.ToSingle(lblApplicationFees.Text);
            _clsLDLA.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _clsLDLA.LicenseClassID = LicenseClassID;
            if (_clsLDLA.Save())
            {
                lblDLApplicationID.Text = _clsLDLA.LocalDrivingLicenseApplicationID.ToString();
                _Mode = enTypeMode.Update;
                this.Text = "Update Local Driving License Application";
                MessageBox.Show("Done Save.");
            }
            else
                MessageBox.Show("Error not save local driving license!!!");
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _SelectedPersonID = obj;
        }

        private void frmAddUpdateLocalDrivingLicesnseApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }

    }
}
