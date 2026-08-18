
// Type: Intermech.Client.Repository
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Cache;
using Intermech.Cache.Policies;
using Intermech.Cache.Storages;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Snapshots;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


namespace Intermech.Client;

/// <summary>A repository</summary>
/// <summary>Репозитории сущностей.
/// Фактически - фабрики сущностей с кэшем построенным по алгоритму замещения элементов кэша, известный как LRU (частный случай
/// семейства алгоритмов LRU-k при k = 1). Он выталкивает элемент, к которому дольше всего не было обращений.</summary>
/// <summary>Реализация репозитория сущностей</summary>
public static class Repository
{
  /// <summary>Репозиторий объектов</summary>
  [NotNull]
  public static readonly IObjectsRepository Objects = (IObjectsRepository) new Repository.ObjectsRepository(200);
  /// <summary>Репозиторий версий объектов</summary>
  [NotNull]
  public static readonly IObjectVersionsRepository ObjectVersions = (IObjectVersionsRepository) new Repository.ObjectVersionsRepository(200);
  /// <summary>Репозиторий итераций объектов</summary>
  [NotNull]
  public static readonly ISnapshotsRepository Snapshots = (ISnapshotsRepository) new Repository.SnapshotsRepository(10);

  /// <summary>Реализация репозитория версий объектов</summary>
  private abstract class ObjectsOrVersionsRepositoryBase : 
    Repository.RepositoryBase<IObject, long, IDBObject>,
    IObjectsOrVersionsRepositoryBase,
    IRepository<long, IDBObject>
  {
    /// <summary>Статический конструктор репозитория</summary>
    /// <param name="category">Категория</param>
    /// <param name="ObjectsInCache">Кол-во сущностей в кэше по-умолчанию</param>
    public ObjectsOrVersionsRepositoryBase(int category, int ObjectsInCache = 300)
      : base(category, ObjectsInCache)
    {
    }

    /// <summary>
    /// Метод-фабрика объектов или версий объектов в зависимости от того, у которого репозитория (объектов или версий) вызывается.
    /// </summary>
    /// <param name="objectOrVersionID">Идентификатор объекта или версии объекта в зависимости от того, у которого репозитория (объектов или
    /// версий) вызывается</param>
    /// <param name="preLoadAttributes">Список флагов, показывающих какие наборы атрибутов должны быть закэшированы ещё при создании</param>
    /// <param name="failIfNotFound">Если Тrue, то в случае недоступности серверного интерфейса объекта выбросится исключительная ситуация</param>
    /// <returns>Созданный контейнер атрибутов объекта</returns>
    public IObject Create(
      long objectOrVersionID,
      ObjectAttributes preLoadAttributes = ObjectAttributes.Default,
      bool failIfNotFound = true)
    {
      // ISSUE: reference to a compiler-generated method
      return this.Get(objectOrVersionID, (RepositoryDelegates<IObject, long, IDBObject>.CreateNewEntitySimpleDelegate) (() => (IObject) this.\u003C\u003En__0<Repository.ObjectImplementation>(objectOrVersionID, (ServerEntityHandler<IDBObject, Repository.ObjectImplementation>) (iDbObject => new Repository.ObjectImplementation(this.Category == 2 ? objectOrVersionID : iDbObject.ObjectID, iDbObject, preLoadAttributes)), failIfNotFound)), failIfNotFound);
    }

    /// <summary>
    /// Метод-фабрика объектов или версий объектов в зависимости от того, у которого репозитория (объектов или версий) вызывается.
    /// </summary>
    /// <param name="objectOrVersionID">Идентификатор объекта или версии объекта в зависимости от того, у которого репозитория (объектов или версий)
    /// вызывается</param>
    /// <param name="iObjectOrVersion">[out] Созданный контейнер атрибутов версии объекта или версии объекта в зависимости от того, у
    /// которого репозитория (объектов или версий) вызывается</param>
    /// <param name="preLoadAttributes">Список флагов, показывающих какие наборы атрибутов должны быть закэшированы ещё при создании</param>
    /// <returns>True, если создание прошло успешно</returns>
    public bool TryCreate(
      long objectOrVersionID,
      out IObject iObjectOrVersion,
      ObjectAttributes preLoadAttributes = ObjectAttributes.Default)
    {
      iObjectOrVersion = this.Create(objectOrVersionID, preLoadAttributes, false);
      return iObjectOrVersion != null;
    }

    /// <summary>
    /// Получить идентификатор типа объекта по идентификатору объекта или версии объекта в зависимости от того, у которого репозитория
    /// (объектов или версий) вызывается.
    /// </summary>
    /// <param name="objectOrVersionID">Идентификатор объекта или версии объекта в зависимости от того, у которого репозитория (объектов или
    /// версий) вызывается</param>
    /// <param name="failIfNotFound">Если Тrue, то в случае недоступности серверного интерфейса объекта выбросится исключительная ситуация</param>
    /// <returns>Идентификатор типа объекта</returns>
    public int GetObjectType(long objectOrVersionID, bool failIfNotFound = true)
    {
      IObject @object = this.Create(objectOrVersionID, ObjectAttributes.ObjectType, failIfNotFound);
      return @object.ExistanceStatus == ExistanceStatuses.NotExistOnServer ? 0 : @object.ObjectType;
    }
  }

