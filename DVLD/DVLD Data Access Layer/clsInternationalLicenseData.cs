using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DVLD_Data_Access_Layer
{
    public class clsInternationalLicenseData
    {
        public static bool Find(int InternationalLicenseID, ref int ApplicationID, 
            ref int DriverID, ref int IssuedUsingLocalLicenseID,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive,
            ref int CreatedByUserID)
        {
            bool isLicenseFound = false;

            string query = "Select * From InternationalLicenses where InternationalLicenseID = @InternationalLicenseID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            Dictionary<string, object> License = clsDataAccessSettings.Find(command);

            if (License.Keys.Count != 0)
            {
                isLicenseFound = true;

                ApplicationID = (int)License["ApplicationID"];
                DriverID = (int)License["DriverID"];
                IssuedUsingLocalLicenseID = (int)License["IssuedUsingLocalLicenseID"];
                IssueDate = (DateTime)License["IssueDate"];
                ExpirationDate = (DateTime)License["ExpirationDate"];
                IsActive = (bool)License["IsActive"];
                CreatedByUserID = (int)License["CreatedByUserID"];
            }
            return isLicenseFound;
        }

        public static int AddNewInternationalLicense(int ApplicationID,
            int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate,
            DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            string query = @"Update InternationalLicenses 
                               set IsActive=0
                               where DriverID=@DriverID;

                            INSERT INTO InternationalLicenses
                            (
                                ApplicationID, DriverID, IssuedUsingLocalLicenseID,
                                IssueDate, ExpirationDate, IsActive,
                                CreatedByUserID
                            )
                            VALUES
                            (
                                @ApplicationID, @DriverID, 
                                @IssuedUsingLocalLicenseID, @IssueDate,
                                @ExpirationDate, @IsActive, @CreatedByUserID
                            );

                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            return clsDataAccessSettings.Insert(command);
        }

        public static bool DeActivateLicense(int LicenseID)
        {
            string query = @"UPDATE InternationalLicenses
                                set IsActive = 0
                                WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@InternationalLicenseID", LicenseID);

            return clsDataAccessSettings.Update(command);
        }

        public static int GetActiveLicenseID(int DriverID)
        {
            string query = @"SELECT Top 1 InternationalLicenseID
                                FROM InternationalLicenses 
                                where DriverID=@DriverID and GetDate() between IssueDate and ExpirationDate 
                                order by ExpirationDate Desc;";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            return clsDataAccessSettings.GetRecordID(command);
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            string query = @"select InternationalLicenseID, ApplicationID,
                                    IssuedUsingLocalLicenseID, IssueDate,
                                    ExpirationDate, IsActive from 
                                    InternationalLicenses 
                                where DriverID = @DriverID 
                                ORDER BY IsActive DESC , 
                                         InternationalLicenseID DESC";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            return clsDataAccessSettings.GetAllRecords(command);
        }

        public static DataTable GetAllInternationalLicenses()
        {
            string query = @"select InternationalLicenseID, ApplicationID,
                                    DriverID, IssuedUsingLocalLicenseID, 
                                    IssueDate, ExpirationDate, IsActive 
                                    from InternationalLicenses 
                                ORDER BY InternationalLicenseID";
            SqlCommand command = new SqlCommand(query);

            return clsDataAccessSettings.GetAllRecords(command);
        }
    }
}