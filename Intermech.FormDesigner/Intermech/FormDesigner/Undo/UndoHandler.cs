// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Undo.UndoHandler
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Undo;

/// <summary>
/// 
/// </summary>
internal class UndoHandler
{
  private IDesignerHost _host;
  private Hashtable _sizePos = new Hashtable();
  private UndoStack _undoStack = new UndoStack();
  private bool _inUndoRedo;
  private int _transactionLevel;
  private int _undoOperations;

  /// <summary>
  /// 
  /// </summary>
  public bool EnableUndo => this._undoStack.CanUndo;

  /// <summary>
  /// 
  /// </summary>
  public bool EnableRedo => this._undoStack.CanRedo;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="host"></param>
  /// <returns></returns>
  public static ArrayList GetSelectedComponentNames(IDesignerHost host)
  {
    ArrayList selectedComponentNames = new ArrayList();
    if (host.GetService(typeof (ISelectionService)) is ISelectionService service)
    {
      foreach (IComponent selectedComponent in (IEnumerable) service.GetSelectedComponents())
      {
        if (selectedComponent.Site != null)
          selectedComponentNames.Add((object) selectedComponent.Site.Name);
        else
          (host.GetService(typeof (IMessageService)) as IMessageService).ShowError($"{selectedComponent} has no site.");
      }
    }
    return selectedComponentNames;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="host"></param>
  /// <param name="names"></param>
  public static void SetSelectedComponentsPerName(IDesignerHost host, ArrayList names)
  {
    ArrayList components = new ArrayList();
    foreach (string name in names)
    {
      if (host.Container.Components[name] != null)
        components.Add((object) host.Container.Components[name]);
      else
        ((IMessageService) host.GetService(typeof (IMessageService))).ShowError($"Can't select component : Component {name} not found.");
    }
    if (!(host.GetService(typeof (ISelectionService)) is ISelectionService service))
      return;
    service.SetSelectedComponents((ICollection) components, SelectionTypes.Replace);
  }

  /// <summary>
  /// 
  /// </summary>
  public void Reset() => this._undoStack.ClearAll();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="host"></param>
  public void Attach(IDesignerHost host)
  {
    this._host = host;
    if (host.GetService(typeof (IComponentChangeService)) is IComponentChangeService service)
    {
      service.ComponentChanged += new ComponentChangedEventHandler(this.ComponentChanged);
      service.ComponentAdded += new ComponentEventHandler(this.ComponentAdded);
      service.ComponentRemoved += new ComponentEventHandler(this.ComponentRemoved);
    }
    host.TransactionOpened += new EventHandler(this.TransactionOpened);
    host.TransactionClosed += new DesignerTransactionCloseEventHandler(this.TransactionClosed);
  }

  /// <summary>
  /// 
  /// </summary>
  public void Detach()
  {
    if (this._host.GetService(typeof (IComponentChangeService)) is IComponentChangeService service)
    {
      service.ComponentChanged -= new ComponentChangedEventHandler(this.ComponentChanged);
      service.ComponentAdded -= new ComponentEventHandler(this.ComponentAdded);
      service.ComponentRemoved -= new ComponentEventHandler(this.ComponentRemoved);
    }
    this._host.TransactionOpened -= new EventHandler(this.TransactionOpened);
    this._host.TransactionClosed -= new DesignerTransactionCloseEventHandler(this.TransactionClosed);
  }

  /// <summary>
  /// 
  /// </summary>
  public void Undo()
  {
    this._inUndoRedo = true;
    try
    {
      this._undoStack.Undo();
    }
    catch (Exception ex)
    {
      string message = $"UndoException : {ex.Message}";
      if (this._host.GetService(typeof (IMessageService)) is IMessageService service)
        service.ShowError(message);
      Console.WriteLine(message);
    }
    finally
    {
      this._inUndoRedo = false;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public void Redo()
  {
    this._inUndoRedo = true;
    try
    {
      this._undoStack.Redo();
    }
    catch (Exception ex)
    {
      string message = $"UndoException : {ex.Message}";
      if (this._host.GetService(typeof (IMessageService)) is IMessageService service)
        service.ShowError(message);
      Console.WriteLine(message);
    }
    finally
    {
      this._inUndoRedo = false;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void InitSizePosTable()
  {
    this._sizePos.Clear();
    foreach (IComponent component in (ReadOnlyCollectionBase) this._host.Container.Components)
    {
      if (component is Control control && component.Site != null)
        this._sizePos[(object) component.Site.Name] = (object) new object[2]
        {
          (object) control.Location,
          (object) control.Size
        };
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="ea"></param>
  private void ComponentChanged(object sender, ComponentChangedEventArgs ea)
  {
    if (this._inUndoRedo || !(ea.Component is IComponent component) || component.Site == null || !this._sizePos.ContainsKey((object) component.Site.Name))
      return;
    if (ea.Member != null && this._sizePos[(object) component.Site.Name] != null)
    {
      if (ea.Member.Name == "Location")
      {
        if (((object[]) this._sizePos[(object) component.Site.Name])[0].Equals(ea.NewValue))
          return;
        ea = new ComponentChangedEventArgs(ea.Component, ea.Member, ((object[]) this._sizePos[(object) component.Site.Name])[0], ea.NewValue);
      }
      else if (ea.Member.Name == "Size")
        ea = new ComponentChangedEventArgs(ea.Component, ea.Member, ((object[]) this._sizePos[(object) component.Site.Name])[1], ea.NewValue);
      else if (ea.Member.Name == "Controls")
        return;
    }
    ++this._undoOperations;
    this._undoStack.Push((IUndoableOperation) new ComponentChangedUndoAction(this._host, ea));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="ea"></param>
  private void ComponentAdded(object sender, ComponentEventArgs ea)
  {
    if (this._inUndoRedo)
      return;
    ++this._undoOperations;
    this._undoStack.Push((IUndoableOperation) new ComponentAddedUndoAction(this._host, ea));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="ea"></param>
  private void ComponentRemoved(object sender, ComponentEventArgs ea)
  {
    if (this._inUndoRedo)
      return;
    string empty = string.Empty;
    if (!(ea.Component is Control component) || component.Parent == null)
      return;
    string name = component.Parent.Name;
    ++this._undoOperations;
    this._undoStack.Push((IUndoableOperation) new ComponentRemovedUndoAction(this._host, ea, name));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TransactionOpened(object sender, EventArgs e)
  {
    if (this._transactionLevel == 0)
    {
      this._undoOperations = 0;
      this.InitSizePosTable();
    }
    ++this._transactionLevel;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TransactionClosed(object sender, DesignerTransactionCloseEventArgs e)
  {
    --this._transactionLevel;
    if (this._transactionLevel != 0 || this._undoOperations <= 0)
      return;
    this._undoStack.UndoLast(this._undoOperations);
  }
}
