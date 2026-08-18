// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.ListContextConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.FormDesigner.Descriptors;
using Intermech.Localization;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Конвертер для свойства "Список".</summary>
public class ListContextConverter : BaseEnumConverter
{
  /// <summary>Конструктор.</summary>
  public ListContextConverter()
    : base(typeof (ListContext))
  {
    this._hash.Add((object) ListContext.Objects, (object) LocalizationHolder.rm.GetString("FormDesigner_ListContextConverter_Objects"));
    this._hash.Add((object) ListContext.Composition, (object) LocalizationHolder.rm.GetString("FormDesigner_ListContextConverter_Composition"));
    this._hash.Add((object) ListContext.Applicability, (object) LocalizationHolder.rm.GetString("FormDesigner_ListContextConverter_Applicability"));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public override bool GetPropertiesSupported(ITypeDescriptorContext context)
  {
    bool propertiesSupported = false;
    if (context.PropertyDescriptor is FormDesignerControlsPropertyDescriptor propertyDescriptor)
      propertiesSupported = (ListContext) propertyDescriptor.GetValue((context.Instance as ObjectsListDescriptor).BaseClass) == ListContext.Composition;
    return propertiesSupported;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="value"></param>
  /// <param name="attributes"></param>
  /// <returns></returns>
  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    return !(context.PropertyDescriptor is FormDesignerControlsPropertyDescriptor propertyDescriptor) ? (PropertyDescriptorCollection) null : propertyDescriptor.ChildProperties;
  }
}
