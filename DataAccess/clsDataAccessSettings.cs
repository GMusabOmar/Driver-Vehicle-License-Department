using System;
using System.Configuration;

namespace DataAccess
{
    public static class clsDataAccessSettings
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
    }
}
