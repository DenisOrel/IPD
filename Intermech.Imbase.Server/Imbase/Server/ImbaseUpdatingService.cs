// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImbaseUpdatingService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server;

internal class ImbaseUpdatingService : LongLifeObject, IImbaseUpdatingService
{
  private const string F_GUID = "cad00130-306c-11d8-b4e9-00304f19f545";
  private List<long> _tableIDsLock = new List<long>();

  public List<Guid> GetExistingRecordsGuids(
    Guid sessionGuid,
    List<Guid> guids,
    out List<Guid> notExistingRecordsGuids)
  {
    List<Guid> existingRecordsGuids = new List<Guid>();
    notExistingRecordsGuids = new List<Guid>();
    IImbaseIndexingService service = ServerServices.GetService(typeof (IImbaseIndexingService)) as IImbaseIndexingService;
    UserSession session = ImbaseServer.GetSession(sessionGuid) as UserSession;
    if (guids != null && service != null && session != null)
    {
      string[] colsNames = new string[1]
      {
        IndexesField.F_TABLE_ID
      };
      foreach (Guid guid in guids)
      {
        DataTable dataTable = service.Search(session.SessionGUID, (List<long>) null, Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID, colsNames, guid.ToString(), SearchesAccuracy.Exact);
        if (dataTable != null && dataTable.Rows.Count > 0)
          existingRecordsGuids.Add(guid);
        else
          notExistingRecordsGuids.Add(guid);
      }
    }
    return existingRecordsGuids;
  }

  public object UpdateRecordsValue(Guid sessionGuid, DataTable dt)
  {
    if (!(ImbaseServer.GetSession(sessionGuid) is UserSession session))
      throw new Exception("Не удалось получить сессию пользователя");
    if (dt == null)
      throw new Exception("Пустая таблица входных данных");
    if (dt.Columns.Count < 4)
      throw new Exception("Таблица входных данных должна иметь больше одного столбца");
    if (!dt.Columns.Contains("cad00130-306c-11d8-b4e9-00304f19f545"))
      throw new Exception($"В таблице входных данных отсутствует обязательный столбец с наименованием '{"cad00130-306c-11d8-b4e9-00304f19f545"}'");
    if (!dt.Columns.Contains(IndexesField.F_TABLE_ID))
      throw new Exception($"В таблице входных данных отсутствует обязательный столбец с наименованием '{IndexesField.F_TABLE_ID}'");
    if (!dt.Columns.Contains(IndexesField.F_TABKEY))
      throw new Exception($"В таблице входных данных отсутствует обязательный столбец с наименованием '{IndexesField.F_TABKEY}'");
    List<string> stringList = new List<string>();
    List<string> exceptions = (List<string>) null;
    List<IMSAttributeType> attrTypes = (List<IMSAttributeType>) null;
    dt = this.CheckValidColumns(dt, out exceptions, out attrTypes);
    stringList.AddRange((IEnumerable<string>) exceptions);
    Dictionary<long, List<DataRow>> dictionary = this.GroupRows(sessionGuid, dt, out exceptions);
    stringList.AddRange((IEnumerable<string>) exceptions);
    foreach (KeyValuePair<long, List<DataRow>> keyValuePair in dictionary)
    {
      long key = keyValuePair.Key;
      try
      {
        if (this._tableIDsLock.Contains(key))
          throw new Exception($"Таблица с идентификатором '{key}' уже обрабатывается.");
        this._tableIDsLock.Add(key);
        exceptions = this.UpdateTable(session, key, keyValuePair.Value, attrTypes, false);
        stringList.AddRange((IEnumerable<string>) exceptions);
      }
      catch (Exception ex)
      {
        stringList.Add(ex.Message);
      }
      this._tableIDsLock.Remove(key);
    }
    return (object) stringList;
  }

