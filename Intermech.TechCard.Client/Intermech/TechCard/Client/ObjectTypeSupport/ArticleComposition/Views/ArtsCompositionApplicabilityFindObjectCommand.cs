// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Views.ArtsCompositionApplicabilityFindObjectCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Commands;
using Intermech.DataFormats;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Navigator.Controls;
using Intermech.TechCard.Client.Classes.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Views;

internal class ArtsCompositionApplicabilityFindObjectCommand : ExtendedSelectedItemsCommand
{
  /// <summary>
  /// 
  /// </summary>
  private readonly NavigatorTreeNode _currentNavigatorTreeNode;
  /// <summary>
  /// 
  /// </summary>
  private AsyncTask<long, NavigatorTreeNode> _searchTask;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="task"></param>
  /// <param name="objectId"></param>
  /// <returns></returns>
  private NavigatorTreeNode TaskFunction(AsyncTaskBase<long, NavigatorTreeNode> task, long objectId)
  {
    foreach (NavigatorTreeNode nextNode in this.GetNextNodes(this._currentNavigatorTreeNode, true))
    {
      if (nextNode != null && nextNode.NodeID.GetObjVerID() == objectId)
        return nextNode;
    }
    return (NavigatorTreeNode) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="node"></param>
  /// <param name="findInNode"></param>
  /// <returns></returns>
  private IEnumerable<NavigatorTreeNode> GetNextNodes(NavigatorTreeNode node, bool findInNode)
  {
    foreach (NavigatorTreeNode descendant in node.GetDescendants(true, (Predicate<NavigatorTreeNode>) (o => true)))
      yield return descendant;
    if (!findInNode)
    {
      NavigatorTreeNode ancestorNextSibling = node.GetNextSiblingOrAncestorNextSibling();
      if (ancestorNextSibling != null)
      {
        foreach (NavigatorTreeNode nextNode in ancestorNextSibling.GetAllNextAndSelf(true, (Predicate<NavigatorTreeNode>) (o => true)))
          yield return nextNode;
      }
    }
  }

  private void ShowNode(NavigatorTreeNode node)
  {
    foreach (NavigatorTreeNode navigatorTreeNode in node.GetAncestors().Reverse<NavigatorTreeNode>())
    {
      navigatorTreeNode.Handle.EnsureVisible();
      navigatorTreeNode.Handle.Expand();
    }
    node.Tree.FocusedNode = node;
  }

  /// <summary>
  /// 
  /// </summary>
  public ArtsCompositionApplicabilityFindObjectCommand(NavigatorTreeNode currentNavigatorTreeNode)
    : base("FindTechObject")
  {
    this._currentNavigatorTreeNode = currentNavigatorTreeNode;
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoExecute()
  {
    if (this.Items.Count == 0)
      return;
    IDBObjectID itemData = (IDBObjectID) this.Items.GetItemData(0, typeof (IDBObjectID));
    if (itemData == null)
      return;
    this._searchTask = new AsyncTask<long, NavigatorTreeNode>((IAsyncTaskAction<long, NavigatorTreeNode>) new AsyncTaskAction<long, NavigatorTreeNode>(new Func<AsyncTaskBase<long, NavigatorTreeNode>, long, NavigatorTreeNode>(this.TaskFunction)), SynchronizationContext.Current);
    this._searchTask.HandleException += new ExceptionHandler(this.OnTaskHandleException);
    this._searchTask.TaskCompleted += new AsyncTaskBase<long, NavigatorTreeNode>.TaskCompletedEventHandler(this.OnTaskCompleted);
    this._searchTask.Execute(itemData.Value);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="args"></param>
  private void OnTaskCompleted(
    object sender,
    AsyncTaskBase<long, NavigatorTreeNode>.TaskCompleteEventArgs args)
  {
    NavigatorTreeNode result = args?.Result;
    if (result == null)
      return;
    this.ShowNode(result);
    Form openForm = Application.OpenForms[Application.OpenForms.Count - 1];
    if (!openForm.Modal)
      return;
    INavigatorTreeViewContextMenuHelper service = ServiceUtils.GetService<INavigatorTreeViewContextMenuHelper>((object) result.Tree.Services, false);
    if (service != null)
      service.CanRestoreFocusedNode = false;
    openForm.DialogResult = DialogResult.Cancel;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnTaskHandleException(object sender, ExceptionEventArgs e)
  {
    ExceptionHelper.ExceptionService.ShowException(e.Exception);
  }
}
