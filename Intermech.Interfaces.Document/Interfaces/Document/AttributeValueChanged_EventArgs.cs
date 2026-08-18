// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.AttributeValueChanged_EventArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Аргумент обработчика события AttributeValueChanged</summary>
public class AttributeValueChanged_EventArgs : EventArgs
{
  /// <summary>Имя атрибута</summary>
  public string AttributeName;
  /// <summary>Старое значение атрибута</summary>
  public object OldValue;
  /// <summary>Новое значение атрибута</summary>
  public object NewValue;
  public bool UpdateUI;
  public bool UpdateLayout;

  /// <summary>Конструктор аргумента события AttributeValueChanged</summary>
  /// <param name="attributeName">Имя атрибута</param>
  /// <param name="oldValue">Старое значение атрибута</param>
  /// <param name="newValue">Новое значение атрибута</param>
  public AttributeValueChanged_EventArgs(
    string attributeName,
    object oldValue,
    object newValue,
    bool updateUI,
    bool updateLayout)
  {
    this.AttributeName = attributeName;
    this.OldValue = oldValue;
    this.NewValue = newValue;
    this.UpdateUI = updateUI;
    this.UpdateLayout = updateLayout;
  }
}