  public object UpdateImbaseTable(Guid sessionGuid, Guid tableGuid, DataTable dt)
  {
    if (!(ImbaseServer.GetSession(sessionGuid) is UserSession session))
      throw new Exception("Не удалось получить сессию пользователя");
    if (dt == null)
      throw new Exception("Пустая таблица входных данных");
    if (dt.Columns.Count < 2)
      throw new Exception("Таблица данных должна содержать больше одного столбца");
    if (!dt.Columns.Contains("cad00130-306c-11d8-b4e9-00304f19f545"))
      throw new Exception($"В таблице входных данных отсутствует обязательный столбец с наименованием '{"cad00130-306c-11d8-b4e9-00304f19f545"}'");
    List<string> stringList = new List<string>();
    long objectIdByGuid = this.GetObjectIDByGuid(session, tableGuid);
    if (objectIdByGuid == 0L)
      throw new Exception($"Отсутствует таблица с глобальным идентификатором '{tableGuid}'.");
    if (this._tableIDsLock.Contains(objectIdByGuid))
      throw new Exception($"Таблица с идентификатором '{objectIdByGuid}' уже обрабатывается.");
    this._tableIDsLock.Add(objectIdByGuid);
    try
    {
      List<string> exceptions = (List<string>) null;
      List<IMSAttributeType> attrTypes = (List<IMSAttributeType>) null;
      dt = this.CheckValidColumns(dt, out exceptions, out attrTypes);
      stringList.AddRange((IEnumerable<string>) exceptions);
      List<string> collection = this.UpdateTable(session, objectIdByGuid, new List<DataRow>((IEnumerable<DataRow>) dt.Select()), attrTypes, true);
      stringList.AddRange((IEnumerable<string>) collection);
    }
    finally
    {
      this._tableIDsLock.Remove(objectIdByGuid);
    }
    return (object) stringList;
  }

  public Tuple<long, long> SearchData(
    Guid sessionGuid,
    Guid catalogGuid,
    List<Tuple<int, object>> data)
  {
    Tuple<long, long> tuple1 = (Tuple<long, long>) null;
    if (sessionGuid != Guid.Empty && data != null && data.Count > 0)
    {
      IImbaseIndexingService service = ServerServices.GetService(typeof (IImbaseIndexingService)) as IImbaseIndexingService;
      UserSession session = ImbaseServer.GetSession(sessionGuid) as UserSession;
      if (service != null && session != null)
      {
        List<long> catalogIDs = (List<long>) null;
        if (catalogGuid != Guid.Empty)
          catalogIDs = new List<long>()
          {
            this.GetObjectIDByGuid(session, catalogGuid)
          };
        long num1 = 0;
        long num2 = -1;
        string[] colsNames = new string[2]
        {
          IndexesField.F_LINK_ID,
          IndexesField.F_TABKEY
        };
        Dictionary<int, IMSAttributeType> dictionary = new Dictionary<int, IMSAttributeType>(data.Count);
        foreach (Tuple<int, object> tuple2 in data)
        {
          if (tuple2.Item1 != 0 && tuple2.Item2 != null && tuple2.Item2 != DBNull.Value && MetaDataHelper.ExistsAttributeType(tuple2.Item1))
          {
            if (!dictionary.ContainsKey(tuple2.Item1))
            {
              IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(tuple2.Item1);
              dictionary.Add(tuple2.Item1, attributeType);
            }
            object parsedValue = (object) null;
            if (this.TryParse(session, tuple2.Item2, dictionary[tuple2.Item1].FieldType, out parsedValue, (object) string.Empty))
            {
              DataTable dataTable = service.Search(sessionGuid, catalogIDs, tuple2.Item1, colsNames, parsedValue.ToString(), SearchesAccuracy.Exact);
              if (dataTable.Rows.Count != 0 && dataTable.Columns.Contains(IndexesField.F_LINK_ID) && dataTable.Columns.Contains(IndexesField.F_TABKEY))
              {
                num1 = Convert.ToInt64(dataTable.Rows[0][IndexesField.F_LINK_ID]);
                num2 = Convert.ToInt64(dataTable.Rows[0][IndexesField.F_TABKEY]);
                break;
              }
            }
          }
        }
        tuple1 = new Tuple<long, long>(num1, num2);
      }
    }
    return tuple1;
  }

