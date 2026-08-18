
// Type: Intermech.Docking.ControlLayoutSystem
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using Intermech.Docking.Rendering;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Docking;

[TypeConverter("Intermech.Docking.ControlLayoutSystemConverter")]
public class ControlLayoutSystem : LayoutSystemBase
{
  private const int ButtonWidth = 16 /*0x10*/;
  private const int ButonHeight = 15;
  protected ControlLayoutSystem.DockControlCollection _controls;
  private bool _collapsed;
  internal Rectangle _titleBarBounds;
  internal Rectangle _tabStripBounds;
  internal Rectangle _documentBounds;
  internal Rectangle _joinCatchmentBounds;
  private DockControl _selectedControl;
  private PopupContainer _popupContainer;
  private Guid _guid;
  private int _popupSize;
  private TitleButton _closeButton;
  private TitleButton _pinButton;
  internal TitleButton _activeButton;
  internal bool _buttonPressed;
  private Point _startPoint;
  private bool _lockControls;
  internal bool _layoutInProgress;
  private bool _wholeMoving;
  private bool _focused;
  internal bool _skipMouseUp;
  internal bool _mouseDown;

  internal event ControlLayoutSystem.ControlLayoutSystemEventHandler SelectedControlChanged;

  public ControlLayoutSystem()
  {
    this._controls = (ControlLayoutSystem.DockControlCollection) null;
    this._collapsed = false;
    this._selectedControl = (DockControl) null;
    this._popupContainer = (PopupContainer) null;
    this._guid = Guid.NewGuid();
    this._popupSize = 0;
    this._closeButton = new TitleButton();
    this._pinButton = new TitleButton();
    this._activeButton = (TitleButton) null;
    this._buttonPressed = false;
    this._startPoint = Point.Empty;
    this._lockControls = false;
    this._layoutInProgress = false;
    this._wholeMoving = false;
    this._focused = false;
    this._controls = new ControlLayoutSystem.DockControlCollection(this);
  }

  public ControlLayoutSystem(Guid guid)
    : this()
  {
    if (!(guid != Guid.Empty))
      return;
    this._guid = guid;
  }

  public ControlLayoutSystem(int desiredWidth, int desiredHeight)
    : this()
  {
    this._workingSize = new SizeF((float) desiredWidth, (float) desiredHeight);
  }

  public ControlLayoutSystem(
    int desiredWidth,
    int desiredHeight,
    DockControl[] controls,
    DockControl selectedControl)
    : this(desiredWidth, desiredHeight)
  {
    this._controls.AddRange(controls);
    if (selectedControl == null)
      return;
    this.SelectedControl = selectedControl;
  }

  private void ActivateButtons()
  {
    if (this._selectedControl == null)
    {
      this._closeButton._visible = false;
      this._pinButton._visible = false;
    }
    else
    {
      int y = this._titleBarBounds.Top + this._titleBarBounds.Height / 2 - 7;
      int num1 = this._titleBarBounds.Right - 2;
      if (this._selectedControl.Closable)
      {
        this._closeButton._visible = true;
        this._closeButton._bounds = new Rectangle(num1 - 16 /*0x10*/, y, 16 /*0x10*/, 15);
        num1 -= 16 /*0x10*/;
      }
      else
        this._closeButton._visible = false;
      bool flag = true;
      foreach (DockControl control in (CollectionBase) this._controls)
      {
        if (!control.Collapsible)
        {
          flag = false;
          break;
        }
      }
      if (this.IsInContainer && this.DockContainer.IsFloating)
        flag = false;
      if (flag)
      {
        this._pinButton._visible = true;
        this._pinButton._bounds = new Rectangle(num1 - 16 /*0x10*/, y, 16 /*0x10*/, 15);
        int num2 = num1 - 16 /*0x10*/;
      }
      else
        this._pinButton._visible = false;
    }
  }

  internal int GetChildIndex(Point pos)
  {
    int childIndex = 0;
    foreach (DockControl control in (CollectionBase) this._controls)
    {
      Rectangle tabBounds = control._tabBounds;
      if (pos.X > tabBounds.Left + tabBounds.Width / 2)
        ++childIndex;
    }
    return childIndex;
  }

  private void OnSelectedControlChanged(DockControl oldControl, DockControl newControl)
  {
    if (this.SelectedControlChanged == null)
      return;
    this.SelectedControlChanged(oldControl, newControl);
  }

