
// Type: Intermech.Controls.PopupMenuBarItem
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Bars;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Controls;

/// <summary>
/// Реализует элемент меню, предназначенный для отображения в качестве контекстного меню.
/// </summary>
public class PopupMenuBarItem : ContextMenuBarItem
{
  private IPopupMenuHost popupHost;

  public override MenuButtonItem Show(Control control, Point position)
  {
    return this.Show(this.GetPopupHost(), control, position);
  }

  /// <summary>
  /// Определает хост отображения для контекстного меню. Базовая реализация используется для определения хоста отображения свойства ToolBar и PopupHost.
  /// </summary>
  /// <returns>Хост отображения для контекстного меню</returns>
  /// <exception cref="T:System.InvalidOperationException">Не удалось определить хост отображения для контекстного меню</exception>
  protected virtual IPopupMenuHost GetPopupHost()
  {
    if (this.ToolBar != null)
      return (IPopupMenuHost) this.ToolBar;
    return this.PopupHost != null ? this.PopupHost : throw new InvalidOperationException("This menu item must belong to a toolbar or a popup host to be shown in this way.");
  }

  /// <summary>
  /// Возвращает или задает хост отображения. Если это свойство не задано, то отобразить контекстное меню на экране не удастся.
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IPopupMenuHost PopupHost
  {
    get => this.popupHost;
    set => this.popupHost = value;
  }
}
