
// Type: Intermech.Navigator.DBObjects.AdvRelationsDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Дескриптор для состава допустимых замен.
/// ВНИМАНИЕ! Дескриптор не предназначен для сохранения своего состояния в потоке!
/// </summary>
public class AdvRelationsDescriptor : HiveDescriptor
{
  /// <summary>Идентификатор типа связи "Проектная"</summary>
  internal static int ProjectRelationTypeID = -1;
  /// <summary>
  /// Уникальный ключ настроек фильтрации состава.
  /// Если фильтрация состава не требуется, можно
  /// указать константу Intermech.SystemGUIDs.filtrationAllVersions.
  /// </summary>
  private string _filtrationOwnerID;
  /// <summary>Контексты, в рамках которых будет получен состав</summary>
  private List<long> _contexts;
  /// <summary>Идентификатор типа корневого объекта</summary>
  private int _objType;
  /// <summary>Идентификатор версии корневого объекта</summary>
  private long _objID;
  /// <summary>Идентификатор корневого объекта</summary>
  private long _ID;
  /// <summary>Идентификатор типа связи по умолчанию</summary>
  private int _relationTypeID;
  /// <summary>Кем объект взят на изменение</summary>
  private long _checkedOutBy;
  /// <summary>Владелец объекта</summary>
  private long _owner;
  /// <summary>Значение атрибута "Сортировка"</summary>
  private long _sorting;
  /// <summary>шаг ЖЦ</summary>
  private int _lcStepIP;
  /// <summary>
  /// Список дополнительных идентификаторов атрибутов, которые будут загружаться в узел независимо от видимых колонок
  /// </summary>
  private List<int> _attributes = new List<int>();
  /// <summary>Список значений дополнительных атрибутов</summary>
  private object[] _values = new object[0];
  /// <summary>Номер версии</summary>
  private long _version;
  /// <summary>Признак базовой версии</summary>
  private long _baseVersion;
  /// <summary>Узлы информационной системы</summary>
  private string _siteID;

