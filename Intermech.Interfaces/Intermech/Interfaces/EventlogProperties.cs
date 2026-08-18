
// Type: Intermech.Interfaces.EventlogProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Свойства события из журнала аудита</summary>
    public class EventlogProperties
    {
      public long EventID;
      public int CategoryType;
      public long CategoryID;
      public long ObjectID;
      public long RelationID;
      public string ObjectName;
      public long UserID;
      public string ComputerName;
      public string Note;
      public ActionType EventType;
      public DateTime BeginDate;
      public DateTime EndDate;
      public EventlogRecordType AuditType;
      /// <summary>
      /// Что именно за свойства события содержатся в данном классе
      /// </summary>
      public EventPropertiesType EventKind;
      /// <summary>
      /// Свойство выставляется в true в случае, если данное событие уже записано в базу данных и его нужно закрыть там
      /// </summary>
      public bool EventInBase;

      /// <summary>Этот конструктор для новых событий</summary>
      public EventlogProperties(
        int categoryType,
        long categoryID,
        long objectID,
        long relationID,
        string objectName,
        long userID,
        string computerName,
        string note,
        ActionType eventType,
        EventlogRecordType auditType)
      {
        this.EventID = 0L;
        this.CategoryType = categoryType;
        this.CategoryID = categoryID;
        this.ObjectID = objectID;
        this.RelationID = relationID;
        this.ObjectName = objectName;
        this.UserID = userID;
        this.ComputerName = computerName;
        this.Note = note;
        this.EventType = eventType;
        this.BeginDate = DateTime.UtcNow;
        this.EndDate = this.BeginDate;
        this.AuditType = auditType;
        this.EventKind = EventPropertiesType.AddEvent;
      }

      /// <summary>
      /// Этот конструктор для закрытия события с расширенным набором свойств
      /// </summary>
      public EventlogProperties(
        long eventID,
        long categoryID,
        long objectID,
        long relationID,
        string objectName,
        string note,
        EventlogRecordType auditType)
      {
        this.EventID = eventID;
        this.CategoryID = categoryID;
        this.ObjectID = objectID;
        this.RelationID = relationID;
        this.ObjectName = objectName;
        this.Note = note;
        this.AuditType = auditType;
        this.EndDate = DateTime.UtcNow;
        this.EventKind = EventPropertiesType.CloseEventExt;
      }

      /// <summary>Этот конструктор для закрытия события</summary>
      public EventlogProperties(long eventID, string note, EventlogRecordType auditType)
      {
        this.EventID = eventID;
        this.Note = note;
        this.AuditType = auditType;
        this.EndDate = DateTime.UtcNow;
        this.EventKind = EventPropertiesType.CloseEventSimple;
      }

      /// <summary>Метод закрывает данное событие свойствами props</summary>
      /// <param name="props">Свойства закрытия события</param>
      public void CloseEvent(EventlogProperties props)
      {
        if (props.Note != "$NO$")
          this.Note = props.Note;
        this.AuditType = props.AuditType;
        this.EndDate = props.EndDate;
        if (props.EventKind != EventPropertiesType.CloseEventExt)
          return;
        this.CategoryID = props.CategoryID;
        this.ObjectID = props.ObjectID;
        this.RelationID = props.RelationID;
        this.ObjectName = props.ObjectName;
        if (this.EventKind != EventPropertiesType.CloseEventSimple)
          return;
        this.EventKind = EventPropertiesType.CloseEventExt;
      }
    }
}
