// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImportEventMessage
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System;


namespace Intermech.Kernel.Services.PortalServices;

internal abstract class ImportEventMessage
{
  protected long templateID;
  protected IUserSession session;

  public ImportEventMessage(IUserSession session, long templateID)
  {
    this.session = session;
    this.templateID = templateID;
  }

  public void CreateProcess()
  {
    IProcess process = (this.session.GetCustomService(typeof (IRouterService)) as IRouterService).CreateProcess(this.session.SessionGUID, this.templateID);
    if (process.StartActivity == null)
      throw new Exception("Start activity not found!");
    this.AddAttachments(process.StartActivity.Attachments);
    process.StartProcess();
  }

  protected abstract void AddAttachments(IAttachments attachments);
}
