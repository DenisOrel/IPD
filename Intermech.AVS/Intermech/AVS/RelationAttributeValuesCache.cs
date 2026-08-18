// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.RelationAttributeValuesCache
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.AVS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.AVS;

[Serializable]
public class RelationAttributeValuesCache : AttributeValuesCache
{
  private AttributeValuesCache objectAttributesCache;
  public ProductInfo projInfo;

  public AttributeValuesCache ObjectAttributesCache
  {
    get => this.objectAttributesCache;
    set => this.objectAttributesCache = value;
  }

  public override long ObjectId
  {
    get => this.ObjectAttributesCache != null ? this.ObjectAttributesCache.ObjectId : -1L;
  }

  public override string ObjectCaption
  {
    get => this.ObjectAttributesCache != null ? this.ObjectAttributesCache.ObjectCaption : "";
  }

  public override Guid ObjectGuid
  {
    get => this.ObjectAttributesCache != null ? this.ObjectAttributesCache.ObjectGuid : Guid.Empty;
  }

  public override int ObjectType
  {
    get => this.ObjectAttributesCache != null ? this.ObjectAttributesCache.ObjectType : -1;
  }

  public virtual long RelationId
  {
    get
    {
      if (this.idIndex == -1)
        this.idIndex = this.GetValueIndex(-20);
      return this.idIndex != -1 ? AvsIDCache.ConvertDbValueToInt64(this.GetValueByIndex(this.idIndex)) : -1L;
    }
  }

  public virtual Guid RelationGuid
  {
    get
    {
      object obj = this.GetValue(-26, false);
      switch (obj)
      {
        case null:
        case DBNull _:
          return Guid.Empty;
        case Guid relationGuid:
          return relationGuid;
        default:
          return new Guid(obj.ToString());
      }
    }
  }

  public virtual int RelationType
  {
    get
    {
      object obj = this.GetValue(-23, false);
      return obj != null ? Convert.ToInt32(obj.ToString()) : -1;
    }
  }

  public virtual Guid ProjectGuid => this.projInfo != null ? this.projInfo.Guid : Guid.Empty;

  public virtual long ProjectId => this.projInfo != null ? this.projInfo.Id : -1L;

  public string ProductDesignation => this.projInfo != null ? this.projInfo.Designation : "";

  public Dictionary<int, bool> PersistentAttrs { get; set; } = new Dictionary<int, bool>();

  public void SetRelationID(long relID, Guid relGuid, int relType, ProductInfo projectInfo)
  {
    this.SetValue(-20, (object) relID, false);
    this.SetValue(-26, (object) relGuid.ToString(), false);
    this.SetValue(-23, (object) relType, false);
    this.projInfo = projectInfo;
    if (this.projInfo == null)
      return;
    this.SetValue(-21, (object) this.projInfo.Id, false);
  }

  /// <summary>Индекс сортировки</summary>
  [Browsable(false)]
  public long SortIndex
  {
    [DebuggerStepThrough] get => this.GetValueInt64(AvsIDCache.Attr_SortIndex, false, 0L);
    set => this.SetValue(AvsIDCache.Attr_SortIndex, (object) value, false);
  }

  /// <summary>Свободный индекс сортировки</summary>
  [Browsable(false)]
  public bool IsFreeSortIndex
  {
    get
    {
      long sortIndex = this.SortIndex;
      return sortIndex == 0L || sortIndex == long.MinValue;
    }
  }

  protected RelationAttributeValuesCache()
  {
  }

  public RelationAttributeValuesCache(
    Dictionary<int, int> attributeDictionary,
    List<AvsRowAttributeInfo> attrInfo,
    ProductInfo projInfo)
    : base(attributeDictionary, attrInfo)
  {
    this.projInfo = projInfo;
  }

  public override AttributeValueMap Clone()
  {
    RelationAttributeValuesCache attributeValuesCache = (RelationAttributeValuesCache) base.Clone();
    if (this.ObjectAttributesCache != null)
      attributeValuesCache.ObjectAttributesCache = this.ObjectAttributesCache;
    attributeValuesCache.projInfo = this.projInfo;
    return (AttributeValueMap) attributeValuesCache;
  }

  public override string ToString()
  {
    string str = this.GetValueString(AvsIDCache.Attr_PosDesignation, false);
    string valueString = this.GetValueString(AvsIDCache.Attr_FGPosDesignation, false);
    if (!string.IsNullOrEmpty(valueString))
      str = $"Поз.Обоз: {valueString}-{str}, ";
    else if (!string.IsNullOrEmpty(valueString))
      str = $"Поз.Обоз: {str}, ";
    return $"{$"[{this.RelationId}] {this.ObjectCaption}. "}Поз: {this.GetValueString(AvsIDCache.Attr_Position, false)}, К-во: {this.GetValueString(AvsIDCache.Attr_Count, false)}, {str}Исполнение: {this.projInfo?.Designation}";
  }
}
