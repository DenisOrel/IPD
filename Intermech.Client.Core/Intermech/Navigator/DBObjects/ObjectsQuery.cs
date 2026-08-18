
// Type: Intermech.Navigator.DBObjects.ObjectsQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Classes.Providers;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Linq;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Реализует запрос к базе данных на чтение информации об объектах из коллекции
/// объектов, т.е. получить значение атрибутов связей нельзя (для этого
/// предназначен класс RelatedObjectsQuery). По умолчания запрос возвращает
/// информацию об объектах любого типа, однако, если указать конкретный тип
/// объектов, то запрос вернет информацию об объектах не только этого типа,
/// но и производных от него. Результаты запроса возвращаются в унифицированном
/// формате, воспринимаемом навигатором, т.е. для каждого объекта предоставляется
/// его идентификатор, поддерживающий интерфейс INodeID, и значения указанных
/// виртуальных колонок.
/// </summary>
public class ObjectsQuery : DBRecordsNodeQuery, IContextAware, IObjectCollectionFilters
{
  /// <summary>Подготовка запроса</summary>
  protected INodeQuerySupport support;
  /// <summary>Контейнер сервисов</summary>
  public AdvancedServiceContainer Services = new AdvancedServiceContainer();
  /// <summary>Идентификатор типа читаемых объектов.</summary>
  protected int _objTypeID;
  /// <summary>
  /// Массив условий, которым должны удовлетворять объекты, читаемые этим
  /// запрсом.
  /// </summary>
  private ConditionStructure[] _conditions;
  /// <summary>
  /// Имя ключевого поля базы данных, используемое для построения закладки,
  /// определяющей позицию для чтения следующей порции данных.
  /// </summary>
  private static readonly object KeyField = (object) ObligatoryObjectAttributes.F_OBJECT_ID;
  /// <summary>Сервис настроек фильтрации списков объектов</summary>
  private static IObjectListFiltration _objectListFiltration = (IObjectListFiltration) null;
  /// <summary>
  /// Массив имен базы данных, используемых для генерации унифицированных
  /// идентификаторов (поддерживающих INodeID).
  /// </summary>
  private static readonly object[] _generatorFields = new object[3]
  {
    (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
    (object) ObligatoryObjectAttributes.F_OBJECT_ID,
    (object) ObligatoryObjectAttributes.F_CHKOUT_BY
  };
  /// <summary>
  /// <summary>
  /// В коллекцию PluginsData можно сохранять свою информацию в виде сериализуемых пар
  /// значений [Ключ] = [Значение]. Данная коллекция будет доступна на серверной стороне
  /// в Select у коллекции объектов.
  /// </summary>
  /// </summary>
  private HybridDictionary _pluginsData = new HybridDictionary();
  /// <summary>
  /// Флаг для наследников ObjectsQuery, которым не нужна фильтрация результатов запроса
  /// </summary>
  protected bool enableFiltration = true;

  /// <summary>
  /// Конструктор запроса, в результате выполнения которого будет прочитана
  /// информация о всех объектах указанного типа и производных от него,
  /// которые удовлетворяют указанным условиям.
  /// </summary>
  /// <param name="support"></param>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <param name="conditions">Массив условий, которым должны удовлетворять объекты</param>
  /// <param name="services"></param>
  public ObjectsQuery(
    INodeQuerySupport support,
    int objTypeID,
    ConditionStructure[] conditions,
    IServiceProvider services)
    : base(ObjectsQuery.KeyField)
  {
    this.support = support;
    this.Services.AdvancedProvider = services;
    this._objTypeID = objTypeID;
    this._conditions = conditions;
  }

  /// <summary>
  /// Добавляет к параметрам запроса условия, указанные в конструкторе
  /// запроса. Этот метод используется при чтении первой/следующей части
  /// списка объектов.
  /// </summary>
  /// <param name="mapping">Схема отображения виртуальных колонок в поля источника данных</param>
  /// <param name="bookmark">Закладка, определяющая позицию для чтения порции</param>
  /// <param name="count">Количество записей, которое должно быть прочитано</param>
  /// <returns>Параметры запроса к базе данных</returns>
  protected override DBRecordSetParams GetQueryParams(
    object bookmark,
    int count,
    RecordMapping mapping)
  {
    DBRecordSetParams queryParams = base.GetQueryParams(bookmark, count, mapping);
    if (this._conditions != null)
      queryParams.Conditions = ConditionStructure.Join(this._conditions, queryParams.Conditions);
    ObjectsQuery._objectListFiltration = !this.enableFiltration || this.Services == null ? (IObjectListFiltration) null : this.Services.GetService(typeof (IObjectListFiltration)) as IObjectListFiltration;
    if (ObjectsQuery._objectListFiltration != null && (ObjectsQuery._objectListFiltration.SelectedFilterGuid != Guid.Empty || ObjectsQuery._objectListFiltration.FilterByCurrentVersionsRule))
    {
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
      IFiltrationService service1 = ServicesManager.GetService(typeof (IFiltrationService)) as IFiltrationService;
      ICurrentUserAndRole service2 = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      VersionsRule currentRule = service1.Filtration != null ? service1.Filtration.CurrentRule : (VersionsRule) null;
      List<ConditionStructure> structures = !ObjectsQuery._objectListFiltration.FilterByCurrentVersionsRule || currentRule == null ? (List<ConditionStructure>) null : ConditionsHelper.CreateStructures(currentRule, service2.CachedEditingContextModificationID);
      ConditionStructure[] joinedConditions = (ConditionStructure[]) null;
      if (ObjectsQuery._objectListFiltration.SelectedFilterGuid != Guid.Empty)
      {
        if (ObjectsQuery._objectListFiltration.SelectedFilterGuid.ToString() == "cad0079c-306c-11d8-b4e9-00304f19f545")
          conditionStructureList.Add(new ConditionStructure(-8, RelationalOperators.Equal, (object) service2.UserID, LogicalOperators.AND, 0, true));
        else if (ObjectsQuery._objectListFiltration.SelectedFilterGuid.ToString() == "cad00799-306c-11d8-b4e9-00304f19f545")
          conditionStructureList.Add(new ConditionStructure(-13, RelationalOperators.LastNDays, (object) 1, LogicalOperators.AND, 0, true));
        else if (ObjectsQuery._objectListFiltration.SelectedFilterGuid.ToString() == "cad0079a-306c-11d8-b4e9-00304f19f545")
          conditionStructureList.Add(new ConditionStructure(-13, RelationalOperators.LastNDays, (object) 7, LogicalOperators.AND, 0, true));
        else if (ObjectsQuery._objectListFiltration.SelectedFilterGuid.ToString() == "cad0079b-306c-11d8-b4e9-00304f19f545")
        {
          conditionStructureList.Add(new ConditionStructure(-13, RelationalOperators.LastNDays, (object) 30, LogicalOperators.AND, 0, true));
        }
        else
        {
          ISelectionsService service3 = ServicesManager.GetService(typeof (ISelectionsService)) as ISelectionsService;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(ObjectsQuery._objectListFiltration.SelectedFilterGuid, false);
            if (dbObject != null)
              joinedConditions = service3.GetConditionStructures((object) sessionKeeper.Session, dbObject.ObjectID);
          }
        }
      }
      if (conditionStructureList.Count > 0)
        queryParams.Conditions = ConditionStructure.Join(conditionStructureList.ToArray(), queryParams.Conditions);
      if (joinedConditions != null)
        queryParams.Conditions = ConditionStructure.Join(joinedConditions, queryParams.Conditions);
      if (structures != null && structures.Count > 0)
        queryParams.Conditions = ConditionStructure.Join(structures.ToArray(), queryParams.Conditions);
    }
    if (ObjectsQuery._objectListFiltration != null && ObjectsQuery._objectListFiltration.GlobalIndexSearchValue != null && ObjectsQuery._objectListFiltration.IsGlobalIndexSearchActived && !string.IsNullOrEmpty(ObjectsQuery._objectListFiltration.GlobalIndexSearchValue.Value))
      queryParams.Conditions = ConditionStructure.Join(new List<ConditionStructure>()
      {
        new ConditionStructure(0, RelationalOperators.InGlobalIndex, (object) ObjectsQuery._objectListFiltration.GlobalIndexSearchValue, LogicalOperators.AND, 0, true)
      }.ToArray(), queryParams.Conditions);
    return queryParams;
  }

