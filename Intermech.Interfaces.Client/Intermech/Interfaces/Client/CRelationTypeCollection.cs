// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CRelationTypeCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Summary description for CRelationTypeCollection.</summary>
internal class CRelationTypeCollection : 
  CacheObjectsCollection,
  IDBRelationTypeCollection,
  IDBCollection,
  IDBSecurity
{
  public CRelationTypeCollection(ClientSession uSession, bool filterRecs)
    : base(uSession, filterRecs)
  {
    this.InitOptions("IMS_RELATION_TYPES", "F_RELATION_TYPE");
    this.ParentID = (object) 0;
  }

  public DataTable GetUsedByAttribute(int attributeID)
  {
    this._clientSession.Guard.ValidateCall();
    StringBuilder stringBuilder = new StringBuilder();
    DataTable table = this._clientSession.ClientCache.GetTable("IMS_ATTR4RELATION_TYPES");
    DataRow[] dataRowArray = table.Select("F_ATTRIBUTE_ID = " + attributeID.ToString());
    int columnIndex = table.Columns.IndexOf("F_RELATION_TYPE");
    if (dataRowArray.Length == 0)
    {
      stringBuilder.Append("-1");
    }
    else
    {
      stringBuilder.Append(dataRowArray[0][columnIndex].ToString());
      for (int index = 1; index < dataRowArray.Length; ++index)
        stringBuilder.AppendFormat(",{0}", dataRowArray[index][columnIndex]);
    }
    DataTable usedByAttribute = this._clientSession.ClientCache.GetTable("IMS_RELATION_TYPES").Clone();
    DataRow[] fromRows = this._clientSession.ClientCache.GetTable("IMS_RELATION_TYPES").Select($"F_RELATION_TYPE IN ({stringBuilder.ToString()})");
    DataSetProcessor.FillCaptions(usedByAttribute);
    DataSetProcessor.AssignRows(usedByAttribute, (IEnumerable<DataRow>) fromRows);
    return usedByAttribute;
  }

  public int Create(RelationTypeProperties relationProperties)
  {
    this._clientSession.Guard.ValidateCall();
    int num = this._clientSession.Session.GetRelationTypeCollection(this._Filtering).Create(relationProperties);
    if (num <= 0)
      return num;
    this.ReloadCache(6);
    return num;
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
    return this._clientSession.Session.GetRelationTypeCollection(this._Filtering).GetVisibleList();
  }

  public string ObjectName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return LocalizationHolder.rm.GetString("Interfaces.Client_34");
    }
  }

  public bool CheckAccess(ActionType rightID, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetRelationTypeCollection() as IDBSecurity).CheckAccess(rightID, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID)
  {
    this._clientSession.Guard.ValidateCall();
    return this.CheckAccess(rightID, true);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetRelationTypeCollection() as IDBSecurity).CheckAccess(rightID, defaultAccess, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, CheckAccessFlags flags)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetRelationTypeCollection() as IDBSecurity).CheckAccess(rightID, defaultAccess, flags);
  }

  public bool IsAccessTypeDeny
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetRelationTypeCollection() as IDBSecurity).IsAccessTypeDeny;
    }
  }

  public bool IsLastDefault
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetRelationTypeCollection() as IDBSecurity).IsLastDefault;
    }
  }

  public CategoryDescriptor Descriptor
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return new CategoryDescriptor(6, 0L);
    }
  }

  public DataTable GetAccessList(out ActionProperties[] actions, out QuickObjectInfo[] users)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetRelationTypeCollection() as IDBSecurity).GetAccessList(out actions, out users);
  }

  public void SetAccess(DataTable accessList, params object[] AddInfo)
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.ClientCache.ClearVisibleList(6);
    (this._clientSession.Session.GetRelationTypeCollection() as IDBSecurity).SetAccess(accessList, AddInfo);
  }

  public IDBSecurity[] GetRelatedSecurity()
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetRelationTypeCollection() as IDBSecurity).GetRelatedSecurity();
  }

  public void RestoreAdminAccess()
  {
    this._clientSession.Guard.ValidateCall();
    (this._clientSession.Session.GetRelationTypeCollection() as IDBSecurity).RestoreAdminAccess();
  }
}
