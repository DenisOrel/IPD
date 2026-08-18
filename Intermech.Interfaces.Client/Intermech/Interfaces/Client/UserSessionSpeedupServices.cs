// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.UserSessionSpeedupServices
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Threading;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Содержит необязательные клиентские прокси-сервисы, значительно ускоряющие работу клиентских сессий
/// за счет кэширования и сокращения количества обращений к серверу приложений.
/// Реализация является thread safe.
/// </summary>
public class UserSessionSpeedupServices
{
  private AtomicRef<ICustomServicesSpeedupService> customServices;
  private AtomicRef<IDBConfigurationsSpeedupService> dbConfigurations;
  private AtomicRef<IClientMetadataCache> clientMetadataCache;

  /// <summary>Создает объект</summary>
  public UserSessionSpeedupServices()
  {
    this.customServices = new AtomicRef<ICustomServicesSpeedupService>();
    this.dbConfigurations = new AtomicRef<IDBConfigurationsSpeedupService>();
    this.clientMetadataCache = new AtomicRef<IClientMetadataCache>();
  }

  /// <summary>
  /// Возвращает или задает сервис типа <see cref="T:Intermech.Interfaces.Client.ICustomServicesSpeedupService" />
  /// Значение может быть не задано и равно null.
  /// </summary>
  public ICustomServicesSpeedupService CustomServices
  {
    [DebuggerStepThrough] get => this.customServices.Value;
    [DebuggerStepThrough] set => this.customServices.Value = value;
  }

  /// <summary>
  /// Возвращает или задает сервис типа <see cref="T:Intermech.Interfaces.IDBConfigurations" />
  /// Значение может быть не задано и равно null.
  /// </summary>
  public IDBConfigurationsSpeedupService DBConfigurations
  {
    [DebuggerStepThrough] get => this.dbConfigurations.Value;
    [DebuggerStepThrough] set => this.dbConfigurations.Value = value;
  }

  /// <summary>
  /// Возвращает или задает сервис типа <see cref="T:Intermech.Interfaces.Client.IClientMetadataCache" />
  /// Значение может быть не задано и равно null.
  /// </summary>
  public IClientMetadataCache ClientMetadataCache
  {
    [DebuggerStepThrough] get => this.clientMetadataCache.Value;
    [DebuggerStepThrough] set => this.clientMetadataCache.Value = value;
  }
}
