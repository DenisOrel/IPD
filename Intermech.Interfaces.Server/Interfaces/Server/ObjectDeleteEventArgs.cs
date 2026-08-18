// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.ObjectDeleteEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;

#nullable disable
namespace Intermech.Interfaces.Server;

public class ObjectDeleteEventArgs : EventArgs
{
  private long _DeleteMode;
  public IUserSession Session;

  public ObjectDeleteEventArgs(long deleteMode, IUserSession session)
  {
    this._DeleteMode = deleteMode;
    this.Session = session;
  }

  public long DeleteMode => this._DeleteMode;
}
