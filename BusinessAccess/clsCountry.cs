using System;
using System.Data;
using DataAccess;

namespace BusinessAccess
{
    public class clsCountry
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public clsCountry()
        {
            this.CountryID = -1;
            this.CountryName = "";
        }
        public clsCountry(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }

        public static DataTable GetAllCountry()
        {
            return clsCountryData.GetAllCountry();
        }

        public static clsCountry Find(int CountryID)
        {
            string CountryName = "";
            if (clsCountryData.FindCountryByID(CountryID, ref CountryName))
                return new clsCountry(CountryID, CountryName);
            else
                return null;
        }

        public static clsCountry Find(string CountryName)
        {
            int CountryID = -1;
            if (clsCountryData.FindCountryByName(CountryName, ref CountryID))
                return new clsCountry(CountryID, CountryName);
            else
                return null;
        }
    }
}
