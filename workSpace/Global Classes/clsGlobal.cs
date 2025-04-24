using BusinessAccess;
using Microsoft.Win32;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace workSpace.Global_Classes
{
    public class clsGlobal
    {
        public static clsUser CurrentUser;
        public static bool RememebrUserNameAndPassword(string UserName, string Password)
        {
            string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();
            string FileName = CurrentDirectory + "\\LoginText.txt";
            try
            {
                if (UserName == "" && File.Exists(FileName))
                {
                    File.Delete(FileName);
                    return true;
                }
                string DataToSave = UserName + "#//#" + Password;
                using (StreamWriter writer = new StreamWriter(FileName))
                {
                    writer.WriteLine(DataToSave);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public static bool RestoreCrediental(ref string UserName, ref string Password)
        {
            string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();
            string FileName = CurrentDirectory + "\\LoginText.txt";
            try
            {
                using (StreamReader reader = new StreamReader(FileName))
                {
                    string Line = "";
                    while((Line = reader.ReadLine()) != null)
                    {
                        string[] Result = Line.Split(new string[] { "#//#" }, StringSplitOptions.None);
                        UserName = Result[0];
                        Password = Result[1];
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public static bool GetUserAndPasswordByRegEdit(string UserName, string Password)
        {
            string KeyName = @"SOFTWARE\DVLD";
            string UValue = "UserName";
            string PValue = "Password";
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(KeyName, true))
                {
                    if (key != null)
                    {
                        if(UserName == "")
                        {
                            key.DeleteValue(UValue);
                            key.DeleteValue(PValue);
                            return true;
                        }
                        key.SetValue(UValue, UserName, RegistryValueKind.String);
                        key.SetValue(PValue, Password, RegistryValueKind.String);
                        return true;
                    }
                }
            }
            catch
            {

            }
            return false;
        }
        public static bool RememberUserAndPasswordByRegEdit(ref string UserName, ref string Password)
        {
            string KeyName = @"SOFTWARE\DVLD";
            string KeyUser = "UserName";
            string KeyPassword = "Password";
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(KeyName, true))
                {
                    if (key != null)
                    {
                        UserName = key.GetValue(KeyUser, null) as string;
                        Password = key.GetValue(KeyPassword, null) as string;
                        return true;
                    }
                }
            }
            catch
            {

            }
            return false;
        }
        public static string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
