using System.Text.RegularExpressions;

namespace DVLD.Global_Classes
{
    internal static class clsValidation
    {
        public static bool ValidateName(string Name)
        {
            string Pattern1 = @"^[A-Za-z]+$";
            string Pattern2 = @"^[A-Za-z]+[-][A-Za-z]+$";
            return Regex.IsMatch(Name, Pattern1) || Regex.IsMatch(Name, Pattern2);
        }

        public static bool ValidateEmail(string EmailAddress)
        {
            string Pattern = @"^[A-Za-z]+[@][A-Za-z]+.com$";
            return Regex.IsMatch(EmailAddress, Pattern);
        }

        public static bool ValidatePhone(string Phone)
        {
            string Pattern = @"^[0-9]{4-15}$";
            return Regex.IsMatch(Phone, Pattern);
        }

        public static bool ValidateInteger(string Number)
        {
            string Pattern = @"^[1-9][0-9]*$";
            return Regex.IsMatch(Number, Pattern);
        }

        public static bool ValidateFloat(string Number)
        {
            string Pattern = @"^[1-9][0-9]?[.][0-9]?";
            return Regex.IsMatch(Number, Pattern);
        }

        public static bool IsNumber(string Number)
        {
            return ValidateInteger(Number) || ValidateFloat(Number);
        }

        public static bool ValidateNationalNumber(string Number)
        {
            string Pattern = @"^[N][0-9]*";
            return Regex.IsMatch(Number, Pattern);
        }
    }
}