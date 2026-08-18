
// Type: Intermech.Docking.LayoutSystemBase
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using Intermech.Docking.Rendering;
using Intermech.Util;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Docking;

public abstract class LayoutSystemBase : IDisposable
{
  internal const int _a = 250;
  internal const int b = 400;
  internal LayoutSystemBase _parent;
  private DockContainer _dockContainer;
  private Rectangle _bounds;
  internal SizeF _workingSize;
  internal StandardDocker _docker;

  internal LayoutSystemBase()
  {
    this._parent = (LayoutSystemBase) null;
    this._dockContainer = (DockContainer) null;
    this._bounds = Rectangle.Empty;
    this._workingSize = (SizeF) new Size(250, 400);
    this._docker = (StandardDocker) null;
  }

  private void DetachDocker()
  {
    this._docker.Commit -= new StandardDocker.DockingManagerCommittedEventHandler(this.OnDockingManagerCommitted);
    this._docker.Cancel -= new EventHandler(this.OnDockingManagerCancelled);
    this._docker = (StandardDocker) null;
  }

  internal void CreateDocker(
    DockManager manager,
    DockContainer container,
    LayoutSystemBase system,
    DockControl dockControl,
    Point pos,
    DockingHints hints,
    DockingManager dockingManager,
    bool canFloat)
  {
    this._docker = dockingManager != DockingManager.Whidbey || !Win32.IsWin2K() ? new StandardDocker(manager, this.DockContainer, this, dockControl, pos, hints, canFloat) : (StandardDocker) new WhidbeyDocker(manager, this.DockContainer, this, dockControl, pos, hints, canFloat);
    this._docker.Commit += new StandardDocker.DockingManagerCommittedEventHandler(this.OnDockingManagerCommitted);
    this._docker.Cancel += new EventHandler(this.OnDockingManagerCancelled);
  }

  public virtual void Dispose()
  {
    if (this._parent is SplitLayoutSystem)
      ((SplitLayoutSystem) this._parent).LayoutSystems.Remove(this);
    this.SetDockContainer((DockContainer) null);
  }

  internal abstract bool IsDockLocationValid(DockLocation location);

  protected internal virtual void Layout(
    RendererBase renderer,
    Graphics graphics,
    Rectangle bounds,
    bool floating)
  {
    this._bounds = bounds;
  }

  internal virtual void OnDockingManagerCancelled(object sender, EventArgs e)
  {
    this.DetachDocker();
  }

  internal virtual void OnDockingManagerCommitted(StandardDocker.DockingSite target)
  {
    this.DetachDocker();
  }

  protected internal virtual void OnDragOver(DragEventArgs drgevent)
  {
  }

  protected internal virtual void OnMouseDoubleClick()
  {
  }

  protected internal virtual void OnMouseDown(MouseEventArgs e)
  {
  }

  protected internal virtual void OnMouseLeave()
  {
  }

  protected internal virtual void OnMouseMove(MouseEventArgs e)
  {
  }

  protected internal virtual void OnMouseUp(MouseEventArgs e)
  {
  }

  internal abstract void Paint(RendererBase renderer, Graphics graphics, Font font);

  internal virtual void SetDockContainer(DockContainer dockContainer)
  {
    this._dockContainer = dockContainer;
  }

  public void SetWorkingSize(Size size)
  {
    this._workingSize = (SizeF) new Size(size.Width, size.Height);
  }

  public SizeF WorkingSize => this._workingSize;

  public Rectangle Bounds => this._bounds;

  public DockContainer DockContainer => this._dockContainer;

  public bool IsInContainer => this._dockContainer != null;

  public LayoutSystemBase Parent => this._parent;

  internal abstract bool ContainsPersistableDockControls { get; }
}
