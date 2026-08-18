// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBUsersGroupObject
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Data;


namespace Intermech.Kernel;

public class DBUsersGroupObject(UserSession uSession, DataTable objectParams) : DBObject(uSession, objectParams)
{
  public override void DoAfterCreateRelation(IDBRelation newrelation)
  {
    long num = newrelation.PartObjectID;
    if (num == 0L)
      num = SqlHelper.GetObjectIDByID(newrelation.PartID, this.UserSession.DataManager);
    if (num == this.UserSession.IdentHelper.AllUsersGroupID)
      throw new KernelExceptionID(sc_13502.ssp_appserver_13503(20181679));
    if (num == this.UserSession.IdentHelper.OwnerGroupID)
      throw new KernelExceptionID(sc_13502.ssp_appserver_13504(1692189800));
    base.DoAfterCreateRelation(newrelation);
  }

  protected override void DoBeforeDeleteRelation(IDBRelation relation, long deleteMode)
  {
    if (this.ObjectID == this.UserSession.IdentHelper.AllUsersGroupID && deleteMode == 0L)
    {
      IDBObject objectById = this.UserSession.GetObjectByID(relation.PartID, false);
      if (objectById != null && objectById.ObjectType == this.UserSession.IdentHelper.UsersTypeID)
      {
        if (this.UserSession.GetRelationCollection(relation.RelationType).Select(new DBRecordSetParams(new ConditionStructure[3]
        {
          new ConditionStructure(-20, RelationalOperators.NotEqual, (object) relation.RelationID, LogicalOperators.AND, 0, false),
          new ConditionStructure(-21, RelationalOperators.Equal, (object) this.ObjectID, LogicalOperators.AND, 0, false),
          new ConditionStructure(-22, RelationalOperators.Equal, (object) relation.PartID, LogicalOperators.AND, 0, false)
        }, new object[1]{ (object) -20 })).Rows.Count == 0)
          throw new KernelExceptionID(sc_13502.ssp_appserver_13505(255848751));
      }
    }
    base.DoBeforeDeleteRelation(relation, deleteMode);
  }

  protected override void DoPurge(long DeleteMode)
  {
    if (this.ObjectID > 0L)
    {
      DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OWNER_ID = :id", this.UserSession.DataManager.Parameter("id", (object) this.ObjectID));
      if (dataTable.Rows.Count > 0)
      {
        long[] objectsID = new long[dataTable.Rows.Count];
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          objectsID[index] = Convert.ToInt64(dataTable.Rows[index][0]);
        throw new ObjectsFoundException(string.Format(sc_13502.ssp_appserver_13506(), (object) this.Caption), string.Empty, objectsID);
      }
      this.DeleteFromProjectTeams();
    }
    base.DoPurge(DeleteMode);
  }

  private void DeleteFromProjectTeams()
  {
    this.UserSession.DataManager.ExecuteDataTable("DELETE FROM IMS_PROJECT_TEAM WHERE F_USER_ID = :usrID", this.UserSession.DataManager.Parameter("usrID", (object) this.ObjectID));
  }

  protected override void DoAfterCommitCreation()
  {
    base.DoAfterCommitCreation();
    DBRoleObject.ReloadRolesCache();
  }

  public override int Delete(long DeleteMode)
  {
    int num = base.Delete(DeleteMode);
    DBRoleObject.ReloadRolesCache();
    return num;
  }
}
