// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Interfaces.WindowMode
// Assembly: Intermech.TechAcad.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 512FF008-192B-42A6-A8D1-B0B0A687059D
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.TechAcad.Interfaces.xml

#nullable disable
namespace Intermech.TechAcad.Interfaces;

/// <summary>Режим отображения окна</summary>
/// <remarks>Аналог SW_Mode in ShowWindow</remarks>
/// &gt;
public enum WindowMode
{
  /// <summary>Hides the window and activates another window.</summary>
  Hide,
  /// <summary>
  /// Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time.
  /// </summary>
  ShowNormal,
  /// <summary>
  /// Activates the window and displays it as a minimized window.
  /// </summary>
  ShowMinimized,
  /// <summary>Maximizes the specified window.</summary>
  Maximize,
  /// <summary>
  /// Displays a window in its most recent size and position. This value is similar to SW_SHOWNORMAL, except that the window is not activated.
  /// </summary>
  ShowNoActivate,
  /// <summary>
  /// Activates the window and displays it in its current size and position
  /// </summary>
  Show,
  /// <summary>
  /// Minimizes the specified window and activates the next top-level window in the Z order.
  /// </summary>
  Minimize,
  /// <summary>
  /// Displays the window as a minimized window. This value is similar to SW_SHOWMINIMIZED, except the window is not activated.
  /// </summary>
  ShowMinNoActive,
  /// <summary>
  /// Displays the window in its current size and position. This value is similar to SW_SHOW, except that the window is not activated.
  /// </summary>
  ShowNa,
  /// <summary>
  /// Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when restoring a minimized window.
  /// </summary>
  Restore,
  /// <summary>
  /// Sets the show state based on the SW_ value specified in the STARTUPINFO structure passed to the CreateProcess function by the program that started the application.
  /// </summary>
  ShowDefault,
  /// <summary>
  /// Minimizes a window, even if the thread that owns the window is not responding. This flag should only be used when minimizing windows from a different thread.
  /// </summary>
  ForceMinimize,
}
