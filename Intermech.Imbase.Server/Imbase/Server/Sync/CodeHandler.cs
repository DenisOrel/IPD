// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.CodeHandler
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Imbase.Server.Sync.DataBase;
using Intermech.Imbase.Server.Sync.Helper;
using Intermech.Imbase.Server.Sync.Records;
using Intermech.Imbase.Server.Sync.Services;
using Intermech.ImpExp.Interface;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Params;
using Intermech.Interfaces.Pdm;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Imbase.Server.Sync;

internal class CodeHandler
{
  private Dictionary<int, List<Guid>> _dictionarySp;
  protected internal Guid TaskGuid;

  public CodeHandler(Guid taskGuid)
  {
    this.TaskGuid = taskGuid;
    this.DefaultMeasureId = ApplicationServices.Container.GetService<IImbaseParamsService>().CommonParams.ImbaseSyncParams.DefaultMeasureId;
  }

  internal long DefaultMeasureId { get; }

  public virtual void Handle(EventRecord record, IDataBase sourceDB, IUserSession session)
  {
  }

  internal void AddEventInfo(EventType type, string eventText)
  {
    ServiceUtils.GetService<IEventLoggerService>((object) ApplicationServices.Container, true).AddMessage(this.TaskGuid, type, eventText);
  }

  private void InitSpSectionCreatedObjTypes(IUserSession session)
  {
    this._dictionarySp = session.GetObjectCollection(MetaDataHelper.GetObjectTypeID("cad00254-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
    {
      (object) MetaDataHelper.GetAttributeTypeID("cad00279-306c-11d8-b4e9-00304f19f545"),
      (object) MetaDataHelper.GetAttributeTypeID(new Guid("cad0027d-306c-11d8-b4e9-00304f19f545"))
    })).AsEnumerable().GroupBy<DataRow, int, Guid>((System.Func<DataRow, int>) (x => Convert.ToInt32(x[0])), (System.Func<DataRow, Guid>) (y => Guid.Parse(Convert.ToString(y[1])))).ToDictionary<IGrouping<int, Guid>, int, List<Guid>>((System.Func<IGrouping<int, Guid>, int>) (x => x.Key), (System.Func<IGrouping<int, Guid>, List<Guid>>) (y => y.ToList<Guid>()));
  }

  private bool TryGetObjectTypeIdBySpSection(
    IUserSession session,
    int spSection,
    out Guid objTypeGuid)
  {
    objTypeGuid = Guid.Empty;
    if (this._dictionarySp == null)
      this.InitSpSectionCreatedObjTypes(session);
    List<Guid> source;
    if (!this._dictionarySp.TryGetValue(spSection, out source))
      return false;
    objTypeGuid = source.FirstOrDefault<Guid>();
    return true;
  }

