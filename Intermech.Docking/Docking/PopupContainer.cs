
// Type: Intermech.Docking.PopupContainer
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using Intermech.Util;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Docking;

internal class PopupContainer : Control
{
  private DockContainer _dockContainer;
  private ControlLayoutSystem _layoutSystem;
  private AutoHideManager _autoHideManager;
  private PopupContainerResizer _resizer;
  private Rectangle _clientBounds;
  private Rectangle _resizeBounds;
  private ToolTips _toolTip;

  public PopupContainer(DockContainer container, AutoHideManager manager)
  {
    this._dockContainer = (DockContainer) null;
    this._layoutSystem = (ControlLayoutSystem) null;
    this._autoHideManager = (AutoHideManager) null;
    this._resizer = (PopupContainerResizer) null;
    this._dockContainer = container;
    this._autoHideManager = manager;
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.SetStyle(ControlStyles.DoubleBuffer, true);
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this._toolTip = new ToolTips((Control) this);
    this._toolTip.GetToolTipText += new ToolTips.GetToolTipTextEventHandler(this.TollTip_GetText);
  }

  private void DisposeResizer()
  {
    this._resizer.Cancel -= new EventHandler(this.Resizer_Cancel);
    this._resizer.Commit -= new PopupContainerResizer.PopupContainerCommitEventHandler(this.Resizer_Commit);
    this._resizer = (PopupContainerResizer) null;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._dockContainer = (DockContainer) null;
      this.SetLayoutSystem((ControlLayoutSystem) null);
      this._autoHideManager = (AutoHideManager) null;
      if (this._toolTip != null)
      {
        this._toolTip.Dispose();
        this._toolTip = (ToolTips) null;
      }
      if (this._resizer != null)
        this.DisposeResizer();
    }
    base.Dispose(disposing);
  }

  private string TollTip_GetText(Point pos)
  {
    return this._clientBounds.Contains(pos) && this._layoutSystem != null ? this._layoutSystem.GetToolTipText(pos) : string.Empty;
  }

  protected override void OnLeave(EventArgs e)
  {
    base.OnLeave(e);
    if (this._layoutSystem == null)
      return;
    this._layoutSystem.InvalidateTitleBar();
  }

  private void Resizer_Commit(int newSize)
  {
    this.DisposeResizer();
    Rectangle bounds = this.Bounds;
    switch (this._dockContainer.Dock)
    {
      case DockStyle.Top:
        bounds.Height = newSize;
        break;
      case DockStyle.Bottom:
        bounds.Y = bounds.Bottom - newSize;
        bounds.Height = newSize;
        break;
      case DockStyle.Left:
        bounds.Width = newSize;
        break;
      case DockStyle.Right:
        bounds.X = bounds.Right - newSize;
        bounds.Width = newSize;
        break;
    }
    this.Bounds = bounds;
    this._layoutSystem.PopupSize = newSize;
  }

  protected override void OnMouseUp(MouseEventArgs mea)
  {
    base.OnMouseUp(mea);
    if (this._resizer != null && mea.Button == MouseButtons.Left)
    {
      this._resizer.OnCommit();
    }
    else
    {
      if (!this._clientBounds.Contains(mea.X, mea.Y) || this._layoutSystem == null)
        return;
      this._layoutSystem.OnMouseUp(mea);
    }
  }

  protected override void OnPaintBackground(PaintEventArgs pea)
  {
    this._dockContainer.WorkingRenderer.DrawDockContainerBackground(pea.Graphics, this.ClientRectangle);
  }

  public void SetLayoutSystem(ControlLayoutSystem A_0)
  {
    if (this._layoutSystem != null)
      this._layoutSystem.PopupContainer = (PopupContainer) null;
    this._layoutSystem = A_0;
    if (this._layoutSystem != null)
      this._layoutSystem.PopupContainer = this;
    this.Repaint();
  }

  private void Resizer_Cancel(object sender, EventArgs e) => this.DisposeResizer();

  internal void DetachAutoHideManager() => this._autoHideManager.Clear();

  private void CreateResizer(Point pos)
  {
    this._resizer = new PopupContainerResizer(this._dockContainer, this, pos);
    this._resizer.Cancel += new EventHandler(this.Resizer_Cancel);
    this._resizer.Commit += new PopupContainerResizer.PopupContainerCommitEventHandler(this.Resizer_Commit);
  }

  protected override void OnMouseLeave(EventArgs e)
  {
    base.OnMouseLeave(e);
    this.Cursor = Cursors.Default;
    if (this._layoutSystem == null)
      return;
    this._layoutSystem.OnMouseLeave();
  }

  protected override void OnMouseDown(MouseEventArgs mea)
  {
    base.OnMouseDown(mea);
    if (this._resizeBounds.Contains(mea.X, mea.Y) && mea.Button == MouseButtons.Left)
    {
      this.CreateResizer(new Point(mea.X, mea.Y));
    }
    else
    {
      if (!this._clientBounds.Contains(mea.X, mea.Y) || this._layoutSystem == null)
        return;
      this._layoutSystem.OnMouseDown(mea);
    }
  }

  protected override void OnPaint(PaintEventArgs pea)
  {
    try
    {
      this._dockContainer.WorkingRenderer.StartRenderSession();
      try
      {
        if (this._layoutSystem == null)
          return;
        this._layoutSystem.Paint(this._dockContainer.WorkingRenderer, pea.Graphics, this.Font);
      }
      finally
      {
        this._dockContainer.WorkingRenderer.FinishRenderSession();
      }
    }
    catch
    {
    }
  }

  public bool IsResizing() => this._resizer != null;

  protected override void OnResize(EventArgs e)
  {
    base.OnResize(e);
    this.Repaint();
  }

  protected override void OnMouseMove(MouseEventArgs mea)
  {
    base.OnMouseMove(mea);
    if (this._resizeBounds.Contains(mea.X, mea.Y) || this._resizer != null)
    {
      if (this._dockContainer.Dock == DockStyle.Left || this._dockContainer.Dock == DockStyle.Right)
        this.Cursor = Cursors.VSplit;
      else
        this.Cursor = Cursors.HSplit;
    }
    else
      this.Cursor = Cursors.Default;
    if (this.Capture && this._resizer != null)
    {
      this._resizer.Update(new Point(mea.X, mea.Y));
    }
    else
    {
      if (!this._clientBounds.Contains(mea.X, mea.Y) || this._layoutSystem == null)
        return;
      this._layoutSystem.OnMouseMove(mea);
    }
  }

  public ControlLayoutSystem GetLayoutSystem() => this._layoutSystem;

  protected override void OnEnter(EventArgs e)
  {
    base.OnEnter(e);
    if (this._layoutSystem == null)
      return;
    this._layoutSystem.InvalidateTitleBar();
  }

  internal void Repaint()
  {
    if (this._layoutSystem == null)
    {
      if (this.ContainsFocus)
        this.Focus();
      while (this.Controls.Count != 0)
        this.Controls.RemoveAt(0);
    }
    else
    {
      this._clientBounds = this.ClientRectangle;
      switch (this._dockContainer.Dock)
      {
        case DockStyle.Top:
          this._resizeBounds = new Rectangle(this._clientBounds.X, this._clientBounds.Bottom - 4, this._clientBounds.Width, 4);
          this._clientBounds.Height -= 4;
          break;
        case DockStyle.Bottom:
          this._resizeBounds = new Rectangle(this._clientBounds.X, this._clientBounds.Y, this._clientBounds.Width, 4);
          this._clientBounds.Y += 4;
          this._clientBounds.Height -= 4;
          break;
        case DockStyle.Left:
          this._resizeBounds = new Rectangle(this._clientBounds.Right - 4, this._clientBounds.Y, 4, this._clientBounds.Height);
          this._clientBounds.Width -= 4;
          break;
        case DockStyle.Right:
          this._resizeBounds = new Rectangle(this._clientBounds.X, this._clientBounds.Y, 4, this._clientBounds.Height);
          this._clientBounds.X += 4;
          this._clientBounds.Width -= 4;
          break;
        default:
          this._resizeBounds = Rectangle.Empty;
          break;
      }
      foreach (DockControl control in (CollectionBase) this._layoutSystem.Controls)
      {
        if (control.Parent != this)
        {
          if (control.Parent != null)
            DockHelper.DetachControl((Control) control);
          this.Controls.Add((Control) control);
        }
      }
      this._layoutSystem.LayoutCollapsed(this._dockContainer.WorkingRenderer, this._clientBounds);
      this.Invalidate();
    }
  }

  internal int GetSize()
  {
    return this._dockContainer.Dock != DockStyle.Left && this._dockContainer.Dock != DockStyle.Right ? this._clientBounds.Height : this._clientBounds.Width;
  }
}
