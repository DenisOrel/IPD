
// Type: Intermech.Docking.SplitLayoutResizer
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Docking;

internal class SplitLayoutResizer : BaseDocker
{
  internal const int _a = 25;
  private DockContainer _dockContainer;
  private SplitLayoutSystem _splitLayoutSystem;
  private LayoutSystemBase _layoutSystem1;
  private LayoutSystemBase _layoutSystem2;
  private Point _point;
  private int _g;
  private int _h;
  private int i;
  private int j;
  private int k;

  public SplitLayoutResizer(
    DockContainer dockContainer,
    SplitLayoutSystem splitLayoutSystem,
    LayoutSystemBase layoutSystem1,
    LayoutSystemBase layoutSystem2,
    Point point,
    DockingHints dockingHints)
    : base((Control) dockContainer, dockingHints, false)
  {
    this._dockContainer = dockContainer;
    this._splitLayoutSystem = splitLayoutSystem;
    this._layoutSystem1 = layoutSystem1;
    this._layoutSystem2 = layoutSystem2;
    this._point = point;
    if (splitLayoutSystem.SplitMode == Orientation.Horizontal)
    {
      Rectangle bounds = layoutSystem1.Bounds;
      this._g = bounds.Y + 25;
      bounds = layoutSystem2.Bounds;
      this._h = bounds.Bottom - 25;
      this.i = (int) layoutSystem1._workingSize.Height + (int) layoutSystem2._workingSize.Height;
    }
    else
    {
      Rectangle bounds = layoutSystem1.Bounds;
      this._g = bounds.X + 25;
      bounds = layoutSystem2.Bounds;
      this._h = bounds.Right - 25;
      this.i = (int) layoutSystem1._workingSize.Width + (int) layoutSystem2._workingSize.Width;
    }
    this.Update(point);
  }

  public override void OnCommit()
  {
    base.OnCommit();
    if (this.Commit == null)
      return;
    this.Commit(this._layoutSystem1, this._layoutSystem2, this.j, this.k);
  }

  public override void Update(Point A_0)
  {
    Rectangle empty = Rectangle.Empty;
    if (this._splitLayoutSystem.SplitMode == Orientation.Horizontal)
    {
      ref Rectangle local = ref empty;
      Rectangle bounds = this._splitLayoutSystem.Bounds;
      int x = bounds.X;
      int y1 = A_0.Y - 2;
      bounds = this._splitLayoutSystem.Bounds;
      int width = bounds.Width;
      local = new Rectangle(x, y1, width, 4);
      if (empty.Y < this._g)
        empty.Y = this._g;
      if (empty.Y > this._h - 4)
        empty.Y = this._h - 4;
      int y2 = empty.Y;
      bounds = this._layoutSystem1.Bounds;
      int y3 = bounds.Y;
      this.j = y2 - y3;
      this.k = this.i - this.j;
    }
    else
    {
      ref Rectangle local = ref empty;
      int x1 = A_0.X - 2;
      int y = this._splitLayoutSystem.Bounds.Y;
      Rectangle bounds = this._splitLayoutSystem.Bounds;
      int height = bounds.Height;
      local = new Rectangle(x1, y, 4, height);
      if (empty.X < this._g)
        empty.X = this._g;
      if (empty.X > this._h - 4)
        empty.X = this._h - 4;
      int x2 = empty.X;
      bounds = this._layoutSystem1.Bounds;
      int x3 = bounds.X;
      this.j = x2 - x3;
      this.k = this.i - this.j;
    }
    this.Redraw(new Rectangle(this._dockContainer.PointToScreen(empty.Location), empty.Size), false);
  }

  public override void OnCancel()
  {
    base.OnCancel();
    if (this.Cancel == null)
      return;
    this.Cancel((object) this, EventArgs.Empty);
  }

  public SplitLayoutSystem d() => this._splitLayoutSystem;

  public event SplitLayoutResizer.SplitResizeEventHandler Commit;

  public event EventHandler Cancel;

  public delegate void SplitResizeEventHandler(
    LayoutSystemBase A_0,
    LayoutSystemBase A_1,
    int A_2,
    int A_3);
}
