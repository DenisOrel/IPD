// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.AdvancedUserControl
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Controls;

public class AdvancedUserControl : 
  UserControl,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl
{
  [NotNull]
  protected readonly object SyncObj = new object();
  [CanBeNull]
  private bool? _inDesignMode;

  protected bool InDesignMode
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (this._inDesignMode ?? (this._inDesignMode = new bool?(this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime || this.GetParents(true).Any<Control>((Func<Control, bool>) (ctrl =>
      {
        ISite site = ctrl.Site;
        return site != null && site.DesignMode;
      }))))).Value;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool Shown { get; private set; }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public event EventHandler AfterShown;

  protected virtual void FireAfterShown()
  {
    EventHandler afterShown = this.AfterShown;
    if (afterShown == null)
      return;
    afterShown((object) this, EventArgs.Empty);
  }

  protected override void WndProc(ref Message m)
  {
    base.WndProc(ref m);
    if (m.Msg != 15 || this.Shown)
      return;
    lock (this.SyncObj)
    {
      if (this.Shown)
        return;
      this.Shown = true;
      this.FireAfterShown();
    }
  }

  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private IEnumerable<Control> GetParents(bool includeThis = false)
  {
    Control control = includeThis ? (Control) this : this.Parent;
    while (true)
    {
      Control control1 = control;
      if ((control1 != null ? (!control1.IsDisposed ? 1 : 0) : 0) != 0)
      {
        yield return control;
        control = control.Parent;
      }
      else
        break;
    }
  }
}
