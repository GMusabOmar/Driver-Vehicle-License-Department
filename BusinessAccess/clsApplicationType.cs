using System;
using System.Data;
using DataAccess;

namespace BusinessAccess
{
    public class clsApplicationType
    {
        public enum enTypeMode { Add = 0, Update = 1 };
        private enTypeMode _Mode = enTypeMode.Add;
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public float ApplicationFees { get; set; }
        public clsApplicationType()
        {
            this.ApplicationTypeID = -1;
            this.ApplicationTypeTitle = "";
            this.ApplicationFees = -1;
            _Mode = enTypeMode.Add;
        }
        public clsApplicationType(int ApplicationTypeID, string ApplicationTypeTitle,
            float ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
            _Mode = enTypeMode.Update;
        }

        public static clsApplicationType Found(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = "";
            float ApplicationFees = -1;
            bool isFound = clsApplicationTypeData.Find(ApplicationTypeID,
                ref ApplicationTypeTitle, ref ApplicationFees);
            if (isFound)
            {
                return new clsApplicationType(ApplicationTypeID,
                    ApplicationTypeTitle, ApplicationFees);
            }
            else
                return null;
        }

        public static DataTable GetAllApplication()
        {
            return clsApplicationTypeData.GetAllApplication();
        }

        private bool _Add()
        {
            this.ApplicationTypeID = clsApplicationTypeData.AddNewApplication(ApplicationTypeTitle,
                ApplicationFees);
            return this.ApplicationTypeID > 0;
        }
        private bool _Update()
        {
            return clsApplicationTypeData.UpdateApplication(ApplicationTypeID, ApplicationTypeTitle,
                ApplicationFees);
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enTypeMode.Add:
                    if (_Add())
                    {
                        _Mode = enTypeMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enTypeMode.Update:
                    return _Update();
            }
            return false;
        }

    }
}
