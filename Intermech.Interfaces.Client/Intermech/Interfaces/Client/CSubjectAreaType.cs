// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CSubjectAreaType
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Summary description for CSubjectAreaType.</summary>
internal class CSubjectAreaType : CacheObjectBase<char>, IDBSubjectAreaType, IDeletable
{
  public CSubjectAreaType(ClientSession uSession, char anAreaID)
    : base(uSession, anAreaID)
  {
    this.InitOptions(11, 0L, "IMS_SUBJECT_AREAS", LocalizationHolder.rm.GetString("Interfaces.Client_124"));
  }

  public override object GetServerObject()
  {
    this._clientSession.Guard.ValidateCall();
    return (object) this._clientSession.Session.GetSubjectAreaType(this._id);
  }

  public void SetGUID(Guid guid)
  {
    this._clientSession.Guard.ValidateCall();
    this.GUID = guid;
  }

  public string AreaName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_AREA_NAME"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.AreaName != value))
        return;
      this._clientSession.Session.GetSubjectAreaType(this._id).AreaName = value;
      this.ReloadClientCache();
    }
  }

  public char AreaID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._id;
    }
  }

  public int Delete(long DeleteMode)
  {
    this._clientSession.Guard.ValidateCall();
    int num = this._clientSession.Session.GetSubjectAreaType(this._id).Delete(DeleteMode);
    this.ReloadClientCache();
    return num;
  }

  public string Note
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_AREA_NOTE"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.Note != value))
        return;
      this._clientSession.Session.GetSubjectAreaType(this._id).Note = value;
      this.ReloadClientCache();
    }
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
      if (!(value != this.GUID))
        return;
      this._clientSession.Session.GetSubjectAreaType(this._id).SetGUID(value);
      this.ReloadClientCache();
    }
  }
}
