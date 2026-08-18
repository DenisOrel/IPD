// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Tools.Controls.Navigator.TechCardNavTreeNodes
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.TechCard.Client.Tools.Controls.Navigator;

/// <summary>TechCard TreeView node's collection</summary>
public class TechCardNavTreeNodes : NavigatorTreeNodes
{
  /// <summary>Create empty list</summary>
  public TechCardNavTreeNodes()
  {
  }

  /// <summary>Create class instance</summary>
  /// <param name="tree">Owner tree</param>
  /// <param name="owner">Owner node</param>
  public TechCardNavTreeNodes(NavigatorTreeView tree, NavigatorTreeNode owner)
    : base(tree, owner)
  {
  }

  /// <summary>Create new node</summary>
  /// <param name="nodeId">Node info</param>
  /// <returns>Created node</returns>
  public override NavigatorTreeNode Add(INodeID nodeId)
  {
    return this.Add(nodeId, (object[]) null, (object[]) null);
  }

  /// <summary>Create new node</summary>
  /// <param name="nodeId">Node info</param>
  /// <param name="values">Node values</param>
  /// <param name="rawValues">Исходные значения</param>
  /// <returns></returns>
  public override NavigatorTreeNode Add(INodeID nodeId, object[] values, object[] rawValues)
  {
    return this.Add(nodeId, values, rawValues, (INode) null);
  }

  /// <summary>Create new node</summary>
  /// <param name="nodeId">Node info</param>
  /// <param name="values">Node values</param>
  /// <param name="rawValues">Исходные значения</param>
  /// <param name="handler"></param>
  public override NavigatorTreeNode Add(
    INodeID nodeId,
    object[] values,
    object[] rawValues,
    INode handler)
  {
    return this.Add(nodeId, values, rawValues, handler, TreeNodeFlags.ImageOutdated);
  }

  /// <summary>Create new node</summary>
  /// <param name="nodeId">Node info</param>
  /// <param name="values">Node values</param>
  /// <param name="rawValues">Исходные значения</param>
  /// <param name="handler"></param>
  /// <param name="flags"></param>
  public override NavigatorTreeNode Add(
    INodeID nodeId,
    object[] values,
    object[] rawValues,
    INode handler,
    TreeNodeFlags flags)
  {
    return this.Add(nodeId, values, rawValues, handler, flags, (object) null);
  }

  /// <summary>Create new node</summary>
  /// <param name="nodeId">Node info</param>
  /// <param name="values">Node values</param>
  /// <param name="rawValues">Исходные значения</param>
  /// <param name="handler"></param>
  /// <param name="flags"></param>
  /// <param name="bookmark"></param>
  public override NavigatorTreeNode Add(
    INodeID nodeId,
    object[] values,
    object[] rawValues,
    INode handler,
    TreeNodeFlags flags,
    object bookmark)
  {
    return this.Add(nodeId, values, rawValues, handler, flags, bookmark, false);
  }

  /// <summary>Create new node</summary>
  /// <param name="nodeId">Node info</param>
  /// <param name="values">Node values</param>
  /// <param name="rawValues">Исходные значения</param>
  /// <param name="handler"></param>
  /// <param name="flags"></param>
  /// <param name="bookmark"></param>
  /// <param name="full"></param>
  public override NavigatorTreeNode Add(
    INodeID nodeId,
    object[] values,
    object[] rawValues,
    INode handler,
    TreeNodeFlags flags,
    object bookmark,
    bool full)
  {
    return this.Add(nodeId, values, rawValues, handler, flags, bookmark, full, (StatesRecord) null);
  }

  /// <summary>Create new node</summary>
  /// <param name="nodeId">Node info</param>
  /// <param name="values">Node values</param>
  /// <param name="rawValues">Исходные значения</param>
  /// <param name="handler"></param>
  /// <param name="flags"></param>
  /// <param name="bookmark"></param>
  /// <param name="full"></param>
  /// <param name="validColumns"></param>
  public override NavigatorTreeNode Add(
    INodeID nodeId,
    object[] values,
    object[] rawValues,
    INode handler,
    TreeNodeFlags flags,
    object bookmark,
    bool full,
    StatesRecord validColumns)
  {
    TechcardNavTreeNode techcardNavTreeNode = new TechcardNavTreeNode(this._tree, this._owner, nodeId, values, rawValues, handler, flags, bookmark, full, validColumns);
    this.Add((NavigatorTreeNode) techcardNavTreeNode);
    return (NavigatorTreeNode) techcardNavTreeNode;
  }

  /// <summary>Create list's clone</summary>
  /// <returns></returns>
  public override object Clone()
  {
    TechCardNavTreeNodes cardNavTreeNodes = new TechCardNavTreeNodes(this._tree, this._owner);
    cardNavTreeNodes.Assign((NavigatorTreeNodes) this);
    return (object) cardNavTreeNodes;
  }
}
