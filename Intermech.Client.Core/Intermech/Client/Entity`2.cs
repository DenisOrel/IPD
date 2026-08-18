
// Type: Intermech.Client.Entity`2
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;


namespace Intermech.Client;

/// <summary>Оболочка над серверной сущностью.
///     Сущность - любое нечто, имеющее идентификатор, серверный интерфейс чего можно получить. Например объект, связь, тип объекта, тип
///     связи, итерация, атрибут, тип атрибута и так далее.
///         Категория - это тип сущности, идентификаторы сущностей уникальны в рамках категории
///             допускается, что сущность будет иметь составной идентификатор, представленный структурой
/// 
///  Оболочка позволяет вызывать методы серверных интерфейсов сущности, не прописывая метод получения серверного интерфейса каждый раз
///     имеет ряд событий, информирующих об изменении статуса сущности
///         (например если при очередном обращении к серверу оказалось, что сущность там была удалена - все подписчики сразу узнают об
///         этом. У потомков событий больше)
///     есть репозиторий сущностей, который кэширует из, позволяя не лезть на сервер при каждом создании есть ряд потомков, например
///     LazyEntityAttributesHolder позволяет кэшировать атрибуты сущностей, дабы исключить повторные обращения к серверу.
/// </summary>
/// <typeparam name="IdType">Тип идентификатора сущности</typeparam>
/// <typeparam name="ServerEntityType">Тип серверного интерфейса сущности</typeparam>
[Serializable]
public abstract class Entity<IdType, ServerEntityType> : IEntity<IdType, ServerEntityType>
  where IdType : struct
  where ServerEntityType : class
{
  /// <summary>Делегат метода получения интерфейса серверной сущности</summary>
  private readonly GetServerInterfaceDelegate<IdType, ServerEntityType> GetServerInterface;
  /// <summary>Статус соединения сущности с сервером</summary>
  private ExistanceStatuses _existanceStatus;

  /// <summary>Идентификатор сущности</summary>
  public IdType ID { get; private set; }

  /// <summary>Статус доступности сущности на сервере</summary>
  public ExistanceStatuses ExistanceStatus
  {
    get => this._existanceStatus;
    protected set
    {
      if (value == ExistanceStatuses.NotExistOnServer && this._existanceStatus != ExistanceStatuses.NotExistOnServer)
      {
        this._existanceStatus = value;
        this.FireIncorrectStatus();
      }
      else
        this._existanceStatus = value;
    }
  }

  /// <summary>
  /// Вызывается если очередная попытка обращения к серверному интерфейсу завершилась некорректно (сущность удалена, либо вышла из зоны
  /// видимости)
  /// </summary>
  public event Entity<IdType, ServerEntityType>.EntityHandler OnIncorrectStatus;

  /// <summary>Конструктор</summary>
  /// <param name="id">Идентификатор сущности</param>
  /// <param name="getServerInterfaceMethod">Метод получения серверного интерфейса сущности из сессии по его идентификатору</param>
  public Entity(
    IdType id,
    GetServerInterfaceDelegate<IdType, ServerEntityType> getServerInterfaceMethod)
  {
    this.ID = id;
    this.GetServerInterface = getServerInterfaceMethod;
  }

  /// <summary>Конструктор из готового серверного интерфеса</summary>
  /// <param name="id">Идентификатор</param>
  /// <param name="getServerInterfaceMethod">Метод получения серверного интерфейса сущности из сессии по его идентификатору</param>
  /// <param name="serverEntity">Интерфейс серверной сущности</param>
  public Entity(
    IdType id,
    GetServerInterfaceDelegate<IdType, ServerEntityType> getServerInterfaceMethod,
    ServerEntityType serverEntity)
    : this(id, getServerInterfaceMethod)
  {
    this.InitFromServerEntity(serverEntity);
  }

  /// <summary>Первичная инициализация полей сущности из её серверного интерфейса</summary>
  /// <param name="serverEntity">Интерфейс серверной сущности</param>
  protected virtual void InitFromServerEntity(ServerEntityType serverEntity)
  {
  }

  /// <summary>
  /// Информирование подписчиков о том, что последняя попытка обращению к серверному интерфейсу сущности завершилась неудачно, соотв.
  /// сущность удалена, либо вышла из зоны видимости
  /// </summary>
  protected virtual void FireIncorrectStatus()
  {
    if (this.OnIncorrectStatus == null)
      return;
    this.OnIncorrectStatus(this);
  }

  /// <summary>
  /// Проверить актуальность сущности (была ли изменена на сервере с момента последнего получения изменений и, если уже была изменена,
  /// вызвать Refresh)
  ///   вызывается каждый раз когда объект данного класса (или потомок) достаётся из кэша репозитория с тем чтобы в том случае,
  ///     если закэшированные данные устарели вызвать их обновление с сервера
  /// </summary>
  /// <param name="failIfNotFound">если true и проект отсутствует на сервере, то выбросит исключиельную ситуацию</param>
  public virtual void CheckActual(bool failIfNotFound = true)
  {
  }

  /// <summary>Вызов метода у серверного интерфейса сущности (вернёт void)</summary>
  /// <param name="serverEntityHandler">Метод обработки серверного интерфейса сущности не возвращающий результата (void)</param>
  /// <param name="failIfNotFound">Если true то попытка вызова для несуществующего объекта выбросит исключительную ситуацию</param>
  public void Invoke(
    ServerEntityHandler<ServerEntityType> serverEntityHandler,
    bool failIfNotFound = true)
  {
    Session.Invoke((Session.SessionHandler) (session =>
    {
      ServerEntityType serverEntity = this.GetServerInterface(session, this.ID, failIfNotFound);
      this.ExistanceStatus = (object) serverEntity != null ? ExistanceStatuses.Exist : ExistanceStatuses.NotExistOnServer;
      if (this.ExistanceStatus != ExistanceStatuses.Exist)
        return;
      serverEntityHandler(serverEntity);
    }));
  }

  /// <summary>Вызов метода у серверного интерфейса сущности (вернёт void)</summary>
  /// <param name="serverEntityHandler">Метод обработки серверного интерфейса сущности не возвращающий результата (void)</param>
  /// <returns>True, если связь с соотв. сущностью на сервере была успешна установленна, то есть она не удалена и видна нам</returns>
  public bool TryInvoke(
    ServerEntityHandler<ServerEntityType> serverEntityHandler)
  {
    this.Invoke(serverEntityHandler, false);
    return this.ExistanceStatus == ExistanceStatuses.Exist;
  }

  /// <summary>Вызов метода у серверного интерфейса сущности (вернёт типизированный результат)</summary>
  /// <typeparam name="T">Тип результата возвращаемого вызовом значения</typeparam>
  /// <param name="serverEntityHandler">Метод обработки серверного интерфейса сущности и возвращающий типизированный результат</param>
  /// <param name="failIfNotFound">Если true то попытка вызова для несуществующего объекта выбросит исключительную ситуацию</param>
  /// <returns>Возвращаемое вызовом значение</returns>
  public T Invoke<T>(
    ServerEntityHandler<ServerEntityType, T> serverEntityHandler,
    bool failIfNotFound = true)
  {
    return Session.Invoke<T>((Session.SessionHandler<T>) (session => (object) this.GetServerInterface(session, this.ID, failIfNotFound) == null ? default (T) : serverEntityHandler(this.GetServerInterface(session, this.ID, failIfNotFound))));
  }

  /// <summary>Вызов метода у серверного интерфейса сущности (вернёт типизированный результат)</summary>
  /// <typeparam name="T">Тип результата возвращаемого вызовом значения</typeparam>
  /// <param name="serverEntityHandler">Метод обработки серверного интерфейса сущности и возвращающий типизированный результат</param>
  /// <param name="returnValue">[out] Результат обработки сетевого интерфейса сущности</param>
  /// <returns>True, если связь с соотв. сущностью на сервере была успешна установленна, то есть она не удалена и видна нам</returns>
  public bool TryInvoke<T>(
    ServerEntityHandler<ServerEntityType, T> serverEntityHandler,
    out T returnValue)
  {
    returnValue = this.Invoke<T>(serverEntityHandler, true);
    return this.ExistanceStatus == ExistanceStatuses.Exist;
  }

  /// <summary>Событие сущности</summary>
  /// <param name="entity">сущность</param>
  public delegate void EntityHandler(Entity<IdType, ServerEntityType> entity)
    where IdType : struct
    where ServerEntityType : class;
}