  internal void ProcessReDocking(
    DockControl dockControl,
    bool wholeMoving,
    StandardDocker.DockingSite dockingSite)
  {
    if (dockingSite._redockType == StandardDocker.RedockType.CreateNewContainer)
    {
      ControlLayoutSystem controlLayoutSystem1;
      if (!wholeMoving)
        controlLayoutSystem1 = this.CreateNewLayoutSystem(this._tabStripBounds.Width, this._tabStripBounds.Height, new DockControl[1]
        {
          dockControl
        }, dockControl);
      else
        controlLayoutSystem1 = this;
      ControlLayoutSystem controlLayoutSystem2 = controlLayoutSystem1;
      if (dockingSite._dockContainer.Empty)
        dockingSite._dockContainer.LayoutSystem.LayoutSystems.Add((LayoutSystemBase) controlLayoutSystem2);
      else
        dockingSite._dockContainer.AddLayoutSystem((LayoutSystemBase) controlLayoutSystem2);
    }
    else if (dockingSite._redockType == StandardDocker.RedockType.JoinExistingSystem)
    {
      if (wholeMoving)
        this.Dock(dockingSite._layoutSystem, dockingSite._childIndex);
      else
        dockControl.PerformDock(dockingSite._layoutSystem, dockingSite._childIndex);
    }
    else
    {
      if (dockingSite._redockType != StandardDocker.RedockType.SplitExistingSystem)
        return;
      if (wholeMoving)
      {
        dockingSite._layoutSystem.SplitForLayoutSystem((LayoutSystemBase) this, dockingSite._dockSide);
      }
      else
      {
        DockControl[] controls = new DockControl[1]
        {
          dockControl
        };
        ControlLayoutSystem newLayoutSystem = this.CreateNewLayoutSystem(dockControl.Width, dockControl.Height, controls, dockControl);
        dockingSite._layoutSystem.SplitForLayoutSystem((LayoutSystemBase) newLayoutSystem, dockingSite._dockSide);
      }
    }
  }

  private void a(LayoutSystemBase A_0, int index, bool A_2)
  {
    SplitLayoutSystem parent = (SplitLayoutSystem) this.Parent;
    parent.LayoutSystems._rangeAdding = true;
    parent.LayoutSystems.Insert(index, A_0);
    parent.LayoutSystems._rangeAdding = false;
    if (A_2)
    {
      float num = this._workingSize.Height - 4f;
      this._workingSize.Height = num / 2f;
      A_0._workingSize.Height = num / 2f;
    }
    else
    {
      float num = this._workingSize.Width - 4f;
      this._workingSize.Width = num / 2f;
      A_0._workingSize.Width = num / 2f;
    }
    parent.OnLayoutSystemsChanged();
  }

  private void a(LayoutSystemBase layoutSystem, Orientation orientation, bool isLeft)
  {
    SplitLayoutSystem parent = (SplitLayoutSystem) this.Parent;
    SplitLayoutSystem layoutSystem1 = new SplitLayoutSystem();
    layoutSystem1.SplitMode = orientation;
    layoutSystem1._workingSize = this._workingSize;
    int index = parent.LayoutSystems.IndexOf((LayoutSystemBase) this);
    parent.LayoutSystems._rangeAdding = true;
    parent.LayoutSystems.Remove((LayoutSystemBase) this);
    parent.LayoutSystems.Insert(index, (LayoutSystemBase) layoutSystem1);
    parent.LayoutSystems._rangeAdding = false;
    layoutSystem1.LayoutSystems.Add((LayoutSystemBase) this);
    if (isLeft)
      layoutSystem1.LayoutSystems.Insert(0, layoutSystem);
    else
      layoutSystem1.LayoutSystems.Add(layoutSystem);
    parent.OnLayoutSystemsChanged();
  }

  private void LayoutDocuments(RendererBase renderer, Graphics g, Rectangle bounds)
  {
    int num1 = 0;
    int num2 = bounds.Width - (renderer.TabStripMetrics.Padding.Left + renderer.TabStripMetrics.Padding.Right);
    int[] numArray1 = new int[this._controls.Count];
    int num3 = 0;
    foreach (DockControl control in (CollectionBase) this._controls)
    {
      int num4 = (int) Math.Ceiling((double) g.MeasureString(control.TabText, control.Font, 999, EverettRenderer.StandardStringFormat).Width) + 30;
      num1 += num4;
      numArray1[num3++] = num4;
      control._textTrimmed = false;
    }
    if (num1 > num2)
    {
      int num5 = num1 - num2;
      for (int index1 = 0; index1 < num3; ++index1)
      {
        int[] numArray2;
        IntPtr index2;
        (numArray2 = numArray1)[(int) (index2 = (IntPtr) index1)] = numArray2[(int) index2] - (int) ((double) num5 * ((double) numArray1[index1] / (double) num1));
        this._controls[index1]._textTrimmed = true;
      }
    }
    bounds = renderer.TabStripMetrics.RemovePadding(bounds);
    int left = bounds.Left;
    int index3 = 0;
    for (int index4 = 0; index4 < this._controls.Count; ++index4)
    {
      DockControl control = this._controls[index4];
      BoxModel tabMetrics = renderer.TabMetrics;
      Rectangle rectangle1 = new Rectangle(left + tabMetrics.Margin.Left, bounds.Top + tabMetrics.Margin.Top, tabMetrics.Padding.Left + numArray1[index3] + tabMetrics.Padding.Right, bounds.Height - (tabMetrics.Margin.Top + tabMetrics.Margin.Bottom));
      Rectangle rectangle2 = rectangle1;
      control._tabBounds = rectangle2;
      left += rectangle1.Width + tabMetrics.ExtraWidth;
      ++index3;
    }
  }

  internal void DrawStripButton(
    Graphics gr,
    RendererBase renderer,
    TitleButton button,
    ButtonType buttonType,
    bool enabled)
  {
    if (!button._visible)
      return;
    DrawItemState state = DrawItemState.Default;
    if (this._activeButton == button)
    {
      state |= DrawItemState.HotLight;
      if (this._buttonPressed)
        state |= DrawItemState.Selected;
    }
    if (!enabled)
      state |= DrawItemState.Disabled;
    renderer.DrawDocumentStripButton(gr, button._bounds, buttonType, state);
  }

