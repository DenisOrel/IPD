
// Type: Intermech.Navigator.EventLog.EventsNodePartBase
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace Intermech.Navigator.EventLog;

public abstract class EventsNodePartBase : 
  EventsNodeItems,
  INodePart,
  INodeItems,
  INodeQuerySupport,
  IContextAware
{
  /// <summary>Название набора колонок - "Атрибуты журнала событий"</summary>
  internal static string columnsSetNameEvent = LocalizationHolder.rm.GetString("Client.Core_607");
  /// <summary>Коллекция названий наборов колонок</summary>
  internal static List<string> columnSetNames = new List<string>(0);
  private object owner;

  /// <summary>Список условий запроса</summary>
  protected abstract ConditionStructure[] Conditions { get; }

  /// <summary>Дополнительные параметры запроса</summary>
  protected abstract HybridDictionary ConditionTags { get; }

  public object Owner
  {
    get => this.owner;
    set => this.owner = value;
  }

  public INodeQuery GetQuery()
  {
    return (INodeQuery) new EventsQuery((INodeQuerySupport) this, this.Conditions, this.ConditionTags)
    {
      Services = this.Services
    };
  }

  public NodeColumnCollection GetDefaultColumns()
  {
    NodeColumnCollection columns = new NodeColumnCollection();
    Helper.CollectColumns(columns);
    return columns;
  }

  /// <summary>
  /// Возвращает коллекцию всех поддерживаемых данным элементом
  /// виртуальных колонок навигатора. Этот метод используется диалогом
  /// настройки отображения грида.
  /// </summary>
  /// <param name="ColumnSetName">Название набора колонок. Пустая строка - набор колонок по умолчанию</param>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public NodeColumnCollection GetSupportedColumns(string ColumnSetName)
  {
    NodeColumnCollection columns = new NodeColumnCollection();
    Helper.CollectColumns(columns);
    return columns;
  }

  /// <summary>
  /// Вернуть список поддерживаемых названий наборов колонок.
  /// Если null - есть только название по умолчанию (Intermech.Navigator.Consts.NavigatorDefaultColumnSetName)
  /// </summary>
  /// <returns></returns>
  public virtual List<string> GetSupportedColumnSetNames()
  {
    if (!EventsNodePartBase.columnSetNames.Contains(EventsNodePartBase.columnsSetNameEvent))
      EventsNodePartBase.columnSetNames.Add(EventsNodePartBase.columnsSetNameEvent);
    return EventsNodePartBase.columnSetNames;
  }

  public object MapColumnToField(NodeColumn column)
  {
    return !(column.SchemeGuid == Consts.ColumnSchemeGuid) ? (object) null : column.ID;
  }

  public List<object> GetSpecialFields()
  {
    return new List<object>()
    {
      (object) ObligatoryObjectAttributes.F_EVENT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
  }

  public INodeID CreateNodeId(object[] fieldValues, RecordAdapter adapter)
  {
    int fieldIndex1 = adapter.GetFieldIndex((object) ObligatoryObjectAttributes.F_EVENT_ID);
    long int64_1 = Convert.ToInt64(fieldValues[fieldIndex1]);
    int fieldIndex2 = adapter.GetFieldIndex((object) ObligatoryObjectAttributes.F_OBJECT_ID);
    long int64_2 = Convert.ToInt64(fieldValues[fieldIndex2]);
    return (INodeID) new EventNodeID(int64_1, int64_2);
  }

  public object CreateRecordId(INodeID nodeId) => (object) ((EventNodeID) nodeId).EventID;

  public IServiceProvider Services { get; set; }
}
