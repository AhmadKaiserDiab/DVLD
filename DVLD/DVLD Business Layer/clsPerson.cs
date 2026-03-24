using System;
using System.Data;
using DVLD_Data_Access_Layer;

namespace DVLD_Business_Layer
{
    public class clsPerson
    {
        enum enMode { Update = 0, AddNew = 1 };

        enMode _Mode;
        public int PersonID { private set; get; }
        public string NationalNumber { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public DateTime DateOfBirth { set; get; }
        public short Gender { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public string ImagePath { set; get; }
        public int NationalityCountryID { set; get; }
        public clsCountry Country { private set; get; }
        public string FullName
        {
            get
            {
                return FirstName + ' ' + SecondName + ' '
                + (ThirdName != string.Empty ? (ThirdName) : "")
                + ' ' + LastName.ToString().Trim();
            }
        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(NationalNumber,
                FirstName, SecondName, ThirdName, LastName, DateOfBirth,
                Gender, Address, Phone, Email, ImagePath, NationalityCountryID);
            return (PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(PersonID, NationalNumber,
                FirstName, SecondName, ThirdName, LastName,
                DateOfBirth, Gender, Address, Phone,
                Email, ImagePath, Country.CountryID);
        }

        private clsPerson(int PersonID, string NationalNumber, string FirstName,
            string SecondName, string ThirdName, string LastName, 
            DateTime DateOfBirth, short Gender, string Address, string Phone,
            string Email, int NationalityCountryID, string ImagePath)
        {
            _Mode = enMode.Update;
            this.PersonID = PersonID;
            this.NationalNumber = NationalNumber;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.Country = clsCountry.Find(NationalityCountryID);
            this.ImagePath = ImagePath;
        }

        public clsPerson()
        {
            _Mode                     = enMode.AddNew;
            this.PersonID             = -1;
            this.NationalNumber       = "";
            this.FirstName            = "";
            this.SecondName           = "";
            this.ThirdName            = "";
            this.LastName             = "";
            this.DateOfBirth          = DateTime.Now.AddYears(-18);
            this.Gender               = 0;
            this.Address              = "";
            this.Phone                = "";
            this.Email                = "";
            this.NationalityCountryID = -1;
            this.ImagePath            = "";
            this.Country              = new clsCountry();
        }

        public static clsPerson Find(int PersonID)
        {
            string NationalNumber = "", FirstName = "", SecondName = "",
                ThirdName = "", LastName = "", Address = "", Phone = "",
                Email = "", Image = "";
            DateTime DateOfBirth = DateTime.Now;
            short Gender = 0;
            int NationalityCountryID = -1;

            bool IsPersonFound = clsPersonData.FindPersonByID(PersonID,
                ref NationalNumber, ref FirstName, ref SecondName, ref ThirdName,
                ref LastName, ref DateOfBirth, ref Gender, ref Address, ref Phone,
                ref Email, ref NationalityCountryID, ref Image);

            if (IsPersonFound)
            {
                return new clsPerson(PersonID, NationalNumber, FirstName,
                    SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, 
                    Phone, Email, NationalityCountryID, Image);
            }
            else
            {
                return null;
            }
        }

        public static clsPerson Find(string NationalNumber)
        {
            int PersonID = -1, NationalityCountryID = -1;
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "",
                Address = "", Phone = "", Email = "", Image = "";
            DateTime DateOfBirth = DateTime.Now;
            short Gender = 0;

            bool IsPersonFound = clsPersonData
                .FindPersonByNationalNumber(NationalNumber, ref PersonID,
                ref FirstName, ref SecondName, ref ThirdName, ref LastName,
                ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email,
                ref NationalityCountryID, ref Image);

            if (IsPersonFound)
            {
                return new clsPerson(PersonID, NationalNumber, FirstName, 
                    SecondName, ThirdName, LastName, DateOfBirth, Gender, Address,
                    Phone, Email, NationalityCountryID, Image);
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
                        if (_AddNewPerson())
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
                        return _UpdatePerson();
                    }
                default:
                    return false;
            }
        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }

        public static bool IsPersonExist(string NationalNumber)
        {
            return clsPersonData.IsPersonExist(NationalNumber);
        }

    }
}
