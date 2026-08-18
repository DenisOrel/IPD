// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Server.FormDesignerService
// Assembly: Intermech.FormDesigner.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ABD17B9B-52A2-4551-9041-386497DBE670
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.FormDesigner.Server.dll

using Intermech.Extensions;
using Intermech.Imbase;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.FormDesigner.Server;

[Serializable]
public class FormDesignerService : LongLifeObject, IFormDesignerService, IFormDesignerServer
{
  public const long cnt_ObjectsCache_Time2Live = 18000000;
  internal long LastClearTime;
  internal FormsCache _srvCache;
  internal OnlineCache<int, FormDesignerService.FormAccess> _usrCache;
  internal OnlineCacheTimed<VersionCacheItem, FormDesignerService.FormAccess> _usrVerCache;
  internal Dictionary<FormDesignerService.ElementInfo, List<UpdateHandlerInfo>> _handlers = new Dictionary<FormDesignerService.ElementInfo, List<UpdateHandlerInfo>>();
  private List<UpdateHandlerInfo> _handlersForAllObjectTypes = new List<UpdateHandlerInfo>();
  private List<UpdateHandlerInfo> _handlersForAllRelationTypes = new List<UpdateHandlerInfo>();
  internal Dictionary<FormDesignerService.ElementInfo, List<UpdateHandlerInfo>> _baseHandlers = new Dictionary<FormDesignerService.ElementInfo, List<UpdateHandlerInfo>>();
  private const string ROOT_NODE = "TypesSettings";
  private const string TYPE_NODE = "Type";
  private const string FORM_NODE = "Form";
  private const string FORM_DISPLAY_ORDER_NODE = "FormDisplayOrder";
  private const string GUID_ATTR = "Guid";
  private const string INDEX_ATTR = "Index";
  private Dictionary<Guid, TypeInfoHelper> _typeInfoDict = new Dictionary<Guid, TypeInfoHelper>();

  internal Dictionary<Guid, ValueInfo> AttrObjValueCache { get; set; }

  internal Dictionary<Guid, ValueInfo> AttrRelValueCache { get; set; }

  public void FlushCache()
  {
    Trace.WriteLine("Flushing forms server cache");
    this.Init();
  }

  private void LoadForms(IUserSession systemSession)
  {
    Dictionary<long, List<int>> dictionary1 = this.ConvertToDictionary(this.GetTypes(systemSession, StartupHolder.GlobalObjGuidType), new System.Func<Guid, int>(MetaDataHelper.GetObjectTypeID));
    this.AddToCache(systemSession, dictionary1, AttributableElements.Object);
    Dictionary<long, List<int>> dictionary2 = this.ConvertToDictionary(this.GetTypes(systemSession, StartupHolder.GlobalRelGuidType), new System.Func<Guid, int>(MetaDataHelper.GetRelationTypeID));
    this.AddToCache(systemSession, dictionary2, AttributableElements.Relation);
  }

  private DataTable GetTypes(IUserSession systemSession, Guid attrGuid)
  {
    DataTable values1 = (systemSession.GetObjectCollection(StartupHolder.DataEditFormsType) as FormDBObjectCollection).GetValues(attrGuid);
    List<int> childrenIdRecursive = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(MetaDataHelper.GetObjectTypeID(StartupHolder.DataEditFormsType));
    for (int index = 1; index < childrenIdRecursive.Count; ++index)
    {
      if (systemSession.GetObjectCollection(childrenIdRecursive[index]) is FormDBObjectCollection objectCollection)
      {
        DataTable values2 = objectCollection.GetValues(attrGuid);
        DataSetProcessor.AddTable(values1, values2, false);
      }
    }
    values1.AcceptChanges();
    return values1;
  }