  private void InvalidateContainer()
  {
    if (!this.IsInContainer)
      return;
    this.DockContainer.Invalidate(this._titleBarBounds);
  }

  protected virtual void CalculateLayout(
    RendererBase renderer,
    Rectangle bounds,
    bool floating,
    out Rectangle titlebarBounds,
    out Rectangle tabstripBounds,
    out Rectangle clientBounds,
    out Rectangle joinCatchmentBounds)
  {
    if (floating)
    {
      titlebarBounds = Rectangle.Empty;
    }
    else
    {
      titlebarBounds = bounds;
      titlebarBounds.Offset(0, renderer.TitleBarMetrics.Margin.Top);
      titlebarBounds.Height = renderer.TitleBarMetrics.Height - (renderer.TitleBarMetrics.Margin.Top + renderer.TitleBarMetrics.Margin.Bottom);
      this.ActivateButtons();
      bounds.Offset(0, renderer.TitleBarMetrics.Height);
      bounds.Height -= renderer.TitleBarMetrics.Height;
    }
    if (this.Controls.Count > 1 || this.DockContainer.FriendDesignMode)
    {
      tabstripBounds = bounds;
      tabstripBounds.Y = tabstripBounds.Bottom - renderer.TabStripMetrics.Height;
      tabstripBounds.Height = renderer.TabStripMetrics.Height;
      tabstripBounds = renderer.TabStripMetrics.RemoveMargin(tabstripBounds);
      bounds.Height -= renderer.TabStripMetrics.Height;
    }
    else
      tabstripBounds = Rectangle.Empty;
    clientBounds = bounds;
    joinCatchmentBounds = titlebarBounds;
  }

  protected internal virtual ControlLayoutSystem CreateNewLayoutSystem()
  {
    return new ControlLayoutSystem();
  }

  protected internal virtual ControlLayoutSystem CreateNewLayoutSystem(
    int desiredWidth,
    int desiredHeight,
    DockControl[] controls,
    DockControl selectedControl)
  {
    return new ControlLayoutSystem(desiredWidth, desiredHeight, controls, selectedControl);
  }

  public override void Dispose()
  {
    DockControl[] array = new DockControl[this.Controls.Count];
    this.Controls.CopyTo(array, 0);
    this.Controls.Clear();
    foreach (Component component in array)
      component.Dispose();
    base.Dispose();
  }

  public void ClosePopup()
  {
    if (!this.IsPoppedUp)
      return;
    this.PopupContainer.DetachAutoHideManager();
  }

  public void Dock(ControlLayoutSystem layoutSystem) => this.Dock(layoutSystem, 0);

  public void Dock(ControlLayoutSystem layoutSystem, int index)
  {
    if (this.Parent != null)
      throw new InvalidOperationException("This layout system already has a parent. To remove it, use the parent layout system's LayoutSystems.Remove method.");
    DockControl selectedControl = this.SelectedControl;
    while (this._controls.Count != 0)
    {
      DockControl control = this._controls[0];
      this._controls.RemoveAt(0);
      layoutSystem.Controls.Insert(index, control);
    }
    if (selectedControl == null)
      return;
    layoutSystem.SelectedControl = selectedControl;
  }

  internal void Repaint()
  {
    if (!this.IsInContainer)
      return;
    if (this.PopupContainer != null)
      this.PopupContainer.Repaint();
    if (this.Collapsed && this.DockContainer.CanShowCollapsed)
      this.DockContainer.Invalidate(this.DockContainer.AutoHideBounds);
    if (this.DockContainer.IsFloating)
      this.DockContainer.Repaint();
    else
      this.DockContainer.PerformResize((LayoutSystemBase) this, this.Bounds);
    this.DockContainer.Invalidate(this.Bounds);
  }

  public void Float(DockManager manager)
  {
    if (this.SelectedControl == null)
      throw new InvalidOperationException("The layout system must have a selected control to be floated.");
    this.Float(manager, this.SelectedControl.FloatingBounds);
  }

  public void Float(DockManager manager, Rectangle bounds)
  {
    if (this.Parent is SplitLayoutSystem)
      ((SplitLayoutSystem) this.Parent).LayoutSystems.Remove((LayoutSystemBase) this);
    FloatingDockContainer floatingDockContainer = manager.CreateFloatingDockContainer();
    floatingDockContainer.LayoutSystem.LayoutSystems.Add((LayoutSystemBase) this);
    floatingDockContainer.SetWindowPos(bounds, true, true);
  }

  internal virtual TitleButton GetButtonAt(int x, int y)
  {
    if (this._closeButton._visible && this._closeButton._bounds.Contains(x, y))
      return this._closeButton;
    return this._pinButton._visible && this._pinButton._bounds.Contains(x, y) ? this._pinButton : (TitleButton) null;
  }

  public virtual DockControl GetControlAt(Point position)
  {
    if (this._tabStripBounds.Contains(position) && !this._closeButton._bounds.Contains(position) && !this._pinButton._bounds.Contains(position))
    {
      foreach (DockControl control in (CollectionBase) this._controls)
      {
        if (control._tabBounds.Contains(position))
          return control;
      }
    }
    return (DockControl) null;
  }

  internal override bool ContainsPersistableDockControls
  {
    get
    {
      foreach (DockControl control in (CollectionBase) this.Controls)
      {
        if (control.PersistState)
          return true;
      }
      return false;
    }
  }

