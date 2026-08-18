// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Import.IntegerRecordFormer
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

internal class IntegerRecordFormer(
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
    long result = long.MinValue;
    if (rec.IntegerValue != long.MinValue)
      result = rec.IntegerValue;
    else if (rec.DoubleValue != double.MinValue && Math.Ceiling(rec.DoubleValue) - Math.Floor(rec.DoubleValue) == 0.0)
      result = Convert.ToInt64(rec.DoubleValue);
    else if (rec.StringValue != string.Empty)
      long.TryParse(rec.StringValue, out result);
    if (result == long.MinValue)
    {
      this.eventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_1111"), (object) attrType.Name), Consts.traceAlways, string.Empty);
      record.IntegerValue = (object) null;
    }
    else
      record.IntegerValue = (object) result;
  }
}
