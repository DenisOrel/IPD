// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.UndoAttributeChanged
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;

#nullable disable
namespace Intermech.Interfaces.Document;

internal class UndoAttributeChanged : IUndoAction
{
  private IUndoManager manager;
  private string parentId;
  private string attributeName;
  private string oldValue;
  private string newValue;

  public UndoAttributeChanged(
    IUndoManager manager,
    DocumentTreeNode parent,
    string attributeName,
    string oldValue,
    string newValue)
  {
    this.manager = manager;
    this.parentId = parent.Id;
    this.attributeName = attributeName;
    this.oldValue = oldValue;
    this.newValue = newValue;
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
      node.SetAttributeValue(this.attributeName, this.oldValue);
      flag = true;
    }
    return flag;
  }

  public string Caption => LocalizationHolder.rm.GetString("Interfaces.Document_167");

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
    return node == null ? (IUndoAction) null : (IUndoAction) new UndoAttributeChanged(this.manager, node, this.attributeName, this.oldValue, this.newValue);
  }

  public bool CloneAction => false;
}
