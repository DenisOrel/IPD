
// Type: Intermech.Navigator.UIColorsScheme
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Intermech.Navigator;

/// <summary>
/// Класс для сохранения цветовой схемы интерфейса пользователя
/// </summary>
[Serializable]
public sealed class UIColorsScheme
{
  /// <summary>
  /// Цвет фона в ячейках "Навигатора".
  /// SystemColors.Window
  /// </summary>
  public Color Background = SystemColors.Window;
  /// <summary>
  /// Цвет текста в ячейках "Навигатора".
  /// SystemColors.WindowText
  /// </summary>
  public Color Foreground = SystemColors.WindowText;
  /// <summary>
  /// Цвет фона в выделенных ячейках "Навигатора".
  /// System.Drawing.SystemColors.Highlight
  /// </summary>
  public Color BackgroundSelected = SystemColors.Highlight;
  /// <summary>
  /// Цвет текста в выделенных ячейках "Навигатора".
  /// System.Drawing.SystemColors.HighlightText
  /// </summary>
  public Color ForegroundSelected = SystemColors.HighlightText;
  /// <summary>
  /// Цвет фона в выделенных, но неактивных ячейках "Навигатора".
  /// System.Drawing.SystemColors.ControlLight
  /// </summary>
  public Color BackgroundSelectedInactive = SystemColors.ControlLight;
  /// <summary>
  /// Цвет текста в выделенных, но неактивных ячейках "Навигатора".
  /// System.Drawing.SystemColors.ControlText
  /// </summary>
  public Color ForegroundSelectedInactive = SystemColors.ControlText;
  /// <summary>
  /// Объект взят на изменение текущим пользователем.
  /// Цвет обычной заливки фона ячеек.
  /// Color.LightCyan
  /// </summary>
  public Color CheckedOutBkColor = Color.LightCyan;
  /// <summary>
  /// Объект взят на изменение текущим пользователем.
  /// Начальный цвет градиентной заливки фона ячеек.
  /// Color.LightSkyBlue
  /// </summary>
  public Color CheckedOutBkStartColor = Color.LightSkyBlue;
  /// <summary>
  /// Объект взят на изменение текущим пользователем.
  /// Конечный цвет градиентной заливки фона ячеек.
  /// Color.GhostWhite
  /// </summary>
  public Color CheckedOutBkEndColor = Color.GhostWhite;
  /// <summary>Заголовок сообщения в форуме</summary>
  public Color ForumCaptionBkColor = Color.DarkGray;
  /// <summary>Cообщение в форуме</summary>
  public Color ForumMessageBkColor = Color.LightGray;
  /// <summary>Цвет текста для заголовка сообщения в форуме</summary>
  public Color ForumCaptionColor = Color.Black;
  /// <summary>Цвет текста для сообщение в форуме</summary>
  public Color ForumMessageColor = Color.Black;
  /// <summary>
  /// Объект взят на изменение текущим пользователем.
  /// Режим градиентной заливки.
  /// LinearGradientMode.ForwardDiagonal
  /// </summary>
  public LinearGradientMode CheckedOutGradientMode = LinearGradientMode.ForwardDiagonal;
  /// <summary>
  ///  Объект взят на изменение текущим пользователем.
  /// Цвет текста
  /// </summary>
  public Color ForegroundCheckedOut = Color.Black;
  /// <summary>
  /// Объект взят на изменение пользователем, отличным от текущего.
  /// Цвет обычной заливки фона ячеек.
  /// Color.Cornsilk
  /// </summary>
  public Color CheckedOutOtherBkColor = Color.Cornsilk;
  /// <summary>
  /// Объект взят на изменение пользователем, отличным от текущего.
  /// Начальный цвет градиентной заливки фона ячеек.
  /// Color.NavajoWhite
  /// </summary>
  public Color CheckedOutOtherBkStartColor = Color.NavajoWhite;
  /// <summary>
  /// Объект взят на изменение пользователем, отличным от текущего.
  /// Конечный цвет градиентной заливки фона ячеек.
  /// Color.GhostWhite
  /// </summary>
  public Color CheckedOutOtherBkEndColor = Color.GhostWhite;
  /// <summary>
  /// Объект взят на изменение пользователем, отличным от текущего.
  /// Режим градиентной заливки.
  /// LinearGradientMode.BackwardDiagonal
  /// </summary>
  public LinearGradientMode CheckedOutOtherGradientMode = LinearGradientMode.BackwardDiagonal;
  /// <summary>
  /// Объект взят на изменение пользователем, отличным от текущего.
  /// Цвет текста
  /// </summary>
  public Color ForegroundCheckedOutOther = Color.Black;
  /// <summary>
  /// Хинт.
  /// Начальный цвет градиентной заливки фона.
  /// Color.PaleGreen
  /// </summary>
  public Color HintCellBkStartColor = Color.PaleGreen;
  /// <summary>
  /// Хинт.
  /// Конечный цвет градиентной заливки фона.
  /// Color.GhostWhite
  /// </summary>
  public Color HintCellBkEndColor = Color.GhostWhite;
  /// <summary>
  /// Хинт.
  /// Режим градиентной заливки.
  /// LinearGradientMode.Horizontal
  /// </summary>
  public LinearGradientMode HintCellGradientMode;
  /// <summary>
  /// Хинт.
  /// Начальный цвет градиентной заливки фона.
  /// Color.LightSkyBlue
  /// </summary>
  public Color InformationCellBkStartColor = Color.LightSkyBlue;
  /// <summary>
  /// Информационная ячейка.
  /// Конечный цвет градиентной заливки фона.
  /// Color.GhostWhite
  /// </summary>
  public Color InformationCellBkEndColor = Color.GhostWhite;
  /// <summary>
  /// Информационная ячейка.
  /// Режим градиентной заливки.
  /// LinearGradientMode.Horizontal
  /// </summary>
  public LinearGradientMode InformationCellGradientMode;
  /// <summary>
  /// Предупреждающая ячейка.
  /// Начальный цвет градиентной заливки фона.
  /// Color.NavajoWhite
  /// </summary>
  public Color WarningCellBkStartColor = Color.NavajoWhite;
  /// <summary>
  /// Предупреждающая ячейка.
  /// Конечный цвет градиентной заливки фона.
  /// Color.SeaShell
  /// </summary>
  public Color WarningCellBkEndColor = Color.SeaShell;
  /// <summary>
  /// Предупреждающая ячейка.
  /// Режим градиентной заливки.
  /// LinearGradientMode.Horizontal
  /// </summary>
  public LinearGradientMode WarningCellGradientMode;
  /// <summary>
  /// Ошибочная ячейка.
  /// Начальный цвет градиентной заливки фона.
  /// Color.Lavender
  /// </summary>
  public Color ErrorCellBkStartColor = Color.Lavender;
  /// <summary>
  /// Ошибочная ячейка.
  /// Конечный цвет градиентной заливки фона.
  /// Color.GhostWhite
  /// </summary>
  public Color ErrorCellBkEndColor = Color.GhostWhite;
  /// <summary>
  /// Ошибочная ячейка.
  /// Режим градиентной заливки.
  /// LinearGradientMode.Horizontal
  /// </summary>
  public LinearGradientMode ErrorCellGradientMode;
  /// <summary>
  /// Комбо-бокс.
  /// Начальный цвет градиентной заливки фона выделенной ячейки.
  /// Color.Lavender
  /// </summary>
  public Color ComboBoxBkStartColor = Color.DeepSkyBlue;
  /// <summary>
  /// Комбо-бокс.
  /// Конечный цвет градиентной заливки фона выделенной ячейки.
  /// Color.GhostWhite
  /// </summary>
  public Color ComboBoxBkEndColor = Color.LightSkyBlue;
  /// <summary>
  /// Комбо-бокс.
  /// Режим градиентной заливки выделенной ячейки.
  /// LinearGradientMode.Horizontal
  /// </summary>
  public LinearGradientMode ComboBoxGradientMode;
  /// <summary>градиент используется для обоих стилей</summary>
  public GradientUsing Gradient = GradientUsing.CheckOut | GradientUsing.CheckedOutOther;
}
