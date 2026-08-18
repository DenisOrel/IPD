// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Tools.Controls.Navigator.TechcardNavTreeNode
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Tools.Controls.Navigator;

/// <summary>TechCard TreeView node</summary>
public class TechcardNavTreeNode : NavigatorTreeNode
{
  /// <summary>Node's check box style</summary>
  private NavigatorTreeViewCheckBoxStyle _checkBoxStyle = NavigatorTreeViewCheckBoxStyle.ThreeState;

  /// <summary>Create node instance</summary>
  /// <param name="tree">Owner node</param>
  /// <param name="parent">Owner node</param>
  /// <param name="nodeId">Node info</param>
  public TechcardNavTreeNode(NavigatorTreeView tree, NavigatorTreeNode parent, INodeID nodeId)
    : this(tree, parent, nodeId, (object[]) null, (object[]) null)
  {
  }

  /// <summary>Create node instance</summary>
  /// <param name="tree">Owner tree</param>
  /// <param name="parent">Owner node</param>
  /// <param name="nodeId">Node info</param>
  /// <param name="values">Node values</param>
  /// <param name="rawValues"></param>
  public TechcardNavTreeNode(
    NavigatorTreeView tree,
    NavigatorTreeNode parent,
    INodeID nodeId,
    object[] values,
    object[] rawValues)
    : this(tree, parent, nodeId, values, rawValues, (INode) null)
  {
  }

  /// <summary>Create node instance</summary>
  /// <param name="tree">Owner tree</param>
  /// <param name="parent">Owner node</param>
  /// <param name="nodeId">Node info</param>
  /// <param name="values">Node values</param>
  /// <param name="rawValues"></param>
  /// <param name="handler"></param>
  public TechcardNavTreeNode(
    NavigatorTreeView tree,
    NavigatorTreeNode parent,
    INodeID nodeId,
    object[] values,
    object[] rawValues,
    INode handler)
    : this(tree, parent, nodeId, values, rawValues, handler, TreeNodeFlags.ImageOutdated)
  {
  }

  /// <summary>Create node instance</summary>
  /// <param name="tree">Owner tree</param>
  /// <param name="parent">Owner node</param>
  /// <param name="nodeId">Node info</param>
  /// <param name="values">Node values</param>
  /// <param name="rawValues"></param>
  /// <param name="handler">Handler</param>
  /// <param name="flags">Flags</param>
  public TechcardNavTreeNode(
    NavigatorTreeView tree,
    NavigatorTreeNode parent,
    INodeID nodeId,
    object[] values,
    object[] rawValues,
    INode handler,
    TreeNodeFlags flags)
    : this(tree, parent, nodeId, values, rawValues, handler, flags, (object) null)
  {
  }

  /// <summary>Create node instance</summary>
  /// <param name="tree">Owner tree</param>
  /// <param name="parent">Owner node</param>
  /// <param name="nodeId">Node info</param>
  /// <param name="values">Node values</param>
  /// <param name="rawValues"></param>
  /// <param name="handler">Handler</param>
  /// <param name="flags">Flags</param>
  /// <param name="bookmark"></param>
  public TechcardNavTreeNode(
    NavigatorTreeView tree,
    NavigatorTreeNode parent,
    INodeID nodeId,
    object[] values,
    object[] rawValues,
    INode handler,
    TreeNodeFlags flags,
    object bookmark)
    : this(tree, parent, nodeId, values, rawValues, handler, flags, bookmark, false)
  {
  }

  /// <summary>Create node instance</summary>
  /// <param name="tree">Owner tree</param>
  /// <param name="parent">Owner node</param>
  /// <param name="nodeId">Node info</param>
  /// <param name="values">Node values</param>
  /// <param name="rawValues"></param>
  /// <param name="handler">Handler</param>
  /// <param name="flags">Flags</param>
  /// <param name="bookmark"></param>
  /// <param name="full"></param>
  public TechcardNavTreeNode(
    NavigatorTreeView tree,
    NavigatorTreeNode parent,
    INodeID nodeId,
    object[] values,
    object[] rawValues,
    INode handler,
    TreeNodeFlags flags,
    object bookmark,
    bool full)
    : this(tree, parent, nodeId, values, rawValues, handler, flags, bookmark, full, (StatesRecord) null)
  {
  }

  /// <summary>Create node instance</summary>
  /// <param name="tree">Owner tree</param>
  /// <param name="parent">Owner node</param>
  /// <param name="nodeId">Node info</param>
  /// <param name="values">Node values</param>
  /// <param name="rawValues"></param>
  /// <param name="handler">Handler</param>
  /// <param name="flags">Flags</param>
  /// <param name="bookmark"></param>
  /// <param name="full"></param>
  /// <param name="validColumns"></param>
  public TechcardNavTreeNode(
    NavigatorTreeView tree,
    NavigatorTreeNode parent,
    INodeID nodeId,
    object[] values,
    object[] rawValues,
    INode handler,
    TreeNodeFlags flags,
    object bookmark,
    bool full,
    StatesRecord validColumns)
    : base(tree, parent, nodeId, values, rawValues, handler, flags, bookmark, full, validColumns)
  {
    this.Children = (NavigatorTreeNodes) new TechCardNavTreeNodes(this.Tree, (NavigatorTreeNode) this);
  }

  /// <summary>Extract child nodes to one-dimension list</summary>
  /// <param name="onlyChecked"></param>
  /// <returns></returns>
  public override NavigatorTreeNodes ExtractNodes(bool onlyChecked)
  {
    TechCardNavTreeNodes nodes = new TechCardNavTreeNodes();
    this.ExtractNodes((NavigatorTreeNodes) nodes, onlyChecked);
    return (NavigatorTreeNodes) nodes;
  }

