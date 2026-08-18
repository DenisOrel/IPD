// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBSubjectAreaCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

public class DBSubjectAreaCollection : 
  DBCollection,
  IDBSubjectAreaCollection,
  IDBCollection,
  IDBSecurity
{
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(5);

  public DBSubjectAreaCollection(UserSession uSession)
    : base(uSession, false)
  {
    this._DBTableName = "IMS_SUBJECT_AREAS";
    this._DBKeyField = "";
    this._AreaSupport = false;
    this._LanguageSupport = false;
    this.InitSecurityOptions(11, 0L);
  }

  static DBSubjectAreaCollection()
  {
    DBSubjectAreaCollection.metadataActions.Add(ActionType.GetAccess, false);
    DBSubjectAreaCollection.metadataActions.Add(ActionType.SetAccess, false);
    DBSubjectAreaCollection.metadataActions.Add(ActionType.Create, false);
    DBSubjectAreaCollection.metadataActions.Add(ActionType.Delete, false);
    DBSubjectAreaCollection.metadataActions.Add(ActionType.EditProperties, false);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBSubjectAreaCollection.metadataActions);
  }

  public override string ObjectName => LocalizationHolder.rm.GetString("Kernel_811");

  public override DataTable Select(string orderBy, params object[] addInfo)
  {
    this._LastEventID = 0L;
    return base.Select(orderBy, addInfo);
  }

  public char Create(string areaName, string areaNote, Guid guid)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    char ch = SqlHelper.NextLetter(dataManager.ExecuteDataTable(sc_12305.ssp_appserver_12306()).Rows);
    this._LastEventID = this.AddEvent(0L, ActionType.Create, EventlogRecordType.AccessDenied, string.Format(LocalizationHolder.rm.GetString("Kernel_812"), (object) areaName));
    this.CheckAccess(ActionType.Create);
    try
    {
      SqlHelper.ValidateEmptyValue(areaName, LocalizationHolder.rm.GetString("Kernel_813"));
      if (guid == Guid.Empty)
        guid = Guid.NewGuid();
      dataManager.ExecuteNonQuery(sc_12305.ssp_appserver_12307(), dataManager.Parameter(sc_12305.ssp_appserver_12308(), (object) ch.ToString()), dataManager.Parameter("name", (object) areaName), dataManager.Parameter("note", (object) areaNote), dataManager.Parameter(sc_12305.ssp_appserver_12309(), (object) guid.ToString()));
      this.UserSession.DBCache.ReloadTables((IUserSession) this.UserSession, dataManager, "IMS_SUBJECT_AREAS");
      (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).CloseEvent(this._LastEventID, 0L, 0L, string.Format(LocalizationHolder.rm.GetString("Kernel_814"), (object) areaName), "", EventlogRecordType.AccessGranted, (IUserSession) this.UserSession);
      return ch;
    }
    catch (Exception ex)
    {
      string str = LocalizationHolder.rm.GetString(sc_12305.ssp_appserver_12310());
      if (ex.Message.IndexOf("IMS_SUBJECT_AREAS_AREA_NAME") >= 0)
      {
        string message = string.Format(LocalizationHolder.rm.GetString(sc_12305.ssp_appserver_12311()), (object) areaName);
        this.CloseEvent(this._LastEventID, EventlogRecordType.Error, str + message);
        throw new AlreadyExistsException(message);
      }
      this.CloseEvent(this._LastEventID, EventlogRecordType.Error, str + ex.Message);
      throw;
    }
  }

  public string GetValidAreaID(string anAreaID)
  {
    string validAreaId = "";
    DataTable table = this.UserSession.DBCache.GetTable("IMS_SUBJECT_AREAS");
    for (int index = 0; index < anAreaID.Length; ++index)
    {
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
      {
        if ((int) row["F_AREA_ID"].ToString()[0] == (int) anAreaID[index])
        {
          validAreaId += anAreaID[index].ToString();
          break;
        }
      }
    }
    return validAreaId;
  }

  public void ValidateAriasID(string anAreaID)
  {
    if (anAreaID != this.GetValidAreaID(anAreaID))
      throw new InvalidAreaIDException(anAreaID);
  }

  public void ValidateAriasString(string anAreaID)
  {
    if (anAreaID.Length > Consts.MaxSubjectAreasCount)
      throw new KernelExceptionID(sc_12305.ssp_appserver_12312(737510225), (object) Consts.MaxSubjectAreasCount);
    this.ValidateAriasID(anAreaID);
  }

  public string GetAreasCaption(string areas)
  {
    return SubjectAreasHelper.GetAreasCaption(this.UserSession.DBCache.GetTable("IMS_SUBJECT_AREAS"), areas);
  }

  public static bool IsVisibleArea(string areas, string sessionAreas)
  {
    if (areas == string.Empty || sessionAreas == string.Empty)
      return true;
    for (int index = 0; index < areas.Length; ++index)
    {
      if (sessionAreas.IndexOf(areas[index]) > -1)
        return true;
    }
    return false;
  }
}
