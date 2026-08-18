// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.AttributeValuesEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;

#nullable disable
namespace Intermech.Interfaces.Server;

public class AttributeValuesEventArgs : EventArgs
{
  private object[] _Values;
  private object[] _OldValues;
  public IUserSession Session;

  public AttributeValuesEventArgs(object[] values, object[] oldValues, IUserSession session)
  {
    this._Values = values;
    this._OldValues = oldValues;
    this.Session = session;
  }

  public object Values => (object) this._Values;

  public object OldValues => (object) this._OldValues;
}
