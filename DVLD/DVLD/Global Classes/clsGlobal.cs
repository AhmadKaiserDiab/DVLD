using System.IO;
using DVLD_Business_Layer;

namespace DVLD.Global_Classes
{
    internal static class clsGlobal
    {
        public static clsUser CurrentUser;

        public static string FolderPath = @"E:\Programming\Programming Advices\ProgrammingAdvices\Level_19\ProfileImagesFolder";

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            string FilePath = @"E:\Programming\Programming Advices\ProgrammingAdvices\Level_19\My DVLD Project\UserCredentials.txt";
            
            string[] Credentials = { Username.Trim(), Password.Trim() };

            File.WriteAllLines(FilePath, Credentials);

            return true;
        }

        public static bool GetStoredCredentials(ref string Username, ref string Password)
        {
            string FilePath = @"E:\Programming\Programming Advices\ProgrammingAdvices\Level_19\My DVLD Project\UserCredentials.txt";

            string[] Credentials = File.ReadAllLines(FilePath);
            if (Credentials.Length != 0)
            {
                Username = Credentials[0].Trim();
                Password = Credentials[1].Trim();
            }
            return Username != string.Empty;
        }
    }
}