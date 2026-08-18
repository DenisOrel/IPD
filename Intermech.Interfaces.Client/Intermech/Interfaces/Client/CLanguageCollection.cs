// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CLanguageCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.Data;
using System.Diagnostics;
using System.Text;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Summary description for CLanguageCollection. IDBLanguageCollection
/// </summary>
internal class CLanguageCollection : 
  CacheObjectsCollection,
  IDBLanguageCollection,
  IDBCollection,
  IDBSecurity
{
  public CLanguageCollection(ClientSession uSession)
    : base(uSession, false)
  {
    this.InitOptions("IMS_LANGUAGES", "F_LANGUAGE_ID");
    this.ParentID = (object) 0;
  }

  private string GetAllLanguages()
  {
    StringBuilder stringBuilder = new StringBuilder("");
    foreach (DataRow row in (InternalDataCollectionBase) this._clientSession.ClientCache.GetTable(this._DBTableName).Rows)
      stringBuilder.Append(row[this._DBKeyField]);
    return stringBuilder.ToString();
  }

  public void CheckValidLanguageID(string aLanguageID)
  {
    this._clientSession.Guard.ValidateCall();
    if (aLanguageID.Length <= 0)
      return;
    string allLanguages = this.GetAllLanguages();
    for (int index = 0; index < aLanguageID.Length; ++index)
    {
      if (allLanguages.IndexOf(aLanguageID[index]) == -1)
        throw new InvalidLanguageIDException(aLanguageID);
    }
  }

  public char Create(string languageName, Guid guid, string cultureID)
  {
    this._clientSession.Guard.ValidateCall();
    int num = (int) this._clientSession.Session.GetLanguageCollection().Create(languageName, guid, cultureID);
    this.ReloadCache(0);
    return (char) num;
  }

  public string DefaultLanguageID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._clientSession.ClientCache.GetTable(this._DBTableName).Select("F_DEFAULT = 1")[0]["F_LANGUAGE_ID"].ToString();
    }
  }

  public long Count
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (long) this.GetCount();
    }
  }

  public int[] GetVisibleList()
  {
    this._clientSession.Guard.ValidateCall();
    return this._clientSession.Session.GetLanguageCollection().GetVisibleList();
  }

  public string ObjectName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return LocalizationHolder.rm.GetString("Interfaces.Client_26");
    }
  }

  public CategoryDescriptor Descriptor
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return new CategoryDescriptor(9, 0L);
    }
  }

  public bool CheckAccess(ActionType rightID, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetLanguageCollection() as IDBSecurity).CheckAccess(rightID, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetLanguageCollection() as IDBSecurity).CheckAccess(rightID);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetLanguageCollection() as IDBSecurity).CheckAccess(rightID, defaultAccess, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, CheckAccessFlags flags)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetLanguageCollection() as IDBSecurity).CheckAccess(rightID, defaultAccess, flags);
  }

  public bool IsAccessTypeDeny
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetLanguageCollection() as IDBSecurity).IsAccessTypeDeny;
    }
  }

  public bool IsLastDefault
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetLanguageCollection() as IDBSecurity).IsLastDefault;
    }
  }

  public DataTable GetAccessList(out ActionProperties[] actions, out QuickObjectInfo[] users)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetLanguageCollection() as IDBSecurity).GetAccessList(out actions, out users);
  }

  public void SetAccess(DataTable accessList, params object[] AddInfo)
  {
    this._clientSession.Guard.ValidateCall();
    (this._clientSession.Session.GetLanguageCollection() as IDBSecurity).SetAccess(accessList, AddInfo);
  }

  public IDBSecurity[] GetRelatedSecurity()
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetLanguageCollection() as IDBSecurity).GetRelatedSecurity();
  }

  public void RestoreAdminAccess()
  {
    this._clientSession.Guard.ValidateCall();
    (this._clientSession.Session.GetLanguageCollection() as IDBSecurity).RestoreAdminAccess();
  }
}
