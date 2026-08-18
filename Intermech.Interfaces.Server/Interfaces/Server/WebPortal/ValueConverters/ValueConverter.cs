// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.WebPortal.ValueConverters.ValueConverter
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;
using System;

#nullable disable
namespace Intermech.Interfaces.Server.WebPortal.ValueConverters;

internal abstract class ValueConverter : IValueConverter
{
  protected IDBAttributeType attrType;
  protected AttributeValue record;
  private IEventLogHelper log;

  public ValueConverter(IDBAttributeType attrType, AttributeValue record)
    : this(attrType, record, (IEventLogHelper) null)
  {
  }

  public ValueConverter(IDBAttributeType attrType, AttributeValue record, IEventLogHelper log)
  {
    this.attrType = attrType;
    this.record = record;
    this.log = log;
  }

  public abstract object GetValue(IUserSession session, bool throwException);

  protected object OnError(bool throwException, string message)
  {
    if (throwException)
      throw new Exception(message);
    if (this.log != null)
      this.log.AddToTrace(message);
    return (object) null;
  }
}