  internal override bool IsDockLocationValid(DockLocation dockLocation)
  {
    foreach (DockControl control in (CollectionBase) this._controls)
    {
      if (!BaseDocker.IsDockLocationValid(dockLocation, control.AllowedStates))
        return false;
    }
    return true;
  }

  internal virtual string GetToolTipText(Point position)
  {
    DockControl controlAt = this.GetControlAt(position);
    if (controlAt != null)
    {
      if (controlAt.ToolTipText.Length != 0 && controlAt._textTrimmed)
        return controlAt.TabText + Environment.NewLine + controlAt.ToolTipText;
      return controlAt._textTrimmed ? controlAt.TabText : controlAt.ToolTipText;
    }
    TitleButton buttonAt = this.GetButtonAt(position.X, position.Y);
    if (buttonAt == this._closeButton)
      return DockLanguage.CloseText;
    return buttonAt == this._pinButton ? DockLanguage.AutoHideText : string.Empty;
  }

  internal bool OnActivated()
  {
    if (!this.IsInContainer || this.SelectedControl == null || !this.SelectedControl.ContainsFocus)
      return false;
    this.Repaint();
    if (this.SelectedControl != null)
      this.DockContainer.Manager.OnDockControlActivated(this.SelectedControl);
    return true;
  }

  internal void OnDeactivated()
  {
    if (!this._focused || !this.IsInContainer)
      return;
    this.DockContainer.Invalidate(this._titleBarBounds);
  }

  internal virtual void InvalidateTitleBar()
  {
    if (this._popupContainer != null)
    {
      this._popupContainer.Invalidate(this._titleBarBounds);
    }
    else
    {
      if (!this.IsInContainer)
        return;
      this.DockContainer.Invalidate(this._titleBarBounds);
    }
  }

  protected internal override void Layout(
    RendererBase renderer,
    Graphics graphics,
    Rectangle bounds,
    bool floating)
  {
    base.Layout(renderer, graphics, bounds, floating);
    if (this.Collapsed && this.DockContainer.CanShowCollapsed)
      return;
    this.CalculateLayout(renderer, bounds, floating, out this._titleBarBounds, out this._tabStripBounds, out this._documentBounds, out this._joinCatchmentBounds);
    if (this is DocumentLayoutSystem)
      return;
    Rectangle documentBounds = this._documentBounds;
    documentBounds.Inflate(-renderer.ControlClientPadding.Width, -renderer.ControlClientPadding.Height);
    this._layoutInProgress = true;
    try
    {
      if (this._titleBarBounds != Rectangle.Empty)
        this.ActivateButtons();
      this.LayoutDocuments(renderer, graphics, this._tabStripBounds);
      foreach (DockControl control in (CollectionBase) this._controls)
      {
        if (control.Parent != this.DockContainer)
        {
          if (control.Parent != null)
            DockHelper.DetachControl((Control) control);
          this.DockContainer.Controls.Add((Control) control);
        }
        control.Visible = control == this._selectedControl;
        control.Bounds = documentBounds;
      }
    }
    finally
    {
      this._layoutInProgress = false;
    }
  }

  protected internal virtual void LayoutCollapsed(RendererBase renderer, Rectangle bounds)
  {
    this._titleBarBounds = bounds;
    this._titleBarBounds.Offset(0, renderer.TitleBarMetrics.Margin.Top);
    this._titleBarBounds.Height = renderer.TitleBarMetrics.Height - (renderer.TitleBarMetrics.Margin.Top + renderer.TitleBarMetrics.Margin.Bottom);
    this.ActivateButtons();
    bounds.Offset(0, renderer.TitleBarMetrics.Height);
    bounds.Height -= renderer.TitleBarMetrics.Height;
    this._documentBounds = bounds;
    this._tabStripBounds = Rectangle.Empty;
    foreach (DockControl control in (CollectionBase) this._controls)
    {
      control.Visible = control == this._selectedControl;
      control.Bounds = this._documentBounds;
    }
  }

  internal virtual void OnButtonPress(TitleButton button)
  {
  }

  internal virtual void OnButtonPressed(TitleButton button)
  {
    if (this._activeButton == this._closeButton)
    {
      this.OnCloseButtonClick(EventArgs.Empty);
    }
    else
    {
      if (this._activeButton != this._pinButton)
        return;
      this.OnPinButtonClick();
    }
  }

  protected virtual void OnCloseButtonClick(EventArgs e)
  {
    if (this.SelectedControl == null)
      return;
    this.SelectedControl.Close();
  }

  protected internal virtual void OnControlEnter(DockControl control) => this.InvalidateContainer();

  protected internal virtual void OnControlLeave(DockControl control) => this.InvalidateContainer();

  internal override void OnDockingManagerCancelled(object sender, EventArgs e)
  {
    base.OnDockingManagerCancelled(sender, e);
    this._startPoint = Point.Empty;
  }

