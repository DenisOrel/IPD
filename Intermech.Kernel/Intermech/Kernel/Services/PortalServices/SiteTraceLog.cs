// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.SiteTraceLog
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;
using System;
using System.Diagnostics;


namespace Intermech.Kernel.Services.PortalServices;

internal static class SiteTraceLog
{
  private static BooleanSwitch _traceLog = new BooleanSwitch("Site.TraceLog", string.Empty, "0");

  public static bool Enabled => SiteTraceLog._traceLog.Enabled;

  public static void Write(string message)
  {
    (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace(message, Consts.traceAlways, string.Empty);
  }

  public static void Write(string message, Exception ex)
  {
    (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).TraceExeption(message, ex, string.Empty);
  }
}
