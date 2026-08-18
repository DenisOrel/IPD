// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.Helper
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.LifeCycles;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Briefcase;

public static class Helper
{
  private static Dictionary<int, int> _levels4Steps;
  private static Dictionary<Guid, long> _casheGUIDToIDs;
  private static List<int> _versionableObjectTypes;
  public static Guid attributeAreasGuid = new Guid("cad001af-306c-11d8-b4e9-00304f19f545");

  private static void ReadGuidDataDelegate(IDataReader reader, ExecuteReaderArgs args)
  {
    Helper._casheGUIDToIDs = args.InputParam as Dictionary<Guid, long>;
    while (reader.Read())
      Helper._casheGUIDToIDs.Add(new Guid(Convert.ToString(reader[0])), Convert.ToInt64(reader[1]));
  }

  private static Dictionary<Guid, long> GetCasheGUIDToIDs(UserSession session)
  {
    if (Helper._casheGUIDToIDs == null)
    {
      object obj = session.DataManager.ExecuteScalar("SELECT COUNT(1) FROM IMS_GUID_RESOLVE");
      Helper._casheGUIDToIDs = new Dictionary<Guid, long>(obj != DBNull.Value ? Convert.ToInt32(obj) : 100);
      session.DataManager.ExecuteReader("SELECT F_GUID, F_ID FROM IMS_GUID_RESOLVE", new ExecuteReaderDelegate(Helper.ReadGuidDataDelegate), new ExecuteReaderArgs((object) Helper._casheGUIDToIDs));
    }
    return Helper._casheGUIDToIDs;
  }

  public static void AddID(UserSession session, Guid guid, long id)
  {
    Helper.GetCasheGUIDToIDs(session).Add(guid, id);
  }

  public static long GetID(UserSession session, Guid guid)
  {
    long id = 0;
    Helper.GetCasheGUIDToIDs(session).TryGetValue(guid, out id);
    return id;
  }

  public static bool IsVersionabe(IUserSession session, int objTypeID)
  {
    if (Helper._versionableObjectTypes == null)
    {
      DataRow[] dataRowArray = (session as UserSession).DBCache.GetTable("IMS_OBJECT_TYPES").Select("F_VERSIONABLE = " + (object) 2);
      if (dataRowArray != null)
      {
        Helper._versionableObjectTypes = new List<int>(dataRowArray.Length);
        for (int index = 0; index < dataRowArray.Length; ++index)
          Helper._versionableObjectTypes.Add(Convert.ToInt32(dataRowArray[index]["F_OBJECT_TYPE"]));
      }
    }
    return Helper._versionableObjectTypes.Contains(objTypeID);
  }

  public static int GetLevelForStep(IUserSession session, int stepID)
  {
    if (Helper._levels4Steps == null)
    {
      DataRow[] dataRowArray = (session as UserSession).DBCache.GetTable("IMS_LC_STEPS").Select(string.Empty, "F_LC_STEP");
      if (dataRowArray != null)
      {
        Helper._levels4Steps = new Dictionary<int, int>(dataRowArray.Length);
        for (int index = 0; index < dataRowArray.Length; ++index)
          Helper._levels4Steps.Add(Convert.ToInt32(dataRowArray[index]["F_LC_STEP"]), Convert.ToInt32(dataRowArray[index]["F_LEVEL_ID"]));
      }
    }
    int levelForStep;
    Helper._levels4Steps.TryGetValue(stepID, out levelForStep);
    return levelForStep;
  }

