// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AttributeValueMap
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.AVS;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

[Serializable]
public class AttributeValueMap : ICloneable
{
  private Dictionary<int, int> attributeDictionary;
  private List<AvsRowAttributeInfo> attrsInfo;
  protected int idIndex = -1;

  public Dictionary<int, int> AttributeDictionary => this.attributeDictionary;

  public List<AvsRowAttributeInfo> AttrsInfo => this.attrsInfo;

  public AttributeValueMap()
    : this(new Dictionary<int, int>(), new List<AvsRowAttributeInfo>())
  {
  }

  public AttributeValueMap(
    Dictionary<int, int> attributeDictionary,
    List<AvsRowAttributeInfo> attrInfo)
  {
    this.AssignMap(attributeDictionary, attrInfo);
  }

  public int GetValueIndex(int attributeId)
  {
    if (this.AttributeDictionary == null)
      return -1;
    int num = -1;
    return this.AttributeDictionary.TryGetValue(attributeId, out num) ? num : -1;
  }

  internal int GetUpdatedValueIndex(int attributeID, int valueIndex)
  {
    int updatedValueIndex = valueIndex;
    if (updatedValueIndex == -1 || updatedValueIndex >= this.AttrsInfo.Count || this.AttrsInfo[valueIndex].AttributeId != attributeID)
      updatedValueIndex = this.GetValueIndex(attributeID);
    return updatedValueIndex;
  }

  internal AvsRowAttributeInfo GetAttributeInfo(int attributeID)
  {
    int valueIndex = this.GetValueIndex(attributeID);
    if (valueIndex == -1)
      return (AvsRowAttributeInfo) null;
    this.AttrsInfo[valueIndex].IndexInValueList = valueIndex;
    return this.AttrsInfo[valueIndex];
  }

  internal AvsRowAttributeInfo CreateAttributeInfoWithValueIndex(
    FieldSource attributeSource,
    int attributeID)
  {
    return new AvsRowAttributeInfo(attributeSource, Guid.Empty, attributeID, (string) null)
    {
      IndexInValueList = this.GetValueIndex(attributeID)
    };
  }

  public void AssignMap(
    Dictionary<int, int> attributeDictionary,
    List<AvsRowAttributeInfo> attrInfo)
  {
    this.attributeDictionary = attributeDictionary;
    this.attrsInfo = attrInfo;
    this.idIndex = -1;
  }

  public virtual AttributeValueMap Clone()
  {
    AttributeValueMap instance = (AttributeValueMap) Activator.CreateInstance(this.GetType(), true);
    instance.attributeDictionary = this.attributeDictionary;
    instance.attrsInfo = this.attrsInfo;
    return instance;
  }

  object ICloneable.Clone() => (object) this.Clone();
}