  private bool AddAttrToTable(
    UserSession session,
    DataTable dtAttrs,
    DataTable dtData,
    string strAttrGuid)
  {
    bool table = true;
    Guid anAttributeGuid = new Guid(strAttrGuid);
    IDBAttributeType attributeType = session.GetAttributeType(anAttributeGuid);
    DataRow row1 = dtAttrs.NewRow();
    row1["F_ATTRIBUTE_GUID"] = (object) anAttributeGuid;
    row1["F_REQUIRED"] = (object) 2;
    row1["F_COMPUTED"] = (object) 0;
    row1["F_FORMULA"] = (object) string.Empty;
    row1["F_UNIQUE"] = (object) 0;
    row1["F_DEFAULT_VALUE"] = attributeType.DefaultValue;
    row1["F_OPTIONS"] = (object) attributeType.Options;
    row1["F_UNITS"] = (object) string.Empty;
    if (attributeType.AttributeType == FieldTypes.ftMeasured)
    {
      long baseMeasureId = MeasureHelper.GetBaseMeasureID(attributeType.PropertiesStructure.SizeType);
      QuickObjectInfo objectInfo = session.GetObjectInfo(baseMeasureId);
      if (!objectInfo.Empty)
        row1["F_UNITS"] = (object) objectInfo.VersionGuid;
    }
    dtAttrs.Rows.Add(row1);
    if (TableLoadHelper.CreateDataColumn(dtData, attributeType) != null && attributeType.DefaultValue != null && attributeType.DefaultValue != DBNull.Value)
    {
      foreach (DataRow row2 in (InternalDataCollectionBase) dtData.Rows)
        row2[strAttrGuid] = attributeType.DefaultValue;
    }
    dtAttrs.AcceptChanges();
    dtData.AcceptChanges();
    return table;
  }

