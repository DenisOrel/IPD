
// Type: Intermech.Search.UI.PropertyGrid.CategoryGridItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.UI.PropertyGrid;

public sealed class CategoryGridItem : GridItem
{
  private string _name;
  private List<GridItem> _children;

  public CategoryGridItem(string name, PropertyDescriptorGridItem[] children)
  {
    this._name = name != null ? name : throw new ArgumentNullException(nameof (name));
    this._children = new List<GridItem>(children.Cast<GridItem>());
  }

  public override string Label => this._name;

  public override object Value => (object) null;

  public override List<GridItem> Children => this._children;
}