  protected static void CreateNewClassifCode(
    IUserSession session,
    IDBObject thisObject,
    IDBObject parentObject)
  {
    IDBAttribute byId = parentObject.Attributes.FindByID(MetaDataHelper.GetAttributeTypeID("cad0014d-306c-11d8-b4e9-00304f19f545"));
    if (byId == null)
      return;
    string asString = byId.AsString;
    string nextClassifierKey = ServiceUtils.GetService<ISelectionsService>((object) session, true).GenerateNextClassifierKey((object) session, parentObject.ObjectType, asString, thisObject.ObjectType);
    thisObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad0014d-306c-11d8-b4e9-00304f19f545"), false, new object[1]
    {
      (object) nextClassifierKey
    });
  }

  protected void AddDelayedEvent(EventRecord eventRecord)
  {
    ServiceUtils.GetService<IDelayedEvents>((object) ApplicationServices.Container, true).AddDelayedEvent(eventRecord);
  }

  protected static long GetObjectByImbaseCode(
    IUserSession session,
    int objType,
    int key,
    int catalogId,
    out string msgInfo)
  {
    StringBuilder stringBuilder = new StringBuilder();
    string catalogClassifCode = string.Empty;
    long num = 0;
    if (catalogId != 0)
    {
      DataTable source = session.GetObjectCollection(MetaDataHelper.GetObjectTypeID("cad00221-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttGUID), RelationalOperators.Equal, (object) catalogId, LogicalOperators.AND, 0, false)
      }, new object[2]
      {
        (object) -2,
        (object) MetaDataHelper.GetAttributeTypeID("cad0014d-306c-11d8-b4e9-00304f19f545")
      }));
      if (source != null && source.Rows.Count > 0)
      {
        num = Convert.ToInt64(source.Rows[0][0]);
        string str1 = Convert.ToString(source.Rows[0][1]);
        if (!string.IsNullOrEmpty(str1) && str1.Length == 2)
          catalogClassifCode = str1;
        if (source.Rows.Count > 1)
        {
          string str2 = string.Join<long>(", ", (IEnumerable<long>) source.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[0]))).ToArray<long>());
          string str3 = $"При поиске каталога по коду Imbase = '{catalogId}' в базе-приемнике найдено {source.Rows.Count} записей: id = '{str2}'";
          stringBuilder.AppendLine(str3);
        }
      }
    }
    IDBObjectCollection objectCollection = session.GetObjectCollection(objType);
    string objectInstanceName = session.GetObjectType(objType).ObjectInstanceName;
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttGUID), RelationalOperators.Equal, (object) key, LogicalOperators.AND, 0, false)
    }, new object[2]
    {
      (object) -2,
      (object) MetaDataHelper.GetAttributeTypeID("cad0014d-306c-11d8-b4e9-00304f19f545")
    });
    DataTable source1 = objectCollection.Select(paramSet);
    long objectByImbaseCode;
    if (source1 == null || source1.Rows.Count == 0)
      objectByImbaseCode = 0L;
    else if (source1.Rows.Count == 1)
    {
      objectByImbaseCode = Convert.ToInt64(source1.Rows[0][0]);
    }
    else
    {
      DataRow[] array1 = source1.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToString(x[1]).StartsWith(catalogClassifCode))).ToArray<DataRow>();
      if (!string.IsNullOrEmpty(catalogClassifCode) && array1.Length != 0)
      {
        if (array1.Length == 1)
        {
          objectByImbaseCode = Convert.ToInt64(array1[0][0]);
        }
        else
        {
          long[] array2 = ((IEnumerable<DataRow>) array1).AsEnumerable<DataRow>().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[0]))).ToArray<long>();
          string str4 = string.Join<long>(", ", (IEnumerable<long>) array2);
          string str5 = $"При поиске объекта {objectInstanceName} по коду Imbase = '{key}' в базе-приемнике в каталоге id = '{num}' найдено {array2.Length} объектов: ids = {str4}.";
          stringBuilder.AppendLine(str5);
          objectByImbaseCode = array2[0];
        }
      }
      else
      {
        long[] array3 = source1.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[0]))).ToArray<long>();
        string str6 = string.Join<long>(", ", (IEnumerable<long>) array3);
        string str7 = $"При поиске {objectInstanceName} по коду Imbase = '{key}' в базе-приемнике найдено {array3.Length} объектов: ids = {str6}.";
        stringBuilder.AppendLine(str7);
        objectByImbaseCode = array3[0];
      }
    }
    msgInfo = stringBuilder.ToString();
    return objectByImbaseCode;
  }

  protected static long[] GetAllTableLinks(IUserSession session, long tableId)
  {
    return session.GetObjectCollection(MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseTableRefTypeGUID)).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseTableRefAttGUID), RelationalOperators.Equal, (object) tableId, LogicalOperators.AND, 0, false)
    }, new object[1]{ (object) -2 })).AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[0]))).ToArray<long>();
  }

  protected static long GetUserID(IUserSession session, string userName)
  {
    DataTable dataTable = session.GetObjectCollection(new Guid("cad00002-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(session.IdentHelper.NameID, RelationalOperators.Equal, (object) userName, LogicalOperators.AND, 0, false)
    }, new object[1]{ (object) -2 }));
    return dataTable.Rows.Count <= 0 ? session.UserID : Convert.ToInt64(dataTable.Rows[0][0]);
  }

  protected static bool UpdateVisibleObjectState(
    IDBObject dbObject,
    ImFileAtt state,
    bool allowDelete)
  {
    if (dbObject == null)
      return false;
    bool flag = false;
    IDBAttribute dbAttribute = dbObject.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad0062f-306c-11d8-b4e9-00304f19f545"));
    if ((state & ImFileAtt.ITF_HIDDEN) == ImFileAtt.ITF_HIDDEN)
    {
      if (dbAttribute == null)
      {
        try
        {
          dbAttribute = dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad0062f-306c-11d8-b4e9-00304f19f545"), false);
        }
        catch (Exception ex)
        {
        }
      }
      if (dbAttribute != null && dbAttribute.AsString != VisibleAttHelper.AllUsersHidden)
      {
        dbAttribute.Value = (object) VisibleAttHelper.AllUsersHidden;
        flag = true;
      }
    }
    else if (allowDelete)
    {
      if (dbAttribute != null)
      {
        try
        {
          dbAttribute.Delete(0L);
          flag = true;
        }
        catch (Exception ex)
        {
        }
      }
    }
    return flag;
  }

  internal static TableRecord GetTableRecord(IDataBase db, int key)
  {
    DataTable dataTable = db.ExecuteDataTable($"SELECT * FROM {"IM_TABLES"} A WHERE {"F_KEY"} = :tableID", db.CreateParameter("tableID", (object) key));
    return dataTable.Rows.Count <= 0 ? (TableRecord) null : new TableRecord(dataTable.Rows[0]);
  }

  protected static object GetTableLinkValue(IUserSession session, object sourceValue)
  {
    if (!(sourceValue is string str))
      return sourceValue;
    string[] strArray = str.Substring(0, str.IndexOf("|")).Split('.');
    int result1;
    int result2;
    if (strArray.Length != 3 || !int.TryParse(strArray[0], out result1) || !int.TryParse(strArray[2], out result2))
      return sourceValue;
    long objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, Intermech.Imbase.Consts.ImbaseTableRefTypeID, result2, result1, out string _);
    if (objectByImbaseCode != 0L)
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(objectByImbaseCode);
      if (!objectInfo.Empty)
        return (object) Convert.ToString((object) objectInfo.VersionGuid);
    }
    return sourceValue;
  }

  protected static object GetImbaseRecordLinkValue(IUserSession session, object sourceValue)
  {
    return !(sourceValue is string oldKey) ? sourceValue : (object) ServiceUtils.GetService<IKeyConverter>((object) session, true).ConvertOldKey(session, oldKey);
  }

  protected static object GetFolderLinkValue(IUserSession session, object sourceValue)
  {
    if (!(sourceValue is string str))
      return sourceValue;
    string[] strArray = str.Substring(0, str.IndexOf("|")).Split('.');
    int result1;
    int result2;
    if (strArray.Length != 2 || !int.TryParse(strArray[0], out result1) || !int.TryParse(strArray[1], out result2))
      return sourceValue;
    long objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, Intermech.Imbase.Consts.ImbaseFolderTypeID, result2, result1, out string _);
    if (objectByImbaseCode != 0L)
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(objectByImbaseCode);
      if (!objectInfo.Empty)
        return (object) Convert.ToString((object) objectInfo.VersionGuid);
    }
    return sourceValue;
  }

  public string GetFullPath(IDataBase db, EventRecord record)
  {
    try
    {
      int catalog = record.Catalog;
      if (catalog == 0)
        return string.Empty;
      DataTable dataTable1 = db.ExecuteDataTable($"SELECT * FROM IM_TABLES WHERE F_KEY={catalog}");
      if (dataTable1.Rows.Count == 0)
        return string.Empty;
      string str1 = Convert.ToString(dataTable1.Rows[0]["F_DESCR"]);
      string str2 = Convert.ToString(dataTable1.Rows[0]["F_TABLE"]);
      string str3 = string.Empty;
      int num = record.Folder;
      while (num > 0)
      {
        DataTable dataTable2 = db.ExecuteDataTable($"SELECT * FROM {str2} WHERE F_LEVEL={num}");
        if (dataTable2.Rows.Count != 0)
        {
          num = Convert.ToInt32(dataTable2.Rows[0]["F_OWNER"]);
          str3 = $"{Convert.ToString(dataTable2.Rows[0]["F_NAME"])}\\{str3}";
        }
        else
          break;
      }
      return !string.IsNullOrEmpty(str3) ? $"{str1}\\{str3}" : str1;
    }
    catch (Exception ex)
    {
      return string.Empty;
    }
  }

  protected long AddNewPicture(
    IUserSession session,
    IDataBase db,
    int graphID,
    long oldPictureObjectId)
  {
    long num = 0;
    BlobRecord blobRecord = CodeHandler.CreateBlobRecord(db, graphID);
    if (blobRecord == null)
      return num;
    IDBObject destObj = (IDBObject) null;
    bool flag = false;
    if (oldPictureObjectId != 0L)
      destObj = session.GetObject(oldPictureObjectId, false);
    if (destObj == null)
    {
      long objectByImbaseCode = CodeHandler.GetObjectByImbaseCode(session, MetaDataHelper.GetObjectTypeID("cad00140-306c-11d8-b4e9-00304f19f545"), graphID, 0, out string _);
      if (objectByImbaseCode == 0L)
      {
        destObj = session.GetObjectCollection(MetaDataHelper.GetObjectTypeID("cad00140-306c-11d8-b4e9-00304f19f545")).Create();
        flag = true;
      }
      else
        destObj = session.GetObject(objectByImbaseCode);
    }
    destObj.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), false, new object[1]
    {
      (object) blobRecord.Source
    });
    destObj.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttGUID), false, new object[1]
    {
      (object) graphID
    });
    this.WriteImage(destObj, blobRecord);
    if (flag)
      destObj.CommitCreation(true);
    return destObj.ObjectID;
  }

  protected static BlobRecord CreateBlobRecord(IDataBase db, int blobId)
  {
    DataTable dataTable = db.ExecuteDataTable($"SELECT * FROM {"IM_BLOBS"} A WHERE {"F_KEY"} = {blobId}");
    return dataTable.Rows.Count != 0 ? new BlobRecord(dataTable.Rows[0]) : (BlobRecord) null;
  }

  protected FolderRecord GetFolderRecord(IDataBase db, string tableName, int level)
  {
    DataTable dataTable = db.ExecuteDataTable($"SELECT * FROM {tableName} A WHERE {"F_LEVEL"} = {level}");
    return dataTable.Rows.Count <= 0 ? (FolderRecord) null : new FolderRecord(dataTable.Rows[0]);
  }

  protected FieldRecord[] GetFields(IDataBase db, int tableID)
  {
    return db.ExecuteDataTable($"SELECT * FROM {"IM_FIELDS"} A WHERE {"F_TABLE_ID"} = {tableID}").AsEnumerable().Select<DataRow, FieldRecord>((System.Func<DataRow, FieldRecord>) (x => new FieldRecord(x))).ToArray<FieldRecord>();
  }

  protected void AddAttributesToRecord(
    IUserSession session,
    IDataBase sourceDB,
    string recTableName,
    int recKey,
    FieldRecord[] fields,
    IDBObject newObject,
    IArticleService artSrv,
    bool onlyDataAttributes)
  {
    DataTable dataTable = sourceDB.ExecuteDataTable($"SELECT * FROM {recTableName} WHERE {"F_KEY"}={recKey}");
    List<string> list = dataTable.Columns.Cast<DataColumn>().Select<DataColumn, string>((System.Func<DataColumn, string>) (x => x.ColumnName)).ToList<string>();
    if (!onlyDataAttributes)
    {
      string empty = string.Empty;
      if (fields.Length != 0)
      {
        FieldRecord fieldRecord = (FieldRecord) null;
        foreach (FieldRecord field in fields)
        {
          if (list.Contains(field.Field))
          {
            if (fieldRecord == null)
              fieldRecord = field;
            else if (fieldRecord.Sort > field.Sort)
              fieldRecord = field;
          }
        }
        if (fieldRecord != null && dataTable.Rows.Count > 0)
          empty = Convert.ToString(dataTable.Rows[0][fieldRecord.Field]);
      }
      IDBAttribute dbAttribute1 = newObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), false);
      if (dbAttribute1 != null)
        dbAttribute1.AsString = empty;
      IDBAttribute dbAttribute2 = newObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttGUID), false);
      if (dbAttribute2 != null)
        dbAttribute2.AsInteger = (long) recKey;
    }
    if (dataTable.Rows.Count <= 0)
      return;
    Dictionary<Guid, string> formules = new Dictionary<Guid, string>(dataTable.Rows.Count);
    List<Guid> guidList = new List<Guid>(dataTable.Rows.Count);
    foreach (DataColumn column in (InternalDataCollectionBase) dataTable.Columns)
    {
      if (!column.ColumnName.Equals("F_KEY") && !column.ColumnName.Equals("F_LEVEL"))
      {
        object obj = dataTable.Rows[0][column.ColumnName];
        for (int index = 0; index < fields.Length; ++index)
        {
          if (fields[index].Field.Equals(column.ColumnName))
          {
            IDBAttributeType attributeType = session.GetAttributeType(fields[index].GUID);
            IDBAttribute dbAttribute = newObject.Attributes.AddAttribute(attributeType.AttributeID, false);
            if (dbAttribute != null && !dbAttribute.ReadOnly && attributeType.Computed == ComputeValueModes.NotComputableValue)
            {
              switch (attributeType.AttributeType)
              {
                case FieldTypes.ftObjectLink:
                  if (obj != null && !DBNull.Value.Equals(obj))
                  {
                    string sourceValue = Convert.ToString(obj);
                    switch (fields[index].EnterMode)
                    {
                      case ImEnterMode.IEM_FOLDER:
                        obj = CodeHandler.GetFolderLinkValue(session, (object) sourceValue);
                        break;
                      case ImEnterMode.IEM_TABLE:
                        obj = CodeHandler.GetTableLinkValue(session, (object) sourceValue);
                        break;
                      case ImEnterMode.IEM_SEARCH_DOCUMENT:
                        obj = SearchLinksHelper.GetSearchDocumentLinkValue(session, (object) sourceValue);
                        break;
                      case ImEnterMode.IEM_SEARCH_OBJECT:
                        obj = SearchLinksHelper.GetSearchObjectLinkValue(session, (object) sourceValue);
                        break;
                      default:
                        this.AddEventInfo(EventType.Warning, $"Недопустимый тип поля IMBASE [{fields[index].EnterMode}] для ссылочного атрибута '{fields[index].LongName}'. Запись каталога id = {recKey}");
                        obj = (object) null;
                        break;
                    }
                    Guid result;
                    if (obj != null && Guid.TryParse(obj.ToString(), out result))
                    {
                      QuickObjectInfo objectInfo = session.GetObjectInfo(result);
                      if (!objectInfo.Empty)
                      {
                        dbAttribute.Value = (object) objectInfo.ObjectID;
                        break;
                      }
                    }
                    this.AddEventInfo(EventType.Warning, string.Format("Ссылка на объект по данным '{0}' не найдена. Ccылочный атрибут '{1}'. Запись каталога id = {1}", (object) sourceValue, (object) fields[index].LongName, (object) recKey));
                    obj = (object) null;
                    break;
                  }
                  break;
                case FieldTypes.ftBoolean:
                  string str = Convert.ToString(obj);
                  bool result1;
                  if (!bool.TryParse(str, out result1) && (str.Contains("+") || str.Contains("1") || str.Contains("T")))
                    result1 = true;
                  dbAttribute.Value = (object) result1;
                  break;
                case FieldTypes.ftMeasured:
                  if (!string.IsNullOrEmpty(Convert.ToString(obj)))
                  {
                    long measureID = 0;
                    if (!string.IsNullOrEmpty(fields[index].Units))
                    {
                      string newShortMeasureName;
                      if (PumpSettings.TryFoundMeasure(fields[index].Units, out newShortMeasureName))
                        fields[index].Units = newShortMeasureName;
                      MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(fields[index].Units);
                      if (!descriptor.Empty)
                        measureID = descriptor.MeasureID;
                    }
                    if (measureID == 0L)
                    {
                      MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(this.DefaultMeasureId);
                      if (!descriptor.Empty)
                        measureID = descriptor.MeasureID;
                    }
                    if (measureID == 0L)
                    {
                      this.AddEventInfo(EventType.Warning, $"Не удалось найти единицу измерения для поля = '{fields[index].LongName}'. Запись каталога id = {recKey}");
                      break;
                    }
                    MeasuredValue measuredValue = new MeasuredValue(Convert.ToDouble(obj), measureID);
                    try
                    {
                      dbAttribute.Value = (object) measuredValue;
                      break;
                    }
                    catch (Exception ex)
                    {
                      this.AddEventInfo(EventType.Warning, $"Для объекта {newObject.NameInMessages} [{newObject.ObjectID}] не удалось записать значение '{measuredValue}' в атрибут '{dbAttribute.Name}'. Ошибка: {ex.Message}");
                      break;
                    }
                  }
                  else
                  {
                    dbAttribute.Value = (object) null;
                    break;
                  }
                default:
                  string formula = Convert.ToString(obj);
                  if (attributeType.AttributeType == FieldTypes.ftString && formula.Contains("{F") && formula.Contains("}"))
                  {
                    formules.Add(fields[index].GUID, this.ParseFormula(fields, formula));
                    guidList.Add(fields[index].GUID);
                  }
                  if (fields[index].EnterMode.HasFlag((Enum) ImEnterMode.IEM_RECORD) && obj is string oldKey)
                    obj = (object) ServiceUtils.GetService<IKeyConverter>((object) session, true).ConvertOldKey(session, oldKey);
                  dbAttribute.Value = obj;
                  break;
              }
            }
            int result2;
            if (attributeType.AttributeID.Equals(MetaDataHelper.GetAttributeTypeID("cad00210-306c-11d8-b4e9-00304f19f545")) && CompareValuesHelper.NormalizedValue(obj) != null && int.TryParse(Convert.ToString(obj), out result2))
            {
              Guid objTypeGuid;
              if (this.TryGetObjectTypeIdBySpSection(session, result2, out objTypeGuid))
                newObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.CreatedObjectAttGUID), false).Value = (object) objTypeGuid.ToString();
              else
                this.AddEventInfo(EventType.Warning, $"Для объекта '{newObject.NameInMessages}' (id = {newObject.ObjectID}) пришло некорректное значение атрибута 'Раздел СП' = {result2}.В базе назначения отсутствует раздел спецификации с таким номером раздела. Источник: код = {recKey}");
            }
          }
        }
      }
    }
    foreach (Guid guid in guidList)
    {
      string formula;
      if (formules.TryGetValue(guid, out formula))
        this.CalculateFormula(newObject, formules, guid, formula);
    }
  }

  private string ParseFormula(FieldRecord[] fields, string formula)
  {
    MatchCollection matchCollection = new Regex("\\{F\\d+\\}").Matches(formula);
    for (int i = 0; i < matchCollection.Count; ++i)
    {
      string str = matchCollection[i].Value.Trim('{', '}');
      for (int index = 0; index < fields.Length; ++index)
      {
        if (fields[index].Field == str)
        {
          formula = formula.Replace(matchCollection[i].Value, $"{{{fields[index].GUID}}}");
          break;
        }
      }
    }
    return formula;
  }

  private void CalculateFormula(
    IDBObject obj,
    Dictionary<Guid, string> formules,
    Guid attributeGuid,
    string formula)
  {
    MatchCollection matchCollection = new Regex("\\{\\w{8}\\-\\w{4}\\-\\w{4}\\-\\w{4}\\-\\w{12}\\}").Matches(formula);
    if (matchCollection.Count == 0)
      return;
    for (int i = 0; i < matchCollection.Count; ++i)
    {
      Guid guid = new Guid(matchCollection[i].Value.Trim('{', '}'));
      if (formules.ContainsKey(guid))
        this.CalculateFormula(obj, formules, guid, formules[guid]);
      formula = formula.Replace(matchCollection[i].Value, obj.GetAttributeByGuid(guid).AsString);
    }
    obj.GetAttributeByGuid(attributeGuid).Value = (object) formula;
    formules.Remove(attributeGuid);
  }

  private void WriteImage(IDBObject destObj, BlobRecord blobRec)
  {
    if (blobRec == null || !blobRec.IsPicture || blobRec.Length <= 0 || !(destObj.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.LibraryImageAttGUID), false) is IBlobWriter blobWriter))
      return;
    using (MemoryStream inStream = new MemoryStream(blobRec.Blob))
    {
      using (MemoryStream outStream = new MemoryStream())
      {
        long length = inStream.Length;
        ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).PackStream((Stream) outStream, (Stream) inStream, 9);
        BlobInformation blobInfo = new BlobInformation(length, outStream.Length, DateTime.Now, string.Empty, ArcMethods.ZLibPacked, blobRec.Source);
        blobWriter.OpenBlob(blobInfo, false);
        outStream.Position = 0L;
        blobWriter.WriteDataBlock(outStream.ToArray());
      }
    }
  }

  protected void LinkAttributes(
    IUserSession session,
    FieldRecord[] fields,
    IDataBase sourceDB,
    EventRecord eventRecord,
    string tableName,
    string recTableName,
    int objectKey)
  {
    Dictionary<Guid, string> formules = new Dictionary<Guid, string>(0);
    this.LinkAttributes(session, fields, sourceDB, eventRecord, tableName, recTableName, objectKey, out formules);
  }

  protected void LinkAttributes(
    IUserSession session,
    FieldRecord[] fields,
    IDataBase sourceDB,
    EventRecord eventRecord,
    string tableName,
    string recTableName,
    int objectKey,
    out Dictionary<Guid, string> formules)
  {
    IDBAttributeTypeCollection attributeTypeCollection = session.GetAttributeTypeCollection(-1);
    DataTable schemaTable = sourceDB.GetSchemaTable(recTableName);
    formules = new Dictionary<Guid, string>(fields.Length);
    foreach (FieldRecord field1 in fields)
    {
      FieldRecord field = field1;
      bool flag = false;
      if (field.ShortName.StartsWith("$"))
      {
        field.ShortName = string.Empty;
        flag = true;
      }
      IDBAttributeType dbAttributeType = this.TryFoundAttribute(session, tableName, field) ?? this.CreateAttribute(session, sourceDB, eventRecord, tableName, field, schemaTable, attributeTypeCollection);
      field.GUID = dbAttributeType.GUID;
      if (flag && dbAttributeType.AttributeType != FieldTypes.ftSystem)
        dbAttributeType.Options |= AttributeOptions.ImbaseFlag_IMHGen;
      if (field.EnterMode == ImEnterMode.IEM_EXPRESSION || schemaTable.AsEnumerable().All<DataRow>((System.Func<DataRow, bool>) (x => string.Compare(Convert.ToString(x["ColumnName"]), field.Field) != 0)))
        formules.Add(field.GUID, field.Data);
    }
    if (formules.Count <= 0)
      return;
    Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>(formules.Count);
    foreach (KeyValuePair<Guid, string> keyValuePair in formules)
    {
      ImbaseFormulaParser imbaseFormulaParser = new ImbaseFormulaParser(session, session.GetAttributeType(keyValuePair.Key), fields);
      dictionary.Add(keyValuePair.Key, imbaseFormulaParser.Parse(keyValuePair.Value));
    }
    formules = dictionary;
  }

  private IDBAttributeType CreateAttribute(
    IUserSession session,
    IDataBase sourceDB,
    EventRecord eventRecord,
    string tableName,
    FieldRecord field,
    DataTable schema,
    IDBAttributeTypeCollection attrTypeColl)
  {
    object _defaultValue = (object) null;
    List<object> objectList = (List<object>) null;
    MultiValueModes _multiValueMode = MultiValueModes.SingleValue;
    if (field.EnterMode == ImEnterMode.IEM_SIMPLE)
      _defaultValue = this.FormingValue(field.DataType, (object) field.Data);
    else if (field.EnterMode == ImEnterMode.IEM_LIST || field.EnterMode == ImEnterMode.IEM_LISTONLY)
    {
      _multiValueMode = MultiValueModes.SingleValueFromList;
      string[] strArray = field.Data.Split(',');
      if (strArray.Length != 3)
      {
        this.AddEventInfo(EventType.Warning, $"В базе-источнике неверное значение поля F_DATA для поля {field.Field} таблицы {tableName} в таблице IM_FIELDS");
      }
      else
      {
        DataTable dataTable = sourceDB.ExecuteDataTable($"SELECT {strArray[2]} FROM {strArray[0]} WHERE {strArray[1]}");
        objectList = new List<object>(dataTable.Rows.Count);
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          objectList.Add(this.FormingValue(field.DataType, dataTable.Rows[index][0]));
      }
    }
    switch (field.DataType)
    {
      case FieldTypes.ftString:
        DataRow[] dataRowArray = schema.Select($"{"ColumnName"} LIKE '{field.Field}'");
        field.Width = dataRowArray.Length == 1 ? Convert.ToInt64(dataRowArray[0]["ColumnSize"]) : (long) Intermech.Consts.MaxStringSize;
        break;
      case FieldTypes.ftMeasured:
        string newShortMeasureName;
        if (PumpSettings.TryFoundMeasure(field.Units, out newShortMeasureName))
          field.Units = newShortMeasureName;
        MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(field.Units);
        field.Width = descriptor.Empty ? -1L : descriptor.PhysicalQuantityID;
        break;
      default:
        if (field.DataType != FieldTypes.ftMeasured)
        {
          field.Width = 0L;
          break;
        }
        break;
    }
    if (field.DataType == FieldTypes.ftString && field.Width > (long) Intermech.Consts.MaxStringSize)
      field.DataType = FieldTypes.ftMemo;
    if (field.DataMode == ImDataMode.IDM_IMAGE)
    {
      field.DataType = FieldTypes.ftObjectLink;
      field.Width = (long) MetaDataHelper.GetObjectTypeID("cad00140-306c-11d8-b4e9-00304f19f545");
    }
    if (field.DataMode == ImDataMode.IDM_TEXT)
    {
      field.DataType = FieldTypes.ftObjectLink;
      field.Width = (long) MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseBLOBTypeGUID);
    }
    if (Convert.ToString(_defaultValue).StartsWith("$"))
      _defaultValue = (object) null;
    AttributeTypeProperties attrProperties = new AttributeTypeProperties(0, field.LongName, field.ShortName, string.Empty, string.Empty, field.DataType, _defaultValue, _multiValueMode, ComputeValueModes.NotComputableValue, field.Width, string.Empty, UniqueValueModes.NotUnique, 0, string.Empty, string.Empty, field.GUID, OptimizationModes.Write, false, AttributeOptions.ImbaseFlag_UsedInTables, string.Empty, 0, 0);
    int anAttributeType = attrTypeColl.Create(attrProperties);
    IDBAttributeType attributeType = session.GetAttributeType(anAttributeType);
    if (attributeType.MultipleValued == MultiValueModes.SingleValueFromList && objectList != null)
    {
      DataTable possibleValues = attributeType.GetPossibleValues();
      if (possibleValues != null)
      {
        possibleValues.BeginLoadData();
        for (int index = 0; index < objectList.Count; ++index)
        {
          object obj = objectList[index];
          possibleValues.Rows.Add((object) index, obj, obj);
        }
        possibleValues.EndLoadData();
        possibleValues.AcceptChanges();
        if (possibleValues.Rows.Count > 0)
          attributeType.SetNewPossibleValues(possibleValues);
      }
    }
    this.AddEventInfo(EventType.Text, $"Создан новый тип атрибута '{attributeType.Name}', ID = {attributeType.AttributeID}, Источник:  поле {field.Field}, таблица {tableName} {this.GetFullPath(sourceDB, eventRecord)}.");
    return attributeType;
  }

  private IDBAttributeType TryFoundAttribute(
    IUserSession session,
    string tableName,
    FieldRecord field)
  {
    return this.TryFoundAttributeInPumpSettingsCache(session, tableName, field) ?? this.TryFoundAttributeByName(session, field);
  }

  private IDBAttributeType TryFoundAttributeByName(IUserSession session, FieldRecord field)
  {
    field.LongName = ImbaseImpHelper.CheckSpecialNames(field.LongName, field.DataType);
    if (field.LongName.Equals("КОД МАТЕРИАЛА", StringComparison.InvariantCultureIgnoreCase))
      field.LongName = "Основной материал";
    IDBAttributeType attributeType = session.GetAttributeType(field.LongName, false);
    if (attributeType != null && attributeType.AttributeID < 0)
    {
      field.LongName += "^";
      attributeType = session.GetAttributeType(field.LongName, false);
    }
    return attributeType;
  }

  private IDBAttributeType TryFoundAttributeInPumpSettingsCache(
    IUserSession session,
    string tableName,
    FieldRecord field)
  {
    Guid attributeGuid = PumpSettings.GetAttributeGuid(tableName, field.Field);
    return !(attributeGuid == Guid.Empty) ? session.GetAttributeType(attributeGuid, false) : (IDBAttributeType) null;
  }

  private object FormingValue(FieldTypes fieldType, object DefValue)
  {
    if (CompareValuesHelper.NormalizedValue(DefValue) == null)
      return (object) null;
    try
    {
      switch (fieldType)
      {
        case FieldTypes.ftInteger:
          return (object) Convert.ToInt64(DefValue);
        case FieldTypes.ftDouble:
          return (object) Convert.ToDouble(DefValue, (IFormatProvider) CultureInfo.InvariantCulture);
        case FieldTypes.ftDateTime:
          return (object) Convert.ToDateTime(DefValue, (IFormatProvider) CultureInfo.InvariantCulture);
        default:
          return DefValue;
      }
    }
    catch
    {
      return (object) null;
    }
  }
}
