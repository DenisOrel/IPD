// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.ImportedInfo
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;
using System;

#nullable disable
namespace Intermech.Interfaces.Server;

public class ImportedInfo
{
  public Guid Guid;
  public long Id;
  public long ObjectId;
  public TransferedObjectCategory Category;
  public SystemTypes SystemType;
  public bool IsNew;
  public long BaseVersionId;

  public bool IsLink { get; set; }

  public ImportedInfo(
    Guid guid,
    long id,
    long objectId,
    TransferedObjectCategory category,
    bool isNew,
    SystemTypes systemType)
  {
    this.Guid = guid;
    this.Id = id;
    this.ObjectId = objectId;
    this.Category = category;
    this.SystemType = systemType;
    this.IsNew = isNew;
  }

  public ImportedInfo(
    Guid guid,
    long id,
    long objectId,
    TransferedObjectCategory category,
    bool isNew)
    : this(guid, id, objectId, category, isNew, SystemTypes.IPS)
  {
  }

  public ImportedInfo(Guid guid, long id, long objectId, bool isNew)
    : this(guid, id, objectId, TransferedObjectCategory.Object, isNew)
  {
  }
}