  private IDBObject CheckOutImaseTable(UserSession session, long tableID, out bool needCheckIn)
  {
    needCheckIn = false;
    IDBObject dbObject = session.GetObjectActualCopy(tableID, false);
    if (dbObject == null)
      throw new Exception($"Не удалось получить таблицу IMBASE (ID = {tableID})");
    if (dbObject.ObjectModifyMode == ObjectModifyModes.CantModify)
      throw new Exception($"Нельзя модифицировать объект '{dbObject.Caption}' (ID = {tableID})");
    if (dbObject.CheckoutBy != 0L && dbObject.CheckoutBy != session.UserID)
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(dbObject.CheckoutBy);
      if (objectInfo.Empty || string.IsNullOrEmpty(objectInfo.Caption))
        throw new Exception($"Объект '{dbObject.Caption}' (ID = {tableID}) взят на изменение пользователем с идентификатором {dbObject.CheckoutBy}");
      throw new Exception($"Объект '{dbObject.Caption}' (ID = {tableID}) взят на изменение пользователем '{objectInfo.Caption}'");
    }
    if (dbObject.ObjectModifyMode != ObjectModifyModes.InBase && (needCheckIn = dbObject.CheckoutBy == 0L))
      dbObject = dbObject.CheckOut(false);
    return dbObject;
  }

  private DataTable CheckValidColumns(
    DataTable dt,
    out List<string> exceptions,
    out List<IMSAttributeType> attrTypes)
  {
    exceptions = new List<string>(dt.Columns.Count - 1);
    attrTypes = new List<IMSAttributeType>(dt.Columns.Count - 1);
    int index = 3;
    while (index < dt.Columns.Count)
    {
      string columnName = dt.Columns[index].ColumnName;
      try
      {
        if (!GuidHelper.IsGuid(columnName))
          throw new Exception($"Наименование столбца '{columnName}' не является глобальным идентификатором атрибута");
        if (!MetaDataHelper.ExistsAttributeType(new Guid(columnName)))
          throw new Exception($"Атрибут с глобальным идентификатором '{columnName}' в системе не обнаружен");
        attrTypes.Add(MetaDataHelper.GetAttributeType(new Guid(columnName)));
        ++index;
      }
      catch (Exception ex)
      {
        dt.Columns.Remove(columnName);
        exceptions.Add(ex.Message);
      }
    }
    return dt;
  }

  private long GetObjectIDByGuid(UserSession session, Guid objGuid)
  {
    long objectIdByGuid = 0;
    QuickObjectInfo objectInfo = session.GetObjectInfo(objGuid);
    if (!objectInfo.Empty)
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(objectInfo.ObjectID, false);
      if (objectActualCopy != null)
        objectIdByGuid = objectActualCopy.ObjectID;
    }
    return objectIdByGuid;
  }

  private string GetTypeName(FieldTypes type)
  {
    string typeName = string.Empty;
    switch (type)
    {
      case FieldTypes.ftInteger:
        typeName = "Целое значение";
        break;
      case FieldTypes.ftDouble:
        typeName = "Вещественное значение";
        break;
      case FieldTypes.ftDateTime:
        typeName = "Дата и время";
        break;
      case FieldTypes.ftBoolean:
        typeName = "Логическое значение";
        break;
      case FieldTypes.ftMeasured:
        typeName = "Значение выраженное в единицах измерения";
        break;
      case FieldTypes.ftGuid:
        typeName = "Глобальный идентификатор";
        break;
    }
    return typeName;
  }

  private Dictionary<long, List<DataRow>> GroupRows(
    Guid sessionGuid,
    DataTable dt,
    out List<string> exceptions)
  {
    if (!(ServerServices.GetService(typeof (IImbaseIndexingService)) is IImbaseIndexingService service))
      throw new Exception("Не удалось получить сервис для работы с индексами IMBASE");
    Dictionary<long, List<DataRow>> dictionary = new Dictionary<long, List<DataRow>>();
    exceptions = new List<string>(dt.Rows.Count);
    string[] colsNames = new string[1]
    {
      IndexesField.F_TABLE_ID
    };
    string empty = string.Empty;
    foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
    {
      try
      {
        long result = 0;
        if (!long.TryParse(Convert.ToString(row[IndexesField.F_TABLE_ID]), out result) || result == 0L)
        {
          empty = Convert.ToString(row["cad00130-306c-11d8-b4e9-00304f19f545"]);
          if (string.IsNullOrEmpty(empty) || !GuidHelper.IsGuid(empty))
            throw new Exception("Не указан идентификатор записи.");
          DataTable dataTable = service.Search(sessionGuid, (List<long>) null, Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID, colsNames, empty, SearchesAccuracy.Exact);
          if (dataTable == null || dataTable.Rows.Count == 0)
            throw new Exception("Запись отсутствует в таблице индексов.");
          result = Convert.ToInt64(dataTable.Rows[0][IndexesField.F_TABLE_ID]);
        }
        if (dictionary.ContainsKey(result))
          dictionary[result].Add(row);
        else
          dictionary.Add(result, new List<DataRow>() { row });
      }
      catch (Exception ex)
      {
        exceptions.Add($"Запись '{empty}'. {ex.Message}");
      }
    }
    return dictionary;
  }

  private bool TryParse(
    UserSession session,
    object objValue,
    FieldTypes type,
    out object parsedValue,
    object objMeasureGuid)
  {
    bool flag = true;
    parsedValue = (object) null;
    if (objValue != null && objValue != DBNull.Value)
    {
      switch (type)
      {
        case FieldTypes.ftInteger:
          long result1 = long.MinValue;
          if (flag = long.TryParse(objValue.ToString(), out result1))
          {
            parsedValue = objValue;
            break;
          }
          break;
        case FieldTypes.ftDouble:
          double result2 = double.MinValue;
          if (flag = double.TryParse(objValue.ToString(), out result2))
          {
            parsedValue = objValue;
            break;
          }
          break;
        case FieldTypes.ftDateTime:
          DateTime result3 = DateTime.MinValue;
          if (flag = DateTime.TryParse(objValue.ToString(), out result3))
          {
            parsedValue = objValue;
            break;
          }
          break;
        case FieldTypes.ftBoolean:
          bool result4 = false;
          if (flag = bool.TryParse(objValue.ToString(), out result4))
          {
            parsedValue = objValue;
            break;
          }
          break;
        case FieldTypes.ftMeasured:
          if (objValue is MeasuredValue measuredValue)
          {
            string g = objMeasureGuid.ToString();
            if (string.IsNullOrEmpty(g))
            {
              parsedValue = (object) measuredValue.Value;
              break;
            }
            QuickObjectInfo objectInfo = session.GetObjectInfo(new Guid(g));
            if (objectInfo.Empty)
            {
              parsedValue = (object) measuredValue.Value;
              break;
            }
            MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(objectInfo.ObjectID);
            parsedValue = (object) (!descriptor.Empty ? measuredValue.Value / descriptor.K : measuredValue.Value);
            break;
          }
          double result5 = double.MinValue;
          if (flag = double.TryParse(objValue.ToString(), out result5))
          {
            parsedValue = objValue;
            break;
          }
          break;
        case FieldTypes.ftGuid:
          if (flag = GuidHelper.IsGuid(objValue.ToString()))
          {
            parsedValue = objValue;
            break;
          }
          break;
        default:
          parsedValue = objValue;
          break;
      }
    }
    else
      parsedValue = objValue;
    return flag;
  }

  private List<string> UpdateTable(
    UserSession session,
    long tableID,
    List<DataRow> rows,
    List<IMSAttributeType> attrTypes,
    bool addIfEmpty)
  {
    List<string> stringList = new List<string>();
    bool needCheckIn = false;
    IDBObject dbObject = this.CheckOutImaseTable(session, tableID, out needCheckIn);
    tableID = dbObject.ObjectID;
    DataSet tables = TableLoadHelper.GetTables((IUserSession) session, tableID, false);
    DataTable dtAttrs = tables != null && tables.Tables.Contains("IMS_ATTR_TYPES") && tables.Tables.Contains("IMS_DATA") ? tables.Tables["IMS_ATTR_TYPES"] : throw new Exception($"Битые данные в таблице IMBASE (ID = {tableID})");
    DataTable table = tables.Tables["IMS_DATA"];
    Dictionary<Guid, object> dictionary = new Dictionary<Guid, object>(attrTypes.Count);
    foreach (IMSAttributeType attrType in attrTypes)
    {
      if (attrType.FieldType == FieldTypes.ftMeasured)
      {
        DataRow[] dataRowArray = dtAttrs.Select($"{"F_ATTRIBUTE_GUID"}='{attrType.AttributeGuid}'");
        dictionary.Add(attrType.AttributeGuid, dataRowArray[0]["F_UNITS"]);
      }
    }
    bool flag = false;
    string str = string.Empty;
    string empty = string.Empty;
    foreach (DataRow row1 in rows)
    {
      try
      {
        if (row1["cad00130-306c-11d8-b4e9-00304f19f545"] != null && row1["cad00130-306c-11d8-b4e9-00304f19f545"] != DBNull.Value)
        {
          empty = Convert.ToString(row1["cad00130-306c-11d8-b4e9-00304f19f545"]);
          str = "F_GUID";
        }
        else if (row1[IndexesField.F_TABKEY] != null && row1[IndexesField.F_TABKEY] != DBNull.Value)
        {
          empty = Convert.ToString(row1[IndexesField.F_TABKEY]);
          str = "F_KEY";
        }
        DataRow[] dataRowArray = table.Select($"{str}='{empty}'");
        DataRow row2;
        if (dataRowArray.Length == 0)
        {
          if (!addIfEmpty)
            throw new Exception($"Запись отсутствует в таблице IMBASE (ID = {tableID})");
          row2 = table.NewRow();
          row2["F_GUID"] = (object) Guid.NewGuid();
          table.Rows.Add(row2);
        }
        else
          row2 = dataRowArray[0];
        foreach (IMSAttributeType attrType in attrTypes)
        {
          try
          {
            str = attrType.AttributeGuid.ToString();
            object objValue = row1[str];
            if (!table.Columns.Contains(str))
            {
              if (dtAttrs.Select($"{"F_ATTRIBUTE_GUID"}='{str}'").Length != 0)
                throw new Exception($"Значение поля '{str}' изменять нельзя, поле в таблице IMBASE (ID = {tableID}) является вычисляемым.");
              this.AddAttrToTable(session, dtAttrs, table, str);
            }
            object parsedValue = (object) null;
            if (!this.TryParse(session, objValue, attrType.FieldType, out parsedValue, attrType.FieldType == FieldTypes.ftMeasured ? dictionary[attrType.AttributeGuid] : (object) null))
              throw new Exception($"Значение атрибута '{attrType.Name}' с глобальным идентификатором '{attrType.AttributeGuid}' не может быть приведено к типу '{this.GetTypeName(attrType.FieldType)}'");
            row2[str] = parsedValue;
            flag = true;
          }
          catch (Exception ex)
          {
            stringList.Add($"Запись '{empty}'. {ex.Message}");
          }
        }
      }
      catch (Exception ex)
      {
        stringList.Add($"Запись '{empty}'. {ex.Message}");
      }
    }
    if (flag)
    {
      TableLoadHelper.StoreData((IUserSession) session, tableID, tables, session.GetCustomService(typeof (ITablesIndexer)) as ITablesIndexer);
      if (needCheckIn)
        dbObject.CheckIn();
      else if (dbObject.ObjectModifyMode == ObjectModifyModes.InBase && session.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService)
        customService.UpdateAfterTableCheckIn(session.SessionGUID, dbObject.ObjectID);
    }
    else if (needCheckIn)
      dbObject.CancelChanges();
    return stringList;
  }
}
