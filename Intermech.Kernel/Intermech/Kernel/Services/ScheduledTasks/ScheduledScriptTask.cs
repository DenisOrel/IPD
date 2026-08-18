// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ScheduledTasks.ScheduledScriptTask
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.CustomServices;
using Intermech.Localization;
using System;


namespace Intermech.Kernel.Services.ScheduledTasks;

internal class ScheduledScriptTask : DBCustomManualScheduledService
{
  public ScheduledScriptTask(ScheduledScriptInfo scriptInfo)
  {
    this.ScriptInfo = scriptInfo ?? throw new ArgumentNullException(nameof (scriptInfo));
  }

  public override Guid GUID => this.ScriptInfo.ScriptGuid;

  public override string ServiceName
  {
    get
    {
      return string.Format(LocalizationHolder.rm.GetString("Kernel_1164"), (object) this.ScriptInfo.ScriptName);
    }
  }

  public override bool ProcessEvent(TimedEventProperties properties)
  {
    return new ScheduledScriptExecutor(this.ScriptInfo).Execute((IUserSession) this.Session);
  }

  public ScheduledScriptInfo ScriptInfo { get; }
}
