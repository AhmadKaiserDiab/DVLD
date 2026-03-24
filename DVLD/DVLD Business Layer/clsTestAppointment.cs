using System;
using System.Data;
using DVLD_Data_Access_Layer;

namespace DVLD_Business_Layer
{
    public class clsTestAppointment
    {
        enum enMode { AddNew = 0, Update = 1 };

        enMode _Mode;
        public int TestAppointmentID { private set; get; }
        public clsTestType.enTestType TestTypeID { set; get; }
        public clsTestType TestTypeInfo { set; get; }
        public int LocalDrivingLicenseApplicationID { set; get; }
        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo { set; get; }
        public DateTime AppointmentDate { set; get; }
        public double PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        public clsUser CreatedByUserInfo { set; get; }
        public bool IsLocked { private set; get; }
        public int RetakeTestApplicationID { set; get; }
        public clsApplication RetakeTestApplicationInfo { private set; get; }

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID =
            clsTestAppointmentData.AddNewTestAppointment((int)TestTypeID,
            LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees,
            CreatedByUserID,RetakeTestApplicationID);
            return (TestAppointmentID != -1);
        }

        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentData
            .UpdateTestAppointment(TestAppointmentID, AppointmentDate);
        }

        private clsTestAppointment(int TestAppointmentID,
            clsTestType.enTestType TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate, double PaidFees, int CreatedByUserID,
            bool IsLocked, int RetakeTestApplicationID)
        {
            _Mode = enMode.Update;
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.TestTypeInfo = clsTestType.FindTestType(TestTypeID);
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.RetakeTestApplicationInfo = clsApplication.FindBaseApplication(RetakeTestApplicationID);
        }

        public clsTestAppointment()
        {
            _Mode = enMode.AddNew;
            TestAppointmentID = -1;
            TestTypeID = clsTestType.enTestType.VisionTest;
            LocalDrivingLicenseApplicationID = -1;
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;
            IsLocked = false;
            RetakeTestApplicationID = -1;
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = -1, LocalDrivingLicenseApplicationID = -1,
                CreatedByUserID = -1, RetakeTestApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            double PaidFees = 0;
            bool IsLocked = false;

            if (clsTestAppointmentData.Find(TestAppointmentID, ref TestTypeID,
                ref LocalDrivingLicenseApplicationID, ref AppointmentDate,
                ref PaidFees, ref CreatedByUserID, ref IsLocked,
                ref RetakeTestApplicationID))
            {
                return new clsTestAppointment(TestAppointmentID, 
                    (clsTestType.enTestType)TestTypeID, 
                    LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, 
                    CreatedByUserID, IsLocked, RetakeTestApplicationID);
            }
            return null;
        }

        public bool Save()
        {
            if (RetakeTestApplicationInfo != null && IsLocked)
            {
                if (! RetakeTestApplicationInfo.SetComplete())
                {
                    // Nothing To Do Here But Complete The Retake Test Application
                    return false;
                }
            }

            switch(_Mode)
            {
                case enMode.AddNew:
                {
                    if(_AddNewTestAppointment())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                case enMode.Update:
                {
                        return _UpdateTestAppointment();
                }
                default:
                    return false;
            }
        }

        public static DataTable GetTestAppointmentsList(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentData
                .GetTestAppointmentsList(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public int GetAsscociatedTestID()
        {
            return clsTestAppointmentData.GetAsscociatedTestID(TestAppointmentID);
        }

        internal static bool LockTestAppointment(int TestAppointmentID)
        {
            return clsTestAppointmentData.LockTestAppointment(TestAppointmentID);
        }

    }
}
