// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.RelationsComparer
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Pdm.Server;

public class RelationsComparer : IRelationsComparer
{
  protected Guid _comparerGuid = Guid.NewGuid();
  protected RelationsAttributeComparerCaps _capabilities = RelationsAttributeComparerCaps.ByAnyAttributes;
  protected List<FieldTypes> _supportedFieldTypes = new List<FieldTypes>();
  protected List<int> _supportedAttributes = new List<int>();

  public RelationsComparer()
  {
    this._supportedFieldTypes.Add(FieldTypes.ftAutoInc);
    this._supportedFieldTypes.Add(FieldTypes.ftBoolean);
    this._supportedFieldTypes.Add(FieldTypes.ftDateTime);
    this._supportedFieldTypes.Add(FieldTypes.ftDouble);
    this._supportedFieldTypes.Add(FieldTypes.ftGuid);
    this._supportedFieldTypes.Add(FieldTypes.ftInteger);
    this._supportedFieldTypes.Add(FieldTypes.ftMeasured);
    this._supportedFieldTypes.Add(FieldTypes.ftObjectLink);
    this._supportedFieldTypes.Add(FieldTypes.ftPassword);
    this._supportedFieldTypes.Add(FieldTypes.ftString);
  }

  protected virtual object GetRelationAttrValue(IDBRelation relation, int attrID)
  {
    return relation.GetAttributeByID(attrID)?.Value;
  }

  protected virtual object GetRelationAttrValue(DataRow row, int attrID, bool useSubstAttrs)
  {
    if (row == null)
      return (object) null;
    int columnIndex = DBRecordSet.AttributeColumnID(row.Table, (object) attrID);
    if (columnIndex == -1)
      columnIndex = DBRecordSet.AttributeColumnID(row.Table, (object) MetaDataHelper.GetAttributeTypeGuid(attrID));
    if (columnIndex == -1 & useSubstAttrs && SubstituteObjects.AttrsIndex.ContainsKey(attrID))
      columnIndex = SubstituteObjects.AttrsIndex[attrID];
    return columnIndex == -1 ? (object) false : row[columnIndex];
  }

  protected virtual bool InternalEqualValues(
    object attr1Value,
    object attr2Value,
    FieldTypes attrFieldType)
  {
    if (this._supportedFieldTypes.IndexOf(attrFieldType) < 0 || attr1Value == null || attr2Value == null)
      return false;
    if (attr1Value == DBNull.Value && attr2Value == DBNull.Value)
      return true;
    switch (attrFieldType)
    {
      case FieldTypes.ftString:
        return attr1Value.ToString() == attr2Value.ToString();
      case FieldTypes.ftInteger:
      case FieldTypes.ftObjectLink:
      case FieldTypes.ftAutoInc:
        long result1;
        long result2;
        return long.TryParse(attr1Value.ToString(), out result1) && long.TryParse(attr2Value.ToString(), out result2) && result1 == result2;
      case FieldTypes.ftDouble:
        double result3;
        double result4;
        return double.TryParse(attr1Value.ToString(), out result3) && double.TryParse(attr2Value.ToString(), out result4) && result3 == result4;
      case FieldTypes.ftDateTime:
        DateTime result5;
        DateTime result6;
        return DateTime.TryParse(attr1Value.ToString(), out result5) && DateTime.TryParse(attr2Value.ToString(), out result6) && result5 == result6;
      case FieldTypes.ftBoolean:
        bool result7;
        bool result8;
        return bool.TryParse(attr1Value.ToString(), out result7) && bool.TryParse(attr2Value.ToString(), out result8) && result7 == result8;
      case FieldTypes.ftMeasured:
        return this.InternalEqualMeasuredValues(attr1Value, attr2Value);
      case FieldTypes.ftGuid:
        return this.InternalEqualGuidValues(attr1Value, attr2Value);
      default:
        return attr1Value != DBNull.Value && attr2Value != DBNull.Value && attr1Value.ToString() == attr2Value.ToString();
    }
  }

