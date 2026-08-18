// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.WebPortal.ValueConverters.IntegerValueConverter
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;
using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.Interfaces.Server.WebPortal.ValueConverters;

internal sealed class IntegerValueConverter : ValueConverter
{
  public IntegerValueConverter(IDBAttributeType attrType, AttributeValue record)
    : base(attrType, record)
  {
  }

  public IntegerValueConverter(
    IDBAttributeType attrType,
    AttributeValue record,
    IEventLogHelper log)
    : base(attrType, record, log)
  {
  }

  public override object GetValue(IUserSession session, bool throwException)
  {
    long result = long.MinValue;
    if (this.record.IntegerValue != long.MinValue)
      result = this.record.IntegerValue;
    else if (this.record.DoubleValue != double.MinValue && Math.Ceiling(this.record.DoubleValue) - Math.Floor(this.record.DoubleValue) == 0.0)
      result = Convert.ToInt64(this.record.DoubleValue);
    else if (this.record.StringValue != string.Empty)
      long.TryParse(this.record.StringValue, out result);
    return result == long.MinValue ? this.OnError(throwException, string.Format(LocalizationHolder.rm.GetString("Interfaces.Server_8"), (object) this.attrType.Name)) : (object) result;
  }
}
