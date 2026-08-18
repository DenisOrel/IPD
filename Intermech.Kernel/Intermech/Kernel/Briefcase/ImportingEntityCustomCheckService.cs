// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportingEntityCustomCheckService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportingEntityCustomCheckService : 
  LongLifeObject,
  IImportingEntityCustomCheckService
{
  private List<IImportingEntityCustomChecker> _checkers;

  public ImportingEntityCustomCheckService()
  {
    this._checkers = new List<IImportingEntityCustomChecker>()
    {
      (IImportingEntityCustomChecker) new WorkCopyChecker(),
      (IImportingEntityCustomChecker) new ImportUsersChecker()
    };
  }

  public void Register(IImportingEntityCustomChecker checker) => this._checkers.Add(checker);

  public void Unregister(IImportingEntityCustomChecker checker) => this._checkers.Remove(checker);

  public bool CheckImportingObject(
    IUserSession session,
    ImportingObject importingObject,
    BriefcaseImportProperties importProperties,
    List<Guid> briefcaseObjects)
  {
    foreach (IImportingEntityCustomChecker checker in this._checkers)
    {
      if (!checker.CheckImportingObject(session, importingObject, importProperties, briefcaseObjects))
        return false;
    }
    return true;
  }

  public bool CheckImportingRelation(
    IUserSession session,
    ImportingRelation importingRelation,
    BriefcaseImportProperties importProperties,
    List<Tuple<Guid, Guid>> notImportedObjects)
  {
    foreach (IImportingEntityCustomChecker checker in this._checkers)
    {
      if (!checker.CheckImportingRelation(session, importingRelation, importProperties, notImportedObjects))
        return false;
    }
    return true;
  }
}
