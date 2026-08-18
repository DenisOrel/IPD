
// Type: Intermech.Search.Navigator.FindInTree.FindInTreeCommandsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Search.UI;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Intermech.Search.Navigator.FindInTree;

public sealed class FindInTreeCommandsProvider : ICommandsProvider
{
  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    CommandsInfo groupCommands = new CommandsInfo();
    if (this.CanFindInTree(items, viewServices, out IDBTypedObjectID _))
      groupCommands.Add("FindInTree", new CommandInfo(-1, new ClickEventHandler(this.FindInTree)));
    return groupCommands;
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  private bool CanFindInTree(
    ISelectedItems items,
    System.IServiceProvider serviceProvider,
    out IDBTypedObjectID typedObjectId)
  {
    return SelectedItemsHelper.TryGetSingleTypedObjectIDWithObjectVersionIDAndObjectTypeID(items, out typedObjectId) && serviceProvider.GetService(typeof (ChildrenView)) != null && serviceProvider.GetService(typeof (NavigatorTreeView)) != null;
  }

  private void FindInTree(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IDBTypedObjectID typedObjectId;
    NavigatorTreeView navigatorTreeView = this.CanFindInTree(items, viewServices, out typedObjectId) ? (NavigatorTreeView) viewServices.GetService(typeof (NavigatorTreeView)) : throw new InvalidOperationException();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ProgressDialog progressDialog = new ProgressDialog();
    progressDialog.Style = ProgressBarStyle.Marquee;
    progressDialog.LabelText = "Поиск в дереве";
    progressDialog.ButtonText = "Прервать";
    progressDialog.ButtonClick += (EventHandler) ((sender, e) => cancellationTokenSource.Cancel());
    Task.Run((Action) (() =>
    {
      NavigatorTreeNode foundNode = navigatorTreeView.RootNode.GetDescendants(true).FirstOrDefault<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (node =>
      {
        cancellationTokenSource.Token.ThrowIfCancellationRequested();
        return node.NodeID is NodeID nodeId2 && nodeId2.ObjectID == typedObjectId.ObjectID;
      }));
      if (foundNode == null)
        return;
      navigatorTreeView.Invoke((Delegate) (() =>
      {
        foundNode.Focus();
        progressDialog.Close();
      }));
    }), cancellationTokenSource.Token);
    int num = (int) progressDialog.ShowDialog();
  }
}
