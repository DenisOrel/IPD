// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.AutoPlace.AttributeModel
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System;

#nullable disable
namespace Intermech.FormDesigner.AutoPlace;

/// <summary>
/// Модель атрибута
/// тип элемента управления
/// позиционирование метки
/// ширина контрола
/// наименование
/// </summary>
internal class AttributeModel
{
  /// <summary>
  /// 
  /// </summary>
  public Type ControlType { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public LabelArrange Arrange { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public int Width { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public string Name { get; set; }

  /// <summary>Конструктор.</summary>
  /// <param name="name">Наименование атрибута</param>
  /// <param name="arrange">Позиционирование метки</param>
  /// <param name="controlType">Тип элемента управления</param>
  /// <param name="width">Ширина элемента управления</param>
  public AttributeModel(string name = "", LabelArrange arrange = LabelArrange.laNone, Type controlType = null, int width = 0)
  {
    this.Arrange = LabelArrange.laNone;
    this.ControlType = controlType;
    this.Name = name;
    this.Width = width;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString() => this.Name;
}
