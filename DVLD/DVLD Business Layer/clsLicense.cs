using System;
using System.Data;
using DVLD_Data_Access_Layer;

namespace DVLD_Business_Layer
{
    public class clsLicense
    {
        public enum enIssueReason
        {
            FirstTime = 1, Renew = 2,
            DamagedReplacement = 3, LostReplacement = 4
        };

        public enIssueReason IssueReason { set; get; }
        public int LicenseID { set; get; }
        public int ApplicationID { set; get; }
        public clsApplication ApplicationInfo { set; get; }
        public int DriverID { set; get; }
        public clsDriver DriverInfo { set; get; }
        public int LicenseClassID { set; get; }
        public clsLicenseClass LicenseClassInfo { set; get; }
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public string Notes { set; get; }
        public float PaidFees { set; get; }
        public bool IsActive { set; get; }
        public int CreatedByUserID { set; get; }
        public clsUser CreatedByUserInfo { set; get; }
        public bool IsDetained { private set; get; }
        public string IssueReasonText
        {
            get
            {
                return _GetIssueReasonText();
            }
        }

        private string _GetIssueReasonText()
        {
            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew License";
                case enIssueReason.DamagedReplacement:
                    return "Replacement For Damaged";
                case enIssueReason.LostReplacement:
                    return "Replacement For Lost";
                default:
                    return "First Time";
            }
        }

        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseData
            .AddNewLicense(ApplicationID, DriverID, LicenseClassID,
            IssueDate, ExpirationDate, Notes, PaidFees, IsActive,
            (int)IssueReason, CreatedByUserID);

            return (LicenseID != -1);
        }

        private bool _DeActivateLicense()
        {
            return clsLicenseData.DeActivateLicense(LicenseID);
        }

        private clsLicense(int LicenseID, int ApplicationID, int DriverID,
                int LicenseClassID, DateTime IssueDate, 
                DateTime ExpirationDate, string Notes, float PaidFees,
                bool IsActive, enIssueReason IssueReason,
                int CreatedByUserID)
        {
            this.IssueReason       = IssueReason;
            this.LicenseID         = LicenseID;
            this.ApplicationID     = ApplicationID;
            this.ApplicationInfo   = clsApplication.FindBaseApplication(ApplicationID);
            this.DriverID          = DriverID;
            this.DriverInfo        = clsDriver.FindDriverByID(DriverID);
            this.LicenseClassID    = LicenseClassID;
            this.LicenseClassInfo  = clsLicenseClass.Find(LicenseClassID);
            this.IssueDate         = IssueDate;
            this.ExpirationDate    = ExpirationDate;
            this.Notes             = Notes;
            this.PaidFees          = PaidFees;
            this.CreatedByUserID   = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindUserByID(CreatedByUserID);
            this.IsDetained        = clsDetainedLicense.IsLicenseDetained(LicenseID);

            if (IsActive && DateTime.Now > ExpirationDate)
            {
                this.IsActive = false;
                _DeActivateLicense();
            }
            else
            {
                this.IsActive = IsActive;
            }
        }

        public clsLicense()
        {
            this.IssueReason     = enIssueReason.FirstTime;
            this.LicenseID       = -1;
            this.ApplicationID   = -1;
            this.DriverID        = -1;
            this.LicenseClassID  = -1;
            this.IssueDate       = DateTime.Now;
            this.ExpirationDate  = DateTime.Now.AddDays(1);
            this.Notes           = "";
            this.PaidFees        = 0;
            this.IsActive        = true;
            this.CreatedByUserID = -1;
            this.IsDetained      = false;
        }

        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = -1, DriverID = -1, LicenseClassID = -1,
                CreatedByUserID = -1, IssueReason = 1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = true;

            if (clsLicenseData.Find(LicenseID, ref ApplicationID, 
                ref DriverID, ref LicenseClassID, ref IssueDate, 
                ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive,
                ref CreatedByUserID, ref IssueReason
                ))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID,
                    LicenseClassID, IssueDate, ExpirationDate, Notes,
                    PaidFees, IsActive, (enIssueReason)IssueReason,
                    CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            return _AddNewLicense();
        }

        public static int GetLicenseID(int PersonID, int SelectedLicenseClassID)
        {
            return clsLicenseData.GetLicenseID(PersonID, SelectedLicenseClassID);
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseData.GetDriverLicenses(DriverID);
        }

        public clsLicense RenewLicense(string Notes, int CreatedByUserID)
        {
            return _IssueNewLicense(enIssueReason.Renew, Notes, CreatedByUserID);
        }

        public clsLicense ReplaceLostLicense(int CreatedByUserID)
        {
            return _IssueNewLicense(enIssueReason.LostReplacement, "", CreatedByUserID);
        }

        public clsLicense ReplaceDamagedLicense(int CreatedByUserID)
        {
            return _IssueNewLicense(enIssueReason.DamagedReplacement, "", CreatedByUserID);
        }

        private clsLicense _IssueNewLicense(enIssueReason IssueReason, string Notes, int CreatedByUserID)
        {
            clsLicense NewLicense = new clsLicense();
            clsApplication NewLicenseApplication = new clsApplication();

            switch (IssueReason)
            {
                case enIssueReason.Renew:
                    NewLicenseApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
                    break;
                case enIssueReason.DamagedReplacement:
                    NewLicenseApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
                    break;
                case enIssueReason.LostReplacement:
                    NewLicenseApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;
                    break;
                default:
                    NewLicenseApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;
                    break;
            }

            NewLicenseApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            NewLicenseApplication.ApplicantPersonID = this.DriverInfo.PersonID; ;
            NewLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            NewLicenseApplication.PaidFees = clsApplicationType.FindApplicationType(NewLicenseApplication.ApplicationTypeID).Fees;
            NewLicenseApplication.CreatedByUserID = CreatedByUserID;

            if (!NewLicenseApplication.Save())
            {
                return null;
            }


            NewLicense.IssueReason = IssueReason;
            NewLicense.ApplicationID = NewLicenseApplication.ApplicationID;
            NewLicense.ApplicationInfo = clsApplication.FindBaseApplication(NewLicense.ApplicationID);
            NewLicense.DriverID = this.DriverID;
            NewLicense.DriverInfo = this.DriverInfo;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.LicenseClassInfo = this.LicenseClassInfo;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.Notes = Notes;
            NewLicense.PaidFees = PaidFees;
            NewLicense.CreatedByUserID = CreatedByUserID;
            NewLicense.CreatedByUserInfo = clsUser.FindUserByID(NewLicense.CreatedByUserID);
            NewLicense.IsDetained = false;

            if (IssueReason == enIssueReason.Renew)
            {
                NewLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            }
            else
            {
                NewLicense.ExpirationDate = this.ExpirationDate;
            }

            if (NewLicense.Save())
            {
                _DeActivateLicense();
                return NewLicense;
            }
            return null;
        }
        
    }
}