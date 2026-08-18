// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Import.ObjectLinkByIDRecordFormer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices.Import;

internal class ObjectLinkByIDRecordFormer(
  IUserSession session,
  IEventLogHelper eventHelper,
  Dictionary<Guid, ImportedInfo> links,
  Dictionary<Guid, long> measures,
  string path) : RecordFormer(session, eventHelper, links, measures, path)
{
  public override void SetRecordValues(
    AttributeInfo attrInfo,
    IDBAttributeType attrType,
    AttributeValue rec,
    AttributeRecord record)
  {
    if (rec.GuidValue == string.Empty || !GuidHelper.IsGuid(rec.GuidValue) || rec.Description == string.Empty || !GuidHelper.IsGuid(rec.Description))
    {
      this.eventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_1117"), (object) attrType.Name), Consts.traceAlways, string.Empty);
      record.IntegerValue = (object) null;
      record.StringValue = (object) null;
    }
    else
    {
      long idByGuid = SqlHelper.GetIDByGuid(new Guid(rec.GuidValue), (this.session as UserSession).DataManager, false);
      if (idByGuid == 0L)
      {
        IDBObject dbObject = this.session.GetObjectCollection(new Guid("cadd960d-306c-11d8-b4e9-00304f19f545")).Create(new Guid(rec.Description));
        dbObject.GUID = new Guid(rec.GuidValue);
        dbObject.Caption = rec.StringValue;
        dbObject.CommitCreation(true);
      }
      else
      {
        IDBObject objectBaseVersionById = this.session.GetObjectBaseVersionByID(idByGuid, false);
        record.IntegerValue = (object) idByGuid;
        record.StringValue = objectBaseVersionById != null ? (object) objectBaseVersionById.Caption : (object) rec.StringValue;
      }
    }
  }
}
