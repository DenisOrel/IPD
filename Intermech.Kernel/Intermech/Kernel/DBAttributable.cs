// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBAttributable
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel;

public abstract class DBAttributable : DBSessionable
{
  protected IDBAttributeCollection _Attributes;
  private int _AttributesState;
  private AttributeValues[] _NewValues;
  protected Dictionary<int, object> ComputedValues;

  public DBAttributable(UserSession uSession)
    : base(uSession)
  {
  }

  public AttributeValues[] NewValues => this._NewValues;

  public int AttributesState => this._AttributesState;

  internal void SetAttributesState(int newValue)
  {
    if ((this._AttributesState & newValue) == newValue)
      return;
    newValue |= this._AttributesState;
    this.DoSetAttributesState(newValue);
    this._AttributesState = newValue;
  }

  internal void SetAttributesState(int newValue, AttributeValues[] newValues)
  {
    this._NewValues = newValues;
    this.SetAttributesState(newValue);
  }

  internal void AddComputedValue(int attrID, object value)
  {
    if (this.ComputedValues == null)
      this.ComputedValues = new Dictionary<int, object>();
    this.ComputedValues[attrID] = value;
  }

  public void CommitComputedValues()
  {
    if (this.ComputedValues == null)
      return;
    foreach (KeyValuePair<int, object> computedValue in this.ComputedValues)
    {
      if (this.Attributes.FindByID(computedValue.Key) is DBAttribute byId)
        byId.SetCalculatedValue(computedValue.Value, false);
    }
    this.ComputedValues = (Dictionary<int, object>) null;
  }

  public void SetAttributesState(int newValue, IDBAttributeCollection newAttributes)
  {
    if (newAttributes != null)
    {
      int count = newAttributes.Count;
      if (count > 0)
      {
        this._NewValues = new AttributeValues[count];
        for (int AttrIndex = 0; AttrIndex < count; ++AttrIndex)
          this._NewValues[AttrIndex] = new AttributeValues(newAttributes[AttrIndex].AttributeID)
          {
            Values = newAttributes[AttrIndex].Values
          };
      }
    }
    this.SetAttributesState(newValue);
  }

  public void ClearAttributesState(int newValue)
  {
    int newValue1 = ~newValue & this._AttributesState;
    this._NewValues = (AttributeValues[]) null;
    this.ComputedValues = (Dictionary<int, object>) null;
    if (this._AttributesState == newValue1)
      return;
    this.DoSetAttributesState(newValue1);
    this._AttributesState = newValue1;
  }

  protected virtual void DoSetAttributesState(int newValue)
  {
  }

  public virtual bool MustCheckValidatingRule => true;

  public virtual long HistoryObjectID => 0;

  public override bool GetDefaultAccess(ActionType at)
  {
    bool defaultAccess;
    switch (at)
    {
      case ActionType.GetAccess:
      case ActionType.SetAccess:
      case ActionType.TakeOwnership:
      case ActionType.ChangeBaseVersion:
      case ActionType.ChangeAccessLevel:
        defaultAccess = this.UserSession.IsAdmin;
        break;
      default:
        defaultAccess = true;
        break;
    }
    return defaultAccess;
  }

  internal IDBAttributeCollection InternalAttributesCollection => this._Attributes;

  internal void ViewsUpdaterPrepare()
  {
    if (!(this.Attributes is DBAttributeCollection attributes))
      return;
    attributes._UpdateViews = attributes._UpdateViews == null ? new UpdateViewsHelper(this.UserSession.DataManager) : throw new KernelException("UpdateViewsHelper already prepared!");
  }

  internal void ViewsUpdaterCommit()
  {
    if (!(this.InternalAttributesCollection is DBAttributeCollection attributesCollection) || attributesCollection._UpdateViews == null)
      return;
    attributesCollection._UpdateViews.ExecuteSQL();
    attributesCollection._UpdateViews = (UpdateViewsHelper) null;
  }

  internal void ViewsUpdaterRollback()
  {
    if (!(this.InternalAttributesCollection is DBAttributeCollection attributesCollection))
      return;
    attributesCollection._UpdateViews = (UpdateViewsHelper) null;
  }

  internal bool ViewsUpdaterInited
  {
    get
    {
      return this.InternalAttributesCollection is DBAttributeCollection attributesCollection && attributesCollection._UpdateViews != null;
    }
  }

  internal void ViewsUpdaterAddValue(
    string viewName,
    long objID,
    string keyFld,
    object value,
    string fldName)
  {
    if (!(this.InternalAttributesCollection is DBAttributeCollection attributesCollection) || attributesCollection._UpdateViews == null)
      throw new KernelException("UpdateViewsHelper not prepared!");
    attributesCollection._UpdateViews.AddData(viewName, objID, keyFld, value, fldName);
  }

  protected virtual void DoAfterSetAdditionalAttributeValue(IDBAttribute attribute)
  {
  }

