// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.WebPortal.ValueConverters.DateTimeValueConverter
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;
using Intermech.Localization;

#nullable disable
namespace Intermech.Interfaces.Server.WebPortal.ValueConverters;

internal sealed class DateTimeValueConverter : ValueConverter
{
  public DateTimeValueConverter(IDBAttributeType attrType, AttributeValue record)
    : base(attrType, record)
  {
  }

  public DateTimeValueConverter(
    IDBAttributeType attrType,
    AttributeValue record,
    IEventLogHelper log)
    : base(attrType, record, log)
  {
  }

  public override object GetValue(IUserSession session, bool throwException)
  {
    if (this.record.StringValue == Consts.CurrentDateFunction)
      return (object) Consts.CurrentDateFunction;
    return this.record.DateTimeValue == string.Empty ? this.OnError(throwException, string.Format(LocalizationHolder.rm.GetString("Interfaces.Server_8"), (object) this.attrType.Name)) : (object) (DateTimeHelper.ToDateTime(this.record.DateTimeValue) + session.TimeZoneOffset);
  }
}
