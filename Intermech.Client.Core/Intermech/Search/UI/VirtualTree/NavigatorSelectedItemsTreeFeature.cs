
// Type: Intermech.Search.UI.VirtualTree.NavigatorSelectedItemsTreeFeature
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Search.UI.VirtualTree;

public sealed class NavigatorSelectedItemsTreeFeature : ITreeFeature
{
  private Intermech.Search.UI.VirtualTree.VirtualTree _tree;

  public NavigatorSelectedItemsTreeFeature(Intermech.Search.UI.VirtualTree.VirtualTree tree)
  {
    this._tree = tree != null ? tree : throw new ArgumentNullException(nameof (tree));
    this._tree.Enter += new EventHandler(this.Tree_Enter);
    this.SelectedItems = (ISelectedItems) new TreeSelectedItems(this._tree);
  }

  public ISelectedItems SelectedItems { get; private set; }

  public bool AddSelectedItemsToGlobalServiceContainerOnFocus { get; set; }

  private void Tree_Enter(object sender, EventArgs e)
  {
    if (!this.AddSelectedItemsToGlobalServiceContainerOnFocus)
      return;
    ServicesManager.RemoveService(typeof (ISimpleSelectedItems));
    ServicesManager.AddService(typeof (ISimpleSelectedItems), (object) this.SelectedItems);
    ServicesManager.RemoveService(typeof (ISelectedItems));
    ServicesManager.AddService(typeof (ISelectedItems), (object) this.SelectedItems);
  }
}
