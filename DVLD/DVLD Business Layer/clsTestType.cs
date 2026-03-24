using System;
using DVLD_Data_Access_Layer;
using System.Data;

namespace DVLD_Business_Layer
{
    public class clsTestType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public clsTestType.enTestType TestTypeID { set; get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Fees { get; set; }

        public clsTestType()
        {
            Mode = enMode.AddNew;
            this.TestTypeID = clsTestType.enTestType.VisionTest;
            this.Title = "";
            this.Description = "";
            this.Fees = 0;
        }

        private clsTestType(enTestType ID, string Title, string Description,
            float Fees)
        {
            this.TestTypeID = ID;
            this.Title = Title;
            this.Description = Description;
            this.Fees = Fees;
        }

        private bool _AddNewTestType()
        {
            this.TestTypeID = (clsTestType.enTestType)clsTestTypeData.AddNewTestType(this.Title, this.Description, this.Fees);
            return (this.Title != "");
        }

        private bool _UpdateTestType()
        {
            return clsTestTypeData.UpdateTestType((int)TestTypeID, Title, Description, Fees);
        }

        public static clsTestType FindTestType(clsTestType.enTestType TestTypeID)
        {
            string Title = "", Description = "";
            float Fees = 0;
            if (clsTestTypeData.FindTestType((int)TestTypeID, ref Title,
                ref Description, ref Fees))
            {
                return new clsTestType(TestTypeID, Title,
                    Description, Fees);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewTestType())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case enMode.Update:
                    return _UpdateTestType();
            }
            return false;
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();
        }
    }
}