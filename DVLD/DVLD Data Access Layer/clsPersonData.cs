using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DVLD_Data_Access_Layer
{
    public static class clsPersonData
    {
        public static bool FindPersonByID(int PersonID, 
            ref string NationalNumber, ref string FirstName, 
            ref string SecondName, ref string ThirdName, 
            ref string LastName, ref DateTime DateOfBirth, 
            ref short Gender, ref string Address, ref string Phone, 
            ref string Email, ref int CountryID, ref string ImagePath)
        {
            bool IsPersonFound = false;

            string query = @"select * From People where PersonID = @PersonID ";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            Dictionary<string, object> Person = clsDataAccessSettings.Find(command);

            if (Person.Keys.Count != 0)
            {
                NationalNumber = (string)Person["NationalNo"];
                FirstName = (string)Person["FirstName"];
                SecondName = (string)Person["SecondName"];

                ThirdName = Person["ThirdName"] != null ?
                            Person["ThirdName"].ToString() : "";

                LastName = (string)Person["LastName"];
                Gender = short.Parse(Person["Gender"].ToString());
                DateOfBirth = (DateTime)Person["DateOfBirth"];
                Address = (string)Person["Address"];
                Phone = (string)Person["Phone"];

                Email = Person["Email"] != null ?
                        Person["Email"].ToString() : "";

                CountryID = (int)Person["NationalityCountryID"];

                ImagePath = Person["ImagePath"] != null ?
                            Person["ImagePath"].ToString() : "";

                IsPersonFound = true;
            }
            return IsPersonFound;
        }

        public static bool FindPersonByNationalNumber(string NationalNumber,
            ref int PersonID, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
            ref short Gender, ref string Address, ref string Phone,
            ref string Email, ref int CountryID, ref string ImagePath)
        {
            bool IsPersonFound = false;

            string query = @"select * From People where NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@NationalNo", NationalNumber);

            Dictionary<string, object> Person = clsDataAccessSettings.Find(command);

            if (Person.Keys.Count != 0)
            {
                PersonID    = (int)Person["PersonID"];
                FirstName   = (string)Person["FirstName"];
                SecondName  = (string)Person["SecondName"];

                ThirdName   = Person["ThirdName"] != null ?
                              Person["ThirdName"].ToString() : "";

                LastName    = (string)Person["LastName"];
                Gender      = short.Parse(Person["Gender"].ToString());
                DateOfBirth = (DateTime)Person["DateOfBirth"];
                Address     = (string)Person["Address"];
                Phone       = (string)Person["Phone"];
                Email       = Person["Email"] != null ?
                              Person["Email"].ToString() : "";

                CountryID   = (int)Person["NationalityCountryID"];

                ImagePath   = Person["ImagePath"] != null ?
                              Person["ImagePath"].ToString() : "";

                IsPersonFound = true;
            }
            return IsPersonFound;
        }

        public static int AddNewPerson(string NationalNumber, string FirstName,
            string SecondName, string ThirdName, string LastName,
            DateTime DateOfBirth, short Gender, string Address, string Phone,
            string Email, string ImagePath, int CountryID)
        {
            string query = @"INSERT INTO People (
                            NationalNo, FirstName, SecondName, ThirdName,
                            LastName, DateOfBirth, Gender, Address, Phone, Email,
                            NationalityCountryID, ImagePath)
                            VALUES (
                                @NationalNo, @FirstName, @SecondName, @ThirdName,
                                @LastName, @DateOfBirth, @Gender, @Address, @Phone, 
                                @Email, @NationalityCountryID, @ImagePath);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@NationalNo", NationalNumber);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@NationalityCountryID", CountryID);

            if (ThirdName == string.Empty || ThirdName == null)
            {
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            }

            if (Email == string.Empty || Email == null)
            {
                command.Parameters.AddWithValue("@Email", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Email", Email);
            }

            if (ImagePath == string.Empty || ImagePath == null)
            {
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }

            return clsDataAccessSettings.Insert(command);
        }

        public static bool UpdatePerson(int PersonID, string NationalNumber,
            string FirstName, string SecondName, string ThirdName,
            string LastName, DateTime DateOfBirth, short Gender, string Address,
            string Phone, string Email, string ImagePath, int CountryID)
        {
            string query = @"Update People 
                             SET 
                               NationalNo           = @NationalNo,
                               FirstName            = @FirstName,
                               SecondName           = @SecondName,
                               ThirdName            = @ThirdName, 
                               LastName             = @LastName, 
                               DateOfBirth          = @DateOfBirth, 
                               Gender	            = @Gender,
                               Address              = @Address,
                               Phone                = @Phone,
                               Email                = @Email,
                               NationalityCountryID = @NationalityCountryID,
                               ImagePath            = @ImagePath
                             where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNumber);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
                command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@NationalityCountryID", CountryID);

            if (ThirdName == string.Empty || ThirdName == null)
            {
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            }

            if (Email == string.Empty || Email == null)
            {
                command.Parameters.AddWithValue("@Email", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Email", Email);
            }

            if (ImagePath == string.Empty || ImagePath == string.Empty)
            {
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }

            return clsDataAccessSettings.Update(command);
        }

        public static bool DeletePerson(int PersonID)
        {
            string query = "Delete from People where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            return clsDataAccessSettings.Delete(command);
        }

        public static DataTable GetAllPeople()
        {
            string query = @"select * from PeopleDataWithCountryName_View order by FirstName";
            SqlCommand command = new SqlCommand(query);
            return clsDataAccessSettings.GetAllRecords(command);
        }

        public static bool IsPersonExist(string NationalNumber)
        {
            string query = @"select found = 1 from People 
                                where NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@NationalNo", NationalNumber);

            return clsDataAccessSettings.IsRecordExist(command);
        }

    }
}