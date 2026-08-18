// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.CADInterfaceTracing
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Diagnostics;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Содержит ключи трассировки для классов обертки над CAD-интерфейсом.
/// </summary>
public static class CADInterfaceTracing
{
  private static readonly TraceSwitch generalSwitch = new TraceSwitch("Tools.CADInterface", string.Empty, "0");
  private static readonly TraceSwitch proxiesSwitch = new TraceSwitch("Tools.CADInterface.Proxies", string.Empty, "0");
  private static readonly IMethodCallFormatter methodCallFormatter = (IMethodCallFormatter) new ThreadBoundMethodCallFormatter((Func<IMethodCallFormatter>) (() => (IMethodCallFormatter) new MethodCallFormatter()));
  private static readonly MethodCallTracer externalCallTracer = new MethodCallTracer(CADInterfaceTracing.generalSwitch, CADInterfaceTracing.methodCallFormatter);
  private static readonly MethodCallTracer proxyCallTracer = new MethodCallTracer(CADInterfaceTracing.proxiesSwitch, CADInterfaceTracing.methodCallFormatter);

  /// <summary>
  /// Возвращает основной переключатель трассировки для обращений к CAD-интерфейсу.
  /// Значение переключателя возвращает максимальный уровень выводимых сообщений.
  /// </summary>
  public static TraceSwitch General
  {
    [DebuggerStepThrough] get => CADInterfaceTracing.generalSwitch;
  }

  /// <summary>
  /// Возвращает переключатель трассировки для обращений к proxy-объектам CAD-интерфейса.
  /// Значение переключателя возвращает максимальный уровень выводимых сообщений.
  /// </summary>
  public static TraceSwitch Proxies
  {
    [DebuggerStepThrough] get => CADInterfaceTracing.proxiesSwitch;
  }

  /// <summary>
  /// Возвращает вспомогательный объект для трассировки обращений к CAD-интерфейсу.
  /// </summary>
  public static MethodCallTracer ExternalCallTracer
  {
    [DebuggerStepThrough] get => CADInterfaceTracing.externalCallTracer;
  }

  /// <summary>
  /// Возвращает вспомогательный объект для трассировки обращений к proxy-объектам CAD-интерфейса.
  /// Этот объект использует для трассироки тех методов proxy-объектов, для которыхнет аналогов в CAD-интерфейсе.
  /// Как правило, это высокоуровневые составные методы, выполняющие несколько вызовов CAD-интерфейса.
  /// </summary>
  public static MethodCallTracer ProxyCallTracer
  {
    [DebuggerStepThrough] get => CADInterfaceTracing.proxyCallTracer;
  }
}
