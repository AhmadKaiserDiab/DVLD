using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DVLD_Data_Access_Layer
{
    public static class clsLicenseData
    {
        public static bool Find(int LicenseID, ref int ApplicationID,
                    ref int DriverID, ref int LicenseClassID,
                    ref DateTime IssueDate, ref DateTime ExpirationDate,
                    ref string Notes, ref float PaidFees, ref bool IsActive,
                    ref int CreatedByUserID, ref int IssueReason)
        {
            bool isLicenseFound = false;

            string query = "select * from Licenses where LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            Dictionary<string, object> License = clsDataAccessSettings.Find(command);

            if (License.Keys.Count != 0)
            {
                isLicenseFound = true;

                ApplicationID = (int)License["ApplicationID"];
                DriverID = (int)License["DriverID"];
                LicenseClassID = (int)License["LicenseClass"];
                IssueDate = (DateTime)License["IssueDate"];
                ExpirationDate = (DateTime)License["ExpirationDate"];
                Notes = License["Notes"] == null ? "" : License["Notes"].ToString();
                PaidFees = (float)((decimal)License["PaidFees"]);
                IsActive = (bool)License["IsActive"];
                CreatedByUserID = (int)License["CreatedByUserID"];
                IssueReason = (byte)License["IssueReason"];
            }
            return isLicenseFound;
        }

        public static int AddNewLicense(int ApplicationID, int DriverID,
            int LicenseClassID, DateTime IssueDate, DateTime ExpirationDate,
            string Notes, double PaidFees, bool IsActive, int IssueReason,
            int UserID)
        {
            string query = @"INSERT INTO Licenses
                            (
                                ApplicationID, DriverID, LicenseClass, IssueDate,
                                ExpirationDate, Notes, PaidFees, IsActive,
                                IssueReason, CreatedByUserID
                            )
                            VALUES
                            (
                                @ApplicationID, @DriverID, @LicenseClass, 
                                @IssueDate, @ExpirationDate, @Notes, 
                                @PaidFees, @IsActive, @IssueReason, 
                                @CreatedByUserID
                            );
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", UserID);

            if (Notes == string.Empty)
            {
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Notes", Notes);
            }

            return clsDataAccessSettings.Insert(command);
        }

        public static bool DeActivateLicense(int LicenseID)
        {
            string query = @"UPDATE Licenses
                                set IsActive = 0
                                WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            return clsDataAccessSettings.Update(command);
        }

        public static int GetLicenseID(int PersonID, int SelectedLicenseClassID)
        {
            string query = @"select top(1) L.LicenseID 
                                from Licenses L
                                join Drivers D 
                                    on L.DriverID = D.DriverID
                                where D.PersonID = @PersonID 
                                  and L.LicenseClass = @LicenseClassID
                                order by IssueDate Desc";
            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", SelectedLicenseClassID);

            return clsDataAccessSettings.GetRecordID(command);
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            string query = @" select L.LicenseID, L.ApplicationID, LC.ClassName,
                                    L.IssueDate, L.ExpirationDate, L.IsActive
                                from Licenses L
                                join LicenseClasses LC
                                on L.LicenseClass = LC.LicenseClassID
                                where DriverID = @DriverID 
                                order by L.IsActive Desc";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            return clsDataAccessSettings.GetAllRecords(command);
        }
    }
}