// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.lPropertyTemplate
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Прототип свойства для преобразования</summary>
public class lPropertyTemplate
{
  private string _engName;
  private string _rusName;
  private List<Attribute> _attrList = new List<Attribute>();

  private event PropertySetValue _beforeSetValue;

  private event PropertySetValue _afterSetValue;

  private event AddCustomAttribute _addCustomAttribute;

  /// <summary>Конструктор</summary>
  /// <param name="englishName">Оригинальное название</param>
  /// <param name="russianName">Переведенное название</param>
  public lPropertyTemplate(string englishName, string russianName)
  {
    this._engName = englishName;
    this._rusName = russianName;
    this._attrList.Add((Attribute) new DisplayNameAttribute(russianName));
  }

  /// <summary>Конструктор</summary>
  /// <param name="englishName">Оригинальное название</param>
  /// <param name="russianName">Переведенное название</param>
  /// <param name="attributes">атрибуты свойства</param>
  public lPropertyTemplate(string englishName, string russianName, Attribute[] attributes)
    : this(englishName, russianName)
  {
    this._attrList.AddRange((IEnumerable<Attribute>) attributes);
  }

  /// <summary>Оригинальное значение</summary>
  public string EnglishName => this._engName;

  /// <summary>Переведенное значение</summary>
  public string RussianName => this._rusName;

  /// <summary>Атрибуты</summary>
  public List<Attribute> Attributes => this._attrList;

  /// <summary>Собитие перед изменением значения</summary>
  public event PropertySetValue BeforeSetValue
  {
    add => this._beforeSetValue += value;
    remove => this._beforeSetValue -= value;
  }

  /// <summary>Вызов события перед изменением значения</summary>
  /// <param name="component"></param>
  /// <param name="e"></param>
  internal void OnBeforeSetValue(object component, SetValueEventArgs e)
  {
    if (this._beforeSetValue == null)
      return;
    this._beforeSetValue(component, e);
  }

  /// <summary>Назначены ли события перед изменением значения</summary>
  public bool HasBeforeEvent
  {
    get => this._beforeSetValue != null && this._beforeSetValue.GetInvocationList().Length != 0;
  }

  /// <summary>Собитие после изменения значения</summary>
  public event PropertySetValue AfterSetValue
  {
    add => this._afterSetValue += value;
    remove => this._afterSetValue -= value;
  }

  /// <summary>Вызов события после изменения значения</summary>
  /// <param name="component"></param>
  /// <param name="e"></param>
  internal void OnAfterSetValue(object component, SetValueEventArgs e)
  {
    if (this._afterSetValue == null)
      return;
    this._afterSetValue(component, e);
  }

  /// <summary>Назначены ли события после изменением значения</summary>
  public bool HasAfterEvent
  {
    get => this._afterSetValue != null && this._afterSetValue.GetInvocationList().Length != 0;
  }

  /// <summary>Собитие для добавления пользовательских атрибутов</summary>
  public event AddCustomAttribute AddAttribute
  {
    add => this._addCustomAttribute += value;
    remove => this._addCustomAttribute -= value;
  }

  /// <summary>Вызов события</summary>
  /// <param name="component"></param>
  /// <param name="pd"></param>
  internal void OnAddCustomAttribute(object component, PropertyDescriptor pd)
  {
    if (this._addCustomAttribute == null)
      return;
    this._addCustomAttribute(component, pd);
  }

  /// <summary>Назначены ли обработчики на событие</summary>
  public bool HasAddCustomAttribute
  {
    get
    {
      return this._addCustomAttribute != null && this._addCustomAttribute.GetInvocationList().Length != 0;
    }
  }
}
