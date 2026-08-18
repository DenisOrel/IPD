// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBUserObject
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Projects;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Protection;
using Intermech.Workspace;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

public class DBUserObject(UserSession uSession, DataTable objectParams) : DBObject(uSession, objectParams)
{
  protected override void DoBeforeCommitCreation()
  {
    if (this.UserSession.GetRelation(this.UserSession.IdentHelper.AllUsersGroupID, this.ID, this.UserSession.IdentHelper.SimpleRelationTypeID) == null)
      this.UserSession.GetRelationCollection(this.UserSession.IdentHelper.SimpleRelationTypeID).Create(this.UserSession.IdentHelper.AllUsersGroupID, this.ObjectID);
    base.DoBeforeCommitCreation();
  }

  internal static void AfterAddUser(IUserSession session, long userID)
  {
    ServerWorkspace serverWorkspace = session.GetObjectCollection(session.IdentHelper.WorkspaceTypeID).Create() as ServerWorkspace;
    serverWorkspace._CanCreate = true;
    serverWorkspace.OwnerID = Math.Abs(userID);
    serverWorkspace.CommitCreation(true);
  }

  protected override void DoCommitCreation()
  {
    base.DoCommitCreation();
    this.UserSession.DBCache.AddUserToCache((IDBObject) this);
    DBUserObject.AfterAddUser((IUserSession) this.UserSession, this.ObjectID);
    IDBAttribute attributeById = this.GetAttributeByID(this.UserSession.IdentHelper.ExternalUserID);
    if (attributeById == null || !attributeById.AsBoolean)
      return;
    this.LCStep = this.UserSession.GetLifecycleStep(new Guid("cadd9502-306c-11d8-b4e9-00304f19f545")).LCStep;
  }

