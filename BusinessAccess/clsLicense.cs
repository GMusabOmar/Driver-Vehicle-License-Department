using System;
using System.Data;
using DataAccess;
using static BusinessAccess.clsLicense;

namespace BusinessAccess
{
    public class clsLicense
    {
        public enum enTypeMode { Add =  1, Update = 2}
        private enTypeMode _Mode = enTypeMode.Add;
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClass { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enum enIssueReason { FirstTime = 1, Renew = 2, Replacement_for_Damaged = 3, Replacement_for_Lost = 4 }
        public enIssueReason IssueReason { get; set; }
        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }
        public int CreatedByUserID { get; set; }

        public clsDriver DriverInfo;
        public clsLicenseClass LicenseClassInfo;
        public clsDetainedLicense DetainedLicenseInfo { get; set; }
        public clsLicense()
        {
            this.LicenseID = 0;
            this.ApplicationID = 0;
            this.DriverID = 0;
            this.LicenseClass = 0;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = true;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = 0;
            _Mode = enTypeMode.Add;
        }
        public clsLicense(int LicenseID, int ApplicationID, int DriverID,
            int LicenseClass, DateTime IssueDate, DateTime ExpirationDate,
            string Notes, decimal PaidFees, bool IsActive, enIssueReason IssueReason,
            int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
            _Mode = enTypeMode.Update;
            DriverInfo = clsDriver.GetDriverInfoByID(this.DriverID);
            LicenseClassInfo = clsLicenseClass.Find(this.LicenseClass);
            DetainedLicenseInfo = clsDetainedLicense.FindLicenseID(this.LicenseID);
        }
        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = 0, DriverID = 0, LicenseClass = 0, CreatedByUserID = 0;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = 0;
            bool IsActive = false;
            byte issueReason = 0;
            bool isFound = clsLicenseData.GetLicenseInfoByID(LicenseID, ref ApplicationID,
                    ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate,
                    ref Notes, ref PaidFees, ref IsActive, ref issueReason, ref CreatedByUserID);
            if (isFound)
                return new clsLicense(LicenseID, ApplicationID, DriverID,
                    LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive,
                    (enIssueReason)issueReason, CreatedByUserID);
            return null;
        }
        public static DataTable GetAllLicense()
        {
            return clsLicenseData.GetAllLicense();
        }
        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseData.AddNewLicense(ApplicationID, DriverID,
                LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees,
                IsActive, (byte)IssueReason, CreatedByUserID);
            return this.LicenseID > 0;
        }
        private bool _UpdateLicense()
        {
            return clsLicenseData.UpdateLicense(LicenseID, ApplicationID, DriverID,
            LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, 
            (byte)IssueReason, CreatedByUserID);
        }
        public bool Save()
        {
            switch(_Mode)
            {
                case enTypeMode.Add:
                    if(_AddNewLicense())
                    {
                        _Mode = enTypeMode.Update;
                        return true;
                    }
                    else
                        return false;
                default:
                    return _UpdateLicense();
            }
        }
        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return (GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1);
        }
        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {

            return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);

        }
        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseData.GetDriverLicenses(DriverID);
        }
        public Boolean IsLicenseExpired()
        {

            return (this.ExpirationDate < DateTime.Now);

        }
        public bool DeactivateCurrentLicense()
        {
            return (clsLicenseData.DeactivateLicense(this.LicenseID));
        }
        public static string GetIssueReasonText(enIssueReason IssueReason)
        {

            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.Replacement_for_Damaged:
                    return "Replacement for Damaged";
                case enIssueReason.Replacement_for_Lost:
                    return "Replacement for Lost";
                default:
                    return "First Time";
            }
        }
        public clsLicense RenewLicense(string Note, int UserID)
        {
            clsApplication _Application = new clsApplication();
            _Application.ApplicantPersonID = DriverInfo.PersonID;
            _Application.ApplicationDate = DateTime.Now;
            _Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            _Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            _Application.LastStatusDate = DateTime.Now;
            _Application.PaidFees = clsApplicationType.Found((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees;
            _Application.CreatedByUserID = UserID;
            if(!_Application.Save())
            {
                return null;
            }
            clsLicense _NewLicene = new clsLicense();
            _NewLicene.ApplicationID = _Application.ApplicationID;
            _NewLicene.DriverID = this.DriverID;
            _NewLicene.LicenseClass = this.LicenseClass;
            _NewLicene.IssueDate = DateTime.Now;
            _NewLicene.ExpirationDate = DateTime.Now.AddYears(LicenseClassInfo.DefaultValidityLength);
            _NewLicene.Notes = Notes;
            _NewLicene.PaidFees = this.PaidFees;
            _NewLicene.IsActive = true;
            _NewLicene.IssueReason = clsLicense.enIssueReason.Renew;
            _NewLicene.CreatedByUserID = UserID;
            if (!_NewLicene.Save())
                return null;
            DeactivateCurrentLicense();
            return _NewLicene;
        }
        public clsLicense ReplaceDamageOrLostLicense(clsLicense.enIssueReason Reason, int UserID)
        {
            clsApplication _Application = new clsApplication();
            _Application.ApplicantPersonID = DriverInfo.PersonID;
            _Application.ApplicationDate = DateTime.Now;
            _Application.ApplicationTypeID = (Reason == enIssueReason.Replacement_for_Damaged) ?
                (int)clsApplication.enApplicationType.ReplacementforaDamagedDrivingLicense :
                (int)clsApplication.enApplicationType.ReplacementforaLostDrivingLicense;
            _Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            _Application.LastStatusDate = DateTime.Now;
            _Application.PaidFees = clsApplicationType.Found((int)Reason).ApplicationFees;
            _Application.CreatedByUserID = UserID;
            if (!_Application.Save())
            {
                return null;
            }
            clsLicense _NewLicene = new clsLicense();
            _NewLicene.ApplicationID = _Application.ApplicationID;
            _NewLicene.DriverID = this.DriverID;
            _NewLicene.LicenseClass = this.LicenseClass;
            _NewLicene.IssueDate = DateTime.Now;
            _NewLicene.ExpirationDate = DateTime.Now.AddYears(LicenseClassInfo.DefaultValidityLength);
            _NewLicene.Notes = Notes;
            _NewLicene.PaidFees = this.PaidFees;
            _NewLicene.IsActive = true;
            _NewLicene.IssueReason = Reason;
            _NewLicene.CreatedByUserID = UserID;
            if (!_NewLicene.Save())
                return null;
            DeactivateCurrentLicense();
            return _NewLicene;
        }
        public int Detain(float fees, int UserID)
        {
            clsDetainedLicense _Detain = new clsDetainedLicense();
            _Detain.LicenseID = this.LicenseID;
            _Detain.DetainDate = DateTime.Now;
            _Detain.FineFees = fees;
            _Detain.CreatedByUserID = UserID;
            if(!_Detain.Save())
            {
                return -1;
            }
            return _Detain.DetainID;
        }
        public bool IsDetain()
        {
            return clsDetainedLicense.IsLicenseDetained(this.LicenseID);
        }
        public bool IsReleaseLisense(int UserID, ref int ApplicationID)
        {
            clsApplication _Application = new clsApplication();
            _Application.ApplicantPersonID = DriverInfo.PersonID;
            _Application.ApplicationDate = DateTime.Now;
            _Application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            _Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            _Application.LastStatusDate = DateTime.Now;
            _Application.PaidFees = clsApplicationType.Found(_Application.ApplicationTypeID).ApplicationFees;
            _Application.CreatedByUserID = UserID;
            if(!_Application.Save())
            {
                ApplicationID = -1;
                return false;
            }
            ApplicationID = _Application.ApplicationID;
            return DetainedLicenseInfo.ReleaseDetainedLicense(DetainedLicenseInfo.DetainID, _Application.ApplicationID);
        }
    }
}
