
// Type: Intermech.Search.Diff.AttributeDiffCollection
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Intermech.Search.Diff;

[TypeConverter(typeof (AttributeDiffCollectionConverter))]
public sealed class AttributeDiffCollection : DiffCollectionBase<AttributeDiff>
{
  private Dictionary<int, AttributeDiff> _diffDictionary = new Dictionary<int, AttributeDiff>();

  public AttributeDiffCollection(
    IAttributeHolder attributeHolder,
    IAttributeHolder otherAttributeHolder)
  {
    if (attributeHolder == null)
      throw new ArgumentNullException(nameof (attributeHolder));
    if (otherAttributeHolder == null)
      throw new ArgumentNullException(nameof (otherAttributeHolder));
    foreach (_Attribute attribute1 in (IEnumerable<_Attribute>) attributeHolder.Attributes)
    {
      _Attribute attribute2 = (_Attribute) null;
      if (otherAttributeHolder.Attributes.HasAttribute(attribute1.TypeID))
        attribute2 = otherAttributeHolder.Attributes.GetAttribute(attribute1.TypeID);
      DiffOperand firstOperand = new DiffOperand(attribute1.Value);
      DiffOperand secondOperand = attribute2 != null ? new DiffOperand(attribute2.Value) : (DiffOperand) null;
      this._diffDictionary.Add(attribute1.TypeID, new AttributeDiff(attribute1.TypeID, firstOperand, secondOperand));
    }
    foreach (_Attribute attribute in (IEnumerable<_Attribute>) otherAttributeHolder.Attributes)
    {
      if (!this._diffDictionary.ContainsKey(attribute.TypeID))
      {
        DiffOperand secondOperand = new DiffOperand(attribute.Value);
        this._diffDictionary.Add(attribute.TypeID, new AttributeDiff(attribute.TypeID, (DiffOperand) null, secondOperand));
      }
    }
  }

  public AttributeDiff this[int attributeTypeID] => this._diffDictionary[attributeTypeID];

  public override IEnumerator<AttributeDiff> GetEnumerator()
  {
    return (IEnumerator<AttributeDiff>) this._diffDictionary.Values.GetEnumerator();
  }
}
