// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.API.ComImbaseAPI
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.Runtime.ComInterop.LocalServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Imbase.API;

[ComVisible(true)]
[Guid("329B2FF8-562F-40E7-8BF4-51712EE8996C")]
[ProgId("IPS.ImbaseAPI")]
[ClassInterface(ClassInterfaceType.None)]
[ComDefaultInterface(typeof (IImbaseAPI))]
public sealed class ComImbaseAPI : FreeThreadedObject, IImbaseAPI
{
  internal const string CTE_CATALOG = "CTE_CATALOG";
  private string _errorMessage;
  private int _errorCode;
  private readonly IImbaseAPIRem impl;
  private readonly IInvokeService invoker;

  public IIPSImbaseFolder SelectCadmechTemplate(int bSelectTemplateFolder)
  {
    string prompt = bSelectTemplateFolder <= 0 ? "Выбор папки для размещения типового элемента" : "Выбор типового элемента";
    long parentId = 0;
    long tableIdByName;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string internalTableName = "CTE_CATALOG";
      tableIdByName = ImbaseRawTable.GetTableIdByName(sessionKeeper.Session, internalTableName, Intermech.Imbase.Consts.ImbaseCatalogTypeID);
    }
    if (tableIdByName == 0L)
      return (IIPSImbaseFolder) null;
    long folderId = SelectFolder2.Select(tableIdByName, prompt, out parentId);
    return folderId != 0L ? (IIPSImbaseFolder) new ImbaseFolder(parentId, folderId) : (IIPSImbaseFolder) null;
  }

  public IIPSImbaseCatalog FindCatalog(int catalogIndex)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string internalTableName;
      switch (catalogIndex)
      {
        case 12:
          internalTableName = "CTE_CATALOG";
          break;
        case 22:
          internalTableName = "DEADJOINTS";
          break;
        case 23:
          internalTableName = "IM_SYSTEM";
          break;
        default:
          return (IIPSImbaseCatalog) null;
      }
      long tableIdByName = ImbaseRawTable.GetTableIdByName(sessionKeeper.Session, internalTableName, Intermech.Imbase.Consts.ImbaseCatalogTypeID);
      return tableIdByName == 0L ? (IIPSImbaseCatalog) null : (IIPSImbaseCatalog) new ImbaseCatalog(tableIdByName);
    }
  }

  public long AddBlob(string blobName, string blobData)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseBLOBTypeID).Create();
      (dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseNoteAttID) ?? dbObject.Attributes.AddAttribute(Intermech.Imbase.Consts.ImbaseNoteAttID, false)).Value = (object) blobData;
      if (string.IsNullOrEmpty(blobName))
        blobName = $"Imbase BLOB {Math.Abs(dbObject.ObjectID)}";
      dbObject.Caption = blobName;
      dbObject.CommitCreation(true);
      return dbObject.ObjectID;
    }
  }

  public string GetBlobData(long blobId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeById = sessionKeeper.Session.GetObject(blobId).GetAttributeByID(Intermech.Imbase.Consts.ImbaseNoteAttID);
      return attributeById != null ? attributeById.Value as string : string.Empty;
    }
  }

  public IIPSImbaseRawTable CreateTable(string origalTableName, string newTableName, int copyData)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      long tableIdByName = ImbaseRawTable.GetTableIdByName(session, origalTableName, Intermech.Imbase.Consts.ImbaseTableTypeID);
      if (tableIdByName == 0L)
        return (IIPSImbaseRawTable) null;
      IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableTypeID);
      if (objectCollection == null)
        throw new Exception(LocalizationHolder.rm.GetString("Imbase_TableType_NullCollection"));
      IDBObject objectActualCopy = session.GetObjectActualCopy(tableIdByName, false);
      if (objectActualCopy == null)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_NullSourceTable"), (object) Convert.ToString(tableIdByName)));
      IDBObject dbObject = objectCollection.Create(tableIdByName);
      if (dbObject == null)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_Prototype_NullNewObject"), (object) tableIdByName));
      List<AttributeValues> attributeValuesList = new List<AttributeValues>(3);
      attributeValuesList.Add(new AttributeValues(-50, (object) objectActualCopy.Caption));
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0020f-306c-11d8-b4e9-00304f19f545"));
      if (attributeByGuid != null)
      {
        if (attributeByGuid.AttributeType is IDBAttributeType4 attributeType && attributeType.Required == RequiredModes.AutoRequired)
        {
          if ((attributeType.Options & AttributeOptions.DisableNulls) != AttributeOptions.None)
            attributeValuesList.Add(new AttributeValues(attributeByGuid.AttributeID, (object) 0));
          else
            attributeValuesList.Add(new AttributeValues(attributeByGuid.AttributeID, (object) DBNull.Value));
        }
        else
          attributeByGuid.Delete(0L);
      }
      if (dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseInternalTableNameAttID) != null)
        attributeValuesList.Add(new AttributeValues(Intermech.Imbase.Consts.ImbaseInternalTableNameAttID, (object) newTableName));
      dbObject.SetAttributesValues(attributeValuesList.ToArray());
      DataSet tables = TableLoadHelper.GetTables(sessionKeeper.Session, tableIdByName, true);
      DataTable table = tables.Tables["IMS_DATA"];
      if (copyData == 0)
      {
        table.Clear();
        table.AcceptChanges();
      }
      else
        TableLoadHelper.ChangeRecordGuids(tables.Tables["IMS_DATA"]);
      TableLoadHelper.StoreData(session, dbObject.ObjectID, tables, session.GetCustomService(typeof (ITablesIndexer)) as ITablesIndexer);
      dbObject.CommitCreation(true);
      return (IIPSImbaseRawTable) new ImbaseRawTable(dbObject.ObjectID);
    }
  }

  public IIPSImbaseRawTable FindTableByName(string tableName)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long tableIdByName = ImbaseRawTable.GetTableIdByName(sessionKeeper.Session, tableName, Intermech.Imbase.Consts.ImbaseTableTypeID);
      return tableIdByName == 0L ? (IIPSImbaseRawTable) null : (IIPSImbaseRawTable) new ImbaseRawTable(tableIdByName);
    }
  }

  public void UpdateBlob(long blobId, string data)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(blobId);
      (dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseNoteAttID) ?? dbObject.Attributes.AddAttribute(Intermech.Imbase.Consts.ImbaseNoteAttID, true)).AsString = data;
    }
  }

  public IIPSImbaseFolder GetFolderById(long folderId)
  {
    if (folderId > 0L)
      return (IIPSImbaseFolder) new ImbaseFolder(0L, folderId);
    if (folderId != -1L)
      return (IIPSImbaseFolder) null;
    Guid objectGUID = new Guid("CADD99FF-306C-11D8-B4E9-00304F19F545");
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectGUID, false);
      return dbObject == null ? (IIPSImbaseFolder) null : (IIPSImbaseFolder) new ImbaseFolder(0L, dbObject.ObjectID);
    }
  }

  public ComImbaseAPI()
  {
    this.impl = (IImbaseAPIRem) new ImbaseAPIRemImplementation();
    if (ServicesManager.GetService(typeof (IImbaseAPI)) == null)
      ServicesManager.AddService(typeof (IImbaseAPI), (object) this);
    this.invoker = ServiceUtils.GetService<IInvokeService>((object) ServicesManager.ServiceContainer, true);
  }

  private IImbaseAPIRem ImbaseService => this.impl;

  private void GetError(Exception e)
  {
    if (ServicesManager.GetService(typeof (IOutputView)) is IOutputView service)
      service.WriteString("CADMECH API", e.Message);
    this._errorMessage = e.Message;
    this._errorCode = 1;
  }

  public int GetVersion()
  {
    try
    {
      this.ClearErrors();
      return this.ImbaseService.Version;
    }
    catch (Exception ex)
    {
      this.GetError(ex);
      return -1;
    }
  }

  public int SelectFromTable(
    string catalogDef,
    string objectDef,
    string filter,
    string showFields,
    string sortOrder,
    int recordsCount,
    string comment,
    ref byte[] dataPacket)
  {
    this.ClearErrors();
    try
    {
      DataTable records = (DataTable) null;
      FieldInfo[] fields = (FieldInfo[]) null;
      ContextInfo contextInfo = new ContextInfo();
      int num = this.invoker.InvokeFunc<int>(-1, (Func<int>) (() => this.ImbaseService.SelectFromTable(catalogDef, objectDef, filter, showFields, sortOrder, recordsCount, comment, ref records, ref fields, ref contextInfo)));
      if (num > 0)
        dataPacket = this.PackTable(fields, records, contextInfo);
      return num;
    }
    catch (Exception ex)
    {
      this.GetError(ex);
      return -1;
    }
  }

  public int ShowPropertyWindow(string guids)
  {
    try
    {
      this.ClearErrors();
      this.invoker.InvokeAction(-1, (Action) (() => this.ImbaseService.ShowPropertyWindow(guids)));
      return 1;
    }
    catch (Exception ex)
    {
      this.GetError(ex);
      return -1;
    }
  }

  public int CreateObject(long recordId, long linkId, ref string objectGuid)
  {
    this.ClearErrors();
    try
    {
      return this.ImbaseService.CreateObject(recordId, linkId, ref objectGuid);
    }
    catch (Exception ex)
    {
      this.GetError(ex);
      return -1;
    }
  }

  public int CreateObjectFromTempKey(string tempKey, ref string objectGuid)
  {
    this.ClearErrors();
    try
    {
      return this.ImbaseService.CreateObjectFromTempKey(tempKey, ref objectGuid);
    }
    catch (Exception ex)
    {
      this.GetError(ex);
      return -1;
    }
  }

  public int ErrorCode() => this._errorCode;

  public string ErrorMessage() => this._errorMessage;

  public int MaterialEntry(string command, string fileData, ref string result)
  {
    try
    {
      result = string.Empty;
      this.ClearErrors();
      int num = this.ImbaseService.MaterialEntry(command, ref fileData);
      result = fileData;
      return num;
    }
    catch (Exception ex)
    {
      this.GetError(ex);
      return -1;
    }
  }

  public int GetKeyInfo(
    string key,
    ref string tableRecord,
    ref string catalogRecord,
    ref string keysList)
  {
    try
    {
      this.ClearErrors();
      return this.ImbaseService.GetKeyInfo(key, ref tableRecord, ref catalogRecord, ref keysList);
    }
    catch (Exception ex)
    {
      this.GetError(ex);
      return -1;
    }
  }

  public int ShowTables(
    int showFlags,
    string fieldNames,
    ref string tableRecord,
    ref string catalogRecord,
    ref string keysList)
  {
    try
    {
      this.ClearErrors();
      string tableRecordLocal = tableRecord;
      string catalogRecordLocal = catalogRecord;
      string keysListLocal = keysList;
      int num = this.invoker.InvokeFunc<int>(-1, (Func<int>) (() => this.ImbaseService.ShowTables(showFlags, fieldNames, ref tableRecordLocal, ref catalogRecordLocal, ref keysListLocal)));
      tableRecord = tableRecordLocal;
      catalogRecord = catalogRecordLocal;
      keysList = keysListLocal;
      return num;
    }
    catch (Exception ex)
    {
      this.GetError(ex);
      return -1;
    }
  }

  public int SelectTable(
    long catalogId,
    string prompt,
    ref long tableId,
    ref string fullList,
    ref long recordKey)
  {
    try
    {
      this.ClearErrors();
      long tableIdLocal = tableId;
      string fullListLocal = fullList;
      long recordKeyLocal = recordKey;
      int num = this.invoker.InvokeFunc<int>(-1, (Func<int>) (() => this.ImbaseService.SelectTable(catalogId, prompt, ref tableIdLocal, ref fullListLocal, ref recordKeyLocal)));
      tableId = tableIdLocal;
      fullList = fullListLocal;
      recordKey = recordKeyLocal;
      return num;
    }
    catch (Exception ex)
    {
      this.GetError(ex);
      return -1;
    }
  }

  public int SelectFolder(long catalogId, string prompt, ref long folderId, ref string fullList)
  {
    try
    {
      this.ClearErrors();
      long folderIdLocal = folderId;
      string fullListLocal = fullList;
      int num = this.invoker.InvokeFunc<int>(-1, (Func<int>) (() => this.ImbaseService.SelectFolder(catalogId, prompt, ref folderIdLocal, ref fullListLocal)));
      folderId = folderIdLocal;
      fullList = fullListLocal;
      return num;
    }
    catch (Exception ex)
    {
      this.GetError(ex);
      return -1;
    }
  }

  public int FindItemByValue(string fieldName, string fieldValue, out string imbaseKey)
  {
    imbaseKey = string.Empty;
    try
    {
      this.ClearErrors();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return CadmechHelper.GetServer(sessionKeeper.Session).FindItemByValue(sessionKeeper.Session.SessionGUID, fieldName, fieldValue, ref imbaseKey);
    }
    catch (Exception ex)
    {
      this.GetError(ex);
      return -1;
    }
  }

  public IIPSImbaseFolder FindRootTemplateFolder(bool createIfNotExist)
  {
    throw new NotImplementedException();
  }

  public int CreateTable(string tableInfo, string structData, string tableData, string addInfo)
  {
    try
    {
      this.ClearErrors();
      return 0;
    }
    catch (Exception ex)
    {
      this.GetError(ex);
      return -1;
    }
  }

  public int GetBaseVersionGuids(string[] objectGuids, out object[] baseData)
  {
    baseData = (object[]) null;
    try
    {
      this.ClearErrors();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return CadmechHelper.GetServer(sessionKeeper.Session).GetBaseVersionGuids(sessionKeeper.Session.SessionGUID, objectGuids, out baseData);
    }
    catch (Exception ex)
    {
      this.GetError(ex);
      return -1;
    }
  }

  private void ClearErrors()
  {
    this._errorCode = 0;
    this._errorMessage = string.Empty;
  }

  private byte[] PackTable(FieldInfo[] fields, DataTable records, ContextInfo contextInfo)
  {
    using (MemoryStream output = new MemoryStream(4096 /*0x1000*/))
    {
      int maxStringSize = 0;
      int bufferSize = this.AnalizeSizes(records, fields, ref maxStringSize);
      byte[] charBuffer = new byte[maxStringSize];
      BinaryWriter writer = new BinaryWriter((Stream) output);
      this.WriteHeader(writer, fields, records, contextInfo, bufferSize);
      this.WriteFields(writer, fields);
      this.WriteRecords(writer, fields, records, charBuffer);
      return output.ToArray();
    }
  }

  private FieldInfo[] AddFakeField(FieldInfo[] fields)
  {
    List<FieldInfo> fieldInfoList = new List<FieldInfo>((IEnumerable<FieldInfo>) fields);
    foreach (FieldInfo fieldInfo in fieldInfoList)
    {
      if ("Размеры и параметры".Equals(fieldInfo.LongName, StringComparison.InvariantCultureIgnoreCase))
        return fields;
    }
    fieldInfoList.Add(new FieldInfo()
    {
      AttributeId = 99999,
      DataOffset = 0,
      DataSize = 1,
      FieldKind = FieldKind.Data,
      FieldType = FieldType.String,
      Flags = 0,
      LongName = "Размеры и параметры",
      Required = false,
      ShortName = string.Empty
    });
    return fieldInfoList.ToArray();
  }

  private void WriteFields(BinaryWriter writer, FieldInfo[] fields)
  {
    int length = fields.Length;
    for (int index = 0; index < length; ++index)
    {
      FieldInfo field = fields[index];
      if (field.AttributeId == -2)
        writer.Write(this.ToBytesArray("F_KEY", 8));
      else if (field.AttributeId == -12)
        writer.Write(this.ToBytesArray("F_GUID", 8));
      else
        writer.Write(this.ToBytesArray("F" + field.AttributeId.ToString(), 8));
      writer.Write(this.ToBytesArray(field.LongName, 64 /*0x40*/));
      writer.Write(this.ToBytesArray(field.ShortName, 8));
      writer.Write(this.ToBytesArray(field.Units, 8));
      writer.Write(field.Flags);
      writer.Write(0);
      writer.Write((int) field.FieldType);
      writer.Write((int) field.FieldKind);
      writer.Write(field.DataSize);
      writer.Write(field.DataOffset);
    }
  }

  private void WriteRecords(
    BinaryWriter writer,
    FieldInfo[] fields,
    DataTable records,
    byte[] charBuffer)
  {
    DataRowCollection rows = records.Rows;
    int count = rows.Count;
    int length = fields.Length;
    int[] numArray = new int[length];
    for (int index = 0; index < length; ++index)
    {
      FieldInfo field = fields[index];
      DataColumn column = records.Columns[field.AttributeId.ToString()];
      numArray[index] = column == null ? -1 : column.Ordinal;
    }
    for (int index1 = 0; index1 < count; ++index1)
    {
      DataRow dataRow = rows[index1];
      for (int index2 = 0; index2 < length; ++index2)
      {
        FieldInfo field = fields[index2];
        int columnIndex = numArray[index2];
        if (columnIndex != -1)
        {
          object obj = dataRow[columnIndex];
          switch (field.FieldType)
          {
            case FieldType.String:
              writer.Write(this.ToBytesArray(ComImbaseAPI.ToString(obj), field.DataSize));
              continue;
            case FieldType.Smallint:
              writer.Write(ComImbaseAPI.ToInt16(obj));
              continue;
            case FieldType.Integer:
              writer.Write(ComImbaseAPI.ToInt32(obj));
              continue;
            case FieldType.Boolean:
              writer.Write(ComImbaseAPI.ToInt16(obj));
              continue;
            case FieldType.Float:
              writer.Write(ComImbaseAPI.ToDouble(obj));
              continue;
            case FieldType.Largeint:
              writer.Write(ComImbaseAPI.ToInt64(obj));
              continue;
            default:
              throw new Exception("unknown data type " + field.FieldType.ToString());
          }
        }
      }
    }
  }

  private static string ToString(object value)
  {
    return value == null || DBNull.Value.Equals(value) ? string.Empty : Convert.ToString(value);
  }

  private static double ToDouble(object value)
  {
    return value == null || DBNull.Value.Equals(value) ? 0.0 : Convert.ToDouble(value);
  }

  private static short ToInt16(object value)
  {
    return value == null || DBNull.Value.Equals(value) ? (short) 0 : Convert.ToInt16(value);
  }

  private static int ToInt32(object value)
  {
    return value == null || DBNull.Value.Equals(value) ? 0 : Convert.ToInt32(value);
  }

  private static long ToInt64(object value)
  {
    return value == null || DBNull.Value.Equals(value) ? 0L : Convert.ToInt64(value);
  }

  private void WriteHeader(
    BinaryWriter writer,
    FieldInfo[] fields,
    DataTable records,
    ContextInfo contextInfo,
    int bufferSize)
  {
    int hi;
    int lo;
    this.SplintInt64(contextInfo.LinkId, out hi, out lo);
    writer.Write((int) contextInfo.TableId);
    writer.Write((int) contextInfo.CatalogId);
    writer.Write(lo);
    writer.Write(fields.Length);
    writer.Write(records.Rows.Count);
    writer.Write(bufferSize);
    writer.Write(0);
    writer.Write(0);
    writer.Write(0);
    writer.Write(0);
    writer.Write(0);
    writer.Write(hi);
    writer.Write(0);
    writer.Write(this.ToBytesArray(contextInfo.TableName, 16 /*0x10*/));
    writer.Write(this.ToBytesArray(contextInfo.Description, 128 /*0x80*/));
    writer.Write(this.ToBytesArray(contextInfo.User, 16 /*0x10*/));
    writer.Write(this.ToBytesArray(contextInfo.IndexFields, 256 /*0x0100*/));
    writer.Write(contextInfo.Created);
    writer.Write(contextInfo.Modified);
  }

  private void SplintInt64(long value, out int hi, out int lo)
  {
    hi = (int) (value >> 32 /*0x20*/ & (long) uint.MaxValue);
    lo = (int) (value & (long) uint.MaxValue);
  }

  private byte[] ToBytesArray(string value, int maxSize)
  {
    int length = maxSize;
    byte[] lpMultiByteStr = new byte[length];
    ComImbaseAPI.WideCharToMultiByte(0, 0, value, length, lpMultiByteStr, length, 0, 0);
    return lpMultiByteStr;
  }

  private int AnalizeSizes(DataTable records, FieldInfo[] fields, ref int maxStringSize)
  {
    int length = fields.Length;
    maxStringSize = 0;
    int num1 = 0;
    List<int> intList = new List<int>();
    for (int index = 0; index < length; ++index)
    {
      if (fields[index].FieldType == FieldType.String)
        intList.Add(index);
    }
    int count = intList.Count;
    if (intList.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) records.Rows)
      {
        for (int index1 = 0; index1 < count; ++index1)
        {
          int index2 = intList[index1];
          FieldInfo field = fields[index2];
          int num2 = row[field.AttributeId.ToString()].ToString().Length + 1;
          if (num2 > field.DataSize)
            fields[index2].DataSize = num2;
          if (num2 > maxStringSize)
            maxStringSize = num2;
        }
      }
    }
    for (int index = 0; index < length; ++index)
    {
      fields[index].DataOffset = num1;
      num1 += fields[index].DataSize;
    }
    return num1;
  }

  [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
  internal static extern int WideCharToMultiByte(
    int CodePage,
    int dwFlags,
    string lpWideCharStr,
    int cchWideChar,
    byte[] lpMultiByteStr,
    int cchMultiByte,
    int lpDefaultChar,
    int lpUsedDefaultChar);
}
