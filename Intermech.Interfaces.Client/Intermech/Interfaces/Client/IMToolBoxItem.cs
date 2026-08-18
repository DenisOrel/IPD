// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IMToolBoxItem
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Drawing;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Класс описывающий item в ToolBox.</summary>
public class IMToolBoxItem : ToolboxItem
{
  /// <summary>Наименование категории.</summary>
  public string ItemCategory { get; set; }

  /// <summary>Наведен указатель.</summary>
  public bool Hovered { get; set; }

  /// <summary>Выделен.</summary>
  public bool Selected { get; set; }

  /// <summary>Тип элемента.</summary>
  public Type ItemType { get; private set; }

  /// <summary>Тип враппера.</summary>
  public Type WrapperType { get; private set; }

  /// <summary>Конструктор.</summary>
  /// <param name="name">Наименование элемента</param>
  /// <param name="toolType">Тип элемента</param>
  /// <param name="wrapperType">Тип враппера</param>
  /// <param name="image">Изображение элемента</param>
  public IMToolBoxItem(string name, Type toolType, Type wrapperType, Bitmap image)
    : this(name, toolType, wrapperType, string.Empty, image)
  {
  }

  /// <summary>Конструктор.</summary>
  /// <param name="name">Наименование элемента</param>
  /// <param name="toolType">Тип элемента</param>
  /// <param name="wrapperType">Тип враппера</param>
  /// <param name="category">Наименование категории</param>
  /// <param name="image">Изображение элемента</param>
  public IMToolBoxItem(
    string name,
    Type toolType,
    Type wrapperType,
    string category = "",
    Bitmap image = null)
    : base(toolType)
  {
    this.DisplayName = name;
    this.ItemType = toolType;
    this.WrapperType = wrapperType;
    this.ItemCategory = category;
    if (image == null)
      return;
    this.Bitmap = image;
    this.Bitmap.MakeTransparent(Color.Magenta);
  }
}
