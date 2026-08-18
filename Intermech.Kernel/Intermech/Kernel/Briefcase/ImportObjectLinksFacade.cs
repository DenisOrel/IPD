// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportObjectLinksFacade
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Briefcase.ImportClasses;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportObjectLinksFacade
{
  private readonly IUserSession _session;
  private readonly ObjectPropertiesLinksImporter _objectPropertiesLinksImporter;
  private readonly ObjectLinksImporter _objectLinksImporter;
  private readonly RelationLinksImporter _relationLinksImporter;
  private readonly RelationPropertiesLinksImporter _relationPropertiesLinksImporter;
  private readonly RelationLinksByIDImporter _relationLinksByIDImporter;
  private readonly ObjectLinksByIDImporter _objectLinksByIDImporter;

  public ImportObjectLinksFacade(
    UserSession session,
    List<IDСorresponds> importingObjects,
    List<long> recordKindStated,
    Action<string> addIntoLogFunc)
  {
    this._session = (IUserSession) session;
    this._objectPropertiesLinksImporter = new ObjectPropertiesLinksImporter(session, importingObjects, recordKindStated, addIntoLogFunc);
    this._objectLinksImporter = new ObjectLinksImporter(session, importingObjects, recordKindStated, addIntoLogFunc);
    this._relationPropertiesLinksImporter = new RelationPropertiesLinksImporter(session, importingObjects, recordKindStated, addIntoLogFunc);
    this._relationLinksImporter = new RelationLinksImporter(session, importingObjects, recordKindStated, addIntoLogFunc);
    this._objectLinksByIDImporter = new ObjectLinksByIDImporter(session, importingObjects, recordKindStated, addIntoLogFunc);
    this._relationLinksByIDImporter = new RelationLinksByIDImporter(session, importingObjects, recordKindStated, addIntoLogFunc);
  }

  public bool ImportLink(object link)
  {
    switch (link)
    {
      case ObjectPropertiesLinks link1:
        return this._objectPropertiesLinksImporter.Import(link1);
      case ObjectLinks link2:
        return !link2.IsID ? this._objectLinksImporter.Import(link2) : this._objectLinksByIDImporter.Import(link2);
      case RelationLinks link3:
        return !link3.IsID ? this._relationLinksImporter.Import(link3) : this._relationLinksByIDImporter.Import(link3);
      case RelationPropertiesLinks link4:
        return this._relationPropertiesLinksImporter.Import(link4);
      default:
        return true;
    }
  }
}
