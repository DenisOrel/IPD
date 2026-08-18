// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.IImportingEntityCustomChecker
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.Briefcase;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Server;

public interface IImportingEntityCustomChecker
{
  bool CheckImportingObject(
    IUserSession session,
    ImportingObject importingObject,
    BriefcaseImportProperties importProperties,
    List<Guid> briefcaseObjects);

  bool CheckImportingRelation(
    IUserSession session,
    ImportingRelation importingRelation,
    BriefcaseImportProperties importProperties,
    List<Tuple<Guid, Guid>> notImportedObjects);
}