  /// <summary>Реализация репозитория версий объектов</summary>
  /// <summary>Статический конструктор репозитория</summary>
  /// <param name="ObjectsInCache">Кол-во сущностей в кэше по-умолчанию</param>
  private class ObjectVersionsRepository(int ObjectsInCache = 300) : 
    Repository.ObjectsOrVersionsRepositoryBase(1, ObjectsInCache),
    IObjectVersionsRepository,
    IObjectsOrVersionsRepositoryBase,
    IRepository<long, IDBObject>
  {
    /// <summary>
    /// Метод получения серверного интерфейса из сессии
    ///   !!! Использовать только внутри Session.Invoke !!!
    /// </summary>
    /// <param name="session">Пользовательская сессия</param>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="failIfNotFound">Выбрасывать ли исключительную ситуацию если серверный интерфейс сущности не получилось получить (напр.
    /// сущность удалена, либо вышла из зоны видимости)</param>
    /// <returns>Серверный интерфейс сущности</returns>
    public override IDBObject GetServerInterface(
      IUserSession session,
      long id,
      bool failIfNotFound)
    {
      return session.GetObject(id, failIfNotFound);
    }
  }

  /// <summary>Реализация репозитория версий объектов</summary>
  /// <summary>Статический конструктор репозитория</summary>
  /// <param name="ObjectsInCache">Кол-во сущностей в кэше по-умолчанию</param>
  private class ObjectsRepository(int ObjectsInCache = 300) : 
    Repository.ObjectsOrVersionsRepositoryBase(2, ObjectsInCache),
    IObjectsRepository,
    IObjectsOrVersionsRepositoryBase,
    IRepository<long, IDBObject>
  {
    /// <summary>
    /// Метод получения серверного интерфейса из сессии
    ///   !!! Использовать только внутри Session.Invoke !!!
    /// </summary>
    /// <param name="session">Пользовательская сессия</param>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="failIfNotFound">Выбрасывать ли исключительную ситуацию если серверный интерфейс сущности не получилось получить (напр.
    /// сущность удалена, либо вышла из зоны видимости)</param>
    /// <returns>Серверный интерфейс сущности</returns>
    public override IDBObject GetServerInterface(
      IUserSession session,
      long id,
      bool failIfNotFound)
    {
      return session.GetObjectByID(id, failIfNotFound);
    }
  }

