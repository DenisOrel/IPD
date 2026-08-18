// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Receptures.RecepturesService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase.Receptures;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Server.Receptures;

public class RecepturesService : LongLifeObject, IRecepturesService
{
  private IDictionary<ReceptureItemInfo, List<long>> _recepturesCache = (IDictionary<ReceptureItemInfo, List<long>>) new ConcurrentDictionary<ReceptureItemInfo, List<long>>();
  private IDictionary<ReceptureItemInfo, List<ReceptureItemInfo>> _compositionOfReceptures = (IDictionary<ReceptureItemInfo, List<ReceptureItemInfo>>) new ConcurrentDictionary<ReceptureItemInfo, List<ReceptureItemInfo>>();
  private RecepturesCacheSyncronizer _recepturesCacheSyncronizer;
  private bool _initialized;

  public RecepturesService()
  {
    this._recepturesCacheSyncronizer = new RecepturesCacheSyncronizer((IRecepturesService) this);
    ApplicationServices.Container.GetService<IServerSynchronizersManager>().RegisterSynchronizer((IServerSynchronizer) this._recepturesCacheSyncronizer);
  }

  private IUserSession GetSystemSession(string sessionName)
  {
    IUserSession systemSession = (IUserSession) null;
    if (ServerServices.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service)
      systemSession = service.GetSystemSessionPermanentClone(sessionName);
    return systemSession;
  }

