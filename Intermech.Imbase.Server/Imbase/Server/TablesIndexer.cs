// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.TablesIndexer
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;

#nullable disable
namespace Intermech.Imbase.Server;

internal class TablesIndexer : LongLifeObject, ITablesIndexer
{
  private static object _lock = new object();
  private static TablesIndexer _instance = new TablesIndexer();

  internal static TablesIndexer Instance => TablesIndexer._instance;

  internal static void IndexTable(long tableId, UserSession session)
  {
    if (tableId < 0L)
      return;
    try
    {
      try
      {
        Monitor.Enter(TablesIndexer._lock);
        DataSet tablesInternal = TableLoadHelper.GetTablesInternal((IUserSession) session, tableId);
        if (tablesInternal == null)
          return;
        TablesIndexer.ProcessTableData(session, tableId, tablesInternal);
      }
      finally
      {
        Monitor.Exit(TablesIndexer._lock);
      }
    }
    catch (Exception ex)
    {
      session.EventLog.AddToTrace($"Ошибка обработки объекта {tableId}.{ex.Message}", 0, "Imbase.Indexing.log");
    }
  }

  private static void ProcessTableData(UserSession session, long tableId)
  {
    TablesIndexer.ProcessTableData(session, tableId, TableLoadHelper.GetTablesInternal((IUserSession) session, tableId));
  }

