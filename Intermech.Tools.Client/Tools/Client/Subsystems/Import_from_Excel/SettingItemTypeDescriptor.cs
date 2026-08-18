// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.SettingItemTypeDescriptor
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Localization;
using Intermech.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

internal class SettingItemTypeDescriptor : ICustomTypeDescriptor
{
  private PropertyDescriptorCollection _pdc;
  private readonly ColumnConfiguration _columnConfiguration;
  private DataTable _dtData;

  public SettingItemTypeDescriptor(ColumnConfiguration columnConfiguration, DataTable dtData)
  {
    this._columnConfiguration = columnConfiguration;
    this._dtData = dtData;
  }

  private void CreatePdc(Attribute[] attributes)
  {
    List<System.ComponentModel.PropertyDescriptor> propertyDescriptorList = new List<System.ComponentModel.PropertyDescriptor>();
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this._columnConfiguration, attributes, true);
    System.ComponentModel.PropertyDescriptor propertyDescriptor1 = properties["ValueKind"];
    if (propertyDescriptor1 != null)
      propertyDescriptorList.Add(propertyDescriptor1);
    switch (this._columnConfiguration.ItemType)
    {
      case SettingItemType.AttributeType:
        System.ComponentModel.PropertyDescriptor propertyDescriptor2 = properties["SettingItemAttributeBelongs"];
        if (propertyDescriptor2 != null)
          propertyDescriptorList.Add(propertyDescriptor2);
        System.ComponentModel.PropertyDescriptor propertyDescriptor3 = properties["SettingItemAttributeUpdateMode"];
        if (propertyDescriptor3 != null)
          propertyDescriptorList.Add(propertyDescriptor3);
        if (this._columnConfiguration.ValueKind == SettingItemValueKind.Constant)
        {
          System.ComponentModel.PropertyDescriptor propertyDescriptor4 = properties["AttributeValue"];
          if (propertyDescriptor4 != null)
            propertyDescriptorList.Add(propertyDescriptor4);
        }
        if (this._columnConfiguration.SettingItemAttributeBelongs == SettingItemAttributeSourceType.Object && (!this.HasImbaseSyncAttribute() || this._columnConfiguration.SyncImbase))
        {
          System.ComponentModel.PropertyDescriptor propertyDescriptor5 = properties["SyncImbase"];
          if (propertyDescriptor5 != null)
          {
            propertyDescriptorList.Add(propertyDescriptor5);
            break;
          }
          break;
        }
        break;
      case SettingItemType.ObjectType:
      case SettingItemType.EntrancyObjectType:
        if (this._columnConfiguration.ValueKind == SettingItemValueKind.Constant)
        {
          System.ComponentModel.PropertyDescriptor propDesc = properties["TypeId"];
          if (propDesc != null)
          {
            SettingItemPropertyDescriptor propertyDescriptor6 = new SettingItemPropertyDescriptor((object) this._columnConfiguration, propDesc);
            propertyDescriptor6.AddAttribute((Attribute) new TypeConverterAttribute(typeof (ObjectTypeLinkConverter)));
            propertyDescriptor6.AddAttribute((Attribute) new EditorAttribute(typeof (ObjectTypeLinkEditor), typeof (UITypeEditor)));
            propertyDescriptor6.SetDisplayName(this._columnConfiguration.ItemType == SettingItemType.ObjectType ? LocalizationHolder.rm.GetString("Tools.Client_248") : LocalizationHolder.rm.GetString("Tools.Client_303"));
            propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor6);
            break;
          }
          break;
        }
        System.ComponentModel.PropertyDescriptor propertyDescriptor7 = properties["DataType"];
        if (propertyDescriptor7 != null)
        {
          propertyDescriptorList.Add(propertyDescriptor7);
          break;
        }
        break;
      case SettingItemType.RelationType:
        if (this._columnConfiguration.ValueKind == SettingItemValueKind.Constant)
        {
          System.ComponentModel.PropertyDescriptor propDesc = properties["TypeId"];
          if (propDesc != null)
          {
            SettingItemPropertyDescriptor propertyDescriptor8 = new SettingItemPropertyDescriptor((object) this._columnConfiguration, propDesc);
            propertyDescriptor8.AddAttribute((Attribute) new TypeConverterAttribute(typeof (RelationTypeLinkConverter)));
            propertyDescriptor8.AddAttribute((Attribute) new EditorAttribute(typeof (RelationTypeAttEditor), typeof (UITypeEditor)));
            propertyDescriptor8.SetDisplayName(LocalizationHolder.rm.GetString("Tools.Client_249"));
            propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor8);
            break;
          }
          break;
        }
        System.ComponentModel.PropertyDescriptor propertyDescriptor9 = properties["DataType"];
        if (propertyDescriptor9 != null)
        {
          propertyDescriptorList.Add(propertyDescriptor9);
          break;
        }
        break;
    }
    this._pdc = new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
  }

  private bool HasImbaseSyncAttribute()
  {
    return this._dtData?.Columns != null && this._dtData.Columns.Cast<DataColumn>().Select<DataColumn, ColumnConfiguration>((System.Func<DataColumn, ColumnConfiguration>) (x => x.ExtendedProperties[(object) Consts.ColumnPropName] as ColumnConfiguration)).Where<ColumnConfiguration>((System.Func<ColumnConfiguration, bool>) (x => x != null)).Any<ColumnConfiguration>((System.Func<ColumnConfiguration, bool>) (x => x.ItemType == SettingItemType.AttributeType && x.SettingItemAttributeBelongs == SettingItemAttributeSourceType.Object && x.SyncImbase));
  }

  public System.ComponentModel.AttributeCollection GetAttributes()
  {
    return TypeDescriptor.GetAttributes((object) this._columnConfiguration, true);
  }

  public string GetClassName()
  {
    return TypeDescriptor.GetClassName((object) this._columnConfiguration, true);
  }

  public string GetComponentName()
  {
    return TypeDescriptor.GetComponentName((object) this._columnConfiguration, true);
  }

  public TypeConverter GetConverter()
  {
    return TypeDescriptor.GetConverter((object) this._columnConfiguration, true);
  }

  public EventDescriptor GetDefaultEvent()
  {
    return TypeDescriptor.GetDefaultEvent((object) this._columnConfiguration, true);
  }

  public System.ComponentModel.PropertyDescriptor GetDefaultProperty()
  {
    return TypeDescriptor.GetDefaultProperty((object) this._columnConfiguration, true);
  }

  public object GetEditor(Type editorBaseType)
  {
    return TypeDescriptor.GetEditor((object) this._columnConfiguration, editorBaseType, true);
  }

  public EventDescriptorCollection GetEvents(Attribute[] attributes)
  {
    return TypeDescriptor.GetEvents((object) this._columnConfiguration, attributes, true);
  }

  public EventDescriptorCollection GetEvents()
  {
    return TypeDescriptor.GetEvents((object) this._columnConfiguration, true);
  }

  public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
  {
    this.CreatePdc(attributes);
    return this._pdc ?? new PropertyDescriptorCollection((System.ComponentModel.PropertyDescriptor[]) null);
  }

  public PropertyDescriptorCollection GetProperties() => this.GetProperties(new Attribute[0]);

  public object GetPropertyOwner(System.ComponentModel.PropertyDescriptor pd)
  {
    return (object) this._columnConfiguration;
  }
}
