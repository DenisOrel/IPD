// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.Undo.UndoRemoveAction
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using Intermech.Localization;

#nullable disable
namespace Intermech.Document.Model.Undo;

internal class UndoRemoveAction : IUndoAction
{
  private IUndoManager manager;
  private string parentId;
  private DocumentTreeNode child;
  private int removeIndex;
  private DocumentTreeNode clone;

  public UndoRemoveAction(
    IUndoManager manager,
    DocumentTreeNode parent,
    DocumentTreeNode child,
    int removeIndex)
  {
    this.manager = manager;
    if (parent is TableData)
      parent = (DocumentTreeNode) (parent as TableData).FindFirstTable();
    this.parentId = parent.Id;
    this.child = child;
    this.removeIndex = removeIndex;
  }

  public bool DoAction()
  {
    bool flag = false;
    VisualNode document = this.manager.Document;
    if (document == null)
      return false;
    DocumentTreeNode node = document.FindNode(this.parentId);
    if (node != null)
    {
      int index = this.removeIndex;
      if (node.NodesCount < index)
        index = node.NodesCount;
      node.InsertChildNode(index, this.child, false, true, true, true);
      flag = true;
    }
    return flag;
  }

  public string Caption => LocalizationHolder.rm.GetString("Document.Model_561");

  public void IdChanged(string oldValue, string newValue)
  {
    if (!(this.parentId == oldValue))
      return;
    this.parentId = newValue;
  }

  public IUndoAction CreateRedoAction()
  {
    VisualNode document = this.manager.Document;
    if (document == null)
      return (IUndoAction) null;
    DocumentTreeNode node = document.FindNode(this.parentId);
    return node == null || this.child == null ? (IUndoAction) null : (IUndoAction) new UndoAddAction(this.manager, node, this.child);
  }

  public DocumentTreeNode Clone
  {
    get => this.clone;
    set => this.clone = value;
  }

  public string NodeId
  {
    get => this.parentId;
    set => this.parentId = value;
  }
}
