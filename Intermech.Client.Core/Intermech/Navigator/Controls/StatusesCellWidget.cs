
// Type: Intermech.Navigator.Controls.StatusesCellWidget
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Navigator.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>Ячейка дерева "Навигатора" со статусами элементов</summary>
/// <summary>Конструктор</summary>
/// <param name="rowWidget">Строка</param>
/// <param name="column">Колонка</param>
public class StatusesCellWidget(Infralution.Controls.VirtualTree.RowWidget rowWidget, Column column) : 
  NavigatorCellWidget(rowWidget, column)
{
  /// <summary>Предыдущий хинт</summary>
  private string prevHint = string.Empty;
  protected const int DefaultIconSize = 16 /*0x10*/;

  /// <summary>Вернуть подсказку для ячейки</summary>
  /// <param name="x">Позиция x курсора мышки</param>
  /// <param name="y">Позиция y курсора мышки</param>
  /// <returns>Подсказка для ячейки</returns>
  protected virtual string GetToolTipText(int x, int y)
  {
    NavigatorTreeView tree = this.Tree as NavigatorTreeView;
    NavigatorTreeNode node = this.Row != null ? this.Row.Item as NavigatorTreeNode : (NavigatorTreeNode) null;
    if (tree == null || node == null || node.NodeID == null || this.CellData.Value == null)
      return string.Empty;
    Rectangle bounds = this.Bounds;
    int num1 = (bounds.Height - 16 /*0x10*/) / 2;
    int num2 = y;
    bounds = this.Bounds;
    int num3 = bounds.Y + num1;
    if (num2 >= num3)
    {
      int num4 = y;
      bounds = this.Bounds;
      int y1 = bounds.Y;
      bounds = this.Bounds;
      int height = bounds.Height;
      int num5 = y1 + height - num1;
      if (num4 <= num5)
      {
        int num6 = x;
        bounds = this.Bounds;
        int right = bounds.Right;
        if (num6 < right)
        {
          int num7 = x;
          bounds = this.Bounds;
          int x1 = bounds.X;
          int num8 = num7 - x1 - 2;
          int iconIndex = num8 / 16 /*0x10*/;
          if (num8 >= iconIndex * 16 /*0x10*/ + 2)
          {
            INode nodeHandler = tree.GetNodeHandler(node);
            System.IServiceProvider services = nodeHandler is IContextAware contextAware ? contextAware.Services : (System.IServiceProvider) null;
            INodeStatusesInfo service = (INodeStatusesInfo) nodeHandler.GetService(typeof (INodeStatusesInfo));
            return service == null ? string.Empty : service.GetDescription(services, node.NodeID, this.CellData.Value, iconIndex);
          }
        }
      }
    }
    return string.Empty;
  }

  /// <summary>Курсор мышки попал в ячейку</summary>
  /// <param name="e">Аргументы события</param>
  public override void OnMouseEnter(MouseEventArgs e)
  {
    string toolTipText = this.GetToolTipText(e.X, e.Y);
    if (toolTipText != null)
    {
      if (!(this.prevHint != toolTipText))
        return;
      this.Tree.ShowToolTip(toolTipText);
      this.prevHint = toolTipText;
    }
    else
    {
      this.prevHint = string.Empty;
      this.Tree.HideToolTip();
    }
  }

  /// <summary>Курсор мышки передвинулся в ячейке</summary>
  /// <param name="e">Аргументы события</param>
  public override void OnMouseMove(MouseEventArgs e)
  {
    base.OnMouseMove(e);
    string toolTipText = this.GetToolTipText(e.X, e.Y);
    if (toolTipText != null)
    {
      if (!(toolTipText != this.prevHint))
        return;
      this.Tree.ShowToolTip(toolTipText);
      this.prevHint = toolTipText;
    }
    else
    {
      this.Tree.ShowToolTip(string.Empty);
      this.prevHint = string.Empty;
    }
  }

  /// <summary>Курсор мыши покинул ячейку</summary>
  /// <param name="e">Аргументы события</param>
  public override void OnMouseLeave(EventArgs e)
  {
    this.Tree.ShowToolTip(string.Empty);
    this.Tree.HideToolTip();
    this.prevHint = string.Empty;
    base.OnMouseLeave(e);
  }

  /// <summary>Отрисовать текст в ячейке</summary>
  /// <param name="graphics">Контекст рисования</param>
  /// <param name="style">Стиль ячейки</param>
  /// <param name="printing">Идёт ли вывод на печать</param>
  protected override void PaintForeground(Graphics graphics, Style style, bool printing)
  {
    if (this.CellData.Value == null || this.CellData.Value == DBNull.Value || !(this.CellData.Value.GetType() == typeof (byte[])))
      return;
    NavigatorTreeView tree = this.Tree as NavigatorTreeView;
    NavigatorTreeNode node = this.Row != null ? this.Row.Item as NavigatorTreeNode : (NavigatorTreeNode) null;
    if (tree == null || node == null || node.NodeID == null)
      return;
    Image[] icons = ((INodeStatusesInfo) tree.GetNodeHandler(node).GetService(typeof (INodeStatusesInfo)))?.GetIcons(node.NodeID, this.CellData.Value);
    if (icons == null)
      return;
    int num1 = 2;
    int num2 = (this.Bounds.Height - 16 /*0x10*/) / 2;
    foreach (Image image1 in icons)
    {
      Rectangle bounds = this.Bounds;
      int num3 = bounds.X + num1 + 16 /*0x10*/;
      bounds = this.Bounds;
      int right = bounds.Right;
      if (num3 > right)
        break;
      Graphics graphics1 = graphics;
      Image image2 = image1;
      bounds = this.Bounds;
      int x = bounds.X + num1;
      bounds = this.Bounds;
      int y = bounds.Y + num2;
      graphics1.DrawImage(image2, x, y, 16 /*0x10*/, 16 /*0x10*/);
      num1 += 18;
    }
  }
}