  private List<long> GetReceptureTableIds(IUserSession session)
  {
    return session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableMixTypeID).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) -2
    })).AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[0]))).ToList<long>();
  }

  private void AddRecepture(ReceptureItemInfo receptureItem, long receptureTableId)
  {
    List<long> longList;
    if (this._recepturesCache.TryGetValue(receptureItem, out longList))
    {
      if (longList.Contains(receptureTableId))
        return;
      longList.Add(receptureTableId);
    }
    else
      this._recepturesCache.Add(receptureItem, new List<long>()
      {
        receptureTableId
      });
  }

  private void AddComponent(ReceptureItemInfo componentItem, ReceptureItemInfo receptureItem)
  {
    List<ReceptureItemInfo> receptureItemInfoList;
    if (this._compositionOfReceptures.TryGetValue(componentItem, out receptureItemInfoList))
    {
      if (receptureItemInfoList.Contains(receptureItem))
        return;
      receptureItemInfoList.Add(receptureItem);
    }
    else
      this._compositionOfReceptures.Add(componentItem, new List<ReceptureItemInfo>()
      {
        receptureItem
      });
  }

  private void CleanRecepturesTableDataFromCache(long receptureTableId)
  {
    foreach (ReceptureItemInfo receptureItemInfo in this._recepturesCache.Where<KeyValuePair<ReceptureItemInfo, List<long>>>((System.Func<KeyValuePair<ReceptureItemInfo, List<long>>, bool>) (x => x.Value.Contains(receptureTableId))).Select<KeyValuePair<ReceptureItemInfo, List<long>>, ReceptureItemInfo>((System.Func<KeyValuePair<ReceptureItemInfo, List<long>>, ReceptureItemInfo>) (x => x.Key)).ToList<ReceptureItemInfo>())
    {
      ReceptureItemInfo key = receptureItemInfo;
      this._recepturesCache[key].Remove(receptureTableId);
      if (this._recepturesCache[key].Count == 0)
      {
        this._recepturesCache.Remove(key);
        foreach (ReceptureItemInfo key1 in this._compositionOfReceptures.Where<KeyValuePair<ReceptureItemInfo, List<ReceptureItemInfo>>>((System.Func<KeyValuePair<ReceptureItemInfo, List<ReceptureItemInfo>>, bool>) (x => x.Value.IndexOf(key) != -1)).Select<KeyValuePair<ReceptureItemInfo, List<ReceptureItemInfo>>, ReceptureItemInfo>((System.Func<KeyValuePair<ReceptureItemInfo, List<ReceptureItemInfo>>, ReceptureItemInfo>) (x => x.Key)).ToList<ReceptureItemInfo>())
        {
          this._compositionOfReceptures[key1].Remove(key);
          if (this._compositionOfReceptures[key1].Count == 0)
            this._compositionOfReceptures.Remove(key1);
        }
      }
    }
  }

  private void InternalInit()
  {
    this._recepturesCache.Clear();
    this._compositionOfReceptures.Clear();
    session = (UserSession) null;
    try
    {
      if (!(this.GetSystemSession("ImbaseReceptures.InitCache") is UserSession session))
        return;
      if (Intermech.Imbase.Consts.ImbaseTableMixTypeID == -1)
        throw new Exception(LocalizationHolder.rm.GetString("RecepturesObjTypeNotFound"));
      foreach (long receptureTableId in this.GetReceptureTableIds((IUserSession) session))
      {
        try
        {
          DataSet tables = TableLoadHelper.GetTables((IUserSession) session, receptureTableId, true);
          if (tables != null)
          {
            if (tables.Tables.Contains("IMS_ATTR_TYPES"))
            {
              if (tables.Tables.Contains("IMS_DATA"))
              {
                DataTable table = tables.Tables["IMS_DATA"];
                if (table != null)
                {
                  foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
                  {
                    string keyValue1 = Convert.ToString(row[Intermech.Imbase.Consts.LinkToCompoundObjectAttGUID.ToString()]);
                    string keyValue2 = Convert.ToString(row[Intermech.Imbase.Consts.LinkToComponentOfCompositeObjectAttGuid.ToString()]);
                    long linkId1;
                    long recordId1;
                    long linkId2;
                    long recordId2;
                    if (ImbaseHelper.TryParseRecordReference((IUserSession) session, keyValue1, out linkId1, out recordId1) && ImbaseHelper.TryParseRecordReference((IUserSession) session, keyValue2, out linkId2, out recordId2))
                      this.AddComponent(new ReceptureItemInfo(linkId2, recordId2), new ReceptureItemInfo(linkId1, recordId1));
                  }
                  foreach (DataRow row in (InternalDataCollectionBase) new DataView(table).ToTable(true, Intermech.Imbase.Consts.LinkToCompoundObjectAttGUID.ToString()).Rows)
                  {
                    string keyValue = Convert.ToString(row[0]);
                    long linkId;
                    long recordId;
                    if (ImbaseHelper.TryParseRecordReference((IUserSession) session, keyValue, out linkId, out recordId))
                      this.AddRecepture(new ReceptureItemInfo(linkId, recordId), receptureTableId);
                  }
                }
              }
            }
          }
        }
        catch (Exception ex)
        {
        }
      }
      this._initialized = true;
    }
    catch (Exception ex)
    {
      session?.EventLog.AddToTrace($"При инициализации кэша таблиц составных объектов возникла ошибка: {ex.Message}{Environment.NewLine}{ex.StackTrace}.", 0, string.Empty);
    }
    finally
    {
      session?.Logout("ImbaseReceptures.InitCache");
    }
  }

  public void InitCache()
  {
    new Action(this.InternalInit).BeginInvoke((AsyncCallback) null, (object) null);
  }

  public bool RecordHasRecepture(ReceptureItemInfo recordInfo)
  {
    if (!this._initialized)
      throw new Exception(LocalizationHolder.rm.GetString("RecepturesCacheNotInitialized"));
    return this._recepturesCache.ContainsKey(recordInfo);
  }

  public List<Tuple<ReceptureItemInfo, MeasuredValue>> GetReceptureComposition(
    IUserSession session,
    ReceptureItemInfo recordInfo)
  {
    if (!this._initialized)
      throw new Exception(LocalizationHolder.rm.GetString("RecepturesCacheNotInitialized"));
    List<Tuple<ReceptureItemInfo, MeasuredValue>> receptureComposition = new List<Tuple<ReceptureItemInfo, MeasuredValue>>();
    List<long> longList;
    if (this._recepturesCache.TryGetValue(recordInfo, out longList))
    {
      foreach (long tableId in longList)
      {
        DataSet tables = TableLoadHelper.GetTables(session, tableId, true);
        if (tables != null && tables.Tables.Contains("IMS_ATTR_TYPES") && tables.Tables.Contains("IMS_DATA"))
        {
          DataTable table = tables.Tables["IMS_DATA"];
          string receptureOldKey = ImbaseHelper.MakeInternalImbaseKey(recordInfo.LinkId, recordInfo.RecordId);
          string receptureKey = ImbaseHelper.ConvertImbaseKey(session, receptureOldKey);
          foreach (DataRow dataRow in table.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToString(x[Intermech.Imbase.Consts.LinkToCompoundObjectAttGUID.ToString()]) == receptureKey || Convert.ToString(x[Intermech.Imbase.Consts.LinkToCompoundObjectAttGUID.ToString()]) == receptureOldKey)).ToList<DataRow>())
          {
            string keyValue = Convert.ToString(dataRow[Intermech.Imbase.Consts.LinkToComponentOfCompositeObjectAttGuid.ToString()]);
            MeasuredValue measuredValue = dataRow["cad00267-306c-11d8-b4e9-00304f19f545"] as MeasuredValue;
            long linkId;
            long recordId;
            if (ImbaseHelper.TryParseRecordReference(session, keyValue, out linkId, out recordId))
              receptureComposition.Add(new Tuple<ReceptureItemInfo, MeasuredValue>(new ReceptureItemInfo(linkId, recordId), measuredValue));
          }
        }
      }
    }
    return receptureComposition;
  }

  public void UpdateCacheAfterTableMixEdit(
    IUserSession session,
    long receptureTableId,
    DataTable dtData)
  {
    if (!this._initialized)
      throw new Exception(LocalizationHolder.rm.GetString("RecepturesCacheNotInitialized"));
    this.CleanRecepturesTableDataFromCache(receptureTableId);
    foreach (DataRow row in (InternalDataCollectionBase) new DataView(dtData).ToTable(true, Intermech.Imbase.Consts.LinkToCompoundObjectAttGUID.ToString()).Rows)
    {
      string keyValue = Convert.ToString(row[0]);
      long linkId;
      long recordId;
      if (ImbaseHelper.TryParseRecordReference(session, keyValue, out linkId, out recordId))
        this.AddRecepture(new ReceptureItemInfo(linkId, recordId), receptureTableId);
    }
    foreach (DataRow row in (InternalDataCollectionBase) dtData.Rows)
    {
      string keyValue1 = Convert.ToString(row[Intermech.Imbase.Consts.LinkToCompoundObjectAttGUID.ToString()]);
      string keyValue2 = Convert.ToString(row[Intermech.Imbase.Consts.LinkToComponentOfCompositeObjectAttGuid.ToString()]);
      long linkId1;
      long recordId1;
      long linkId2;
      long recordId2;
      if (ImbaseHelper.TryParseRecordReference(session, keyValue1, out linkId1, out recordId1) && ImbaseHelper.TryParseRecordReference(session, keyValue2, out linkId2, out recordId2))
        this.AddComponent(new ReceptureItemInfo(linkId2, recordId2), new ReceptureItemInfo(linkId1, recordId1));
    }
  }

  public void UpdateCacheOnAnotherServers(IUserSession session, long receptureTableId)
  {
    this._recepturesCacheSyncronizer?.AddEvent(receptureTableId.ToString(), ((UserSession) session).DataManager);
  }

  private void OnBeforePurgeObjectEvent(IDBObject sender, IUserSession session)
  {
    if (sender == null || session == null || sender.ObjectType != Intermech.Imbase.Consts.ImbaseTableMixTypeID)
      return;
    this.CleanRecepturesTableDataFromCache(sender.ObjectID);
  }

  private void OnBeforeNextLCStepEvent(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    if (sender == null || nextstep == null || session == null || nextstep.LevelID != session.IdentHelper.DeletedID || sender.ObjectType != Intermech.Imbase.Consts.ImbaseTableMixTypeID)
      return;
    this.CleanRecepturesTableDataFromCache(sender.ObjectID);
  }

  public void SubscribeOnSystemEvents(IEventLogHelper elh)
  {
    elh.BeforeNextLCStepEvent += new NextLCStepHandler(this.OnBeforeNextLCStepEvent);
    elh.BeforePurgeObjectEvent += new ObjectEventHandler(this.OnBeforePurgeObjectEvent);
  }
}