  internal static void ProcessTableData(UserSession session, long tableId, DataSet tableData)
  {
    if (tableId < 0L)
      return;
    List<DataColumn> dataColumnList = new List<DataColumn>();
    List<int> intList = new List<int>(32 /*0x20*/);
    DataTable table = tableData.Tables["IMS_DATA"];
    DataRowCollection rows1 = tableData.Tables["IMS_ATTR_TYPES"].Rows;
    for (int index = 0; index < rows1.Count; ++index)
    {
      string str = Convert.ToString(rows1[index]["F_ATTRIBUTE_GUID"]);
      IDBAttributeType attributeType = session.GetAttributeType(new Guid(str), false);
      if (attributeType != null)
      {
        intList.Add(attributeType.AttributeID);
        if (attributeType.AttributeType == FieldTypes.ftObjectLink)
        {
          DataColumn column = table.Columns[str];
          if (column != null)
            dataColumnList.Add(column);
        }
      }
    }
    int columnIndex = 2;
    DataTable dataTable1 = session.DataManager.ExecuteDataTable("SELECT * FROM IMS_IMBASE_ATTRS WHERE F_OBJECT_ID = " + Math.Abs(tableId).ToString());
    dataTable1.PrimaryKey = new DataColumn[2]
    {
      dataTable1.Columns[0],
      dataTable1.Columns[1]
    };
    object[] keys = new object[2]
    {
      (object) tableId,
      (object) 0
    };
    int count1 = intList.Count;
    dataTable1.Columns.Add("F_NEW", typeof (bool));
    for (int index = 0; index < count1; ++index)
    {
      int num = intList[index];
      if (num > 0)
      {
        keys[1] = (object) num;
        DataRow dataRow = dataTable1.Rows.Find(keys);
        if (dataRow == null)
          dataTable1.Rows.Add((object) tableId, (object) num, (object) true);
        else
          dataRow[columnIndex] = (object) false;
      }
    }
    IDbManager dataManager = session.DataManager;
    try
    {
      dataManager.BeginTransaction();
      DataRowCollection rows2 = dataTable1.Rows;
      int count2 = rows2.Count;
      for (int index = 0; index < count2; ++index)
      {
        DataRow dataRow = rows2[index];
        if (DBNull.Value.Equals(dataRow[columnIndex]))
          dataManager.ExecuteNonQuery($"DELETE FROM IMS_IMBASE_ATTRS WHERE F_OBJECT_ID={Convert.ToInt64(dataRow[0])} AND F_ATTRIBUTE_ID={Convert.ToInt32(dataRow[1])}");
        else if (Convert.ToBoolean(dataRow[columnIndex]))
          dataManager.ExecuteNonQuery($"INSERT INTO IMS_IMBASE_ATTRS VALUES({Convert.ToInt64(dataRow[0])},{Convert.ToInt32(dataRow[1])})");
      }
      if (dataColumnList.Count > 0)
      {
        List<string> guids = new List<string>(32 /*0x20*/);
        foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
        {
          foreach (DataColumn column in dataColumnList)
            TablesIndexer.ExtractObjectIds(row[column], guids);
        }
        long[] numArray = TablesIndexer.ConvertGuids((IUserSession) session, guids);
        DataTable dataTable2 = session.DataManager.ExecuteDataTable("SELECT * FROM IMS_IMBASE_OBJ_LINKS WHERE F_TABLE_ID = " + Math.Abs(tableId).ToString());
        dataTable2.PrimaryKey = new DataColumn[2]
        {
          dataTable2.Columns[0],
          dataTable2.Columns[1]
        };
        dataTable2.Columns.Add("F_NEW", typeof (bool));
        int length = numArray.Length;
        for (int index = 0; index < length; ++index)
        {
          long num = numArray[index];
          keys[1] = (object) num;
          DataRow dataRow = dataTable2.Rows.Find(keys);
          if (dataRow == null)
            dataTable2.Rows.Add((object) tableId, (object) num, (object) true);
          else
            dataRow[columnIndex] = (object) false;
        }
        DataRowCollection rows3 = dataTable2.Rows;
        int count3 = rows3.Count;
        for (int index = 0; index < count3; ++index)
        {
          DataRow dataRow = rows3[index];
          if (DBNull.Value.Equals(dataRow[columnIndex]))
            dataManager.ExecuteNonQuery($"DELETE FROM IMS_IMBASE_OBJ_LINKS WHERE F_TABLE_ID={Convert.ToInt64(dataRow[0])} AND F_OBJECT_ID={Convert.ToInt64(dataRow[1])}");
          else if (Convert.ToBoolean(dataRow[columnIndex]))
            dataManager.ExecuteNonQuery($"INSERT INTO IMS_IMBASE_OBJ_LINKS VALUES({Convert.ToInt64(dataRow[0])},{Convert.ToInt64(dataRow[1])})");
        }
      }
      else
        dataManager.ExecuteNonQuery($"DELETE FROM IMS_IMBASE_OBJ_LINKS WHERE F_TABLE_ID={Math.Abs(tableId)}");
      if (MetaDataHelper.GetAllAttributes4ObjectTypeList(Intermech.Imbase.Consts.ImbaseTableTypeID).FirstOrDefault<IMSAttribute4ObjectType>((System.Func<IMSAttribute4ObjectType, bool>) (x => x.AttributeID == Intermech.Imbase.Consts.ImbaseTableRecordsCountAttID)) != null)
      {
        IDBAttribute dbAttribute = session.GetObjectActualCopy(tableId, true).Attributes.AddAttribute(Intermech.Imbase.Consts.ImbaseTableRecordsCountAttID, false);
        try
        {
          dbAttribute.Value = (object) table.Rows.Count;
        }
        catch
        {
        }
      }
      if (MetaDataHelper.GetAllAttributes4ObjectTypeList(Intermech.Imbase.Consts.ImbaseTableRefTypeID).FirstOrDefault<IMSAttribute4ObjectType>((System.Func<IMSAttribute4ObjectType, bool>) (x => x.AttributeID == Intermech.Imbase.Consts.ImbaseTableRecordsCountAttID)) != null)
      {
        string commandText = $"SELECT F_OBJECT_ID FROM IMS_OBJECT_LINKS WHERE F_ATTRIBUTE_ID = {Intermech.Imbase.Consts.ImbaseTableRefAttID} AND F_TOOBJECT_ID = {tableId.ToString()}";
        foreach (DataRow row in (InternalDataCollectionBase) session.DataManager.ExecuteDataTable(commandText).Rows)
        {
          long int64 = Convert.ToInt64(row[0]);
          IDBObject objectActualCopy = session.GetObjectActualCopy(int64, false);
          if (objectActualCopy != null)
          {
            IDBAttribute dbAttribute = objectActualCopy.Attributes.AddAttribute(Intermech.Imbase.Consts.ImbaseTableRecordsCountAttID, false);
            try
            {
              dbAttribute.Value = (object) table.Rows.Count;
            }
            catch
            {
            }
          }
        }
      }
      dataManager.Commit();
    }
    catch
    {
      dataManager.Rollback();
    }
  }

