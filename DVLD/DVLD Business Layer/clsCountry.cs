using System.Data;
using DVLD_Data_Access_Layer;

namespace DVLD_Business_Layer
{
    public class clsCountry
    {
        public int CountryID { get; set; }
        public string CountryName { get; set;}
        
        private clsCountry(int CountryID, string CountryName)
        {
            this.CountryID   = CountryID;
            this.CountryName = CountryName;
        }

        public clsCountry()
        {
            CountryID   = -1;
            CountryName = "";
        }

        public static clsCountry Find(int CountryID)
        {
            string CountryName = "";

            if (clsCountryData.Find(CountryID, ref CountryName))
            {
                return new clsCountry(CountryID, CountryName);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }

    }
}
