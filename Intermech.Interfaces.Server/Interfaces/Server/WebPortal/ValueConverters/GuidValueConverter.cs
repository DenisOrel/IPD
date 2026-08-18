// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.WebPortal.ValueConverters.GuidValueConverter
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;
using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.Interfaces.Server.WebPortal.ValueConverters;

internal sealed class GuidValueConverter : ValueConverter
{
  public GuidValueConverter(IDBAttributeType attrType, AttributeValue record)
    : base(attrType, record)
  {
  }

  public GuidValueConverter(IDBAttributeType attrType, AttributeValue record, IEventLogHelper log)
    : base(attrType, record, log)
  {
  }

  public override object GetValue(IUserSession session, bool throwException)
  {
    if (GuidHelper.IsGuid(this.record.StringValue))
      return (object) new Guid(this.record.StringValue);
    if (throwException)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces.Server_8"), (object) this.attrType.Name));
    return (object) null;
  }
}
