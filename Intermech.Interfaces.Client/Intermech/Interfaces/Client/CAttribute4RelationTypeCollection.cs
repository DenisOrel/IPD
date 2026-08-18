// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CAttribute4RelationTypeCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// 
/// </summary>
internal class CAttribute4RelationTypeCollection : 
  CacheObjectsCollection,
  IDBAttribute4RelationTypeCollection,
  IDBAttribute4TypeCollection,
  IDBCollection
{
  private FieldTypes _ftFilter;

  public CAttribute4RelationTypeCollection(
    ClientSession uSession,
    int relationTypeID,
    bool filterRecs)
    : base(uSession, filterRecs)
  {
    this.ParentID = (object) relationTypeID;
    this.InitOptions("IMS_ATTR4RELATION_TYPES", "F_ATTRIBUTE_ID");
  }

  public IDBAttributeType[] GetAttributeTypeList(object[] idList, bool failIfNotFound)
  {
    throw new OperationNotApplicableException();
  }

  public IDBAttributeType4Relation Create(Attribute4RelationTypeProperties attrProperties)
  {
    this._clientSession.Guard.ValidateCall();
    IDBAttributeType4Relation attributeType4Relation = ((IDBAttribute4RelationTypeCollection) this._clientSession.Session.GetRelationType((int) this.ParentID).Attributes).Create(attrProperties);
    if (attributeType4Relation == null)
      return attributeType4Relation;
    this.ReloadCache(3);
    return attributeType4Relation;
  }

  public IDBAttributeType4 GetAttributeByID(int attributeID, bool throwNotFoundException)
  {
    this._clientSession.Guard.ValidateCall();
    DataRow row = this._clientSession.ClientCache.GetTable("IMS_ATTR4RELATION_TYPES").Rows.Find(new object[2]
    {
      (object) (int) this.ParentID,
      (object) attributeID
    });
    if (row != null)
      return Convert.ToInt32(this._clientSession.ClientCache.GetTable("IMS_ATTRIBUTES").Rows.Find((object) attributeID)["F_ATTRIBUTE_TYPE"]) == 13 ? (IDBAttributeType4) new DBMeasureAttributeType4Relation(this._clientSession, row) : (IDBAttributeType4) new CAttributeType4Relation(this._clientSession, row);
    if (throwNotFoundException)
      throw new AttributeTypeNotFoundException(string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_133"), (object) this._clientSession.GetRelationType((int) this.ParentID).Description, (object) this._clientSession.GetAttributeType(attributeID).Name));
    return (IDBAttributeType4) null;
  }

  public IDBAttributeType4 GetAttributeByID(int attributeID)
  {
    this._clientSession.Guard.ValidateCall();
    return this.GetAttributeByID(attributeID, false);
  }

  public IDBAttributeType4 GetAttributeByName(string attributeName, bool throwNotFoundException)
  {
    int attributeByTypeNameId = MetaDataHelper.GetAttributeByTypeNameID(attributeName);
    if (attributeByTypeNameId != -10000)
      return this.GetAttributeByID(attributeByTypeNameId, throwNotFoundException);
    if (throwNotFoundException)
      throw new AttributeTypeNotFoundException(string.Format(LocalizationHolder.rm.GetString("AttributeTypeNameNotFound"), (object) attributeName));
    return (IDBAttributeType4) null;
  }

  public IDBAttributeType4 GetAttributeByName(string attributeName)
  {
    return this.GetAttributeByName(attributeName, false);
  }

  public IDBAttributeType4 GetAttributeByGUID(Guid attributeGuid, bool throwNotFoundException)
  {
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(attributeGuid);
    if (attributeTypeId != -10000)
      return this.GetAttributeByID(attributeTypeId, throwNotFoundException);
    if (throwNotFoundException)
      throw new AttributeTypeNotFoundException(string.Format(LocalizationHolder.rm.GetString("AttributeTypeGuidNotFound"), (object) attributeGuid));
    return (IDBAttributeType4) null;
  }

  public IDBAttributeType4 GetAttributeByGUID(Guid attributeGuid)
  {
    return this.GetAttributeByGUID(attributeGuid, false);
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
    return this._clientSession.Session.GetRelationType((int) this.ParentID).Attributes.GetVisibleList();
  }

  public override DataTable Select(string orderBy, params object[] addInfo)
  {
    this._clientSession.Guard.ValidateCall();
    if (addInfo != null)
    {
      foreach (object obj in addInfo)
      {
        if (obj is string && obj.ToString() == "ALL_FIELDS")
          this._DBTableName = "IMS_ATTR4RELTYPE_VIEW";
      }
    }
    DataTable dataTable = base.Select(orderBy, addInfo);
    if (this._ftFilter != FieldTypes.ftUnknown && this._DBTableName == "IMS_ATTR4RELATION_TYPES")
    {
      for (int index = dataTable.Rows.Count - 1; index >= 0; --index)
      {
        if (this._ftFilter != this._clientSession.GetAttributeType(Convert.ToInt32(dataTable.Rows[index]["F_ATTRIBUTE_ID"])).AttributeType)
          dataTable.Rows.RemoveAt(index);
      }
      dataTable.AcceptChanges();
      this._ftFilter = FieldTypes.ftUnknown;
    }
    return dataTable;
  }

  public override object ParentID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return base.ParentID;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.ParentID == value)
        return;
      if (this._clientSession.ClientCache.GetTable("IMS_RELATION_TYPES").Rows.Find(value) == null)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_134"), value));
      base.ParentID = value;
    }
  }

  protected override string GetParentSQL()
  {
    return this._ftFilter != FieldTypes.ftUnknown ? $"(F_RELATION_TYPE = {this.ParentID} AND F_ATTRIBUTE_TYPE = {(int) this._ftFilter})" : $"(F_RELATION_TYPE = {this.ParentID})";
  }

  public BasicAttributeProperties[] GetEnabledAttributes(bool includeSystem)
  {
    AttributeSourceTypes attributeSource = !includeSystem ? AttributeSourceTypes.Auto : AttributeSourceTypes.Relation;
    string filterStr = !MetaDataHelper.GetRelationType(Convert.ToInt32(this.ParentID)).AnyAttributes ? "F_RELATION_TYPE = " + this.ParentID.ToString() : string.Empty;
    return AttributeCacheHelper.GetEnabledAttributes(this._clientSession.ClientCache.GetTable("IMS_ATTRIBUTES"), this._clientSession.ClientCache.GetTable(this._DBTableName), filterStr, attributeSource);
  }

  public bool IsEnabledAttribute(int attributeID)
  {
    return AttributeCacheHelper.IsEnabledRelationTypeAttribute(attributeID, Convert.ToInt32(this.ParentID));
  }
}
