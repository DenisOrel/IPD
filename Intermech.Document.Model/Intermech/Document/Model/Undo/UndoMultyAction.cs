// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.Undo.UndoMultyAction
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Document.Model.Undo;

internal class UndoMultyAction : IUndoAction
{
  private IUndoManager manager;
  private string caption;
  private List<IUndoAction> actions = new List<IUndoAction>();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="manager"></param>
  /// <param name="caption"></param>
  public UndoMultyAction(IUndoManager manager, string caption)
  {
    this.manager = manager;
    this.caption = caption;
  }

  public List<IUndoAction> Actions => this.actions;

  public override string ToString()
  {
    if (this.caption != null && this.caption != string.Empty)
      return this.caption;
    return this.actions.Count > 0 ? this.actions[0].ToString() : "";
  }

  public bool DoAction()
  {
    VisualNode document = this.manager.Document;
    if (document == null)
      return false;
    bool flag = false;
    this.manager.LockUndo();
    document.SuspendUpdateGeometryRefreshUI();
    document.SuspendUpdateLayout();
    try
    {
      for (int index1 = this.actions.Count - 1; index1 >= 0; --index1)
      {
        if ((this.actions[index1] is ICloneAction action ? action.Clone : (DocumentTreeNode) null) == null)
        {
          flag = this.actions[index1].DoAction() | flag;
        }
        else
        {
          DocumentTreeNode node = this.manager.Document.FindNode(action.CloneId);
          if (node != null)
          {
            DocumentTreeNode documentTreeNode = node.Clone(true, true);
            DocumentTreeNode parent = node.Parent;
            if (parent != null)
            {
              int index2 = node.Index;
              parent.RemoveChildNode(node, false, false);
              parent.InsertChildNode(index2, action.Clone, false, true, false, false);
              action.Clone = documentTreeNode;
            }
          }
        }
      }
    }
    finally
    {
      document.ResumeUpdateLayout(true, true);
      document.ResumeUpdateRefreshUI(true, true);
      this.manager.UnlockUndo();
    }
    return flag;
  }

  public string Caption
  {
    get => this.actions.Count > 0 && this.caption == "" ? this.actions[0].Caption : this.caption;
  }

  public void IdChanged(string oldValue, string newValue)
  {
    for (int index = this.actions.Count - 1; index >= 0; --index)
      this.actions[index].IdChanged(oldValue, newValue);
  }

  public IUndoAction CreateRedoAction()
  {
    UndoMultyAction redoAction1 = new UndoMultyAction(this.manager, this.caption);
    for (int index = this.actions.Count - 1; index >= 0; --index)
    {
      IUndoAction action = this.actions[index];
      IUndoAction redoAction2 = this.actions[index].CreateRedoAction();
      if (redoAction2 != null)
      {
        redoAction1.Actions.Add(redoAction2);
        if (action is ICloneAction)
          (redoAction2 as ICloneAction).Clone = (action as ICloneAction).Clone;
      }
    }
    return (IUndoAction) redoAction1;
  }
}
