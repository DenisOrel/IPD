// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.BeforeRecordsSelectEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Kernel.Search;
using System;

#nullable disable
namespace Intermech.Interfaces.Server;

public class BeforeRecordsSelectEventArgs : EventArgs
{
  public DBRecordSetParams OldParameters;
  public DBRecordSetParams? NewParameters;
  public IUserSession Session;

  public BeforeRecordsSelectEventArgs(DBRecordSetParams parameters, IUserSession session)
  {
    this.OldParameters = parameters;
    this.Session = session;
  }
}
