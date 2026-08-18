// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator.ArtsCompositionCellDrawingItemStatusPainter
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.Navigator.Controls;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Data;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params;
using System.Drawing;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator;

/// <summary>Класс по отображения содержимого статусов узлов</summary>
internal class ArtsCompositionCellDrawingItemStatusPainter : IGridCellPainter, IGridCellDrawing
{
  /// <summary>
  /// 
  /// </summary>
  private const int DefaultIconSize = 16 /*0x10*/;
  /// <summary>
  /// </summary>
  private readonly IArtsCompositionImageService _imageService;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="imageService"></param>
  internal ArtsCompositionCellDrawingItemStatusPainter(IArtsCompositionImageService imageService)
  {
    this._imageService = imageService;
  }

  /// <summary>
  /// Рассчитать, сколько пикселей у ячейки "отъедается" под служебные цели - под значки и прочее
  /// </summary>
  /// <param name="nodeId">Идентификатор узла</param>
  /// <param name="column">Колонка ячейки</param>
  /// <param name="cellBounds">Границы ячейки</param>
  /// <param name="cellValue">Значение ячейки</param>
  /// <param name="columns">Коллекция колонок атрибутов</param>
  /// <param name="control">Контрол, в котором происходит отображение</param>
  public int ServiceWidth(
    INodeID nodeId,
    iGCol column,
    Rectangle cellBounds,
    object cellValue,
    NodeColumnCollection columns,
    iGrid control)
  {
    int num1 = 0;
    object obj;
    if (!columns[column.Index].ID.Equals((object) ArtsCompositionColumnScheme.Consts.F_ITEM_STATUS) || !(cellValue is NodeDelayedValue nodeDelayedValue) || nodeDelayedValue.Value == null || !((obj = nodeDelayedValue.Value) is ArtsCompositionItemStatus))
      return num1;
    ArtsCompositionItemStatus status = (ArtsCompositionItemStatus) obj;
    Image[] imageArray = (Image[]) null;
    int index1 = this._imageService.ImageIndex(status);
    if (index1 != -1)
      imageArray = new Image[1]
      {
        this._imageService.ImageList.Images[index1]
      };
    if (imageArray == null)
      return num1;
    int num2 = 2;
    for (int index2 = 0; index2 < imageArray.Length; ++index2)
      num2 += 18;
    return num2;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeId">Идентификатор узла</param>
  /// <param name="e">Параметры отображения</param>
  /// <param name="columns">Коллекция колонок атрибутов</param>
  /// <param name="grid">Элемент управления, в котором отображается ячейки</param>
  public void PaintCell(
    INodeID nodeId,
    iGCustomDrawCellEventArgs e,
    NodeColumnCollection columns,
    iGrid grid)
  {
    iGCell cell = grid.Cells[e.RowIndex, e.ColIndex];
    if (cell == null)
      return;
    object obj1 = cell.Value;
    object obj2;
    if (!(cell.Col.Tag is NodeColumn tag) || !tag.ID.Equals((object) ArtsCompositionColumnScheme.Consts.F_ITEM_STATUS) || !(obj1 is NodeDelayedValue nodeDelayedValue) || nodeDelayedValue.Value == null || !((obj2 = nodeDelayedValue.Value) is ArtsCompositionItemStatus))
      return;
    ArtsCompositionItemStatus status = (ArtsCompositionItemStatus) obj2;
    Image[] imageArray = (Image[]) null;
    int index = this._imageService.ImageIndex(status);
    if (index != -1)
      imageArray = new Image[1]
      {
        this._imageService.ImageList.Images[index]
      };
    if (imageArray == null)
      return;
    int num = 2;
    foreach (Image image in imageArray)
    {
      try
      {
        e.Graphics.DrawImage(image, e.Bounds.X + num, e.Bounds.Y + 1, 16 /*0x10*/, 16 /*0x10*/);
      }
      finally
      {
        image?.Dispose();
      }
      num += 18;
    }
  }
}
