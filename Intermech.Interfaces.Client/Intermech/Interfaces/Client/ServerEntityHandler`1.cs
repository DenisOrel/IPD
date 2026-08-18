// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ServerEntityHandler`1
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Обработчик серверного интерфейса сущности не возвращающий результата (void)</summary>
/// <typeparam name="ServerEntityType">Тип серверного интерфейса сущности</typeparam>
/// <param name="serverEntity">Cерверный интерфейс сущности</param>
public delegate void ServerEntityHandler<ServerEntityType>(ServerEntityType serverEntity) where ServerEntityType : class;
