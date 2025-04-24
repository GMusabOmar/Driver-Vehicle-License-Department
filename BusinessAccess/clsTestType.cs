using System;
using DataAccess;
using System.Data;

namespace BusinessAccess
{
    public class clsTestType
    {
        public enum enTypeMode { Add = 0, Update = 1};
        private enTypeMode _Mode = enTypeMode.Add;
        public enum enTypeID { Vision = 1, Written = 2, Practical = 3};
        public enTypeID TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }
        public clsTestType()
        {
            this.TestTypeID = enTypeID.Vision;
            this.TestTypeTitle = "";
            this.TestTypeDescription = "";
            this.TestTypeFees = -1;
            _Mode = enTypeMode.Add;
        }
        public clsTestType(enTypeID TestTypeID, string TestTypeTitle,
            string TestTypeDescription, decimal TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
            _Mode = enTypeMode.Update;
        }
        public static clsTestType Found(enTypeID TestTypeID)
        {
            string TestTypeTitle = "", TestTypeDescription = "";
            decimal TestTypeFees = -1;
            bool isFound = clsTestTypeData.Found((int)TestTypeID,
                ref TestTypeTitle, ref TestTypeDescription, ref TestTypeFees);
            if (isFound)
                return new clsTestType(TestTypeID, TestTypeTitle,
                        TestTypeDescription, TestTypeFees);
            else
                return null;
        }
        private bool _AddNewTest()
        {
            this.TestTypeID = (enTypeID)clsTestTypeData.AddNewTest(this.TestTypeTitle,
                this.TestTypeDescription, this.TestTypeFees);
            return this.TestTypeID > 0;
        }
        private bool _UpdateTest()
        {
            return clsTestTypeData.Update((int)this.TestTypeID,
                this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
        }
        public bool Save()
        {
            switch(_Mode)
            {
                case enTypeMode.Add:
                    if (_AddNewTest())
                    {
                        _Mode = enTypeMode.Update;
                        return true;
                    }
                    else
                        return false;
                default:
                    return _UpdateTest();
            }
        }
        public static DataTable GetAllTest()
        {
            return clsTestTypeData.GetAllTest();
        }

    }
}
