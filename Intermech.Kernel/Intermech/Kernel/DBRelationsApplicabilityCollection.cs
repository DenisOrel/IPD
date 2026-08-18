// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBRelationsApplicabilityCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel;

public class DBRelationsApplicabilityCollection : DBCollection, IDBRelationsApplicabilityCollection
{
  internal static ConcurrentDictionary<MyCompositeKey, DataTable> _ApplCache = new ConcurrentDictionary<MyCompositeKey, DataTable>();
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(3);

  static DBRelationsApplicabilityCollection()
  {
    DBRelationsApplicabilityCollection.metadataActions.Add(ActionType.GetAccess, false);
    DBRelationsApplicabilityCollection.metadataActions.Add(ActionType.SetAccess, false);
    DBRelationsApplicabilityCollection.metadataActions.Add(ActionType.EditLink, false);
  }

  public DBRelationsApplicabilityCollection(UserSession uSession)
    : base(uSession, false)
  {
    this._DBTableName = "IMS_TYPES_APPLICABILITY";
    this._DBKeyField = "F_APPLICABILITY_ID";
    this._AreaSupport = false;
    this._LanguageSupport = false;
    this.InitSecurityOptions(4, 0L);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBRelationsApplicabilityCollection.metadataActions);
  }

  public override string ObjectName => LocalizationHolder.rm.GetString("Kernel_526");

  public DataTable GetApplicabilitiesList(int relationType, int objectType, int inObjectType)
  {
    if (objectType < 0 && inObjectType < 0)
      throw new KernelExceptionID(sc_13578.ssp_appserver_13615(1591477762));
    MyCompositeKey key = new MyCompositeKey(new object[3]
    {
      (object) relationType,
      (object) objectType,
      (object) inObjectType
    });
    DataTable applicabilitiesList1;
    if (DBRelationsApplicabilityCollection._ApplCache.TryGetValue(key, out applicabilitiesList1))
      return applicabilitiesList1;
    DataTable table = this.UserSession.DBCache.GetTable("IMS_TYPES_APPLICABILITY");
    DataTable applicabilitiesList2 = table.Clone();
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      if (relationType > -1)
        stringBuilder.AppendFormat("F_RELATION_TYPE = {0}", (object) relationType);
      ArrayList objsTreeList1 = new ArrayList();
      ArrayList objsTreeList2 = new ArrayList();
      ArrayList arrayList = (ArrayList) null;
      if (objectType > -1)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append(" AND ");
        stringBuilder.Append("F_OBJECT_TYPE = {0}");
        (this.UserSession.GetObjectType(objectType) as DBObjectType).FillParentsArray(objsTreeList1);
        arrayList = objsTreeList1;
      }
      if (inObjectType > -1)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append(" AND ");
        if (objectType > -1)
          stringBuilder.Append("F_INOBJECT_TYPE = {1}");
        else
          stringBuilder.Append("F_INOBJECT_TYPE = {0}");
        (this.UserSession.GetObjectType(inObjectType) as DBObjectType).FillParentsArray(objsTreeList2);
        arrayList = objsTreeList2;
      }
      if (objectType > -1 && inObjectType > -1)
      {
        for (int index1 = 0; index1 < objsTreeList1.Count; ++index1)
        {
          for (int index2 = 0; index2 < objsTreeList2.Count; ++index2)
          {
            DataRow[] dataRowArray = table.Select(string.Format(stringBuilder.ToString(), objsTreeList1[index1], objsTreeList2[index2]));
            if (dataRowArray.Length != 0)
            {
              foreach (DataRow fromRow in dataRowArray)
              {
                SqlHelper.AssignRow(applicabilitiesList2, fromRow);
                if (index1 > 0 || index2 > 0)
                {
                  applicabilitiesList2.Rows[applicabilitiesList2.Rows.Count - 1]["F_PUBLIC"] = (object) Convert.ToInt32((object) InheritModes.Inherited);
                  applicabilitiesList2.AcceptChanges();
                }
                if ((Convert.ToInt32(fromRow["F_OPTIONS"]) & 2) == 2)
                {
                  while (applicabilitiesList2.Rows.Count > 1)
                    applicabilitiesList2.Rows.RemoveAt(0);
                  return applicabilitiesList2;
                }
              }
            }
          }
        }
        while (applicabilitiesList2.Rows.Count > 1)
          applicabilitiesList2.Rows.RemoveAt(applicabilitiesList2.Rows.Count - 1);
      }
      else
      {
        for (int index3 = 0; index3 < arrayList.Count; ++index3)
        {
          DataRow[] fromRows = table.Select(string.Format(stringBuilder.ToString(), arrayList[index3]));
          if (inObjectType > -1 && index3 > 0)
          {
            if (fromRows.Length != 0)
            {
              foreach (DataRow dataRow in fromRows)
              {
                bool flag = false;
                for (int index4 = 0; index4 < applicabilitiesList2.Rows.Count; ++index4)
                {
                  if (MetaDataHelper.IsObjectTypeChildOf(Convert.ToInt32(dataRow["F_OBJECT_TYPE"]), Convert.ToInt32(applicabilitiesList2.Rows[index4]["F_OBJECT_TYPE"])))
                  {
                    flag = true;
                    break;
                  }
                }
                if (!flag)
                {
                  DataRow row = applicabilitiesList2.NewRow();
                  for (int columnIndex = 0; columnIndex < applicabilitiesList2.Columns.Count; ++columnIndex)
                    row[columnIndex] = dataRow[columnIndex];
                  applicabilitiesList2.Rows.Add(row);
                }
              }
            }
            else
              continue;
          }
          else if (objectType > -1 && index3 > 0)
          {
            int columnIndex1 = applicabilitiesList2.Columns.IndexOf("F_INOBJECT_TYPE");
            foreach (DataRow dataRow in fromRows)
            {
              bool flag = false;
              for (int index5 = 0; index5 < applicabilitiesList2.Rows.Count; ++index5)
              {
                if (Convert.ToInt32(dataRow[columnIndex1]) == Convert.ToInt32(applicabilitiesList2.Rows[index5][columnIndex1]))
                {
                  flag = true;
                  break;
                }
              }
              if (!flag)
              {
                DataRow row = applicabilitiesList2.NewRow();
                for (int columnIndex2 = 0; columnIndex2 < applicabilitiesList2.Columns.Count; ++columnIndex2)
                  row[columnIndex2] = dataRow[columnIndex2];
                applicabilitiesList2.Rows.Add(row);
              }
            }
            applicabilitiesList2.AcceptChanges();
          }
          else
            SqlHelper.AssignRows(applicabilitiesList2, (IEnumerable<DataRow>) fromRows);
          foreach (DataRow row in (InternalDataCollectionBase) applicabilitiesList2.Rows)
          {
            if (index3 == 0)
              row["F_PUBLIC"] = (object) Convert.ToInt32((object) InheritModes.Public);
            else if (Convert.ToInt32(row["F_PUBLIC"]) == Convert.ToInt32((object) InheritModes.Private))
              row["F_PUBLIC"] = (object) Convert.ToInt32((object) InheritModes.Inherited);
          }
        }
        foreach (DataRow row in (InternalDataCollectionBase) applicabilitiesList2.Rows)
        {
          if (Convert.ToInt32(row["F_PUBLIC"]) == Convert.ToInt32((object) InheritModes.Public))
            row["F_PUBLIC"] = (object) Convert.ToInt32((object) InheritModes.Private);
        }
        applicabilitiesList2.AcceptChanges();
      }
    }
    DBRelationsApplicabilityCollection._ApplCache[key] = DataSetProcessor.CopyTable(applicabilitiesList2);
    return applicabilitiesList2;
  }

  public IDBRelationsApplicability GetApplicability(
    int relationType,
    int objectType,
    int inObjectType)
  {
    DataTable applicabilitiesList = this.GetApplicabilitiesList(relationType, objectType, inObjectType);
    return applicabilitiesList.Rows.Count == 0 ? (IDBRelationsApplicability) null : this.GetApplicability(Convert.ToInt32(applicabilitiesList.Rows[0]["F_APPLICABILITY_ID"]));
  }

  public DataRow GetApplicabilityRow(int relationType, int objectType, int inObjectType)
  {
    DataTable applicabilitiesList = this.GetApplicabilitiesList(relationType, objectType, inObjectType);
    return applicabilitiesList.Rows.Count == 0 ? (DataRow) null : applicabilitiesList.Rows[0];
  }

  public IDBRelationsApplicability GetApplicability(int applicabilityID)
  {
    return (IDBRelationsApplicability) new DBRelationsApplicability(this.UserSession, applicabilityID);
  }

  public int Create(
    RelationsApplicabilityProperties applicabilityProperties)
  {
    DBRelationsApplicabilityCollection._ApplCache.Clear();
    IDbManager dataManager = this.UserSession.DataManager;
    IDBObjectType objectType1 = this.UserSession.GetObjectType(applicabilityProperties.ObjectType);
    IDBObjectType objectType2 = this.UserSession.GetObjectType(applicabilityProperties.InObjectType);
    IDBRelationType relationType = this.UserSession.GetRelationType(applicabilityProperties.RelationType);
    string str1 = string.Format(LocalizationHolder.rm.GetString("Kernel_527"), (object) objectType1.ObjectTypeName, (object) objectType2.ObjectTypeName, (object) relationType.Description);
    (objectType1 as IDBSecurity).CheckAccess(ActionType.EditLink);
    (objectType2 as IDBSecurity).CheckAccess(ActionType.EditLink);
    this._LastEventID = this.EventHelper.AddEvent(0L, 0L, 4, (long) applicabilityProperties.InObjectType, objectType2.ObjectTypeName, string.Format(LocalizationHolder.rm.GetString("CreateApplicabilityEvent"), (object) objectType1.ObjectTypeName, (object) objectType2.ObjectTypeName, (object) relationType.Description), ActionType.EditLink, EventlogRecordType.AccessGranted, this.UserSession.UserID, this.UserSession.ComputerName, (IUserSession) this.UserSession);
    long EventID = this.EventHelper.AddEvent(0L, 0L, 4, (long) applicabilityProperties.ObjectType, objectType1.ObjectTypeName, string.Format(LocalizationHolder.rm.GetString("CreateApplicabilityEvent"), (object) objectType1.ObjectTypeName, (object) objectType2.ObjectTypeName, (object) relationType.Description), ActionType.EditLink, EventlogRecordType.AccessGranted, this.UserSession.UserID, this.UserSession.ComputerName, (IUserSession) this.UserSession);
    this.UserSession.StartTransaction();
    int int32;
    try
    {
      if (this.UserSession.DBCache.GetTable("IMS_TYPES_APPLICABILITY").Select($"F_RELATION_TYPE = {applicabilityProperties.RelationType} AND F_OBJECT_TYPE = {applicabilityProperties.ObjectType} AND F_INOBJECT_TYPE = {applicabilityProperties.InObjectType}").Length != 0)
        throw new KernelExceptionID(sc_13578.ssp_appserver_13616(1463975883), (object) applicabilityProperties.RelationType, (object) applicabilityProperties.ObjectType, (object) applicabilityProperties.InObjectType);
      dataManager.ExecuteSpNonQuery("IMS_ADD_TYPES_APPLICABILITY", dataManager.Parameter("inINOBJECT_TYPE", (object) applicabilityProperties.InObjectType), dataManager.Parameter("inOBJECT_TYPE", (object) applicabilityProperties.ObjectType), dataManager.Parameter("inRELATION_TYPE", (object) applicabilityProperties.RelationType), dataManager.Parameter("inMAX_LINKS", (object) applicabilityProperties.MaximumLinks), dataManager.Parameter("inMIN_LINKS", (object) Convert.ToInt32((object) applicabilityProperties.ApplicabilityMode)), dataManager.Parameter("inCLONE_RELATIONS", (object) Convert.ToInt32(applicabilityProperties.CloneChildRelations)), dataManager.Parameter("inCONSTRAINT_MODE", (object) Convert.ToInt32((object) applicabilityProperties.RelationConstraintMode)), dataManager.OutputParameter("outAPPLICABILITY_ID", (object) applicabilityProperties.ApplicabilityID));
      int32 = Convert.ToInt32(dataManager.GetOutputParameterValue("outAPPLICABILITY_ID"));
      DataTable dataTable = dataManager.ExecuteDataTable("SELECT * FROM IMS_TYPES_APPLICABILITY WHERE F_APPLICABILITY_ID = " + int32.ToString());
      if (dataTable.Rows.Count != 1)
        throw new KernelExceptionID(sc_13578.ssp_appserver_13617(126907576), (object) int32);
      this.UserSession.DBCache.AddRow("IMS_TYPES_APPLICABILITY", dataTable.Rows[0], (IUserSession) this.UserSession);
      IDBRelationsApplicability applicability = this.GetApplicability(int32);
      applicability.IsContent = applicabilityProperties.IsContent;
      applicability.Options = applicabilityProperties.Options;
      this.UserSession.Commit();
      (this.EventHelper as EventLogHelper).OnCreateApplicability((IUserSession) this.UserSession, applicabilityProperties);
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      string str2 = string.Format(LocalizationHolder.rm.GetString(sc_13578.ssp_appserver_13618()), (object) str1, (object) ex.Message);
      this.CloseEvent(this._LastEventID, EventlogRecordType.Error, str2);
      this.CloseEvent(EventID, EventlogRecordType.Error, str2);
      if (!(ex is AccessDeniedException))
        throw new KernelException(str2, ex);
      throw;
    }
    return int32;
  }
}
