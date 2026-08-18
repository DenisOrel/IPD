
// Type: Intermech.Navigator.Controls.GridStatusesPainter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System.Drawing;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.Controls;

/// <summary>Класс по отрисовке статусов узлов</summary>
public class GridStatusesPainter : IGridCellPainter, IGridCellDrawing
{
  private INode _node;
  private const int DefaultIconSize = 16 /*0x10*/;

  /// <summary>Интерфейс, предоставляющий информацию о статусах</summary>
  private INodeStatusesInfo StatusesInfo
  {
    get
    {
      return this._node == null ? (INodeStatusesInfo) null : (INodeStatusesInfo) this._node.GetService(typeof (INodeStatusesInfo));
    }
  }

  /// <summary>Узел</summary>
  public INode Node
  {
    set => this._node = value;
  }

  /// <summary>
  /// Рассчитать, сколько пикселей у ячейки "отъедается" под служебные цели - под значки и прочее
  /// </summary>
  /// <param name="nodeID">Идентификатор узла</param>
  /// <param name="column">Колонка ячейки</param>
  /// <param name="cellBounds">Границы ячейки</param>
  /// <param name="cellValue">Значение ячейки</param>
  /// <param name="columns">Коллекция колонок атрибутов</param>
  /// <param name="control">Контрол, в котором происходит отрисовка</param>
  public int ServiceWidth(
    INodeID nodeID,
    iGCol column,
    Rectangle cellBounds,
    object cellValue,
    NodeColumnCollection columns,
    iGrid control)
  {
    int num = 0;
    if (!columns[column.Index].ID.Equals((object) "F_STATUSES") || cellValue == null || this.StatusesInfo == null)
      return num;
    Image[] icons = this.StatusesInfo.GetIcons(nodeID, cellValue);
    num = 2;
    for (int index = 0; index < icons.Length; ++index)
      num += 18;
    return num;
  }

  /// <summary>Отрисовать ячейку</summary>
  /// <param name="nodeID">Идентификатор узла</param>
  /// <param name="e">Параметры отрисовки</param>
  /// <param name="columns">Коллекция колонок атрибутов</param>
  /// <param name="grid">Элемент управления, в котором отрисовываются ячейки</param>
  public void PaintCell(
    INodeID nodeID,
    iGCustomDrawCellEventArgs e,
    NodeColumnCollection columns,
    iGrid grid)
  {
    iGCell cell = grid.Cells[e.RowIndex, e.ColIndex];
    object columnValue = cell?.Value;
    if (!(cell.Col.Tag is NodeColumn tag) || !tag.ID.Equals((object) "F_STATUSES") || columnValue == null || this.StatusesInfo == null)
      return;
    Image[] icons = this.StatusesInfo.GetIcons(nodeID, columnValue);
    int num = 2;
    for (int index = 0; index < icons.Length; ++index)
    {
      e.Graphics.DrawImage(icons[index], e.Bounds.X + num, e.Bounds.Y + 1, 16 /*0x10*/, 16 /*0x10*/);
      num += 18;
    }
  }
}
