// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.ObjectsPublishedEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Server;

public class ObjectsPublishedEventArgs
{
  public long UserID { get; private set; }

  public List<long> ObjectIDs { get; private set; }

  public List<long> Sites { get; private set; }

  public ObjectsPublishedEventArgs(long userID, List<long> objectIDs, List<long> sites)
  {
    this.UserID = userID;
    this.ObjectIDs = objectIDs;
    this.Sites = sites;
  }
}
