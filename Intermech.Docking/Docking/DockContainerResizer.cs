
// Type: Intermech.Docking.DockContainerResizer
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Docking;

internal class DockContainerResizer : BaseDocker
{
  private DockContainer _dockContainer;
  private int _b;
  private int _c;
  private int _d;

  public DockContainerResizer(DockManager dockManager, DockContainer dockContainer, Point pos)
    : base((Control) dockContainer, dockManager.DockingHints, false)
  {
    this._dockContainer = (DockContainer) null;
    this._d = 0;
    this._dockContainer = dockContainer;
    Rectangle rectangle = Rectangle.Empty;
    if (dockManager != null)
    {
      rectangle = WhidbeyDocker.GetDockingBounds(dockManager, dockContainer.Parent, false);
      rectangle = new Rectangle(dockContainer.PointToClient(rectangle.Location), rectangle.Size);
    }
    int currentSize = dockContainer.CurrentSize;
    switch (dockContainer.Dock)
    {
      case DockStyle.Top:
        this._b = pos.Y - (currentSize - dockContainer.MinSize);
        this._c = dockContainer.MaxSize == 0 ? dockContainer.Parent.ClientRectangle.Height - dockContainer.Top : Math.Min(pos.Y + (dockContainer.MaxSize - currentSize), dockContainer.Parent.ClientRectangle.Height - dockContainer.Top);
        if (rectangle != Rectangle.Empty && this._c > rectangle.Bottom)
        {
          this._c = rectangle.Bottom;
          break;
        }
        break;
      case DockStyle.Bottom:
        this._b = dockContainer.MaxSize == 0 ? -dockContainer.Top : Math.Max(pos.Y - (dockContainer.MaxSize - currentSize), -dockContainer.Top);
        this._c = pos.Y + (currentSize - dockContainer.MinSize);
        if (rectangle != Rectangle.Empty && this._b < rectangle.Top)
        {
          this._b = rectangle.Top;
          break;
        }
        break;
      case DockStyle.Left:
        this._b = pos.X - (currentSize - dockContainer.MinSize);
        this._c = dockContainer.MaxSize == 0 ? dockContainer.Parent.ClientRectangle.Width - dockContainer.Left : Math.Min(pos.X + (dockContainer.MaxSize - currentSize), dockContainer.Parent.ClientRectangle.Width - dockContainer.Left);
        if (rectangle != Rectangle.Empty && this._c > rectangle.Right)
        {
          this._c = rectangle.Right;
          break;
        }
        break;
      case DockStyle.Right:
        this._b = dockContainer.MaxSize == 0 ? -dockContainer.Left : Math.Max(pos.X - (dockContainer.MaxSize - currentSize), -dockContainer.Left);
        this._c = pos.X + (currentSize - dockContainer.MinSize);
        if (rectangle != Rectangle.Empty && this._b < rectangle.Left)
        {
          this._b = rectangle.Left;
          break;
        }
        break;
    }
    this.Update(pos);
  }

  public override void OnCommit()
  {
    base.OnCommit();
    if (this.Commit == null)
      return;
    this.Commit(this._d);
  }

  public override void Update(Point pos)
  {
    Rectangle rectangle = Rectangle.Empty;
    if (this._dockContainer.Vertical)
    {
      rectangle = new Rectangle(pos.X - 2, 0, 4, this._dockContainer.Height);
      if (rectangle.X < this._b)
        rectangle.X = this._b;
      if (rectangle.X > this._c - 4)
        rectangle.X = this._c - 4;
    }
    else
    {
      rectangle = new Rectangle(0, pos.Y - 2, this._dockContainer.Width, 4);
      if (rectangle.Y < this._b)
        rectangle.Y = this._b;
      if (rectangle.Y > this._c - 4)
        rectangle.Y = this._c - 4;
    }
    switch (this._dockContainer.Dock)
    {
      case DockStyle.Top:
        this._d = this._dockContainer.Height + (rectangle.Y - this._dockContainer.Height);
        break;
      case DockStyle.Bottom:
        this._d = this._dockContainer.Height - rectangle.Y;
        break;
      case DockStyle.Left:
        this._d = this._dockContainer.Width + (rectangle.X - this._dockContainer.Width);
        break;
      case DockStyle.Right:
        this._d = this._dockContainer.Width - rectangle.X;
        break;
    }
    this.Redraw(new Rectangle(this._dockContainer.PointToScreen(rectangle.Location), rectangle.Size), false);
  }

  public override void Dispose()
  {
    base.Dispose();
    this._dockContainer = (DockContainer) null;
  }

  public override void OnCancel()
  {
    base.OnCancel();
    if (this.Cancel == null)
      return;
    this.Cancel((object) this, EventArgs.Empty);
  }

  public event DockContainerResizer.CommitEventHandler Commit;

  public event EventHandler Cancel;

  public delegate void CommitEventHandler(int newSize);
}
