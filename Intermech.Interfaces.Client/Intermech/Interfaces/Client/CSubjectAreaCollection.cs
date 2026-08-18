// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CSubjectAreaCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using ImSSP;
using Intermech.Localization;
using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Summary description for CSubjectAreaTypeCollection. IDBSubjectAreaTypeCollection
/// </summary>
internal class CSubjectAreaCollection : 
  CacheObjectsCollection,
  IDBSubjectAreaCollection,
  IDBCollection,
  IDBSecurity
{
  public CSubjectAreaCollection(ClientSession uSession)
    : base(uSession, false)
  {
    this.InitOptions("IMS_SUBJECT_AREAS", "F_AREA_ID");
    this.ParentID = (object) 0;
  }

  public string GetValidAreaID(string anAreaID)
  {
    this._clientSession.Guard.ValidateCall();
    string validAreaId = "";
    DataTable table = this._clientSession.ClientCache.GetTable(this._DBTableName);
    for (int index = 0; index < anAreaID.Length; ++index)
    {
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
      {
        if ((int) row[this._DBKeyField].ToString()[0] == (int) anAreaID[index])
        {
          validAreaId += anAreaID[index].ToString();
          break;
        }
      }
    }
    return validAreaId;
  }

  /// <summary>
  /// Проверяет на валидность идентификаторы предметных областей
  /// и выдает исключение InvalidAreaIDException
  /// </summary>
  public void ValidateAriasID(string anAreaID)
  {
    this._clientSession.Guard.ValidateCall();
    if (anAreaID != this.GetValidAreaID(anAreaID))
      throw new InvalidAreaIDException(anAreaID);
  }

  /// <summary>
  /// Проверяет на допустимость присвоения метаданным строки с идентификаторами предметных областей
  /// </summary>
  public void ValidateAriasString(string anAreaID)
  {
    this._clientSession.Guard.ValidateCall();
    if (anAreaID.Length > Consts.MaxSubjectAreasCount)
      throw new KernelExceptionID(sc_10505.ssp_appserver_10506(601595557), (object) Consts.MaxSubjectAreasCount);
    this.ValidateAriasID(anAreaID);
  }

  public string GetAreasCaption(string areas)
  {
    this._clientSession.Guard.ValidateCall();
    return SubjectAreasHelper.GetAreasCaption(this._clientSession.ClientCache.GetTable(this._DBTableName), areas);
  }

  public char Create(string areaName, string areaNote, Guid guid)
  {
    this._clientSession.Guard.ValidateCall();
    int num = (int) this._clientSession.Session.GetSubjectAreaCollection().Create(areaName, areaNote, guid);
    this.ReloadCache(0);
    return (char) num;
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
    return this._clientSession.Session.GetSubjectAreaCollection().GetVisibleList();
  }

  public string ObjectName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return LocalizationHolder.rm.GetString("Interfaces.Client_36");
    }
  }

  public bool CheckAccess(ActionType rightID, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetSubjectAreaCollection() as IDBSecurity).CheckAccess(rightID, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetSubjectAreaCollection() as IDBSecurity).CheckAccess(rightID);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetSubjectAreaCollection() as IDBSecurity).CheckAccess(rightID, defaultAccess, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, CheckAccessFlags flags)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetSubjectAreaCollection() as IDBSecurity).CheckAccess(rightID, defaultAccess, flags);
  }

  public bool IsAccessTypeDeny
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetSubjectAreaCollection() as IDBSecurity).IsAccessTypeDeny;
    }
  }

  public bool IsLastDefault
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetSubjectAreaCollection() as IDBSecurity).IsLastDefault;
    }
  }

  public CategoryDescriptor Descriptor
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return new CategoryDescriptor(11, 0L);
    }
  }

  public DataTable GetAccessList(out ActionProperties[] actions, out QuickObjectInfo[] users)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetSubjectAreaCollection() as IDBSecurity).GetAccessList(out actions, out users);
  }

  public void SetAccess(DataTable accessList, params object[] AddInfo)
  {
    this._clientSession.Guard.ValidateCall();
    (this._clientSession.Session.GetSubjectAreaCollection() as IDBSecurity).SetAccess(accessList, AddInfo);
  }

  public IDBSecurity[] GetRelatedSecurity()
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetSubjectAreaCollection() as IDBSecurity).GetRelatedSecurity();
  }

  public void RestoreAdminAccess()
  {
    this._clientSession.Guard.ValidateCall();
    (this._clientSession.Session.GetSubjectAreaCollection() as IDBSecurity).RestoreAdminAccess();
  }
}
