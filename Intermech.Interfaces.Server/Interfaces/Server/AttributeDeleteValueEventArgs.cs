// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.AttributeDeleteValueEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;

#nullable disable
namespace Intermech.Interfaces.Server;

public class AttributeDeleteValueEventArgs : EventArgs
{
  private int _Index;
  private bool _BatchMode;
  public IUserSession Session;

  public AttributeDeleteValueEventArgs(int valueIndex, bool batchMode, IUserSession session)
  {
    this._Index = valueIndex;
    this._BatchMode = batchMode;
    this.Session = session;
  }

  public int Index => this._Index;

  public bool BatchMode => this._BatchMode;
}
