// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Projects.DBProjectObject
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Projects;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel.Projects;

public class DBProjectObject(UserSession uSession, DataTable objectParams) : 
  DBObject(uSession, objectParams),
  IDBProjectObject,
  IObjectTemplater
{
  private bool _DontSetAdditionalLevelAttr;

  protected internal override bool ReadOnlyProjectID() => true;

  private void ClearProjectLinks()
  {
    if (this.ObjectID > 0L)
    {
      IDbManager dataManager = this.UserSession.DataManager;
      DataTable dataTable = dataManager.ExecuteDataTable(sc_13518.ssp_appserver_13519(), dataManager.Parameter("prjID", (object) this.ObjectID));
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        if (this.UserSession.GetObject(Convert.ToInt64(dataTable.Rows[index][0])) is DBObject dbObject)
          dbObject.SetProjectID(0L);
      }
    }
    if (this.UserSession.CurrentProjectID != this.ObjectID)
      return;
    this.UserSession.CurrentProjectID = 0L;
  }

  protected override void DoPurge(long DeleteMode)
  {
    this.ClearProjectLinks();
    this.UserSession.DataManager.ExecuteNonQuery(sc_13518.ssp_appserver_13520(), this.UserSession.DataManager.Parameter("prjID", (object) this.ObjectID));
    base.DoPurge(DeleteMode);
  }

  protected override void DoDelete()
  {
    this.ValidateManagerRights();
    this.ClearProjectLinks();
    base.DoDelete();
  }

  protected override void DoCommitCreation()
  {
    if ((long) this.UserSession.SecurityLevel < this.GetAttributeByID(this.UserSession.IdentHelper.SecurityLevelID).AsInteger)
      throw new KernelExceptionID(sc_13518.ssp_appserver_13521(950001442), (object) DBSecurity.GetSecurityLevelDescription((IUserSession) this.UserSession, (long) this.UserSession.SecurityLevel));
    base.DoCommitCreation();
    IDbManager dataManager = this.UserSession.DataManager;
    dataManager.ExecuteNonQuery("INSERT INTO IMS_PROJECT_TEAM (F_PROJECT_ID, F_USER_ID, F_OPTIONS) VALUES (:prjID, :usrID, :fopt)", dataManager.Parameter("prjID", (object) Math.Abs(this.ObjectID)), dataManager.Parameter("usrID", (object) this.OwnerID), dataManager.Parameter("fopt", (object) 1));
  }

  private void ValidateManagerRights()
  {
    if (!this.IsProjectManager())
      throw new KernelExceptionID(sc_13518.ssp_appserver_13522(512898191), (object) this.Caption);
  }

  protected internal override IDBSecurity ProjectSecurity
  {
    get
    {
      if (this.ProjectID > 0L && this._ProjectSecurity == null)
        this._ProjectSecurity = (IDBSecurity) new ProjectDBSecurity(this.UserSession, this, (DBObject) this, true);
      return this._ProjectSecurity;
    }
  }

  internal override void SetAccessLevel(int value, List<long> excludeList)
  {
    DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable(sc_13518.ssp_appserver_13523(), this.UserSession.DataManager.Parameter("projID", (object) this.ObjectID));
    excludeList = new List<long>(dataTable.Rows.Count + 1);
    excludeList.Add(this.ObjectID);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
      excludeList.Add(Convert.ToInt64(dataTable.Rows[index][0]));
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      if (this.UserSession.GetObject(Convert.ToInt64(dataTable.Rows[index][0]), false) is DBObject dbObject && (dbObject.ObjectTypeClass.Options & ObjectTypeOptions.MandateAccess) == ObjectTypeOptions.MandateAccess)
        dbObject.SetAccessLevel(Convert.ToInt32(value), excludeList);
    }
    base.SetAccessLevel(value, excludeList);
    if (this._DontSetAdditionalLevelAttr)
      return;
    this.GetAttributeByID(this.UserSession.IdentHelper.SecurityLevelID).AsInteger = (long) value;
  }

  protected override void DoBeforeSetAdditionalAttributeValue(
    IDBAttribute attribute,
    object newValue)
  {
    if (newValue != null && newValue != DBNull.Value && attribute.AttributeID == this.UserSession.IdentHelper.SecurityLevelID)
    {
      if (Convert.ToInt64(newValue) > attribute.AsInteger)
      {
        ProjectParticipantInfo[] participants = this.GetParticipants();
        long int64 = Convert.ToInt64(newValue);
        for (int index = 0; index < participants.Length; ++index)
        {
          IDBObject dbObject = this.UserSession.GetObject(participants[index].ParticipantID, false);
          if (dbObject != null)
          {
            if (dbObject.ObjectType == this.UserSession.IdentHelper.GroupsTypeID)
              throw new KernelException(string.Format(sc_13518.ssp_appserver_13524(), (object) this.Caption));
            if (dbObject.GetAttributeByID(this.UserSession.IdentHelper.SecurityLevelID).AsInteger < int64)
              throw new KernelExceptionID(sc_13518.ssp_appserver_13525(187980808), (object) this.Caption, (object) DBSecurity.GetSecurityLevelDescription((IUserSession) this.UserSession, int64));
          }
        }
      }
      this._DontSetAdditionalLevelAttr = true;
      this.SetAccessLevel(Convert.ToInt32(newValue), (List<long>) null);
      this._DontSetAdditionalLevelAttr = false;
    }
    base.DoBeforeSetAdditionalAttributeValue(attribute, newValue);
  }

  protected override void DoAfterSetAdditionalAttributeValue(IDBAttribute attribute)
  {
    if (attribute.AttributeID == ObjectsVisibilityHelper.AttrVisibilityId && ServerConsts.CopyProjectVisibility)
    {
      List<int> parentTypeIDs = new List<int>(3);
      parentTypeIDs.Add(this.UserSession.DBCache.ArticleTypeID);
      parentTypeIDs.Add(this.UserSession.DBCache.DocumentTypeID);
      if (this.UserSession.DBCache.ProductTypeID > 0)
        parentTypeIDs.Add(this.UserSession.DBCache.ProductTypeID);
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) parentTypeIDs);
      StringBuilder stringBuilder = new StringBuilder();
      foreach (int num in childrenIdRecursive)
        stringBuilder.Append(num.ToString() + ",");
      --stringBuilder.Length;
      ObjectsVisibilityHelper.SetProjVisibility((IUserSession) this.UserSession, this.UserSession.DataManager.ExecuteDataTable($"SELECT F_OBJECT_ID, F_OBJECT_TYPE FROM IMS_OBJECTS WHERE F_PROJECT_ID = :projID AND F_OBJECT_TYPE IN ({stringBuilder})", this.UserSession.DataManager.Parameter("projID", (object) this.ProjectID)), attribute.AsString, ServerConsts.CopyArcVisibility);
    }
    base.DoAfterSetAdditionalAttributeValue(attribute);
  }

  public override AttributeValues[] SetAttributesValues(
    AttributeValues[] valuesList,
    bool deleteNotExistingAttributes,
    bool dontDeleteBlobs,
    bool returnDelta,
    GetAttributeValuesModes modes,
    Dictionary<string, Exception> exceptionsList)
  {
    if (this.IsCreationMode)
    {
      int index1 = -1;
      int num = -1;
      for (int index2 = 0; index2 < valuesList.Length; ++index2)
      {
        if (valuesList[index2].AttributeID == this.UserSession.IdentHelper.SecurityLevelID)
          index1 = index2;
        else if (valuesList[index2].AttributeID == -80)
          num = index2;
      }
      if (num == -1 && index1 > -1)
      {
        valuesList[index1].AttributeID = -80;
        valuesList[index1].AttributeType = FieldTypes.ftSystem;
      }
    }
    return base.SetAttributesValues(valuesList, deleteNotExistingAttributes, dontDeleteBlobs, returnDelta, modes, exceptionsList);
  }

  internal void SetProjectAccessLevel(int levelID)
  {
    DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable("SELECT F_OBJECT_ID, F_OBJECT_TYPE FROM IMS_OBJECTS WHERE F_ACCESS <> :levelID AND F_PROJECT_ID = :prjID", this.UserSession.DataManager.Parameter(nameof (levelID), (object) levelID), this.UserSession.DataManager.Parameter("prjID", (object) this.ObjectID));
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      if ((MetaDataHelper.GetObjectType(Convert.ToInt32(dataTable.Rows[index][1])).Options & ObjectTypeOptions.MandateAccess) == ObjectTypeOptions.MandateAccess && this.UserSession.GetObject(Convert.ToInt64(dataTable.Rows[index][0])) is DBObject dbObject)
        dbObject.DoSetAccessLevel(levelID);
    }
  }

  public override void DoAfterCreateRelation(IDBRelation newrelation)
  {
    if (newrelation.PartObjectID != 0L)
    {
      IDBObject dbObject = this.UserSession.GetObject(newrelation.PartObjectID);
      if ((MetaDataHelper.GetObjectType(dbObject.ObjectType).Options & ObjectTypeOptions.CurrentProjectEnabled) == ObjectTypeOptions.CurrentProjectEnabled)
      {
        if (dbObject.ProjectID == 0L)
          dbObject.ProjectID = this.ObjectID;
        else if (dbObject.ProjectID != this.ObjectID)
          throw new KernelExceptionID(384, (object) dbObject.NameInMessages, (object) this.NameInMessages, (object) this.UserSession.GetObject(dbObject.ProjectID).NameInMessages, (object) dbObject.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(dbObject.ObjectID));
      }
    }
    base.DoAfterCreateRelation(newrelation);
  }

  public override void DoAfterCreate()
  {
    IDBAttribute attributeById = this.GetAttributeByID(this.UserSession.IdentHelper.SecurityLevelID);
    if (attributeById.AsInteger != (long) this.UserSession.SecurityLevel)
      attributeById.AsInteger = (long) this.UserSession.SecurityLevel;
    base.DoAfterCreate();
  }

  public override long ProjectID
  {
    set
    {
      if (value > 0L && this.ObjectID != value)
        throw new KernelExceptionID(sc_13518.ssp_appserver_13527(1854255671), (object) this.Caption, (object) this.UserSession.GetObject(value).Caption);
    }
  }

  public long LinkedObjectsCount
  {
    get
    {
      return Convert.ToInt64(this.UserSession.DataManager.ExecuteScalar("SELECT COUNT(*) FROM IMS_OBJECTS WHERE F_PROJECT_ID = :prjID", this.UserSession.DataManager.Parameter("prjID", (object) this.ObjectID)));
    }
  }

  public long[] LinkedObjects
  {
    get
    {
      IDbManager dataManager = this.UserSession.DataManager;
      DataTable dataTable = dataManager.ExecuteDataTable(sc_13518.ssp_appserver_13528(), dataManager.Parameter("prjID", (object) this.ObjectID));
      long[] linkedObjects = new long[dataTable.Rows.Count];
      for (int index = 0; index < dataTable.Rows.Count; ++index)
        linkedObjects[index] = Convert.ToInt64(dataTable.Rows[index][0]);
      return linkedObjects;
    }
  }

  public void IncludeParticipants(ProjectParticipantInfo[] participants)
  {
    if (participants.Length == 0)
      return;
    this.ValidateManagerRights();
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter1 = dataManager.Parameter("prjID", (object) this.ObjectID);
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("partID", (object) participants[0].ParticipantID);
    IDbDataParameter dbDataParameter3 = dataManager.Parameter("opts", (object) (participants[0].ProjectManager ? 1 : 0));
    this.UserSession.StartTransaction();
    try
    {
      for (int index = 0; index < participants.Length; ++index)
      {
        IDBObject dbObject = this.UserSession.GetObject(participants[index].ParticipantID, true);
        if (dbObject.ObjectType == this.UserSession.IdentHelper.UsersTypeID)
        {
          IDBAttribute attributeById = dbObject.GetAttributeByID(this.UserSession.IdentHelper.SecurityLevelID);
          if (attributeById == null)
            throw new KernelExceptionID(sc_13518.ssp_appserver_13529(122811901), (object) dbObject.Caption);
          if (attributeById.AsInteger < this.GetAttributeByID(this.UserSession.IdentHelper.SecurityLevelID).AsInteger)
            throw new KernelExceptionID(sc_13518.ssp_appserver_13530(889717713), (object) dbObject.Caption, (object) this.Caption);
        }
        else if (dbObject.ObjectType == this.UserSession.IdentHelper.GroupsTypeID && this.AccessLevel > 0)
          throw new KernelExceptionID(sc_13518.ssp_appserver_13531(898585507), (object) this.Caption);
        dbDataParameter2.Value = (object) participants[index].ParticipantID;
        dbDataParameter3.Value = (object) Convert.ToInt32(participants[index].ProjectManager ? 1 : 0);
        dataManager.ExecuteNonQuery("INSERT INTO IMS_PROJECT_TEAM (F_PROJECT_ID, F_USER_ID, F_OPTIONS) VALUES (:prjID, :partID, :opts)", dbDataParameter1, dbDataParameter2, dbDataParameter3);
      }
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  public void ExcludeParticipants(long[] users)
  {
    this.ValidateManagerRights();
    ProjectParticipantInfo[] participants = this.GetParticipants();
    for (int index1 = 0; index1 < users.Length; ++index1)
    {
      for (int index2 = 0; index2 < participants.Length; ++index2)
      {
        if (users[index1] == participants[index2].ParticipantID)
        {
          participants[index2].ProjectManager = false;
          break;
        }
      }
    }
    bool flag = false;
    for (int index = 0; index < participants.Length; ++index)
    {
      if (participants[index].ProjectManager)
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      throw new KernelExceptionID(sc_13518.ssp_appserver_13532(389066315));
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter1 = dataManager.Parameter("prjID", (object) this.ObjectID);
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("partID", (object) users[0]);
    this.UserSession.StartTransaction();
    try
    {
      for (int index = 0; index < users.Length; ++index)
      {
        dbDataParameter2.Value = (object) users[index];
        dataManager.ExecuteNonQuery("DELETE FROM IMS_PROJECT_TEAM WHERE F_PROJECT_ID = :prjID AND F_USER_ID = :partID", dbDataParameter1, dbDataParameter2);
      }
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  public void SetParticipantOptions(long userID, ProjectParticipantOptions options)
  {
    this.ValidateManagerRights();
    IDbManager dataManager = this.UserSession.DataManager;
    if (this.IsProjectParticipant(userID))
      dataManager.ExecuteNonQuery("UPDATE IMS_PROJECT_TEAM SET F_OPTIONS = :opts WHERE F_PROJECT_ID = :prjID AND F_USER_ID = :usrID", dataManager.Parameter("opts", (object) (int) options), dataManager.Parameter("prjID", (object) this.ObjectID), dataManager.Parameter("usrID", (object) userID));
    else
      dataManager.ExecuteNonQuery("INSERT INTO IMS_PROJECT_TEAM (F_PROJECT_ID, F_USER_ID, F_OPTIONS) VALUES (:prjID, :usrID, :opts)", dataManager.Parameter("opts", (object) (int) options), dataManager.Parameter("prjID", (object) this.ObjectID), dataManager.Parameter("usrID", (object) userID));
  }

  public ProjectParticipantInfo[] GetParticipants()
  {
    IDbManager dataManager = this.UserSession.DataManager;
    DataTable dataTable = dataManager.ExecuteDataTable("SELECT F_USER_ID, F_OPTIONS FROM IMS_PROJECT_TEAM WHERE F_PROJECT_ID = :prjID", dataManager.Parameter("prjID", (object) this.ObjectID));
    ProjectParticipantInfo[] participants = new ProjectParticipantInfo[dataTable.Rows.Count];
    for (int index = 0; index < dataTable.Rows.Count; ++index)
      participants[index] = new ProjectParticipantInfo(Convert.ToInt64(dataTable.Rows[index][0]), (Convert.ToInt32(dataTable.Rows[index][1]) & 1) == 1);
    return participants;
  }

  public ProjectParticipantInfoEx[] GetParticipantsInfo()
  {
    ProjectParticipantInfo[] participants = this.GetParticipants();
    ProjectParticipantInfoEx[] participantsInfo = new ProjectParticipantInfoEx[participants.Length];
    for (int index = 0; index < participants.Length; ++index)
      participantsInfo[index] = new ProjectParticipantInfoEx(participants[index].ParticipantID, participants[index].ProjectManager, this.UserSession.GetObjectInfo(participants[index].ParticipantID).Caption);
    return participantsInfo;
  }

  public bool IsProjectManager(long userID)
  {
    if (this.OwnerID == userID)
      return true;
    object obj = this.UserSession.DataManager.ExecuteScalar("SELECT F_OPTIONS FROM IMS_PROJECT_TEAM WHERE F_PROJECT_ID = :prjID AND F_USER_ID = :usrID", this.UserSession.DataManager.Parameter("prjID", (object) this.ObjectID), this.UserSession.DataManager.Parameter("usrID", (object) userID));
    return obj != null && obj != DBNull.Value && (Convert.ToInt32(obj) & 1) == 1;
  }

  public bool IsProjectManager() => this.IsProjectManager(this.UserSession.UserID);

  public bool IsProjectParticipant(long userID)
  {
    if (this.OwnerID == userID)
      return true;
    object obj = this.UserSession.DataManager.ExecuteScalar("SELECT F_OPTIONS FROM IMS_PROJECT_TEAM WHERE F_PROJECT_ID = :prjID AND F_USER_ID = :usrID", this.UserSession.DataManager.Parameter("prjID", (object) this.ObjectID), this.UserSession.DataManager.Parameter("usrID", (object) userID));
    return obj != null && obj != DBNull.Value;
  }

  public bool IsProjectParticipant()
  {
    if (this.OwnerID == this.UserSession.UserID)
      return true;
    object obj = this.UserSession.DataManager.ExecuteScalar($"SELECT F_OPTIONS FROM IMS_PROJECT_TEAM WHERE F_PROJECT_ID = :prjID AND F_USER_ID IN ({this.UserSession.DBSecurity._GroupsSQL})", this.UserSession.DataManager.Parameter("prjID", (object) this.ObjectID));
    return obj != null && obj != DBNull.Value;
  }
}
