// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.UndoAttributeRemove
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;

#nullable disable
namespace Intermech.Interfaces.Document;

internal class UndoAttributeRemove : IUndoAction
{
  private IUndoManager manager;
  private string parentId;
  private string attributeName;
  private string attributeValue;

  public UndoAttributeRemove(
    IUndoManager manager,
    DocumentTreeNode parent,
    string attributeName,
    string attributeValue)
  {
    this.manager = manager;
    this.parentId = parent.Id;
    this.attributeName = attributeName;
    this.attributeValue = attributeValue;
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
      node.SetAttributeValue(this.attributeName, this.attributeValue);
      flag = true;
    }
    return flag;
  }

  public string Caption => LocalizationHolder.rm.GetString("Interfaces.Document_166");

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
    return node == null ? (IUndoAction) null : (IUndoAction) new UndoAttributeAdd(this.manager, node, this.attributeName, this.attributeValue);
  }

  public bool CloneAction => false;
}
