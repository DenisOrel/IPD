// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IMServerAppConfigurationProxy
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Клиентский прокси-сервис для серверного сервиса IMServerAppConfiguration.
/// Он получает все серверные значения опций и ключей трассировки за одно обращение к серверу приложений,
/// а затем кэширует их на все время жизни клиента IPS.
/// </summary>
/// <remarks>Реализация является thread safe.</remarks>
internal sealed class IMServerAppConfigurationProxy : IMServerAppConfiguration
{
  private readonly IMServerAppConfiguration serverService;
  private readonly object syncRoot;
  private bool isInitialized;
  private Dictionary<string, string> options;
  private Dictionary<string, TraceLevel> traceSwitches;

  /// <summary>Создает объект.</summary>
  /// <param name="serverService">Серверный сервис</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="serverService" /> содержит null</exception>
  public IMServerAppConfigurationProxy(IMServerAppConfiguration serverService)
  {
    this.serverService = serverService != null ? serverService : throw new ArgumentNullException(nameof (serverService));
    this.syncRoot = new object();
  }

  /// <summary>
  /// Возвращает значение указанной опции из app.config сервера приложений из секции appSettings.
  /// </summary>
  /// <param name="optionName">Имя опции</param>
  /// <returns>Значение опции или null, если указанная опция не найдена, либо ее чтение запрещено по соображениям безопасности</returns>
  /// <exception cref="T:ArgumentNullException">optionName</exception>
  public string GetConfigurationOption(string optionName)
  {
    if (optionName == null)
      throw new ArgumentNullException(nameof (optionName));
    lock (this.syncRoot)
    {
      this.InitializeLazily();
      string str;
      return this.options.TryGetValue(optionName, out str) ? str : (string) null;
    }
  }

  /// <summary>
  /// Возвращает значение ключа трассировки из app.config сервера приложений из секции system.diagnostics.
  /// </summary>
  /// <param name="switchName">Имя ключа трассировки</param>
  /// <returns>Значение ключа трассировки</returns>
  /// <exception cref="T:ArgumentNullException">switchName</exception>
  public TraceLevel GetTraceSwitch(string switchName)
  {
    if (switchName == null)
      throw new ArgumentNullException(nameof (switchName));
    lock (this.syncRoot)
    {
      this.InitializeLazily();
      TraceLevel traceLevel;
      return this.traceSwitches.TryGetValue(switchName, out traceLevel) ? traceLevel : TraceLevel.Off;
    }
  }

  /// <summary>
  /// Возвращает все значения опций и ключей трассировки из app.config сервера приложений.
  /// </summary>
  /// <returns>Кортеж из двух словарей: значений опций и значений ключей трассировки</returns>
  public Tuple<Dictionary<string, string>, Dictionary<string, TraceLevel>> GetAll()
  {
    lock (this.syncRoot)
    {
      this.InitializeLazily();
      return Tuple.Create<Dictionary<string, string>, Dictionary<string, TraceLevel>>(new Dictionary<string, string>((IDictionary<string, string>) this.options), new Dictionary<string, TraceLevel>((IDictionary<string, TraceLevel>) this.traceSwitches));
    }
  }

  private void InitializeLazily()
  {
    if (this.isInitialized)
      return;
    this.InitializeCore();
    this.isInitialized = true;
  }

  private void InitializeCore()
  {
    Tuple<Dictionary<string, string>, Dictionary<string, TraceLevel>> all = this.serverService.GetAll();
    this.options = all.Item1;
    this.traceSwitches = all.Item2;
  }
}
