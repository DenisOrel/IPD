// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem.GridViewSettingsAppl
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem;

internal class GridViewSettingsAppl : GridViewSettingsExportItem
{
  private readonly string _сategoryExpSetting = "Настройки экспорта";
  private readonly string _сategoryRelType = "Тип связи";
  private readonly string _сategoryProjType = "Тип родительского объекта";
  private readonly string _сategoryPartType = "Тип дочернего объекта";
  private XmlExchangeExportAppl _exportAppl;
  private bool _inBase;

  public GridViewSettingsAppl(XmlExchangeExportAppl exportAppl, bool inBase, bool readOnly)
    : base((XmlExchangeExportItem) exportAppl, readOnly)
  {
    this._exportAppl = exportAppl;
    this._inBase = inBase;
  }

  private void CreatePdc(Attribute[] attributes)
  {
    if (this._exportAppl == null)
      return;
    List<PropertyDescriptor> listProperty = new List<PropertyDescriptor>();
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this._exportAppl, attributes, true);
    PropertyDescriptor propDesc1 = properties["RelTypeID"];
    if (propDesc1 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportAppl, propDesc1);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._сategoryRelType));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Тип связи"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("reltypeid - Тип связи"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc2 = properties["RelTypeGuid"];
    if (propDesc2 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportAppl, propDesc2);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._сategoryRelType));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Глобальный идентификатор типа связи"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("reltype_guid - Глобальный идентификатор типа связи"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (TypeRelationConverter)));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc3 = properties["ProjTypeID"];
    if (propDesc3 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportAppl, propDesc3);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._сategoryProjType));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Тип родительского объекта"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("projtypeid - Тип родительского объекта"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc4 = properties["ProjTypeGuid"];
    if (propDesc4 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportAppl, propDesc4);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._сategoryProjType));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Глобальный идентификатор типа родительского объекта"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("projtype_guid - Глобальный идентификатор типа родительского объекта"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (TypeObjectConverter)));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc5 = properties["PartTypeID"];
    if (propDesc5 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportAppl, propDesc5);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._сategoryPartType));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Тип дочернего объекта"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("parttypeid - Тип дочернего объекта"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(this._inBase));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc6 = properties["PartTypeGuid"];
    if (propDesc6 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportAppl, propDesc6);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._сategoryPartType));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Глобальный идентификатор типа дочернего объекта"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("parttype_guid - Глобальный идентификатор типа дочернего объекта"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(this._inBase));
      if (this._inBase)
        propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (TypeObjectConverter)));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc7 = properties["ApplMode"];
    if (propDesc7 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportAppl, propDesc7);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._сategoryExpSetting));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Режим проверки применяемости/раскрытия состава"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("applmode - Режим проверки применяемости/раскрытия состава для объектов при экспорте"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc8 = properties["DirMode"];
    if (propDesc8 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportAppl, propDesc8);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._сategoryExpSetting));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Направление действия правила применяемости/раскрытия составов"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("dirmode - Направление действия правила применяемости/раскрытия составов"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    this.CreateCollectionProperty(listProperty);
  }

  public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
  {
    if (this._pdc == null)
    {
      base.GetProperties(attributes);
      this.CreatePdc(attributes);
    }
    return this._pdc ?? new PropertyDescriptorCollection((PropertyDescriptor[]) null);
  }
}
