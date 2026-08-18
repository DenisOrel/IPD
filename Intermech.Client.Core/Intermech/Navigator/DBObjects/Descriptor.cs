
// Type: Intermech.Navigator.DBObjects.Descriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Classes.Providers;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Класс, предназначенный для описания элементов "Объект базы данных" из
/// пространства навигации, включаемых в коллекцию дескрипторов элементов.
/// Такие коллекции используются для создания всевозможных виртуальных
/// элементов.
/// </summary>
public class Descriptor : 
  ObjectsItems,
  IDescriptor,
  INodeItems,
  IPersistable,
  ICloneable,
  IDescriptorElementStatuses
{
  /// <summary>Нормализованный идентификатор версии объекта</summary>
  protected long _objID;
  /// <summary>Идентификатор версии объекта</summary>
  protected long _realObjID;
  /// <summary>Guid версии объекта</summary>
  protected Guid _objGuid;
  /// <summary>Типа объекта</summary>
  protected int _objTypeID;
  /// <summary>Некорректный идентификатор объекта</summary>
  protected long _invalidObjID;
  /// <summary>Статус подбора корневой версии объекта</summary>
  protected ObjectFiltrationState _state;
  /// <summary>
  /// Статусы элемента пространства навигации. Установка и чтение отдельных полей должно выполняться
  /// с помощью службы IElementStatusesClientService
  /// </summary>
  protected byte[] statuses;
  /// <summary>
  /// Частично-заполненный дескриптор (без обращения к СУБД)
  /// </summary>
  protected bool partiallyFilled;
  /// <summary>
  /// Свойство XML для записи нормализованного идентификатора версии объекта
  /// </summary>
  private const string PropObjectID = "ObjId";
  /// <summary>Свойство XML для записи ид. типа объекта</summary>
  private const string PropObjectTypeID = "TypeId";
  /// <summary>Свойство XML для записи идентификатора версии объекта</summary>
  private const string PropRealObjectID = "RealObjId";
  /// <summary>Guid версии объекта</summary>
  private const string PropObjectGuid = "ObjGuid";
  /// <summary>Номер версии объекта</summary>
  private const string PropObjectVersion = "ObjVersion";
  /// <summary>Признак базовой версии</summary>
  private const string PropObjectBaseVersion = "ObjBaseVersion";
  /// <summary>Служба по работе со статусами элементов</summary>
  private static IElementStatusesClientService _statuses;
  /// <summary>
  /// Служба по работе со статусами элементов в дескрипторах
  /// </summary>
  private static IDescriptorElementStatusesService _desrcStatuses;
  /// <summary>Служба фильтрации составов</summary>
  private static IFiltrationService _filtration;
  /// <summary>Номер версии объекта</summary>
  private long _version;
  /// <summary>Признак базовой версии</summary>
  private long _baseVersion;

  /// <summary>Создать незаполненный экземпляр класса</summary>
  public Descriptor()
  {
    Descriptor._filtration = Descriptor._filtration == null ? ServicesManager.GetService(typeof (IFiltrationService)) as IFiltrationService : Descriptor._filtration;
    Descriptor._statuses = Descriptor._statuses == null ? ServicesManager.GetService(typeof (IElementStatusesClientService)) as IElementStatusesClientService : Descriptor._statuses;
    Descriptor._desrcStatuses = Descriptor._desrcStatuses == null ? ServicesManager.GetService(typeof (IDescriptorElementStatusesService)) as IDescriptorElementStatusesService : Descriptor._desrcStatuses;
  }

  /// <summary>
  /// Создает дескриптор (есть вся информация об объекте, обращение к СУБД не требуется)
  /// </summary>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="objGuid">Guid версии объекта</param>
  /// <param name="state">Статус подобранной версии</param>
  public Descriptor(long objID, Guid objGuid, ObjectFiltrationState state)
    : this()
  {
    this._realObjID = objID;
    this._objID = Math.Abs(objID);
    this._objGuid = objGuid;
    this._state = state;
    this._objTypeID = -1;
    this.partiallyFilled = false;
    Descriptor._statuses.SetElementStatuses16("cad005f2-306c-11d8-b4e9-00304f19f545", this.Statuses, (short) this._state);
    if (Descriptor._desrcStatuses != null)
      Descriptor._desrcStatuses.FireSetDescriptorStatuses((IDescriptorElementStatuses) this);
    this.SetLifecycleLevelStatuses();
  }

  /// <summary>Создает дескриптор</summary>
  /// <param name="objID">Идентификатор версии объекта</param>
  public Descriptor(long objID)
    : this()
  {
    this._realObjID = objID;
    this._objID = Math.Abs(objID);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ObjectSystemProperties systemProperties = sessionKeeper.Session.GetObjectSystemProperties(objID, false, true);
      if (systemProperties != null)
      {
        this._objGuid = systemProperties.VersionGuid;
        this._version = (long) systemProperties.VersionID;
        this._baseVersion = Convert.ToInt64(systemProperties.IsBaseVersion);
        this._objTypeID = systemProperties.ObjectTypeID;
        this.SetLifecycleLevelStatuses(systemProperties.LCStepID);
      }
      else
      {
        this._objGuid = Guid.Empty;
        this._realObjID = 0L;
        this._objID = 0L;
        this._invalidObjID = objID;
        this._version = 0L;
        this._baseVersion = 0L;
        this._objTypeID = -1;
      }
      this._state = sessionKeeper.Session.GetObjectVersionFiltrationState(this._realObjID, Descriptor._filtration.RuleClass);
      Descriptor._statuses.SetElementStatuses16("cad005f2-306c-11d8-b4e9-00304f19f545", this.Statuses, (short) this._state);
      if (Descriptor._desrcStatuses == null)
        return;
      Descriptor._desrcStatuses.FireSetDescriptorStatuses((IDescriptorElementStatuses) this);
    }
  }

  /// <summary>Создает дескриптор</summary>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="state">Статус подобранной версии</param>
  public Descriptor(long objID, ObjectFiltrationState state)
    : this(objID, state, false)
  {
  }

  /// <summary>Создает дескриптор</summary>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="state">Статус подобранной версии</param>
  /// <param name="notCheckObject">Не выполнять обращение к серверу приложений, дескриптор получается частично заполненным</param>
  public Descriptor(long objID, ObjectFiltrationState state, bool notCheckObject)
    : this()
  {
    this._realObjID = objID;
    this._objID = Math.Abs(objID);
    this._state = state;
    this.partiallyFilled = notCheckObject;
    if (notCheckObject)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ObjectSystemProperties systemProperties = sessionKeeper.Session.GetObjectSystemProperties(objID, false, true);
      if (systemProperties != null)
      {
        this._objGuid = systemProperties.VersionGuid;
        this._version = (long) systemProperties.VersionID;
        this._baseVersion = Convert.ToInt64(systemProperties.IsBaseVersion);
        this._objTypeID = systemProperties.ObjectTypeID;
        this.SetLifecycleLevelStatuses(systemProperties.LCStepID);
      }
      else
      {
        this._objGuid = Guid.Empty;
        this._realObjID = 0L;
        this._objID = 0L;
        this._invalidObjID = objID;
        this._version = 0L;
        this._baseVersion = 0L;
        this._objTypeID = -1;
      }
    }
    Descriptor._statuses.SetElementStatuses16("cad005f2-306c-11d8-b4e9-00304f19f545", this.Statuses, (short) this._state);
    if (Descriptor._desrcStatuses == null)
      return;
    Descriptor._desrcStatuses.FireSetDescriptorStatuses((IDescriptorElementStatuses) this);
  }

  /// <summary>Создает дескриптор</summary>
  /// <param name="objGuid">Guid объекта</param>
  public Descriptor(Guid objGuid)
    : this()
  {
    this._state = ObjectFiltrationState.fsCorrespondingSingle;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ObjectSystemProperties systemProperties = sessionKeeper.Session.GetObjectSystemProperties(objGuid, false);
      if (systemProperties != null)
      {
        this._objGuid = systemProperties.VersionGuid;
        this._realObjID = systemProperties.ObjectID;
        this._objID = Math.Abs(this._realObjID);
        this._objTypeID = systemProperties.ObjectTypeID;
        this._version = (long) systemProperties.VersionID;
        this._baseVersion = Convert.ToInt64(systemProperties.IsBaseVersion);
        this.SetLifecycleLevelStatuses(systemProperties.LCStepID);
      }
      else
      {
        this._objGuid = Guid.Empty;
        this._realObjID = 0L;
        this._objID = 0L;
        this._version = 0L;
        this._baseVersion = 0L;
        this._objTypeID = -1;
      }
      this._state = sessionKeeper.Session.GetObjectVersionFiltrationState(this._realObjID, Descriptor._filtration.RuleClass);
      Descriptor._statuses.SetElementStatuses16("cad005f2-306c-11d8-b4e9-00304f19f545", this.Statuses, (short) this._state);
      if (Descriptor._desrcStatuses == null)
        return;
      Descriptor._desrcStatuses.FireSetDescriptorStatuses((IDescriptorElementStatuses) this);
    }
  }

  /// <summary>Создает дескриптор</summary>
  /// <param name="objGuid">Guid объекта</param>
  /// <param name="state">Статус подобранной версии</param>
  public Descriptor(Guid objGuid, ObjectFiltrationState state)
    : this()
  {
    this._state = state;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ObjectSystemProperties systemProperties = sessionKeeper.Session.GetObjectSystemProperties(objGuid, false);
      if (systemProperties != null)
      {
        this._objGuid = systemProperties.VersionGuid;
        this._realObjID = systemProperties.ObjectID;
        this._objID = Math.Abs(this._realObjID);
        this._objTypeID = systemProperties.ObjectTypeID;
        this._version = (long) systemProperties.VersionID;
        this._baseVersion = Convert.ToInt64(systemProperties.IsBaseVersion);
        this.SetLifecycleLevelStatuses(systemProperties.LCStepID);
      }
      else
      {
        this._objGuid = Guid.Empty;
        this._realObjID = 0L;
        this._objID = 0L;
        this._version = 0L;
        this._baseVersion = 0L;
        this._objTypeID = -1;
      }
    }
    Descriptor._statuses.SetElementStatuses16("cad005f2-306c-11d8-b4e9-00304f19f545", this.Statuses, (short) this._state);
    if (Descriptor._desrcStatuses == null)
      return;
    Descriptor._desrcStatuses.FireSetDescriptorStatuses((IDescriptorElementStatuses) this);
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора.
  /// </summary>
  /// <param name="state"></param>
  protected Descriptor(PersistentState state)
    : this()
  {
    object obj1 = state.GetValue("ObjId");
    this._objID = obj1 == null || !(obj1 is long num1) ? 0L : num1;
    object obj2 = state.GetValue("RealObjId");
    this._realObjID = obj2 == null || !(obj2 is long num2) ? this._objID : num2;
    object obj3 = state.GetValue("TypeId");
    this._objTypeID = obj3 == null || !(obj3 is int num3) ? -1 : num3;
    object obj4 = state.GetValue(nameof (ObjGuid));
    this._objGuid = obj4 == null || !(obj4 is Guid guid) ? Guid.Empty : guid;
    object obj5 = state.GetValue("ObjVersion");
    this._version = obj5 == null || !(obj5 is long num4) ? 0L : num4;
    object obj6 = state.GetValue("ObjBaseVersion");
    this._baseVersion = obj6 == null || !(obj6 is long num5) ? 0L : num5;
    this.CorrectState();
    if (!ObjectHelper.IsUnknownObjectVersionID(this._objID) && !this.CheckAccessLevel(this._objID))
      throw new Exception();
    this.SetLifecycleLevelStatuses();
  }

  /// <summary>Скорректировать значение статуса у корневого объекта</summary>
  public virtual void CorrectState()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._state = sessionKeeper.Session.GetObject(this._realObjID, false) == null ? sessionKeeper.Session.GetObjectVersionFiltrationState(-this._realObjID, Descriptor._filtration.RuleClass) : sessionKeeper.Session.GetObjectVersionFiltrationState(this._realObjID, Descriptor._filtration.RuleClass);
      Descriptor._statuses.SetElementStatuses16("cad005f2-306c-11d8-b4e9-00304f19f545", this.Statuses, (short) this._state);
    }
    if (Descriptor._desrcStatuses == null)
      return;
    Descriptor._desrcStatuses.FireSetDescriptorStatuses((IDescriptorElementStatuses) this);
  }

  /// <summary>Скорректировать значения полей у дескриптора</summary>
  /// <param name="objID">Новый идентификатор версии объекта</param>
  public virtual void CorrectDescriptor(long objID)
  {
    this._realObjID = objID;
    this._objID = Math.Abs(objID);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objID, false);
      if (objectActualCopy != null)
      {
        this._objGuid = objectActualCopy.ObjectGUID;
        this._version = (long) objectActualCopy.VersionID;
        this._baseVersion = Convert.ToInt64(objectActualCopy.IsBaseVersion);
      }
      else
      {
        this._objGuid = Guid.Empty;
        this._realObjID = 0L;
        this._objID = 0L;
        this._invalidObjID = objID;
        this._version = 0L;
        this._baseVersion = 0L;
      }
      this._state = sessionKeeper.Session.GetObjectVersionFiltrationState(this._realObjID, Descriptor._filtration.RuleClass);
      Descriptor._statuses.SetElementStatuses16("cad005f2-306c-11d8-b4e9-00304f19f545", this.Statuses, (short) this._state);
    }
    if (Descriptor._desrcStatuses == null)
      return;
    Descriptor._desrcStatuses.FireSetDescriptorStatuses((IDescriptorElementStatuses) this);
  }

  /// <summary>Идентификатор версии объекта</summary>
  public long ObjectID
  {
    [DebuggerStepThrough] get => this._realObjID;
  }

  /// <summary>Guid версии объекта</summary>
  public Guid ObjGuid
  {
    [DebuggerStepThrough] get => this._objGuid;
  }

  /// <summary>Некорректный идентификатор объекта</summary>
  public long InvalidObjID
  {
    [DebuggerStepThrough] get => this._invalidObjID;
  }

  /// <summary>Является ли дескриптор некорректно заполненным</summary>
  public bool InvalidDescriptor
  {
    get
    {
      return this.partiallyFilled ? this._objID == 0L || this._realObjID == 0L : this._objGuid == Guid.Empty || this._objID == 0L || this._realObjID == 0L;
    }
  }

  /// <summary>Статус подбора версии объекта</summary>
  public ObjectFiltrationState State
  {
    [DebuggerStepThrough] get => this._state;
    set => this._state = value;
  }

  /// <summary>Номер версии</summary>
  public long Version
  {
    [DebuggerStepThrough] get => this._version;
  }

  /// <summary>Признак базовой версии</summary>
  public long BaseVersion
  {
    [DebuggerStepThrough] get => this._baseVersion;
  }

  /// <summary>Отобразить колонку в поле</summary>
  /// <param name="column">Колонка</param>
  /// <returns>Поле</returns>
  public virtual object MapColumnToField(NodeColumn column)
  {
    return column.SchemeGuid == Intermech.Navigator.Consts.NavigatorColumnSchemeGuid && column.ID.Equals((object) "F_STATUSES") ? (object) new NodeColumnID((object) -77, AttributeSourceTypes.Object) : Helper.MapColumnToFieldName(column) ?? this.MapVirtualColumnToField(column);
  }

  /// <summary>Создать описание корневого узла</summary>
  /// <returns>Описание корневого узла</returns>
  public virtual INodeID GetRecordNodeID()
  {
    if (this.InvalidDescriptor)
      return (INodeID) null;
    if (Descriptor._desrcStatuses != null)
      Descriptor._desrcStatuses.FireSetDescriptorStatuses((IDescriptorElementStatuses) this);
    if (this.partiallyFilled)
      return this.CreateObjectNodeIdFromParams(new CreateObjectNodeParams(-1, this._realObjID, 0L, 0L, -1L, -1, string.Empty, -1, 0L, 0L, ObjectFiltrationState.fsNotRequired, 0L, 0L, string.Empty, 0L, Guid.Empty, 0L));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        ObjectSystemPropertiesEx systemPropertiesEx = sessionKeeper.Session.GetObjectSystemPropertiesEx(this._objGuid, false);
        if (systemPropertiesEx == null)
          return (INodeID) null;
        this._realObjID = systemPropertiesEx.ObjectID;
        this._objID = Math.Abs(this._realObjID);
        return this.CreateObjectNodeIdFromParams(new CreateObjectNodeParams(systemPropertiesEx.ObjectTypeID, systemPropertiesEx.ObjectID, systemPropertiesEx.ID, systemPropertiesEx.CheckOutBy, -1L, systemPropertiesEx.LCStepID, systemPropertiesEx.Caption, -1, systemPropertiesEx.OwnerID, 0L, this._state, (long) systemPropertiesEx.VersionID, Convert.ToInt64(systemPropertiesEx.IsBaseVersion), systemPropertiesEx.SiteID, 0L, Guid.Empty, 0L));
      }
      catch (ObjectNotFoundException ex)
      {
        return (INodeID) null;
      }
    }
  }

  /// <summary>Виртуальный метод создания NodeID. Написано для того, чтобы потомки могли перекрыть и создавать свои, расширенные NodeID</summary>
  protected virtual INodeID CreateObjectNodeIdFromParams(
    CreateObjectNodeParams createObjectNodeParams)
  {
    return (INodeID) new NodeID(createObjectNodeParams);
  }

  /// <summary>Получить значения полей</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="fields">Поля</param>
  /// <returns>Значения полей</returns>
  public virtual object[] GetRecordValues(INodeID nodeID, object[] fields)
  {
    try
    {
      object[] recordValues = new object[fields.Length];
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(((NodeID) nodeID).ObjectID);
        for (int index = 0; index < recordValues.Length; ++index)
        {
          try
          {
            if (fields[index] is ObligatoryObjectAttributes || fields[index] is int)
            {
              object[] valuesById = dbObject.GetValuesByID((int) fields[index], false);
              if (valuesById != null)
                recordValues[index] = valuesById[0];
            }
            else if (fields[index] is string)
              recordValues[index] = dbObject.GetAttributeByName((string) fields[index]).Value;
            else if (fields[index] is NodeColumnID)
            {
              if ((fields[index] as NodeColumnID).ID.Equals((object) -77))
              {
                this.SetLifecycleLevelStatuses(dbObject.LCStep);
                if (Descriptor._desrcStatuses != null)
                  Descriptor._desrcStatuses.FireSetDescriptorStatuses((IDescriptorElementStatuses) this);
                recordValues[index] = (object) this.Statuses;
              }
              else
                recordValues[index] = this.GetUnknownFieldValue(fields[index], nodeID);
            }
            else
              recordValues[index] = this.GetUnknownFieldValue(fields[index], nodeID);
          }
          catch
          {
          }
        }
      }
      return recordValues;
    }
    catch (ObjectNotFoundException ex)
    {
      return (object[]) null;
    }
  }

  /// <summary>Получение из дескриптора значения поля неизвестного типа. Предназначено для перекрытия и обработки кастомных (напр. виртуальных,
  /// чьё значение рассчитывается уже на клиенте) полей в потомках</summary>
  protected virtual object GetUnknownFieldValue(object field, INodeID nodeID) => (object) null;

  public override IUpdateAnalyser GetAnalyser(
    NodeViewCapabilities capabilities,
    object sender,
    NotificationEventArgs e)
  {
    return e.EventName == "ObjectsCreated" ? (IUpdateAnalyser) null : base.GetAnalyser(capabilities, sender, e);
  }

  /// <summary>Распарсить адрес</summary>
  /// <param name="address">Адрес</param>
  /// <returns>Описание узла по его адресу</returns>
  public override INodeID ParseAddress(string address)
  {
    if (this.InvalidDescriptor)
      return (INodeID) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this._objGuid);
        long result;
        if (long.TryParse(address, out result) && Math.Abs(dbObject.ObjectID) == Math.Abs(result))
          return (INodeID) new NodeID(new CreateObjectNodeParams(dbObject.ObjectType, dbObject.ObjectID, dbObject.ID, dbObject.CheckoutBy, -1L, dbObject.LCStep, dbObject.Caption, -1, dbObject.OwnerID, 0L, ObjectFiltrationState.fsNotRequired, (long) dbObject.VersionID, Convert.ToInt64(dbObject.IsBaseVersion), dbObject.SiteID, 0L, Guid.Empty, dbObject.ModificationID));
        if (string.Compare(dbObject.Caption, address, true) == 0)
          return (INodeID) new NodeID(new CreateObjectNodeParams(dbObject.ObjectType, dbObject.ObjectID, dbObject.ID, dbObject.CheckoutBy, -1L, dbObject.LCStep, dbObject.Caption, -1, dbObject.OwnerID, 0L, ObjectFiltrationState.fsNotRequired, (long) dbObject.VersionID, Convert.ToInt64(dbObject.IsBaseVersion), dbObject.SiteID, 0L, Guid.Empty, dbObject.ModificationID));
      }
      catch (ObjectNotFoundException ex)
      {
      }
      return (INodeID) null;
    }
  }

  /// <summary>Сравнить экземпляр класса с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    return !(obj is Descriptor descriptor) ? base.Equals(obj) : this._objGuid == descriptor._objGuid;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  [DebuggerStepThrough]
  public override int GetHashCode() => this._objGuid.GetHashCode();

  /// <summary>Формирует сериализованное представление объекта.</summary>
  /// <param name="state">Контейнер значений для хранения сериализованного представления объекта</param>
  public virtual void GetObjectData(PersistentState state)
  {
    state.AddValue("ObjId", (object) this._objID);
    state.AddValue("RealObjId", (object) this._realObjID);
    state.AddValue("ObjGuid", (object) this._objGuid);
    state.AddValue("ObjVersion", (object) this._version);
    state.AddValue("ObjBaseVersion", (object) this._baseVersion);
  }

  /// <summary>Создать точную копию экземпляра объекта</summary>
  /// <returns>Точная копия экземпляра объекта</returns>
  public object Clone()
  {
    return (object) new Descriptor()
    {
      _objID = this._objID,
      _realObjID = this._realObjID,
      _objGuid = this._objGuid,
      _state = this._state,
      partiallyFilled = this.partiallyFilled
    };
  }

  /// <summary>
  /// Дескриптор элемента пространства навигации, чьи статусы управляются данным интерфейсом
  /// </summary>
  public IDescriptor RootDescriptor
  {
    [DebuggerStepThrough] get => (IDescriptor) this;
  }

  /// <summary>
  /// Статусы элемента пространства навигации. Установка и чтение отдельных полей должно выполняться
  /// с помощью службы IElementStatusesClientService
  /// </summary>
  public byte[] Statuses
  {
    get
    {
      if (this.statuses == null)
        this.statuses = new byte[Descriptor._statuses.Capacity];
      return this.statuses;
    }
    set => this.statuses = value;
  }

  private bool CheckAccessLevel(long objectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return this.CheckAccessLevel(sessionKeeper.Session.GetObjectActualCopy(objectVersionID, true));
  }

  private bool CheckAccessLevel(IDBObject @object)
  {
    return @object.AccessLevel <= @object.Session.SecurityLevel;
  }

  private void SetLifecycleLevelStatuses()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = (IDBObject) null;
      if (!ObjectHelper.IsUnknownObjectVersionID(this._objID))
        dbObject = sessionKeeper.Session.GetObject(this._objID, false);
      else if (this._objGuid != Guid.Empty)
        dbObject = sessionKeeper.Session.GetObject(this._objGuid, false);
      if (dbObject == null)
        return;
      this.SetLifecycleLevelStatuses(dbObject.LCStep);
    }
  }

  private void SetLifecycleLevelStatuses(int lifecycleStepID)
  {
    IMSLifeCycleStep lcStep = MetaDataHelper.GetLCStep(lifecycleStepID);
    if (lcStep == null)
      return;
    int lifecycleLevelID = lcStep.LevelID;
    List<IMSLifeCycleLevel> lcLevelsList = MetaDataHelper.GetLCLevelsList();
    lcLevelsList.Sort();
    int num = lcLevelsList.IndexOf(lcLevelsList.Find((Predicate<IMSLifeCycleLevel>) (o => o.LevelID == lifecycleLevelID)));
    if (num < 0)
      return;
    Descriptor._statuses.SetElementStatuses16("{7074E0E4-B3AB-4B3E-AD56-050CD256AF10}", this.Statuses, Convert.ToInt16(num + 1));
  }
}
