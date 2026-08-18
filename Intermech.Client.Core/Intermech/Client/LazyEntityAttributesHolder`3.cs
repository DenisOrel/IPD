
// Type: Intermech.Client.LazyEntityAttributesHolder`3
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Extensions;
using Intermech.Interfaces.Client;
using System;


namespace Intermech.Client;

/// <summary>Класс, описывающий сущность (сущностью может быть объект, тип объекта и прочая неведомая) с "ленивыми" атрибутами
///    (то есть загружаемыми с сервера по первому требованию и кэшированными для последующих запросов)
/// </summary>
/// <typeparam name="IdType">Тип идентификатора сущности</typeparam>
/// <typeparam name="ServerEntityType">Тип интерфейса, реализующего работу с данной сущностью на сервере</typeparam>
/// <typeparam name="LoadAttributeFlags">[Enum с атрибутом Flags] Тип набора флагов, перечисляющих наборы атрибутов, которые могут быть
/// закэшированы</typeparam>
public abstract class LazyEntityAttributesHolder<IdType, ServerEntityType, LoadAttributeFlags> : 
  Entity<IdType, ServerEntityType>
  where IdType : struct
  where ServerEntityType : class
  where LoadAttributeFlags : struct, IConvertible
{
  /// <summary>Набор флагов, перечисляющий закэшированные в данном экземпляре наборы атрибутов</summary>
  private LoadAttributeFlags _loadedAttributes;
  /// <summary>Набор флагов, какие атрибуты должны быть инициализированы при создании объекта и при обновлении его содержимого</summary>
  private LoadAttributeFlags _preLoadAttributes;
  /// <summary>Процедуры инициализации набора параметров по идентифицирующего его флагу</summary>
  private LazyEntityAttributesHolder<IdType, ServerEntityType, LoadAttributeFlags>.EntityAttributeDelegate _initAttribute;

  /// <summary>Событие вызываемое перед обновлением атрибутов</summary>
  public event LazyEntityAttributesHolder<IdType, ServerEntityType, LoadAttributeFlags>.LazyEntityAttributesHandler OnBeforeRefresh;

  /// <summary>Событие вызываемое после обновления атрибутов</summary>
  public event LazyEntityAttributesHolder<IdType, ServerEntityType, LoadAttributeFlags>.LazyEntityAttributesHandler OnAfterRefresh;

  /// <summary>Набор флагов, перечисляющий закэшированные в данном экземпляре наборы атрибутов</summary>
  protected LoadAttributeFlags LoadedAttributes => this._loadedAttributes;

  /// <summary>Приватная инициализация полей</summary>
  /// <param name="initAttribute">Функция инициализации значения атрибута из серверного интерфейса сущности и флага, показывающего какой
  /// набор атрибутов надо загрузить</param>
  /// <param name="preLoadAttributes">Набор флагов, перечисляющий атрибуты, которые должны быть закэшированы ещё при создании сущности и
  /// сразу кэшироваться при вызове Refresh</param>
  /// <param name="initialization">Делегат метода инициализации списка атрибутов</param>
  private void Init(
    LazyEntityAttributesHolder<IdType, ServerEntityType, LoadAttributeFlags>.EntityAttributeDelegate initAttribute,
    LoadAttributeFlags preLoadAttributes,
    LazyEntityAttributesHolder<IdType, ServerEntityType, LoadAttributeFlags>.SimpleDelegate initialization)
  {
    this._initAttribute = initAttribute;
    this._preLoadAttributes = preLoadAttributes;
    initialization();
  }

  /// <summary>Конструктор</summary>
  /// <param name="id">Идентификатор сущности</param>
  /// <param name="getServerInterface">Делегат метода получения серверного интерфейса</param>
  /// <param name="initAttribute">Функция инициализации значения атрибута из серверного интерфейса сущности и флага, показывающего какой
  /// набор атрибутов надо загрузить</param>
  /// <param name="preLoadAttributes">Набор флагов, перечисляющий атрибуты, которые должны быть закэшированы ещё при создании сущности и
  /// сразу кэшироваться при вызове Refresh</param>
  public LazyEntityAttributesHolder(
    IdType id,
    GetServerInterfaceDelegate<IdType, ServerEntityType> getServerInterface,
    LazyEntityAttributesHolder<IdType, ServerEntityType, LoadAttributeFlags>.EntityAttributeDelegate initAttribute,
    LoadAttributeFlags preLoadAttributes = default (LoadAttributeFlags))
    : base(id, getServerInterface)
  {
    this.Init(initAttribute, preLoadAttributes, (LazyEntityAttributesHolder<IdType, ServerEntityType, LoadAttributeFlags>.SimpleDelegate) (() => this.Invoke((ServerEntityHandler<ServerEntityType>) (serverEntity => this.InitFromServerEntity(serverEntity)), true)));
  }

  /// <summary>Конструктор</summary>
  /// <param name="id">Идентификатор сущности</param>
  /// <param name="getServerInterface">Делегат метода получения серверного интерфейса</param>
  /// <param name="preLoadAttributes">Набор флагов, перечисляющий атрибуты, которые должны быть закэшированы ещё при создании сущности и
  /// сразу кэшироваться при вызове Refresh</param>
  public LazyEntityAttributesHolder(
    IdType id,
    GetServerInterfaceDelegate<IdType, ServerEntityType> getServerInterface,
    LoadAttributeFlags preLoadAttributes = default (LoadAttributeFlags))
    : this(id, getServerInterface, (LazyEntityAttributesHolder<IdType, ServerEntityType, LoadAttributeFlags>.EntityAttributeDelegate) null, preLoadAttributes)
  {
  }

  /// <summary>Конструктор из готового серверного интерфеса</summary>
  /// <param name="id">Идентификатор</param>
  /// <param name="getServerInterface">Функция получения серверного интерфейса сущности</param>
  /// <param name="initAttribute">Функция инициализации значения атрибута из серверного интерфейса сущности и флага, показывающего какой
  /// набор атрибутов надо загрузить</param>
  /// <param name="serverEntity">Интерфейс серверной сущности</param>
  /// <param name="preLoadAttributes">Набор флагов, перечисляющий атрибуты, которые должны быть закэшированы ещё при создании сущности и
  /// сразу кэшироваться при вызове Refresh</param>
  protected LazyEntityAttributesHolder(
    IdType id,
    GetServerInterfaceDelegate<IdType, ServerEntityType> getServerInterface,
    LazyEntityAttributesHolder<IdType, ServerEntityType, LoadAttributeFlags>.EntityAttributeDelegate initAttribute,
    ServerEntityType serverEntity,
    LoadAttributeFlags preLoadAttributes = default (LoadAttributeFlags))
    : base(id, getServerInterface)
  {
    LazyEntityAttributesHolder<IdType, ServerEntityType, LoadAttributeFlags> attributesHolder = this;
    this.ExistanceStatus = ExistanceStatuses.Exist;
    this.Init(initAttribute, preLoadAttributes, (LazyEntityAttributesHolder<IdType, ServerEntityType, LoadAttributeFlags>.SimpleDelegate) (() => attributesHolder.InitFromServerEntity(serverEntity)));
  }

  /// <summary>Первичная инициализация полей сущности из её серверного интерфейса</summary>
  /// <param name="serverEntity">Интерфейс серверной сущности</param>
  protected override void InitFromServerEntity(ServerEntityType serverEntity)
  {
    if (this._preLoadAttributes.Equals((object) default (LoadAttributeFlags)))
      return;
    this.LoadAttributes(serverEntity, this._preLoadAttributes);
  }

  /// <summary>
  /// Информирование подписчиков о том, что последняя попытка обращению к серверному интерфейсу сущности завершилась неудачно, соотв.
  /// сущность удалена, либо вышла из зоны видимости
  /// </summary>
  protected override void FireIncorrectStatus()
  {
    base.FireIncorrectStatus();
    this._loadedAttributes = default (LoadAttributeFlags);
  }

  /// <summary>
  /// Метод, обрабатывающий ссылку на сущность и коллекцию флагов (или отдельный флаг), идентифицирующих наборы атрибутов, которые могут
  /// быть загружены
  /// </summary>
  /// <exception cref="T:System.Exception">Thrown when an exception error condition occurs.</exception>
  /// <param name="serverEntityType">Type of the server entity</param>
  /// <param name="loadAttributeFlag">Коллекция флагов (или отдельный флаг), идентифицирующих наборы атрибутов, которые могут быть загружены</param>
  protected virtual void InitAttribute(
    ServerEntityType serverEntityType,
    LoadAttributeFlags loadAttributeFlag)
  {
    if (this._initAttribute == null)
      throw new Exception("method InitAttribute must be overrided or use diffent constructor with GetServerInterfaceDelegate param");
    this._initAttribute(serverEntityType, loadAttributeFlag);
  }

  /// <summary>
  /// Проверить что все перечисленные в параметре наборы атрибутов зекэшировать, если чего-то в кэше нет - закэшировать (вызвать для
  /// каждого незакэшированого атрибута _initAttribute)
  /// </summary>
  /// <param name="attributeFlagstoLoad">Набор флагов, идентифицирующий требуемые атрибуты</param>
  public void LoadAttributes(LoadAttributeFlags attributeFlagstoLoad)
  {
    if (this.ExistanceStatus == ExistanceStatuses.NotExistOnServer || !((Enum) (ValueType) attributeFlagstoLoad).HasNewFlags<LoadAttributeFlags>(this._loadedAttributes))
      return;
    this.Invoke((ServerEntityHandler<ServerEntityType>) (serverEntity => this.LoadAttributes(serverEntity, attributeFlagstoLoad)), false);
  }

  /// <summary>Каширование наборов атрибутов, которые идентифицируются переданным набором флагов</summary>
  /// <param name="serverEntity">Серверный интерфейс</param>
  /// <param name="newAttributeFlagstoLoad">Набор флагов, идентифицирующих наборы атрибутов, которые требуется загрузить</param>
  public void LoadAttributes(
    ServerEntityType serverEntity,
    LoadAttributeFlags newAttributeFlagstoLoad)
  {
    foreach (LoadAttributeFlags loadAttributeFlag in ((Enum) (ValueType) newAttributeFlagstoLoad).ForEachNewFlag<LoadAttributeFlags>(this._loadedAttributes))
      this.InitAttribute(serverEntity, loadAttributeFlag);
    ((Enum) (ValueType) this._loadedAttributes).AddFlags<LoadAttributeFlags>(newAttributeFlagstoLoad);
  }

  /// <summary>
  /// Вызывать данный метод перед каждым чтением значения поля атрибута. Проверяет что значение данного атрибута уже загружено и, если это
  /// не так, инициализирует его (_initAttribute)
  /// </summary>
  /// <param name="loadAttributeFlag">Флаг, обозначающий атрибут, значение которого требуется</param>
  protected void BeforeGetAttribute(LoadAttributeFlags loadAttributeFlag)
  {
    if (this.ExistanceStatus == ExistanceStatuses.NotExistOnServer || ((Enum) (ValueType) this._loadedAttributes).HasFlag((Enum) (ValueType) loadAttributeFlag))
      return;
    this.Invoke((ServerEntityHandler<ServerEntityType>) (serverEntity => this.InitAttribute(serverEntity, loadAttributeFlag)), false);
    if (this.ExistanceStatus == ExistanceStatuses.NotExistOnServer)
      return;
    ((Enum) (ValueType) this._loadedAttributes).AddFlags<LoadAttributeFlags>(loadAttributeFlag);
  }

  /// <summary>Обновить значения закешированных атрибутов</summary>
  /// <param name="failIfNotFound">если true и проект отсутствует на сервере, то выбросит исключиельную ситуацию</param>
  /// <returns>true если все атрибуты были обновлены успешно</returns>
  public bool Refresh(bool failIfNotFound = true)
  {
    return this.Refresh(default (ServerEntityType), failIfNotFound);
  }

  /// <summary>Обновить значения закешированных атрибутов</summary>
  /// <param name="serverEntity">Интерфейс серверной итерации</param>
  /// <param name="failIfNotFound">если true и проект отсутствует на сервере, то выбросит исключиельную ситуацию</param>
  /// <returns>true если все атрибуты были обновлены успешно</returns>
  protected virtual bool Refresh(ServerEntityType serverEntity, bool failIfNotFound = false)
  {
    if (this.ExistanceStatus == ExistanceStatuses.NotExistOnServer)
      return false;
    this.FireEventBeforeRefresh();
    this._loadedAttributes = default (LoadAttributeFlags);
    this.ExistanceStatus = ExistanceStatuses.Unknown;
    if (failIfNotFound || !this._preLoadAttributes.Equals((object) default (LoadAttributeFlags)))
    {
      if ((object) serverEntity == null)
        this.Invoke((ServerEntityHandler<ServerEntityType>) (serverEntityInternal => this.InitFromServerEntity(serverEntityInternal)), failIfNotFound);
      else
        this.InitFromServerEntity(serverEntity);
    }
    this.FireEventAfterRefresh();
    return this.ExistanceStatus != ExistanceStatuses.NotExistOnServer;
  }

  /// <summary>
  /// Проверить актуальность объекта сущности (был ли измененён с момента последнего получения изменений и, если уже был измененён, вызвать
  /// Refresh)
  ///   вызывается каждый раз когда объект данного класса (или потомок) достаётся из кэша репозитория с тем чтобы в том случае,
  ///     если закэшированные данные устарели вызвать их обновление с сервера
  /// </summary>
  /// <param name="failIfNotFound">если true и проект отсутствует на сервере, то выбросит исключиельную ситуацию</param>
  public override void CheckActual(bool failIfNotFound = true)
  {
    if (this.ExistanceStatus == ExistanceStatuses.NotExistOnServer)
      return;
    this.Invoke((ServerEntityHandler<ServerEntityType>) (serverEntity =>
    {
      if (this.IsActual(serverEntity))
        return;
      this.Refresh(serverEntity);
    }), true);
  }

  /// <summary>
  /// Вызывается для проверки актуальности сущности (была ли она изменена с момента первоначального получения интерфейса)
  ///   вызывается каждый раз когда объект данного класса (или потомок) достаётся из кэша репозитория с тем чтобы в том случае,
  ///     если закэшированные данные устарели вызвать их обновление с сервера
  /// </summary>
  /// <param name="serverEntity">Серверный интерфейс сущности</param>
  /// <returns>True если данные актуальны и обновление не требуется</returns>
  protected virtual bool IsActual(ServerEntityType serverEntity) => true;

  /// <summary>Информировать подписчиков события вызываемого перед обновлением атрибутов</summary>
  protected virtual void FireEventBeforeRefresh()
  {
    if (this.OnBeforeRefresh == null)
      return;
    this.OnBeforeRefresh(this);
  }

  /// <summary>Информировать подписчиков события вызываемого после обновления атрибутов</summary>
  protected virtual void FireEventAfterRefresh()
  {
    if (this.OnAfterRefresh == null)
      return;
    this.OnAfterRefresh(this);
  }

  /// <summary>
  /// Метод, обрабатывающий ссылку на сущность и коллекцию флагов (или отдельный флаг), идентифицирующих наборы атрибутов, которые могут
  /// быть загружены
  /// </summary>
  /// <param name="serverEntityType">Серверный интерфейс сущности</param>
  /// <param name="loadAttributeFlag">Коллекция флагов (или отдельный флаг), идентифицирующих наборы атрибутов, которые могут быть загружены</param>
  public delegate void EntityAttributeDelegate(
    ServerEntityType serverEntityType,
    LoadAttributeFlags loadAttributeFlag)
    where IdType : struct
    where ServerEntityType : class
    where LoadAttributeFlags : struct, IConvertible;

  /// <summary>Метод, обрабатывающий ссылку на сущность с ленивыми атрибутами</summary>
  /// <param name="lazyEntityAttributesHolder">Сущность с "ленивыми" атрибутами</param>
  public delegate void LazyEntityAttributesHandler(
    LazyEntityAttributesHolder<IdType, ServerEntityType, LoadAttributeFlags> lazyEntityAttributesHolder)
    where IdType : struct
    where ServerEntityType : class
    where LoadAttributeFlags : struct, IConvertible;

  /// <summary>Простейший делегат</summary>
  private delegate void SimpleDelegate()
    where IdType : struct
    where ServerEntityType : class
    where LoadAttributeFlags : struct, IConvertible;
}
