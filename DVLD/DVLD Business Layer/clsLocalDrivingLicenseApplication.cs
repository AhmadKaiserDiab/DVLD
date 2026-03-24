using System;
using System.Data;
using DVLD_Data_Access_Layer;

namespace DVLD_Business_Layer
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        enMode LocalDrivingLicenseApplicationMode = enMode.Update;
        public int LocalDrivingLicenseApplicationID { set; get; }
        public int LicenseClassID { set; get; }
        public clsLicenseClass LicenseClassInfo { set; get; }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = 
                clsLocalDrivingLicenseApplicationData
                .AddNewLocalDrivingLicenseApplication(this.ApplicationID,
                LicenseClassID);

            return (LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.
                    UpdateLocalDrivingLicenseApplication(
                        LocalDrivingLicenseApplicationID, LicenseClassID);
        }

        private clsLocalDrivingLicenseApplication(
            int LocalDrivingLicenseApplicationID, int LicenseClassID,
            int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, 
            int ApplicationTypeID, enApplicationStatus ApplicationStatus, 
            DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
            
            : base(ApplicationID, ApplicantPersonID, ApplicationDate, 1,
                  ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
        {
            LocalDrivingLicenseApplicationMode    = enMode.Update;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID; ;
            this.LicenseClassID                   = LicenseClassID;
            this.LicenseClassInfo                 = clsLicenseClass.Find(LicenseClassID);
        }

        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;
            LocalDrivingLicenseApplicationMode = enMode.AddNew;
            ApplicationStatus = enApplicationStatus.New;
            ApplicationTypeID = 1;
            ApplicationTypeInfo = clsApplicationType.FindApplicationType(ApplicationTypeID);
        }

        public static clsLocalDrivingLicenseApplication FindByLocalDrivingAppLicenseID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;
            
            if (clsLocalDrivingLicenseApplicationData.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID,
                ref ApplicationID, ref LicenseClassID))
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                return new clsLocalDrivingLicenseApplication(
                    LocalDrivingLicenseApplicationID, LicenseClassID, 
                    ApplicationID, Application.ApplicantPersonID,
                    Application.ApplicationDate, Application.ApplicationTypeID,
                    (enApplicationStatus)Application.ApplicationStatus,
                    Application.LastStatusDate, Application.PaidFees, 
                    Application.CreatedByUserID);
            }
            return null;
        }

        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;

            if (
                clsLocalDrivingLicenseApplicationData
                .GetLocalDrivingLicenseApplicationInfoByApplicationID ( 
                    ApplicationID, ref LocalDrivingLicenseApplicationID, 
                    ref LicenseClassID)
                )
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                return new clsLocalDrivingLicenseApplication(
                    LocalDrivingLicenseApplicationID, LicenseClassID,
                    ApplicationID, Application.ApplicantPersonID,
                    Application.ApplicationDate, Application.ApplicationTypeID,
                    (enApplicationStatus)Application.ApplicationStatus,
                    Application.LastStatusDate, Application.PaidFees,
                    Application.CreatedByUserID);
            }
            return null;
        }

        public new bool Save()
        {
            base._Mode = (clsApplication.enMode)LocalDrivingLicenseApplicationMode;
            if (!base.Save())
            {
                return false;
            }

            switch (LocalDrivingLicenseApplicationMode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewLocalDrivingLicenseApplication())
                        {
                            LocalDrivingLicenseApplicationMode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case enMode.Update:
                    {
                        return _UpdateLocalDrivingLicenseApplication();
                    }
            }
            return false;
        }

        public static new bool Delete(int LocalDrivingLicenseApplicationID)
        {
            int BaseApplicationID = FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID).ApplicationID;
            if (clsLocalDrivingLicenseApplicationData.Delete(LocalDrivingLicenseApplicationID))
            {
                return clsApplication.Delete(BaseApplicationID);
            }
            return false;
        }

        public static new bool Cancel(int LocalDrivingLicenseApplicationID)
        {
            int BaseApplicationID = FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID).ApplicationID;
            return clsApplication.Cancel(BaseApplicationID);
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }

        public static int GetActiveLocalDrivingLicenseApplicationID(int PersonID, int LicenseClassID)
        {
            return clsLocalDrivingLicenseApplicationData
                .GetActiveLocalDrivingLicenseApplicationID(PersonID, LicenseClassID);
        }

        public static int GetLicenseID(int PersonID, int SelectedLicenseClassID)
        {
            return clsLicense.GetLicenseID(PersonID, SelectedLicenseClassID);
        }

        public byte GetPassedTestsCount()
        {
            return clsLocalDrivingLicenseApplicationData.GetPassedTestsCount(this.LocalDrivingLicenseApplicationID);
        }

        public int GetLicenseID()
        {
            return clsLocalDrivingLicenseApplicationData.GetLicenseID(this.ApplicationID);
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static byte GetTotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.GetTotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public byte GetTotalTrialsPerTest(clsTestType.enTestType TestTypeID)
        {
            return GetTotalTrialsPerTest(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesAttendTestType(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public int IssueLicenseForFirstTime(string Notes, int CreatedByUserID)
        {
            clsDriver Driver = clsDriver.FindDriverByPersonID(this.ApplicantPersonID);

            if (Driver == null)
            {
                Driver.PersonID        = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;

                if (! Driver.Save())
                {
                    return -1;
                }
            }

            clsLicense NewLicense      = new clsLicense();
            NewLicense.IssueReason     = clsLicense.enIssueReason.FirstTime;
            NewLicense.ApplicationID   = this.ApplicationID;
            NewLicense.DriverID        = Driver.DriverID;
            NewLicense.LicenseClassID  = this.LicenseClassID;
            NewLicense.IssueDate       = DateTime.Now;
            NewLicense.ExpirationDate  = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            NewLicense.Notes           = Notes;
            NewLicense.PaidFees        = this.ApplicationTypeInfo.Fees;
            NewLicense.IsActive        = true;
            NewLicense.CreatedByUserID = CreatedByUserID;

            if (NewLicense.Save())
            {
                this.SetComplete();
                return NewLicense.LicenseID;
            }
            return -1;
        }

    }
}
