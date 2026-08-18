// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.InverseImbaseSynchObjectsService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

#nullable disable
namespace Intermech.Imbase.Server;

internal class InverseImbaseSynchObjectsService : LongLifeObject, IInverseImbaseSynchObjectsService
{
  private Dictionary<Guid, InverseImbaseSynchObjectsService.ProcessedInfo> _tasks = new Dictionary<Guid, InverseImbaseSynchObjectsService.ProcessedInfo>();

  public DataTable GetInfoAboutObjectsProcessed(Guid taskGuid, out int count, out int current)
  {
    DataTable objectsProcessed = (DataTable) null;
    count = current = 0;
    if (this._tasks.ContainsKey(taskGuid))
    {
      InverseImbaseSynchObjectsService.ProcessedInfo task = this._tasks[taskGuid];
      count = task.Count;
      current = task.Current;
      objectsProcessed = task.ProcessedDataCopy();
    }
    return objectsProcessed;
  }

  public void StopTask(Guid taskGuid)
  {
    if (!this._tasks.ContainsKey(taskGuid))
      return;
    this._tasks[taskGuid].TaskRunning = false;
  }

  public void UpdateInfo(Guid sessionGuid, Guid taskGuid, List<long> objIDs, List<int> attrIDs)
  {
    if (objIDs == null || objIDs.Count <= 0 || attrIDs == null || attrIDs.Count <= 0)
      return;
    attrIDs.Sort();
    InverseImbaseSynchObjectsService.Helper helper = new InverseImbaseSynchObjectsService.Helper(sessionGuid)
    {
      AttributeIDs = attrIDs
    };
    helper.LoadTask(this._tasks, taskGuid);
    try
    {
      Dictionary<int, List<long>> source = this.GroupObjectsByType(helper, objIDs);
      List<int> list = source.Where<KeyValuePair<int, List<long>>>((System.Func<KeyValuePair<int, List<long>>, bool>) (x => !ImbaseHelper.CanObjectTypeContainAttribute(x.Key, Intermech.Imbase.Consts.ImbaseObjectRefAttID))).Select<KeyValuePair<int, List<long>>, int>((System.Func<KeyValuePair<int, List<long>>, int>) (x => x.Key)).ToList<int>();
      DataTable dataTable = (DataTable) null;
      foreach (KeyValuePair<int, List<long>> keyValuePair in source)
      {
        int key = keyValuePair.Key;
        List<long> objIDs1 = keyValuePair.Value;
        try
        {
          if (list.Contains(key))
          {
            objIDs1.ForEach((Action<long>) (x => objIDs.Remove(x)));
            throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_ObjType_ImbaseObjRef_CantContainsAttr"));
          }
          DataTable dataObjects = this.GetDataObjects(helper.Session, key, objIDs1);
          if (dataTable == null)
            dataTable = dataObjects;
          else
            dataTable.MergeEx(dataObjects);
        }
        catch (ApplicationException ex)
        {
          helper.Task.AddNotSynchType(key, ex.Message);
        }
      }
      if (dataTable == null || dataTable.Rows.Count <= 0)
        return;
      helper.Task.Count = dataTable.Rows.Count;
      this.ProcessObjects(helper, dataTable);
    }
    catch (OperationCanceledException ex)
    {
    }
    finally
    {
      helper.Task.TaskRunning = false;
      helper.Task.FinishedTime = DateTime.Now;
    }
  }

  private Dictionary<int, List<long>> GroupObjectsByType(
    InverseImbaseSynchObjectsService.Helper helper,
    List<long> objIDs)
  {
    Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>();
    foreach (long objId in objIDs)
    {
      int objectTypeId = helper.Session.GetObjectInfo(objId).ObjectTypeID;
      if (dictionary.ContainsKey(objectTypeId))
        dictionary[objectTypeId].Add(objId);
      else
        dictionary.Add(objectTypeId, new List<long>()
        {
          objId
        });
    }
    return dictionary;
  }

  public void UpdateInfo(Guid sessionGuid, Guid taskGuid, int objTypeID, List<int> attrIDs)
  {
    if (objTypeID == -1 || attrIDs == null || attrIDs.Count <= 0)
      return;
    attrIDs.Sort();
    InverseImbaseSynchObjectsService.Helper helper = new InverseImbaseSynchObjectsService.Helper(sessionGuid)
    {
      AttributeIDs = attrIDs
    };
    helper.LoadTask(this._tasks, taskGuid);
    try
    {
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(objTypeID);
      List<int> list = childrenIdRecursive.Where<int>((System.Func<int, bool>) (x => !ImbaseHelper.CanObjectTypeContainAttribute(x, Intermech.Imbase.Consts.ImbaseObjectRefAttID))).ToList<int>();
      DataTable dataTable = (DataTable) null;
      foreach (int num in childrenIdRecursive)
      {
        try
        {
          if (list.Contains(num))
            throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_ObjType_ImbaseObjRef_CantContainsAttr"));
          DataTable dataObjects = this.GetDataObjects(helper.Session, num);
          if (dataTable == null)
            dataTable = dataObjects;
          else
            dataTable.MergeEx(dataObjects);
        }
        catch (ApplicationException ex)
        {
          helper.Task.AddNotSynchType(num, ex.Message);
        }
      }
      if (dataTable == null || dataTable.Rows.Count <= 0)
        return;
      helper.Task.Count = dataTable.Rows.Count;
      this.ProcessObjects(helper, dataTable);
    }
    catch (OperationCanceledException ex)
    {
    }
    finally
    {
      helper.Task.TaskRunning = false;
      helper.Task.FinishedTime = DateTime.Now;
    }
  }

  private void ProcessObjects(InverseImbaseSynchObjectsService.Helper helper, DataTable objsData)
  {
    Dictionary<long, long> objWithIMBASE = new Dictionary<long, long>();
    Dictionary<long, List<Tuple<long, long>>> objWithLink = new Dictionary<long, List<Tuple<long, long>>>();
    string message = LocalizationHolder.rm.GetString("Imbase_Obj_NotBindWithImbase");
    long result1 = 0;
    long result2 = -1;
    foreach (DataRow row in (InternalDataCollectionBase) objsData.Rows)
    {
      if (!helper.Task.TaskRunning)
        throw new OperationCanceledException();
      long int64 = Convert.ToInt64(row[SynchStrHelper.COLUMN_NAME_OBJECT_ID]);
      try
      {
        if (!long.TryParse(Convert.ToString(row[SynchStrHelper.COLUMN_NAME_IMBASE_OBJECT_REF]), out result1) || result1 == 0L)
          throw new ApplicationException(message);
        QuickObjectInfo objectInfo = helper.Session.GetObjectInfo(result1);
        if (objectInfo.ObjectTypeID == Intermech.Imbase.Consts.ImbaseFolderTypeID || objectInfo.ObjectTypeID == Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID)
        {
          if (!objWithIMBASE.ContainsKey(int64))
            objWithIMBASE.Add(int64, result1);
        }
        else
        {
          if (objectInfo.ObjectTypeID != Intermech.Imbase.Consts.ImbaseTableRefTypeID)
            throw new ApplicationException("Объект может ссылаться только на папку, запись каталога или запись таблицы IMBASE");
          if (!long.TryParse(Convert.ToString(row[SynchStrHelper.COLUMN_NAME_RECORD_ID]), out result2) || result2 < 0L)
            throw new ApplicationException(message);
          if (objWithLink.ContainsKey(result1))
            objWithLink[result1].Add(Tuple.Create<long, long>(int64, result2));
          else
            objWithLink.Add(result1, new List<Tuple<long, long>>(1)
            {
              Tuple.Create<long, long>(int64, result2)
            });
        }
      }
      catch (ApplicationException ex)
      {
        helper.Task.AddProcessedObject(int64, helper.Session.GetObjectInfo(int64).Caption, 0L, string.Empty, SynchStrHelper.NotSynchronized, ex.Message);
        ++helper.Task.Current;
      }
    }
    if (objWithIMBASE.Count > 0)
      this.SynchObjWithIMBASE(helper, objWithIMBASE);
    if (objWithLink.Count <= 0)
      return;
    if (!(ServerServices.GetService(typeof (IImbaseIndexingService)) is IImbaseIndexingService service))
      throw new Exception(LocalizationHolder.rm.GetString("Imbase_ImpossibleCheckUniqueIndexes"));
    this.SynchObjectWithLink(helper, objWithLink, service);
  }

