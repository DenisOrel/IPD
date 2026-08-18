
// Type: Intermech.Docking.FloatingForm
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Docking;

internal class FloatingForm : Form
{
  private const int _a = 132;
  private const int _b = 2;
  private const int _c = 1;
  private const int _d = 517;
  private FloatingDockContainer _dockContainer;
  private Point _f;

  public FloatingForm(FloatingDockContainer dockContainer)
  {
    this.InitializeComponent();
    this._dockContainer = dockContainer;
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.StartPosition = FormStartPosition.Manual;
    this.ShowInTaskbar = false;
    this.MinimumSize = new Size(150, 150);
  }

  private bool CanShowContextMenu()
  {
    if (this._dockContainer.HasSingleControlLayoutSystem)
    {
      ControlLayoutSystem layoutSystem = (ControlLayoutSystem) this._dockContainer.LayoutSystem.LayoutSystems[0];
      if (layoutSystem.SelectedControl != null)
      {
        this._dockContainer.OnShowControlContextMenu(new ShowControlContextMenuEventArgs(layoutSystem.SelectedControl, layoutSystem.SelectedControl.PointToClient(Cursor.Position)));
        return true;
      }
    }
    return false;
  }

  protected override void OnActivated(EventArgs A_0) => base.OnActivated(A_0);

  [DllImport("User32.dll", CharSet = CharSet.Auto)]
  public static extern uint SendMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);

  protected override void WndProc(ref Message A_0)
  {
    if (A_0.Msg == 161 && FloatingForm.SendMessage(this.Handle, 132, 0U, (uint) (int) A_0.LParam) == 1U)
    {
      this._dockContainer.LayoutSystem.CreateDocker(this._dockContainer.Manager, (DockContainer) this._dockContainer, (LayoutSystemBase) this._dockContainer.LayoutSystem, (DockControl) null, this.PointToClient(new Point((int) A_0.LParam)), this._dockContainer.Manager.DockingHints, this._dockContainer.Manager.DockingManager, true);
      this._dockContainer._activeLayoutSystem = (LayoutSystemBase) this._dockContainer.LayoutSystem;
      this.Capture = false;
      this._dockContainer.Capture = true;
    }
    if (A_0.Msg == 132)
    {
      base.WndProc(ref A_0);
      if (A_0.Result.ToInt32() != 2)
        return;
      A_0.Result = new IntPtr(1);
    }
    else if (A_0.Msg == 517 && this.CanShowContextMenu())
      A_0.Result = IntPtr.Zero;
    else
      base.WndProc(ref A_0);
  }

  protected override void OnResize(EventArgs A_0)
  {
    base.OnResize(A_0);
    if (this._dockContainer == null)
      return;
    foreach (DockControl dockControl in this._dockContainer.GetDockControls())
      dockControl.FloatingSize = this.Size;
  }

  protected override void OnDoubleClick(EventArgs e) => base.OnDoubleClick(e);

  protected override void OnMouseDown(MouseEventArgs e)
  {
    base.OnMouseDown(e);
    if (e.Button != MouseButtons.Left)
      return;
    this._f = new Point(e.X, e.Y);
  }

  protected override void OnMouseMove(MouseEventArgs e)
  {
    base.OnMouseMove(e);
    if (e.Button != MouseButtons.Left || !(this._f != Point.Empty))
      return;
    Rectangle rectangle = new Rectangle(this._f, SystemInformation.DragSize);
    rectangle.Offset(-SystemInformation.DragSize.Width / 2, -SystemInformation.DragSize.Height / 2);
    if (rectangle.Contains(e.X, e.Y))
      return;
    this._f.Y += SystemInformation.ToolWindowCaptionHeight + SystemInformation.FrameBorderSize.Height;
    this._dockContainer.LayoutSystem.CreateDocker(this._dockContainer.Manager, (DockContainer) this._dockContainer, (LayoutSystemBase) this._dockContainer.LayoutSystem, (DockControl) null, this._f, this._dockContainer.Manager.DockingHints, this._dockContainer.Manager.DockingManager, true);
    this._dockContainer._activeLayoutSystem = (LayoutSystemBase) this._dockContainer.LayoutSystem;
    this.Capture = false;
    this._dockContainer.Capture = true;
    this._f = Point.Empty;
  }

  protected override void OnMouseUp(MouseEventArgs e)
  {
    base.OnMouseUp(e);
    this._f = Point.Empty;
  }

  protected override void OnMove(EventArgs e)
  {
    base.OnMove(e);
    if (this._dockContainer == null)
      return;
    foreach (DockControl dockControl in this._dockContainer.GetDockControls())
      dockControl.FloatingLocation = this.Location;
  }

  private void InitializeComponent()
  {
    this.SuspendLayout();
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(684, 462);
    this.Name = nameof (FloatingForm);
    this.ResumeLayout(false);
  }
}
