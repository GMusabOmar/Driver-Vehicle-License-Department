using System;
using System.Data;
using DataAccess;

namespace BusinessAccess
{
    public class clsTestAppointment
    {
        public enum enTypeMode { Add = 1, Update = 2};
        private enTypeMode _Mode = enTypeMode.Add;
        public int TestAppointmentID { get; set; }
        public clsTestType.enTypeID TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public int TestID
        {
            get
            {
                return _GetTestID();
            }
        }
        public clsApplication RetakeTestAppInfo { get; set; }
        public clsTestAppointment()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = clsTestType.enTypeID.Vision;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = -1;
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            this.RetakeTestApplicationID = -1;
            _Mode = enTypeMode.Add;
        }
        public clsTestAppointment(int TestAppointmentID, clsTestType.enTypeID TestTypeID,
            int LocalDrivingLicenseApplicationID, DateTime AppointmentDate,
            decimal PaidFees, int CreatedByUserID, bool IsLocked,
            int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            _Mode = enTypeMode.Update;
        }
        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = -1, LocalDrivingLicenseApplicationID = -1, CreatedByUserID = -1, RetakeTestApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = -1;
            bool IsLocked = false;
            bool isFound = clsTestAppointmentData.GetTestAppointmentInfoByID(TestAppointmentID,
                            ref TestTypeID, ref LocalDrivingLicenseApplicationID, 
                            ref AppointmentDate, ref PaidFees, ref CreatedByUserID, 
                            ref IsLocked, ref RetakeTestApplicationID);
            if (isFound)
            {
                return new clsTestAppointment(TestAppointmentID, (clsTestType.enTypeID)TestTypeID,
                LocalDrivingLicenseApplicationID, AppointmentDate,
                PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            }
            else
                return null;
        }
        public static clsTestAppointment GetLastTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID)
        {
            int TestAppointmentID = -1, CreatedByUserID = -1, RetakeTestApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = -1;
            bool IsLocked = false;
            bool isFound = clsTestAppointmentData.GetLastTestAppointment(TestTypeID, LocalDrivingLicenseApplicationID,
            ref TestAppointmentID, ref AppointmentDate, ref PaidFees,
            ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID);
            if (isFound)
            {
                return new clsTestAppointment(TestAppointmentID, (clsTestType.enTypeID)TestTypeID,
                LocalDrivingLicenseApplicationID, AppointmentDate,
                PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            }
            else
                return null;
        }
        public static DataTable GetAllTestAppointments()
        {
            return clsTestAppointmentData.GetAllTestAppointments();
        }
        public static DataTable GetApplicationTestAppointmentsPerTestType(int TestTypeID, int LocalDrivingLicenseApplicationID)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType(TestTypeID, LocalDrivingLicenseApplicationID);
        }
        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment((int)TestTypeID,
                LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees,
                CreatedByUserID, IsLocked, RetakeTestApplicationID);
            return this.TestAppointmentID > 0;
        }

        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentData.UpdateTestAppointment(TestAppointmentID, (int)TestTypeID,
            LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees,
            CreatedByUserID, IsLocked, RetakeTestApplicationID);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enTypeMode.Add:
                    if (_AddNewTestAppointment())
                    {
                        _Mode = enTypeMode.Update;
                        return true;
                    }
                    else
                        return false;
                default:
                    return _UpdateTestAppointment();
            }
        }

        public static bool DeleteTestAppointment(int TestAppointmentID)
        {
            return clsTestAppointmentData.DeleteTestAppointment(TestAppointmentID);
        }

        private int _GetTestID()
        {
            return clsTestAppointmentData.GetTestID(TestAppointmentID);
        }

    }
}
