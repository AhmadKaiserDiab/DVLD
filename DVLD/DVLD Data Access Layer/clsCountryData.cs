using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DVLD_Data_Access_Layer
{
    public static class clsCountryData
    {
        public static bool Find(int CountryID, ref string CountryName)
        {
            bool IsCountryFound = false;

            string query = @"Select * From Countries 
                                where CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            Dictionary<string, object> Country = clsDataAccessSettings.Find(command);

            if (Country.Keys.Count != 0)
            {
                CountryName = (string)Country["CountryName"];
                IsCountryFound = true;
            }

            return IsCountryFound;
        }

        public static DataTable GetAllCountries()
        {
            string query = "select * from Countries";
            SqlCommand command = new SqlCommand(query);
            return clsDataAccessSettings.GetAllRecords(command) ;
        }
    }
}