  /// <summary>
  /// Добавляет к параметрам запроса условия, указанные в конструкторе
  /// запроса. Этот метод используется при чтении информации о конкретных
  /// объектах, указанных с помощью коллекции унифицированных идентификаторв.
  /// </summary>
  /// <param name="mapping">Схема отображения виртуальных колонок в поля источника данных</param>
  /// <param name="recordIds">?Массив унифицированных идентификаторов объектов</param>
  /// <returns>Параметры запроса к базе данных</returns>
  protected override DBRecordSetParams GetQueryParams(object[] recordIds, RecordMapping mapping)
  {
    DBRecordSetParams queryParams = base.GetQueryParams(recordIds, mapping);
    queryParams.Conditions = ConditionStructure.Join(this.GetFilterCondition(recordIds), queryParams.Conditions);
    if (this._conditions != null)
      queryParams.Conditions = ConditionStructure.Join(this._conditions, queryParams.Conditions);
    return queryParams;
  }

  protected override DBRecordSetParams GetQueryParams(RecordMapping mapping, bool withSortInfo)
  {
    DBRecordSetParams queryParams = new DBRecordSetParams((ConditionStructure[]) null);
    IEnumerable<NodeColumnID> source = ((IEnumerable<object>) mapping.Fields).Select<object, NodeColumnID>((System.Func<object, NodeColumnID>) (field => field as NodeColumnID)).Where<NodeColumnID>((System.Func<NodeColumnID, bool>) (field => field != null));
    int length1 = source.Count<NodeColumnID>();
    queryParams.Columns = new object[length1];
    queryParams.ColumnsInfo = new Intermech.Kernel.Search.ColumnInfo[length1];
    queryParams.ColumnNames = new ColumnNameMapping[length1];
    int index1 = 0;
    foreach (NodeColumnID nodeColumnId in source)
    {
      queryParams.Columns[index1] = nodeColumnId.ID;
      queryParams.ColumnsInfo[index1].AttributeSource = nodeColumnId.AttrSource;
      queryParams.ColumnNames[index1] = ColumnNameMapping.Index;
      ++index1;
    }
    if (withSortInfo && mapping.SortFields != null)
    {
      int length2 = ((IEnumerable<object>) mapping.SortFields).Count<object>((System.Func<object, bool>) (field => field is NodeColumnID));
      queryParams.SortColumns = new object[length2];
      queryParams.Orders = new SortOrders[length2];
      for (int index2 = 0; index2 < mapping.SortFields.Length; ++index2)
      {
        if (mapping.SortFields[index2] is NodeColumnID sortField)
        {
          queryParams.SortColumns[index2] = sortField.ID;
          queryParams.Orders[index2] = mapping.SortOrders[index2] == NodeColumnSortOrder.Ascending ? SortOrders.ASC : SortOrders.DESC;
        }
      }
    }
    return queryParams;
  }

