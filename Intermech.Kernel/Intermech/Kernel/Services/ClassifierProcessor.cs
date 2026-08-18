// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ClassifierProcessor
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace Intermech.Kernel.Services;

internal class ClassifierProcessor
{
  private const int folderKeyLength = 250;
  private const string tabl = "IMS_SELECTIONS";
  private const string fFld = "F_FOLDER_ID";
  private const string fKey = "F_FOLDER_KEY";
  private const string fObj = "F_OBJECT_ID";
  private const string fFid = "F_ID";
  private const string parameterPrefix = "V_";
  private const string pFld = "V_F_FOLDER_ID";
  private const string pObj = "V_F_OBJECT_ID";
  private const string pFid = "V_F_ID";
  private const string pKey = "V_F_FOLDER_KEY";
  private const string qCount = "SELECT COUNT(*) FROM {0} WHERE {1} = :{2} AND {3} = :{4}";
  private const string qSelect = "SELECT {0} FROM {1} WHERE {2} = :{3}";
  private const string qInsert = "INSERT INTO {0} ({1}, {2}, {3}, {4}) VALUES (:{5}, :{6}, :{7}, :{8})";
  private const string qDelete = "DELETE FROM {0} WHERE {1} = :{2} AND {3} = :{4}";
  private const string qUpdate = "UPDATE {0} SET {1} = :{2} WHERE {3} = :{4}";

  internal static void DoAddBlankObject(
    UserSession userSession,
    long folderID,
    long objectID,
    long id,
    string folderKey)
  {
    IDbManager dataManager = userSession.DataManager;
    dataManager.ExecuteNonQuery("INSERT INTO IMS_SELECTIONS (F_FOLDER_ID, F_OBJECT_ID, F_ID, F_FOLDER_KEY) VALUES (:fldID, :objID, :fID1, :fldKey)", dataManager.Parameter("fldID", (object) folderID), dataManager.Parameter("objID", (object) Math.Abs(objectID)), dataManager.Parameter("fID1", (object) id), dataManager.Parameter("fldKey", (object) folderKey));
  }

