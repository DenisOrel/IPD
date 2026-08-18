
// Type: Intermech.Navigator.NavGraphicsCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Drawing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Intermech.Navigator;

/// <summary>
/// Класс, реализующий кэш графических элементов для "Навигатора"
/// </summary>
public class NavGraphicsCache : INavGraphicsCache, IDisposable
{
  /// <summary>Оптимально допустимое количество кистей в кэше</summary>
  public static int OptimalBrushes = 2500;
  /// <summary>
  /// % удаляемых старых кистей при сборке мусора в кэше кистей
  /// </summary>
  public static byte BrushesToRemove = 30;
  /// <summary>Хэш градиентных кистей</summary>
  private Dictionary<string, NavGradientBrush> _gradientBrushes = new Dictionary<string, NavGradientBrush>();
  /// <summary>Цветовая схема "Навигатора" по умолчанию</summary>
  private UIColorsScheme _defColorsScheme = new UIColorsScheme();
  /// <summary>все схемы пользователя</summary>
  private AllUsersColors schemes;

  public event EventHandler UIColorsSchemeChanged;

  public AllUsersColors Schemes => this.schemes;

  /// <summary>Очистить все кэши</summary>
  public virtual void Clear()
  {
    foreach (IDisposable disposable in this._gradientBrushes.Values)
      disposable.Dispose();
    this._gradientBrushes.Clear();
  }

  /// <summary>Удалить из кэша кистей лишние элементы</summary>
  public virtual void PurgeBrushesCache()
  {
    if (this._gradientBrushes.Count < NavGraphicsCache.OptimalBrushes)
      return;
    ArrayList arrayList = new ArrayList(this._gradientBrushes.Count);
    foreach (NavGradientBrush navGradientBrush in this._gradientBrushes.Values)
      arrayList.Add((object) navGradientBrush);
    arrayList.Sort();
    for (int index = arrayList.Count / 100 * (int) NavGraphicsCache.BrushesToRemove - 1; index >= 0; --index)
    {
      NavGradientBrush navGradientBrush = arrayList[index] as NavGradientBrush;
      arrayList.Remove((object) navGradientBrush);
      this._gradientBrushes.Remove(NavGradientBrush.GetHash(navGradientBrush.StartColor, navGradientBrush.EndColor, navGradientBrush.Mode, navGradientBrush.Rect));
      navGradientBrush.Dispose();
    }
  }

  /// <summary>Текущая цветовая схема "Навигатора"</summary>
  public UIColorsScheme CurrentColorsScheme
  {
    get => this.schemes == null ? this._defColorsScheme : this.schemes.CurrentColorsScheme.Scheme;
  }

  /// <summary>Вернуть градиентную кисть с указанными параметрами</summary>
  /// <param name="startColor">Начальный цвет</param>
  /// <param name="endColor">Конечный цвет</param>
  /// <param name="mode">Режим отрисовки</param>
  /// <param name="rect">Область отрисовки</param>
  /// <param name="useGradient"></param>
  /// <returns>Градиентная nкисть с указанными параметрами</returns>
  public NavGradientBrush GetNavGradientBrush(
    Color startColor,
    Color endColor,
    LinearGradientMode mode,
    Rectangle rect,
    bool useGradient)
  {
    return new NavGradientBrush(startColor, endColor, mode, rect, useGradient);
  }

  /// <summary>Вернуть градиентную кисть с указанными параметрами</summary>
  /// <param name="startColor">Начальный цвет</param>
  /// <param name="endColor">Конечный цвет</param>
  /// <param name="mode">Режим отрисовки</param>
  /// <param name="rect">Область отрисовки</param>
  /// <returns>Градиентная nкисть с указанными параметрами</returns>
  public NavGradientBrush GetNavGradientBrush(
    Color startColor,
    Color endColor,
    LinearGradientMode mode,
    Rectangle rect)
  {
    return this.GetNavGradientBrush(startColor, endColor, mode, rect, false);
  }

  /// <summary>Очистить ресурсы класса</summary>
  public void Dispose() => this.Clear();

  public void LoadUserColorsScheme(long userID)
  {
    this.schemes = new AllUsersColors();
    this.schemes.LoadFromUserSettings(userID);
  }

  public void OnUserColorsSchemeChange()
  {
    if (this.UIColorsSchemeChanged == null)
      return;
    this.UIColorsSchemeChanged((object) null, (EventArgs) null);
  }
}
