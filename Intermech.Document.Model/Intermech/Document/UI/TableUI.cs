// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.TableUI
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>ИП таблицы</summary>
public class TableUI : RectanglePageElementUI
{
  /// <summary>Получить PageElementUI под заданной точкой</summary>
  /// <param name="point">Точка</param>
  /// <param name="layer">Слой на котором находится найденный PageElementUI</param>
  /// <param name="recursive">Опрашивать все дочерние PageElementUI</param>
  /// <returns>Найденный PageElementUI</returns>
  public override PageElementUI GetPageElementUIAtPoint(
    Point point,
    ref int layer,
    bool recursive,
    bool ignoreGrabHandle)
  {
    if (this.element != null && !this.element.IsVisibleNow)
      return (PageElementUI) null;
    PageElementUI elementUiAtPoint = base.GetPageElementUIAtPoint(point, ref layer, recursive, ignoreGrabHandle);
    int num = 0;
    if (this.DocumentControl != null && this.DocumentControl.NodeInSelection((DocumentTreeNode) this.element))
      num = 20;
    if (this.HasFixedStructureParent())
      num += 10;
    if (layer < num + 4 && this.TopRegion1.Contains(point))
    {
      layer = num + 4;
      elementUiAtPoint = (PageElementUI) this;
    }
    else if (layer < num + 2 && this.TopRegion2.Contains(point))
    {
      layer = num + 2;
      elementUiAtPoint = (PageElementUI) this;
    }
    return elementUiAtPoint;
  }

  internal override void OnMouseUp(MouseEventArgs e)
  {
    if (this.PageControl == null)
      return;
    Point point = new Point(e.X, e.Y);
    if (e.Button == MouseButtons.Left)
    {
      this.leftMouseDownPos = point;
      if (this.IsMoving)
      {
        Point delta = new Point(e.X - this.startPoint.X, e.Y - this.startPoint.Y);
        this.EndMoving(e, Control.ModifierKeys, this.startPoint, delta);
      }
      else if (!this.PageControl.IsTableCellsSelecting && !this.PageControl.IsTableColumnsSelecting && !this.PageControl.IsTableRowsSelecting)
      {
        PageElementUI elementUiAtPoint = this.PageControl.GetPageElementUIAtPoint(new Point(e.X, e.Y), true);
        if (elementUiAtPoint != null && elementUiAtPoint != this)
          elementUiAtPoint.OnMouseUp(e);
      }
    }
    if (this.DocumentControl != null && this.DocumentControl.DocumentManager != null)
      this.DocumentControl.DocumentManager.SetMessageText("");
    base.OnMouseUp(e);
  }

  /// <summary>Область выделения всей таблицы, должна быть над всеми ячейками</summary>
  public Rectangle TopRegion1
  {
    [DebuggerStepThrough] get
    {
      return this.Bounds with { Height = 2 };
    }
  }

  /// <summary>Область выделения всей таблицы, может закрываться ячейками</summary>
  public Rectangle TopRegion2
  {
    [DebuggerStepThrough] get
    {
      Rectangle bounds = this.Bounds;
      bounds.Location = new Point(bounds.X, bounds.Y + 2);
      bounds.Height = 2;
      return bounds;
    }
  }
}
