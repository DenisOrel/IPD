// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ServerEntityHandler`2
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Обработчик серверного интерфейса сущности возвращающий типизированный результат</summary>
/// <typeparam name="ServerEntityType">Тип серверного интерфейса сущности</typeparam>
/// <typeparam name="T">Тип результата обработки серверного интерфейса сущности</typeparam>
/// <param name="serverEntity">Серверный интерфейс сущности</param>
/// <returns>Результат обработки серверного интерфейса сущности</returns>
public delegate T ServerEntityHandler<ServerEntityType, T>(ServerEntityType serverEntity) where ServerEntityType : class;
