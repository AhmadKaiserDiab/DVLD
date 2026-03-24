using System;
using System.Data;
using DVLD_Data_Access_Layer;

namespace DVLD_Business_Layer
{
    public class clsInternationalLicense
    {
        public int InternationalLicenseID { private set; get; }
        public int ApplicationID { set; get; }
        public clsApplication ApplicationInfo { private set; get; }
        public int DriverID { set; get; }
        public clsDriver DriverInfo { private set; get; }
        public int IssuedUsingLocalLicenseID { set; get; }
        public clsLicense IssuedUsingLocalLicenseInfo { private set; get; }
        public DateTime IssueDate { private set; get; }
        public DateTime ExpirationDate { private set; get; }
        public bool IsActive { private set; get; }
        public int CreatedByUserID { set; get; }
        public clsUser CreatedByUserInfo { private set; get; }

        public clsInternationalLicense(int InternationalLicenseID,
            int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive,
            int CreatedByUserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.ApplicationInfo = clsApplication.FindBaseApplication(ApplicationID);
            this.DriverID = DriverID;
            this.DriverInfo = clsDriver.FindDriverByID(DriverID);
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssuedUsingLocalLicenseInfo = clsLicense.Find(IssuedUsingLocalLicenseID);
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindUserByID(CreatedByUserID);

            if (DateTime.Now > ExpirationDate)
            {
                _DeActivateLicense(InternationalLicenseID);
                this.IsActive = false;
            }
            this.IsActive = IsActive;
        }

        public clsInternationalLicense()
        {
            InternationalLicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            IssuedUsingLocalLicenseID = -1;
            IssueDate = DateTime.Now;
            ExpirationDate = IssueDate.AddYears(5);
            IsActive = true;
            CreatedByUserID = -1;
        }

        private bool _AddNewInternationalLicense()
        {
            clsApplication IntApplication = new clsApplication();

            IntApplication.ApplicantPersonID = clsDriver.FindDriverByID(DriverID).PersonID;
            IntApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            IntApplication.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            IntApplication.PaidFees = clsApplicationType.FindApplicationType(IntApplication.ApplicationTypeID).Fees;
            IntApplication.CreatedByUserID = CreatedByUserID;

            if (IntApplication.Save())
            {
                this.ApplicationID = IntApplication.ApplicationID;
                this.InternationalLicenseID = clsInternationalLicenseData
                .AddNewInternationalLicense(ApplicationID,
                DriverID, IssuedUsingLocalLicenseID, IssueDate,
                IssueDate.AddYears(5), IsActive, CreatedByUserID);
            }
            return (InternationalLicenseID != -1);
        }

        private bool _DeActivateLicense(int LicenseID)
        {
            return clsInternationalLicenseData.DeActivateLicense(LicenseID);
        }

        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
            int ApplicationID = -1, DriverID = -1,
                IssuedUsingLocalLicenseID = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            bool IsActive = true;

            if (clsInternationalLicenseData.Find(InternationalLicenseID,
                ref ApplicationID, ref DriverID,
                ref IssuedUsingLocalLicenseID, ref IssueDate,
                ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                return new clsInternationalLicense(InternationalLicenseID,
                    ApplicationID, DriverID, IssuedUsingLocalLicenseID,
                    IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            return _AddNewInternationalLicense();
        }

        public static int GetActiveLicenseID(int DriverID)
        {
            return clsInternationalLicenseData.GetActiveLicenseID(DriverID);
        }

        internal static DataTable GetDriverLicenses(int DriverID)
        {
            return clsInternationalLicenseData.GetDriverLicenses(DriverID);
        }

        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicenseData.GetAllInternationalLicenses();
        }
    }
}