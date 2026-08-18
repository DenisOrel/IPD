// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBLanguageCollection
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
using System.Text;


namespace Intermech.Kernel;

public class DBLanguageCollection : DBCollection, IDBLanguageCollection, IDBCollection, IDBSecurity
{
  public static string DefaultLanguage = string.Empty;
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(3);

  public DBLanguageCollection(UserSession uSession)
    : base(uSession, false)
  {
    this._DBTableName = "IMS_LANGUAGES";
    this._DBKeyField = "";
    this._AreaSupport = false;
    this._LanguageSupport = false;
    this.InitSecurityOptions(9, 0L);
  }

  static DBLanguageCollection()
  {
    DBLanguageCollection.metadataActions.Add(ActionType.GetAccess, false);
    DBLanguageCollection.metadataActions.Add(ActionType.SetAccess, false);
    DBLanguageCollection.metadataActions.Add(ActionType.Create, false);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBLanguageCollection.metadataActions);
  }

  public override string ObjectName => LocalizationHolder.rm.GetString("Kernel_732");

  public string DefaultLanguageID => DBLanguageCollection.DefaultLanguage;

  public char Create(string languageName, Guid guid, string cultureID)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    char ch = SqlHelper.NextLetter(dataManager.ExecuteDataTable(sc_12296.ssp_appserver_12297()).Rows);
    this._LastEventID = this.AddEvent(0L, ActionType.Create, EventlogRecordType.AccessDenied, string.Format(LocalizationHolder.rm.GetString("Kernel_733"), (object) languageName));
    this.CheckAccess(ActionType.Create);
    try
    {
      SqlHelper.ValidateEmptyValue(languageName, LocalizationHolder.rm.GetString("Kernel_734"));
      if (guid == Guid.Empty)
        guid = Guid.NewGuid();
      SqlHelper.ValidateEmptyValue(cultureID, LocalizationHolder.rm.GetString("Kernel_735"));
      if (this.UserSession.DBCache.GetTable("IMS_LANGUAGES").Select("F_CULTURE_ID = " + SqlHelper.QString(cultureID)).Length != 0)
        throw new KernelExceptionID(sc_12296.ssp_appserver_12298(1585853394), (object) cultureID);
      dataManager.ExecuteNonQuery(sc_12296.ssp_appserver_12299(), dataManager.Parameter(sc_12296.ssp_appserver_12300(), (object) ch.ToString()), dataManager.Parameter("name", (object) languageName), dataManager.Parameter(sc_12296.ssp_appserver_12301(), (object) guid.ToString()), dataManager.Parameter("cultID", (object) cultureID));
      this.UserSession.DBCache.ReloadTables((IUserSession) this.UserSession, dataManager, "IMS_LANGUAGES");
      (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).CloseEvent(this._LastEventID, 0L, 0L, string.Format(LocalizationHolder.rm.GetString("Kernel_736"), (object) languageName), "", EventlogRecordType.AccessGranted, (IUserSession) this.UserSession);
      return ch;
    }
    catch (Exception ex)
    {
      string str = LocalizationHolder.rm.GetString(sc_12296.ssp_appserver_12302());
      if (ex.Message.IndexOf("IMS_LANGUAGES_LANGUAGE_NAME") >= 0)
      {
        string message = string.Format(LocalizationHolder.rm.GetString(sc_12296.ssp_appserver_12303()), (object) languageName);
        this.CloseEvent(this._LastEventID, EventlogRecordType.Error, str + message);
        throw new AlreadyExistsException(message);
      }
      this.CloseEvent(this._LastEventID, EventlogRecordType.Error, str + ex.Message);
      throw;
    }
  }

  private string GetAllLanguages()
  {
    StringBuilder stringBuilder = new StringBuilder("");
    foreach (DataRow row in (InternalDataCollectionBase) this.UserSession.DBCache.GetTable("IMS_LANGUAGES").Rows)
      stringBuilder.Append(row["F_LANGUAGE_ID"]);
    return stringBuilder.ToString();
  }

  public override DataTable Select(string orderBy, params object[] addInfo)
  {
    this._LastEventID = 0L;
    return base.Select(orderBy, addInfo);
  }

  public void CheckValidLanguageID(string languageIDs)
  {
    if (languageIDs.Length <= 0)
      return;
    string allLanguages = this.GetAllLanguages();
    for (int index = 0; index < languageIDs.Length; ++index)
    {
      if (allLanguages.IndexOf(languageIDs[index]) == -1)
        throw new InvalidLanguageIDException(languageIDs);
    }
  }
}
