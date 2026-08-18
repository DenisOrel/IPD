// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportEventLog
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportEventLog
{
  private IEventLogHelper _eventHelper;

  public ImportEventLog(IEventLogHelper eventHelper, string logFileName)
  {
    this.LogFileName = logFileName;
    this._eventHelper = eventHelper;
  }

  public void AddToTrace(string eventString)
  {
    this._eventHelper.AddToTrace(eventString, Consts.traceAlways, this.LogFileName);
  }

  public string LogFileName { get; }
}
