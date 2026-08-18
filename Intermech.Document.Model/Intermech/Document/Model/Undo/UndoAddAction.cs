// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.Undo.UndoAddAction
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using Intermech.Localization;

#nullable disable
namespace Intermech.Document.Model.Undo;

internal class UndoAddAction : IUndoAction
{
  private IUndoManager manager;
  private string childId;
  private int index;
  private DocumentTreeNode child;

  public UndoAddAction(IUndoManager manager, DocumentTreeNode parent, DocumentTreeNode child)
  {
    this.manager = manager;
    if (parent is TableData)
      parent = (DocumentTreeNode) (parent as TableData).FindFirstTable();
    this.NodeId = parent.Id;
    this.childId = child.Id;
    this.index = child.Index;
  }

  public bool DoAction()
  {
    bool flag = false;
    VisualNode document = this.manager.Document;
    if (document == null)
      return false;
    DocumentTreeNode node1 = document.FindNode(this.NodeId);
    DocumentTreeNode node2 = document.FindNode(this.childId);
    if (node1 != null && node2 != null)
    {
      this.child = node2;
      node2.Remove(true, true);
      flag = true;
    }
    return flag;
  }

  public string Caption => LocalizationHolder.rm.GetString("Document.Model_560");

  public void IdChanged(string oldValue, string newValue)
  {
    if (this.NodeId == oldValue)
      this.NodeId = newValue;
    if (!(this.childId == oldValue))
      return;
    this.childId = newValue;
  }

  public IUndoAction CreateRedoAction()
  {
    VisualNode document = this.manager.Document;
    if (document == null)
      return (IUndoAction) null;
    DocumentTreeNode node = document.FindNode(this.NodeId);
    return node == null || this.child == null ? (IUndoAction) null : (IUndoAction) new UndoRemoveAction(this.manager, node, this.child, this.index);
  }

  public DocumentTreeNode Clone { get; set; }

  public string NodeId { get; set; }
}
