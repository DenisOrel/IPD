// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.RelVisInfo
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Pdm.Server;

internal struct RelVisInfo
{
  public int ObjectTypeID;
  public long ObjectID;
  public int RelationTypeID;
  public List<long> ParentIds;

  public RelVisInfo(bool empty)
  {
    this.ParentIds = new List<long>();
    this.RelationTypeID = -1;
    this.ObjectID = -1L;
    this.ObjectTypeID = -1;
  }

  public RelVisInfo(long objectID, int objectTypeID, int relationTypeID)
  {
    this.ParentIds = new List<long>();
    this.RelationTypeID = relationTypeID;
    this.ObjectID = objectID;
    this.ObjectTypeID = objectTypeID;
  }

  public override bool Equals(object obj)
  {
    RelVisInfo relVisInfo = (RelVisInfo) obj;
    return this.RelationTypeID == relVisInfo.RelationTypeID && this.ObjectID == relVisInfo.ObjectID;
  }

  public override int GetHashCode()
  {
    return this.RelationTypeID.GetHashCode() * 31 /*0x1F*/ + this.ObjectID.GetHashCode();
  }
}
