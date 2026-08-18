// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Import.RecordFormer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices.Import;

internal abstract class RecordFormer : IRecordFormer
{
  protected IUserSession session;
  protected IEventLogHelper eventHelper;
  protected Dictionary<Guid, ImportedInfo> links;
  protected Dictionary<Guid, long> measures;
  protected string path;

  public RecordFormer(
    IUserSession session,
    IEventLogHelper eventHelper,
    Dictionary<Guid, ImportedInfo> links,
    Dictionary<Guid, long> measures,
    string path)
  {
    this.session = session;
    this.eventHelper = eventHelper;
    this.links = links;
    this.measures = measures;
    this.path = path;
  }

  public abstract void SetRecordValues(
    AttributeInfo attrInfo,
    IDBAttributeType attrType,
    AttributeValue rec,
    AttributeRecord record);

  protected IDBObject FindObject(
    IUserSession session,
    Dictionary<Guid, ImportedInfo> links,
    Guid objectGuid)
  {
    ImportedInfo importedInfo = (ImportedInfo) null;
    if (links != null && !links.TryGetValue(objectGuid, out importedInfo) || links == null)
      return session.GetObject(objectGuid, false);
    return importedInfo != null ? session.GetObject(importedInfo.ObjectId) : (IDBObject) null;
  }
}