  internal void AfterSetAdditionalAttributeValue(IDBAttribute attribute)
  {
    if (!AttributesTypeHelper.IsComplexAttributeType(attribute.AttributeType.AttributeType))
      this.UpdateWorkCopyAttribute(attribute);
    this.DoAfterSetAdditionalAttributeValue(attribute);
  }

  private void UpdateWorkCopyAttribute(IDBAttribute attribute)
  {
    if (!this.IsNotModifyRelation(attribute))
      return;
    IDBRelation relation = this.UserSession.GetRelation((this as DBRelation).GUID, -(this as DBRelation).ProjID, false);
    if (relation == null)
      return;
    IDBAttribute attributeById = relation.GetAttributeByID(attribute.AttributeID);
    if (attributeById != null)
      attributeById.Assign(attribute);
    else
      relation.Attributes.AddAttribute(attribute.AttributeID, false)?.Assign(attribute);
  }

  protected virtual void DoAfterSetComplexAttributeValue(IDBAttribute attribute)
  {
  }

  internal void AfterSetComplexAttributeValue(IDBAttribute attribute)
  {
    this.UpdateWorkCopyAttribute(attribute);
    this.DoAfterSetComplexAttributeValue(attribute);
  }

  protected virtual void DoBeforeSetAdditionalAttributeValue(
    IDBAttribute attribute,
    object newValue)
  {
  }

  internal void BeforeSetAdditionalAttributeValue(IDBAttribute attribute, object newValue)
  {
    this.DoBeforeSetAdditionalAttributeValue(attribute, newValue);
  }

  protected virtual void DoAfterDeleteAdditionalAttributeValue(
    IDBAttribute attribute,
    AttributeDataTableValue deletedValue)
  {
  }

  internal void AfterDeleteAdditionalAttributeValue(
    IDBAttribute attribute,
    AttributeDataTableValue deletedValue)
  {
    if (this.IsNotModifyRelation(attribute))
    {
      IDBRelation relation = this.UserSession.GetRelation((this as DBRelation).GUID, -(this as DBRelation).ProjID, false);
      if (relation != null && relation.GetAttributeByID(attribute.AttributeID) is DBAttribute attributeById)
        attributeById.Assign(attribute);
    }
    this.DoAfterDeleteAdditionalAttributeValue(attribute, deletedValue);
  }

  protected virtual void DoBeforeAddAttribute(int attributeID, object[] initValues)
  {
  }

  internal void BeforeAddAttribute(int attributeID, object[] initValues)
  {
    this.DoBeforeAddAttribute(attributeID, initValues);
  }

  protected virtual void DoAfterAddAttribute(IDBAttribute attribute)
  {
  }

  internal void AfterAddAttribute(IDBAttribute attribute) => this.DoAfterAddAttribute(attribute);

  protected virtual void DoBeforeDeleteAttribute(IDBAttribute attribute)
  {
  }

  internal void BeforeDeleteAttribute(IDBAttribute attribute)
  {
    this.DoBeforeDeleteAttribute(attribute);
  }

  private bool IsNotModifyRelation(IDBAttribute attribute)
  {
    if (!(this is DBRelation) || attribute.TemporaryAttribute)
      return false;
    DBRelation dbRelation = this as DBRelation;
    return dbRelation.ProjID > 0L && dbRelation.Applicability != null && !dbRelation.Applicability.IsContent;
  }

  protected virtual void DoBeforeDeleteAdditionalAttributeValue(IDBAttribute attribute)
  {
  }

  internal void BeforeDeleteAdditionalAttributeValue(IDBAttribute attribute)
  {
    this.DoBeforeDeleteAdditionalAttributeValue(attribute);
  }

  public virtual IDBAttributeCollection Attributes => this._Attributes;

  internal void RebuildComputedAttrs()
  {
    for (int AttrIndex = 0; AttrIndex < this.Attributes.Count; ++AttrIndex)
    {
      DBAttribute attribute = this.Attributes[AttrIndex] as DBAttribute;
      if (attribute.AttributeType.Computed == ComputeValueModes.IndexValue || attribute.AttributeType.Computed == ComputeValueModes.StoredValue)
        attribute.Compute(false);
    }
  }

  public abstract object[] GetValuesByName(string attributeName, bool throwNotFoundException);

  public abstract object[] GetValuesByID(int attributeID, bool throwNotFoundException);

  public abstract object[] GetValuesByGuid(Guid guid, bool throwNotFoundException);

  public abstract string[] GetDescriptionsByID(int attributeID, bool throwNotFoundException);

  public abstract string[] GetDescriptionsByGuid(Guid guid, bool throwNotFoundException);

  public virtual string[] GetDescriptionsByName(string attributeName, bool throwNotFoundException)
  {
    int attributeByTypeNameId = MetaDataHelper.GetAttributeByTypeNameID(attributeName);
    return attributeByTypeNameId != -10000 ? this.GetDescriptionsByID(attributeByTypeNameId, throwNotFoundException) : throw new KernelExceptionID(sc_12383.ssp_appserver_12384(117342903), (object) attributeName);
  }

