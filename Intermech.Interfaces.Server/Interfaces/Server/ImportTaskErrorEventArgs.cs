// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.ImportTaskErrorEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;

#nullable disable
namespace Intermech.Interfaces.Server;

public class ImportTaskErrorEventArgs : SessionableEventArgs
{
  public long TaskID { get; private set; }

  public Exception Exception { get; private set; }

  public ImportTaskErrorEventArgs(IUserSession session, long taskID, Exception ex)
    : base(session)
  {
    this.TaskID = taskID;
    this.Exception = ex;
  }
}