  private static long[] ConvertGuids(IUserSession session, List<string> guids)
  {
    int count = guids.Count;
    List<long> longList = new List<long>(count);
    for (int index = 0; index < count; ++index)
    {
      string guid = guids[index];
      try
      {
        if (!string.IsNullOrEmpty(guid))
        {
          if (guid.Length == 36)
          {
            QuickObjectInfo objectInfo1 = session.GetObjectInfo(new Guid(guid));
            if (!objectInfo1.Empty)
            {
              QuickObjectInfo objectInfo2 = session.GetObjectInfo(objectInfo1.ObjectID);
              if (objectInfo2.Empty)
              {
                if (objectInfo1.ObjectID < 0L)
                {
                  long objectID = -objectInfo2.ObjectID;
                  objectInfo2 = session.GetObjectInfo(objectID);
                  if (!objectInfo2.Empty)
                    objectInfo1.ObjectID *= -1L;
                  else
                    continue;
                }
                else
                  continue;
              }
              if (!longList.Contains(objectInfo1.ObjectID))
                longList.Add(objectInfo1.ObjectID);
            }
          }
        }
      }
      catch
      {
      }
    }
    return longList.ToArray();
  }

  private static void ExtractObjectIds(object value, List<string> guids)
  {
    if (value == null)
      return;
    if (value is ValuesArray valuesArray)
    {
      int length = valuesArray.Length;
      for (int index = 0; index < length; ++index)
      {
        object obj = valuesArray.GetValue(index);
        if (obj != null)
        {
          try
          {
            string str = obj.ToString();
            if (!string.IsNullOrEmpty(str))
            {
              if (!guids.Contains(str))
                guids.Add(str);
            }
          }
          catch
          {
          }
        }
      }
    }
    else
    {
      if (DBNull.Value.Equals(value))
        return;
      string str = value.ToString();
      if (string.IsNullOrEmpty(str) || guids.Contains(str))
        return;
      guids.Add(str);
    }
  }

  private bool NeedUpdate(IDbManager dbManager, IEventLogHelper eventLogHelper, int version)
  {
    object obj = dbManager.ExecuteScalar("SELECT F_VERSION_ID FROM IMS_DBVERSION WHERE F_MODULE_NAME = 'IMBASE'");
    if (obj != null)
    {
      if (obj != DBNull.Value)
      {
        try
        {
          int int32 = Convert.ToInt32(obj);
          if (int32 < version && AdminUtilsService.ServerRunMode == ServerRunModes.Console)
            Console.WriteLine($"Обновляется база данных Imbase до версии {version}");
          return int32 < version;
        }
        catch (Exception ex)
        {
          if (eventLogHelper != null)
          {
            eventLogHelper.AddToTrace("Ошибка при получении текущей версии ядра: " + ex.Message, Intermech.Consts.traceAlways, string.Empty);
            goto label_9;
          }
          goto label_9;
        }
      }
    }
    if (obj == null)
    {
      dbManager.ExecuteScalar("INSERT INTO IMS_DBVERSION VALUES('IMBASE',0,0)");
      return true;
    }
label_9:
    return false;
  }

  internal static void SubscribeOnSystemEvents(IEventLogHelper elh)
  {
    elh.BeforeNextLCStepEvent += new NextLCStepHandler(TablesIndexer.OnBeforeNextLCStepEvent);
    elh.BeforePurgeObjectEvent += new ObjectEventHandler(TablesIndexer.OnBeforePurgeObjectEvent);
    elh.ChangeAttributeDataTypeEvent += new ChangeAttributeDataTypeHandler(TablesIndexer.OnChangeAttributeDataTypeEvent);
    elh.BeforeDeleteAttributeTypeEvent += new DeleteAttributeTypeHandler(TablesIndexer.OnBeforeDeleteAttributeTypeEvent);
    elh.GetUsedAttributesEvent += new GetUsedAttributesHandler(TablesIndexer.OnGetUsedAttributesEvent);
  }

