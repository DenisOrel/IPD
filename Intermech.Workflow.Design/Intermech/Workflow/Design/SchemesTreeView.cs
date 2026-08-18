// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.SchemesTreeView
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class SchemesTreeView : NavigatorTreeView
{
  public bool dontSavePath;

  public override void Build(NodeIDPath path)
  {
    this.dontSavePath = true;
    try
    {
      base.Build(path);
    }
    finally
    {
      this.dontSavePath = false;
    }
  }

  protected override void RaiseSelectedItemsChanged()
  {
    base.RaiseSelectedItemsChanged();
    if (this.dontSavePath)
      return;
    wfFunx.SaveTreePath((NavigatorTreeView) this);
  }

  protected override void TreeDragDrop(object sender, DragEventArgs e)
  {
    IDataObject data1 = e.Data;
    if (!data1.GetDataPresent(typeof (IOSource)))
      return;
    IOSource data2 = data1.GetData(typeof (IOSource)) as IOSource;
    if (!this.MayDropIOSource(data2))
      return;
    if (this.DragDropLastDestNode.Handler is SchemesRootNode)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ISelectedItems selectedItems = data2.SelectedItems;
        for (int index = 0; index < selectedItems.Count; ++index)
        {
          IDBRelationID itemData = selectedItems.GetItemData(index, typeof (IDBRelationID)) as IDBRelationID;
          sessionKeeper.Session.GetRelation(itemData.Value, false)?.Delete(0L);
        }
      }
    }
    else
    {
      string contextMenuCommand = "Cut";
      if (!this.IsNavigatorContextMenuCommandExist(data2.SelectedItems, data2.Services, contextMenuCommand))
        contextMenuCommand = "Copy";
      this.ExecuteNavigatorContextMenuCommand(data2.SelectedItems, data2.Services, contextMenuCommand);
      this.ExecuteNavigatorContextMenuCommand(this.GetDropTarget(), (System.IServiceProvider) this._services, "Paste");
    }
    if (!(data2.Control is ChildrenView))
      return;
    ((ChildrenView) data2.Control).ReloadItems();
  }
}
