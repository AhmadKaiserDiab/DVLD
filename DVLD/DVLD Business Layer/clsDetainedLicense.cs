using System;
using System.Data;
using DVLD_Data_Access_Layer;

namespace DVLD_Business_Layer
{
    public class clsDetainedLicense
    {
        public int DetainID { set; get; }
        public int LicenseID { set; get; }
        public clsLicense LicenseInfo { set; get; }
        public DateTime DetainDate { set; get; }
        public float FineFees { set; get; }
        public int CreatedByUserID { set; get; }
        public clsUser CreatedByUserInfo { set; get; }
        public bool IsReleased { set; get; }
        public DateTime ReleaseDate { set; get; }
        public int ReleasedByUserID { set; get; }
        public clsUser ReleasedByUserInfo { set; get; }
        public int ReleaseApplicationID { set; get; }

        private clsDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate,
            float FineFees, int CreatedByUserID, bool IsReleased, 
            DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.LicenseInfo = clsLicense.Find(LicenseID);
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindUserByID(CreatedByUserID);
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleasedByUserInfo = clsUser.FindUserByID(ReleasedByUserID);
            this.ReleaseApplicationID = ReleaseApplicationID;
        }

        public clsDetainedLicense()
        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseDate = DateTime.MaxValue;
            this.ReleasedByUserID = -1;
        }

        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {
            int DetainID = -1, CreatedByUserID = -1,
                ReleasedByUserID = -1, ReleaseApplicationID = -1;

            DateTime DetainDate = DateTime.Now, ReleaseDate = DateTime.Now;
            float FineFees = 0;
            bool IsReleased = false;

            if (clsDetainedLicenseData.FindByLicenseID(LicenseID, ref DetainID,
                    ref DetainDate, ref FineFees, ref CreatedByUserID,
                    ref IsReleased, ref ReleaseDate, ref ReleasedByUserID,
                    ref ReleaseApplicationID))
            {
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate,
                        FineFees, CreatedByUserID, IsReleased, ReleaseDate,
                        ReleasedByUserID, ReleaseApplicationID);
            }
            return null;
        }

        public bool DetainLicense()
        {
            this.DetainID = clsDetainedLicenseData.DetainLicense(LicenseID,
                FineFees,CreatedByUserID);
            return DetainID != -1;
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID)
        {
            clsApplication ReleaseApplication = new clsApplication();

            ReleaseApplication.ApplicantPersonID = LicenseInfo.DriverInfo.PersonID;
            ReleaseApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            ReleaseApplication.PaidFees = clsApplicationType.FindApplicationType(ReleaseApplication.ApplicationTypeID).Fees;
            ReleaseApplication.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            ReleaseApplication.CreatedByUserID = ReleasedByUserID;

            if (ReleaseApplication.Save())
            {
                this.ReleaseApplicationID = ReleaseApplication.ApplicationID;
                return clsDetainedLicenseData.ReleaseDetainedLicense(DetainID,
                    ReleasedByUserID, ReleaseApplicationID);
            }
            return false;
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicenseData.IsLicenseDetained(LicenseID);
        }

        public static DataTable GetAllDetainedLicensesApplications()
        {
            return clsDetainedLicenseData.GetAllDetainedLicenses();
        }

    }
}