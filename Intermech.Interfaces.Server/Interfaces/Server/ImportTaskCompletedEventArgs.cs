// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.ImportTaskCompletedEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Server;

public class ImportTaskCompletedEventArgs : SessionableEventArgs
{
  public RemoteData Data { get; private set; }

  public List<Tuple<long, int>> ObjectIDs { get; private set; }

  public ImportTaskCompletedEventArgs(
    IUserSession session,
    RemoteData data,
    List<Tuple<long, int>> objectIDs)
    : base(session)
  {
    this.Data = data;
    this.ObjectIDs = objectIDs;
  }
}