  protected virtual bool InternalEqualMeasuredValues(object attr1Value, object attr2Value)
  {
    string str1 = attr1Value.ToString();
    string str2 = attr2Value.ToString();
    if (string.IsNullOrEmpty(str1) && string.IsNullOrEmpty(str2))
      return true;
    return (!string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2)) && (string.IsNullOrEmpty(str1) || !string.IsNullOrEmpty(str2)) && MeasureHelper.Compare(MeasureHelper.ConvertToMeasuredValue(attr1Value.ToString()), MeasureHelper.ConvertToMeasuredValue(attr2Value.ToString())) == CompareResult.Equal;
  }

  protected virtual bool InternalEqualGuidValues(object attr1Value, object attr2Value)
  {
    if (attr1Value == DBNull.Value || attr2Value == DBNull.Value)
      return false;
    string str1 = attr1Value.ToString();
    string str2 = attr2Value.ToString();
    return GuidHelper.IsGuid(str1) && GuidHelper.IsGuid(str2) && new Guid(str1).Equals(new Guid(str2));
  }

  protected virtual bool CheckAttributes(List<int> attrIDs)
  {
    if (this._supportedAttributes.Count == 0)
      return attrIDs.Count > 0;
    for (int index = 0; index < attrIDs.Count; ++index)
    {
      if (this._supportedAttributes.IndexOf(attrIDs[index]) < 0)
        return false;
    }
    return attrIDs.Count > 0;
  }

  public virtual Guid ComparerGuid
  {
    [DebuggerStepThrough] get => this._comparerGuid;
  }

  public virtual RelationsAttributeComparerCaps Capabilities
  {
    [DebuggerStepThrough] get => this._capabilities;
  }

  public virtual bool CanCompareByAttribute(int attrID)
  {
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrID);
    if (attributeType == null || this._supportedFieldTypes.IndexOf(attributeType.RealFieldType) < 0)
      return false;
    return this._supportedAttributes.Count == 0 || this._supportedAttributes.IndexOf(attrID) >= 0;
  }

  public virtual List<int> SupportedAttributes
  {
    [DebuggerStepThrough] get => this._supportedAttributes;
  }

  public virtual bool EqualsTo(IUserSession session, int attrID, long prjLinkID1, long prjLinkID2)
  {
    return this.EqualsTo(session, new List<int>() { attrID }, prjLinkID1, prjLinkID2);
  }

  public virtual bool EqualsTo(
    IUserSession session,
    List<int> attrIDs,
    long prjLinkID1,
    long prjLinkID2)
  {
    if (session == null || attrIDs == null || attrIDs.Count == 0 || prjLinkID1 == 0L || prjLinkID2 == 0L)
      return false;
    if (prjLinkID1 == prjLinkID2)
      return true;
    if (!this.CheckAttributes(attrIDs))
      return false;
    IDBRelation relation1 = session.GetRelation(prjLinkID1, false);
    IDBRelation relation2 = session.GetRelation(prjLinkID2, false);
    if (relation1 == null || relation2 == null)
      return false;
    for (int index = 0; index < attrIDs.Count; ++index)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrIDs[index]);
      if (attributeType == null || !this.InternalEqualValues(this.GetRelationAttrValue(relation1, attributeType.AttributeID), this.GetRelationAttrValue(relation2, attributeType.AttributeID), attributeType.RealFieldType))
        return false;
    }
    return true;
  }

  public virtual bool EqualsTo(
    IUserSession session,
    int attrID,
    long prjLinkID1,
    long prjLinkID2,
    DataRow row1,
    DataRow row2,
    bool useSubstAttrs)
  {
    return this.EqualsTo(session, new List<int>() { attrID }, prjLinkID1, prjLinkID2, row1, row2, useSubstAttrs);
  }

  public virtual bool EqualsTo(
    IUserSession session,
    List<int> attrIDs,
    long prjLinkID1,
    long prjLinkID2,
    DataRow row1,
    DataRow row2,
    bool useSubstAttrs)
  {
    if (session == null || attrIDs == null || attrIDs.Count == 0 || prjLinkID1 == 0L || prjLinkID2 == 0L || row1 == null || row2 == null)
      return false;
    if (prjLinkID1 == prjLinkID2)
      return true;
    if (!this.CheckAttributes(attrIDs))
      return false;
    for (int index = 0; index < attrIDs.Count; ++index)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrIDs[index]);
      if (attributeType == null || !this.InternalEqualValues(this.GetRelationAttrValue(row1, attributeType.AttributeID, useSubstAttrs), this.GetRelationAttrValue(row2, attributeType.AttributeID, useSubstAttrs), attributeType.RealFieldType))
        return false;
    }
    return true;
  }
}
