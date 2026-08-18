// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.LinkedObject
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;

#nullable disable
namespace Intermech.Interfaces.Server;

[Serializable]
public class LinkedObject : IComparable<LinkedObject>
{
  public long ObjectID { get; set; }

  public long RelationID { get; set; }

  public LinkedObject(long objectID)
    : this(objectID, 0L)
  {
  }

  public LinkedObject(long objectID, long relationID)
  {
    this.ObjectID = objectID;
    this.RelationID = relationID;
  }

  public int CompareTo(LinkedObject other)
  {
    return !this.ObjectID.Equals(other.ObjectID) || !this.RelationID.Equals(other.RelationID) ? 1 : 0;
  }
}
