// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.WebPortal.ValueConverters.MeasuredValueConverter
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;
using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.Interfaces.Server.WebPortal.ValueConverters;

internal sealed class MeasuredValueConverter : ValueConverter
{
  public MeasuredValueConverter(IDBAttributeType attrType, AttributeValue record)
    : base(attrType, record)
  {
  }

  public MeasuredValueConverter(
    IDBAttributeType attrType,
    AttributeValue record,
    IEventLogHelper log)
    : base(attrType, record, log)
  {
  }

  public override object GetValue(IUserSession session, bool throwException)
  {
    if (MeasureHelper.Measures == null || MeasureHelper.Measures.Length == 0)
      MeasureHelper.Init(session.GetMeasuresList());
    MeasureDescriptor measureDescriptor = (MeasureDescriptor) null;
    bool flag = false;
    if (this.record.GuidValue != string.Empty)
    {
      IDBObject dbObject = session.GetObject(new Guid(this.record.GuidValue), false);
      if (dbObject != null)
        measureDescriptor = MeasureHelper.FindDescriptor(dbObject.ObjectID);
    }
    if (measureDescriptor == null && this.record.StringValue != string.Empty)
    {
      measureDescriptor = MeasureHelper.FindDescriptor(this.record.StringValue);
      if (measureDescriptor != null && !measureDescriptor.Empty)
        flag = true;
    }
    if (measureDescriptor == null || measureDescriptor.Empty || this.record.DoubleValue == double.MinValue)
      return this.OnError(throwException, string.Format(LocalizationHolder.rm.GetString("Interfaces.Server_8"), (object) this.attrType.Name));
    MeasuredValue measuredValue = new MeasuredValue(this.record.DoubleValue, measureDescriptor.MeasureID);
    if (flag)
    {
      measuredValue.Caption = this.record.StringValue;
    }
    else
    {
      MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(this.record.StringValue);
      measuredValue.Caption = descriptor == null || descriptor.Empty ? MeasureHelper.ConvertToString(measuredValue.Value, measuredValue.MeasureID, false) : this.record.StringValue;
    }
    return (object) measuredValue;
  }
}
