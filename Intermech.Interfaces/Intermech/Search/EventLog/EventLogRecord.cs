
// Type: Intermech.Search.EventLog.EventLogRecord
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Linq.Expressions;


namespace Intermech.Search.EventLog
{
    [Serializable]
    public sealed class EventLogRecord : BaseModel
    {
      private EventlogRecordType _type;
      private DateTime _eventStart;
      private DateTime _eventEnd;
      private long _eventID;
      private ActionType _action;
      private string _objectName;
      private long _userVersionID;
      private long _objectVersionID;
      private long _relationID;
      private string _comment;
      private string _category;
      private long _categoryID;
      private string _machineName;

      public EventlogRecordType Type
      {
        get => this._type;
        set
        {
          if (this._type == value)
            return;
          this._type = value;
          this.OnPropertyChanged<EventlogRecordType>((Expression<Func<EventlogRecordType>>) (() => this.Type));
        }
      }

      public DateTime EventStart
      {
        get => this._eventStart;
        set
        {
          if (!(this._eventStart != value))
            return;
          this._eventStart = value;
          this.OnPropertyChanged<DateTime>((Expression<Func<DateTime>>) (() => this.EventStart));
        }
      }

      public DateTime EventEnd
      {
        get => this._eventEnd;
        set
        {
          if (!(this._eventEnd != value))
            return;
          this._eventEnd = value;
          this.OnPropertyChanged<DateTime>((Expression<Func<DateTime>>) (() => this.EventEnd));
        }
      }

      public long EventID
      {
        get => this._eventID;
        set
        {
          if (this._eventID == value)
            return;
          this._eventID = value;
          this.OnPropertyChanged<long>((Expression<Func<long>>) (() => this.EventID));
        }
      }

      public ActionType Action
      {
        get => this._action;
        set
        {
          if (this._action == value)
            return;
          this._action = value;
          this.OnPropertyChanged<ActionType>((Expression<Func<ActionType>>) (() => this.Action));
        }
      }

      public string ObjectName
      {
        get => this._objectName;
        set
        {
          if (!(this._objectName != value))
            return;
          this._objectName = value;
          this.OnPropertyChanged<string>((Expression<Func<string>>) (() => this.ObjectName));
        }
      }

      public long UserVersionID
      {
        get => this._userVersionID;
        set
        {
          if (this._userVersionID == value)
            return;
          this._userVersionID = value;
          this.OnPropertyChanged<long>((Expression<Func<long>>) (() => this.UserVersionID));
        }
      }

      public long ObjectVersionID
      {
        get => this._objectVersionID;
        set
        {
          if (this._objectVersionID == value)
            return;
          this._objectVersionID = value;
          this.OnPropertyChanged<long>((Expression<Func<long>>) (() => this.ObjectVersionID));
        }
      }

      public long RelationID
      {
        get => this._relationID;
        set
        {
          if (this._relationID == value)
            return;
          this._relationID = value;
          this.OnPropertyChanged<long>((Expression<Func<long>>) (() => this.RelationID));
        }
      }

      public string Comment
      {
        get => this._comment;
        set
        {
          if (!(this._comment != value))
            return;
          this._comment = value;
          this.OnPropertyChanged<string>((Expression<Func<string>>) (() => this.Comment));
        }
      }

      public string Category
      {
        get => this._category;
        set
        {
          if (!(this._category != value))
            return;
          this._category = value;
          this.OnPropertyChanged<string>((Expression<Func<string>>) (() => this.Category));
        }
      }

      public long CategoryID
      {
        get => this._categoryID;
        set
        {
          if (this._categoryID == value)
            return;
          this._categoryID = value;
          this.OnPropertyChanged<long>((Expression<Func<long>>) (() => this.CategoryID));
        }
      }

      public string MachineName
      {
        get => this._machineName;
        set
        {
          if (!(this._machineName != value))
            return;
          this._machineName = value;
          this.OnPropertyChanged<string>((Expression<Func<string>>) (() => this.MachineName));
        }
      }
    }
}
