// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Server.AutoSelectionCache.AutoSelectionRuleCacheService
// Assembly: Intermech.AutoSelection.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 89DFCE1C-C473-4D66-BEC0-EFA8A5FDFD64
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.AutoSelection.Server.dll

using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.AutoSelection.AutoSelectionCache;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;

#nullable disable
namespace Intermech.AutoSelection.Server.AutoSelectionCache;

internal class AutoSelectionRuleCacheService : LongLifeObject, IAutoSelectionRuleCacheService
{
  private bool _objTypesLoaded;
  private readonly List<int> _objectTypeIds = new List<int>();
  private readonly ConcurrentDictionary<long, AutoSelectionRuleCacheService.ImbaseCacheRec> _imbaseCache;
  private readonly ConcurrentDictionary<long, AutoSelectionRuleCache> _ruleCache;
  private readonly ConcurrentDictionary<int, List<AutoSelectionRuleCache>> _ruleTypeCache;

  private void InitializeData()
  {
    if (ServerServices.GetService(typeof (IEventLogHelper)) is IEventLogHelper service)
      service.AfterCacheReload += new Intermech.Interfaces.Server.CacheReloadHandler(this.CacheReloadHandler);
    this.LoadCache();
  }

  private void CacheReloadHandler(IDbManager db)
  {
    this.ClearCache(true);
    this.LoadCache();
  }

