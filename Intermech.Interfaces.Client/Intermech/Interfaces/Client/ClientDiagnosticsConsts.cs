// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ClientDiagnosticsConsts
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Константы для систем диагностики клиентских приложений IPS.
/// </summary>
public static class ClientDiagnosticsConsts
{
  private static readonly string eventLogSourceName = "Клиент IPS";

  /// <summary>
  /// Имя источника событий для общесистемных журналов событий.
  /// </summary>
  public static string EventLogSourceName
  {
    [DebuggerStepThrough] get => ClientDiagnosticsConsts.eventLogSourceName;
  }
}