  internal override void OnDockingManagerCommitted(StandardDocker.DockingSite target)
  {
    base.OnDockingManagerCommitted(target);
    if (target == null || target._redockType == StandardDocker.RedockType.None || target._redockType == StandardDocker.RedockType.AlreadyActioned)
      return;
    DockControl selectedControl = this.SelectedControl;
    DockContainer dockContainer = this.DockContainer;
    DockManager manager = this.DockContainer.Manager;
    if (this._wholeMoving)
      ((SplitLayoutSystem) this.Parent).LayoutSystems.Remove((LayoutSystemBase) this);
    else
      DockHelper.DetachDockControl(selectedControl);
    if (target._redockType == StandardDocker.RedockType.Float)
    {
      if (this._wholeMoving)
      {
        this.Float(manager, target._bounds);
      }
      else
      {
        DockControl[] controls = new DockControl[1]
        {
          selectedControl
        };
        this.CreateNewLayoutSystem(selectedControl.Width, selectedControl.Height, controls, selectedControl).Float(manager, target._bounds);
      }
    }
    else
    {
      if (target._dockContainer == null)
        return;
      this.ProcessReDocking(selectedControl, this._wholeMoving, target);
      selectedControl?.Activate();
    }
  }

  protected internal override void OnDragOver(DragEventArgs drgevent)
  {
    base.OnDragOver(drgevent);
    DockControl controlAt = this.GetControlAt(this.DockContainer.PointToClient(new Point(drgevent.X, drgevent.Y)));
    if (controlAt == null || this.SelectedControl == controlAt)
      return;
    this.SelectedControl = controlAt;
  }

  protected internal override void OnMouseDoubleClick()
  {
    Point client = this.DockContainer.PointToClient(Cursor.Position);
    if (this.DockContainer.Manager == null)
      return;
    if (this._titleBarBounds.Contains(client) && !this._closeButton._bounds.Contains(client) && !this._pinButton._bounds.Contains(client) && !this.LockControls && this.Floatable && !this.DockContainer.IsFloating)
      this.Float(this.DockContainer.Manager);
    if (this.LockControls)
      return;
    DockControl controlAt = this.GetControlAt(client);
    if (controlAt == null)
      return;
    if (controlAt.IsFloating)
    {
      controlAt.PerformDock();
      controlAt.Activate();
    }
    else
    {
      if (!controlAt.Floatable)
        return;
      controlAt.Float();
    }
  }

  protected internal override void OnMouseDown(MouseEventArgs mea)
  {
    this._mouseDown = true;
    base.OnMouseDown(mea);
    if (this._titleBarBounds.Contains(mea.X, mea.Y) && this.SelectedControl != null)
      this.SelectedControl.Activate();
    if ((mea.Button & MouseButtons.Left) == MouseButtons.Left)
    {
      if (this._titleBarBounds.Contains(mea.X, mea.Y))
        this._startPoint = new Point(mea.X, mea.Y);
      if (this._activeButton != null)
      {
        this._buttonPressed = true;
        this.InvalidateTitleBar();
        this.OnButtonPress(this._activeButton);
        this._startPoint = Point.Empty;
        return;
      }
    }
    DockControl controlAt = this.GetControlAt(new Point(mea.X, mea.Y));
    if (controlAt == null)
      return;
    controlAt.Activate();
    if ((mea.Button & MouseButtons.Left) != MouseButtons.Left)
      return;
    this._startPoint = new Point(mea.X, mea.Y);
  }

  protected internal override void OnMouseLeave()
  {
    base.OnMouseLeave();
    if (this._activeButton != null)
    {
      this._activeButton = (TitleButton) null;
      this.InvalidateTitleBar();
    }
    this._buttonPressed = false;
    this._skipMouseUp = false;
  }

  protected internal override void OnMouseMove(MouseEventArgs e)
  {
    if (this.DockContainer == null)
      return;
    this.DockContainer.Cursor = Cursors.Default;
    if (this._layoutInProgress)
      return;
    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
    {
      if (this._docker != null)
      {
        this._docker.Update(Cursor.Position);
        return;
      }
      Rectangle rectangle = new Rectangle(this._startPoint, SystemInformation.DragSize);
      rectangle.Offset(-(rectangle.Width / 2), -(rectangle.Height / 2));
      if (!rectangle.Contains(e.X, e.Y) && this.IsInContainer && this._startPoint != Point.Empty && !this.Collapsed && !this.LockControls)
      {
        DockControl controlAt = this.GetControlAt(this._startPoint);
        this._wholeMoving = controlAt == null;
        DockingHints hints = this.DockContainer.Manager == null ? this.DockContainer.DockingHints : this.DockContainer.Manager.DockingHints;
        DockingManager dockingManager = this.DockContainer.Manager == null ? this.DockContainer.DockingManager : this.DockContainer.Manager.DockingManager;
        this.CreateDocker(this.DockContainer.Manager, this.DockContainer, (LayoutSystemBase) this, controlAt, this._startPoint, hints, dockingManager, this._wholeMoving ? this.Floatable : controlAt.Floatable);
        return;
      }
    }
    TitleButton buttonAt = this.GetButtonAt(e.X, e.Y);
    if (buttonAt == this._activeButton)
      return;
    this._activeButton = buttonAt;
    this.InvalidateTitleBar();
  }

