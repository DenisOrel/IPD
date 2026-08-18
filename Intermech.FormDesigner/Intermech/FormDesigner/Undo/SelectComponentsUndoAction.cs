// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Undo.SelectComponentsUndoAction
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System.Collections;
using System.ComponentModel.Design;

#nullable disable
namespace Intermech.FormDesigner.Undo;

/// <summary>
/// 
/// </summary>
internal class SelectComponentsUndoAction : IUndoableOperation
{
  private IDesignerHost _host;
  private ArrayList _oldComponentNames;
  private ArrayList _newComponentNames;

  /// <summary>Конструктор.</summary>
  /// <param name="host"></param>
  /// <param name="oldComponentNames"></param>
  public SelectComponentsUndoAction(IDesignerHost host, ArrayList oldComponentNames)
  {
    this._host = host;
    this._oldComponentNames = oldComponentNames;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="newComponentNames"></param>
  public void SetNewSelection(ArrayList newComponentNames)
  {
    this._newComponentNames = newComponentNames;
  }

  /// <summary>
  /// 
  /// </summary>
  public void Undo()
  {
    UndoHandler.SetSelectedComponentsPerName(this._host, this._oldComponentNames);
  }

  /// <summary>
  /// 
  /// </summary>
  public void Redo()
  {
    UndoHandler.SetSelectedComponentsPerName(this._host, this._newComponentNames);
  }
}
