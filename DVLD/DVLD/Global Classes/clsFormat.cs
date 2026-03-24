using System;
namespace DVLD.Global_Classes
{
    internal class clsFormat
    {
        public static string DateToShort(DateTime dt)
        {
            return dt.ToString("dd/MM/yyyy");
        }
    }
}