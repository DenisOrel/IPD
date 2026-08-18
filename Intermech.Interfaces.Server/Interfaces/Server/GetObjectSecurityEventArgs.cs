// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.GetObjectSecurityEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Server;

public class GetObjectSecurityEventArgs : EventArgs
{
  public List<IDBSecurity> SecurityList;
  public bool SetAccessMode;

  public GetObjectSecurityEventArgs(List<IDBSecurity> securityList)
  {
    this.SecurityList = securityList;
  }
}
