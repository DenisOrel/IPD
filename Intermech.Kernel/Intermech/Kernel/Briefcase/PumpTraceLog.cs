// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.PumpTraceLog
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;
using System;
using System.Diagnostics;


namespace Intermech.Kernel.Briefcase;

internal static class PumpTraceLog
{
  private static BooleanSwitch _traceLog = new BooleanSwitch("Pump.TraceLog", string.Empty, "0");

  public static bool Enabled => PumpTraceLog._traceLog.Enabled;

  public static void Write(string message)
  {
    (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace(message, Consts.traceAlways, string.Empty);
  }

  public static void Write(string message, Exception ex)
  {
    (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).TraceExeption(message, ex, string.Empty);
  }
}
