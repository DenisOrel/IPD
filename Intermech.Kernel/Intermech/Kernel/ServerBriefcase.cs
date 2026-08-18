// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.ServerBriefcase
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Briefcase;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Streams;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;


namespace Intermech.Kernel;

public sealed class ServerBriefcase : LongLifeObject, IServerBriefcase, ICategoryExportManager
{
  private DataSet _SystemImportData;
  private static Hashtable _importProgress = (Hashtable) null;
  private static Hashtable _importStructures = Hashtable.Synchronized(new Hashtable());
  private Hashtable _importedTypes;
  private int _warningCounter;
  private Dictionary<Guid, CheckThread> _checkImportThreads;
  private static Hashtable _exportProgressHashtable = Hashtable.Synchronized(new Hashtable());
  private static Hashtable _exportStructureHashtable = Hashtable.Synchronized(new Hashtable());
  private ExportCategoryHolder categoryExport = new ExportCategoryHolder();

  private string _logFileName(IUserSession session) => $"Briefcase_{session.UserName}";

  public ServerBriefcase() => this._checkImportThreads = new Dictionary<Guid, CheckThread>(1);

  public BriefcaseImportProgress ImportProgress(Guid NumOfBriefcase)
  {
    return ServerBriefcase._importProgress != null && ServerBriefcase._importProgress[(object) NumOfBriefcase] != null ? (BriefcaseImportProgress) ServerBriefcase._importProgress[(object) NumOfBriefcase] : (BriefcaseImportProgress) null;
  }

  public void CheckBriefcaseMetadata(
    Guid sessionGUID,
    Guid numOfBriefcase,
    string briefcaseFolder,
    CheckOptions options)
  {
    if (!Directory.Exists(briefcaseFolder))
      throw new KernelExceptionID(sc_12860.ssp_appserver_12861(1300292990), (object) briefcaseFolder);
    string ErrorMessage = string.Empty;
    DataSet[] dataSetArray = BriefcaseProcs.CheckBriefcase(UserSession.GetSessionByID(sessionGUID), new BriefcaseLocation(BriefcaseLocation.Computer.Server, briefcaseFolder), out ErrorMessage) ? BriefcaseProcs.ReadMetaDataXML(briefcaseFolder) : throw new KernelException(ErrorMessage);
    if (dataSetArray == null)
      throw new KernelExceptionID(sc_12860.ssp_appserver_12862(613504737), (object) briefcaseFolder);
    this.CheckBriefcaseMetadata(sessionGUID, numOfBriefcase, dataSetArray[0], dataSetArray[1], options);
  }

  public void CheckBriefcaseMetadata(
    Guid sessionGUID,
    Guid numOfBriefcase,
    DataSet metaData,
    DataSet importMetaData,
    CheckOptions options)
  {
    CheckThread checkThread = new CheckThread(UserSession.GetSessionByID(sessionGUID) as UserSession, metaData, importMetaData, options, numOfBriefcase);
    checkThread.SetImportProgressEvent += new SetImportProgressEventHandler(this.SetBriefcaseImportProgress);
    this._checkImportThreads.Add(numOfBriefcase, checkThread);
    checkThread.Start($"Check_{numOfBriefcase}");
  }

  public void CancelCheck(Guid NumOfBriefcase)
  {
    if (this._checkImportThreads.ContainsKey(NumOfBriefcase))
    {
      this._checkImportThreads[NumOfBriefcase].Cancel();
      this._checkImportThreads.Remove(NumOfBriefcase);
    }
    if (!ServerBriefcase._importProgress.Contains((object) NumOfBriefcase))
      return;
    ServerBriefcase._importProgress.Remove((object) NumOfBriefcase);
  }

  private void SetBriefcaseImportProgress(object sender, SetImportProgressEventArgs e)
  {
    if (ServerBriefcase._importProgress == null)
      ServerBriefcase._importProgress = Hashtable.Synchronized(new Hashtable());
    if (ServerBriefcase._importProgress[(object) e.Briefcase] == null)
      ServerBriefcase._importProgress.Add((object) e.Briefcase, (object) e.ImportProgress);
    else
      ServerBriefcase._importProgress[(object) e.Briefcase] = (object) e.ImportProgress;
  }

  public void BriefcaseTransferStart(
    Guid NumOfBriefcase,
    BriefcaseImportProperties ImportProperties,
    BriefcaseFilesStructure FileStructure)
  {
    if (ServerBriefcase._importStructures == null)
      ServerBriefcase._importStructures = Hashtable.Synchronized(new Hashtable());
    ServerBriefcase._importStructures.Add((object) NumOfBriefcase, (object) new BriefcaseImportStructure(ImportProperties, FileStructure));
    if (ServerBriefcase._importProgress == null)
      ServerBriefcase._importProgress = Hashtable.Synchronized(new Hashtable());
    ServerBriefcase._importProgress.Add((object) NumOfBriefcase, (object) new BriefcaseImportProgress(FileStructure == null ? OperationType.Importing : OperationType.Unpacking));
  }

  public void BriefcaseTransferStep(Guid numOfBriefcase, byte[] bytes, int bytesLength)
  {
    FileInfo fileInfo = new FileInfo(Path.Combine(((BriefcaseImportStructure) ServerBriefcase._importStructures[(object) numOfBriefcase]).ImportProperties.ServerTempFolder, BriefcaseConsts.prefixPack + numOfBriefcase.ToString()));
    using (FileStream fileStream = new FileStream(fileInfo.FullName, fileInfo.Exists ? FileMode.Append : FileMode.Create, FileAccess.Write))
    {
      try
      {
        if (bytes == null || bytesLength <= 0)
          return;
        fileStream.Write(bytes, 0, bytesLength);
      }
      catch (Exception ex)
      {
        ((BriefcaseImportProgress) ServerBriefcase._importProgress[(object) numOfBriefcase]).Operation = OperationType.Error;
        ((BriefcaseImportProgress) ServerBriefcase._importProgress[(object) numOfBriefcase]).ErrorException = new Exception(LocalizationHolder.rm.GetString("Kernel_853"), ex);
      }
    }
  }

