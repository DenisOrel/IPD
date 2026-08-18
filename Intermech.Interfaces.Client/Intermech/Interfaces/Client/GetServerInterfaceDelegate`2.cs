// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.GetServerInterfaceDelegate`2
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Делегат метода получения серверного интерфейса сущности из пользовательской сессии по идентификатору сущности</summary>
/// <typeparam name="IdType">Тип идентификатора сущности</typeparam>
/// <typeparam name="ServerEntityType">Тип серверного интерфейса сущности</typeparam>
/// <param name="session">Пользовательская сессия</param>
/// <param name="id">Идентификатор сущности</param>
/// <param name="failIfNotFound">Выбрасывать ли исключительную ситуацию если серверный интерфейс сущности не получилось получить (напр.
/// сущность удалена, либо вышла из зоны видимости)</param>
/// <returns>Серверный интерфейс сущности</returns>
public delegate ServerEntityType GetServerInterfaceDelegate<IdType, ServerEntityType>(
  IUserSession session,
  IdType id,
  bool failIfNotFound)
  where IdType : struct
  where ServerEntityType : class;
