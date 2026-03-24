using System;
using System.IO;
using System.Windows.Forms;

namespace DVLD.Global_Classes
{
    class clsUtil
    {
        private static string GenerateGUID()
        {
            Guid GuidNumber = Guid.NewGuid();
            return GuidNumber.ToString();
        }

        private static bool CreateFolderIfNotExist(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
            }
            return true;
        }

        private static string ReplaceFileNameWithGUID(string FileName)
        {
            string FileExtension = Path.GetExtension(FileName);
            string NewName = GenerateGUID() + FileExtension;
            return NewName;
        }

        internal static bool SaveImageToProjectFolder(ref string SourceFile)
        {
            string NewImageName = ReplaceFileNameWithGUID(SourceFile);
            if(CreateFolderIfNotExist(clsGlobal.FolderPath))
            {
                File.Copy(SourceFile ,  Path.Combine(clsGlobal.FolderPath,NewImageName) , true);
                SourceFile = NewImageName;
            }
            return true;
        }
    }
}