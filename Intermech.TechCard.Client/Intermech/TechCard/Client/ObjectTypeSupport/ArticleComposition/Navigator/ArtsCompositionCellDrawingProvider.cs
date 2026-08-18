// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator.ArtsCompositionCellDrawingProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.Navigator.Controls;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator;

/// <summary>
/// Провайдер для отображения полей закладки для состава контекстных сборочных единиц
/// </summary>
internal class ArtsCompositionCellDrawingProvider : IGridCellDrawingProvider
{
  /// <summary>
  /// 
  /// </summary>
  private IDictionary<int, IGridCellDrawing> _cellDrawings;
  /// <summary>
  /// 
  /// </summary>
  private readonly IArtsCompositionImageService _imageService;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="imageService"></param>
  internal ArtsCompositionCellDrawingProvider(IArtsCompositionImageService imageService)
  {
    this._imageService = imageService;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public IDictionary<int, IGridCellDrawing> GetCellDrawings()
  {
    if (this._cellDrawings != null)
      return this._cellDrawings;
    this._cellDrawings = (IDictionary<int, IGridCellDrawing>) new Dictionary<int, IGridCellDrawing>();
    this._cellDrawings[ArtsCompositionColumnScheme.Consts.F_ITEM_STATUS] = (IGridCellDrawing) new ArtsCompositionCellDrawingItemStatusPainter(this._imageService);
    return this._cellDrawings;
  }
}
