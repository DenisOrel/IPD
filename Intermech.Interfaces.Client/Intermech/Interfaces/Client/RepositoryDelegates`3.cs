// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.RepositoryDelegates`3
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Класс-хранилище делегатов. Выделено, чтобы 1) было public 2) generic-параметры с контрактами были в одном месте, а не в каждом
/// делегате.
/// </summary>
/// <typeparam name="T">Тип создаваемой сущности</typeparam>
/// <typeparam name="IdType">Тип идентификатора сущности в рамках категории</typeparam>
/// <typeparam name="ServerEntityType">Тип интерфейса, реализующего работу с сущностью на сервере</typeparam>
public abstract class RepositoryDelegates<T, IdType, ServerEntityType>
  where T : IEntity<IdType, ServerEntityType>
  where IdType : struct
  where ServerEntityType : class
{
  /// <summary>Упрощённый делегат метода, отвечающего за создание сущности в том случае, если она не был найден в кэше</summary>
  /// <returns>Объект-сущность</returns>
  public delegate T CreateNewEntitySimpleDelegate()
    where T : IEntity<IdType, ServerEntityType>
    where IdType : struct
    where ServerEntityType : class;

  /// <summary>Упрощённый делегат метода, отвечающего за создание сущности в том случае, если она не был найден в кэше</summary>
  /// <param name="id">Идентификатор сущности в рамках её категории</param>
  /// <returns>Объект-сущность</returns>
  public delegate T CreateNewEntityDelegate(IdType id)
    where T : IEntity<IdType, ServerEntityType>
    where IdType : struct
    where ServerEntityType : class;

  /// <summary>Делегат метода, отвечающего за создание сущности в том случае, если она не был найден в кэше</summary>
  /// <param name="id">Идентификатор сущности в рамках её категории</param>
  /// <param name="failIfNotFound">Если Тrue, то в случае недоступности серверного интерфейса сущности выбросится исключительная ситуация</param>
  /// <returns>Объект-сущность</returns>
  public delegate T CreateNewEntityFullDelegate(IdType id, bool failIfNotFound = true)
    where T : IEntity<IdType, ServerEntityType>
    where IdType : struct
    where ServerEntityType : class;
}