  private static void OnGetUsedAttributesEvent(IUserSession iSession, UsedAttributesEventArgs args)
  {
    if (!(iSession is UserSession userSession))
      return;
    bool noLockMode = userSession.DataManager.DataProvider.NoLockMode;
    userSession.DataManager.DataProvider.NoLockMode = false;
    try
    {
      foreach (DataRow row in (InternalDataCollectionBase) userSession.DataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_IMBASE_ATTRS GROUP BY F_OBJECT_ID ORDER BY F_OBJECT_ID").Rows)
      {
        object obj = row[0];
        if (obj != null && !DBNull.Value.Equals(obj))
          args.AddAttribute(Convert.ToInt32(row[0]));
      }
    }
    finally
    {
      userSession.DataManager.DataProvider.NoLockMode = noLockMode;
    }
  }

  private static void OnBeforeDeleteAttributeTypeEvent(
    IDBAttributeType sender,
    IUserSession iSession)
  {
    if (sender == null || iSession == null || !(iSession is UserSession userSession))
      return;
    DataTable dataTable = userSession.DataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_IMBASE_ATTRS WHERE F_ATTRIBUTE_ID=" + (object) sender.AttributeID);
    int count = dataTable.Rows.Count;
    if (count != 0)
    {
      int num = Math.Min(10, count);
      StringBuilder stringBuilder = new StringBuilder(64 /*0x40*/);
      for (int index = 0; index < num; ++index)
      {
        --count;
        if (stringBuilder.Length > 0)
          stringBuilder.Append(',');
        stringBuilder.Append(dataTable.Rows[index][0].ToString());
      }
      string str = stringBuilder.ToString();
      if (count > 0)
        str += $" и еще в {count} таблицах.";
      throw new KernelException(string.Format("Нельзя удалять атрибут '{0}', т.к. он используется в таблицах IMBASE " + str, (object) sender.Name));
    }
  }

  private static void OnChangeAttributeDataTypeEvent(
    IDBAttributeType sender,
    FieldTypes newDataType,
    IUserSession iSession)
  {
    if (sender == null || iSession == null || !(iSession is UserSession session))
      return;
    DataTable dataTable1 = session.DataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_IMBASE_ATTRS WHERE F_ATTRIBUTE_ID=" + (object) sender.AttributeID);
    if (dataTable1 == null || dataTable1.Rows.Count <= 0)
      return;
    DataRowCollection rows = dataTable1.Rows;
    int count = rows.Count;
    for (int index = 0; index < count; ++index)
    {
      long int32 = (long) Convert.ToInt32(rows[index]["F_OBJECT_ID"]);
      DataSet tablesInternal = TableLoadHelper.GetTablesInternal((IUserSession) session, int32);
      DataTable table1 = tablesInternal.Tables["IMS_DATA"];
      DataTable table2 = tablesInternal.Tables["IMS_ATTR_TYPES"];
      DataTable dataTable2 = new DataTable();
      DataTable dataTable3 = dataTable2;
      AttributeTypeProperties propertiesStructure = sender.PropertiesStructure;
      string columnName1 = propertiesStructure.AttributeGuid.ToString();
      int attributeType1 = (int) newDataType;
      int num1 = TableLoadHelper.IsArray(sender) ? 1 : 0;
      DataColumn dataColumn1 = TableLoadHelper.CreateDataColumn(dataTable3, columnName1, (FieldTypes) attributeType1, num1 != 0);
      DataColumn column1 = table1.Columns[(sender as IDBGuid).GUID.ToString()];
      if (dataColumn1.DataType.Equals(column1.DataType))
      {
        if (typeof (ValuesArray).Equals(dataColumn1.DataType) && !column1.ExtendedProperties[(object) "dataType"].Equals(dataColumn1.ExtendedProperties[(object) "dataType"]))
        {
          column1.ColumnName = "$change";
          DataTable dataTable4 = dataTable2;
          propertiesStructure = sender.PropertiesStructure;
          string columnName2 = propertiesStructure.AttributeGuid.ToString();
          int attributeType2 = (int) newDataType;
          int num2 = TableLoadHelper.IsArray(sender) ? 1 : 0;
          DataColumn dataColumn2 = TableLoadHelper.CreateDataColumn(dataTable4, columnName2, (FieldTypes) attributeType2, num2 != 0);
          Type extendedProperty = dataColumn1.ExtendedProperties[(object) "dataType"] as Type;
          foreach (DataRow row in (InternalDataCollectionBase) table1.Rows)
          {
            if (row[column1] is ValuesArray valuesArray)
            {
              Array array = (Array) valuesArray.GetArray();
              TablesIndexer.ConvertValues(array, extendedProperty);
              row[dataColumn2] = (object) new ValuesArray(array, extendedProperty);
            }
            else
              row[dataColumn2] = row[column1];
          }
          table1.Columns.Remove(column1);
          dataColumn2.DefaultValue = column1.DefaultValue;
          table1.AcceptChanges();
          TableLoadHelper.StoreData((IUserSession) session, int32, tablesInternal, (ITablesIndexer) null);
        }
      }
      else
      {
        DataColumn column2 = table1.Columns.Add("$change", dataColumn1.DataType);
        TypeDescriptor.GetConverter(column2.DataType);
        foreach (DataRow row in (InternalDataCollectionBase) table1.Rows)
          row[column2] = row[column1];
        table1.Columns.Remove(column1);
        column2.ColumnName = column1.ColumnName;
        column2.DefaultValue = column1.DefaultValue;
        table1.AcceptChanges();
        TableLoadHelper.StoreData((IUserSession) session, int32, tablesInternal, (ITablesIndexer) null);
      }
    }
  }

  private static void ConvertValues(Array values, Type newType)
  {
  }

  private static void OnBeforeNextLCStepEvent(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession iSession)
  {
    if (sender == null || nextstep == null || iSession == null || nextstep.LevelID != iSession.IdentHelper.DeletedID)
      return;
    TablesIndexer.ThrowIfObjectUsedInImbase(sender, iSession);
  }

  private static void OnBeforePurgeObjectEvent(IDBObject sender, IUserSession session)
  {
    if (sender == null || session == null)
      return;
    TablesIndexer.ThrowIfObjectUsedInImbase(sender, session);
  }

  private static void ThrowIfObjectUsedInImbase(IDBObject sender, IUserSession iSession)
  {
    if (!(iSession is UserSession userSession))
      return;
    object obj = userSession.DataManager.ExecuteScalar("SELECT F_TABLE_ID FROM IMS_IMBASE_OBJ_LINKS WHERE F_OBJECT_ID = :objID", userSession.DataManager.Parameter("objID", (object) sender.ObjectID));
    if (obj != null && !DBNull.Value.Equals(obj))
    {
      long int64 = Convert.ToInt64(obj);
      throw new KernelException($"Нельзя удалять объект '{sender.Caption}' ({sender.ObjectID}), т.к. ссылка на него используется в таблице IMBASE {int64}").WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(sender.ObjectID));
    }
  }

  internal void CheckUpdateIndexes(IUserSession isession, bool forceUpdate)
  {
    int version = 1;
    UserSession serviceProvider = isession as UserSession;
    if (!forceUpdate && !this.NeedUpdate(serviceProvider.DataManager, serviceProvider.EventLogHelper, version))
      return;
    ServiceUtils.GetService<ITablesIndexerService>((object) serviceProvider, true).StartTask(isession.SessionGUID, Guid.NewGuid(), "Обновление внутренних индексов Imbase", (object) null);
  }

  public void UpdateTable(Guid sessionGuid, long tableId)
  {
    TablesIndexer.ProcessTableData(ImbaseServer.GetSession(sessionGuid) as UserSession, tableId);
  }
}
