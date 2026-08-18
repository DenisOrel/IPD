// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.WebPortal.PersistentObject
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;
using System.IO;

#nullable disable
namespace Intermech.Interfaces.Server.WebPortal;

public sealed class PersistentObject : TransferedObject
{
  public long ObjectID { get; private set; }

  public string LinkedGuid { get; private set; }

  public bool IsLink { get; private set; }

  public PersistentObject()
  {
  }

  public PersistentObject(long objectID, string linkedGuid, bool isLink, ObjectTag tag)
  {
    this.ObjectID = objectID;
    this.LinkedGuid = linkedGuid;
    this.IsLink = isLink;
    this.Tag = (TransferedObjectTag) tag;
  }

  public override void Save(BinaryWriter writer)
  {
    this.SaveGuid(writer);
    writer.Write(this.ObjectID);
    writer.Write(this.IsLink);
    if (string.IsNullOrEmpty(this.LinkedGuid))
    {
      writer.Write(0);
    }
    else
    {
      writer.Write(this.LinkedGuid.Length);
      writer.Write(this.LinkedGuid.ToCharArray());
    }
    this.Tag.Save(writer);
  }

  public override void Load(BinaryReader reader)
  {
    this.LoadGuid(reader);
    this.ObjectID = reader.ReadInt64();
    this.IsLink = reader.ReadBoolean();
    int length = reader.ReadInt32();
    this.LinkedGuid = length == 0 ? string.Empty : TransferedObject.GetString(length, reader);
    this.Tag = (TransferedObjectTag) new ObjectTag();
    this.Tag.Load(reader);
  }
}
