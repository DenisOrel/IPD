
// Type: Intermech.Docking.AutoHideManager
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using Intermech.Docking.Rendering;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Docking;

internal class AutoHideManager : IDisposable
{
  private DockContainer _dockContainer;
  private Timer _showTimer;
  private Timer _hideTimer;
  private PopupContainer _popupContainer;
  private ControlLayoutSystem _controlLayoutSystem;
  private Rectangle _bounds;
  private Point _lastPos;

  public AutoHideManager(DockContainer dockContainer)
  {
    this._controlLayoutSystem = (ControlLayoutSystem) null;
    this._bounds = Rectangle.Empty;
    this._lastPos = Point.Empty;
    this._dockContainer = dockContainer;
    this._showTimer = new Timer();
    this._showTimer.Interval = 500;
    this._showTimer.Tick += new EventHandler(this.OnShowTimerTick);
    this._hideTimer = new Timer();
    this._hideTimer.Interval = 800;
    this._hideTimer.Tick += new EventHandler(this.OnHideTimerTick);
    this._popupContainer = new PopupContainer(dockContainer, this);
  }

  internal static int GetHiddenSize() => Control.DefaultFont.Height + 9;

  internal void Hide(bool skipAnimation)
  {
    if (this._controlLayoutSystem == null)
      return;
    this._hideTimer.Enabled = false;
    if (!skipAnimation)
    {
      Rectangle result;
      this.CalcPopupBounds(this._controlLayoutSystem.PopupSize, out result);
      this.AnimatePopup(this._popupContainer.Bounds, result);
    }
    this._popupContainer.SetLayoutSystem((ControlLayoutSystem) null);
    if (this._popupContainer.Parent != null)
    {
      this._dockContainer.Parent.Resize -= new EventHandler(this.Parent_Resize);
      this._popupContainer.Parent.Controls.Remove((Control) this._popupContainer);
    }
    ControlLayoutSystem controlLayoutSystem = this._controlLayoutSystem;
    this._controlLayoutSystem = (ControlLayoutSystem) null;
    if (controlLayoutSystem == null || controlLayoutSystem.SelectedControl == null)
      return;
    controlLayoutSystem.SelectedControl.OnAutoHidePopupClosed(EventArgs.Empty);
  }

  private DockControl GetDockControlAt(Point pos)
  {
    foreach (LayoutSystemBase layoutSystem in this._dockContainer._layoutSystems)
    {
      if (layoutSystem is ControlLayoutSystem)
      {
        ControlLayoutSystem controlLayoutSystem = (ControlLayoutSystem) layoutSystem;
        if (controlLayoutSystem.Collapsed)
        {
          foreach (DockControl control in (CollectionBase) controlLayoutSystem.Controls)
          {
            if (control._tabBounds.Contains(pos))
              return control;
          }
        }
      }
    }
    return (DockControl) null;
  }

  public void Paint(Graphics g, Rectangle bounds)
  {
    this._dockContainer.WorkingRenderer.DrawCollapsedBackground(g, bounds);
    DockSide dockSide = DockSide.Right;
    switch (this._dockContainer.Dock)
    {
      case DockStyle.Top:
        dockSide = DockSide.Top;
        break;
      case DockStyle.Bottom:
        dockSide = DockSide.Bottom;
        break;
      case DockStyle.Left:
        dockSide = DockSide.Left;
        break;
    }
    foreach (LayoutSystemBase layoutSystem in this._dockContainer._layoutSystems)
    {
      if (layoutSystem is ControlLayoutSystem && ((ControlLayoutSystem) layoutSystem).Collapsed)
      {
        ControlLayoutSystem controlLayoutSystem = (ControlLayoutSystem) layoutSystem;
        foreach (DockControl control in (CollectionBase) controlLayoutSystem.Controls)
        {
          DrawItemState state = DrawItemState.Default;
          if (control == controlLayoutSystem.SelectedControl)
            state |= DrawItemState.Selected;
          string text = control.TabText;
          if (this._dockContainer.WorkingRenderer.TabTextDisplay == TabTextDisplayMode.SelectedTab && control != controlLayoutSystem.SelectedControl)
            text = string.Empty;
          this._dockContainer.WorkingRenderer.DrawCollapsedTab(g, control._tabBounds, dockSide, control.WorkingTabImage, text, this._dockContainer.Font, control.BackColor, control.ForeColor, state, this._dockContainer.Vertical);
        }
      }
    }
  }