  /// <summary>Ленивый класс для хранения кэшированной информации о реально существующем в БД объекте</summary>
  [Serializable]
  private class ObjectImplementation : 
    LazyEntityAttributesHolder<long, IDBObject, ObjectAttributes>,
    IObject,
    IEntity<long, IDBObject>
  {
    /// <summary>Уникальный идентификатор объекта (НЕ ВЕРСИИ!!!)</summary>
    private long _objectID;
    /// <summary>GUID Версии</summary>
    private Guid _versionGUID;
    /// <summary>GUID объекта (НЕ ВЕРСИИ!!!)</summary>
    private Guid _objectGUID;
    /// <summary>Cтроковое представление объекта</summary>
    private string _caption;
    /// <summary>Дата создания</summary>
    private DateTime _createDate;
    /// <summary>Дата последней модификации объекта</summary>
    private DateTime _modifyDate;
    /// <summary>Этап жизненного цикла</summary>
    private int _lcStep;
    /// <summary>Тип объекта</summary>
    private int _objectType;
    /// <summary>Узлы информационной системы</summary>
    private string _siteID;
    /// <summary>
    /// Идентификатор версии объекта, на основе которой была создана данная версия объекта. Если это самая первая версия (или родительская
    /// версия былу удалена), то возвращает -1.
    /// </summary>
    private long _parentVersionID;
    /// <summary>Идентификатор проекта, к которому принадлежит объект. Если == 0, то объект создан вне контекста проекта.</summary>
    private long _projectID;

    /// <summary>Уникальный идентификатор версии объекта</summary>
    private long _versionID => this.ID;

    /// <summary>Конструктор</summary>
    /// <param name="versionID">Идентификатор версии объекта</param>
    /// <param name="preLoadAttributes">Набор флагов, перечисляющий атрибуты, которые должны быть закэшированы ещё при создании сущности и
    /// сразу кэшироваться при вызове Refresh</param>
    public ObjectImplementation(long versionID, ObjectAttributes preLoadAttributes = ObjectAttributes.Default)
      : base(versionID, new GetServerInterfaceDelegate<long, IDBObject>(((IRepository<long, IDBObject>) Repository.ObjectVersions).GetServerInterface), preLoadAttributes | ObjectAttributes.ModifyDate)
    {
    }

    /// <summary>Конструктор</summary>
    /// <param name="versionID">Идентификатор версии объекта</param>
    /// <param name="iDBObjectObject">Zero-based index of the database object</param>
    /// <param name="preLoadAttributes">Набор флагов, перечисляющий атрибуты, которые должны быть закэшированы ещё при создании сущности и
    /// сразу кэшироваться при вызове Refresh</param>
    public ObjectImplementation(
      long versionID,
      IDBObject iDBObjectObject,
      ObjectAttributes preLoadAttributes = ObjectAttributes.Default)
      : base(versionID, new GetServerInterfaceDelegate<long, IDBObject>(((IRepository<long, IDBObject>) Repository.ObjectVersions).GetServerInterface), (LazyEntityAttributesHolder<long, IDBObject, ObjectAttributes>.EntityAttributeDelegate) null, iDBObjectObject, preLoadAttributes | ObjectAttributes.ModifyDate)
    {
    }

    /// <summary>
    /// Вызывается для проверки актуальности сущности (была ли она изменена с момента первоначального получения интерфейса)
    ///   вызывается каждый раз когда объект данного класса (или потомок) достаётся из кэша репозитория с тем чтобы в том случае,
    ///     если закэшированные данные устарели вызвать их обновление с сервера
    /// </summary>
    /// <param name="iDBObject">Серверный интерфейс сущности</param>
    /// <returns>True если данные актуальны и обновление не требуется</returns>
    protected override bool IsActual(IDBObject iDBObject)
    {
      return iDBObject.ModifyDate == this._modifyDate;
    }

    /// <summary>Загрузить указанные данные объекта из переданного серверного интерфейса</summary>
    /// <exception cref="T:System.Exception">Thrown when an exception error condition occurs.</exception>
    /// <param name="iDBObject">Серверный интерфейс объекта</param>
    /// <param name="attribute">Набор флагов, показывающий какие данные требуется загрузить</param>
    protected override void InitAttribute(IDBObject iDBObject, ObjectAttributes attribute)
    {
      switch (attribute)
      {
        case ObjectAttributes.ObjectID:
          this._objectID = iDBObject.ID;
          break;
        case ObjectAttributes.VersionGUID:
          this._versionGUID = iDBObject.ObjectGUID;
          break;
        case ObjectAttributes.ObjectGUID:
          this._objectGUID = iDBObject.GUID;
          break;
        case ObjectAttributes.Caption:
          this._caption = iDBObject.Caption;
          break;
        case ObjectAttributes.CreateDate:
          this._createDate = iDBObject.CreateDate;
          break;
        case ObjectAttributes.ModifyDate:
          this._modifyDate = iDBObject.ModifyDate;
          break;
        case ObjectAttributes.LCStep:
          this._lcStep = iDBObject.LCStep;
          break;
        case ObjectAttributes.ObjectType:
          this._objectType = iDBObject.ObjectType;
          break;
        case ObjectAttributes.SiteID:
          this._siteID = iDBObject.SiteID;
          break;
        case ObjectAttributes.ParentVersionID:
          this._parentVersionID = iDBObject.ParentVersionID;
          break;
        case ObjectAttributes.ProjectID:
          this._projectID = iDBObject.ParentVersionID;
          break;
        default:
          throw new Exception("Unknown snapshot attribute: " + attribute.ToString());
      }
    }

    /// <summary>Уникальный идентификатор версии объекта</summary>
    public long VersionID
    {
      [DebuggerStepThrough] get => this.ID;
    }

    /// <summary>Уникальный идентификатор объекта (НЕ ВЕРСИИ!!!)</summary>
    public long ObjectID
    {
      [DebuggerStepThrough] get
      {
        this.BeforeGetAttribute(ObjectAttributes.ObjectID);
        return this._objectID;
      }
    }

    /// <summary>GUID Версии</summary>
    public Guid VersionGUID
    {
      [DebuggerStepThrough] get
      {
        this.BeforeGetAttribute(ObjectAttributes.VersionGUID);
        return this._versionGUID;
      }
    }

    /// <summary>GUID объекта (НЕ ВЕРСИИ!!!)</summary>
    public Guid ObjectGUID
    {
      [DebuggerStepThrough] get
      {
        this.BeforeGetAttribute(ObjectAttributes.ObjectGUID);
        return this._objectGUID;
      }
    }

    /// <summary>Cтроковое представление объекта</summary>
    public string Caption
    {
      [DebuggerStepThrough] get
      {
        this.BeforeGetAttribute(ObjectAttributes.Caption);
        return this._caption;
      }
    }

    /// <summary>Дата создания</summary>
    public DateTime CreateDate
    {
      [DebuggerStepThrough] get
      {
        this.BeforeGetAttribute(ObjectAttributes.CreateDate);
        return this._createDate;
      }
    }

    /// <summary>Дата последней модификации объекта</summary>
    public DateTime ModifyDate
    {
      [DebuggerStepThrough] get
      {
        this.BeforeGetAttribute(ObjectAttributes.ModifyDate);
        return this._modifyDate;
      }
    }

    /// <summary>Этап жизненного цикла</summary>
    public int LCStep
    {
      [DebuggerStepThrough] get
      {
        this.BeforeGetAttribute(ObjectAttributes.LCStep);
        return this._lcStep;
      }
    }

    /// <summary>Тип объекта</summary>
    public int ObjectType
    {
      [DebuggerStepThrough] get
      {
        this.BeforeGetAttribute(ObjectAttributes.ObjectType);
        return this._objectType;
      }
    }

    /// <summary>Узлы информационной системы</summary>
    public string SiteID
    {
      [DebuggerStepThrough] get
      {
        this.BeforeGetAttribute(ObjectAttributes.SiteID);
        return this._siteID;
      }
    }

    /// <summary>
    /// Идентификатор версии объекта, на основе которой была создана данная версия объекта. Если это самая первая версия (или родительская
    /// версия былу удалена), то возвращает -1.
    /// </summary>
    public long ParentVersionID
    {
      [DebuggerStepThrough] get
      {
        this.BeforeGetAttribute(ObjectAttributes.ParentVersionID);
        return this._parentVersionID;
      }
    }

    /// <summary>Идентификатор проекта, к которому принадлежит объект. Если == 0, то объект создан вне контекста проекта.</summary>
    public long ProjectID
    {
      [DebuggerStepThrough] get
      {
        this.BeforeGetAttribute(ObjectAttributes.ProjectID);
        return this._projectID;
      }
    }
  }

