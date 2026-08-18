// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportClasses.LinksImporter`1
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Briefcase.ImportClasses;

internal abstract class LinksImporter<TLink>
{
  protected Action<string> addIntoLogFunc;
  protected UserSession session;
  protected List<IDСorresponds> importingObjects;
  protected List<long> recordKindStated;

  public LinksImporter(
    UserSession session,
    List<IDСorresponds> importingObjects,
    List<long> recordKindStated,
    Action<string> addIntoLogFunc)
  {
    this.session = session;
    this.importingObjects = importingObjects;
    this.recordKindStated = recordKindStated;
    this.addIntoLogFunc = addIntoLogFunc;
  }

  public abstract bool Import(TLink link);
}