  public void StartImport(Guid sessionGUID, Guid NumOfBriefcase)
  {
    BriefcaseImporter briefcaseImporter = new BriefcaseImporter(UserSession.GetSessionByID(sessionGUID) as UserSession, NumOfBriefcase, ServerBriefcase._importStructures[(object) NumOfBriefcase] as BriefcaseImportStructure);
    briefcaseImporter.SetImportProgressEvent += new SetImportProgressEventHandler(this.SetBriefcaseImportProgress);
    briefcaseImporter.Importing();
  }

  public string GetImportLogPath(Guid sessionGUID, Guid NumOfBriefcase)
  {
    FileInfo fileInfo = new FileInfo(((UserSession.GetSessionByID(sessionGUID) as UserSession).EventLogHelper as EventLogHelper).GetFullTraceFileName(string.Format(BriefcaseConsts.logImportName, (object) NumOfBriefcase)));
    return fileInfo.Exists ? fileInfo.FullName : string.Empty;
  }

  public byte[] GetImportLog(Guid sessionGUID, Guid NumOfBriefcase)
  {
    FileInfo fileInfo = new FileInfo(((UserSession.GetSessionByID(sessionGUID) as UserSession).EventLogHelper as EventLogHelper).GetFullTraceFileName(string.Format(BriefcaseConsts.logImportName, (object) NumOfBriefcase)));
    if (!fileInfo.Exists)
      return (byte[]) null;
    using (MemoryStream outStream = new MemoryStream())
    {
      using (FileStream inStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
      {
        try
        {
          if (!(ServerServices.GetService(typeof (IPackedStream)) is IPackedStream service))
            return (byte[]) null;
          service.PackStream((Stream) outStream, (Stream) inStream, 9);
          return outStream.ToArray();
        }
        catch
        {
          return (byte[]) null;
        }
      }
    }
  }

  public void CancelImport(Guid NumOfBriefcase)
  {
    if (ServerBriefcase._importStructures[(object) NumOfBriefcase] != null)
      ServerBriefcase._importStructures.Remove((object) NumOfBriefcase);
    if (ServerBriefcase._importProgress[(object) NumOfBriefcase] == null)
      return;
    ServerBriefcase._importProgress.Remove((object) NumOfBriefcase);
  }

  private int ImportLifecycleLevel(IUserSession session, int levelID)
  {
    if (levelID == 0)
      return 0;
    ServerBriefcase.CategoryIDStruct key = new ServerBriefcase.CategoryIDStruct(8, levelID);
    object importedType = this._importedTypes[(object) key];
    if (importedType != null)
      return (int) importedType;
    DataRow[] dataRowArray1 = this._SystemImportData.Tables["IMS_LEVELS"].Select("F_LEVEL_ID = " + levelID.ToString());
    if (dataRowArray1.Length == 0)
    {
      (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace(LocalizationHolder.rm.GetString("Kernel_855") + (object) levelID, Consts.traceAlways, this._logFileName(session));
      ++this._warningCounter;
      return 0;
    }
    DataRow[] dataRowArray2 = (session as UserSession).DBCache.GetTable("IMS_LEVELS").Select("F_GUID = " + SqlHelper.QString(dataRowArray1[0]["F_GUID"].ToString()));
    int int32;
    if (dataRowArray2.Length == 0)
    {
      int32 = session.GetLifecycleLevelCollection().Create(dataRowArray1[0]["F_LEVEL_NAME"].ToString(), dataRowArray1[0]["F_LITERA"].ToString(), this.TranslateAreaID(session, dataRowArray1[0]["F_AREA_ID"].ToString()), new Guid(dataRowArray1[0]["F_GUID"].ToString()), Convert.ToBoolean(dataRowArray1[0]["F_DEFAULT"]));
      if (dataRowArray1[0]["F_ICON"] != DBNull.Value)
        session.GetLifecycleLevel(int32).LevelIcon = (byte[]) dataRowArray1[0]["F_ICON"];
    }
    else
    {
      int32 = Convert.ToInt32(dataRowArray2[0]["F_LEVEL_ID"]);
      IDBLifecycleLevelType lifecycleLevel = session.GetLifecycleLevel(int32);
      lifecycleLevel.LevelName = dataRowArray1[0]["F_LEVEL_NAME"].ToString();
      lifecycleLevel.Litera = dataRowArray1[0]["F_LITERA"].ToString();
      lifecycleLevel.IsDefaultLevel = Convert.ToBoolean(dataRowArray1[0]["F_DEFAULT"]);
      if (dataRowArray1[0]["F_ICON"] != DBNull.Value)
        lifecycleLevel.LevelIcon = (byte[]) dataRowArray1[0]["F_ICON"];
    }
    this._importedTypes[(object) key] = (object) int32;
    return int32;
  }

  private string ImportLanguage(IUserSession session, string languageID)
  {
    if (languageID.Trim() == string.Empty)
      return languageID;
    ServerBriefcase.CategoryIDStruct key = new ServerBriefcase.CategoryIDStruct(9, SqlHelper.GetCharID(languageID[0]));
    object importedType = this._importedTypes[(object) key];
    if (importedType != null)
      return importedType.ToString();
    DataRow[] dataRowArray1 = this._SystemImportData.Tables["IMS_LANGUAGES"].Select("F_LANGUAGE_ID = " + SqlHelper.QString(languageID));
    if (dataRowArray1.Length == 0)
    {
      (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace(LocalizationHolder.rm.GetString("Kernel_856") + languageID, Consts.traceAlways, this._logFileName(session));
      ++this._warningCounter;
      return string.Empty;
    }
    DataRow[] dataRowArray2 = (session as UserSession).DBCache.GetTable("IMS_LANGUAGES").Select("F_LANGUAGE_NAME = " + SqlHelper.QString(Convert.ToString(dataRowArray1[0]["F_LANGUAGE_NAME"])));
    string str = dataRowArray2.Length != 0 ? Convert.ToString(dataRowArray2[0]["F_LANGUAGE_ID"]) : session.GetLanguageCollection().Create(Convert.ToString(dataRowArray1[0]["F_LANGUAGE_NAME"]), new Guid(Convert.ToString(dataRowArray1[0]["F_GUID"])), Convert.ToString(dataRowArray1[0]["F_CULTURE_ID"])).ToString();
    this._importedTypes[(object) key] = (object) str;
    return str;
  }

  private char ImportArea(IUserSession session, char areaID)
  {
    ServerBriefcase.CategoryIDStruct key = new ServerBriefcase.CategoryIDStruct(11, SqlHelper.GetCharID(areaID));
    object importedType = this._importedTypes[(object) key];
    if (importedType != null)
      return (char) importedType;
    DataRow[] dataRowArray1 = this._SystemImportData.Tables["IMS_SUBJECT_AREAS"].Select("F_AREA_ID = " + SqlHelper.QString(areaID.ToString()));
    if (dataRowArray1.Length == 0)
    {
      (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace(LocalizationHolder.rm.GetString("Kernel_857") + areaID.ToString(), Consts.traceAlways, this._logFileName(session));
      ++this._warningCounter;
      return Convert.ToChar("");
    }
    DataRow[] dataRowArray2 = (session as UserSession).DBCache.GetTable("IMS_SUBJECT_AREAS").Select("F_AREA_NAME = " + SqlHelper.QString(dataRowArray1[0]["F_AREA_NAME"].ToString()));
    char ch = dataRowArray2.Length != 0 ? Convert.ToChar(dataRowArray2[0]["F_AREA_ID"]) : session.GetSubjectAreaCollection().Create(dataRowArray1[0]["F_AREA_NAME"].ToString(), dataRowArray1[0]["F_AREA_NOTE"].ToString(), new Guid(dataRowArray1[0]["F_GUID"].ToString()));
    this._importedTypes[(object) key] = (object) ch;
    return ch;
  }

  private string TranslateAreaID(IUserSession session, string areaID)
  {
    if (areaID == string.Empty)
      return string.Empty;
    string empty = string.Empty;
    foreach (char areaID1 in areaID)
      empty += this.ImportArea(session, areaID1).ToString();
    return empty;
  }

  private int ImportAttributeGroup(IUserSession session, int groupID)
  {
    ServerBriefcase.CategoryIDStruct key = new ServerBriefcase.CategoryIDStruct(12, groupID);
    object importedType = this._importedTypes[(object) key];
    if (importedType != null)
      return (int) importedType;
    DataRow[] dataRowArray1 = this._SystemImportData.Tables["IMS_ATTR_GROUPS"].Select("F_GROUP_ID = " + groupID.ToString());
    if (dataRowArray1.Length == 0)
    {
      (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_858"), (object) groupID), Consts.traceAlways, this._logFileName(session));
      ++this._warningCounter;
      return -1;
    }
    DataRow[] dataRowArray2 = (session as UserSession).DBCache.GetTable("IMS_OBJECT_TYPES").Select("F_GROUP_NAME = " + SqlHelper.QString(dataRowArray1[0]["F_GROUP_NAME"].ToString()));
    int aGroupID = dataRowArray2.Length != 0 ? Convert.ToInt32(dataRowArray2[0]["F_GROUP_ID"]) : session.GetAttributesGroupCollection().Create(dataRowArray1[0]["F_GROUP_NAME"].ToString(), dataRowArray1[0]["F_NOTE"].ToString(), dataRowArray1[0]["F_LANGUAGE_ID"].ToString(), dataRowArray1[0]["F_AREA_ID"].ToString(), new Guid(dataRowArray1[0]["F_AREA_ID"].ToString()));
    IDBAttributesGroup attributesGroup = session.GetAttributesGroup(aGroupID);
    DataTable dataTable = attributesGroup.Attributes.Select("");
    foreach (DataRow dataRow in this._SystemImportData.Tables["IMS_ATTR_IN_GROUPS"].Select("F_GROUP_ID = " + groupID.ToString()))
    {
      int attributeID = this.ImportAttribute(session, Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]));
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (Convert.ToInt32(row["F_ATTRIBUTE_ID"]) == attributeID)
        {
          attributeID = 0;
          break;
        }
      }
      if (attributeID > 0)
        attributesGroup.IncludeAttribute(attributeID);
    }
    this._importedTypes[(object) key] = (object) aGroupID;
    return aGroupID;
  }

  private int ImportObjectType(IUserSession session, int objectTypeID)
  {
    if (objectTypeID < 0)
      return objectTypeID;
    ServerBriefcase.CategoryIDStruct key = new ServerBriefcase.CategoryIDStruct(4, objectTypeID);
    object importedType = this._importedTypes[(object) key];
    if (importedType != null)
      return (int) importedType;
    DataRow[] dataRowArray1 = this._SystemImportData.Tables["IMS_OBJECT_TYPES"].Select("F_OBJECT_TYPE = " + objectTypeID.ToString());
    if (dataRowArray1.Length == 0)
    {
      (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_859"), (object) objectTypeID), Consts.traceAlways, this._logFileName(session));
      ++this._warningCounter;
      return -1;
    }
    Guid anObjectTypeGuid = new Guid(dataRowArray1[0]["F_GUID"].ToString());
    DataRow[] dataRowArray2 = (session as UserSession).DBCache.GetTable("IMS_OBJECT_TYPES").Select("F_GUID = " + SqlHelper.QString(anObjectTypeGuid.ToString()));
    ObjectTypeProperties typeProperties = new ObjectTypeProperties(dataRowArray1[0]);
    int num = -1;
    DataRow[] dataRowArray3 = this._SystemImportData.Tables["IMS_OBJTYPES_TREE"].Select("F_OBJECT_TYPE = " + objectTypeID.ToString());
    if (dataRowArray3.Length != 0)
      num = Convert.ToInt32(dataRowArray3[0]["F_PARENT_ID"]);
    IDBObjectType objectType1;
    int objectType2;
    if (dataRowArray2.Length != 0)
    {
      objectType1 = session.GetObjectType(anObjectTypeGuid);
      typeProperties.ObjectType = objectType1.ObjectType;
      objectType1.PropertiesStructure = typeProperties;
      objectType1.ParentTypeID = num;
      objectType2 = objectType1.ObjectType;
    }
    else
    {
      typeProperties.ObjectType = 0;
      objectType2 = session.GetObjectTypeCollection(-1).Create(typeProperties);
      objectType1 = session.GetObjectType(objectType2);
      if (num > -1)
        objectType1.ParentTypeID = num;
    }
    objectType1.Icon = dataRowArray1[0]["F_ICON"] != DBNull.Value ? (byte[]) dataRowArray1[0]["F_ICON"] : new byte[0];
    IDBAttribute4ObjectTypeCollection attributes = objectType1.Attributes as IDBAttribute4ObjectTypeCollection;
    attributes.Select("");
    DataRow[] dataRowArray4 = this._SystemImportData.Tables["IMS_ATTR4OBJ_TYPES"].Select("F_OBJECT_TYPE = " + objectTypeID.ToString());
    this._SystemImportData.Tables["IMS_FORMULA_ATTRS"].Select("F_OBJECT_TYPE = " + objectTypeID.ToString());
    DataRow[] dataRowArray5 = new DataRow[dataRowArray4.Length];
    foreach (DataRow row in dataRowArray4)
    {
      int attributeID = this.ImportAttribute(session, Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
      IDBAttributeType4Object attributeById = attributes.GetAttributeByID(attributeID, false) as IDBAttributeType4Object;
      Attribute4ObjectTypeProperties attrProperties = new Attribute4ObjectTypeProperties(row);
      attrProperties.ObjectType = objectType2;
      attrProperties.AttributeID = attributeID;
      if (attributeById == null)
        attributeById = attributes.Create(attrProperties);
      else
        attributeById.Attribute4ObjectPropertiesStructure = attrProperties;
      DataRow[] fromRows = this._SystemImportData.Tables["IMS_POSSIBLE_VALUES"].Select($"F_OBJECT_TYPE = {objectTypeID} AND F_ATTRIBUTE_ID = {row["F_ATTRIBUTE_ID"]}");
      DataTable dataTable = this._SystemImportData.Tables["IMS_POSSIBLE_VALUES"].Clone();
      SqlHelper.AssignRows(dataTable, (IEnumerable<DataRow>) fromRows);
      attributeById.SetPossibleValues(dataTable);
    }
    this._importedTypes[(object) key] = (object) objectType2;
    return objectType2;
  }

  private int ImportRelationType(IUserSession session, int relationTypeID)
  {
    if (relationTypeID < 0)
      return relationTypeID;
    object importedType = this._importedTypes[(object) new ServerBriefcase.CategoryIDStruct(6, relationTypeID)];
    if (importedType != null)
      return (int) importedType;
    if (this._SystemImportData.Tables["IMS_RELATION_TYPES"].Select("F_RELATION_TYPE = " + relationTypeID.ToString()).Length != 0)
      return -1;
    (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_860"), (object) relationTypeID), Consts.traceAlways, this._logFileName(session));
    ++this._warningCounter;
    return -1;
  }

  private int ImportAttribute(IUserSession session, int attributeID)
  {
    ServerBriefcase.CategoryIDStruct key = new ServerBriefcase.CategoryIDStruct(3, attributeID);
    object importedType = this._importedTypes[(object) key];
    if (importedType != null)
      return (int) importedType;
    DataRow[] dataRowArray1 = this._SystemImportData.Tables["IMS_ATTRIBUTES"].Select("F_ATTRIBUTE_ID = " + attributeID.ToString());
    if (dataRowArray1.Length == 0)
    {
      (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_861"), (object) attributeID), Consts.traceAlways, this._logFileName(session));
      ++this._warningCounter;
      return -1;
    }
    Guid anAttributeGuid = new Guid(dataRowArray1[0]["F_GUID"].ToString());
    DataRow[] dataRowArray2 = (session as UserSession).DBCache.GetTable("IMS_ATTRIBUTES").Select("F_GUID = " + SqlHelper.QString(anAttributeGuid.ToString()));
    AttributeTypeProperties attrProperties = new AttributeTypeProperties(dataRowArray1[0]);
    attrProperties.AreaID = this.TranslateAreaID(session, attrProperties.AreaID);
    attrProperties.LanguageID = this.ImportLanguage(session, attrProperties.LanguageID);
    attrProperties.LevelID = this.ImportLifecycleLevel(session, attrProperties.LevelID);
    foreach (DataRow dataRow in (session as UserSession).DBCache.GetTable("IMS_FORMULA_ATTRS").Select("F_FORMULA_ID = " + attributeID.ToString()))
      this.ImportAttribute(session, Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]));
    switch (attrProperties.FieldType)
    {
      case FieldTypes.ftObjectLink:
      case FieldTypes.ftObjectLinkByID:
        if (attrProperties.SizeType >= 0L)
        {
          attrProperties.SizeType = Convert.ToInt64(this.ImportObjectType(session, Convert.ToInt32(attrProperties.SizeType)));
          break;
        }
        break;
    }
    IDBAttributeType attributeType;
    int attributeId;
    if (dataRowArray2.Length != 0)
    {
      attributeType = session.GetAttributeType(anAttributeGuid);
      attrProperties.AttributeID = attributeType.AttributeID;
      attributeType.PropertiesStructure = attrProperties;
      attributeId = attributeType.AttributeID;
    }
    else
    {
      attrProperties.AttributeID = 0;
      attributeId = session.GetAttributeTypeCollection(-1).Create(attrProperties);
      attributeType = session.GetAttributeType(attributeId);
    }
    DataTable dataTable = this._SystemImportData.Tables["IMS_POSSIBLE_VALUES"].Clone();
    DataRow[] fromRows = this._SystemImportData.Tables["IMS_POSSIBLE_VALUES"].Select($"F_ATTRIBUTE_ID = {attributeID} AND F_OBJECT_TYPE = -1 AND F_RELATION_TYPE = -1");
    SqlHelper.AssignRows(dataTable, (IEnumerable<DataRow>) fromRows);
    attributeType.SetPossibleValues(dataTable);
    this._importedTypes[(object) key] = (object) attributeId;
    return attributeId;
  }

  public void PauseImport()
  {
  }

  public DateTime SystemModifyDate(Guid sessionGUID)
  {
    return (UserSession.GetSessionByID(sessionGUID) as UserSession).DBCache.ModifyDate;
  }

  public List<CheckMetadataLogItem> CheckMetadata(
    Guid sessionGUID,
    string BriefcaseFolder,
    bool System)
  {
    if (!Directory.Exists(BriefcaseFolder))
      throw new KernelExceptionID(sc_12860.ssp_appserver_12863(1455822160), (object) BriefcaseFolder);
    string ErrorMessage = string.Empty;
    if (!BriefcaseProcs.CheckBriefcase(UserSession.GetSessionByID(sessionGUID), new BriefcaseLocation(BriefcaseLocation.Computer.Server, BriefcaseFolder), out ErrorMessage))
      throw new KernelException(ErrorMessage);
    DataSet MetaData = new DataSet("SYSTEM");
    MetaData.ReadXmlSchema(Path.Combine(BriefcaseFolder, "Metadata.xsd"));
    int num = (int) MetaData.ReadXml(Path.Combine(BriefcaseFolder, "Metadata.xml"));
    return this.CheckMetadata(sessionGUID, MetaData, System);
  }

  public List<CheckMetadataLogItem> CheckMetadata(Guid sessionGUID, DataSet MetaData, bool System)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGUID);
    List<CheckMetadataLogItem> checkMetadataLogItemList = new List<CheckMetadataLogItem>();
    int[] numArray = new int[7]{ 3, 4, 6, 7, 9, 8, 11 };
    CheckOptions options = CheckOptions.IsErrorAlways;
    foreach (int num in numArray)
    {
      DataTable dataTable = (DataTable) null;
      switch (num)
      {
        case 3:
          dataTable = MetaData.Tables["IMS_ATTRIBUTES"];
          break;
        case 4:
          dataTable = MetaData.Tables["IMS_OBJECT_TYPES"];
          break;
        case 6:
          dataTable = MetaData.Tables["IMS_RELATION_TYPES"];
          break;
        case 7:
          dataTable = MetaData.Tables["IMS_LC_STEPS"];
          break;
        case 8:
          dataTable = MetaData.Tables["IMS_LEVELS"];
          break;
        case 9:
          dataTable = MetaData.Tables["IMS_LANGUAGES"];
          break;
        case 11:
          dataTable = MetaData.Tables["IMS_SUBJECT_AREAS"];
          break;
        case 12:
          dataTable = MetaData.Tables["IMS_ATTR_GROUPS"];
          break;
      }
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        ICheckItem checkItem = (ICheckItem) null;
        if (!System || SystemGUIDs.IsSystemGUID(row["F_GUID"].ToString()))
        {
          switch (num)
          {
            case 3:
              checkItem = (ICheckItem) new CheckAttributeType(sessionById as UserSession, MetaData, row, options);
              break;
            case 4:
              checkItem = (ICheckItem) new CheckObjectType(sessionById as UserSession, MetaData, row, options);
              break;
            case 6:
              checkItem = (ICheckItem) new CheckRelationType(sessionById as UserSession, MetaData, row, options);
              break;
            case 7:
              checkItem = (ICheckItem) new CheckLCStep(sessionById as UserSession, MetaData, row, options);
              break;
            case 8:
              checkItem = (ICheckItem) new CheckLCLevel(sessionById as UserSession, MetaData, row, options);
              break;
            case 9:
              checkItem = (ICheckItem) new CheckLanguage(sessionById as UserSession, MetaData, row, options);
              break;
            case 11:
              checkItem = (ICheckItem) new CheckSubjectArea(sessionById as UserSession, MetaData, row, options);
              break;
            case 12:
              checkItem = (ICheckItem) new CheckAttributesGroup(sessionById as UserSession, MetaData, row, options);
              break;
          }
          checkItem.Initialize();
          if (checkItem.Existing)
            checkItem.Check();
          if ((checkItem as ILogged<CheckMetadataLogItem>).Log.Count > 0)
            checkMetadataLogItemList.AddRange((IEnumerable<CheckMetadataLogItem>) (checkItem as ILogged<CheckMetadataLogItem>).Log);
        }
      }
    }
    return checkMetadataLogItemList;
  }

  public Guid StartExport(Guid sessionGUID, BriefcaseExportProperties exportProperties)
  {
    this.CheckExportRights(sessionGUID);
    Guid guid = Guid.NewGuid();
    BriefcaseExportStructure bes = new BriefcaseExportStructure(exportProperties);
    if (!bes.ExportProperties.ServerPlacement)
      bes.ExportProperties.ServerFolder = ServerBriefcaseConsts.ServerBriefcaseContainerFolderPath + ServerBriefcaseConsts.ServerBriefcaseFolderPrefix + guid.ToString();
    bes.ExportProperties.ServerFolder = ServerBriefcaseProcs.VerifyBriefcaseFolderSyntax(bes.ExportProperties.ServerFolder);
    BriefcaseExporter briefcaseExporter = new BriefcaseExporter(UserSession.GetSessionByID(sessionGUID) as UserSession, guid, bes);
    ServerBriefcase._exportStructureHashtable[(object) guid] = (object) bes;
    briefcaseExporter.SetExportProgressEvent += new SetExportProgressHandler(this.SetBriefcaseExportProgress);
    this.SetBriefcaseExportProgress((object) this, guid, new BriefcaseExportProgress(ExportOperationType.Idle)
    {
      Percent = 0
    });
    briefcaseExporter.Exporting();
    return guid;
  }

  public void CancelExport(Guid briefcaseGuid)
  {
  }

  public ExportAttribute[] ValidateExportAttributes(
    Guid sessionGUID,
    ExportAttribute[] aExportAttributes)
  {
    this.CheckExportRights(sessionGUID);
    if (aExportAttributes == null || aExportAttributes.Length == 0)
      return (ExportAttribute[]) null;
    IUserSession sessionById = UserSession.GetSessionByID(sessionGUID);
    int objectTypeId = MetaDataHelper.GetObjectType(new Guid("cad00014-306c-11d8-b4e9-00304f19f545")).ObjectTypeID;
    List<ExportAttribute> exportAttributeList = new List<ExportAttribute>();
    for (int index1 = 0; index1 < aExportAttributes.Length; ++index1)
    {
      if (aExportAttributes[index1].Category == 1)
      {
        List<object> objectList = (List<object>) null;
        DataTable dataTable = sessionById.GetObjectCollection(objectTypeId).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
        {
          (object) -2
        }));
        List<long> longList = new List<long>();
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          longList.Add(Convert.ToInt64(row[0]));
        for (int index2 = 0; index2 < aExportAttributes[index1].Identifiers.Length; ++index2)
        {
          if (longList.IndexOf(Convert.ToInt64(aExportAttributes[index1].Identifiers[index2])) != -1)
          {
            if (objectList == null)
              objectList = new List<object>();
            objectList.Add(aExportAttributes[index1].Identifiers[index2]);
          }
        }
        if (objectList != null)
          exportAttributeList.Add(new ExportAttribute(aExportAttributes[index1].Category, objectList.ToArray()));
      }
    }
    return exportAttributeList.Count > 0 ? exportAttributeList.ToArray() : (ExportAttribute[]) null;
  }

  public void CheckExportRights(Guid sessionGUID)
  {
    new BriefcaseExportRightsChecker(UserSession.GetSessionByID(sessionGUID) as UserSession).CheckAccess();
  }

  private void SetBriefcaseExportProgress(
    object sender,
    Guid NumOfBriefcase,
    BriefcaseExportProgress briefcaseExportProgress)
  {
    if (ServerBriefcase._exportProgressHashtable[(object) NumOfBriefcase] == null)
      ServerBriefcase._exportProgressHashtable.Add((object) NumOfBriefcase, (object) briefcaseExportProgress);
    else
      ServerBriefcase._exportProgressHashtable[(object) NumOfBriefcase] = (object) briefcaseExportProgress;
  }

  public BriefcaseExportProgress GetExportProgress(Guid NumOfBriefcase)
  {
    return ServerBriefcase._exportProgressHashtable != null && ServerBriefcase._exportProgressHashtable[(object) NumOfBriefcase] != null ? (BriefcaseExportProgress) ServerBriefcase._exportProgressHashtable[(object) NumOfBriefcase] : (BriefcaseExportProgress) null;
  }

  public DataTable GetDatatable(
    Guid sessionGUID,
    string tableName,
    string condition,
    string order)
  {
    this.CheckExportRights(sessionGUID);
    IUserSession sessionById = UserSession.GetSessionByID(sessionGUID);
    if (condition != null && condition != string.Empty)
      condition = " WHERE " + condition;
    if (order != null && order != string.Empty)
      order = " WHERE " + order;
    return (sessionById as UserSession).DataManager.ExecuteDataTable($"SELECT * FROM {tableName}{condition}{order}");
  }

  public DataSet GetDataset(Guid sessionGUID, string[] tableNames, bool includeLocalization)
  {
    this.CheckExportRights(sessionGUID);
    IUserSession sessionById = UserSession.GetSessionByID(sessionGUID);
    if (tableNames.Length == 0)
      return (DataSet) null;
    IDbManager dataManager = (sessionById as UserSession).DataManager;
    DataSet dataset;
    if (tableNames[0] == "SYSTEM")
    {
      dataset = ((sessionById as UserSession).DBCache as CacheDataset)._DBSet.Copy();
      DataTable table = dataset.Tables["IMS_ATTRIBUTES"];
      for (int index = table.Rows.Count - 1; index >= 0; --index)
      {
        if (Convert.ToInt32(table.Rows[index]["F_ATTRIBUTE_ID"]) < 0)
          table.Rows.RemoveAt(index);
      }
      table.Columns.Add("F_OBJECT_GUID", Type.GetType("System.String"));
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
      {
        switch ((FieldTypes) Convert.ToInt32(row["F_ATTRIBUTE_TYPE"]))
        {
          case FieldTypes.ftExternalLink:
          case FieldTypes.ftMeasured:
            if (row["F_SIZE_TYPE"] != DBNull.Value)
            {
              long int64 = Convert.ToInt64(row["F_SIZE_TYPE"]);
              if (int64 > 0L)
              {
                object obj = dataManager.ExecuteScalar("SELECT F_GUID FROM IMS_GUID WHERE F_OBJECT_ID = " + int64.ToString());
                if (obj != null && obj != DBNull.Value)
                {
                  row["F_OBJECT_GUID"] = obj;
                  continue;
                }
                row["F_SIZE_TYPE"] = (object) 0;
                continue;
              }
              continue;
            }
            continue;
          default:
            continue;
        }
      }
      table.AcceptChanges();
    }
    else
    {
      dataset = (sessionById as UserSession).DataManager.ExecuteDataSet(tableNames[0], "SELECT * FROM " + tableNames[0]);
      for (int index = 1; index < tableNames.Length; ++index)
      {
        DataTable table = (sessionById as UserSession).DataManager.ExecuteDataTable("SELECT * FROM " + tableNames[index]);
        dataset.Tables.Add(table);
      }
    }
    if (includeLocalization)
    {
      DataTable table = dataManager.ExecuteDataTable("SELECT * FROM IMS_LOCALIZATION");
      table.TableName = "IMS_LOCALIZATION";
      dataset.Tables.Add(table);
    }
    return dataset;
  }

  public BriefcaseFilesStructure GetBriefcaseFilesStructure(Guid sessionGUID, Guid NumOfBriefcase)
  {
    this.CheckExportRights(sessionGUID);
    if (!ServerBriefcase._exportStructureHashtable.ContainsKey((object) NumOfBriefcase) || !ServerBriefcase._exportProgressHashtable.ContainsKey((object) NumOfBriefcase))
      return (BriefcaseFilesStructure) null;
    BriefcaseExportStructure briefcaseExportStructure = ServerBriefcase._exportStructureHashtable[(object) NumOfBriefcase] as BriefcaseExportStructure;
    if ((ServerBriefcase._exportProgressHashtable[(object) NumOfBriefcase] as BriefcaseExportProgress).Operation != ExportOperationType.Finished)
      return (BriefcaseFilesStructure) null;
    if (briefcaseExportStructure.ExportProperties.ServerPlacement)
      return (BriefcaseFilesStructure) null;
    ArrayList arrayList1 = new ArrayList();
    ArrayList arrayList2 = new ArrayList();
    string serverFolder = briefcaseExportStructure.ExportProperties.ServerFolder;
    if (!Directory.Exists(serverFolder))
      return (BriefcaseFilesStructure) null;
    ArrayList arrayList3 = new ArrayList();
    foreach (string directory in Directory.GetDirectories(serverFolder, "*.*"))
    {
      if (BriefcaseConsts.BriefcaseFolders.IndexOf(Path.GetFileName(directory).ToUpper()) != -1)
        arrayList3.Add((object) Path.GetFileName(directory));
    }
    foreach (string file in Directory.GetFiles(serverFolder, "*.*"))
    {
      if (BriefcaseConsts.BriefcaseFiles.IndexOf(Path.GetFileName(file).ToUpper()) != -1)
        arrayList2.Add((object) Path.GetFileName(file));
    }
    for (int index = 0; index < arrayList3.Count; ++index)
      this.CollectFolderStructure(serverFolder, (string) arrayList3[index], arrayList1, arrayList2);
    return new BriefcaseFilesStructure(arrayList2, arrayList1);
  }

  private void CollectFolderStructure(
    string lRootFolder,
    string lRelativeFolder,
    ArrayList lFolderList,
    ArrayList lFileList)
  {
    string path = lRootFolder + lRelativeFolder;
    lFolderList.Add((object) lRelativeFolder);
    foreach (string file in Directory.GetFiles(path, "*.*"))
      lFileList.Add((object) file.Substring(lRootFolder.Length));
    foreach (string directory in Directory.GetDirectories(path, "*.*"))
      this.CollectFolderStructure(lRootFolder, directory.Substring(lRootFolder.Length), lFolderList, lFileList);
  }

  public ImFileReader GetBriefcaseFile(Guid sessionGUID, Guid NumOfBriefcase, string filePath)
  {
    BriefcaseExportStructure briefcaseExportStructure = ServerBriefcase._exportStructureHashtable[(object) NumOfBriefcase] as BriefcaseExportStructure;
    BriefcaseExportProgress briefcaseExportProgress = ServerBriefcase._exportProgressHashtable[(object) NumOfBriefcase] as BriefcaseExportProgress;
    if (briefcaseExportStructure == null || briefcaseExportProgress == null || briefcaseExportStructure.ExportProperties.ServerPlacement || briefcaseExportProgress.Operation != ExportOperationType.Finished)
      return (ImFileReader) null;
    string str = briefcaseExportStructure.ExportProperties.ServerFolder + Path.DirectorySeparatorChar.ToString() + filePath;
    return File.Exists(str) ? new ImFileReader(str) : (ImFileReader) null;
  }

  public ImFileReader GetExportLog(Guid sessionGUID, Guid NumOfBriefcase)
  {
    this.CheckExportRights(sessionGUID);
    if (!ServerBriefcase._exportStructureHashtable.ContainsKey((object) NumOfBriefcase) || !ServerBriefcase._exportProgressHashtable.ContainsKey((object) NumOfBriefcase))
      return (ImFileReader) null;
    BriefcaseExportProgress briefcaseExportProgress = ServerBriefcase._exportProgressHashtable[(object) NumOfBriefcase] as BriefcaseExportProgress;
    if (briefcaseExportProgress.Operation == ExportOperationType.Finished || briefcaseExportProgress.Operation == ExportOperationType.Error)
    {
      string str = $"{(ServerBriefcase._exportStructureHashtable[(object) NumOfBriefcase] as BriefcaseExportStructure).ExportProperties.ServerFolder}{Path.DirectorySeparatorChar.ToString()}export.log";
      if (File.Exists(str))
        return new ImFileReader(str);
    }
    return (ImFileReader) null;
  }

  public void DisposeBriefcase(Guid NumOfBriefcase)
  {
    if (!ServerBriefcase._exportStructureHashtable.ContainsKey((object) NumOfBriefcase) || !ServerBriefcase._exportProgressHashtable.ContainsKey((object) NumOfBriefcase))
      return;
    BriefcaseExportProgress briefcaseExportProgress = ServerBriefcase._exportProgressHashtable[(object) NumOfBriefcase] as BriefcaseExportProgress;
    if (briefcaseExportProgress.Operation != ExportOperationType.Finished && briefcaseExportProgress.Operation != ExportOperationType.Error)
      return;
    BriefcaseExportStructure briefcaseExportStructure = ServerBriefcase._exportStructureHashtable[(object) NumOfBriefcase] as BriefcaseExportStructure;
    if (!briefcaseExportStructure.ExportProperties.ServerPlacement)
    {
      Exception exception = (Exception) null;
      try
      {
        BriefcaseProcs.DeleteBriefcase(briefcaseExportStructure.ExportProperties.ServerFolder, true, out exception);
        Directory.Delete(briefcaseExportStructure.ExportProperties.ServerFolder);
      }
      catch (Exception ex)
      {
      }
    }
    ServerBriefcase._exportStructureHashtable.Remove((object) NumOfBriefcase);
    ServerBriefcase._exportProgressHashtable.Remove((object) NumOfBriefcase);
  }

  public IBrowseFolder GetFolderBrowser()
  {
    return (IBrowseFolder) ServerServices.GetService(typeof (IBrowseFolder));
  }

  public void RegisterCategoryExport(int category, ICategoryExport iCategoryExport)
  {
    if (iCategoryExport == null)
      return;
    ArrayList arrayList = this.categoryExport[category];
    if (arrayList.IndexOf((object) iCategoryExport) != -1)
      return;
    arrayList.Add((object) iCategoryExport);
  }

  public void UnregisterCategoryExport(int category, ICategoryExport iCategoryExport)
  {
    if (iCategoryExport == null)
      return;
    ArrayList arrayList = this.categoryExport[category];
    int index = arrayList.IndexOf((object) iCategoryExport);
    if (index == -1)
      return;
    arrayList.RemoveAt(index);
  }

  public ICategoryExport[] GetRegisteredCategoryExport(int category)
  {
    return (ICategoryExport[]) this.categoryExport[category].ToArray(typeof (ICategoryExport));
  }

  public long[] GetLinkedObjectVersions(Guid sessionGUID, int category, long[] ids)
  {
    ICategoryExport[] registeredCategoryExport = this.GetRegisteredCategoryExport(category);
    IUserSession sessionById = UserSession.GetSessionByID(sessionGUID);
    List<long> longList = new List<long>();
    for (int index1 = 0; index1 < registeredCategoryExport.Length; ++index1)
    {
      for (int index2 = 0; index2 < ids.Length; ++index2)
      {
        long[] linkedObjectVersions = registeredCategoryExport[index1].GetLinkedObjectVersions(sessionById, category, (object) ids[index2]);
        if (linkedObjectVersions != null)
        {
          for (int index3 = 0; index3 < linkedObjectVersions.Length; ++index3)
          {
            if (longList.IndexOf(linkedObjectVersions[index3]) < 0)
              longList.Add(linkedObjectVersions[index3]);
          }
        }
      }
    }
    return longList.ToArray();
  }

  public ExportAttribute[] GetLinkedDataByAttribute(
    Guid sessionGUID,
    int category,
    AttributableElements kind,
    long attributableID,
    int attributeId,
    object attrValueOriginal,
    ref object attrValueCurrent)
  {
    ICategoryExport[] registeredCategoryExport = this.GetRegisteredCategoryExport(category);
    IUserSession sessionById = UserSession.GetSessionByID(sessionGUID);
    bool flag = sessionById.GetAttributeType(attributeId).AttributeType == FieldTypes.ftShortBlob;
    IDBAttributable relation;
    if (kind != AttributableElements.Object)
    {
      if (kind != AttributableElements.Relation)
        throw new ArgumentOutOfRangeException(nameof (kind));
      relation = (IDBAttributable) sessionById.GetRelation(attributableID);
    }
    else
      relation = (IDBAttributable) sessionById.GetObject(attributableID);
    List<ExportAttribute> exportAttributeList = new List<ExportAttribute>();
    for (int index = 0; index < registeredCategoryExport.Length; ++index)
    {
      if (!flag || registeredCategoryExport[index].ProcessShortBlobs)
      {
        ExportAttribute[] linkedDataByAttribute = registeredCategoryExport[index].GetLinkedDataByAttribute(sessionById, kind, attributableID, relation, attributeId, attrValueOriginal, ref attrValueCurrent);
        if (linkedDataByAttribute != null && linkedDataByAttribute.Length != 0)
          exportAttributeList.AddRange((IEnumerable<ExportAttribute>) linkedDataByAttribute);
      }
    }
    return exportAttributeList.ToArray();
  }

  public bool ProcessShortBlobsFromCategoryExport(int category)
  {
    foreach (ICategoryExport categoryExport in this.GetRegisteredCategoryExport(category))
    {
      if (categoryExport.ProcessShortBlobs)
        return true;
    }
    return false;
  }

  private class CategoryIDStruct
  {
    public int CategoryType;
    public int CategoryID;

    public CategoryIDStruct(int aCategoryType, int aCategoryID)
    {
      this.CategoryType = aCategoryType;
      this.CategoryID = aCategoryID;
    }

    public override int GetHashCode() => this.CategoryType ^ this.CategoryID;

    public override bool Equals(object obj)
    {
      ServerBriefcase.CategoryIDStruct categoryIdStruct = (ServerBriefcase.CategoryIDStruct) obj;
      return categoryIdStruct.GetHashCode() == this.GetHashCode() && this.CategoryType == categoryIdStruct.CategoryType && this.CategoryID == categoryIdStruct.CategoryID;
    }
  }
}
