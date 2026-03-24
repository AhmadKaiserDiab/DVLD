using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DVLD_Data_Access_Layer
{
    public class clsLicenseClassData
    {
        public static bool Find(int LicenseClassID, ref string ClassName,
            ref string ClassDescription, ref byte MinimumAllowedAge,
            ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool isFound = false;

            string query = @"select * from LicenseClasses 
                                where LicenseClassID = @LicenseClassID ";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            Dictionary <string, object> LicenseClass = clsDataAccessSettings.Find(command);

            if(LicenseClass.Keys.Count != 0)
            {
                ClassName             =        LicenseClass["ClassName"].ToString();
                ClassDescription      =        LicenseClass["ClassDescription"].ToString();
                MinimumAllowedAge     = (byte) LicenseClass["MinimumAllowedAge"];
                DefaultValidityLength = (byte) LicenseClass["DefaultValidityLength"];
                ClassFees             = (float)(decimal)LicenseClass["ClassFees"];
                isFound = true;
            }
            return isFound;
        }

        public static bool GetLicenseClassInfoByClassName(string ClassName, 
            ref int LicenseClassID, ref string ClassDescription, 
            ref byte MinimumAllowedAge, ref byte DefaultValidityLength,
            ref float ClassFees)
        {
            bool isFound = false;

            string query = @"select * from LicenseClasses 
                                where ClassName = @ClassName";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@ClassName", ClassName);

            Dictionary<string, object> LicenseClass = clsDataAccessSettings.Find(command);

            if (LicenseClass.Keys.Count != 0)
            {
                LicenseClassID        = (int)  LicenseClass["LicenseClassID"];
                ClassDescription      =        LicenseClass["ClassDescription"].ToString();
                MinimumAllowedAge     = (byte) LicenseClass["MinimumAllowedAge"];
                DefaultValidityLength = (byte) LicenseClass["DefaultValidityLength"];
                ClassFees             = (float)(decimal)LicenseClass["ClassFees"];
                isFound = true;
            }
            return isFound;
        }

        public static DataTable GetAllLicenseClasses()
        {
            string query = @"select * From LicenseClasses";
            SqlCommand command = new SqlCommand(query);
            return clsDataAccessSettings.GetAllRecords(command);
        }

    }
}