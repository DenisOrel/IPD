
// Type: Intermech.Navigator.DBObjects.ObjectsItems
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.Selections;
using Intermech.Navigator.Selections.Implementation;
using System;
using System.Diagnostics;


namespace Intermech.Navigator.DBObjects;

/// <summary>Класс для работы с элементами пространства навигации</summary>
public abstract class ObjectsItems : INodeItems, IContextAware
{
  /// <summary>"ObjTypeId"</summary>
  private const string PropObjTypeID = "ObjTypeId";
  /// <summary>"ObjId"</summary>
  private const string PropObjID = "ObjId";
  /// <summary>"PrjLinkId"</summary>
  private const string PropPrjLinkID = "PrjLinkId";
  /// <summary>Кэш названий шагов жизненных циклов</summary>
  protected static IObjectLCStepsCache lcStepCache;
  /// <summary>Кэш названий уровней продвижения</summary>
  protected static IObjectLevelIDsCache levelIDsCache;
  /// <summary>Контейнер сервисов</summary>
  private AdvancedServiceContainer _services;
  private INodesFactory _nodesFactory;
  /// <summary>
  /// Флажок, позволяющий нодам обрабатывать управляемые уведомления
  /// </summary>
  public bool AcceptManagedEvents = true;

  /// <summary>Фабрика нод элементов. Если null, то используется стандартная (Holder.Factory)</summary>
  public INodesFactory NodesFactory
  {
    [DebuggerStepThrough] get => this._nodesFactory;
    set
    {
      this._nodesFactory = value;
      if (this._services == null)
        this._services = new AdvancedServiceContainer();
      this._services.AddService(typeof (INodesFactory), (object) value);
    }
  }

  /// <summary>
  /// Возвращает набор флагов атрибутов навигатора для элемента,
  /// представляющего объект базы данных.
  /// </summary>
  /// <param name="nodeID">Унифицированный идентификатор объекта базы данных</param>
  /// <returns>Набор флагов атрибутов</returns>
  public virtual ContentAttributes GetAttributesOf(INodeID nodeID)
  {
    return ContentAttributes.HasChildren | ContentAttributes.Slow | ContentAttributes.Large;
  }

  /// <summary>
  /// Создает элемент пространства навигации, представляющий указанный с
  /// помощью унифицированного идентификатора объект базы данных, и возвращает
  /// ссылку на основной интерфейс элемента.
  /// </summary>
  /// <param name="nodeID">Идентификатор, описывающий объект базы данных</param>
  /// <returns>Ссылка на основной интерфейс элемента</returns>
  public virtual INode GetChild(INodeID nodeID)
  {
    NodeID nodeID1 = nodeID as NodeID;
    SelectionNodeID nodeID2 = nodeID as SelectionNodeID;
    IFactory service = (IFactory) ServicesManager.GetService(typeof (IFactory));
    INodesFactory nodesFactory = (this._services == null ? (INodesFactorySupported) null : (INodesFactorySupported) this._services.GetService(typeof (INodesFactorySupported)))?.GetNodesFactory((IServiceProvider) this._services, (INodeID) nodeID1) ?? this.NodesFactory ?? (this._services == null ? (INodesFactory) null : (INodesFactory) this._services.GetService(typeof (INodesFactory))) ?? (INodesFactory) service;
    if (nodeID2 != null)
      return nodesFactory.GetNode((INodeID) nodeID2, (object) nodeID2.TypeID, (object) nodeID2.ObjectID, (object) nodeID2.HandSelection, (object) nodeID2.SampleFunction, (object) nodeID2.SearchInLocalTypes);
    if (nodeID1 == null)
      return (INode) null;
    return nodesFactory.GetNode(nodeID, (object) nodeID1.TypeID, (object) nodeID1.ObjectID);
  }

  /// <summary>
  /// Возвращает адрес объекта базы данных, который будет выводиться в
  /// адресной строке навигатора.
  /// </summary>
  /// <param name="nodeID">Идентификатор, описывающий объект базы данных</param>
  /// <returns>Адрес объекта базы данны</returns>
  public string GetAddress(INodeID nodeID)
  {
    return !(nodeID is NodeID nodeId) ? Helper.GetAddress(nodeID) : nodeId.Caption;
  }

