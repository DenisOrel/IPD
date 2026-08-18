
// Type: Intermech.Navigator.Utils
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Contexts;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Persistence;
using Intermech.Search;
using Intermech.Search.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Navigator;

public sealed class Utils
{
  private static IDictionary<NodeColumnSortOrder, NodeColumnCollection> _navigatorColumns = (IDictionary<NodeColumnSortOrder, NodeColumnCollection>) new Dictionary<NodeColumnSortOrder, NodeColumnCollection>(Enum.GetValues(typeof (NodeColumnSortOrder)).Length);
  /// <summary>
  /// Отображать окно по выбору контекстов редактирования при открытии объекта в новом окне
  /// </summary>
  private static bool _showSelectContextWindow = true;

  /// <summary>
  /// Возвращает элемент-папку из пространства навигации, который описывается указанным путем.
  /// </summary>
  /// <param name="path">Полный путь к элементу навигации</param>
  /// <param name="services">Дополнительные сервисы</param>
  /// <returns>Элемент навигации</returns>
  public static INode GetHandlerFromPath(NodeIDPath path, System.IServiceProvider services)
  {
    INode handlerFromPath = (INode) new EtherealNode(path.RootDescriptor);
    for (int Index = 0; Index < path.Length; ++Index)
    {
      handlerFromPath = handlerFromPath.GetChild(path[Index]);
      if (handlerFromPath == null)
        throw new InvalidOperationException();
      if (handlerFromPath is IContextAware contextAware)
        contextAware.Services = services;
    }
    return handlerFromPath;
  }

  public static object GetDataFromPath(NodeIDPath path, System.Type dataFormat, System.IServiceProvider services)
  {
    INode node = (INode) new EtherealNode(path.RootDescriptor);
    for (int Index = 0; Index < path.Length - 1; ++Index)
    {
      node = node.GetChild(path[Index]);
      if (node == null)
        return (object) null;
      if (node is IContextAware contextAware)
        contextAware.Services = services;
    }
    return node.GetData(path.LastID, dataFormat);
  }

  public static PersistentState[] SerializePath(NodeIDPath path, System.IServiceProvider services)
  {
    if (path == null || !(path.RootDescriptor is IPersistable))
      return (PersistentState[]) null;
    List<PersistentState> persistentStateList = new List<PersistentState>(path.Length + 1);
    persistentStateList.Add(FormatterServices.GetObjectState((object) path.RootDescriptor));
    if (path.Length > 0)
    {
      PersistentState persistentState1 = path.RootDescriptor.Serialize(path[0]);
      if (persistentState1 == null)
        return persistentStateList.ToArray();
      persistentStateList.Add(persistentState1);
      if (path.Length > 1)
      {
        INode child = path.RootDescriptor.GetChild(path[0]);
        if (child is IContextAware contextAware1)
          contextAware1.Services = services;
        for (int Index = 1; Index < path.Length; ++Index)
        {
          PersistentState persistentState2 = child.Serialize(path[Index]);
          if (persistentState2 != null)
          {
            persistentStateList.Add(persistentState2);
            if (Index + 1 < path.Length)
            {
              child = child.GetChild(path[Index]);
              if (child is IContextAware contextAware2)
                contextAware2.Services = services;
            }
          }
          else
            break;
        }
      }
    }
    return persistentStateList.ToArray();
  }

  public static NodeIDPath DeserializePath(PersistentState[] persistPath, System.IServiceProvider services)
  {
    if (persistPath == null || persistPath.Length == 0)
      return new NodeIDPath((IDescriptor) null);
    IDescriptor rootDescriptor = (IDescriptor) FormatterServices.RestoreObject(persistPath[0]);
    NodeIDPath nodeIdPath = new NodeIDPath(rootDescriptor);
    if (persistPath.Length > 1)
    {
      nodeIdPath.Add(rootDescriptor.Deserialize(persistPath[1]), false);
      if (persistPath.Length > 2)
      {
        INode child = rootDescriptor.GetChild(nodeIdPath[0]);
        if (child == null)
          return nodeIdPath;
        if (child is IContextAware contextAware1)
          contextAware1.Services = services;
        for (int index = 2; index < persistPath.Length; ++index)
        {
          INodeID NodeID = child.Deserialize(persistPath[index]);
          if (NodeID != null)
          {
            nodeIdPath.Add(NodeID);
            if (index + 1 < persistPath.Length)
            {
              child = child.GetChild(nodeIdPath[index - 1]);
              if (child is IContextAware contextAware2)
                contextAware2.Services = services;
            }
            if (child == null)
              break;
          }
          else
            break;
        }
      }
      return nodeIdPath;
    }
    return nodeIdPath == null || nodeIdPath.RootDescriptor == null ? (NodeIDPath) null : nodeIdPath;
  }

