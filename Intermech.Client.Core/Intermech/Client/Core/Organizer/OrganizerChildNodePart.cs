
// Type: Intermech.Client.Core.Organizer.OrganizerChildNodePart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Часть элемента навигации, работающую со списком объектов.
/// </summary>
public class OrganizerChildNodePart : ObjectsPart
{
  /// <summary>Коллекция колонок по умолчанию</summary>
  private NodeColumnCollection _defaultColumns;

  /// <summary>
  /// 
  /// </summary>
  public HybridDictionary Tag { get; set; }

  /// <summary>Конструктор.</summary>
  /// <param name="typeID">Идентификатор типа объектов, среди которых осуществляется выбор данных</param>
  /// <param name="columns">Коллекция колонок для отображения данных</param>
  /// <param name="conditions">Набор условий для выбора данных</param>
  /// <param name="services">Контейнер сервисов</param>
  public OrganizerChildNodePart(
    int typeID,
    NodeColumnCollection columns,
    ConditionStructure[] conditions,
    IServiceProvider services)
    : base(typeID, conditions, (IConditionsProvider) null, services)
  {
    this._defaultColumns = columns;
  }

  /// <summary>Получение коллекции колонок по умолчанию.</summary>
  /// <returns>Коллекция колонок по умолчанию</returns>
  public override NodeColumnCollection GetDefaultColumns()
  {
    NodeColumnCollection columns = new NodeColumnCollection();
    if (this._defaultColumns != null)
      columns.AddRange((IEnumerable<NodeColumn>) this._defaultColumns);
    this.AddObligatoryColumns(columns);
    return columns.Count <= 0 ? Utils.DefaultColumnsObjects() : columns;
  }

  /// <summary>
  /// Возвращает коллекцию всех поддерживаемых данным элементом виртуальных колонок навигатора.
  /// Этот метод используется диалогом настройки отображения грида.
  /// </summary>
  /// <param name="ColumnSetName">Название набора колонок.
  /// Intermech.Navigator.Consts.NavigatorDefaultColumnSetName - набор колонок по умолчанию</param>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public override NodeColumnCollection GetSupportedColumns(string ColumnSetName)
  {
    NodeColumnCollection supportedColumns = base.GetSupportedColumns(ColumnSetName);
    this.AddObligatoryColumns(supportedColumns);
    return supportedColumns.Count <= 0 ? Utils.DefaultSupportedColumnsObjects() : supportedColumns;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="conditions"></param>
  /// <returns></returns>
  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    INodeQuery query = base.GetQuery(conditions);
    if (!(query is ObjectsQuery objectsQuery))
      return query;
    objectsQuery.Services.AddService(typeof (OrganizerChildNodePart), (object) this);
    return query;
  }

  /// <summary>Добавление обязательных солонок.</summary>
  /// <param name="columns">Создаваемая коллекция колонок</param>
  private void AddObligatoryColumns(NodeColumnCollection columns)
  {
    columns = columns ?? new NodeColumnCollection();
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    if (service == null)
      return;
    if (!columns.ColumnIDExists((object) ObligatoryObjectAttributes.CAPTION))
    {
      NodeColumn column = service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.CAPTION, NodeColumnSortOrder.Ascending, 0);
      columns.Insert(0, column);
    }
    int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID(SystemGUIDs.attributeStart);
    if (!columns.ColumnIDExists((object) attributeTypeId1))
      columns.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectColumnSchemeGuid, (object) attributeTypeId1));
    int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID(SystemGUIDs.attributeDueDate);
    if (columns.ColumnIDExists((object) attributeTypeId2))
      return;
    columns.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectColumnSchemeGuid, (object) attributeTypeId2));
  }
}
