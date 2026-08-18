// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.Undo.UndoManager
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model.Undo;

public class UndoManager : IUndoManager
{
  private readonly ImDocumentEditorFormBase form;
  private int lockCount;
  private int multyActionCreationCount;
  private UndoMultyAction currentMultyUndo;
  private readonly List<IUndoAction> actions = new List<IUndoAction>();
  private readonly List<IUndoAction> redoActions = new List<IUndoAction>();
  public static bool CloneMode = true;

  /// <summary>Конструктор</summary>
  /// <param name="node">верхний узел в котором находится менеджер</param>
  public UndoManager(ImDocumentEditorFormBase form) => this.form = form;

  public void Clear()
  {
    this.actions.Clear();
    this.redoActions.Clear();
    this.currentMultyUndo = (UndoMultyAction) null;
  }

  public bool MultyActionCreation => this.multyActionCreationCount > 0;

  public List<IUndoAction> Actions => this.actions;

  public List<IUndoAction> RedoActions => this.redoActions;

  private void SetId(string oldVal, string newVal)
  {
    if (string.IsNullOrEmpty(oldVal))
      return;
    foreach (IUndoAction action in this.actions)
      action.IdChanged(oldVal, newVal);
    foreach (IUndoAction redoAction in this.redoActions)
      redoAction.IdChanged(oldVal, newVal);
  }

  public void LockUndo() => ++this.lockCount;

  public void UnlockUndo()
  {
    if (this.lockCount <= 0)
      return;
    --this.lockCount;
  }

  private void UpdateCommands()
  {
    if (this.form.CommandManager == null)
      return;
    this.form.BeginQuery();
    try
    {
      ICommandState command1 = this.form.CommandManager.FindCommand("Undo");
      if (command1 != null)
        this.form.CommandManager.QueryStatus(command1);
      ICommandState command2 = this.form.CommandManager.FindCommand("Redo");
      if (command2 == null)
        return;
      this.form.CommandManager.QueryStatus(command2);
    }
    finally
    {
      this.form.EndQuery();
    }
  }

  private void OnUndoAdded()
  {
    this.redoActions.Clear();
    if (this.currentMultyUndo != null)
      return;
    this.UpdateCommands();
  }

  /// <summary>Начать создание сложного Undo</summary>
  /// <param name="caption">заголовок , если равен "" используется заголовок первого undo в списке</param>
  /// <returns></returns>
  public IUndoAction BeginCreateMultyUndo(string caption)
  {
    if (this.IsLocked)
      return (IUndoAction) null;
    if (!this.MultyActionCreation)
    {
      this.currentMultyUndo = new UndoMultyAction((IUndoManager) this, caption);
      LogManager.AddLine(string.Format(LocalizationHolder.rm.GetString("Document.Model_629"), (object) this.currentMultyUndo.ToString()));
    }
    ++this.multyActionCreationCount;
    return (IUndoAction) this.currentMultyUndo;
  }

  /// <summary>Начать создание сложного Undo</summary>
  /// <param name="caption">заголовок , если равен "" используется заголовок первого undo в списке</param>
  /// <returns></returns>
  public IUndoAction BeginCreateMultyUndo(string caption, List<IUndoAction> actions)
  {
    IUndoAction multyUndo = this.BeginCreateMultyUndo(caption);
    foreach (IUndoAction action in actions)
    {
      if (this.Actions.Contains(action))
        this.Actions.Remove(action);
      this.CreateUndo(action, false);
    }
    return multyUndo;
  }

  public IUndoAction EndCreateMultyUndo()
  {
    IUndoAction currentMultyUndo = (IUndoAction) this.currentMultyUndo;
    if (this.IsLocked)
      return (IUndoAction) null;
    if (this.currentMultyUndo != null)
    {
      --this.multyActionCreationCount;
      if (!this.MultyActionCreation)
      {
        if (this.currentMultyUndo.Actions.Count > 0 && !this.IsLocked)
          this.actions.Add((IUndoAction) this.currentMultyUndo);
        LogManager.AddLine(string.Format(LocalizationHolder.rm.GetString("Document.Model_630"), (object) this.currentMultyUndo.ToString()));
        this.currentMultyUndo = (UndoMultyAction) null;
        this.UpdateCommands();
      }
    }
    return currentMultyUndo;
  }

  public IUndoAction CreateUndo(IUndoAction action, bool ignoreLock)
  {
    if (this.IsLocked && !ignoreLock)
      return (IUndoAction) null;
    LogManager.AddLine($"{action}");
    this.AddAction(action);
    this.OnUndoAdded();
    return action;
  }

  private DocumentTreeNode GetClone(string id)
  {
    DocumentTreeNode clone = (DocumentTreeNode) null;
    DocumentTreeNode documentTreeNode = this.Document.FindNode(id);
    switch (documentTreeNode)
    {
      case null:
      case Page _:
      case ImDocument _:
        return clone;
      default:
        if (this.Document.IsTemplate)
        {
          TableElement tableElement = (TableElement) null;
          if (documentTreeNode is RectangleElement rectangleElement && rectangleElement.ParentCell != null)
            tableElement = rectangleElement.ParentCell.TopLevelTable as TableElement;
          if (tableElement != null)
            documentTreeNode = (DocumentTreeNode) tableElement;
        }
        clone = documentTreeNode.Clone(true, true);
        goto case null;
    }
  }