  /// <summary>
  /// Создает коллекцию колонок, состоящую из одной виртуальной колонки -
  /// "Заголовок объекта". Эта коллекция используется навигатором для показа
  /// дерева в режиме одной колонки.
  /// </summary>
  /// <param name="sortOrder"></param>
  /// <returns></returns>
  public static NodeColumnCollection CaptionColumnOnly(NodeColumnSortOrder sortOrder)
  {
    return new NodeColumnCollection()
    {
      {
        ((IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes))).CreateColumn(Consts.NavigatorColumnSchemeGuid, (object) "F_CAPTION", sortOrder, 0),
        250
      }
    };
  }

  /// <summary>
  /// Создает коллекцию колонок, состоящую из виртуальной колонки -
  /// "Заголовок объекта", а также из колонки "Статусы"
  /// </summary>
  /// <param name="sortOrder"></param>
  /// <returns></returns>
  public static NodeColumnCollection CaptionAndStatesesColumns(NodeColumnSortOrder sortOrder)
  {
    NodeColumnCollection columnCollection = new NodeColumnCollection();
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    columnCollection.Add(service.CreateColumn(Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.CAPTION, sortOrder, sortOrder != NodeColumnSortOrder.None ? 0 : -1), 250);
    columnCollection.Add(service.CreateColumn(Consts.NavigatorColumnSchemeGuid, (object) "F_STATUSES", NodeColumnSortOrder.None, -1), 100);
    return columnCollection;
  }

  /// <summary>
  /// Создает коллекцию колонок, состоящую из виртуальной колонки -
  /// "Заголовок объекта", а также из колонки "Статусы" - для гридов
  /// </summary>
  /// <param name="sortOrder"></param>
  /// <returns></returns>
  public static NodeColumnCollection CaptionAndStatesesGridColumns(NodeColumnSortOrder sortOrder)
  {
    return new NodeColumnCollection()
    {
      {
        Holder.ColumnSchemes.CreateColumn(Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.CAPTION, sortOrder, sortOrder != NodeColumnSortOrder.None ? 0 : -1),
        250
      },
      {
        Holder.ColumnSchemes.CreateColumn(Consts.NavigatorColumnSchemeGuid, (object) "F_STATUSES", NodeColumnSortOrder.None, -1),
        100
      }
    };
  }

  private static NodeColumnCollection CreateNavigatorColumns(NodeColumnSortOrder captionSortOrder)
  {
    NodeColumnCollection columns = new NodeColumnCollection();
    columns.Add(Holder.ColumnSchemes.CreateColumn(Consts.NavigatorColumnSchemeGuid, (object) "F_CAPTION"), 400);
    columns.Add(Holder.ColumnSchemes.CreateColumn(Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.CAPTION, captionSortOrder, captionSortOrder != NodeColumnSortOrder.None ? 0 : -1), 250);
    columns.Add(Holder.ColumnSchemes.CreateColumn(Consts.NavigatorColumnSchemeGuid, (object) "F_STATUSES", NodeColumnSortOrder.None, -1), 100);
    Intermech.Navigator.DBObjects.Helper.AddObligatoryColumns(columns, false, true);
    Intermech.Navigator.DBObjects.Helper.AddObligatoryColumnsAdv(columns);
    Intermech.Navigator.DBObjects.Helper.AddObligatoryColumnsRelation(columns);
    Intermech.Navigator.DBObjects.Helper.AddObligatoryColumnsRelationAdv(columns);
    Intermech.Navigator.DBObjects.Helper.AddAllColumns(columns);
    Intermech.Navigator.DBObjects.Helper.AddAllColumnsRelation(columns);
    return columns;
  }

  public static NodeColumnCollection NavigatorColumns(NodeColumnSortOrder captionSortOrder)
  {
    return Utils._navigatorColumns.LazyGet<NodeColumnSortOrder, NodeColumnCollection>(captionSortOrder, new System.Func<NodeColumnSortOrder, NodeColumnCollection>(Utils.CreateNavigatorColumns));
  }

  /// <summary>
  /// Создает коллекцию колонок для использования навигатором для показа
  /// версий объектов. В зависимости от переменной isList формируется сортировка по различным
  /// колонкам.
  /// </summary>
  /// <param name="sortOrder">Порядок сортировки</param>
  /// <param name="isList"></param>
  /// <returns></returns>
  public static NodeColumnCollection VersionColumns(NodeColumnSortOrder sortOrder, bool isList)
  {
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    NodeColumnCollection columnCollection = new NodeColumnCollection();
    sortOrder = !isList ? sortOrder : NodeColumnSortOrder.None;
    NodeColumn column = service.CreateColumn(Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.CAPTION, sortOrder, sortOrder != NodeColumnSortOrder.None ? 1 : -1);
    columnCollection.Add(column);
    columnCollection.Add(column, 250);
    columnCollection.Add(service.CreateColumn(Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_ID), 125);
    columnCollection.Add(service.CreateColumn(Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_VERSION_ID, sortOrder, sortOrder != NodeColumnSortOrder.None ? 1 : -1));
    columnCollection.Add(service.CreateColumn(Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJ_CREATE));
    return columnCollection;
  }

  /// <summary>Вернуть список колонок по умолчанию</summary>
  /// <returns>Список по умолчанию</returns>
  public static NodeColumnCollection DefaultColumnsObjects()
  {
    return new NodeColumnCollection()
    {
      {
        ((IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes))).CreateColumn(Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.CAPTION, NodeColumnSortOrder.Ascending, 0),
        250
      }
    };
  }

  /// <summary>
  /// Вернуть стандартный список поддерживаемых колонок объектов
  /// </summary>
  /// <returns>Стандартный список поддерживаемых колонок объектов</returns>
  public static NodeColumnCollection DefaultSupportedColumnsObjects(object sender = null)
  {
    return Utils.NavigatorColumns(NodeColumnSortOrder.Ascending);
  }

  public static NodeColumnCollection GetObjectsColumnsOnly()
  {
    NodeColumnCollection columns = new NodeColumnCollection();
    columns.Add(((IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes))).CreateColumn(Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.CAPTION, NodeColumnSortOrder.Ascending, 0), 250);
    Intermech.Navigator.DBObjects.Helper.AddObligatoryColumns(columns, false, true);
    Intermech.Navigator.DBObjects.Helper.AddObligatoryColumnsAdv(columns);
    Intermech.Navigator.DBObjects.Helper.AddAllColumns(columns);
    return columns;
  }

  /// <summary>
  /// Создает коллекцию колонок для использования навигатором для показа контекстного состава
  /// </summary>
  /// <returns></returns>
  public static NodeColumnCollection GetContextColumns(object sender) => Utils.ContextColumns();

  /// <summary>
  /// Создает коллекцию колонок для использования навигатором для показа контекстного состава
  /// </summary>
  /// <returns></returns>
  public static NodeColumnCollection ContextColumns()
  {
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    NodeColumnCollection columnCollection = new NodeColumnCollection();
    NodeColumn column1 = service.CreateColumn(Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_TYPE);
    column1.SortOrder = NodeColumnSortOrder.None;
    columnCollection.Add(column1, 220);
    NodeColumn column2 = service.CreateColumn(Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_ID);
    column2.SortOrder = NodeColumnSortOrder.None;
    columnCollection.Add(column2, 100);
    NodeColumn column3 = service.CreateColumn(Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.CAPTION);
    column3.SortOrder = NodeColumnSortOrder.None;
    columnCollection.Add(column3, 200);
    int columnID = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      columnID = sessionKeeper.Session.IdentHelper.GetAttributeID("cad00267-306c-11d8-b4e9-00304f19f545");
    NodeColumn column4 = service.CreateColumn(Consts.RelationColumnSchemeGuid, (object) columnID);
    column4.SortOrder = NodeColumnSortOrder.None;
    columnCollection.Add(column4, 100);
    return columnCollection;
  }

  /// <summary>
  /// Создает коллекцию колонок для использования навигатором для показа контекстного состава, включая статусы
  /// </summary>
  /// <param name="sender">Элемент управления</param>
  /// <returns>Коллекция колонок</returns>
  public static NodeColumnCollection GetContextStatusColumns(object sender)
  {
    return Utils.ContextStatusColumns();
  }

  /// <summary>
  /// Создает коллекцию колонок для использования навигатором для показа контекстного состава, включая статусы
  /// </summary>
  /// <returns>Коллекция колонок</returns>
  public static NodeColumnCollection ContextStatusColumns()
  {
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    NodeColumnCollection columnCollection = new NodeColumnCollection();
    NodeColumn column1 = service.CreateColumn(Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_TYPE);
    column1.SortOrder = NodeColumnSortOrder.None;
    columnCollection.Add(column1, 220);
    NodeColumn column2 = service.CreateColumn(Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_ID);
    column2.SortOrder = NodeColumnSortOrder.None;
    columnCollection.Add(column2, 100);
    NodeColumn column3 = service.CreateColumn(Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.CAPTION);
    column3.SortOrder = NodeColumnSortOrder.None;
    columnCollection.Add(column3, 200);
    int columnID = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      columnID = sessionKeeper.Session.IdentHelper.GetAttributeID("cad00267-306c-11d8-b4e9-00304f19f545");
    NodeColumn column4 = service.CreateColumn(Consts.RelationColumnSchemeGuid, (object) columnID);
    column4.SortOrder = NodeColumnSortOrder.None;
    columnCollection.Add(column4, 100);
    columnCollection.Add(Holder.ColumnSchemes.CreateColumn(Consts.NavigatorColumnSchemeGuid, (object) "F_STATUSES", NodeColumnSortOrder.None, -1), 100);
    return columnCollection;
  }

  /// <summary>
  /// Вернуть список поддерживаемых колонок для дерева "Навигатора"
  /// </summary>
  /// <param name="sender">Элемент управления, для которого требуется набор колонок</param>
  /// <returns>Набор поддерживаемых колонок</returns>
  public static NodeColumnCollection GetNavigatorColumns(object sender = null)
  {
    return Utils.NavigatorColumns(NodeColumnSortOrder.Ascending);
  }

  /// <summary>
  /// Вернуть список поддерживаемых колонок объектов для дерева "Навигатора"
  /// </summary>
  /// <param name="sender">Элемент управления, для которого требуется набор колонок</param>
  /// <returns>Набор поддерживаемых колонок объектов</returns>
  public static NodeColumnCollection GetObjectsColumns(object sender = null)
  {
    return Utils.DefaultSupportedColumnsObjects();
  }

  /// <summary>Открыть новое окно "Навигатора"</summary>
  /// <param name="rootDescriptor">Описание корневого узла</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  public static NavWindow OpenNewWindow(IDescriptor rootDescriptor, System.IServiceProvider viewServices)
  {
    return Utils.OpenNewWindow(rootDescriptor, viewServices, (GetSupportedColumnsEventHandler) null, (NodeIDPath) null);
  }

  /// <summary>
  /// Метод возвращает флаг, возможно ли открытие в новом окне Навигатора объекта заданного типа
  /// </summary>
  /// <param name="objectTypeID">Тип объектов</param>
  /// <returns></returns>
  public static bool EnableOpenInNewWindow(int objectTypeID)
  {
    int objectTypeId1 = MetaDataHelper.GetObjectTypeID("cad00156-306c-11d8-b4e9-00304f19f545");
    int objectTypeId2 = MetaDataHelper.GetObjectTypeID("cad00157-306c-11d8-b4e9-00304f19f545");
    return !MetaDataHelper.IsObjectTypeChildOf(objectTypeID, objectTypeId1) && !MetaDataHelper.IsObjectTypeChildOf(objectTypeID, objectTypeId2);
  }

  /// <summary>
  /// Событие генерируется перед отображением окна по выбору объектов
  /// </summary>
  /// <param name="sender">Отправитель (SelectionWindow)</param>
  /// <param name="e">Аргументы события</param>
  private static void DoSelectionWindowBeforeShow(object sender, EventArgs e)
  {
    if (!(sender is Intermech.Navigator.Controls.SelectionWindow selectionWindow))
      return;
    selectionWindow.cbDontShowAgain.Visible = Utils._showSelectContextWindow;
    selectionWindow.cbDontShowAgain.Enabled = true;
    selectionWindow.cbDontShowAgain.Checked = false;
  }

  /// <summary>
  /// Событие генерируется после закрытия окна по выбору объектов
  /// </summary>
  /// <param name="sender">Отправитель (SelectionWindow)</param>
  /// <param name="e">Аргументы события</param>
  private static void DoSelectionWindowAfterClose(object sender, EventArgs e)
  {
    if (!(sender is Intermech.Navigator.Controls.SelectionWindow selectionWindow))
      return;
    Utils._showSelectContextWindow = !selectionWindow.cbDontShowAgain.Checked;
    selectionWindow.cbDontShowAgain.Visible = false;
    selectionWindow.cbDontShowAgain.Enabled = false;
  }

  /// <summary>Открыть новое окно "Навигатора"</summary>
  /// <param name="rootDescriptor">Описание корневого узла</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="supportedColumns">Коллекция поддерживаемых колонок в дереве "Навигатора"</param>
  public static NavWindow OpenNewWindow(
    IDescriptor rootDescriptor,
    System.IServiceProvider viewServices,
    GetSupportedColumnsEventHandler supportedColumnsProvider)
  {
    return Utils.OpenNewWindow(rootDescriptor, viewServices, supportedColumnsProvider, (NodeIDPath) null);
  }

  /// <summary>Открыть новое окно "Навигатора"</summary>
  /// <param name="descriptor">Описание корневого узла</param>
  /// <param name="serviceProvider">Контейнер сервисов</param>
  /// <param name="supportedColumns">Коллекция поддерживаемых колонок в дереве "Навигатора"</param>
  /// <param name="path">Путь, по которому надо раскрутить окно (rootDescriptor должен совпадать по ссылке с дескриптором внутри пути - один и тот же объект), или null</param>
  /// <returns>Новое окно Навигатора</returns>
  public static NavWindow OpenNewWindow(
    IDescriptor descriptor,
    System.IServiceProvider serviceProvider,
    GetSupportedColumnsEventHandler supportedColumnsProvider,
    NodeIDPath path)
  {
    if (descriptor == null)
      throw new ArgumentNullException(nameof (descriptor));
    if (path != null && !object.Equals((object) descriptor, (object) path.RootDescriptor))
      throw new ArgumentException();
    long objectID = 0;
    int num = -1;
    List<long> contextObjectIDs = new List<long>();
    if (descriptor is Intermech.Navigator.DBObjects.Descriptor dbObjectDescriptor)
    {
      if (dbObjectDescriptor.InvalidDescriptor)
      {
        Utils.NotifyWrongDbObjectDescriptor(dbObjectDescriptor);
        return (NavWindow) null;
      }
      objectID = dbObjectDescriptor.ObjectID;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        num = sessionKeeper.Session.GetObjectInfo(objectID).ObjectTypeID;
        if (!Utils.EnableOpenInNewWindow(num))
          return (NavWindow) null;
        IDBEditingContextsService customService = sessionKeeper.Session.GetCustomService(typeof (IDBEditingContextsService)) as IDBEditingContextsService;
        contextObjectIDs = customService.FindObjectsContexts((object) sessionKeeper.Session.SessionGUID, new List<long>((IEnumerable<long>) new long[1]
        {
          objectID
        }), false);
        contextObjectIDs = customService.GetAllLinkedContexts((object) sessionKeeper.Session.SessionGUID, contextObjectIDs);
      }
    }
    DockManager dockManager = Holder.DockManager;
    if (dockManager != null && dockManager.DocumentContainer != null && dbObjectDescriptor != null)
    {
      int objectTypeId = MetaDataHelper.GetObjectTypeID("cad0011b-306c-11d8-b4e9-00304f19f545");
      if (MetaDataHelper.IsObjectTypeChildOf(num, objectTypeId))
      {
        foreach (DockControl document in dockManager.DocumentContainer.Documents)
        {
          if (document is NavWindow navWindow && navWindow.RootDescriptor is Intermech.Navigator.DBObjects.Descriptor rootDescriptor && Math.Abs(rootDescriptor.ObjectID) == Math.Abs(objectID))
          {
            navWindow.Activate();
            return navWindow;
          }
        }
      }
    }
    ICurrentUserAndRole service1 = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    CurrentEditingContext currentEditingContext = CurrentEditingContext.Dummy;
    if (contextObjectIDs.Count == 1 && (contextObjectIDs.IndexOf(Math.Abs(service1.CachedEditingContextID)) == 0 || contextObjectIDs.IndexOf(-Math.Abs(service1.CachedEditingContextID)) == 0))
      contextObjectIDs.Clear();
    bool flag1 = false;
    if ((ServicesManager.GetService(typeof (IConfigurationOptionRepository)) as IConfigurationOptionRepository).Find(ConfigurationOptionKeys.Versions_AutoSelectContext) is bool flag2 && flag2)
    {
      if (contextObjectIDs.Count > 1)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(-1);
          objectCollection.LocalTypesMode = true;
          DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
          dbRecordSetParams.Columns = new object[1]
          {
            (object) ObligatoryObjectAttributes.F_OBJECT_ID
          };
          ref DBRecordSetParams local = ref dbRecordSetParams;
          ConditionStructure[] conditionStructureArray = new ConditionStructure[2];
          ConditionStructure conditionStructure = new ConditionStructure();
          conditionStructure.Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_ID;
          conditionStructure.RelationalOperator = RelationalOperators.In;
          conditionStructure.Value = (object) contextObjectIDs.ToArray();
          conditionStructure.SQL = string.Empty;
          conditionStructure.LogicalOperator = LogicalOperators.AND;
          conditionStructureArray[0] = conditionStructure;
          conditionStructure = new ConditionStructure();
          conditionStructure.Attribute = (object) ObligatoryObjectAttributes.F_LEVEL_ID;
          conditionStructure.RelationalOperator = RelationalOperators.NotIn;
          conditionStructure.Value = (object) new int[2]
          {
            MetaDataHelper.GetLCLevelID(new Guid("cad009de-306c-11d8-b4e9-00304f19f545")),
            MetaDataHelper.GetLCLevelID(new Guid("cad0000e-306c-11d8-b4e9-00304f19f545"))
          };
          conditionStructure.SQL = string.Empty;
          conditionStructureArray[1] = conditionStructure;
          local.Conditions = conditionStructureArray;
          dbRecordSetParams.RecordCount = -1;
          DBRecordSetParams paramSet = dbRecordSetParams;
          DataTable dataTable = objectCollection.Select(paramSet);
          contextObjectIDs.Clear();
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
            contextObjectIDs.Add(int64Value);
          }
        }
      }
      if (contextObjectIDs.Count == 1)
      {
        if (service1.CachedEditingContextID != contextObjectIDs[0])
        {
          CanSetContextModeCode setContextModeCode = service1.CachedContextMode == EditingContextMode.AutoUpdate ? service1.CanSetContextAutoUpdateMode(contextObjectIDs[0]) : CanSetContextModeCode.None;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            if (sessionKeeper.Session.GetObjectActualCopy(contextObjectIDs[0], false) is IDBEditingContextsObject objectActualCopy)
            {
              currentEditingContext = new CurrentEditingContext(contextObjectIDs[0], objectActualCopy.LinkedContextNumber, setContextModeCode == CanSetContextModeCode.CanSetAutoUpdate ? service1.CachedContextMode : EditingContextMode.Default);
              flag1 = true;
            }
          }
        }
        else
          flag1 = true;
      }
    }
    if (contextObjectIDs.Count > 0 && Utils._showSelectContextWindow)
    {
      if (!flag1)
      {
        try
        {
          SelectionWindow.OnSelectionWindowBeforeShow += new SelectionWindowBeforeShow(Utils.DoSelectionWindowBeforeShow);
          SelectionWindow.OnSelectionWindowAfterClose += new SelectionWindowAfterClose(Utils.DoSelectionWindowAfterClose);
          Dictionary<int, List<long>> objectIDs = new Dictionary<int, List<long>>();
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            for (int index = 0; index < contextObjectIDs.Count; ++index)
            {
              QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(contextObjectIDs[index]);
              if (!objectInfo.Empty)
              {
                if (!objectIDs.ContainsKey(objectInfo.ObjectTypeID))
                  objectIDs.Add(objectInfo.ObjectTypeID, new List<long>());
                objectIDs[objectInfo.ObjectTypeID].Add(-Math.Abs(objectInfo.ObjectID));
                objectIDs[objectInfo.ObjectTypeID].Add(Math.Abs(objectInfo.ObjectID));
              }
            }
          }
          long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Client.Core_1537"), LocalizationHolder.rm.GetString("Client.Core_1538"), (IDescriptor) new DictDescriptor(Consts.NotificationsAndContextsCategoryID, 0, LocalizationHolder.rm.GetString("Client.Core_614"), objectIDs)
          {
            ExpandNodes = false
          }, SelectionOptions.Default | SelectionOptions.HideTree | SelectionOptions.DisableSelectFromTree | SelectionOptions.DisableMultiselect);
          if (numArray != null)
          {
            if (numArray.Length != 0)
            {
              if (service1.CachedEditingContextID != numArray[0])
              {
                CanSetContextModeCode setContextModeCode = service1.CachedContextMode == EditingContextMode.AutoUpdate ? service1.CanSetContextAutoUpdateMode(numArray[0]) : CanSetContextModeCode.None;
                using (SessionKeeper sessionKeeper = new SessionKeeper())
                {
                  if (sessionKeeper.Session.GetObjectActualCopy(numArray[0], false) is IDBEditingContextsObject objectActualCopy)
                    currentEditingContext = new CurrentEditingContext(numArray[0], objectActualCopy.LinkedContextNumber, setContextModeCode == CanSetContextModeCode.CanSetAutoUpdate ? service1.CachedContextMode : EditingContextMode.Default);
                }
              }
            }
          }
        }
        finally
        {
          SelectionWindow.OnSelectionWindowAfterClose -= new SelectionWindowAfterClose(Utils.DoSelectionWindowAfterClose);
          SelectionWindow.OnSelectionWindowBeforeShow -= new SelectionWindowBeforeShow(Utils.DoSelectionWindowBeforeShow);
        }
      }
    }
    NavWindow navigatorWindow = new NavWindow();
    bool flag3 = false;
    if (ServicesManager.GetService(typeof (INavigatorTreeCollapseService)) is INavigatorTreeCollapseService service2)
      flag3 = service2.EnableTreeCollapse(descriptor, serviceProvider);
    navigatorWindow.ViewsBridge.UseDelay = false;
    INotificationService notificationService = ServiceLocator.Get<INotificationService>();
    try
    {
      INodeID recordNodeId = descriptor.GetRecordNodeID();
      string filtrationOwnerId = navigatorWindow.FiltrationOwnerID;
      IFiltrationService filtrationService = navigatorWindow.FiltrationService;
      if (filtrationService != null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          IVersionRulesCacheService customService = session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
          FiltrationSettings filtrationSettings = customService.GetFiltrationSettings((object) session.SessionGUID, filtrationService.FiltrationServiceOwnerID, true);
          filtrationSettings.OwnerID = filtrationOwnerId;
          if (!currentEditingContext.IsDummy)
            filtrationSettings.EditingContext = currentEditingContext;
          if (!service1.IsContextToolbarVisible && filtrationSettings.EditingContext != null)
            filtrationSettings.EditingContext = filtrationSettings.EditingContext.WithContextMode(EditingContextMode.Default);
          customService.SetFiltrationSettings((object) session.SessionGUID, filtrationOwnerId, filtrationSettings);
          if (filtrationService is FiltrationPanel filtrationPanel)
          {
            filtrationPanel.FiltrationsCache[filtrationOwnerId] = filtrationSettings;
            filtrationPanel.FiltrationReload(filtrationOwnerId);
          }
        }
      }
      navigatorWindow.TreeView.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(Utils.GetNavigatorColumns);
      bool flag4 = false;
      if (ServicesManager.GetService(typeof (IEnableTreeMultiSelectService)) is IEnableTreeMultiSelectService service3)
        flag4 = service3.EnableTreeMultiSelect(descriptor, serviceProvider);
      if (flag4)
        navigatorWindow.TreeView.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.TwoState;
      bool flag5 = true;
      if (ServicesManager.GetService(typeof (IEnableTreeColumnsSortingService)) is IEnableTreeColumnsSortingService service4)
        flag5 = service4.EnableTreeColumnsSorting(descriptor, serviceProvider);
      navigatorWindow.TreeView.DisableColumnsSorting = !flag5;
      navigatorWindow.btClearSorting.Enabled = flag5;
      if (!flag5)
        navigatorWindow.btClearSorting.Checked = true;
      if (!flag5)
        navigatorWindow.btClearSorting.Checked = true;
      if (supportedColumnsProvider != null)
        navigatorWindow.TreeView.OnGetSupportedColumnsEventHandler += supportedColumnsProvider;
      else
        navigatorWindow.TreeView.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(Utils.GetNavigatorColumns);
      if (flag3 && !navigatorWindow.spTreeView.IsCollapsed)
        navigatorWindow.spTreeView.ToggleState();
      if (service1.CachedEditingContextSource == EditingContextSource.SessionContext && !currentEditingContext.IsDummy && service1.CachedEditingContextID != currentEditingContext.ContextID)
      {
        service1.CachedContextMode = service1.IsContextToolbarVisible ? currentEditingContext.ContextMode : EditingContextMode.Default;
        service1.EditingContextID = currentEditingContext.ContextID;
      }
      if (path == null)
        navigatorWindow.RootDescriptor = descriptor;
      else
        navigatorWindow.RootPath = path;
      notificationService.FireEvent((object) null, (NotificationEventArgs) new NavigatorWindowOpeningEventArgs((NavWindowBase) navigatorWindow, descriptor, path, serviceProvider));
      navigatorWindow.Show(Holder.DockManager, CoreConfigurationOptions.UI_OpenNearMode ? DockOpenOrder.NearRight : DockOpenOrder.DefaultOpenOrder);
      navigatorWindow.Activate();
      if (recordNodeId is NodeID nodeId)
        RecentObjectsNode.MRUObjects.Add(nodeId.ObjectID, ObjectAction.OpenInNewWindow, DateTime.UtcNow);
    }
    finally
    {
      navigatorWindow.ViewsBridge.UseDelay = true;
    }
    notificationService.FireEvent((object) null, (NotificationEventArgs) new NavigatorWindowOpenedEventArgs(navigatorWindow, serviceProvider));
    return navigatorWindow;
  }

  /// <summary>Возможно ли создание объекта не включенного куда либо</summary>
  /// <param name="objectType"></param>
  /// <returns></returns>
  public static bool CreateFreeObject(int objectType)
  {
    IMSObjectType objectType1 = MetaDataHelper.GetObjectType(objectType);
    if (objectType1 == null || objectType1.IsDisableManualCreate)
      return false;
    return MetaDataHelper.IsObjectTypeChildOf(objectType1.Guid, new Guid("cad00002-306c-11d8-b4e9-00304f19f545")) || !MetaDataHelper.GetObjectTypeParentApplicabilities(objectType).Any<IMSApplicability>((System.Func<IMSApplicability, bool>) (item => item.ApplicabilityMode == ApplicabilityModes.AnyRequired || item.ApplicabilityMode == ApplicabilityModes.Required));
  }

  internal static void NotifyWrongDbObjectDescriptor(Intermech.Navigator.DBObjects.Descriptor dbObjectDescriptor)
  {
    int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("NavigatorUtils_1"), (object) dbObjectDescriptor.InvalidObjID), LocalizationHolder.rm.GetString("Client.Core_132"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsRemoved", dbObjectDescriptor.InvalidObjID);
    service.FireEvent((object) null, (NotificationEventArgs) e);
  }
}
