
// Type: Intermech.Search.UI.PropertyGrid.PropertyGridTreeView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using System;


namespace Intermech.Search.UI.PropertyGrid;

public sealed class PropertyGridTreeView : Infralution.Controls.VirtualTree.VirtualTree
{
  public event EventHandler TopRowChanged;

  public override Row TopRow
  {
    get => base.TopRow;
    set
    {
      base.TopRow = value;
      this.OnTopRowChanged();
    }
  }

  protected override void BindDataSource()
  {
    base.BindDataSource();
    this.SelectedRow = (Row) null;
  }

  private void OnTopRowChanged()
  {
    EventHandler topRowChanged = this.TopRowChanged;
    if (topRowChanged == null)
      return;
    topRowChanged((object) this, new EventArgs());
  }
}
