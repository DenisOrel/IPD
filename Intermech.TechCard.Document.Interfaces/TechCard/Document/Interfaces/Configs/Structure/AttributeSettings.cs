// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.AttributeSettings
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure;

[TypeConverter(typeof (AttributeSettingsConverter))]
[Serializable]
public class AttributeSettings : IEquatable<AttributeSettings>
{
  private MetaDataCacheItem _itemType;
  private IMSAttributeType _attribute;

  private string GetItemText()
  {
    MetaDataCacheItem itemType = this.GetItemType();
    switch (this.ItemKind)
    {
      case AttributableElements.Object:
        return (itemType is IMSObjectType imsObjectType ? imsObjectType.ObjectName : (string) null) ?? string.Empty;
      case AttributableElements.Relation:
        return (itemType is IMSRelationType imsRelationType ? imsRelationType.Description : (string) null) ?? string.Empty;
      default:
        return string.Empty;
    }
  }

  public AttributeSettings(AttributableElements itemKind, string itemGuid, string attributeGuid)
  {
    this.ItemKind = itemKind;
    this.ItemGuid = GuidHelper.IsGuid(itemGuid) ? new Guid(itemGuid) : Guid.Empty;
    this.AttributeGuid = GuidHelper.IsGuid(attributeGuid) ? new Guid(attributeGuid) : Guid.Empty;
  }

  public AttributeSettings(AttributableElements itemKind, Guid itemGuid, Guid attributeGuid)
  {
    this.ItemKind = itemKind;
    this.ItemGuid = itemGuid;
    this.AttributeGuid = attributeGuid;
  }

  public AttributeSettings([NotNull] AttributeSettings kind)
    : this(kind.ItemKind, kind.ItemGuid, kind.AttributeGuid)
  {
    this._itemType = kind._itemType;
    this._attribute = kind._attribute;
  }

  public AttributableElements ItemKind { get; }

  public Guid ItemGuid { get; }

  public Guid AttributeGuid { get; }

  public MetaDataCacheItem GetItemType()
  {
    if (this._itemType != null)
      return this._itemType;
    if (this.ItemGuid == Guid.Empty)
      return (MetaDataCacheItem) null;
    switch (this.ItemKind)
    {
      case AttributableElements.Object:
        this._itemType = (MetaDataCacheItem) MetaDataHelper.GetObjectType(this.ItemGuid);
        break;
      case AttributableElements.Relation:
        this._itemType = (MetaDataCacheItem) MetaDataHelper.GetRelationType(this.ItemGuid);
        break;
    }
    return this._itemType;
  }

  public int GetItemTypeId()
  {
    MetaDataCacheItem itemType = this.GetItemType();
    if (itemType == null)
      return -1;
    switch (this.ItemKind)
    {
      case AttributableElements.Object:
        return ((IMSObjectType) itemType).ObjectTypeID;
      case AttributableElements.Relation:
        return ((IMSRelationType) itemType).RelationTypeID;
      default:
        return -1;
    }
  }

  public IMSAttributeType GetAttribute()
  {
    if (this._attribute != null || !(this.AttributeGuid != Guid.Empty))
      return this._attribute;
    this._attribute = MetaDataHelper.GetAttributeType(this.AttributeGuid);
    return this._attribute;
  }

  public int GetAttributeId()
  {
    IMSAttributeType attribute = this.GetAttribute();
    return attribute == null ? 0 : attribute.AttributeID;
  }

  public string GetText()
  {
    return this.ItemKind == AttributableElements.None ? string.Empty : $"<{this.GetItemText()}>.{this.GetAttribute()?.Name}";
  }

  public override string ToString() => this.GetText();

  public override int GetHashCode()
  {
    Guid guid = this.ItemGuid;
    int hashCode1 = guid.GetHashCode();
    guid = this.AttributeGuid;
    int hashCode2 = guid.GetHashCode();
    return hashCode1 ^ hashCode2;
  }

  public bool Equals(AttributeSettings other)
  {
    if (other == null)
      return false;
    Guid guid = this.ItemGuid;
    if (!guid.Equals(other.ItemGuid))
      return false;
    guid = this.AttributeGuid;
    return guid.Equals(other.AttributeGuid);
  }

  public bool IsComment => false;

  public string Code => string.Empty;
}
