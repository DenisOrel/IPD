// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CObjectTypeCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Summary description for CObjectTypeCollection.</summary>
internal class CObjectTypeCollection : 
  CacheObjectsCollection,
  IDBObjectTypeCollection,
  IDBCollection,
  IDBSecurity
{
  /// <summary>
  /// Список типов объектов, видеть списки которых юзеру запрощено (кэш)
  /// </summary>
  internal static List<int> DisabledViewObjectTypes = (List<int>) null;
  /// <summary>
  /// Ид. юзера, для которого последний раз получали список DisabledViewObjectTypes
  /// </summary>
  internal static long DisabledViewUserID = -1;

  public CObjectTypeCollection(ClientSession uSession, int parentTypeID, bool filterRecs)
    : base(uSession, filterRecs)
  {
    this.InitOptions("IMS_OBJECT_TYPES", "F_OBJECT_TYPE");
    this.ParentID = (object) parentTypeID;
  }

  public List<int> GetDisabledAccess(ActionType at)
  {
    throw new Exception("Данный метод нельзя вызывать на стороне клиента!");
  }

  /// <summary>
  /// Метод проверяет имеет ли право данный юзер просматривать список объектов того типа, для которого создана данная коллекция.
  /// </summary>
  public bool CanViewObjects()
  {
    if (CObjectTypeCollection.DisabledViewObjectTypes == null || this._clientSession.UserID != CObjectTypeCollection.DisabledViewUserID)
    {
      CObjectTypeCollection.DisabledViewObjectTypes = this._clientSession.Session.GetObjectTypeCollection((int) this.ParentID).GetDisabledAccess(ActionType.View);
      CObjectTypeCollection.DisabledViewUserID = this._clientSession.UserID;
    }
    return CObjectTypeCollection.DisabledViewObjectTypes.IndexOf((int) this.ParentID) < 0;
  }

  public DataTable GetTypesHierarchy()
  {
    this._clientSession.Guard.ValidateCall();
    return this._Filtering ? this._clientSession.ClientCache.GetFilteredTable("IMS_OBJTYPES_TREE", this._DBKeyField) : this._clientSession.ClientCache.GetTable("IMS_OBJTYPES_TREE");
  }

  public DataTable SelectRecursive(string orderBy, params object[] addInfo)
  {
    this._clientSession.Guard.ValidateCall();
    if ((int) this.ParentID <= -1)
      throw new KernelExceptionID(164);
    DataTable dataTable = this.Select(orderBy, addInfo);
    DataTable toTable = dataTable.Copy();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      DataRow[] fromRows = this._clientSession.GetObjectTypeCollection(Convert.ToInt32(row["F_OBJECT_TYPE"]), this._Filtering).SelectRecursive(orderBy, addInfo).Select();
      DataSetProcessor.AssignRows(toTable, (IEnumerable<DataRow>) fromRows);
    }
    return toTable;
  }

  public DataTable GetUsedByAttribute(int attributeID)
  {
    this._clientSession.Guard.ValidateCall();
    return ObjectTypesCacheHelper.GetUsedByAttribute(this._clientSession.ClientCache.GetTable("IMS_ATTR4OBJ_TYPES"), this._clientSession.ClientCache.GetTable("IMS_OBJECT_TYPES"), attributeID);
  }

  public int Create(ObjectTypeProperties typeProperties)
  {
    this._clientSession.Guard.ValidateCall();
    int num = this._clientSession.Session.GetObjectTypeCollection((int) this.ParentID, this._Filtering).Create(typeProperties);
    if (num <= 0)
      return num;
    this.ReloadCache(4);
    return num;
  }

  protected override string GetParentSQL()
  {
    int[] visibleIDs = (int[]) null;
    if (this._Filtering)
      visibleIDs = this._clientSession.ClientCache.GetVisibleList(4);
    return ObjectTypesCacheHelper.GetParentSQL(this._clientSession.ClientCache.GetTable("IMS_OBJTYPES_TREE"), (int) this.ParentID, visibleIDs);
  }

  public override DataTable Select(string orderBy, params object[] addInfo)
  {
    this._clientSession.Guard.ValidateCall();
    return ObjectTypesCacheHelper.AddInfoToTable(this._clientSession.CacheDataSet, base.Select(orderBy, addInfo), addInfo);
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
    return this._clientSession.Session.GetObjectTypeCollection((int) this.ParentID, this._Filtering).GetVisibleList();
  }

  public string ObjectName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return LocalizationHolder.rm.GetString("Interfaces.Client_33");
    }
  }

  public bool CheckAccess(ActionType rightID, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetObjectTypeCollection(Convert.ToInt32(this.ParentID)) as IDBSecurity).CheckAccess(rightID, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID)
  {
    this._clientSession.Guard.ValidateCall();
    return this.CheckAccess(rightID, true);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetObjectTypeCollection(Convert.ToInt32(this.ParentID)) as IDBSecurity).CheckAccess(rightID, defaultAccess, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, CheckAccessFlags flags)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetObjectTypeCollection(Convert.ToInt32(this.ParentID)) as IDBSecurity).CheckAccess(rightID, defaultAccess, flags);
  }

  public bool IsAccessTypeDeny
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetObjectTypeCollection(Convert.ToInt32(this.ParentID)) as IDBSecurity).IsAccessTypeDeny;
    }
  }

  public bool IsLastDefault
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetObjectTypeCollection(Convert.ToInt32(this.ParentID)) as IDBSecurity).IsLastDefault;
    }
  }

  public CategoryDescriptor Descriptor
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return new CategoryDescriptor(4, 0L);
    }
  }

  public DataTable GetAccessList(out ActionProperties[] actions, out QuickObjectInfo[] users)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetObjectTypeCollection(Convert.ToInt32(this.ParentID)) as IDBSecurity).GetAccessList(out actions, out users);
  }

  public void SetAccess(DataTable accessList, params object[] AddInfo)
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.ClientCache.ClearVisibleList(4);
    (this._clientSession.Session.GetObjectTypeCollection(Convert.ToInt32(this.ParentID)) as IDBSecurity).SetAccess(accessList, AddInfo);
  }

  public IDBSecurity[] GetRelatedSecurity()
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetObjectTypeCollection(Convert.ToInt32(this.ParentID)) as IDBSecurity).GetRelatedSecurity();
  }

  public void RestoreAdminAccess()
  {
    this._clientSession.Guard.ValidateCall();
    (this._clientSession.Session.GetObjectTypeCollection(Convert.ToInt32(this.ParentID)) as IDBSecurity).RestoreAdminAccess();
  }
}