  protected virtual void DoAfterDeleteAdditionalAttribute(IDBAttribute attribute)
  {
  }

  internal void AfterDeleteAdditionalAttribute(IDBAttribute attribute)
  {
    if (this.IsNotModifyRelation(attribute))
    {
      IDBRelation relation = this.UserSession.GetRelation((this as DBRelation).GUID, -(this as DBRelation).ProjID, false);
      if (relation != null && relation.GetAttributeByID(attribute.AttributeID) is DBAttribute attributeById)
        attributeById.Purge(false);
    }
    this.DoAfterDeleteAdditionalAttribute(attribute);
  }

  public virtual AttributeValues[] GetCalculatedValues(
    AttributeValues[] valuesList,
    GetAttributeValuesModes modes)
  {
    this.Attributes.ClearDeltaValues();
    for (int AttrIndex = 0; AttrIndex < this.Attributes.Count; ++AttrIndex)
      (this.Attributes[AttrIndex] as DBAttribute)._TemporaryAttribute = true;
    AttributeValues[] calculatedValues = this.SetAttributesValues(valuesList, false, true, true, modes) ?? new AttributeValues[0];
    this.Deleted = true;
    return calculatedValues;
  }

  public abstract AttributeValues[] SetAttributesValues(
    AttributeValues[] valuesList,
    bool deleteNotExistingAttributes,
    bool dontDeleteBlobs,
    bool returnDelta,
    GetAttributeValuesModes modes);

  public abstract AttributeValues[] SetAttributesValues(
    AttributeValues[] valuesList,
    bool deleteNotExistingAttributes,
    bool dontDeleteBlobs,
    bool returnDelta,
    GetAttributeValuesModes modes,
    Dictionary<string, Exception> exceptionsList);

  public virtual IDBAttribute TryToAddOrDelAttribute(int attrID, object newValue)
  {
    if (this.UserSession == null || this.Attributes == null || attrID == 0 || attrID == -10000)
      return (IDBAttribute) null;
    IMSAttributeType attributeType1 = MetaDataHelper.GetAttributeType(attrID);
    if (attributeType1 == null)
      return (IDBAttribute) null;
    IDBAttribute byId = this.Attributes.FindByID(attrID);
    object[] initValues = newValue as object[];
    bool flag = false;
    if (attributeType1.MultiValueMode == MultiValueModes.MultiValues || attributeType1.MultiValueMode == MultiValueModes.MultiValuesFromList)
      flag = initValues == null || initValues.Length == 0;
    if (((newValue == null ? 1 : (newValue == DBNull.Value ? 1 : 0)) | (flag ? 1 : 0)) != 0)
    {
      if (byId == null)
        return (IDBAttribute) null;
      if (!(byId.AttributeType is IDBAttributeType4 attributeType2))
        return (IDBAttribute) null;
      if (attributeType2.Required == RequiredModes.Manual)
      {
        byId.Delete(0L);
        return (IDBAttribute) null;
      }
    }
    if (byId != null || newValue == null)
    {
      if (byId != null && newValue != null)
      {
        if (attributeType1.MultiValueMode == MultiValueModes.MultiValues || attributeType1.MultiValueMode == MultiValueModes.MultiValuesFromList)
        {
          if (!(newValue is object[] objArray) || objArray.Length == 0)
            byId.ClearValues();
          else
            byId.Values = objArray;
        }
        else
          byId.Value = newValue;
      }
      return byId;
    }
    IDBAttribute addOrDelAttribute = initValues != null ? this.Attributes.AddAttribute(attrID, false, initValues) : this.Attributes.AddAttribute(attrID, false);
    if (addOrDelAttribute != null && initValues == null)
      addOrDelAttribute.Value = newValue;
    return addOrDelAttribute;
  }

  protected void UpdateValuesListByArgs(
    ref AttributeValues[] valuesList,
    AttributesValuesEventArgs args)
  {
    if (args.ModifiedValuesList == null)
      return;
    List<AttributeValues> attributeValuesList = (List<AttributeValues>) null;
    for (int index1 = 0; index1 < args.ModifiedValuesList.Count; ++index1)
    {
      bool flag = true;
      for (int index2 = 0; index2 < valuesList.Length; ++index2)
      {
        if (args.ModifiedValuesList[index1].AttributeID == valuesList[index2].AttributeID)
        {
          valuesList[index2].Values = args.ModifiedValuesList[index1].Values;
          flag = false;
          break;
        }
      }
      if (flag)
      {
        if (attributeValuesList == null)
          attributeValuesList = new List<AttributeValues>();
        attributeValuesList.Add(args.ModifiedValuesList[index1]);
      }
    }
    if (attributeValuesList == null)
      return;
    attributeValuesList.AddRange((IEnumerable<AttributeValues>) valuesList);
    valuesList = attributeValuesList.ToArray();
  }

  public abstract long CreatorID { get; }

  public int[] GetExistsAttributes() => this.Attributes.GetExistsAttributes();

  public abstract IDBAttributeType GetAttributeType(int attributeID);
}
