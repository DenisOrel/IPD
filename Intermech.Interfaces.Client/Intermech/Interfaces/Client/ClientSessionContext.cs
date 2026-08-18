// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ClientSessionContext
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Контекст выполнения для <see cref="T:Intermech.Interfaces.Client.ClientSession" />. Он помогает получить доступ к необходимым общим объектам и сервисам клиента без
/// необходимости напрямую обращаться к контейнеру сервисов.
/// </summary>
/// <remarks>
/// Использование данного класса позволяет явно контролировать, какие сервисы доступны в <see cref="T:Intermech.Interfaces.Client.ClientSession" />. Это важно при
/// разработке клиентов IPS на базе разных технлогий - Winform, Web и др.
/// Реализация является thread safe.
/// </remarks>
internal sealed class ClientSessionContext
{
  internal ClientSessionContext(
    IClientCache clientCache,
    ICustomServicesSpeedupService customServicesSpeedupService = null,
    IDBConfigurationsSpeedupService dbConfigurationsSpeedupService = null)
  {
    this.ClientCache = clientCache ?? throw new ArgumentNullException(nameof (clientCache));
    this.CustomServicesSpeedupService = customServicesSpeedupService;
    this.DBConfigurationsSpeedupService = dbConfigurationsSpeedupService;
  }

  /// <summary>
  /// Сервис клиентского кэша.
  /// Значение должно быть задано.
  /// </summary>
  public IClientCache ClientCache { get; }

  /// <summary>
  /// Ускоряющий клиентский сервис для получения сервисов сервера приложений.
  /// Значение может быть не задано и равно null.
  /// </summary>
  public ICustomServicesSpeedupService CustomServicesSpeedupService { get; }

  /// <summary>
  /// Ускоряющий клиентский сервис для работы с конфигурацией текущего пользователя.
  /// Значение может быть не задано и равно null.
  /// </summary>
  public IDBConfigurationsSpeedupService DBConfigurationsSpeedupService { get; }
}
