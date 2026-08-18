// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.TreeListNodeWrapper
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Interfaces.Document;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Вспомогательный класс для представления узлов документа
/// в TreeList</summary>
[Serializable]
public class TreeListNodeWrapper
{
  private int sortIndex;
  private TreeListNode owner;
  private DocumentTreeNode documentNode;
  private NodeFilter nodeFilter;

  /// <summary>Фильтр узлов</summary>
  public NodeFilter NodeFilter
  {
    [DebuggerStepThrough] get => this.nodeFilter;
    set
    {
      if (this.nodeFilter == value)
        return;
      this.nodeFilter = value;
      this.SynchronizeTree(false);
    }
  }

  /// <summary>Владелец</summary>
  public TreeListNode Owner
  {
    [DebuggerStepThrough] get => this.owner;
  }

  /// <summary>Конструктор</summary>
  /// <param name="owner">Владелец</param>
  /// <param name="nodeFilter">Фильтр</param>
  public TreeListNodeWrapper(TreeListNode owner, NodeFilter nodeFilter)
  {
    this.owner = owner;
    if (owner != null)
      owner.Tag = (object) this;
    this.nodeFilter = nodeFilter;
  }

  /// <summary>Проверить удовлетворяет ли узел фильтру</summary>
  /// <returns>Удовлетворяет ли узел фильтру</returns>
  public virtual bool CheckNode()
  {
    return this.nodeFilter == null || this.nodeFilter.CheckNode((object) this.documentNode);
  }

  /// <summary>Обработчик события NameChanged узла документа</summary>
  /// <param name="sender">Объект вызвавший событие</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void DocumentNode_NameChanged(object sender, NameChanged_EventArgs e)
  {
    if (this.DocumentNode == null || this.owner == null)
      return;
    this.owner[(object) 0] = (object) this.DocumentNode.GetDefautCaption();
  }

  /// <summary>Узел документа</summary>
  public DocumentTreeNode DocumentNode
  {
    [DebuggerStepThrough] get => this.documentNode;
    set
    {
      if (this.documentNode == value)
        return;
      if (this.documentNode != null)
      {
        this.documentNode.NameChanged -= new NameChanged_EventHandler(this.DocumentNode_NameChanged);
        this.documentNode.ChildNodesPositionExchanged -= new ChildNodesPositionExchanged_EventHandler(this.childNodesPositionExchanged);
        this.documentNode.BeginStructureChangingEvent -= new StructureChanging_EventHandler(this.beginChangingDocNodeStructure);
        this.documentNode.EndStructureChangingEvent -= new StructureChanging_EventHandler(this.endChangingDocNodeStructure);
      }
      this.documentNode = value;
      if (this.documentNode == null)
        return;
      object obj = this.owner[(object) 0];
      string defautCaption = this.documentNode.GetDefautCaption();
      if (obj == null || obj.ToString() != defautCaption)
        this.owner[(object) 0] = (object) defautCaption;
      this.documentNode.NameChanged += new NameChanged_EventHandler(this.DocumentNode_NameChanged);
      this.documentNode.ChildNodesPositionExchanged += new ChildNodesPositionExchanged_EventHandler(this.childNodesPositionExchanged);
      this.documentNode.BeginStructureChangingEvent += new StructureChanging_EventHandler(this.beginChangingDocNodeStructure);
      this.documentNode.EndStructureChangingEvent += new StructureChanging_EventHandler(this.endChangingDocNodeStructure);
    }
  }

  private void childNodesPositionExchanged(object sender, ChildNodesPositionExchanged_EventArgs e)
  {
    if (this.owner == null)
      return;
    if (this.owner.Nodes.Count > e.Index1 && this.owner.Nodes.Count > e.Index2 && this.DocumentNode != null && this.DocumentNode.Nodes[e.Index1].ShowInTreeView && this.DocumentNode.Nodes[e.Index2].ShowInTreeView)
    {
      TreeListNodeWrapper tag1 = this.owner.Nodes[e.Index1].Tag as TreeListNodeWrapper;
      TreeListNodeWrapper tag2 = this.owner.Nodes[e.Index1].Tag as TreeListNodeWrapper;
      if (tag1 != null && tag2 != null)
      {
        this.owner.TreeList?.BeginSort();
        int sortIndex = tag1.SortIndex;
        tag1.SortIndex = tag2.SortIndex;
        tag2.SortIndex = sortIndex;
        this.owner.TreeList?.EndSort();
        return;
      }
    }
    this.SynchronizeTree(true);
  }

  private void beginChangingDocNodeStructure(object sender, StructureChanging_EventArgs e)
  {
    this.owner?.TreeList?.BeginUnboundLoad();
  }

  private void endChangingDocNodeStructure(object sender, StructureChanging_EventArgs e)
  {
    this.SynchronizeTree(false);
    this.owner?.TreeList?.EndUnboundLoad();
  }

  /// <summary>Разорвать связь с узлом документа</summary>
  public virtual void DisconnectDocumentNode()
  {
    this.DocumentNode = (DocumentTreeNode) null;
    if (this.owner == null)
      return;
    for (int index = 0; index < this.owner.Nodes.Count; ++index)
    {
      if (this.owner.Nodes[index].Tag is TreeListNodeWrapper tag)
        tag.DisconnectDocumentNode();
    }
  }

