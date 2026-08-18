// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportClasses.ObjectPropertiesLinksImporter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Briefcase.ImportClasses;

internal sealed class ObjectPropertiesLinksImporter(
  UserSession session,
  List<IDСorresponds> importingObjects,
  List<long> recordKindStated,
  Action<string> addIntoLogFunc) : LinksImporter<ObjectPropertiesLinks>(session, importingObjects, recordKindStated, addIntoLogFunc)
{
  public override bool Import(ObjectPropertiesLinks link)
  {
    IDСorresponds idСorresponds1 = link.OldOwnerID != 0L ? this.importingObjects.Find((Predicate<IDСorresponds>) (x => x.SourceObjectID == link.OldOwnerID)) : (IDСorresponds) null;
    IDСorresponds idСorresponds2 = link.OldProjectID != 0L ? this.importingObjects.Find((Predicate<IDСorresponds>) (x => x.SourceObjectID == link.OldProjectID)) : (IDСorresponds) null;
    IDСorresponds idСorresponds3 = link.OldCreatorID != 0L ? this.importingObjects.Find((Predicate<IDСorresponds>) (x => x.SourceObjectID == link.OldCreatorID)) : (IDСorresponds) null;
    if (idСorresponds1 == null)
    {
      this.addIntoLogFunc(string.Format(LocalizationHolder.rm.GetString("Kernel_293"), (object) link.OldOwnerID));
      link.OldOwnerID = 0L;
      return false;
    }
    if (link.OldProjectID != 0L && idСorresponds2 == null)
    {
      this.addIntoLogFunc(string.Format(LocalizationHolder.rm.GetString("Kernel_294"), (object) link.OldProjectID));
      link.OldProjectID = 0L;
      return false;
    }
    if (link.OldCreatorID != 0L && idСorresponds3 == null)
    {
      this.addIntoLogFunc($"Объект {link.OldCreatorID} не был импортирован. Обновление ссылки на создателя прервано.");
      link.OldCreatorID = 0L;
      return false;
    }
    this.session.StartTransaction();
    try
    {
      IDbDataParameter dbDataParameter1 = this.session.DataManager.Parameter("verType", (object) 0);
      IDbDataParameter dbDataParameter2 = this.session.DataManager.Parameter("owner", (object) (idСorresponds1 != null ? idСorresponds1.HostObjectID : this.session.UserID));
      IDbDataParameter dbDataParameter3 = this.session.DataManager.Parameter("project", (object) (idСorresponds2 != null ? idСorresponds2.HostObjectID : 0L));
      IDbDataParameter dbDataParameter4 = this.session.DataManager.Parameter("creator", (object) (idСorresponds3 != null ? idСorresponds3.HostObjectID : 0L));
      IDbDataParameter dbDataParameter5 = this.session.DataManager.Parameter("objID", (object) link.ObjectID);
      List<string> stringList = new List<string>((IEnumerable<string>) this.session.DBCache.GetUpdateTables(-1, link.ObjectType, -1));
      stringList.Insert(0, "IMS_OBJECTS");
      foreach (string str in stringList)
        this.session.DataManager.ExecuteNonQuery($"UPDATE {str} SET F_OBJECT_VER_TYPE = :verType, F_OWNER_ID = :owner, F_PROJECT_ID = :project, F_CREATOR_ID = :creator WHERE  F_OBJECT_ID = :objID", dbDataParameter1, dbDataParameter2, dbDataParameter3, dbDataParameter4, dbDataParameter5);
      this.session.Commit();
      this.recordKindStated.Add(link.ObjectID);
      return true;
    }
    catch (Exception ex)
    {
      this.session.Rollback();
      this.addIntoLogFunc(string.Format(LocalizationHolder.rm.GetString("Kernel_297"), (object) link.ObjectID, (object) ex.Message));
      return false;
    }
  }
}
