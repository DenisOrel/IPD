// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.AttributeValueEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;

#nullable disable
namespace Intermech.Interfaces.Server;

public class AttributeValueEventArgs : EventArgs
{
  private object _Value;
  private object _OldValue;
  private bool _BatchMode;
  public object NewValue;
  public IUserSession Session;

  public AttributeValueEventArgs(
    object value,
    object oldValue,
    bool batchMode,
    IUserSession session)
  {
    this._Value = value;
    this._OldValue = oldValue;
    this._BatchMode = batchMode;
    this.Session = session;
  }

  public object Value => this._Value;

  public object OldValue => this._OldValue;

  public bool BatchMode => this._BatchMode;
}
