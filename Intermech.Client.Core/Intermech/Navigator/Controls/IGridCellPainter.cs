
// Type: Intermech.Navigator.Controls.IGridCellPainter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System.Drawing;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.Controls;

/// <summary>Интерфейс отрисовки ячеек грида</summary>
public interface IGridCellPainter : IGridCellDrawing
{
  /// <summary>
  /// Рассчитать, сколько пикселей у ячейки "отъедается" под служебные цели - под значки и прочее
  /// </summary>
  /// <param name="nodeID">Идентификатор узла</param>
  /// <param name="column">Колонка ячейки</param>
  /// <param name="cellBounds">Границы ячейки</param>
  /// <param name="cellValue">Значение ячейки</param>
  /// <param name="columns">Коллекция колонок атрибутов</param>
  /// <param name="control">Контрол, в котором происходит отрисовка</param>
  int ServiceWidth(
    INodeID nodeID,
    iGCol column,
    Rectangle cellBounds,
    object cellValue,
    NodeColumnCollection columns,
    iGrid control);

  /// <summary>Отрисовать ячейку</summary>
  /// <param name="nodeID">Идентификатор узла</param>
  /// <param name="e">Параметры отрисовки</param>
  /// <param name="columns">Коллекция колонок атрибутов</param>
  /// <param name="control">Контрол, в котором происходит отрисовка</param>
  void PaintCell(
    INodeID nodeID,
    iGCustomDrawCellEventArgs e,
    NodeColumnCollection columns,
    iGrid control);
}
