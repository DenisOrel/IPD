// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.ColumnConfiguration
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

[Serializable]
public class ColumnConfiguration : 
  ISerializable,
  IXmlSerializable,
  IEquatable<ColumnConfiguration>,
  ICloneable
{
  public SettingItemType ItemType { get; set; }

  [CustomDisplayName("Tools.Client_292")]
  [CustomDescription("Tools.Client_286")]
  [CustomCategory("Tools.Client_298")]
  public int TypeId { get; set; } = -1;

  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Tools.Client_293")]
  [CustomDescription("Tools.Client_287")]
  [CustomCategory("Tools.Client_299")]
  [DefaultValue(SettingItemValueKind.Variable)]
  public SettingItemValueKind ValueKind { get; set; }

  [CustomDisplayName("Tools.Client_294")]
  [CustomDescription("Tools.Client_288")]
  [CustomCategory("Tools.Client_299")]
  [DefaultValue(SettingItemDataType.TypeName)]
  public SettingItemDataType DataType { get; set; }

  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Tools.Client_295")]
  [CustomDescription("Tools.Client_289")]
  [CustomCategory("Tools.Client_298")]
  [DefaultValue(SettingItemAttributeSourceType.Object)]
  public SettingItemAttributeSourceType SettingItemAttributeBelongs { get; set; }

  [CustomDisplayName("Tools.Client_296")]
  [CustomDescription("Tools.Client_290")]
  [CustomCategory("Tools.Client_298")]
  public string AttributeValue { get; set; }

  [CustomDisplayName("Tools.Client_297")]
  [CustomDescription("Tools.Client_291")]
  [CustomCategory("Tools.Client_298")]
  [DefaultValue(SettingItemAttributeUpdateMode.Skip)]
  public SettingItemAttributeUpdateMode SettingItemAttributeUpdateMode { get; set; }

  [TypeConverter(typeof (YesNoBooleanConverter))]
  [CustomDisplayName("Tools.Client_300")]
  [CustomDescription("Tools.Client_301")]
  [CustomCategory("Tools.Client_298")]
  [DefaultValue(false)]
  public bool SyncImbase { get; set; }

  [Browsable(false)]
  public int Index { get; set; }

  public ColumnConfiguration()
  {
  }

  protected ColumnConfiguration(SerializationInfo info, StreamingContext context)
  {
    this.ItemType = (SettingItemType) info.GetInt32(nameof (ItemType));
    this.TypeId = -1;
    string Guid = info.GetString(nameof (TypeId));
    if (string.IsNullOrEmpty(Guid))
    {
      switch (this.ItemType)
      {
        case SettingItemType.AttributeType:
          this.TypeId = MetaDataHelper.GetAttributeTypeID(Guid);
          break;
        case SettingItemType.ObjectType:
        case SettingItemType.EntrancyObjectType:
          this.TypeId = MetaDataHelper.GetObjectTypeID(Guid);
          break;
        case SettingItemType.RelationType:
          this.TypeId = MetaDataHelper.GetRelationTypeID(Guid);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
    this.ValueKind = (SettingItemValueKind) info.GetInt32(nameof (ValueKind));
    this.DataType = (SettingItemDataType) info.GetInt32(nameof (DataType));
    this.SettingItemAttributeBelongs = (SettingItemAttributeSourceType) info.GetInt32(nameof (SettingItemAttributeBelongs));
    this.AttributeValue = info.GetString(nameof (AttributeValue));
    this.SettingItemAttributeUpdateMode = (SettingItemAttributeUpdateMode) info.GetInt32(nameof (SettingItemAttributeUpdateMode));
    this.SyncImbase = info.GetBoolean(nameof (SyncImbase));
    this.Index = info.GetInt32(nameof (Index));
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("ItemType", (object) this.ItemType);
    string empty = string.Empty;
    if (this.TypeId != -1)
    {
      switch (this.ItemType)
      {
        case SettingItemType.AttributeType:
          empty = MetaDataHelper.GetAttributeTypeGuid(this.TypeId).ToString();
          break;
        case SettingItemType.ObjectType:
        case SettingItemType.EntrancyObjectType:
          empty = MetaDataHelper.GetObjectTypeGuid(this.TypeId).ToString();
          break;
        case SettingItemType.RelationType:
          empty = MetaDataHelper.GetRelationTypeGuid(this.TypeId).ToString();
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
    info.AddValue("TypeId", (object) empty);
    info.AddValue("ValueKind", (object) this.ValueKind);
    info.AddValue("DataType", (object) this.DataType);
    info.AddValue("SettingItemAttributeBelongs", (object) this.SettingItemAttributeBelongs);
    info.AddValue("AttributeValue", (object) this.AttributeValue);
    info.AddValue("SettingItemAttributeUpdateMode", (object) this.SettingItemAttributeUpdateMode);
    info.AddValue("SyncImbase", this.SyncImbase);
    info.AddValue("Index", this.Index);
  }

  public override bool Equals(object obj) => this.Equals(obj as ColumnConfiguration);

  public bool Equals(ColumnConfiguration other)
  {
    return other != null && this.ItemType == other.ItemType && this.TypeId == other.TypeId && this.ValueKind == other.ValueKind && this.DataType == other.DataType && this.SettingItemAttributeBelongs == other.SettingItemAttributeBelongs && this.AttributeValue == other.AttributeValue && this.SettingItemAttributeUpdateMode == other.SettingItemAttributeUpdateMode && this.SyncImbase == other.SyncImbase && this.Index == other.Index;
  }

  public override int GetHashCode()
  {
    return ((((((((-627682607 * -1521134295 + this.ItemType.GetHashCode()) * -1521134295 + this.TypeId.GetHashCode()) * -1521134295 + this.ValueKind.GetHashCode()) * -1521134295 + this.DataType.GetHashCode()) * -1521134295 + this.SettingItemAttributeBelongs.GetHashCode()) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.AttributeValue)) * -1521134295 + this.SettingItemAttributeUpdateMode.GetHashCode()) * -1521134295 + this.SyncImbase.GetHashCode()) * -1521134295 + this.Index.GetHashCode();
  }

  public object Clone()
  {
    return (object) new ColumnConfiguration()
    {
      AttributeValue = this.AttributeValue,
      DataType = this.DataType,
      Index = this.Index,
      ItemType = this.ItemType,
      SettingItemAttributeBelongs = this.SettingItemAttributeBelongs,
      SettingItemAttributeUpdateMode = this.SettingItemAttributeUpdateMode,
      SyncImbase = this.SyncImbase,
      TypeId = this.TypeId,
      ValueKind = this.ValueKind
    };
  }

  public XmlSchema GetSchema() => (XmlSchema) null;

  public void ReadXml(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "AttributeValue":
            this.AttributeValue = reader.Value;
            continue;
          case "DataType":
            this.DataType = (SettingItemDataType) Convert.ToInt32(reader.Value);
            continue;
          case "Index":
            this.Index = Convert.ToInt32(reader.Value);
            continue;
          case "ItemType":
            this.ItemType = (SettingItemType) Convert.ToInt32(reader.Value);
            continue;
          case "SettingItemAttributeBelongs":
            this.SettingItemAttributeBelongs = (SettingItemAttributeSourceType) Convert.ToInt32(reader.Value);
            continue;
          case "SettingItemAttributeUpdateMode":
            this.SettingItemAttributeUpdateMode = (SettingItemAttributeUpdateMode) Convert.ToInt32(reader.Value);
            continue;
          case "SyncImbase":
            this.SyncImbase = Convert.ToBoolean(reader.Value);
            continue;
          case "TypeId":
            this.TypeId = -1;
            string Guid = reader.Value;
            if (!string.IsNullOrEmpty(Guid))
            {
              switch (this.ItemType)
              {
                case SettingItemType.AttributeType:
                  this.TypeId = MetaDataHelper.GetAttributeTypeID(Guid);
                  continue;
                case SettingItemType.ObjectType:
                case SettingItemType.EntrancyObjectType:
                  this.TypeId = MetaDataHelper.GetObjectTypeID(Guid);
                  continue;
                case SettingItemType.RelationType:
                  this.TypeId = MetaDataHelper.GetRelationTypeID(Guid);
                  continue;
                default:
                  throw new ArgumentOutOfRangeException();
              }
            }
            else
              continue;
          case "ValueKind":
            this.ValueKind = (SettingItemValueKind) Convert.ToInt32(reader.Value);
            continue;
          default:
            continue;
        }
      }
    }
    reader.Read();
  }

  public void WriteXml(XmlWriter writer)
  {
    writer.WriteAttributeString("ItemType", this.ItemType.ToString("d"));
    string empty = string.Empty;
    if (this.TypeId != -1)
    {
      switch (this.ItemType)
      {
        case SettingItemType.AttributeType:
          empty = MetaDataHelper.GetAttributeTypeGuid(this.TypeId).ToString();
          break;
        case SettingItemType.ObjectType:
        case SettingItemType.EntrancyObjectType:
          empty = MetaDataHelper.GetObjectTypeGuid(this.TypeId).ToString();
          break;
        case SettingItemType.RelationType:
          empty = MetaDataHelper.GetRelationTypeGuid(this.TypeId).ToString();
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
    writer.WriteAttributeString("TypeId", empty);
    writer.WriteAttributeString("ValueKind", this.ValueKind.ToString("d"));
    writer.WriteAttributeString("DataType", this.DataType.ToString("d"));
    writer.WriteAttributeString("SettingItemAttributeBelongs", this.SettingItemAttributeBelongs.ToString("d"));
    writer.WriteAttributeString("AttributeValue", this.AttributeValue);
    writer.WriteAttributeString("SettingItemAttributeUpdateMode", this.SettingItemAttributeUpdateMode.ToString("d"));
    writer.WriteAttributeString("SyncImbase", this.SyncImbase.ToString());
    writer.WriteAttributeString("Index", this.Index.ToString());
  }
}
