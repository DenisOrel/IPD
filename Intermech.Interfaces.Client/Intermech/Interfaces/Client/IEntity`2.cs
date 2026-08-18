// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IEntity`2
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Оболочка над серверной сущностью</summary>
/// <typeparam name="IdType">Тип идентификатора сущности</typeparam>
/// <typeparam name="ServerEntityType">Тип серверного интерфейса сущности</typeparam>
public interface IEntity<IdType, ServerEntityType>
  where IdType : struct
  where ServerEntityType : class
{
  /// <summary>Идентификатор сущности</summary>
  /// <value>The identifier</value>
  IdType ID { get; }

  /// <summary>Статус доступности сущности на сервере</summary>
  /// <value>The existance status</value>
  ExistanceStatuses ExistanceStatus { get; }

  /// <summary>
  /// Проверить актуальность сущности (была ли изменена на сервере с момента последнего получения изменений и, если уже была изменена,
  /// вызвать Refresh)
  ///   вызывается каждый раз когда объект данного класса (или потомок) достаётся из кэша репозитория с тем чтобы в том случае,
  ///     если закэшированные данные устарели вызвать их обновление с сервера.
  /// </summary>
  /// <param name="failIfNotFound">если true и проект отсутствует на сервере, то выбросит исключиельную ситуацию</param>
  void CheckActual(bool failIfNotFound = true);

  /// <summary>Вызов метода у серверного интерфейса сущности (вернёт void)</summary>
  /// <param name="serverEntityHandler">Метод обработки серверного интерфейса сущности не возвращающий результата (void)</param>
  /// <param name="failIfNotFound">Если true то попытка вызова для несуществующего объекта выбросит исключительную ситуацию</param>
  void Invoke(
    ServerEntityHandler<ServerEntityType> serverEntityHandler,
    bool failIfNotFound = true);

  /// <summary>Вызов метода у серверного интерфейса сущности (вернёт void)</summary>
  /// <param name="serverEntityHandler">Метод обработки серверного интерфейса сущности не возвращающий результата (void)</param>
  /// <returns>True, если связь с соотв. сущностью на сервере была успешна установленна, то есть она не удалена и видна нам</returns>
  bool TryInvoke(
    ServerEntityHandler<ServerEntityType> serverEntityHandler);

  /// <summary>Вызов метода у серверного интерфейса сущности (вернёт типизированный результат)</summary>
  /// <typeparam name="T">Тип результата возвращаемого вызовом значения</typeparam>
  /// <param name="serverEntityHandler">Метод обработки серверного интерфейса сущности и возвращающий типизированный результат</param>
  /// <param name="failIfNotFound">Если true то попытка вызова для несуществующего объекта выбросит исключительную ситуацию</param>
  /// <returns>Возвращаемое вызовом значение</returns>
  T Invoke<T>(
    ServerEntityHandler<ServerEntityType, T> serverEntityHandler,
    bool failIfNotFound = true);

  /// <summary>Вызов метода у серверного интерфейса сущности (вернёт типизированный результат)</summary>
  /// <typeparam name="T">Тип результата возвращаемого вызовом значения</typeparam>
  /// <param name="serverEntityHandler">Метод обработки серверного интерфейса сущности и возвращающий типизированный результат</param>
  /// <param name="returnValue">[out] Результат обработки сетевого интерфейса сущности</param>
  /// <returns>True, если связь с соотв. сущностью на сервере была успешна установленна, то есть она не удалена и видна нам</returns>
  bool TryInvoke<T>(
    ServerEntityHandler<ServerEntityType, T> serverEntityHandler,
    out T returnValue);
}