  /// <summary>Реализация репозитория итераций объектов</summary>
  /// <summary>Конструктор</summary>
  /// <param name="ObjectsInCache">Кол-во сущностей в кэше по-умолчанию</param>
  private class SnapshotsRepository(int ObjectsInCache = 300) : 
    Repository.RepositoryBase<ISnapshot, long, IDBObjectSnapshot>(23, ObjectsInCache),
    ISnapshotsRepository,
    IRepository<long, IDBObjectSnapshot>
  {
    /// <summary>
    /// Метод получения серверного интерфейса из сессии
    ///   !!! Использовать только внутри Session.Invoke !!!
    /// </summary>
    /// <param name="session">Пользовательская сессия</param>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="failIfNotFound">Выбрасывать ли исключительную ситуацию если серверный интерфейс сущности не получилось получить (напр.
    /// сущность удалена, либо вышла из зоны видимости)</param>
    /// <returns>Серверный интерфейс сущности</returns>
    public override IDBObjectSnapshot GetServerInterface(
      IUserSession session,
      long id,
      bool failIfNotFound)
    {
      return session.GetSnapshot(id, failIfNotFound);
    }

    /// <summary>Конструктор экземляров итераций</summary>
    /// <param name="snapshotID">Идентификатор итерации</param>
    /// <param name="preLoadAttributes">Список флагов, показывающих какие наборы атрибутов должны быть закэшированы ещё при создании</param>
    /// <param name="failIfNotFound">Если Тrue, то в случае недоступности серверного интерфейса итерации выбросится исключительная ситуация</param>
    /// <returns>Созданная итерация</returns>
    public ISnapshot Create(
      long snapshotID,
      SnapshotAttributes preLoadAttributes = SnapshotAttributes.Default,
      bool failIfNotFound = true)
    {
      // ISSUE: reference to a compiler-generated method
      return this.Get(snapshotID, (RepositoryDelegates<ISnapshot, long, IDBObjectSnapshot>.CreateNewEntitySimpleDelegate) (() => (ISnapshot) this.\u003C\u003En__0<Repository.SnapshotImplentation>(snapshotID, (ServerEntityHandler<IDBObjectSnapshot, Repository.SnapshotImplentation>) (iDbSnapshot => new Repository.SnapshotImplentation(snapshotID, iDbSnapshot, preLoadAttributes)), failIfNotFound)), failIfNotFound);
    }

    /// <summary>Конструктор экземляров итераций</summary>
    /// <param name="snapshotID">Идентификатор итерации</param>
    /// <param name="snapshot">[out] Созданная итерация</param>
    /// <param name="preLoadAttributes">Список флагов, показывающих какие наборы атрибутов должны быть закэшированы ещё при создании</param>
    /// <returns>True, если создание прошло успешно</returns>
    public bool TryCreate(
      long snapshotID,
      out ISnapshot snapshot,
      SnapshotAttributes preLoadAttributes = SnapshotAttributes.Default)
    {
      snapshot = this.Create(snapshotID, preLoadAttributes, false);
      return snapshot != null;
    }
  }

