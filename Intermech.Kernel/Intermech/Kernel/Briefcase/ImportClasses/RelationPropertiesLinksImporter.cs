// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportClasses.RelationPropertiesLinksImporter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Briefcase.ImportClasses;

internal sealed class RelationPropertiesLinksImporter(
  UserSession session,
  List<IDСorresponds> importingObjects,
  List<long> recordKindStated,
  Action<string> addIntoLogFunc) : LinksImporter<RelationPropertiesLinks>(session, importingObjects, recordKindStated, addIntoLogFunc)
{
  public override bool Import(RelationPropertiesLinks link)
  {
    IDСorresponds idСorresponds = link.OldCreatorID != 0L ? this.importingObjects.Find((Predicate<IDСorresponds>) (x => x.SourceObjectID == link.OldCreatorID)) : (IDСorresponds) null;
    if (link.OldCreatorID != 0L && idСorresponds == null)
    {
      this.addIntoLogFunc($"Объект {link.OldCreatorID} не был импортирован. Обновление ссылки на создателя прервано.");
      link.OldCreatorID = 0L;
      return false;
    }
    this.session.StartTransaction();
    try
    {
      IDbDataParameter dbDataParameter1 = this.session.DataManager.Parameter("creator", (object) (idСorresponds != null ? idСorresponds.HostObjectID : 0L));
      IDbDataParameter dbDataParameter2 = this.session.DataManager.Parameter("prjLinkID", (object) link.PrjLinkID);
      List<string> stringList = new List<string>();
      string[] updateTables = this.session.DBCache.GetUpdateTables(-1, -1, link.RelationType);
      if (updateTables != null && updateTables.Length != 0)
        stringList.AddRange((IEnumerable<string>) updateTables);
      stringList.Insert(0, "IMS_RELATIONS");
      foreach (string str in stringList)
        this.session.DataManager.ExecuteNonQuery($"UPDATE {str} SET F_REL_CREATOR = :creator WHERE F_PRJLINK_ID = :prjLinkID", dbDataParameter1, dbDataParameter2);
      this.session.Commit();
      return true;
    }
    catch (Exception ex)
    {
      this.session.Rollback();
      this.addIntoLogFunc($"Ошибка при обновлении ссылки на создатель связи {link.PrjLinkID}: {ex.Message}");
      return false;
    }
  }
}
