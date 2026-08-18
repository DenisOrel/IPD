// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.ExtendedTransferedObject
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;
using System;
using System.IO;

#nullable disable
namespace Intermech.Interfaces.Server;

public class ExtendedTransferedObject : TransferedObject
{
  public long[] FileSizes;

  public ExtendedTransferedObject()
  {
  }

  public ExtendedTransferedObject(ChangeType changesType, TransferedObjectCategory category)
    : base(changesType, category)
  {
  }

  public ExtendedTransferedObject(
    ChangeType changesType,
    TransferedObjectCategory category,
    TransferedObjectTag tag)
    : base(changesType, category, tag)
  {
  }

  public ExtendedTransferedObject(
    ChangeType changesType,
    TransferedObjectCategory category,
    string[] dataFiles,
    TransferedObjectTag tag)
    : base(changesType, category, dataFiles, tag)
  {
  }

  public override void Load(BinaryReader reader)
  {
    base.Load(reader);
    int length = reader.ReadInt32();
    if (length <= 0)
      return;
    this.FileSizes = new long[length];
    for (int index = 0; index < length; ++index)
      this.FileSizes[index] = reader.ReadInt64();
  }

  public override void Save(BinaryWriter writer)
  {
    base.Save(writer);
    if (this.FileSizes != null)
    {
      writer.Write(this.FileSizes.Length);
      for (int index = 0; index < this.FileSizes.Length; ++index)
        writer.Write(this.FileSizes[index]);
    }
    else
      writer.Write(0);
  }

  public TransferedObject ToTransferedObject
  {
    get
    {
      return new TransferedObject(new Guid(this.GUID), this.ChangesType, this.Category, this.DataFiles, this.Tag);
    }
  }

  public override TransferedObject Clone()
  {
    ExtendedTransferedObject transferedObject = new ExtendedTransferedObject(this.ChangesType, this.Category, this.DataFiles, this.Tag?.Clone());
    transferedObject.GUID = this.GUID;
    transferedObject.FileSizes = this.FileSizes != null ? (long[]) this.FileSizes.Clone() : (long[]) null;
    transferedObject.Completed = this.Completed;
    return (TransferedObject) transferedObject;
  }
}
