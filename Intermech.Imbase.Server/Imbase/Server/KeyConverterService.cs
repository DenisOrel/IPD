// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.KeyConverterService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Imbase.Server.Sync.Helper;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

#nullable disable
namespace Intermech.Imbase.Server;

internal class KeyConverterService : LongLifeObject, IKeyConverter
{
  private IAsyncResult _asyncResult;
  private bool _cancel;
  private static readonly object LinksLockObject = new object();
  private static SortedList<int, long> ReferencesList = (SortedList<int, long>) null;
  private int _completed;
  private List<ObjectInfoForExteption> _convertedInfo;
  private bool _isFirstTaskComplete;
  private object _lockObject = new object();
  private bool _paused;
  private IUserSession _session;
  private bool _terminated;

  public List<ObjectInfoForExteption> ConvertedInfo => this._convertedInfo;

  public bool IsFirstTaskComplete
  {
    get
    {
      int num = this._isFirstTaskComplete ? 1 : 0;
      this._isFirstTaskComplete = false;
      return num != 0;
    }
  }

  public int State
  {
    get
    {
      if (this._terminated)
        return -2;
      if (this._asyncResult == null)
        return 0;
      return this._paused ? -1 : this._completed + 1;
    }
  }

  public int Value => !this._terminated ? this._completed : 100;

  public string ConvertOldKey(IUserSession session, string oldKey)
  {
    if (string.IsNullOrEmpty(oldKey) || oldKey.Length != 20 || char.ToUpper(oldKey[0]) != 'I' && char.ToUpper(oldKey[1]) != '6')
      return oldKey;
    long linkId = -1;
    string s1 = oldKey.Substring(2, 6);
    int catalogKey = 0;
    ref int local1 = ref catalogKey;
    if (!int.TryParse(s1, NumberStyles.AllowHexSpecifier, (IFormatProvider) null, out local1))
      return oldKey;
    string s2 = oldKey.Substring(8, 6);
    int catalogRecordKey = 0;
    ref int local2 = ref catalogRecordKey;
    if (!int.TryParse(s2, NumberStyles.AllowHexSpecifier, (IFormatProvider) null, out local2))
      return oldKey;
    string s3 = oldKey.Substring(14, 6);
    long recordId = 0;
    ref long local3 = ref recordId;
    if (!long.TryParse(s3, NumberStyles.AllowHexSpecifier, (IFormatProvider) null, out local3) || !KeyConverterService.FindNewLinkId(session, catalogKey, catalogRecordKey, ref linkId))
      return oldKey;
    string keyValue = ImbaseHelper.MakeInternalImbaseKey(linkId, recordId);
    return ImbaseHelper.ConvertImbaseKey(session, keyValue);
  }

  public void Pause()
  {
    lock (this._lockObject)
      this._paused = true;
  }

  public void Start(Guid sessionGuid)
  {
    lock (this._lockObject)
    {
      if (this._asyncResult == null)
      {
        this._paused = false;
        this._cancel = false;
        this._terminated = false;
        this._completed = 0;
        this._session = ImbaseServer.GetSession(sessionGuid);
        this._convertedInfo = new List<ObjectInfoForExteption>(0);
        this._asyncResult = new KeyConverterService.ScanTablesHandler(this.ScanProcess).BeginInvoke((object) this, new AsyncCallback(this.OnTaskTerminated), (object) null);
      }
      else
        this._paused = false;
    }
  }

  public void Stop()
  {
    if (this._asyncResult == null)
      return;
    lock (this._lockObject)
      this._cancel = true;
    this._asyncResult.AsyncWaitHandle.WaitOne();
    this._asyncResult = (IAsyncResult) null;
  }