  protected internal override void OnMouseUp(MouseEventArgs e)
  {
    base.OnMouseUp(e);
    this._mouseDown = false;
    if (this._docker != null)
      this._skipMouseUp = false;
    if (this._skipMouseUp)
    {
      this._skipMouseUp = false;
    }
    else
    {
      this._startPoint = Point.Empty;
      if (this._docker != null)
        this._docker.OnCommit();
      else if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
      {
        DockControl dc = this.GetControlAt(new Point(e.X, e.Y));
        if (dc == null && this._titleBarBounds.Contains(e.X, e.Y))
          dc = this.SelectedControl;
        if (dc == null || !this.IsInContainer)
          return;
        Point screen = this.DockContainer.PointToScreen(new Point(e.X, e.Y));
        Point client = dc.PointToClient(screen);
        this.DockContainer.OnShowControlContextMenu(new ShowControlContextMenuEventArgs(dc, client));
      }
      else if ((e.Button & MouseButtons.Left) == MouseButtons.Left && this._activeButton != null)
      {
        this._buttonPressed = false;
        this.InvalidateTitleBar();
        this.OnButtonPressed(this._activeButton);
      }
      else
      {
        if ((e.Button & MouseButtons.Middle) != MouseButtons.Middle)
          return;
        DockControl dockControl = this.GetControlAt(new Point(e.X, e.Y));
        if (dockControl == null && this._titleBarBounds.Contains(e.X, e.Y))
          dockControl = this.SelectedControl;
        if (dockControl == null || dockControl.DockLocation != DockLocation.Document || !dockControl.Closable || !this.IsInContainer)
          return;
        dockControl.Close();
      }
    }
  }

  protected virtual void OnPinButtonClick()
  {
    this._activeButton = (TitleButton) null;
    this.Collapsed = !this.Collapsed;
    if (!this.IsInContainer || this.SelectedControl == null)
      return;
    if (this.Collapsed)
    {
      this.DockContainer.AutoHideManager.PopupDockControl(this.SelectedControl, true, false);
      this.DockContainer.AutoHideManager.Hide(false);
    }
    else
    {
      this.SelectedControl.Activate();
      this.SelectedControl.Focus();
    }
  }

  internal override void Paint(RendererBase renderer, Graphics graphics, Font font)
  {
    if (this.IsInContainer && this.DockContainer.FriendDesignMode)
    {
      ISelectionService service = (ISelectionService) this.DockContainer.GetService(typeof (ISelectionService));
      this._focused = false;
      foreach (DockControl control in (CollectionBase) this.Controls)
      {
        if (service.GetComponentSelected((object) control))
        {
          this._focused = true;
          break;
        }
      }
    }
    else
      this._focused = this.ContainsFocus;
    if (this.SelectedControl != null)
      renderer.DrawControlClientBackground(graphics, this._documentBounds, this.SelectedControl.BackColor);
    else
      renderer.DrawControlClientBackground(graphics, this._documentBounds, SystemColors.Control);
    if ((this._controls.Count > 1 || this.DockContainer != null && this.DockContainer.FriendDesignMode) && this._tabStripBounds != Rectangle.Empty)
    {
      int selectedTabOffset = 0;
      if (this._selectedControl != null)
        selectedTabOffset = this._selectedControl._tabBounds.X - this.Bounds.Left;
      renderer.DrawTabStripBackground(graphics, this._tabStripBounds, selectedTabOffset);
      foreach (DockControl control in (CollectionBase) this._controls)
      {
        DrawItemState state = DrawItemState.Default;
        if (this._selectedControl == control)
          state |= DrawItemState.Selected;
        bool drawSeparator = true;
        if (this._selectedControl != null && this._controls.IndexOf(control) == this._controls.IndexOf(this._selectedControl) - 1)
          drawSeparator = false;
        renderer.DrawTabStripTab(graphics, control._tabBounds, control.WorkingTabImage, control.TabText, control.Font, control.BackColor, control.ForeColor, state, drawSeparator);
      }
    }
    Rectangle titleBarBounds = this._titleBarBounds;
    if (titleBarBounds == Rectangle.Empty || titleBarBounds.Width <= 0 || titleBarBounds.Height <= 0)
      return;
    renderer.DrawTitleBarBackground(graphics, titleBarBounds, this._focused);
    if (this._closeButton._visible)
      titleBarBounds.Width -= 16 /*0x10*/;
    if (this._pinButton._visible)
      titleBarBounds.Width -= 16 /*0x10*/;
    Rectangle bounds1 = renderer.TitleBarMetrics.RemovePadding(titleBarBounds);
    if (bounds1.Width > 8)
      renderer.DrawTitleBarText(graphics, bounds1, this._focused, this._selectedControl == null ? "Empty Layout System" : this._selectedControl.Text, this.DockContainer.Font);
    Rectangle bounds2;
    if (this._closeButton._visible)
    {
      int left1 = this._closeButton._bounds.Left;
      bounds2 = this.Bounds;
      int left2 = bounds2.Left;
      if (left1 > left2)
      {
        DrawItemState state = DrawItemState.Default;
        if (this._activeButton == this._closeButton)
        {
          state |= DrawItemState.HotLight;
          if (this._buttonPressed)
            state |= DrawItemState.Selected;
        }
        renderer.DrawTitleBarButton(graphics, this._closeButton._bounds, ButtonType.Close, state, this._focused, false);
      }
    }
    if (!this._pinButton._visible)
      return;
    int left3 = this._pinButton._bounds.Left;
    bounds2 = this.Bounds;
    int left4 = bounds2.Left;
    if (left3 <= left4)
      return;
    DrawItemState state1 = DrawItemState.Default;
    if (this._activeButton == this._pinButton)
    {
      state1 |= DrawItemState.HotLight;
      if (this._buttonPressed)
        state1 |= DrawItemState.Selected;
    }
    renderer.DrawTitleBarButton(graphics, this._pinButton._bounds, ButtonType.Pin, state1, this._focused, this.Collapsed);
  }

