using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DVLD_Data_Access_Layer
{
    public static class clsApplicationData
    {
        public static bool FindApplicationByID(int ApplicationID, 
            ref int ApplicantPersonID, ref DateTime ApplicationDate,
            ref int ApplicationTypeID, ref short ApplicationStatus,
            ref DateTime ApplicationLastStatusDate, ref float PaidFees, 
            ref int CreatedByUserID)
        {
            bool IsApplicationFound = false;

            string query = @"select * from Applications 
                                where ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            Dictionary<string, object> Application = clsDataAccessSettings.Find(command);

            if (Application.Keys.Count != 0)
            {
                ApplicantPersonID         = (int)      Application["ApplicantPersonID"];
                ApplicationDate           = (DateTime) Application["ApplicationDate"];
                ApplicationTypeID         = (int)      Application["ApplicationTypeID"];
                ApplicationStatus         = (byte)     Application["ApplicationStatus"];
                ApplicationLastStatusDate = (DateTime) Application["LastStatusDate"];
                PaidFees                  = (float)(decimal)    Application["PaidFees"];
                CreatedByUserID           = (int)      Application["CreatedByUserID"];

                IsApplicationFound = true;
            }
            return IsApplicationFound;
        }

        public static int AddNewApplication(int ApplicantPersonID, 
            DateTime ApplicationDate, int ApplicationTypeID, 
            short ApplicationStatus, DateTime ApplicationLastStatusDate,
            double PaidFees, int CreatedByUserID)
        { 
            string query = @"INSERT INTO Applications (
                                ApplicantPersonID, ApplicationDate,
                                ApplicationTypeID, ApplicationStatus, 
                                LastStatusDate, PaidFees, CreatedByUserID)
                            VALUES (
                                @ApplicantPersonID, @ApplicationDate,
                                @ApplicationTypeID, @ApplicationStatus,
                                @LastStatusDate, @PaidFees, @CreatedByUserID);
                        SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", ApplicationLastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            return clsDataAccessSettings.Insert(command);
        }

        public static bool UpdateApplicationStatus(
            int ApplicationID, short ApplicationStatus)
        {
            string query = @"Update Applications 
                             SET 
                               ApplicationStatus = @ApplicationStatus,
                               LastStatusDate    = @LastStatusDate
                             where ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);

            return clsDataAccessSettings.Update(command);
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            string query = "delete from Applications where ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            return clsDataAccessSettings.Delete(command);
        }

    }
}