// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Data.Repositories.ObjectRepositoryServerHandler
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Search.Data.Repositories;

public sealed class ObjectRepositoryServerHandler : LongLifeObject, IObjectRepositoryServerHandler
{
  public DataTable FindApplicabilitiesInLinks(
    Guid userSessionGuid,
    long objectVersionID,
    DBRecordSetParams @params)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
      throw new ArgumentException();
    UserSession userSession = this.GetUserSession(userSessionGuid);
    List<long> objectVersionIds = this.GetToObjectVersionIds(userSession, objectVersionID);
    return this.GetDataTable((IUserSession) userSession, @params, this.GroupObjectVersionIdsByObjectTypeID((IUserSession) userSession, objectVersionIds));
  }

  public DataTable FindApplicabilitiesInClassifiers(
    Guid userSessionGuid,
    long objectVersionID,
    DBRecordSetParams @params)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
      throw new ArgumentException();
    UserSession userSession = this.GetUserSession(userSessionGuid);
    List<long> classifierVersionIds = this.GetClassifierVersionIds(userSession, objectVersionID);
    return this.GetDataTable((IUserSession) userSession, @params, this.GroupObjectVersionIdsByObjectTypeID((IUserSession) userSession, classifierVersionIds));
  }

  private UserSession GetUserSession(Guid userSessionGuid)
  {
    return UserSession.GetSessionByID(userSessionGuid) as UserSession;
  }

  private List<long> GetToObjectVersionIds(UserSession userSession, long objectVersionID)
  {
    IDbManager dataManager = userSession.DataManager;
    IDbDataParameter dbDataParameter = dataManager.Parameter("objID", (object) objectVersionID);
    object obj = dataManager.ExecuteScalar("SELECT F_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID = :objID", dbDataParameter);
    if (obj == null || obj == DBNull.Value)
      return new List<long>();
    List<long> list = dataManager.ExecuteDataTable("select distinct F_OBJECT_ID from IMS_OBJECT_LINKS where F_TOOBJECT_ID = :objID", dbDataParameter).Rows.Cast<DataRow>().Select<DataRow, long>((System.Func<DataRow, long>) (o => DataSetProcessor.GetInt64Value(o[0], 0L))).ToList<long>();
    DataTable dataTable = dataManager.ExecuteDataTable("select distinct F_OBJECT_ID from IMS_ID_LINKS where F_TO_ID = :id111", dataManager.Parameter("id111", obj));
    list.AddRange((IEnumerable<long>) dataTable.Rows.Cast<DataRow>().Select<DataRow, long>((System.Func<DataRow, long>) (o => DataSetProcessor.GetInt64Value(o[0], 0L))).ToList<long>());
    return list;
  }

  private List<long> GetClassifierVersionIds(UserSession userSession, long objectVersionID)
  {
    return userSession.DataManager.ExecuteDataTable($"select distinct F_FOLDER_ID from IMS_SELECTIONS where F_OBJECT_ID = {Math.Abs(objectVersionID)}").Rows.Cast<DataRow>().Select<DataRow, long>((System.Func<DataRow, long>) (o => DataSetProcessor.GetInt64Value(o[0], 0L))).ToList<long>();
  }

  private Dictionary<int, List<long>> GroupObjectVersionIdsByObjectTypeID(
    IUserSession userSession,
    List<long> objectVersionIds)
  {
    Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>();
    foreach (long objectVersionId in objectVersionIds)
    {
      IDBObject dbObject = userSession.GetObject(objectVersionId);
      List<long> longList = (List<long>) null;
      dictionary.TryGetValue(dbObject.ObjectType, out longList);
      if (longList == null)
      {
        longList = new List<long>();
        dictionary.Add(dbObject.ObjectType, longList);
      }
      longList.Add(objectVersionId);
    }
    return dictionary;
  }

  private DataTable GetDataTable(
    IUserSession userSession,
    DBRecordSetParams @params,
    Dictionary<int, List<long>> objectVersionIdsGroupedByObjectTypeID)
  {
    ConditionStructure[] conditions = @params.Conditions;
    @params.RecordCount = -1;
    DataTable dataTable1 = (DataTable) null;
    foreach (KeyValuePair<int, List<long>> keyValuePair in objectVersionIdsGroupedByObjectTypeID)
    {
      int key = keyValuePair.Key;
      List<long> longList = keyValuePair.Value;
      @params.Conditions = ConditionStructure.Join(new ConditionStructure()
      {
        Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        LogicalOperator = LogicalOperators.AND,
        RelationalOperator = RelationalOperators.In,
        SQL = "",
        Value = (object) longList.ToArray()
      }, conditions);
      DataTable dataTable2 = userSession.GetObjectCollection(key).Select(@params);
      if (dataTable1 == null)
      {
        dataTable1 = dataTable2;
        dataTable1.BeginLoadData();
      }
      else
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
          dataTable1.LoadDataRow(row.ItemArray, false);
      }
    }
    dataTable1?.EndLoadData();
    return dataTable1;
  }
}
