
// Type: Intermech.Navigator.Controls.ContextMenuEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Аргументы для обработчика событий, связанного с показом пользовательского контекстного меню в гриде
/// </summary>
public class ContextMenuEventArgs : EventArgs
{
  /// <summary>Координаты в контроле</summary>
  private Point _location;
  /// <summary>Контрол для отображения контекстного меню</summary>
  private Control _control;

  /// <summary>Координаты в контроле</summary>
  public Point Location
  {
    [DebuggerStepThrough] get => this._location;
  }

  /// <summary>Контрол для отображения контекстного меню</summary>
  public Control Control
  {
    [DebuggerStepThrough] get => this._control;
  }

  /// <summary>Создать аргументы</summary>
  /// <param name="control">Грид</param>
  /// <param name="location">Координаты, в которых можно показывать контекстное меню</param>
  public ContextMenuEventArgs(Point location, Control control)
  {
    this._location = location;
    this._control = control;
  }
}
