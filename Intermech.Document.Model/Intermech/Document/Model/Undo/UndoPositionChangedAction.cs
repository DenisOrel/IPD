// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.Undo.UndoPositionChangedAction
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using Intermech.Localization;

#nullable disable
namespace Intermech.Document.Model.Undo;

internal class UndoPositionChangedAction : IUndoAction
{
  private readonly IUndoManager manager;
  private string parentId;
  private readonly int oldPos;
  private readonly int newPos;
  private readonly bool exchanged;
  private DocumentTreeNode clone;

  public UndoPositionChangedAction(
    IUndoManager manager,
    DocumentTreeNode parent,
    int oldPos,
    int newPos,
    bool exchanged)
  {
    this.manager = manager;
    if (parent is TableData tableData)
      parent = (DocumentTreeNode) tableData.FindFirstTable();
    this.parentId = parent.Id;
    this.oldPos = oldPos;
    this.newPos = newPos;
    this.exchanged = exchanged;
  }

  public bool DoAction()
  {
    bool flag = false;
    if (this.manager.Document == null)
      return false;
    DocumentTreeNode node1 = this.manager.Document.FindNode(this.parentId);
    if (node1 != null)
    {
      int oldPos = this.oldPos;
      if (node1.NodesCount < oldPos)
        return false;
      if (this.exchanged)
      {
        node1.Nodes.Exchange(this.oldPos, this.newPos);
      }
      else
      {
        DocumentTreeNode node2 = node1.Nodes[this.newPos];
        if (node2 != null)
          node1.InsertChildNode(oldPos, node2, false, true, true, true);
      }
      flag = true;
    }
    return flag;
  }

  public string Caption => LocalizationHolder.rm.GetString("Document.Model_563");

  public void IdChanged(string oldValue, string newValue)
  {
    if (!(this.parentId == oldValue))
      return;
    this.parentId = newValue;
  }

  public IUndoAction CreateRedoAction()
  {
    DocumentTreeNode node = this.manager.Document?.FindNode(this.parentId);
    return node == null ? (IUndoAction) null : (IUndoAction) new UndoPositionChangedAction(this.manager, node, this.newPos, this.oldPos, this.exchanged);
  }

  public bool CloneAction => true;

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
