// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBLanguageType
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

public class DBLanguageType : DBSessionable, IDeletable, IDBLanguageType, IDBGuid
{
  private string _LanguageID;
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(4);

  static DBLanguageType()
  {
    DBLanguageType.metadataActions.Add(ActionType.GetAccess, false);
    DBLanguageType.metadataActions.Add(ActionType.SetAccess, false);
    DBLanguageType.metadataActions.Add(ActionType.Delete, false);
    DBLanguageType.metadataActions.Add(ActionType.EditProperties, false);
  }

  public DBLanguageType(UserSession uSession, string aLanguageID)
    : base(uSession)
  {
    this._LanguageID = aLanguageID.Trim();
    DataTable table = this.UserSession.DBCache.GetTable("IMS_LANGUAGES");
    if (this._LanguageID == "")
    {
      DataRow row = table.NewRow();
      row["F_LANGUAGE_ID"] = (object) "";
      row["F_LANGUAGE_NAME"] = (object) LocalizationHolder.rm.GetString("Kernel_710");
      row["F_DEFAULT"] = (object) 1;
      this.paramsTable.Create(row);
    }
    else
    {
      this.paramsTable.Create(table.Rows.Find((object) aLanguageID));
      if (this.paramsTable.RowsCount == 0)
        throw new KernelExceptionID(sc_12269.ssp_appserver_12270(1955525395), (object) aLanguageID);
    }
    this.InitSecurityOptions(9, 0L);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBLanguageType.metadataActions);
  }

  private void ValidateSystemLang()
  {
    if (this.LanguageID == "")
      throw new KernelException(LocalizationHolder.rm.GetString(sc_12269.ssp_appserver_12271()));
  }

  public override string ObjectName
  {
    get => string.Format(LocalizationHolder.rm.GetString("Kernel_712"), (object) this.LanguageName);
  }

  private void CheckLangTable(string tableName, string categoryName)
  {
    if (Convert.ToInt64(this.UserSession.DataManager.ExecuteScalar($"{sc_12269.ssp_appserver_12272()}{tableName} WHERE F_LANGUAGE_ID LIKE {SqlHelper.QString($"%{this.LanguageID.ToString()}%")}")) > 0L)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_713"), (object) this.LanguageName, (object) categoryName));
  }

  public int Delete(long DeleteMode)
  {
    this.UserSession.ValidateSystemDelete((object) this, LocalizationHolder.rm.GetString(sc_12269.ssp_appserver_12273()));
    this.ValidateSystemLang();
    long EventID = this.AddEvent(0L, ActionType.Delete, EventlogRecordType.AccessDenied);
    this.CheckAccess(ActionType.Delete);
    if (!this.UserSession.CanChangeObject(9, (object) this.LanguageID))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_919"), (object) this.LanguageName));
    try
    {
      if (this.IsDefaultLanguage)
        throw new KernelException(LocalizationHolder.rm.GetString(sc_12269.ssp_appserver_12274()));
      IDbManager dataManager = this.UserSession.DataManager;
      this.CheckLangTable("IMS_ATTR_GROUPS", LocalizationHolder.rm.GetString(sc_12269.ssp_appserver_12275()));
      this.CheckLangTable("IMS_ATTRIBUTES", LocalizationHolder.rm.GetString(sc_12269.ssp_appserver_12276()));
      string commandText = sc_12269.ssp_appserver_12277() + SqlHelper.QString(this.LanguageID);
      dataManager.ExecuteNonQuery(commandText);
      this.UserSession.DBCache.DeleteRecords("IMS_LANGUAGES", "F_LANGUAGE_ID = " + SqlHelper.QString(this.LanguageID), (IUserSession) this.UserSession);
      this.Deleted = true;
    }
    catch (Exception ex)
    {
      this.CloseEvent(EventID, EventlogRecordType.Error, ex.Message);
      throw;
    }
    return 0;
  }

  public string LanguageID => this._LanguageID;

  public string LanguageName
  {
    get => this.paramsTable[109].ToString();
    set
    {
      if (!(this.LanguageName != value))
        return;
      this.ValidateSystemLang();
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_718") + value : LocalizationHolder.rm.GetString("Kernel_719"));
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_LANGUAGE_NAME");
      try
      {
        SqlHelper.ValidateEmptyValue(value, LocalizationHolder.rm.GetString("Kernel_720"));
        SqlHelper.ValidateFieldLength(LocalizationHolder.rm.GetString("MDName"), value.Length, Consts.MaxObjectNameLength);
        this.UserSession.DataManager.ExecuteNonQuery($"{sc_12269.ssp_appserver_12278()}F_LANGUAGE_NAME = {SqlHelper.QString(value)}{sc_12269.ssp_appserver_12279()}{SqlHelper.QString(this.LanguageID)}");
        this.UserSession.DBCache.ChangeTableValue(sc_12269.ssp_appserver_12280() + SqlHelper.QString(this.LanguageID), "IMS_LANGUAGES", "F_LANGUAGE_NAME", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[109] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_721");
        if (ex.Message.IndexOf(sc_12269.ssp_appserver_12281()) >= 0)
        {
          string message = string.Format(LocalizationHolder.rm.GetString("Kernel_722"), (object) value);
          this.CloseEvent(EventID, EventlogRecordType.Error, str + message);
          throw new AlreadyExistsException(message);
        }
        this.CloseEvent(EventID, EventlogRecordType.Error, str + ex.Message);
        throw;
      }
    }
  }

  public string CultureID
  {
    get => this.paramsTable[23].ToString();
    set
    {
      if (!(this.CultureID != value))
        return;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_723") + value : LocalizationHolder.rm.GetString("Kernel_724"));
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_CULTURE_ID");
      try
      {
        SqlHelper.ValidateEmptyValue(value, LocalizationHolder.rm.GetString("Kernel_725"));
        foreach (DataRow dataRow in this.UserSession.DBCache.GetTable("IMS_LANGUAGES").Select("F_LANGUAGE_ID <> " + SqlHelper.QString(this.LanguageID)))
        {
          if (dataRow["F_CULTURE_ID"].ToString() == value)
            throw new KernelExceptionID(sc_12269.ssp_appserver_12282(68016449), (object) value);
        }
        string.Format(LocalizationHolder.rm.GetString("Kernel_726"), (object) value);
        this.UserSession.DataManager.ExecuteNonQuery($"{sc_12269.ssp_appserver_12283()}F_CULTURE_ID = {SqlHelper.QString(value)}{sc_12269.ssp_appserver_12284()}F_LANGUAGE_ID = {SqlHelper.QString(this.LanguageID)}");
        this.UserSession.DBCache.ChangeTableValue($"F_LANGUAGE_ID{sc_12269.ssp_appserver_12285()}{SqlHelper.QString(this.LanguageID)}", "IMS_LANGUAGES", "F_CULTURE_ID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[23] = (object) value;
      }
      catch (Exception ex)
      {
        this.CloseEvent(EventID, EventlogRecordType.Error, LocalizationHolder.rm.GetString("Kernel_727") + ex.Message);
        throw;
      }
    }
  }

  public bool IsDefaultLanguage
  {
    get => Convert.ToBoolean(this.paramsTable[108]);
    set
    {
      if (this.IsDefaultLanguage == value)
        return;
      this.ValidateSystemLang();
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_DEFAULT");
      this.UserSession.StartTransaction();
      try
      {
        if (value)
        {
          this.UserSession.DataManager.ExecuteNonQuery(sc_12269.ssp_appserver_12286() + SqlHelper.QString(this.LanguageID));
          this.UserSession.DataManager.ExecuteNonQuery(sc_12269.ssp_appserver_12287() + SqlHelper.QString(this.LanguageID));
        }
        else
        {
          DataTable table = this.UserSession.DBCache.GetTable("IMS_LANGUAGES");
          bool flag = true;
          foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
          {
            if (Convert.ToString(row["F_LANGUAGE_ID"]) != this.LanguageID)
            {
              flag = false;
              this.UserSession.DataManager.ExecuteNonQuery(sc_12269.ssp_appserver_12288() + SqlHelper.QString(Convert.ToString(row["F_LANGUAGE_ID"])));
              break;
            }
          }
          if (flag)
            throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12269.ssp_appserver_12289()), (object) this.LanguageName));
          this.UserSession.DataManager.ExecuteNonQuery(sc_12269.ssp_appserver_12290() + SqlHelper.QString(this.LanguageID));
        }
        this.UserSession.DBCache.ReloadTables((IUserSession) this.UserSession, this.UserSession.DataManager, "IMS_LANGUAGES");
        this.paramsTable[108] = (object) Convert.ToInt32(value);
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        this.CloseEvent(EventID, EventlogRecordType.Error, LocalizationHolder.rm.GetString("Kernel_729") + ex.Message);
        throw;
      }
    }
  }

  private void CheckChangeEnable(string propertyID)
  {
    if (!this.UserSession.CanChangeObjectElement(9, (object) this.LanguageID, ObligatoryElementKeys.GetKeyForObjectProperty(propertyID)))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_914"), (object) DataSetProcessor.GetCaption(propertyID)));
  }

  public bool IsSystemGUID => SystemGUIDs.IsSystemGUID(this.GUID);

  public Guid GUID
  {
    get => new Guid(this.paramsTable[76].ToString());
    set
    {
      if (!(value != this.GUID))
        return;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_730") + value.ToString());
      this.CheckAccess(ActionType.EditProperties);
      if (!this.UserSession.CanChangeObject(9, (object) this.LanguageID))
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_914"), (object) DataSetProcessor.GetCaption("F_GUID")));
      try
      {
        if (!this.UserSession.DeveloperMode)
          throw new KernelExceptionID(sc_12269.ssp_appserver_12291(1143416252));
        this.UserSession.DataManager.ExecuteNonQuery(sc_12269.ssp_appserver_12292() + SqlHelper.QString(value.ToString()) + sc_12269.ssp_appserver_12293() + SqlHelper.QString(this.LanguageID.ToString()));
        this.UserSession.DBCache.ChangeTableValue(sc_12269.ssp_appserver_12294() + SqlHelper.QString(this.LanguageID.ToString()), "IMS_LANGUAGES", "F_GUID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[76] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString(sc_12269.ssp_appserver_12295()) + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }
}
