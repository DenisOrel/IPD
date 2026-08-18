// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Import.BooleanRecordFormer
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

internal class BooleanRecordFormer(
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
    long num = long.MinValue;
    if (rec.IntegerValue != long.MinValue)
      num = rec.IntegerValue;
    else if (rec.DoubleValue != double.MinValue && Math.Ceiling(rec.DoubleValue) - Math.Floor(rec.DoubleValue) == 0.0)
      num = Convert.ToInt64(rec.DoubleValue);
    switch (num)
    {
      case long.MinValue:
        if (rec.StringValue != string.Empty)
        {
          string upper = rec.StringValue.ToUpper();
          if (upper == LocalizationHolder.rm.GetString("Kernel_1113") || upper == "TRUE" || upper == LocalizationHolder.rm.GetString("Kernel_1114"))
          {
            num = 1L;
            goto case 0;
          }
          if (upper == LocalizationHolder.rm.GetString("Kernel_1115") || upper == "FALSE" || upper == LocalizationHolder.rm.GetString("Kernel_1116"))
          {
            num = 0L;
            goto case 0;
          }
          this.eventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_1112"), (object) num, (object) attrType.Name), Consts.traceAlways, string.Empty);
          record.IntegerValue = (object) null;
          break;
        }
        this.eventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_1111"), (object) attrType.Name), Consts.traceAlways, string.Empty);
        record.IntegerValue = (object) null;
        break;
      case 0:
      case 1:
        record.IntegerValue = (object) num;
        break;
      default:
        this.eventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_1112"), (object) num, (object) attrType.Name), Consts.traceAlways, string.Empty);
        record.IntegerValue = (object) null;
        break;
    }
  }
}
