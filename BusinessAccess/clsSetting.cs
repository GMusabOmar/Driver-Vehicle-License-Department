using System;
using DataAccess;

namespace BusinessAccess
{
    public class clsSetting
    {
        public int SettingID { get; set; }
        public int InternationalExpirFees { get; set; }
        public clsSetting()
        {
            this.SettingID = 0;
            this.InternationalExpirFees = 0;
        }
        public clsSetting(int SettingID, int InternationalExpirFees)
        {
            this.SettingID = SettingID;
            this.InternationalExpirFees = InternationalExpirFees;
        }
        public static clsSetting Find(int SettingID)
        {
            int InternationalExpirFees = 0;
            if (clsSettingData.FoundByID(SettingID, ref InternationalExpirFees))
                return new clsSetting(SettingID, InternationalExpirFees);
            else
                return null;
        }
    }
}