  private void AnimatePopup(Rectangle A_0, Rectangle A_1)
  {
    float num1 = (float) (A_1.X - A_0.X);
    float num2 = (float) (A_1.Y - A_0.Y);
    float num3 = (float) (A_1.Width - A_0.Width);
    float num4 = (float) (A_1.Height - A_0.Height);
    int tickCount = Environment.TickCount;
    while (Environment.TickCount < tickCount + 100)
    {
      float num5 = (float) (Environment.TickCount - tickCount) / 100f;
      Rectangle rectangle = new Rectangle((int) ((float) A_0.X + num1 * num5), (int) ((float) A_0.Y + num2 * num5), (int) ((float) A_0.Width + num3 * num5), (int) ((float) A_0.Height + num4 * num5));
      this._popupContainer.SetBounds(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, BoundsSpecified.All);
      Application.DoEvents();
      if (this._popupContainer == null)
        break;
    }
  }

  private Rectangle CalcPopupBounds(int popupSize, out Rectangle result)
  {
    Rectangle rectangle = this._dockContainer.Bounds;
    if (this._dockContainer.AutoHideVisible)
    {
      switch (this._dockContainer.Dock)
      {
        case DockStyle.Top:
          rectangle = new Rectangle(rectangle.X, rectangle.Y + AutoHideManager.GetHiddenSize(), rectangle.Width, 0);
          break;
        case DockStyle.Bottom:
          rectangle = new Rectangle(rectangle.X, rectangle.Bottom - AutoHideManager.GetHiddenSize(), rectangle.Width, 0);
          break;
        case DockStyle.Left:
          rectangle = new Rectangle(rectangle.X + AutoHideManager.GetHiddenSize(), rectangle.Y, 0, rectangle.Height);
          break;
        case DockStyle.Right:
          rectangle = new Rectangle(rectangle.Right - AutoHideManager.GetHiddenSize(), rectangle.Y, 0, rectangle.Height);
          break;
      }
    }
    result = rectangle;
    int num = popupSize + 4;
    switch (this._dockContainer.Dock)
    {
      case DockStyle.Top:
        rectangle.Height = num;
        return rectangle;
      case DockStyle.Bottom:
        rectangle.Offset(0, -num);
        rectangle.Height = num;
        return rectangle;
      case DockStyle.Left:
        rectangle.Width = num;
        return rectangle;
      case DockStyle.Right:
        rectangle.Offset(-num, 0);
        rectangle.Width = num;
        return rectangle;
      default:
        return rectangle;
    }
  }

  private void Parent_Resize(object A_0, EventArgs A_1)
  {
    if (!this._hideTimer.Enabled)
      return;
    this.Hide(true);
  }

  internal void PopupDockControl(DockControl dockControl, bool skipAnimate, bool activate)
  {
    try
    {
      if (this._controlLayoutSystem == dockControl._layoutSystem && dockControl._layoutSystem.SelectedControl == dockControl)
        return;
      dockControl._layoutSystem.SelectedControl = dockControl;
      this.Hide(true);
      Rectangle result;
      this._bounds = this.CalcPopupBounds(dockControl._layoutSystem.PopupSize, out result);
      this._popupContainer.SetLayoutSystem(dockControl._layoutSystem);
      this._popupContainer.Visible = false;
      this._dockContainer.Parent.Controls.Add((Control) this._popupContainer);
      this._popupContainer.Bounds = result;
      this._popupContainer.Visible = true;
      this._popupContainer.BringToFront();
      if (!skipAnimate)
        this.AnimatePopup(result, this._bounds);
      if (this._popupContainer == null)
        return;
      this._popupContainer.Bounds = this._bounds;
      this._controlLayoutSystem = dockControl._layoutSystem;
      this._hideTimer.Enabled = true;
      dockControl.OnAutoHidePopupOpened(EventArgs.Empty);
      this._dockContainer.Parent.Resize += new EventHandler(this.Parent_Resize);
    }
    finally
    {
      if (activate)
        dockControl.Activate();
    }
  }

  public void Layout(RendererBase renderer, Graphics g, Rectangle bounds)
  {
    int A_4 = 0;
    foreach (LayoutSystemBase layoutSystem in this._dockContainer._layoutSystems)
    {
      if (layoutSystem is ControlLayoutSystem && ((ControlLayoutSystem) layoutSystem).Collapsed)
      {
        ControlLayoutSystem controlSystem = (ControlLayoutSystem) layoutSystem;
        this.LayoutControlSystem(renderer, g, controlSystem, bounds, ref A_4);
      }
    }
  }