  internal static CheckResult FindAttribute(
    UserSession userSession,
    out IDBAttributeType attrType,
    Guid guid,
    string alias,
    string name)
  {
    attrType = (IDBAttributeType) null;
    if (guid != Guid.Empty)
      attrType = userSession.GetAttributeType(guid, false);
    if (attrType != null)
      return CheckResult.FindByGuid;
    attrType = userSession.GetAttributeType(name, false);
    if (attrType != null)
      return CheckResult.FindByName;
    if (alias != string.Empty)
    {
      DataRow[] dataRowArray = userSession.DBCache.GetTable("IMS_ATTRIBUTES").Select("F_ALIAS=" + DataSetProcessor.QString(alias));
      if (dataRowArray.Length != 0)
        attrType = userSession.GetAttributeType(Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_ID"]), false);
      if (attrType != null)
        return CheckResult.FindByAlias;
    }
    return CheckResult.None;
  }

  public static int FindObjectType(UserSession userSession, DataSet MetaData, int BrefObjectID)
  {
    DataRow dataRow = MetaData.Tables["IMS_OBJECT_TYPES"].Rows.Find((object) BrefObjectID);
    if (dataRow != null)
    {
      int objectTypeId = MetaDataHelper.GetObjectTypeID(new Guid(dataRow["F_GUID"].ToString()));
      if (objectTypeId != -1)
        return objectTypeId;
      IDBObjectType objectType = userSession.GetObjectType(dataRow["F_OBJ_TYPE_NAME"].ToString(), false);
      if (objectType != null)
        return objectType.ObjectType;
    }
    return -1;
  }

  internal static CheckResult FindRelation(
    UserSession userSession,
    out IDBRelationType relType,
    Guid guid,
    string name)
  {
    relType = userSession.GetRelationType(guid, false);
    if (relType != null)
      return CheckResult.FindByGuid;
    relType = userSession.GetRelationType(name, false);
    return relType != null ? CheckResult.FindByName : CheckResult.NotFound;
  }

  public static string GetUniIdentifilerObjectType(IDBObjectType baseObjType, DataSet MetaData)
  {
    switch (Helper.GetBriefcaseObjectType(baseObjType, MetaData))
    {
      case CheckResult.NotFound:
        return string.Empty;
      case CheckResult.FindByGuid:
        return MetaData.Tables["IMS_OBJECT_TYPES"].Select($"{"F_OBJ_TYPE_NAME"} = {DataSetProcessor.QString(baseObjType.ObjectTypeName)}").Length != 0 ? string.Format(BriefcaseConsts.logFormatName, (object) baseObjType.ObjectTypeName) : string.Format(BriefcaseConsts.logFormatGUID, (object) (baseObjType as IDBGuid).GUID.ToString());
      default:
        return string.Format(BriefcaseConsts.logFormatName, (object) baseObjType.ObjectTypeName);
    }
  }

  internal static CheckResult GetBriefcaseObjectType(IDBObjectType baseObjType, DataSet MetaData)
  {
    if (MetaData.Tables["IMS_OBJECT_TYPES"].Select($"{"F_GUID"} = {DataSetProcessor.QString((baseObjType as IDBGuid).GUID.ToString())}").Length != 0)
      return CheckResult.FindByGuid;
    return MetaData.Tables["IMS_OBJECT_TYPES"].Select($"{"F_OBJ_TYPE_NAME"} = {DataSetProcessor.QString(baseObjType.ObjectTypeName)}").Length != 0 ? CheckResult.FindByName : CheckResult.NotFound;
  }

  public static DataRow GetSourceAttributeRow(DataRow BriefRow, DataSet MetaData)
  {
    return BriefRow["F_SOURCE_ID"] != null ? MetaData.Tables["IMS_ATTRIBUTES"].Rows.Find(BriefRow["F_SOURCE_ID"]) : (DataRow) null;
  }

  public static DataRow GetMasterAttributeRow(DataRow BriefRow, DataSet MetaData)
  {
    return BriefRow["F_MASTER_ID"] != null && BriefRow["F_MASTER_ID"] != DBNull.Value ? MetaData.Tables["IMS_ATTRIBUTES"].Rows.Find(BriefRow["F_MASTER_ID"]) : (DataRow) null;
  }

  public static bool CheckSize(
    FieldTypes brefType,
    long brefTypeSize,
    FieldTypes dbType,
    long dbTypeSize)
  {
    return brefType == FieldTypes.ftString && dbType == FieldTypes.ftString ? brefTypeSize >= dbTypeSize : brefType == dbType;
  }

  public static string GetConformitySubjectAreas(
    IUserSession session,
    DataSet MetaData,
    string briefSubjAreas,
    bool throwException = false)
  {
    string empty = string.Empty;
    briefSubjAreas = briefSubjAreas.Trim();
    if (briefSubjAreas.Length > 0)
    {
      foreach (char key in briefSubjAreas.ToCharArray())
      {
        DataRow dataRow = MetaData.Tables["IMS_SUBJECT_AREAS"].Rows.Find((object) key);
        if (dataRow != null)
        {
          DataRow[] dataRowArray = (session as UserSession).DBCache.GetTable("IMS_SUBJECT_AREAS").Select($"F_GUID={SqlHelper.QString(Convert.ToString(dataRow["F_GUID"]))}");
          if (dataRowArray.Length == 0)
          {
            if (throwException)
              throw new Exception($"Предметная область с глобальным идентификатором {dataRow["F_GUID"]} не найдена в базе назначения");
          }
          else
            empty += Convert.ToString(dataRowArray[0]["F_AREA_ID"]);
        }
      }
    }
    return empty;
  }

  private static int GetLCSchemaID(UserSession session, string guid)
  {
    IDBLCSchema lcSchema = session.GetLCSchema(new Guid(guid), false);
    return lcSchema == null ? -1 : lcSchema.SchemaID;
  }

  public static int GetConformityLCSchemes(
    UserSession session,
    DataSet MetaData,
    int briefLCScheme)
  {
    int conformityLcSchemes = -1;
    if (briefLCScheme >= 0)
    {
      DataRow dataRow = MetaData.Tables["IMS_LC_SCHEMAS"].Rows.Find((object) briefLCScheme);
      if (dataRow != null)
        conformityLcSchemes = Helper.GetLCSchemaID(session, Convert.ToString(dataRow["F_GUID"]));
    }
    if (conformityLcSchemes == -1)
      conformityLcSchemes = Helper.GetLCSchemaID(session, "cad00801-306c-11d8-b4e9-00304f19f545");
    return conformityLcSchemes;
  }

  public static string GetConformityLanguage(
    UserSession session,
    DataSet MetaData,
    string briefLanguage,
    bool throwException = false)
  {
    string empty = string.Empty;
    briefLanguage = briefLanguage.Trim();
    if (briefLanguage.Length > 0)
    {
      foreach (char key in briefLanguage.ToCharArray())
      {
        DataRow dataRow = MetaData.Tables["IMS_LANGUAGES"].Rows.Find((object) key);
        if (dataRow != null)
        {
          DataRow[] dataRowArray = session.DBCache.GetTable("IMS_LANGUAGES").Select($"F_GUID={SqlHelper.QString(Convert.ToString(dataRow["F_GUID"]))}");
          if (dataRowArray.Length == 0)
          {
            if (throwException)
              throw new Exception($"Язык с глобальным идентификатором {dataRow["F_GUID"]} не найден в базе назначения");
          }
          else
            empty += Convert.ToString(dataRowArray[0]["F_LANGUAGE_ID"]);
        }
      }
    }
    return empty;
  }

  public static int GetObjectTypeLevel(DataTable table, int StartLevel, int ObjectType)
  {
    DataRow[] dataRowArray = table.Select("F_OBJECT_TYPE=" + ObjectType.ToString());
    return dataRowArray.Length != 0 ? Helper.GetObjectTypeLevel(table, StartLevel + 1, Convert.ToInt32(dataRowArray[0]["F_PARENT_ID"])) : StartLevel;
  }

  public static int GetConformityLCLevel(
    UserSession session,
    DataTable lcTable,
    int oldlevelID,
    bool throwException = false)
  {
    if (oldlevelID <= 0)
      return 0;
    DataRow dataRow = lcTable.Rows.Find((object) oldlevelID);
    IDBLifecycleLevelType lifecycleLevel = session.GetLifecycleLevel(new Guid(Convert.ToString(dataRow["F_GUID"])), false);
    if (lifecycleLevel != null)
      return lifecycleLevel.LevelID;
    if (throwException)
      throw new Exception($"Уровень продвижения с глобальным идентификатором {dataRow["F_GUID"]} не найден в базе назначения");
    return 0;
  }

  public static int GetConformityLCStep(
    UserSession session,
    DataTable lcTable,
    int oldLCStepID,
    bool throwException = false)
  {
    if (oldLCStepID <= 0)
      return -1;
    DataRow dataRow = lcTable.Rows.Find((object) oldLCStepID);
    IDBLifecycleStep lifecycleStep = session.GetLifecycleStep(new Guid(Convert.ToString(dataRow["F_GUID"])), false);
    if (lifecycleStep != null)
      return lifecycleStep.LCStep;
    if (throwException)
      throw new Exception($"Шаг ЖЦ с глобальным идентификатором {dataRow["F_GUID"]} не найден в базе назначения");
    return -1;
  }

  public static int GetConformityAttribureType(
    UserSession session,
    DataTable table,
    int oldAttribyteType,
    bool throwException = false)
  {
    DataRow dataRow = table.Rows.Find((object) oldAttribyteType);
    if (dataRow == null)
    {
      if (throwException)
        throw new Exception(LocalizationHolder.rm.GetString("Kernel_984") + oldAttribyteType.ToString());
      return 0;
    }
    IDBAttributeType attributeType = session.GetAttributeType(new Guid(Convert.ToString(dataRow["F_GUID"])), false);
    if (attributeType != null)
      return attributeType.AttributeID;
    if (throwException)
      throw new Exception(string.Format(BriefcaseConsts.ImportAttributeTypeNotFound, dataRow["F_GUID"]));
    return 0;
  }

  public static int GetConformityAttribureGroup(
    UserSession session,
    DataTable Table,
    int oldAttribyteGroup,
    bool throwException = false)
  {
    DataRow dataRow = Table.Rows.Find((object) oldAttribyteGroup);
    IDBAttributesGroup dbAttributesGroup = dataRow != null ? session.GetAttributesGroup(new Guid(Convert.ToString(dataRow["F_GUID"]))) : throw new Exception(LocalizationHolder.rm.GetString("Kernel_1162") + oldAttribyteGroup.ToString());
    if (dbAttributesGroup != null)
      return dbAttributesGroup.GroupID;
    if (throwException)
      throw new Exception($"Группа атрибутов с глобальным идентификатором {dataRow["F_GUID"]} не найдена в базе назначения");
    return -1;
  }

  public static int GetConformityRelationType(
    IUserSession session,
    DataTable Table,
    int oldRelationType,
    bool throwException = false)
  {
    DataRow dataRow = Table.Rows.Find((object) oldRelationType);
    IDBRelationType relationType = session.GetRelationType(new Guid(Convert.ToString(dataRow["F_GUID"])), false);
    if (relationType != null)
      return relationType.RelationType;
    if (throwException)
      throw new Exception($"Тип связей с глобальным идентификатором {dataRow["F_GUID"]} не найден в базе назначения");
    return -1;
  }

  public static int GetConformityObjectType(
    IUserSession session,
    DataTable Table,
    int oldObjectType,
    bool throwException = false)
  {
    DataRow dataRow = Table.Rows.Find((object) oldObjectType);
    if (dataRow == null)
    {
      if (throwException)
        throw new Exception($"Тип объектов с идентификатором {oldObjectType} не найден в базе назначения");
      return -1;
    }
    IDBObjectType objectType = session.GetObjectType(new Guid(Convert.ToString(dataRow["F_GUID"])), false);
    if (objectType != null)
      return objectType.ObjectType;
    if (throwException)
      throw new Exception($"Тип объектов с глобальным идентификатором {dataRow["F_GUID"]} не найден в базе назначения");
    return -1;
  }

  public static object GetValueFromField(string ValueFieldName, AttributeRecord AttrRec)
  {
    return Helper.GetValueFromField(ValueFieldName, AttrRec.IntegerValue, AttrRec.StringValue, AttrRec.DoubleValue, AttrRec.DateValue);
  }

  private static object GetValueFromField(
    string valueFieldName,
    object integerValue,
    object stringValue,
    object doubleValue,
    object dateValue)
  {
    switch (valueFieldName)
    {
      case "F_INTEGER_VALUE":
        return integerValue;
      case "F_STRING_VALUE":
        return stringValue;
      case "F_DOUBLE_VALUE":
        return doubleValue;
      case "F_DATE_VALUE":
        return dateValue;
      default:
        return (object) null;
    }
  }

  public static string ValueToLog(object value, object guid, bool Quote) => $"{value} {{{guid}}}";

  public static DataRow[] GetAttributesForObjectType(IUserSession session, int objectTypeID)
  {
    return Helper.GetAttributesForType(session, "IMS_ATTR4OBJ_TYPES", objectTypeID);
  }

  public static DataRow[] GetAttributesForRelationType(IUserSession session, int relationTypeID)
  {
    return Helper.GetAttributesForType(session, "IMS_ATTR4RELATION_TYPES", relationTypeID);
  }

  private static DataRow[] GetAttributesForType(IUserSession session, string tableName, int typeID)
  {
    DataTable table = (session as UserSession).DBCache.GetTable(tableName);
    string str = (string) null;
    switch (tableName)
    {
      case "IMS_ATTR4OBJ_TYPES":
        str = "F_OBJECT_TYPE";
        break;
      case "IMS_ATTR4RELATION_TYPES":
        str = "F_RELATION_TYPE";
        break;
    }
    return table.Select($"{str}={typeID}");
  }

  public static DataRow GetAttributeTypeRow(IUserSession session, int attributeID)
  {
    return (session as UserSession).DBCache.GetTable("IMS_ATTRIBUTES").Rows.Find((object) attributeID);
  }
}
