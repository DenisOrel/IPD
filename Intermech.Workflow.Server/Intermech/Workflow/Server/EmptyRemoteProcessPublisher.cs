// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.EmptyRemoteProcessPublisher
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Services.PortalServices;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Server;

internal class EmptyRemoteProcessPublisher : Publisher
{
  private readonly CustomPublishDataInfo _processInfo;
  private Packet4Publish _packet;

  public override string PublicationInfo => $"Процесс \"{this._processInfo.Name}\" без вложений.";

  public EmptyRemoteProcessPublisher(CustomPublishDataInfo processInfo, Packet4Publish packet)
    : base(PublishType.Simple)
  {
    this._processInfo = processInfo;
    this._packet = packet;
  }

  public override ITransferedObject[] Pack(IUserSession session, IBackupWriter writer)
  {
    return (ITransferedObject[]) ProcessXMLFileFormer.Pack(this._processInfo, session, writer);
  }

  public override ITask GetExportTask(
    IUserSession session,
    long userID,
    string taskName,
    Guid userGuid,
    TaskPriority priority,
    ITransferedObject[] units,
    IDBAttribute attributeTaskFiles)
  {
    return (ITask) new AutoTransferPublishTask(userID, userGuid, taskName, TaskType.ProcessPublish, priority, units, (List<PublishCompositionObject>) null, new ExtendedPublishOptions(this._processInfo.Options.EnableSites, new char?(), new char?(), priority), this._packet, (List<SiteCodesInfo>) null, attributeTaskFiles);
  }
}
