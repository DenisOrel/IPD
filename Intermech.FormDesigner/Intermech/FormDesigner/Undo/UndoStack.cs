// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Undo.UndoStack
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using ImSSP;
using System;
using System.Collections;

#nullable disable
namespace Intermech.FormDesigner.Undo;

/// <summary>This class implements an undo stack</summary>
internal class UndoStack
{
  public bool AcceptChanges = true;
  private Stack _undostack = new Stack();
  private Stack _redostack = new Stack();

  /// <summary>
  /// This property is EXCLUSIVELY for the UndoQueue class, don't USE it.
  /// </summary>
  internal Stack _UndoStack => this._undostack;

  /// <summary>
  /// 
  /// </summary>
  public bool CanUndo => this._undostack.Count > 0;

  /// <summary>
  /// 
  /// </summary>
  public bool CanRedo => this._redostack.Count > 0;

  public event EventHandler ActionUndone;

  public event EventHandler ActionRedone;

  /// <summary>
  /// You call this method to pool the last x operations from the undo stack to make 1 operation from it.
  /// </summary>
  public void UndoLast(int x) => this._undostack.Push((object) new UndoQueue(this, x));

  /// <summary>
  /// Call this method to undo the last operation on the stack.
  /// </summary>
  public void Undo()
  {
    if (this._undostack.Count <= 0)
      return;
    IUndoableOperation undoableOperation = this._undostack.Pop() as IUndoableOperation;
    this._redostack.Push((object) undoableOperation);
    undoableOperation.Undo();
    this.OnActionUndone();
  }

  /// <summary>Call this method to redo the last undone operation.</summary>
  public void Redo()
  {
    if (this._redostack.Count <= 0)
      return;
    IUndoableOperation undoableOperation = this._redostack.Pop() as IUndoableOperation;
    this._undostack.Push((object) undoableOperation);
    undoableOperation.Redo();
    this.OnActionRedone();
  }

  /// <summary>
  /// Call this method to push an UndoableOperation on the undostack, the redostack will be cleared, if you use this method.
  /// </summary>
  public void Push(IUndoableOperation operation)
  {
    if (operation == null)
      throw new ArgumentNullException(sc_7192.ssp_imclient_7193());
    if (!this.AcceptChanges)
      return;
    this._undostack.Push((object) operation);
    this.ClearRedoStack();
  }

  /// <summary>
  /// Call this method, if you want to clear the redo stack.
  /// </summary>
  public void ClearRedoStack() => this._redostack.Clear();

  /// <summary>
  /// 
  /// </summary>
  public void ClearAll()
  {
    this._undostack.Clear();
    this._redostack.Clear();
  }

  /// <summary>
  /// 
  /// </summary>
  protected void OnActionUndone()
  {
    if (this.ActionUndone == null)
      return;
    this.ActionUndone((object) null, (EventArgs) null);
  }

  /// <summary>
  /// 
  /// </summary>
  protected void OnActionRedone()
  {
    if (this.ActionRedone == null)
      return;
    this.ActionRedone((object) null, (EventArgs) null);
  }
}