  /// <summary>Реализация хранилища кэшированной информации о реально существующей в БД итерации</summary>
  private class SnapshotImplentation : 
    LazyEntityAttributesHolder<long, IDBObjectSnapshot, SnapshotAttributes>,
    ISnapshot,
    IEntity<long, IDBObjectSnapshot>
  {
    /// <summary>Имя снимка</summary>
    protected string _name;
    /// <summary>Дата и время последней модификации итерации</summary>
    private DateTime _modifyDate;
    /// <summary>Владелец итерации</summary>
    private long _ownerID;
    /// <summary>Идентификатор головного объекта итерации</summary>
    private long _rootObjectID;
    /// <summary>Идентификатор версии головного объекта итерации</summary>
    private long _rootObjectVersionID;
    /// <summary>
    /// / <summary>
    ///  Таблица дополнительных атрибутов корневого объекта итерации
    /// / </summary>
    /// </summary>
    private DataTable _rootObjectAttributes;
    /// <summary>Хэш-список идентификаторов версий объектов, входящих в итерацию</summary>
    private HashSet<long> _objectVerIdInSnapshot;
    /// <summary>
    /// Хэш таблица, где ключом является идентификатор версии объекта, включённого в итерацию, а значением - таблица дополнительных атрибутов
    /// этого объекта
    /// </summary>
    private Dictionary<long, DataTable> _objectAttributes;

    /// <summary>Gets the object attributes</summary>
    private Dictionary<long, DataTable> ObjectAttributes
    {
      [DebuggerStepThrough] get
      {
        if (this._objectAttributes == null)
          this._objectAttributes = new Dictionary<long, DataTable>();
        return this._objectAttributes;
      }
    }

    /// <summary>Конструктор итерации</summary>
    /// <param name="id">Идентификатор итерации</param>
    /// <param name="preLoadAttributes">Набор флагов, перечисляющий атрибуты, которые должны быть закэшированы ещё при создании сущности и
    /// сразу кэшироваться при вызове Refresh</param>
    public SnapshotImplentation(long id, SnapshotAttributes preLoadAttributes = SnapshotAttributes.Default)
      : base(id, new GetServerInterfaceDelegate<long, IDBObjectSnapshot>(((IRepository<long, IDBObjectSnapshot>) Repository.Snapshots).GetServerInterface), preLoadAttributes)
    {
    }

    /// <summary>Конструктор итерации</summary>
    /// <param name="id">Идентификатор итерации</param>
    /// <param name="iDBObjectSnapshot">Серверный интерфейс итерации</param>
    /// <param name="preLoadAttributes">Набор флагов, перечисляющий атрибуты, которые должны быть закэшированы ещё при создании сущности и
    /// сразу кэшироваться при вызове Refresh</param>
    public SnapshotImplentation(
      long id,
      IDBObjectSnapshot iDBObjectSnapshot,
      SnapshotAttributes preLoadAttributes = SnapshotAttributes.Default)
      : base(id, new GetServerInterfaceDelegate<long, IDBObjectSnapshot>(((IRepository<long, IDBObjectSnapshot>) Repository.Snapshots).GetServerInterface), (LazyEntityAttributesHolder<long, IDBObjectSnapshot, SnapshotAttributes>.EntityAttributeDelegate) null, iDBObjectSnapshot, preLoadAttributes | SnapshotAttributes.ModifyDate)
    {
    }

    /// <summary>
    /// Вызывается для проверки актуальности сущности (была ли она изменена с момента первоначального получения интерфейса)
    ///   вызывается каждый раз когда объект данного класса (или потомок) достаётся из кэша репозитория с тем чтобы в том случае,
    ///     если закэшированные данные устарели вызвать их обновление с сервера
    /// </summary>
    /// <param name="iDBObjectSnapshot">Серверный интерфейс сущности</param>
    /// <returns>True если данные актуальны и обновление не требуется</returns>
    protected override bool IsActual(IDBObjectSnapshot iDBObjectSnapshot)
    {
      return iDBObjectSnapshot.SnapshotModifyDate == this._modifyDate;
    }

    /// <summary>Обновить значения закешированных атрибутов</summary>
    /// <param name="iDBObjectSnapshot">Интерфейс серверной итерации</param>
    /// <param name="failIfNotFound">если true и проект отсутствует на сервере, то выбросит исключиельную ситуацию</param>
    /// <returns>true если все атрибуты были обновлены успешно</returns>
    protected override bool Refresh(IDBObjectSnapshot iDBObjectSnapshot, bool failIfNotFound = false)
    {
      this._objectAttributes = (Dictionary<long, DataTable>) null;
      return base.Refresh(iDBObjectSnapshot, failIfNotFound);
    }

    /// <summary>Загрузить указанные данные итерации из переданного серверного интерфейса</summary>
    /// <exception cref="T:System.Exception">Thrown when an exception error condition occurs.</exception>
    /// <param name="iDBObjectSnapshot">Серверный интерфейс итерации</param>
    /// <param name="attribute">Набор флагов, показывающий какие данные требуется загрузить</param>
    protected override void InitAttribute(
      IDBObjectSnapshot iDBObjectSnapshot,
      SnapshotAttributes attribute)
    {
      switch (attribute)
      {
        case SnapshotAttributes.Name:
          this._name = iDBObjectSnapshot.SnapshotName;
          break;
        case SnapshotAttributes.ModifyDate:
          this._modifyDate = iDBObjectSnapshot.SnapshotModifyDate;
          break;
        case SnapshotAttributes.Owner:
          this._ownerID = iDBObjectSnapshot.SnapshotOwnerID;
          break;
        case SnapshotAttributes.RootObject:
          this._rootObjectID = iDBObjectSnapshot.ID;
          this._rootObjectVersionID = iDBObjectSnapshot.ObjectID;
          break;
        case SnapshotAttributes.RootObjectAttributes:
          this._rootObjectAttributes = iDBObjectSnapshot.GetAttributes(this.RootObjectVersionID);
          this.ObjectAttributes[this.RootObjectVersionID] = this.RootObjectAttributes;
          break;
        case SnapshotAttributes.ObjectsInSnapshot:
          this._objectVerIdInSnapshot = new HashSet<long>((IEnumerable<long>) iDBObjectSnapshot.GetObjectsList());
          break;
        default:
          throw new Exception("Unknown snapshot attribute: " + attribute.ToString());
      }
    }

    /// <summary>Имя итерации</summary>
    public string Name
    {
      [DebuggerStepThrough] get
      {
        this.BeforeGetAttribute(SnapshotAttributes.Name);
        return this._name;
      }
    }

    /// <summary>Дата и время последней модификации итерации</summary>
    public DateTime ModifyDate
    {
      [DebuggerStepThrough] get
      {
        this.BeforeGetAttribute(SnapshotAttributes.ModifyDate);
        return this._modifyDate;
      }
    }

    /// <summary>Владелец итерации</summary>
    public long OwnerID
    {
      [DebuggerStepThrough] get
      {
        this.BeforeGetAttribute(SnapshotAttributes.Owner);
        return this._ownerID;
      }
    }

    /// <summary>Идентификатор головного объекта итерации</summary>
    public long RootObjectID
    {
      [DebuggerStepThrough] get
      {
        this.BeforeGetAttribute(SnapshotAttributes.RootObject);
        return this._rootObjectID;
      }
    }

    /// <summary>Идентификатор версии головного объекта итерации</summary>
    public long RootObjectVersionID
    {
      [DebuggerStepThrough] get
      {
        this.BeforeGetAttribute(SnapshotAttributes.RootObject);
        return this._rootObjectVersionID;
      }
    }

    /// <summary>Таблица дополнительных атрибутов корневого объекта итерации</summary>
    public DataTable RootObjectAttributes
    {
      [DebuggerStepThrough] get
      {
        this.BeforeGetAttribute(SnapshotAttributes.RootObjectAttributes);
        return this._rootObjectAttributes;
      }
    }

    /// <summary>Проверить входит ли версия объекта в итерацию</summary>
    /// <param name="objectVerID">Идентификатор версии</param>
    /// <returns>True если данный объект входит в итерацию</returns>
    [DebuggerStepThrough]
    public bool ObjectInSnapshot(long objectVerID)
    {
      this.BeforeGetAttribute(SnapshotAttributes.ObjectsInSnapshot);
      return this._objectVerIdInSnapshot.Contains(objectVerID);
    }

    /// <summary>Получить таблицу дополнительных атрибутов объекта, включённого в итерацию</summary>
    /// <exception cref="T:Intermech.ObjectNotFoundException">Thrown when an Object Not Found error condition occurs.</exception>
    /// <param name="objectVerID">Идетификатор версии объекта</param>
    /// <param name="failIfNotFound">[default true] Выбрасывать ли исключение если объект с переданным идентификатором не включён в итерацию</param>
    /// <returns>Таблица дополнительных атрибутов объекта, включённого в итерацию</returns>
    public DataTable GetObjectInSnapshotAttributes(long objectVerID, bool failIfNotFound = true)
    {
      if (objectVerID == this.RootObjectVersionID)
        return this.RootObjectAttributes;
      if (!this.ObjectAttributes.ContainsKey(objectVerID))
      {
        if (this.ObjectInSnapshot(objectVerID))
        {
          this._objectAttributes[objectVerID] = this.Invoke<DataTable>((ServerEntityHandler<IDBObjectSnapshot, DataTable>) (dbSnapshot => dbSnapshot.GetAttributes(objectVerID)), true);
        }
        else
        {
          if (failIfNotFound)
            throw new ObjectNotFoundException(objectVerID);
          this._objectAttributes[objectVerID] = (DataTable) null;
        }
      }
      return this._objectAttributes[objectVerID];
    }
  }

