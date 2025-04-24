using System;
using System.IO;

namespace workSpace.Global_Classes
{
    class util
    {
        private static bool CreateFolderIfDoesNotExist(string Folder)
        {
            if (!Directory.Exists(Folder)) 
            {
                try
                {
                    Directory.CreateDirectory(Folder);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error To Create Folder : " + e.Message);
                    return false;
                }
            }
            return true;
        }
        private static string GenerateGUID()
        {
            var myGuid = Guid.NewGuid();
            return myGuid.ToString();
        }
        private static string ReplaceFileNameWithGUID(string strFile)
        {
            var FileInfo = new FileInfo(strFile);
            return GenerateGUID() + FileInfo.Extension;
        }
        public static bool CopyImageToProjectImagesFolder(ref string ImagePath)
        {
            string Destination_Folder = @"C:\Users\K.P._M\Visual Studio\Windows Form\workSpace\Photos\";
            if(!CreateFolderIfDoesNotExist(Destination_Folder))
            {
                return false;
            }
            string Destination_File = Destination_Folder + ReplaceFileNameWithGUID(ImagePath);
            try
            {
                File.Copy(ImagePath, Destination_File, true);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error To Copy Image : " + e.Message);
                return false;
            }
            ImagePath = Destination_File;
            return true;
        }

    }
}
