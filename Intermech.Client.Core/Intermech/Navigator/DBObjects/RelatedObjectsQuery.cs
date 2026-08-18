
// Type: Intermech.Navigator.DBObjects.RelatedObjectsQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode;
using Intermech.Client.Core.Navigator.Classes.Providers;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Nodes;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Queries;
using Intermech.PropertyEditors;
using Intermech.Search.CompositionByObjectTypesFilters;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Linq;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Реализует запрос к базе данных на чтение инфрормации об объектах из
/// коллекции связей объектов, т.е. позволяет прочитать значения атрибутов не
/// только объектов, но и связей. Результаты запроса возвращаются в
/// унифицированном формате, воспринимаемом навигатором, т.е. для каждого
/// объекта предоставляется его идентификатор, поддерживающий интерфейс INodeID,
/// и значения указанных виртуальных колонок.
/// </summary>
public class RelatedObjectsQuery : 
  DBRecordsNodeQuery,
  INodePartContextAware,
  IContextAware,
  IFiltrateVersionsLogHolder
{
  private IRelatedObjectQueryFilterMode _queryFilter = (IRelatedObjectQueryFilterMode) new RelatedObjectQueryFilterMode();
  protected INodeQuerySupport support;
  protected long objId;
  protected int objTypeID;
  protected RelatedObjectsRole role;
  protected int relTypeId;
  protected object[] relTypeFields;
  protected ConditionStructure[] conditions;
  /// <summary>
  /// Настройки фильтрации (более низкий приоритет, чем filtrationOwnerID)
  /// </summary>
  protected IFiltrationClass filtrationClass;
  /// <summary>Уникальный ключ настроек фильтрации состава</summary>
  protected string filtrationOwnerID;
  protected IServiceProvider _services;
  protected ICurrentUserAndRole _userRole;
  protected static readonly object KeyField = (object) ObligatoryObjectAttributes.F_PRJLINK_ID;
  protected static readonly object[] GeneratorFields = new object[6]
  {
    (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
    (object) ObligatoryObjectAttributes.F_OBJECT_ID,
    (object) ObligatoryObjectAttributes.F_CHKOUT_BY,
    (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
    (object) new Guid("cad001f0-306c-11d8-b4e9-00304f19f545"),
    (object) new Guid("cad005f1-306c-11d8-b4e9-00304f19f545")
  };
  /// <summary>Протокол подбора версий</summary>
  protected FiltrateVersionsLog _log = new FiltrateVersionsLog();
  /// <summary>
  /// Идентификатор родительского типа объектов для типизированнго запроса в коллекцию связей
  /// </summary>
  protected int _parentObjTypeID = -1;
  /// <summary>
  /// Фильтровать набор данных после открытия.
  /// Используется для сортировки на клиенте, например в workflow (История утверждения)
  /// </summary>
  [Obsolete("Use QueryFilter instead. Will be removed in IPS8", true)]
  public bool FilterDataTable;
  /// <summary>
  /// Фильтровать набор данных в соответствии с настройками отображения составов
  /// </summary>
  [Obsolete("Use QueryFilter instead. Will be removed in IPS8", true)]
  public bool FilterDataByVersionRule = true;
  private RecordMapping _lastMapping;

  /// <summary>Параметры фильтрации данных</summary>
  public IRelatedObjectQueryFilterMode QueryFilter
  {
    get => this._queryFilter;
    set => this._queryFilter = value ?? throw new ArgumentNullException(nameof (QueryFilter));
  }

  /// <summary>
  /// Конструктор запроса, в результате выполнения которого будет прочитана
  /// информация о всех объектах, связанных с указанным объектом заданным
  /// типом связи и удовлетворяющих указанным условиям.
  /// </summary>
  /// <param name="support"></param>
  /// <param name="objId">Идентификатор версии объекта</param>
  /// <param name="objTypeID"></param>
  /// <param name="role">Роль связанных объектов</param>
  /// <param name="relTypeId">Идентификатор типа связи</param>
  /// <param name="conditions">Массив условий, которым должны удовлетворять связанные объекты</param>
  public RelatedObjectsQuery(
    INodeQuerySupport support,
    long objId,
    int objTypeID,
    RelatedObjectsRole role,
    int relTypeId,
    ConditionStructure[] conditions)
    : base(RelatedObjectsQuery.KeyField)
  {
    this.support = support;
    this.objId = objId;
    this.objTypeID = objTypeID;
    this.role = role;
    this.relTypeId = relTypeId;
    this.conditions = conditions;
  }

  /// <summary>
  /// Конструктор запроса, в результате выполнения которого будет прочитана
  /// информация о всех объектах, связанных с указанным объектом заданным
  /// типом связи и удовлетворяющих указанным условиям.
  /// </summary>
  /// <param name="support"></param>
  /// <param name="objId">Идентификатор версии объекта</param>
  /// <param name="objTypeID"></param>
  /// <param name="role">Роль связанных объектов</param>
  /// <param name="relTypeId">Идентификатор типа связи</param>
  /// <param name="parentObjTypeID">Идентификатор родительского типа объектов для типизированнго запроса в коллекцию связей</param>
  /// <param name="conditions">Массив условий, которым должны удовлетворять связанные объекты</param>
  public RelatedObjectsQuery(
    INodeQuerySupport support,
    long objId,
    int objTypeID,
    RelatedObjectsRole role,
    int relTypeId,
    int parentObjTypeID,
    ConditionStructure[] conditions)
    : this(support, objId, objTypeID, role, relTypeId, conditions)
  {
    this._parentObjTypeID = parentObjTypeID;
  }

  /// <summary>
  /// Устанавливает или возвращает объект, задающий настройки фильтрации состава или применяемости объекта.
  /// </summary>
  public IFiltrationClass FiltrationClass
  {
    [DebuggerStepThrough] get => this.filtrationClass;
    [DebuggerStepThrough] set => this.filtrationClass = value;
  }

  /// <summary>
  /// Идентификатор родительского типа объектов для типизированнго запроса в коллекцию связей.
  /// </summary>
  public int ParentObjectTypeID
  {
    get => this._parentObjTypeID;
    set => this._parentObjTypeID = value;
  }

  /// <summary>Контейнер сервисов.</summary>
  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => this._services;
    [DebuggerStepThrough] set => this._services = value;
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
    this._lastMapping = mapping;
    DBRecordSetParams queryParams = base.GetQueryParams(bookmark, count, mapping);
    if (this.conditions != null)
      queryParams.Conditions = ConditionStructure.Join(this.conditions, queryParams.Conditions);
    if (this.objId != 0L)
      queryParams.Conditions = ConditionStructure.Join(this.GetLevelCondition(), queryParams.Conditions);
    return queryParams;
  }

  /// <summary>
  /// Добавляет к параметрам запроса условие, позволяющее получить информацию
  /// о интересующих объектах, входящих в состав указанного объекта заданной
  /// связью.
  /// </summary>
  /// <param name="mapping">Схема отображения виртуальных колонок в поля источника данных</param>
  /// <param name="recordIds">?Массив унифицированных идентификаторов объектов</param>
  /// <returns>Параметры запроса к базе данных</returns>
  protected override DBRecordSetParams GetQueryParams(object[] recordIds, RecordMapping mapping)
  {
    DBRecordSetParams queryParams = base.GetQueryParams(recordIds, mapping);
    queryParams.Conditions = ConditionStructure.Join(this.GetFilterCondition(recordIds), queryParams.Conditions);
    if (this.conditions != null)
      queryParams.Conditions = ConditionStructure.Join(this.conditions, queryParams.Conditions);
    if (this.objId != 0L)
      queryParams.Conditions = ConditionStructure.Join(this.GetLevelCondition(), queryParams.Conditions);
    return queryParams;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="mapping"></param>
  /// <param name="withSortInfo"></param>
  /// <returns></returns>
  protected override DBRecordSetParams GetQueryParams(RecordMapping mapping, bool withSortInfo)
  {
    DBRecordSetParams queryParams = new DBRecordSetParams((ConditionStructure[]) null);
    IEnumerable<NodeColumnID> source = ((IEnumerable<object>) mapping.Fields).Select<object, NodeColumnID>((System.Func<object, NodeColumnID>) (field => field as NodeColumnID)).Where<NodeColumnID>((System.Func<NodeColumnID, bool>) (field => field != null));
    int length = source.Count<NodeColumnID>();
    queryParams.Columns = new object[length];
    queryParams.ColumnsInfo = new Intermech.Kernel.Search.ColumnInfo[length];
    queryParams.ColumnNames = new ColumnNameMapping[length];
    int index1 = 0;
    foreach (NodeColumnID nodeColumnId in source)
    {
      queryParams.Columns[index1] = nodeColumnId.ID;
      queryParams.ColumnsInfo[index1].AttributeID = nodeColumnId.ID;
      queryParams.ColumnsInfo[index1].AttributeSource = nodeColumnId.AttrSource;
      queryParams.ColumnNames[index1] = ColumnNameMapping.Index;
      ++index1;
    }
    if (withSortInfo && mapping.SortFields != null)
    {
      List<object> objectList = new List<object>();
      List<SortOrders> sortOrdersList = new List<SortOrders>();
      for (int index2 = 0; index2 < mapping.SortFields.Length; ++index2)
      {
        if (mapping.SortFields[index2] is NodeColumnID sortField && !objectList.Contains(sortField.ID))
        {
          objectList.Add(sortField.ID);
          sortOrdersList.Add(mapping.SortOrders[index2] == NodeColumnSortOrder.Ascending ? SortOrders.ASC : SortOrders.DESC);
        }
      }
      queryParams.SortColumns = objectList.ToArray();
      queryParams.Orders = sortOrdersList.ToArray();
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
    this._log[this.relTypeId] = (Dictionary<FiltrateVersionsLogEntryKey, FiltrateVersionsLogEntry>) null;
    this._userRole = this._userRole == null ? ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole : this._userRole;
    RelationPair service1 = this.Services != null ? this.Services.GetService(typeof (RelationPair)) as RelationPair : (RelationPair) null;
    List<int> intList = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      List<int> list = this.GetChildObjectTypes(sessionKeeper.Session, ref queryParams);
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(this.relTypeId);
      if (!string.IsNullOrEmpty(this.filtrationOwnerID))
        relationCollection.FiltrationOwnerID = this.filtrationOwnerID;
      else if (this.filtrationClass != null)
        relationCollection.FiltrationOwnerID = this.filtrationClass.FiltrationOwnerID;
      relationCollection.FiltrationRule = this.Services != null ? this.Services.GetService(typeof (VersionsRule)) as VersionsRule : (VersionsRule) null;
      this.GetClientPluginsData(ref queryParams.Tags);
      if (list != null && list.Count > 0 && this._parentObjTypeID == -1)
      {
        ICompositionLoadService customService = sessionKeeper.Session.GetCustomService(typeof (ICompositionLoadService)) as ICompositionLoadService;
        List<int> childObjectTypes = customService.GetPresentCompositionTypes((object) sessionKeeper.Session.SessionGUID, (IEnumerable<long>) new long[1]
        {
          this.objId
        }, this.relTypeId, (this.role == RelatedObjectsRole.Composition ? 1 : 0) != 0);
        if (this.objId < 0L && childObjectTypes == null && this.role == RelatedObjectsRole.Composition)
          childObjectTypes = customService.GetPresentCompositionTypes((object) sessionKeeper.Session.SessionGUID, (IEnumerable<long>) new long[1]
          {
            Math.Abs(this.objId)
          }, this.relTypeId, (this.role == RelatedObjectsRole.Composition ? 1 : 0) != 0);
        if (childObjectTypes == null)
          return (DataTable) null;
        if (this.role == RelatedObjectsRole.Composition)
        {
          if (!OptimizationSettings.FullCompositionsSorting)
          {
            childObjectTypes = MetaDataHelper.OptimizeChildObjectTypes((IEnumerable<int>) childObjectTypes);
          }
          else
          {
            int[] array = childObjectTypes.ToArray();
            foreach (int num in array)
            {
              if (!MetaDataHelper.IsLocalObjectType(num) && ((IEnumerable<int>) array).Contains<int>(MetaDataHelper.GetObjectTypeParentID(num)))
                childObjectTypes.Remove(num);
            }
          }
        }
        else if (this.role == RelatedObjectsRole.Applicability && !OptimizationSettings.FullCompositionsSorting)
          childObjectTypes = MetaDataHelper.OptimizeChildObjectTypes((IEnumerable<int>) childObjectTypes);
        if (childObjectTypes.Count > 1 && list != null && list.Count > 1)
          childObjectTypes.Sort((IComparer<int>) new RelatedObjectsQuery.IndexComparer((IList<int>) list));
        list = childObjectTypes;
      }
      relationCollection.ChildObjectTypes = (IList<int>) list;
      if (queryParams.Tags == null)
        queryParams.Tags = new HybridDictionary(2, true);
      if (service1 != null && !service1.Empty)
        queryParams.Tags[(object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}"] = (object) service1;
      if (ServicesManager.GetService(typeof (IFiltrationService)) is IFiltrationService service2 && service2.Filtration != null && service2.Filtration.Tags != null)
        queryParams.Tags[(object) "{4534BBF7-86AF-4BCB-B7FF-C9AE40D28CB4}"] = service2.Filtration.Tags[(object) "{4534BBF7-86AF-4BCB-B7FF-C9AE40D28CB4}"];
      BeforeClientRecordsSelectEventArgs args = new BeforeClientRecordsSelectEventArgs(queryParams, sessionKeeper.Session, this.Services);
      QueryEvents.FireBeforeClientRecordsSelect((object) this, args);
      DataTable dataTable;
      if (this.role == RelatedObjectsRole.Composition || this.objId == 0L)
      {
        dataTable = relationCollection.Select(args.NewParameters.HasValue ? args.NewParameters.Value : queryParams);
      }
      else
      {
        if (queryParams.Tags == null)
          queryParams.Tags = new HybridDictionary();
        if (sessionKeeper.Session.GetObjectInfo(this.objId).Empty && this.objId < 0L)
          this.objId = Math.Abs(this.objId);
        dataTable = relationCollection.EntersInVersion(args.NewParameters.HasValue ? args.NewParameters.Value : queryParams, this.objId);
      }
      if (dataTable != null && dataTable.ExtendedProperties.ContainsKey((object) FiltrateVersionsLog.Key) && UISettings.ShowVersionsLog)
        this._log.AssignRelTypeLog(dataTable.ExtendedProperties[(object) FiltrateVersionsLog.Key]);
      if (this.QueryFilter.FilterDataByVersionRule)
        this.FilterDataTableByAutoSortVersionRule(queryParams, dataTable);
      if (this.Services is AdvancedServiceContainer services)
      {
        if (services.GetService(typeof (FiltrateVersionsLog)) is FiltrateVersionsLog service3)
        {
          if (UISettings.ShowVersionsLog)
            service3.AssignRelTypeLog((object) this._log.ToString(this.relTypeId));
        }
        else if (UISettings.ShowVersionsLog)
        {
          FiltrateVersionsLog serviceInstance = new FiltrateVersionsLog();
          serviceInstance.AssignRelTypeLog((object) this._log.ToString(this.relTypeId));
          services.AddService(typeof (FiltrateVersionsLog), (object) serviceInstance);
        }
      }
      this.ApplyCompositionByObjectTypesFilter(dataTable, queryParams);
      if (this.QueryFilter.FilterDataTable && this._lastMapping != null)
        dataTable = this.FilterTable(this._lastMapping, dataTable);
      return this.GetVirtualDataTable(new NavigatorVirtualColumnProviderArgs(this.mapping, dataTable, new ElementTypeInfo(this.relTypeId, AttributableElements.Relation)));
    }
  }

  protected virtual void FilterDataTableByAutoSortVersionRule(
    DBRecordSetParams queryParams,
    DataTable mainResult)
  {
    switch (this.role)
    {
      case RelatedObjectsRole.Composition:
        this.FilterCompositionByAutosortVersionRule(mainResult, queryParams.Columns);
        break;
      case RelatedObjectsRole.Applicability:
        if (ObjectApplicabilityByRelationsNode.GetRelationTypeIdsFromCompositionsAutosortRule(this.objTypeID).Length == 0)
          break;
        this.FilterApplicabilitiesByAutosortVersionRule(mainResult, ((IEnumerable<object>) queryParams.Columns).ToList<object>());
        break;
    }
  }

  private void ApplyCompositionByObjectTypesFilter(
    DataTable dataTable,
    DBRecordSetParams recordSetParams)
  {
    if (this.Services == null || !(this.Services.GetService(typeof (IViewState)) is IViewState service1) || (service1.ViewState & ViewStateFlags.NodeInTree) <= ViewStateFlags.None || !(this.Services.GetService(typeof (ICompositionByObjectTypesFilterProvider)) is ICompositionByObjectTypesFilterProvider service2) || service2.Filter == null)
      return;
    List<int> list1 = ((IEnumerable<int>) service2.Filter.GetCheckedPartTypeIdsForProjectType(this.objTypeID)).ToList<int>();
    if (list1.Count <= 0)
      return;
    List<int> list2 = ((IEnumerable<int>) this.GetAllHiddenTypes(list1.ToArray())).ToList<int>();
    int columnIndex = -1;
    if (recordSetParams.Columns != null)
    {
      object obj = ((IEnumerable<object>) recordSetParams.Columns).FirstOrDefault<object>((System.Func<object, bool>) (o => object.Equals(o, (object) ObligatoryObjectAttributes.F_OBJECT_TYPE) || object.Equals(o, (object) -7)));
      if (obj != null)
        columnIndex = Array.IndexOf<object>(recordSetParams.Columns, obj);
    }
    if (columnIndex == -1 && recordSetParams.ColumnsInfo != null)
    {
      Intermech.Kernel.Search.ColumnInfo columnInfo = ((IEnumerable<Intermech.Kernel.Search.ColumnInfo>) recordSetParams.ColumnsInfo).FirstOrDefault<Intermech.Kernel.Search.ColumnInfo>((System.Func<Intermech.Kernel.Search.ColumnInfo, bool>) (o => object.Equals(o.AttributeID, (object) ObligatoryObjectAttributes.F_OBJECT_TYPE) || object.Equals(o.AttributeID, (object) -7)));
      if (columnInfo.AttributeID != null)
        columnIndex = Array.IndexOf<Intermech.Kernel.Search.ColumnInfo>(recordSetParams.ColumnsInfo, columnInfo);
    }
    foreach (DataRow row in dataTable.Rows.Cast<DataRow>().ToArray<DataRow>())
    {
      int int32Value = DataSetProcessor.GetInt32Value(row, columnIndex, -1);
      if (list2.Contains(int32Value))
        dataTable.Rows.Remove(row);
    }
  }

  private int[] GetAllHiddenTypes(int[] hiddenTypes)
  {
    List<int> intList = new List<int>();
    foreach (int hiddenType in hiddenTypes)
    {
      if (ObjectTypeHelper.IsAbstract(hiddenType))
      {
        foreach (int objectTypeID in MetaDataHelper.GetObjectTypeChildrenID(hiddenType))
        {
          if (ObjectTypeHelper.IsAbstract(objectTypeID))
            intList.AddRange((IEnumerable<int>) this.GetAllHiddenTypes(new int[1]
            {
              objectTypeID
            }));
          else
            intList.Add(objectTypeID);
        }
      }
      else
        intList.Add(hiddenType);
    }
    return intList.ToArray();
  }

  protected virtual void GetClientPluginsData(ref HybridDictionary hybridDictionary)
  {
    if (!(ServicesManager.GetService(typeof (IClientPluginsService)) is IClientPluginsService service))
      return;
    service.GetClientPluginsData(ref hybridDictionary);
  }

  private void FilterApplicabilitiesByAutosortVersionRule(
    DataTable mainResult,
    List<object> columns)
  {
    CompositionsAutosortRule rule = this._userRole.Rule;
    int columnIndex1 = columns.IndexOf((object) ObligatoryObjectAttributes.F_OBJECT_TYPE);
    int columnIndex2 = columns.IndexOf((object) ObligatoryObjectAttributes.F_RELATION_TYPE);
    for (int index = mainResult.Rows.Count - 1; index >= 0; --index)
    {
      DataRow row = mainResult.Rows[index];
      int int32Value = DataSetProcessor.GetInt32Value(row, columnIndex1, -1);
      List<int> parentsIdReverse = MetaDataHelper.GetObjectTypeParentsIDReverse(int32Value);
      parentsIdReverse.Add(int32Value);
      parentsIdReverse.Reverse();
      foreach (int num in parentsIdReverse)
      {
        int currentObjectTypeID = num;
        ParentObjectType parentObjectType = rule.ParentObjectTypes.Where<ParentObjectType>((System.Func<ParentObjectType, bool>) (o => o.ObjectTypeID == currentObjectTypeID)).FirstOrDefault<ParentObjectType>();
        if (parentObjectType == null)
        {
          if (currentObjectTypeID == parentsIdReverse.Last<int>())
          {
            row.Delete();
            break;
          }
        }
        else
        {
          int relationTypeID = DataSetProcessor.GetInt32Value(row, columnIndex2, -1);
          ChildRelationType childRelationType = parentObjectType.ChildRelationTypes.Where<ChildRelationType>((System.Func<ChildRelationType, bool>) (o => o.RelationTypeID == relationTypeID)).FirstOrDefault<ChildRelationType>();
          if (childRelationType == null || !childRelationType.Visible)
          {
            row.Delete();
            break;
          }
          if (childRelationType != null)
          {
            if (childRelationType.Visible)
              break;
          }
        }
      }
    }
    mainResult.AcceptChanges();
  }

  private void FilterCompositionByAutosortVersionRule(DataTable dataTable, object[] columns)
  {
    CompositionsAutosortRule rule = this._userRole.Rule;
    if (rule == null)
      return;
    ParentObjectType parentObjectType = rule.ParentObjectTypes.FirstOrDefault<ParentObjectType>((System.Func<ParentObjectType, bool>) (o => o.ObjectTypeID == this.objTypeID));
    if (parentObjectType == null)
      return;
    ChildRelationType childRelationType = parentObjectType.ChildRelationTypes.FirstOrDefault<ChildRelationType>((System.Func<ChildRelationType, bool>) (o => o.RelationTypeID == this.relTypeId));
    if (childRelationType == null)
      return;
    int obligatoryAttribute = this.GetColumnIndexForObligatoryAttribute(columns, ObligatoryObjectAttributes.F_OBJECT_TYPE);
    if (obligatoryAttribute < 0)
      return;
    for (int index = dataTable.Rows.Count - 1; index >= 0; --index)
    {
      DataRow row = dataTable.Rows[index];
      int int32Value = DataSetProcessor.GetInt32Value(row, obligatoryAttribute, -1);
      if (!ObjectTypeHelper.IsUnknownObjectTypeID(int32Value) && !this.IsVisibleChildObjectType(childRelationType, int32Value))
        row.Delete();
    }
    dataTable.AcceptChanges();
  }

  private int GetColumnIndexForObligatoryAttribute(
    object[] columns,
    ObligatoryObjectAttributes obligatoryAttribute)
  {
    int obligatoryAttribute1 = Array.IndexOf<object>(columns, (object) obligatoryAttribute);
    if (obligatoryAttribute1 == -1)
      obligatoryAttribute1 = Array.IndexOf<object>(columns, (object) (int) obligatoryAttribute);
    return obligatoryAttribute1;
  }

  private bool IsVisibleChildObjectType(ChildRelationType childRelationType, int objectTypeID)
  {
    foreach (ChildObjectType childObjectType in childRelationType.ChildObjectTypes)
    {
      if (!this.IsVisibleChildObjectType(childObjectType, objectTypeID))
        return false;
    }
    return true;
  }

  private bool IsVisibleChildObjectType(ChildObjectType childObjectType, int objectTypeID)
  {
    if (childObjectType.ObjectTypeID == objectTypeID && !childObjectType.Visible)
      return false;
    foreach (ChildObjectType child in childObjectType.Children)
    {
      if (!this.IsVisibleChildObjectType(child, objectTypeID))
        return false;
    }
    return true;
  }

  /// <summary>Оптимизация запроса.</summary>
  /// <param name="session">Сессия</param>
  /// <param name="queryParams">Параметры запроса</param>
  /// <returns>Список дочерних типов объектов для типизированного запроса в коллекцию связей</returns>
  private List<int> GetChildObjectTypes(IUserSession session, ref DBRecordSetParams queryParams)
  {
    List<int> childObjectTypes = new List<int>();
    if (this._parentObjTypeID != -1)
    {
      childObjectTypes = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(this._parentObjTypeID);
      if (childObjectTypes.Count > 0)
        return childObjectTypes;
    }
    bool flag1 = false;
    bool flag2 = false;
    CompositionsAutosortRule rule = this._userRole.Rule;
    if (this.role == RelatedObjectsRole.Composition)
    {
      int index1 = rule.IndexOfParentObjectType(this.objTypeID, true);
      if (index1 >= 0)
      {
        ChildRelationType childRelationType = rule.ParentObjectTypes[index1][this.relTypeId];
        if (childRelationType != null)
        {
          for (int index2 = 0; index2 < childRelationType.ChildObjectTypes.Count; ++index2)
          {
            List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(childRelationType.ChildObjectTypes[index2].ObjectTypeID);
            if (!flag1)
            {
              for (int index3 = 0; index3 < childrenIdRecursive.Count; ++index3)
              {
                IMSObjectType objectType = MetaDataHelper.GetObjectType(childrenIdRecursive[index3]);
                flag1 = objectType != null && objectType.IsLocalType;
                if (flag1)
                  break;
              }
            }
            for (int index4 = 0; index4 < childrenIdRecursive.Count; ++index4)
            {
              if (childObjectTypes.IndexOf(childrenIdRecursive[index4]) < 0)
                childObjectTypes.Add(childrenIdRecursive[index4]);
            }
          }
        }
        if (!flag1 && !OptimizationSettings.FullCompositionsSorting)
          childObjectTypes.Clear();
        flag2 = childObjectTypes.Count > 0;
      }
      if (!flag2)
      {
        DataTable applicabilitiesList = session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(this.relTypeId, -1, this.objTypeID);
        int num1 = -1;
        if (applicabilitiesList != null && applicabilitiesList.Rows.Count > 0)
        {
          List<int> intList1 = new List<int>(applicabilitiesList.Rows.Count);
          foreach (DataRow row in (InternalDataCollectionBase) applicabilitiesList.Rows)
          {
            int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
            if (!intList1.Contains(int32))
              intList1.Add(int32);
          }
          if (intList1.Count == 1)
          {
            num1 = intList1[0];
          }
          else
          {
            List<List<int>> intListList = new List<List<int>>(intList1.Count);
            for (int index5 = 0; index5 < intList1.Count; ++index5)
            {
              List<int> parentsIdReverse = MetaDataHelper.GetObjectTypeParentsIDReverse(intList1[index5]);
              List<int> intList2 = new List<int>(parentsIdReverse.Count);
              for (int index6 = parentsIdReverse.Count - 1; index6 >= 0 && !MetaDataHelper.IsLocalObjectType(parentsIdReverse[index6]); --index6)
                intList2.Insert(0, parentsIdReverse[index6]);
              intListList.Add(intList2);
            }
            int index7 = 0;
            bool flag3 = false;
            while (true)
            {
              int num2 = -1;
              for (int index8 = 0; index8 < intListList.Count; ++index8)
              {
                if (intListList[index8].Count <= index7)
                {
                  flag3 = true;
                  break;
                }
                if (!flag3)
                {
                  num2 = intListList[0][index7];
                  if (num2 != intListList[index8][index7])
                  {
                    flag3 = true;
                    break;
                  }
                  if (flag3)
                    break;
                }
                else
                  break;
              }
              if (!flag3)
              {
                if (num2 != -1)
                  num1 = num2;
                ++index7;
              }
              else
                break;
            }
          }
        }
        applicabilitiesList?.Dispose();
        childObjectTypes.Add(num1);
      }
    }
    else
    {
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
      DataTable applicabilitiesList = session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(this.relTypeId, this.objTypeID, -1);
      List<int> source = new List<int>();
      for (int index = 0; index < applicabilitiesList.Rows.Count; ++index)
      {
        int int32 = Convert.ToInt32(applicabilitiesList.Rows[index]["F_INOBJECT_TYPE"]);
        if (!source.Contains(int32))
          source.Add(int32);
      }
      foreach (int childTypeID in (IEnumerable<int>) source.OrderBy<int, int>((System.Func<int, int>) (o => o)))
      {
        List<int> objectTypeParentsId = MetaDataHelper.GetObjectTypeParentsID(childTypeID);
        objectTypeParentsId.Insert(0, childTypeID);
        for (int index = 0; index < objectTypeParentsId.Count; ++index)
        {
          if (!childObjectTypes.Contains(objectTypeParentsId[index]))
          {
            if (MetaDataHelper.IsLocalObjectType(objectTypeParentsId[index]))
            {
              childObjectTypes.Add(objectTypeParentsId[index]);
              break;
            }
            if (index == objectTypeParentsId.Count - 1)
              childObjectTypes.Add(objectTypeParentsId[index]);
          }
        }
      }
      if (conditionStructureList.Count > 0)
      {
        for (int index = 0; index < queryParams.Conditions.Length; ++index)
        {
          if (index == queryParams.Conditions.Length - 1)
            queryParams.Conditions[index].LogicalOperator = LogicalOperators.AND;
          conditionStructureList.Insert(0, queryParams.Conditions[index]);
        }
        queryParams.Conditions = conditionStructureList.ToArray();
      }
    }
    return childObjectTypes;
  }

  /// <summary>
  /// 
  /// </summary>
  protected override INodeQuerySupport Support => this.support;

  /// <summary>
  /// Создает и возвращает условие, позволяющее найти объекты, принадлежащие
  /// составу/применяемости указанного объекта
  /// </summary>
  /// <returns>Условие запроса к базе данных</returns>
  private ConditionStructure GetLevelCondition()
  {
    int attributeID = this.role == RelatedObjectsRole.Composition ? -21 : -22;
    long conditionValue = this.objId;
    if (this.role == RelatedObjectsRole.Applicability)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        conditionValue = sessionKeeper.Session.GetObjectF_ID(Math.Abs(this.objId));
    }
    return new ConditionStructure(attributeID, RelationalOperators.Equal, (object) conditionValue, LogicalOperators.NONE, 0, false);
  }

  /// <summary>
  /// Формирует условие запроса, которое позволяет получить информацию об
  /// интересующих объектах, которые указываютcя с помощью унифицированных
  /// идентификаторов.
  /// </summary>
  /// <param name="recordIds">Массив унифицированных идентификаторов объектов</param>
  /// <returns>Условие запроса к базе данных</returns>
  private ConditionStructure GetFilterCondition(object[] recordIds)
  {
    return recordIds.Length != 0 && recordIds[0] is Guid ? new ConditionStructure(-26, RelationalOperators.In, (object) recordIds, LogicalOperators.NONE, 0, false) : new ConditionStructure(-20, RelationalOperators.In, (object) recordIds, LogicalOperators.NONE, 0, false);
  }

  /// <summary>
  /// Возвращает массив имен атрибутов, относящихся к типу связи, а не к типу
  /// объекта. Используется для определения источника значений этих атрибутов.
  /// </summary>
  private object[] RelationTypeFields
  {
    get
    {
      if (this.relTypeFields == null)
      {
        using (SessionKeeper keeper = new SessionKeeper())
        {
          List<int> typesForRelationType = Helper.GetTypesForRelationType(keeper, this.relTypeId);
          this.relTypeFields = new object[typesForRelationType.Count];
          for (int index = 0; index < typesForRelationType.Count; ++index)
          {
            IDBAttributeType attributeType = keeper.Session.GetAttributeType(typesForRelationType[index], false);
            if (attributeType != null)
              this.relTypeFields[index] = (object) attributeType.Name;
          }
        }
      }
      return this.relTypeFields;
    }
  }

  /// <summary>Протокол подбора версий.</summary>
  public FiltrateVersionsLog Log
  {
    [DebuggerStepThrough] get => this._log;
    [DebuggerStepThrough] set => this._log.Assign((object) value);
  }

  public object NodePart => (object) (this.support as INodePart);

  /// <summary>
  /// Вспомогательный класс для упорядочивания элементов в массивах.
  /// Позволяет отсортировать элементы в массиве согласно их позициям
  /// в другом эталонном массиве, либо по значениям, если не задан эталон
  /// </summary>
  protected class IndexComparer : IComparer<int>
  {
    /// <summary>Массив-эталон</summary>
    private IList<int> _list;

    /// <summary>Создать экземпляр класса</summary>
    /// <param name="list">Массив-эталон</param>
    public IndexComparer(IList<int> list) => this._list = list;

    /// <summary>
    /// Отыскать существующий в эталонном массиве родительский тип объекта
    /// для указанного дочернего типа
    /// </summary>
    /// <param name="objType">Дочерний тип</param>
    /// <returns>Найденный в эталонном списке тип или значение objType</returns>
    public int ExistingType(int objType)
    {
      if (this._list == null || this._list.IndexOf(objType) >= 0)
        return objType;
      for (int objectTypeParentId = MetaDataHelper.GetObjectTypeParentID(objType); objectTypeParentId != -1; objectTypeParentId = MetaDataHelper.GetObjectTypeParentID(objectTypeParentId))
      {
        if (this._list.IndexOf(objectTypeParentId) >= 0)
          return objectTypeParentId;
      }
      return objType;
    }

    /// <summary>Сравнить два числа из массивов</summary>
    /// <param name="x">Первое число</param>
    /// <param name="y">Второе число</param>
    /// <returns>-1, 0, 1</returns>
    public int Compare(int x, int y)
    {
      return this._list == null ? x.CompareTo(y) : this._list.IndexOf(this.ExistingType(x)).CompareTo(this._list.IndexOf(this.ExistingType(y)));
    }
  }
}
