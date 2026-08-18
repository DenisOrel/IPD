
// Type: Intermech.Navigator.DBObjects.LC_ID_ColumnImageList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Controls;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Класс позволяет получить список изображений и индексы изображений для коллекции уровеней продвижения
/// </summary>
internal class LC_ID_ColumnImageList : IGridColumnImageList, IGridCellDrawing
{
  /// <summary>Список изображений</summary>
  internal ImageList _images;

  /// <summary>
  /// Нужно ли запрещать ячейке отрисовку текста, помимо значка
  /// </summary>
  public bool DrawOnlyIcon
  {
    get => false;
    set
    {
    }
  }

  /// <summary>Список изображений</summary>
  public ImageList ImageList
  {
    get
    {
      if (this._images == null && CacheManager.Cache("ObjectLevelIDsCache") is IObjectLevelIDsCache objectLevelIdsCache)
        this._images = objectLevelIdsCache.ImageList;
      return this._images;
    }
  }

  /// <summary>Получить индекс изображения для указанной ячейки</summary>
  /// <param name="nodeID">Идентификатор узла</param>
  /// <param name="cell">Ячейка</param>
  /// <param name="columns">Список доступных колонок</param>
  /// <param name="control">Контрол</param>
  /// <returns>-1 или индекс изображения</returns>
  public int ImageIndex(INodeID nodeID, iGCell cell, NodeColumnCollection columns, iGrid control)
  {
    string key = cell.Value != null ? cell.Value.ToString() : string.Empty;
    return key == string.Empty || this.ImageList == null ? -1 : this.ImageList.Images.IndexOfKey(key);
  }
}
