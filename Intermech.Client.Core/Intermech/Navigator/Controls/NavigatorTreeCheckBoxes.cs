
// Type: Intermech.Navigator.Controls.NavigatorTreeCheckBoxes
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>Helper класс, позволяющий сделать так, чтобы ноды у дерева были с возможностью простановки галок</summary>
public class NavigatorTreeCheckBoxes : IDisposable
{
  /// <summary>Дерево навигатора, функциональность которого расширяем</summary>
  private NavigatorTreeView _treeView;

  /// <summary>Конструктор</summary>
  /// <param name="treeView">Дерево, которое должно</param>
  public NavigatorTreeCheckBoxes(NavigatorTreeView treeView)
  {
    Intermech.Diagnostics.Check.ArgumentNotNull<NavigatorTreeView>(treeView, nameof (treeView));
    Intermech.Diagnostics.Check.Assert(treeView.BeforeSetCheckState == null, "CheckableNavTreeViewHelper допускается применять только к деревьям, у которых не установлен делегат BeforeSetCheckState");
    this._treeView = treeView;
    lock (this._treeView)
    {
      this._treeView.AllowCheckParentWithoutChildren = true;
      this._treeView.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.ThreeState;
      this._treeView.PlusJobCompleted += new PlusJobCompletedEventHandler(this._treeView_PlusJobCompleted);
      this._treeView.AfterPopulateNode += new EventHandler<NodeEventArgs>(this._treeView_AfterPopulateNode);
    }
  }

  /// <summary>Разрыв связи с деревом</summary>
  protected void ClearLinksWithTree()
  {
    if (this._treeView == null)
      return;
    this._treeView.AfterPopulateNode -= new EventHandler<NodeEventArgs>(this._treeView_AfterPopulateNode);
    this._treeView.PlusJobCompleted -= new PlusJobCompletedEventHandler(this._treeView_PlusJobCompleted);
    this._treeView = (NavigatorTreeView) null;
  }

  /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
  public void Dispose() => this.ClearLinksWithTree();

  /// <summary>Событие вызывается после фоновой зачитки дочерних узлов</summary>
  private void _treeView_PlusJobCompleted(NavigatorTreeNode node)
  {
    if (node.CheckState == CheckState.Indeterminate && !node.HasChildren)
    {
      node.CheckState = CheckState.Checked;
      node.SetCheckState(CheckState.Checked, true, false, false);
    }
    if (!node.HasChildren)
      return;
    this.UpdateChildChecksAfterExpand(node);
  }

  private void _treeView_AfterPopulateNode(object sender, NodeEventArgs e)
  {
  }

  protected void UpdateChildChecksAfterExpand(NavigatorTreeNode node)
  {
    if (!node.ShowCheckState || node.CheckState == CheckState.Unchecked)
      return;
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) node.Children)
    {
      if (child.ShowCheckState && child.CheckState != CheckState.Checked)
        child.CheckState = !child.HasChildren ? CheckState.Checked : CheckState.Indeterminate;
    }
    node.SetCheckState(node.CheckState, true, false, false);
  }
}
