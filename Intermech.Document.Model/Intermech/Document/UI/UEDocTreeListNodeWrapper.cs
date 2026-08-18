// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.UEDocTreeListNodeWrapper
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Document.Model.ImportBlanks;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Вспомогательный класс для представления узлов документа UEdit
/// в TreeList</summary>
[Serializable]
public class UEDocTreeListNodeWrapper
{
  private TreeListNode owner;
  private CloneBase ueDocNode;

  /// <summary>Владелец</summary>
  public TreeListNode Owner
  {
    [DebuggerStepThrough] get => this.owner;
  }

  /// <summary>Конструктор</summary>
  /// <param name="owner">Владелец</param>
  public UEDocTreeListNodeWrapper(TreeListNode owner)
  {
    this.owner = owner;
    if (owner == null)
      return;
    owner.Tag = (object) this;
  }

  /// <summary>Узел документа</summary>
  public CloneBase UEDocNode
  {
    [DebuggerStepThrough] get => this.ueDocNode;
    set
    {
      if (this.ueDocNode == value)
        return;
      this.ueDocNode = value;
      if (this.ueDocNode == null)
        return;
      this.owner[(object) 0] = (object) UEDocTreeListNodeWrapper.GetDefaultCaption(this.ueDocNode);
    }
  }

  /// <summary>Получить подпись узла</summary>
  /// <param name="ueDocNode">Узел</param>
  /// <returns>Подпись узла</returns>
  public static string GetDefaultCaption(CloneBase ueDocNode)
  {
    return $"{ueDocNode.GetType().Name} \\ {ueDocNode.Id} \\ {ueDocNode.Name}";
  }

  /// <summary>Конструктор</summary>
  public virtual void DisconnectUEDocNode()
  {
    this.UEDocNode = (CloneBase) null;
    if (this.owner == null)
      return;
    for (int index = 0; index < this.owner.Nodes.Count; ++index)
    {
      if (this.owner.Nodes[index].Tag is UEDocTreeListNodeWrapper tag)
        tag.DisconnectUEDocNode();
    }
  }

  /// <summary>Удалить узел из TreeList</summary>
  public void RemoveNode()
  {
    if (this.owner != null)
      this.RemoveNode(this.owner);
    else
      this.DisconnectUEDocNode();
  }

  /// <summary>Удалить узел из TreeList</summary>
  /// <param name="treeListNode">Узел TreeList</param>
  public void RemoveNode(TreeListNode treeListNode)
  {
    if (treeListNode == null)
      return;
    if (treeListNode.Tag is UEDocTreeListNodeWrapper tag)
      tag.DisconnectUEDocNode();
    if (treeListNode.ParentNode != null)
    {
      if (treeListNode.ParentNode.Nodes == null)
        return;
      treeListNode.ParentNode.Nodes.Remove(treeListNode);
    }
    else
    {
      if (treeListNode.TreeList == null)
        return;
      treeListNode.TreeList.Nodes.Remove(treeListNode);
    }
  }

  /// <summary>Получить узел документа для узла TreeList</summary>
  /// <param name="treeListNode">Узел TreeList</param>
  /// <returns>Узел документа для узла TreeList</returns>
  public static CloneBase GetDocNode(TreeListNode treeListNode)
  {
    CloneBase docNode = (CloneBase) null;
    if (treeListNode != null && treeListNode.Tag is UEDocTreeListNodeWrapper tag)
      docNode = tag.UEDocNode;
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
      treeList.BeginUpdate();
      treeList.BeginUnboundLoad();
    }
    if (this.ueDocNode != null && this.ueDocNode is GroupClone ueDocNode)
    {
      for (int index = 0; index < ueDocNode.ChildList.Count; ++index)
      {
        TreeListNode owner;
        if (treeList == null)
          owner = (TreeListNode) null;
        else
          owner = treeList.AppendNode((object) new object[1]
          {
            (object) UEDocTreeListNodeWrapper.GetDefaultCaption(ueDocNode.ChildList[index])
          }, this.owner);
        new UEDocTreeListNodeWrapper(owner)
        {
          UEDocNode = ueDocNode.ChildList[index]
        }.SynchronizeTree(true);
      }
    }
    if (treeList == null)
      return;
    treeList.EndUpdate();
    treeList.EndUnboundLoad();
  }
}
