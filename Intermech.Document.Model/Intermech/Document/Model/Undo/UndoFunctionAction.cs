// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.Undo.UndoFunctionAction
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System.Reflection;

#nullable disable
namespace Intermech.Document.Model.Undo;

internal class UndoFunctionAction : IUndoAction
{
  private readonly IUndoManager manager;
  private string nodeId;
  private readonly string funcName;
  private readonly object[] oldValues;
  private readonly object[] newValues;
  private readonly string caption;

  public UndoFunctionAction(
    IUndoManager manager,
    DocumentTreeNode node,
    string funcName,
    object[] oldValues,
    object[] newValues)
  {
    this.manager = manager;
    this.nodeId = node.Id;
    this.funcName = funcName;
    this.oldValues = oldValues;
    this.newValues = newValues;
    this.caption = funcName;
  }

  private UndoFunctionAction(
    IUndoManager manager,
    string caption,
    string nodeId,
    string funcName,
    object[] oldValues,
    object[] newValues)
  {
    this.manager = manager;
    this.nodeId = nodeId;
    this.funcName = funcName;
    this.caption = caption;
    this.oldValues = oldValues;
    this.newValues = newValues;
  }

  public override string ToString() => this.funcName;

  public bool DoAction()
  {
    VisualNode document = this.manager.Document;
    if (document == null)
      return false;
    DocumentTreeNode node = document.FindNode(this.nodeId);
    int num = 0;
    if (node == null)
      return num != 0;
    MethodInfo method = FindFieldHelper.FindMethod(node.GetType(), this.funcName, this.oldValues);
    if (!(method != (MethodInfo) null))
      return num != 0;
    method.Invoke((object) node, this.oldValues);
    return num != 0;
  }

  public string Caption => this.caption;

  public void IdChanged(string oldValue, string newValue)
  {
    if (!(this.nodeId == oldValue))
      return;
    this.nodeId = newValue;
  }

  public IUndoAction CreateRedoAction()
  {
    return (IUndoAction) new UndoFunctionAction(this.manager, this.caption, this.nodeId, this.funcName, this.newValues, this.oldValues);
  }
}