  /// <summary>
  /// Возвращает таблицу, содержащую результаты запроса. Базовый класс
  /// вызывает этот метод, чтобы получить результаты запроса в формате
  /// источника данных, а затем транслирует их в унифицированный формат,
  /// понятный навигатору.
  /// </summary>
  /// <param name="queryParams">Параметры запроса к базе данных</param>
  /// <returns>Таблица с значениями атрибутов объектов</returns>
  protected override DataTable GetDataTable(DBRecordSetParams queryParams)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      queryParams.Tags = this._pluginsData;
      queryParams.Tags[(object) "{7FB30639-2F65-4407-B78E-523547B1B133}"] = (object) true;
      this.BeforeSelect(queryParams);
      BeforeClientRecordsSelectEventArgs args = new BeforeClientRecordsSelectEventArgs(queryParams, sessionKeeper.Session, (IServiceProvider) this.Services);
      QueryEvents.FireBeforeClientRecordsSelect((object) this, args);
      if (this.Services != null && this.Services.GetService(typeof (ObjectsSelectionOptionsHolder)) is ObjectsSelectionOptionsHolder service && service.Options.HasFlag((Enum) ObjectsSelectionOptions.ShowNotOwnedWorkCopies))
        queryParams.Tags[(object) "ShowNotOwnedWorkCopies"] = (object) true;
      return this.GetVirtualDataTable(new NavigatorVirtualColumnProviderArgs(this.mapping, this.OnSelect(sessionKeeper.Session, args.NewParameters.HasValue ? args.NewParameters.Value : queryParams), new ElementTypeInfo(this._objTypeID, AttributableElements.Object)));
    }
  }

  protected virtual void PrepareCollection(IDBObjectCollection objectCollection)
  {
    if (this.Services == null || !(this.Services.GetService(typeof (ObjectsSelectionOptionsHolder)) is ObjectsSelectionOptionsHolder service))
      return;
    if (service.Options.HasFlag((Enum) ObjectsSelectionOptions.LocalTypesMode) && objectCollection.ObjectTypeID == -1)
      objectCollection.LocalTypesMode = true;
    if (service.Options.HasFlag((Enum) ObjectsSelectionOptions.ShowAllModifications))
      objectCollection.ShowAllModifications = true;
    if (!service.Options.HasFlag((Enum) ObjectsSelectionOptions.TrashMode))
      return;
    objectCollection.TrashMode = true;
  }

  protected virtual DataTable OnSelect(IUserSession session, DBRecordSetParams queryParams)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(this._objTypeID);
    this.PrepareCollection(objectCollection);
    return objectCollection.Select(queryParams);
  }

  protected virtual void BeforeSelect(DBRecordSetParams queryParams)
  {
  }

  protected override INodeQuerySupport Support => this.support;

  /// <summary>
  /// Формирует условие запроса, которое позволяет получить информацию об
  /// интересующих объектах, которые указываются с помощью унифицированных
  /// идентификаторов.
  /// </summary>
  /// <param name="recordIds">?Массив унифицированных идентификаторов объектов</param>
  /// <returns>Условие запроса к базе данных</returns>
  protected ConditionStructure GetFilterCondition(object[] recordIds)
  {
    return new ConditionStructure(-2, RelationalOperators.In, (object) recordIds, LogicalOperators.NONE, 0, false);
  }

  /// <summary>Контейнер сервисов</summary>
  IServiceProvider IContextAware.Services
  {
    [DebuggerStepThrough] get => (IServiceProvider) this.Services;
    set => this.Services.AdvancedProvider = value;
  }

  /// <summary>
  /// В коллекцию PluginsData можно сохранять свою информацию в виде сериализуемых пар
  /// значений [Ключ] = [Значение]. Данная коллекция будет доступна на серверной стороне
  /// в Select у коллекции объектов.
  /// </summary>
  public HybridDictionary PluginsData
  {
    [DebuggerStepThrough] get => this._pluginsData;
  }
}