  public static void DoAdd(
    UserSession userSession,
    long folderID,
    long[] objectIDs,
    string folderKey)
  {
    IDbManager dataManager = userSession.DataManager;
    objectIDs = ((IEnumerable<long>) objectIDs).Distinct<long>().ToArray<long>();
    List<IDbDataParameter> dbDataParameterList = new List<IDbDataParameter>();
    DataTable toTable = (DataTable) null;
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      int index = 0;
      int num = 0;
      while (index < objectIDs.Length)
      {
        stringBuilder.AppendFormat(":parID{0},", (object) index);
        dbDataParameterList.Add(dataManager.Parameter("parID" + index.ToString(), (object) Math.Abs(objectIDs[index])));
        if (++index == objectIDs.Length || ++num == dataManager.DataProvider.MaximumINOperands)
        {
          --stringBuilder.Length;
          dbDataParameterList.Add(dataManager.Parameter("fldID", (object) folderID));
          DataTable dataTable = dataManager.ExecuteDataTable($"SELECT O.F_OBJECT_ID, O.F_ID FROM IMS_OBJECTS O WHERE O.F_OBJECT_ID IN ({stringBuilder.ToString()}) AND NOT EXISTS(SELECT S.F_OBJECT_ID FROM IMS_SELECTIONS S WHERE S.F_FOLDER_ID = :fldID AND S.F_OBJECT_ID = O.F_OBJECT_ID)", dbDataParameterList.ToArray());
          if (toTable == null)
            toTable = dataTable;
          else
            SqlHelper.AssignRows(toTable, (IEnumerable<DataRow>) dataTable.Select());
          stringBuilder.Clear();
          dbDataParameterList.Clear();
          num = 0;
        }
      }
    }
    if (toTable == null || toTable.Rows.Count <= 0)
      return;
    userSession.StartTransaction();
    try
    {
      for (int index = 0; index < toTable.Rows.Count; ++index)
        dataManager.AddBatchSQL("INSERT INTO IMS_SELECTIONS (F_FOLDER_ID, F_OBJECT_ID, F_ID, F_FOLDER_KEY) VALUES (:fldID, :objID, :fID1, :fldKey)", new DbCommandParam[4]
        {
          new DbCommandParam("fldID", DbType.Int64, (object) folderID),
          new DbCommandParam("objID", DbType.Int64, toTable.Rows[index][0]),
          new DbCommandParam("fID1", DbType.Int64, toTable.Rows[index][1]),
          new DbCommandParam("fldKey", DbType.String, (object) folderKey)
        });
      dataManager.ExecuteBatchSQL();
      userSession.Commit();
    }
    catch
    {
      userSession.Rollback();
      throw;
    }
  }

  public static void Add(
    UserSession userSession,
    long folderID,
    long[] objectIDs,
    string folderKey)
  {
    IDBObject classificator = userSession.GetObject(folderID);
    (classificator as DBObject).CheckAccess(ActionType.IncludeInComposition);
    ClassifierProcessor.CheckEnableFolder((IUserSession) userSession, classificator);
    ClassifierProcessor.DoAdd(userSession, folderID, objectIDs, folderKey);
  }

  public static void DeleteFromClassifier(
    UserSession userSession,
    string folderKey,
    long[] objectIDs)
  {
    IDbManager dataManager = userSession.DataManager;
    DataTable toTable = (DataTable) null;
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      List<IDbDataParameter> dbDataParameterList = new List<IDbDataParameter>();
      int index = 0;
      int num = 0;
      while (index < objectIDs.Length)
      {
        stringBuilder.AppendFormat(":parID{0},", (object) index);
        dbDataParameterList.Add(dataManager.Parameter("parID" + index.ToString(), (object) Math.Abs(objectIDs[index])));
        if (++index == objectIDs.Length || ++num == dataManager.DataProvider.MaximumINOperands)
        {
          --stringBuilder.Length;
          dbDataParameterList.Add(dataManager.Parameter("fldKey", (object) (folderKey + "%")));
          DataTable dataTable = dataManager.ExecuteDataTable($"SELECT S.F_FOLDER_ID, S.F_OBJECT_ID FROM IMS_SELECTIONS S WHERE S.F_FOLDER_KEY LIKE :fldKey AND S.F_ID IN (SELECT O.F_ID FROM IMS_OBJECTS O WHERE O.F_OBJECT_ID IN ({stringBuilder.ToString()}))", dbDataParameterList.ToArray());
          if (toTable == null)
            toTable = dataTable;
          else
            SqlHelper.AssignRows(toTable, (IEnumerable<DataRow>) dataTable.Select());
          stringBuilder.Clear();
          dbDataParameterList.Clear();
          num = 0;
        }
      }
    }
    if (toTable == null || toTable.Rows.Count <= 0)
      return;
    userSession.StartTransaction();
    try
    {
      for (int index = 0; index < toTable.Rows.Count; ++index)
        dataManager.AddBatchSQL("DELETE FROM IMS_SELECTIONS WHERE F_FOLDER_ID = :fldID AND F_OBJECT_ID = :objID", new DbCommandParam[2]
        {
          new DbCommandParam("fldID", DbType.Int64, toTable.Rows[index][0]),
          new DbCommandParam("objID", DbType.Int64, toTable.Rows[index][1])
        });
      dataManager.ExecuteBatchSQL();
      userSession.Commit();
    }
    catch
    {
      userSession.Rollback();
      throw;
    }
  }

  internal static void DoDelete(UserSession userSession, long folderID, long[] objectIDs)
  {
    IDbManager dataManager = userSession.DataManager;
    userSession.StartTransaction();
    try
    {
      foreach (long objectId in objectIDs)
        dataManager.AddBatchSQL("DELETE FROM IMS_SELECTIONS WHERE F_FOLDER_ID = :fldID AND F_OBJECT_ID = :objID", new DbCommandParam[2]
        {
          new DbCommandParam("fldID", DbType.Int64, (object) folderID),
          new DbCommandParam("objID", DbType.Int64, (object) Math.Abs(objectId))
        });
      dataManager.ExecuteBatchSQL();
      userSession.Commit();
    }
    catch
    {
      userSession.Rollback();
      throw;
    }
  }

  internal static void DoDeleteByID(UserSession userSession, long folderID, long[] IDs)
  {
    IDbManager dataManager = userSession.DataManager;
    userSession.StartTransaction();
    try
    {
      foreach (long id in IDs)
        dataManager.AddBatchSQL("DELETE FROM IMS_SELECTIONS WHERE F_FOLDER_ID = :fldID AND F_ID = :fID1", new DbCommandParam[2]
        {
          new DbCommandParam("fldID", DbType.Int64, (object) folderID),
          new DbCommandParam("fID1", DbType.Int64, (object) id)
        });
      dataManager.ExecuteBatchSQL();
      userSession.Commit();
    }
    catch
    {
      userSession.Rollback();
      throw;
    }
  }

  public static void Delete(UserSession userSession, long folderID, long[] objectIDs)
  {
    (userSession.GetObject(folderID) as DBObject).CheckAccess(ActionType.ExcludeFromComposition);
    ClassifierProcessor.DoDelete(userSession, folderID, objectIDs);
  }

  public static void DeleteByID(UserSession userSession, long folderID, long[] IDs)
  {
    (userSession.GetObject(folderID) as DBObject).CheckAccess(ActionType.ExcludeFromComposition);
    ClassifierProcessor.DoDeleteByID(userSession, folderID, IDs);
  }

  public static bool Exists(UserSession userSession, long folderID, long objectID)
  {
    IDbManager dataManager = userSession.DataManager;
    string commandText = $"SELECT COUNT(*) FROM {"IMS_SELECTIONS"} WHERE {"F_FOLDER_ID"} = :{"V_F_FOLDER_ID"} AND {"F_OBJECT_ID"} = :{"V_F_OBJECT_ID"}";
    return Convert.ToInt32(dataManager.ExecuteScalar(commandText, dataManager.Parameter("V_F_FOLDER_ID", (object) folderID), dataManager.Parameter("V_F_OBJECT_ID", (object) Math.Abs(objectID)))) > 0;
  }

  public static long[] ExistsObjectsID(UserSession userSession, long folderID, long[] objectIDs)
  {
    objectIDs = ((IEnumerable<long>) objectIDs).Distinct<long>().ToArray<long>();
    List<long> longList = new List<long>();
    IDbManager dataManager = userSession.DataManager;
    int index1 = 0;
    int num = 0;
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      while (index1 < objectIDs.Length)
      {
        stringBuilder.Append(Math.Abs(objectIDs[index1]).ToString() + ",");
        if (++index1 == objectIDs.Length || ++num == dataManager.DataProvider.MaximumINOperands)
        {
          --stringBuilder.Length;
          DataTable dataTable = dataManager.ExecuteDataTable($"SELECT F_OBJECT_ID FROM IMS_SELECTIONS WHERE F_FOLDER_ID = :fldID AND F_OBJECT_ID IN ({stringBuilder.ToString()})", dataManager.Parameter("fldID", (object) folderID));
          for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
            longList.Add(Convert.ToInt64(dataTable.Rows[index2][0]));
          num = 0;
          stringBuilder.Clear();
        }
      }
    }
    return longList.ToArray();
  }

  public static void CheckEnableFolder(IUserSession session, IDBObject classificator)
  {
    bool flag = true;
    long rootClassifier = ServerServices.ServiceContainer.GetService<ISelectionsService>().GetRootClassifier((object) session, classificator);
    IDBObject dbObject = rootClassifier != classificator.ObjectID ? session.GetObject(rootClassifier) : classificator;
    IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0156e-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid != null && attributeByGuid.AsBoolean)
    {
      int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cad0014d-306c-11d8-b4e9-00304f19f545");
      IDBAttribute attributeById = classificator.GetAttributeByID(attributeTypeId);
      if (attributeById != null && !string.IsNullOrEmpty(attributeById.AsString))
        flag = session.GetObjectCollection(new Guid("cad00150-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[2]
        {
          new ConditionStructure(attributeTypeId, RelationalOperators.StartString, (object) attributeById.AsString, LogicalOperators.AND, 0, true),
          new ConditionStructure(-2, RelationalOperators.NotEqual, (object) classificator.ObjectID, LogicalOperators.AND, 0, false)
        }, new object[1]{ (object) -2 })).Rows.Count == 0;
    }
    if (!flag)
      throw new Exception($"Классификация для {dbObject.NameInMessages} возможна только в последнюю папку");
  }
}