  private void LayoutControlSystem(
    RendererBase renderer,
    Graphics g,
    ControlLayoutSystem controlSystem,
    Rectangle bounds,
    ref int A_4)
  {
    A_4 += 3;
    int num1 = 0;
    if (renderer.TabTextDisplay == TabTextDisplayMode.SelectedTab)
    {
      foreach (DockControl control in (CollectionBase) controlSystem.Controls)
      {
        int num2 = !this._dockContainer.Vertical ? (int) Math.Ceiling((double) g.MeasureString(control.TabText, this._dockContainer.Font, 999, EverettRenderer.StandardStringFormat).Width) : (int) Math.Ceiling((double) g.MeasureString(control.TabText, this._dockContainer.Font, 999, EverettRenderer.GetStandardVerticalStringFormat()).Height);
        if (num2 > num1)
          num1 = num2;
      }
    }
    foreach (DockControl control in (CollectionBase) controlSystem.Controls)
    {
      Rectangle rectangle = new Rectangle(bounds.Left - 1, bounds.Top - 1, AutoHideManager.GetHiddenSize() - 2, AutoHideManager.GetHiddenSize() - 2);
      switch (this._dockContainer.Dock)
      {
        case DockStyle.Bottom:
          rectangle.Offset(0, 3);
          break;
        case DockStyle.Right:
          rectangle.Offset(3, 0);
          break;
      }
      int num3 = 23;
      if (renderer.TabTextDisplay == TabTextDisplayMode.AllTabs)
      {
        SizeF sizeF;
        int num4;
        if (this._dockContainer.Vertical)
        {
          sizeF = g.MeasureString(control.TabText, this._dockContainer.Font, 999, EverettRenderer.GetStandardVerticalStringFormat());
          num4 = num3 + (int) Math.Ceiling((double) sizeF.Height);
        }
        else
        {
          sizeF = g.MeasureString(control.TabText, this._dockContainer.Font, 999, EverettRenderer.StandardStringFormat);
          num4 = num3 + (int) Math.Ceiling((double) sizeF.Width);
        }
        num3 = num4 + 2;
      }
      else if (controlSystem.SelectedControl == control)
        num3 += num1 + 16 /*0x10*/;
      if (this._dockContainer.Vertical)
      {
        rectangle.Offset(0, A_4);
        rectangle.Height = num3;
        A_4 += num3;
      }
      else
      {
        rectangle.Offset(A_4, 0);
        rectangle.Width = num3;
        A_4 += num3;
      }
      control._tabBounds = rectangle;
    }
    A_4 += 10;
  }

  public void Clear() => this.Hide(true);

  public void OnDragOver(Point pos)
  {
    DockControl dockControlAt = this.GetDockControlAt(pos);
    if (dockControlAt == null)
      return;
    this.PopupDockControl(dockControlAt, true, false);
  }

  public void OnMouseWheel(MouseEventArgs mea)
  {
  }

  public void OnMouseUp(MouseEventArgs mea)
  {
  }

  public void OnMouseDown(MouseEventArgs mea)
  {
    DockControl dockControlAt = this.GetDockControlAt(new Point(mea.X, mea.Y));
    if (dockControlAt == null)
      return;
    this.PopupDockControl(dockControlAt, false, true);
  }

  public void OnMouseMove(MouseEventArgs mea)
  {
    if (mea.X == this._lastPos.X || mea.Y == this._lastPos.Y)
      return;
    this._lastPos.X = mea.X;
    this._lastPos.Y = mea.Y;
    this._showTimer.Enabled = false;
    this._showTimer.Enabled = true;
  }

  private void OnShowTimerTick(object sender, EventArgs e)
  {
    this._showTimer.Enabled = false;
    DockControl dockControlAt = this.GetDockControlAt(this._dockContainer.PointToClient(Cursor.Position));
    if (dockControlAt == null)
      return;
    this.PopupDockControl(dockControlAt, false, false);
  }

  private void OnHideTimerTick(object sender, EventArgs e)
  {
    Rectangle rectangle = this._popupContainer.ClientRectangle;
    int num = rectangle.Contains(this._popupContainer.PointToClient(Cursor.Position)) ? 1 : 0;
    rectangle = this._dockContainer.AutoHideBounds;
    bool flag = rectangle.Contains(this._dockContainer.PointToClient(Cursor.Position));
    if (num != 0 || flag || this._popupContainer.IsResizing() || this._popupContainer.ContainsFocus)
      return;
    this.Hide(false);
  }

  public void Dispose()
  {
    this.Hide(true);
    if (this._hideTimer != null)
    {
      this._hideTimer.Tick -= new EventHandler(this.OnHideTimerTick);
      this._hideTimer.Dispose();
      this._hideTimer = (Timer) null;
    }
    if (this._showTimer != null)
    {
      this._showTimer.Tick -= new EventHandler(this.OnShowTimerTick);
      this._showTimer.Dispose();
      this._showTimer = (Timer) null;
    }
    if (this._popupContainer == null)
      return;
    this._popupContainer.Dispose();
    this._popupContainer = (PopupContainer) null;
  }
}
