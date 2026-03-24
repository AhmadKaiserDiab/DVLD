using System;
using System.Data;
using DVLD_Data_Access_Layer;

namespace DVLD_Business_Layer
{
    public class clsApplicationType
    {
        enum enMode { AddNew = 0 , Update = 1};
        private enMode _Mode = enMode.AddNew;

        public int ID { private set; get; }
        public string Title { get; set; }
        public float Fees { get; set; }

        public clsApplicationType()
        {
            _Mode = enMode.AddNew;
            ID = -1;
            Title = "";
            Fees = 0;
        }

        private clsApplicationType(int ApplicationTypeID,
            string ApplicationTypeTitle, float ApplicationTypeFees)
        {
            _Mode = enMode.Update;
            this.ID = ApplicationTypeID;
            this.Title = ApplicationTypeTitle;
            this.Fees = ApplicationTypeFees;
        }

        private bool _AddNewApplicationType()
        {
            this.ID = clsApplicationTypeData.AddNewApplicationType(this.Title, this.Fees);
            return (this.ID != -1);
        }

        private bool _UpdateApplicationType()
        {
            return clsApplicationTypeData.UpdateApplicationType(ID,
                Title, Fees);
        }

        public static clsApplicationType FindApplicationType(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = "";
            float ApplicationTypeFees = 0;
            if (clsApplicationTypeData.FindApplicationTypeByID(ApplicationTypeID,
                ref ApplicationTypeTitle, ref ApplicationTypeFees))
            {
                return new clsApplicationType(ApplicationTypeID,
                    ApplicationTypeTitle, ApplicationTypeFees);
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
                        if (_AddNewApplicationType())
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
                    return _UpdateApplicationType();
            }
            return false;
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }
    }
}
