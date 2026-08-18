// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Import.BlobRecordFormer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Intermech.Kernel.Services.PortalServices.Import;

internal class BlobRecordFormer(
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
    record.Path2File = Path.Combine(this.path, rec.FileName);
    record.DateValue = (object) (rec.DateTimeValue != string.Empty ? DateTimeHelper.ToDateTime(rec.DateTimeValue) : DateTime.UtcNow);
    record.ArcMethod = (object) rec.ArcMethod;
    record.IntegerValue = (object) rec.IntegerValue;
    record.DoubleValue = (object) rec.DoubleValue;
    record.FileNote = attrInfo.FieldType == FieldTypes.ftFile ? (object) string.Empty : (object) rec.StringValue;
    record.FileSize = (object) rec.IntegerValue;
    if (attrType.AttributeType == FieldTypes.ftFile && !string.IsNullOrEmpty(rec.StringValue) && ((IEnumerable<string>) SearchAttributes.RedliningExtensions).Any<string>((Func<string, bool>) (_ => rec.StringValue.ToLower().EndsWith(_))))
    {
      record.FileType = (object) (FileTypes) (rec.FileType == FileTypes.ftNormal ? 3 : (int) rec.FileType);
      record.StringValue = (object) rec.StringValue.Remove(rec.StringValue.ToLower().IndexOf(SearchAttributes.RLFExtention));
      if (((string) record.StringValue).ToLower().EndsWith(".rxml"))
        record.AttributeId = this.session.IdentHelper.FileAttributeID;
    }
    else
    {
      record.FileType = (object) rec.FileType;
      record.StringValue = (object) rec.StringValue;
    }
    if (!(rec.FileAuthor != string.Empty))
      return;
    if (!GuidHelper.IsGuid(rec.FileAuthor))
      this.eventHelper.AddToTrace($"Не найдено корректное значение для поля FileAuthor атрибута {attrType.Name}", Consts.traceAlways, string.Empty);
    IDBObject dbObject = this.FindObject(this.session, this.links, new Guid(rec.FileAuthor));
    if (dbObject != null)
      record.FileAuthor = (object) dbObject.ObjectID;
    else
      record.FileAuthor = (object) null;
  }
}
