// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.BlankTreeListNodeWrapper
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Document.Model.ImportBlanks;
using Intermech.Localization;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Вспомогательный класс для представления узлов бланка
/// в TreeList</summary>
[Serializable]
public class BlankTreeListNodeWrapper
{
  private TreeListNode owner;
  private PrimitiveBase blankNode;

  /// <summary>Узел TreeList представляющий узел бланка</summary>
  public TreeListNode Owner
  {
    [DebuggerStepThrough] get => this.owner;
  }

  /// <summary>Конструктор</summary>
  /// <param name="owner">Узел TreeList</param>
  public BlankTreeListNodeWrapper(TreeListNode owner)
  {
    this.owner = owner;
    if (owner == null)
      return;
    owner.Tag = (object) this;
  }

  /// <summary>Узел бланка</summary>
  public PrimitiveBase BlankNode
  {
    [DebuggerStepThrough] get => this.blankNode;
    set
    {
      if (this.blankNode == value)
        return;
      this.blankNode = value;
      if (this.blankNode == null)
        return;
      this.owner[(object) 0] = (object) BlankTreeListNodeWrapper.GetDefaultCaption(this.blankNode);
    }
  }

  /// <summary>Получить подпись</summary>
  /// <param name="blankNode">Узел бланка</param>
  /// <returns>Подпись</returns>
  public static string GetDefaultCaption(PrimitiveBase blankNode)
  {
    return $"{blankNode.GetType().Name} \\ {blankNode.Id} \\ {blankNode.Name}";
  }

  /// <summary>Разорвать связь с узлом</summary>
  public virtual void DisconnectBlankNode()
  {
    this.BlankNode = (PrimitiveBase) null;
    if (this.owner == null)
      return;
    for (int index = 0; index < this.owner.Nodes.Count; ++index)
    {
      if (this.owner.Nodes[index].Tag is BlankTreeListNodeWrapper tag)
        tag.DisconnectBlankNode();
    }
  }

  /// <summary>Найти узел TreeList для узла бланка</summary>
  /// <param name="docNode">Узел бланка</param>
  /// <returns>Узел TreeList</returns>
  public TreeListNode FindNode(PrimitiveBase docNode)
  {
    if (this.owner == null)
      return (TreeListNode) null;
    if (docNode == this.BlankNode)
      return this.owner;
    TreeListNode node = (TreeListNode) null;
    for (int index = 0; index < this.owner.Nodes.Count; ++index)
    {
      if (this.owner.Nodes[index].Tag is BlankTreeListNodeWrapper tag)
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
      this.DisconnectBlankNode();
  }

  /// <summary>Удалить узел TreeList</summary>
  /// <param name="treeListNode">Узел TreeList</param>
  public void RemoveNode(TreeListNode treeListNode)
  {
    if (treeListNode == null)
      return;
    if (treeListNode.Tag is BlankTreeListNodeWrapper tag)
      tag.DisconnectBlankNode();
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

  /// <summary>Получить узел бланка для узла TreeList</summary>
  /// <param name="treeListNode">Узел TreeList</param>
  /// <returns>Узел бланка</returns>
  public static PrimitiveBase GetDocNode(TreeListNode treeListNode)
  {
    PrimitiveBase docNode = (PrimitiveBase) null;
    if (treeListNode != null && treeListNode.Tag is BlankTreeListNodeWrapper tag)
      docNode = tag.BlankNode;
    return docNode;
  }

  /// <summary>Синхронизировать дерево TreeList и дерево бланка</summary>
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
    if (this.blankNode != null && treeList != null && this.blankNode is GroupPrimitive blankNode1)
    {
      if (this.blankNode is Area blankNode && blankNode.Variants.Count > 0)
      {
        TreeListNode treeListNode = treeList.AppendNode((object) new object[1]
        {
          (object) LocalizationHolder.rm.GetString("Document.Model_6")
        }, this.owner);
        BlankTreeListNodeWrapper treeListNodeWrapper = new BlankTreeListNodeWrapper(treeListNode);
        for (int index = 0; index < blankNode.Variants.Count; ++index)
          new BlankTreeListNodeWrapper(treeList.AppendNode((object) new object[1]
          {
            (object) BlankTreeListNodeWrapper.GetDefaultCaption(blankNode.Variants[index])
          }, treeListNode))
          {
            BlankNode = blankNode.Variants[index]
          }.SynchronizeTree(true);
        treeListNode[(object) 0] = (object) LocalizationHolder.rm.GetString("Document.Model_7");
      }
      for (int index = 0; index < blankNode1.ChildList.Count; ++index)
        new BlankTreeListNodeWrapper(treeList.AppendNode((object) new object[1]
        {
          (object) BlankTreeListNodeWrapper.GetDefaultCaption(blankNode1.ChildList[index])
        }, this.owner))
        {
          BlankNode = blankNode1.ChildList[index]
        }.SynchronizeTree(true);
    }
    if (treeList == null)
      return;
    treeList.EndUpdate();
    treeList.EndUnboundLoad();
  }
}
