// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBRelationService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

public class DBRelationService : AttributableCreatorContainer, IDBRelationService
{
  protected override string KeyFieldName => "F_PRJLINK_ID";

  protected override string SystemTableName => "IMS_RELATIONS";

  public IDBRelation[] GetRelations(IUserSession uSession, long[] relationIDs, bool failIfNotFound)
  {
    if (relationIDs.Length == 0)
      return new IDBRelation[0];
    if (relationIDs.Length == 1)
      return new IDBRelation[1]
      {
        this.GetRelation(uSession, relationIDs[0], failIfNotFound)
      };
    string notFoundMessage = !failIfNotFound ? string.Empty : "Связь номер {0} не найдена.";
    DataTable mainTable = this.GetMainTable(uSession, relationIDs, notFoundMessage);
    List<IDBRelation> dbRelationList = new List<IDBRelation>(mainTable.Rows.Count);
    while (mainTable.Rows.Count > 0)
    {
      int int32 = Convert.ToInt32(mainTable.Rows[0]["F_RELATION_TYPE"]);
      Guid guid = (uSession.GetRelationType(int32) as IDBGuid).GUID;
      if (this.GetCreator((object) guid) is IDBRelationCreator creator)
        dbRelationList.Add(creator.CreateRelation(uSession, guid, mainTable));
      else
        dbRelationList.Add((IDBRelation) new DBRelation(uSession as UserSession, mainTable));
      mainTable.Rows.RemoveAt(0);
    }
    return dbRelationList.ToArray();
  }

  public IDBRelation GetRelation(IUserSession uSession, long relationID, bool failIfNotFound)
  {
    IDbManager dataManager = (uSession as UserSession).DataManager;
    DataTable tbl = dataManager.ExecuteDataTable("SELECT * FROM IMS_RELATIONS WHERE F_PRJLINK_ID = :id", dataManager.Parameter("id", (object) relationID));
    if (tbl.Rows.Count != 0)
      return this.GetRelation4Type(uSession, tbl);
    if (failIfNotFound)
      throw new KernelExceptionID(sc_13797.ssp_appserver_13798(1420998064), (object) relationID);
    return (IDBRelation) null;
  }

  public IDBRelation GetRelation(
    IUserSession uSession,
    Guid guid,
    long prjID,
    bool failIfNotFound,
    bool getActualCopy)
  {
    IDbManager dataManager = (uSession as UserSession).DataManager;
    IDbDataParameter dbDataParameter = dataManager.Parameter("prj_guid", (object) guid);
    DataTable tbl;
    if (prjID == -1L)
    {
      tbl = dataManager.ExecuteDataTable("SELECT * FROM IMS_RELATIONS WHERE F_PRJ_GUID = :prj_guid", dbDataParameter);
      if (tbl.Rows.Count > 1 & getActualCopy)
      {
        IDBObject objectActualCopy = uSession.GetObjectActualCopy(Math.Abs(Convert.ToInt64(tbl.Rows[0]["F_PROJ_ID"])), true);
        if (Convert.ToInt64(tbl.Rows[0]["F_PROJ_ID"]) != objectActualCopy.ObjectID)
        {
          tbl.Rows.RemoveAt(0);
          tbl.AcceptChanges();
          if (Convert.ToInt64(tbl.Rows[0]["F_PROJ_ID"]) != objectActualCopy.ObjectID)
            throw new KernelException($"Actual copy relation not found. Relation Guid = {guid}, actual copy ObjectID = {objectActualCopy.ObjectID}");
        }
      }
    }
    else
      tbl = dataManager.ExecuteDataTable("SELECT * FROM IMS_RELATIONS WHERE F_PRJ_GUID = :prj_guid AND F_PROJ_ID = :prjID", dbDataParameter, dataManager.Parameter(nameof (prjID), (object) prjID));
    if (tbl.Rows.Count != 0)
      return this.GetRelation4Type(uSession, tbl);
    if (failIfNotFound)
      throw new KernelExceptionID(sc_13797.ssp_appserver_13799(2021900836), (object) guid.ToString());
    return (IDBRelation) null;
  }

  public IDBRelation GetRelation(IUserSession uSession, Guid guid, long prjID)
  {
    return this.GetRelation(uSession, guid, prjID, true, false);
  }

