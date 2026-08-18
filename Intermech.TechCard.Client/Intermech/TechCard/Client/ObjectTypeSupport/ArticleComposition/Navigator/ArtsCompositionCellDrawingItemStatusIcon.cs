// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator.ArtsCompositionCellDrawingItemStatusIcon
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.Navigator.Controls;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Data;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator;

/// <summary>
/// 
/// </summary>
internal class ArtsCompositionCellDrawingItemStatusIcon : IGridColumnImageList, IGridCellDrawing
{
  /// <summary>
  /// 
  /// </summary>
  private readonly IArtsCompositionImageService _imageService;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="imageService"></param>
  internal ArtsCompositionCellDrawingItemStatusIcon(IArtsCompositionImageService imageService)
  {
    this._imageService = imageService;
  }

  /// <summary>
  /// Нужно ли запрещать ячейке отрисовку текста, помимо значка
  /// </summary>
  public bool DrawOnlyIcon
  {
    get => true;
    set
    {
    }
  }

  /// <summary>Список изображений</summary>
  public ImageList ImageList => this._imageService?.ImageList;

  /// <summary>Получить индекс изображения для указанной ячейки</summary>
  /// <param name="nodeId">Идентификатор узла</param>
  /// <param name="cell">Ячейка</param>
  /// <param name="columns">Список доступных колонок</param>
  /// <param name="control">Контрол</param>
  /// <returns>-1 или индекс изображения</returns>
  public int ImageIndex(INodeID nodeId, iGCell cell, NodeColumnCollection columns, iGrid control)
  {
    if (cell == null)
      return -1;
    int num = 0;
    if (!columns[cell.ColIndex].ID.Equals((object) ArtsCompositionColumnScheme.Consts.F_ITEM_STATUS))
      return num;
    object obj;
    if (!(cell.Value is NodeDelayedValue nodeDelayedValue) || nodeDelayedValue.Value == null || !((obj = nodeDelayedValue.Value) is ArtsCompositionItemStatus))
      return -1;
    ArtsCompositionItemStatus status = (ArtsCompositionItemStatus) obj;
    IArtsCompositionImageService imageService = this._imageService;
    return imageService == null ? -1 : imageService.ImageIndex(status);
  }
}
