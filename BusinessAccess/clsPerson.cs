using System;
using System.Data;
using DataAccess;

namespace BusinessAccess
{
    public class clsPerson
    {
        public enum enModeType { Add = 1, Update = 2}
        private enModeType _Mode = enModeType.Add;
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string Name 
        { 
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; } 
        }
        public DateTime DateOfBirth { get; set; }
        public short Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }
        public clsCountry CountryInfo;
        
        public clsPerson()
        {
            this.PersonID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Gendor = -1;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";
            CountryInfo = new clsCountry();
            _Mode = enModeType.Add;
        }

        public clsPerson(int PersonID, string NationalNo, string FirstName,
            string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
            short Gendor, string Address, string Phone, string Email, int NationalityCountryID,
            string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            CountryInfo = clsCountry.Find(NationalityCountryID);
            this.ImagePath = ImagePath;
            _Mode = enModeType.Update;
        }

        public static clsPerson FindPersonByPersonID(int PersonID)
        {
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "",
                    LastName = "", ImagePath = "", Address = "", Phone = "", Email = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gendor = 0;
            int NationalityCountryID = -1;
            bool isFound = DataAccess.clsPersonData.FindPersonByPersonID(PersonID, ref NationalNo, 
                ref FirstName, ref SecondName, ref ThirdName, ref LastName, 
                ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, 
                ref NationalityCountryID, ref ImagePath);
            if (isFound)
            {
                return new clsPerson(PersonID, NationalNo, FirstName,
                                            SecondName, ThirdName, LastName, 
                                            DateOfBirth, Gendor, Address, Phone,
                                            Email, NationalityCountryID, ImagePath);
            }
            else
                return null;
        }

        public static clsPerson FindPersonByNationalNo(string NationalNo)
        {
            string FirstName = "", SecondName = "", ThirdName = "",
                    LastName = "", ImagePath = "", Address = "", Phone = "", Email = "";
            DateTime DateOfBirth = DateTime.Now;
            short Gendor = -1;
            int NationalityCountryID = -1, PersonID = -1;
            bool isFound = DataAccess.clsPersonData.FindPersonByNationalNo(NationalNo, ref PersonID,
                ref FirstName, ref SecondName, ref ThirdName, ref LastName,
                ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email,
                ref NationalityCountryID, ref ImagePath);
            if (isFound)
            {
                return new clsPerson(PersonID, NationalNo, FirstName,
                                            SecondName, ThirdName, LastName,
                                            DateOfBirth, Gendor, Address, Phone,
                                            Email, NationalityCountryID, ImagePath);
            }
            else
                return null;
        }

        private bool _AddNewPerson()
        {
            this.PersonID = DataAccess.clsPersonData.AddNewPerson(this.NationalNo, this.FirstName,
                            this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth,
                            this.Gendor, this.Address, this.Phone, this.Email,
                            this.NationalityCountryID, this.ImagePath);
            return this.PersonID > -1;
        }
        private bool _UpdatePerson()
        {
            return DataAccess.clsPersonData.UpdatePerson(this.PersonID, this.NationalNo, this.FirstName,
                            this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth,
                            this.Gendor, this.Address, this.Phone, this.Email,
                            this.NationalityCountryID, this.ImagePath);
        }

        public bool Save()
        {
            switch (_Mode) {
                case enModeType.Add:
                    if(_AddNewPerson())
                    {
                        _Mode = enModeType.Update;
                        return true;
                    }
                    return false;
                case enModeType.Update:
                    return _UpdatePerson();
            }
            return false;
        }

        public static bool DeletePerson(int PersonID)
        {
            return DataAccess.clsPersonData.DeletePerson(PersonID);
        }

        public static DataTable GetAllPeople()
        {
            return DataAccess.clsPersonData.GetAllPeople();
        }

        public static bool IsPersonExist(int PersonID)
        {
            return DataAccess.clsPersonData.IsPersonExist(PersonID);
        }

        public static bool IsPersonExist(string NationalNo)
        {
            return DataAccess.clsPersonData.IsPersonExist(NationalNo);
        }

    }
}
