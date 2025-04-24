using System;
using System.Diagnostics;

namespace DataAccess
{
    public class clsEventLogData
    {
        public enum enEntryType { Info = 1, Error = 2, Warning = 3}
        public string SourceName {  get; set; }
        public string description {  get; set; }
        public enEntryType? EntryType = null;
        public clsEventLogData()
        {
            SourceName = null;
            this.description = null;
            EntryType = null;
        }
        public clsEventLogData(string sourceName, string description, enEntryType entryType)
        {
            SourceName = sourceName;
            this.description = description;
            EntryType = entryType;
        }
        private static EventLogEntryType GetEntryType(enEntryType EntryType)
        {
            switch(EntryType)
            {
                case enEntryType.Info:
                    return EventLogEntryType.Information;
                case enEntryType.Error:
                    return EventLogEntryType.Error;
                default:
                    return EventLogEntryType.Warning;
            }
        }
        public static clsEventLogData SetEvent(string sourceName, string description, enEntryType entryType)
        {

            if(!EventLog.Exists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, "Application");
                EventLog.WriteEntry(sourceName, description, GetEntryType(entryType));
                return new clsEventLogData(sourceName, description, entryType);
            }
            return null;
        }
    }
}
