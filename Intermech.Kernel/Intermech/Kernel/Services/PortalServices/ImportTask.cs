// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImportTask
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.IO;


namespace Intermech.Kernel.Services.PortalServices;

internal class ImportTask : Task
{
  protected string updateGuid;

  public ImportTask()
  {
  }

  public ImportTask(long taskID) => this.TaskID = taskID;

  public ImportTask(long userID, Guid userGuid, string name, string updateGuid)
    : this(userID, userGuid, name, TaskType.ImportObjects, TaskPriority.Normal, updateGuid, (TransferedObject[]) null)
  {
  }

  internal string UpdateGuid => this.updateGuid;

  public ImportTask(
    long userID,
    Guid userGuid,
    string name,
    TaskType taskType,
    TaskPriority priority,
    string updateGuid,
    TransferedObject[] units)
    : base(userID, userGuid, name, taskType, priority, (ITransferedObject[]) units)
  {
    this.updateGuid = updateGuid;
  }

  public override byte[] Save(IUserSession session, IDBObject backupObject)
  {
    this.SaveUpdateGuid(backupObject);
    return new byte[0];
  }

  public override void Load(IUserSession session, IDBObject backupObject, byte[] bytes)
  {
    if (backupObject == null)
      return;
    this.LoadUpdateGuid(backupObject);
  }

  protected void SaveUpdateGuid(IDBObject backupObject)
  {
    IDBAttribute attributeByGuid = backupObject.GetAttributeByGuid(PortalConsts.attributeUpdateGuid);
    if (attributeByGuid == null)
    {
      backupObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(PortalConsts.attributeUpdateGuid), false, new object[1]
      {
        (object) this.updateGuid
      });
    }
    else
    {
      if (!(attributeByGuid.AsString != this.updateGuid))
        return;
      attributeByGuid.AsString = this.updateGuid;
    }
  }

  protected void LoadUpdateGuid(IDBObject backupObject)
  {
    this.updateGuid = (backupObject.GetAttributeByGuid(PortalConsts.attributeUpdateGuid) ?? throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1052"), (object) backupObject.ObjectGUID))).AsString;
  }

  public override void LoadTransferedObjects(BinaryReader reader)
  {
    List<TransferedObject> transferedObjectList = new List<TransferedObject>();
    while (reader.BaseStream.Position < reader.BaseStream.Length)
      transferedObjectList.Add(TransferedObjectHelper.LoadFor(reader, false));
    this.Units = (ITransferedObject[]) transferedObjectList.ToArray();
  }

  protected override ITransferSettingsService GetSettingsService()
  {
    return (ITransferSettingsService) ServiceUtils.GetService<IImportRulesService>((object) ServerServices.ServiceContainer, true);
  }
}
