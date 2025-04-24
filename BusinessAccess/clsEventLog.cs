using DataAccess;
using System;

namespace BusinessAccess
{
    public class clsEventLog
    {
        public enum enEntryType { Info = 1, Error = 2, Warning = 3 }
        public string sourceName {  get; set; }
        public string description {  get; set; }
        public enEntryType entryType {  get; set; }
        public clsEventLog()
        {
            this.sourceName = null;
            this.description = null;
            this.entryType = enEntryType.Info;
        }
        public clsEventLog(string sourceName, string description, enEntryType entryType)
        {
            this.sourceName = sourceName;
            this.description = description;
            this.entryType = entryType;
        }
        public static clsEventLog SetEvent(string sourceName, string description, enEntryType entryType)
        {
            clsEventLogData _EventLog = clsEventLogData.SetEvent(sourceName, description, (clsEventLogData.enEntryType)entryType);
            if (_EventLog != null)
            {
                return new clsEventLog(sourceName, description, entryType);
            }
            return null;
        }
    }
}
