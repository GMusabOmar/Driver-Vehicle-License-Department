using System;
using System.Data;
using DataAccess;

namespace BusinessAccess
{
    public class clsApplication
    {
        public enum enTypeMode { Add = 0, Update = 1}
        public enTypeMode Mode = enTypeMode.Add;
        public enum enApplicationType { NewLocalDrivingLicense = 1,
            RenewDrivingLicense = 2, ReplacementforaLostDrivingLicense = 3,
            ReplacementforaDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5,
            NewInternationalLicense = 6, RetakeTest = 7
        }
        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3}
        public enApplicationStatus ApplicationStatus { get; set; }
        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public clsPerson PersonInfo { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public clsApplicationType ApplicationTypeInfo;
        public string StatusString
        {
            get
            {
                switch(ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    default:
                        return "Completed";
                }
            }
        }
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInof;
        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            Mode = enTypeMode.Add;
        }
        public clsApplication(int ApplicationID, int ApplicantPersonID, 
            DateTime ApplicationDate, int ApplicationTypeID, enApplicationStatus ApplicationStatus,
            DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.PersonInfo = clsPerson.FindPersonByPersonID(this.ApplicantPersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.Found(this.ApplicationTypeID);
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            CreatedByUserInof = clsUser.GetUserByUserID(this.CreatedByUserID);
            Mode = enTypeMode.Update;
        }
        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            int ApplicantPersonID = -1, ApplicationTypeID = -1, CreatedByUserID = -1;
            byte ApplicationStatus = 0;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            float PaidFees = -1;
            bool isFound = clsApplicationData.GetApplicationInfoByID(ApplicationID,
                ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID,
                ref ApplicationStatus, ref LastStatusDate, ref PaidFees,
                 ref CreatedByUserID);
            if (isFound)
            {
                return new clsApplication(ApplicationID, ApplicantPersonID,
                        ApplicationDate, ApplicationTypeID, (enApplicationStatus)ApplicationStatus,
                        LastStatusDate, PaidFees, CreatedByUserID);
            }
            else
                return null;
        }
        public static DataTable GetAllApplications()
        {
            return clsApplicationData.GetAllApplications();
        }
        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(ApplicantPersonID,
                ApplicationDate, ApplicationTypeID, (byte)ApplicationStatus, LastStatusDate,
                PaidFees, CreatedByUserID);
            return this.ApplicationID > 0;
        }
        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(ApplicationID, ApplicantPersonID,
            ApplicationDate, ApplicationTypeID, (byte)ApplicationStatus,
            LastStatusDate, PaidFees, CreatedByUserID);
        }
        public bool Save()
        {
            switch(Mode)
            {
                case enTypeMode.Add:
                    if (_AddNewApplication())
                    {
                        Mode = enTypeMode.Update;
                        return true;
                    }
                    else
                        return false;
                default:
                    return _UpdateApplication();
            }
        }
        public bool DeleteApplication()
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }
        public static bool IsApplicationExists(int ApplicationID)
        {
            return clsApplicationData.IsApplicationExists(ApplicationID);
        }
        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, (byte)enApplicationStatus.Cancelled);
        }
        public bool SetComplete()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, (int)enApplicationStatus.Completed);
        }
        public static int GetActiveApplicationID(int ApplicantPersonID, enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(ApplicantPersonID, (int)ApplicationTypeID);
        }
        public static bool DoesPersonHaveActiveApplication(int ApplicantPersonID, int ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(ApplicantPersonID, ApplicationTypeID);
        }
        public static int GetActiveApplicationIDForLicenseClass(int ApplicantPersonID, enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(ApplicantPersonID, (int)ApplicationTypeID, LicenseClassID);
        }

        public int GetActiveApplicationID(clsApplication.enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicantPersonID, ApplicationTypeID);
        }
        public bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return DoesPersonHaveActiveApplication(this.ApplicantPersonID, ApplicationTypeID);
        }


    }
}
