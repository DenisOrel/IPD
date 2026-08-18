// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.VersionRulesCacheService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Search;
using Intermech.Search.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;


namespace Intermech.Kernel.Services;

[Serializable]
public sealed class VersionRulesCacheService : 
  LongLifeObject,
  IVersionRulesCacheService,
  IClientVersionRulesCacheService
{
  internal static int FValidType = 0;
  internal static int FCommonVersionRuleTypeID = -1;
  internal static int FPersonalVersionRuleTypeID = -1;
  internal static int FSystemVersionRuleTypeID = -1;
  internal object SyncRoot = new object();
  private HybridDictionary FRules = new HybridDictionary();
  internal List<VersionsRule> FLifecycleLevelRules = new List<VersionsRule>();
  internal VersionsRule FAllVersionsRule = new VersionsRule();
  internal VersionsRule FAllConcreteVersionsRule = new VersionsRule();
  internal VersionsRule FLatestVersionsRule = new VersionsRule();
  internal VersionsRule FBaseVersionsRule = new VersionsRule();
  internal VersionsRule FSequentialModificationsRule = new VersionsRule();
  internal VersionsRule FDefaultRule;
  internal HybridDictionary FUserRulesCaches = new HybridDictionary(0, true);
  internal HybridDictionary FRolesSettings = new HybridDictionary(0, true);

  public VersionRulesCacheService()
  {
  }

  public VersionRulesCacheService(object usrSession) => this.Load(usrSession);

  public void Clear()
  {
    lock (this.SyncRoot)
      this.FRules.Clear();
    lock (this.FLifecycleLevelRules)
      this.FLifecycleLevelRules.Clear();
    lock (this.FUserRulesCaches.SyncRoot)
      this.FUserRulesCaches.Clear();
    lock (this.FRolesSettings.SyncRoot)
      this.FRolesSettings.Clear();
    this.FDefaultRule = (VersionsRule) null;
  }

  public void Delete(long Object_ID)
  {
    if (Object_ID == 0L)
      return;
    VersionsRule versionsRule = this[Object_ID];
    lock (this.SyncRoot)
    {
      try
      {
        this.FRules.Remove((object) Object_ID);
      }
      catch
      {
      }
    }
    lock (this.FLifecycleLevelRules)
    {
      for (int index = this.FLifecycleLevelRules.Count - 1; index >= 0; --index)
      {
        if (this.FLifecycleLevelRules[index].RuleObjectID == Object_ID)
          this.FLifecycleLevelRules.RemoveAt(index);
      }
    }
    lock (this.FUserRulesCaches)
    {
      IDictionaryEnumerator enumerator = this.FUserRulesCaches.GetEnumerator();
      enumerator.Reset();
      while (enumerator.MoveNext())
      {
        if (enumerator.Value is UserRulesCache userRulesCache)
        {
          userRulesCache.DeleteRuleVars(Object_ID);
          userRulesCache.DeleteRuleTuning(Object_ID);
        }
      }
    }
  }

  private IUserSession GetUserSession(object usrSession)
  {
    switch (usrSession)
    {
      case IUserSession _:
        return usrSession as IUserSession;
      case Guid sessionGUID:
        return UserSession.GetSessionByID(sessionGUID);
      case string _:
        return UserSession.GetSessionByID(new Guid((string) usrSession));
      default:
        return (IUserSession) null;
    }
  }

  private VersionsRule AddLifecycleLevelRule(
    IUserSession session,
    DataRow level,
    ref List<VersionsRule> toDelete)
  {
    if (VersionRulesCacheService.FCommonVersionRuleTypeID == -1 || VersionRulesCacheService.FPersonalVersionRuleTypeID == -1 || VersionRulesCacheService.FSystemVersionRuleTypeID == -1)
      return (VersionsRule) null;
    if (level == null)
      return (VersionsRule) null;
    int int32 = Convert.ToInt32(level["F_LEVEL_ID"]);
    Guid guid = new Guid(level["F_GUID"].ToString());
    string str1 = level["F_LEVEL_NAME"].ToString();
    string str2 = string.Format(LocalizationHolder.rm.GetString("Kernel_653"), (object) str1);
    lock (this.SyncRoot)
    {
      IDictionaryEnumerator enumerator = this.FRules.GetEnumerator();
      enumerator.Reset();
      while (enumerator.MoveNext())
      {
        if (enumerator.Value is VersionsRule versionsRule)
        {
          int StandardCriterions;
          int AdvancedCriterions;
          versionsRule.CriterionsCount(out StandardCriterions, out AdvancedCriterions);
          if (StandardCriterions == 1 || AdvancedCriterions == 1)
          {
            VersionsRuleCriterion criterion = versionsRule.Criterions[0];
            if (!(criterion.MainAttribute.Attribute.AttrGUID != "cad00030-306c-11d8-b4e9-00304f19f545") && criterion.ComparableValues.Count == 1 && !(criterion.CompareFunction != "EQUALS") && (versionsRule.RuleObjectType == session.IdentHelper.objtypeVersionRuleSystem || toDelete == null || toDelete.Contains(versionsRule)))
            {
              ComparableValue comparableValue = criterion.ComparableValues[0];
              try
              {
                if (Convert.ToInt64(comparableValue.Value) != Convert.ToInt64(int32))
                  continue;
              }
              catch
              {
                continue;
              }
              if (versionsRule.RuleObjectType == session.IdentHelper.objtypeVersionRuleSystem)
              {
                if (versionsRule.RuleObjectCaption == str2)
                  return versionsRule;
                if (GuidHelper.IsGuid(versionsRule.RuleObjectGuid) && versionsRule.RuleObjectGuid.IndexOf("cad") == 0)
                  return versionsRule;
                try
                {
                  IDBObject dbObject = session.GetObject(versionsRule.RuleObjectID);
                  try
                  {
                    dbObject = dbObject.CheckOut(false);
                    dbObject.Caption = str2;
                  }
                  finally
                  {
                    if (dbObject.ObjectID < 0L)
                      dbObject.CheckIn();
                  }
                  return versionsRule;
                }
                catch (Exception ex)
                {
                  if (ServerServices.GetService(typeof (IEventLogHelper)) is IEventLogHelper service)
                    service.AddEvent(versionsRule.RuleObjectID, 0L, 14, 0L, versionsRule.RuleObjectCaption, string.Format(LocalizationHolder.rm.GetString("Kernel_654"), (object) str1, (object) ex.Message), ActionType.Load, EventlogRecordType.Error, session.UserID, session.ComputerName, session);
                }
              }
            }
          }
        }
      }
    }
    long num = 0;
    VersionsRule versionsRule1 = new VersionsRule();
    try
    {
      versionsRule1.ConvertToSystemRule(session, str2, "cad00030-306c-11d8-b4e9-00304f19f545", (object) int32);
      IDBObject RuleObject = session.GetObjectCollection(VersionRulesCacheService.FCommonVersionRuleTypeID).Create(VersionRulesCacheService.FSystemVersionRuleTypeID);
      num = RuleObject.ObjectID;
      RuleObject.Caption = str2;
      RuleObject.OwnerID = session.IdentHelper.SysdbaID;
      try
      {
        versionsRule1.SaveToObject(session, RuleObject);
        RuleObject.CommitCreation(true);
        num = RuleObject.ObjectID;
      }
      catch
      {
        num = 0L;
      }
      if (num == 0L)
        return (VersionsRule) null;
      this.LoadRule((object) session, num);
    }
    catch (Exception ex)
    {
      if (ServerServices.GetService(typeof (IEventLogHelper)) is IEventLogHelper service)
        service.AddEvent(num, 0L, 14, 0L, str2, string.Format(LocalizationHolder.rm.GetString("Kernel_655"), (object) str1, (object) ex.Message), ActionType.Load, EventlogRecordType.Error, session.UserID, session.ComputerName, session);
      return (VersionsRule) null;
    }
    return this[num];
  }

  private int LifecycleLevelRuleExists(List<VersionsRule> list, VersionsRule rule)
  {
    if (rule == null || list.Count == 0)
      return -1;
    if (list.Contains(rule))
      return list.IndexOf(rule);
    for (int index = 0; index < list.Count; ++index)
    {
      if (list[index].IsCompatible(rule))
        return index;
    }
    return -1;
  }

  private void PrepareSystemRules(object usrSession)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13849(192509515), (object) "VersionRulesCacheService.PrepareSystemRules");
    Guid anObjectTypeGuid = new Guid("cad00278-306c-11d8-b4e9-00304f19f545");
    if (userSession.GetObjectType(anObjectTypeGuid, false) == null)
      return;
    if (this.FLatestVersionsRule.Criterions.Count == 0)
    {
      this.FAllVersionsRule.ConvertToAllVersionsRule(userSession);
      this.FAllConcreteVersionsRule.ConvertToAllConcreteVersionsRule(userSession);
      this.FLatestVersionsRule.ConvertToLatestVersionsRule(userSession);
      this.FBaseVersionsRule.ConvertToBaseVersions(userSession);
      this.FSequentialModificationsRule.ConvertToSequentialModifications(userSession);
    }
    this.CheckDefaultRule(userSession);
    this.CheckSystemRules();
    if (this.FLifecycleLevelRules.Count != 0 && (long) this.FLifecycleLevelRules.Count == userSession.GetLifecycleLevelCollection().Count)
      return;
    List<VersionsRule> flifecycleLevelRules1 = this.FLifecycleLevelRules;
    List<VersionsRule> flifecycleLevelRules2 = this.FLifecycleLevelRules;
    this.FLifecycleLevelRules = new List<VersionsRule>(0);
    lock (this.FLifecycleLevelRules)
    {
      DataTable dataTable = userSession.GetLifecycleLevelCollection().Select(string.Empty);
      if (dataTable != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          VersionsRule rule = this.AddLifecycleLevelRule(userSession, row, ref flifecycleLevelRules2);
          if (rule != null)
          {
            this.FLifecycleLevelRules.Add(rule.Clone() as VersionsRule);
            int index = this.LifecycleLevelRuleExists(flifecycleLevelRules1, rule);
            if (index >= 0)
              flifecycleLevelRules1.RemoveAt(index);
          }
        }
        dataTable.Dispose();
      }
    }
    if (flifecycleLevelRules1.Count > 0)
    {
      for (int index = 0; index < flifecycleLevelRules1.Count; ++index)
      {
        flifecycleLevelRules1[index].CurrentRuleType = VersionsRuleType.vrtStandardRule;
        flifecycleLevelRules1[index].RuleObjectType = userSession.IdentHelper.objtypeVersionRuleUser;
        this.Delete(flifecycleLevelRules1[index].RuleObjectID);
        IDBObject objectActualCopy = userSession.GetObjectActualCopy(flifecycleLevelRules1[index].RuleObjectID, false);
        if (objectActualCopy != null)
        {
          try
          {
            if (objectActualCopy.CheckoutBy != 0L)
              objectActualCopy.CancelChanges(true);
            objectActualCopy.Delete(0L);
          }
          catch
          {
          }
        }
      }
    }
    if (flifecycleLevelRules2.Count <= 0)
      return;
    for (int index = 0; index < flifecycleLevelRules2.Count; ++index)
    {
      flifecycleLevelRules2[index].CurrentRuleType = VersionsRuleType.vrtStandardRule;
      flifecycleLevelRules2[index].RuleObjectType = userSession.IdentHelper.objtypeVersionRuleUser;
      this.Delete(flifecycleLevelRules2[index].RuleObjectID);
      IDBObject objectActualCopy = userSession.GetObjectActualCopy(flifecycleLevelRules2[index].RuleObjectID, false);
      if (objectActualCopy != null)
      {
        try
        {
          if (objectActualCopy.CheckoutBy != 0L)
            objectActualCopy.CancelChanges(true);
          objectActualCopy.Delete(0L);
        }
        catch
        {
        }
      }
    }
  }

  public int Count
  {
    get
    {
      lock (this.SyncRoot)
        return this.FRules.Count;
    }
  }

  public VersionsRule this[long Object_ID]
  {
    get
    {
      if (Object_ID == 0L)
        return (VersionsRule) null;
      lock (this.SyncRoot)
      {
        VersionsRule frule = this.FRules[(object) Object_ID] as VersionsRule;
        this.CheckSystemRules();
        return frule;
      }
    }
  }

  public VersionsRule this[int Index]
  {
    get
    {
      if (Index < 0 || Index >= this.FRules.Count)
        throw new KernelExceptionID(sc_13834.ssp_appserver_13850(1254618645), (object) VersionRulesCacheService.VersionRulesCacheServiceConsts.Exception1);
      lock (this.SyncRoot)
      {
        IDictionaryEnumerator enumerator = this.FRules.GetEnumerator();
        enumerator.Reset();
        int num = 0;
        while (enumerator.MoveNext())
        {
          if (num == Index)
            return enumerator.Value as VersionsRule;
          ++num;
        }
        return (VersionsRule) null;
      }
    }
  }

  public VersionsRule this[object usrSession, long Object_ID]
  {
    get
    {
      if (Object_ID == 0L)
        return (VersionsRule) null;
      lock (this.SyncRoot)
      {
        if (!(this.FRules[(object) Object_ID] is VersionsRule frule))
        {
          this.Load(usrSession);
          frule = this.FRules[(object) Object_ID] as VersionsRule;
          this.CheckDefaultRule(this.GetUserSession(usrSession));
        }
        this.CheckSystemRules();
        return frule;
      }
    }
  }

  private UserRulesCache GetUserRulesCache(long UserID)
  {
    if (UserID == 0L)
      return (UserRulesCache) null;
    if (this.FUserRulesCaches == null)
      this.FUserRulesCaches = new HybridDictionary(0, true);
    return this.FUserRulesCaches[(object) UserID] as UserRulesCache;
  }

  private UserRulesCache GetUserRulesCache(long UserID, object usrSession)
  {
    if (UserID == 0L)
      return (UserRulesCache) null;
    if (this.FUserRulesCaches == null)
      this.FUserRulesCaches = new HybridDictionary(0, true);
    if (this.FUserRulesCaches[(object) UserID] is UserRulesCache fuserRulesCach)
      return fuserRulesCach;
    UserRulesCache userRulesCache;
    try
    {
      userRulesCache = new UserRulesCache(this, usrSession);
      if (userRulesCache.UserID != UserID)
        return (UserRulesCache) null;
      this.FUserRulesCaches[(object) UserID] = (object) userRulesCache;
    }
    catch
    {
      return (UserRulesCache) null;
    }
    return userRulesCache;
  }

  public int Load(object usrSession)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13851(460062541), (object) "VersionRulesCacheService.Load");
    MetaDataHelper.SyncLCStepsMetadata((userSession as IUserSessionCacheDataSet).CacheDataSet);
    UserRulesCache userRulesCache = this.GetUserRulesCache(userSession.UserID, (object) userSession);
    VersionRulesCacheService.FValidType = userSession.IdentHelper.objtypeVersionRule;
    VersionRulesCacheService.FCommonVersionRuleTypeID = userSession.IdentHelper.objtypeVersionRuleCommon;
    VersionRulesCacheService.FPersonalVersionRuleTypeID = userSession.IdentHelper.objtypeVersionRuleUser;
    VersionRulesCacheService.FSystemVersionRuleTypeID = userSession.IdentHelper.objtypeVersionRuleSystem;
    if (VersionRulesCacheService.FValidType != 0 && VersionRulesCacheService.FCommonVersionRuleTypeID != -1 && VersionRulesCacheService.FPersonalVersionRuleTypeID != -1)
    {
      if (VersionRulesCacheService.FSystemVersionRuleTypeID != -1)
      {
        try
        {
          IDBObjectCollection objectCollection = userSession.GetObjectCollection(VersionRulesCacheService.FValidType);
          ColumnDescriptor[] columns = new ColumnDescriptor[1]
          {
            new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
          };
          object[] objArray = new object[0];
          SortOrders[] sortOrdersArray = new SortOrders[0];
          DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, columns);
          DataTable dataTable;
          try
          {
            dataTable = objectCollection.Select(paramSet);
          }
          catch
          {
            dataTable = (DataTable) null;
          }
          if (dataTable == null || dataTable.Rows.Count == 0)
            return 0;
          lock (this.SyncRoot)
          {
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            {
              if (row != null)
              {
                long int64 = Convert.ToInt64(row[0]);
                if (!(this.FRules[(object) int64] is VersionsRule frule))
                {
                  VersionsRule versionsRule = new VersionsRule();
                  versionsRule.LoadFromObject(userSession, int64);
                  this.FRules[(object) int64] = (object) versionsRule;
                  int StandardCriterions;
                  int AdvancedCriterions;
                  versionsRule.CriterionsCount(out StandardCriterions, out AdvancedCriterions);
                  if (StandardCriterions == 1 || AdvancedCriterions == 1)
                  {
                    VersionsRuleCriterion criterion = versionsRule.Criterions[0];
                    if (!(criterion.MainAttribute.Attribute.AttrGUID != "cad00030-306c-11d8-b4e9-00304f19f545") && criterion.ComparableValues.Count == 1 && !(criterion.CompareFunction != "EQUALS") && criterion.ComparableValues[0].ValueType == "CONST" && versionsRule.CurrentRuleType == VersionsRuleType.vrtSystemRule && MetaDataHelper.ExistsLCLevel(Convert.ToInt32(versionsRule.Criterions[0].ComparableValues[0].Value)))
                      this.FLifecycleLevelRules.Add(versionsRule.Clone() as VersionsRule);
                  }
                }
                else
                  frule.LoadFromObject(userSession, Convert.ToInt64(row[0]));
              }
            }
            userRulesCache?.Load((object) userSession, false);
            return this.FRules.Count;
          }
        }
        finally
        {
          this.PrepareSystemRules((object) userSession);
          this.FRules.Values.Cast<VersionsRule>().Where<VersionsRule>((System.Func<VersionsRule, bool>) (o => o.RuleObjectGuid == this.FBaseVersionsRule.RuleObjectGuid)).FirstOrDefault<VersionsRule>()?.ConvertToBaseVersions(userSession);
        }
      }
    }
    return -1;
  }

  public bool RuleExists(long Object_ID)
  {
    lock (this.SyncRoot)
      return this.FRules[(object) Object_ID] is VersionsRule;
  }

  public bool RuleExists(object usrSession, long Object_ID)
  {
    bool flag = this.RuleExists(Object_ID);
    if (flag)
      return flag;
    this.Load(usrSession);
    return this.RuleExists(Object_ID);
  }

  public int LoadRule(object usrSession, long Object_ID)
  {
    return this.LoadRule(usrSession, Object_ID, DateTime.MinValue);
  }

  public int LoadRule(object usrSession, long Object_ID, DateTime actualDate)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13852(460806799), (object) "VersionRulesCacheService.LoadRule");
    using (UserSessionContext.CaptureSession(userSession))
    {
      lock (this.SyncRoot)
      {
        this.FRules.Remove((object) Object_ID);
        this.FRules.Remove((object) -Object_ID);
        IDBObject RuleObject1 = userSession.GetObject(Object_ID, false);
        if (RuleObject1 == null)
          return 0;
        VersionsRule rule = new VersionsRule();
        rule.LoadFromObject(userSession, RuleObject1);
        int StandardCriterions;
        int AdvancedCriterions;
        rule.CriterionsCount(out StandardCriterions, out AdvancedCriterions);
        rule.ActualDate = actualDate;
        if (StandardCriterions == 1 && AdvancedCriterions == 1)
        {
          VersionsRuleCriterion criterion = rule.Criterions[0];
          if (criterion.MainAttribute.Attribute.AttrGUID == "cad00030-306c-11d8-b4e9-00304f19f545" && criterion.ComparableValues.Count == 1 && criterion.CompareFunction == "EQUALS" && criterion.ComparableValues[0].ValueType == "CONST" && rule.CurrentRuleType == VersionsRuleType.vrtSystemRule)
          {
            lock (this.FLifecycleLevelRules)
            {
              int index = this.LifecycleLevelRuleExists(this.FLifecycleLevelRules, rule);
              if (index >= 0)
                this.FLifecycleLevelRules.RemoveAt(index);
              this.FLifecycleLevelRules.Add(rule.Clone() as VersionsRule);
            }
          }
        }
        if (this.FRules[(object) rule.RuleObjectID] != null)
          this.FRules[(object) rule.RuleObjectID] = (object) rule;
        else
          this.FRules.Add((object) rule.RuleObjectID, (object) rule);
        if (rule.EditingRule && rule.IsDefault)
        {
          if (this.FDefaultRule != null && this.FDefaultRule.RuleObjectGuid != rule.RuleObjectGuid && (this.FDefaultRule.RuleObjectID != 0L || GuidHelper.IsGuid(this.FDefaultRule.RuleObjectGuid)))
          {
            IDBObject RuleObject2 = this.FDefaultRule.RuleObjectID != 0L ? userSession.GetObject(this.FDefaultRule.RuleObjectID, false) : userSession.GetObject(new Guid(this.FDefaultRule.RuleObjectGuid), false);
            if (RuleObject2 != null)
            {
              this.FDefaultRule.LoadFromObject(userSession, RuleObject2);
              try
              {
                this.FDefaultRule.SaveToObject(userSession, RuleObject2);
              }
              catch
              {
              }
            }
          }
          this.FDefaultRule = rule;
          if (this.FDefaultRule.RuleObjectID != 0L || GuidHelper.IsGuid(this.FDefaultRule.RuleObjectGuid))
          {
            IDBObject RuleObject3 = this.FDefaultRule.RuleObjectID != 0L ? userSession.GetObject(this.FDefaultRule.RuleObjectID, false) : userSession.GetObject(new Guid(this.FDefaultRule.RuleObjectGuid), false);
            if (RuleObject3 != null)
            {
              this.FDefaultRule.LoadFromObject(userSession, RuleObject3);
              try
              {
                this.FDefaultRule.SaveToObject(userSession, RuleObject3);
              }
              catch
              {
              }
            }
          }
        }
        this.CheckDefaultRule(userSession);
        this.CheckSystemRules();
        return 1;
      }
    }
  }

  public void UpdateLifecycleRule(object usrSession, int LevelID)
  {
    this.PrepareSystemRules((object) (this.GetUserSession(usrSession) ?? throw new KernelExceptionID(sc_13834.ssp_appserver_13853(1944147634), (object) "VersionRulesCacheService.UpdateLifecycleRule")));
  }

  public VersionsRuleType RuleType(object usrSession, long Object_ID)
  {
    VersionsRule versionsRule = this[(object) (MyUserSessionHelper.GetUserSession(usrSession) ?? throw new KernelExceptionID(sc_13834.ssp_appserver_13854(1792792711), (object) "VersionRulesCacheService.RuleType")), Object_ID];
    return versionsRule != null ? versionsRule.CurrentRuleType : VersionsRuleType.vrtStandardRule;
  }

  private void CheckDefaultRule(IUserSession userSession)
  {
    lock (this.SyncRoot)
    {
      long defaultVersionRuleVersionID = userSession.Configurations.ReadInteger(ConfigurationOptionKeys.Versions_DefaultVersionRule.Module, ConfigurationOptionKeys.Versions_DefaultVersionRule.Section, ConfigurationOptionKeys.Versions_DefaultVersionRule.Name, 0L, DBConfigMode.GlobalOnly);
      if (defaultVersionRuleVersionID == 0L)
        this.FDefaultRule = this.BaseVersionsRule;
      else
        this.FDefaultRule = this.FRules.Values.Cast<VersionsRule>().Where<VersionsRule>((System.Func<VersionsRule, bool>) (o => o.RuleObjectID == defaultVersionRuleVersionID)).FirstOrDefault<VersionsRule>() ?? this.BaseVersionsRule;
    }
  }

  private void CheckSystemRules()
  {
    lock (this.SyncRoot)
    {
      IDictionaryEnumerator enumerator = this.FRules.GetEnumerator();
      enumerator.Reset();
      while (enumerator.MoveNext())
      {
        VersionsRule versionsRule = enumerator.Value as VersionsRule;
        if (versionsRule.RuleObjectGuid == this.LatestVersionsRule.RuleObjectGuid)
          versionsRule.Assign((object) this.LatestVersionsRule);
        if (versionsRule.RuleObjectGuid == this.AllVersionsRule.RuleObjectGuid)
          versionsRule.Assign((object) this.AllVersionsRule);
        if (versionsRule.RuleObjectGuid == this.AllConcreteVersionsRule.RuleObjectGuid)
          versionsRule.Assign((object) this.AllConcreteVersionsRule);
      }
    }
  }

  public List<VersionsRule> GetEditingRules()
  {
    List<VersionsRule> editingRules = new List<VersionsRule>();
    lock (this.SyncRoot)
    {
      IDictionaryEnumerator enumerator = this.FRules.GetEnumerator();
      enumerator.Reset();
      while (enumerator.MoveNext())
      {
        if (enumerator.Value is VersionsRule versionsRule && versionsRule.EditingRule)
          editingRules.Add(versionsRule.Clone() as VersionsRule);
      }
      return editingRules;
    }
  }

  public bool SetDefaultVersionsRule(object usrSession, VersionsRule versionRule)
  {
    if (versionRule == null || versionRule.RuleObjectID == 0L)
      return false;
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13855(1004573355), (object) "VersionRulesCacheService.SetDefaultVersionsRule");
    IConfigurationOptionRepository optionRepository = ServiceLocator.Get<IConfigurationOptionRepository>();
    using (UserSessionContext.CaptureSession(userSession.SessionGUID))
      optionRepository.AddOrUpdate(ConfigurationOptionKeys.Versions_DefaultVersionRule, (object) versionRule.RuleObjectID);
    return true;
  }

  private string GetKey(long Rule_Object_ID) => Convert.ToString(Rule_Object_ID);

  public bool NeedRuleVars(long Rule_Object_ID)
  {
    if (Rule_Object_ID == 0L)
      return false;
    VersionsRule versionsRule = this[Rule_Object_ID];
    return versionsRule != null && versionsRule.HasVariableValues();
  }

  public VersionsRule GetRuleVars(long UserID, int index, long Rule_Object_ID)
  {
    if (Rule_Object_ID == 0L || UserID == 0L)
      return (VersionsRule) null;
    return this.GetUserRulesCache(UserID)?.GetRuleVars(index, Rule_Object_ID);
  }

  public bool SetRuleVars(object usrSession, long Rule_Object_ID, int index, VersionsRule value)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13856(1577413674), (object) "VersionRulesCacheService.SetRuleVars");
    if (Rule_Object_ID == 0L || value == null)
      return false;
    UserRulesCache userRulesCache = this.GetUserRulesCache(userSession.UserID, (object) userSession);
    if (userRulesCache == null)
      return false;
    userRulesCache.SetRuleVars((object) userSession, Rule_Object_ID, index, value);
    return true;
  }

  public int RuleVarsCount(long UserID, long Rule_Object_ID)
  {
    if (Rule_Object_ID == 0L || UserID == 0L)
      return 0;
    UserRulesCache userRulesCache = this.GetUserRulesCache(UserID);
    return userRulesCache == null ? 0 : userRulesCache.RuleVarsCount(Rule_Object_ID);
  }

  public ArrayList RuleVarsList(long UserID, long Rule_Object_ID)
  {
    if (Rule_Object_ID == 0L || UserID == 0L)
      return (ArrayList) null;
    return this.GetUserRulesCache(UserID)?.RuleVarsList(Rule_Object_ID);
  }

  public int RuleVarsAdd(object usrSession, VersionsRule Vars, long Rule_Object_ID)
  {
    if (Vars == null)
      return -1;
    IUserSession userSession = this.GetUserSession(usrSession);
    UserRulesCache userRulesCache = userSession != null ? this.GetUserRulesCache(userSession.UserID, (object) userSession) : throw new KernelExceptionID(sc_13834.ssp_appserver_13857(59215971), (object) "VersionRulesCacheService.RuleVarsAdd");
    if (userRulesCache == null)
      return -1;
    int num = userRulesCache.RuleVarsAdd((object) userSession, Vars, Rule_Object_ID);
    Vars.RuleObjectModified = DateTime.UtcNow;
    return num;
  }

  public bool RuleVarsDel(object usrSession, long Rule_Object_ID, int index)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    UserRulesCache userRulesCache = userSession != null ? this.GetUserRulesCache(userSession.UserID, (object) userSession) : throw new KernelExceptionID(sc_13834.ssp_appserver_13858(1063737178), (object) "VersionRulesCacheService.RuleVarsDel");
    if (userRulesCache == null)
      return false;
    userRulesCache.RuleVarsDel((object) userSession, Rule_Object_ID, index);
    return true;
  }

  public void SaveRuleVars(object usrSession)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13859(524849777), (object) "VersionRulesCacheService.SaveRuleVars");
    this.GetUserRulesCache(userSession.UserID, (object) userSession)?.SaveRuleVars((object) userSession);
  }

  public void LoadRuleVars(object usrSession)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13860(12009154), (object) "VersionRulesCacheService.LoadRuleVars");
    this.GetUserRulesCache(userSession.UserID, (object) userSession)?.LoadRuleVars((object) userSession);
  }

  public void ResetDateTime(object usrSession)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13861(321928777), (object) "VersionRulesCacheService.ResetDateTime");
    this.GetUserRulesCache(userSession.UserID, (object) userSession)?.PushRuleVarsActualDate();
  }

  public string[] GetFiltrationSettingsList(object userSession)
  {
    IUserSession userSession1 = this.GetUserSession(userSession);
    UserRulesCache userRulesCache = userSession1 != null ? this.GetUserRulesCache(userSession1.UserID, (object) userSession1) : throw new KernelExceptionID(sc_13834.ssp_appserver_13862(2008806038), (object) "VersionRulesCacheService.GetFiltrationSettingsList");
    return userRulesCache == null ? new string[0] : userRulesCache.GetFiltrationSettingsList();
  }

  public FiltrationSettings GetFiltrationSettings(object usrSession, string OwnerID)
  {
    return this.GetFiltrationSettings(usrSession, OwnerID, true);
  }

  public FiltrationSettings GetFiltrationSettings(
    object usrSession,
    string OwnerID,
    bool GetDefaults)
  {
    switch (OwnerID)
    {
      case "cad001e0-306c-11d8-b4e9-00304f19f545":
      case "cad001e3-306c-11d8-b4e9-00304f19f545":
        return new FiltrationSettings()
        {
          CurrentRule = this.AllVersionsRule
        };
      case "cad001df-306c-11d8-b4e9-00304f19f545":
      case "cad0069c-306c-11d8-b4e9-00304f19f545":
        return new FiltrationSettings()
        {
          CurrentRule = this.LatestVersionsRule
        };
      case "cad00601-306c-11d8-b4e9-00304f19f545":
        return new FiltrationSettings()
        {
          CurrentRule = this.BaseVersionsRule
        };
      case "cad00602-306c-11d8-b4e9-00304f19f545":
        return new FiltrationSettings()
        {
          CurrentRule = this.SequentialModificationsRule
        };
      default:
        IUserSession userSession = this.GetUserSession(usrSession);
        UserRulesCache userRulesCache = userSession != null ? this.GetUserRulesCache(userSession.UserID, (object) userSession) : throw new KernelExceptionID(sc_13834.ssp_appserver_13863(155464850), (object) "VersionRulesCacheService.GetFiltrationSettings");
        if (userRulesCache == null)
          return (FiltrationSettings) null;
        using (UserSessionContext.CaptureSession(userSession.SessionGUID))
          return userRulesCache.GetFiltrationSettings(OwnerID, GetDefaults);
    }
  }

  public bool SetFiltrationSettings(object usrSession, string OwnerID, FiltrationSettings value)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    UserRulesCache userRulesCache = userSession != null ? this.GetUserRulesCache(userSession.UserID, (object) userSession) : throw new KernelExceptionID(sc_13834.ssp_appserver_13864(1493400344), (object) "VersionRulesCacheService.SetFiltrationSettings");
    if (userRulesCache == null)
      return false;
    userRulesCache.SetFiltrationSettings(OwnerID, value);
    return true;
  }

  public bool LoadFiltrationTuning(object usrSession)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    UserRulesCache userRulesCache = userSession != null ? this.GetUserRulesCache(userSession.UserID, (object) userSession) : throw new KernelExceptionID(sc_13834.ssp_appserver_13865(928719312), (object) "VersionRulesCacheService.LoadFiltrationTuning");
    if (userRulesCache == null)
      return false;
    userRulesCache.LoadFiltrationTuning((object) userSession);
    return true;
  }

  public void SaveFiltrationTuning(object usrSession)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13866(1463973458), (object) "VersionRulesCacheService.SaveFiltrationTuning");
    this.GetUserRulesCache(userSession.UserID, (object) userSession)?.SaveFiltrationTuning((object) userSession);
  }

  public void SaveFiltrationTuning(long UserID, object usrSession)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13867(910437979), (object) "VersionRulesCacheService.SaveFiltrationTuning");
    this.GetUserRulesCache(UserID, (object) userSession)?.SaveFiltrationTuning((object) userSession);
  }

  public bool DeleteRuleTuning(object usrSession, long Rule_Object_ID)
  {
    if (Rule_Object_ID == 0L)
      return false;
    IUserSession userSession = this.GetUserSession(usrSession);
    UserRulesCache userRulesCache = userSession != null ? this.GetUserRulesCache(userSession.UserID, (object) userSession) : throw new KernelExceptionID(sc_13834.ssp_appserver_13868(520452940), (object) "VersionRulesCacheService.DeleteRuleTuning");
    if (userRulesCache == null)
      return false;
    userRulesCache.DeleteRuleTuning(Rule_Object_ID);
    return true;
  }

  public bool DeleteRuleTuning(object usrSession, string OwnerID)
  {
    return this.SetFiltrationSettings(usrSession, OwnerID, (FiltrationSettings) null);
  }

  public VersionsRule GetFiltrationRule(
    object usrSession,
    IFiltrationSettings Filtration,
    bool defaults)
  {
    bool RuleCompatible = false;
    bool RuleValid = false;
    bool VarsOutOfRange = false;
    VersionsRule filtrationRule = this.GetFiltrationRule(usrSession, Filtration, ref RuleCompatible, ref RuleValid, ref VarsOutOfRange);
    if (((filtrationRule == null || !RuleCompatible ? 1 : (!RuleValid ? 1 : 0)) | (VarsOutOfRange ? 1 : 0)) != 0)
      filtrationRule = !defaults ? (VersionsRule) null : this.LatestVersionsRule;
    return filtrationRule;
  }

  public VersionsRule GetFiltrationRule(
    object usrSession,
    IFiltrationSettings Filtration,
    ref bool RuleCompatible,
    ref bool RuleValid,
    ref bool VarsOutOfRange)
  {
    RuleCompatible = false;
    RuleValid = false;
    VarsOutOfRange = true;
    if (Filtration == null || Filtration.CurrentRule == null)
    {
      RuleCompatible = true;
      RuleValid = true;
      VarsOutOfRange = false;
      return this.LatestVersionsRule;
    }
    if (Filtration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtAllVersionsRule)
    {
      RuleCompatible = true;
      RuleValid = true;
      VarsOutOfRange = false;
      return this.AllVersionsRule;
    }
    if (Filtration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtLatestVersionsRule)
    {
      RuleCompatible = true;
      RuleValid = true;
      VarsOutOfRange = false;
      return this.LatestVersionsRule;
    }
    if (Filtration.CurrentRule.RuleObjectID == 0L)
    {
      RuleCompatible = true;
      RuleValid = true;
      VarsOutOfRange = false;
      return Filtration.CurrentRule;
    }
    int currentRuleVars = Filtration.CurrentRuleVars;
    IUserSession userSession = MyUserSessionHelper.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13869(683471247), (object) "VersionRulesCacheService.GetCurrentRule");
    VersionsRule filtrationRule1 = this[(object) userSession, Filtration.CurrentRule.RuleObjectID];
    if (filtrationRule1 == null)
      return (VersionsRule) null;
    if (currentRuleVars < 0 || filtrationRule1 != null && !filtrationRule1.HasVariableValues())
    {
      RuleCompatible = true;
      RuleValid = filtrationRule1 != null && filtrationRule1.Valid(userSession);
      VarsOutOfRange = false;
      return filtrationRule1;
    }
    VersionsRule filtrationRule2 = this.GetRuleVars(userSession.UserID, currentRuleVars, Filtration.CurrentRule.RuleObjectID);
    VarsOutOfRange = filtrationRule2 == null && currentRuleVars >= 0;
    if (filtrationRule2 == null)
      filtrationRule2 = filtrationRule1;
    if (!filtrationRule2.HasVariableValues())
      VarsOutOfRange = false;
    RuleCompatible = filtrationRule1.IsCompatible(filtrationRule2);
    RuleValid = filtrationRule2 != null && filtrationRule2.Valid(userSession);
    return filtrationRule2;
  }

  public VersionsRule AllVersionsRule => this.FAllVersionsRule;

  public VersionsRule AllConcreteVersionsRule => this.FAllConcreteVersionsRule;

  public VersionsRule LatestVersionsRule => this.FLatestVersionsRule;

  public VersionsRule BaseVersionsRule => this.FBaseVersionsRule;

  public VersionsRule SequentialModificationsRule => this.FSequentialModificationsRule;

  [Obsolete("Use GetDefaultVersionRule", true)]
  public VersionsRule DefaultVersionsRule
  {
    get
    {
      lock (this.SyncRoot)
        return this.FDefaultRule;
    }
  }

  public VersionsRule GetDefaultVersionRule(Guid userSessionGuid)
  {
    this.CheckDefaultRule(this.GetUserSession((object) userSessionGuid));
    lock (this.SyncRoot)
      return this.FDefaultRule;
  }

  public bool LoadUserSettings(object usrSession)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    UserRulesCache userRulesCache = userSession != null ? this.GetUserRulesCache(userSession.UserID, (object) userSession) : throw new KernelExceptionID(sc_13834.ssp_appserver_13870(51703967), (object) "VersionRulesCacheService.LoadUserSettings");
    if (userRulesCache == null)
      return false;
    userRulesCache.LoadUserSettings((object) userSession);
    return true;
  }

  public bool SaveUserSettings(object usrSession)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    UserRulesCache userRulesCache = userSession != null ? this.GetUserRulesCache(userSession.UserID, (object) userSession) : throw new KernelExceptionID(sc_13834.ssp_appserver_13871(173767160), (object) "VersionRulesCacheService.SaveUserSettings");
    return userRulesCache != null && userRulesCache.SaveUserSettings((object) userSession);
  }

  public bool Save(object usrSession)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    UserRulesCache userRulesCache = userSession != null ? this.GetUserRulesCache(userSession.UserID, (object) userSession) : throw new KernelExceptionID(sc_13834.ssp_appserver_13872(675488142), (object) "VersionRulesCacheService.Save");
    if (userRulesCache == null)
      return false;
    userRulesCache.Save((object) userSession);
    return true;
  }

  internal ISettingsContainer GetUserSettings(long UserID)
  {
    return this.GetUserRulesCache(UserID)?.SettingsContainer;
  }

  internal ISettingsContainer GetUserSettings(object usrSession, long UserID)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13873(2144798552), (object) "VersionRulesCacheService.GetUserSettings");
    return this.GetUserRulesCache(UserID, (object) userSession)?.SettingsContainer;
  }

  public object this[long UserID, object Key]
  {
    get
    {
      object obj = (object) null;
      ISettingsContainer userSettings = this.GetUserSettings(UserID);
      if (userSettings != null)
        obj = userSettings[Key];
      return obj;
    }
    set
    {
      ISettingsContainer userSettings = this.GetUserSettings(UserID);
      if (userSettings == null)
        return;
      userSettings[Key] = value;
    }
  }

  public bool LoadRolesSettings(object usrSession)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13874(140419789), (object) "VersionRulesCacheService.LoadRolesSettings");
    if (this.FRolesSettings == null)
      this.FRolesSettings = new HybridDictionary(0, true);
    IDBObjectCollection objectCollection = userSession.GetObjectCollection(userSession.IdentHelper.RolesTypeID);
    ColumnDescriptor[] columns = new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
    };
    object[] objArray = new object[0];
    SortOrders[] sortOrdersArray = new SortOrders[0];
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, columns);
    DataTable dataTable;
    try
    {
      dataTable = objectCollection.Select(paramSet);
    }
    catch
    {
      dataTable = (DataTable) null;
    }
    if (dataTable == null || dataTable.Rows.Count == 0)
      return false;
    lock (this.FRolesSettings.SyncRoot)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (row != null && !(this.FRolesSettings[(object) Convert.ToInt64(row[0])] is SettingsContainer))
        {
          SettingsContainer settingsContainer = new SettingsContainer(Convert.ToInt64(row[0]), userSession.IdentHelper.SettingsAttributeID, Convert.ToString(row[0]));
          settingsContainer.LoadFromObject(userSession);
          this.FRolesSettings.Add((object) Convert.ToInt64(row[0]), (object) settingsContainer);
        }
      }
      return true;
    }
  }

  public bool SaveRolesSettings(object usrSession, long RoleID)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13875(21520809), (object) "VersionRulesCacheService.SaveRolesSettings");
    return this.FRolesSettings[(object) RoleID] is SettingsContainer frolesSetting && frolesSetting.SaveToObject(userSession);
  }

  public object GetRoleSettingsObject(long RoleID, object Key)
  {
    if (this.FRolesSettings == null)
      return (object) null;
    return !(this.FRolesSettings[(object) RoleID] is SettingsContainer frolesSetting) ? (object) null : frolesSetting[Key];
  }

  public void SetRoleSettingsObject(long RoleID, object Key, object value)
  {
    if (this.FRolesSettings == null || !(this.FRolesSettings[(object) RoleID] is SettingsContainer frolesSetting))
      return;
    frolesSetting[Key] = value;
  }

  internal abstract class VersionRulesCacheServiceConsts
  {
    internal static readonly string VarsRemark = LocalizationHolder.rm.GetString("Kernel_650");
    internal static readonly string TuneRemark = LocalizationHolder.rm.GetString("Kernel_651");
    internal static readonly string Exception1 = LocalizationHolder.rm.GetString("Kernel_652");
  }
}
