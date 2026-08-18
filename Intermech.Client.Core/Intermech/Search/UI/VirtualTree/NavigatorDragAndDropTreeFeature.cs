
// Type: Intermech.Search.UI.VirtualTree.NavigatorDragAndDropTreeFeature
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using System;


namespace Intermech.Search.UI.VirtualTree;

public sealed class NavigatorDragAndDropTreeFeature : ITreeFeature
{
  private Intermech.Search.UI.VirtualTree.VirtualTree _tree;

  public NavigatorDragAndDropTreeFeature(Intermech.Search.UI.VirtualTree.VirtualTree tree)
  {
    if (tree == null)
      throw new ArgumentNullException();
    this._tree.GetAllowedRowDropLocations += new GetAllowedRowDropLocationsHandler(this.Tree_GetAllowedRowDropLocations);
    this._tree.GetAllowRowDrag += new GetAllowRowDragHandler(this.Tree_GetAllowRowDrag);
    this._tree.GetRowDropEffect += new GetRowDropEffectHandler(this.Tree_GetRowDropEffect);
    this._tree.RowDrop += new RowDropHandler(this.Tree_RowDrop);
  }

  private void Tree_GetAllowedRowDropLocations(object sender, GetAllowedRowDropLocationsEventArgs e)
  {
    throw new NotImplementedException();
  }

  private void Tree_GetAllowRowDrag(object sender, GetAllowRowDragEventArgs e)
  {
    throw new NotImplementedException();
  }

  private void Tree_GetRowDropEffect(object sender, GetRowDropEffectEventArgs e)
  {
    throw new NotImplementedException();
  }

  private void Tree_RowDrop(object sender, RowDropEventArgs e)
  {
    throw new NotImplementedException();
  }
}
