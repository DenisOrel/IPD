// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.WebPortal.ValueConverters.BooleanValueConverter
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;
using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.Interfaces.Server.WebPortal.ValueConverters;

internal sealed class BooleanValueConverter : ValueConverter
{
  public BooleanValueConverter(IDBAttributeType attrType, AttributeValue record)
    : base(attrType, record)
  {
  }

  public BooleanValueConverter(
    IDBAttributeType attrType,
    AttributeValue record,
    IEventLogHelper log)
    : base(attrType, record, log)
  {
  }

  public override object GetValue(IUserSession session, bool throwException)
  {
    long num1 = long.MinValue;
    if (this.record.IntegerValue != long.MinValue)
      num1 = this.record.IntegerValue;
    else if (this.record.DoubleValue != double.MinValue && Math.Ceiling(this.record.DoubleValue) - Math.Floor(this.record.DoubleValue) == 0.0)
      num1 = Convert.ToInt64(this.record.DoubleValue);
    switch (num1)
    {
      case long.MinValue:
        if (!(this.record.StringValue != string.Empty))
          return this.OnError(throwException, string.Format(LocalizationHolder.rm.GetString("Interfaces.Server_8"), (object) this.attrType.Name));
        string upper = this.record.StringValue.ToUpper();
        long num2;
        if (upper == LocalizationHolder.rm.GetString("Interfaces.Server_10") || upper == "TRUE" || upper == LocalizationHolder.rm.GetString("Interfaces.Server_11"))
        {
          num2 = 1L;
        }
        else
        {
          if (!(upper == LocalizationHolder.rm.GetString("Interfaces.Server_12")) && !(upper == "FALSE") && !(upper == LocalizationHolder.rm.GetString("Interfaces.Server_13")))
            return this.OnError(throwException, string.Format(LocalizationHolder.rm.GetString("Interfaces.Server_9"), (object) num1, (object) this.attrType.Name));
          num2 = 0L;
        }
        return (object) num2;
      case 0:
      case 1:
        return (object) num1;
      default:
        return this.OnError(throwException, string.Format(LocalizationHolder.rm.GetString("Interfaces.Server_9"), (object) num1, (object) this.attrType.Name));
    }
  }
}
