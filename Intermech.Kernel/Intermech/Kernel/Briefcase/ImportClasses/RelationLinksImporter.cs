// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportClasses.RelationLinksImporter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Briefcase.ImportClasses;

internal class RelationLinksImporter(
  UserSession session,
  List<IDСorresponds> importingObjects,
  List<long> recordKindStated,
  Action<string> addIntoLogFunc) : AttributableLinksImporter<RelationLinks>(session, importingObjects, recordKindStated, addIntoLogFunc, "F_PRJLINK_ID")
{
  protected override long GetAttributableID(RelationLinks link) => link.RelationID;

  protected override string GetAttributeTableName(RelationLinks link) => "IMS_RELATION_ATTRS";

  protected override string[] GetUpdateTables(RelationLinks link)
  {
    return this.session.DBCache.GetUpdateTables(link.AttributeID, -1, link.Type);
  }
}
