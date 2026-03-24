using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_Data_Access_Layer
{
    public static class clsApplicationTypeData
    {
        public static bool FindApplicationTypeByID(int ApplicationTypeID,
            ref string ApplicationTypeTitle, ref float ApplicationFees)
        {
            bool isApplicationFound = false;

            string query = @"SELECT * FROM ApplicationTypes where ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            Dictionary <string, object> Application = clsDataAccessSettings.Find(command);

            if (Application.Keys.Count != 0)
            {
                isApplicationFound = true;
                ApplicationTypeTitle = Application["ApplicationTypeTitle"].ToString();
                ApplicationFees = (float)(decimal)Application["ApplicationFees"];
            }
            return isApplicationFound;
        }

        public static int AddNewApplicationType(string Title, float Fees)
        {
            string query = @"Insert Into ApplicationTypes (
                                ApplicationTypeTitle, ApplicationFees)
                                Values (@Title,@Fees)                
                                SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", Title);
            command.Parameters.AddWithValue("@ApplicationFees", Fees);
            
            return clsDataAccessSettings.Insert(command);
        }

        public static bool UpdateApplicationType(int ApplicationTypeID,
            string ApplicationTypeTitle, float ApplicationFees)
        {
            string query = @"Update ApplicationTypes
                                    set 
                                    ApplicationTypeTitle    = @ApplicationTypeTitle,
                                    ApplicationFees         = @ApplicationFees
                                    where ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);

            return clsDataAccessSettings.Update(command);
        }

        public static DataTable GetAllApplicationTypes()
        {
             string query = @"SELECT ApplicationTypeID, ApplicationTypeTitle,
                                ApplicationFees FROM ApplicationTypes";

            SqlCommand command = new SqlCommand(query);
            return clsDataAccessSettings.GetAllRecords(command);
        }
    }
}