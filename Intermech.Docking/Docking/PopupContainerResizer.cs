
// Type: Intermech.Docking.PopupContainerResizer
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Docking;

internal class PopupContainerResizer : BaseDocker
{
  private DockContainer _dockContainer;
  private PopupContainer _popupContainer;
  private int _c;
  private int _d;
  private int _e;

  public PopupContainerResizer(
    DockContainer dockContainer,
    PopupContainer popupContainer,
    Point pos)
    : base((Control) dockContainer, dockContainer.DockingHints, false)
  {
    this._e = 0;
    this._dockContainer = dockContainer;
    this._popupContainer = popupContainer;
    int size = popupContainer.GetSize();
    switch (dockContainer.Dock)
    {
      case DockStyle.Top:
        this._c = pos.Y - (size - dockContainer.MinSize);
        this._d = dockContainer.MaxSize == 0 ? 0 : pos.Y + (dockContainer.MaxSize - size);
        break;
      case DockStyle.Bottom:
        this._c = dockContainer.MaxSize == 0 ? 0 : pos.Y - (dockContainer.MaxSize - size);
        this._d = pos.Y + (size - dockContainer.MinSize);
        break;
      case DockStyle.Left:
        this._c = pos.X - (size - dockContainer.MinSize);
        this._d = dockContainer.MaxSize == 0 ? 0 : pos.X + (dockContainer.MaxSize - size);
        break;
      case DockStyle.Right:
        this._c = dockContainer.MaxSize == 0 ? 0 : pos.X - (dockContainer.MaxSize - size);
        this._d = pos.X + (size - dockContainer.MinSize);
        break;
    }
    this.Update(pos);
  }

  public override void OnCommit()
  {
    base.OnCommit();
    if (this.Commit == null)
      return;
    this.Commit(this._e);
  }

  public override void Update(Point A_0)
  {
    Rectangle rectangle = Rectangle.Empty;
    if (this._dockContainer.Vertical)
    {
      rectangle = new Rectangle(A_0.X - 2, 0, 4, this._popupContainer.Height);
      if (rectangle.X < this._c && this._c != 0)
        rectangle.X = this._c;
      if (rectangle.X > this._d - 4 && this._d != 0)
        rectangle.X = this._d - 4;
    }
    else
    {
      rectangle = new Rectangle(0, A_0.Y - 2, this._popupContainer.Width, 4);
      if (rectangle.Y < this._c && this._c != 0)
        rectangle.Y = this._c;
      if (rectangle.Y > this._d - 4 && this._d != 0)
        rectangle.Y = this._d - 4;
    }
    switch (this._dockContainer.Dock)
    {
      case DockStyle.Top:
        this._e = this._popupContainer.Height + (rectangle.Y - this._popupContainer.Height);
        break;
      case DockStyle.Bottom:
        this._e = this._popupContainer.Height - rectangle.Y;
        break;
      case DockStyle.Left:
        this._e = this._popupContainer.Width + (rectangle.X - this._popupContainer.Width);
        break;
      case DockStyle.Right:
        this._e = this._popupContainer.Width - rectangle.X;
        break;
    }
    this.Redraw(new Rectangle(this._popupContainer.PointToScreen(rectangle.Location), rectangle.Size), false);
  }

  public override void Dispose()
  {
    base.Dispose();
    this._dockContainer = (DockContainer) null;
    this._popupContainer = (PopupContainer) null;
  }

  public override void OnCancel()
  {
    base.OnCancel();
    if (this.Cancel == null)
      return;
    this.Cancel((object) this, EventArgs.Empty);
  }

  public event PopupContainerResizer.PopupContainerCommitEventHandler Commit;

  public event EventHandler Cancel;

  public delegate void PopupContainerCommitEventHandler(int A_0);
}