  /// <summary>
  /// Восстанавливает идентификатор объекта базы данных по указанному
  /// имени из адресной строки. Если найти адресуемый объект не удается,
  /// то метод вернет null.
  /// </summary>
  /// <param name="address">Адрес объекта базы данных</param>
  /// <returns>Унифицированный идентификатор объекта базы данных</returns>
  public abstract INodeID ParseAddress(string address);

  /// <summary>
  /// Возвращает строковое представление идентификатора, описывающего объект
  /// базы данных.
  /// </summary>
  /// <param name="nodeID">Унифицированный идентификатор</param>
  /// <returns>Строковое представление идентификатора</returns>
  public PersistentState Serialize(INodeID nodeID)
  {
    NodeID nodeId = (NodeID) nodeID;
    PersistentState persistentState = new PersistentState();
    persistentState.AddValue("ObjTypeId", (object) nodeId.TypeID);
    persistentState.AddValue("ObjId", (object) nodeId.ObjectID);
    if (nodeId.PrjLinkID != -1L)
      persistentState.AddValue("PrjLinkId", (object) nodeId.PrjLinkID);
    return persistentState;
  }

  /// <summary>
  /// Восстанавливает унифицированный идентификатор объекта базы данных из
  /// его строкового представления.
  /// </summary>
  /// <param name="persistentNodeID">Строковое представление идентификатора</param>
  /// <returns>Унифицированный идентификатор</returns>
  public INodeID Deserialize(PersistentState persistentNodeID)
  {
    int objTypeId = (int) persistentNodeID.GetValue("ObjTypeId");
    long num1 = (long) persistentNodeID.GetValue("ObjId");
    long num2 = persistentNodeID.Contains("PrjLinkId") ? (long) persistentNodeID.GetValue("PrjLinkId") : -1L;
    long objId = num1;
    long prjLinkId = num2;
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    Guid empty3 = Guid.Empty;
    return (INodeID) new NodeID(new CreateObjectNodeParams(objTypeId, objId, 0L, 0L, prjLinkId, -1, empty1, -1, 0L, 0L, ObjectFiltrationState.fsNotRequired, 0L, 0L, empty2, 0L, empty3, 0L));
  }

  /// <summary>
  /// Возвращает данные указанного формата для объекта базы данных с указанным
  /// идентификатором.
  /// </summary>
  /// <param name="nodeID">Унифицированный идентификатор объекта базы данных</param>
  /// <param name="dataFormat">Формат данных</param>
  /// <returns>Объект, представляющий данные указанного формата</returns>
  public virtual object GetData(INodeID nodeID, Type dataFormat)
  {
    ObjectsItems.lcStepCache = ObjectsItems.lcStepCache == null ? CacheManager.Cache("ObjectLCStepsCache") as IObjectLCStepsCache : ObjectsItems.lcStepCache;
    ObjectsItems.levelIDsCache = ObjectsItems.levelIDsCache == null ? CacheManager.Cache("ObjectLevelIDsCache") as IObjectLevelIDsCache : ObjectsItems.levelIDsCache;
    NodeID nodeId = nodeID as NodeID;
    if (dataFormat == typeof (INodeID))
      return (object) nodeID;
    if (nodeId == null)
      return (object) null;
    if (nodeID is SelectionNodeID selectionNodeId)
    {
      if (dataFormat == typeof (INavigatorIconInformation))
        return (object) new NavigatorIconInformation((object) new DBSelectionID(selectionNodeId.ObjectID, selectionNodeId.ID, selectionNodeId.HandSelection, selectionNodeId.SelectionType));
      if (dataFormat == typeof (IDBSelectionID) || dataFormat == typeof (IDBObjectTypeSelectionID))
        return (object) new DBObjectTypeSelectionID(selectionNodeId.ObjectID, selectionNodeId.ID, selectionNodeId.HandSelection, selectionNodeId.SelectionType, selectionNodeId.BindedObjectTypeID);
    }
    if (dataFormat == typeof (IBinding))
      return (object) FreeBinding.Value;
    if (dataFormat == typeof (INode))
      return (object) this.GetChild(nodeID);
    if (dataFormat == typeof (IDescriptor))
      return (object) new Descriptor((nodeID as NodeID).ObjectID);
    if (dataFormat == typeof (ICanOpenInNewWindow))
      return (object) new CanOpenInNewWindow();
    if (dataFormat == typeof (IDBTypedObjectID))
      return (object) new DBTypedObjectID(nodeID.TypeID, (nodeID as NodeID).ObjectID, (nodeID as NodeID).ID, (nodeID as NodeID).Caption, (nodeID as NodeID).Owner, (nodeID as NodeID).Version, (nodeID as NodeID).BaseVersion, (nodeID as NodeID).SiteID, (nodeID as NodeID).ModificationID);
    if (dataFormat == typeof (IDBObjectID))
      return (object) new DBObjectID((nodeID as NodeID).ObjectID, (nodeID as NodeID).ID, (nodeID as NodeID).Caption, (nodeID as NodeID).Owner);
    if (dataFormat == typeof (IDBRelationID))
      return (object) new DBRelationID(((NodeID) nodeID).PrjLinkID, ((NodeID) nodeID).ObjectID, ((NodeID) nodeID).RelationTypeID, ((NodeID) nodeID).Sorting, ((NodeID) nodeID).RelGuid, ((NodeID) nodeID).ProjID);
    if (dataFormat == typeof (IDBObjectTypeID))
      return (object) new DBObjectTypeID(nodeID.TypeID);
    if (dataFormat == typeof (IDBCheckedOutByID))
      return (object) new DBCheckedOutByID(nodeId.ObjectID, nodeId.CheckedOutBy, nodeId.Owner);
    if (dataFormat == typeof (IDBLCStepID))
      return (object) new DBLCStepID(nodeId.LCStepID, ObjectsItems.lcStepCache.GetName(nodeId.LCStepID));
    return dataFormat == typeof (IDBObjectFiltrationState) ? (object) new DBObjectFiltrationState(nodeId.State) : (object) null;
  }

