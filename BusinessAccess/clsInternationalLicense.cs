using System;
using System.Data;
using DataAccess;

namespace BusinessAccess
{
    public class clsInternationalLicense : clsApplication
    {
        public enum enMode { Add = 0, Update = 1 };
        private enMode _Mode = enMode.Add;
        public int InternationalLicenseID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public clsDriver DriverInfo { get; set; }
        public clsInternationalLicense()
        {
            this.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            this.InternationalLicenseID = 0;
            this.DriverID = 0;
            this.IssuedUsingLocalLicenseID = 0;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = true;
            _Mode = enMode.Add;
        }
        public clsInternationalLicense(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID, enApplicationStatus ApplicationStatus,
            DateTime LastStatusDate, float PaidFees, int CreatedByUserID,
            int InternationalLicenseID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive)
        {
            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID = ApplicationTypeID;
            base.ApplicationStatus = ApplicationStatus;
            base.LastStatusDate = LastStatusDate;
            base.PaidFees = PaidFees;
            base.CreatedByUserID = CreatedByUserID;

            this.InternationalLicenseID = InternationalLicenseID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            _Mode = enMode.Update;
            DriverInfo = clsDriver.GetDriverInfoByID(this.DriverID);
        }
        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
            int DriverID = 0, IssuedUsingLocalLicenseID = 0, ApplicationID = 0, CreatedByUserID = 0;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            bool IsActive = false;
            bool isFound = clsInternationalLicenseData.GetInternationalLicenseByID(
                InternationalLicenseID, ref ApplicationID, ref DriverID, 
                ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive,
                 ref CreatedByUserID);
            if (isFound)
            {
                clsApplication _Application = clsApplication.FindBaseApplication(ApplicationID);
                return new clsInternationalLicense(ApplicationID, _Application.ApplicantPersonID,
                            _Application.ApplicationDate, _Application.ApplicationTypeID,
                            _Application.ApplicationStatus,
                            _Application.LastStatusDate, _Application.PaidFees, CreatedByUserID,
                            InternationalLicenseID, DriverID, IssuedUsingLocalLicenseID,
                            IssueDate, ExpirationDate, IsActive);
            }
            else
                return null;
        }
        public static clsInternationalLicense FindByDriverID(int DriverID)
        {
            int InternationalLicenseID = 0, IssuedUsingLocalLicenseID = 0, ApplicationID = 0, CreatedByUserID = 0;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            bool IsActive = false;
            bool isFound = clsInternationalLicenseData.GetInternationalLicenseByDriverID(
                DriverID, ref InternationalLicenseID, ref ApplicationID, 
                ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, 
                ref IsActive, ref CreatedByUserID);
            if (isFound)
            {
                clsApplication _Application = clsApplication.FindBaseApplication(ApplicationID);
                return new clsInternationalLicense(ApplicationID, _Application.ApplicantPersonID,
                            _Application.ApplicationDate, _Application.ApplicationTypeID,
                            _Application.ApplicationStatus,
                            _Application.LastStatusDate, _Application.PaidFees, CreatedByUserID,
                            InternationalLicenseID, DriverID, IssuedUsingLocalLicenseID,
                            IssueDate, ExpirationDate, IsActive);
            }
            else
                return null;
        }

        public static DataTable GetAllInternationalLicense()
        {
            return clsInternationalLicenseData.GetAllInternationalLicense();
        }
        public static DataTable GetAllInternationalLicenseByDriverID(int DriverID)
        {
            return clsInternationalLicenseData.GetAllInternationalLicenseByDriverID(DriverID);
        }
        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicenseData.AddNewInternationalLicense( ApplicationID,  DriverID,
             IssuedUsingLocalLicenseID,  IssueDate,  ExpirationDate,
             IsActive,  CreatedByUserID);
            return this.InternationalLicenseID > 0;
        }
        private bool _UpdateAddNewInternationalLicense()
        {
            return clsInternationalLicenseData.UpdateInternationalLicense(InternationalLicenseID,
             ApplicationID,  DriverID,  IssuedUsingLocalLicenseID,
             IssueDate,  ExpirationDate,  IsActive,  CreatedByUserID);
        }
        public bool Save()
        {
            base.Mode = (clsApplication.enTypeMode)_Mode;
            if (!base.Save())
                return false;
            switch(_Mode)
            {
                case enMode.Add:
                    if(_AddNewInternationalLicense())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                default:
                    return _UpdateAddNewInternationalLicense();
            }
        }
        public static bool IsInternationalLicenseExists(int DriverID)
        {
            return clsInternationalLicenseData.IsInternationalLicenseExists(DriverID);
        }
    }
}
