
// Type: Intermech.Navigator.INavGraphicsCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Drawing;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Intermech.Navigator;

/// <summary>Интерфейс кэша графических элементов для "Навигатора"</summary>
public interface INavGraphicsCache
{
  /// <summary>событие изменения цветовой схемы</summary>
  event EventHandler UIColorsSchemeChanged;

  /// <summary>набор всех схем пользователя</summary>
  AllUsersColors Schemes { get; }

  void LoadUserColorsScheme(long userID);

  void OnUserColorsSchemeChange();

  /// <summary>Очистить все кэши</summary>
  void Clear();

  /// <summary>Текущая цветовая схема "Навигатора"</summary>
  UIColorsScheme CurrentColorsScheme { get; }

  /// <summary>Вернуть градиентную кисть с указанными параметрами</summary>
  /// <param name="startColor">Начальный цвет</param>
  /// <param name="endColor">Конечный цвет</param>
  /// <param name="mode">Режим отрисовки</param>
  /// <param name="rect">Область отрисовки</param>
  /// <param name="useGradient"></param>
  /// <returns>Градиентная nкисть с указанными параметрами</returns>
  NavGradientBrush GetNavGradientBrush(
    Color startColor,
    Color endColor,
    LinearGradientMode mode,
    Rectangle rect,
    bool useGradient);

  /// <summary>Вернуть градиентную кисть с указанными параметрами</summary>
  /// <param name="startColor">Начальный цвет</param>
  /// <param name="endColor">Конечный цвет</param>
  /// <param name="mode">Режим отрисовки</param>
  /// <param name="rect">Область отрисовки</param>
  /// <returns>Градиентная nкисть с указанными параметрами</returns>
  NavGradientBrush GetNavGradientBrush(
    Color startColor,
    Color endColor,
    LinearGradientMode mode,
    Rectangle rect);
}
