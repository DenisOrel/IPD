// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportObjectLinks
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportObjectLinks : Importer
{
  private readonly List<IDСorresponds> _importingObjects;

  public ImportObjectLinks(
    UserSession session,
    ArrayList objectLinks,
    List<IDСorresponds> importingObjects)
    : base(session, string.Empty, string.Empty)
  {
    this.ObjectLinks = objectLinks;
    this._importingObjects = importingObjects;
  }

  public bool Import()
  {
    bool flag = false;
    List<long> recordKindStated = new List<long>(this.ObjectLinks.Count);
    ImportObjectLinksFacade objectLinksFacade = new ImportObjectLinksFacade(this.session, this._importingObjects, recordKindStated, new Action<string>(((Importer) this).AddIntoLog));
    for (int index = 0; index < this.ObjectLinks.Count; ++index)
    {
      if (!objectLinksFacade.ImportLink(this.ObjectLinks[index]))
        flag = true;
    }
    foreach (IDСorresponds importingObject in this._importingObjects)
    {
      if (!recordKindStated.Contains(importingObject.HostObjectID))
      {
        QuickObjectInfo objectInfo = this.session.GetObjectInfo(importingObject.HostObjectID);
        if (objectInfo.Empty)
        {
          this.AddIntoLog(string.Format(LocalizationHolder.rm.GetString("Kernel_302"), (object) importingObject.HostObjectID));
          flag = true;
        }
        else
        {
          this.session.StartTransaction();
          try
          {
            IDbDataParameter dbDataParameter1 = this.session.DataManager.Parameter("verType", (object) 0);
            IDbDataParameter dbDataParameter2 = this.session.DataManager.Parameter("objID", (object) objectInfo.ObjectID);
            this.session.DataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_OBJECT_VER_TYPE = :verType WHERE F_OBJECT_ID = :objID", dbDataParameter1, dbDataParameter2);
            List<string> stringList = new List<string>((IEnumerable<string>) this.session.DBCache.GetUpdateTables(-1, objectInfo.ObjectTypeID, -1));
            stringList.Insert(0, "IMS_OBJECTS");
            foreach (string str in stringList)
              this.session.DataManager.ExecuteNonQuery($"UPDATE {str} SET F_OBJECT_VER_TYPE = :verType WHERE  F_OBJECT_ID = :objID", dbDataParameter1, dbDataParameter2);
            this.session.Commit();
          }
          catch (Exception ex)
          {
            this.session.Rollback();
            flag = true;
            this.AddIntoLog(string.Format(LocalizationHolder.rm.GetString("Kernel_305"), (object) importingObject.HostObjectID, (object) ex.Message));
          }
        }
      }
    }
    return !flag;
  }
}