  /// <summary>Clone node object</summary>
  /// <returns>Tree node's clone</returns>
  public override object Clone()
  {
    TechcardNavTreeNode techcardNavTreeNode = new TechcardNavTreeNode(this.Tree, this.Parent, this.NodeID, this.Values, this.RawValues, this.Handler, this.Flags, this.Bookmark, this.Full, this.ValidColumns);
    for (int index = 0; index < this.Children.Count; ++index)
      techcardNavTreeNode.Children.Add(this.Children[index].Clone() as NavigatorTreeNode);
    return (object) techcardNavTreeNode;
  }

  /// <summary>
  /// Установка значения CheckState узлу и его дочерним узлам.
  /// При необходимости меняются значения и у родительских узлов
  /// </summary>
  /// <param name="value">Значение</param>
  public override void SetCheckState(CheckState value)
  {
    if (this._checkState == value)
      return;
    if (this.Tree is TechCardNavTreeViewControl tree && tree.CheckoutMode.Equals((object) TechCheckoutMode.Manual))
    {
      tree.DoRaiseCheckStateChanging((NavigatorTreeNode) this, this._checkState, ref value);
      if (this._checkState == value)
        return;
      this._checkState = value;
      tree.UpdateTreeNode((NavigatorTreeNode) this);
      tree.DoRaiseCheckStateChanged((NavigatorTreeNode) this);
    }
    else
      base.SetCheckState(value);
  }

  /// <summary>Set check state internal (directly)</summary>
  /// <param name="state"></param>
  public virtual void SetCheckStateInternal(CheckState state) => this._checkState = state;

  /// <summary>Выделение элементов дерева</summary>
  /// <param name="mode"></param>
  /// <param name="recursive"></param>
  public virtual void SetCheckStateCommon(NavTreeNodeSelectMode mode, bool recursive)
  {
    CheckState state = CheckState.Indeterminate;
    switch (mode)
    {
      case NavTreeNodeSelectMode.Select:
        state = CheckState.Checked;
        break;
      case NavTreeNodeSelectMode.Clear:
        state = CheckState.Unchecked;
        break;
      case NavTreeNodeSelectMode.Invert:
        if (this.CheckState == CheckState.Checked)
        {
          state = CheckState.Unchecked;
          break;
        }
        if (this.CheckState == CheckState.Unchecked)
        {
          state = CheckState.Checked;
          break;
        }
        break;
    }
    if (this.CheckState != state)
      this.SetCheckStateInternal(state);
    if (!recursive || this.Children == null)
      return;
    foreach (TechcardNavTreeNode child in (List<NavigatorTreeNode>) this.Children)
      child?.SetCheckStateCommon(mode, true);
  }

  /// <summary>Разворот узла</summary>
  /// <param name="recursive">Рекурсивный режим</param>
  public virtual void ExpandNode(bool recursive)
  {
    if (!recursive)
    {
      if (this.Expanded)
        return;
      this.Expanded = true;
    }
    else
      ((INavigatorTreeViewClientService) ServicesManager.GetService(typeof (INavigatorTreeViewClientService))).ExpandAll((NavigatorTreeNode) this);
  }

  /// <summary>Сворачивает узел в трубочку</summary>
  /// <param name="recursive"></param>
  public virtual void CollapseNode(bool recursive)
  {
    if (this.Expanded)
      this.Expanded = false;
    if (!recursive || this.Children == null)
      return;
    foreach (TechcardNavTreeNode child in (List<NavigatorTreeNode>) this.Children)
      child?.CollapseNode(true);
  }

  /// <summary>Checked child node's count</summary>
  public int CheckedCount
  {
    get => this._checkedCount;
    set => this._checkedCount = value;
  }

  /// <summary>Indeterminate child node's count</summary>
  public int IndeterminateCount
  {
    get => this._indeterminateCount;
    set => this._indeterminateCount = value;
  }

  /// <summary>Node's check box style</summary>
  public NavigatorTreeViewCheckBoxStyle CheckBoxStyle
  {
    get => this._checkBoxStyle;
    set => this._checkBoxStyle = value;
  }

  /// <summary>Class for storing TechcardNavTreeNode status</summary>
  public class NodeStateKeeper
  {
    /// <summary>
    /// 
    /// </summary>
    public CheckState state;
    /// <summary>
    /// 
    /// </summary>
    public int checkedCount;
    /// <summary>
    /// 
    /// </summary>
    public int indetermCount;

    /// <summary>Constructor</summary>
    public NodeStateKeeper()
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="node"></param>
    public NodeStateKeeper(NavigatorTreeNode node) => this.SaveState(node);

    /// <summary>Save node's state</summary>
    /// <param name="node"></param>
    public void SaveState(NavigatorTreeNode node)
    {
      if (!(node is TechcardNavTreeNode techcardNavTreeNode))
        return;
      this.state = techcardNavTreeNode.CheckState;
      this.checkedCount = techcardNavTreeNode.CheckedCount;
      this.indetermCount = techcardNavTreeNode.IndeterminateCount;
    }

    /// <summary>Restore node's state</summary>
    /// <param name="node"></param>
    public void RestoreState(NavigatorTreeNode node)
    {
      if (!(node is TechcardNavTreeNode techcardNavTreeNode))
        return;
      techcardNavTreeNode.SetCheckStateInternal(this.state);
      techcardNavTreeNode.CheckedCount = this.checkedCount;
      techcardNavTreeNode.IndeterminateCount = this.indetermCount;
    }
  }
}
