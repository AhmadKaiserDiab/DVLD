using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DVLD_Data_Access_Layer
{
    public static class clsDetainedLicenseData
    {
        public static bool FindByLicenseID(int LicenseID, ref int DetainID,
            ref DateTime DetainDate, ref float FineFees, ref int CreatedByUserID,
            ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID,
            ref int ReleaseApplicationID)
        {
            bool isFound = false;

            string query = @"select top 1 * From DetainedLicenses 
                                where LicenseID = @LicenseID 
                                order by DetainID desc";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            Dictionary<string, object> DetainedLicense = clsDataAccessSettings.Find(command);

            if (DetainedLicense.Keys.Count > 0)
            {
                DetainID = (int)DetainedLicense["DetainID"];
                DetainDate = (DateTime)DetainedLicense["DetainDate"];
                FineFees = (float)(decimal)DetainedLicense["FineFees"];
                CreatedByUserID = (int)DetainedLicense["CreatedByUserID"];
                IsReleased = (bool)DetainedLicense["IsReleased"];
                ReleaseDate = DetainedLicense["ReleaseDate"] == null ? DateTime.MaxValue : (DateTime)DetainedLicense["ReleaseDate"];
                ReleasedByUserID = DetainedLicense["ReleasedByUserID"] == null ? -1 : (int)DetainedLicense["ReleasedByUserID"];
                ReleaseApplicationID = DetainedLicense["ReleaseApplicationID"] == null ? -1 : (int)DetainedLicense["ReleaseApplicationID"];

                isFound = true;
            }
            return isFound;
        }

        public static int DetainLicense(int LicenseID, float FineFees,
            int CreatedByUserID)
        {
            string query = @"INSERT INTO DetainedLicenses
                                           (
                                            LicenseID,
                                            DetainDate,
                                            FineFees,
                                            CreatedByUserID,
                                            IsReleased
                                           )
                                     VALUES(
                                            @LicenseID,
                                            @DetainDate,
                                            @FineFees,
                                            @CreatedByUserID,
                                            @IsReleased
                                            );
                            
                                SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DateTime.Now);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", 0);

            return clsDataAccessSettings.Insert(command);

        }

        public static bool ReleaseDetainedLicense(int DetainID,
            int ReleasedByUserID, int ReleaseApplicationID)
        {
            string query = @"Update DetainedLicenses
                                    Set IsReleased = 1,                            
                                        ReleaseDate = @ReleaseDate,
                                        ReleasedByUserID = @ReleasedByUserID,
                                        ReleaseApplicationID = @ReleaseApplicationID
                                        where DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

            return clsDataAccessSettings.Update(command);

        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            string query = @"select IsDetained=1 
                            from detainedLicenses 
                            where 
                            LicenseID=@LicenseID 
                            and IsReleased=0;";

            SqlCommand commnad = new SqlCommand(query);
            commnad.Parameters.AddWithValue("@LicenseID", LicenseID);
            return clsDataAccessSettings.IsRecordExist(commnad);
        }

        public static DataTable GetAllDetainedLicenses()
        {
            string query = @"select * from DetainedLicenses_View";
            SqlCommand command = new SqlCommand(query);
            return clsDataAccessSettings.GetAllRecords(command);
        }
    }
}