  /// <summary>Найти узел TreeList для узла документа</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns>Узел TreeList</returns>
  public TreeListNode FindNode(DocumentTreeNode docNode)
  {
    if (this.owner == null)
      return (TreeListNode) null;
    if (docNode == this.DocumentNode)
      return this.owner;
    TreeListNode node = (TreeListNode) null;
    for (int index = 0; index < this.owner.Nodes.Count; ++index)
    {
      if (this.owner.Nodes[index].Tag is TreeListNodeWrapper tag)
        node = tag.FindNode(docNode);
      if (node != null)
        break;
    }
    return node;
  }

  /// <summary>Удалить узел из TreeList</summary>
  public void RemoveNode()
  {
    if (this.owner != null)
      this.RemoveNode(this.owner);
    else
      this.DisconnectDocumentNode();
  }

  /// <summary>Удалить узел из TreeList</summary>
  /// <param name="treeListNode">Узел TreeList</param>
  public void RemoveNode(TreeListNode treeListNode)
  {
    if (treeListNode == null)
      return;
    if (treeListNode.Tag is TreeListNodeWrapper tag)
      tag.DisconnectDocumentNode();
    if (treeListNode.ParentNode != null)
      treeListNode.ParentNode.Nodes?.Remove(treeListNode);
    else
      treeListNode.TreeList?.Nodes.Remove(treeListNode);
  }

  /// <summary>Индекс сортировки</summary>
  public int SortIndex
  {
    [DebuggerStepThrough] get => this.sortIndex;
    set => this.sortIndex = value;
  }

  /// <summary>Получить узел документа для заданного узла TreeList</summary>
  /// <param name="treeListNode">Узел TreeList</param>
  /// <returns>Узел документа</returns>
  public static DocumentTreeNode GetDocNode(TreeListNode treeListNode)
  {
    DocumentTreeNode docNode = (DocumentTreeNode) null;
    if (treeListNode?.Tag is TreeListNodeWrapper tag)
      docNode = tag.DocumentNode;
    return docNode;
  }

  /// <summary>
  /// Синхонизировать узлы дерева TreeList с деревом документа
  /// </summary>
  /// <param name="recursive">Для всех дочерних узлов</param>
  public void SynchronizeTree(bool recursive)
  {
    if (this.owner == null)
      return;
    TreeList treeList = this.owner.TreeList;
    if (treeList != null)
    {
      treeList.BeginSort();
      treeList.BeginUpdate();
      treeList.BeginUnboundLoad();
    }
    TreeListNodeWrapper treeListNodeWrapper = (TreeListNodeWrapper) null;
    DocumentTreeNode documentTreeNode = (DocumentTreeNode) null;
    if (this.documentNode != null && this.documentNode.ShowInTreeView)
    {
      if (this.documentNode.Nodes != null)
      {
        int num = 0;
        for (int index1 = 0; index1 < this.documentNode.Nodes.Count; ++index1)
        {
          treeListNodeWrapper = (TreeListNodeWrapper) null;
          documentTreeNode = (DocumentTreeNode) null;
          TreeListNode treeListNode = (TreeListNode) null;
          int index2;
          for (index2 = 0; index2 < this.owner.Nodes.Count; ++index2)
          {
            DocumentTreeNode docNode = TreeListNodeWrapper.GetDocNode(this.owner.Nodes[index2]);
            if (docNode == this.documentNode.Nodes[index1])
            {
              treeListNode = this.owner.Nodes[index2];
              if (!docNode.ShowInTreeView)
                index2 = this.owner.Nodes.Count;
              object obj = treeListNode[(object) 0];
              string defautCaption = this.documentNode.Nodes[index1].GetDefautCaption();
              if (obj == null || obj.ToString() != defautCaption)
              {
                treeListNode[(object) 0] = (object) defautCaption;
                break;
              }
              break;
            }
          }
          if (index2 >= this.owner.Nodes.Count)
          {
            if (this.documentNode.Nodes[index1].ShowInTreeView)
            {
              TreeListNode owner;
              if (treeList == null)
                owner = (TreeListNode) null;
              else
                owner = treeList.AppendNode((object) new object[1]
                {
                  (object) this.documentNode.Nodes[index1].GetDefautCaption()
                }, this.owner);
              new TreeListNodeWrapper(owner, this.nodeFilter)
              {
                DocumentNode = this.documentNode.Nodes[index1],
                SortIndex = num
              }.SynchronizeTree(true);
              ++num;
            }
          }
          else
          {
            TreeListNodeWrapper tag = treeListNode.Tag as TreeListNodeWrapper;
            if (index2 != num)
              tag.SortIndex = num;
            if (recursive)
              tag.SynchronizeTree(recursive);
            ++num;
          }
        }
        for (int index = this.owner.Nodes.Count - 1; index >= 0; --index)
        {
          TreeListNode node = this.owner.Nodes[index];
          DocumentTreeNode docNode = TreeListNodeWrapper.GetDocNode(node);
          if (docNode == null || !docNode.ShowInTreeView || docNode.Parent != this.documentNode)
            this.RemoveNode(node);
        }
      }
      else
      {
        for (int index = this.owner.Nodes.Count - 1; index > -1; --index)
          this.RemoveNode(this.owner.Nodes[index]);
      }
      if (!this.CheckNode() && this.owner.Nodes.Count == 0)
        this.RemoveNode();
    }
    else
      this.RemoveNode();
    if (treeList == null)
      return;
    treeList.EndSort();
    treeList.EndUpdate();
    treeList.EndUnboundLoad();
  }
}
