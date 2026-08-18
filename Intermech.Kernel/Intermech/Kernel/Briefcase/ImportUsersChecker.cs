// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportUsersChecker
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportUsersChecker : IImportingEntityCustomChecker
{
  private int _userTypeID;

  public ImportUsersChecker()
  {
    this._userTypeID = MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545");
  }

  public bool CheckImportingObject(
    IUserSession session,
    ImportingObject importingObject,
    BriefcaseImportProperties importProperties,
    List<Guid> briefcaseObjects)
  {
    if (importProperties.NewUser == 0L)
      return true;
    if (importingObject.Object.ObjectType == session.IdentHelper.UsersTypeID && !briefcaseObjects.Contains((Guid) importingObject.Object.ObjectGuid))
      return false;
    importingObject.Object.OwnerId = -1L * importProperties.NewUser;
    importingObject.Object.CreatorID = -1L * importProperties.NewUser;
    return true;
  }

  public bool CheckImportingRelation(
    IUserSession session,
    ImportingRelation importingRelation,
    BriefcaseImportProperties importProperties,
    List<Tuple<Guid, Guid>> notImportedObjects)
  {
    if (importProperties.NewUser == 0L)
      return true;
    if (notImportedObjects.Exists((Predicate<Tuple<Guid, Guid>>) (x => x.Item2 == (Guid) importingRelation.Relation.PartId)))
      return false;
    importingRelation.Relation.CreatorID = -1L * importProperties.NewUser;
    return true;
  }
}