  protected override void DoDelete()
  {
    if (this.ObjectID == this.UserSession.UserID || UserSession.Sessions.IsLoggedIn(this.ObjectID))
      throw new KernelExceptionID(sc_13496.ssp_appserver_13497(1759890532), (object) this.Caption);
    IDBObjectCollection objectCollection = this.UserSession.GetObjectCollection(-1);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-6, RelationalOperators.Equal, (object) this.ObjectID, LogicalOperators.NONE, 0, false)
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    DataTable dataTable1 = objectCollection.Select(paramSet);
    if (dataTable1.Rows.Count > 0)
    {
      long[] objectsID = new long[dataTable1.Rows.Count];
      for (int index = 0; index < dataTable1.Rows.Count; ++index)
        objectsID[index] = Convert.ToInt64(dataTable1.Rows[index][0]);
      throw new ObjectsFoundException(string.Format(sc_13496.ssp_appserver_13498(), (object) this.Caption), string.Empty, objectsID);
    }
    paramSet = new DBRecordSetParams(new ConditionStructure[2]
    {
      new ConditionStructure(-8, RelationalOperators.Equal, (object) this.ObjectID, LogicalOperators.AND, 0, false),
      new ConditionStructure(-9, RelationalOperators.Equal, (object) this.UserSession.IdentHelper.PersonalLevelID, LogicalOperators.NONE, 0, false)
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    (objectCollection as DBObjectCollection)._ShowPersonalObjects = true;
    (objectCollection as DBObjectCollection).GlobalSelectMode = true;
    (objectCollection as DBObjectCollection).LocalTypesMode = true;
    foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
    {
      IDBObject dbObject = this.UserSession.GetObject(Convert.ToInt64(row[0]), false);
      if (dbObject != null)
      {
        if (dbObject is ServerWorkspace)
          (dbObject as ServerWorkspace)._CanDelete = true;
        dbObject.Delete(0L);
      }
    }
    IDbManager dataManager = this.UserSession.DataManager;
    if (this.ObjectID > 0L)
    {
      DataTable dataTable2 = dataManager.ExecuteDataTable("SELECT F_PROJECT_ID FROM IMS_PROJECT_TEAM WHERE F_USER_ID = :usrID AND F_OPTIONS > 0", dataManager.Parameter("usrID", (object) this.ObjectID));
      for (int index = 0; index < dataTable2.Rows.Count; ++index)
      {
        if (this.UserSession.GetObject(Convert.ToInt64(dataTable2.Rows[index][0]), false) is IDBProjectObject dbProjectObject)
        {
          long[] users = new long[1]{ this.ObjectID };
          dbProjectObject.ExcludeParticipants(users);
        }
      }
      dataManager.ExecuteDataTable("DELETE FROM IMS_PROJECT_TEAM WHERE F_USER_ID = :usrID", dataManager.Parameter("usrID", (object) this.ObjectID));
    }
    ISitesCacheService customService1 = (ISitesCacheService) this.Session.GetCustomService(typeof (ISitesCacheService));
    if (customService1.Info != null && this.Session.GetRelation(customService1.Info.ID, this.ID) != null)
      ((IPortalConnector) this.Session.GetCustomService(typeof (IPortalConnector)))?.DeleteUser(this.Session.SessionGUID, this.GetAttributeByGuid(new Guid("cad00018-306c-11d8-b4e9-00304f19f545")).AsString);
    if (!(this.Session.GetCustomService(typeof (IUserFavouritesService)) is IUserFavouritesService customService2))
      return;
    customService2.ClearFavourites(this.Session.SessionGUID);
  }

  protected override void DoPurge(long DeleteMode)
  {
    if (this.ObjectID > 0L)
    {
      DataTable dataTable1 = this.UserSession.DataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_CREATOR_ID = :id", this.UserSession.DataManager.Parameter("id", (object) this.ObjectID));
      if (dataTable1.Rows.Count > 0)
      {
        long[] objectsID = new long[dataTable1.Rows.Count];
        for (int index = 0; index < dataTable1.Rows.Count; ++index)
          objectsID[index] = Convert.ToInt64(dataTable1.Rows[index][0]);
        throw new ObjectsFoundException(string.Format(sc_13496.ssp_appserver_13499(), (object) this.Caption), string.Empty, objectsID);
      }
      DataTable dataTable2 = this.UserSession.DataManager.ExecuteDataTable("SELECT F_PRJLINK_ID FROM IMS_RELATIONS WHERE F_REL_CREATOR = :id", this.UserSession.DataManager.Parameter("id", (object) this.ObjectID));
      if (dataTable2.Rows.Count > 0)
      {
        long[] relationsID = new long[dataTable2.Rows.Count];
        for (int index = 0; index < dataTable2.Rows.Count; ++index)
          relationsID[index] = Convert.ToInt64(dataTable2.Rows[index][0]);
        throw new RelationsFoundException(string.Format(sc_13496.ssp_appserver_13500(), (object) this.Caption), string.Empty, relationsID);
      }
      DataTable dataTable3 = this.UserSession.DataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OWNER_ID = :id", this.UserSession.DataManager.Parameter("id", (object) this.ObjectID));
      if (dataTable3.Rows.Count > 0)
      {
        long[] objectsID = new long[dataTable3.Rows.Count];
        for (int index = 0; index < dataTable3.Rows.Count; ++index)
          objectsID[index] = Convert.ToInt64(dataTable3.Rows[index][0]);
        throw new ObjectsFoundException(string.Format(sc_13496.ssp_appserver_13501(), (object) this.Caption), string.Empty, objectsID);
      }
    }
    base.DoPurge(DeleteMode);
  }

  internal static void WriteAttributeValue(IDBAttribute attribute, AttributeValueEventArgs args)
  {
    args.NewValue = (object) args.Value.ToString().ToUpper();
  }

  internal static void Init()
  {
    (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddAttributeWriteHandler((object) new Guid("cad00018-306c-11d8-b4e9-00304f19f545"), new WriteAttributeValueHandler(DBUserObject.WriteAttributeValue));
  }

  protected override void DoBeforeRemoveObject(DBRelation dBRelation, long newProjID)
  {
    if (dBRelation.ProjID == this.UserSession.IdentHelper.AllUsersGroupID)
      throw new KernelExceptionID(387);
  }

  public override AttributeValues[] SetAttributesValues(
    AttributeValues[] valuesList,
    bool deleteNotExistingAttributes,
    bool dontDeleteBlobs,
    bool returnDelta,
    GetAttributeValuesModes modes,
    Dictionary<string, Exception> exceptionsList)
  {
    if (!((ISiteServerService) this.UserSession.GetCustomService(typeof (ISiteServerService))).Initialized)
      return base.SetAttributesValues(valuesList, deleteNotExistingAttributes, dontDeleteBlobs, returnDelta, modes, exceptionsList);
    (this.Session as UserSession).StartTransaction();
    try
    {
      AttributeValues[] attributeValuesArray = base.SetAttributesValues(valuesList, deleteNotExistingAttributes, dontDeleteBlobs, returnDelta, modes, exceptionsList);
      foreach (AttributeValues values in valuesList)
      {
        if (values.AttributeID == this.Session.IdentHelper.PasswordID)
        {
          ISitesCacheService customService1 = (ISitesCacheService) this.Session.GetCustomService(typeof (ISitesCacheService));
          if (customService1.Info != null)
          {
            IPortalConnector customService2 = (IPortalConnector) this.Session.GetCustomService(typeof (IPortalConnector));
            if (customService2 != null && this.Session.GetRelation(this.Session.GetObject(customService1.Info.ID).ObjectID, this.ID) != null)
            {
              if (values.Values != null && values.Values[0] is PswPackage newPassword)
                customService2.ChangeUserPassword(this.Session.SessionGUID, this.GetAttributeByGuid(new Guid("cad00018-306c-11d8-b4e9-00304f19f545")).AsString, newPassword);
              else
                customService2.ChangeUserPassword(this.Session.SessionGUID, this.GetAttributeByGuid(new Guid("cad00018-306c-11d8-b4e9-00304f19f545")).AsString, values.AsString);
            }
          }
        }
      }
      (this.Session as UserSession).Commit();
      return attributeValuesArray;
    }
    catch
    {
      (this.Session as UserSession).Rollback();
      throw;
    }
  }

  protected override void DoAfterSetAdditionalAttributeValue(IDBAttribute attribute)
  {
    base.DoAfterSetAdditionalAttributeValue(attribute);
    if (this.ObjectID > 0L && attribute.AttributeID == this.UserSession.IdentHelper.UserNameID)
      this.UserSession.DBCache.ClearUsersCache();
    if (this.IsCreationMode || this.UserSession.IdentHelper.ExternalUserID != attribute.AttributeID || this.LevelID == this.UserSession.IdentHelper.DeletedID)
      return;
    this.LCStep = this.UserSession.GetLifecycleStep(!attribute.AsBoolean ? new Guid("cadd9503-306c-11d8-b4e9-00304f19f545") : new Guid("cadd9502-306c-11d8-b4e9-00304f19f545")).LCStep;
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
