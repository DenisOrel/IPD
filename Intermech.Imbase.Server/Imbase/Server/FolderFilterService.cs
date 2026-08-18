// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.FolderFilterService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Filters;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

#nullable disable
namespace Intermech.Imbase.Server;

internal class FolderFilterService : LongLifeObject, IFolderFilterService, ICommonFilterService
{
  internal Dictionary<long, IMSLifeCycleStep> _obj2LCStepBefore = new Dictionary<long, IMSLifeCycleStep>();
  protected DataTable _filterData;
  protected DateTime _filterDate;

  protected void InitializeData() => this._filterDate = DateTime.Now;

  private static DataTable CreateEmptyTable() => FolderFilterService.CreateFilterTableOnly();

  internal static DataTable CreateFilterTableAll(string tableName)
  {
    return new DataTable("filterAll")
    {
      Columns = {
        {
          "F_OBJECT_ID",
          typeof (long)
        },
        {
          "F_OBJECT_GUID",
          typeof (string)
        },
        {
          "F_OBJECT_TYPE",
          typeof (int)
        },
        {
          "F_PATH",
          typeof (string)
        },
        {
          "F_GUID",
          typeof (string)
        },
        {
          "F_OWNER",
          typeof (string)
        }
      },
      RemotingFormat = SerializationFormat.Binary
    };
  }

  protected static DataTable CreateFilterTableOnly()
  {
    return FolderFilterService.CreateFilterTableOnly("filter");
  }

  protected static DataTable CreateFilterTableOnly(string tableName)
  {
    return new DataTable(tableName)
    {
      Columns = {
        {
          "F_GUID",
          typeof (string)
        },
        {
          "F_OWNER",
          typeof (string)
        }
      },
      RemotingFormat = SerializationFormat.Binary
    };
  }

