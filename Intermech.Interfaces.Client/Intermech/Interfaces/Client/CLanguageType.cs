// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CLanguageType
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Summary description for CLanguage. IDBLanguage</summary>
internal class CLanguageType : CacheObjectBase<string>, IDBLanguageType, IDeletable
{
  public CLanguageType(ClientSession uSession, string aLanguageID)
    : base(uSession, aLanguageID)
  {
    this.InitOptions(9, 0L, "IMS_LANGUAGES", LocalizationHolder.rm.GetString("Interfaces.Client_118"));
  }

  public override object GetServerObject()
  {
    this._clientSession.Guard.ValidateCall();
    return (object) this._clientSession.Session.GetLanguage(this._id);
  }

  public void SetGUID(Guid guid)
  {
    this._clientSession.Guard.ValidateCall();
    this.GUID = guid;
  }

  public string LanguageName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_LANGUAGE_NAME"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.LanguageName != value))
        return;
      this._clientSession.Session.GetLanguage(this._id).LanguageName = value;
      this.ReloadClientCache();
    }
  }

  public string CultureID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_CULTURE_ID"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.CultureID != value))
        return;
      this._clientSession.Session.GetLanguage(this._id).CultureID = value;
      this.ReloadClientCache();
    }
  }

  public string LanguageID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._id;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      throw new OperationNotApplicableException();
    }
  }

  public bool IsDefaultLanguage
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToBoolean(this.paramsTable[0]["F_DEFAULT"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.IsDefaultLanguage == value)
        return;
      this._clientSession.Session.GetLanguage(this._id).IsDefaultLanguage = value;
      this.ReloadClientCache();
    }
  }

  public int Delete(long DeleteMode)
  {
    this._clientSession.Guard.ValidateCall();
    int num = (this._clientSession.Session.GetLanguage(this._id) as IDeletable).Delete(DeleteMode);
    this.ReloadClientCache();
    return num;
  }

  public override Guid GUID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return base.GUID;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.GUID != value))
        return;
      this._clientSession.Session.GetLanguage(this._id).GUID = value;
      this.ReloadClientCache();
    }
  }
}
