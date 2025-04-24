using System;
using System.Data;
using DataAccess;

namespace BusinessAccess
{
    public class clsLocalDrivingLicenseAppliction : clsApplication
    {
        public enum enModeType { Add = 0, Update = 1}
        private enModeType _Mode = enModeType.Add;
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public string PersonFullName { get; set; }
        public clsLicenseClass LicenseClassInfo;
        public clsLocalDrivingLicenseAppliction()
        {
            this._Mode = enModeType.Add;
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;
        }
        public clsLocalDrivingLicenseAppliction(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID, enApplicationStatus ApplicationStatus,
            DateTime LastStatusDate, float PaidFees, int CreatedByUserID,
            int LocalDrivingLicenseApplicationID, int LicenseClassID)
        {
            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID = ApplicationTypeID;
            base.ApplicationStatus = ApplicationStatus;
            base.LastStatusDate = LastStatusDate;
            base.PaidFees = PaidFees;
            base.CreatedByUserID = CreatedByUserID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassID = LicenseClassID;
            this._Mode = enModeType.Update;
            this.PersonFullName = clsPerson.FindPersonByPersonID(this.ApplicantPersonID).Name;
            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
        }
        public static clsLocalDrivingLicenseAppliction FindByLocalDrivingAppLicenseID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;
            bool isFound = clsLocalDrivingLicenseApplictionData.FoundByID(LocalDrivingLicenseApplicationID,
                ref ApplicationID, ref LicenseClassID);
            if(isFound)
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);
                return new clsLocalDrivingLicenseAppliction(ApplicationID,
                    Application.ApplicantPersonID, Application.ApplicationDate,
                    Application.ApplicationTypeID, Application.ApplicationStatus,
                    Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID,
                    LocalDrivingLicenseApplicationID, LicenseClassID);
            }
            else
                return null;
        }

        public static clsLocalDrivingLicenseAppliction FoundByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;
            bool isFound = clsLocalDrivingLicenseApplictionData.FoundByApplicationID(ApplicationID,
                ref LocalDrivingLicenseApplicationID, ref LicenseClassID);
            if (isFound)
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);
                return new clsLocalDrivingLicenseAppliction(ApplicationID,
                    Application.ApplicantPersonID, Application.ApplicationDate,
                    Application.ApplicationTypeID, Application.ApplicationStatus,
                    Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID,
                    LocalDrivingLicenseApplicationID, LicenseClassID);
            }
            else
                return null;
        }

        public static DataTable GetAllDrivingLicense()
        {
            return clsLocalDrivingLicenseApplictionData.GetAllDrivingLicense();
        }

        private bool _AddNewLocalDrivingLicense()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplictionData.AddNewLocalDrivingLicense(this.ApplicationID, this.LicenseClassID);
            return this.LocalDrivingLicenseApplicationID > 0;
        }

        private bool _UpdateLocalDrivingLicense()
        {
            return clsLocalDrivingLicenseApplictionData.UpdateLocalDrivingLicense(this.LocalDrivingLicenseApplicationID,
                this.ApplicationID, this.LicenseClassID);
        }

        public bool Save()
        {
            base.Mode = (clsApplication.enTypeMode)_Mode;
            if (!base.Save())
                return false;
            switch(this._Mode)
            {
                case enModeType.Add:
                    if (_AddNewLocalDrivingLicense())
                    {
                        _Mode = enModeType.Update;
                        return true;
                    }
                    else
                        return false;
                default:
                    return _UpdateLocalDrivingLicense();
            }
        }

        public bool DeleteLocalDrivingLicense()
        {
            // delete subClass then SuperCalss.
            bool IsDeleteLocalDrivingLicense = clsLocalDrivingLicenseApplictionData.DeleteLocalDrivingLicense(this.LocalDrivingLicenseApplicationID);
            if (!IsDeleteLocalDrivingLicense)
                return false;
            return base.DeleteApplication();
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsLocalDrivingLicenseApplictionData.DoesPassTestType(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public bool DoesPassTestType(clsTestType.enTypeID TestTypeID)
        {
            return clsLocalDrivingLicenseApplictionData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesPassPreviousTest(clsTestType.enTypeID TestTypeID)
        {
            switch(TestTypeID)
            {
                case clsTestType.enTypeID.Vision:
                    return true;
                case clsTestType.enTypeID.Written:
                    return DoesPassTestType(clsTestType.enTypeID.Vision);
                case clsTestType.enTypeID.Practical:
                    return DoesPassTestType(clsTestType.enTypeID.Written);
                default:
                    return false;
            }
        }

        public static bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsLocalDrivingLicenseApplictionData.DoesAttendTestType(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public bool DoesAttendTestType(clsTestType.enTypeID TestTypeID)
        {
            return clsLocalDrivingLicenseApplictionData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsLocalDrivingLicenseApplictionData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, TestTypeID);
        }
        
        public byte TotalTrialsPerTest(clsTestType.enTypeID TestTypeID)
        {
            return clsLocalDrivingLicenseApplictionData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsLocalDrivingLicenseApplictionData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public bool IsThereAnActiveScheduledTest(clsTestType.enTypeID TestTypeID)
        {
            return clsLocalDrivingLicenseApplictionData.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }
        public byte GetPassedTestCount()
        {
            return clsTest.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }
        public bool PassedAllTests()
        {
            return clsTest.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }
        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.PassedAllTests(LocalDrivingLicenseApplicationID);
        }
        public clsTest GetLastTestPerTestType(clsTestType.enTypeID TestTypeID)
        {
            return clsTest.GetLastTestByPersonAndLicenseClassAndTestAppointmentID(this.ApplicantPersonID, this.LicenseClassID, (int)TestTypeID);
        }
        public int IssueDrivingLicenseForFirstTime(string Note, int UserID)
        {
            int DriverID = -1;
            clsDriver Driver = clsDriver.GetDriverInfoByPersonID(this.ApplicantPersonID);
            if(Driver == null)
            {
                Driver = new clsDriver();
                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = UserID;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                    DriverID = -1;
            }
            else
                DriverID = Driver.DriverID;
            clsLicense _License = new clsLicense();
            _License.ApplicationID = this.ApplicationID;
            _License.DriverID = DriverID;
            _License.LicenseClass = LicenseClassID;
            _License.IssueDate = DateTime.Now;
            _License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            _License.Notes = Note;
            _License.PaidFees = this.LicenseClassInfo.ClassFees;
            _License.IsActive = true;
            _License.IssueReason = clsLicense.enIssueReason.FirstTime;
            _License.CreatedByUserID = UserID;
            if (_License.Save())
            {
                this.SetComplete();
                return _License.LicenseID;
            }
          return -1;
        }
        public int GetActiveLicenseID()
        {
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }
        public bool IsLicenseIssued()
        {
            return GetActiveLicenseID() != -1;
        }
    }
}