  private void Imbase_RuleSetAttributes(IDBObject imbaseObject, AttributeValues[] attrValues)
  {
    if (imbaseObject == null || attrValues == null || attrValues.Length == 0)
      return;
    if (!imbaseObject.ReadOnly)
      imbaseObject.SetAttributesValues(attrValues);
    else if (imbaseObject.ObjectModifyMode != ObjectModifyModes.Checkout)
    {
      imbaseObject.SetAttributesValues(attrValues);
    }
    else
    {
      bool flag = false;
      foreach (AttributeValues attrValue in attrValues)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrValue.AttributeID);
        if (attributeType != null)
        {
          IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(imbaseObject.ObjectType, attrValue.AttributeID);
          flag = attribute4ObjectType != null ? attribute4ObjectType.IsContent || (attribute4ObjectType.Options & AttributeOptions.ModifyInBase) != AttributeOptions.ModifyInBase : attributeType.IsContent || (attributeType.Options & AttributeOptions.ModifyInBase) != AttributeOptions.ModifyInBase;
          if (flag)
            break;
        }
      }
      if (flag)
        imbaseObject = imbaseObject.CheckOut();
      imbaseObject.SetAttributesValues(attrValues);
      if (!flag)
        return;
      imbaseObject.CheckIn();
    }
  }

  private void Imbase_RulesRegister(List<long> ruleIdList, long imbaseObjectId, Guid sessionGuid)
  {
    if (ruleIdList == null || ruleIdList.Count == 0)
      return;
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (sessionById == null)
      return;
    foreach (long ruleId in ruleIdList)
      this.RuleCacheAdd(ruleId, sessionById);
    List<long> ruleIdList1 = new List<long>();
    AutoSelectionRuleCacheService.ImbaseCacheRec imbaseCacheRec;
    if (this._imbaseCache.TryGetValue(imbaseObjectId, out imbaseCacheRec) && imbaseCacheRec.RuleList != null && (imbaseCacheRec.OwnerObject == 0L || imbaseCacheRec.OwnerObject == imbaseObjectId))
      ruleIdList1.AddRange((IEnumerable<long>) imbaseCacheRec.RuleList);
    foreach (long ruleId in ruleIdList)
    {
      if (!ruleIdList1.Contains(ruleId))
        ruleIdList1.Add(ruleId);
    }
    this.Imbase_RuleUpdate(imbaseObjectId, ruleIdList1, sessionById);
  }

  private void Imbase_RulesUnregister(List<long> ruleIdList, long imbaseObjectId, Guid sessionGuid)
  {
    if (ruleIdList == null || ruleIdList.Count == 0)
      return;
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (sessionById == null)
      return;
    List<long> ruleIdList1 = new List<long>();
    AutoSelectionRuleCacheService.ImbaseCacheRec imbaseCacheRec;
    if (this._imbaseCache.TryGetValue(imbaseObjectId, out imbaseCacheRec) && imbaseCacheRec.RuleList != null && imbaseCacheRec.OwnerObject == 0L)
      ruleIdList1.AddRange((IEnumerable<long>) imbaseCacheRec.RuleList);
    foreach (long ruleId in ruleIdList)
    {
      if (ruleIdList1.Contains(ruleId))
        ruleIdList1.Remove(ruleId);
    }
    this.Imbase_RuleUpdate(imbaseObjectId, ruleIdList1, sessionById);
  }

  private void Imbase_RulesUpdate(List<long> ruleIdList, long imbaseObjectId, Guid sessionGuid)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (sessionById == null)
      return;
    this.Imbase_UpdateCache(imbaseObjectId, ruleIdList);
    this.Imbase_RuleUpdate(imbaseObjectId, ruleIdList, sessionById);
  }

  private void Imbase_RuleUpdate(long imbaseObjectId, List<long> ruleIdList, IUserSession session)
  {
    if (imbaseObjectId == 0L)
      return;
    IDBObject objectActualCopy = session.GetObjectActualCopy(imbaseObjectId, false);
    if (objectActualCopy == null)
      return;
    int attributeId = MetaDataHelper.GetAttributeID((object) AutoSelectionConsts.attrTypeRuleLinkGuid);
    List<AttributeValues> attributeValuesList = new List<AttributeValues>()
    {
      ruleIdList.Count != 0 ? new AttributeValues(attributeId, (object) ruleIdList.Cast<object>().ToArray<object>()) : new AttributeValues(attributeId, (object) DBNull.Value)
    };
    this.Imbase_RuleSetAttributes(objectActualCopy, attributeValuesList.ToArray());
    this.Imbase_UpdateCache(imbaseObjectId, ruleIdList);
  }

  private void Imbase_UpdateCache(long imbaseObjectId, List<long> ruleIdList, bool updateChildMode = false)
  {
    AutoSelectionRuleCacheService.ImbaseCacheRec imbaseCacheRec1;
    if (!this._imbaseCache.TryGetValue(imbaseObjectId, out imbaseCacheRec1))
    {
      AutoSelectionRuleCacheService.ImbaseCacheRec imbaseCacheRec2 = new AutoSelectionRuleCacheService.ImbaseCacheRec(0L, ruleIdList);
      this._imbaseCache.TryAdd(imbaseObjectId, imbaseCacheRec2);
    }
    else
    {
      if (imbaseCacheRec1.RuleList == null)
      {
        imbaseCacheRec1.RuleList = new List<long>((IEnumerable<long>) ruleIdList);
      }
      else
      {
        imbaseCacheRec1.RuleList.Clear();
        imbaseCacheRec1.RuleList.AddRange((IEnumerable<long>) ruleIdList);
      }
      if (updateChildMode)
        return;
      long ownerObjectId = imbaseCacheRec1.OwnerObject;
      if (ownerObjectId != 0L && ownerObjectId != imbaseObjectId)
      {
        this._imbaseCache.Where<KeyValuePair<long, AutoSelectionRuleCacheService.ImbaseCacheRec>>((System.Func<KeyValuePair<long, AutoSelectionRuleCacheService.ImbaseCacheRec>, bool>) (item => item.Key != imbaseObjectId && item.Value.OwnerObject == ownerObjectId)).InvokeForAll<KeyValuePair<long, AutoSelectionRuleCacheService.ImbaseCacheRec>>((Action<KeyValuePair<long, AutoSelectionRuleCacheService.ImbaseCacheRec>>) (item =>
        {
          item.Value.OwnerObject = 0L;
          item.Value.RuleList = (List<long>) null;
        }));
        imbaseCacheRec1.OwnerObject = 0L;
      }
      this._imbaseCache.Where<KeyValuePair<long, AutoSelectionRuleCacheService.ImbaseCacheRec>>((System.Func<KeyValuePair<long, AutoSelectionRuleCacheService.ImbaseCacheRec>, bool>) (item => item.Value.OwnerObject == imbaseObjectId)).InvokeForAll<KeyValuePair<long, AutoSelectionRuleCacheService.ImbaseCacheRec>>((Action<KeyValuePair<long, AutoSelectionRuleCacheService.ImbaseCacheRec>>) (item => this.Imbase_UpdateCache(item.Key, ruleIdList, true)));
    }
  }

  private void Imbase_FillHierarchy(
    long imbaseObjectId,
    Dictionary<long, List<long>> hierarchyList,
    DataTable hierarchyData)
  {
    DataRow[] dataRowArray1 = hierarchyData.Select($"{"F_OBJECT_ID"} = '{imbaseObjectId}'");
    if (dataRowArray1.Length == 0)
      return;
    string str1 = dataRowArray1[0]["F_PATH"].ToString();
    if (str1.Length < 4)
      return;
    string str2 = str1.Substring(0, str1.Length - 2);
    DataRow[] dataRowArray2 = hierarchyData.Select($"F_PATH = '{str2}'");
    if (dataRowArray2.Length == 0)
      return;
    List<long> longList;
    if (!hierarchyList.TryGetValue(imbaseObjectId, out longList))
    {
      longList = new List<long>();
      hierarchyList.Add(imbaseObjectId, longList);
    }
    foreach (DataRow dataRow in dataRowArray2)
    {
      long result;
      long.TryParse(dataRow["F_OBJECT_ID"].ToString(), out result);
      if (result != 0L)
      {
        longList.Add(result);
        this.Imbase_FillHierarchy(result, hierarchyList, hierarchyData);
      }
    }
  }

  private AutoSelectionRuleCacheService.ImbaseCacheRec GetRuleByImbaseObjOnly(
    long imbaseObjectId,
    IUserSession session)
  {
    AutoSelectionRuleCacheService.ImbaseCacheRec ruleByImbaseObjOnly1;
    if (this._imbaseCache.TryGetValue(imbaseObjectId, out ruleByImbaseObjOnly1))
      return ruleByImbaseObjOnly1;
    AutoSelectionRuleCacheService.ImbaseCacheRec ruleByImbaseObjOnly2 = new AutoSelectionRuleCacheService.ImbaseCacheRec(0L);
    this._imbaseCache.TryAdd(imbaseObjectId, ruleByImbaseObjOnly2);
    IDBObject dbObject = session.GetObject(imbaseObjectId, false);
    if (dbObject == null)
      return ruleByImbaseObjOnly2;
    IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(AutoSelectionConsts.attrTypeRuleLinkGuid, false);
    if (attributeByGuid == null || attributeByGuid.Values.Length == 0)
      return ruleByImbaseObjOnly2;
    List<long> objectIDs = new List<long>();
    foreach (object obj in attributeByGuid.Values)
    {
      long result;
      try
      {
        long.TryParse(obj.ToString(), out result);
      }
      catch (FormatException ex)
      {
        result = 0L;
      }
      if (result != 0L)
        objectIDs.Add(result);
    }
    if (objectIDs.Count == 0)
      return ruleByImbaseObjOnly2;
    List<ObjInfoItem> objectInfoList = ObjInfoHelper.GetObjectInfoList((IEnumerable<long>) objectIDs);
    ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) objectInfoList, session);
    List<long> list = objectInfoList.Where<ObjInfoItem>((System.Func<ObjInfoItem, bool>) (item => item.ObjTypeID != -1)).Select<ObjInfoItem, long>((System.Func<ObjInfoItem, long>) (item => item.ObjectID)).ToList<long>();
    if (list.Count != 0)
      ruleByImbaseObjOnly2.RuleList = list;
    return ruleByImbaseObjOnly2;
  }

  private void Types_LoadObjectTypeIDS(IUserSession session)
  {
    if (!(session is UserSession userSession) || !session.IsAdmin && !userSession.IsSystemSession)
      return;
    List<int> collection = new List<int>();
    BlobInformation config_info;
    byte[] config_file;
    session.Configurations.LoadConfigData("AutoSelectionObjectTypes", out config_info, out config_file, 0L);
    if (config_info.RealFileSize > 0L)
    {
      using (MemoryStream inStream = new MemoryStream(config_file))
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load((Stream) inStream);
        foreach (XmlNode childNode in xmlDocument.FirstChild.ChildNodes)
        {
          int result;
          if (childNode.Name.Equals("ObjectType") && int.TryParse(childNode.InnerText, out result) && result != 0)
            collection.Add(result);
        }
      }
    }
    this._objectTypeIds.Clear();
    this._objectTypeIds.AddRange((IEnumerable<int>) collection);
    this._objTypesLoaded = true;
  }

  private void Types_SaveObjetTypeIDS(List<int> objectTypeIDs, IUserSession session)
  {
    if (!(session is UserSession userSession) || !session.IsAdmin && !userSession.IsSystemSession || objectTypeIDs == null)
      return;
    using (MemoryStream outStream = new MemoryStream())
    {
      XmlDocument xmlDocument = new XmlDocument();
      XmlNode element1 = (XmlNode) xmlDocument.CreateElement("AutoSelectionObjectTypes");
      foreach (int objectTypeId in objectTypeIDs)
      {
        XmlNode element2 = (XmlNode) xmlDocument.CreateElement("ObjectType");
        element2.InnerText = objectTypeId.ToString();
        element1.AppendChild(element2);
      }
      xmlDocument.AppendChild(element1);
      xmlDocument.Save((Stream) outStream);
      session.Configurations.WriteConfigData(new BlobInformation(outStream.Length, outStream.Length, DateTime.Now, "AutoSelectionObjectTypes", ArcMethods.NotPacked, string.Empty), outStream.ToArray(), 0L);
    }
    this._objectTypeIds.Clear();
    if (objectTypeIDs.Count != 0)
    {
      objectTypeIDs.Sort();
      this._objectTypeIds.Add(objectTypeIDs[0]);
      for (int index = 1; index < objectTypeIDs.Count; ++index)
      {
        if (objectTypeIDs[index - 1] != objectTypeIDs[index])
          this._objectTypeIds.Add(objectTypeIDs[index]);
      }
    }
    this._objTypesLoaded = true;
  }

  private void Types_RulesRegister(List<long> ruleIdList, long typeId, Guid sessionGuid)
  {
    if (ruleIdList == null || ruleIdList.Count == 0)
      return;
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (sessionById == null)
      return;
    foreach (long ruleId in ruleIdList)
      this.RuleCacheAdd(ruleId, sessionById);
    if (!this._ruleTypeCache.ContainsKey((int) typeId))
      return;
    List<long> ruleIdList1 = this.GetRulesByObjectType((int) typeId) ?? new List<long>();
    foreach (long ruleId in ruleIdList)
    {
      if (!ruleIdList1.Contains(ruleId))
        ruleIdList1.Add(ruleId);
    }
    this.Type_RuleUpdate((int) typeId, ruleIdList1, sessionById);
  }

  private void Types_RulesUnregister(List<long> ruleIdList, long typeId, Guid sessionGuid)
  {
    if (ruleIdList == null || ruleIdList.Count == 0)
      return;
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (sessionById == null)
      return;
    foreach (long ruleId in ruleIdList)
      this.RuleCacheAdd(ruleId, sessionById);
    List<long> ruleIdList1 = this.GetRulesByObjectType((int) typeId) ?? new List<long>();
    lock (this)
    {
      foreach (long ruleId in ruleIdList)
      {
        if (ruleIdList1.Contains(ruleId))
          ruleIdList1.Remove(ruleId);
      }
    }
    this.Type_RuleRemove((int) typeId, ruleIdList1, sessionById);
    this.Type_RuleUpdate((int) typeId, ruleIdList1, sessionById);
  }

  private void Types_RulesUpdate(List<long> ruleIdList, long typeId, Guid sessionGuid)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (sessionById == null)
      return;
    this.Type_RuleUpdate((int) typeId, ruleIdList, sessionById);
  }

  private void Type_RuleUpdate(int typeId, List<long> ruleIdList, IUserSession session)
  {
    if (typeId == -1 || ruleIdList == null || ruleIdList.Count == 0)
      return;
    lock (this)
    {
      int attributeId1 = MetaDataHelper.GetAttributeID((object) "cad00202-306c-11d8-b4e9-00304f19f545");
      int attributeId2 = MetaDataHelper.GetAttributeID((object) AutoSelectionConsts.attrTypeTypeLink);
      for (int index = 0; index < ruleIdList.Count; ++index)
      {
        IDBObject objectActualCopy = session.GetObjectActualCopy(ruleIdList[index], false);
        if (objectActualCopy == null)
          return;
        List<AttributeValues> attributeValuesList = new List<AttributeValues>()
        {
          new AttributeValues(attributeId1, (object) index),
          new AttributeValues(attributeId2, (object) 1)
        };
        this.Imbase_RuleSetAttributes(objectActualCopy, attributeValuesList.ToArray());
      }
    }
    this.Type_UpdateCache(typeId, ruleIdList);
  }

  private void Type_RuleRemove(int typeId, List<long> ruleIdList, IUserSession session)
  {
    if (ruleIdList == null || ruleIdList.Count == 0)
      return;
    int attributeId1 = MetaDataHelper.GetAttributeID((object) "cad00202-306c-11d8-b4e9-00304f19f545");
    int attributeId2 = MetaDataHelper.GetAttributeID((object) AutoSelectionConsts.attrTypeTypeLink);
    foreach (long ruleId in ruleIdList)
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(ruleId, false);
      if (objectActualCopy == null)
        return;
      List<AttributeValues> attributeValuesList = new List<AttributeValues>()
      {
        new AttributeValues(attributeId1, (object) -1),
        new AttributeValues(attributeId2, (object) DBNull.Value)
      };
      this.Imbase_RuleSetAttributes(objectActualCopy, attributeValuesList.ToArray());
    }
    List<AutoSelectionRuleCache> selectionRuleCacheList;
    if (!this._ruleTypeCache.TryGetValue(typeId, out selectionRuleCacheList) || selectionRuleCacheList == null)
      return;
    foreach (AutoSelectionRuleCache selectionRuleCache in selectionRuleCacheList)
    {
      if (ruleIdList.Contains(selectionRuleCache.SelectionRuleID))
      {
        selectionRuleCache.TypeLinked = 0;
        selectionRuleCache.OrderID = -1L;
      }
    }
    selectionRuleCacheList.Sort((IComparer<AutoSelectionRuleCache>) new AutoSelectionRuleCacheComparer());
  }

  private void Type_UpdateCache(int typeId, List<long> ruleIdList)
  {
    List<AutoSelectionRuleCache> selectionRuleCacheList;
    if (typeId == -1 || ruleIdList == null || ruleIdList.Count == 0 || !this._ruleTypeCache.TryGetValue(typeId, out selectionRuleCacheList) || selectionRuleCacheList == null)
      return;
    foreach (AutoSelectionRuleCache selectionRuleCache in selectionRuleCacheList)
    {
      int num = ruleIdList.IndexOf(selectionRuleCache.SelectionRuleID);
      selectionRuleCache.TypeLinked = num == -1 ? 0 : 1;
      selectionRuleCache.OrderID = (long) num;
    }
    selectionRuleCacheList.Sort((IComparer<AutoSelectionRuleCache>) new AutoSelectionRuleCacheComparer());
  }

  public AutoSelectionRuleCacheService()
  {
    this._ruleCache = new ConcurrentDictionary<long, AutoSelectionRuleCache>();
    this._ruleTypeCache = new ConcurrentDictionary<int, List<AutoSelectionRuleCache>>();
    this._imbaseCache = new ConcurrentDictionary<long, AutoSelectionRuleCacheService.ImbaseCacheRec>();
    this.InitializeData();
  }

  public List<int> GetObjectTypes(Guid sessionGuid)
  {
    if (this._objTypesLoaded)
      return new List<int>((IEnumerable<int>) this._objectTypeIds);
    bool flag = false;
    IUserSession session = UserSession.GetSessionByID(sessionGuid);
    try
    {
      if (session == null || !session.IsAdmin)
      {
        flag = true;
        session = AutoSelectionServerCache.DBTimedEvents.GetSystemSessionTemporaryClone("AutoSelect.GetObjectTypes1");
      }
      this.Types_LoadObjectTypeIDS(session);
      return new List<int>((IEnumerable<int>) this._objectTypeIds);
    }
    finally
    {
      if (flag && session != null)
        session.Logout("AutoSelect.GetObjectTypes1");
    }
  }

  public void SetObjectTypes(List<int> objectTypeIDs, Guid sessionGuid)
  {
    if (!this._objTypesLoaded)
      return;
    bool flag = false;
    IUserSession session = UserSession.GetSessionByID(sessionGuid);
    try
    {
      if (session == null || !session.IsAdmin)
      {
        flag = true;
        session = AutoSelectionServerCache.DBTimedEvents.GetSystemSessionTemporaryClone("AutoSelect.GetObjectTypes2");
      }
      this.Types_SaveObjetTypeIDS(objectTypeIDs, session);
    }
    finally
    {
      if (flag && session != null)
        session.Logout("AutoSelect.GetObjectTypes2");
    }
  }

  public List<int> GetAllRulesObjTypes()
  {
    List<int> allRulesObjTypes = new List<int>();
    if (this._ruleTypeCache == null)
      return allRulesObjTypes;
    foreach (KeyValuePair<int, List<AutoSelectionRuleCache>> keyValuePair in this._ruleTypeCache)
    {
      if (keyValuePair.Value != null && keyValuePair.Value.Count > 0)
        allRulesObjTypes.Add(keyValuePair.Key);
    }
    return allRulesObjTypes;
  }

  public List<long> GetAllRulesByObjectType(int objectTypeId)
  {
    List<long> rulesByObjectType = new List<long>();
    List<AutoSelectionRuleCache> source;
    if (objectTypeId == -1 || !this._ruleTypeCache.TryGetValue(objectTypeId, out source) || source == null)
      return rulesByObjectType;
    rulesByObjectType.AddRange(source.Select<AutoSelectionRuleCache, long>((System.Func<AutoSelectionRuleCache, long>) (ruleCache => ruleCache.SelectionRuleID)));
    return rulesByObjectType;
  }

  public List<long> GetRulesByObjectType(int objectType)
  {
    List<long> rulesByObjectType = new List<long>();
    List<AutoSelectionRuleCache> selectionRuleCacheList;
    if (objectType == -1 || !this._ruleTypeCache.TryGetValue(objectType, out selectionRuleCacheList))
      return rulesByObjectType;
    foreach (AutoSelectionRuleCache selectionRuleCache in selectionRuleCacheList)
    {
      if (selectionRuleCache.TypeLinked == 1)
        rulesByObjectType.Add(selectionRuleCache.SelectionRuleID);
    }
    return rulesByObjectType;
  }

  public List<long> GetRulesByObject(long objectId, Guid sessionGuid)
  {
    List<long> rulesByObject = new List<long>();
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (sessionById == null)
      return rulesByObject;
    QuickObjectInfo objectInfo = sessionById.GetObjectInfo(objectId);
    if (objectInfo.Empty)
      return rulesByObject;
    int objectTypeId = objectInfo.ObjectTypeID;
    List<AutoSelectionRuleCache> selectionRuleCacheList;
    if (!this._ruleTypeCache.TryGetValue(objectTypeId, out selectionRuleCacheList) || selectionRuleCacheList == null || selectionRuleCacheList.Count == 0)
      return rulesByObject;
    List<Guid> guidList = new List<Guid>();
    foreach (AutoSelectionRuleCache selectionRuleCache in selectionRuleCacheList)
    {
      if (!guidList.Contains(selectionRuleCache.AttributeTypeGuid))
        guidList.Add(selectionRuleCache.AttributeTypeGuid);
    }
    IMSObjectType objectType = MetaDataHelper.GetObjectType(objectTypeId);
    IDBObject dbObject = (IDBObject) null;
    foreach (Guid guid in guidList)
    {
      if (objectType != null && (objectType.AnyAttributes || MetaDataHelper.GetAttribute4ObjectType(objectType.Guid, guid) != null))
      {
        if (dbObject == null)
        {
          dbObject = sessionById.GetObject(objectId, false);
          if (dbObject == null)
            break;
        }
        IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(guid, false);
        if (attributeByGuid != null)
        {
          if (!attributeByGuid.Value.Equals((object) DBNull.Value))
          {
            long result;
            try
            {
              if (!long.TryParse(attributeByGuid.Value.ToString(), out result))
                result = 0L;
            }
            catch (FormatException ex)
            {
              result = 0L;
            }
            long imbaseCatalogId = 0;
            if (result != 0L)
            {
              foreach (long num in this.GetRulesByImbaseObj(result, imbaseCatalogId, sessionById.SessionGUID))
              {
                if (num != 0L && !rulesByObject.Contains(num))
                  rulesByObject.Add(num);
              }
            }
          }
        }
      }
    }
    if (rulesByObject.Count == 0)
      rulesByObject = this.GetRulesByObjectType(objectTypeId);
    return rulesByObject;
  }

  public List<long> GetRulesByImbaseObj(
    long imbaseObjectId,
    long imbaseCatalogId,
    Guid sessionGuid)
  {
    List<long> collection = new List<long>();
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (sessionById == null)
      return collection;
    AutoSelectionRuleCacheService.ImbaseCacheRec ruleByImbaseObjOnly1 = this.GetRuleByImbaseObjOnly(imbaseObjectId, sessionById);
    if (ruleByImbaseObjOnly1 == null)
      return collection;
    if (ruleByImbaseObjOnly1.RuleList != null && ruleByImbaseObjOnly1.RuleList.Count != 0)
    {
      collection.AddRange((IEnumerable<long>) ruleByImbaseObjOnly1.RuleList);
      return collection;
    }
    IImbaseServer imbaseServerService = AutoSelectionUtils.GetImbaseServerService(sessionById, true);
    DataTable foldersForObjects = imbaseServerService.GetFoldersForObjects(sessionById.SessionGUID, new long[1]
    {
      imbaseObjectId
    }, new long[1]{ imbaseCatalogId });
    if (foldersForObjects == null || foldersForObjects.Rows.Count == 0)
      return collection;
    DataRow[] dataRowArray = foldersForObjects.Select(string.Format("{0} is NULL OR LEN({0}) = '0'", (object) "F_PATH"));
    Dictionary<long, long> source = new Dictionary<long, long>();
    if (dataRowArray.Length != 0)
    {
      IDBRelationCollection relationCollection = sessionById.GetRelationCollection(sessionById.IdentHelper.SortedRelationTypeID);
      DBRecordSetParams paramSet = new DBRecordSetParams(1);
      paramSet.Columns = new object[2]
      {
        (object) ObligatoryObjectAttributes.F_PROJ_ID,
        (object) ObligatoryObjectAttributes.F_PART_ID
      };
      relationCollection.RelationTypeID = -1;
      int attributeTypeId = MetaDataHelper.GetAttributeTypeID(new Guid("cadd937c-306c-11d8-b4e9-00304f19f545"));
      paramSet.Conditions = new ConditionStructure[2]
      {
        new ConditionStructure(-23, RelationalOperators.In, (object) sessionById.IdentHelper.SortedRelationTypeID, LogicalOperators.AND, 0, false),
        new ConditionStructure(attributeTypeId, RelationalOperators.Greater, (object) -1, LogicalOperators.NONE, 0, false)
      };
      relationCollection.LocalTypesMode = true;
      foreach (DataRow dataRow in dataRowArray)
      {
        long result1;
        long.TryParse(dataRow["F_OBJECT_ID"].ToString(), out result1);
        DataTable dataTable = relationCollection.EntersInVersion(paramSet, result1);
        if (dataTable != null && dataTable.Rows.Count > 0)
        {
          long result2;
          long.TryParse(foldersForObjects.Rows[0][0].ToString(), out result2);
          source[result2] = result1;
        }
      }
    }
    if (source.Count > 0)
    {
      List<long> longList = new List<long>()
      {
        imbaseObjectId
      };
      longList.AddRange(source.Select<KeyValuePair<long, long>, long>((System.Func<KeyValuePair<long, long>, long>) (entry => entry.Key)));
      foldersForObjects = imbaseServerService.GetFoldersForObjects(sessionById.SessionGUID, longList.ToArray(), (long[]) null);
    }
    Dictionary<long, List<long>> hierarchyList = new Dictionary<long, List<long>>();
    this.Imbase_FillHierarchy(imbaseObjectId, hierarchyList, foldersForObjects);
    if (hierarchyList.Count > 0)
    {
      List<long> longList1 = new List<long>((IEnumerable<long>) new long[1]
      {
        imbaseObjectId
      });
      long num1 = imbaseObjectId;
      long num2 = 0;
      List<long> longList2;
      while (num1 != 0L && hierarchyList.TryGetValue(num1, out longList2) && longList2.Count == 1)
      {
        num1 = longList2[0];
        if (num1 != 0L)
        {
          AutoSelectionRuleCacheService.ImbaseCacheRec ruleByImbaseObjOnly2 = this.GetRuleByImbaseObjOnly(num1, sessionById);
          num2 = ruleByImbaseObjOnly2.OwnerObject == 0L || ruleByImbaseObjOnly2.OwnerObject == num1 ? num1 : ruleByImbaseObjOnly2.OwnerObject;
          if (ruleByImbaseObjOnly2.RuleList != null)
          {
            collection.AddRange((IEnumerable<long>) ruleByImbaseObjOnly2.RuleList);
            break;
          }
          longList1.Add(num1);
        }
        else
          break;
      }
      if (num2 != 0L)
      {
        foreach (long key in longList1)
        {
          if (this._imbaseCache.TryGetValue(key, out ruleByImbaseObjOnly1))
          {
            ruleByImbaseObjOnly1.OwnerObject = num2;
            ruleByImbaseObjOnly1.RuleList = new List<long>((IEnumerable<long>) collection);
          }
        }
      }
      return collection;
    }
    ruleByImbaseObjOnly1.RuleList = new List<long>();
    return collection;
  }

  public List<long> GetRulesByImbaseObjOnly(long imbaseObjectId, Guid sessionGuid)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (sessionById == null)
      return new List<long>();
    AutoSelectionRuleCacheService.ImbaseCacheRec ruleByImbaseObjOnly = this.GetRuleByImbaseObjOnly(imbaseObjectId, sessionById);
    return ruleByImbaseObjOnly?.RuleList == null || ruleByImbaseObjOnly.OwnerObject != imbaseObjectId && ruleByImbaseObjOnly.OwnerObject != 0L ? new List<long>() : ruleByImbaseObjOnly.RuleList;
  }

  public void RulesRegister(
    List<long> ruleIdList,
    long objectId,
    AutoSelectionLinkMode linkMode,
    Guid sessionGuid)
  {
    if (linkMode != AutoSelectionLinkMode.asotObjectType)
    {
      if (linkMode != AutoSelectionLinkMode.asotImbaseObject)
        return;
      this.Imbase_RulesRegister(ruleIdList, objectId, sessionGuid);
    }
    else
      this.Types_RulesRegister(ruleIdList, objectId, sessionGuid);
  }

  public void RulesUnregister(
    List<long> ruleIdList,
    long objectId,
    AutoSelectionLinkMode linkMode,
    Guid sessionGuid)
  {
    if (linkMode != AutoSelectionLinkMode.asotObjectType)
    {
      if (linkMode != AutoSelectionLinkMode.asotImbaseObject)
        return;
      this.Imbase_RulesUnregister(ruleIdList, objectId, sessionGuid);
    }
    else
      this.Types_RulesUnregister(ruleIdList, objectId, sessionGuid);
  }

  public void RulesUpdate(
    List<long> ruleIdList,
    long objectId,
    AutoSelectionLinkMode linkMode,
    Guid sessionGuid)
  {
    if (linkMode != AutoSelectionLinkMode.asotObjectType)
    {
      if (linkMode != AutoSelectionLinkMode.asotImbaseObject)
        return;
      this.Imbase_RulesUpdate(ruleIdList, objectId, sessionGuid);
    }
    else
      this.Types_RulesUpdate(ruleIdList, (long) (int) objectId, sessionGuid);
  }

  public void ClearCache() => this.ClearCache(true);

  public void ClearCache(bool imbaseNeedClear)
  {
    this._ruleCache.Clear();
    this._ruleTypeCache.Clear();
    if (!imbaseNeedClear)
      return;
    this._imbaseCache.Clear();
  }

  public void LoadCache()
  {
    this.ClearCache(false);
    IUserSession sessionTemporaryClone = AutoSelectionServerCache.DBTimedEvents.GetSystemSessionTemporaryClone("AutoSelect.LoadCache");
    try
    {
      this.Types_LoadObjectTypeIDS(sessionTemporaryClone);
      Guid anAttributeGuid1 = new Guid("cad001a0-306c-11d8-b4e9-00304f19f545");
      Guid anAttributeGuid2 = new Guid("cad001d0-306c-11d8-b4e9-00304f19f545");
      Guid guid1 = new Guid("cad00202-306c-11d8-b4e9-00304f19f545");
      IDBAttributeType attributeType1 = sessionTemporaryClone.GetAttributeType(anAttributeGuid1);
      if (attributeType1 == null)
        return;
      string name1 = attributeType1.Name;
      IDBAttributeType attributeType2 = sessionTemporaryClone.GetAttributeType(anAttributeGuid2);
      if (attributeType2 == null)
        return;
      string name2 = attributeType2.Name;
      IDBObjectCollection objectCollection = sessionTemporaryClone.GetObjectCollection(AutoSelectionConsts.objTypeRuleGuid);
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[5]
      {
        (object) -2,
        (object) anAttributeGuid1,
        (object) anAttributeGuid2,
        (object) AutoSelectionConsts.attrTypeTypeLink,
        (object) guid1
      }, (object[]) null, (SortOrders[]) null);
      IMSObjectType objectType1 = MetaDataHelper.GetObjectType(AutoSelectionConsts.objTypeRuleGuid);
      List<int> childrenIdRecursive = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(objectType1.ObjectTypeID);
      childrenIdRecursive.Remove(objectType1.ObjectTypeID);
      DataTable dataTable = childrenIdRecursive.Count > 0 ? objectCollection.SelectWithLocalObjects(paramSet) : objectCollection.Select(paramSet);
      if (dataTable == null || dataTable.Rows.Count == 0)
        return;
      for (int index = 0; index <= dataTable.Rows.Count - 1; ++index)
      {
        DataRow row = dataTable.Rows[index];
        long result1 = 0;
        try
        {
          long.TryParse(row[0].ToString(), out result1);
        }
        catch (InvalidCastException ex)
        {
        }
        if (result1 != 0L)
        {
          Guid guid2 = Guid.Empty;
          string str1 = row[name1].ToString();
          if (GuidHelper.IsGuid(str1))
          {
            try
            {
              guid2 = new Guid(str1);
            }
            catch (FormatException ex)
            {
              guid2 = Guid.Empty;
            }
          }
          Guid attributeTypeGuid = Guid.Empty;
          string str2 = row[name2].ToString();
          if (GuidHelper.IsGuid(str2))
          {
            try
            {
              attributeTypeGuid = new Guid(str2);
            }
            catch (FormatException ex)
            {
              attributeTypeGuid = Guid.Empty;
            }
          }
          int result2;
          int.TryParse(row[3].ToString(), out result2);
          int result3;
          int.TryParse(row[4].ToString(), out result3);
          AutoSelectionRuleCache selectionRuleCache = new AutoSelectionRuleCache(result1, guid2, attributeTypeGuid)
          {
            TypeLinked = result2,
            OrderID = (long) result3
          };
          this._ruleCache.TryAdd(result1, selectionRuleCache);
          if (!(guid2 == Guid.Empty))
          {
            IDBObjectType objectType2 = sessionTemporaryClone.GetObjectType(guid2, false);
            if (objectType2 != null)
            {
              int objectType3 = objectType2.ObjectType;
              List<AutoSelectionRuleCache> selectionRuleCacheList;
              if (!this._ruleTypeCache.TryGetValue(objectType3, out selectionRuleCacheList))
              {
                selectionRuleCacheList = new List<AutoSelectionRuleCache>()
                {
                  selectionRuleCache
                };
                this._ruleTypeCache.TryAdd(objectType3, selectionRuleCacheList);
              }
              else
                selectionRuleCacheList.Add(selectionRuleCache);
            }
          }
        }
      }
      AutoSelectionRuleCacheComparer ruleCacheComparer = new AutoSelectionRuleCacheComparer();
      foreach (int key in (IEnumerable<int>) this._ruleTypeCache.Keys)
        this._ruleTypeCache[key].Sort((IComparer<AutoSelectionRuleCache>) ruleCacheComparer);
    }
    finally
    {
      sessionTemporaryClone.Logout("AutoSelect.LoadCache");
    }
  }

  public void RuleCacheAdd(long objectId, IUserSession session)
  {
    if (objectId == 0L)
      return;
    Guid guid = Guid.Empty;
    if (this._ruleCache.ContainsKey(objectId))
      return;
    IDBObject dbObject = session.GetObject(objectId, false);
    if (dbObject == null)
      return;
    Guid attributeGuid1 = new Guid("cad001a0-306c-11d8-b4e9-00304f19f545");
    Guid attributeGuid2 = new Guid("cad001d0-306c-11d8-b4e9-00304f19f545");
    Guid attributeGuid3 = new Guid("cad00202-306c-11d8-b4e9-00304f19f545");
    IDBAttribute attributeByGuid1 = dbObject.GetAttributeByGuid(attributeGuid1);
    if (attributeByGuid1 != null && !attributeByGuid1.IsNull)
    {
      string str = attributeByGuid1.Value.ToString();
      if (GuidHelper.IsGuid(str))
      {
        try
        {
          guid = new Guid(str);
        }
        catch (FormatException ex)
        {
          guid = Guid.Empty;
        }
      }
    }
    Guid attributeTypeGuid = Guid.Empty;
    IDBAttribute attributeByGuid2 = dbObject.GetAttributeByGuid(attributeGuid2);
    if (attributeByGuid2 != null && !attributeByGuid2.IsNull)
    {
      string str = attributeByGuid2.Value.ToString();
      if (GuidHelper.IsGuid(str))
      {
        try
        {
          attributeTypeGuid = new Guid(str);
        }
        catch (FormatException ex)
        {
          attributeTypeGuid = Guid.Empty;
        }
      }
    }
    int result1 = 0;
    IDBAttribute attributeByGuid3 = dbObject.GetAttributeByGuid(AutoSelectionConsts.attrTypeTypeLink);
    if (attributeByGuid3 != null && !attributeByGuid3.IsNull)
      int.TryParse(attributeByGuid3.AsString, out result1);
    int result2 = -1;
    IDBAttribute attributeByGuid4 = dbObject.GetAttributeByGuid(attributeGuid3);
    if (attributeByGuid4 != null && !attributeByGuid4.IsNull)
      int.TryParse(attributeByGuid4.AsString, out result2);
    AutoSelectionRuleCache selectionRuleCache = new AutoSelectionRuleCache(objectId, guid, attributeTypeGuid)
    {
      TypeLinked = result1,
      OrderID = (long) result2
    };
    this._ruleCache.TryAdd(objectId, selectionRuleCache);
    if (guid == Guid.Empty)
      return;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(guid);
    if (objectType == null)
      return;
    int objectTypeId = objectType.ObjectTypeID;
    List<AutoSelectionRuleCache> selectionRuleCacheList;
    if (!this._ruleTypeCache.TryGetValue(objectTypeId, out selectionRuleCacheList))
    {
      selectionRuleCacheList = new List<AutoSelectionRuleCache>()
      {
        selectionRuleCache
      };
      this._ruleTypeCache.TryAdd(objectTypeId, selectionRuleCacheList);
    }
    else
      selectionRuleCacheList.Add(selectionRuleCache);
    selectionRuleCacheList.Sort((IComparer<AutoSelectionRuleCache>) new AutoSelectionRuleCacheComparer());
  }

  public void RuleCacheUpdate(long objectId, IDBAttribute attribute, IUserSession session)
  {
    if (objectId == 0L)
      return;
    this.RuleCacheUpdate(new List<long>() { objectId }, attribute, session);
  }

  public void RuleCacheUpdate(
    List<long> objectIdList,
    IDBAttribute attribute,
    IUserSession session)
  {
    if (attribute == null || session == null || objectIdList == null || objectIdList.Count == 0)
      return;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attribute.AttributeID);
    if (attributeType == null)
      return;
    Guid attributeGuid = attributeType.AttributeGuid;
    Guid g1 = new Guid("cad001a0-306c-11d8-b4e9-00304f19f545");
    Guid g2 = new Guid("cad001d0-306c-11d8-b4e9-00304f19f545");
    Guid g3 = new Guid("cad00202-306c-11d8-b4e9-00304f19f545");
    if (attributeGuid.Equals(AutoSelectionConsts.attrTypeTypeLink) || attributeGuid.Equals(g3))
    {
      foreach (long objectId in objectIdList)
      {
        AutoSelectionRuleCache selectionRuleCache;
        if (this._ruleCache.TryGetValue(objectId, out selectionRuleCache))
        {
          int result;
          int.TryParse(attribute.AsString, out result);
          if (attributeGuid.Equals(AutoSelectionConsts.attrTypeTypeLink))
            selectionRuleCache.TypeLinked = result == -1 ? 0 : result;
          else
            selectionRuleCache.OrderID = (long) result;
        }
      }
    }
    else
    {
      if (!attributeGuid.Equals(g1) && !attributeGuid.Equals(g2))
        return;
      Guid objTypeGuid = Guid.Empty;
      string asString = attribute.AsString;
      if (GuidHelper.IsGuid(asString))
      {
        try
        {
          objTypeGuid = new Guid(asString);
        }
        catch (FormatException ex)
        {
        }
      }
      if (objTypeGuid == Guid.Empty)
      {
        this.RuleCacheDelete(objectIdList, session);
      }
      else
      {
        foreach (long objectId in objectIdList)
        {
          AutoSelectionRuleCache selectionRuleCache;
          if (this._ruleCache.TryGetValue(objectId, out selectionRuleCache) && selectionRuleCache != null)
          {
            if (attributeGuid.Equals(g2))
              selectionRuleCache.AttributeTypeGuid = objTypeGuid;
            else if (attributeGuid.Equals(g1))
            {
              IMSObjectType objectType1 = MetaDataHelper.GetObjectType(selectionRuleCache.ObjectTypeGuid);
              List<AutoSelectionRuleCache> selectionRuleCacheList1;
              if (objectType1 != null && this._ruleTypeCache.TryGetValue(objectType1.ObjectTypeID, out selectionRuleCacheList1))
                selectionRuleCacheList1.Remove(selectionRuleCache);
              selectionRuleCache.ObjectTypeGuid = objTypeGuid;
              IMSObjectType objectType2 = MetaDataHelper.GetObjectType(objTypeGuid);
              if (objectType2 != null)
              {
                int objectTypeId = objectType2.ObjectTypeID;
                List<AutoSelectionRuleCache> selectionRuleCacheList2;
                if (!this._ruleTypeCache.TryGetValue(objectTypeId, out selectionRuleCacheList2))
                {
                  selectionRuleCacheList2 = new List<AutoSelectionRuleCache>()
                  {
                    selectionRuleCache
                  };
                  this._ruleTypeCache.TryAdd(objectTypeId, selectionRuleCacheList2);
                }
                else
                  selectionRuleCacheList2.Add(selectionRuleCache);
              }
            }
          }
        }
      }
    }
  }

  public void RuleCacheDelete(long objectId, IUserSession session)
  {
    if (objectId == 0L)
      return;
    this.RuleCacheDelete(new List<long>() { objectId }, session);
  }

  public void RuleCacheDelete(List<long> objectIdList, IUserSession session)
  {
    if (objectIdList == null || objectIdList.Count == 0)
      return;
    List<AutoSelectionRuleCache> selectionRuleCacheList1 = new List<AutoSelectionRuleCache>();
    foreach (long objectId in objectIdList)
    {
      AutoSelectionRuleCache selectionRuleCache;
      if (this._ruleCache.TryGetValue(objectId, out selectionRuleCache))
      {
        if (selectionRuleCache != null)
          selectionRuleCacheList1.Add(selectionRuleCache);
        this._ruleCache.TryRemove(objectId, out AutoSelectionRuleCache _);
      }
    }
    if (selectionRuleCacheList1.Count == 0)
      return;
    foreach (AutoSelectionRuleCache selectionRuleCache in selectionRuleCacheList1)
    {
      IMSObjectType objectType = selectionRuleCache.ObjectTypeGuid != Guid.Empty ? MetaDataHelper.GetObjectType(selectionRuleCache.ObjectTypeGuid) : (IMSObjectType) null;
      List<AutoSelectionRuleCache> selectionRuleCacheList2;
      if (objectType != null && this._ruleTypeCache.TryGetValue(objectType.ObjectTypeID, out selectionRuleCacheList2) && selectionRuleCacheList2.Contains(selectionRuleCache))
        selectionRuleCacheList2.Remove(selectionRuleCache);
    }
    foreach (AutoSelectionRuleCache selectionRuleCache in selectionRuleCacheList1)
    {
      List<long> longList = new List<long>();
      foreach (KeyValuePair<long, AutoSelectionRuleCacheService.ImbaseCacheRec> keyValuePair in this._imbaseCache)
      {
        AutoSelectionRuleCacheService.ImbaseCacheRec imbaseCacheRec = keyValuePair.Value;
        if (imbaseCacheRec.RuleList != null && imbaseCacheRec.RuleList.Contains(selectionRuleCache.SelectionRuleID))
          imbaseCacheRec.RuleList.Remove(selectionRuleCache.SelectionRuleID);
      }
      foreach (long key in longList)
        this._imbaseCache.TryRemove(key, out AutoSelectionRuleCacheService.ImbaseCacheRec _);
    }
  }

  private class ImbaseCacheRec
  {
    public ImbaseCacheRec(long ownerObjectId, List<long> ruleIdList = null)
    {
      this.OwnerObject = ownerObjectId;
      if (ruleIdList == null)
        return;
      this.RuleList = new List<long>((IEnumerable<long>) ruleIdList);
    }

    public long OwnerObject { get; set; }

    public List<long> RuleList { get; internal set; }
  }
}
