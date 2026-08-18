
// Type: Intermech.Navigator.NavGradientBrush
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Intermech.Navigator;

/// <summary>
/// Класс, реализующий градиентную кисть для отрисовки фона
/// </summary>
[DebuggerDisplay("HitCount: {_hitCount}; StartColor: {_startColor}; EndColor: {_endColor}; Mode: {_mode}; Rect: {_rect}")]
public class NavGradientBrush : 
  IComparable,
  IComparable<NavGradientBrush>,
  IDisposable,
  ILastAccessTime
{
  /// <summary>Счётчик "попаданий" в кисть из кэша</summary>
  private long _hitCount;
  /// <summary>Время и дата последнего доступа к объекту</summary>
  private DateTime _lastAccess = DateTime.UtcNow;
  /// <summary>Градиентная кисть</summary>
  private Brush _brush;
  /// <summary>Начальный цвет</summary>
  private Color _startColor = Color.LightSkyBlue;
  /// <summary>Конечный цвет</summary>
  private Color _endColor = Color.GhostWhite;
  /// <summary>Режим отрисовки</summary>
  private LinearGradientMode _mode = LinearGradientMode.BackwardDiagonal;
  /// <summary>Область отрисовки</summary>
  private Rectangle _rect;

  /// <summary>Счётчик "попаданий" в кисть из кэша</summary>
  public long HitCount => this._hitCount;

  /// <summary>Градиентная кисть</summary>
  public Brush Brush => this._brush;

  /// <summary>Начальный цвет</summary>
  public Color StartColor => this._startColor;

  /// <summary>Конечный цвет</summary>
  public Color EndColor => this._endColor;

  /// <summary>Режим отрисовки</summary>
  public LinearGradientMode Mode => this._mode;

  /// <summary>Область отрисовки</summary>
  public Rectangle Rect => this._rect;

  /// <summary>Создать градиентную кисть с указанными параметрами</summary>
  /// <param name="startColor">Начальный цвет</param>
  /// <param name="endColor">Конечный цвет</param>
  /// <param name="mode">Режим отрисовки</param>
  /// <param name="rect">Область отрисовки</param>
  /// <param name="useGradient"></param>
  public NavGradientBrush(
    Color startColor,
    Color endColor,
    LinearGradientMode mode,
    Rectangle rect,
    bool useGradient)
  {
    this._startColor = startColor;
    this._endColor = endColor;
    this._mode = mode;
    this._rect = rect;
    this._lastAccess = DateTime.UtcNow;
    if (!useGradient || startColor == endColor || endColor == Color.Empty || rect.Width < 1 || rect.Height < 1)
      this._brush = (Brush) new SolidBrush(startColor);
    else
      this._brush = (Brush) new LinearGradientBrush(rect, startColor, endColor, mode);
  }

  /// <summary>
  /// Сравнить текущий экземпляр объекта с указанным объектом
  /// </summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    if (!(obj is NavGradientBrush navGradientBrush))
      return base.Equals(obj);
    return this._startColor == navGradientBrush._startColor && this._endColor == navGradientBrush._endColor && this._mode == navGradientBrush._mode && this._rect.Equals((object) navGradientBrush._rect) && this._lastAccess == navGradientBrush._lastAccess;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра объекта</summary>
  /// <returns>32-битный хэш-код экземпляра объекта</returns>
  public override int GetHashCode()
  {
    return this._startColor.GetHashCode() << 26 ^ this._endColor.GetHashCode() << 20 ^ this._mode.GetHashCode() << 16 /*0x10*/ ^ this._rect.GetHashCode();
  }

  /// <summary>Вернуть строковое представление экземпляра объекта</summary>
  /// <returns>Строковое представление экземпляра объекта</returns>
  public override string ToString()
  {
    return NavGradientBrush.GetHash(this._startColor, this._endColor, this._mode, this._rect);
  }

  /// <summary>Вернуть хэш-строку для словарика</summary>
  /// <param name="startColor">Начальный цвет</param>
  /// <param name="endColor">Конечный цвет</param>
  /// <param name="mode">Режим отрисовки</param>
  /// <param name="rect">Область отрисовки</param>
  /// <returns>Хэш-строка для словарика</returns>
  public static string GetHash(
    Color startColor,
    Color endColor,
    LinearGradientMode mode,
    Rectangle rect)
  {
    return $"{rect.ToString()},{startColor.ToString()},{endColor.ToString()},{mode.ToString()}";
  }

  /// <summary>Сравнить два экземпляра класса</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>-1 - экземпляр класса меньше, чем obj, 0 - равен, 1 - больше, чем obj</returns>
  public int CompareTo(object obj)
  {
    return !(obj is NavGradientBrush navGradientBrush) ? 0 : this._lastAccess.CompareTo(navGradientBrush._lastAccess);
  }

  /// <summary>Сравнить два экземпляра класса</summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>-1 - экземпляр класса меньше, чем other, 0 - равен, 1 - больше, чем other</returns>
  public int CompareTo(NavGradientBrush other)
  {
    return other == null ? 0 : this._lastAccess.CompareTo(other._lastAccess);
  }

  /// <summary>Очистить ресурсы класса</summary>
  public void Dispose() => this._brush.Dispose();

  /// <summary>Время и дата последнего доступа к объекту</summary>
  public DateTime LastAccess => this._lastAccess;

  /// <summary>
  /// Накрутить счётчик "попаданий" в кисть из кэша, обновить время и дату последнего обновления к объекту
  /// </summary>
  public void Hit()
  {
    ++this._hitCount;
    this._lastAccess = DateTime.UtcNow;
  }
}
