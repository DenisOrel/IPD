// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportClasses.ObjectLinksImporter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Briefcase.ImportClasses;

internal class ObjectLinksImporter(
  UserSession session,
  List<IDСorresponds> importingObjects,
  List<long> recordKindStated,
  Action<string> addIntoLogFunc) : AttributableLinksImporter<ObjectLinks>(session, importingObjects, recordKindStated, addIntoLogFunc, "F_OBJECT_ID")
{
  protected override long GetAttributableID(ObjectLinks link) => link.ObjectID;

  protected override string GetAttributeTableName(ObjectLinks link)
  {
    return (Convert.ToInt32(this.session.DBCache.GetTable("IMS_OBJECT_TYPES").Rows.Find((object) link.Type)["F_OPTIONS"]) & 16 /*0x10*/) != 0 ? $"IMV_A{link.Type}" : "IMS_OBJECT_ATTRS";
  }

  protected override string[] GetUpdateTables(ObjectLinks link)
  {
    return this.session.DBCache.GetUpdateTables(link.AttributeID, link.Type, -1);
  }

  protected override void OnAfterUpdate(params IDbDataParameter[] commandParameters)
  {
    this.session.DataManager.ExecuteNonQuery("UPDATE IMS_OBJECT_LINKS SET F_TOOBJECT_ID=:int WHERE F_OBJECT_ID=:attributableID AND F_ATTRIBUTE_ID=:attrID AND F_INLIST_ID=:list", commandParameters);
  }
}
