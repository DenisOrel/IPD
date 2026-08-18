// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.CustomDataPublisher`1
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices;

internal class CustomDataPublisher<T> : Publisher
{
  protected T data;
  protected string enableSites;
  private readonly TransferedObjectCategory _category;

  public override string PublicationInfo => string.Empty;

  public CustomDataPublisher(
    T data,
    TransferedObjectCategory category,
    string enableSites,
    PublishType publishType)
    : base(publishType)
  {
    this.data = data;
    this.enableSites = enableSites;
    this._category = category;
  }

  public override ITransferedObject[] Pack(IUserSession session, IBackupWriter writer)
  {
    ExtendedTransferedObject unit = new ExtendedTransferedObject(ChangeType.ctCreate, TransferedObjectCategory.AutoTransfer);
    this.GetXMLFileFormer(session, unit, writer).SaveAttributes();
    return new ITransferedObject[1]
    {
      (ITransferedObject) unit
    };
  }

  protected virtual CustomXMLFileFormer<T> GetXMLFileFormer(
    IUserSession session,
    ExtendedTransferedObject unit,
    IBackupWriter writer)
  {
    return new CustomXMLFileFormer<T>(session, unit, writer, this.data);
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
    return (ITask) new PublishTask(userID, userGuid, taskName, TaskType.Publish, priority, (List<PublishCompositionObject>) null, new ExtendedPublishOptions(PublishCompositionOptions.None, 0, (List<int>) null, (List<int>) null, (FiltrationSettings) null, this.enableSites, false, new char?(), new char?(), priority), units, attributeTaskFiles);
  }
}
