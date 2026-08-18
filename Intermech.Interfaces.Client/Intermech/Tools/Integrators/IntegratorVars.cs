// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IntegratorVars
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.ControlFlow;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Содержит динамические переменные, влияющие на работу интеграторов.
/// </summary>
public static class IntegratorVars
{
  /// <summary>
  /// Переключатель, позволяющий активировать режим сохранения ресурсов интегрируемого приложения.
  /// </summary>
  public static readonly DynamicVariable<bool> ConserveAppResources = new DynamicVariable<bool>("IntegratorVars.ConserveAppResources", true);
  /// <summary>
  /// Переключатель, позволяющий открыть "легкую" сессию подключения к API интегрируемого приложения.
  /// При открытии такой сессии интегратор не выполняет никакой настройки приложения для работы в паре с IPS.
  /// По умолчанию значение переключателя установлено в false.
  /// </summary>
  public static readonly DynamicVariable<bool> NakedApiSessions = new DynamicVariable<bool>("IntegratorVars.NakedApiSessions", false);
}
