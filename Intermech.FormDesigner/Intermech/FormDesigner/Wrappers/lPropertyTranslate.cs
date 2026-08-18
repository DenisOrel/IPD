// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.lPropertyTranslate
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Класс для перевода значний</summary>
public class lPropertyTranslate : Dictionary<Type, Dictionary<string, lPropertyTemplate>>
{
  private Dictionary<string, lPropertyTemplate> _common = new Dictionary<string, lPropertyTemplate>();
  /// <summary>Статиский экземпляр</summary>
  public static lPropertyTranslate PropertyTranslate = new lPropertyTranslate();

  /// <summary>Добавить общие свойств</summary>
  /// <param name="template"></param>
  public void AddCommonTemplate(lPropertyTemplate template)
  {
    this._common[template.EnglishName] = template;
  }

  /// <summary>Перевести одиночное свойсто</summary>
  /// <param name="component">исходный компонент</param>
  /// <param name="opd">исходный PropertyDescriptor</param>
  /// <param name="onlyTranslated">null-если свойство не переведено, иначе исходное значение PropertyDescriptor</param>
  /// <returns>результат перевода (в зависимости от onlyTranslated)</returns>
  public PropertyDescriptor Translate(
    object component,
    PropertyDescriptor opd,
    bool onlyTranslated)
  {
    Dictionary<string, lPropertyTemplate> common;
    if (!this.ContainsKey(opd.ComponentType))
    {
      if (!this._common.ContainsKey(opd.Name))
        return !onlyTranslated ? opd : (PropertyDescriptor) null;
      common = this._common;
    }
    else
      common = this[opd.ComponentType];
    if (!common.ContainsKey(opd.Name))
      return !onlyTranslated ? opd : (PropertyDescriptor) null;
    lPropertyDescriptor pd = component == null ? new lPropertyDescriptor(opd) : new lPropertyDescriptor(opd, opd.GetValue(component));
    lPropertyTemplate propertyTemplate = common[opd.Name];
    if (propertyTemplate.HasBeforeEvent)
      pd.BeforeSetValue += new PropertySetValue(this.npd_BeforeSetValue);
    if (propertyTemplate.HasAfterEvent)
      pd.AfterSetValue += new PropertySetValue(this.npd_AfterSetValue);
    pd.Template = propertyTemplate;
    if (propertyTemplate.HasAddCustomAttribute)
      propertyTemplate.OnAddCustomAttribute(component, (PropertyDescriptor) pd);
    Attribute[] collection = new Attribute[pd.Attributes.Count];
    pd.Attributes.CopyTo((Array) collection, 0);
    List<Type> typeList = new List<Type>();
    foreach (Attribute attribute in pd.Attributes)
      typeList.Add(attribute.GetType());
    List<Attribute> attributeList = new List<Attribute>((IEnumerable<Attribute>) collection);
    foreach (Attribute attribute in propertyTemplate.Attributes)
    {
      Type type = attribute.GetType();
      int index = typeList.IndexOf(type);
      if (index >= 0)
        attributeList[index] = attribute;
      else
        attributeList.Add(attribute);
    }
    pd.SetAttributes(attributeList.ToArray());
    return (PropertyDescriptor) pd;
  }

  private void npd_AfterSetValue(object component, SetValueEventArgs e)
  {
    if (!(e.PropertyDescriptor is lPropertyDescriptor propertyDescriptor))
      return;
    propertyDescriptor.Template.OnAfterSetValue(component, e);
  }

  private void npd_BeforeSetValue(object component, SetValueEventArgs e)
  {
    if (!(e.PropertyDescriptor is lPropertyDescriptor propertyDescriptor))
      return;
    propertyDescriptor.Template.OnBeforeSetValue(component, e);
  }
}
