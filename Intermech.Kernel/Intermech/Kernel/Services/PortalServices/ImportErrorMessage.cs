// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImportErrorMessage
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class ImportErrorMessage : ImportEventMessage
{
  private long _taskId;

  public ImportErrorMessage(IUserSession session, long templateID, long taskID)
    : base(session, templateID)
  {
    this._taskId = taskID;
  }

  protected override void AddAttachments(IAttachments attachments) => attachments.Add(this._taskId);
}