  /// <summary>
  /// Установить/откорректировать значения статических полей класса
  /// </summary>
  internal static void CorrectStatics()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      AdvRelationsDescriptor.ProjectRelationTypeID = AdvRelationsDescriptor.ProjectRelationTypeID == -1 ? sessionKeeper.Session.IdentHelper.SPRelationTypeID : AdvRelationsDescriptor.ProjectRelationTypeID;
  }

  public override bool Equals(object obj)
  {
    if (!(obj is AdvRelationsDescriptor relationsDescriptor))
      return base.Equals(obj);
    return this._objID == relationsDescriptor._objID && this._filtrationOwnerID == relationsDescriptor._filtrationOwnerID && this._relationTypeID == relationsDescriptor._relationTypeID;
  }

  /// <summary>
  /// Уникальный ключ настроек фильтрации состава.
  /// Если фильтрация состава не требуется, можно
  /// указать константу Intermech.SystemGUIDs.filtrationAllVersions.
  /// </summary>
  public string FiltrationOwnerID
  {
    [DebuggerStepThrough] get => this._filtrationOwnerID;
    set
    {
      this._filtrationOwnerID = value != string.Empty ? value : "cad001e2-306c-11d8-b4e9-00304f19f545";
    }
  }

  /// <summary>Контексты, в рамках которых будет получен состав</summary>
  public List<long> Contexts
  {
    [DebuggerStepThrough] get => this._contexts;
    set
    {
      if (value == null || value.Count <= 0)
        return;
      this._contexts = new List<long>(value.Count);
      for (int index = 0; index < value.Count; ++index)
        this._contexts.Add(value[index]);
    }
  }

  /// <summary>Идентификатор типа корневого объекта</summary>
  public int ObjType
  {
    [DebuggerStepThrough] get => this._objType;
  }

  /// <summary>Идентификатор версии корневого объекта</summary>
  public long ObjID
  {
    [DebuggerStepThrough] get => this._objID;
    set
    {
      if (this._objID == value)
        return;
      this._objID = value;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this._objID);
        this._ID = dbObject.ID;
        this._objType = dbObject.ObjectType;
        this._checkedOutBy = dbObject.CheckoutBy;
        this._caption = dbObject.Caption;
        for (int index = 0; index < this._attributes.Count; ++index)
        {
          IDBAttribute byId = dbObject.Attributes.FindByID(this._attributes[index]);
          this[this._attributes[index]] = byId?.Value;
        }
      }
    }
  }

  /// <summary>Идентификатор корневого объекта</summary>
  public long ID
  {
    [DebuggerStepThrough] get => this._ID;
  }

  /// <summary>
  /// Идентификатор типа по умолчанию связи, по которой будет получен состав
  /// </summary>
  public int RelationTypeID
  {
    [DebuggerStepThrough] get => this._relationTypeID;
    set => this._relationTypeID = value >= 0 ? value : AdvRelationsDescriptor.ProjectRelationTypeID;
  }

  /// <summary>Кем объект взят на изменение</summary>
  public long CheckedOutBy
  {
    [DebuggerStepThrough] get => this._checkedOutBy;
    set => this._checkedOutBy = value;
  }

  /// <summary>
  /// Список дополнительных идентификаторов атрибутов, которые будут загружаться в узел независимо от видимых колонок
  /// </summary>
  public List<int> Attributes
  {
    [DebuggerStepThrough] get => this._attributes;
    set
    {
      this._attributes = value != null ? value : new List<int>();
      if (this._values != null && this._values.Length == this._attributes.Count)
        return;
      this._values = new object[this._attributes.Count];
    }
  }

  /// <summary>Владелец объекта</summary>
  public long Owner
  {
    [DebuggerStepThrough] get => this._owner;
    set => this._owner = value;
  }

  /// <summary>Значение атрибута "Сортировка"</summary>
  public long Sorting
  {
    [DebuggerStepThrough] get => this._sorting;
    set => this._sorting = value;
  }

  /// <summary>Значение атрибута "Шаг ЖЦ"</summary>
  public int LCStepID
  {
    [DebuggerStepThrough] get => this._lcStepIP;
    set => this._lcStepIP = value;
  }

  /// <summary>Список значений дополнительных атрибутов</summary>
  public object[] Values
  {
    [DebuggerStepThrough] get => this._values;
    set
    {
      this._values = value == null || value.Length != this._attributes.Count ? new object[this._attributes.Count] : value;
    }
  }

  /// <summary>Значение указанного атрибута</summary>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <returns>null, если значение атрибута не найдено</returns>
  public object this[int attributeID]
  {
    get
    {
      return !this._attributes.Contains(attributeID) ? (object) null : this._values[this._attributes.IndexOf(attributeID)];
    }
    set
    {
      if (!this._attributes.Contains(attributeID))
        return;
      this._values[this._attributes.IndexOf(attributeID)] = value;
    }
  }

  /// <summary>Номер версии объекта</summary>
  public long Version
  {
    [DebuggerStepThrough] get => this._version;
    set => this._version = value;
  }

  /// <summary>Признак базовой версии</summary>
  public long BaseVersion
  {
    [DebuggerStepThrough] get => this._baseVersion;
    set => this._baseVersion = value;
  }

  /// <summary>Узлы информационной системы</summary>
  public string SiteID
  {
    [DebuggerStepThrough] get => this._siteID;
    set => this._siteID = value;
  }

  /// <summary>
  /// Создает дескриптор элемента навигации состава допустимых замен.
  /// </summary>
  /// <param name="categoryID">Категория</param>
  /// <param name="typeID">Тип</param>
  /// <param name="objID">Идентификатор версии корневого объекта.</param>
  /// <param name="objType">Идентификатор типа корневого объекта</param>
  /// <param name="relationTypeID">Тип связи по умолчанию, по которому надо получать состав</param>
  /// <param name="caption">Заголовок</param>
  /// <param name="filtrationOwnerID">Уникальный ключ настроек фильтрации состава</param>
  /// <param name="contexts">Список контекстов, в рамках которых будет считываться состав</param>
  /// <param name="checkedOutBy">Кем объект взят на изменение</param>
  /// <param name="owner">Владелец объекта</param>
  /// <param name="sorting">Значение атрибута "Сортировка"</param>
  /// <param name="lcStepID">Шаг ЖЦ</param>
  /// <param name="attributes">Список дополнительных идентификаторов атрибутов, которые будут загружаться в узел независимо от видимых колонок</param>
  /// <param name="version">Версия объекта</param>
  /// <param name="baseVersion">Признак базовой версии</param>
  public AdvRelationsDescriptor(
    int categoryID,
    int typeID,
    string filtrationOwnerID,
    List<long> contexts,
    long objID,
    int objType,
    int relationTypeID,
    string caption,
    long checkedOutBy,
    long owner,
    long sorting,
    int lcStepID,
    List<int> attributes,
    long version,
    long baseVersion)
    : base(categoryID, typeID, caption)
  {
    AdvRelationsDescriptor.CorrectStatics();
    this.FiltrationOwnerID = filtrationOwnerID;
    this.Contexts = contexts;
    this.CheckedOutBy = checkedOutBy;
    this.Attributes = attributes;
    this.ObjID = objID;
    this.RelationTypeID = relationTypeID;
    this.Owner = owner;
    this.Sorting = sorting;
    this.LCStepID = lcStepID;
    this.Version = version;
    this.BaseVersion = baseVersion;
  }

  /// <summary>Cериализовать описание узла</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <returns>Сериализованное представление узла</returns>
  public override PersistentState Serialize(INodeID nodeID) => (PersistentState) null;

  /// <summary>Десериализовать описание узла</summary>
  /// <param name="persistNodeID">Сериализованное представление узла</param>
  /// <returns>Описание узла</returns>
  public override INodeID Deserialize(PersistentState persistNodeID) => (INodeID) null;

  /// <summary>
  /// Вернуть описание корневого узла на основании данных дескриптора
  /// </summary>
  /// <returns>Описание коревого узла на основании данных дескриптора</returns>
  public override INodeID GetRecordNodeID()
  {
    return (INodeID) new AdvRelationsNodeID((CreateObjectNodeParams) new AdvCreateObjectNodeParams(this.ObjType, this.ObjID, this._ID, this._checkedOutBy, -1L, this.LCStepID, this.Caption, this.RelationTypeID, this.Owner, this.Sorting, ObjectFiltrationState.fsNotRequired, this.Version, this.BaseVersion, this.SiteID, this.FiltrationOwnerID, this.Contexts, -1, -1L, Guid.Empty, 0L, this.Attributes, this.Values));
  }

  /// <summary>Вернуть дочерний узел по его описанию</summary>
  /// <param name="nodeID">Описание дочернего узла</param>
  /// <returns>Новый дочерний узел по его описанию</returns>
  public override INode GetChild(INodeID nodeID)
  {
    return !(nodeID is AdvRelationsNodeID advRelationsNodeId) ? base.GetChild(nodeID) : (INode) new AdvRelationsNode((CreateObjectNodeParams) new AdvCreateObjectNodeParams(advRelationsNodeId.ObjectTypeID, advRelationsNodeId.ObjectID, advRelationsNodeId.ID, advRelationsNodeId.CheckedOutBy, advRelationsNodeId.PrjLinkID, advRelationsNodeId.LCStepID, advRelationsNodeId.Caption, advRelationsNodeId.RelationTypeID > 0 ? advRelationsNodeId.RelationTypeID : this._relationTypeID, advRelationsNodeId.Owner, advRelationsNodeId.Sorting, advRelationsNodeId.State, advRelationsNodeId.Version, advRelationsNodeId.BaseVersion, advRelationsNodeId.SiteID, advRelationsNodeId.FiltrationOwnerID, advRelationsNodeId.Contexts, advRelationsNodeId.ProjObjType, advRelationsNodeId.ProjID, advRelationsNodeId.RelGuid, advRelationsNodeId.ModificationID, advRelationsNodeId.Attributes, advRelationsNodeId.Values));
  }

  /// <summary>Вернуть данные по описанию узла</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="dataFormat">Тип запрашиваемых данных</param>
  /// <returns>Запрошенные данные или null</returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (IDescriptor))
      return (object) new AdvRelationsDescriptor(this._categoryID, this._typeID, this.FiltrationOwnerID, this.Contexts, this.ObjID, this.ObjType, this.RelationTypeID, this.Caption, this.CheckedOutBy, this.Owner, this.Sorting, this.LCStepID, this.Attributes, this.Version, this.BaseVersion);
    if (dataFormat == typeof (ICanOpenInNewWindow))
      return (object) new CanOpenInNewWindow();
    if (nodeID is AdvRelationsNodeID advRelationsNodeId)
    {
      if (dataFormat == typeof (IDBTypedObjectID))
        return (object) new DBTypedObjectID(advRelationsNodeId.ObjectTypeID, advRelationsNodeId.ObjectID, advRelationsNodeId.ID, advRelationsNodeId.Caption, advRelationsNodeId.Owner, advRelationsNodeId.Version, advRelationsNodeId.BaseVersion, advRelationsNodeId.SiteID, advRelationsNodeId.ModificationID);
      if (dataFormat == typeof (IDBObjectID))
        return (object) new DBObjectID(advRelationsNodeId.ObjectID, advRelationsNodeId.ID, advRelationsNodeId.Caption, advRelationsNodeId.Owner);
      if (dataFormat == typeof (IDBRelationID))
        return (object) new DBRelationID(advRelationsNodeId.PrjLinkID, advRelationsNodeId.ObjectID, advRelationsNodeId.RelationTypeID, advRelationsNodeId.Sorting, advRelationsNodeId.RelGuid, advRelationsNodeId.ProjID);
      if (dataFormat == typeof (IDBObjectTypeID))
        return (object) new DBObjectTypeID(advRelationsNodeId.ObjectTypeID);
      if (dataFormat == typeof (IDBCheckedOutByID))
        return (object) new DBCheckedOutByID(advRelationsNodeId.ObjectID, advRelationsNodeId.CheckedOutBy, advRelationsNodeId.Owner);
      if (dataFormat == typeof (IDBObjectFiltrationState))
        return (object) new DBObjectFiltrationState(advRelationsNodeId.State);
    }
    return base.GetData(nodeID, dataFormat);
  }
}
