// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBImporter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Briefcase;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;


namespace Intermech.Kernel;

public class DBImporter : DBSessionable, IDBImporter
{
  private readonly ImportStore _store;
  private Dictionary<Guid, BriefcaseImportProgress> _progress;
  private Dictionary<Guid, BriefcaseImporter> _importers;
  private readonly string _logFileName;

  public List<AttributeTypePossibleValues> PossibleValuesAttributeType
  {
    get => this._store.PossibleValuesAttributeType;
  }

  public List<SaveImportValues> DefaultValueObjectLink => this._store.DefaultValueObjectLink;

  public List<SaveImportValues> MeasureValueObjectLink => this._store.MeasureValueObjectLink;

  public override object InitializeLifetimeService() => (object) null;

  public ArrayList ObjectLinks { get; }

  public DBImporter(UserSession userSession, string logFileName)
    : base(userSession)
  {
    this._logFileName = logFileName;
    this.ObjectLinks = new ArrayList();
    this._store = new ImportStore();
    this._progress = new Dictionary<Guid, BriefcaseImportProgress>(1);
    this._importers = new Dictionary<Guid, BriefcaseImporter>(1);
  }

  public void CloseImporter()
  {
  }

  public long LastObjectID
  {
    get
    {
      IDbManager dataManager = (this.Session as UserSession).DataManager;
      return dataManager.DataProvider.NextGeneratorValue("IMS_OBJECTS_GEN", dataManager);
    }
  }

  public bool ImportMetadata(
    Guid NumOfBriefcase,
    DataSet Metadata,
    DataSet ImportingList,
    IgnoringErrors ignoringErrors)
  {
    BriefcaseImporter briefcaseImporter = new BriefcaseImporter(this.UserSession, NumOfBriefcase, this._logFileName);
    briefcaseImporter.SetImportProgressEvent += new SetImportProgressEventHandler(this.SetImportProgress);
    try
    {
      this._progress.Add(NumOfBriefcase, new BriefcaseImportProgress(OperationType.ImportingMetaData));
      this._importers.Add(NumOfBriefcase, briefcaseImporter);
      return briefcaseImporter.ImportMetadata(Metadata, ImportingList, this._store, ignoringErrors, true, false);
    }
    finally
    {
      briefcaseImporter.SetImportProgressEvent -= new SetImportProgressEventHandler(this.SetImportProgress);
    }
  }

  private void SetImportProgress(object sender, SetImportProgressEventArgs e)
  {
    if (this._progress.ContainsKey(e.Briefcase))
      this._progress[e.Briefcase] = e.ImportProgress;
    else
      this._progress.Add(e.Briefcase, e.ImportProgress);
  }

  public BriefcaseImportProgress GetProgress(Guid NumOfBriefcase)
  {
    return this._progress.ContainsKey(NumOfBriefcase) ? this._progress[NumOfBriefcase] : (BriefcaseImportProgress) null;
  }

  public void EndImportMetadata(Guid NumOfBriefcase)
  {
    if (this._progress.ContainsKey(NumOfBriefcase))
      this._progress.Remove(NumOfBriefcase);
    if (!this._importers.ContainsKey(NumOfBriefcase))
      return;
    this._importers.Remove(NumOfBriefcase);
  }

  public bool SetPossibleValues(
    List<AttributeTypePossibleValues> possibleValuesAttributeType,
    Hashtable importingObjectIDs)
  {
    return this.SetPossibleValues(possibleValuesAttributeType, this.ConvertHashtable(importingObjectIDs));
  }

  public bool SetPossibleValues(
    List<AttributeTypePossibleValues> possibleValuesAttributeType,
    List<Tuple<long, long>> importingObjectIDs)
  {
    new BriefcaseImporter(this.UserSession, this._logFileName).SetPossibleValues(this.UserSession, possibleValuesAttributeType, this.ConvertImportingObjectIDs(importingObjectIDs));
    return true;
  }

  public bool SetObjectLinks(ArrayList objLinks, List<IDСorresponds> importingObjectIDs)
  {
    return new ImportObjectLinks(this.UserSession, objLinks, importingObjectIDs).Import();
  }

