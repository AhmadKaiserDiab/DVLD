using System.Data;
using DVLD_Data_Access_Layer;

namespace DVLD_Business_Layer
{
    public class clsLicenseClass
    {
        public int LicenseClassID { set; get; }
        public string ClassName { set; get; }
        public string ClassDescription { set; get; }
        public byte MinimumAllowedAge { set; get; }
        public byte DefaultValidityLength { set; get; }
        public float ClassFees { set; get; }

        private clsLicenseClass(int LicenseClassID, string ClassName, string ClassDescription,
            byte MinimumAllowedAge, byte DefaultValidityLength, float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
        }

        public clsLicenseClass()
        {
            this.LicenseClassID        = -1;
            this.ClassName             = "";
            this.ClassDescription      = "";
            this.MinimumAllowedAge     = 0;
            this.DefaultValidityLength = 0;
            this.ClassFees             = 0;
        }

        public static clsLicenseClass Find(int LicenseClassID)
        {
            string ClassName = "", ClassDescription = "";
            byte MinimumAllowedAge = 18, DefaultValidityLength = 1;
            float ClassFees = 0;

            if (clsLicenseClassData.Find(LicenseClassID, ref ClassName,
                ref ClassDescription, ref MinimumAllowedAge, 
                ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClass(LicenseClassID, ClassName, 
                    ClassDescription, MinimumAllowedAge, DefaultValidityLength,
                    ClassFees);
            }
            return null;
        }

        public static clsLicenseClass Find(string ClassName)
        {
            int LicenseClassID = -1;
            string ClassDescription = "";
            byte MinimumAllowedAge = 18; byte DefaultValidityLength = 1;
            float ClassFees = 0;

            if (clsLicenseClassData.GetLicenseClassInfoByClassName(ClassName,
                ref LicenseClassID, ref ClassDescription, ref MinimumAllowedAge,
                ref DefaultValidityLength, ref ClassFees))
            {

                return new clsLicenseClass(LicenseClassID, ClassName,
                    ClassDescription, MinimumAllowedAge, DefaultValidityLength,
                    ClassFees);
            }
            return null;
        }

        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassData.GetAllLicenseClasses();
        }             
    }
}
