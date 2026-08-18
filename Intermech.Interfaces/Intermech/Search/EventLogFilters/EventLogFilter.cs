
// Type: Intermech.Search.EventLogFilters.EventLogFilter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Linq.Expressions;


namespace Intermech.Search.EventLogFilters
{
    [Serializable]
    public sealed class EventLogFilter : BaseModel, ICloneable
    {
      public static readonly EventLogFilter AllEventsFilter = new EventLogFilter()
      {
        Guid = Guid.Empty,
        Name = "Все события"
      };
      private string _name;
      private bool _hasTypeCondition;
      private RelationalOperators _typeRelop;
      private EventlogRecordType _type;
      private bool _hasEventStartCondition;
      private RelationalOperators _eventStartRelop;
      private DateTime _eventStart;
      private bool _hasEventEndCondition;
      private RelationalOperators _eventEndRelop;
      private DateTime _eventEnd;
      private bool _hasEventIDCondition;
      private RelationalOperators _eventIDRelop;
      private long _eventID;
      private bool _hasActionCondition;
      private RelationalOperators _actionRelop;
      private string[] _action = new string[0];
      private bool _hasObjectNameCondition;
      private RelationalOperators _objectNameRelop;
      private string _objectName;
      private bool _hasUserCondition;
      private RelationalOperators _userRelop;
      private long _userVersionID;
      private bool _hasObjectVersionIDCondition;
      private RelationalOperators _objectVersionIDRelop;
      private long _objectVersionID;
      private bool _hasRelationIDCondition;
      private RelationalOperators _relationIDRelop;
      private long _relationID;
      private bool _hasCommentCondition;
      private RelationalOperators _commentRelop;
      private string _comment;
      private bool _hasCategoryCondition;
      private RelationalOperators _categoryRelop;
      private int _category;
      private bool _hasCategoryIDCondition;
      private RelationalOperators _categoryIDRelop;
      private long _categoryID;
      private bool _hasMachineNameCondition;
      private RelationalOperators _machineNameRelop;
      private string _machineName;

      private EventLogFilter()
      {
      }

      public EventLogFilter(Guid guid)
      {
        this.Guid = !(guid == Guid.Empty) ? guid : throw new ArgumentException();
      }

      public Guid Guid { get; private set; }

      public string Name
      {
        get => this._name;
        set
        {
          if (!(this._name != value))
            return;
          this._name = value;
          this.OnPropertyChanged<string>((Expression<Func<string>>) (() => this.Name));
        }
      }

      public bool HasTypeCondition
      {
        get => this._hasTypeCondition;
        set
        {
          if (this._hasTypeCondition == value)
            return;
          this._hasTypeCondition = value;
          this.OnPropertyChanged<bool>((Expression<Func<bool>>) (() => this.HasTypeCondition));
        }
      }

      public RelationalOperators TypeRelop
      {
        get => this._typeRelop;
        set
        {
          if (this._typeRelop == value)
            return;
          this._typeRelop = value;
          this.OnPropertyChanged<RelationalOperators>((Expression<Func<RelationalOperators>>) (() => this.TypeRelop));
        }
      }

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

      public bool HasEventStartCondition
      {
        get => this._hasEventStartCondition;
        set
        {
          if (this._hasEventStartCondition == value)
            return;
          this._hasEventStartCondition = value;
          this.OnPropertyChanged<bool>((Expression<Func<bool>>) (() => this.HasEventStartCondition));
        }
      }

