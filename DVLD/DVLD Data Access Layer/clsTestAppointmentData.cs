using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DVLD_Data_Access_Layer
{
    public static class clsTestAppointmentData
    {
        public static bool Find(int TestAppointmentID, ref int TestTypeID,
                ref int LocalDrivingLicenseApplicationID, 
                ref DateTime AppointmentDate, ref double PaidFees,
                ref int CreatedByUserID, ref bool IsLocked, 
                ref int RetakeTestApplicationID)
        {
            bool isAppointmentFound = false;

            string query = @"SELECT * FROM TestAppointments where TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            Dictionary<string,object> Appointment = clsDataAccessSettings.Find(command);

            if (Appointment.Keys.Count != 0)
            {
                isAppointmentFound = true;
                TestTypeID                       = (int)Appointment["TestTypeID"];
                LocalDrivingLicenseApplicationID = (int)Appointment["LocalDrivingLicenseApplicationID"];
                AppointmentDate                  = (DateTime)Appointment["AppointmentDate"];
                PaidFees                         = (float)(decimal)Appointment["PaidFees"];
                CreatedByUserID                  = (int)Appointment["CreatedByUserID"];
                IsLocked                         = bool.Parse(Appointment["IsLocked"].ToString());
                RetakeTestApplicationID          = Appointment["RetakeTestApplicationID"] == null? -1 : (int) Appointment["RetakeTestApplicationID"];
            }
            return isAppointmentFound;
        }

        public static int AddNewTestAppointment(int TestTypeID,
                    int LocalDrivingLicenseApplicationID,
                    DateTime AppointmentDate, double PaidFees,
                    int CreatedByUserID, int RetakeTestApplicationID)
        {
            string query = @"Insert Into TestAppointments (TestTypeID,
                    LocalDrivingLicenseApplicationID,AppointmentDate, PaidFees,
            CreatedByUserID,IsLocked,RetakeTestApplicationID)
            values
                (@TestTypeID,@LocalDrivingLicenseApplicationID,@AppointmentDate,
                @PaidFees,@CreatedByUserID,@IsLocked,@RetakeTestApplicationID);
                Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", 0);

            if (RetakeTestApplicationID != -1)
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
            }
            else
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            }

            return clsDataAccessSettings.Insert(command);
        }

        public static bool UpdateTestAppointment(int TestAppointmentID,
            DateTime AppointmentDate)
        {
            string query = @"Update TestAppointments
                            set AppointmentDate = @AppointmentDate
                            where TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);

            return clsDataAccessSettings.Update(command);
        }

        public static DataTable GetTestAppointmentsList(
            int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            string query = @"select TestAppointmentID, AppointmentDate, PaidFees, IsLocked
                            FROM TestAppointments
                            where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                            and TestTypeID = @TestTypeID
                            Order By TestAppointmentID Desc";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            
            return clsDataAccessSettings.GetAllRecords(command);
        }

        public static int GetAsscociatedTestID(int TestAppointmentID)
        {
            string query = @"select TestID from Tests where 
                                TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            return clsDataAccessSettings.GetRecordID(command);
        }

        public static bool LockTestAppointment(int TestAppointmentID)
        {
            string query = @"Update TestAppointments
                                set IsLocked = 1
                                where TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            return clsDataAccessSettings.Update(command);
        }

    }
}