using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DVLD_Data_Access_Layer
{
    public static class clsUserData
    {
        public static bool FindUserByID(int UserID, ref int PersonID,
            ref string UserName, ref string PassWord, ref bool IsActive)
        {
            bool IsUserFound = false;
            
            string query = @"select * From Users where UserID = @UserID";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@UserID", UserID);

            Dictionary<string, object> User = clsDataAccessSettings.Find(command);

            if (User.Keys.Count != 0)
            {
                PersonID = (int)User["PersonID"];
                UserName = User["UserName"].ToString();
                PassWord = User["Password"].ToString();
                IsActive = (bool)User["IsActive"];
                IsUserFound = true;
            }
            return IsUserFound;
        }

        public static bool FindUserByUsernameAndPassword(string UserName,
            string PassWord, ref int UserID, ref int PersonID,
            ref bool IsActive)
        {
            bool IsUserFound = false;

            string query = @"select * From Users where
                                UserName = @UserName and @PassWord = @PassWord";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@PassWord", PassWord);

            Dictionary<string, object> User = clsDataAccessSettings.Find(command);

            if (User.Keys.Count != 0)
            {
                UserID = (int)User["UserID"];
                PersonID = (int)User["PersonID"];
                IsActive = (bool)User["IsActive"];
                IsUserFound = true;
            }
            return IsUserFound;
        }

        public static int AddNewUser(int PersonID, string UserName, 
            string Password, bool IsActive)
        {
            string query = @"INSERT INTO Users(
                                    PersonID, UserName, Password, IsActive)
                            VALUES(
                                    @PersonID, @UserName, @Password, @IsActive);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);

            if (IsActive)
            {
                command.Parameters.AddWithValue("@IsActive", 1);
            }
            else
            {
                command.Parameters.AddWithValue("@IsActive", 0);
            }
            return clsDataAccessSettings.Insert(command);
        }

        public static bool UpdateUser(int UserID, string UserName,
            string Password, bool IsActive)
        {
            string query = @"Update Users 
                             SET 
                               UserName   = @UserName,
                               Password   = @Password,
                               IsActive   = @IsActive
                             where UserID = @UserID";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            
            if (IsActive)
            {
                command.Parameters.AddWithValue("@IsActive", 1);
            }
            else
            {
                command.Parameters.AddWithValue("@IsActive", 0);
            }
            return clsDataAccessSettings.Update(command);
        }

        public static bool ChangePassWord(int UserID, string Password)
        {
            string query = @"Update Users 
                             SET Password = @Password
                             where UserID = @UserID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@Password", Password);
            return clsDataAccessSettings.Update(command);            
        }

        public static bool DeleteUser(int UserID)
        {
            string query = "Delete from Users where UserID = @UserID";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@UserID", UserID);

            return clsDataAccessSettings.Delete(command);
        }

        public static DataTable GetAllUsers()
        {            
            string query = @"SELECT U.UserID, P.PersonID, 
                            FullName = P.FirstName + ' ' + P.SecondName + ' ' 
                            + ISNULL(P.ThirdName, '') + ' ' + P.LastName,
                            U.UserName, U.IsActive FROM People AS P 
                            INNER JOIN Users AS U ON P.PersonID = U.PersonID";
            SqlCommand command = new SqlCommand(query);
            return clsDataAccessSettings.GetAllRecords(command);
        }

        public static bool IsUserExist(int PersonID)
        {
            string query = @"select found = 1 from Users 
                                    where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            return clsDataAccessSettings.IsRecordExist(command);
        }

        public static bool IsUserExist(string UserName)
        { 
            string query = @"select found = 1 from Users 
                                where UserName = @UserName";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@UserName", UserName);

            return clsDataAccessSettings.IsRecordExist(command);
        }

        public static bool DeActivateUser(string UserName)
        {
            string query = @"Update Users 
                             SET IsActive   = 0,
                             where UserName = @UserName";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@UserName", UserName);
            return clsDataAccessSettings.Update(command);
        }
    }
}