      public RelationalOperators EventStartRelop
      {
        get => this._eventStartRelop;
        set
        {
          if (this._eventStartRelop == value)
            return;
          this._eventStartRelop = value;
          this.OnPropertyChanged<RelationalOperators>((Expression<Func<RelationalOperators>>) (() => this.EventStartRelop));
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

      public bool HasEventEndCondition
      {
        get => this._hasEventEndCondition;
        set
        {
          if (this._hasEventEndCondition == value)
            return;
          this._hasEventEndCondition = value;
          this.OnPropertyChanged<bool>((Expression<Func<bool>>) (() => this.HasEventEndCondition));
        }
      }

      public RelationalOperators EventEndRelop
      {
        get => this._eventEndRelop;
        set
        {
          if (this._eventEndRelop == value)
            return;
          this._eventEndRelop = value;
          this.OnPropertyChanged<RelationalOperators>((Expression<Func<RelationalOperators>>) (() => this.EventEndRelop));
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

      public bool HasEventIDCondition
      {
        get => this._hasEventIDCondition;
        set
        {
          if (this._hasEventIDCondition == value)
            return;
          this._hasEventIDCondition = value;
          this.OnPropertyChanged<bool>((Expression<Func<bool>>) (() => this.HasEventIDCondition));
        }
      }

      public RelationalOperators EventIDRelop
      {
        get => this._eventIDRelop;
        set
        {
          if (this._eventIDRelop == value)
            return;
          this._eventIDRelop = value;
          this.OnPropertyChanged<RelationalOperators>((Expression<Func<RelationalOperators>>) (() => this.EventIDRelop));
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

      public bool HasActionCondition
      {
        get => this._hasActionCondition;
        set
        {
          if (this._hasActionCondition == value)
            return;
          this._hasActionCondition = value;
          this.OnPropertyChanged<bool>((Expression<Func<bool>>) (() => this.HasActionCondition));
        }
      }

      public RelationalOperators ActionRelop
      {
        get => this._actionRelop;
        set
        {
          if (this._actionRelop == value)
            return;
          this._actionRelop = value;
          this.OnPropertyChanged<RelationalOperators>((Expression<Func<RelationalOperators>>) (() => this.ActionRelop));
        }
      }

      public string[] Action
      {
        get => this._action;
        set
        {
          if (this._action == value)
            return;
          this._action = value;
          this.OnPropertyChanged<string[]>((Expression<Func<string[]>>) (() => this.Action));
        }
      }

      public bool HasObjectNameCondition
      {
        get => this._hasObjectNameCondition;
        set
        {
          if (this._hasObjectNameCondition == value)
            return;
          this._hasObjectNameCondition = value;
          this.OnPropertyChanged<bool>((Expression<Func<bool>>) (() => this.HasObjectNameCondition));
        }
      }

      public RelationalOperators ObjectNameRelop
      {
        get => this._objectNameRelop;
        set
        {
          if (this._objectNameRelop == value)
            return;
          this._objectNameRelop = value;
          this.OnPropertyChanged<RelationalOperators>((Expression<Func<RelationalOperators>>) (() => this.ObjectNameRelop));
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

      public bool HasUserCondition
      {
        get => this._hasUserCondition;
        set
        {
          if (this._hasUserCondition == value)
            return;
          this._hasUserCondition = value;
          this.OnPropertyChanged<bool>((Expression<Func<bool>>) (() => this.HasUserCondition));
        }
      }

      public RelationalOperators UserRelop
      {
        get => this._userRelop;
        set
        {
          if (this._userRelop == value)
            return;
          this._userRelop = value;
          this.OnPropertyChanged<RelationalOperators>((Expression<Func<RelationalOperators>>) (() => this.UserRelop));
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

      public bool HasObjectVersionIDCondition
      {
        get => this._hasObjectVersionIDCondition;
        set
        {
          if (this._hasObjectVersionIDCondition == value)
            return;
          this._hasObjectVersionIDCondition = value;
          this.OnPropertyChanged<bool>((Expression<Func<bool>>) (() => this.HasObjectVersionIDCondition));
        }
      }

      public RelationalOperators ObjectVersionIDRelop
      {
        get => this._objectVersionIDRelop;
        set
        {
          if (this._objectVersionIDRelop == value)
            return;
          this._objectVersionIDRelop = value;
          this.OnPropertyChanged<RelationalOperators>((Expression<Func<RelationalOperators>>) (() => this.ObjectVersionIDRelop));
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

      public bool HasRelationIDCondition
      {
        get => this._hasRelationIDCondition;
        set
        {
          if (this._hasRelationIDCondition == value)
            return;
          this._hasRelationIDCondition = value;
          this.OnPropertyChanged<bool>((Expression<Func<bool>>) (() => this.HasRelationIDCondition));
        }
      }

      public RelationalOperators RelationIDRelop
      {
        get => this._relationIDRelop;
        set
        {
          if (this._relationIDRelop == value)
            return;
          this._relationIDRelop = value;
          this.OnPropertyChanged<RelationalOperators>((Expression<Func<RelationalOperators>>) (() => this.RelationIDRelop));
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

      public bool HasCommentCondition
      {
        get => this._hasCommentCondition;
        set
        {
          if (this._hasCommentCondition == value)
            return;
          this._hasCommentCondition = value;
          this.OnPropertyChanged<bool>((Expression<Func<bool>>) (() => this.HasCommentCondition));
        }
      }

      public RelationalOperators CommentRelop
      {
        get => this._commentRelop;
        set
        {
          if (this._commentRelop == value)
            return;
          this._commentRelop = value;
          this.OnPropertyChanged<RelationalOperators>((Expression<Func<RelationalOperators>>) (() => this.CommentRelop));
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

      public bool HasCategoryCondition
      {
        get => this._hasCategoryCondition;
        set
        {
          if (this._hasCategoryCondition == value)
            return;
          this._hasCategoryCondition = value;
          this.OnPropertyChanged<bool>((Expression<Func<bool>>) (() => this.HasCategoryCondition));
        }
      }

      public RelationalOperators CategoryRelop
      {
        get => this._categoryRelop;
        set
        {
          if (this._categoryRelop == value)
            return;
          this._categoryRelop = value;
          this.OnPropertyChanged<RelationalOperators>((Expression<Func<RelationalOperators>>) (() => this.CategoryRelop));
        }
      }

      public int Category
      {
        get => this._category;
        set
        {
          if (this._category == value)
            return;
          this._category = value;
          this.OnPropertyChanged<int>((Expression<Func<int>>) (() => this.Category));
        }
      }

      public bool HasCategoryIDCondition
      {
        get => this._hasCategoryIDCondition;
        set
        {
          if (this._hasCategoryIDCondition == value)
            return;
          this._hasCategoryIDCondition = value;
          this.OnPropertyChanged<bool>((Expression<Func<bool>>) (() => this.HasCategoryIDCondition));
        }
      }

      public RelationalOperators CategoryIDRelop
      {
        get => this._categoryIDRelop;
        set
        {
          if (this._categoryIDRelop == value)
            return;
          this._categoryIDRelop = value;
          this.OnPropertyChanged<RelationalOperators>((Expression<Func<RelationalOperators>>) (() => this.CategoryIDRelop));
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

      public bool HasMachineNameCondition
      {
        get => this._hasMachineNameCondition;
        set
        {
          if (this._hasMachineNameCondition == value)
            return;
          this._hasMachineNameCondition = value;
          this.OnPropertyChanged<bool>((Expression<Func<bool>>) (() => this.HasMachineNameCondition));
        }
      }

      public RelationalOperators MachineNameRelop
      {
        get => this._machineNameRelop;
        set
        {
          if (this._machineNameRelop == value)
            return;
          this._machineNameRelop = value;
          this.OnPropertyChanged<RelationalOperators>((Expression<Func<RelationalOperators>>) (() => this.MachineNameRelop));
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

      public EventLogFilter Clone()
      {
        return new EventLogFilter(this.Guid)
        {
          Name = this.Name,
          HasTypeCondition = this.HasTypeCondition,
          TypeRelop = this.TypeRelop,
          Type = this.Type,
          HasEventStartCondition = this.HasEventStartCondition,
          EventStartRelop = this.EventStartRelop,
          EventStart = this.EventStart,
          HasEventEndCondition = this.HasEventEndCondition,
          EventEndRelop = this.EventEndRelop,
          EventEnd = this.EventEnd,
          HasEventIDCondition = this.HasEventIDCondition,
          EventIDRelop = this.EventIDRelop,
          EventID = this.EventID,
          HasActionCondition = this.HasActionCondition,
          ActionRelop = this.ActionRelop,
          Action = this.Action,
          HasObjectNameCondition = this.HasObjectNameCondition,
          ObjectNameRelop = this.ObjectNameRelop,
          ObjectName = this.ObjectName,
          HasUserCondition = this.HasUserCondition,
          UserRelop = this.UserRelop,
          UserVersionID = this.UserVersionID,
          HasObjectVersionIDCondition = this.HasObjectVersionIDCondition,
          ObjectVersionIDRelop = this.ObjectVersionIDRelop,
          ObjectVersionID = this.ObjectVersionID,
          HasRelationIDCondition = this.HasRelationIDCondition,
          RelationIDRelop = this.RelationIDRelop,
          RelationID = this.RelationID,
          HasCommentCondition = this.HasCommentCondition,
          CommentRelop = this.CommentRelop,
          Comment = this.Comment,
          HasCategoryCondition = this.HasCategoryCondition,
          CategoryRelop = this.CategoryRelop,
          Category = this.Category,
          HasCategoryIDCondition = this.HasCategoryIDCondition,
          CategoryIDRelop = this.CategoryIDRelop,
          CategoryID = this.CategoryID,
          HasMachineNameCondition = this.HasMachineNameCondition,
          MachineNameRelop = this.MachineNameRelop,
          MachineName = this.MachineName
        };
      }

      object ICloneable.Clone() => (object) this.Clone();
    }
}
