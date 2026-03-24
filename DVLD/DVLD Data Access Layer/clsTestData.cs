using System;
using System.Data.SqlClient;

namespace DVLD_Data_Access_Layer
{
    public static class clsTestData
    {
        public static int AddNewTest(int TestAppointmentID, bool Result,
            string Notes, int UserID)
        {
            string query = @"Insert Into Tests (
                                    TestAppointmentID, TestResult,
                                    Notes, CreatedByUserID)
                                Values(
                                    @TestAppointmentID, @TestResult, @Notes,
                                    @CreatedByUserID);
                                SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@TestAppointmentID",TestAppointmentID);

            if(Result)
            {
                command.Parameters.AddWithValue("@TestResult",1);
            }
            else
            {
                command.Parameters.AddWithValue("@TestResult",0);
            }
            
            if(Notes == string.Empty)
            {
                command.Parameters.AddWithValue("@Notes",DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Notes",Notes);
            }
            command.Parameters.AddWithValue("@CreatedByUserID",UserID);

            return clsDataAccessSettings.Insert(command);
        }   
    }
}
