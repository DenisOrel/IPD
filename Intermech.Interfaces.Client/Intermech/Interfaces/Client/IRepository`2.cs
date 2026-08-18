// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IRepository`2
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Интерфейс репозитория сущностей определённой категории</summary>
/// <typeparam name="IdType">Тип идентификатор сущности в рамках её категории</typeparam>
/// <typeparam name="ServerEntityType">Тип интерфейса, реализующего работу с сущностью на сервере</typeparam>
public interface IRepository<IdType, ServerEntityType>
  where IdType : struct
  where ServerEntityType : class
{
  /// <summary>Категория</summary>
  /// <value>The category.</value>
  int Category { get; }

  /// <summary>
  /// Метод получения серверного интерфейса из сессии
  ///   !!! Использовать только внутри Session.Invoke !!!
  /// </summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="id">Идентификатор сущности</param>
  /// <param name="failIfNotFound">Выбрасывать ли исключительную ситуацию если серверный интерфейс сущности не получилось получить (напр.
  /// сущность удалена, либо вышла из зоны видимости)</param>
  /// <returns>Серверный интерфейс сущности</returns>
  ServerEntityType GetServerInterface(IUserSession session, IdType id, bool failIfNotFound);

  /// <summary>Получение серверного интерфейса сущности и вызов метода её обработки (обработка не возвращает резутата)</summary>
  /// <param name="id">Идентификатор сущности, метод которого требуется вызвать</param>
  /// <param name="serverEntityHandler">Метод обработки серверного интерфейса сущности и возвращающий типизированный результат</param>
  /// <param name="failIfNotFound">Если true то попытка вызова для несуществующего объекта выбросит исключительную ситуацию</param>
  void Invoke(
    IdType id,
    ServerEntityHandler<ServerEntityType> serverEntityHandler,
    bool failIfNotFound = true);

  /// <summary>Попытка получения серверного интерфейса сущности и вызова метода её обработки (обработка не возвращает резутата)</summary>
  /// <param name="id">Идентификатор сущности, метод которого требуется вызвать</param>
  /// <param name="serverEntityHandler">Метод обработки серверного интерфейса сущности и возвращающий типизированный результат</param>
  /// <returns>true if it succeeds, false if it fails</returns>
  bool TryInvoke(
    IdType id,
    ServerEntityHandler<ServerEntityType> serverEntityHandler);

  /// <summary>Получение серверного интерфейса сущности и вызов метода её обработки (вернёт типизированный результат)</summary>
  /// <typeparam name="T">Тип результата возвращаемого вызовом значения</typeparam>
  /// <param name="id">Идентификатор сущности, метод которого требуется вызвать</param>
  /// <param name="serverEntityHandler">Метод обработки серверного интерфейса сущности и возвращающий типизированный результат</param>
  /// <param name="failIfNotFound">Если true то попытка вызова для несуществующего объекта выбросит исключительную ситуацию</param>
  /// <returns>Возвращаемое вызовом значение</returns>
  T Invoke<T>(
    IdType id,
    ServerEntityHandler<ServerEntityType, T> serverEntityHandler,
    bool failIfNotFound = true);

  /// <summary>Попытка получения серверного интерфейса сущности и вызова метода её обработки (вернёт типизированный результат)</summary>
  /// <typeparam name="T">Тип результата возвращаемого вызовом значения</typeparam>
  /// <param name="id">Идентификатор сущности, метод которого требуется вызвать</param>
  /// <param name="serverEntityHandler">Метод обработки серверного интерфейса сущности и возвращающий типизированный результат</param>
  /// <param name="returnValue">[out] Результат обработки сетевого интерфейса сущности</param>
  /// <returns>True, если связь с соотв. сущностью на сервере была успешна установленна, то есть она не удалена и видна нам</returns>
  bool TryInvoke<T>(
    IdType id,
    ServerEntityHandler<ServerEntityType, T> serverEntityHandler,
    out T returnValue);
}
