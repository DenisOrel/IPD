// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ExistanceStatuses
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Возможные статусы доступности сущности на сервере</summary>
public enum ExistanceStatuses
{
  /// <summary>
  /// Неизвестно существует ли сущность на сервере, проверка ещё не производилась
  /// </summary>
  Unknown,
  /// <summary>
  /// По меньшей мере одно соединение с сущностью на сервере уже производилось, соотв. сущность там как минимум была доступна (скорее всего доступна до сих пор)
  /// </summary>
  Exist,
  /// <summary>
  /// Последняя попытка получения серверного интерфейса сущности завершилась неудачно, скорее всего сущность отсутствует на сервере, либо вне зоны видимости
  /// </summary>
  NotExistOnServer,
}
