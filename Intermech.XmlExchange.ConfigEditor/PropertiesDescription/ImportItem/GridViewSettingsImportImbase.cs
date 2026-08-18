// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ImportItem.GridViewSettingsImportImbase
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.XmlExchange.ConfigEditor.ImportConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ImportItem;

internal class GridViewSettingsImportImbase(XmlExchangeImportImbase imbaseSettings, bool readOnly) : 
  GridViewSettingsImportItem((XmlExchangeImportItem) imbaseSettings, readOnly)
{
  private string _categoryCatalog = "Каталог ImBase для импорта";
  private string _categoryFolder = "Папка каталога ImBase для импорта";
  private string _categoryTable = "Таблица ImBase для импорта";

  public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
  {
    if (this._pdc == null)
    {
      base.GetProperties(attributes);
      this.CreatePdc(attributes);
    }
    return this._pdc ?? new PropertyDescriptorCollection((PropertyDescriptor[]) null);
  }

  private new void CreatePdc(Attribute[] attributes)
  {
    if (this._importItem == null)
      return;
    List<PropertyDescriptor> listProperty = new List<PropertyDescriptor>();
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this._importItem, attributes, true);
    PropertyDescriptor propDesc1 = properties["Catalog"];
    if (propDesc1 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._importItem, propDesc1);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._categoryCatalog));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Глобальный идентификатор версии каталога"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("guid - Глобальный идентификатор версии каталога"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptor.AddAttribute((Attribute) new EditorAttribute(typeof (CatalogImbaseSelected), typeof (UITypeEditor)));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (ImbaseItemConverter)));
      propertyDescriptor.AddAttribute((Attribute) new DefaultValueAttribute(typeof (XmlExchangeImportImbaseItem), string.Empty));
      propertyDescriptor.AddAttribute((Attribute) new RefreshPropertiesAttribute(RefreshProperties.All));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc2 = properties["Folder"];
    if (propDesc2 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._importItem, propDesc2);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._categoryFolder));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Глобальный идентификатор версии папки"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("guid - Глобальный идентификатор версии папки"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (ImbaseItemConverter)));
      propertyDescriptor.AddAttribute((Attribute) new EditorAttribute(typeof (FolderImbaseSelected), typeof (UITypeEditor)));
      propertyDescriptor.AddAttribute((Attribute) new DefaultValueAttribute(typeof (XmlExchangeImportImbaseItem), string.Empty));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc3 = properties["Table"];
    if (propDesc3 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._importItem, propDesc3);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._categoryTable));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Глобальный идентификатор версии таблицы"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("guid - Глобальный идентификатор версии таблицы"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (ImbaseItemConverter)));
      propertyDescriptor.AddAttribute((Attribute) new EditorAttribute(typeof (TableImbaseSelected), typeof (UITypeEditor)));
      propertyDescriptor.AddAttribute((Attribute) new DefaultValueAttribute(typeof (XmlExchangeImportImbaseItem), string.Empty));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    this.CreateCollectionProperty(listProperty);
  }
}
