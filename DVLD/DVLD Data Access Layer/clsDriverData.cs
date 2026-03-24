using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DVLD_Data_Access_Layer
{
    public static class clsDriverData
    {
        public static bool FindDriverByID(int DriverID, ref int PersonID,
            ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isDriverFound = false;
            string query = @"select * From Drivers where DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            Dictionary<string,object> Driver = clsDataAccessSettings.Find(command);

            if (Driver.Keys.Count != 0)
            {
                PersonID = int.Parse(Driver["PersonID"].ToString());
                CreatedByUserID = int.Parse(Driver["CreatedByUserID"].ToString());
                CreatedDate = DateTime.Parse(Driver["CreatedDate"].ToString());

                isDriverFound = true;
            }
            return isDriverFound;
        }

        public static bool FindDriverByPersonID(int PersonID, ref int DriverID,
            ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isDriverFound = false;
            string query = @"select * From Drivers where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            Dictionary<string,object> Driver = clsDataAccessSettings.Find(command);

            if (Driver.Keys.Count != 0)
            {
                DriverID = int.Parse(Driver["DriverID"].ToString());
                CreatedByUserID = int.Parse(Driver["CreatedByUserID"].ToString());
                CreatedDate = DateTime.Parse(Driver["CreatedDate"].ToString());

                isDriverFound = true;
            }
            return isDriverFound;
        }

        public static int AddNewDriver(int PersonID, int CreatedByUserID)
        {
            string query = @"INSERT INTO Drivers(PersonID, CreatedByUserID, CreatedDate)
                            VALUES(@PersonID, @CreatedByUserID, @CreatedDate);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", DateTime.Now.Date);

            return clsDataAccessSettings.Insert(command);
        }

        public static DataTable GetAllDrivers()
        {
            string query = @"select * from DriversDataWithActiveLicensesCount_View";
            SqlCommand command = new SqlCommand(query);
            return clsDataAccessSettings.GetAllRecords(command);
        }
    }
}