// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.SettingItem
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Search;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

[Obsolete]
[Serializable]
public class SettingItem
{
  [OptionalField]
  private int _index;

  public SettingItemType ItemType { get; private set; }

  [CustomDisplayName("Tools.Client_292")]
  [CustomDescription("Tools.Client_286")]
  [CustomCategory("Tools.Client_298")]
  public int TypeId { get; set; }

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
  public int Index
  {
    get => this._index;
    set => this._index = value;
  }

  public SettingItem(SettingItemType itemItemType)
    : this(itemItemType, -1)
  {
  }

  public SettingItem(SettingItemType itemItemType, int id)
  {
    this.AttributeValue = string.Empty;
    this.ItemType = itemItemType;
    this.TypeId = id;
    if (itemItemType != SettingItemType.AttributeType)
      return;
    this.DataType = SettingItemDataType.TypeId;
  }
}
