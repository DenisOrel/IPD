
// Type: Intermech.Search.Data.Adapters.AttributeCollectionRelationObjectNodeIDAdapter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.DBObjects;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;


namespace Intermech.Search.Data.Adapters;

public sealed class AttributeCollectionRelationObjectNodeIDAdapter : AttributeCollectionBase
{
  /// <summary>
  /// Initializes a new instance of the <see cref="T:Intermech.Search.Data.Adapters.AttributeCollectionRelationObjectNodeIDAdapter" /> class.
  /// </summary>
  /// <param name="nodeID">Идентификатор узла</param>
  public AttributeCollectionRelationObjectNodeIDAdapter(NodeID nodeID)
  {
    this.NodeID = nodeID != null ? nodeID : throw new ArgumentNullException(nameof (nodeID));
  }

  /// <summary>Получить идентификатор узла</summary>
  /// <value>Идентификатор узла</value>
  public NodeID NodeID { get; private set; }

  public override void Add(_Attribute attribute) => throw new NotImplementedException();

  public override void AddRange(IEnumerable<_Attribute> attributes)
  {
    throw new NotImplementedException();
  }

  public override object GetAttributeValue(int attributeTypeID)
  {
    if (AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID))
      throw new ArgumentException(nameof (attributeTypeID));
    if (!this.HasAttribute(attributeTypeID))
      throw new Intermech.Search.AttributeNotFoundException(attributeTypeID);
    if (attributeTypeID == -16)
      return (object) this.NodeID.BaseVersion;
    if (attributeTypeID == -50)
      return (object) this.NodeID.Caption;
    if (attributeTypeID == -6)
      return (object) this.NodeID.CheckedOutBy;
    if (attributeTypeID == -3)
      return (object) this.NodeID.ID;
    if (attributeTypeID == -4)
      return (object) this.NodeID.LCStepID;
    if (attributeTypeID == -15)
      return (object) this.NodeID.ModificationID;
    if (attributeTypeID == -2)
      return (object) this.NodeID.ObjectID;
    if (attributeTypeID == -7)
      return (object) this.NodeID.ObjectTypeID;
    if (attributeTypeID == -8)
      return (object) this.NodeID.Owner;
    if (attributeTypeID == -20)
      return (object) this.NodeID.PrjLinkID;
    if (attributeTypeID == -21)
      return (object) this.NodeID.ProjID;
    if (attributeTypeID == -23)
      return (object) this.NodeID.RelationTypeID;
    if (attributeTypeID == -26)
      return (object) this.NodeID.RelGuid;
    if (attributeTypeID == -17)
      return (object) this.NodeID.SiteID;
    if (attributeTypeID == -7)
      return (object) this.NodeID.TypeID;
    return attributeTypeID == -5 ? (object) this.NodeID.Version : (object) null;
  }

  public override bool HasAttribute(int attributeTypeID)
  {
    return !AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID) ? new List<int>()
    {
      -16,
      -50,
      -6,
      Constants.ExplicitPartVersionIDAttributeTypeID,
      -3,
      -4,
      -15,
      -2,
      -7,
      -8,
      -20,
      -21,
      -23,
      -26,
      -17,
      -7,
      -5
    }.Contains(attributeTypeID) : throw new ArgumentException(nameof (attributeTypeID));
  }

  public override void SetAttributeValue(int attributeTypeID, object value)
  {
    throw new NotImplementedException();
  }

  public override _Attribute GetAttribute(int attributeTypeID)
  {
    throw new NotImplementedException();
  }

  public override IEnumerator<_Attribute> GetEnumerator() => throw new NotImplementedException();
}