  protected static void SaveFiltersTableOnly(IDBObject folderObject, DataTable filterData)
  {
    if (folderObject == null || filterData == null)
      return;
    IDBAttribute dbAttribute = folderObject.GetAttributeByID(Intermech.Imbase.Consts.FilterBlobAttId);
    if (filterData.Rows.Count > 0)
    {
      if (dbAttribute == null)
        dbAttribute = folderObject.Attributes.AddAttribute(Intermech.Imbase.Consts.FilterBlobAttId, false);
      filterData.RemotingFormat = SerializationFormat.Binary;
      using (MemoryStream memoryStream = new MemoryStream(32000))
      {
        new BinaryFormatter().Serialize((Stream) memoryStream, (object) filterData);
        using (MemoryStream outStream = new MemoryStream(Convert.ToInt32(memoryStream.Length / 2L)))
        {
          ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).PackStream((Stream) outStream, (Stream) memoryStream, Convert.ToInt32((object) ZLibCompressLevels.LevelMax));
          IBlobWriter blobWriter = dbAttribute as IBlobWriter;
          blobWriter.OpenBlob(new BlobInformation(memoryStream.Length, outStream.Length, DateTime.Now, string.Empty, ArcMethods.ZLibPacked, "filter"), false);
          blobWriter.WriteDataBlock(outStream.ToArray());
        }
      }
    }
    else
      dbAttribute?.Delete(0L);
  }

  protected static DataTable LoadFiltersTableOnly(IDBObject folderObject)
  {
    if (folderObject == null)
      return (DataTable) null;
    IDBAttribute attributeById = folderObject.GetAttributeByID(Intermech.Imbase.Consts.FilterBlobAttId);
    if (attributeById == null || !(attributeById is IDBShortBlobAttribute shortBlobAttribute))
      return FolderFilterService.CreateEmptyTable();
    DataTable dataTable = (DataTable) null;
    ShortBlobValue blobValue = shortBlobAttribute.GetBlobValue();
    if (blobValue.RealFileSize > 0L)
    {
      byte[] buffer = blobValue.Value;
      if (buffer != null)
      {
        MemoryStream memoryStream = (MemoryStream) null;
        try
        {
          using (MemoryStream inStream = new MemoryStream(buffer))
          {
            if (blobValue.ArcMethod == ArcMethods.NotPacked)
            {
              memoryStream = inStream;
            }
            else
            {
              memoryStream = new MemoryStream(Convert.ToInt32(blobValue.RealFileSize));
              ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) memoryStream, (Stream) inStream);
            }
            memoryStream.Position = 0L;
            dataTable = (DataTable) new BinaryFormatter().Deserialize((Stream) memoryStream);
            dataTable.RemotingFormat = SerializationFormat.Binary;
          }
        }
        catch (Exception ex)
        {
          if (ServerServices.GetService(typeof (IOutputView)) is IOutputView service)
          {
            string text = string.Format(LocalizationHolder.rm.GetString("Imbase.Server_24"), (object) folderObject.ObjectID);
            service.WriteString("IMBASE", text);
            service.WriteString("IMBASE", ex.Message);
          }
        }
        finally
        {
          memoryStream?.Close();
        }
      }
    }
    return dataTable;
  }

  protected static ColumnContents[] CreateContents(bool checkBlobs)
  {
    if (!checkBlobs)
      return (ColumnContents[]) null;
    ColumnContents[] contents = new ColumnContents[7];
    contents[6] = ColumnContents.ID;
    return contents;
  }

  protected static object[] GetColumns(bool checkBlobs)
  {
    return !checkBlobs ? new object[6]
    {
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_ID),
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_TYPE),
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.CAPTION),
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_LC_STEP),
      (object) Intermech.Imbase.Consts.ClassifFolderKeyAttId,
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_GUID)
    } : new object[7]
    {
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_ID),
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_TYPE),
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.CAPTION),
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_LC_STEP),
      (object) Intermech.Imbase.Consts.ClassifFolderKeyAttId,
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_GUID),
      (object) Intermech.Imbase.Consts.FilterBlobAttId
    };
  }

  protected static void RenameColumns(DataTable resultTable, bool checkBlobs)
  {
    DataColumnCollection columns = resultTable.Columns;
    columns[0].ColumnName = "F_OBJECT_ID";
    columns[1].ColumnName = "F_OBJECT_TYPE";
    columns[2].ColumnName = "CAPTION";
    columns[3].ColumnName = "F_SORT";
    columns[4].ColumnName = "F_PATH";
    columns[5].ColumnName = "F_GUID";
    if (!checkBlobs)
      return;
    columns[6].ColumnName = "F_BLOBID";
  }

  protected void FilterData_SynchonizationLoad(IUserSession session)
  {
    if (session == null || this._filterData == null)
      return;
    BlobInformation config_info;
    byte[] config_file;
    session.Configurations.LoadConfigData("FolderFilterService.FilterData", out config_info, out config_file, 0L);
    if (config_info.RealFileSize == 0L || config_info.ModifyDate <= this._filterDate || config_file == null || config_file.Length == 0)
      return;
    lock (this)
    {
      this._filterDate = config_info.ModifyDate;
      using (MemoryStream serializationStream = new MemoryStream(config_file))
      {
        serializationStream.Position = 0L;
        this._filterData = (DataTable) new BinaryFormatter().Deserialize((Stream) serializationStream);
        this._filterData.RemotingFormat = SerializationFormat.Binary;
      }
    }
  }

  protected void FilterData_SynchonizationSave(IUserSession session)
  {
    if (session == null || this._filterData == null)
      return;
    lock (this)
    {
      using (MemoryStream serializationStream = new MemoryStream(32000))
      {
        this._filterData.RemotingFormat = SerializationFormat.Binary;
        new BinaryFormatter().Serialize((Stream) serializationStream, (object) this._filterData);
        IUserSession userSession = (IUserSession) null;
        if (!session.IsAdmin)
        {
          userSession = ServiceUtils.GetService<IDBTimedEvents>((object) ApplicationServices.Container, true).GetSystemSessionTemporaryClone("imbase.filter.save");
          session = userSession;
        }
        try
        {
          session.Configurations.WriteConfigData(new BlobInformation(serializationStream.Length, serializationStream.Length, DateTime.Now, "FolderFilterService.FilterData", ArcMethods.NotPacked, string.Empty), serializationStream.ToArray(), 0L);
        }
        finally
        {
          userSession?.Logout("imbase.filter.save");
        }
      }
    }
  }

  public FolderFilterService()
    : this((IUserSession) null)
  {
  }

  public FolderFilterService(IUserSession session)
  {
    this._filterData = FolderFilterService.CreateFilterTableAll("filter_data");
    this.InitializeData();
    FolderFilterService.FilterThreadLoader filterThreadLoader = new FolderFilterService.FilterThreadLoader(session, ServiceUtils.GetService<IApplicationStateEventsService>((object) ApplicationServices.Container, true), ref this._filterData);
  }

  string[] IFolderFilterService.GetFilter(
    Guid sessionGuid,
    Guid folderId,
    long catalogId,
    string ownerGuid)
  {
    List<string> stringList = new List<string>(32 /*0x20*/);
    if (sessionGuid == Guid.Empty || this._filterData == null)
      return stringList.ToArray();
    this.FilterData_SynchonizationLoad(ImbaseServer.GetSession(sessionGuid));
    if (this._filterData == null || this._filterData.Rows.Count == 0)
      return stringList.ToArray();
    foreach (DataRow dataRow in this._filterData.Select($"{FolderFilterService.GetFolderSQLCond(folderId)} AND {FolderFilterService.GetOwnerSQLCond(ownerGuid)}"))
    {
      if (dataRow != null)
        stringList.Add(dataRow["F_GUID"].ToString());
    }
    return stringList.ToArray();
  }

  public DataTable GetFilter(Guid sessionGuid, string filterCond)
  {
    if (sessionGuid == Guid.Empty)
      return (DataTable) null;
    if (this._filterData == null)
      return (DataTable) null;
    this.FilterData_SynchonizationLoad(ImbaseServer.GetSession(sessionGuid));
    if (this._filterData == null || this._filterData.Rows.Count == 0)
      return (DataTable) null;
    DataTable filter = this._filterData.Copy();
    filter.RemotingFormat = SerializationFormat.Binary;
    if (filterCond != string.Empty)
    {
      filter.BeginLoadData();
      try
      {
        filter.Clear();
        DataRow[] dataRowArray = this._filterData.Select(filterCond);
        if (dataRowArray.Length == 0)
          return filter;
        foreach (DataRow row in dataRowArray)
          filter.ImportRow(row);
        filter.AcceptChanges();
      }
      finally
      {
        filter.EndLoadData();
      }
    }
    return filter;
  }

  bool IFolderFilterService.SetFilter(
    Guid sessionGuid,
    Guid folderGuid,
    string ownerGuid,
    string[] addValues,
    string[] delValues)
  {
    if (sessionGuid == Guid.Empty || folderGuid == Guid.Empty || (addValues == null || addValues.Length == 0) && (delValues == null || delValues.Length == 0) || this._filterData == null)
      return false;
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    if (!session.IsAdmin)
    {
      Guid guid = !string.IsNullOrEmpty(ownerGuid) ? new Guid(ownerGuid) : Guid.Empty;
      IDBObject dbObject = session.GetObject(session.UserID);
      if (dbObject == null || dbObject.GUID != guid && dbObject.ObjectGUID != guid)
        return false;
    }
    DataTable emptyTable = FolderFilterService.CreateEmptyTable();
    string filterExpression = $"{FolderFilterService.GetFolderSQLCond(folderGuid)} AND {FolderFilterService.GetOwnerSQLCond(ownerGuid)}";
    IDBObject folderObject = session.GetObject(folderGuid, false);
    if (folderObject == null)
      return false;
    this.FilterData_SynchonizationLoad(session);
    DataRow[] collection = this._filterData.Select(filterExpression);
    List<DataRow> source = new List<DataRow>((IEnumerable<DataRow>) collection);
    lock (this)
    {
      IDbManager dataManager = ((UserSession) session).DataManager;
      string empty = string.Empty;
      if (collection.Length != 0)
        empty = collection[0]["F_PATH"].ToString();
      dataManager.BeginTransaction();
      try
      {
        int guidIdx;
        if (delValues != null && delValues.Length != 0)
        {
          HashSet<string> stringSet = new HashSet<string>((IEnumerable<string>) delValues);
          int count = source.Count;
          guidIdx = this._filterData.Columns.IndexOf("F_GUID");
          for (int index = count - 1; index >= 0; --index)
          {
            DataRow dataRow = source[index];
            if (stringSet.Contains(dataRow[guidIdx].ToString()))
            {
              source.RemoveAt(index);
              dataRow.Delete();
            }
          }
        }
        if (addValues != null && addValues.Length != 0)
        {
          if (empty == string.Empty)
          {
            IDBAttribute attributeById = folderObject.GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId);
            if (attributeById != null)
              empty = attributeById.Value.ToString();
          }
          guidIdx = this._filterData.Columns.IndexOf("F_GUID");
          int count = source.Count;
          HashSet<string> stringSet = new HashSet<string>(source.Select<DataRow, string>((System.Func<DataRow, string>) (row => row[guidIdx].ToString())));
          int length = addValues.Length;
          for (int index = 0; index < length; ++index)
          {
            string addValue = addValues[index];
            if (!stringSet.Contains(addValue))
            {
              DataRow dataRow = this._filterData.Rows.Add((object) folderObject.ObjectID, (object) folderObject.ObjectGUID.ToString(), (object) folderObject.ObjectType, (object) empty, (object) addValue, (object) ownerGuid);
              source.Add(dataRow);
            }
          }
        }
        guidIdx = this._filterData.Columns.IndexOf("F_GUID");
        int columnIndex = this._filterData.Columns.IndexOf("F_OWNER");
        foreach (DataRow dataRow in source)
        {
          if (dataRow != null)
            emptyTable.Rows.Add(dataRow[guidIdx], dataRow[columnIndex]);
        }
        foreach (DataRow dataRow in this._filterData.Select($"{FolderFilterService.GetFolderSQLCond(folderGuid)} AND NOT{FolderFilterService.GetOwnerSQLCond(ownerGuid)}"))
        {
          if (dataRow != null)
            emptyTable.Rows.Add(dataRow[guidIdx], dataRow[columnIndex]);
        }
        emptyTable.AcceptChanges();
        this._filterData.AcceptChanges();
        FolderFilterService.SaveFiltersTableOnly(folderObject, emptyTable);
        dataManager.Commit();
      }
      catch
      {
        dataManager.Rollback();
        throw;
      }
    }
    this.FilterData_SynchonizationSave(session);
    return emptyTable.Rows.Count > 0;
  }

  DataTable IFolderFilterService.LoadFoldersFor(
    Guid sessionGuid,
    long folderId,
    string ownerGuid,
    long catalogId)
  {
    if (sessionGuid == Guid.Empty || folderId == 0L || this._filterData == null)
      return (DataTable) null;
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    if (session == null)
      return (DataTable) null;
    this.FilterData_SynchonizationLoad(session);
    DataRow[] dataRowArray = this._filterData.Select($"{FolderFilterService.GetFolderSQLCond(folderId)} AND {FolderFilterService.GetOwnerSQLCond(ownerGuid)}");
    if (dataRowArray.Length == 0)
      return FolderFilterService.GetTopLevelNodes(session, catalogId);
    IDBObject dbObject = session.GetObject(catalogId, false);
    if (dbObject == null)
      return (DataTable) null;
    IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId);
    if (attributeById == null)
      return (DataTable) null;
    string conditionValue = attributeById.AsString.Substring(0, 2);
    HashSet<string> source = new HashSet<string>();
    int columnIndex = this._filterData.Columns.IndexOf("F_GUID");
    foreach (DataRow dataRow in dataRowArray)
    {
      string text = dataRow[columnIndex].ToString();
      if (!string.IsNullOrEmpty(text) && GuidHelper.IsGuid(text))
        source.Add(text);
    }
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseFolderTypeID);
    DBRecordSetParams paramsSet = ImbaseServer.CreateParamsSet(new ConditionStructure[2]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) conditionValue, LogicalOperators.AND, 0, false),
      new ConditionStructure(Convert.ToInt32((object) ObligatoryObjectAttributes.F_GUID), RelationalOperators.In, (object) source.ToArray<string>(), LogicalOperators.NONE, 0, false)
    });
    DataTable dataTable = objectCollection.Select(paramsSet);
    if (dataTable == null)
      return (DataTable) null;
    ImbaseServer.AppendFilterColumn(dataTable);
    if (dataTable.Columns.Count > 0)
      ImbaseServer.RenameColumns(dataTable);
    if (dataTable.Rows.Count > 0)
      ImbaseServer.BuildUpTree(dataTable, session, paramsSet);
    dataTable.AcceptChanges();
    dataTable.RemotingFormat = SerializationFormat.Binary;
    return dataTable;
  }

  private static DataTable GetTopLevelNodes(IUserSession session, long catalogId)
  {
    return ImbaseServer.Instance.GetSubfolders(session.SessionGUID, catalogId, (int[]) null);
  }

  public DataTable ApplyFilter(
    Guid sessionGuid,
    long filterObjId,
    string ownerGuid,
    DataTable dataTable,
    HybridDictionary extArgs = null)
  {
    if (sessionGuid == Guid.Empty || filterObjId == 0L || dataTable == null)
      return (DataTable) null;
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    if (session == null)
      return (DataTable) null;
    DataTable dataTable1 = dataTable.Copy();
    dataTable1.RemotingFormat = SerializationFormat.Binary;
    IDBObject dbObject = session.GetObject(filterObjId);
    if (dbObject == null)
      return dataTable1;
    this.FilterData_SynchonizationLoad(session);
    DataRow[] filterRows = this._filterData.Select($"{FolderFilterService.GetFolderSQLCond(filterObjId)} AND {FolderFilterService.GetOwnerSQLCond(ownerGuid)}");
    if (!FolderFilterService.IsFoldersContainsFilter(dataTable, filterRows, sessionGuid))
    {
      IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId);
      if (attributeById != null && attributeById.Value != DBNull.Value)
      {
        string str = attributeById.Value.ToString();
        if (!str.Equals(string.Empty) && str.Length > 2)
        {
          string nodePath = str;
          while (nodePath.Length > 2)
          {
            nodePath = str.Substring(0, nodePath.Length - 2);
            filterRows = this._filterData.Select($"{FolderFilterService.GetPathSQLCond(nodePath)} AND {FolderFilterService.GetOwnerSQLCond(ownerGuid)}");
            if (filterRows.Length != 0 && FolderFilterService.IsFoldersContainsFilter(dataTable, filterRows, sessionGuid))
              break;
          }
        }
      }
      if (filterRows.Length == 0)
        return dataTable1;
    }
    HashSet<string> stringSet = new HashSet<string>();
    int columnIndex = dataTable1.Columns.IndexOf("F_PATH");
    if (columnIndex == -1)
      return dataTable1;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
    {
      string str = Convert.ToString(row[columnIndex]).Substring(0, 2);
      if (!str.Equals(string.Empty))
        stringSet.Add(str);
    }
    return FolderFilterService.ApplyFilter(sessionGuid, filterRows, dataTable1);
  }

  public static string GetOwnerSQLCond(string ownerGuid)
  {
    return $"( {(string.IsNullOrEmpty(ownerGuid) ? "F_OWNER='' OR F_OWNER IS NULL" : $"F_OWNER='{ownerGuid}'")} )";
  }

  public static string GetFolderSQLCond(long objectId) => $"( {$"F_OBJECT_ID='{objectId}'"} )";

  public static string GetFolderSQLCond(Guid objectGuid)
  {
    return $"( {$"F_OBJECT_GUID ='{objectGuid}'"} )";
  }

  public static string GetPathSQLCond(string nodePath) => $"( {$"F_PATH ='{nodePath}'"} )";

  internal static DataTable LoadFoldersForFilterOnly(Guid sessionGuid, DataRow[] filterRows)
  {
    if (sessionGuid == Guid.Empty || filterRows == null || filterRows.Length == 0)
      return (DataTable) null;
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    if (session == null)
      return (DataTable) null;
    HashSet<Guid> source = new HashSet<Guid>();
    int columnIndex = filterRows[0].Table.Columns.IndexOf("F_GUID");
    foreach (DataRow filterRow in filterRows)
    {
      string str = filterRow[columnIndex].ToString();
      if (!string.IsNullOrEmpty(str) && GuidHelper.IsGuid(str))
        source.Add(new Guid(str));
    }
    ConditionStructure[] conds = new ConditionStructure[1]
    {
      new ConditionStructure(Convert.ToInt32((object) ObligatoryObjectAttributes.F_GUID), RelationalOperators.In, (object) source.ToArray<Guid>(), LogicalOperators.NONE, 0, false)
    };
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseFolderTypeID);
    DBRecordSetParams paramsSet = ImbaseServer.CreateParamsSet(conds);
    if (objectCollection == null)
      return (DataTable) null;
    DataTable resultTable = objectCollection.Select(paramsSet);
    ImbaseServer.AppendFilterColumn(resultTable);
    if (resultTable != null)
      ImbaseServer.RenameColumns(resultTable);
    if (resultTable != null)
    {
      resultTable.AcceptChanges();
      resultTable.RemotingFormat = SerializationFormat.Binary;
    }
    return resultTable;
  }

  internal static bool IsFoldersContainsFilter(
    DataTable folderTable,
    DataRow[] filterRows,
    Guid sessionGuid)
  {
    if (folderTable == null || folderTable.Rows.Count == 0 || filterRows == null || filterRows.Length == 0 || sessionGuid == Guid.Empty || ImbaseServer.GetSession(sessionGuid) == null)
      return false;
    DataTable source = FolderFilterService.LoadFoldersForFilterOnly(sessionGuid, filterRows);
    if (source == null || source.Rows.Count == 0)
      return false;
    HashSet<string> bucket = new HashSet<string>();
    ImbaseHelper.CollectAllClassificatorsCollection((ICollection<string>) bucket, (IEnumerable<string>) source.AsEnumerable().Select<DataRow, string>((System.Func<DataRow, string>) (row => row["F_PATH"].ToString())));
    foreach (DataRow row in (InternalDataCollectionBase) folderTable.Rows)
    {
      string str = row["F_PATH"].ToString();
      if (!(str == string.Empty) && bucket.Contains(str))
        return true;
    }
    return false;
  }

  internal static DataTable ApplyFilter(
    Guid sessionGuid,
    DataRow[] filterRows,
    DataTable dataTable)
  {
    if (sessionGuid == Guid.Empty)
      return (DataTable) null;
    if (dataTable == null)
      return (DataTable) null;
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    if (session == null)
      return (DataTable) null;
    DataTable dataTable1 = dataTable.Copy();
    dataTable1.RemotingFormat = SerializationFormat.Binary;
    if (filterRows == null || filterRows.Length == 0)
      return dataTable1;
    HashSet<string> source1 = new HashSet<string>();
    int columnIndex1 = dataTable1.Columns.IndexOf("F_PATH");
    if (columnIndex1 == -1)
      return dataTable1;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
    {
      string str = Convert.ToString(row[columnIndex1]).Substring(0, 2);
      if (!str.Equals(string.Empty))
        source1.Add(str);
    }
    HashSet<Guid> source2 = new HashSet<Guid>();
    int columnIndex2 = filterRows[0].Table.Columns.IndexOf("F_GUID");
    foreach (DataRow filterRow in filterRows)
    {
      string g = filterRow[columnIndex2].ToString();
      if (!string.IsNullOrEmpty(g))
        source2.Add(new Guid(g));
    }
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
    string[] array1 = source1.ToArray<string>();
    for (int index = 0; index < source1.Count; ++index)
    {
      int groupID = 0;
      LogicalOperators logicalOperator = LogicalOperators.OR;
      if (source1.Count != 1)
      {
        if (index == 0)
          groupID = 1;
        else if (index == source1.Count - 1)
        {
          groupID = -1;
          logicalOperator = LogicalOperators.AND;
        }
      }
      else
        logicalOperator = LogicalOperators.AND;
      conditionStructureList.Add(new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) array1[index], logicalOperator, groupID, false));
    }
    conditionStructureList.Add(new ConditionStructure(Convert.ToInt32((object) ObligatoryObjectAttributes.F_GUID), RelationalOperators.In, (object) source2.ToArray<Guid>(), LogicalOperators.NONE, 0, false));
    DBRecordSetParams paramsSet = ImbaseServer.CreateParamsSet(conditionStructureList.ToArray());
    DataTable dataTable2 = ImbaseHelper.SelectObjects(session, paramsSet, Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS);
    ImbaseServer.AppendFilterColumn(dataTable2);
    if (dataTable2 != null)
      ImbaseServer.RenameColumns(dataTable2);
    if (dataTable2 != null && dataTable2.Rows.Count == 0)
      return dataTable1;
    if (dataTable2 != null)
    {
      ImbaseServer.BuildUpTree(dataTable2, session, paramsSet);
      dataTable2.AcceptChanges();
    }
    int resPathIndex = dataTable1.Columns.IndexOf("F_PATH");
    int num1 = dataTable2.Columns.IndexOf("F_PATH");
    if (resPathIndex == -1 || num1 == -1)
      return dataTable1;
    ImbaseServer.AppendRows(dataTable1, dataTable2, 5);
    dataTable1.CaseSensitive = true;
    long[] array2 = dataTable1.AsEnumerable().GroupBy<DataRow, string>((System.Func<DataRow, string>) (x => Convert.ToString(x[resPathIndex]))).Where<IGrouping<string, DataRow>>((System.Func<IGrouping<string, DataRow>, bool>) (g => g.Count<DataRow>() > 1)).SelectMany<IGrouping<string, DataRow>, DataRow>((System.Func<IGrouping<string, DataRow>, IEnumerable<DataRow>>) (g => (IEnumerable<DataRow>) g)).Select<DataRow, long>((System.Func<DataRow, long>) (g => Convert.ToInt64(g["F_OBJECT_ID"]))).ToArray<long>();
    if (array2.Length != 0)
      throw new ObjectsFoundException("Невозможно применить фильтр, т.к. имеется несколько объектов с одинаковым значением ключа папки классификатора:", string.Empty, ((IEnumerable<long>) array2).ToArray<long>());
    dataTable1.PrimaryKey = new DataColumn[1]
    {
      dataTable1.Columns[resPathIndex]
    };
    DataColumn weightColumn = dataTable1.Columns.Add("FF_WEIGHT", typeof (short));
    SortedList<string, DataRow> sortedList = new SortedList<string, DataRow>((IDictionary<string, DataRow>) dataTable1.AsEnumerable().ToDictionary<DataRow, string, DataRow>((System.Func<DataRow, string>) (row => Convert.ToString(row[resPathIndex])), (System.Func<DataRow, DataRow>) (row => row)), (IComparer<string>) StringComparer.Ordinal);
    List<DataRow> dataRowList = new List<DataRow>(dataTable1.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
    {
      if ((row["#FLT"].Equals((object) DBNull.Value) ? 0 : Convert.ToInt32(row["#FLT"])) != 0)
      {
        string str = row["F_PATH"].ToString();
        dataRowList.Clear();
        int index1 = sortedList.Keys.IndexOf(str);
        if (index1 != -1)
        {
          dataRowList.Add(sortedList.Values[index1]);
          for (int index2 = index1 + 1; index2 < sortedList.Count && sortedList.Keys[index2].StartsWith(str, StringComparison.Ordinal); ++index2)
            dataRowList.Add(sortedList.Values[index2]);
        }
        foreach (DataRow dataRow in dataRowList)
          dataRow[weightColumn] = (object) 1;
        if (str.Length > 2)
        {
          int index3 = sortedList.IndexOfKey(str.Remove(str.Length - 2));
          if (index3 != -1)
            sortedList.Values[index3]["#FLT"] = (object) true;
        }
      }
    }
    bool flag = false;
    int num2 = dataTable1.Columns.IndexOf("F_EXP");
    int columnIndex3 = num2 != -1 ? num2 : dataTable1.Columns.IndexOf("#FLT");
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      int int32_1 = row[columnIndex3].Equals((object) DBNull.Value) ? 0 : Convert.ToInt32(row[columnIndex3]);
      string str = row["F_PATH"].ToString();
      dataRowList.Clear();
      int index4 = sortedList.Keys.IndexOf(str);
      if (index4 >= 0)
      {
        dataRowList.Add(sortedList.Values[index4]);
        if (int32_1 != 0)
        {
          for (int index5 = index4 + 1; index5 < sortedList.Count && sortedList.Keys[index5].StartsWith(str, StringComparison.Ordinal); ++index5)
            dataRowList.Add(sortedList.Values[index5]);
        }
        if (dataRowList.Count != 0)
        {
          flag = true;
          foreach (DataRow dataRow in dataRowList)
          {
            object obj = dataRow[weightColumn];
            int int32_2 = obj.Equals((object) DBNull.Value) ? 0 : Convert.ToInt32(obj);
            dataRow[weightColumn] = (object) (int32_2 | 2);
          }
        }
      }
    }
    string maxNodeWeightStr = Convert.ToString(flag ? 3 : 1);
    IEnumerable<DataRow> source3 = (IEnumerable<DataRow>) dataTable1.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => string.CompareOrdinal(Convert.ToString(row[weightColumn]), maxNodeWeightStr) == 0));
    if (!source3.Any<DataRow>())
    {
      dataTable1.Clear();
      return dataTable1;
    }
    HashSet<string> bucket = new HashSet<string>();
    ImbaseHelper.CollectAllClassificatorsCollection((ICollection<string>) bucket, source3.Select<DataRow, string>((System.Func<DataRow, string>) (row => row["F_PATH"].ToString())));
    for (int index = dataTable1.Rows.Count - 1; index >= 0; --index)
    {
      DataRow row = dataTable1.Rows[index];
      string str = row["F_PATH"].ToString();
      if (!bucket.Contains(str))
        dataTable1.Rows.Remove(row);
    }
    dataTable1.AcceptChanges();
    dataTable1.RemotingFormat = SerializationFormat.Binary;
    return dataTable1;
  }

  DataTable IFolderFilterService.LoadCatalogTable(
    Guid sessionGuid,
    long catalogId,
    bool checkBlobs)
  {
    return FolderFilterService.LoadCatalogTable(sessionGuid, catalogId, checkBlobs, Intermech.Imbase.Consts.ImbaseFolderTypeID);
  }

  DataTable IFolderFilterService.LoadAllCatalogTable(
    Guid sessionGuid,
    long catalogId,
    bool checkBlobs)
  {
    return FolderFilterService.LoadCatalogTable(sessionGuid, catalogId, checkBlobs, Intermech.Imbase.Consts.ImbaseRootObjectTypeID);
  }

  public static DataTable LoadCatalogTable(
    Guid sessionGuid,
    long catalogId,
    bool checkBlobs,
    int recObjTypeId)
  {
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    IDBAttribute attributeById = session.GetObject(catalogId).GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId);
    if (attributeById == null)
      return (DataTable) null;
    string asString = attributeById.AsString;
    DBRecordSetParams rParams = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) asString, LogicalOperators.NONE, 0, true)
    }, FolderFilterService.GetColumns(checkBlobs))
    {
      Contents = FolderFilterService.CreateContents(checkBlobs),
      TableName = "f",
      Tags = new HybridDictionary()
    };
    rParams.Tags[(object) "{7FB30639-2F65-4407-B78E-523547B1B133}"] = (object) true;
    DataTable dt = ImbaseHelper.SelectObjects(session, rParams, recObjTypeId);
    int num = Array.IndexOf<object>(rParams.Columns, (object) Intermech.Imbase.Consts.ClassifFolderKeyAttId);
    int recObjTypeId1 = recObjTypeId;
    int classifKeyColumnIndex = num;
    DataTable resultTable = FolderFilterService.RemoveWithMissingParents(dt, recObjTypeId1, classifKeyColumnIndex);
    FolderFilterService.RenameColumns(resultTable, checkBlobs);
    resultTable.RemotingFormat = SerializationFormat.Binary;
    return resultTable;
  }

  public static DataTable RemoveWithMissingParents(
    DataTable dt,
    int recObjTypeId,
    int classifKeyColumnIndex)
  {
    DataTable source = dt;
    if (source != null && classifKeyColumnIndex > -1)
    {
      HashSet<string> stringSet = new HashSet<string>((IEnumerable<string>) source.AsEnumerable().Select<DataRow, string>((System.Func<DataRow, string>) (item => Convert.ToString(item[classifKeyColumnIndex]))));
      List<DataRow> dataRowList = new List<DataRow>(source.Rows.Count);
      int num = recObjTypeId == Intermech.Imbase.Consts.ImbaseFolderTypeID ? 4 : 2;
      foreach (DataRow row in (InternalDataCollectionBase) source.Rows)
      {
        string str1 = Convert.ToString(row[classifKeyColumnIndex]);
        int length1 = str1.Length;
        for (int length2 = num; length2 < length1; length2 += 2)
        {
          string str2 = str1.Substring(0, length2);
          if (!stringSet.Contains(str2))
            dataRowList.Add(row);
        }
      }
      if (dataRowList.Count > 0)
      {
        dataRowList.ForEach((Action<DataRow>) (x => x.Delete()));
        source.AcceptChanges();
      }
    }
    return source;
  }

  private void WritePathAttributeValueHandler(IDBAttribute attribute, AttributeValueEventArgs args)
  {
    if (args == null || args.NewValue == args.OldValue || attribute == null || attribute.AttributeID != Intermech.Imbase.Consts.ClassifFolderKeyAttId || !(attribute is DBAttribute dbAttribute) || !dbAttribute.IsObjectAttribute || !(dbAttribute.ParentObject is DBObject parentObject) || parentObject.IsCreationMode)
      return;
    this.FilterData_SynchonizationLoad(attribute.Session);
    string nodePath = args.OldValue != null ? args.OldValue.ToString() : string.Empty;
    string str = args.Value != null ? args.Value.ToString() : string.Empty;
    string filterExpression = nodePath != string.Empty ? FolderFilterService.GetPathSQLCond(nodePath) : FolderFilterService.GetFolderSQLCond(parentObject.ObjectID);
    lock (this)
    {
      DataRow[] dataRowArray = this._filterData.Select(filterExpression);
      if (dataRowArray.Length == 0)
        return;
      int columnIndex = this._filterData.Columns.IndexOf("F_PATH");
      foreach (DataRow dataRow in dataRowArray)
      {
        if (dataRow != null)
          dataRow[columnIndex] = (object) str;
      }
      this._filterData.AcceptChanges();
    }
    this.FilterData_SynchonizationSave(attribute.Session);
  }

  private void DoImObjectDelete(IDBObject dbObject)
  {
    if (dbObject == null || !MetaDataHelper.IsObjectTypeChildOf(dbObject.ObjectType, Intermech.Imbase.Consts.ImbaseRootObjectTypeID))
      return;
    this.FilterData_SynchonizationLoad(dbObject.Session);
    string folderSqlCond = FolderFilterService.GetFolderSQLCond(dbObject.ObjectID);
    lock (this)
    {
      DataRow[] dataRowArray = this._filterData.Select(folderSqlCond);
      if (dataRowArray.Length == 0)
        return;
      foreach (DataRow dataRow in dataRowArray)
        dataRow?.Delete();
      this._filterData.AcceptChanges();
    }
    this.FilterData_SynchonizationSave(dbObject.Session);
  }

  private void DoImObjectCommitCreationEvent(IDBObject dbObject, IUserSession session)
  {
    if (dbObject == null || session == null || !MetaDataHelper.IsObjectTypeChildOf(dbObject.ObjectType, Intermech.Imbase.Consts.ImbaseRootObjectTypeID))
      return;
    this.FilterData_SynchonizationLoad(dbObject.Session);
    string empty = string.Empty;
    IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId);
    if (attributeById != null)
      empty = attributeById.Value.ToString();
    lock (this)
    {
      if (!FolderFilterService.FilterThreadLoader.LoadFilterDataInfo(dbObject, empty, ref this._filterData))
        return;
      this._filterData.AcceptChanges();
    }
    this.FilterData_SynchonizationSave(dbObject.Session);
  }

  private void DoBeforeObjNextLCStepHandler(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    if (sender == null || nextstep == null || session == null || !MetaDataHelper.IsObjectTypeChildOf(sender.ObjectType, Intermech.Imbase.Consts.ImbaseRootObjectTypeID))
      return;
    this._obj2LCStepBefore.Remove(sender.ObjectID);
    this._obj2LCStepBefore.Add(sender.ObjectID, MetaDataHelper.GetLCStep(sender.LCStep));
  }

  private void DoAfterObjNextLCStepHandler(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    if (sender == null || nextstep == null || session == null || !MetaDataHelper.IsObjectTypeChildOf(sender.ObjectType, Intermech.Imbase.Consts.ImbaseRootObjectTypeID))
      return;
    IMSLifeCycleStep imsLifeCycleStep;
    if (!this._obj2LCStepBefore.TryGetValue(sender.ObjectID, out imsLifeCycleStep))
      return;
    try
    {
      if (imsLifeCycleStep == null || imsLifeCycleStep.LevelID != session.IdentHelper.DeletedID && nextstep.LevelID != session.IdentHelper.DeletedID)
        return;
      if (nextstep.LevelID == session.IdentHelper.DeletedID)
        this.DoImObjectDelete(sender);
      else
        this.DoImObjectCommitCreationEvent(sender, sender.Session);
    }
    finally
    {
      this._obj2LCStepBefore.Remove(sender.ObjectID);
    }
  }

  public void SubscribeOnSystemlEvents(IEventLogHelper eventHelper)
  {
    if (eventHelper == null)
      return;
    eventHelper.BeforeNextLCStepEvent += new NextLCStepHandler(this.DoBeforeObjNextLCStepHandler);
    eventHelper.AfterNextLCStepEvent += new NextLCStepHandler(this.DoAfterObjNextLCStepHandler);
    eventHelper.CommitCreationObjectEvent += new ObjectEventHandler(this.DoImObjectCommitCreationEvent);
    int classifFolderKeyAttId = Intermech.Imbase.Consts.ClassifFolderKeyAttId;
    eventHelper.AddAttributeWriteHandler((object) classifFolderKeyAttId, new WriteAttributeValueHandler(this.WritePathAttributeValueHandler));
  }

  public void UnSubscribeOnSystemEvents(IEventLogHelper eventHelper)
  {
    if (eventHelper == null)
      return;
    eventHelper.BeforeNextLCStepEvent -= new NextLCStepHandler(this.DoBeforeObjNextLCStepHandler);
    eventHelper.AfterNextLCStepEvent -= new NextLCStepHandler(this.DoAfterObjNextLCStepHandler);
    eventHelper.CommitCreationObjectEvent -= new ObjectEventHandler(this.DoImObjectCommitCreationEvent);
    int classifFolderKeyAttId = Intermech.Imbase.Consts.ClassifFolderKeyAttId;
    eventHelper.RemoveAttributeWriteHandler((object) classifFolderKeyAttId, new WriteAttributeValueHandler(this.WritePathAttributeValueHandler));
  }

  internal class FilterThreadLoader
  {
    private IUserSession _session;
    private DataTable _filterTable;
    private bool _applicationExit;
    private IApplicationStateEventsService _applicationStateEvents;

    private void LoadFilterData()
    {
      if (this._session == null)
        return;
      DataTable filterTableAll = FolderFilterService.CreateFilterTableAll("filter_data");
      try
      {
        DataTable dataTable = this.LoadAllFoldersWithFilters(this._session);
        if (dataTable == null || dataTable.Rows.Count == 0)
          return;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (this._applicationExit)
            return;
          this.LoadFilterDataInfo(Convert.ToInt64(row[0]), row[2].ToString(), ref filterTableAll);
        }
        if (this._filterTable != null)
          DataSetProcessor.AddTable(this._filterTable, filterTableAll, true);
        else
          this._filterTable = filterTableAll;
      }
      finally
      {
        this._session.Logout(nameof (FolderFilterService));
        this._session = (IUserSession) null;
        this._applicationStateEvents.Exit -= new EventHandler(this.OnBeforeApplicationExit);
        this._applicationStateEvents.EmergencyExit -= new EventHandler(this.OnBeforeApplicationEmergencyExit);
      }
    }

    private bool LoadFilterDataInfo(
      long objectId,
      string classifierPath,
      ref DataTable filterTable)
    {
      if (objectId == 0L)
        return false;
      IDBObject dbObject = this._session.GetObject(objectId, false);
      return dbObject != null && FolderFilterService.FilterThreadLoader.LoadFilterDataInfo(dbObject, classifierPath, ref filterTable);
    }

    private DataTable LoadAllFoldersWithFilters(IUserSession session)
    {
      if (session == null)
        return (DataTable) null;
      DataTable dataTable = (DataTable) null;
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>()
      {
        new ConditionStructure(Intermech.Imbase.Consts.FilterBlobAttId, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, false)
      };
      List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>()
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
        new ColumnDescriptor((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
      };
      DBRecordSetParams paramSet = new DBRecordSetParams(conditionStructureList.ToArray(), columnDescriptorList.ToArray());
      IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseFolderTypeID);
      if (objectCollection != null)
        dataTable = objectCollection.Select(paramSet);
      return dataTable;
    }

    private void OnBeforeApplicationExit(object sender, EventArgs e)
    {
      this._applicationExit = true;
    }

    private void OnBeforeApplicationEmergencyExit(object sender, EventArgs e)
    {
      this._applicationExit = true;
    }

    public FilterThreadLoader(
      IUserSession session,
      IApplicationStateEventsService applicationStateEvents,
      ref DataTable filterTable)
    {
      if (applicationStateEvents == null)
        throw new ArgumentNullException(nameof (applicationStateEvents));
      this._session = session == null ? ServiceUtils.GetService<IDBTimedEvents>((object) ApplicationServices.Container, true).GetSystemSessionTemporaryClone(nameof (FolderFilterService)) : session.Clone(nameof (FolderFilterService));
      this._filterTable = filterTable;
      this._applicationStateEvents = applicationStateEvents;
      this._applicationStateEvents.Exit += new EventHandler(this.OnBeforeApplicationExit);
      this._applicationStateEvents.EmergencyExit += new EventHandler(this.OnBeforeApplicationEmergencyExit);
      new Thread(new ThreadStart(this.LoadFilterData)).Start();
    }

    public static bool LoadFilterDataInfo(
      IDBObject dbObject,
      string classificatorPath,
      ref DataTable filterTable)
    {
      if (dbObject == null)
        return false;
      DataTable dataTable = FolderFilterService.LoadFiltersTableOnly(dbObject);
      if (dataTable == null || dataTable.Rows.Count == 0)
        return false;
      int columnIndex1 = dataTable.Columns.IndexOf("F_GUID");
      int columnIndex2 = dataTable.Columns.IndexOf("F_OWNER");
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        string str1 = row[columnIndex1].ToString();
        string str2 = row[columnIndex2].ToString();
        filterTable.Rows.Add((object) dbObject.ObjectID, (object) dbObject.ObjectGUID, (object) dbObject.ObjectType, (object) classificatorPath, (object) str1, (object) str2);
      }
      return true;
    }
  }
}
