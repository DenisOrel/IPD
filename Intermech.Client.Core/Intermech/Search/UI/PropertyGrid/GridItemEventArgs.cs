
// Type: Intermech.Search.UI.PropertyGrid.GridItemEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Search.UI.PropertyGrid;

public sealed class GridItemEventArgs : EventArgs
{
  public GridItemEventArgs(GridItem gridItem, int gridItemIindex)
  {
    if (gridItem == null)
      throw new ArgumentNullException(nameof (gridItem));
    if (gridItemIindex < 0)
      throw new ArgumentException();
    this.GridItem = gridItem;
    this.GridItemIndex = gridItemIindex;
  }

  public GridItem GridItem { get; private set; }

  public int GridItemIndex { get; private set; }
}