  public bool SetDefaultValues(
    List<SaveImportValues> defaultValueObjectLink,
    Hashtable importingObjectIDs)
  {
    return this.SetDefaultValues(defaultValueObjectLink, this.ConvertHashtable(importingObjectIDs));
  }

  public bool SetDefaultValues(
    List<SaveImportValues> defaultValueObjectLink,
    List<Tuple<long, long>> importingObjectIDs)
  {
    new BriefcaseImporter(this.UserSession, this._logFileName).SetDefaultValues(this.UserSession, defaultValueObjectLink, this.ConvertImportingObjectIDs(importingObjectIDs));
    return true;
  }

  public bool SetMeasureValues(
    List<SaveImportValues> measureValueObjectLink,
    Hashtable importingObjectIDs)
  {
    return this.SetMeasureValues(measureValueObjectLink, this.ConvertHashtable(importingObjectIDs));
  }

  public bool SetMeasureValues(
    List<SaveImportValues> measureValueObjectLink,
    List<Tuple<long, long>> importingObjectIDs)
  {
    new BriefcaseImporter(this.UserSession, this._logFileName).SetMeasureValues(this.UserSession, measureValueObjectLink, this.ConvertImportingObjectIDs(importingObjectIDs));
    return true;
  }

  public IImportedObjectInfo ImportObject(ImportingObject importingObject)
  {
    return this.ImportObject(importingObject, true);
  }

  public IImportedObjectInfo ImportObject(ImportingObject importingObject, bool createLinksArray)
  {
    ImportPumpObject importPumpObject = new ImportPumpObject(this.UserSession, importingObject, createLinksArray, (Dictionary<Int96, long>) null, importingObject.Object.Object_id == 0L);
    this.UserSession.StartTransaction();
    try
    {
      IImportedObjectInfo importedObjectInfo = (IImportedObjectInfo) importPumpObject.Import();
      if (importedObjectInfo.ObjectID != 0L & createLinksArray)
        this.ObjectLinks.AddRange((ICollection) importPumpObject.ObjectLinks);
      foreach (string EventStr in importPumpObject.Log)
        this.EventHelper.AddToTrace(EventStr, Consts.traceAlways, this._logFileName);
      this.UserSession.DataManager.ExecuteBatchSQL();
      this.UserSession.Commit();
      return importedObjectInfo;
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      this.EventHelper.AddToTrace(ex.Message, Consts.traceAlways, this._logFileName);
      this.EventHelper.AddToTrace(ex.StackTrace, Consts.traceAlways, this._logFileName);
      return (IImportedObjectInfo) new ImportedObjectInfo(ex);
    }
  }

  public long ImportRelation(ImportingRelation importingRelation)
  {
    return this.ImportRelation(importingRelation, true);
  }

  public long ImportRelation(ImportingRelation importingRelation, bool createLinksArray)
  {
    this.UserSession.StartTransaction();
    try
    {
      ImportPumpRelation importPumpRelation = new ImportPumpRelation(this.UserSession, importingRelation, createLinksArray);
      long num = importPumpRelation.AddNewRelation();
      if (createLinksArray && num != 0L)
        this.ObjectLinks.AddRange((ICollection) importPumpRelation.ObjectLinks);
      foreach (string EventStr in importPumpRelation.Log)
        this.EventHelper.AddToTrace(EventStr, Consts.traceAlways, this._logFileName);
      this.UserSession.DataManager.ExecuteBatchSQL();
      this.UserSession.Commit();
      return num;
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      this.EventHelper.AddToTrace(ex.Message, Consts.traceAlways, this._logFileName);
      this.EventHelper.AddToTrace(ex.StackTrace, Consts.traceAlways, this._logFileName);
      throw;
    }
  }

  private List<Tuple<long, long>> ConvertHashtable(Hashtable input)
  {
    List<Tuple<long, long>> tupleList = new List<Tuple<long, long>>();
    foreach (DictionaryEntry dictionaryEntry in input)
      tupleList.Add(new Tuple<long, long>(Convert.ToInt64(dictionaryEntry.Key), Convert.ToInt64(dictionaryEntry.Value)));
    return tupleList;
  }

