// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.TypedImportedInfo
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;
using System;

#nullable disable
namespace Intermech.Interfaces.Server;

public class TypedImportedInfo : ImportedInfo
{
  public int ObjectType;

  public TypedImportedInfo(
    Guid guid,
    long id,
    long objectId,
    TransferedObjectCategory category,
    bool isNew,
    SystemTypes systemType,
    int objectType)
    : base(guid, id, objectId, category, isNew)
  {
    this.ObjectType = objectType;
  }

  public TypedImportedInfo(
    Guid guid,
    long id,
    long objectId,
    TransferedObjectCategory category,
    bool isNew,
    int objectType)
    : this(guid, id, objectId, category, isNew, SystemTypes.IPS, objectType)
  {
  }
}
