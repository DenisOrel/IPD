// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.RelationTypeSecurity
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

internal class RelationTypeSecurity : DBSessionable, IDBSecurity
{
  private static Dictionary<ActionType, bool> metadataActions;
  internal static ConcurrentDictionary<int, bool> DontCacheAccess4Types = new ConcurrentDictionary<int, bool>();

  internal static void InitDontCacheAccess4Types(UserSession session)
  {
    DataTable dataTable = session.DataManager.ExecuteDataTable("SELECT DISTINCT F_CATEGORY_ID FROM IMS_CATEGORY_ACCESS WHERE F_CATEGORY_TYPE = :catID AND F_USER_ID = :creator_group", session.DataManager.Parameter("catID", (object) 6), session.DataManager.Parameter("creator_group", (object) session.IdentHelper.RelationCreatorGroupID));
    RelationTypeSecurity.DontCacheAccess4Types.Clear();
    for (int index = 0; index < dataTable.Rows.Count; ++index)
      RelationTypeSecurity.DontCacheAccess4Types.TryAdd(Convert.ToInt32(dataTable.Rows[index][0]), true);
  }

  static RelationTypeSecurity()
  {
    RelationTypeSecurity.metadataActions = new Dictionary<ActionType, bool>(3);
    RelationTypeSecurity.metadataActions.Add(ActionType.EditLink, true);
    RelationTypeSecurity.metadataActions.Add(ActionType.DeleteLink, true);
    RelationTypeSecurity.metadataActions.Add(ActionType.AddLink, true);
  }

  public RelationTypeSecurity(UserSession uSession, int aRelationTypeID, long creatorID)
    : base(uSession)
  {
    this.InitSecurityOptions(6, (long) aRelationTypeID);
    this.UseAccessCache = false;
    this._AccessOwnerID = creatorID;
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, RelationTypeSecurity.metadataActions);
  }

  protected override string GetExtendedAccessSQL()
  {
    return this.IsUserOwner() ? this.UserSession.IdentHelper.RelationCreatorGroupID.ToString() : string.Empty;
  }
}
