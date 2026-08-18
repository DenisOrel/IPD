// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportBriefcaseBase
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Briefcase;
using System;


namespace Intermech.Kernel.Briefcase;

internal abstract class ImportBriefcaseBase
{
  protected ImportEventLog eventLog;
  protected UserSession session;
  protected string briefcasePath;
  protected Guid briefcase;
  protected SetImportProgressEventHandler setImportProgressEvent;

  public ImportBriefcaseBase(
    UserSession session,
    ImportEventLog eventLog,
    SetImportProgressEventHandler setImportProgressEvent)
    : this(session, eventLog, setImportProgressEvent, Guid.Empty, (string) null)
  {
  }

  public ImportBriefcaseBase(
    UserSession session,
    ImportEventLog eventLog,
    SetImportProgressEventHandler setImportProgressEvent,
    Guid briefcase,
    string briefcasePath)
  {
    this.session = session;
    this.eventLog = eventLog;
    this.setImportProgressEvent = setImportProgressEvent;
    this.briefcase = briefcase;
    this.briefcasePath = briefcasePath;
  }

  protected void SetImportProgress(Guid briefcase, BriefcaseImportProgress importProgress)
  {
    this.setImportProgressEvent((object) this, new SetImportProgressEventArgs(briefcase, importProgress));
  }
}