  private List<IDСorresponds> ConvertImportingObjectIDs(List<Tuple<long, long>> importingObjectIDs)
  {
    return importingObjectIDs.ConvertAll<IDСorresponds>((Converter<Tuple<long, long>, IDСorresponds>) (x => new IDСorresponds(x.Item1, 0L, x.Item2, 0L, true)));
  }

  public long[] ImportSequrity(SecurityRecord[] importingRecords)
  {
    if (importingRecords == null || importingRecords.Length == 0)
      return (long[]) null;
    List<long> longList = new List<long>();
    for (int index = 0; index < importingRecords.Length; ++index)
    {
      ImportSecurity importSecurity = new ImportSecurity(this.UserSession);
      long num = importSecurity.ImportNewSecurity(importingRecords[index]);
      if (num == 0L)
        this.EventHelper.AddToTrace(importSecurity.ErrorException.Message, Consts.traceAlways, this._logFileName);
      longList.Add(num);
    }
    return longList.ToArray();
  }

  public IImportedObjectInfo[] ImportObjects(ImportingObject[] importingObjects)
  {
    return this.ImportObjects(importingObjects, true);
  }

  public IImportedObjectInfo[] ImportObjects(
    ImportingObject[] importingObjects,
    bool createLinksArray)
  {
    if (importingObjects == null || importingObjects.Length == 0)
      return (IImportedObjectInfo[]) null;
    Dictionary<Int96, long> versions = new Dictionary<Int96, long>(importingObjects.Length);
    List<IImportedObjectInfo> importedObjectInfoList = new List<IImportedObjectInfo>(importingObjects.Length);
    this.UserSession.StartTransaction();
    try
    {
      foreach (ImportingObject importingObject in importingObjects)
      {
        ImportPumpObject importPumpObject = new ImportPumpObject(this.UserSession, importingObject, createLinksArray, versions, importingObject.Object.Object_id == 0L);
        IImportedObjectInfo importedObjectInfo = (IImportedObjectInfo) importPumpObject.Import();
        if (importingObject.Object.VersionId >= 0)
          versions[new Int96(importedObjectInfo.ID, (long) importingObject.Object.VersionId)] = importedObjectInfo.ObjectID;
        if (importedObjectInfo.ObjectID != 0L & createLinksArray)
          this.ObjectLinks.AddRange((ICollection) importPumpObject.ObjectLinks);
        foreach (string EventStr in importPumpObject.Log)
          this.EventHelper.AddToTrace(EventStr, Consts.traceAlways, this._logFileName);
        importedObjectInfoList.Add(importedObjectInfo);
      }
      this.UserSession.DataManager.ExecuteBatchSQL();
      this.UserSession.Commit();
      return importedObjectInfoList.ToArray();
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      this.EventHelper.AddToTrace(ex.Message, Consts.traceAlways, this._logFileName);
      this.EventHelper.AddToTrace(ex.StackTrace, Consts.traceAlways, this._logFileName);
      throw;
    }
  }

  public DataTable GetAttributeValues(int objectTypeID, long objectID)
  {
    IDBObjectType dbObjectType = objectTypeID != -1 ? this.UserSession.GetObjectType(objectTypeID) : throw new KernelException("Вызов функции GetAttributeValues для нетипизированной коллекции объектов.");
    return this.UserSession.DataManager.ExecuteDataTable($"SELECT * FROM {(dbObjectType.IsLocalType ? (object) dbObjectType.AttributesTableName : (object) "IMS_OBJECT_ATTRS")} WHERE F_OBJECT_ID = :objectID ORDER BY F_ATTRIBUTE_ID, F_INLIST_ID", this.UserSession.DataManager.Parameter(nameof (objectID), (object) objectID));
  }

  public long[] ImportRelations(ImportingRelation[] importingRelations)
  {
    return this.ImportRelations(importingRelations, true);
  }

