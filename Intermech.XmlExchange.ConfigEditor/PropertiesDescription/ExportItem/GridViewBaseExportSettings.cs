// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem.GridViewBaseExportSettings
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem;

internal class GridViewBaseExportSettings : ICustomTypeDescriptor, IConfigItemProperties
{
  protected PropertyDescriptorCollection _pdc;
  protected XmlExchangeExportSettings _baseExportSettings;

  public GridViewBaseExportSettings(XmlExchangeExportSettings baseExportSettings, bool readOnly)
  {
    this._baseExportSettings = baseExportSettings;
    this.ReadOnlyProperties(readOnly);
  }

  protected void CreatePdc(Attribute[] attributes)
  {
    if (this._baseExportSettings == null)
      return;
    string category1 = "Файлы результата выгрузки";
    string category2 = "Настройки экспорта";
    List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this._baseExportSettings, attributes, true);
    PropertyDescriptor propDesc1 = properties["PacketFileFormat"];
    if (propDesc1 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._baseExportSettings, propDesc1);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category1));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Шаблон генерации имени файла пакета экспорта"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("packetfileformat - Шаблон генерации имени файла пакета экспорта"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptorList.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc2 = properties["LogFileFormat"];
    if (propDesc2 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._baseExportSettings, propDesc2);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category1));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Шаблон генерации имени файла лога"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("logfileformat - Шаблон генерации имени файла лога"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptorList.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc3 = properties["MetaFileFormat"];
    if (propDesc3 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._baseExportSettings, propDesc3);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category1));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Шаблон генерации имени файла экспорта метаданных"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("metafileformat - Шаблон генерации имени файла экспорта метаданных"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptorList.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc4 = properties["ObjFileFormat"];
    if (propDesc4 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._baseExportSettings, propDesc4);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category1));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Шаблон генерации имени файла с данными экспортированных объектов"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("objfileformat - Шаблон генерации имени файла с данными экспортированных объектов"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptorList.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc5 = properties["RelFileFormat"];
    if (propDesc5 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._baseExportSettings, propDesc5);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category1));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Шаблон генерации имени файла с данными экспортированных связей"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("relfileformat - Шаблон генерации имени файла с данными экспортированных связей"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptorList.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc6 = properties["DataDirFormat"];
    if (propDesc6 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._baseExportSettings, propDesc6);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category1));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Шаблон генерации имени папки с двоичными данными, выгружаемыми файлами"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("datadirformat - Шаблон генерации имени папки с двоичными данными, выгружаемыми файлами"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptorList.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc7 = properties["DateTimeFormat"];
    if (propDesc7 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._baseExportSettings, propDesc7);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category1));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Шаблон сохранения данных типа дата/время в XML"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("datetimeformat – Шаблон сохранения данных типа дата/время в XML"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptorList.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc8 = properties["TimeZoneName"];
    if (propDesc8 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._baseExportSettings, propDesc8);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category1));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Наименование часового пояса, для которого будут выгружаться  данные содержащие время"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute($"timezone - Наименование часового пояса, для которого будут выгружаться  данные содержащие время.{Environment.NewLine}Если параметр не задан, по умолчанию время выгружается в виде GMT+0 (по Гринвичу)."));
      propertyDescriptor.AddAttribute((Attribute) new EditorAttribute(typeof (TimeZoneEditor), typeof (UITypeEditor)));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (TimeZoneConverter)));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptorList.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc9 = properties["CompressMode"];
    if (propDesc9 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._baseExportSettings, propDesc9);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category1));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Режим архивации экспортируемых данных"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("сompress – Режим архивации экспортируемых данных"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptorList.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc10 = properties["TaskMode"];
    if (propDesc10 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._baseExportSettings, propDesc10);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category1));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Режим выгрузки - записи объектов в файл при экспорте"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("task - Режим выгрузки - записи объектов в файл при экспорте"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptorList.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc11 = properties["ChecksumMode"];
    if (propDesc11 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._baseExportSettings, propDesc11);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category1));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Режим выгрузки контрольных сумм для файлов."));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("сhecksum – Режим выгрузки контрольных сумм для файлов"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptorList.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc12 = properties["ExtraDataMode"];
    if (propDesc12 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._baseExportSettings, propDesc12);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category2));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Режимы выгрузки дополнительных данных"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("extradata - Режимы выгрузки дополнительных данных. Параметр представляет собой набор битовых флагов."));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptor.AddAttribute((Attribute) new EditorAttribute(typeof (ExtraDataModeSelected), typeof (UITypeEditor)));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (ExtraDataModeConverter)));
      propertyDescriptorList.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc13 = properties["DefObjAttrMode"];
    if (propDesc13 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._baseExportSettings, propDesc13);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category2));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Режим выгрузки атрибутов объектов/связей по умолчанию"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute($"defattr - Режим выгрузки атрибутов объектов/связей по умолчанию. {Environment.NewLine}Используется для  определения правила выгрузки атрибутов объектов/связей, типы которых не указаны в настройка экспорта. "));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptorList.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc14 = properties["ObjVerRule"];
    if (propDesc14 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._baseExportSettings, propDesc14);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category2));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Глобальный идентификатор правила подбора версий объектов"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute($"objverrule – Глобальный идентификатор правила подбора версий объектов. {Environment.NewLine}Если параметр не задан – используется текущее правило подбора (заданное в навигаторе)"));
      propertyDescriptor.AddAttribute((Attribute) new EditorAttribute(typeof (RulePropertyEditor), typeof (UITypeEditor)));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (RuleConverter)));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptorList.Add((PropertyDescriptor) propertyDescriptor);
    }
    this._pdc = new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
  }

  public AttributeCollection GetAttributes()
  {
    return TypeDescriptor.GetAttributes((object) this._baseExportSettings, true);
  }

  public string GetClassName()
  {
    return TypeDescriptor.GetClassName((object) this._baseExportSettings, true);
  }

  public string GetComponentName()
  {
    return TypeDescriptor.GetComponentName((object) this._baseExportSettings, true);
  }

  public TypeConverter GetConverter()
  {
    return TypeDescriptor.GetConverter((object) this._baseExportSettings, true);
  }

  public EventDescriptor GetDefaultEvent()
  {
    return TypeDescriptor.GetDefaultEvent((object) this._baseExportSettings, true);
  }

  public PropertyDescriptor GetDefaultProperty()
  {
    return TypeDescriptor.GetDefaultProperty((object) this._baseExportSettings, true);
  }

  public object GetEditor(Type editorBaseType)
  {
    return TypeDescriptor.GetEditor((object) this._baseExportSettings, editorBaseType, true);
  }

  public EventDescriptorCollection GetEvents(Attribute[] attributes)
  {
    return TypeDescriptor.GetEvents((object) this._baseExportSettings, attributes, true);
  }

  public EventDescriptorCollection GetEvents()
  {
    return TypeDescriptor.GetEvents((object) this._baseExportSettings, true);
  }

  public PropertyDescriptorCollection GetProperties() => this.GetProperties(new Attribute[0]);

  public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
  {
    if (this._pdc == null)
      this.CreatePdc(attributes);
    return this._pdc ?? new PropertyDescriptorCollection((PropertyDescriptor[]) null);
  }

  public object GetPropertyOwner(PropertyDescriptor pd)
  {
    return pd is XmlConfigPropertyDescriptor propertyDescriptor ? propertyDescriptor.Owner : (object) this._baseExportSettings;
  }

  public void ResetOldValues()
  {
    if (this._pdc == null)
      return;
    foreach (PropertyDescriptor propertyDescriptor1 in this._pdc)
    {
      if (propertyDescriptor1 is XmlConfigPropertyDescriptor propertyDescriptor2)
        propertyDescriptor2.ResetOldValue((object) this._baseExportSettings);
    }
  }

  public void ResetValues()
  {
    if (this._pdc == null)
      return;
    foreach (PropertyDescriptor propertyDescriptor1 in this._pdc)
    {
      if (propertyDescriptor1 is XmlConfigPropertyDescriptor propertyDescriptor2)
        propertyDescriptor2.ResetValue((object) this._baseExportSettings);
    }
  }

  private void SetOldValues()
  {
    if (this._pdc == null)
      return;
    foreach (PropertyDescriptor propertyDescriptor1 in this._pdc)
    {
      if (propertyDescriptor1 is XmlConfigPropertyDescriptor propertyDescriptor2)
        propertyDescriptor2.SetOldValue((object) this._baseExportSettings);
    }
  }

  public void SaveSettings()
  {
    this.ResetOldValues();
    ConfigEditorModeView.GetModeView()?.GetConfig(this._baseExportSettings);
  }

  public void ResetSettings() => this.SetOldValues();

  public void ReadOnlyProperties(bool readOnly)
  {
    if (!readOnly)
      return;
    TypeDescriptor.AddAttributes((object) this._baseExportSettings, (Attribute) new ReadOnlyAttribute(true));
  }
}
