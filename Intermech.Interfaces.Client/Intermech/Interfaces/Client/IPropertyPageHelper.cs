// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IPropertyPageHelper
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.Interfaces.Client;

public static class IPropertyPageHelper
{
  /// <summary>Возвращает список имен настроек PropertyGrid'а</summary>
  public static List<string> GetOptionNames(ICustomTypeDescriptor wrapper)
  {
    if (wrapper == null)
      throw new ArgumentNullException(nameof (wrapper));
    List<string> optionNames = new List<string>();
    PropertyDescriptorCollection properties = wrapper.GetProperties();
    if (properties != null)
    {
      optionNames = IPropertyPageHelper.GetBrowsableProperties(properties);
      List<string> browsableProperties = IPropertyPageHelper.GetNestedBrowsableProperties(properties, (object) wrapper);
      optionNames.AddRange((IEnumerable<string>) browsableProperties);
    }
    return optionNames;
  }

  /// <summary>
  /// Возвращает список имен чек-боксов и лейблов данного контрола,
  /// проверяя в том числе и вложенные контролы
  /// </summary>
  public static List<string> GetOptionNames(Control control)
  {
    if (control == null)
      throw new ArgumentNullException(nameof (control));
    List<string> optionNames1 = new List<string>();
    foreach (object control1 in (ArrangedElementCollection) control.Controls)
    {
      switch (control1)
      {
        case CheckBox checkBox:
          optionNames1.Add(checkBox.Text);
          continue;
        case Label label:
          optionNames1.Add(label.Text);
          continue;
        case ComboBox comboBox:
          IEnumerator enumerator = comboBox.Items.GetEnumerator();
          try
          {
            while (enumerator.MoveNext())
            {
              string str = enumerator.Current.ToString();
              if (str != null)
                optionNames1.Add(str);
            }
            continue;
          }
          finally
          {
            if (enumerator is IDisposable disposable)
              disposable.Dispose();
          }
        case RadioButton radioButton:
          optionNames1.Add(radioButton.Text);
          continue;
        case GroupBox groupBox:
          optionNames1.Add(groupBox.Text);
          List<string> stringList1 = optionNames1;
          List<string> optionNames2 = IPropertyPageHelper.GetOptionNames((Control) groupBox);
          IEnumerable<string> collection1 = optionNames2 != null ? optionNames2.Where<string>((Func<string, bool>) (name => name != null)) : (IEnumerable<string>) null;
          stringList1.AddRange(collection1);
          continue;
        case Control control2:
          List<string> stringList2 = optionNames1;
          List<string> optionNames3 = IPropertyPageHelper.GetOptionNames(control2);
          IEnumerable<string> collection2 = optionNames3 != null ? optionNames3.Where<string>((Func<string, bool>) (name => name != null)) : (IEnumerable<string>) null;
          stringList2.AddRange(collection2);
          continue;
        default:
          continue;
      }
    }
    return optionNames1;
  }

  public static List<string> GetOptionNames(object propertyPage)
  {
    if (propertyPage == null)
      throw new ArgumentNullException(nameof (propertyPage));
    List<string> optionNames = new List<string>();
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(propertyPage);
    if (properties != null)
    {
      optionNames = IPropertyPageHelper.GetBrowsableProperties(properties);
      List<string> browsableProperties = IPropertyPageHelper.GetNestedBrowsableProperties(properties, propertyPage);
      optionNames.AddRange((IEnumerable<string>) browsableProperties);
    }
    return optionNames;
  }

  /// <summary>
  /// Из данной коллекции свойств возвращает только те, что имеют != null DisplayName и являются Browsable
  /// </summary>
  private static List<string> GetBrowsableProperties(
    PropertyDescriptorCollection propertyDescriptors)
  {
    List<string> browsableProperties = new List<string>();
    foreach (object propertyDescriptor1 in propertyDescriptors)
    {
      if (propertyDescriptor1 != null && propertyDescriptor1 is PropertyDescriptor propertyDescriptor2 && !string.IsNullOrEmpty(propertyDescriptor2.DisplayName) && propertyDescriptor2.IsBrowsable)
        browsableProperties.Add(propertyDescriptor2.DisplayName);
    }
    return browsableProperties;
  }

  /// <summary>
  /// Из данной коллекции свойств возвращает вложенные свойства
  /// </summary>
  /// <param name="component">Родительский компонент свойств</param>
  private static List<string> GetNestedBrowsableProperties(
    PropertyDescriptorCollection propertyDescriptors,
    object component)
  {
    List<string> browsableProperties = new List<string>();
    foreach (object propertyDescriptor1 in propertyDescriptors)
    {
      if (propertyDescriptor1 != null && propertyDescriptor1 is PropertyDescriptor propertyDescriptor2 && !string.IsNullOrEmpty(propertyDescriptor2.DisplayName) && propertyDescriptor2.IsBrowsable)
      {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(propertyDescriptor2.GetValue(component));
        browsableProperties.AddRange((IEnumerable<string>) IPropertyPageHelper.GetBrowsableProperties(properties));
      }
    }
    return browsableProperties;
  }
}
