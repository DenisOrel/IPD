// Decompiled with JetBrains decompiler
// Type: Intermech.Windows.Forms.SimpleBaseForm
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Windows.Forms;

public class SimpleBaseForm : 
  Form,
  IContainerControl,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IComponent,
  IDisposable
{
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected bool InDesignMode
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;
    }
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void CheckNotDisposed() => Intermech.Diagnostics.Check.NotDisposed(this.IsDisposed, this.GetType().Name);

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void CheckNotInDesignMode()
  {
    Intermech.Diagnostics.Check.Assert(this.InDesignMode, "Excecuted context only for design-mode!");
  }

  protected virtual bool ReadyToShow => true;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected bool IsShown { get; private set; }

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
    if (m.Msg != 15 || this.IsShown || !this.ReadyToShow)
      return;
    this.IsShown = true;
    this.FireAfterShown();
  }
}