  /// <summary>Реализация репозитория сущностей определённой категории</summary>
  /// <typeparam name="EntityType">Тип интерфейса экземляров</typeparam>
  /// <typeparam name="IdType">Тип идентификатор сущности в рамках её категории</typeparam>
  /// <typeparam name="ServerEntityType">Тип интерфейса, реализующего работу с сущностью на сервере</typeparam>
  private abstract class RepositoryBase<EntityType, IdType, ServerEntityType> : 
    IRepository<IdType, ServerEntityType>
    where EntityType : IEntity<IdType, ServerEntityType>
    where IdType : struct
    where ServerEntityType : class
  {
    /// <summary>Менеджер кэша сущностей</summary>
    private readonly CacheManager _cacheManager;
    /// <summary>Максимально количество сущностей, содержащихся в кэше</summary>
    public const int DefaultObjectsInCache = 300;

    /// <summary>Категория</summary>
    public int Category { get; private set; }

    /// <summary>Статический конструктор репозитория</summary>
    /// <param name="category">Категория</param>
    /// <param name="ObjectsInCache">Кол-во сущностей в кэше по-умолчанию</param>
    public RepositoryBase(int category, int ObjectsInCache = 300)
    {
      this.Category = category;
      this._cacheManager = new CacheManager((IStorage) new InMemoryStorage((long) ObjectsInCache), (IReplacementPolicy) new Lru());
    }

    /// <summary>
    /// Получение сущности по идентификатору её идентификатору
    ///   более удобен для использования с лямбда-методом создания сущности, т.к. передачу туда параметров, которые там итак должны быть
    ///   (т.к. были переданны в данный метод)
    /// </summary>
    /// <exception cref="T:System.Exception">Thrown when an exception error condition occurs</exception>
    /// <param name="id">Идентификатор сущности в рамках её категории</param>
    /// <param name="createNewEntitySimpleDelegate">Метод создания сущности в том случае, если она не была обнаружена в кэше</param>
    /// <param name="failIfNotFound">Если Тrue, то в случае недоступности серверного интерфейса сущности выбросится исключительная ситуация</param>
    /// <returns>Объект-сущность</returns>
    protected EntityType Get(
      IdType id,
      RepositoryDelegates<EntityType, IdType, ServerEntityType>.CreateNewEntitySimpleDelegate createNewEntitySimpleDelegate,
      bool failIfNotFound = true)
    {
      EntityType data = (EntityType) this._cacheManager[(object) id];
      if ((object) data != null)
      {
        if (data.ExistanceStatus != ExistanceStatuses.NotExistOnServer)
          data.CheckActual(failIfNotFound);
        if (data.ExistanceStatus == ExistanceStatuses.NotExistOnServer & failIfNotFound)
          throw new Exception($"Object with id={id.ToString()} not found in category={this.Category.ToString()}");
      }
      else
      {
        data = createNewEntitySimpleDelegate();
        this._cacheManager.Add((object) id, (object) data);
      }
      return data;
    }

    /// <summary>
    /// Упрощённый статический метод получения сущности по её идентификатору
    ///   более удобен для использования с лямбда-методом создания сущности, т.к. передачу туда параметров, которые там итак должны быть
    ///   (т.к. были переданны в данный метод)
    /// </summary>
    /// <param name="id">Идентификатор сущности в рамках её категории</param>
    /// <param name="createNewEntityDelegate">Метод создания сущности в том случае, если она не была обнаружена в кэше</param>
    /// <param name="failIfNotFound">Если Тrue, то в случае недоступности серверного интерфейса сущности выбросится исключительная ситуация</param>
    /// <returns>Объект-сущность</returns>
    protected EntityType Get(
      IdType id,
      RepositoryDelegates<EntityType, IdType, ServerEntityType>.CreateNewEntityDelegate createNewEntityDelegate,
      bool failIfNotFound = true)
    {
      return this.Get(id, (RepositoryDelegates<EntityType, IdType, ServerEntityType>.CreateNewEntitySimpleDelegate) (() => createNewEntityDelegate(id)), failIfNotFound);
    }

    /// <summary>Получение сущности по идентификатору</summary>
    /// <param name="id">Идентификатор сущности в рамках её категории</param>
    /// <param name="createNewEntityFullDelegate">Метод создания сущности в том случае, если она не была обнаружена в кэше</param>
    /// <param name="failIfNotFound">Если Тrue, то в случае недоступности серверного интерфейса сущности выбросится исключительная ситуация</param>
    /// <returns>Объект-сущность</returns>
    protected EntityType Get(
      IdType id,
      RepositoryDelegates<EntityType, IdType, ServerEntityType>.CreateNewEntityFullDelegate createNewEntityFullDelegate,
      bool failIfNotFound = true)
    {
      return this.Get(id, (RepositoryDelegates<EntityType, IdType, ServerEntityType>.CreateNewEntitySimpleDelegate) (() => createNewEntityFullDelegate(id, failIfNotFound)), failIfNotFound);
    }

    /// <summary>
    /// Метод получения серверного интерфейса из сессии
    ///   !!! Использовать только внутри Session.Invoke !!!
    /// </summary>
    /// <param name="session">Пользовательская сессия</param>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="failIfNotFound">Выбрасывать ли исключительную ситуацию если серверный интерфейс сущности не получилось получить (напр.
    /// сущность удалена, либо вышла из зоны видимости)</param>
    /// <returns>Серверный интерфейс сущности</returns>
    public abstract ServerEntityType GetServerInterface(
      IUserSession session,
      IdType id,
      bool failIfNotFound);

    /// <summary>Получение серверного интерфейса сущности и вызов метода её обработки (обработка не возвращает резутата)</summary>
    /// <param name="id">Идентификатор сущности, метод которого требуется вызвать</param>
    /// <param name="serverEntityHandler">Метод обработки серверного интерфейса сущности и возвращающий типизированный результат</param>
    /// <param name="failIfNotFound">Если true то попытка вызова для несуществующего объекта выбросит исключительную ситуацию</param>
    public void Invoke(
      IdType id,
      ServerEntityHandler<ServerEntityType> serverEntityHandler,
      bool failIfNotFound = true)
    {
      Session.Invoke((Session.SessionHandler) (session =>
      {
        ServerEntityType serverInterface = this.GetServerInterface(session, id, failIfNotFound);
        if ((object) serverInterface == null)
          return;
        serverEntityHandler(serverInterface);
      }));
    }

    /// <summary>Попытка получения серверного интерфейса сущности и вызова метода её обработки (обработка не возвращает резутата)</summary>
    /// <param name="id">Идентификатор сущности, метод которого требуется вызвать</param>
    /// <param name="serverEntityHandler">Метод обработки серверного интерфейса сущности и возвращающий типизированный результат</param>
    /// <returns>true if it succeeds, false if it fails</returns>
    public bool TryInvoke(
      IdType id,
      ServerEntityHandler<ServerEntityType> serverEntityHandler)
    {
      bool found = false;
      Session.Invoke((Session.SessionHandler) (session =>
      {
        ServerEntityType serverInterface = this.GetServerInterface(session, id, false);
        found = (object) serverInterface != null;
        if (!found)
          return;
        serverEntityHandler(serverInterface);
      }));
      return found;
    }

    /// <summary>Получение серверного интерфейса сущности и вызов метода её обработки (вернёт типизированный результат)</summary>
    /// <typeparam name="T">Тип результата возвращаемого вызовом значения</typeparam>
    /// <param name="id">Идентификатор сущности, метод которого требуется вызвать</param>
    /// <param name="serverEntityHandler">Метод обработки серверного интерфейса сущности и возвращающий типизированный результат</param>
    /// <param name="failIfNotFound">Если true то попытка вызова для несуществующего объекта выбросит исключительную ситуацию</param>
    /// <returns>Возвращаемое вызовом значение</returns>
    public T Invoke<T>(
      IdType id,
      ServerEntityHandler<ServerEntityType, T> serverEntityHandler,
      bool failIfNotFound = true)
    {
      return Session.Invoke<T>((Session.SessionHandler<T>) (session =>
      {
        ServerEntityType serverInterface = this.GetServerInterface(session, id, failIfNotFound);
        return (object) serverInterface == null ? default (T) : serverEntityHandler(serverInterface);
      }));
    }

    /// <summary>Попытка получения серверного интерфейса сущности и вызова метода её обработки (вернёт типизированный результат)</summary>
    /// <typeparam name="T">Тип результата возвращаемого вызовом значения</typeparam>
    /// <param name="id">Идентификатор сущности, метод которого требуется вызвать</param>
    /// <param name="serverEntityHandler">Метод обработки серверного интерфейса сущности и возвращающий типизированный результат</param>
    /// <param name="returnValue">[out] Результат обработки сетевого интерфейса сущности</param>
    /// <returns>True, если связь с соотв. сущностью на сервере была успешна установленна, то есть она не удалена и видна нам</returns>
    public bool TryInvoke<T>(
      IdType id,
      ServerEntityHandler<ServerEntityType, T> serverEntityHandler,
      out T returnValue)
    {
      returnValue = Session.Invoke<T>((Session.SessionHandler<T>) (session =>
      {
        ServerEntityType serverInterface = this.GetServerInterface(session, id, false);
        return (object) serverInterface == null ? default (T) : serverEntityHandler(serverInterface);
      }));
      return false;
    }
  }
}
