using System.Data;
using DVLD_Data_Access_Layer;

namespace DVLD_Business_Layer
{
    public class clsUser
    {
        enum enMode { Update = 0, AddNew = 1 };
        enMode _Mode;

        public int UserID { set; get; }
        public int PersonID { set; get; }
        public clsPerson PersonInfo;
        public string UserName { set; get; }
        public string PassWord { set; get; }
        public bool IsActive { set; get; }

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(PersonID, UserName, PassWord,
                IsActive);
            return (UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(UserID, UserName, PassWord, IsActive);
        }

        private clsUser(int UserID, int PersonID, string UserName, string PassWord,
            bool IsActive)
        {
            _Mode = enMode.Update;
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.PassWord = PassWord;
            this.IsActive = IsActive;
            this.PersonInfo = clsPerson.Find(PersonID);
        }

        public clsUser()
        {
            _Mode           = enMode.AddNew;
            this.UserID     = -1;
            this.PersonID   = -1;
            this.UserName   = "";
            this.PassWord   = "";
            this.IsActive   = true;
            this.PersonInfo = new clsPerson();
        }

        public static clsUser FindUserByID(int UserID)
        {
            string UserName = "", PassWord = "";
            bool IsActive = false;
            int PersonID = -1;

            if (clsUserData.FindUserByID(UserID, ref PersonID, ref UserName,
                ref PassWord, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, PassWord, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUser VerifyUserLogin(string UserName, string PassWord)
        {
            int UserID = -1;
            bool IsActive = false;
            int PersonID = -1;

            if (clsUserData.FindUserByUsernameAndPassword(UserName,
                PassWord, ref UserID, ref PersonID, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, PassWord, IsActive);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewUser())
                        {
                            _Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case enMode.Update:
                    {
                        return _UpdateUser();
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        public bool ChangePassWord()
        {
            return clsUserData.ChangePassWord(UserID, PassWord);
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static bool IsUserExist(int PersonID)
        {
            return clsUserData.IsUserExist(PersonID);
        }

        public static bool IsUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }

        public static bool DeActivateUser(string UserName)
        {
            return clsUserData.DeActivateUser(UserName);
        }

    }
}