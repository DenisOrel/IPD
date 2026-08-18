// Decompiled with JetBrains decompiler
// Type: Intermech.Search.RecentObjects.RecentObjectsServerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Search.RecentObjects;

public sealed class RecentObjectsServerService : LongLifeObject, IRecentObjectsServerService
{
  private static readonly Guid RecentObjectsAdvancedUserSettingsKey = new Guid("{D67A0706-DC24-4AED-AD58-859F867736CF}");
  private static readonly string RecentObjectsMaxCountAdvancedUserSettingsKey = RecentObjectsServerService.RecentObjectsAdvancedUserSettingsKey.ToString() + ".Max";
  private static readonly string AllowableRecentObjectActionsAdvancedUserSettingsKey = RecentObjectsServerService.RecentObjectsAdvancedUserSettingsKey.ToString() + ".Actions";

  public long[] GetCurrentUserRecentObjects(Guid userSessionGuid)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this.GetCurrentUserRecentObjectsInternal();
  }

  public long[] GetOtherUserRecentObjects(Guid userSessionGuid, long userVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(userVersionID) ? this.GetOtherUserRecentObjects(userVersionID) : throw new ArgumentException();
  }

  public long[] GetRecentObjectsAccessSettings(Guid userSessionGuid)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this.GetRecentObjectsAccessSettings();
  }

  public void SetRecentObjectsAccessSettings(Guid userSessionGuid, long[] objectVersionIds)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (objectVersionIds == null || ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) objectVersionIds))
        throw new ArgumentException();
      this.SetRecentObjectsAccessSettings(objectVersionIds);
    }
  }

  public RecentObjectsSettings GetCurrentUserRecentObjectsSettings(Guid userSessionGuid)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this.GetCurrentUserRecentObjectsSettingsInternal();
  }

  public void SetCurrentUserRecentObjectsSettings(
    Guid userSessionGuid,
    RecentObjectsSettings recentObjectsSettings)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (recentObjectsSettings == null)
        throw new ArgumentNullException(nameof (recentObjectsSettings));
      this.SetCurrentUserRecentObjectsSettings(recentObjectsSettings);
    }
  }

  public void SaveCurrentUserRecentObjects(Guid userSessionGuid, long[] objectVersionIds)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (objectVersionIds == null || ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) objectVersionIds))
        throw new ArgumentException();
      this.SaveCurrentUserRecentObjects(((IEnumerable<long>) objectVersionIds).Distinct<long>().ToArray<long>());
    }
  }

  private void SaveCurrentUserRecentObjects(long[] objectVersionIds)
  {
    RecentObjectsServerService.MRUObjectActions mruObjectActions = new RecentObjectsServerService.MRUObjectActions();
    mruObjectActions.ObjectIds.AddRange((IEnumerable<long>) objectVersionIds);
    this.SaveCurrentUserMRUObjectActions(mruObjectActions);
  }

  private void SaveCurrentUserMRUObjectActions(
    RecentObjectsServerService.MRUObjectActions mruObjectActions)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      (sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService)[sessionKeeper.Session.UserID, (object) RecentObjectsServerService.RecentObjectsAdvancedUserSettingsKey] = (object) this.SerializeMRUObjectActions(mruObjectActions);
  }

  private long FindConfigurationForUser(long userVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(Constants.UserConfigurationObjectTypeID);
      if (objectCollection is DBRecordSet)
        ((DBRecordSet) objectCollection).GlobalSelectMode = true;
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
      // ISSUE: explicit reference operation
      (^ref dbRecordSetParams).Conditions = new ConditionStructure[1]
      {
        new ConditionStructure()
        {
          Attribute = (object) ObligatoryObjectAttributes.F_OWNER_ID,
          RelationalOperator = RelationalOperators.Equal,
          Value = (object) userVersionID,
          SQL = string.Empty
        }
      };
      dbRecordSetParams.Columns = new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      };
      dbRecordSetParams.RecordCount = -1;
      DBRecordSetParams paramSet = dbRecordSetParams;
      DataTable dataTable = objectCollection.Select(paramSet);
      return dataTable.Rows.Count > 0 ? DataSetProcessor.GetInt64Value(dataTable.Rows[0], 0, 0L) : 0L;
    }
  }

  private Dictionary<object, object> GetAdvancedUserSettings(long userConfigurationVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new BlobProcReader(this.GetAdvancedUserSettingsDBAttribute(sessionKeeper.Session, userConfigurationVersionID), 0, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(sessionKeeper.Session);
        memoryStream.Seek(0L, SeekOrigin.Begin);
        if (!(new BinaryFormatter().Deserialize((Stream) memoryStream) is Dictionary<object, object> advancedUserSettings))
          advancedUserSettings = new Dictionary<object, object>();
        return advancedUserSettings;
      }
    }
  }

  private IDBAttribute GetAdvancedUserSettingsDBAttribute(
    IUserSession userSession,
    long userConfigurationVersionID)
  {
    IDBAttribute attributeById = userSession.GetObject(userConfigurationVersionID).GetAttributeByID(Constants.ConfigurationFilesAttributeTypeID);
    for (int index = 0; index < attributeById.ValuesCount && !(attributeById.AsString == "Advanced user settings"); ++index)
      attributeById.Index = index;
    return attributeById;
  }

  private byte[] SerializeMRUObjectActions(
    RecentObjectsServerService.MRUObjectActions mruObjectActions)
  {
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter()
      {
        Binder = ((SerializationBinder) new RecentObjectsServerService.RecentObjectCollectionSerializationBinder())
      }.Serialize((Stream) serializationStream, (object) mruObjectActions);
      return serializationStream.GetBuffer();
    }
  }

  private long[] GetCurrentUserRecentObjectsInternal()
  {
    RecentObjectsSettings settingsInternal = this.GetCurrentUserRecentObjectsSettingsInternal();
    return this.GetCurrentUserMRUObjectActions().ObjectIds.Where<long>((System.Func<long, bool>) (o => !ObjectHelper.IsUnknownObjectVersionID(o))).Reverse<long>().Take<long>(settingsInternal.RecentObjectsMaxCount).Reverse<long>().ToArray<long>();
  }

  private RecentObjectsServerService.MRUObjectActions GetCurrentUserMRUObjectActions()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      object serializedMRUObjectActions = (sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService)[sessionKeeper.Session.UserID, (object) RecentObjectsServerService.RecentObjectsAdvancedUserSettingsKey];
      return serializedMRUObjectActions is byte[] ? this.DeserializeMRUObjectActions((byte[]) serializedMRUObjectActions) ?? RecentObjectsServerService.MRUObjectActions.Empty : RecentObjectsServerService.MRUObjectActions.Empty;
    }
  }

  private RecentObjectsServerService.MRUObjectActions GetMRUObjectActions(long userVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ((IRecentObjectsSharingService) sessionKeeper.Session.GetCustomService(typeof (IRecentObjectsSharingService))).ValidateAccessMode(sessionKeeper.Session.SessionGUID, userVersionID);
    long configurationForUser = this.FindConfigurationForUser(userVersionID);
    if (!ObjectHelper.IsUnknownObjectVersionID(configurationForUser))
    {
      Dictionary<object, object> advancedUserSettings = this.GetAdvancedUserSettings(configurationForUser);
      object serializedMRUObjectActions = (object) null;
      // ISSUE: variable of a boxed type
      __Boxed<Guid> advancedUserSettingsKey = (System.ValueType) RecentObjectsServerService.RecentObjectsAdvancedUserSettingsKey;
      ref object local = ref serializedMRUObjectActions;
      if (advancedUserSettings.TryGetValue((object) advancedUserSettingsKey, out local) && serializedMRUObjectActions is byte[] && ((byte[]) serializedMRUObjectActions).Length != 0)
        return this.DeserializeMRUObjectActions((byte[]) serializedMRUObjectActions) ?? RecentObjectsServerService.MRUObjectActions.Empty;
    }
    return RecentObjectsServerService.MRUObjectActions.Empty;
  }

  private RecentObjectsServerService.MRUObjectActions DeserializeMRUObjectActions(
    byte[] serializedMRUObjectActions)
  {
    using (MemoryStream serializationStream = new MemoryStream(serializedMRUObjectActions))
      return new BinaryFormatter()
      {
        Binder = ((SerializationBinder) new RecentObjectsServerService.RecentObjectCollectionSerializationBinder())
      }.Deserialize((Stream) serializationStream) as RecentObjectsServerService.MRUObjectActions;
  }

  private long[] GetOtherUserRecentObjects(long userVersionID)
  {
    return this.GetMRUObjectActions(userVersionID).ObjectIds.ToArray();
  }

  private long[] GetRecentObjectsAccessSettings()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ((IRecentObjectsSharingService) sessionKeeper.Session.GetCustomService(typeof (IRecentObjectsSharingService))).GetAccessObjectIDs(sessionKeeper.Session.SessionGUID);
  }

  private void SetRecentObjectsAccessSettings(long[] objectVersionIds)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ((IRecentObjectsSharingService) sessionKeeper.Session.GetCustomService(typeof (IRecentObjectsSharingService))).SetAccessObjectIDs(sessionKeeper.Session.SessionGUID, ((IEnumerable<long>) objectVersionIds).Distinct<long>().ToArray<long>());
  }

  private RecentObjectsSettings GetCurrentUserRecentObjectsSettingsInternal()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IVersionRulesCacheService customService = sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
      return new RecentObjectsSettings((int) (customService[sessionKeeper.Session.UserID, (object) RecentObjectsServerService.RecentObjectsMaxCountAdvancedUserSettingsKey] ?? (object) RecentObjectsSettings.Default.RecentObjectsMaxCount), (RecentObjectAction) (customService[sessionKeeper.Session.UserID, (object) RecentObjectsServerService.AllowableRecentObjectActionsAdvancedUserSettingsKey] ?? (object) RecentObjectsSettings.Default.AllowableRecentObjectActions));
    }
  }

  private void SetCurrentUserRecentObjectsSettings(RecentObjectsSettings recentObjectsSettings)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IVersionRulesCacheService customService = sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
      customService[sessionKeeper.Session.UserID, (object) RecentObjectsServerService.RecentObjectsMaxCountAdvancedUserSettingsKey] = (object) recentObjectsSettings.RecentObjectsMaxCount;
      customService[sessionKeeper.Session.UserID, (object) RecentObjectsServerService.AllowableRecentObjectActionsAdvancedUserSettingsKey] = (object) recentObjectsSettings.AllowableRecentObjectActions;
    }
  }

  private sealed class RecentObjectCollectionSerializationBinder : SerializationBinder
  {
    private const string AssemblyName = "Intermech.Interfaces.Client, Version=6.0.0.1, Culture=neutral, PublicKeyToken=null";
    private const string MRUObjectActionsTypeName = "Intermech.Interfaces.Client.MRUObjectActions";
    private const string MRUObjectActionTypeName = "Intermech.Interfaces.Client.MRUObjectAction";

    public override void BindToName(
      Type serializedType,
      out string assemblyName,
      out string typeName)
    {
      if (serializedType == typeof (RecentObjectsServerService.MRUObjectActions))
      {
        assemblyName = "Intermech.Interfaces.Client, Version=6.0.0.1, Culture=neutral, PublicKeyToken=null";
        typeName = "Intermech.Interfaces.Client.MRUObjectActions";
      }
      else if (serializedType == typeof (RecentObjectsServerService.MRUObjectAction))
      {
        assemblyName = "Intermech.Interfaces.Client, Version=6.0.0.1, Culture=neutral, PublicKeyToken=null";
        typeName = "Intermech.Interfaces.Client.MRUObjectAction";
      }
      else
        base.BindToName(serializedType, out assemblyName, out typeName);
    }

    public override Type BindToType(string assemblyName, string typeName)
    {
      switch (typeName)
      {
        case "Intermech.Interfaces.Client.MRUObjectActions":
          return typeof (RecentObjectsServerService.MRUObjectActions);
        case "Intermech.Interfaces.Client.MRUObjectAction":
          return typeof (RecentObjectsServerService.MRUObjectAction);
        case "Intermech.DataFormats.ObjectAction":
          return typeof (int);
        default:
          return Type.GetType(typeName);
      }
    }
  }

  [Serializable]
  private sealed class MRUObjectActions : List<RecentObjectsServerService.MRUObjectAction>
  {
    private List<long> _objectIDs = new List<long>();

    public static RecentObjectsServerService.MRUObjectActions Empty
    {
      get => new RecentObjectsServerService.MRUObjectActions();
    }

    public List<long> ObjectIds => this._objectIDs;
  }

  [Serializable]
  private sealed class MRUObjectAction
  {
    private long _objectID;
    private int _action;
    private DateTime _date;

    public long ObjectVersionID
    {
      get => this._objectID;
      set => this._objectID = value;
    }

    public int Action
    {
      get => this._action;
      set => this._action = value;
    }

    public DateTime DateTime
    {
      get => this._date;
      set => this._date = value;
    }
  }
}
