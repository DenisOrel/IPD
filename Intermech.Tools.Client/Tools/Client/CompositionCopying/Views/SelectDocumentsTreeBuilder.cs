// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.SelectDocumentsTreeBuilder
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.Client.CompositionCopying.Model;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal sealed class SelectDocumentsTreeBuilder
{
  private bool allowLoopNodes;

  public SelectDocumentsTreeBuilder() => this.allowLoopNodes = true;

  public bool AllowLoopNodes
  {
    get => this.allowLoopNodes;
    set => this.allowLoopNodes = value;
  }

  public SelectDocumentsTreeNodeVM CreateTree(
    CopyingSession session,
    DBObjectGraphVertex vertex,
    bool populateChildren)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (vertex == null)
      throw new ArgumentNullException(nameof (vertex));
    SelectDocumentsTreeNodeVM treeNodeInternal = this.CreateTreeNodeInternal(session, vertex, populateChildren);
    treeNodeInternal.IsExpanded = true;
    return treeNodeInternal;
  }

  private SelectDocumentsTreeNodeVM CreateTreeNodeInternal(
    CopyingSession session,
    DBObjectGraphVertex vertex,
    bool populateChildren)
  {
    SelectDocumentsTreeNodeVM treeNodeInternal = new SelectDocumentsTreeNodeVM(vertex.Caption, new DBObjectGraphVertexReference(session, vertex));
    this.AddDrawingEdges(session, vertex, treeNodeInternal);
    if (populateChildren && !treeNodeInternal.IsVirtual)
      this.PopulateChildren(treeNodeInternal);
    return treeNodeInternal;
  }

  private void PopulateChildren(SelectDocumentsTreeNodeVM parentTreeNode)
  {
    CopyingSession session = parentTreeNode.VertexReference.Session;
    foreach (DBObjectGraphVertex verticesByOutEdge in (IEnumerable<DBObjectGraphVertex>) session.Graph.GetVerticesByOutEdges(parentTreeNode.VertexReference.Vertex))
    {
      bool flag = this.IsLoop(parentTreeNode, verticesByOutEdge);
      if (!flag || this.allowLoopNodes)
      {
        SelectDocumentsTreeNodeVM treeNodeInternal = this.CreateTreeNodeInternal(session, verticesByOutEdge, false);
        treeNodeInternal.InitializeParentNode(parentTreeNode);
        if (!flag && !treeNodeInternal.IsVirtual)
          this.PopulateChildren(treeNodeInternal);
        parentTreeNode.Nodes.Add(treeNodeInternal);
      }
    }
  }

  private bool IsLoop(SelectDocumentsTreeNodeVM parentTreeNode, DBObjectGraphVertex childVertex)
  {
    for (; parentTreeNode != null; parentTreeNode = parentTreeNode.ParentNode)
    {
      if (parentTreeNode.VertexReference != null && parentTreeNode.VertexReference.Vertex == childVertex)
        return true;
    }
    return false;
  }

  private void AddDrawingEdges(
    CopyingSession session,
    DBObjectGraphVertex vertex,
    SelectDocumentsTreeNodeVM rootNode)
  {
    ICollection<DBObjectGraphEdge> inEdges = session.Graph.GetInEdges(vertex, (Predicate<DBObjectGraphEdge>) (x => x.Source.IsCADModelDrawing()));
    if (inEdges.Count <= 0)
      return;
    SelectDocumentsTreeNodeVM parentNode = new SelectDocumentsTreeNodeVM("Связанные чертежи");
    rootNode.Nodes.Add(parentNode);
    foreach (DBObjectGraphEdge dbObjectGraphEdge in (IEnumerable<DBObjectGraphEdge>) inEdges)
    {
      SelectDocumentsTreeNodeVM treeNodeInternal = this.CreateTreeNodeInternal(session, dbObjectGraphEdge.Source, false);
      treeNodeInternal.InitializeParentNode(parentNode);
      parentNode.Nodes.Add(treeNodeInternal);
    }
  }
}
