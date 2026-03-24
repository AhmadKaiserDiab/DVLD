using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DVLD_Data_Access_Layer
{
    public static class clsTestTypeData
    {
        public static bool FindTestType(int TestTypeID, ref string TestTypeTitle,
            ref string TestTypeDescription, ref float TestTypeFees)
        {
            bool isTestTypeFound = false;
            string query = @"SELECT * FROM TestTypes where TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            Dictionary<String, object> TestType = clsDataAccessSettings.Find(command);

            if (TestType.Keys.Count != 0)
            {
                isTestTypeFound = true;
                TestTypeTitle = TestType["TestTypeTitle"].ToString();
                TestTypeDescription = TestType["TestTypeDescription"].ToString();
                TestTypeFees = (float)(decimal)TestType["TestTypeFees"];
            }
            return isTestTypeFound;
        }

        public static int AddNewTestType(string Title, string Description, float Fees)
        {
            string query = @"Insert Into TestTypes (TestTypeTitle, 
                                        TestTypeTitle, TestTypeFees)
                                Values (@TestTypeTitle, 
                                    @TestTypeDescription, @ApplicationFees)
                                where TestTypeID = @TestTypeID;
                                SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@TestTypeTitle", Title);
            command.Parameters.AddWithValue("@TestTypeDescription", Description);
            command.Parameters.AddWithValue("@ApplicationFees", Fees);
            
            return clsDataAccessSettings.Insert(command);
        }

        public static bool UpdateTestType(int TestTypeID, string TestTypeTitle,
            string TestTypeDescription, float TestTypeFees)
        {
            string query = @"Update TestTypes
                            set 
                            TestTypeTitle       = @TestTypeTitle,
                            TestTypeDescription = @TestTypeDescription,
                            TestTypeFees        = @TestTypeFees
                            where TestTypeID    = @TestTypeID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);

            return clsDataAccessSettings.Update(command);
        }

        public static DataTable GetAllTestTypes()
        {
            string query = @"SELECT * FROM TestTypes order by TestTypeID";
            SqlCommand command = new SqlCommand(query);
            return clsDataAccessSettings.GetAllRecords(command);
        }
    }
}