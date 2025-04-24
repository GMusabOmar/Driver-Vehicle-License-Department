using System;
using System.Data;
using DataAccess;

namespace BusinessAccess
{
    public class clsDetainedLicense
    {
        public enum enTypeMode { Add = 0, Update = 1};
        private enTypeMode _Mode = enTypeMode.Add;
        public int DetainID {  get; set; }
        public int LicenseID {  get; set; }
        public DateTime DetainDate {  get; set; }
        public float FineFees {  get; set; }
        public int CreatedByUserID {  get; set; }
        public clsUser CreatedByUserInfo { set; get; }
        public bool IsReleased {  get; set; }
        public DateTime ReleaseDate {  get; set; }
        public int ReleasedByUserID {  get; set; }
        public clsUser ReleasedByUserInfo { set; get; }
        public int ReleaseApplicationID {  get; set; }

        public clsUser UserInfo { get; set; }
        public clsDetainedLicense()
        {
            this.DetainID = 0;
            this.LicenseID = 0;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = 0;
            this.IsReleased = false;
            this.ReleaseDate = DateTime.Now;
            this.ReleasedByUserID = 0;
            this.ReleaseApplicationID = 0;
            this._Mode = enTypeMode.Add;
        }
        public clsDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate,
            float FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate,
            int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.GetUserByUserID(this.CreatedByUserID);
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
            this.ReleasedByUserInfo = clsUser.GetUserByUserID(this.ReleasedByUserID);
            this._Mode = enTypeMode.Update;
        }
        public static clsDetainedLicense Find(int DetainID)
        {
            int LicenseID = 0, CreatedByUserID = 0, ReleasedByUserID = 0,
                ReleaseApplicationID = 0;
            DateTime DetainDate = DateTime.Now, ReleaseDate = DateTime.Now;
            float FineFees = 0;
            bool IsReleased = false;
            bool isFound = clsDetainedLicenseData.FindByID(DetainID, ref LicenseID, ref DetainDate,
            ref FineFees, ref CreatedByUserID, ref IsReleased,
            ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID);
            if(isFound)
            {
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate,
                            FineFees, CreatedByUserID, IsReleased, ReleaseDate,
                            ReleasedByUserID, ReleaseApplicationID);
            }
            else
                return null;
        }
        public static clsDetainedLicense FindLicenseID(int LicenseID)
        {
            int DetainID = 0, CreatedByUserID = 0, ReleasedByUserID = 0,
                ReleaseApplicationID = 0;
            DateTime DetainDate = DateTime.Now, ReleaseDate = DateTime.Now;
            float FineFees = 0;
            bool IsReleased = false;
            bool isFound = clsDetainedLicenseData.FindByLicenseID(LicenseID, ref DetainID, ref DetainDate,
            ref FineFees, ref CreatedByUserID, ref IsReleased,
            ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID);
            if (isFound)
            {
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate,
                            FineFees, CreatedByUserID, IsReleased, ReleaseDate,
                            ReleasedByUserID, ReleaseApplicationID);
            }
            else
                return null;
        }
        public static DataTable GetAllDetainedLicense()
        {
            return clsDetainedLicenseData.GetAllDetainedLicense();
        }
        private bool _AddNewDetainedLicense()
        {
            this.DetainID = clsDetainedLicenseData.AddNewDetainedLicense(this.LicenseID, 
                    this.DetainDate,this.FineFees, this.CreatedByUserID);
            return this.DetainID > 0;
        }
        private bool _UpdateDetainedLicense()
        {
            return clsDetainedLicenseData.UpdateDetainedLicense(this.DetainID, this.LicenseID,
            this.DetainDate, this.FineFees, this.CreatedByUserID);
        }
        public bool Save()
        {
            switch(_Mode)
            {
                case enTypeMode.Add:
                    if(_AddNewDetainedLicense())
                    {
                        _Mode = enTypeMode.Update;
                        return true;
                    }
                    else
                        return false;
                default:
                    return _UpdateDetainedLicense();
            }
        }
        public bool ReleaseDetainedLicense(int DetainID, int ApplicationID)
        {
            return clsDetainedLicenseData.ReleaseDetainedLicense(DetainID, this.ReleasedByUserID, ApplicationID);
        }
        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicenseData.IsLicenseDetained(LicenseID);
        }
    }
}