  public virtual object[] GetData(NodeIDCollection nodeIDs, Type dataFormat)
  {
    object[] data = new object[nodeIDs.Count];
    for (int index = 0; index < data.Length; ++index)
      data[index] = this.GetData(nodeIDs[index], dataFormat);
    return data;
  }

  public virtual IUpdateAnalyser GetAnalyser(
    NodeViewCapabilities capabilities,
    object sender,
    NotificationEventArgs e)
  {
    if (e is DBObjectsEventArgs objectsEventArgs)
    {
      switch (e.EventName)
      {
        case "ObjectsCreated":
          return !capabilities.CanAppend ? (IUpdateAnalyser) null : (IUpdateAnalyser) new ObjectsCreatedAnalyser(objectsEventArgs.ObjectIDs);
        case "ObjectsChanged":
          return (IUpdateAnalyser) new ObjectsChangedAnalyser(objectsEventArgs.ObjectIDs);
        case "ObjectsRemoved":
          return (IUpdateAnalyser) new ObjectsRemovedAnalyser(objectsEventArgs.ObjectIDs);
        case "ObjectsCheckedIn":
        case "ObjectsChangesCancelled":
          return (IUpdateAnalyser) new ObjectsCheckedInAnalyser(objectsEventArgs.ObjectIDs);
      }
    }
    if (e is DBObjectsCheckOutEventArgs checkOutEventArgs && e.EventName == "ObjectsCheckedOut")
      return (IUpdateAnalyser) new ObjectsCheckedOutAnalyser(checkOutEventArgs.ObjectIDs, checkOutEventArgs.NewObjectIDs);
    DBObjectsManagedEventArgs managedEventArgs = e as DBObjectsManagedEventArgs;
    if (objectsEventArgs == null || !(e.EventName == "ManagedObjectsCreated"))
      return (IUpdateAnalyser) null;
    return !capabilities.CanAppend && (!this.AcceptManagedEvents || !managedEventArgs.AcceptEvent) ? (IUpdateAnalyser) null : (IUpdateAnalyser) new ObjectsCreatedAnalyser(objectsEventArgs.ObjectIDs);
  }

  public virtual object GetService(Type service) => this._services.GetService(service);

  /// <summary>Контейнер сервисов</summary>
  public virtual IServiceProvider Services
  {
    [DebuggerStepThrough] get => (IServiceProvider) this._services;
    set
    {
      if (this._services == null)
      {
        this._services = new AdvancedServiceContainer((IServiceProvider) null, value);
      }
      else
      {
        if (this._services.AdvancedProvider == value)
          return;
        this._services.AdvancedProvider = value;
      }
    }
  }
}
