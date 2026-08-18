// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CAttributeTypeCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Summary description for AttributeTypeCollection.</summary>
internal class CAttributeTypeCollection : 
  CacheObjectsCollection,
  IDBAttributeTypeCollection,
  IDBCollection,
  IDBSecurity
{
  public CAttributeTypeCollection(ClientSession uSession, bool filterRecs)
    : base(uSession, filterRecs)
  {
    this.InitOptions("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID");
    this.ParentID = (object) 0;
  }

  /// <summary>Создает атрибут и возвращает его идентификатор</summary>
  public int Create(AttributeTypeProperties attrProperties)
  {
    this._clientSession.Guard.ValidateCall();
    int num = this._clientSession.Session.GetAttributeTypeCollection((int) this.ParentID, this._Filtering).Create(attrProperties);
    if (num <= 0)
      return num;
    this.ReloadCache(3);
    return num;
  }

  public AttributeTypePropertiesValidator GetValidatorForRelationType(int attributeID)
  {
    this._clientSession.Guard.ValidateCall();
    CAttributeType cattributeType = CAttributeTypeCreator.CreateCAttributeType(this._clientSession, attributeID);
    IDBAttributeTypeCollection attributeTypeCollection = (IDBAttributeTypeCollection) new CAttributeTypeCollection(this._clientSession, this._Filtering);
    attributeTypeCollection.ParentID = (object) -1;
    IDBAttributeTypeCollection attrTypeList = attributeTypeCollection;
    return AttributeCacheHelper.GetValidatorForObjectType((IDBAttributeType) cattributeType, attrTypeList);
  }

  public AttributeTypePropertiesValidator GetValidator(FieldTypes fldtype)
  {
    this._clientSession.Guard.ValidateCall();
    AttributeTypePropertiesValidator validator = new AttributeTypePropertiesValidator();
    string name = AttributeCacheHelper.FillValidator(ref validator, fldtype, this._clientSession.AreaID, this._clientSession.ClientCache.GetTable("IMS_ATTRIBUTES"));
    if (name == "")
    {
      validator.PossibleValuesTable = (DataTable) null;
    }
    else
    {
      DataRow[] fromRows = this._clientSession.ClientCache.GetTable("IMS_POSSIBLE_VALUES").Select("F_ATTRIBUTE_ID = 0 AND F_OBJECT_TYPE = 0 AND F_RELATION_TYPE = 0");
      DataTable toTable = new DataTable("IMS_POSSIBLE_VALUES");
      DataColumn column1 = new DataColumn(this._clientSession.ClientCache.GetTable("IMS_POSSIBLE_VALUES").Columns["F_INLIST_ID"].ColumnName, this._clientSession.ClientCache.GetTable("IMS_POSSIBLE_VALUES").Columns["F_INLIST_ID"].DataType);
      DataColumn column2 = new DataColumn(this._clientSession.ClientCache.GetTable("IMS_POSSIBLE_VALUES").Columns[name].ColumnName, this._clientSession.ClientCache.GetTable("IMS_POSSIBLE_VALUES").Columns[name].DataType);
      DataColumn column3 = new DataColumn(this._clientSession.ClientCache.GetTable("IMS_POSSIBLE_VALUES").Columns["F_DESCRIPTION"].ColumnName, this._clientSession.ClientCache.GetTable("IMS_POSSIBLE_VALUES").Columns["F_DESCRIPTION"].DataType);
      toTable.Columns.Add(column1);
      toTable.Columns.Add(column2);
      toTable.Columns.Add(column3);
      DataSetProcessor.AssignRows(toTable, (IEnumerable<DataRow>) fromRows);
      validator.PossibleValuesTable = toTable;
    }
    return validator;
  }

  public IDBAttributeType GetAttributeType(object objID, bool failIfNotFound)
  {
    this._clientSession.Guard.ValidateCall();
    IDBAttributeType attributeType = (IDBAttributeType) null;
    DataTable attributeTable = this._Filtering ? this._clientSession.ClientCache.GetFilteredTable("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID") : this._clientSession.ClientCache.GetTable("IMS_ATTRIBUTES");
    int attributeId = AttributeCacheHelper.GetAttributeID(objID, attributeTable, failIfNotFound);
    if (attributeId != 0)
    {
      try
      {
        attributeType = (IDBAttributeType) CAttributeTypeCreator.CreateCAttributeType(this._clientSession, attributeId);
      }
      catch
      {
        if (failIfNotFound)
          throw;
      }
    }
    return attributeType;
  }

  public IDBAttributeType[] GetAttributeTypeList(object[] idList, bool failIfNotFound)
  {
    this._clientSession.Guard.ValidateCall();
    if (idList == null)
      return new IDBAttributeType[0];
    IDBAttributeType[] attributeTypeList1 = (IDBAttributeType[]) new CAttributeType[idList.Length];
    int length = 0;
    foreach (object id in idList)
    {
      attributeTypeList1[length] = this.GetAttributeType(id, failIfNotFound);
      if (attributeTypeList1[length] != null)
        ++length;
    }
    if (length >= idList.Length)
      return attributeTypeList1;
    IDBAttributeType[] attributeTypeList2 = (IDBAttributeType[]) new CAttributeType[length];
    for (int index = 0; index < length; ++index)
      attributeTypeList2[index] = attributeTypeList1[index];
    return attributeTypeList2;
  }

  public AttributeTypePropertiesValidator GetValidatorForObjectType(int attributeID)
  {
    this._clientSession.Guard.ValidateCall();
    CAttributeType cattributeType = CAttributeTypeCreator.CreateCAttributeType(this._clientSession, attributeID);
    IDBAttributeTypeCollection attributeTypeCollection = (IDBAttributeTypeCollection) new CAttributeTypeCollection(this._clientSession, this._Filtering);
    attributeTypeCollection.ParentID = (object) -1;
    IDBAttributeTypeCollection attrTypeList = attributeTypeCollection;
    return AttributeCacheHelper.GetValidatorForObjectType((IDBAttributeType) cattributeType, attrTypeList);
  }

  /// <summary>
  /// Возвращает SQL-условие, отсеивающее только объекты, входящие в состав parentID
  /// </summary>
  /// <returns></returns>
  protected override string GetParentSQL()
  {
    return AttributeCacheHelper.GetAttributesForParentSQL(this._clientSession.ClientCache.GetTable("IMS_ATTR_IN_GROUPS"), this.ParentID);
  }

  public override DataTable Select(string orderBy, params object[] addInfo)
  {
    this._clientSession.Guard.ValidateCall();
    DataTable dataTable = base.Select(orderBy, addInfo);
    if (addInfo != null)
    {
      dataTable = AttributeCacheHelper.AddInfoToTable(dataTable, addInfo, (IUserSession) this._clientSession);
      this.FillCaptions(dataTable);
    }
    return dataTable;
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
    return this._clientSession.Session.GetAttributeTypeCollection((int) this.ParentID, this._Filtering).GetVisibleList();
  }

  public string ObjectName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return LocalizationHolder.rm.GetString("AttributesTypeObjectName");
    }
  }

  public bool CheckAccess(ActionType rightID, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetAttributeTypeCollection(0) as IDBSecurity).CheckAccess(rightID, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID)
  {
    this._clientSession.Guard.ValidateCall();
    return this.CheckAccess(rightID, true);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetAttributeTypeCollection(0) as IDBSecurity).CheckAccess(rightID, defaultAccess, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, CheckAccessFlags flags)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetAttributeTypeCollection(0) as IDBSecurity).CheckAccess(rightID, defaultAccess, flags);
  }

  public bool IsAccessTypeDeny
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetAttributeTypeCollection(0) as IDBSecurity).IsAccessTypeDeny;
    }
  }

  public bool IsLastDefault
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetAttributeTypeCollection(0) as IDBSecurity).IsLastDefault;
    }
  }

  public CategoryDescriptor Descriptor
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return new CategoryDescriptor(3, 0L);
    }
  }

  public DataTable GetAccessList(out ActionProperties[] actions, out QuickObjectInfo[] users)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetAttributeTypeCollection(0) as IDBSecurity).GetAccessList(out actions, out users);
  }

  public void SetAccess(DataTable accessList, params object[] AddInfo)
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.ClientCache.ClearVisibleList(3);
    (this._clientSession.Session.GetAttributeTypeCollection(0) as IDBSecurity).SetAccess(accessList, AddInfo);
  }

  public IDBSecurity[] GetRelatedSecurity()
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetAttributeTypeCollection(0) as IDBSecurity).GetRelatedSecurity();
  }

  public void RestoreAdminAccess()
  {
    this._clientSession.Guard.ValidateCall();
    (this._clientSession.Session.GetAttributeTypeCollection(0) as IDBSecurity).RestoreAdminAccess();
  }
}
