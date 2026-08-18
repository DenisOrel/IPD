// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.LastFocusedControlTracker
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.Controls;

public class LastFocusedControlTracker : IDisposable
{
  private readonly ContainerControl _owner;
  [NotNull]
  private List<Control> _hookedControls = new List<Control>();
  private Control _lastActiveControl;

  public LastFocusedControlTracker([NotNull] ContainerControl owner, bool designMode)
  {
    this._owner = owner;
    if (designMode)
      return;
    this.HookChildControls(owner.Controls);
    this._lastActiveControl = this._owner.ActiveControl;
  }

  public void Dispose()
  {
    this.UnHookAllChildControls();
    this._hookedControls = (List<Control>) null;
  }

  private void HookControl([NotNull] Control control)
  {
    if (!control.HasChildren)
      control.Leave += new EventHandler(this.childControl_Leave);
    control.Disposed += new EventHandler(this.childControl_Disposed);
    control.ControlAdded += new ControlEventHandler(this.childControl_ControlAdded);
    control.ControlRemoved += new ControlEventHandler(this.control_ControlRemoved);
    this._hookedControls.Add(control);
    this.HookChildControls(control.Controls);
  }

  private void UnHookControl([NotNull] Control control, bool removeFromList = true, bool recursive = false)
  {
    control.Disposed -= new EventHandler(this.childControl_Disposed);
    control.Leave -= new EventHandler(this.childControl_Leave);
    control.ControlAdded -= new ControlEventHandler(this.childControl_ControlAdded);
    control.ControlRemoved -= new ControlEventHandler(this.control_ControlRemoved);
    if (removeFromList)
      this._hookedControls.Remove(control);
    if (!recursive)
      return;
    this.UnHookChildControls(control.Controls);
  }

  private void HookChildControls([CanBeNull, ItemNotNull] Control.ControlCollection childControls)
  {
    if (childControls == null)
      return;
    foreach (Control childControl in (ArrangedElementCollection) childControls)
      this.HookControl(childControl);
  }

  private void UnHookChildControls(
    [CanBeNull, ItemNotNull] Control.ControlCollection childControls,
    bool removeFromList = true,
    bool recursive = false)
  {
    if (childControls == null)
      return;
    foreach (Control childControl in (ArrangedElementCollection) childControls)
      this.UnHookControl(childControl, removeFromList, recursive);
  }

  private void UnHookAllChildControls()
  {
    foreach (Control hookedControl in this._hookedControls)
      this.UnHookControl(hookedControl, false);
    this._hookedControls.Clear();
  }

  private void childControl_ControlAdded([CanBeNull] object sender, [NotNull] ControlEventArgs e)
  {
    if (!(sender is Control control))
      return;
    this.HookControl(control);
    if (control.Parent == null)
      return;
    control.Parent.Leave -= new EventHandler(this.childControl_Leave);
  }

  private void control_ControlRemoved([CanBeNull] object sender, [NotNull] ControlEventArgs e)
  {
    if (!(sender is Control control))
      return;
    this.UnHookControl(control, recursive: true);
    Control parent = control.Parent;
    if ((parent != null ? (!parent.HasChildren ? 1 : 0) : 0) == 0)
      return;
    control.Parent.Leave += new EventHandler(this.childControl_Leave);
  }

  private void childControl_Leave([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this._lastActiveControl = (Control) sender;
  }

  private void childControl_Disposed([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (!(sender is Control control))
      return;
    this.UnHookControl(control);
  }

  [CanBeNull]
  public Control LastActiveControl => this._owner?.ActiveControl ?? this._lastActiveControl;
}
