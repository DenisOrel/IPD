
// Type: Intermech.Docking.FloatingDockContainer
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using Intermech.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.Docking;

internal class FloatingDockContainer : DockContainer
{
  private const int _a = 64 /*0x40*/;
  private const int _b = 16 /*0x10*/;
  private const int _c = 128 /*0x80*/;
  private const int _d = 4;
  private FloatingForm _floatingForm;
  private ControlLayoutSystem _f;
  private bool _g;

  public FloatingDockContainer()
  {
    this._f = (ControlLayoutSystem) null;
    this._g = true;
    this._floatingForm = new FloatingForm(this);
    this._floatingForm.Activated += new EventHandler(((DockContainer) this).Form_Activated);
    this._floatingForm.Deactivate += new EventHandler(((DockContainer) this).Form_Deactivate);
    this._floatingForm.Closing += new CancelEventHandler(this.Form_Closing);
    this._floatingForm.DoubleClick += new EventHandler(this.Form_DoubleClick);
    this.LayoutSystem.LayoutSystemsChanged += new EventHandler(this.LayoutSystems_Changed);
    this.LayoutSystems_Changed((object) this.LayoutSystem, EventArgs.Empty);
    this._floatingForm.Controls.Add((Control) this);
    this.Dock = DockStyle.Fill;
  }

  public Rectangle GetBounds() => this._floatingForm.Bounds;

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.LayoutSystem.LayoutSystemsChanged -= new EventHandler(this.LayoutSystems_Changed);
      this._floatingForm.Activated -= new EventHandler(((DockContainer) this).Form_Activated);
      this._floatingForm.Deactivate -= new EventHandler(((DockContainer) this).Form_Deactivate);
      this._floatingForm.Closing -= new CancelEventHandler(this.Form_Closing);
      this._floatingForm.DoubleClick -= new EventHandler(this.Form_DoubleClick);
      this._floatingForm.Controls.Remove((Control) this);
      this._floatingForm.Dispose();
    }
    base.Dispose(disposing);
  }

  public void SetLocation(Point A_0) => this._floatingForm.Location = A_0;

  public void SetSize(Size A_0) => this._floatingForm.Size = A_0;

  private void Form_Closing(object A_0, CancelEventArgs A_1)
  {
    if (!this._g)
      return;
    A_1.Cancel = true;
    DockControl[] dockControls = this.GetDockControls();
    foreach (DockControl dockControl in dockControls)
    {
      if (!dockControl.Closable)
        return;
    }
    CancelEventArgs e = new CancelEventArgs();
    foreach (DockControl dockControl in dockControls)
    {
      dockControl.OnClosing(e);
      if (e.Cancel)
        return;
    }
    this.HideForm();
    foreach (DockControl dockControl in dockControls)
      dockControl.OnClosed(EventArgs.Empty);
  }

  private void Form_DoubleClick(object A_0, EventArgs A_1)
  {
    ControlLayoutSystem layoutSystem1 = this.GetLayoutSystem(this.LayoutSystem);
    if (layoutSystem1 == null)
      return;
    DockControl selectedControl = layoutSystem1.SelectedControl;
    if (selectedControl == null)
      return;
    ControlLayoutSystem controlLayoutSystem = selectedControl.LastControlLayoutSystem;
    SplitLayoutSystem layoutSystem2 = this.LayoutSystem;
    this.LayoutSystem = new SplitLayoutSystem();
    this.Manager.DisposeFloatingContainer(this);
    int num = selectedControl.LastIndexInFixedLayoutSystem;
    if (num < 0)
      num = 0;
    if (num > controlLayoutSystem.Controls.Count)
      num = controlLayoutSystem.Controls.Count;
    ControlLayoutSystem layoutSystem3 = controlLayoutSystem;
    int index = num;
    layoutSystem2.MoveToLayoutSystem(layoutSystem3, index);
    selectedControl.Activate();
  }

  private void OnSelectedControlChanged(DockControl A_0, DockControl A_1)
  {
    if (A_1 != null && !A_1._firstShowed && this._f != null)
    {
      this._f.Controls[0].OnBeforeFirstShown(EventArgs.Empty);
      if (this._f != null)
        this._f.Controls[0]._firstShowed = true;
    }
    if (A_1 != null)
      this._floatingForm.Text = A_1.Text;
    else
      this._floatingForm.Text = string.Empty;
  }

  public void SetWindowPos(Rectangle bounds, bool visible, bool activate)
  {
    int num = 0;
    int A_6 = !visible ? num | 128 /*0x80*/ : num | 64 /*0x40*/;
    if (!activate)
      A_6 |= 16 /*0x10*/;
    Win32.SetWindowPos(this._floatingForm.Handle, 0, bounds.X, bounds.Y, bounds.Width, bounds.Height, A_6);
    this._floatingForm.Visible = visible;
    if (!visible)
      return;
    foreach (Control control in (ArrangedElementCollection) this._floatingForm.Controls)
      control.Visible = true;
  }

  public override SplitLayoutSystem LayoutSystem
  {
    get => base.LayoutSystem;
    set
    {
      this.LayoutSystem.LayoutSystemsChanged -= new EventHandler(this.LayoutSystems_Changed);
      base.LayoutSystem = value;
      this.LayoutSystem.LayoutSystemsChanged += new EventHandler(this.LayoutSystems_Changed);
      this.LayoutSystems_Changed((object) this.LayoutSystem, EventArgs.Empty);
    }
  }

  public void b(bool A_0) => this._g = A_0;

  private void LayoutSystems_Changed(object A_0, EventArgs A_1)
  {
    if (this._f != null)
      this._f.SelectedControlChanged -= new ControlLayoutSystem.ControlLayoutSystemEventHandler(this.OnSelectedControlChanged);
    if (this.HasSingleControlLayoutSystem)
    {
      this._f = (ControlLayoutSystem) this.LayoutSystem.LayoutSystems[0];
      this._f.SelectedControlChanged += new ControlLayoutSystem.ControlLayoutSystemEventHandler(this.OnSelectedControlChanged);
      this.OnSelectedControlChanged((DockControl) null, this._f.SelectedControl);
    }
    else
    {
      this._floatingForm.Text = string.Empty;
      this._f = (ControlLayoutSystem) null;
    }
  }

  public void c()
  {
    Win32.ShowWindow(this._floatingForm.Handle, 4);
    foreach (Control control in (ArrangedElementCollection) this._floatingForm.Controls)
      control.Visible = true;
  }

  public void SetVisible(bool A_0) => this._floatingForm.Visible = A_0;

  public override bool IsFloating => true;

  public void e() => this.LayoutSystems_Changed((object) null, (EventArgs) null);

  public void HideForm() => this._floatingForm.Hide();

  public Size GetSize() => this._floatingForm.Size;

  public Point GetLocation() => this._floatingForm.Location;

  public void ShowForm() => this._floatingForm.Show();

  public void ActivateForm() => this._floatingForm.Activate();

  public bool GetVisible() => this._floatingForm.Visible;

  public bool k() => this._g;

  public Form GetForm() => (Form) this._floatingForm;
}
