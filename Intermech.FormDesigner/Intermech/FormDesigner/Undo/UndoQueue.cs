// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Undo.UndoQueue
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using ImSSP;
using System;
using System.Collections;

#nullable disable
namespace Intermech.FormDesigner.Undo;

/// <summary>
/// This class stacks the last x operations from the undostack and makes one undo/redo operation from it.
/// </summary>
internal class UndoQueue : IUndoableOperation
{
  private ArrayList _undolist = new ArrayList();

  /// <summary>Конструктор.</summary>
  /// <param name="stack"></param>
  /// <param name="numops"></param>
  public UndoQueue(UndoStack stack, int numops)
  {
    if (stack == null)
      throw new ArgumentNullException(sc_7190.ssp_imclient_7191());
    for (int index = 0; index < numops; ++index)
    {
      if (stack._UndoStack.Count != 0)
        this._undolist.Add(stack._UndoStack.Pop());
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public void Undo()
  {
    for (int index = 0; index < this._undolist.Count; ++index)
      (this._undolist[index] as IUndoableOperation).Undo();
  }

  /// <summary>
  /// 
  /// </summary>
  public void Redo()
  {
    for (int index = this._undolist.Count - 1; index >= 0; --index)
      (this._undolist[index] as IUndoableOperation).Redo();
  }
}
