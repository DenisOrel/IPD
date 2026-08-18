// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.ControlColors
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Drawing;

#nullable disable
namespace Intermech.Document.Model.UI;

/// <summary>Цвета фона и текста для различных элементов управления</summary>
public static class ControlColors
{
  /// <summary>Цвет нейтрального по цвету фона элемента управления для ввода текста или числовых значений</summary>
  public static Color colorInvariantElement = Color.White;
  /// <summary>Цвет активного элемента управления, отвечающего за горизонтальные размеры или отступы</summary>
  public static Color colorHorizSizeActive = SystemColors.Window;
  /// <summary>Цвет неактивного элемента управления, отвечающего за горизонтальные размеры или отступы</summary>
  public static Color colorHorizSizeInactive = Color.FromArgb(242, 242, 242);
  /// <summary>Цвет активного элемента управления, отвечающего за вертикальные размеры или отступы</summary>
  public static Color colorVerticalSizeActive = SystemColors.Window;
  /// <summary>Цвет неактивного элемента управления, отвечающего за вертикальные размеры или отступы</summary>
  public static Color colorVerticalSizeInactive = Color.FromArgb(228, 224 /*0xE0*/, 216);
  /// <summary>Цвет фона ячейки, в которой нельзя выполнять редактирование текста</summary>
  public static Color colorDisabledCell = Color.FromArgb(242, 242, 242);
  /// <summary>Цвет фона ячейки, в которой указано некорректное значение</summary>
  public static Color colorErrorCell = Color.FromArgb((int) byte.MaxValue, 200, 200);
  /// <summary>Цвет фона ячейки, принадлежащей заголовку</summary>
  public static Color colorHeaderCell = Color.FromArgb(212, 225, 247);
  /// <summary>Цвет текса в запрещённой для редактирования ячейке</summary>
  public static Color colorDisabledText = SystemColors.GrayText;
  /// <summary>Цвет текса в разрешённой для редактирования ячейке</summary>
  public static Color colorEnabledText = SystemColors.ControlText;
  /// <summary>Цвет фона для заголовков и группирующих панелей</summary>
  public static Color colorHeaderBackground = Color.FromArgb(185, 181, 174);
}