  private void AddAction(IUndoAction action)
  {
    if (action is ICloneAction cloneAction && UndoManager.CloneMode)
    {
      DocumentTreeNode clone = this.GetClone(cloneAction.NodeId);
      cloneAction.Clone = clone;
    }
    if (this.MultyActionCreation)
      this.currentMultyUndo.Actions.Add(action);
    else
      this.actions.Add(action);
  }

  public IUndoAction CreateUndo(DocumentTreeNode obj, string propertyName)
  {
    return this.CreateUndo((object) obj, propertyName, (object) null, (object) null);
  }

  public IUndoAction CreateUndo(object obj, string propertyName, object oldValue, object newValue)
  {
    if (propertyName == "Id")
      this.SetId((string) oldValue, (string) newValue);
    if (this.IsLocked)
      return (IUndoAction) null;
    switch (obj)
    {
      case DocumentTreeNode documentTreeNode when documentTreeNode.IsVirtualNode:
        return (IUndoAction) null;
      case PageElementNode pageElementNode:
        PageData page = pageElementNode.Page;
        if (page != null && this.form.Document == page.OwnerDocument && this.form.Document.pageThreadStatus != null && this.form.Document.pageThreadStatus.StartDistributingPage == page.Index)
          return (IUndoAction) null;
        break;
    }
    IUndoAction action = !(obj is DocumentTreeNode) ? (IUndoAction) new UndoObjectPropertyAction((IUndoManager) this, obj, propertyName, oldValue, newValue) : (IUndoAction) new UndoPropertyChangedAction((IUndoManager) this, obj as DocumentTreeNode, propertyName, oldValue, newValue);
    LogManager.AddLine(string.Format(LocalizationHolder.rm.GetString("Document.Model_631"), (object) action.ToString(), oldValue != null ? (object) oldValue.ToString() : (object) "NULL", newValue != null ? (object) newValue.ToString() : (object) "NULL"));
    this.AddAction(action);
    this.OnUndoAdded();
    return action;
  }

  public IUndoAction CreateUndo(DocumentTreeNode parent, DocumentTreeNode child, int removeIndex)
  {
    if (this.IsLocked)
      return (IUndoAction) null;
    UndoRemoveAction action = new UndoRemoveAction((IUndoManager) this, parent, child, removeIndex);
    LogManager.AddLine($"{action}");
    this.AddAction((IUndoAction) action);
    this.OnUndoAdded();
    return (IUndoAction) action;
  }

  public IUndoAction CreateUndo(DocumentTreeNode parent, DocumentTreeNode addNode)
  {
    if (this.IsLocked)
      return (IUndoAction) null;
    UndoAddAction action = new UndoAddAction((IUndoManager) this, parent, addNode);
    LogManager.AddLine($"{action}");
    this.AddAction((IUndoAction) action);
    this.OnUndoAdded();
    return (IUndoAction) action;
  }

  public IUndoAction CreateUndo(DocumentTreeNode parent, int oldPos, int newPos, bool exchanged)
  {
    if (this.IsLocked)
      return (IUndoAction) null;
    UndoPositionChangedAction action = new UndoPositionChangedAction((IUndoManager) this, parent, oldPos, newPos, exchanged);
    LogManager.AddLine($"{action}");
    this.AddAction((IUndoAction) action);
    this.OnUndoAdded();
    return (IUndoAction) action;
  }

  public bool DoUndo()
  {
    if (!this.CanUndo())
      return false;
    this.LockUndo();
    try
    {
      IUndoAction action = this.actions[this.actions.Count - 1];
      action.DoAction();
      this.actions.RemoveAt(this.actions.Count - 1);
      this.redoActions.Add(action);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    finally
    {
      this.UnlockUndo();
    }
    this.form.DocumentManager?.UpdateSelectedElementInfo();
    this.UpdateCommands();
    return true;
  }

  public bool IsLocked
  {
    get
    {
      if (this.lockCount > 0)
        return true;
      VisualNode document = this.Document;
      return document == null || document.IsUndoLocked();
    }
  }

  public VisualNode Document
  {
    get
    {
      return this.form.DocumentsComplect != null ? (VisualNode) this.form.DocumentsComplect : (VisualNode) this.form.Document;
    }
  }

  public Control Form => (Control) this.form;

  public bool CanUndo() => this.actions.Count > 0;

  public bool CanRedo() => this.redoActions.Count > 0;

  public bool DoRedo()
  {
    if (this.IsLocked || !this.CanRedo())
      return false;
    this.LockUndo();
    try
    {
      IUndoAction redoAction = this.redoActions[this.redoActions.Count - 1].CreateRedoAction();
      redoAction?.DoAction();
      this.redoActions.RemoveAt(this.redoActions.Count - 1);
      this.actions.Add(redoAction?.CreateRedoAction());
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    finally
    {
      this.UnlockUndo();
    }
    this.form.DocumentManager?.UpdateSelectedElementInfo();
    this.UpdateCommands();
    return true;
  }
}
