using System;
using System.Data;
using DataAccess;

namespace BusinessAccess
{
    public class clsLicenseClass
    {
        public enum enTypeMode { Add = 1, Update = 2 }
        private enTypeMode _Mode = enTypeMode.Add;
        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public decimal ClassFees { get; set; }
        public clsLicenseClass()
        {
            this.LicenseClassID = 0;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 0;
            this.DefaultValidityLength = 0;
            this.ClassFees = 0;
            _Mode = enTypeMode.Add;
        }
        public clsLicenseClass(int LicenseClassID, string ClassName,
            string ClassDescription, byte MinimumAllowedAge, byte DefaultValidityLength,
            decimal ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
            _Mode = enTypeMode.Update;
        }
        public static clsLicenseClass Find(int LicenseClassID)
        {
            string ClassName = "", ClassDescription = "";
            byte MinimumAllowedAge = 0, DefaultValidityLength = 0;
            decimal ClassFees = 0;
            bool isFound = clsLicenseClassData.GetLicenseClassByID(LicenseClassID, ref ClassName,
            ref ClassDescription, ref MinimumAllowedAge,
            ref DefaultValidityLength, ref ClassFees);
            if (isFound)
                return new clsLicenseClass(LicenseClassID, ClassName,
            ClassDescription, MinimumAllowedAge, DefaultValidityLength,
            ClassFees);
            else
                return null;
        }
        public static clsLicenseClass Find(string ClassName)
        {
            int LicenseClassID = 0;
            string ClassDescription = "";
            byte MinimumAllowedAge = 0, DefaultValidityLength = 0;
            decimal ClassFees = 0;
            bool isFound = clsLicenseClassData.GetLicenseClassByClassName(ClassName, 
                ref LicenseClassID, ref ClassDescription, ref MinimumAllowedAge,
                ref DefaultValidityLength, ref ClassFees);
            if (isFound)
                return new clsLicenseClass(LicenseClassID, ClassName,
            ClassDescription, MinimumAllowedAge, DefaultValidityLength,
            ClassFees);
            else
                return null;
        }
        public static DataTable GetAllLicenseClass()
        {
            return clsLicenseClassData.GetAllLicenseClass();
        }
        private bool _AddNewLicenseClass()
        {
            this.LicenseClassID = clsLicenseClassData.AddNewLicenseClass(ClassName,
            ClassDescription, MinimumAllowedAge,
            DefaultValidityLength, ClassFees);
            return this.LicenseClassID > 0;
        }
        private bool _UpdateLicenseClass()
        {
            return clsLicenseClassData.UpdateLicenseClass(LicenseClassID, ClassName,
                    ClassDescription, MinimumAllowedAge, DefaultValidityLength,
                    ClassFees);
        }
        public bool Save()
        {
            switch(_Mode)
            {
                case enTypeMode.Add:
                    if(_AddNewLicenseClass())
                    {
                        _Mode = enTypeMode.Update;
                        return true;
                    }
                    else
                        return false;
                default:
                    return _UpdateLicenseClass();
            }
        }
    }
}