  public long[] ImportRelations(ImportingRelation[] importingRelations, bool createLinksArray)
  {
    this.UserSession.StartTransaction();
    try
    {
      if (PumpTraceLog.Enabled)
        PumpTraceLog.Write($"import relations packet. Length = {importingRelations.Length}");
      List<long> longList = new List<long>();
      foreach (ImportingRelation importingRelation in importingRelations)
      {
        if (PumpTraceLog.Enabled)
          PumpTraceLog.Write($"importingRelation.Relation.RelationType = {importingRelation.Relation.RelationType}. importingRelation.Relation.ProjId = {importingRelation.Relation.ProjId} importingRelation.Relation.PartId = {importingRelation.Relation.PartId}");
        ImportPumpRelation importPumpRelation = new ImportPumpRelation(this.UserSession, importingRelation, createLinksArray);
        long num = importPumpRelation.AddNewRelation();
        if (createLinksArray && num != -1L)
          this.ObjectLinks.AddRange((ICollection) importPumpRelation.ObjectLinks);
        foreach (string EventStr in importPumpRelation.Log)
          this.EventHelper.AddToTrace(EventStr, Consts.traceAlways, this._logFileName);
        longList.Add(num);
      }
      this.UserSession.DataManager.ExecuteBatchSQL();
      this.UserSession.Commit();
      return longList.ToArray();
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      this.EventHelper.AddToTrace(ex.Message, Consts.traceAlways, this._logFileName);
      this.EventHelper.AddToTrace(ex.StackTrace, Consts.traceAlways, this._logFileName);
      throw;
    }
  }

  public bool SetVersionsTree(DataTable treeTable)
  {
    this.UserSession.StartTransaction();
    try
    {
      IDbManager dataManager = this.UserSession.DataManager;
      for (int index = 0; index < treeTable.Rows.Count; ++index)
        DBHelper.ExecuteNonQuery((IUserSession) this.UserSession, true, "INSERT INTO IMS_VERSIONS_TREE (F_PARENT_ID, F_OBJECT_ID) VALUES (:projID, :partID)", dataManager.Parameter("projID", treeTable.Rows[index]["F_PARENT_ID"]), dataManager.Parameter("partID", treeTable.Rows[index]["F_OBJECT_ID"]));
      this.UserSession.Commit();
      return true;
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      this.EventHelper.AddToTrace(ex.Message, Consts.traceAlways, this._logFileName);
      this.EventHelper.AddToTrace(ex.StackTrace, Consts.traceAlways, this._logFileName);
      return false;
    }
  }

  public bool IncludeObjectIntoSelection(long selectionID, string key, long objectID, long id)
  {
    this.UserSession.StartTransaction();
    try
    {
      IDbManager dataManager = this.UserSession.DataManager;
      IDbDataParameter dbDataParameter1 = dataManager.Parameter("fID", (object) selectionID);
      IDbDataParameter dbDataParameter2 = dataManager.Parameter("objID", (object) objectID);
      IDbDataParameter dbDataParameter3 = dataManager.Parameter("oID", (object) id);
      if (key == null || key == string.Empty)
      {
        DBHelper.ExecuteNonQuery((IUserSession) this.UserSession, true, "INSERT INTO IMS_SELECTIONS (F_FOLDER_ID, F_OBJECT_ID, F_ID, F_FOLDER_KEY) VALUES (:fID, :objID, :oID, NULL)", dbDataParameter1, dbDataParameter2, dbDataParameter3);
      }
      else
      {
        IDbDataParameter dbDataParameter4 = dataManager.Parameter("fKey", (object) key);
        DBHelper.ExecuteNonQuery((IUserSession) this.UserSession, true, "INSERT INTO IMS_SELECTIONS (F_FOLDER_ID, F_OBJECT_ID, F_ID, F_FOLDER_KEY) VALUES (:fID, :objID, :oID, :fKey)", dbDataParameter1, dbDataParameter2, dbDataParameter3, dbDataParameter4);
      }
      this.UserSession.Commit();
      return true;
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      this.EventHelper.AddToTrace(ex.Message, Consts.traceAlways, this._logFileName);
      this.EventHelper.AddToTrace(ex.StackTrace, Consts.traceAlways, this._logFileName);
      return false;
    }
  }

