using System;
using System.Data;
using DataAccess;

namespace BusinessAccess
{
    public class clsUser
    {
        public enum enTypeMode { Add = 0, Update = 1}
        private enTypeMode _Mode = enTypeMode.Add;
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public clsPerson PersonInfo;
        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = false;
            _Mode = enTypeMode.Add;
        }
        public clsUser(int UserID, int PersonID, string UserName, string Password,
                        bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            PersonInfo = clsPerson.FindPersonByPersonID(this.PersonID);
            _Mode = enTypeMode.Update;
        }
        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonID,
                                    this.UserName, this.Password, this.IsActive);
            return this.UserID > 0;
        }
        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.UserName, this.Password, this.IsActive);
        }
        public bool Save()
        {
            switch(_Mode)
            {
                case enTypeMode.Add:
                    if(_AddNewUser())
                    {
                        _Mode = enTypeMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enTypeMode.Update:
                    return _UpdateUser();
            }
            return false;
        }

        public static clsUser GetUserByUserID(int UserID)
        {
            int PersonID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;
            bool isFound = clsUserData.GetUserByUserID(UserID, ref PersonID,
                ref UserName, ref Password, ref IsActive);
            if (isFound)
            {
                return new clsUser(UserID, PersonID, UserName, Password,
                        IsActive);
            }
            else
                return null;
        }
        public static clsUser GetUserByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;
            bool isFound = clsUserData.GetUserByPersonID(PersonID, ref UserID,
                ref UserName, ref Password, ref IsActive);
            if (isFound)
            {
                return new clsUser(UserID, PersonID, UserName, Password,
                        IsActive);
            }
            else
                return null;
        }
        public static clsUser GetUserByUserNameAndPassword(string UserName, string Password)
        {
            int UserID = -1, PersonID = -1;
            bool IsActive = true;
            bool isFound = clsUserData.GetUserByUserNameAndPassword(UserName, Password,
                ref PersonID, ref UserID, ref IsActive);
            if (isFound)
            {
                return new clsUser(UserID, PersonID, UserName, Password,
                        IsActive);
            }
            else
                return null;
        }
        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }
        public static bool IsUserExistsByUserID(int UserID)
        {
            return clsUserData.IsUserExistsByUserID(UserID);
        }
        public static bool IsUserExistsByPersonID(int PersonID)
        {
            return clsUserData.IsUserExistsByPersonID(PersonID);
        }
        public static bool IsUserExistsByUserName(string UserName)
        {
            return clsUserData.IsUserExistsByUserName(UserName);
        }
        public static bool IsUserExistsByUserNameAndPassword(string UserName, string Password)
        {
            return clsUserData.IsUserExistsByUserNameAndPassword(UserName, Password);
        }
        public static DataTable GetAllUser()
        {
            return clsUserData.GetAllUser();
        }

    }
}
