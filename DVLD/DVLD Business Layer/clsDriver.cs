using System;
using System.Data;
using DVLD_Data_Access_Layer;

namespace DVLD_Business_Layer
{
    public class clsDriver
    {
        public int DriverID { private set; get; }
        public int PersonID { set; get; }
        public clsPerson PersonInfo { set; get; }
        public int CreatedByUserID { set; get; }
        public clsUser CreatedByUserInfo { set; get; }
        public DateTime CreatedDate { private set; get; }

        private bool _AddNewDriver()
        {
            this.DriverID = clsDriverData.AddNewDriver(PersonID, CreatedByUserID);
            return (DriverID != -1);
        }

        private clsDriver(int DriverID, int PersonID, int CreatedByUserID,
            DateTime CreatedDate)
        {
            this.DriverID          = DriverID;
            this.PersonID          = PersonID;
            this.PersonInfo        = clsPerson.Find(PersonID);
            this.CreatedByUserID   = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindUserByID(CreatedByUserID);
            this.CreatedDate       = CreatedDate;
        }

        public clsDriver()
        {
            this.DriverID        = -1;
            this.PersonID        = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate     = DateTime.Now;
        }

        public static clsDriver FindDriverByID(int DriverID)
        {
            int PersonID = -1, UserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (clsDriverData.FindDriverByID(DriverID, ref PersonID, ref UserID,
                ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, UserID, CreatedDate);
            }
            else
            {
                return null;
            }
        }

        public static clsDriver FindDriverByPersonID(int PersonID)
        {
            int DriverID = -1, UserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (clsDriverData.FindDriverByPersonID(PersonID, ref DriverID, ref UserID,
                ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, UserID, CreatedDate);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            return _AddNewDriver();
        }

        public static DataTable GetAllDrivers()
        {
            return clsDriverData.GetAllDrivers();
        }

        public DataTable GetLocalLicenses()
        {
            return clsLicense.GetDriverLicenses(DriverID);
        }

        public DataTable GetInternationalLicenses()
        {
            return clsInternationalLicense.GetDriverLicenses(DriverID);
        }
    }
}       