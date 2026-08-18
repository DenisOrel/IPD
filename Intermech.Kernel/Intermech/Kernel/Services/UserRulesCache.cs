// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.UserRulesCache
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Kernel.Services;

internal sealed class UserRulesCache
{
  public static long FiltrationLife = 90;
  internal VersionRulesCacheService Owner;
  internal long UserID;
  private HybridDictionary FRuleVars = new HybridDictionary(0, true);
  private HybridDictionary FFiltrationTuning = new HybridDictionary(0, true);
  private Intermech.Interfaces.SettingsContainer FSettingsContainer = new Intermech.Interfaces.SettingsContainer();

  internal ISettingsContainer SettingsContainer => (ISettingsContainer) this.FSettingsContainer;

  public UserRulesCache()
  {
  }

  public UserRulesCache(VersionRulesCacheService AOwner, object usrSession)
  {
    this.Owner = AOwner;
    if (this.Owner == null)
      throw new KernelException(UserRulesCache.UserRulesCacheConsts.Exception1);
    this.UserID = (this.GetUserSession(usrSession) ?? throw new KernelExceptionID(210, (object) LocalizationHolder.rm.GetString("Kernel_649"))).UserID;
    this.Load(usrSession, true);
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

  private string GetKey(long Rule_Object_ID) => Convert.ToString(Rule_Object_ID);

  public void CheckRuleVars()
  {
    if (this.Owner == null)
      return;
    ArrayList arrayList = new ArrayList();
    bool flag = false;
    if (this.FRuleVars != null && this.FRuleVars.Count > 0)
    {
      lock (this.FRuleVars.SyncRoot)
      {
        IDictionaryEnumerator enumerator = this.FRuleVars.GetEnumerator();
        enumerator.Reset();
        while (enumerator.MoveNext())
        {
          long int64 = Convert.ToInt64(enumerator.Key);
          if (this.Owner[int64] == null)
          {
            if (arrayList.IndexOf((object) int64) < 0)
              arrayList.Add((object) int64);
            flag = true;
          }
        }
      }
    }
    if (arrayList.Count <= 0)
      return;
    for (int index = 0; index < arrayList.Count; ++index)
    {
      long int64 = Convert.ToInt64(arrayList[index]);
      if (flag)
      {
        lock (this.FRuleVars.SyncRoot)
          this.FRuleVars.Remove((object) int64);
      }
    }
  }

  public void PushRuleVarsActualDate()
  {
    if (this.Owner == null || this.FRuleVars == null || this.FRuleVars.Count <= 0)
      return;
    lock (this.FRuleVars.SyncRoot)
    {
      IDictionaryEnumerator enumerator = this.FRuleVars.GetEnumerator();
      enumerator.Reset();
      while (enumerator.MoveNext())
      {
        VersionsRule versionsRule = this.Owner[Convert.ToInt64(enumerator.Key)];
        if (enumerator.Value is ArrayList arrayList && arrayList.Count != 0)
        {
          for (int index = 0; index < arrayList.Count; ++index)
          {
            (arrayList[index] as VersionsRule).PushActualDate();
            (arrayList[index] as VersionsRule).ActualDate = DateTime.MinValue;
          }
        }
      }
    }
  }

  public void PopRuleVarsActualDate()
  {
    if (this.Owner == null || this.FRuleVars == null || this.FRuleVars.Count <= 0)
      return;
    lock (this.FRuleVars.SyncRoot)
    {
      IDictionaryEnumerator enumerator = this.FRuleVars.GetEnumerator();
      enumerator.Reset();
      while (enumerator.MoveNext())
      {
        VersionsRule versionsRule = this.Owner[Convert.ToInt64(enumerator.Key)];
        if (enumerator.Value is ArrayList arrayList && arrayList.Count != 0)
        {
          for (int index = 0; index < arrayList.Count; ++index)
            (arrayList[index] as VersionsRule).PopActualDate();
        }
      }
    }
  }

  public VersionsRule GetRuleVars(int index, long Rule_Object_ID)
  {
    if (Rule_Object_ID == 0L)
      return (VersionsRule) null;
    lock (this.FRuleVars.SyncRoot)
      return !(this.FRuleVars[(object) Rule_Object_ID] is ArrayList fruleVar) || index < 0 || index >= fruleVar.Count ? (VersionsRule) null : fruleVar[index] as VersionsRule;
  }

  public bool SetRuleVars(object usrSession, long Rule_Object_ID, int index, VersionsRule value)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13835(572778906), (object) "UserRulesCache.SetRuleVars");
    if (Rule_Object_ID == 0L || value == null)
      return false;
    lock (this.FRuleVars.SyncRoot)
    {
      VersionsRule versionsRule = this.Owner[Rule_Object_ID];
      if (versionsRule == null || !versionsRule.Valid(userSession) || !versionsRule.IsCompatible(value) || !(this.FRuleVars[(object) Rule_Object_ID] is ArrayList fruleVar))
        return false;
      int count = fruleVar.Count;
      if (index < 0 || index >= count)
        throw new KernelExceptionID(sc_13834.ssp_appserver_13836(647898857), (object) index, (object) 0, (object) count);
      value.RuleObjectModified = DateTime.UtcNow;
      fruleVar[index] = (object) value;
    }
    return true;
  }

  public void DeleteRuleVars(long Rule_Object_ID)
  {
    if (Rule_Object_ID == 0L)
      return;
    lock (this.FRuleVars.SyncRoot)
      this.FRuleVars.Remove((object) Rule_Object_ID);
  }

  public int RuleVarsCount(long Rule_Object_ID)
  {
    if (Rule_Object_ID == 0L)
      return 0;
    lock (this.FRuleVars.SyncRoot)
      return !(this.FRuleVars[(object) Rule_Object_ID] is ArrayList fruleVar) ? 0 : fruleVar.Count;
  }

  public ArrayList RuleVarsList(long Rule_Object_ID)
  {
    if (Rule_Object_ID == 0L)
      return (ArrayList) null;
    lock (this.FRuleVars.SyncRoot)
      return this.FRuleVars[(object) Rule_Object_ID] as ArrayList;
  }

  public int RuleVarsAdd(object usrSession, VersionsRule Vars, long Rule_Object_ID)
  {
    if (Vars == null)
      return -1;
    if (this.GetUserSession(usrSession) == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13837(1255650204), (object) "UserRulesCache.RuleVarsAdd");
    ArrayList arrayList = this.RuleVarsList(Rule_Object_ID);
    lock (this.FRuleVars.SyncRoot)
    {
      if (arrayList == null)
      {
        arrayList = new ArrayList();
        this.FRuleVars[(object) Rule_Object_ID] = (object) arrayList;
      }
      Vars.RuleObjectModified = DateTime.UtcNow;
      return arrayList.Add((object) Vars);
    }
  }

  public bool RuleVarsDel(object usrSession, long Rule_Object_ID, int index)
  {
    if (this.GetUserSession(usrSession) == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13838(936013631), (object) "UserRulesCache.RuleVarsDel");
    ArrayList arrayList = this.RuleVarsList(Rule_Object_ID);
    lock (this.FRuleVars.SyncRoot)
    {
      if (arrayList == null)
        return false;
      int count = arrayList.Count;
      if (index < 0 || index >= count)
        throw new KernelExceptionID(sc_13834.ssp_appserver_13839(976115329), (object) index, (object) 0, (object) count);
      arrayList.RemoveAt(index);
    }
    return true;
  }

  public void CheckFiltrationSettings()
  {
    if (this.Owner == null)
      return;
    ArrayList arrayList1 = new ArrayList();
    ArrayList arrayList2 = new ArrayList();
    bool flag = false;
    DateTime utcNow = DateTime.UtcNow;
    if (this.FFiltrationTuning != null && this.FFiltrationTuning.Count > 0)
    {
      lock (this.FFiltrationTuning.SyncRoot)
      {
        IDictionaryEnumerator enumerator = this.FFiltrationTuning.GetEnumerator();
        enumerator.Reset();
        while (enumerator.MoveNext())
        {
          if (enumerator.Value is FiltrationSettings filtrationSettings && filtrationSettings.CurrentRule != null)
          {
            if (filtrationSettings.CurrentRule.CurrentRuleType == VersionsRuleType.vrtStandardRule && filtrationSettings.CurrentRule.RuleObjectID == 0L)
              arrayList2.Add((object) filtrationSettings);
            else if ((long) (utcNow.Date - filtrationSettings.LastAccess.Date).Days >= UserRulesCache.FiltrationLife)
              arrayList2.Add((object) filtrationSettings);
            else if (this.Owner[filtrationSettings.CurrentRule.RuleObjectID] == null)
            {
              if (arrayList1.IndexOf((object) filtrationSettings.CurrentRule.RuleObjectID) < 0)
                arrayList1.Add((object) filtrationSettings.CurrentRule.RuleObjectID);
              filtrationSettings.CurrentRule = (VersionsRule) null;
              filtrationSettings.CurrentRuleVars = -1;
              flag = true;
            }
          }
        }
      }
    }
    if (arrayList1.Count > 0)
    {
      for (int index = 0; index < arrayList1.Count; ++index)
      {
        long int64 = Convert.ToInt64(arrayList1[index]);
        if (flag)
          this.DeleteRuleTuning(int64);
      }
    }
    if (arrayList2.Count <= 0)
      return;
    for (int index = 0; index < arrayList2.Count; ++index)
    {
      if (arrayList2[index] is FiltrationSettings filtrationSettings)
        this.DeleteRuleTuning(filtrationSettings.OwnerID);
    }
  }

  public bool DeleteRuleTuning(long Rule_Object_ID)
  {
    if (this.Owner == null || Rule_Object_ID == 0L || this.FFiltrationTuning == null || this.FFiltrationTuning.Count == 0 || this.FFiltrationTuning.SyncRoot == null)
      return false;
    lock (this.FFiltrationTuning.SyncRoot)
    {
      ArrayList arrayList = new ArrayList();
      IDictionaryEnumerator enumerator = this.FFiltrationTuning.GetEnumerator();
      if (enumerator == null)
        return false;
      enumerator.Reset();
      while (enumerator.MoveNext())
      {
        object key = enumerator.Key;
        if (enumerator.Value is FiltrationSettings filtrationSettings && key != null)
        {
          if (filtrationSettings.RuleVars == null)
            filtrationSettings.RuleVars = new Dictionary<long, int>();
          if (filtrationSettings.RuleVars.ContainsKey(Rule_Object_ID))
          {
            filtrationSettings.RuleVars.Remove(Rule_Object_ID);
            if (filtrationSettings.RuleVars.Count <= 0)
              arrayList.Add(key);
          }
        }
      }
      if (arrayList.Count > 0)
      {
        for (int index = 0; index < arrayList.Count; ++index)
          this.FFiltrationTuning.Remove(arrayList[index]);
      }
    }
    return true;
  }

  public bool DeleteRuleTuning(string OwnerID)
  {
    return this.SetFiltrationSettings(OwnerID, (FiltrationSettings) null);
  }

  public FiltrationSettings GetFiltrationSettings(string OwnerID)
  {
    return this.GetFiltrationSettings(OwnerID, true);
  }

  public FiltrationSettings GetFiltrationSettings(string OwnerID, bool GetDefaults)
  {
    filtrationSettings = (FiltrationSettings) null;
    lock (this.FFiltrationTuning.SyncRoot)
    {
      if (this.FFiltrationTuning[(object) OwnerID] is FiltrationSettings filtrationSettings)
        filtrationSettings.LastAccess = DateTime.UtcNow;
    }
    if (((filtrationSettings == null ? 1 : (filtrationSettings.OwnerID == null ? 1 : 0)) & (GetDefaults ? 1 : 0)) != 0)
    {
      filtrationSettings = new FiltrationSettings();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        filtrationSettings.CurrentRule = this.Owner.GetDefaultVersionRule(sessionKeeper.Session.SessionGUID);
    }
    if (filtrationSettings != null && filtrationSettings.CurrentRule == null && filtrationSettings.RuleID == 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        filtrationSettings.CurrentRule = this.Owner.GetDefaultVersionRule(sessionKeeper.Session.SessionGUID);
    }
    return filtrationSettings;
  }

  public bool SetFiltrationSettings(string OwnerID, FiltrationSettings value)
  {
    if (this.Owner == null)
      return false;
    if (this.FFiltrationTuning == null)
      this.FFiltrationTuning = new HybridDictionary();
    lock (this.FFiltrationTuning.SyncRoot)
    {
      if (value == null)
      {
        this.FFiltrationTuning.Remove((object) OwnerID);
      }
      else
      {
        value.LastAccess = DateTime.UtcNow;
        value.LastChangeTime = DateTime.UtcNow;
        if (value.CurrentRule != null && (value.CurrentRule.CurrentRuleType == VersionsRuleType.vrtLatestVersionsRule || value.CurrentRule.CurrentRuleType == VersionsRuleType.vrtAllVersionsRule) && value.RuleVars != null)
          value.RuleVars.Clear();
        this.FFiltrationTuning[(object) OwnerID] = (object) value;
      }
    }
    return true;
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
      filtrationRule = !defaults ? (VersionsRule) null : this.Owner.LatestVersionsRule;
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
    if (Filtration == null || Filtration.CurrentRule == null || Filtration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtLatestVersionsRule)
    {
      RuleCompatible = true;
      RuleValid = true;
      VarsOutOfRange = false;
      return this.Owner.LatestVersionsRule;
    }
    if (Filtration.CurrentRule != null && Filtration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtAllVersionsRule)
    {
      RuleCompatible = true;
      RuleValid = true;
      VarsOutOfRange = false;
      return this.Owner.AllVersionsRule;
    }
    if (Filtration.CurrentRule != null && Filtration.CurrentRule.RuleObjectID == 0L)
      return (VersionsRule) null;
    int currentRuleVars = Filtration.CurrentRuleVars;
    IUserSession userSession = MyUserSessionHelper.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13840(686272101), (object) "UserRulesCache.GetCurrentRule");
    VersionsRule filtrationRule1 = this.Owner[(object) userSession, Filtration.CurrentRule.RuleObjectID];
    if (filtrationRule1 == null)
      return (VersionsRule) null;
    if (currentRuleVars < 0 || filtrationRule1 != null && !filtrationRule1.HasVariableValues())
    {
      RuleCompatible = true;
      RuleValid = filtrationRule1 != null && filtrationRule1.Valid(userSession);
      VarsOutOfRange = false;
      return filtrationRule1;
    }
    VersionsRule filtrationRule2 = this.GetRuleVars(currentRuleVars, Filtration.CurrentRule.RuleObjectID);
    VarsOutOfRange = filtrationRule2 == null && currentRuleVars >= 0;
    if (filtrationRule2 == null)
      filtrationRule2 = filtrationRule1;
    if (!filtrationRule2.HasVariableValues())
      VarsOutOfRange = false;
    RuleCompatible = filtrationRule1.IsCompatible(filtrationRule2);
    RuleValid = filtrationRule2 != null && filtrationRule2.Valid(userSession);
    return filtrationRule2;
  }

  public string[] GetFiltrationSettingsList()
  {
    lock (this.FFiltrationTuning.SyncRoot)
    {
      string[] filtrationSettingsList = new string[this.FFiltrationTuning.Keys.Count];
      this.FFiltrationTuning.Keys.CopyTo((Array) filtrationSettingsList, 0);
      return filtrationSettingsList;
    }
  }

  public void LoadFiltrationTuning(object usrSession)
  {
    if (this.UserID == 0L)
      return;
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13841(1001468783), (object) "UserRulesCache.LoadFiltrationTuning");
    if (this.UserID != 0L && this.UserID != userSession.UserID)
      return;
    lock (this)
    {
      if (this.FFiltrationTuning != null)
        this.FFiltrationTuning.Clear();
      BlobInformation config_info = new BlobInformation(0L, 0L, DateTime.Now, string.Empty, ArcMethods.NotPacked, VersionRulesCacheService.VersionRulesCacheServiceConsts.TuneRemark);
      byte[] config_file = (byte[]) null;
      try
      {
        userSession.Configurations.LoadConfigData("Filtration tuning", out config_info, out config_file);
      }
      catch
      {
      }
      if (config_info.RealFileSize > 0L && config_file.Length != 0)
      {
        using (MemoryStream serializationStream = new MemoryStream(config_file))
        {
          try
          {
            this.FFiltrationTuning = new BinaryFormatter().Deserialize((Stream) serializationStream) as HybridDictionary;
          }
          catch
          {
            this.FFiltrationTuning = (HybridDictionary) null;
          }
        }
      }
      if (this.FFiltrationTuning != null)
        return;
      this.FFiltrationTuning = new HybridDictionary(0);
    }
  }

  public void LoadRuleVars(object usrSession)
  {
    if (this.UserID == 0L)
      return;
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13842(912315090), (object) "UserRulesCache.LoadRuleVars");
    if (this.UserID != 0L && this.UserID != userSession.UserID)
      return;
    lock (this)
    {
      if (this.FRuleVars != null)
        this.FRuleVars.Clear();
      BlobInformation config_info = new BlobInformation(0L, 0L, DateTime.Now, string.Empty, ArcMethods.NotPacked, VersionRulesCacheService.VersionRulesCacheServiceConsts.VarsRemark);
      byte[] config_file = (byte[]) null;
      try
      {
        userSession.Configurations.LoadConfigData("Rule variables", out config_info, out config_file);
      }
      catch
      {
      }
      if (config_info.RealFileSize > 0L && config_file.Length != 0)
      {
        using (MemoryStream serializationStream = new MemoryStream(config_file))
        {
          try
          {
            this.FRuleVars = new BinaryFormatter().Deserialize((Stream) serializationStream) as HybridDictionary;
          }
          catch
          {
            this.FRuleVars = (HybridDictionary) null;
          }
        }
      }
      if (this.FRuleVars != null)
        return;
      this.FRuleVars = new HybridDictionary(0, true);
    }
  }

  public bool LoadUserSettings(object usrSession)
  {
    if (this.FSettingsContainer == null)
      this.FSettingsContainer = new Intermech.Interfaces.SettingsContainer();
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13843(1266332490), (object) "UserRulesCache.LoadSettings");
    return (this.UserID == 0L || this.UserID == userSession.UserID) && this.FSettingsContainer.LoadFromUserConfig(userSession);
  }

  public void Load(object usrSession, bool ForceReload)
  {
    if (this.UserID == 0L)
      return;
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13844(647768928), (object) "UserRulesCache.Load");
    if (this.UserID != 0L && this.UserID != userSession.UserID)
      return;
    if (((this.FRuleVars == null ? 1 : (this.FRuleVars.Count == 0 ? 1 : 0)) | (ForceReload ? 1 : 0)) != 0)
      this.LoadRuleVars((object) userSession);
    if (((this.FFiltrationTuning == null ? 1 : (this.FFiltrationTuning.Count == 0 ? 1 : 0)) | (ForceReload ? 1 : 0)) != 0)
      this.LoadFiltrationTuning((object) userSession);
    if (this.FSettingsContainer != null && !(this.FSettingsContainer.OwnerID == string.Empty))
      return;
    this.LoadUserSettings((object) userSession);
  }

  public void SaveFiltrationTuning(object usrSession)
  {
    if (this.UserID == 0L)
      return;
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13845(775364441), (object) "UserRulesCache.SaveFiltrationTuning");
    if (this.FFiltrationTuning == null)
      this.FFiltrationTuning = new HybridDictionary(0, true);
    this.CheckFiltrationSettings();
    lock (this.FFiltrationTuning.SyncRoot)
    {
      using (MemoryStream serializationStream = new MemoryStream())
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, (object) this.FFiltrationTuning);
        BlobInformation config_info = new BlobInformation(serializationStream.Length, serializationStream.Length, DateTime.Now, "Filtration tuning", ArcMethods.NotPacked, string.Format(VersionRulesCacheService.VersionRulesCacheServiceConsts.TuneRemark, (object) this.FFiltrationTuning.Count));
        userSession.Configurations.WriteConfigData(config_info, serializationStream.ToArray());
      }
    }
  }

  public void SaveRuleVars(object usrSession)
  {
    if (this.UserID == 0L)
      return;
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13846(1777943052), (object) "UserRulesCache.SaveRuleVars");
    if (this.FRuleVars == null)
      this.FRuleVars = new HybridDictionary(0);
    this.CheckRuleVars();
    try
    {
      this.PushRuleVarsActualDate();
      lock (this.FRuleVars.SyncRoot)
      {
        int num = 0;
        int count = this.FRuleVars.Count;
        IDictionaryEnumerator enumerator = this.FRuleVars.GetEnumerator();
        enumerator.Reset();
        while (enumerator.MoveNext())
        {
          if (enumerator.Value is ArrayList arrayList)
            num += arrayList.Count;
        }
        using (MemoryStream serializationStream = new MemoryStream())
        {
          new BinaryFormatter().Serialize((Stream) serializationStream, (object) this.FRuleVars);
          BlobInformation config_info = new BlobInformation(serializationStream.Length, serializationStream.Length, DateTime.Now, "Rule variables", ArcMethods.NotPacked, string.Format(VersionRulesCacheService.VersionRulesCacheServiceConsts.VarsRemark, (object) count, (object) num));
          userSession.Configurations.WriteConfigData(config_info, serializationStream.ToArray());
        }
      }
    }
    finally
    {
      this.PopRuleVarsActualDate();
    }
  }

  public bool SaveUserSettings(object usrSession)
  {
    if (this.UserID == 0L)
      return false;
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13847(944242304), (object) "UserRulesCache.SaveUserSettings");
    if (this.FSettingsContainer == null)
      this.FSettingsContainer = new Intermech.Interfaces.SettingsContainer();
    return this.FSettingsContainer.SaveToUserConfig(userSession);
  }

  public void Save(object usrSession)
  {
    if (this.UserID == 0L)
      return;
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13834.ssp_appserver_13848(1539073708), (object) "UserRulesCache.Save");
    this.SaveRuleVars((object) userSession);
    this.SaveFiltrationTuning((object) userSession);
    this.SaveUserSettings((object) userSession);
  }

  private abstract class UserRulesCacheConsts
  {
    internal static readonly string Exception1 = LocalizationHolder.rm.GetString("Kernel_648");
  }
}
