
// Type: Intermech.Navigator.EventLog.EventNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.EventLog;

/// <summary>
/// Класс объектов, идентифицирующих события из журнала. Все события трактуются
/// однотипными (код типа - 0), т.к. каких-либо различий в их обработке нет.
/// Категория событий жрнала - это предопределенная категория CategoryEventLog
/// из Intermech.Consts.
/// </summary>
public class EventNodeID : INodeID, IEventID
{
  private long _eventID;
  /// <summary>id версии объекта для которого произошло событие</summary>
  private long _objectID;
  private object _cookie;

  public EventNodeID(long eventID, long objectID)
  {
    this._eventID = eventID;
    this._objectID = objectID;
    this._cookie = (object) null;
  }

  public long EventID => this._eventID;

  public long ObjectID => this._objectID;

  int INodeID.CategoryID => 10;

  int INodeID.TypeID => 0;

  object INodeID.Cookie
  {
    get => this._cookie;
    set => this._cookie = value;
  }

  long IEventID.Value => this._eventID;

  public override bool Equals(object obj)
  {
    return obj is EventNodeID eventNodeId && this._eventID == eventNodeId._eventID;
  }

  public override int GetHashCode() => this._eventID.GetHashCode();
}
