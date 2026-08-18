// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.ObligatoryAttributeValueEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;

#nullable disable
namespace Intermech.Interfaces.Server;

public class ObligatoryAttributeValueEventArgs : EventArgs
{
  private object _OldValue;
  private object _NewValue;
  private IUserSession _Session;

  public ObligatoryAttributeValueEventArgs(object oldValue, object newValue, IUserSession session)
  {
    this._NewValue = newValue;
    this._OldValue = oldValue;
    this._Session = session;
  }

  public object OldValue => this._OldValue;

  public object NewValue => this._NewValue;

  public IUserSession Session => this._Session;
}
