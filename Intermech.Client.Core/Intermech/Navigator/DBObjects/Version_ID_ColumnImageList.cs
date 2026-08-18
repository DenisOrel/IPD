
// Type: Intermech.Navigator.DBObjects.Version_ID_ColumnImageList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Класс позволяет получить список изображений и индексы изображений для версий объектов
/// </summary>
internal class Version_ID_ColumnImageList : IGridColumnImageList, IGridCellDrawing
{
  /// <summary>Индекс изображения "imgBaseVersion"</summary>
  internal static int _imgBaseVersion = -1;
  /// <summary>Индекс изображения "imgNonBaseVersion"</summary>
  internal static int _imgNonBaseVersion = -1;
  /// <summary>Индекс изображения "BaseVersionEmpty"</summary>
  internal static int _imgBaseVersionEmpty = -1;

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
    get => ((INamedImageList) ServicesManager.GetService(typeof (INamedImageList))).ImageList;
  }

  /// <summary>Получить индекс изображения для указанной ячейки</summary>
  /// <param name="nodeID">Идентификатор узла</param>
  /// <param name="cell">Ячейка</param>
  /// <param name="columns">Список доступных колонок</param>
  /// <param name="control">Контрол</param>
  /// <returns>-1 или индекс изображения</returns>
  public int ImageIndex(INodeID nodeID, iGCell cell, NodeColumnCollection columns, iGrid control)
  {
    if (Version_ID_ColumnImageList._imgBaseVersion < 0)
    {
      INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
      Version_ID_ColumnImageList._imgNonBaseVersion = service.ImageIndex("imgNonBaseVersion");
      Version_ID_ColumnImageList._imgBaseVersion = service.ImageIndex("imgBaseVersion");
      Version_ID_ColumnImageList._imgBaseVersionEmpty = service.ImageIndex("imgBaseVersionEmpty");
    }
    if (UISettings.NavigatorWindowBaseVersionsMode == NavigatorWindowBaseVersionsMode.Hidden || !(nodeID is NodeID nodeId))
      return -1;
    return (nodeId.BaseVersion & 1L) == 0L ? ((UISettings.NavigatorWindowBaseVersionsMode & NavigatorWindowBaseVersionsMode.ShowOtherVersions) == NavigatorWindowBaseVersionsMode.ShowOtherVersions ? Version_ID_ColumnImageList._imgNonBaseVersion : Version_ID_ColumnImageList._imgBaseVersionEmpty) : ((UISettings.NavigatorWindowBaseVersionsMode & NavigatorWindowBaseVersionsMode.ShowBaseVersions) == NavigatorWindowBaseVersionsMode.ShowBaseVersions ? Version_ID_ColumnImageList._imgBaseVersion : Version_ID_ColumnImageList._imgBaseVersionEmpty);
  }
}
