// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.BasicAttributeTypeCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

internal abstract class BasicAttributeTypeCollection : DBCollection
{
  private Dictionary<int, IDBAttributeType4> _Attributes;
  protected string _DBTypeField = string.Empty;

  public BasicAttributeTypeCollection(UserSession uSession, bool filterRecs)
    : base(uSession, filterRecs)
  {
  }

  protected Dictionary<int, IDBAttributeType4> Attributes
  {
    get
    {
      if (this._Attributes == null)
        this._Attributes = new Dictionary<int, IDBAttributeType4>();
      return this._Attributes;
    }
  }

  protected abstract IDBAttributeType GetAttributeType4(int attrID, bool failIfNotFound);

  public IDBAttributeType GetAttributeType(object objID, bool failIfNotFound)
  {
    return this.GetAttributeType(objID, (DataTable) null, failIfNotFound);
  }

  internal IDBAttributeType GetAttributeType(
    object objID,
    DataTable attrTable,
    bool failIfNotFound)
  {
    IDBAttributeType attributeType = (IDBAttributeType) null;
    DataTable attributeTable = attrTable ?? this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES");
    int attributeId = AttributeCacheHelper.GetAttributeID(objID, attributeTable, failIfNotFound);
    if (attributeId != 0)
      attributeType = this.GetAttributeType4(attributeId, failIfNotFound);
    return attributeType;
  }

  public IDBAttributeType[] GetAttributeTypeList(object[] idList, bool failIfNotFound)
  {
    if (idList == null)
      return new IDBAttributeType[0];
    IDBAttributeType[] attributeTypeList1 = new IDBAttributeType[idList.Length];
    int length = 0;
    DataTable table = this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES");
    foreach (object id in idList)
    {
      attributeTypeList1[length] = this.GetAttributeType(id, table, failIfNotFound);
      if (attributeTypeList1[length] != null)
        ++length;
    }
    if (length >= idList.Length)
      return attributeTypeList1;
    IDBAttributeType[] attributeTypeList2 = new IDBAttributeType[length];
    for (int index = 0; index < length; ++index)
      attributeTypeList2[index] = attributeTypeList1[index];
    return attributeTypeList2;
  }

  public BasicAttributeProperties[] GetEnabledAttributes(bool includeSystem)
  {
    AttributeSourceTypes attributeSource = !includeSystem ? AttributeSourceTypes.Auto : this.CollectionSourceType;
    string filterStr = !this.AnyAttributes ? $"{this._DBTypeField} = {this.ParentID}" : string.Empty;
    return AttributeCacheHelper.GetEnabledAttributes(this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES"), this.UserSession.DBCache.GetTable(this._DBTableName), filterStr, attributeSource);
  }

  public bool IsEnabledAttribute(int attributeID)
  {
    if (attributeID < 0)
      return ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) attributeID) == this.CollectionSourceType;
    return this.AnyAttributes || this.GetAttributeByID(attributeID, false) != null;
  }

  public virtual IDBAttributeType4 GetAttributeByID(int attributeID, bool throwNotFoundException)
  {
    throw new OperationNotApplicableException();
  }

  public abstract bool AnyAttributes { get; }

  public abstract AttributeSourceTypes CollectionSourceType { get; }
}
