
// Type: Intermech.Search.UI.PropertyGrid.GridItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;
using System.Drawing;


namespace Intermech.Search.UI.PropertyGrid;

public abstract class GridItem
{
  public abstract string Label { get; }

  public abstract object Value { get; }

  public abstract List<GridItem> Children { get; }

  public virtual Color BackColor => Color.Empty;
}
