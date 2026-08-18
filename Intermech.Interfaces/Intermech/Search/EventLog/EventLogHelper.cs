
// Type: Intermech.Search.EventLog.EventLogHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Data;


namespace Intermech.Search.EventLog
{
    public static class EventLogHelper
    {
      public static EventLogRecord CreateEventLogRecordFromDataRow(DataRow dataRow)
      {
        return dataRow != null ? new EventLogRecord()
        {
          Action = (ActionType) DataSetProcessor.GetInt32Value(dataRow, "F_AUDIT_TYPE", -1),
          Category = DataSetProcessor.GetStringValue(dataRow, "F_CATEGORY_TYPE", (string) null),
          CategoryID = (long) DataSetProcessor.GetInt32Value(dataRow, "F_CATEGORY_ID", 0),
          Comment = DataSetProcessor.GetStringValue(dataRow, "F_NOTE", (string) null),
          EventEnd = DataSetProcessor.GetDateTimeValue(dataRow, "F_END_DATE", DateTime.MinValue),
          EventID = DataSetProcessor.GetInt64Value(dataRow, "F_EVENT_ID", 0L),
          EventStart = DataSetProcessor.GetDateTimeValue(dataRow, "F_START_DATE", DateTime.MinValue),
          MachineName = DataSetProcessor.GetStringValue(dataRow, "F_COMPUTER_NAME", (string) null),
          ObjectName = DataSetProcessor.GetStringValue(dataRow, "F_OBJECT_NAME", (string) null),
          ObjectVersionID = DataSetProcessor.GetInt64Value(dataRow, "F_OBJECT_ID", 0L),
          RelationID = DataSetProcessor.GetInt64Value(dataRow, "F_PRJLINK_ID", 0L),
          Type = (EventlogRecordType) DataSetProcessor.GetInt32Value(dataRow, "F_EVENT_TYPE", 0),
          UserVersionID = DataSetProcessor.GetInt64Value(dataRow, "F_USER_ID", 0L)
        } : throw new ArgumentNullException(nameof (dataRow));
      }
    }
}