  public long GetNextID(string tableName)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    string commandText = string.Empty;
    switch (dataManager.DataProvider.Name)
    {
      case "Oracle":
        commandText = $"SELECT {tableName}_GEN.nextval FROM dual";
        break;
      case "Linter":
        commandText = $"SELECT {tableName}_GEN.nextval";
        break;
      case "PostgreSQL":
        commandText = $"SELECT NEXTVAL('{tableName}_GEN')";
        break;
      case "Sql":
        commandText = $"SELECT IDENT_CURRENT('{tableName}')";
        break;
    }
    object obj = dataManager.ExecuteScalar(commandText);
    return obj == null || obj == DBNull.Value ? -1L : Convert.ToInt64(obj) + 1L;
  }

  public void SetTriggersIMS_OBJECT_ATTRS(bool enable)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    try
    {
      switch (dataManager.DataProvider.Name)
      {
        case "Oracle":
          List<string> stringList = new List<string>();
          DataTable dataTable = dataManager.ExecuteDataTable("SELECT TRIGGER_NAME FROM SYS.ALL_TRIGGERS WHERE TABLE_NAME LIKE 'IMS_OBJECT_ATTRS'");
          for (int index = 0; index < dataTable.Rows.Count; ++index)
            dataManager.ExecuteNonQuery($"ALTER TRIGGER {Convert.ToString(dataTable.Rows[index][0])} {(enable ? (object) "ENABLE" : (object) "DISABLE")}");
          break;
        case "Sql":
        case "PostgreSQL":
          dataManager.ExecuteNonQuery($"ALTER TABLE IMS_OBJECT_ATTRS {(enable ? (object) "ENABLE" : (object) "DISABLE")} TRIGGER ALL");
          break;
      }
    }
    catch
    {
    }
  }

  public void DropIndexes(Guid pumpGuid)
  {
    BriefcaseImportProgress briefcaseImportProgress = new BriefcaseImportProgress(OperationType.Importing);
    this._progress.Add(pumpGuid, briefcaseImportProgress);
    Thread thread = new Thread(new ParameterizedThreadStart(this.DropIndexes));
    thread.IsBackground = true;
    thread.Name = $"DropIndexes_{pumpGuid}";
    thread.Start((object) pumpGuid);
    thread.Join();
  }

  private void DropIndexes(object args)
  {
    BriefcaseImportProgress briefcaseImportProgress = this._progress[(Guid) args];
    string sessionName = $"DBImporter.DropIndexes_{Guid.NewGuid()}";
    IUserSession userSession = this.UserSession.Clone(true, sessionName);
    try
    {
      IDbManager dataManager = (userSession as UserSession).DataManager;
      string[] errorMessages;
      dataManager.DataProvider.DisableIndexes(dataManager, out errorMessages);
      if (errorMessages != null && errorMessages.Length != 0)
      {
        for (int index = 0; index < errorMessages.Length; ++index)
          this.EventHelper.AddToTrace(errorMessages[index], Consts.traceAlways, this._logFileName);
        briefcaseImportProgress.ErrorException = new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1140"), (object) this._logFileName));
      }
      briefcaseImportProgress.Percent = 100;
      briefcaseImportProgress.Operation = OperationType.Finished;
    }
    finally
    {
      userSession.Logout(sessionName);
    }
  }

  public void CreateIndexes(Guid pumpGuid)
  {
    BriefcaseImportProgress briefcaseImportProgress = new BriefcaseImportProgress(OperationType.Importing);
    this._progress.Add(pumpGuid, briefcaseImportProgress);
    Thread thread = new Thread(new ParameterizedThreadStart(this.CreateIndexes));
    thread.IsBackground = true;
    thread.Name = $"CreateIndexes_{pumpGuid}";
    thread.Start((object) pumpGuid);
    thread.Join();
  }

  private void CreateIndexes(object args)
  {
    BriefcaseImportProgress briefcaseImportProgress = this._progress[(Guid) args];
    string sessionName = $"DBImporter.CreateIndexes_{Guid.NewGuid()}";
    IUserSession userSession = this.UserSession.Clone(true, sessionName);
    try
    {
      IDbManager dataManager = (userSession as UserSession).DataManager;
      string[] errorMessages;
      dataManager.DataProvider.EnableIndexes(dataManager, out errorMessages);
      if (errorMessages != null && errorMessages.Length != 0)
      {
        for (int index = 0; index < errorMessages.Length; ++index)
          this.EventHelper.AddToTrace(errorMessages[index], Consts.traceAlways, this._logFileName);
        briefcaseImportProgress.ErrorException = new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1141"), (object) this._logFileName));
      }
      briefcaseImportProgress.Percent = 100;
      briefcaseImportProgress.Operation = OperationType.Finished;
    }
    finally
    {
      userSession.Logout(sessionName);
    }
  }

  public void SetObjectVerType(IDBObject dbObject, ObjectRecordKind verTypeID)
  {
    (dbObject as DBObject).SetObjectVerType(verTypeID);
  }

  public Dictionary<int, LCSchemaInfo> GetSchemaInfo4ObjTypes()
  {
    DataTable table = this.UserSession.DataManager.ExecuteDataTable("SELECT F_SCHEMA_ID, F_LC_STEP, F_LC_NAME, F_FIRST, F_LEVEL_ID FROM IMS_LC_STEPS ORDER BY F_SCHEMA_ID");
    List<LCSchemaInfo> lcSchemaInfoList = new List<LCSchemaInfo>();
    LCSchemaInfo lcSchemaInfo = (LCSchemaInfo) null;
    for (int index = 0; index < table.Rows.Count; ++index)
    {
      int int32_1 = Convert.ToInt32(table.Rows[index][0]);
      int int32_2 = Convert.ToInt32(table.Rows[index][1]);
      string name = Convert.ToString(table.Rows[index][2]);
      int int32_3 = Convert.ToInt32(table.Rows[index][3]);
      int int32_4 = Convert.ToInt32(table.Rows[index][4]);
      if (lcSchemaInfo == null || lcSchemaInfo.SchemaID != int32_1)
      {
        lcSchemaInfo = new LCSchemaInfo(int32_1);
        lcSchemaInfoList.Add(lcSchemaInfo);
      }
      if (int32_3 == 1)
        lcSchemaInfo.FirtsLCStep = int32_2;
      lcSchemaInfo.AddStep(int32_2, name, int32_4);
    }
    table = this.UserSession.DataManager.ExecuteDataTable("SELECT F_OBJECT_TYPE, F_SCHEMA_ID FROM IMS_OBJECT_TYPES");
    Dictionary<int, LCSchemaInfo> schemaInfo4ObjTypes = new Dictionary<int, LCSchemaInfo>(table.Rows.Count);
    for (int i = 0; i < table.Rows.Count; i++)
      schemaInfo4ObjTypes.Add(Convert.ToInt32(table.Rows[i][0]), lcSchemaInfoList.Find((Predicate<LCSchemaInfo>) (x => x.SchemaID == Convert.ToInt32(table.Rows[i][1]))));
    lcSchemaInfoList.Clear();
    return schemaInfo4ObjTypes;
  }

  public void AddBlobAttribute(
    int attributeID,
    long objectID,
    long userID,
    BlobAttributeValue[] blobs)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    long activeStorageId = (ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool).GetActiveStorageID((IUserSession) this.UserSession);
    DbCommandParam dbCommandParam1 = dataManager.BatchParameter("storID", DbType.Int64, (object) Convert.ToDouble(activeStorageId));
    DbCommandParam dbCommandParam2 = dataManager.BatchParameter("objID", DbType.Int64, (object) objectID);
    if (userID == 0L)
    {
      DbCommandParam dbCommandParam3 = dataManager.BatchParameter("attrID", DbType.Int32, (object) attributeID);
      object obj = dataManager.ExecuteScalar("SELECT F_OBJECT_TYPE FROM IMS_OBJECTS WHERE F_OBJECT_ID = :objID", dataManager.Parameter("objID", (object) objectID));
      int objectTypeID = obj != null && obj != DBNull.Value ? Convert.ToInt32(obj) : throw new KernelException($"Объект N{objectID} не найден.");
      string attributesTableName = this.UserSession.DBCache.GetAttributesTableName(objectTypeID);
      string[] updateTables = this.UserSession.DBCache.GetUpdateTables(attributeID, objectTypeID, -1);
      for (int dataValue = 0; dataValue < blobs.Length; ++dataValue)
        DBHelper.AddBatchSQL((IUserSession) this.UserSession, false, $"INSERT INTO {attributesTableName} (F_ATTRIBUTE_ID, F_OBJECT_ID, F_INLIST_ID, F_INTEGER_VALUE, F_STRING_VALUE, F_DOUBLE_VALUE, F_DATE_VALUE) VALUES (:attrID, :objID, :inlistID, :blobID, :flName, :storID, :modifyDate)", new DbCommandParam[7]
        {
          dbCommandParam3,
          dbCommandParam2,
          dataManager.BatchParameter("inlistID", DbType.Int32, (object) dataValue),
          dataManager.BatchParameter("blobID", DbType.Int64, (object) blobs[dataValue].BlobID),
          dataManager.BatchParameter("flName", DbType.String, (object) blobs[dataValue].FileName),
          dbCommandParam1,
          dataManager.BatchParameter("modifyDate", DbType.Date, (object) blobs[dataValue].FileModifyDate)
        });
      if (updateTables != null)
      {
        string format = "UPDATE {0} SET F{1} = :flName, F{1}ID = :blobID, F{1}ID2 = :storID, F{1}ID3 = :modifyDate WHERE F_OBJECT_ID = :objID";
        DbCommandParam dbCommandParam4 = dataManager.BatchParameter("flName", DbType.String, (object) blobs[0].FileName);
        DbCommandParam dbCommandParam5 = dataManager.BatchParameter("blobID", DbType.Int64, (object) blobs[0].BlobID);
        DbCommandParam dbCommandParam6 = dataManager.BatchParameter("modifyDate", DbType.Date, (object) blobs[0].FileModifyDate);
        foreach (string str in updateTables)
          dataManager.AddBatchSQL(string.Format(format, (object) str, (object) attributeID), new DbCommandParam[5]
          {
            dbCommandParam4,
            dbCommandParam5,
            dbCommandParam6,
            dbCommandParam1,
            dbCommandParam2
          });
      }
    }
    else
    {
      for (int dataValue = 0; dataValue < blobs.Length; ++dataValue)
        DBHelper.AddBatchSQL((IUserSession) this.UserSession, false, "INSERT INTO IMS_PUMP_WCFILES (F_OBJECT_ID, F_INLIST_ID, F_FILE_ID, F_USER_ID, F_STORAGE_ID) VALUES (:objID, :inlistID, :blobID, :userID, :storID)", new DbCommandParam[5]
        {
          dbCommandParam2,
          dataManager.BatchParameter("inlistID", DbType.Int32, (object) dataValue),
          dataManager.BatchParameter("blobID", DbType.Int64, (object) blobs[dataValue].BlobID),
          dataManager.BatchParameter(nameof (userID), DbType.Int64, (object) userID),
          dbCommandParam1
        });
    }
    this.UserSession.StartTransaction();
    try
    {
      dataManager.ExecuteBatchSQL();
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  public void AddBlobAttribute(int attributeID, Dictionary<long, BlobAttributeValue[]> blobs)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    long activeStorageId = (ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool).GetActiveStorageID((IUserSession) this.UserSession);
    DbCommandParam dbCommandParam1 = dataManager.BatchParameter("attrID", DbType.Int32, (object) attributeID);
    DbCommandParam dbCommandParam2 = dataManager.BatchParameter("storID", DbType.Int64, (object) Convert.ToDouble(activeStorageId));
    foreach (KeyValuePair<long, BlobAttributeValue[]> blob in blobs)
    {
      DbCommandParam dbCommandParam3 = dataManager.BatchParameter("objID", DbType.Int64, (object) blob.Key);
      object obj = dataManager.ExecuteScalar("SELECT F_OBJECT_TYPE FROM IMS_OBJECTS WHERE F_OBJECT_ID = :objID", dataManager.Parameter("objID", (object) blob.Key));
      int objectTypeID = obj != null && obj != DBNull.Value ? Convert.ToInt32(obj) : throw new KernelException($"Объект N{blob.Key} не найден.");
      string attributesTableName = this.UserSession.DBCache.GetAttributesTableName(objectTypeID);
      string[] updateTables = this.UserSession.DBCache.GetUpdateTables(attributeID, objectTypeID, -1);
      for (int dataValue = 0; dataValue < blob.Value.Length; ++dataValue)
        DBHelper.AddBatchSQL((IUserSession) this.UserSession, false, $"INSERT INTO {attributesTableName} (F_ATTRIBUTE_ID, F_OBJECT_ID, F_INLIST_ID, F_INTEGER_VALUE, F_STRING_VALUE, F_DOUBLE_VALUE, F_DATE_VALUE) VALUES (:attrID, :objID, :inlistID, :blobID, :flName, :storID, :modifyDate)", new DbCommandParam[7]
        {
          dbCommandParam1,
          dbCommandParam3,
          dataManager.BatchParameter("inlistID", DbType.Int32, (object) dataValue),
          dataManager.BatchParameter("blobID", DbType.Int64, (object) blob.Value[dataValue].BlobID),
          dataManager.BatchParameter("flName", DbType.String, (object) blob.Value[dataValue].FileName),
          dbCommandParam2,
          dataManager.BatchParameter("modifyDate", DbType.Date, (object) blob.Value[dataValue].FileModifyDate)
        });
      if (updateTables != null)
      {
        string format = "UPDATE {0} SET F{1} = :flName, F{1}ID = :blobID, F{1}ID2 = :storID, F{1}ID3 = :modifyDate WHERE F_OBJECT_ID = :objID";
        DbCommandParam dbCommandParam4 = dataManager.BatchParameter("flName", DbType.String, (object) blob.Value[0].FileName);
        DbCommandParam dbCommandParam5 = dataManager.BatchParameter("blobID", DbType.Int64, (object) blob.Value[0].BlobID);
        DbCommandParam dbCommandParam6 = dataManager.BatchParameter("modifyDate", DbType.Date, (object) blob.Value[0].FileModifyDate);
        foreach (string str in updateTables)
          dataManager.AddBatchSQL(string.Format(format, (object) str, (object) attributeID), new DbCommandParam[5]
          {
            dbCommandParam4,
            dbCommandParam5,
            dbCommandParam6,
            dbCommandParam2,
            dbCommandParam3
          });
      }
    }
    this.UserSession.StartTransaction();
    try
    {
      dataManager.ExecuteBatchSQL();
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  public void SetImbaseTableAttributes(long tableID, List<int> attributeIDs)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    DbCommandParam dbCommandParam = dataManager.BatchParameter("pTable", DbType.Int64, (object) tableID);
    foreach (int attributeId in attributeIDs)
      DBHelper.AddBatchSQL((IUserSession) this.UserSession, false, "INSERT INTO IMS_IMBASE_ATTRS(F_OBJECT_ID, F_ATTRIBUTE_ID) VALUES (:pTable, :pAttribute)", new DbCommandParam[2]
      {
        dbCommandParam,
        dataManager.BatchParameter("pAttribute", DbType.Int32, (object) attributeId)
      });
    this.UserSession.StartTransaction();
    try
    {
      dataManager.ExecuteBatchSQL();
      this.UserSession.Commit();
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      this.EventHelper.AddToTrace(ex.Message, Consts.traceAlways, this._logFileName);
      this.EventHelper.AddToTrace(ex.StackTrace, Consts.traceAlways, this._logFileName);
    }
  }
}
