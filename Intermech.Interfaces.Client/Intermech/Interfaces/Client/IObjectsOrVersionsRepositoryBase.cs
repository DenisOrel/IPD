// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IObjectsOrVersionsRepositoryBase
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>База для интерфейсов репозиторием версий объектов и объектов. Содержит методы, общие для обоих интерфейсов, общую функциональность для обоих</summary>
public interface IObjectsOrVersionsRepositoryBase : IRepository<long, IDBObject>
{
  /// <summary>
  /// Метод-фабрика объектов или версий объектов в зависимости от того, у которого репозитория (объектов или версий) вызывается.
  /// </summary>
  /// <param name="objectOrVersionID">Идентификатор объекта или версии объекта в зависимости от того, у которого репозитория (объектов или
  /// версий) вызывается</param>
  /// <param name="preLoadAttributes">Список флагов, показывающих какие наборы атрибутов должны быть закэшированы ещё при создании</param>
  /// <param name="failIfNotFound">Если Тrue, то в случае недоступности серверного интерфейса объекта выбросится исключительная ситуация</param>
  /// <returns>Созданный контейнер атрибутов объекта</returns>
  IObject Create(long objectOrVersionID, ObjectAttributes preLoadAttributes = ObjectAttributes.Default, bool failIfNotFound = true);

  /// <summary>
  /// Метод-фабрика объектов или версий объектов в зависимости от того, у которого репозитория (объектов или версий) вызывается.
  /// </summary>
  /// <param name="objectOrVersionID">Идентификатор объекта или версии объекта в зависимости от того, у которого репозитория (объектов или версий)
  /// вызывается</param>
  /// <param name="iObjectOrVersion">[out] Созданный контейнер атрибутов версии объекта или версии объекта в зависимости от того, у
  /// которого репозитория (объектов или версий) вызывается</param>
  /// <param name="preLoadAttributes">Список флагов, показывающих какие наборы атрибутов должны быть закэшированы ещё при создании</param>
  /// <returns>True, если создание прошло успешно</returns>
  bool TryCreate(
    long objectOrVersionID,
    out IObject iObjectOrVersion,
    ObjectAttributes preLoadAttributes = ObjectAttributes.Default);

  /// <summary>
  /// Получить идентификатор типа объекта по идентификатору объекта или версии объекта в зависимости от того, у которого репозитория
  /// (объектов или версий) вызывается.
  /// </summary>
  /// <param name="objectOrVersionID">Идентификатор объекта или версии объекта в зависимости от того, у которого репозитория (объектов или
  /// версий) вызывается</param>
  /// <param name="failIfNotFound">Если Тrue, то в случае недоступности серверного интерфейса объекта выбросится исключительная ситуация</param>
  /// <returns>Идентификатор типа объекта</returns>
  int GetObjectType(long objectOrVersionID, bool failIfNotFound = true);
}
