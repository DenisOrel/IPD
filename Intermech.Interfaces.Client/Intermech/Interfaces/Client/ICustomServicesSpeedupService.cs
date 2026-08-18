// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ICustomServicesSpeedupService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс клиентского сервиса, ускоряющего получение сервисов сервера приложений.
/// Реализация должна быть thread safe.
/// </summary>
public interface ICustomServicesSpeedupService
{
  /// <summary>Возвращает сервис сервера приложений</summary>
  /// <param name="serviceType">Тип сервиса</param>
  /// <returns>Объект сервиса или null</returns>
  object GetCustomService(Type serviceType);
}
