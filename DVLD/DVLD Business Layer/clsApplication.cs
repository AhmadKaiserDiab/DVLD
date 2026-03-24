using System;
using DVLD_Data_Access_Layer;

namespace DVLD_Business_Layer
{
    public class clsApplication
    {
        protected enum enMode { AddNew = 0, Update = 1 };
        public enum enApplicationType
        {
            NewDrivingLicense = 1,
            RenewDrivingLicense = 2,
            ReplaceLostDrivingLicense = 3, ReplaceDamagedDrivingLicense = 4,
            ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6,
            RetakeTest = 7
        };
        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };

        protected enMode _Mode = enMode.AddNew;
        public int ApplicationID { set; get; }
        public int ApplicantPersonID { set; get; }
        public clsPerson ApplicantPerson { set; get; }
        public string ApplicantFullName { get { return ApplicantPerson.FullName; } }
        public DateTime ApplicationDate { set; get; }
        public int ApplicationTypeID { set; get; }
        public clsApplicationType ApplicationTypeInfo { protected set; get; }
        public enApplicationStatus ApplicationStatus { set; get; }
        public string StatusText
        {
            get
            {
                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }
        }
        public DateTime LastStatusDate { set; get; }
        public float PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        public clsUser CreatedByUserInfo { set; get; }

        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(
                ApplicantPersonID, ApplicationDate, (int)ApplicationTypeID,
                (short)ApplicationStatus, LastStatusDate, PaidFees,
                CreatedByUserID);

            return (ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {
            //return clsApplicationData.UpdateApplication(this.ApplicationID,
            //    this.ApplicantPersonID, this.ApplicationDate,
            //    this.ApplicationTypeID, (byte)this.ApplicationStatus,
            //    this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return clsApplicationData.UpdateApplicationStatus(ApplicationID, (byte)ApplicationStatus);
        }

        protected clsApplication(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
            enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
                float PaidFees, int CreatedByUserID)

        {
            this.ApplicationID       = ApplicationID;
            this.ApplicantPersonID   = ApplicantPersonID;
            this.ApplicantPerson     = clsPerson.Find(ApplicantPersonID);
            this.ApplicationDate     = ApplicationDate;
            this.ApplicationTypeID   = ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.FindApplicationType(ApplicationTypeID);
            this.ApplicationStatus   = ApplicationStatus;
            this.LastStatusDate      = LastStatusDate;
            this.PaidFees            = PaidFees;
            this.CreatedByUserID     = CreatedByUserID;
            this.CreatedByUserInfo   = clsUser.FindUserByID(CreatedByUserID);
            _Mode = enMode.Update;
        }

        public clsApplication()
        {
            _Mode = enMode.AddNew;
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.CreatedByUserID = -1;
            this.PaidFees = 0;
        }

        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            int ApplicantPersonID = -1, ApplicationTypeID = -1, CreatedByUserID = -1;
            short ApplicationStatus = 0;
            DateTime ApplicationDate = DateTime.Now, ApplicationLastStatusDate = DateTime.Now;
            float PaidFees = 0;

            if (clsApplicationData.FindApplicationByID(ApplicationID, ref ApplicantPersonID,
                ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus,
                ref ApplicationLastStatusDate, ref PaidFees, ref CreatedByUserID))
            {
                return new clsApplication(ApplicationID, ApplicantPersonID,
                    ApplicationDate, ApplicationTypeID,
                    (enApplicationStatus)ApplicationStatus,
                    ApplicationLastStatusDate, PaidFees, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }
        
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewApplication())
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
                    return _UpdateApplication();
            }
            return false;
        }

        public bool SetComplete()
        {
            return SetComplete(ApplicationID);
        }

        public static bool SetComplete(int ApplicationID)
        {
            //    ApplicationStatus = "Completed";
            return clsApplicationData.UpdateApplicationStatus(ApplicationID,
                (int)enApplicationStatus.Completed);
        }

        public static bool Cancel(int ApplicationID)
        {
            //ApplicationStatus = "Cancelled";
            return clsApplicationData.UpdateApplicationStatus(ApplicationID,
                (int)enApplicationStatus.Cancelled);
        }

        public static bool Delete(int ApplicationID)
        {
            return clsApplicationData.DeleteApplication(ApplicationID);
        }

    }
}