  public IDBRelation GetRelation(
    IUserSession uSession,
    long projectID,
    long partID,
    int relationType,
    long partObjectID)
  {
    IDbManager dataManager = (uSession as UserSession).DataManager;
    if (partObjectID == 0L)
    {
      DataTable tbl;
      if (relationType < 0)
        tbl = dataManager.ExecuteDataTable($"SELECT * FROM IMS_RELATIONS WHERE F_PROJ_ID = :id1 AND F_PART_ID = :id2 AND (F_CREATE_DATE <= {dataManager.DataProvider.Now})", dataManager.Parameter("id1", (object) projectID), dataManager.Parameter("id2", (object) partID));
      else
        tbl = dataManager.ExecuteDataTable($"SELECT * FROM IMS_RELATIONS WHERE F_PROJ_ID = :id1 AND F_PART_ID = :id2 AND F_RELATION_TYPE = :id3 AND (F_CREATE_DATE <= {dataManager.DataProvider.Now})", dataManager.Parameter("id1", (object) projectID), dataManager.Parameter("id2", (object) partID), dataManager.Parameter("id3", (object) relationType));
      return tbl.Rows.Count == 0 ? (IDBRelation) null : this.GetRelation4Type(uSession, tbl);
    }
    if (relationType < 0)
    {
      DataTable tbl = dataManager.ExecuteDataTable($"SELECT * FROM IMS_RELATIONS WHERE F_PROJ_ID = :id1 AND F_PART_ID = :id2 AND (F_CREATE_DATE <= {dataManager.DataProvider.Now})", dataManager.Parameter("id1", (object) projectID), dataManager.Parameter("id2", (object) partID));
      IDBRelation relation = (IDBRelation) null;
      while (tbl.Rows.Count > 0)
      {
        IDBRelation relation4Type = this.GetRelation4Type(uSession, tbl);
        if (MetaDataHelper.GetAttribute4RelationType(relation4Type.RelationType, uSession.IdentHelper.CompositionVersionID) == null)
          return relation4Type;
        IDBAttribute attributeById = relation4Type.GetAttributeByID(uSession.IdentHelper.CompositionVersionID);
        if (attributeById == null)
          relation = relation4Type;
        else if (attributeById.AsInteger == Math.Abs(partObjectID))
          return relation4Type;
        tbl.Rows[0].Delete();
        tbl.AcceptChanges();
      }
      return relation;
    }
    OptimizationModes optimizationMode = (uSession as UserSession).DBCache.GetOptimizationMode(uSession.IdentHelper.CompositionVersionID, -1, relationType);
    switch (optimizationMode)
    {
      case OptimizationModes.Read:
      case OptimizationModes.Seek:
        object obj = dataManager.ExecuteScalar($"SELECT F_PRJLINK_ID FROM IMV_R{relationType} WHERE F_PROJ_ID = :id1 AND F_PART_ID = :id2 AND F{uSession.IdentHelper.CompositionVersionID} = :id3 AND (F_CREATE_DATE <= {dataManager.DataProvider.Now})", dataManager.Parameter("id1", (object) projectID), dataManager.Parameter("id2", (object) partID), dataManager.Parameter("id3", (object) Math.Abs(partObjectID)));
        if (obj != null && obj != DBNull.Value)
        {
          DataTable tbl = dataManager.ExecuteDataTable("SELECT * FROM IMS_RELATIONS WHERE F_PRJLINK_ID = :id1", dataManager.Parameter("id1", obj));
          return tbl.Rows.Count == 0 ? (IDBRelation) null : this.GetRelation4Type(uSession, tbl);
        }
        break;
    }
    DataTable tbl1 = dataManager.ExecuteDataTable($"SELECT * FROM IMS_RELATIONS WHERE F_PROJ_ID = :id1 AND F_PART_ID = :id2 AND F_RELATION_TYPE = :id3 AND (F_CREATE_DATE <= {dataManager.DataProvider.Now})", dataManager.Parameter("id1", (object) projectID), dataManager.Parameter("id2", (object) partID), dataManager.Parameter("id3", (object) relationType));
    if (tbl1.Rows.Count == 0)
      return (IDBRelation) null;
    IDBRelation relation1 = (IDBRelation) null;
    while (tbl1.Rows.Count > 0)
    {
      IDBRelation relation4Type = this.GetRelation4Type(uSession, tbl1);
      if (optimizationMode == OptimizationModes.NotFound)
        return relation4Type;
      IDBAttribute attributeById = relation4Type.GetAttributeByID(uSession.IdentHelper.CompositionVersionID);
      if (attributeById == null)
        relation1 = relation4Type;
      else if (attributeById.AsInteger == Math.Abs(partObjectID))
        return relation4Type;
      tbl1.Rows[0].Delete();
      tbl1.AcceptChanges();
    }
    return relation1;
  }

  public IDBRelation GetRelation(IUserSession uSession, DataTable tbl, int index)
  {
    DataTable dataTable = tbl.Clone();
    SqlHelper.AssignRow(dataTable, tbl.Rows[index]);
    return this.GetRelation4Type(uSession, dataTable);
  }

  private IDBRelation GetRelation4Type(IUserSession uSession, DataTable tbl)
  {
    int int32 = Convert.ToInt32(tbl.Rows[0]["F_RELATION_TYPE"]);
    Guid guid = (uSession.GetRelationType(int32) as IDBGuid).GUID;
    return this.GetCreator((object) guid) is IDBRelationCreator creator ? creator.CreateRelation(uSession, guid, tbl) : (IDBRelation) new DBRelation(uSession as UserSession, tbl);
  }
}
