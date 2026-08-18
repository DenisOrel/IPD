// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.RemoteProcessWithAttachmentsPublisher
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

internal sealed class RemoteProcessWithAttachmentsPublisher : AutoTransferPublisher
{
  private readonly long _localProcessID;
  private readonly long _localActivityID;

  public RemoteProcessWithAttachmentsPublisher(
    CustomPublishDataInfo processInfo,
    PublishComposition composition,
    Packet4Publish packet,
    bool createReceipt,
    long localProcessID,
    long localActivityID)
    : base(processInfo, composition, processInfo.Options, packet, createReceipt)
  {
    this._localProcessID = localProcessID;
    this._localActivityID = localActivityID;
  }

  protected override void BeforeCompositionPack(
    IUserSession session,
    SiteInfo info,
    IBackupWriter writer,
    List<ITransferedObject> transObjs)
  {
    base.BeforeCompositionPack(session, info, writer, transObjs);
    transObjs.AddRange((IEnumerable<ITransferedObject>) ProcessXMLFileFormer.Pack(this.info, session, writer));
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
    return (ITask) new AutoTransferPublishTask(userID, userGuid, taskName, TaskType.ProcessPublish, priority, units, this.composition.Objects, new ExtendedPublishOptions(this.options.EnableSites, this.options.OwnerSite, this.options.CompositionOwnerSite, priority), this.Packet, this.recordedCodes, attributeTaskFiles);
  }

  protected override void CreateAdditionalReceiptAttributes(
    IUserSession session,
    SiteInfo info,
    IDBObject receipt)
  {
    DBReceiptCreator.SetReceiptAttribute(receipt, PortalConsts.attributeActionID, (object) this._localActivityID);
    DBReceiptCreator.SetReceiptAttribute(receipt, PortalConsts.attributeProcessID, (object) this._localProcessID);
  }
}
