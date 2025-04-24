using System;
using System.Text.RegularExpressions;

namespace workSpace.Global_Classes
{
    class clsValidatoin
    {
        public static bool CheckEmail(string Email)
        {
            var patt = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
            var reg = new Regex(patt);
            return reg.IsMatch(Email);
        }
    }
}
