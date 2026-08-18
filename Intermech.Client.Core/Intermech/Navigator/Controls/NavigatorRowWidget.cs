
// Type: Intermech.Navigator.Controls.NavigatorRowWidget
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>Строка дерева "Навигатора"</summary>
/// <summary>Конструктор</summary>
/// <param name="panelWidget">Панель</param>
/// <param name="row">Строка</param>
public class NavigatorRowWidget(PanelWidget panelWidget, Row row) : Infralution.Controls.VirtualTree.RowWidget(panelWidget, row)
{
  /// <summary>Вернуть стиль выделенной строки</summary>
  /// <returns>стиль выделенной строки</returns>
  protected override Style GetSelectedStyle()
  {
    Style unselectedStyle = this.GetUnselectedStyle();
    Infralution.Controls.VirtualTree.VirtualTree tree = this.Tree;
    if (tree.ContainsFocus)
      return tree.RowSelectedStyle.Copy(tree.RowStyle, unselectedStyle);
    if (tree.RowSelectedUnfocusedStyle.BackColor != tree.RowSelectedUnfocusedStyle.BorderColor)
      tree.RowSelectedUnfocusedStyle.BackColor = tree.RowSelectedUnfocusedStyle.BorderColor;
    return tree.RowSelectedUnfocusedStyle.Copy(tree.RowStyle, unselectedStyle);
  }

  protected override void PaintIcon(Graphics graphics, bool printing)
  {
    base.PaintIcon(graphics, printing);
  }

  /// <summary>Нажата правая клавиша мыши в строке</summary>
  /// <param name="e">Аргументы события</param>
  protected override void OnRightMouseDown(MouseEventArgs e)
  {
  }
}
