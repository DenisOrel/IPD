// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Import.StringRecordFormer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;


namespace Intermech.Kernel.Services.PortalServices.Import;

internal class StringRecordFormer(
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
    string str = string.Empty;
    if (attrInfo.FieldType == FieldTypes.ftMemo)
    {
      string path = Path.Combine(this.path, rec.FileName);
      if (!File.Exists(path))
      {
        this.eventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_1118"), (object) path, (object) attrInfo.Name), Consts.traceAlways, string.Empty);
        record.StringValue = (object) null;
        return;
      }
      using (FileStream fileStream = new FileStream(path, FileMode.Open))
      {
        using (StreamReader streamReader = new StreamReader((Stream) fileStream, Encoding.UTF8))
          str = streamReader.ReadToEnd();
      }
    }
    else if (attrInfo.FieldType == FieldTypes.ftString)
    {
      str = rec.StringValue;
      if (str != string.Empty && attrType.AttributeID == this.session.IdentHelper.LoginNameID)
      {
        ISitesCacheService customService = (ISitesCacheService) this.session.GetCustomService(typeof (ISitesCacheService));
        if (str.IndexOf('\\') == 1 && (int) customService.Info.Code == (int) str[0])
          str = str.Remove(0, 2);
      }
    }
    else if (rec.StringValue != string.Empty)
      str = rec.StringValue;
    else if (rec.IntegerValue != long.MinValue)
      str = Convert.ToString(rec.IntegerValue);
    else if (rec.DoubleValue != double.MinValue)
      str = Convert.ToString(rec.DoubleValue, (IFormatProvider) CultureInfo.InvariantCulture);
    else if (rec.DateTimeValue != string.Empty)
    {
      str = rec.DateTimeValue;
    }
    else
    {
      this.eventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_1111"), (object) attrType.Name), Consts.traceAlways, string.Empty);
      record.StringValue = (object) null;
      return;
    }
    record.StringValue = (object) str;
  }
}