  private Dictionary<long, List<int>> ConvertToDictionary(DataTable dt, System.Func<Guid, int> f)
  {
    return dt.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToString(x[2]) != string.Empty)).ToLookup<DataRow, long, string>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[0])), (System.Func<DataRow, string>) (x => Convert.ToString(x[2]))).ToDictionary<IGrouping<long, string>, long, List<int>>((System.Func<IGrouping<long, string>, long>) (x => x.Key), (System.Func<IGrouping<long, string>, List<int>>) (x => x.Distinct<string>().Select<string, int>((System.Func<string, int>) (y => f(new Guid(y)))).ToList<int>()));
  }

  private void AddToCache(
    IUserSession systemSession,
    Dictionary<long, List<int>> dict,
    AttributableElements kind)
  {
    if (dict.Count <= 0)
      return;
    DataTable objectsInfo = this.GetObjectsInfo(systemSession, (IEnumerable<long>) dict.Keys);
    List<DataRow> list1 = objectsInfo.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt64(x[0]) < 0L)).ToList<DataRow>();
    List<DataRow> list2 = objectsInfo.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt64(x[0]) > 0L)).ToList<DataRow>();
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    foreach (DataRow dataRow in list1)
    {
      long int64_1 = Convert.ToInt64(dataRow[0]);
      int int32 = Convert.ToInt32(dataRow[1]);
      string caption = Convert.ToString(dataRow[2]);
      long int64_2 = Convert.ToInt64(dataRow[3]);
      string str = Convert.ToString(dataRow[4]);
      bool hasFormula = !string.IsNullOrEmpty(str) && str != "0";
      FormInformation fi = new FormInformation(Math.Abs(int64_1), caption, hasFormula, int32, int64_2);
      this._srvCache.Add(dict[int64_1].ToArray(), fi, kind);
    }
    foreach (DataRow dataRow in list2)
    {
      long int64_3 = Convert.ToInt64(dataRow[0]);
      int int32 = Convert.ToInt32(dataRow[1]);
      string caption = Convert.ToString(dataRow[2]);
      long int64_4 = Convert.ToInt64(dataRow[3]);
      string str = Convert.ToString(dataRow[4]);
      bool hasFormula = !string.IsNullOrEmpty(str) && str != "0";
      int[] array = dict[int64_3].ToArray();
      if (int64_4 == 0L)
      {
        FormInformation fi = new FormInformation(int64_3, caption, hasFormula, int32, int64_4);
        this._srvCache.Add(array, fi, kind).ForEach((Action<FormsCache.CacheItem>) (x => x.Visible4AllUser = true));
      }
      else
      {
        foreach (int typeId in array)
        {
          FormsCache.CacheItems typesForms = this._srvCache.GetTypesForms(typeId, kind);
          FormsCache.CacheItem cacheItem = (FormsCache.CacheItem) null;
          long key = int64_3;
          ref FormsCache.CacheItem local = ref cacheItem;
          if (!typesForms.TryGetValue(key, out local))
          {
            FormInformation fi = new FormInformation(int64_3, caption, hasFormula, int32, int64_4);
            cacheItem = this._srvCache.Add(typeId, fi, kind);
            cacheItem.Visible4EditUser = false;
          }
          else
            cacheItem.FormInfo.HasFormula = hasFormula;
          cacheItem.Visible4AllUser = true;
        }
      }
    }
  }

  private DataTable GetObjectsInfo(IUserSession systemSession, IEnumerable<long> objIDs)
  {
    ColumnDescriptor[] columns = new ColumnDescriptor[5]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_CHKOUT_BY, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) MetaDataHelper.GetAttributeID((object) StartupHolder.FormulaGuidType), AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
    DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) objIDs.ToArray<long>(), LogicalOperators.NONE, 0, false)
    }, columns);
    DataTable toTable = systemSession.ObjectsSelect(StartupHolder.DataEditFormsType, dbRecordSetParams);
    List<int> childrenIdRecursive = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(MetaDataHelper.GetObjectTypeID(StartupHolder.DataEditFormsType));
    for (int index = 1; index < childrenIdRecursive.Count; ++index)
    {
      IDBObjectCollection objectCollection = systemSession.GetObjectCollection(childrenIdRecursive[index]);
      if (objectCollection != null)
      {
        DataTable fromTable = objectCollection.Select(dbRecordSetParams);
        DataSetProcessor.AddTable(toTable, fromTable, false);
      }
    }
    toTable.AcceptChanges();
    return toTable;
  }

  private void Init()
  {
    this._srvCache = new FormsCache();
    this._usrCache = new OnlineCache<int, FormDesignerService.FormAccess>();
    this._usrVerCache = new OnlineCacheTimed<VersionCacheItem, FormDesignerService.FormAccess>();
    this.AttrObjValueCache = new Dictionary<Guid, ValueInfo>(0);
    this.AttrRelValueCache = new Dictionary<Guid, ValueInfo>(0);
    IUserSession systemSession = this.GetSystemSession(nameof (FormDesignerService));
    try
    {
      if (systemSession == null)
        throw new Exception(LocalizationHolder.rm.GetString("FormDesigner.Server_2"));
      this.LoadForms(systemSession);
      this.LoadConfiguration(systemSession);
    }
    catch (Exception ex)
    {
      Trace.WriteLine(ex.ToString());
    }
    finally
    {
      systemSession?.Logout(nameof (FormDesignerService));
    }
  }

  public FormDesignerService()
  {
    this.Init();
    IEventLogHelper service = ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    service.CommitEvent += new TransactionHandler(this.On_eventHelper_CommitEvent);
    service.RollbackEvent += new TransactionHandler(this.On_eventHelper_RollbackEvent);
  }

  private int[] ParseTypes(
    IUserSession session,
    List<string> typeGuidsStr,
    AttributableElements kind)
  {
    List<int> intList = (List<int>) null;
    if (typeGuidsStr != null)
    {
      intList = new List<int>(typeGuidsStr.Count);
      switch (kind)
      {
        case AttributableElements.Object:
          using (List<string>.Enumerator enumerator = typeGuidsStr.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              string current = enumerator.Current;
              if (GuidHelper.IsGuid(current))
              {
                Guid guid = new Guid(current);
                int num = MetaDataHelper.GetObjectTypeID(guid);
                if (num == -1)
                {
                  IDBObjectType objectType = session.GetObjectType(guid, false);
                  if (objectType != null)
                    num = objectType.ObjectType;
                  else
                    continue;
                }
                if (!intList.Contains(num))
                  intList.Add(num);
              }
            }
            break;
          }
        case AttributableElements.Relation:
          using (List<string>.Enumerator enumerator = typeGuidsStr.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              string current = enumerator.Current;
              if (GuidHelper.IsGuid(current))
              {
                int relationTypeId = MetaDataHelper.GetRelationTypeID(new Guid(current));
                if (!intList.Contains(relationTypeId))
                  intList.Add(relationTypeId);
              }
            }
            break;
          }
      }
    }
    return intList == null || intList.Count <= 0 ? (int[]) null : intList.ToArray();
  }

  internal int[] ParseTypes(
    IUserSession session,
    IDBAttribute attribute,
    AttributableElements kind)
  {
    List<int> intList = new List<int>();
    if (session != null && attribute != null)
    {
      string empty = string.Empty;
      for (int index = 0; index < attribute.ValuesCount; ++index)
      {
        attribute.Index = index;
        if (!(attribute.Value.GetType() == typeof (DBNull)))
        {
          string asString = attribute.AsString;
          if (GuidHelper.IsGuid(asString))
          {
            Guid guid = new Guid(asString);
            switch (kind)
            {
              case AttributableElements.Object:
                int num = MetaDataHelper.GetObjectTypeID(guid);
                if (num == -1)
                {
                  IDBObjectType objectType = session.GetObjectType(guid, false);
                  if (objectType != null)
                    num = objectType.ObjectType;
                  else
                    continue;
                }
                if (num != -1 && !intList.Contains(num))
                {
                  intList.Add(num);
                  continue;
                }
                continue;
              case AttributableElements.Relation:
                int relationTypeId = MetaDataHelper.GetRelationTypeID(guid);
                if (relationTypeId != -1 && !intList.Contains(relationTypeId))
                {
                  intList.Add(relationTypeId);
                  continue;
                }
                continue;
              default:
                continue;
            }
          }
        }
      }
    }
    return intList.ToArray();
  }

  private List<int> CollectChildObjectTypes(int objectTypeID, IUserSession session)
  {
    List<int> intList = new List<int>();
    if (objectTypeID != -1 && objectTypeID != -1)
    {
      IDBObjectType objectType = session.GetObjectType(objectTypeID);
      if (objectType != null)
      {
        ArrayList objsTreeList = new ArrayList();
        objectType.FillChildrenList(objsTreeList);
        foreach (object obj in objsTreeList)
          intList.Add(Convert.ToInt32(obj));
      }
    }
    else
    {
      List<IMSObjectType> objectTypesList = MetaDataHelper.GetObjectTypesList();
      intList.Capacity = objectTypesList.Count;
      foreach (IMSObjectType imsObjectType in objectTypesList)
      {
        if (imsObjectType != null && imsObjectType.ObjectTypeID != -1)
          intList.Add(imsObjectType.ObjectTypeID);
      }
    }
    return intList;
  }

  private List<int> CollectChildRelationTypes(int relationTypeID)
  {
    List<int> intList = new List<int>();
    if (relationTypeID != -1 && relationTypeID != -1)
    {
      intList.Add(relationTypeID);
    }
    else
    {
      List<IMSRelationType> relationTypesList = MetaDataHelper.GetRelationTypesList();
      intList.Capacity = relationTypesList.Count;
      foreach (IMSRelationType imsRelationType in relationTypesList)
      {
        if (imsRelationType != null && imsRelationType.RelationTypeID != -1)
          intList.Add(imsRelationType.RelationTypeID);
      }
    }
    return intList;
  }

  protected bool GetLocalCachedForms(
    ref int elemTypeID,
    UserSession session,
    AttributableElements kind,
    Dictionary<FormInformation, FormDesignerService.FormAccess> newDict)
  {
    bool localCachedForms = false;
    if (session != null)
    {
      lock (this._srvCache)
      {
        List<FormInformation> source = new List<FormInformation>();
        FormsCache.CacheItem cacheItem1;
        switch (kind)
        {
          case AttributableElements.Object:
            if (elemTypeID != -1)
            {
              if (elemTypeID != -1)
              {
                int num1 = elemTypeID;
                cacheItem1 = (FormsCache.CacheItem) null;
                CacheHelper.CacheBaseItem<FormDesignerService.FormAccess> cacheBaseItem = (CacheHelper.CacheBaseItem<FormDesignerService.FormAccess>) null;
                while (num1 != -1)
                {
                  source.Clear();
                  FormsCache.CacheItems typesForms1 = this._srvCache.GetTypesForms(num1, kind);
                  CacheHelper.CacheBaseItems<FormDesignerService.FormAccess> typesForms2 = (CacheHelper.CacheBaseItems<FormDesignerService.FormAccess>) this._usrCache.GetTypesForms(session.UserID, session.RoleID, num1, kind);
                  foreach (KeyValuePair<long, FormsCache.CacheItem> keyValuePair in (ConcurrentDictionary<long, FormsCache.CacheItem>) typesForms1)
                  {
                    FormsCache.CacheItem cacheItem2 = keyValuePair.Value;
                    FormInformation formInfo = cacheItem2.FormInfo;
                    long num2 = Math.Abs(formInfo.ID);
                    if (!typesForms2.TryGetValue(num2, out cacheBaseItem))
                    {
                      if (!cacheItem2.Visible4EditUser && formInfo.CheckOutBy == session.UserID || !cacheItem2.Visible4AllUser && formInfo.CheckOutBy != session.UserID)
                      {
                        IDBObject objectActualCopy = session.GetObjectActualCopy(num2, false);
                        if (objectActualCopy != null)
                        {
                          FormInformation key = new FormInformation(Math.Abs(formInfo.ID), objectActualCopy);
                          newDict[key] = FormDesignerService.FormAccess.faHidden;
                        }
                      }
                      else
                      {
                        FormInformation newFI = formInfo;
                        if (formInfo.CheckOutBy == session.UserID)
                        {
                          newFI = new FormInformation(num2, string.Empty, false);
                          newFI = newDict.Keys.FirstOrDefault<FormInformation>((System.Func<FormInformation, bool>) (x => x == newFI));
                          if (newFI == null)
                          {
                            IDBObject objectActualCopy = session.GetObjectActualCopy(formInfo.ID, false);
                            if (objectActualCopy != null)
                              newFI = new FormInformation(objectActualCopy);
                          }
                        }
                        if (newFI != null && !source.Contains(newFI))
                          source.Add(newFI);
                      }
                    }
                    else
                      newDict[cacheBaseItem.FormInfo] = cacheBaseItem.Value;
                  }
                  if (source.Count > 0)
                  {
                    Dictionary<long, FormDesignerService.FormAccess> formVisibility = this.GetFormVisibility(source.Select<FormInformation, long>((System.Func<FormInformation, long>) (x => x.ID)).ToList<long>().ToArray(), (IUserSession) session);
                    foreach (FormInformation formInformation in source)
                    {
                      FormDesignerService.FormAccess formAccess = FormDesignerService.FormAccess.faHidden;
                      formVisibility.TryGetValue(formInformation.ID, out formAccess);
                      newDict[formInformation.CloneWithActualID(Math.Abs(formInformation.ID))] = formAccess;
                    }
                  }
                  if (newDict.Count > 0)
                  {
                    elemTypeID = num1;
                    return localCachedForms = true;
                  }
                  num1 = session.DBCache.GetObjectTypeParentID(num1);
                  localCachedForms = false;
                }
                elemTypeID = num1;
                break;
              }
              break;
            }
            break;
          case AttributableElements.Relation:
            if (elemTypeID != -1)
            {
              if (elemTypeID != -1)
              {
                cacheItem1 = (FormsCache.CacheItem) null;
                CacheHelper.CacheBaseItem<FormDesignerService.FormAccess> cacheBaseItem = (CacheHelper.CacheBaseItem<FormDesignerService.FormAccess>) null;
                source.Clear();
                FormsCache.CacheItems typesForms3 = this._srvCache.GetTypesForms(elemTypeID, kind);
                CacheHelper.CacheBaseItems<FormDesignerService.FormAccess> typesForms4 = (CacheHelper.CacheBaseItems<FormDesignerService.FormAccess>) this._usrCache.GetTypesForms(session.UserID, session.RoleID, elemTypeID, kind);
                foreach (KeyValuePair<long, FormsCache.CacheItem> keyValuePair in (ConcurrentDictionary<long, FormsCache.CacheItem>) typesForms3)
                {
                  FormsCache.CacheItem cacheItem3 = keyValuePair.Value;
                  FormInformation formInfo = cacheItem3.FormInfo;
                  long num = Math.Abs(formInfo.ID);
                  if (!typesForms4.TryGetValue(num, out cacheBaseItem))
                  {
                    if (!cacheItem3.Visible4EditUser && formInfo.CheckOutBy == session.UserID || !cacheItem3.Visible4AllUser && formInfo.CheckOutBy != session.UserID)
                    {
                      IDBObject objectActualCopy = session.GetObjectActualCopy(num, false);
                      if (objectActualCopy != null)
                      {
                        FormInformation key = new FormInformation(Math.Abs(formInfo.ID), objectActualCopy);
                        newDict[key] = FormDesignerService.FormAccess.faHidden;
                      }
                    }
                    else
                    {
                      FormInformation newFI = formInfo;
                      if (formInfo.CheckOutBy == session.UserID)
                      {
                        newFI = new FormInformation(num, string.Empty, false);
                        newFI = newDict.Keys.FirstOrDefault<FormInformation>((System.Func<FormInformation, bool>) (x => x == newFI));
                        if (newFI == null)
                        {
                          IDBObject objectActualCopy = session.GetObjectActualCopy(formInfo.ID, false);
                          if (objectActualCopy != null)
                            newFI = new FormInformation(Math.Abs(formInfo.ID), objectActualCopy);
                          else
                            continue;
                        }
                      }
                      if (newFI != null && !source.Contains(newFI))
                        source.Add(newFI);
                    }
                  }
                  else
                    newDict[cacheBaseItem.FormInfo] = cacheBaseItem.Value;
                }
                if (source.Count > 0)
                {
                  Dictionary<long, FormDesignerService.FormAccess> formVisibility = this.GetFormVisibility(source.Select<FormInformation, long>((System.Func<FormInformation, long>) (x => x.ID)).ToList<long>().ToArray(), (IUserSession) session);
                  foreach (FormInformation key in source)
                  {
                    FormDesignerService.FormAccess formAccess = FormDesignerService.FormAccess.faHidden;
                    formVisibility.TryGetValue(key.ID, out formAccess);
                    newDict[key] = formAccess;
                  }
                }
                if (newDict.Count > 0)
                {
                  localCachedForms = true;
                  break;
                }
                break;
              }
              break;
            }
            break;
        }
      }
    }
    return localCachedForms;
  }

  protected FormDesignerService.FormAccess GetFormVisibility(
    long formObjectID,
    IUserSession session)
  {
    Dictionary<long, FormDesignerService.FormAccess> formVisibility1 = this.GetFormVisibility(new long[1]
    {
      formObjectID
    }, session);
    FormDesignerService.FormAccess formVisibility2 = FormDesignerService.FormAccess.faUnknown;
    long key = formObjectID;
    ref FormDesignerService.FormAccess local = ref formVisibility2;
    formVisibility1.TryGetValue(key, out local);
    return formVisibility2;
  }

  protected Dictionary<long, FormDesignerService.FormAccess> GetFormVisibility(
    long[] formObjectIDs,
    IUserSession session)
  {
    Dictionary<long, FormDesignerService.FormAccess> formVisibility = new Dictionary<long, FormDesignerService.FormAccess>();
    if (formObjectIDs != null && formObjectIDs.Length != 0 && session is UserSession)
    {
      formVisibility = ((IEnumerable<long>) formObjectIDs).Distinct<long>().ToDictionary<long, long, FormDesignerService.FormAccess>((System.Func<long, long>) (x => x), (System.Func<long, FormDesignerService.FormAccess>) (y => FormDesignerService.FormAccess.faVisible));
      List<int> childrenIdRecursive1 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(StartupHolder.DataEditFormsType);
      List<int> intList = new List<int>();
      foreach (int objTypeID in childrenIdRecursive1)
      {
        if (objTypeID != -1)
        {
          IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeID);
          if (objectType != null && objectType.VersionsMode != ObjectVersionModes.Abstract && !intList.Contains(objectType.DefaultRelation))
            intList.Add(objectType.DefaultRelation);
        }
      }
      List<int> childrenIdRecursive2 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00002-306c-11d8-b4e9-00304f19f545"));
      childrenIdRecursive2.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00003-306c-11d8-b4e9-00304f19f545")));
      childrenIdRecursive2.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00007-306c-11d8-b4e9-00304f19f545")));
      IDBRelationCollection relationCollection = intList.Count == 1 ? session.GetRelationCollection(intList[0]) : session.GetRelationCollection(-1);
      relationCollection.ChildObjectTypes = (IList<int>) MetaDataHelper.OptimizeChildObjectTypes((IEnumerable<int>) childrenIdRecursive2);
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
      {
        new ConditionStructure(-23, RelationalOperators.In, (object) intList.ToArray(), LogicalOperators.AND, 0, false),
        new ConditionStructure(-21, RelationalOperators.In, (object) formObjectIDs, LogicalOperators.NONE, 0, false)
      }, new ColumnDescriptor[2]
      {
        new ColumnDescriptor((object) -21, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) -2, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
      });
      DataTable dataTable = relationCollection.Select(paramSet);
      if (dataTable == null)
      {
        foreach (long key in formVisibility.Keys)
          formVisibility[key] = FormDesignerService.FormAccess.faUnknown;
      }
      else if (dataTable.Rows.Count > 0)
      {
        List<long> longList = new List<long>((IEnumerable<long>) (session as UserSession).DBSecurity.GetGroupsList());
        Dictionary<long, FormDesignerService.FormAccess> dictionary = new Dictionary<long, FormDesignerService.FormAccess>();
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          long int64_1 = Convert.ToInt64(row[0]);
          long int64_2 = Convert.ToInt64(row[1]);
          bool flag = longList.Contains(int64_2);
          FormDesignerService.FormAccess formAccess = FormDesignerService.FormAccess.faHidden;
          if (dictionary.TryGetValue(int64_1, out formAccess))
          {
            if (formAccess != FormDesignerService.FormAccess.faVisible & flag)
              dictionary[int64_1] = FormDesignerService.FormAccess.faVisible;
          }
          else
            dictionary.Add(int64_1, flag ? FormDesignerService.FormAccess.faVisible : FormDesignerService.FormAccess.faHidden);
        }
        foreach (KeyValuePair<long, FormDesignerService.FormAccess> keyValuePair in dictionary)
          formVisibility[keyValuePair.Key] = keyValuePair.Value;
      }
    }
    return formVisibility;
  }

  protected List<FormInformation> UpdateForms(
    IDBAttributable parent,
    IDBRelation parentRelation,
    int typeIDWithForm,
    AttributableElements kind,
    Dictionary<FormInformation, FormDesignerService.FormAccess> dict,
    IUserSession session,
    bool bHasOwnForms)
  {
    long userID = session.UserID;
    long roleId = session.RoleID;
    List<FormInformation> list = dict.Where<KeyValuePair<FormInformation, FormDesignerService.FormAccess>>((System.Func<KeyValuePair<FormInformation, FormDesignerService.FormAccess>, bool>) (x => x.Value == FormDesignerService.FormAccess.faVisible)).Select<KeyValuePair<FormInformation, FormDesignerService.FormAccess>, FormInformation>((System.Func<KeyValuePair<FormInformation, FormDesignerService.FormAccess>, FormInformation>) (x => x.Key)).ToList<FormInformation>().Select<FormInformation, FormInformation>((System.Func<FormInformation, FormInformation>) (x => x.CloneWithActualID(x.CheckOutBy != userID ? x.ID : -x.ID))).ToList<FormInformation>();
    Guid typeGuid = kind == AttributableElements.Object ? MetaDataHelper.GetObjectTypeGuid(parent.TypeID) : MetaDataHelper.GetRelationTypeGuid(parent.TypeID);
    Dictionary<FormInformation, Tuple<FormOrderPriority, int>> dict1 = this.OrderTypeFormsForHandlers(session, typeGuid, list);
    UpdateHandlerEventArgs args = new UpdateHandlerEventArgs(parent, parentRelation, kind, dict1);
    FormDesignerService.ElementInfo key = new FormDesignerService.ElementInfo(parent.TypeID, kind);
    List<UpdateHandlerInfo> collection = (List<UpdateHandlerInfo>) null;
    List<UpdateHandlerInfo> updateHandlerInfoList = new List<UpdateHandlerInfo>();
    if (this._handlers.TryGetValue(key, out collection) && collection != null)
      updateHandlerInfoList.AddRange((IEnumerable<UpdateHandlerInfo>) collection);
    updateHandlerInfoList.AddRange(kind == AttributableElements.Object ? (IEnumerable<UpdateHandlerInfo>) this._handlersForAllObjectTypes : (IEnumerable<UpdateHandlerInfo>) this._handlersForAllRelationTypes);
    updateHandlerInfoList.Sort((IComparer<UpdateHandlerInfo>) new FormDesignerService.UpdateHandlerInfoComparer());
    foreach (UpdateHandlerInfo updateHandlerInfo in updateHandlerInfoList)
    {
      updateHandlerInfo.Handler((object) this, args);
      if (!args.ContinueProcessing)
        break;
    }
    List<FormInformation> formInformationList = this.OrderFormsAfterHandlers(args);
    if (dict.Count > 0)
    {
      OnlineCacheBase<int, FormDesignerService.FormAccess>.CachedForms dict2 = new OnlineCacheBase<int, FormDesignerService.FormAccess>.CachedForms(dict.Count);
      foreach (KeyValuePair<FormInformation, FormDesignerService.FormAccess> keyValuePair in dict)
        dict2.Add(keyValuePair.Key.ID, new CacheHelper.CacheBaseItem<FormDesignerService.FormAccess>(keyValuePair.Key, keyValuePair.Value));
      if (args.StoreInTypesCache)
      {
        this._usrCache.CleanCache(userID, roleId, typeIDWithForm, kind);
        this._usrCache.Add(userID, roleId, typeIDWithForm, kind, dict2);
      }
      else
        this._usrCache.Add(userID, roleId, typeIDWithForm, kind, dict2);
    }
    if (args.StoreInVersionCache)
    {
      VersionCacheItem versionCacheItem = (VersionCacheItem) null;
      long relationId = parentRelation != null ? parentRelation.RelationID : 0L;
      if (parent is IDBObject dbObject)
        versionCacheItem = !dbObject.IsCreationMode ? new VersionCacheItem(dbObject.ObjectID, relationId) : (VersionCacheItem) null;
      else if (parent is IDBRelation dbRelation)
        versionCacheItem = new VersionCacheItem(dbRelation.RelationID, relationId);
      if (versionCacheItem != null)
      {
        this._usrVerCache.CleanCache(userID, roleId, versionCacheItem, kind);
        OnlineCacheBase<VersionCacheItem, FormDesignerService.FormAccess>.CachedForms dict3 = new OnlineCacheBase<VersionCacheItem, FormDesignerService.FormAccess>.CachedForms(dict.Count);
        foreach (FormInformation formInformation in formInformationList)
        {
          FormInformation formInfo = formInformation.CloneWithActualID(Math.Abs(formInformation.ID));
          dict3.Add(formInfo.ID, new CacheHelper.CacheBaseItem<FormDesignerService.FormAccess>(formInfo, FormDesignerService.FormAccess.faVisible));
        }
        this._usrVerCache.Add(userID, roleId, versionCacheItem, kind, dict3);
      }
    }
    return formInformationList;
  }

  protected List<FormInformation> UpdateForms(
    int parentTypeID,
    int typeIDWithForm,
    AttributableElements kind,
    Dictionary<FormInformation, FormDesignerService.FormAccess> dict,
    IUserSession session,
    bool bHasOwnForms)
  {
    long userId = session.UserID;
    long roleId = session.RoleID;
    List<FormInformation> list = dict.Where<KeyValuePair<FormInformation, FormDesignerService.FormAccess>>((System.Func<KeyValuePair<FormInformation, FormDesignerService.FormAccess>, bool>) (x => x.Value == FormDesignerService.FormAccess.faVisible)).Select<KeyValuePair<FormInformation, FormDesignerService.FormAccess>, FormInformation>((System.Func<KeyValuePair<FormInformation, FormDesignerService.FormAccess>, FormInformation>) (x => x.Key)).ToList<FormInformation>();
    Guid typeGuid = kind == AttributableElements.Object ? MetaDataHelper.GetObjectTypeGuid(parentTypeID) : MetaDataHelper.GetRelationTypeGuid(parentTypeID);
    Dictionary<FormInformation, Tuple<FormOrderPriority, int>> dict1 = this.OrderTypeFormsForHandlers(session, typeGuid, list);
    UpdateHandlerEventArgs args = new UpdateHandlerEventArgs(parentTypeID, kind, dict1);
    FormDesignerService.ElementInfo key = new FormDesignerService.ElementInfo(parentTypeID, kind);
    List<UpdateHandlerInfo> collection = (List<UpdateHandlerInfo>) null;
    List<UpdateHandlerInfo> updateHandlerInfoList = new List<UpdateHandlerInfo>();
    if (this._handlers.TryGetValue(key, out collection) && collection != null)
      updateHandlerInfoList.AddRange((IEnumerable<UpdateHandlerInfo>) collection);
    updateHandlerInfoList.AddRange(kind == AttributableElements.Object ? (IEnumerable<UpdateHandlerInfo>) this._handlersForAllObjectTypes : (IEnumerable<UpdateHandlerInfo>) this._handlersForAllRelationTypes);
    updateHandlerInfoList.Sort((IComparer<UpdateHandlerInfo>) new FormDesignerService.UpdateHandlerInfoComparer());
    foreach (UpdateHandlerInfo updateHandlerInfo in updateHandlerInfoList)
    {
      updateHandlerInfo.Handler((object) this, args);
      if (!args.ContinueProcessing)
        break;
    }
    List<FormInformation> formInformationList = this.OrderFormsAfterHandlers(args);
    if (dict.Count > 0)
    {
      OnlineCacheBase<int, FormDesignerService.FormAccess>.CachedForms dict2 = new OnlineCacheBase<int, FormDesignerService.FormAccess>.CachedForms(dict.Count);
      foreach (KeyValuePair<FormInformation, FormDesignerService.FormAccess> keyValuePair in dict)
        dict2.Add(keyValuePair.Key.ID, new CacheHelper.CacheBaseItem<FormDesignerService.FormAccess>(keyValuePair.Key, keyValuePair.Value));
      if (args.StoreInTypesCache)
      {
        this._usrCache.CleanCache(userId, roleId, typeIDWithForm, kind);
        this._usrCache.Add(userId, roleId, typeIDWithForm, kind, dict2);
      }
      else
        this._usrCache.Add(userId, roleId, typeIDWithForm, kind, dict2);
    }
    return formInformationList;
  }

  protected List<FormInformation> UpdateForms(
    IDBAttributable parent,
    IDBRelation parentRelation,
    int typeIDWithForm,
    AttributableElements kind,
    Dictionary<FormInformation, FormDesignerService.FormAccess> dict,
    IUserSession session)
  {
    long userID = session.UserID;
    long roleId = session.RoleID;
    dict = dict ?? new Dictionary<FormInformation, FormDesignerService.FormAccess>(0);
    List<FormInformation> list = dict.Where<KeyValuePair<FormInformation, FormDesignerService.FormAccess>>((System.Func<KeyValuePair<FormInformation, FormDesignerService.FormAccess>, bool>) (x => x.Value == FormDesignerService.FormAccess.faVisible)).Select<KeyValuePair<FormInformation, FormDesignerService.FormAccess>, FormInformation>((System.Func<KeyValuePair<FormInformation, FormDesignerService.FormAccess>, FormInformation>) (x => x.Key)).ToList<FormInformation>();
    list.Select<FormInformation, FormInformation>((System.Func<FormInformation, FormInformation>) (x => x.CloneWithActualID(x.CheckOutBy != userID ? x.ID : -x.ID))).ToList<FormInformation>();
    Guid typeGuid = kind == AttributableElements.Object ? MetaDataHelper.GetObjectTypeGuid(parent.TypeID) : MetaDataHelper.GetRelationTypeGuid(parent.TypeID);
    Dictionary<FormInformation, Tuple<FormOrderPriority, int>> dict1 = this.OrderTypeFormsForHandlers(session, typeGuid, list);
    UpdateHandlerEventArgs args = new UpdateHandlerEventArgs(parent, parentRelation, kind, dict1);
    FormDesignerService.ElementInfo key = new FormDesignerService.ElementInfo(parent.TypeID, kind);
    List<UpdateHandlerInfo> source1 = (List<UpdateHandlerInfo>) null;
    List<UpdateHandlerInfo> locHandlers = new List<UpdateHandlerInfo>();
    if (this._handlers.TryGetValue(key, out source1) && source1 != null)
      locHandlers = source1.Where<UpdateHandlerInfo>((System.Func<UpdateHandlerInfo, bool>) (x => x.Order == 100 && !locHandlers.Contains(x))).ToList<UpdateHandlerInfo>();
    List<UpdateHandlerInfo> source2 = kind == AttributableElements.Object ? this._handlersForAllObjectTypes : this._handlersForAllRelationTypes;
    locHandlers.AddRange(source2.Where<UpdateHandlerInfo>((System.Func<UpdateHandlerInfo, bool>) (x => x.Order == 100 && !locHandlers.Contains(x))));
    locHandlers.Sort((IComparer<UpdateHandlerInfo>) new FormDesignerService.UpdateHandlerInfoComparer());
    foreach (UpdateHandlerInfo updateHandlerInfo in locHandlers)
    {
      updateHandlerInfo.Handler((object) this, args);
      if (!args.ContinueProcessing)
        break;
    }
    List<FormInformation> formInformationList = this.OrderFormsAfterHandlers(args);
    if (args.StoreInTypesCache && dict.Count > 0)
    {
      OnlineCacheBase<int, FormDesignerService.FormAccess>.CachedForms dict2 = new OnlineCacheBase<int, FormDesignerService.FormAccess>.CachedForms(dict.Count);
      foreach (KeyValuePair<FormInformation, FormDesignerService.FormAccess> keyValuePair in dict)
        dict2.Add(keyValuePair.Key.ID, new CacheHelper.CacheBaseItem<FormDesignerService.FormAccess>(keyValuePair.Key, keyValuePair.Value));
      this._usrCache.CleanCache(userID, roleId, typeIDWithForm, kind);
      this._usrCache.Add(userID, roleId, typeIDWithForm, kind, dict2);
    }
    if (args.StoreInVersionCache)
    {
      VersionCacheItem versionCacheItem = (VersionCacheItem) null;
      long relationId = parentRelation != null ? parentRelation.RelationID : 0L;
      if (parent is IDBObject dbObject)
        versionCacheItem = !dbObject.IsCreationMode ? new VersionCacheItem(dbObject.ObjectID, relationId) : (VersionCacheItem) null;
      else if (parent is IDBRelation dbRelation)
        versionCacheItem = new VersionCacheItem(dbRelation.RelationID, relationId);
      if (versionCacheItem != null)
      {
        this._usrVerCache.CleanCache(userID, roleId, versionCacheItem, kind);
        OnlineCacheBase<VersionCacheItem, FormDesignerService.FormAccess>.CachedForms dict3 = new OnlineCacheBase<VersionCacheItem, FormDesignerService.FormAccess>.CachedForms(formInformationList.Count);
        foreach (FormInformation formInformation in formInformationList)
        {
          FormInformation formInfo = formInformation.CloneWithActualID(Math.Abs(formInformation.ID));
          dict3.Add(formInfo.ID, new CacheHelper.CacheBaseItem<FormDesignerService.FormAccess>(formInfo, FormDesignerService.FormAccess.faVisible));
        }
        this._usrVerCache.Add(userID, roleId, versionCacheItem, kind, dict3);
      }
    }
    return formInformationList;
  }

  private Dictionary<FormInformation, Tuple<FormOrderPriority, int>> OrderTypeFormsForHandlers(
    IUserSession session,
    Guid typeGuid,
    List<FormInformation> forms)
  {
    Dictionary<FormInformation, Tuple<FormOrderPriority, int>> result = new Dictionary<FormInformation, Tuple<FormOrderPriority, int>>();
    if (forms.Count > 0)
    {
      IFormDesignerService customService = session.GetCustomService(typeof (IFormDesignerService)) as IFormDesignerService;
      int index = -1;
      if (customService != null)
      {
        Dictionary<Guid, int> displayOrderForType = customService.GetFormDisplayOrderForType(typeGuid);
        if (displayOrderForType != null)
        {
          List<FormInformation> formInformationList = new List<FormInformation>(forms.Count);
          foreach (FormInformation form in forms)
          {
            QuickObjectInfo objectInfo = session.GetObjectInfo(form.ID);
            if (!objectInfo.Empty)
            {
              if (displayOrderForType.ContainsKey(objectInfo.VersionGuid))
                form.OrderIndex = displayOrderForType[objectInfo.VersionGuid];
              else
                formInformationList.Add(form);
            }
          }
          if (formInformationList.Count > 0)
          {
            index = forms.Max<FormInformation>((System.Func<FormInformation, int>) (x => x.OrderIndex));
            formInformationList.ForEach((Action<FormInformation>) (x => x.OrderIndex = ++index));
          }
        }
        else
          forms.ForEach((Action<FormInformation>) (x => x.OrderIndex = ++index));
      }
      forms = forms.OrderBy<FormInformation, int>((System.Func<FormInformation, int>) (x => x.OrderIndex)).ToList<FormInformation>();
      index = 0;
      forms.ForEach((Action<FormInformation>) (x => result.Add(x, Tuple.Create<FormOrderPriority, int>(FormOrderPriority.Medium, index += 100))));
    }
    return result;
  }

  private List<FormInformation> OrderFormsAfterHandlers(UpdateHandlerEventArgs args)
  {
    List<FormInformation> formInformationList = new List<FormInformation>();
    Dictionary<FormInformation, Tuple<FormOrderPriority, int>> newFormInformation = args.GetNewFormInformation;
    if (newFormInformation != null)
    {
      Dictionary<FormOrderPriority, Dictionary<FormInformation, int>> dictionary = newFormInformation.GroupBy<KeyValuePair<FormInformation, Tuple<FormOrderPriority, int>>, FormOrderPriority, KeyValuePair<FormInformation, Tuple<FormOrderPriority, int>>>((System.Func<KeyValuePair<FormInformation, Tuple<FormOrderPriority, int>>, FormOrderPriority>) (x => x.Value.Item1), (System.Func<KeyValuePair<FormInformation, Tuple<FormOrderPriority, int>>, KeyValuePair<FormInformation, Tuple<FormOrderPriority, int>>>) (x => x)).ToDictionary<IGrouping<FormOrderPriority, KeyValuePair<FormInformation, Tuple<FormOrderPriority, int>>>, FormOrderPriority, Dictionary<FormInformation, int>>((System.Func<IGrouping<FormOrderPriority, KeyValuePair<FormInformation, Tuple<FormOrderPriority, int>>>, FormOrderPriority>) (x => x.Key), (System.Func<IGrouping<FormOrderPriority, KeyValuePair<FormInformation, Tuple<FormOrderPriority, int>>>, Dictionary<FormInformation, int>>) (y => y.ToDictionary<KeyValuePair<FormInformation, Tuple<FormOrderPriority, int>>, FormInformation, int>((System.Func<KeyValuePair<FormInformation, Tuple<FormOrderPriority, int>>, FormInformation>) (x => x.Key), (System.Func<KeyValuePair<FormInformation, Tuple<FormOrderPriority, int>>, int>) (x => x.Value.Item2)).OrderBy<KeyValuePair<FormInformation, int>, int>((System.Func<KeyValuePair<FormInformation, int>, int>) (x => x.Value)).ToDictionary<KeyValuePair<FormInformation, int>, FormInformation, int>((System.Func<KeyValuePair<FormInformation, int>, FormInformation>) (x => x.Key), (System.Func<KeyValuePair<FormInformation, int>, int>) (x => x.Value))));
      int num = -1;
      foreach (KeyValuePair<FormOrderPriority, Dictionary<FormInformation, int>> keyValuePair1 in dictionary)
      {
        foreach (KeyValuePair<FormInformation, int> keyValuePair2 in keyValuePair1.Value)
        {
          keyValuePair2.Key.OrderIndex = ++num;
          formInformationList.Add(keyValuePair2.Key);
        }
      }
    }
    else if (args.OldList != null)
      formInformationList = args.OldList;
    return formInformationList;
  }

  protected virtual bool GetFormsFromCache(
    long ID,
    long relationID,
    IUserSession session,
    AttributableElements elemType,
    out List<FormInformation> formInfoList)
  {
    bool formsFromCache = false;
    formInfoList = (List<FormInformation>) null;
    if ((elemType == AttributableElements.Object && ID != 0L || elemType == AttributableElements.Relation && ID != 0L) && session != null)
    {
      OnlineCacheBase<VersionCacheItem, FormDesignerService.FormAccess>.CachedForms forms = (OnlineCacheBase<VersionCacheItem, FormDesignerService.FormAccess>.CachedForms) null;
      VersionCacheItem versionCacheItem = new VersionCacheItem(ID, relationID);
      if (this._usrVerCache.GetTypesForms(session.UserID, session.RoleID, versionCacheItem, elemType, out forms) && forms != null)
      {
        formInfoList = forms.Values.Select<CacheHelper.CacheBaseItem<FormDesignerService.FormAccess>, FormInformation>((System.Func<CacheHelper.CacheBaseItem<FormDesignerService.FormAccess>, FormInformation>) (x => x.FormInfo)).ToList<FormInformation>();
        formsFromCache = true;
      }
    }
    formInfoList = formInfoList ?? new List<FormInformation>(0);
    return formsFromCache;
  }

  protected virtual bool GetFormsFromCache(
    long ID,
    long relationID,
    IUserSession session,
    AttributableElements elemType,
    out Dictionary<FormInformation, FormDesignerService.FormAccess> formInfoDict)
  {
    bool formsFromCache = false;
    formInfoDict = (Dictionary<FormInformation, FormDesignerService.FormAccess>) null;
    if ((elemType == AttributableElements.Object && ID != 0L || elemType == AttributableElements.Relation && ID != 0L) && session != null)
    {
      OnlineCacheBase<VersionCacheItem, FormDesignerService.FormAccess>.CachedForms forms = (OnlineCacheBase<VersionCacheItem, FormDesignerService.FormAccess>.CachedForms) null;
      VersionCacheItem versionCacheItem = new VersionCacheItem(ID, relationID);
      if (this._usrVerCache.GetTypesForms(session.UserID, session.RoleID, versionCacheItem, elemType, out forms) && forms != null)
      {
        formInfoDict = forms.Values.ToDictionary<CacheHelper.CacheBaseItem<FormDesignerService.FormAccess>, FormInformation, FormDesignerService.FormAccess>((System.Func<CacheHelper.CacheBaseItem<FormDesignerService.FormAccess>, FormInformation>) (x => x.FormInfo), (System.Func<CacheHelper.CacheBaseItem<FormDesignerService.FormAccess>, FormDesignerService.FormAccess>) (y => y.Value));
        formsFromCache = true;
      }
    }
    formInfoDict = formInfoDict ?? new Dictionary<FormInformation, FormDesignerService.FormAccess>(0);
    return formsFromCache;
  }

  protected virtual void ClearVersionCache()
  {
    if (this.LastClearTime + 9000000L > DateTime.Now.Ticks)
      return;
    long ticks = DateTime.Now.Ticks;
    this.LastClearTime = ticks;
    this._usrVerCache.ClearCache4Time(ticks - 18000000L);
  }

  private void On_eventHelper_RollbackEvent(IUserSession session)
  {
    bool flag = false;
    Guid sessionGuid = session.SessionGUID;
    if (this.AttrObjValueCache.ContainsKey(sessionGuid))
    {
      this.AttrObjValueCache.Remove(sessionGuid);
      flag = true;
    }
    if (this.AttrRelValueCache.ContainsKey(session.SessionGUID))
    {
      this.AttrRelValueCache.Remove(sessionGuid);
      flag = true;
    }
    if (!flag)
      return;
    this.SyncFormsCaches();
  }

  private void On_eventHelper_CommitEvent(IUserSession session)
  {
    bool flag = false;
    Guid sessionGuid = session.SessionGUID;
    if (this.AttrObjValueCache.ContainsKey(sessionGuid))
    {
      ValueInfo valueInfo = this.AttrObjValueCache[sessionGuid];
      List<string> addedValues = (List<string>) null;
      List<string> deletedValues = (List<string>) null;
      valueInfo.GetChangedValues(out addedValues, out deletedValues);
      IDBObject objectActualCopy = session.GetObjectActualCopy(valueInfo.FormID, false);
      if (objectActualCopy != null)
      {
        this.AddToCache(objectActualCopy, this.ParseTypes(session, addedValues, AttributableElements.Object), AttributableElements.Object);
        this.MarkAsRemoved(objectActualCopy, this.ParseTypes(session, deletedValues, AttributableElements.Object), AttributableElements.Object);
      }
      this.AttrObjValueCache.Remove(sessionGuid);
      flag = true;
    }
    if (this.AttrRelValueCache.ContainsKey(sessionGuid))
    {
      ValueInfo valueInfo = this.AttrRelValueCache[sessionGuid];
      List<string> addedValues = (List<string>) null;
      List<string> deletedValues = (List<string>) null;
      valueInfo.GetChangedValues(out addedValues, out deletedValues);
      IDBObject objectActualCopy = session.GetObjectActualCopy(valueInfo.FormID, false);
      if (objectActualCopy != null)
      {
        this.AddToCache(objectActualCopy, this.ParseTypes(session, addedValues, AttributableElements.Relation), AttributableElements.Relation);
        this.MarkAsRemoved(objectActualCopy, this.ParseTypes(session, deletedValues, AttributableElements.Relation), AttributableElements.Relation);
      }
      this.AttrRelValueCache.Remove(sessionGuid);
      flag = true;
    }
    if (!flag)
      return;
    this.SyncFormsCaches();
  }

  public void AddToCache(IDBObject iDBObj)
  {
    bool flag = false;
    if (iDBObj is DBObject dbObject)
    {
      IUserSession userSession = (IUserSession) dbObject.UserSession;
      FormInformation fi = new FormInformation(Math.Abs(iDBObj.ObjectID), iDBObj);
      IDBAttribute attributeByGuid1 = iDBObj.GetAttributeByGuid(StartupHolder.GlobalObjGuidType, false);
      int[] types1 = this.ParseTypes(userSession, attributeByGuid1, AttributableElements.Object);
      if (types1.Length != 0)
      {
        this._srvCache.Add(types1, fi, AttributableElements.Object);
        this._srvCache.ChangeCheckInInfo(fi, AttributableElements.Object);
        flag = true;
      }
      IDBAttribute attributeByGuid2 = iDBObj.GetAttributeByGuid(StartupHolder.GlobalRelGuidType, false);
      int[] types2 = this.ParseTypes(userSession, attributeByGuid2, AttributableElements.Relation);
      if (types2.Length != 0)
      {
        this._srvCache.Add(types2, fi, AttributableElements.Relation);
        this._srvCache.ChangeCheckInInfo(fi, AttributableElements.Relation);
        flag = true;
      }
    }
    if (!flag)
      return;
    this.SyncFormsCaches();
  }

  public void AddToCache(IDBObject iDBObj, IDBAttribute iDBAttr)
  {
    bool flag = false;
    if (iDBObj is DBObject dbObject && iDBAttr != null)
    {
      IUserSession userSession = (IUserSession) dbObject.UserSession;
      FormInformation fi = new FormInformation(Math.Abs(iDBObj.ObjectID), iDBObj);
      if (fi.CheckOutBy == userSession.UserID)
      {
        Guid guid = ((iDBAttr as DBAttribute).AttributeType as IDBGuid).GUID;
        AttributableElements kind;
        int[] types;
        if (guid == StartupHolder.GlobalObjGuidType)
        {
          kind = AttributableElements.Object;
          types = this.ParseTypes(userSession, iDBAttr, kind);
        }
        else
        {
          if (!(guid == StartupHolder.GlobalRelGuidType))
            return;
          kind = AttributableElements.Relation;
          types = this.ParseTypes(userSession, iDBAttr, kind);
        }
        if (types != null)
        {
          if (types.Length != 0)
            this._srvCache.Add(types, fi, kind);
          else
            this._srvCache.MarkAsRemoved(fi, kind);
          flag = true;
        }
      }
    }
    if (!flag)
      return;
    this.SyncFormsCaches();
  }

  public void ChangeFormsCaption(IDBObject iDBObj)
  {
    if (iDBObj == null)
      return;
    long userId = (iDBObj as DBObject).UserSession.UserID;
    this._usrCache.ChangeCaptionForm(userId, Math.Abs(iDBObj.ObjectID), iDBObj.Caption);
    this.ClearUserVersionCache(userId);
  }

  public void ChangeFormsCondition(IDBObject iDBObj, bool hasFormula)
  {
    if (iDBObj == null)
      return;
    FormInformation fi = new FormInformation(Math.Abs(iDBObj.ObjectID), iDBObj, hasFormula);
    long userId = (iDBObj as DBObject).UserSession.UserID;
    this._usrCache.ChangeConditionForm(userId, fi);
    this.ClearUserVersionCache(userId);
  }

  public void ChangeFormsCondition(IDBObject iDBObj, object formula)
  {
    if (iDBObj == null)
      return;
    FormInformation fi = new FormInformation(Math.Abs(iDBObj.ObjectID), iDBObj, formula);
    long userId = (iDBObj as DBObject).UserSession.UserID;
    this._usrCache.ChangeConditionForm(userId, fi);
    this.ClearUserVersionCache(userId);
  }

  public void ChangeFormsVisible(IDBObject iDBObj)
  {
    if (iDBObj == null)
      return;
    FormInformation fi = new FormInformation(Math.Abs(iDBObj.ObjectID), iDBObj, false);
    long userId = (iDBObj as DBObject).UserSession.UserID;
    this._usrCache.ChangeFormInfo(userId, fi, this.GetFormVisibility(iDBObj.ObjectID, iDBObj.Session));
    this.ClearUserVersionCache(userId);
  }

  public void ChangeFormsVisibleForUserCache(IDBObject iDBObj, int typeID, bool bValue)
  {
    if (iDBObj == null)
      return;
    long userId = (iDBObj as DBObject).UserSession.UserID;
    this._usrCache.ChangeFormInfo(userId, Math.Abs(iDBObj.ObjectID), typeID, bValue ? FormDesignerService.FormAccess.faVisible : FormDesignerService.FormAccess.faHidden);
    this.ClearUserVersionCache(userId);
  }

  public void CheckInForm(IDBObject iDBObj)
  {
    if (iDBObj == null)
      return;
    FormInformation fi = new FormInformation(iDBObj.ObjectID, iDBObj);
    this._srvCache.ChangeCheckInInfo(fi, AttributableElements.Object);
    this._srvCache.ChangeCheckInInfo(fi, AttributableElements.Relation);
    long userId = (iDBObj as DBObject).UserSession.UserID;
    this._usrCache.Remove(fi);
    this.ClearUserVersionCache();
  }

  public void CheckOutForm(IDBObject iDBObj)
  {
    if (iDBObj == null)
      return;
    FormInformation fi = new FormInformation(iDBObj.ObjectID, iDBObj);
    this._srvCache.ChangeCheckOutInfo(fi, AttributableElements.Object);
    this._srvCache.ChangeCheckOutInfo(fi, AttributableElements.Relation);
    this._usrCache.Remove(fi);
    this.ClearUserVersionCache((iDBObj as DBObject).UserSession.UserID);
  }

  public Dictionary<FormInformation, bool[]> GetFormsForObjectsType(
    int typesID,
    AttributableElements kind)
  {
    FormsCache.CacheItems typesForms = this._srvCache.GetTypesForms(typesID, kind);
    Dictionary<FormInformation, bool[]> formsForObjectsType = new Dictionary<FormInformation, bool[]>(typesForms != null ? typesForms.Count : 0);
    foreach (FormsCache.CacheItem cacheItem in (IEnumerable<FormsCache.CacheItem>) typesForms.Values)
      formsForObjectsType.Add(cacheItem.FormInfo, new bool[2]
      {
        cacheItem.Visible4AllUser,
        cacheItem.Visible4EditUser
      });
    return formsForObjectsType;
  }

  public void MarkAsRemoved(IDBObject iDBObj, int typesID, IDBAttribute iDBAttr)
  {
    if (!(iDBObj is DBObject dbObject) || iDBAttr == null)
      return;
    IUserSession userSession = (IUserSession) dbObject.UserSession;
    FormInformation fi = new FormInformation(Math.Abs(iDBObj.ObjectID), iDBObj);
    if (fi.CheckOutBy != userSession.UserID)
      return;
    Guid guid = ((iDBAttr as DBAttribute).AttributeType as IDBGuid).GUID;
    AttributableElements kind;
    if (guid == StartupHolder.GlobalObjGuidType)
    {
      kind = AttributableElements.Object;
    }
    else
    {
      if (!(guid == StartupHolder.GlobalRelGuidType))
        return;
      kind = AttributableElements.Relation;
    }
    this._srvCache.MarkAsRemoved(typesID, fi, kind);
    this.SyncFormsCaches();
  }

  public void RemoveFromCache(IDBObject iDBObj)
  {
    if (!(iDBObj is DBObject dbObject))
      return;
    FormInformation fi = new FormInformation(Math.Abs(iDBObj.ObjectID), iDBObj, false);
    if (fi.CheckOutBy != 0L && fi.CheckOutBy != dbObject.UserSession.UserID)
      return;
    this._srvCache.Remove(fi, AttributableElements.Object);
    this._srvCache.Remove(fi, AttributableElements.Relation);
    this.SyncFormsCaches();
  }

  public void RemoveFormFromUserCacheToCurrUser(IDBObject iDBObj, int typesID)
  {
    if (iDBObj == null)
      return;
    FormInformation fi = new FormInformation(Math.Abs(iDBObj.ObjectID), iDBObj);
    long userId = (iDBObj as DBObject).UserSession.UserID;
    if (typesID != -1)
      this._usrCache.CleanCache(userId, typesID, fi);
    else
      this._usrCache.CleanCache(userId, fi);
    this.ClearUserVersionCache(userId);
  }

  public void RemoveTypeFromCache(int typesID, AttributableElements kind)
  {
    this._srvCache.RemoveType(typesID, kind);
    this.SyncFormsCaches();
  }

  public void UndoCheckOutForm(IDBObject iDBObj)
  {
    if (iDBObj == null)
      return;
    FormInformation fi = new FormInformation(iDBObj.ObjectID, iDBObj);
    this._srvCache.UndoCheckOutInfo(fi, AttributableElements.Object);
    this._srvCache.UndoCheckOutInfo(fi, AttributableElements.Relation);
    this._usrCache.Remove(fi);
    this.ClearUserVersionCache((iDBObj as DBObject).UserSession.UserID);
  }

  public void ClearUserVersionCache()
  {
    if (this._usrVerCache == null)
      return;
    this._usrVerCache.CleanCache();
    this.SyncFormsCaches();
  }

  public void ClearUserVersionCache(long userID)
  {
    if (this._usrVerCache == null)
      return;
    this._usrVerCache.CleanCache(userID);
    this.SyncFormsCaches();
  }

  public ICollection<FormInformation> GetForms(
    long[] objectIDs,
    AttributableElements kind,
    Guid sessionID,
    bool checkVisibility = false)
  {
    if (objectIDs == null || objectIDs.Length == 0)
      return (ICollection<FormInformation>) new List<FormInformation>(0);
    UserSession sessionById = sessionID != Guid.Empty ? UserSession.GetSessionByID(sessionID) as UserSession : (UserSession) null;
    if (sessionById == null)
      return (ICollection<FormInformation>) new List<FormInformation>(0);
    List<long> list = ((IEnumerable<long>) objectIDs).Select<long, long>((System.Func<long, long>) (x => Math.Abs(x))).ToList<long>();
    GenericListHelper.MakeUnique<long>(list);
    List<FormsCache.CacheItem> formsById = this._srvCache.GetFormsById(list.ToArray(), kind);
    List<FormInformation> source1 = new List<FormInformation>(list.Count);
    List<long> longList = new List<long>(objectIDs.Length);
    foreach (FormsCache.CacheItem cacheItem in formsById)
    {
      if (cacheItem != null)
      {
        FormInformation formInfo = cacheItem.FormInfo;
        if (formInfo != null)
        {
          if (formInfo.CheckOutBy != sessionById.UserID)
          {
            source1.Add(formInfo);
            list.Remove(formInfo.ID);
          }
          else
          {
            list.Remove(formInfo.ID);
            longList.Add(-formInfo.ID);
          }
        }
      }
    }
    CacheHelper.CacheBaseItem<FormDesignerService.FormAccess> cacheBaseItem = (CacheHelper.CacheBaseItem<FormDesignerService.FormAccess>) null;
    foreach (KeyValuePair<OnlineCacheBase<int, FormDesignerService.FormAccess>.Key<int>, OnlineCacheBase<int, FormDesignerService.FormAccess>.CachedForms> keyValuePair in (IEnumerable<KeyValuePair<OnlineCacheBase<int, FormDesignerService.FormAccess>.Key<int>, OnlineCacheBase<int, FormDesignerService.FormAccess>.CachedForms>>) this._usrCache.CacheData)
    {
      if (keyValuePair.Key.UserID == sessionById.UserID && keyValuePair.Key.Kind == kind)
      {
        if (longList.Count != 0)
        {
          foreach (long key in longList.ToArray())
          {
            if (keyValuePair.Value.TryGetValue(key, out cacheBaseItem) && cacheBaseItem != null)
            {
              source1.Add(cacheBaseItem.FormInfo);
              longList.Remove(key);
              if (longList.Count == 0)
                break;
            }
          }
        }
        else
          break;
      }
    }
    longList.AddRange(list.Select<long, long>((System.Func<long, long>) (x => -x)));
    GenericListHelper.MakeUnique<long>(longList);
    list.AddRange((IEnumerable<long>) longList);
    if (list.Count > 0)
    {
      ColumnDescriptor[] columns = new ColumnDescriptor[5]
      {
        new ColumnDescriptor((object) -2, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
        new ColumnDescriptor((object) -50, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
        new ColumnDescriptor((object) StartupHolder.FormulaGuidType, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 0),
        new ColumnDescriptor((object) -7, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
        new ColumnDescriptor((object) -6, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
      };
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(-2, RelationalOperators.In, (object) list.ToArray(), LogicalOperators.NONE, 0, false)
      }, columns);
      DataTable source2 = sessionById.ObjectsSelect(StartupHolder.DataEditFormsType, dbRecordSetParams);
      if (source2 != null && source2.Rows.Count > 0)
        source1.AddRange((IEnumerable<FormInformation>) source2.AsEnumerable().Select<DataRow, FormInformation>((System.Func<DataRow, FormInformation>) (x => new FormInformation(Convert.ToInt64(x[0]), Convert.ToString(x[1]), x[2] != null && !(x[2] is DBNull), Convert.ToInt32(x[3]), Convert.ToInt64(x[4])))));
    }
    if (!checkVisibility)
      return (ICollection<FormInformation>) source1;
    Dictionary<long, FormDesignerService.FormAccess> formVisibiltyInfo = this.GetFormVisibility(source1.Select<FormInformation, long>((System.Func<FormInformation, long>) (item => item.ID)).ToArray<long>(), (IUserSession) sessionById);
    return source1 != null ? (ICollection<FormInformation>) source1.Where<FormInformation>((System.Func<FormInformation, bool>) (item => formVisibiltyInfo.ContainsKey(item.ID) && formVisibiltyInfo[item.ID] == FormDesignerService.FormAccess.faVisible)).ToList<FormInformation>() : (ICollection<FormInformation>) source1;
  }

  public ICollection<FormInformation> GetFormsForObject(long objectID, Guid sessionID)
  {
    return this.GetFormsForObject(objectID, 0L, sessionID);
  }

  public ICollection<FormInformation> GetFormsForObject(
    long objectID,
    long relationID,
    Guid sessionID)
  {
    List<FormInformation> formInformationList = (List<FormInformation>) null;
    UserSession sessionById = sessionID != Guid.Empty ? UserSession.GetSessionByID(sessionID) as UserSession : (UserSession) null;
    if (sessionById != null && objectID != 0L && objectID != 0L)
    {
      IDBObject parent = sessionById.GetObject(objectID, false);
      if (parent != null)
      {
        IDBRelation parentRelation = (IDBRelation) null;
        bool flag = true;
        if (relationID != 0L && relationID != -1L)
        {
          parentRelation = sessionById.GetRelation(relationID, false);
          flag = parentRelation != null;
        }
        if (flag)
        {
          Dictionary<FormInformation, FormDesignerService.FormAccess> formInfoDict = (Dictionary<FormInformation, FormDesignerService.FormAccess>) null;
          int objectType = parent.ObjectType;
          if (!parent.IsCreationMode && (sessionById.GetObjectType(objectType).Options & ObjectTypeOptions.ExtendedAudit) == ObjectTypeOptions.ExtendedAudit)
            (parent as DBSessionable).AddEvent(parent.ObjectID, ActionType.ViewCard, EventlogRecordType.AccessGranted);
          if (!this.GetFormsFromCache(objectID, relationID, (IUserSession) sessionById, AttributableElements.Object, out formInfoDict))
          {
            bool localCachedForms = this.GetLocalCachedForms(ref objectType, sessionById, AttributableElements.Object, formInfoDict);
            formInformationList = this.UpdateForms((IDBAttributable) parent, parentRelation, objectType, AttributableElements.Object, formInfoDict, (IUserSession) sessionById, localCachedForms);
          }
          else
            formInformationList = this.UpdateForms((IDBAttributable) parent, parentRelation, objectType, AttributableElements.Object, formInfoDict, (IUserSession) sessionById);
        }
      }
    }
    return (ICollection<FormInformation>) formInformationList ?? (ICollection<FormInformation>) new List<FormInformation>(0);
  }

  public ICollection<FormInformation> GetFormsForObjectType(int objectTypeID, Guid sessionID)
  {
    ICollection<FormInformation> formInformations = (ICollection<FormInformation>) null;
    if (objectTypeID != -1 && objectTypeID != -1 && sessionID != Guid.Empty && UserSession.GetSessionByID(sessionID) is UserSession sessionById)
    {
      Dictionary<FormInformation, FormDesignerService.FormAccess> dictionary = new Dictionary<FormInformation, FormDesignerService.FormAccess>();
      int elemTypeID = objectTypeID;
      bool localCachedForms = this.GetLocalCachedForms(ref elemTypeID, sessionById, AttributableElements.Object, dictionary);
      formInformations = (ICollection<FormInformation>) this.UpdateForms(objectTypeID, elemTypeID, AttributableElements.Object, dictionary, (IUserSession) sessionById, localCachedForms);
    }
    return formInformations ?? (ICollection<FormInformation>) new List<FormInformation>(0);
  }

  public ICollection<FormInformation> GetFormsForRelation(long relationID, Guid sessionID)
  {
    List<FormInformation> formInformationList = (List<FormInformation>) null;
    if (UserSession.GetSessionByID(sessionID) is UserSession sessionById && relationID != 0L && relationID != -1L)
    {
      IDBRelation relation = sessionById.GetRelation(relationID, false);
      if (relation != null)
      {
        Dictionary<FormInformation, FormDesignerService.FormAccess> formInfoDict = new Dictionary<FormInformation, FormDesignerService.FormAccess>();
        int relationType = relation.RelationType;
        if (!this.GetFormsFromCache(relationID, relationID, (IUserSession) sessionById, AttributableElements.Relation, out formInfoDict))
        {
          bool localCachedForms = this.GetLocalCachedForms(ref relationType, sessionById, AttributableElements.Relation, formInfoDict);
          formInformationList = this.UpdateForms((IDBAttributable) relation, relation, relationType, AttributableElements.Relation, formInfoDict, (IUserSession) sessionById, localCachedForms);
        }
        else
          formInformationList = this.UpdateForms((IDBAttributable) relation, relation, relationType, AttributableElements.Relation, formInfoDict, (IUserSession) sessionById);
      }
    }
    return (ICollection<FormInformation>) formInformationList ?? (ICollection<FormInformation>) new List<FormInformation>(0);
  }

  public ICollection<FormInformation> GetFormsForRelationType(int relationTypeID, Guid sessionID)
  {
    ICollection<FormInformation> formInformations = (ICollection<FormInformation>) null;
    if (relationTypeID != -1 && relationTypeID != -1 && sessionID != Guid.Empty && UserSession.GetSessionByID(sessionID) is UserSession sessionById)
    {
      Dictionary<FormInformation, FormDesignerService.FormAccess> dictionary = new Dictionary<FormInformation, FormDesignerService.FormAccess>();
      int elemTypeID = relationTypeID;
      bool localCachedForms = this.GetLocalCachedForms(ref elemTypeID, sessionById, AttributableElements.Relation, dictionary);
      formInformations = (ICollection<FormInformation>) this.UpdateForms(relationTypeID, elemTypeID, AttributableElements.Relation, dictionary, (IUserSession) sessionById, localCachedForms);
    }
    return formInformations ?? (ICollection<FormInformation>) new List<FormInformation>(0);
  }

  public DataTable GetImbaseObjectsWithForms(long imbaseObjId, Guid sessionGuid)
  {
    if (imbaseObjId == 0L || sessionGuid == Guid.Empty)
      return (DataTable) null;
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (sessionById == null)
      return (DataTable) null;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(Intermech.Imbase.Consts.FormListAttributeTypeGuid);
    if (attributeType == null)
      return (DataTable) null;
    if (MetaDataHelper.GetObjectType(Intermech.Imbase.Consts.ImbaseRootObjectTypeGUID) == null)
      return (DataTable) null;
    IDBAttribute attributeById = sessionById.GetObject(imbaseObjId, false)?.GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId);
    if (attributeById == null || attributeById.Value == DBNull.Value)
      return (DataTable) null;
    ICollection<string> source = (ICollection<string>) ImbaseHelper.CollectAllClassificators(new string[1]
    {
      attributeById.AsString
    });
    ConditionStructure[] conditions = new ConditionStructure[2]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.In, (object) source.ToArray<string>(), LogicalOperators.AND, 0, false),
      new ConditionStructure(attributeType.AttributeID, RelationalOperators.NotEmpty, (object) null, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID)
    };
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>(3)
    {
      new ColumnDescriptor((object) -2, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.DESC, 0)
    };
    if (attributeType.MultiValueMode == MultiValueModes.SingleValue || attributeType.MultiValueMode == MultiValueModes.SingleValueFromList)
      columnDescriptorList.Add(new ColumnDescriptor((object) attributeType.AttributeID, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    DBRecordSetParams rParams = new DBRecordSetParams(conditions, columnDescriptorList.ToArray());
    DataTable table = ImbaseHelper.SelectObjects(sessionById, rParams, Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS);
    if (table == null || table.Rows.Count <= 1)
      return table;
    DataView dataView = table.AsDataView();
    dataView.Sort = "cad0014d-306c-11d8-b4e9-00304f19f545 DESC ";
    return dataView.ToTable();
  }

  public List<long> GetFormsByImbaseObject(
    long imbaseObjID,
    int[] objectTypeIDs,
    DataTable objTables,
    Guid sessionGuid)
  {
    List<long> formsByImbaseObject = new List<long>();
    if (imbaseObjID == 0L || sessionGuid == Guid.Empty || objTables == null || objTables.Rows.Count == 0)
      return formsByImbaseObject;
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (sessionById == null)
      return formsByImbaseObject;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(Intermech.Imbase.Consts.FormListAttributeTypeGuid);
    if (attributeType == null)
      return formsByImbaseObject;
    DataRow[] dataRowArray = objTables.Select($"[{"cad00029-306c-11d8-b4e9-00304f19f545"}] = {imbaseObjID}");
    if (dataRowArray == null || dataRowArray.Length == 0)
      return formsByImbaseObject;
    List<Guid> guidList1 = (List<Guid>) null;
    if (objectTypeIDs != null && objectTypeIDs.Length != 0 && (attributeType.FieldType == FieldTypes.ftGuid || attributeType.FieldType == FieldTypes.ftString))
      guidList1 = ((IEnumerable<int>) objectTypeIDs).Select<int, Guid>((System.Func<int, Guid>) (x => MetaDataHelper.GetObjectTypeGuid(x))).ToList<Guid>(objectTypeIDs.Length);
    List<object> objectList = new List<object>();
    long result1 = 0;
    string empty = string.Empty;
    foreach (DataRow dataRow in dataRowArray)
    {
      if (dataRow != null)
      {
        if (attributeType.MultiValueMode == MultiValueModes.SingleValue || attributeType.MultiValueMode == MultiValueModes.SingleValueFromList)
        {
          object obj = dataRow[2];
          if (obj != null && obj != DBNull.Value)
          {
            if (attributeType.FieldType == FieldTypes.ftString)
            {
              string str = Convert.ToString(obj);
              if (!string.IsNullOrEmpty(str))
                objectList = ((IEnumerable<object>) ((IEnumerable<string>) str.Split(';')).Where<string>((System.Func<string, bool>) (x => GuidHelper.IsGuid(x)))).ToList<object>();
            }
            else
              objectList.Add(obj);
          }
        }
        else if ((attributeType.MultiValueMode == MultiValueModes.MultiValues || attributeType.MultiValueMode == MultiValueModes.MultiValuesFromList) && long.TryParse(Convert.ToString(dataRow[0]), out result1) && result1 != 0L)
        {
          IDBObject dbObject = sessionById.GetObject(result1, false);
          if (dbObject != null)
          {
            IDBAttribute attributeById = dbObject.GetAttributeByID(attributeType.AttributeID);
            if (attributeById != null && attributeById.ValuesCount != 0)
              objectList = ((IEnumerable<object>) attributeById.Values).Where<object>((System.Func<object, bool>) (x => x != null && x != DBNull.Value)).ToList<object>();
          }
        }
      }
    }
    if (objectList.Count == 0)
      return formsByImbaseObject;
    switch (attributeType.FieldType)
    {
      case FieldTypes.ftString:
      case FieldTypes.ftGuid:
        List<Guid> guidList2 = new List<Guid>();
        foreach (object obj in objectList)
        {
          string str = Convert.ToString(obj);
          if (GuidHelper.IsGuid(str))
            guidList2.Add(new Guid(str));
        }
        List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
        if (guidList1 != null && guidList1.Count != 0)
        {
          int attributeTypeId = MetaDataHelper.GetAttributeTypeID(StartupHolder.GlobalObjGuidType);
          conditionStructureList.Add(new ConditionStructure(attributeTypeId, RelationalOperators.In, (object) guidList1.ToArray(), LogicalOperators.AND, 0, false));
        }
        conditionStructureList.Add(new ConditionStructure(-12, RelationalOperators.In, (object) guidList2.ToArray(), LogicalOperators.NONE, 0, false));
        ColumnDescriptor[] columns = new ColumnDescriptor[1]
        {
          new ColumnDescriptor((object) -2, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
        };
        DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(conditionStructureList.ToArray(), columns);
        DataTable dataTable = sessionById.ObjectsSelect(StartupHolder.DataEditFormsType, dbRecordSetParams);
        if (dataTable != null)
        {
          long result2 = 0;
          IEnumerator enumerator = dataTable.Rows.GetEnumerator();
          try
          {
            while (enumerator.MoveNext())
            {
              DataRow current = (DataRow) enumerator.Current;
              if (current != null && long.TryParse(Convert.ToString(current[0]), out result2) && !formsByImbaseObject.Contains(result2))
                formsByImbaseObject.Add(result2);
            }
            break;
          }
          finally
          {
            if (enumerator is IDisposable disposable)
              disposable.Dispose();
          }
        }
        else
          break;
      case FieldTypes.ftObjectLink:
        long result3 = 0;
        foreach (object obj in objectList)
        {
          if (obj != null && long.TryParse(Convert.ToString(obj), out result3) && result3 != 0L && !formsByImbaseObject.Contains(result3))
            formsByImbaseObject.Add(result3);
        }
        if (objectTypeIDs != null && objectTypeIDs.Length != 0)
        {
          List<long> longList = formsByImbaseObject;
          formsByImbaseObject = new List<long>();
          foreach (int objectTypeId in objectTypeIDs)
          {
            FormsCache.CacheItems typesForms = this._srvCache.GetTypesForms(objectTypeId, AttributableElements.Object);
            if (typesForms != null)
            {
              foreach (CacheHelper.CacheBaseItem<bool> cacheBaseItem in (IEnumerable<FormsCache.CacheItem>) typesForms.Values)
              {
                FormInformation formInfo = cacheBaseItem.FormInfo;
                if (longList.Contains(formInfo.ID) && !formsByImbaseObject.Contains(formInfo.ID))
                  formsByImbaseObject.Add(formInfo.ID);
              }
            }
          }
          break;
        }
        break;
    }
    return formsByImbaseObject;
  }

  public void Register(int typeID, AttributableElements kind, UpdateHandlerInfo handler)
  {
    IUserSession systemSession = this.GetSystemSession("FormDS.Register");
    try
    {
      this.Register(typeID, kind, handler, systemSession);
    }
    finally
    {
      systemSession?.Logout("FormDS.Register");
    }
  }

  public void Register(
    int typeID,
    AttributableElements kind,
    UpdateHandlerInfo handler,
    IUserSession session)
  {
    if (typeID == -1 || typeID == -1)
    {
      switch (kind)
      {
        case AttributableElements.Object:
          if (this._handlersForAllObjectTypes.Contains(handler))
            break;
          this._handlersForAllObjectTypes.Add(handler);
          this._handlersForAllObjectTypes.Sort((IComparer<UpdateHandlerInfo>) new FormDesignerService.UpdateHandlerInfoComparer());
          break;
        case AttributableElements.Relation:
          if (this._handlersForAllRelationTypes.Contains(handler))
            break;
          this._handlersForAllRelationTypes.Add(handler);
          this._handlersForAllRelationTypes.Sort((IComparer<UpdateHandlerInfo>) new FormDesignerService.UpdateHandlerInfoComparer());
          break;
      }
    }
    else
    {
      FormDesignerService.ElementInfo key1 = new FormDesignerService.ElementInfo(typeID, kind);
      List<UpdateHandlerInfo> updateHandlerInfoList1 = (List<UpdateHandlerInfo>) null;
      if (this._baseHandlers.TryGetValue(key1, out updateHandlerInfoList1))
      {
        if (!updateHandlerInfoList1.Contains(handler))
          updateHandlerInfoList1.Add(handler);
      }
      else
        this._baseHandlers.Add(key1, new List<UpdateHandlerInfo>((IEnumerable<UpdateHandlerInfo>) new UpdateHandlerInfo[1]
        {
          handler
        }));
      List<int> intList = new List<int>();
      switch (kind)
      {
        case AttributableElements.Object:
          intList = this.CollectChildObjectTypes(typeID, session);
          break;
        case AttributableElements.Relation:
          intList = this.CollectChildRelationTypes(typeID);
          break;
      }
      foreach (int typeID1 in intList)
      {
        FormDesignerService.ElementInfo key2 = new FormDesignerService.ElementInfo(typeID1, kind);
        List<UpdateHandlerInfo> updateHandlerInfoList2 = (List<UpdateHandlerInfo>) null;
        if (this._handlers.TryGetValue(key2, out updateHandlerInfoList2))
        {
          if (!updateHandlerInfoList2.Contains(handler))
          {
            updateHandlerInfoList2.Add(handler);
            updateHandlerInfoList2.Sort((IComparer<UpdateHandlerInfo>) new FormDesignerService.UpdateHandlerInfoComparer());
          }
        }
        else
          this._handlers.Add(key2, new List<UpdateHandlerInfo>((IEnumerable<UpdateHandlerInfo>) new UpdateHandlerInfo[1]
          {
            handler
          }));
      }
    }
  }

  public void UpdateHandlerList()
  {
    if (this._baseHandlers.Count <= 0)
      return;
    this._handlers.Clear();
    IUserSession systemSession = this.GetSystemSession("FormDS.UpdateHandlerList");
    try
    {
      foreach (KeyValuePair<FormDesignerService.ElementInfo, List<UpdateHandlerInfo>> baseHandler in this._baseHandlers)
      {
        List<int> intList = new List<int>();
        if (baseHandler.Key.Kind == AttributableElements.Object)
          intList = this.CollectChildObjectTypes(baseHandler.Key.TypeID, systemSession);
        else if (baseHandler.Key.Kind == AttributableElements.Relation)
          intList = this.CollectChildRelationTypes(baseHandler.Key.TypeID);
        foreach (int typeID in intList)
          this._handlers[new FormDesignerService.ElementInfo(typeID, baseHandler.Key.Kind)] = baseHandler.Value;
      }
    }
    finally
    {
      systemSession?.Logout("FormDS.UpdateHandlerList");
    }
  }

  public void DeleteHandlersAfterDeleteBaseType(int typeID, AttributableElements kind)
  {
    FormDesignerService.ElementInfo key = new FormDesignerService.ElementInfo(typeID, kind);
    if (!this._baseHandlers.ContainsKey(key))
      return;
    this._baseHandlers.Remove(key);
  }

  private void AddToCache(IDBObject iDBObj, int[] typeIDs, AttributableElements kind)
  {
    if (!(iDBObj is DBObject dbObject) || typeIDs == null)
      return;
    UserSession userSession = dbObject.UserSession;
    FormInformation fi = new FormInformation(Math.Abs(iDBObj.ObjectID), iDBObj);
    long userId = userSession.UserID;
    if (fi.CheckOutBy != userId)
      return;
    if (typeIDs.Length != 0)
    {
      this._srvCache.Add(typeIDs, fi, kind);
      foreach (int typeId in typeIDs)
        this._usrCache.ChangeFormInfo(userId, Math.Abs(iDBObj.ObjectID), typeId, FormDesignerService.FormAccess.faVisible);
    }
    else
      this._srvCache.MarkAsRemoved(fi, kind);
    this.ClearUserVersionCache(userId);
  }

  private IUserSession GetSystemSession(string sessionName)
  {
    return !(ServerServices.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service) ? (IUserSession) null : service.GetSystemSessionPermanentClone(sessionName);
  }

  private void MarkAsRemoved(IDBObject iDBObj, int[] typeIDs, AttributableElements kind)
  {
    if (!(iDBObj is DBObject dbObject) || typeIDs == null)
      return;
    UserSession userSession = dbObject.UserSession;
    FormInformation fi = new FormInformation(Math.Abs(iDBObj.ObjectID), iDBObj);
    long userId = userSession.UserID;
    if (fi.CheckOutBy != userId)
      return;
    this._srvCache.MarkAsRemoved(typeIDs, fi, kind);
    if (typeIDs.Length != 0)
    {
      foreach (int typeId in typeIDs)
        this._usrCache.CleanCache(userId, typeId, fi);
    }
    else
      this._usrCache.CleanCache(userId, fi);
    this.ClearUserVersionCache(userId);
  }

  private string GetTypesSettings()
  {
    XmlDocument xmlDocument = new XmlDocument();
    XmlNode element1 = (XmlNode) xmlDocument.CreateElement("TypesSettings");
    foreach (KeyValuePair<Guid, TypeInfoHelper> keyValuePair1 in this._typeInfoDict)
    {
      XmlElement element2 = xmlDocument.CreateElement("Type");
      element2.SetAttribute("Guid", keyValuePair1.Key.ToString());
      XmlElement element3 = xmlDocument.CreateElement("FormDisplayOrder");
      foreach (KeyValuePair<Guid, int> keyValuePair2 in (IEnumerable<KeyValuePair<Guid, int>>) keyValuePair1.Value.FormsDisplayOrder)
      {
        XmlElement element4 = xmlDocument.CreateElement("Form");
        element4.SetAttribute("Guid", keyValuePair2.Key.ToString());
        element4.SetAttribute("Index", keyValuePair2.Value.ToString());
        element3.AppendChild((XmlNode) element4);
      }
      if (element3.HasChildNodes)
        element2.AppendChild((XmlNode) element3);
      if (element2.HasChildNodes)
      {
        element2.AppendChild((XmlNode) element3);
        element1.AppendChild((XmlNode) element2);
      }
    }
    return !element1.HasChildNodes ? string.Empty : element1.OuterXml;
  }

  private Guid GuidFromString(string guid)
  {
    return !GuidHelper.IsGuid(guid) ? Guid.Empty : new Guid(guid);
  }

  private void LoadConfiguration(IUserSession session)
  {
    if (session == null)
      return;
    BlobInformation config_info;
    byte[] config_file;
    session.Configurations.LoadConfigData("FormDesigner.TypesSettings", out config_info, out config_file, 0L);
    if (config_info.RealFileSize <= 0L || config_file == null || config_file.Length == 0)
      return;
    string xml = string.Empty;
    IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
    lock (this)
    {
      using (MemoryStream inStream = new MemoryStream(config_file))
      {
        inStream.Position = 0L;
        using (MemoryStream memoryStream = new MemoryStream(config_file.Length / 4))
        {
          service.UnpackStream((Stream) memoryStream, (Stream) inStream);
          memoryStream.Position = 0L;
          using (BinaryReader binaryReader = new BinaryReader((Stream) memoryStream))
            xml = binaryReader.ReadString();
        }
      }
    }
    this.ParseXML(xml);
  }

  private void ParseXML(string xml)
  {
    if (string.IsNullOrEmpty(xml))
      return;
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.InnerXml = xml;
    foreach (XmlNode childNode in xmlDocument.DocumentElement.ChildNodes)
    {
      XmlAttribute attribute1 = childNode.Attributes["Guid"];
      if (attribute1 != null)
      {
        Guid guid = this.GuidFromString(attribute1.Value);
        if (!(guid == Guid.Empty) && !this._typeInfoDict.ContainsKey(guid))
        {
          XmlNodeList xmlNodeList = childNode.SelectNodes($"{"FormDisplayOrder"}/{"Form"}");
          Dictionary<Guid, int> dict = new Dictionary<Guid, int>(xmlNodeList.Count);
          foreach (XmlNode xmlNode in xmlNodeList)
          {
            XmlAttribute attribute2 = xmlNode.Attributes["Guid"];
            if (attribute2 != null)
            {
              Guid key = this.GuidFromString(attribute2.Value);
              XmlAttribute attribute3 = xmlNode.Attributes["Index"];
              if (attribute3 != null)
              {
                int result = -1;
                if (int.TryParse(attribute3.Value, out result) && result != -1 && !dict.ContainsKey(key))
                  dict.Add(key, result);
              }
            }
          }
          if (dict.Count != 0)
            this._typeInfoDict.Add(guid, new TypeInfoHelper(guid, (IDictionary<Guid, int>) dict));
        }
      }
    }
  }

  private void SaveConfiguration()
  {
    IUserSession systemSession = this.GetSystemSession("FormDS.SaveConfiguration");
    try
    {
      if (systemSession == null)
        return;
      string typesSettings = this.GetTypesSettings();
      IDBConfigurations configurations = systemSession.Configurations;
      if (configurations == null)
        return;
      IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
      using (MemoryStream memoryStream = new MemoryStream(typesSettings.Length))
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) memoryStream))
        {
          binaryWriter.Write(typesSettings);
          binaryWriter.Flush();
          memoryStream.Position = 0L;
          using (MemoryStream outStream = new MemoryStream((int) memoryStream.Length / 2))
          {
            service.PackStream((Stream) outStream, (Stream) memoryStream, 9);
            BlobInformation config_info = new BlobInformation(outStream.Length, outStream.Length, DateTime.Now, "FormDesigner.TypesSettings", ArcMethods.ZLibPacked, string.Empty);
            configurations.WriteConfigData(config_info, outStream.ToArray(), 0L);
          }
        }
      }
    }
    finally
    {
      systemSession?.Logout("FormDS.SaveConfiguration");
    }
  }

  public void AddFormDisplayOrderForType(Guid typeGuid, Dictionary<Guid, int> dict)
  {
    if (!(typeGuid != Guid.Empty) || dict == null || dict.Count <= 0)
      return;
    if (this._typeInfoDict.ContainsKey(typeGuid))
      this._typeInfoDict[typeGuid].AddFormsDisplayOrder(dict);
    else
      this._typeInfoDict.Add(typeGuid, new TypeInfoHelper(typeGuid, (IDictionary<Guid, int>) dict));
    this.SaveConfiguration();
  }

  public void ClearFormDisplayOrderForType(Guid typeGuid)
  {
    if (!(typeGuid != Guid.Empty) || !this._typeInfoDict.ContainsKey(typeGuid))
      return;
    TypeInfoHelper typeInfoHelper = this._typeInfoDict[typeGuid];
    typeInfoHelper.ClearFormsDisplayOrder();
    if (typeInfoHelper.IsEmpty)
      this._typeInfoDict.Remove(typeGuid);
    this.SaveConfiguration();
  }

  public Dictionary<Guid, int> GetFormDisplayOrderForType(Guid typeGuid)
  {
    IDictionary<Guid, int> dictionary = (IDictionary<Guid, int>) null;
    if (typeGuid != Guid.Empty)
    {
      if (this._typeInfoDict.ContainsKey(typeGuid))
      {
        dictionary = this._typeInfoDict[typeGuid].FormsDisplayOrder;
      }
      else
      {
        foreach (Guid key in MetaDataHelper.GetObjectTypeParentsGuid(typeGuid))
        {
          if (this._typeInfoDict.ContainsKey(key))
          {
            dictionary = this._typeInfoDict[key].FormsDisplayOrder;
            break;
          }
        }
      }
    }
    return dictionary == null ? (Dictionary<Guid, int>) null : new Dictionary<Guid, int>(dictionary);
  }

  public void RemoveFormDisplayOrderForType(Guid typeGuid, List<Guid> guids)
  {
    if (!(typeGuid != Guid.Empty) || !this._typeInfoDict.ContainsKey(typeGuid))
      return;
    TypeInfoHelper typeInfoHelper = this._typeInfoDict[typeGuid];
    typeInfoHelper.RemoveFormsDisplayOrder(guids);
    if (typeInfoHelper.IsEmpty)
      this._typeInfoDict.Remove(typeGuid);
    this.SaveConfiguration();
  }

  public void SetFormDisplayOrderForType(Guid typeGuid, Dictionary<Guid, int> dict)
  {
    if (!(typeGuid != Guid.Empty) || dict == null || dict.Count <= 0)
      return;
    if (this._typeInfoDict.ContainsKey(typeGuid))
      this._typeInfoDict[typeGuid].SetFormDisplayIndexes(dict);
    else
      this._typeInfoDict.Add(typeGuid, new TypeInfoHelper(typeGuid, (IDictionary<Guid, int>) dict));
    this.SaveConfiguration();
  }

  private void SyncFormsCaches()
  {
    if (!(ServerServices.ServiceContainer.GetService(typeof (IFormsCacheSynchronizer)) is IFormsCacheSynchronizer service))
      return;
    IUserSession systemSession = this.GetSystemSession("FormDesignerService.SyncFormsCaches");
    try
    {
      Trace.WriteLine("Request for flushing forms server cache");
      service.AddEvent("0", (systemSession as UserSession).DataManager);
    }
    finally
    {
      systemSession.Logout("FormDesignerService.SyncFormsCaches");
    }
  }

  public enum FormAccess
  {
    faUnknown = -1, // 0xFFFFFFFF
    faHidden = 0,
    faVisible = 1,
  }

  internal class ElementInfo
  {
    public int TypeID { get; private set; }

    public AttributableElements Kind { get; private set; }

    public ElementInfo(int typeID, AttributableElements kind)
    {
      this.TypeID = typeID;
      this.Kind = kind;
    }

    public override int GetHashCode()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.TypeID.GetHashCode());
      stringBuilder.Append(this.Kind.GetHashCode());
      return stringBuilder.ToString().GetHashCode();
    }

    public override bool Equals(object obj)
    {
      if (!(obj is FormDesignerService.ElementInfo elementInfo))
        return base.Equals(obj);
      return this.TypeID == elementInfo.TypeID && this.Kind == elementInfo.Kind;
    }
  }

  private class UpdateHandlerInfoComparer : IComparer<UpdateHandlerInfo>
  {
    public int Compare(UpdateHandlerInfo x, UpdateHandlerInfo y)
    {
      if (x.Order < y.Order)
        return -1;
      return x.Order != y.Order ? 1 : 0;
    }
  }
}
