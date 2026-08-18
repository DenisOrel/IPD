// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBAttributesGroupCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel;

internal class DBAttributesGroupCollection : 
  DBCollection,
  IDBAttributesGroupCollection,
  IDBCollection,
  IDBSecurity
{
  private IDBAttributesGroup _ParentGroup;
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(3);

  public DBAttributesGroupCollection(UserSession uSession, bool filterRecs)
    : base(uSession, filterRecs)
  {
    this._DBTableName = "IMS_ATTR_GROUPS";
    this._DBKeyField = "F_GROUP_ID";
    this._AreaSupport = filterRecs;
    this._LanguageSupport = filterRecs;
    this.ParentID = (object) -1;
    this.InitSecurityOptions(12, 0L);
  }

  static DBAttributesGroupCollection()
  {
    DBAttributesGroupCollection.metadataActions.Add(ActionType.GetAccess, false);
    DBAttributesGroupCollection.metadataActions.Add(ActionType.SetAccess, false);
    DBAttributesGroupCollection.metadataActions.Add(ActionType.Create, false);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBAttributesGroupCollection.metadataActions);
  }

  protected override string GetParentSQL(object parentID)
  {
    return Convert.ToInt32(this.ParentID) < 0 ? string.Empty : $" F_PARENT_ID = {this.ParentID.ToString()} ";
  }

  public override object ParentID
  {
    get => base.ParentID;
    set
    {
      int int32 = Convert.ToInt32(value);
      this._ParentGroup = int32 <= 0 ? (IDBAttributesGroup) null : this.UserSession.GetAttributesGroup(int32, true);
      base.ParentID = value;
    }
  }

  public override string ObjectName => LocalizationHolder.rm.GetString("Kernel_31");

  public override long Create(params object[] properties)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    this._LastEventID = this.AddEvent(0L, ActionType.Create, EventlogRecordType.AccessDenied, string.Format(LocalizationHolder.rm.GetString("Kernel_32"), (object) properties[0].ToString()));
    this.CheckAccess(ActionType.Create);
    this.UserSession.StartTransaction();
    try
    {
      int num = 0;
      if (properties[2].ToString() != "")
        this.UserSession.GetLanguage(properties[2].ToString());
      this.UserSession.GetSubjectAreaCollection().ValidateAriasString(properties[3].ToString());
      SqlHelper.ValidateEmptyValue(properties[0].ToString(), LocalizationHolder.rm.GetString("Kernel_33"));
      if (new Guid(properties[4].ToString()) == Guid.Empty)
        properties[4] = (object) Guid.NewGuid().ToString();
      dataManager.ExecuteSpNonQuery("IMS_ADD_ATTR_GROUPS", dataManager.Parameter("inGROUP_NAME", (object) properties[0].ToString()), dataManager.Parameter("inNOTE", properties[1]), dataManager.Parameter("inAREA_ID", properties[3]), dataManager.Parameter("inLANGUAGE_ID", properties[2]), dataManager.Parameter("inGUID", properties[4]), dataManager.OutputParameter("outGROUP_ID", (object) num));
      int int32 = Convert.ToInt32(dataManager.GetOutputParameterValue("outGROUP_ID"));
      this.UserSession.DBCache.ReloadTables((IUserSession) this.UserSession, dataManager, "IMS_ATTR_GROUPS");
      (this.UserSession.GetAttributesGroup(int32) as DBAttributesGroup).SetCreatorAccess();
      if (this._ParentGroup != null)
        this.UserSession.GetAttributesGroup(int32).ParentID = this._ParentGroup.GroupID;
      (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).CloseEvent(this._LastEventID, 0L, (long) int32, string.Format(LocalizationHolder.rm.GetString("Kernel_34"), (object) properties[0].ToString()), "", EventlogRecordType.AccessGranted, (IUserSession) this.UserSession);
      this.UserSession.Commit();
      return (long) int32;
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      string str = LocalizationHolder.rm.GetString("Kernel_35");
      if (ex.Message.IndexOf("IMS_ATTR_GROUPS_GROUP_NAME") >= 0)
      {
        string message = string.Format(LocalizationHolder.rm.GetString("Kernel_36"), properties[0]);
        this.CloseEvent(this._LastEventID, EventlogRecordType.Error, str + message);
        throw new AlreadyExistsException(message);
      }
      this.CloseEvent(this._LastEventID, EventlogRecordType.Error, str + ex.Message);
      throw;
    }
  }

  public int Create(
    string groupName,
    string groupNote,
    string languageID,
    string areaID,
    Guid guid)
  {
    return Convert.ToInt32(this.Create((object) groupName, (object) groupNote, (object) languageID, (object) areaID, (object) guid.ToString()));
  }
}
