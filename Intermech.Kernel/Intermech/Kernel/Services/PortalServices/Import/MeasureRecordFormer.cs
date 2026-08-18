// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Import.MeasureRecordFormer
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

internal class MeasureRecordFormer(
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
    if (MeasureHelper.Measures == null || MeasureHelper.Measures.Length == 0)
      MeasureHelper.Init(this.session.GetMeasuresList());
    MeasureDescriptor measureDescriptor = (MeasureDescriptor) null;
    bool flag = false;
    if (rec.GuidValue != string.Empty && GuidHelper.IsGuid(rec.GuidValue))
    {
      long measureID = 0;
      Guid guid = new Guid(rec.GuidValue);
      if (!this.measures.TryGetValue(guid, out measureID))
      {
        IDBObject dbObject = this.session.GetObject(guid, false);
        if (dbObject != null)
        {
          measureID = dbObject.ObjectID;
          this.measures.Add(guid, dbObject.ObjectID);
        }
      }
      if (measureID != 0L)
        measureDescriptor = MeasureHelper.FindDescriptor(measureID);
    }
    double doubleValue = rec.DoubleValue;
    if (rec.StringValue != string.Empty)
    {
      MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(rec.StringValue, false);
      if (measuredValue != null && measuredValue.MeasureID != 0L && measuredValue.MeasureID != -1L)
      {
        MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(measuredValue.MeasureID);
        if (descriptor != null && !descriptor.Empty)
        {
          if (measureDescriptor == null || measureDescriptor.Empty)
          {
            measureDescriptor = descriptor;
            flag = true;
          }
          else if (measureDescriptor.PhysicalQuantityID == descriptor.PhysicalQuantityID)
            flag = true;
        }
        if (rec.DoubleValue == double.MinValue)
          doubleValue = MeasureHelper.ConvertToBaseMeasure(measuredValue).Value;
      }
    }
    if (measureDescriptor == null || measureDescriptor.Empty)
    {
      this.eventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_1111"), (object) attrType.Name), Consts.traceAlways, string.Empty);
      record.IntegerValue = (object) null;
      record.DoubleValue = (object) null;
      record.StringValue = (object) null;
    }
    else
    {
      MeasuredValue measuredValue = new MeasuredValue(doubleValue, measureDescriptor.MeasureID);
      measuredValue.Caption = !flag ? MeasureHelper.ConvertToString(measuredValue.Value, measuredValue.MeasureID, false) : rec.StringValue;
      record.IntegerValue = (object) measuredValue.MeasureID;
      record.DoubleValue = (object) measuredValue.Value;
      record.StringValue = (object) measuredValue.Caption;
    }
  }
}
