using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DVLD_Data_Access_Layer
{
    public static class clsLocalDrivingLicenseApplicationData
    {
        public static bool FindByLocalDrivingAppLicenseID(
            int LocalDrivingLicenseApplicationID, ref int ApplicationID,
            ref int LicenseClassID)
        {
            bool IsApplicationFound = false;

            string query = @"select * From LocalDrivingLicenseApplications 
                                where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID ";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            Dictionary<string, object> Application = clsDataAccessSettings.Find(command);
            if (Application.Keys.Count != 0)
            {
                ApplicationID  = (int)Application["ApplicationID"];
                LicenseClassID = (int)Application["LicenseClassID"];

                IsApplicationFound = true;
            }
            return IsApplicationFound;
        }

        public static bool GetLocalDrivingLicenseApplicationInfoByApplicationID(
            int ApplicationID, ref int LocalDrivingLicenseApplicationID,
            ref int LicenseClassID)
        {
            bool isFound = false;

            string query = @"SELECT * FROM LocalDrivingLicenseApplications 
                                WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            Dictionary<string, object> Application = clsDataAccessSettings.Find(command);

            if (Application.Keys.Count != 0)
            {
                LocalDrivingLicenseApplicationID = (int)Application["LocalDrivingLicenseApplicationID"];
                LicenseClassID = (int) Application["LicenseClassID"];
                isFound = true;
            }
            return isFound;
        }

        public static int AddNewLocalDrivingLicenseApplication(int ApplicationID,
            int LicenseClassID)
        {
            string Query = @"INSERT INTO LocalDrivingLicenseApplications 
                                    (ApplicationID, LicenseClassID)
                                    VALUES(@ApplicationID, @LicenseClassID)
                                    SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            return clsDataAccessSettings.Insert(command);
        }

        public static bool UpdateLocalDrivingLicenseApplication(
            int LocalDrivingLicenseApplicationID, int LicenseClassID)
        {
            string Query = @"UPDATE LocalDrivingLicenseApplications
                            SET 
                                LicenseClassID = @LicenseClassID
                            WHERE 
                                LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(Query);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            return clsDataAccessSettings.Update(command);
        }

        public static bool Delete(int LocalDrivingLicenseApplicationID)
        {
            string Query = @"delete from LocalDrivingLicenseApplications
                                WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(Query);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            return clsDataAccessSettings.Delete(command);
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            string query = @"select * from LocalDrivingLicenseApplications_View
                                    order by ApplicationDate Desc";
            SqlCommand command = new SqlCommand(query);
            return clsDataAccessSettings.GetAllRecords(command);
        }

        public static int GetActiveLocalDrivingLicenseApplicationID(int PersonID, 
            int LicenseClassID)
        {
            string query = @"select A.ApplicationID from Applications A 
                                join LocalDrivingLicenseApplications LDLA 
                                	on LDLA.ApplicationID = A.ApplicationID
                                join LicenseClasses LC
                                	on LC.LicenseClassID = LDLA.LicenseClassID
                                where A.ApplicantPersonID = @PersonID and
                                      ApplicationStatus = 1 and
                                      LC.LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            return clsDataAccessSettings.GetRecordID(command);
        }

        public static byte GetPassedTestsCount(int LocalDrivingLicenseApplicationID)
        {
            string query = @"select count(*) from
                                    TestAppointments TA
                                join Tests T 
                                    on T.TestAppointmentID = TA.TestAppointmentID
                                where 
		                            TA.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
		                        and 
		                            T.TestResult = 1";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            return (byte)clsDataAccessSettings.GetCount(command);
        }

        public static int GetLicenseID(int ApplicationID)
        {
            string query = "select LicenseID from Licenses where ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            return clsDataAccessSettings.GetRecordID(command);
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            string query = @"SELECT found=1
                    FROM LocalDrivingLicenseApplications 
                    INNER JOIN TestAppointments 
                    ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
                    WHERE
                    (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)  
                    AND(TestAppointments.TestTypeID = @TestTypeID) and IsLocked=0";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            return clsDataAccessSettings.IsRecordExist(command);
        }

        public static byte GetTotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            string query = @"Select Count(*) from TestAppointments
                    where TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                    and TestAppointments.TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            return (byte)clsDataAccessSettings.GetCount(command);
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            string query = @"select found = 1 from TestAppointments join Tests
                            on Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                            where TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                            and TestAppointments.TestTypeID = @TestTypeID
                            and Tests.TestResult = 1";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            return clsDataAccessSettings.IsRecordExist(command);
        }

        public static bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            string query = @"select found = 1 from TestAppointments join Tests
                            on Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                            where TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                            and TestAppointments.TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            return clsDataAccessSettings.IsRecordExist(command);
        }
    }
}