  internal override void SetDockContainer(DockContainer dockContainer)
  {
    if (dockContainer == null && this.IsInContainer)
    {
      foreach (DockControl control in (CollectionBase) this.Controls)
      {
        if (control.Parent == this.DockContainer)
          DockHelper.DetachControl((Control) control);
      }
    }
    foreach (DockControl control in (CollectionBase) this.Controls)
      control.AssingContainer(dockContainer);
    base.SetDockContainer(dockContainer);
  }

  public void SplitForLayoutSystem(LayoutSystemBase layoutSystem, DockSide side)
  {
    if (layoutSystem == null)
      throw new ArgumentNullException();
    if (side == DockSide.None)
      throw new ArgumentException();
    if (layoutSystem.Parent != null)
      throw new InvalidOperationException("This layout system must be removed from its parent before it can be moved to a new layout system.");
    SplitLayoutSystem splitLayoutSystem = this.Parent != null ? (SplitLayoutSystem) this.Parent : throw new InvalidOperationException("This layout system is not parented yet.");
    if (splitLayoutSystem.SplitMode == Orientation.Horizontal)
    {
      if (side == DockSide.Top || side == DockSide.Bottom)
        this.a(layoutSystem, side == DockSide.Top ? splitLayoutSystem.LayoutSystems.IndexOf((LayoutSystemBase) this) : splitLayoutSystem.LayoutSystems.IndexOf((LayoutSystemBase) this) + 1, true);
      else
        this.a(layoutSystem, Orientation.Vertical, side == DockSide.Left);
    }
    else
    {
      if (splitLayoutSystem.SplitMode != Orientation.Vertical)
        return;
      if (side == DockSide.Left || side == DockSide.Right)
        this.a(layoutSystem, side == DockSide.Left ? splitLayoutSystem.LayoutSystems.IndexOf((LayoutSystemBase) this) : splitLayoutSystem.LayoutSystems.IndexOf((LayoutSystemBase) this) + 1, false);
      else
        this.a(layoutSystem, Orientation.Horizontal, side == DockSide.Top);
    }
  }

  [DefaultValue(false)]
  [Browsable(false)]
  public virtual bool Collapsed
  {
    get => this._collapsed;
    set
    {
      if (this._collapsed == value)
        return;
      this._collapsed = value;
      if (!this.IsInContainer || !this.DockContainer.CanShowCollapsed)
        return;
      if (this._collapsed)
      {
        foreach (DockControl control in (CollectionBase) this._controls)
        {
          if (control.Parent == this.DockContainer)
            DockHelper.DetachControl((Control) control);
        }
      }
      else
      {
        if (this.Parent != null)
          ((SplitLayoutSystem) this.Parent).a(this);
        if (this.PopupContainer != null)
          this.PopupContainer.DetachAutoHideManager();
      }
      if (!this.IsInContainer)
        return;
      this.DockContainer.LayoutSystemsChanged();
    }
  }

