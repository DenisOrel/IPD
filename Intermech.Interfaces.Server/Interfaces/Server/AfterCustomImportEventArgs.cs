// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.AfterCustomImportEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Server;

public class AfterCustomImportEventArgs
{
  public IUserSession Session;
  public List<long> ProcessedObjects;
  public Exception Error;

  public AfterCustomImportEventArgs(
    IUserSession session,
    List<long> processedObjects,
    Exception error)
  {
    this.Session = session;
    this.ProcessedObjects = processedObjects;
    this.Error = error;
  }
}
