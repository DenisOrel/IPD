// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.WebPortal.PersistentRelation
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;
using System;
using System.IO;

#nullable disable
namespace Intermech.Interfaces.Server.WebPortal;

public sealed class PersistentRelation : TransferedObject
{
  public long ParentObjectID { get; private set; }

  public Guid RelationGuid { get; private set; }

  public PersistentRelation()
  {
  }

  public PersistentRelation(long objectID, Guid relationGuid)
  {
    this.ParentObjectID = objectID;
    this.RelationGuid = relationGuid;
  }

  public override void Save(BinaryWriter writer)
  {
    this.SaveGuid(writer);
    writer.Write(this.ParentObjectID);
    this.RelationGuid.ToString();
    writer.Write(this.RelationGuid.ToString().Length);
    writer.Write(this.RelationGuid.ToString().ToCharArray());
  }

  public override void Load(BinaryReader reader)
  {
    this.LoadGuid(reader);
    this.ParentObjectID = reader.ReadInt64();
    int length = reader.ReadInt32();
    this.GUID = length > 0 ? TransferedObject.GetString(length, reader) : Guid.NewGuid().ToString();
  }
}