  private void CheckDuplicateCodes(DataTable tbl)
  {
    using (IEnumerator<IGrouping<int, DataRow>> enumerator = tbl.AsEnumerable().GroupBy<DataRow, int>((System.Func<DataRow, int>) (x => Convert.ToInt32(x[tbl.Columns[1]]))).Where<IGrouping<int, DataRow>>((System.Func<IGrouping<int, DataRow>, bool>) (x => x.Count<DataRow>() > 1)).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        IGrouping<int, DataRow> current = enumerator.Current;
        List<string> values = new List<string>();
        foreach (DataRow dataRow in (IEnumerable<DataRow>) current)
          values.Add($"{Convert.ToString(dataRow[3])}({Convert.ToInt64(dataRow[0])})");
        this._terminated = true;
        this._cancel = true;
        throw new Exception($" Каталоги : {string.Join(",", (IEnumerable<string>) values)} имеют одинаковый Код Imbase.");
      }
    }
  }

  private void ConvertInReceptures(IUserSession session)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableMixTypeID);
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_ID)
    })
    {
      ColumnNames = new ColumnNameMapping[1]
      {
        ColumnNameMapping.ID
      },
      FailIfNotFound = false
    };
    foreach (long num in objectCollection.Select(paramSet).AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[0]))))
    {
      try
      {
        DataSet tablesInternal = TableLoadHelper.GetTablesInternal(session, num);
        this.ProcessRecordReferences(tablesInternal);
        if (tablesInternal.HasChanges())
        {
          tablesInternal.AcceptChanges();
          TableLoadHelper.StoreData(this._session, num, tablesInternal, (ITablesIndexer) null);
        }
      }
      catch (Exception ex)
      {
        this._convertedInfo.Add(new ObjectInfoForExteption(num, string.Empty, ex.Message));
      }
    }
  }

  private void ConvertInReferences(
    List<long> tableLinkIds,
    List<int> recordRefAttIds,
    int szTable,
    double totalSize)
  {
    int count1 = tableLinkIds.Count;
    int count2 = recordRefAttIds.Count;
    for (int index1 = 0; index1 < count1; ++index1)
    {
      while (this._paused)
      {
        if (this._cancel)
          return;
        Thread.Sleep(1000);
      }
      if (this._cancel)
        break;
      long tableLinkId = tableLinkIds[index1];
      this._completed = (int) ((double) (index1 + szTable) / totalSize * 100.0);
      try
      {
        IDBObject dbObject = this._session.GetObject(tableLinkId);
        for (int index2 = 0; index2 < count2; ++index2)
        {
          int recordRefAttId = recordRefAttIds[index2];
          IDBAttribute byId = dbObject.Attributes.FindByID(recordRefAttId);
          if (byId != null)
          {
            string oldKey = Convert.ToString(byId.Value);
            if (!string.IsNullOrEmpty(oldKey))
            {
              string str = this.ConvertOldKey(this._session, oldKey);
              if (str != oldKey)
                byId.Value = (object) str;
            }
          }
        }
      }
      catch (Exception ex)
      {
        this._convertedInfo.Add(new ObjectInfoForExteption(tableLinkId, string.Empty, ex.Message));
      }
    }
  }

  private void ConvertInTables(List<long> tableIds, double totalSize)
  {
    for (int index = 0; index < tableIds.Count; ++index)
    {
      while (this._paused)
      {
        if (this._cancel)
          return;
        Thread.Sleep(1000);
      }
      if (this._cancel)
        break;
      long tableId = tableIds[index];
      this._completed = (int) ((double) index / totalSize * 100.0);
      try
      {
        DataSet tablesInternal = TableLoadHelper.GetTablesInternal(this._session, tableId);
        this.ProcessRecordReferences(tablesInternal);
        this.ProcessSearchLinks(tablesInternal);
        if (tablesInternal.HasChanges())
        {
          tablesInternal.AcceptChanges();
          TableLoadHelper.StoreData(this._session, tableId, tablesInternal, (ITablesIndexer) null);
        }
      }
      catch (Exception ex)
      {
        this._convertedInfo.Add(new ObjectInfoForExteption(tableId, string.Empty, ex.Message));
      }
    }
  }

  private void ConvertMaterialPropertiesObjectName(IUserSession session)
  {
    if (session != null)
    {
      IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.MaterialPropertiesObjTypeGuid);
      if (objectCollection != null)
      {
        DataTable dataTable = objectCollection.Select(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID
        }));
        if (dataTable != null && dataTable.Rows.Count > 0)
        {
          string empty1 = string.Empty;
          string empty2 = string.Empty;
          Dictionary<long, string> dictionary1 = new Dictionary<long, string>(dataTable.Rows.Count);
          Dictionary<string, string> dictionary2 = new Dictionary<string, string>(dataTable.Rows.Count);
          List<string> keyValues = new List<string>(dataTable.Rows.Count);
          int num = 0;
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            while (this._paused)
            {
              if (this._cancel)
                return;
              Thread.Sleep(1000);
            }
            if (this._cancel)
              return;
            long int64 = Convert.ToInt64(row[0]);
            IDBObject dbObject = session.GetObject(int64, false);
            if (dbObject != null)
            {
              string captionFromBlob = this.GetCaptionFromBlob(session, dbObject);
              if (!string.IsNullOrEmpty(captionFromBlob))
              {
                dbObject.Caption = captionFromBlob;
                this._completed = (int) ((double) num++ / (double) dataTable.Rows.Count * 100.0);
              }
              else
              {
                string caption = dbObject.Caption;
                if (!dictionary2.ContainsKey(caption))
                {
                  string str = this.ConvertOldKey(this._session, dbObject.Caption);
                  dictionary2.Add(caption, str);
                  keyValues.Add(str);
                  dictionary1.Add(int64, str);
                }
                else
                  dictionary1.Add(int64, dictionary2[caption]);
              }
            }
          }
          if (keyValues.Count > 0 && session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService)
          {
            Dictionary<string, string> dictionary3 = customService.NameRecordReferences(session.SessionGUID, keyValues);
            if (dictionary3 != null && dictionary3.Count > 0)
            {
              foreach (KeyValuePair<long, string> keyValuePair in dictionary1)
              {
                while (this._paused)
                {
                  if (this._cancel)
                    return;
                  Thread.Sleep(1000);
                }
                if (this._cancel)
                  return;
                this._completed = (int) ((double) num++ / (double) dataTable.Rows.Count * 100.0);
                if (dictionary3.ContainsKey(keyValuePair.Value))
                  session.GetObject(keyValuePair.Key).Caption = dictionary3[keyValuePair.Value];
              }
            }
          }
        }
      }
    }
    this._completed = 100;
  }

  private static bool FindNewLinkId(
    IUserSession session,
    int catalogKey,
    int catalogRecordKey,
    ref long linkId)
  {
    SortedList<int, long> linksForCatalog = KeyConverterService.GetLinksForCatalog(session);
    if (linksForCatalog == null || !linksForCatalog.ContainsKey(catalogRecordKey))
      return false;
    linkId = linksForCatalog[catalogRecordKey];
    return true;
  }

  private void FixRecordRefsInTable(DataTable recsTable, List<string> columns)
  {
    DataRowCollection rows = recsTable.Rows;
    int count1 = rows.Count;
    int count2 = columns.Count;
    for (int index1 = 0; index1 < count1; ++index1)
    {
      DataRow dataRow = rows[index1];
      for (int index2 = 0; index2 < count2; ++index2)
      {
        string oldKey = Convert.ToString(dataRow[columns[index2]]);
        if (!string.IsNullOrEmpty(oldKey))
        {
          string str = this.ConvertOldKey(this._session, oldKey);
          if (!(str == oldKey))
            dataRow[columns[index2]] = (object) str;
        }
      }
    }
  }

  private string GetCaptionFromBlob(IUserSession session, IDBObject obj)
  {
    string captionFromBlob = string.Empty;
    if (obj != null)
    {
      IDBAttribute attributeByGuid = obj.GetAttributeByGuid(new Guid("cadd93d3-306c-11d8-b4e9-00304f19f545"));
      if (attributeByGuid != null)
      {
        XmlDocument xmlDocument = new XmlDocument();
        string str = this.ReadBlob(attributeByGuid);
        if (!string.IsNullOrEmpty(str))
        {
          xmlDocument.InnerXml = str;
          XmlNode xmlNode = xmlDocument.SelectSingleNode("doc/description");
          if (xmlNode != null)
          {
            XmlAttribute attribute = xmlNode.Attributes["name"];
            captionFromBlob = attribute != null ? attribute.Value : string.Empty;
          }
        }
      }
    }
    return captionFromBlob;
  }

  private static SortedList<int, long> GetLinksForCatalog(IUserSession session)
  {
    if (KeyConverterService.ReferencesList == null)
    {
      try
      {
        Monitor.Enter(KeyConverterService.LinksLockObject);
        DataTable dataTable = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID, RelationalOperators.Greater, (object) 0, LogicalOperators.NONE, 0, false)
        }, new object[2]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID
        })
        {
          SortColumns = new object[1]
          {
            (object) Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID
          },
          Orders = new SortOrders[1]{ SortOrders.ASC }
        });
        SortedList<int, long> sortedList = new SortedList<int, long>(dataTable.Rows.Count);
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          sortedList[Convert.ToInt32(row[1])] = Convert.ToInt64(row[0]);
        KeyConverterService.ReferencesList = sortedList;
      }
      finally
      {
        Monitor.Exit(KeyConverterService.LinksLockObject);
      }
    }
    return KeyConverterService.ReferencesList;
  }

  private void GetObjectWithRefAttributes(
    IDBObjectCollection tableRefObjects,
    List<long> tableLinkIds,
    List<int> recordRefAttIds)
  {
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      new ConditionStructure(-1, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, false)
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(conditions, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    paramSet.TableName = "tbl";
    int count1 = recordRefAttIds.Count;
    for (int index1 = 0; index1 < count1; ++index1)
    {
      conditions[0].Attribute = (object) recordRefAttIds[index1];
      DataRowCollection rows = tableRefObjects.Select(paramSet).Rows;
      int count2 = rows.Count;
      for (int index2 = 0; index2 < count2; ++index2)
      {
        long int64 = Convert.ToInt64(rows[index2][0]);
        if (!tableLinkIds.Contains(int64))
          tableLinkIds.Add(int64);
      }
    }
  }

  private List<string> GetRecordRefColumns(DataTable attTable)
  {
    List<string> recordRefColumns = new List<string>(32 /*0x20*/);
    DataRowCollection rows = attTable.Rows;
    for (int index = 0; index < rows.Count; ++index)
    {
      DataRow dataRow = rows[index];
      if (dataRow.RowState != DataRowState.Deleted)
      {
        string g = Convert.ToString(dataRow["F_ATTRIBUTE_GUID"]);
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(g));
        if (attributeType != null)
        {
          if ((attributeType.Options & AttributeOptions.ImbaseFlag_TableRecordRef) == AttributeOptions.ImbaseFlag_TableRecordRef)
            recordRefColumns.Add(g);
          else if ((Convert.ToInt32(dataRow["F_OPTIONS"]) & 131072 /*0x020000*/) != 0)
            recordRefColumns.Add(g);
        }
      }
    }
    return recordRefColumns;
  }

  private void GetRecordReferenceAttributes(IUserSession iSession, List<int> recordRefAttIds)
  {
    UserSession userSession = iSession as UserSession;
    IDBObjectType objectType = userSession.GetObjectType(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
    if (!objectType.AnyAttributes)
    {
      IDBAttribute4TypeCollection attributes = objectType.Attributes;
    }
    DataTable table = userSession.DBCache.GetTable("IMS_ATTRIBUTES");
    userSession.DBCache.EnterReadLocker();
    try
    {
      DataColumn column = table.Columns["F_OPTIONS"];
      DataRowCollection rows = table.Rows;
      int num = 131072 /*0x020000*/;
      int count = rows.Count;
      for (int index = 0; index < count; ++index)
      {
        DataRow dataRow = rows[index];
        if ((Convert.ToInt32(dataRow[column]) & num) != 0)
        {
          int int32 = Convert.ToInt32(dataRow[0]);
          recordRefAttIds.Add(int32);
        }
      }
    }
    finally
    {
      userSession.DBCache.ExitReadLocker();
    }
    IDBAttributeType attributeType = userSession.GetAttributeType(new Guid("cae0cf41-b150-4211-995e-cc73f44b7152"), false);
    if (attributeType == null)
      return;
    int attributeId = attributeType.AttributeID;
    if (recordRefAttIds.Contains(attributeId))
      return;
    attributeType.Options |= AttributeOptions.ImbaseFlag_TableRecordRef;
    recordRefAttIds.Add(attributeId);
  }

  private void OnTaskTerminated(IAsyncResult ar)
  {
    this._asyncResult = (IAsyncResult) null;
    this._terminated = true;
  }

  private void ProcessRecordReferences(DataSet tableData)
  {
    if (tableData == null)
      return;
    List<string> recordRefColumns = this.GetRecordRefColumns(tableData.Tables["IMS_ATTR_TYPES"]);
    if (recordRefColumns == null || recordRefColumns.Count == 0)
      return;
    this.FixRecordRefsInTable(tableData.Tables["IMS_DATA"], recordRefColumns);
  }

  private void ProcessSearchLinks(DataSet tableData)
  {
    if (tableData == null)
      return;
    DataTable table1 = tableData.Tables["IMS_ATTR_TYPES"];
    DataTable table2 = tableData.Tables["IMS_DATA"];
    foreach (DataRow row1 in (InternalDataCollectionBase) table1.Rows)
    {
      bool flag1 = false;
      bool flag2 = false;
      int int32 = Convert.ToInt32(row1["F_OPTIONS"]);
      if ((int32 & 1024 /*0x0400*/) != 0)
        flag2 = true;
      if ((int32 & 2048 /*0x0800*/) != 0)
        flag1 = true;
      if (flag2 || flag1)
      {
        string columnName = Convert.ToString(row1["F_ATTRIBUTE_GUID"]);
        if (table2.Columns.IndexOf(columnName) != -1)
        {
          foreach (DataRow row2 in (InternalDataCollectionBase) table2.Rows)
          {
            object obj1 = row2[columnName];
            if (obj1 != null && !DBNull.Value.Equals(obj1))
            {
              string sourceValue = obj1.ToString();
              if (!string.IsNullOrWhiteSpace(sourceValue))
              {
                object obj2 = (object) null;
                if (flag1)
                  obj2 = SearchLinksHelper.GetSearchDocumentLinkValue(this._session, (object) sourceValue);
                if (flag2)
                  obj2 = SearchLinksHelper.GetSearchObjectLinkValue(this._session, (object) sourceValue);
                if (!sourceValue.Equals(obj2))
                  row2[columnName] = obj2;
              }
            }
          }
        }
      }
    }
  }

  private string ReadBlob(IDBAttribute attr)
  {
    string str = string.Empty;
    if (attr != null)
    {
      try
      {
        if (attr is IBlobReader blobReader)
        {
          BlobInformation blobInformation = blobReader.OpenBlob(0);
          if (blobInformation.RealFileSize != 0L)
          {
            byte[] buffer = blobReader.ReadDataBlock();
            blobReader.CloseBlob();
            if (buffer != null)
            {
              if (buffer.Length != 0)
              {
                using (MemoryStream inStream = new MemoryStream(buffer))
                {
                  inStream.Position = 0L;
                  using (MemoryStream memoryStream = new MemoryStream((int) blobInformation.RealFileSize))
                  {
                    ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) memoryStream, (Stream) inStream);
                    memoryStream.Position = 0L;
                    using (BinaryReader binaryReader = new BinaryReader((Stream) memoryStream, Encoding.UTF8))
                      str = binaryReader.ReadString();
                  }
                }
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        return ex.Message;
      }
    }
    return str;
  }

  private void ScanProcess(object sender)
  {
    DataRowCollection rows = this._session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableTypeID).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_ID)
    })
    {
      ColumnNames = new ColumnNameMapping[1]
      {
        ColumnNameMapping.ID
      },
      TableName = "f",
      FailIfNotFound = false
    }).Rows;
    int count = rows.Count;
    List<long> tableIds = new List<long>(count);
    for (int index = 0; index < count; ++index)
      tableIds.Add(Convert.ToInt64(rows[index][0]));
    List<int> recordRefAttIds = new List<int>(64 /*0x40*/);
    List<long> tableLinkIds = new List<long>(8192 /*0x2000*/);
    this.GetRecordReferenceAttributes(this._session, recordRefAttIds);
    if (recordRefAttIds.Count > 0)
      this.GetObjectWithRefAttributes(this._session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID), tableLinkIds, recordRefAttIds);
    double totalSize = (double) (count + tableLinkIds.Count);
    if (this._cancel)
      return;
    this.ConvertInTables(tableIds, totalSize);
    this.ConvertInReferences(tableLinkIds, recordRefAttIds, count, totalSize);
    this._paused = this._cancel = this._terminated = false;
    this._completed = 0;
    this._isFirstTaskComplete = true;
    this.ConvertMaterialPropertiesObjectName(this._session);
    this.ConvertInReceptures(this._session);
  }

  private delegate void ScanTablesHandler(object sender);
}