  private void SynchObjWithIMBASE(
    InverseImbaseSynchObjectsService.Helper helper,
    Dictionary<long, long> objWithIMBASE)
  {
    foreach (KeyValuePair<long, long> keyValuePair in objWithIMBASE)
    {
      if (!helper.Task.TaskRunning)
        throw new OperationCanceledException();
      long key = keyValuePair.Key;
      long num = keyValuePair.Value;
      try
      {
        AttributeValues[] objectAttributes1 = this.GetObjectAttributes(helper.Session, key);
        AttributeValues[] objectAttributes2 = this.GetImbaseObjectAttributes(helper.Session, num);
        List<int> intList = new List<int>(helper.AttributeIDs.Count);
        List<AttributeValues> attributeValuesList1 = new List<AttributeValues>(helper.AttributeIDs.Count);
        List<AttributeValues> attributeValuesList2 = new List<AttributeValues>(helper.AttributeIDs.Count);
        List<AttributeValues> attrForUpdate = new List<AttributeValues>(helper.AttributeIDs.Count);
        foreach (int attributeId in helper.AttributeIDs)
        {
          int attrID = attributeId;
          AttributeValues av = ((IEnumerable<AttributeValues>) objectAttributes1).FirstOrDefault<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == attrID));
          if (av == null)
            intList.Add(attrID);
          else if (this.IsAttributeValueEmpty(av))
          {
            attributeValuesList1.Add(av);
          }
          else
          {
            AttributeValues attributeValues = ((IEnumerable<AttributeValues>) objectAttributes2).FirstOrDefault<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == attrID));
            if (attributeValues != null && AttributeValues.ValuesEquals(av.Values, attributeValues.Values))
              attributeValuesList2.Add(av);
            else
              attrForUpdate.Add(av);
          }
        }
        string notNeedToSync = SynchStrHelper.NotNeedToSync;
        StringBuilder sb = new StringBuilder($"{LocalizationHolder.rm.GetString("Imbase_Synch_Result")}:");
        sb.AppendLine("");
        if (intList.Count > 0)
        {
          sb.AppendLine("");
          sb.AppendLine("У синхронизируемого объекта отсутствую атрибуты:");
          intList.ForEach((Action<int>) (x => sb.AppendLine($"'{MetaDataHelper.GetAttributeTypeName(x)}' (ID = {x.ToString()})")));
        }
        if (attributeValuesList1.Count > 0)
        {
          sb.AppendLine("");
          sb.AppendLine("У синхронизируемого объекта не заполнены атрибуты:");
          attributeValuesList1.ForEach((Action<AttributeValues>) (x => sb.AppendLine($"'{x.AttributeName}' (ID = {x.AttributeID.ToString()})")));
        }
        if (attributeValuesList2.Count > 0)
        {
          sb.AppendLine("");
          sb.AppendLine("Атрибуты, не нуждающиеся в синхронизации:");
          attributeValuesList2.ForEach((Action<AttributeValues>) (x => sb.AppendLine($"'{x.AttributeName}' (ID = {x.AttributeID.ToString()})")));
        }
        if (attrForUpdate.Count > 0)
        {
          string str = this.Save(helper.Session, num, objectAttributes2, attrForUpdate, ref notNeedToSync);
          sb.Append(str);
        }
        helper.Task.AddProcessedObject(key, helper.Session.GetObjectInfo(key).Caption, num, helper.Session.GetObjectInfo(num).Caption, notNeedToSync, sb.ToString());
      }
      catch (ApplicationException ex)
      {
        helper.Task.AddProcessedObject(key, helper.Session.GetObjectInfo(key).Caption, num, helper.Session.GetObjectInfo(num).Caption, SynchStrHelper.NotSynchronized, ex.Message);
      }
      ++helper.Task.Current;
    }
  }

  private AttributeValues[] GetImbaseObjectAttributes(IUserSession session, long imbaseObjID)
  {
    IDBObject dbObject = session.GetObjectActualCopy(imbaseObjID, false);
    if (dbObject == null)
      throw new ApplicationException("Не удалось получить объект IMBASE");
    if (dbObject.ObjectModifyMode == ObjectModifyModes.CreateVersion)
      throw new ApplicationException("Объект IMBASE модифицируется через выпуск версии");
    if (dbObject.ObjectModifyMode == ObjectModifyModes.CantModify)
      throw new ApplicationException("Объект IMBASE находится на шаге жизненного цикла, который запрещает его модификацию");
    if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
    {
      if (dbObject.CheckoutBy == 0L)
      {
        try
        {
          dbObject = dbObject.CheckOut();
        }
        catch (Exception ex)
        {
          throw new ApplicationException($"При взятии на редактирование объекта IMBASE, произошла ошибка\r\n{ex.InnerException}");
        }
      }
      else if (dbObject.CheckoutBy != session.UserID)
        throw new ApplicationException("Объект IMBASE взят на редактирование другим пользователем");
    }
    return dbObject.GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGuid);
  }

  private bool IsAttributeValueEmpty(AttributeValues av)
  {
    bool flag = true;
    if (av.Values != null && av.Values.Length != 0)
    {
      foreach (object obj in av.Values)
      {
        if (!string.IsNullOrEmpty(Convert.ToString(obj).Trim()))
        {
          flag = false;
          break;
        }
      }
    }
    return flag;
  }

  private string Save(
    IUserSession session,
    long imbaseObjID,
    AttributeValues[] imbaseObjAttributes,
    List<AttributeValues> attrForUpdate,
    ref string status)
  {
    StringBuilder sb = new StringBuilder();
    Dictionary<string, Exception> dictEx = new Dictionary<string, Exception>();
    IDBObject objectActualCopy = session.GetObjectActualCopy(imbaseObjID, false);
    try
    {
      attrForUpdate.ForEach((Action<AttributeValues>) (x => x.ReadOnly = false));
      AttributeValues[] source = (objectActualCopy as DBObject).SetAttributesValues(attrForUpdate.ToArray(), false, true, true, GetAttributeValuesModes.IncludeName, dictEx);
      List<AttributeValues> list = attrForUpdate.Where<AttributeValues>((System.Func<AttributeValues, bool>) (x => !dictEx.ContainsKey(x.AttributeName))).ToList<AttributeValues>();
      if (list.Count > 0)
      {
        sb.AppendLine("");
        sb.AppendLine($"{LocalizationHolder.rm.GetString("Imbase_Obj_SaveAttrs_Success")}:");
        sb.AppendLine(this.GetString(imbaseObjAttributes, list));
        status = SynchStrHelper.Synchronized;
      }
      if (dictEx.Count > 0)
      {
        sb.AppendLine("");
        sb.AppendLine($"{LocalizationHolder.rm.GetString("Imbase_Obj_SaveAttrs_ErrorList")}:");
        foreach (KeyValuePair<string, Exception> keyValuePair in dictEx)
          sb.AppendLine($"'{keyValuePair.Key}' (ID = {MetaDataHelper.GetAttributeByTypeNameID(keyValuePair.Key).ToString()}): {keyValuePair.Value.Message}");
        if (status != SynchStrHelper.Synchronized)
          status = SynchStrHelper.NotSynchronized;
      }
      if (source != null)
      {
        if (source.Length != 0)
        {
          sb.AppendLine("");
          sb.AppendLine($"{LocalizationHolder.rm.GetString("Imbase_Obj_SaveAttrs_ChangedByServer")}:");
          sb.AppendLine(this.GetString(imbaseObjAttributes, ((IEnumerable<AttributeValues>) source).ToList<AttributeValues>()));
          status = SynchStrHelper.Synchronized;
        }
      }
    }
    catch (Exception ex)
    {
      sb.AppendLine("");
      sb.AppendLine($"{LocalizationHolder.rm.GetString("Imbase_Obj_SaveAttrs_Error")}:");
      attrForUpdate.ForEach((Action<AttributeValues>) (x => sb.AppendLine($"'{x.AttributeName}' (ID = {x.AttributeID.ToString()})")));
      sb.AppendLine("");
      sb.AppendLine(ex.Message);
      status = SynchStrHelper.NotSynchronized;
    }
    return sb.ToString();
  }

  private string GetString(AttributeValues[] oldAVs, List<AttributeValues> savedAVs)
  {
    StringBuilder stringBuilder = new StringBuilder();
    string format1 = LocalizationHolder.rm.GetString("Imbase_Attr_OldValue_NewValue");
    string format2 = LocalizationHolder.rm.GetString("Imbase_Attr_NewValue");
    string format3 = LocalizationHolder.rm.GetString("Imbase_Attr_OldValues_NewValues");
    string format4 = LocalizationHolder.rm.GetString("Imbase_Attr_NewValues");
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    foreach (AttributeValues savedAv in savedAVs)
    {
      AttributeValues av = savedAv;
      AttributeValues attributeValues = ((IEnumerable<AttributeValues>) oldAVs).FirstOrDefault<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == av.AttributeID));
      if (av.MultipleValued == MultiValueModes.SingleValue || av.MultipleValued == MultiValueModes.SingleValueFromList)
      {
        if (attributeValues != null)
          stringBuilder.AppendLine(string.Format(format1, (object) av.AttributeName, (object) av.AttributeID.ToString(), (object) Convert.ToString(attributeValues.Values[0]), (object) Convert.ToString(av.Values[0])));
        else
          stringBuilder.AppendLine(string.Format(format2, (object) av.AttributeName, (object) av.AttributeID.ToString(), (object) Convert.ToString(av.Values[0])));
      }
      else
      {
        foreach (object obj in av.Values)
          empty2 += $"{Convert.ToString(obj)}; ";
        if (attributeValues != null)
        {
          foreach (object obj in attributeValues.Values)
            empty1 += $"{Convert.ToString(obj)}; ";
          stringBuilder.AppendLine(string.Format(format3, (object) av.AttributeName, (object) av.AttributeID.ToString(), (object) empty1, (object) empty2));
        }
        else
          stringBuilder.AppendLine(string.Format(format4, (object) av.AttributeName, (object) av.AttributeID.ToString(), (object) empty2));
      }
    }
    return stringBuilder.ToString();
  }

  private void SynchObjectWithLink(
    InverseImbaseSynchObjectsService.Helper helper,
    Dictionary<long, List<Tuple<long, long>>> objWithLink,
    IImbaseIndexingService iIis)
  {
    if (!helper.Task.TaskRunning)
      throw new OperationCanceledException();
    DataTable allLinks = this.GetAllLinks(helper, objWithLink);
    if (allLinks == null)
      return;
    if (!helper.Task.TaskRunning)
      throw new OperationCanceledException();
    Dictionary<long, List<InverseImbaseSynchObjectsService.CatalogInfo>> dictionary = this.GroupDataFromTable(helper, objWithLink, allLinks);
    if (dictionary == null)
      return;
    foreach (KeyValuePair<long, List<InverseImbaseSynchObjectsService.CatalogInfo>> keyValuePair in dictionary)
    {
      if (!helper.Task.TaskRunning)
        throw new OperationCanceledException();
      List<Tuple<long, long, long>> objectsForTable = this.GetObjectsForTable(objWithLink, keyValuePair.Value);
      if (objectsForTable != null)
      {
        try
        {
          this.ProcessTable(helper, keyValuePair.Key, objectsForTable, keyValuePair.Value, iIis);
        }
        catch (ApplicationException ex)
        {
          foreach (InverseImbaseSynchObjectsService.CatalogInfo catalogInfo in keyValuePair.Value)
            catalogInfo.LinkIDs.ForEach((Action<long>) (x => this.RemoveLinkData(helper, x, objWithLink, ex.Message)));
        }
      }
    }
  }

  private DataTable GetTablesData(IUserSession session, List<long> linkIDs)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
    if (objectCollection == null)
      throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_ObjectCollection_TableRef_Error"));
    objectCollection.ObjectTypeID = Intermech.Imbase.Consts.ImbaseTableRefTypeID;
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) linkIDs.ToArray(), (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID)
    }, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Intermech.Imbase.Consts.ImbaseTableRefAttID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0)
    });
    DataTable tablesData = objectCollection.Select(paramSet);
    if (tablesData == null || tablesData.Rows.Count == 0)
      throw new ApplicationException("Не удалось получить информацию о ярлыках IMBASE, на которые ссылаются выделенные объекты");
    return tablesData;
  }

  private DataTable GetLinksData(IUserSession session, List<long> tableIDs)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
    if (objectCollection == null)
      throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_ObjectCollection_TableRef_Error"));
    objectCollection.ObjectTypeID = Intermech.Imbase.Consts.ImbaseTableRefTypeID;
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ImbaseTableRefAttID, RelationalOperators.In, (object) tableIDs.ToArray(), (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID)
    }, new ColumnDescriptor[3]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Intermech.Imbase.Consts.ImbaseTableRefAttID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0)
    });
    DataTable linksData = objectCollection.Select(paramSet);
    if (linksData == null || linksData.Rows.Count == 0)
      throw new ApplicationException("Не удалось получить информацию о ярлыках IMBASE, на которые ссылаются выделенные объекты");
    return linksData;
  }

  private Dictionary<string, long> GetCatalogs(IUserSession session, List<string> classifKeys)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseCatalogTypeID);
    if (objectCollection == null)
      throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_ObjectCollection_Catalog_Error"));
    objectCollection.ObjectTypeID = Intermech.Imbase.Consts.ImbaseCatalogTypeID;
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.In, (object) classifKeys.ToArray(), LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0)
    });
    DataTable source = objectCollection.Select(paramSet);
    if (source == null || source.Rows.Count == 0)
      throw new ApplicationException("Не удалось получить информацию о каталогах IMBASE");
    return source.AsEnumerable().Select<DataRow, DataRow>((System.Func<DataRow, DataRow>) (x => x)).ToDictionary<DataRow, string, long>((System.Func<DataRow, string>) (x => Convert.ToString(x[SynchStrHelper.COLUMN_NAME_CLASSIF_KEY])), (System.Func<DataRow, long>) (x => Convert.ToInt64(x[SynchStrHelper.COLUMN_NAME_OBJECT_ID])));
  }

  private Dictionary<long, InverseImbaseSynchObjectsService.CatalogIndexes> GetIndexes(
    IUserSession session,
    List<long> catalogIDs)
  {
    Dictionary<long, InverseImbaseSynchObjectsService.CatalogIndexes> indexes1 = new Dictionary<long, InverseImbaseSynchObjectsService.CatalogIndexes>();
    Guid sessionGuid = session.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService ? session.SessionGUID : throw new ApplicationException("Не удалось получить сервис индексирования");
    List<long> catalogIDs1 = catalogIDs;
    string[] colsNames = new string[3]
    {
      IndexesField.F_CATALOG_ID,
      IndexesField.F_ATTRIBUTE_ID,
      IndexesField.F_FLAG
    };
    DataTable indexes2 = customService.GetIndexes(sessionGuid, catalogIDs1, colsNames);
    if (indexes2 != null)
    {
      Lookup<long, DataRow> lookup = (Lookup<long, DataRow>) indexes2.AsEnumerable().ToLookup<DataRow, long, DataRow>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[IndexesField.F_CATALOG_ID])), (System.Func<DataRow, DataRow>) (x => x));
      int num = 17;
      foreach (IGrouping<long, DataRow> source in lookup)
      {
        List<int> intList1 = new List<int>(source.Count<DataRow>());
        List<int> intList2 = new List<int>(source.Count<DataRow>());
        foreach (DataRow dataRow in (IEnumerable<DataRow>) source)
        {
          int int32 = Convert.ToInt32(dataRow[IndexesField.F_ATTRIBUTE_ID]);
          intList2.Add(int32);
          if (Convert.ToInt32(dataRow[IndexesField.F_FLAG]) == num)
            intList1.Add(int32);
        }
        indexes1.Add(source.Key, new InverseImbaseSynchObjectsService.CatalogIndexes()
        {
          Indexes = intList2,
          UniqueIndexes = intList1
        });
      }
    }
    return indexes1;
  }

  private DataTable GetAllLinks(
    InverseImbaseSynchObjectsService.Helper helper,
    Dictionary<long, List<Tuple<long, long>>> objWithLink)
  {
    DataTable allLinks = (DataTable) null;
    List<long> list1 = objWithLink.Keys.ToList<long>();
    DataTable tablesData = this.GetTablesData(helper.Session, list1);
    List<long> list2 = tablesData.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[SynchStrHelper.COLUMN_NAME_OBJECT_ID]))).ToList<long>();
    list2.Sort();
    string msg1 = "Не удалось получить данные ярлыка IMBASE";
    foreach (long linkID in list1)
    {
      if (list2.BinarySearch(linkID) <= -1)
        this.RemoveLinkData(helper, linkID, objWithLink, msg1);
    }
    List<long> source = new List<long>(tablesData.Rows.Count);
    string msg2 = "Ярлык не ссылается на таблицу IMBASE";
    foreach (DataRow row in (InternalDataCollectionBase) tablesData.Rows)
    {
      long result = 0;
      if (!long.TryParse(Convert.ToString(row[SynchStrHelper.COLUMN_NAME_IMBASE_TABLE_REF]), out result) || result == 0L)
        this.RemoveLinkData(helper, Convert.ToInt64(row[SynchStrHelper.COLUMN_NAME_OBJECT_ID]), objWithLink, msg2);
      else
        source.Add(result);
    }
    if (source.Count > 0)
    {
      List<long> list3 = source.Distinct<long>().ToList<long>();
      allLinks = this.GetLinksData(helper.Session, list3);
    }
    return allLinks;
  }

  private Dictionary<long, List<InverseImbaseSynchObjectsService.CatalogInfo>> GroupDataFromTable(
    InverseImbaseSynchObjectsService.Helper helper,
    Dictionary<long, List<Tuple<long, long>>> objWithLink,
    DataTable dtAllLinks)
  {
    Dictionary<long, List<InverseImbaseSynchObjectsService.CatalogInfo>> dictionary = new Dictionary<long, List<InverseImbaseSynchObjectsService.CatalogInfo>>();
    Dictionary<long, Dictionary<string, List<long>>> source = this.GroupLinkIDsByTableIDs(helper, objWithLink, dtAllLinks);
    if (source != null)
    {
      List<string> list1 = source.SelectMany<KeyValuePair<long, Dictionary<string, List<long>>>, string>((System.Func<KeyValuePair<long, Dictionary<string, List<long>>>, IEnumerable<string>>) (x => x.Value.Select<KeyValuePair<string, List<long>>, string>((System.Func<KeyValuePair<string, List<long>>, string>) (y => y.Key)))).Distinct<string>().ToList<string>();
      Dictionary<string, long> catalogIDs = this.GetCatalogs(helper.Session, list1);
      List<string> list2 = list1.Where<string>((System.Func<string, bool>) (x => !catalogIDs.ContainsKey(x))).ToList<string>();
      Dictionary<long, InverseImbaseSynchObjectsService.CatalogIndexes> indexes = this.GetIndexes(helper.Session, catalogIDs.Values.ToList<long>());
      string msg = "Не удалось получить информацию о каталоге, которому принадлежит ярлык";
      foreach (KeyValuePair<long, Dictionary<string, List<long>>> keyValuePair1 in source)
      {
        List<InverseImbaseSynchObjectsService.CatalogInfo> catalogInfoList = new List<InverseImbaseSynchObjectsService.CatalogInfo>();
        foreach (KeyValuePair<string, List<long>> keyValuePair2 in keyValuePair1.Value)
        {
          if (list2.Contains(keyValuePair2.Key))
          {
            keyValuePair2.Value.ForEach((Action<long>) (x => this.RemoveLinkData(helper, x, objWithLink, msg)));
          }
          else
          {
            long key = catalogIDs[keyValuePair2.Key];
            InverseImbaseSynchObjectsService.CatalogInfo catalogInfo = new InverseImbaseSynchObjectsService.CatalogInfo()
            {
              CatalogID = key,
              LinkIDs = keyValuePair2.Value
            };
            catalogInfo.Indexes = indexes.ContainsKey(key) ? indexes[key] : (InverseImbaseSynchObjectsService.CatalogIndexes) null;
            catalogInfoList.Add(catalogInfo);
          }
        }
        if (catalogInfoList.Count != 0)
          dictionary.Add(keyValuePair1.Key, catalogInfoList);
      }
    }
    return dictionary.Count <= 0 ? (Dictionary<long, List<InverseImbaseSynchObjectsService.CatalogInfo>>) null : dictionary;
  }

  private Dictionary<long, Dictionary<string, List<long>>> GroupLinkIDsByTableIDs(
    InverseImbaseSynchObjectsService.Helper helper,
    Dictionary<long, List<Tuple<long, long>>> objWithLink,
    DataTable dtAllLinks)
  {
    Dictionary<long, Dictionary<string, List<long>>> dictionary1 = new Dictionary<long, Dictionary<string, List<long>>>(dtAllLinks.Rows.Count);
    string empty = string.Empty;
    string msg = "У ярлыка IMBASE атрибут 'Ключ папки классификатора' имеет неверное значение";
    Dictionary<string, List<long>> dictionary2 = (Dictionary<string, List<long>>) null;
    List<long> longList = (List<long>) null;
    foreach (DataRow row in (InternalDataCollectionBase) dtAllLinks.Rows)
    {
      long int64_1 = Convert.ToInt64(row[SynchStrHelper.COLUMN_NAME_OBJECT_ID]);
      string str = Convert.ToString(row[SynchStrHelper.COLUMN_NAME_CLASSIF_KEY]);
      if (str.Length < 4)
      {
        this.RemoveLinkData(helper, int64_1, objWithLink, msg);
      }
      else
      {
        long int64_2 = Convert.ToInt64(row[SynchStrHelper.COLUMN_NAME_IMBASE_TABLE_REF]);
        string key = str.Substring(0, 2);
        if (dictionary1.TryGetValue(int64_2, out dictionary2))
        {
          if (dictionary2.TryGetValue(key, out longList))
            longList.Add(int64_1);
          else
            dictionary2.Add(key, new List<long>()
            {
              int64_1
            });
        }
        else
          dictionary1.Add(int64_2, new Dictionary<string, List<long>>()
          {
            {
              key,
              new List<long>() { int64_1 }
            }
          });
      }
    }
    return dictionary1.Count <= 0 ? (Dictionary<long, Dictionary<string, List<long>>>) null : dictionary1;
  }

  private List<Tuple<long, long, long>> GetObjectsForTable(
    Dictionary<long, List<Tuple<long, long>>> objWithLink,
    List<InverseImbaseSynchObjectsService.CatalogInfo> links)
  {
    List<Tuple<long, long, long>> tupleList = new List<Tuple<long, long, long>>();
    List<Tuple<long, long>> source = (List<Tuple<long, long>>) null;
    foreach (InverseImbaseSynchObjectsService.CatalogInfo link in links)
    {
      foreach (long linkId in link.LinkIDs)
      {
        long linkID = linkId;
        if (objWithLink.TryGetValue(linkID, out source))
          tupleList.AddRange((IEnumerable<Tuple<long, long, long>>) source.Select<Tuple<long, long>, Tuple<long, long, long>>((System.Func<Tuple<long, long>, Tuple<long, long, long>>) (x => Tuple.Create<long, long, long>(x.Item1, linkID, x.Item2))).ToList<Tuple<long, long, long>>());
      }
    }
    return tupleList.Count <= 0 ? (List<Tuple<long, long, long>>) null : tupleList;
  }

  private void RemoveLinkData(
    InverseImbaseSynchObjectsService.Helper helper,
    long linkID,
    Dictionary<long, List<Tuple<long, long>>> objWithLink,
    string msg)
  {
    if (!objWithLink.ContainsKey(linkID))
      return;
    string caption = helper.Session.GetObjectInfo(linkID).Caption;
    foreach (Tuple<long, long> tuple in objWithLink[linkID])
    {
      helper.Task.AddProcessedObject(tuple.Item1, helper.Session.GetObjectInfo(tuple.Item1).Caption, linkID, caption, SynchStrHelper.NotSynchronized, msg);
      ++helper.Task.Current;
    }
    objWithLink.Remove(linkID);
  }

  private DataSet GetTableObject(IUserSession session, long tableID)
  {
    IDBObject tableObject = session.GetObjectActualCopy(tableID, false);
    if (tableObject == null)
      throw new ApplicationException($"Не удалось получить таблицу IMBASE (ID = {tableID})");
    if (tableObject.ObjectModifyMode == ObjectModifyModes.CreateVersion)
      throw new ApplicationException($"Таблица IMBASE '{tableObject.Caption}' (ID = {tableID.ToString()}) модифицируется через выпуск версии");
    if (tableObject.ObjectModifyMode == ObjectModifyModes.CantModify)
      throw new ApplicationException($"Таблица IMBASE '{tableObject.Caption}' (ID = {tableID.ToString()}) находится на шаге жизненного цикла, который запрещает ее модификацию");
    if (tableObject.ObjectModifyMode == ObjectModifyModes.Checkout)
    {
      if (tableObject.CheckoutBy == 0L)
      {
        try
        {
          tableObject = tableObject.CheckOut();
        }
        catch (Exception ex)
        {
          throw new ApplicationException($"При взятии на редактирование таблицы IMBASE '{tableObject.Caption}' (ID = {tableID.ToString()}), произошла ошибка\r\n{ex.InnerException}");
        }
      }
      else if (tableObject.CheckoutBy != session.UserID)
        throw new ApplicationException($"Таблица IMBASE '{tableObject.Caption}' (ID = {tableID.ToString()}) взята на редактирование другим пользователем");
    }
    DataSet tablesInternal;
    try
    {
      tablesInternal = TableLoadHelper.GetTablesInternal(tableObject);
      if (tablesInternal != null && tablesInternal.Tables.Contains("IMS_ATTR_TYPES"))
      {
        if (tablesInternal.Tables.Contains("IMS_DATA"))
          goto label_17;
      }
      throw new Exception("");
    }
    catch (Exception ex)
    {
      throw new ApplicationException($"Не удалось получить таблицы с данными у объекта типа 'Таблицы IMBASE' - '{tableObject.Caption}' (ID = {tableID.ToString()})", ex);
    }
label_17:
    return tablesInternal;
  }

  private bool CheckUniqueIndexes(
    IImbaseIndexingService iIis,
    Guid sessionGuid,
    long tableId,
    DataSet ds)
  {
    try
    {
      List<int> uIndexes;
      iIis.CheckUniqueBeforeTableDataChange(sessionGuid, tableId, ds.Tables["IMS_ATTR_TYPES"], ds.Tables["IMS_DATA"], out uIndexes, out List<long> _);
      return !uIndexes.Any<int>();
    }
    catch (Exception ex)
    {
      return false;
    }
  }

  private void ProcessTable(
    InverseImbaseSynchObjectsService.Helper helper,
    long tableID,
    List<Tuple<long, long, long>> objects,
    List<InverseImbaseSynchObjectsService.CatalogInfo> catalogs,
    IImbaseIndexingService iIis)
  {
    DataSet tableObject = this.GetTableObject(helper.Session, tableID);
    Dictionary<long, Tuple<string, string>> dictionary = this.ChangeTable(helper, tableObject, objects);
    if (tableObject.HasChanges())
    {
      if (this.CheckUniqueIndexes(iIis, helper.Session.SessionGUID, tableID, tableObject))
      {
        try
        {
          tableObject.AcceptChanges();
          TableLoadHelper.StoreData(helper.Session, tableID, tableObject, helper.Session.GetCustomService(typeof (ITablesIndexer)) as ITablesIndexer);
          iIis.UpdateAfterTableCheckIn(helper.Session.SessionGUID, tableID);
          foreach (Tuple<long, long, long> tuple1 in objects)
          {
            if (dictionary.ContainsKey(tuple1.Item1))
            {
              Tuple<string, string> tuple2 = dictionary[tuple1.Item1];
              helper.Task.AddProcessedObject(tuple1.Item1, helper.Session.GetObjectInfo(tuple1.Item1).Caption, tuple1.Item2, helper.Session.GetObjectInfo(tuple1.Item2).Caption, tuple2.Item2, tuple2.Item1);
              ++helper.Task.Current;
            }
          }
        }
        catch (Exception ex)
        {
          string description = string.Format(LocalizationHolder.rm.GetString("Imbase_ChangesDontSave"), (object) ex.Message);
          foreach (Tuple<long, long, long> tuple3 in objects)
          {
            if (dictionary.ContainsKey(tuple3.Item1))
            {
              Tuple<string, string> tuple4 = dictionary[tuple3.Item1];
              helper.Task.AddProcessedObject(tuple3.Item1, helper.Session.GetObjectInfo(tuple3.Item1).Caption, tuple3.Item2, helper.Session.GetObjectInfo(tuple3.Item2).Caption, SynchStrHelper.NotSynchronized, description);
              ++helper.Task.Current;
            }
          }
        }
      }
      else
      {
        string description = LocalizationHolder.rm.GetString("Imbase_ChangesDontSaveTableNotUnique");
        foreach (Tuple<long, long, long> tuple5 in objects)
        {
          if (dictionary.ContainsKey(tuple5.Item1))
          {
            Tuple<string, string> tuple6 = dictionary[tuple5.Item1];
            helper.Task.AddProcessedObject(tuple5.Item1, helper.Session.GetObjectInfo(tuple5.Item1).Caption, tuple5.Item2, helper.Session.GetObjectInfo(tuple5.Item2).Caption, SynchStrHelper.NotSynchronized, description);
            ++helper.Task.Current;
          }
        }
      }
    }
    else
    {
      if (dictionary == null)
        return;
      foreach (Tuple<long, long, long> tuple7 in objects)
      {
        if (dictionary.ContainsKey(tuple7.Item1))
        {
          Tuple<string, string> tuple8 = dictionary[tuple7.Item1];
          helper.Task.AddProcessedObject(tuple7.Item1, helper.Session.GetObjectInfo(tuple7.Item1).Caption, tuple7.Item2, helper.Session.GetObjectInfo(tuple7.Item2).Caption, tuple8.Item2, tuple8.Item1);
          ++helper.Task.Current;
        }
      }
    }
  }

  private Dictionary<long, Tuple<string, string>> ChangeTable(
    InverseImbaseSynchObjectsService.Helper helper,
    DataSet ds,
    List<Tuple<long, long, long>> objects)
  {
    Dictionary<long, Tuple<string, string>> dictionary = new Dictionary<long, Tuple<string, string>>();
    DataTable table1 = ds.Tables["IMS_ATTR_TYPES"];
    DataTable table2 = ds.Tables["IMS_DATA"];
    string notSynchronized = SynchStrHelper.NotSynchronized;
    string empty = string.Empty;
    foreach (Tuple<long, long, long> tuple in objects)
    {
      long num1 = tuple.Item1;
      long num2 = tuple.Item2;
      long recID = tuple.Item3;
      string status = SynchStrHelper.NotSynchronized;
      try
      {
        DataRow row = table2.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt64(x["F_KEY"]) == recID));
        if (row == null)
          throw new ApplicationException("Не удалось получить строку таблицы IMBASE");
        AttributeValues[] objectAttributes = this.GetObjectAttributes(helper.Session, num1);
        if (((IEnumerable<AttributeValues>) objectAttributes).Where<AttributeValues>((System.Func<AttributeValues, bool>) (x => helper.AttributeIDs.BinarySearch(x.AttributeID) > -1)).Count<AttributeValues>() == 0)
          throw new ApplicationException("У объекта отсутствуют атрибуты для синхронизации");
        string str = this.ChangeValuesInRow(helper, objectAttributes, table1, table2, row, out status);
        dictionary[num1] = Tuple.Create<string, string>(str, status);
      }
      catch (ApplicationException ex)
      {
        helper.Task.AddProcessedObject(num1, helper.Session.GetObjectInfo(num1).Caption, num2, helper.Session.GetObjectInfo(num2).Caption, status, ex.Message);
        ++helper.Task.Current;
      }
    }
    return dictionary.Count <= 0 ? (Dictionary<long, Tuple<string, string>>) null : dictionary;
  }

  private string ChangeValuesInRow(
    InverseImbaseSynchObjectsService.Helper helper,
    AttributeValues[] objAttributes,
    DataTable dtAttrs,
    DataTable dtData,
    DataRow row,
    out string status)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine("Атрибуты:");
    string strAttrGuid = string.Empty;
    string report = string.Empty;
    status = SynchStrHelper.NotSynchronized;
    foreach (int attributeId in helper.AttributeIDs)
    {
      int attrID = attributeId;
      AttributeValues attributeValues = ((IEnumerable<AttributeValues>) objAttributes).FirstOrDefault<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == attrID));
      if (attributeValues == null)
        stringBuilder.AppendLine($"'{MetaDataHelper.GetAttributeTypeName(attrID)}' (ID = {attrID.ToString()}) - отсутствует у объекта");
      else if (this.IsAttributeValueEmpty(attributeValues))
      {
        stringBuilder.AppendLine($"'{attributeValues.AttributeName}' (ID = {attrID.ToString()}) - имеет пустое значение");
      }
      else
      {
        strAttrGuid = attributeValues.AttributeGuid.ToString();
        DataRow dataRow = dtAttrs.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToString(x["F_ATTRIBUTE_GUID"]) == strAttrGuid));
        if (dataRow == null)
        {
          stringBuilder.AppendLine(this.AddAttributeToTable(helper.Session, row, attributeValues, dtAttrs, dtData));
          status = SynchStrHelper.Synchronized;
        }
        else if (Convert.ToInt32(dataRow["F_REQUIRED"]) != 2 || Convert.ToInt32(dataRow["F_COMPUTED"]) != 0)
        {
          stringBuilder.AppendLine($"'{attributeValues.AttributeName}' (ID = {attrID.ToString()}) - является вычисляемым полем таблицы");
        }
        else
        {
          DataColumn column = row.Table.Columns[strAttrGuid];
          try
          {
            status = !this.ChangeValueInTable(helper.Session, attributeValues, column, row, out report) ? (status == SynchStrHelper.NotSynchronized ? SynchStrHelper.NotNeedToSync : status) : SynchStrHelper.Synchronized;
            stringBuilder.AppendLine(report);
          }
          catch (ApplicationException ex)
          {
            stringBuilder.AppendLine($"'{attributeValues.AttributeName}' (ID = {attributeValues.AttributeID.ToString()}): {ex.Message}");
            if (ex.InnerException != null)
              stringBuilder.AppendLine(ex.InnerException.Message);
          }
        }
      }
    }
    return stringBuilder.ToString();
  }

  private string AddAttributeToTable(
    IUserSession session,
    DataRow row,
    AttributeValues objAV,
    DataTable dtAttrs,
    DataTable dtData)
  {
    StringBuilder stringBuilder = new StringBuilder();
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(objAV.AttributeGuid);
    DataRow row1 = dtAttrs.NewRow();
    row1["F_ATTRIBUTE_GUID"] = (object) objAV.AttributeGuid;
    row1["F_REQUIRED"] = (object) 2;
    row1["F_COMPUTED"] = (object) 0;
    row1["F_FORMULA"] = (object) string.Empty;
    row1["F_UNIQUE"] = (object) 0;
    row1["F_DEFAULT_VALUE"] = attributeType.DefaultValue;
    row1["F_OPTIONS"] = (object) attributeType.Options;
    row1["F_UNITS"] = (object) string.Empty;
    dtAttrs.Rows.Add(row1);
    string columnName = objAV.AttributeGuid.ToString();
    bool isArray = this.IsArray(attributeType.MultiValueMode);
    DataColumn dataColumn = TableLoadHelper.CreateDataColumn(dtData, columnName, attributeType.FieldType, isArray);
    if (!isArray)
    {
      string format = LocalizationHolder.rm.GetString("Imbase_Attr_NewValue");
      if (objAV.AttributeType == FieldTypes.ftMeasured)
      {
        MeasuredValue measuredValue = objAV.Values[0] as MeasuredValue;
        dataColumn.ExtendedProperties[(object) "F_MEASURE"] = (object) measuredValue.MeasureID;
        row[columnName] = (object) measuredValue.Value;
      }
      else
        row[columnName] = objAV.AttributeType != FieldTypes.ftObjectLink ? objAV.Values[0] : (object) session.GetObjectInfo(Convert.ToInt64(objAV.Values[0])).VersionGuid;
      stringBuilder.AppendLine(string.Format(format, (object) objAV.AttributeName, (object) objAV.AttributeID.ToString(), (object) Convert.ToString(row[columnName])));
    }
    else
    {
      string format = LocalizationHolder.rm.GetString("Imbase_Attr_NewValues");
      object[] objArray = new object[objAV.Values.Length];
      if (objAV.AttributeType == FieldTypes.ftMeasured)
      {
        long toMeasureID = 0;
        bool flag = false;
        for (int index = 0; index < objAV.Values.Length; ++index)
        {
          if (!(objAV.Values[index] is MeasuredValue mValue))
          {
            objArray[index] = (object) DBNull.Value;
          }
          else
          {
            if (!flag)
            {
              dataColumn.ExtendedProperties[(object) "F_MEASURE"] = (object) (toMeasureID = mValue.MeasureID);
              flag = true;
            }
            MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(mValue, toMeasureID);
            objArray[index] = (object) measuredValue.Value;
          }
        }
      }
      else if (objAV.AttributeType == FieldTypes.ftObjectLink)
      {
        for (int index = 0; index < objAV.Values.Length; ++index)
          objArray[index] = objAV.Values[index] == null || objAV.Values[index] == DBNull.Value ? (object) DBNull.Value : (object) session.GetObjectInfo(Convert.ToInt64(objAV.Values[index])).VersionGuid;
      }
      else
        objArray = objAV.Values;
      row[columnName] = (object) new ValuesArray((Array) objArray, dataColumn.ExtendedProperties[(object) "dataType"] as Type);
      string empty = string.Empty;
      foreach (object obj in objArray)
        empty += $"{Convert.ToString(obj)}; ";
      stringBuilder.AppendLine(string.Format(format, (object) objAV.AttributeName, (object) objAV.AttributeID.ToString(), (object) empty));
    }
    return stringBuilder.ToString();
  }

  private bool IsArray(MultiValueModes mode)
  {
    return mode == MultiValueModes.MultiValuesFromList || mode == MultiValueModes.MultiValues;
  }

  protected bool ChangeValueInTable(
    IUserSession session,
    AttributeValues objAV,
    DataColumn column,
    DataRow row,
    out string report)
  {
    bool flag1 = false;
    report = string.Empty;
    string columnName = objAV.AttributeGuid.ToString();
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string empty3 = string.Empty;
    object obj1 = (object) null;
    bool flag2 = false;
    string format;
    if (!this.IsArray(objAV.MultipleValued))
    {
      empty2 = Convert.ToString(row[columnName]);
      if (objAV.AttributeType == FieldTypes.ftMeasured)
      {
        MeasuredValue measuredValue = objAV.Values[0] as MeasuredValue;
        long num = measuredValue.MeasureID;
        if (column.ExtendedProperties.ContainsKey((object) "F_MEASURE"))
        {
          num = Convert.ToInt64(column.ExtendedProperties[(object) "F_MEASURE"]);
          if (num != measuredValue.MeasureID)
          {
            try
            {
              measuredValue = MeasureHelper.ConvertToMeasuredValue(measuredValue, num);
            }
            catch (Exception ex)
            {
              throw new ApplicationException("Не удалось привести значение атрибута объекта к единице измерения, указанной в таблице IMBASE", ex);
            }
          }
        }
        obj1 = (object) measuredValue.Value;
        double result = 0.0;
        MeasuredValue val2 = double.TryParse(empty2, out result) ? new MeasuredValue(result, num) : (MeasuredValue) null;
        flag2 = val2 != null && MeasureHelper.Compare(measuredValue, val2) == CompareResult.Equal;
      }
      else if (objAV.AttributeType == FieldTypes.ftObjectLink)
      {
        long int64 = Convert.ToInt64(objAV.Values[0]);
        long result = 0;
        if (GuidHelper.IsGuid(empty2))
        {
          QuickObjectInfo objectInfo = session.GetObjectInfo(new Guid(empty2));
          result = !objectInfo.Empty ? objectInfo.ObjectID : 0L;
          obj1 = (object) session.GetObjectInfo(int64).VersionGuid;
        }
        else
        {
          if (!long.TryParse(empty2, out result))
            result = 0L;
          obj1 = (object) int64;
        }
        flag2 = result != 0L && int64 == result;
      }
      else
      {
        obj1 = objAV.Values[0];
        flag2 = obj1.ToString().Equals(Convert.ToString(row[columnName]), StringComparison.InvariantCulture);
      }
      empty3 = Convert.ToString(obj1);
      format = LocalizationHolder.rm.GetString("Imbase_Attr_OldValue_NewValue");
    }
    else
    {
      object[] array = row[columnName] is ValuesArray valuesArray ? valuesArray.GetArray() : (object[]) null;
      object[] objArray = new object[objAV.Values.Length];
      if (objAV.AttributeType == FieldTypes.ftMeasured)
      {
        List<MeasuredValue> list = ((IEnumerable<object>) objAV.Values).Select<object, MeasuredValue>((System.Func<object, MeasuredValue>) (x => x as MeasuredValue)).ToList<MeasuredValue>();
        long num;
        if (column.ExtendedProperties.ContainsKey((object) "F_MEASURE"))
          num = Convert.ToInt64(column.ExtendedProperties[(object) "F_MEASURE"]);
        else
          num = (list.FirstOrDefault<MeasuredValue>((System.Func<MeasuredValue, bool>) (x => x != null)) ?? throw new ApplicationException("Ошибка в значениях атрибута объекта")).MeasureID;
        for (int index = 0; index < list.Count; ++index)
        {
          MeasuredValue mValue = list[index];
          if (mValue != null)
          {
            if (mValue.MeasureID != num)
            {
              try
              {
                list[index] = MeasureHelper.ConvertToMeasuredValue(mValue, num);
                empty3 += $"{Convert.ToString((object) list[index])}; ";
                continue;
              }
              catch (Exception ex)
              {
                list[index] = (MeasuredValue) null;
                empty3 += "; ";
                continue;
              }
            }
          }
          empty3 += $"{Convert.ToString(mValue.Value)}; ";
        }
        List<MeasuredValue> measuredValueList1 = new List<MeasuredValue>();
        bool flag3 = false;
        double result = 0.0;
        foreach (object obj2 in array)
        {
          MeasuredValue measuredValue = (MeasuredValue) null;
          if (double.TryParse(Convert.ToString(obj2), out result))
          {
            measuredValue = new MeasuredValue(result, num);
            flag3 = true;
            empty2 += $"{Convert.ToString(result)}; ";
          }
          else
            empty2 += "; ";
          measuredValueList1.Add(measuredValue);
        }
        List<MeasuredValue> measuredValueList2 = flag3 ? measuredValueList1 : (List<MeasuredValue>) null;
        if (measuredValueList2 != null && measuredValueList2.Count == list.Count)
        {
          flag2 = true;
          for (int index = 0; index < measuredValueList2.Count; ++index)
          {
            if (list[index] != measuredValueList2[index] && (list[index] == null || measuredValueList2[index] == null || MeasureHelper.Compare(list[index], measuredValueList2[index]) != CompareResult.Equal))
            {
              flag2 = false;
              break;
            }
          }
        }
        if (!flag2)
        {
          for (int index = 0; index < list.Count; ++index)
          {
            MeasuredValue measuredValue = list[index];
            objArray[index] = measuredValue != null ? (object) measuredValue.Value : (object) DBNull.Value;
          }
          obj1 = (object) new ValuesArray((Array) objArray, column.ExtendedProperties[(object) "dataType"] as Type);
        }
      }
      else if (objAV.AttributeType == FieldTypes.ftObjectLink)
      {
        if (array != null)
        {
          foreach (object obj3 in array)
            empty2 += $"{Convert.ToString(obj3)}; ";
        }
        bool flag4 = false;
        if (array.Length == objAV.Values.Length)
        {
          flag2 = true;
          string empty4 = string.Empty;
          for (int index = 0; index < objAV.Values.Length; ++index)
          {
            long int64 = objAV.Values[index] == null || objAV.Values[index] == DBNull.Value ? 0L : Convert.ToInt64(objAV.Values[index]);
            long result = 0;
            string str = Convert.ToString(array[index]);
            if (GuidHelper.IsGuid(str))
            {
              QuickObjectInfo objectInfo = session.GetObjectInfo(new Guid(str));
              result = !objectInfo.Empty ? objectInfo.ObjectID : 0L;
              flag4 = true;
            }
            else if (!long.TryParse(str, out result))
              result = 0L;
            long num = result;
            if (int64 != num)
            {
              flag2 = false;
              break;
            }
          }
        }
        if (!flag2)
        {
          if (flag4)
          {
            for (int index = 0; index < objAV.Values.Length; ++index)
            {
              object obj4 = objAV.Values[index];
              if (obj4 == null || obj4 == DBNull.Value)
              {
                objArray[index] = (object) DBNull.Value;
              }
              else
              {
                QuickObjectInfo objectInfo = session.GetObjectInfo(Convert.ToInt64(obj4));
                objArray[index] = !objectInfo.Empty ? (object) objectInfo.VersionGuid : (object) DBNull.Value;
              }
            }
          }
          else
            objArray = objAV.Values;
          foreach (object obj5 in objArray)
            empty3 += $"{Convert.ToString(obj5)}; ";
          obj1 = (object) new ValuesArray((Array) objArray, column.ExtendedProperties[(object) "dataType"] as Type);
        }
      }
      else
      {
        foreach (object obj6 in objAV.Values)
          empty3 += $"{Convert.ToString(obj6)}; ";
        if (array != null)
        {
          foreach (object obj7 in array)
            empty2 += $"{Convert.ToString(obj7)}; ";
        }
        if (objAV.Values.Length == array.Length)
        {
          flag2 = true;
          for (int index = 0; index < array.Length; ++index)
          {
            if (objAV.Values[index] != array[index] && !(Convert.ToString(objAV.Values[index]) == Convert.ToString(array[index])))
            {
              flag2 = false;
              obj1 = (object) new ValuesArray((Array) objAV.Values, column.ExtendedProperties[(object) "dataType"] as Type);
              break;
            }
          }
        }
      }
      format = LocalizationHolder.rm.GetString("Imbase_Attr_OldValues_NewValues");
    }
    if (flag2)
    {
      report = $"'{objAV.AttributeName}' (ID = {objAV.AttributeID.ToString()}) - значения равны";
    }
    else
    {
      report = string.Format(format, (object) objAV.AttributeName, (object) objAV.AttributeID.ToString(), (object) empty2, (object) empty3);
      row[columnName] = obj1;
      flag1 = true;
    }
    return flag1;
  }

  private DataTable GetDataObjects(IUserSession session, int objTypeID, List<long> objIDs = null)
  {
    DataTable dataObjects = (DataTable) null;
    IDBObjectCollection objectCollection = session.GetObjectCollection(objTypeID);
    if (objectCollection != null)
    {
      objectCollection.ObjectTypeID = objTypeID;
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
      if (objIDs != null)
        conditionStructureList.Add(new ConditionStructure(-2, RelationalOperators.In, (object) objIDs.ToArray(), (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID));
      else
        conditionStructureList.Add(new ConditionStructure(-7, RelationalOperators.Equal, (object) objTypeID, LogicalOperators.NONE, 0, false));
      List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>()
      {
        new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
        new ColumnDescriptor((object) Intermech.Imbase.Consts.ImbaseObjectRefAttID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.ASC, 0),
        new ColumnDescriptor((object) Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
      };
      DBRecordSetParams paramSet = new DBRecordSetParams(conditionStructureList.ToArray(), columnDescriptorList.ToArray());
      dataObjects = objectCollection.Select(paramSet);
    }
    if (dataObjects == null || dataObjects.Rows.Count == 0)
      throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_Objs_Data_Empty"));
    return dataObjects;
  }

  private AttributeValues[] GetObjectAttributes(IUserSession session, long objID)
  {
    AttributeValues[] attributesValues = (session.GetObjectActualCopy(objID, false) ?? throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_GetObject_Error"))).GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGuid);
    return attributesValues != null && attributesValues.Length != 0 ? attributesValues : throw new ApplicationException("У объекта отсутствуют атрибуты для синхронизации");
  }

  private class ProcessedInfo
  {
    internal DataTable ProcessedData;

    internal int Count { get; set; }

    internal int Current { get; set; }

    internal bool TaskRunning { get; set; }

    internal DateTime FinishedTime { get; set; }

    public ProcessedInfo()
    {
      this.ProcessedData = new DataTable();
      this.ProcessedData.Columns.AddRange(new DataColumn[6]
      {
        new DataColumn(SynchStrHelper.COLUMN_NAME_OBJECT_ID),
        new DataColumn(SynchStrHelper.COLUMN_NAME_CAPTION),
        new DataColumn(SynchStrHelper.COLUMN_NAME_IMBASE_ID),
        new DataColumn(SynchStrHelper.COLUMN_NAME_IMBASE_CAPTION),
        new DataColumn(SynchStrHelper.COLUMN_NAME_STATUS),
        new DataColumn(SynchStrHelper.COLUMN_NAME_REPORT)
      });
      this.Count = this.Current = 0;
      this.TaskRunning = true;
    }

    internal void AddNotSynchType(int typeID, string description)
    {
      lock (this.ProcessedData)
        this.ProcessedData.Rows.Add((object) typeID, (object) MetaDataHelper.GetObjectTypeName(typeID), null, null, (object) SynchStrHelper.NotSynchronized, (object) description);
    }

    internal void AddProcessedObject(
      long objID,
      string objCaption,
      long imbaseObjID,
      string imbaseCaption,
      string status,
      string description)
    {
      lock (this.ProcessedData)
      {
        if (imbaseObjID != 0L)
          this.ProcessedData.Rows.Add((object) objID, (object) objCaption, (object) imbaseObjID, (object) imbaseCaption, (object) status, (object) description);
        else
          this.ProcessedData.Rows.Add((object) objID, (object) objCaption, null, (object) imbaseCaption, (object) status, (object) description);
      }
    }

    internal DataTable ProcessedDataCopy()
    {
      lock (this.ProcessedData)
      {
        DataTable dataTable = this.ProcessedData.Copy();
        this.ProcessedData.Clear();
        return dataTable;
      }
    }
  }

  private class Helper
  {
    internal IUserSession Session { get; private set; }

    internal InverseImbaseSynchObjectsService.ProcessedInfo Task { get; private set; }

    internal List<int> AttributeIDs { get; set; }

    internal Helper(Guid sessionGuid)
    {
      this.Session = ImbaseServer.GetSession(sessionGuid);
      if (this.Session == null)
        throw new Exception(LocalizationHolder.rm.GetString("Imbase.Server.ImbaseindexingService.NullSession"));
    }

    internal void LoadTask(
      Dictionary<Guid, InverseImbaseSynchObjectsService.ProcessedInfo> tasks,
      Guid taskGuid)
    {
      if (tasks.ContainsKey(taskGuid))
      {
        this.Task = tasks[taskGuid];
        this.Task.TaskRunning = true;
      }
      else
      {
        this.Task = new InverseImbaseSynchObjectsService.ProcessedInfo();
        foreach (Guid key in tasks.Where<KeyValuePair<Guid, InverseImbaseSynchObjectsService.ProcessedInfo>>((System.Func<KeyValuePair<Guid, InverseImbaseSynchObjectsService.ProcessedInfo>, bool>) (x => !x.Value.TaskRunning && (DateTime.Now - x.Value.FinishedTime).TotalMinutes > 10.0)).ToDictionary<KeyValuePair<Guid, InverseImbaseSynchObjectsService.ProcessedInfo>, Guid, InverseImbaseSynchObjectsService.ProcessedInfo>((System.Func<KeyValuePair<Guid, InverseImbaseSynchObjectsService.ProcessedInfo>, Guid>) (x => x.Key), (System.Func<KeyValuePair<Guid, InverseImbaseSynchObjectsService.ProcessedInfo>, InverseImbaseSynchObjectsService.ProcessedInfo>) (y => y.Value)).Keys)
          tasks.Remove(key);
        tasks.Add(taskGuid, this.Task);
      }
    }
  }

  private class CatalogInfo
  {
    public long CatalogID { get; set; }

    public InverseImbaseSynchObjectsService.CatalogIndexes Indexes { get; set; }

    public List<long> LinkIDs { get; set; }
  }

  private class CatalogIndexes
  {
    public List<int> UniqueIndexes { get; set; }

    public List<int> Indexes { get; set; }
  }
}
