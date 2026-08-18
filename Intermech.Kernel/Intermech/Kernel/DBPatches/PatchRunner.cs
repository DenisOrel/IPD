// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBPatches.PatchRunner
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.DBPatches;
using Intermech.Interfaces.Server;
using System;


namespace Intermech.Kernel.DBPatches;

public sealed class PatchRunner : AbstractPatchRunner
{
  private IOutputView outputView;
  private IEventLogHelper eventLog;

  public PatchRunner(IOutputView outputView = null, IEventLogHelper eventLog = null)
  {
    this.outputView = outputView;
    this.eventLog = eventLog;
  }

  protected override void LogPatchException(
    AbstractPatch patch,
    Exception exception,
    string errorMessage,
    string errorType,
    string errorStackTrace)
  {
    if (this.outputView != null)
    {
      this.outputView.WriteString("Ошибки", errorMessage);
      this.outputView.WriteString("Ошибки", errorType);
      this.outputView.WriteString("Ошибки", errorStackTrace);
    }
    if (this.eventLog == null)
      return;
    this.eventLog.AddToTrace(errorMessage, Consts.traceError, (string) null);
    this.eventLog.AddToTrace(errorType, Consts.traceError, (string) null);
    this.eventLog.AddToTrace(errorStackTrace, Consts.traceError, (string) null);
  }
}
