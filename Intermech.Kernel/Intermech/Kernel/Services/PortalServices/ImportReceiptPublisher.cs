// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImportReceiptPublisher
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.WebPortal;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices;

internal class ImportReceiptPublisher : Publisher
{
  private readonly ImportReceipt _receipt;

  public override string PublicationInfo => $"Квитанция {this._receipt.ReceiptID}";

  public ImportReceiptPublisher(ImportReceipt receipt)
    : base(PublishType.Simple)
  {
    this._receipt = receipt;
  }

  public override ITransferedObject[] Pack(IUserSession session, IBackupWriter writer)
  {
    ISitesCacheService customService = (ISitesCacheService) session.GetCustomService(typeof (ISitesCacheService));
    List<ExtendedTransferedObject> transferedObjectList = new List<ExtendedTransferedObject>(2);
    ExtendedTransferedObject unit1 = new ExtendedTransferedObject(ChangeType.ctCreate, TransferedObjectCategory.AutoTransfer);
    new CustomXMLFileFormer<string>(session, unit1, writer, string.Empty).SaveAttributes();
    transferedObjectList.Add(unit1);
    ExtendedTransferedObject unit2 = new ExtendedTransferedObject(ChangeType.ctCreate, TransferedObjectCategory.Receipt, (TransferedObjectTag) new ObjectTag(false, false, customService.Info.Code, PublishObjectRootType.rtUnknown));
    ObjectXMLFileFormer objectXmlFileFormer = new ObjectXMLFileFormer(session, unit2, writer, session.GetObject(this._receipt.ReceiptID), new Attributes4ObjectTag(PublishObjectRootType.rtUnknown, string.Empty));
    objectXmlFileFormer.CheckAttributes = false;
    objectXmlFileFormer.SaveAttributes();
    transferedObjectList.Add(unit2);
    return (ITransferedObject[]) transferedObjectList.ToArray();
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
    IVersionRulesCacheService customService = session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
    return (ITask) new PublishTask(userID, userGuid, taskName, TaskType.Publish, priority, (List<PublishCompositionObject>) null, new ExtendedPublishOptions(PublishCompositionOptions.None, 0, (List<int>) null, (List<int>) null, customService.GetFiltrationSettings((object) session, "cad005aa-306c-11d8-b4e9-00304f19f545"), this._receipt.EnableSites, false, new char?(), new char?(), priority), units, attributeTaskFiles);
  }
}
