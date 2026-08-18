// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImportCompleteMessage
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class ImportCompleteMessage : ImportEventMessage
{
  private List<long> _objectIDs;

  public ImportCompleteMessage(IUserSession session, long templateID, List<long> objectIDs)
    : base(session, templateID)
  {
    this._objectIDs = objectIDs;
  }

  protected override void AddAttachments(IAttachments attachments)
  {
    foreach (long objectId in this._objectIDs)
      attachments.Add(objectId);
  }
}
