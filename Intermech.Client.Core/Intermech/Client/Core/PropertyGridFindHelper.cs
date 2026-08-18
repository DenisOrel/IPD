
// Type: Intermech.Client.Core.PropertyGridFindHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.PropertyEditors;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary> Класс-помошник для работы с Property grid-ом </summary>
public static class PropertyGridFindHelper
{
  /// <summary> Активировать атрибут с переданым идентификатором </summary>
  /// <param name="grid"> PropertyGrid, в котором необходимо активировать атрибут </param>
  /// <param name="attributeID"> Идентификатор атрибута </param>
  public static GridItem SelectAttributeInObjectGrid(ObjectPropertyGrid grid, int attributeID)
  {
    if (grid == null || attributeID == -1)
      return (GridItem) null;
    GridItem recurceGridItem = grid.SelectedGridItem;
    if (recurceGridItem == null)
      return (GridItem) null;
    while (recurceGridItem.Parent != null && recurceGridItem.GridItemType != GridItemType.Root)
      recurceGridItem = recurceGridItem.Parent;
    if (recurceGridItem == null)
      return (GridItem) null;
    GridItem gridItem = PropertyGridFindHelper.RecurceSearchGridItem(recurceGridItem, attributeID);
    if (gridItem != null)
      grid.SelectedGridItem = gridItem;
    return gridItem;
  }

  /// <summary> Рекурсивный поиск GridItem-а </summary>
  private static GridItem RecurceSearchGridItem(GridItem recurceGridItem, int attributeID)
  {
    if (ObjectPropertyGrid.GetAttributeIDbyGridItem(recurceGridItem) == attributeID)
      return recurceGridItem;
    GridItem gridItem1 = (GridItem) null;
    foreach (GridItem gridItem2 in recurceGridItem.GridItems)
    {
      if (ObjectPropertyGrid.GetAttributeIDbyGridItem(recurceGridItem) == attributeID)
        return gridItem2;
      gridItem1 = PropertyGridFindHelper.RecurceSearchGridItem(gridItem2, attributeID);
      if (gridItem1 != null)
        break;
    }
    return gridItem1;
  }
}
