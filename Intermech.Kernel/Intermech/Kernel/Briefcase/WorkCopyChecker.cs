// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.WorkCopyChecker
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Briefcase;

internal sealed class WorkCopyChecker : IImportingEntityCustomChecker
{
  public bool CheckImportingObject(
    IUserSession session,
    ImportingObject importingObject,
    BriefcaseImportProperties importProperties,
    List<Guid> briefcaseObjects)
  {
    return importingObject.Object.Object_id > 0L;
  }

  public bool CheckImportingRelation(
    IUserSession session,
    ImportingRelation importingRelation,
    BriefcaseImportProperties importProperties,
    List<Tuple<Guid, Guid>> notImportedObjects)
  {
    return true;
  }
}
