// Decompiled with JetBrains decompiler
// Type: Intermech.Windows.Forms.SimpleBaseUserControl
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Controls;
using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Windows.Forms;

[Designer(typeof (AdvParentControlDesigner))]
[CLSCompliant(false)]
public class SimpleBaseUserControl : 
  UserControl,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl,
  IDesignModeControlsContainer,
  IArrowKeysNavigationSupported,
  ILastFocusedControlTracker
{
  [NotNull]
  private readonly object _syncObj = new object();
  [NotNull]
  protected readonly object _SyncObj = new object();
  private bool? _inDesignMode;
  [CanBeNull]
  private Control _upControl;
  [CanBeNull]
  private Control _downControl;
  [CanBeNull]
  private Control _leftControl;
  [CanBeNull]
  private Control _rightControl;
  [CanBeNull]
  private LastFocusedControlTracker _lastFocusedControlTracker;

  [CanBeNull]
  protected virtual List<(Control DesignModeControl, string FieldName)> GetDesignModeChildControls()
  {
    return (List<(Control, string)>) null;
  }

  [CanBeNull]
  List<(Control DesignModeControl, string FieldName)> IDesignModeControlsContainer.GetDesignModeChildControls()
  {
    return this.GetDesignModeChildControls();
  }

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

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void CheckNotDisposed() => Intermech.Diagnostics.Check.NotDisposed(this.IsDisposed, this.GetType().Name);

  protected virtual bool ReadyToProcessFirstPaint => true;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool FirstPaintWasCalled { get; private set; }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public event EventHandler OnFirstPaint;

  protected virtual void FireFirstPaint()
  {
    EventHandler onFirstPaint = this.OnFirstPaint;
    if (onFirstPaint == null)
      return;
    onFirstPaint((object) this, EventArgs.Empty);
  }

  protected override void WndProc(ref Message m)
  {
    base.WndProc(ref m);
    if (m.Msg != 15 || this.FirstPaintWasCalled || !this.ReadyToProcessFirstPaint)
      return;
    lock (this._syncObj)
    {
      if (this.FirstPaintWasCalled || !this.ReadyToProcessFirstPaint)
        return;
      this.FirstPaintWasCalled = true;
      this.FireFirstPaint();
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(null)]
  [Category("Navigation")]
  public Control UpControl
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._upControl;
    set
    {
      if (this._upControl == value)
        return;
      Control upControl = this._upControl;
      if (value != null)
        value.Disposed += new EventHandler(this.UpControl_Disposed);
      if (this._upControl != null)
        this._upControl.Disposed -= new EventHandler(this.UpControl_Disposed);
      this._upControl = value;
      if (value is IArrowKeysNavigationSupported navigationSupported1 && navigationSupported1.DownControl == null)
        navigationSupported1.DownControl = (Control) this;
      if (upControl == null || upControl.IsDisposed || !(upControl is IArrowKeysNavigationSupported navigationSupported2) || navigationSupported2.DownControl != this)
        return;
      navigationSupported2.DownControl = (Control) null;
    }
  }

  private void UpControl_Disposed([NotNull] object sender, [NotNull] EventArgs e)
  {
    if (this._upControl == null)
      return;
    this._upControl.Disposed -= new EventHandler(this.UpControl_Disposed);
    this._upControl = (Control) null;
  }

  [Category("Navigation")]
  public event OnNavigateDelegate OnNavigateToUp;

  public virtual void NavigateToUp()
  {
    if (this.OnNavigateToLeft == null && this._upControl == null && this.Parent != null)
      this.Parent.SelectNextControl((Control) this, false, true, true, true);
    bool blockDefaultNavigation = false;
    OnNavigateDelegate onNavigateToUp = this.OnNavigateToUp;
    if (onNavigateToUp != null)
      onNavigateToUp((IArrowKeysNavigationSupported) this, ref blockDefaultNavigation);
    if (this._upControl == null || blockDefaultNavigation || !this._upControl.CanFocus)
      return;
    this._upControl.Focus();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(null)]
  [Category("Navigation")]
  public Control DownControl
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._downControl;
    }
    set
    {
      if (this._downControl == value)
        return;
      Control downControl = this._downControl;
      if (value != null)
        value.Disposed += new EventHandler(this.DownControl_Disposed);
      if (this._downControl != null)
        this._downControl.Disposed -= new EventHandler(this.DownControl_Disposed);
      this._downControl = value;
      if (value is IArrowKeysNavigationSupported navigationSupported1 && navigationSupported1.UpControl == null)
        navigationSupported1.UpControl = (Control) this;
      if (downControl == null || downControl.IsDisposed || !(downControl is IArrowKeysNavigationSupported navigationSupported2) || navigationSupported2.UpControl != this)
        return;
      navigationSupported2.UpControl = (Control) null;
    }
  }

  private void DownControl_Disposed([NotNull] object sender, [NotNull] EventArgs e)
  {
    if (this._downControl == null)
      return;
    this._downControl.Disposed -= new EventHandler(this.DownControl_Disposed);
    this._downControl = (Control) null;
  }

  [Category("Navigation")]
  public event OnNavigateDelegate OnNavigateToDown;

  public virtual void NavigateToDown()
  {
    if (this.OnNavigateToLeft == null && this._upControl == null && this.Parent != null)
      this.Parent.SelectNextControl((Control) this, true, true, true, true);
    bool blockDefaultNavigation = false;
    OnNavigateDelegate onNavigateToDown = this.OnNavigateToDown;
    if (onNavigateToDown != null)
      onNavigateToDown((IArrowKeysNavigationSupported) this, ref blockDefaultNavigation);
    if (this._downControl == null || blockDefaultNavigation || !this._downControl.CanFocus)
      return;
    this._downControl.Focus();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(null)]
  [Category("Navigation")]
  public Control LeftControl
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._leftControl;
    }
    set
    {
      if (this._leftControl == value)
        return;
      Control leftControl = this._leftControl;
      if (value != null)
        value.Disposed += new EventHandler(this.LeftControl_Disposed);
      if (this._leftControl != null)
        this._leftControl.Disposed -= new EventHandler(this.LeftControl_Disposed);
      this._leftControl = value;
      if (value is IArrowKeysNavigationSupported navigationSupported1 && navigationSupported1.RightControl == null)
        navigationSupported1.RightControl = (Control) this;
      if (leftControl == null || leftControl.IsDisposed || !(leftControl is IArrowKeysNavigationSupported navigationSupported2) || navigationSupported2.RightControl != this)
        return;
      navigationSupported2.RightControl = (Control) null;
    }
  }

  private void LeftControl_Disposed([NotNull] object sender, [NotNull] EventArgs e)
  {
    if (this._leftControl == null)
      return;
    this._leftControl.Disposed -= new EventHandler(this.LeftControl_Disposed);
    this._leftControl = (Control) null;
  }

  [Category("Navigation")]
  public event OnNavigateDelegate OnNavigateToLeft;

  public virtual void NavigateToLeft()
  {
    if (this.OnNavigateToLeft == null && this._upControl == null && this.Parent != null)
      this.Parent.SelectNextControl((Control) this, false, true, true, true);
    bool blockDefaultNavigation = false;
    OnNavigateDelegate onNavigateToLeft = this.OnNavigateToLeft;
    if (onNavigateToLeft != null)
      onNavigateToLeft((IArrowKeysNavigationSupported) this, ref blockDefaultNavigation);
    if (this._leftControl == null || blockDefaultNavigation || !this._leftControl.CanFocus)
      return;
    this._leftControl.Focus();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(null)]
  [Category("Navigation")]
  public Control RightControl
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._rightControl;
    }
    set
    {
      if (this._rightControl == value)
        return;
      Control rightControl = this._rightControl;
      if (value != null)
        value.Disposed += new EventHandler(this.RightControl_Disposed);
      if (this._rightControl != null)
        this._rightControl.Disposed -= new EventHandler(this.RightControl_Disposed);
      this._rightControl = value;
      if (value is IArrowKeysNavigationSupported navigationSupported1 && navigationSupported1.LeftControl == null)
        navigationSupported1.LeftControl = (Control) this;
      if (rightControl == null || rightControl.IsDisposed || !(rightControl is IArrowKeysNavigationSupported navigationSupported2) || navigationSupported2.LeftControl != this)
        return;
      navigationSupported2.LeftControl = (Control) null;
    }
  }

  private void RightControl_Disposed([NotNull] object sender, [NotNull] EventArgs e)
  {
    if (this._rightControl == null)
      return;
    this._rightControl.Disposed -= new EventHandler(this.RightControl_Disposed);
    this._rightControl = (Control) null;
  }

  [Category("Navigation")]
  public event OnNavigateDelegate OnNavigateToRight;

  public virtual void NavigateToRight()
  {
    if (this.OnNavigateToLeft == null && this._upControl == null && this.Parent != null)
      this.Parent.SelectNextControl((Control) this, true, true, true, true);
    bool blockDefaultNavigation = false;
    OnNavigateDelegate onNavigateToRight = this.OnNavigateToRight;
    if (onNavigateToRight != null)
      onNavigateToRight((IArrowKeysNavigationSupported) this, ref blockDefaultNavigation);
    if (this._rightControl == null || blockDefaultNavigation || !this._rightControl.CanFocus)
      return;
    this._rightControl.Focus();
  }

  protected override bool IsInputKey(Keys keyData)
  {
    switch (keyData)
    {
      case Keys.Left:
      case Keys.Left | Keys.Shift:
      case Keys.Left | Keys.Control:
        return this._leftControl != null || this.OnNavigateToLeft != null;
      case Keys.Up:
      case Keys.Up | Keys.Shift:
      case Keys.Up | Keys.Control:
        return this._upControl != null || this.OnNavigateToUp != null;
      case Keys.Right:
      case Keys.Right | Keys.Shift:
      case Keys.Right | Keys.Control:
        return this._rightControl != null || this.OnNavigateToRight != null;
      case Keys.Down:
      case Keys.Down | Keys.Shift:
      case Keys.Down | Keys.Control:
        return this._downControl != null || this.OnNavigateToDown != null;
      default:
        return base.IsInputKey(keyData);
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(false)]
  public bool TrackLastFocusedChildControl
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._lastFocusedControlTracker != null;
    }
    set
    {
      if (value == this.TrackLastFocusedChildControl)
        return;
      if (value)
      {
        this._lastFocusedControlTracker = new LastFocusedControlTracker((ContainerControl) this, this.InDesignMode);
      }
      else
      {
        if (this._lastFocusedControlTracker == null)
          return;
        this._lastFocusedControlTracker.Dispose();
        this._lastFocusedControlTracker = (LastFocusedControlTracker) null;
      }
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [CanBeNull]
  public Control LastFocusedChildControl
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (this.InDesignMode)
        return (Control) null;
      return this._lastFocusedControlTracker != null ? this._lastFocusedControlTracker.LastActiveControl : throw new Exception("TrackLastFocusedChildControl is false, LastFocusedChildControl tracking is disabled");
    }
  }
}
