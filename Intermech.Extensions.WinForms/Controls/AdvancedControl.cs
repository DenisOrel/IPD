// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.AdvancedControl
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Extensions;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Controls;

public class AdvancedControl : 
  Control,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent
{
  private bool? _inDesignMode;

  protected bool InDesignMode
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (this._inDesignMode ?? (this._inDesignMode = new bool?(this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime || this.GetParentsEnumeration(true).Any<Control>((Func<Control, bool>) (ctrl =>
      {
        ISite site = ctrl.Site;
        return site != null && site.DesignMode;
      }))))).Value;
    }
  }

  protected virtual bool ReadyToShow => true;

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
    if (m.Msg != 15 || this.Shown || !this.ReadyToShow)
      return;
    this.Shown = true;
    this.FireAfterShown();
  }
}
