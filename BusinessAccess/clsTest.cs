using System;
using System.Data;
using DataAccess;

namespace BusinessAccess
{
    public class clsTest
    {
        public enum enTypeMode { Add = 1, Update = 2}
        private enTypeMode _Mode = enTypeMode.Add;
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }
        public clsTestAppointment TestAppointmentInfo;
        public clsTest()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = "";
            this.CreatedByUserID = 0;
            _Mode = enTypeMode.Add;
        }
        public clsTest(int TestID, int TestAppointmentID, bool TestResult, 
            string Notes, int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            _Mode = enTypeMode.Update;
            this.TestAppointmentInfo = clsTestAppointment.Find(this.TestAppointmentID);
        }
        public static clsTest GetTestByID(int TestID)
        {
            int TestAppointmentID = -1, CreatedByUserID = -1;
            bool TestResult = false;
            string Notes = "";
            bool isFound = clsTestData.GetTestByID(TestID, ref TestAppointmentID,
            ref TestResult, ref Notes, ref CreatedByUserID);
            if (isFound)
            {
                return new clsTest(TestID, TestAppointmentID, TestResult,
                            Notes, CreatedByUserID);
            }
            else
                return null;
        }

        public static clsTest GetLastTestByPersonAndLicenseClassAndTestAppointmentID(int PersonID,
            int LicenseClassID, int TestTypeID)
        {
            int TestAppointmentID = -1, CreatedByUserID = -1, TestID = -1;
            bool TestResult = false;
            string Notes = "";
            bool isFound = clsTestData.GetLastTestByPersonAndLicenseClassAndTestAppointmentID(PersonID,
            LicenseClassID, TestTypeID, ref TestAppointmentID,
            ref TestResult, ref Notes, ref CreatedByUserID);
            if (isFound)
            {
                return new clsTest(TestID, TestAppointmentID, TestResult,
                            Notes, CreatedByUserID);
            }
            else
                return null;
        }
        public static DataTable GetAllTest()
        {
            return clsTestData.GetAllTest();
        }
        private bool _AddNewTest()
        {
            this.TestID = clsTestData.AddNewTest(TestAppointmentID, TestResult,
                Notes, CreatedByUserID);
            return this.TestID > 0;
        }
        private bool _UpdateTest()
        {
            return clsTestData.UpdateTest(TestID, TestAppointmentID, TestResult,
                Notes, CreatedByUserID);
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
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassTest(LocalDrivingLicenseApplicationID);
        }
        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3;
        }

    }
}
