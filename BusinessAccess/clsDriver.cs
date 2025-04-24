using System;
using System.Data;
using DataAccess;

namespace BusinessAccess
{
    public class clsDriver
    {
        public enum enTypeMode { Add = 1, Update = 2}
        private enTypeMode _Mode = enTypeMode.Add;
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public clsPerson PersonInfo;
        public clsDriver()
        {
            this.DriverID = 0;
            this.PersonID = 0;
            this.CreatedByUserID = 0;
            this.CreatedDate = DateTime.Now;
            _Mode = enTypeMode.Add;
        }
        public clsDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            _Mode = enTypeMode.Update;
            this.PersonInfo = clsPerson.FindPersonByPersonID(this.PersonID);
        }
        public static clsDriver GetDriverInfoByID(int DriverID)
        {
            int PersonID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;
            bool isFound = clsDriverData.GetDriverInfoByID(DriverID, ref PersonID,
                            ref CreatedByUserID, ref CreatedDate);
            if (isFound)
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            return null;
        }
        public static clsDriver GetDriverInfoByPersonID(int PersonID)
        {
            int DriverID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;
            bool isFound = clsDriverData.GetDriverInfoByPersonID(PersonID, ref DriverID,
                            ref CreatedByUserID, ref CreatedDate);
            if (isFound)
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            return null;
        }
        public static DataTable GetAllDriver()
        {
            return clsDriverData.GetAllDriver();
        }
        private bool _AddNewDriver()
        {
            this.DriverID = clsDriverData.AddNewDriver(PersonID, CreatedByUserID, CreatedDate);
            return this.DriverID > 0;
        }
        private bool _UpdateDriver()
        {
            return clsDriverData.UpdateDriver(DriverID,PersonID,CreatedByUserID, CreatedDate);
        }
        public bool Save()
        {
            switch(_Mode)
            {
                case enTypeMode.Add:
                    if (_AddNewDriver())
                    {
                        _Mode = enTypeMode.Update;
                        return true;
                    }
                    else
                        return false;
                default:
                    return _UpdateDriver();
            }
        }
        public static DataTable GetLicenses(int DriverID)
        {
            return clsLicense.GetDriverLicenses(DriverID);
        }
    }
}