  private bool ContainsFocus
  {
    get
    {
      foreach (Control control in (CollectionBase) this._controls)
      {
        if (control.ContainsFocus)
          return true;
      }
      return false;
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ControlLayoutSystem.DockControlCollection Controls => this._controls;

  public Guid Guid => this._guid;

  public bool IsPoppedUp => this.PopupContainer != null;

  internal Rectangle JoinCatchmentBounds => this._joinCatchmentBounds;

  public bool LockControls
  {
    get => this._lockControls;
    set => this._lockControls = value;
  }

  internal PopupContainer PopupContainer
  {
    get => this._popupContainer;
    set => this._popupContainer = value;
  }

  internal int PopupSize
  {
    get
    {
      if (this._popupSize != 0)
        return this._popupSize;
      return this.IsInContainer ? this.DockContainer.ContentSize : 200;
    }
    set => this._popupSize = value;
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual DockControl SelectedControl
  {
    get => this._selectedControl;
    set
    {
      if (value != null && !this._controls.Contains(value))
        throw new ArgumentOutOfRangeException();
      DockControl selectedControl = this._selectedControl;
      if (selectedControl != null)
        selectedControl.PrimaryControl = selectedControl.ActiveControl;
      this._selectedControl = value;
      this.Repaint();
      if (this.IsPoppedUp)
      {
        selectedControl?.OnAutoHidePopupClosed(EventArgs.Empty);
        if (this._selectedControl != null)
          this._selectedControl.OnAutoHidePopupOpened(EventArgs.Empty);
      }
      this.OnSelectedControlChanged(selectedControl, this._selectedControl);
    }
  }

  private bool Floatable
  {
    get
    {
      foreach (DockControl control in (CollectionBase) this.Controls)
      {
        if (!control.Floatable)
          return false;
      }
      return true;
    }
  }

  internal virtual DockControl FindLastUsedControl() => this._controls[0];

  internal delegate void ControlLayoutSystemEventHandler(
    DockControl oldControl,
    DockControl newControl);

  public class DockControlCollection : CollectionBase
  {
    private ControlLayoutSystem _layoutSystem;
    private bool _rangeAdding;
    private bool _updating;

    internal DockControlCollection(ControlLayoutSystem A_0)
    {
      this._rangeAdding = false;
      this._updating = false;
      this._layoutSystem = A_0;
    }

    private void Control_Leave(object sender, EventArgs A_1)
    {
      this._layoutSystem.OnControlLeave((DockControl) sender);
    }

    public int Add(DockControl control)
    {
      int count = this.Count;
      this.Insert(count, control);
      return count;
    }

    public void AddRange(DockControl[] controls)
    {
      this._rangeAdding = true;
      foreach (DockControl control in controls)
        this.Add(control);
      this._rangeAdding = false;
      this._layoutSystem.Repaint();
    }

    private void Control_Enter(object sender, EventArgs e)
    {
      this._layoutSystem.OnControlEnter((DockControl) sender);
      if (!this._layoutSystem.IsInContainer || this._layoutSystem.DockContainer.ImplicitManager == null)
        return;
      this._layoutSystem.DockContainer.ImplicitManager.OnDockControlActivated((DockControl) sender);
    }

    public bool Contains(DockControl control) => this.List.Contains((object) control);

    public void CopyTo(DockControl[] array, int index) => this.List.CopyTo((Array) array, index);

    public int IndexOf(DockControl control) => this.List.IndexOf((object) control);

    public void Insert(int index, DockControl control)
    {
      if (control == null || control.LayoutSystem == this._layoutSystem && (this.IndexOf(control) == index || this.Count == 1))
        return;
      if (control.LayoutSystem != null)
      {
        if (this.Contains(control) && this.IndexOf(control) < index)
          --index;
        control.LayoutSystem.Controls.Remove(control);
      }
      this.List.Insert(index, (object) control);
    }

    protected override void OnClear()
    {
      base.OnClear();
      DocumentContainer documentContainer = (DocumentContainer) null;
      if (this._layoutSystem != null)
        documentContainer = this._layoutSystem.DockContainer as DocumentContainer;
      foreach (DockControl document in (CollectionBase) this)
      {
        document.Enter -= new EventHandler(this.Control_Enter);
        document.Leave -= new EventHandler(this.Control_Leave);
        document._layoutSystem = (ControlLayoutSystem) null;
        documentContainer?.DocumentRemoved(document);
      }
    }

    protected override void OnClearComplete()
    {
      base.OnClearComplete();
      this._layoutSystem.SelectedControl = (DockControl) null;
      this._layoutSystem.Repaint();
    }

    protected override void OnInsertComplete(int index, object value)
    {
      base.OnInsertComplete(index, value);
      if (this._updating)
        return;
      DockControl document = (DockControl) value;
      document._layoutSystem = this._layoutSystem;
      if (this._layoutSystem.IsInContainer && this._layoutSystem.DockContainer.Manager != null && this._layoutSystem.DockContainer.Manager != document.Manager)
        document.Manager = this._layoutSystem.DockContainer.Manager;
      if (this._layoutSystem.IsInContainer)
        document.AssingContainer(this._layoutSystem.DockContainer);
      if (this._layoutSystem._selectedControl == null)
        this._layoutSystem.SelectedControl = document;
      document.Enter += new EventHandler(this.Control_Enter);
      document.Leave += new EventHandler(this.Control_Leave);
      if (this._layoutSystem != null && this._layoutSystem.DockContainer is DocumentContainer)
        ((DocumentContainer) this._layoutSystem.DockContainer).AddDocumentDockControl(document);
      if (this._rangeAdding)
        return;
      this._layoutSystem.Repaint();
    }

    protected override void OnRemoveComplete(int index, object value)
    {
      base.OnRemoveComplete(index, value);
      if (this._updating)
        return;
      DockControl document = (DockControl) value;
      if (this._layoutSystem.IsInContainer && document.Parent == this._layoutSystem.DockContainer)
        DockHelper.DetachControl((Control) document);
      document._layoutSystem = (ControlLayoutSystem) null;
      document.Enter -= new EventHandler(this.Control_Enter);
      document.Leave -= new EventHandler(this.Control_Leave);
      if (this._layoutSystem._selectedControl == value)
        this._layoutSystem.SelectedControl = this._layoutSystem._controls.Count != 0 ? this._layoutSystem.FindLastUsedControl() : (DockControl) null;
      if (this._layoutSystem != null && this._layoutSystem.DockContainer is DocumentContainer)
        ((DocumentContainer) this._layoutSystem.DockContainer).DocumentRemoved(document);
      this._layoutSystem.Repaint();
    }

    public void Remove(DockControl control)
    {
      if (this._layoutSystem.IsInContainer && !this._layoutSystem.DockContainer.IsFloating)
        control.LastIndexInFixedLayoutSystem = this.IndexOf(control);
      this.List.Remove((object) control);
    }

    public void SetChildIndex(DockControl control, int index)
    {
      if (control == null)
        throw new ArgumentNullException();
      if (!this.Contains(control))
        throw new ArgumentOutOfRangeException();
      if (this.IndexOf(control) < index)
        --index;
      this._updating = true;
      this.List.Remove((object) control);
      this.List.Insert(index, (object) control);
      this._updating = false;
      this._layoutSystem.Repaint();
    }

    public DockControl this[int index] => (DockControl) this.List[index];

    internal int PersistableCount
    {
      get
      {
        int persistableCount = 0;
        foreach (DockControl dockControl in (CollectionBase) this)
        {
          if (dockControl.PersistState)
            ++persistableCount;
        }
        return persistableCount;
      }
    }
  }
}
