
// Type: Intermech.Docking.DockContainer
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using Intermech.Docking.Rendering;
using Intermech.Util;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.Docking;

[ToolboxItem(false)]
[DefaultEvent("DockingStarted")]
[Designer(typeof (DockContainerDesigner))]
public class DockContainer : Control
{
  private DockManager _manager;
  private SplitLayoutSystem _layoutSystem;
  internal ArrayList _layoutSystems;
  private RendererBase _workingRenderer;
  private RendererBase _renderer;
  private DockingHints _dockingHints;
  private DockingManager _dockingManager;
  private Guid _guid;
  private ToolTips _toolTip;
  private int _minSize;
  private int _maxSize;
  private DockContainerResizer _resizer;
  private Rectangle _splitBounds;
  private Rectangle _n;
  private bool _sizable;
  private AutoHideManager _autoHideManager;
  private bool _autoHideVisible;
  private bool _collapsed;
  private int _contentSize;
  private Rectangle _autoHideBounds;
  internal LayoutSystemBase _activeLayoutSystem;

  public event EventHandler DockingFinished;

  public event EventHandler DockingStarted;

  public event ShowControlContextMenuEventHandler ShowControlContextMenu;

  public DockContainer()
  {
    this._manager = (DockManager) null;
    this._layoutSystem = (SplitLayoutSystem) null;
    this._layoutSystems = (ArrayList) null;
    this._workingRenderer = (RendererBase) null;
    this._dockingHints = DockingHints.TranslucentFill;
    this._dockingManager = DockingManager.Standard;
    this._guid = Guid.NewGuid();
    this._toolTip = (ToolTips) null;
    this._minSize = 50;
    this._maxSize = 0;
    this._resizer = (DockContainerResizer) null;
    this._splitBounds = Rectangle.Empty;
    this._n = Rectangle.Empty;
    this._sizable = true;
    this._autoHideManager = (AutoHideManager) null;
    this._autoHideVisible = false;
    this._collapsed = false;
    this._contentSize = 0;
    this._autoHideBounds = Rectangle.Empty;
    this._activeLayoutSystem = (LayoutSystemBase) null;
    this._layoutSystem = new SplitLayoutSystem();
    this._layoutSystem.SetDockContainer(this);
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.SetStyle(ControlStyles.DoubleBuffer, true);
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this.SetStyle(ControlStyles.Selectable, false);
    this._layoutSystems = new ArrayList();
    this._autoHideManager = new AutoHideManager(this);
    this.AllowDrop = true;
    this._toolTip = new ToolTips((Control) this);
    this._toolTip.GetToolTipText += new ToolTips.GetToolTipTextEventHandler(this.GetToolTipText);
  }

  private void DetachResizer()
  {
    this._resizer.Cancel -= new EventHandler(this.ResizeCancel);
    this._resizer.Commit -= new DockContainerResizer.CommitEventHandler(this.ResizeFinish);
    this._resizer = (DockContainerResizer) null;
  }

  private string GetToolTipText(Point pos)
  {
    LayoutSystemBase layoutSystemAt = this.GetLayoutSystemAt(pos);
    return layoutSystemAt is ControlLayoutSystem ? ((ControlLayoutSystem) layoutSystemAt).GetToolTipText(pos) : string.Empty;
  }

  private void ResizeFinish(int value)
  {
    this.DetachResizer();
    if (this.Vertical)
      this.Width = value;
    else
      this.Height = value;
  }

  internal new object GetService(System.Type type) => base.GetService(type);

  private void AddRecursive(LayoutSystemBase system)
  {
    this._layoutSystems.Add((object) system);
    if (!(system is SplitLayoutSystem))
      return;
    foreach (LayoutSystemBase layoutSystem in (CollectionBase) ((SplitLayoutSystem) system).LayoutSystems)
      this.AddRecursive(layoutSystem);
  }

  private void FillLayoutList(ArrayList list, SplitLayoutSystem splitSystem)
  {
    foreach (LayoutSystemBase layoutSystem in (CollectionBase) splitSystem.LayoutSystems)
    {
      if (layoutSystem is ControlLayoutSystem)
        list.Add((object) layoutSystem);
      else
        this.FillLayoutList(list, (SplitLayoutSystem) layoutSystem);
    }
  }

  private void ResizeCancel(object sender, EventArgs e) => this.DetachResizer();

  internal void PerformResize(LayoutSystemBase layoutSystem, Rectangle bounds)
  {
    if (!this.IsHandleCreated)
      return;
    using (Graphics graphics = this.CreateGraphics())
    {
      if (layoutSystem == this._layoutSystem)
        layoutSystem.Layout(this.WorkingRenderer, graphics, bounds, this.IsFloating);
      else
        layoutSystem.Layout(this.WorkingRenderer, graphics, bounds, false);
      if (!this._autoHideVisible)
        return;
      this._autoHideManager.Layout(this.WorkingRenderer, graphics, this._autoHideBounds);
    }
  }

  public void AddLayoutSystem(SplitLayoutSystem baseSystem, bool toEnd, LayoutSystemBase newSystem)
  {
    int num = (double) newSystem._workingSize.Width < (double) newSystem._workingSize.Height ? (int) newSystem._workingSize.Width : (int) newSystem._workingSize.Height;
    Size size = baseSystem.Bounds.Size;
    LayoutSystemBase[] layoutSystemBaseArray = new LayoutSystemBase[baseSystem.LayoutSystems.Count];
    baseSystem.LayoutSystems.CopyTo(layoutSystemBaseArray, 0);
    baseSystem.LayoutSystems._rangeAdding = true;
    baseSystem.LayoutSystems.Clear();
    if (layoutSystemBaseArray.Length == 1 && layoutSystemBaseArray[0] is SplitLayoutSystem)
    {
      SplitLayoutSystem splitLayoutSystem = (SplitLayoutSystem) layoutSystemBaseArray[0];
      layoutSystemBaseArray = new LayoutSystemBase[splitLayoutSystem.LayoutSystems.Count];
      splitLayoutSystem.LayoutSystems.CopyTo(layoutSystemBaseArray, 0);
      splitLayoutSystem.LayoutSystems.Clear();
    }
    SplitLayoutSystem splitLayoutSystem1 = new SplitLayoutSystem(size.Width, size.Height, baseSystem.SplitMode, layoutSystemBaseArray);
    Orientation splitMode = baseSystem.SplitMode == Orientation.Horizontal ? Orientation.Vertical : Orientation.Horizontal;
    newSystem._workingSize = new SizeF((float) num, (float) num);
    SplitLayoutSystem layoutSystem;
    if (toEnd)
    {
      LayoutSystemBase[] layoutSystems = new LayoutSystemBase[2]
      {
        (LayoutSystemBase) splitLayoutSystem1,
        newSystem
      };
      layoutSystem = new SplitLayoutSystem(0, 0, splitMode, layoutSystems);
    }
    else
    {
      LayoutSystemBase[] layoutSystems = new LayoutSystemBase[2]
      {
        newSystem,
        (LayoutSystemBase) splitLayoutSystem1
      };
      layoutSystem = new SplitLayoutSystem(0, 0, splitMode, layoutSystems);
    }
    baseSystem.LayoutSystems.Add((LayoutSystemBase) layoutSystem);
    baseSystem.LayoutSystems._rangeAdding = false;
    this.LayoutSystemsChanged(true, false);
  }

  public void AddLayoutSystem(
    SplitLayoutSystem baseSystem,
    bool toEnd,
    LayoutSystemBase newSystem,
    Orientation orientation)
  {
    int num = (double) newSystem._workingSize.Width < (double) newSystem._workingSize.Height ? (int) newSystem._workingSize.Width : (int) newSystem._workingSize.Height;
    if (baseSystem.SplitMode == orientation)
    {
      if (toEnd)
        this.LayoutSystem.LayoutSystems.Add(newSystem);
      else
        this.LayoutSystem.LayoutSystems.Insert(0, newSystem);
    }
    else if (baseSystem.LayoutSystems.Count == 1 && baseSystem.LayoutSystems[0] is SplitLayoutSystem && ((SplitLayoutSystem) baseSystem.LayoutSystems[0]).SplitMode == orientation)
    {
      SplitLayoutSystem layoutSystem = (SplitLayoutSystem) baseSystem.LayoutSystems[0];
      if (toEnd)
        layoutSystem.LayoutSystems.Add(newSystem);
      else
        layoutSystem.LayoutSystems.Insert(0, newSystem);
    }
    else
      this.AddLayoutSystem(baseSystem, toEnd, newSystem);
    if (baseSystem.SplitMode == Orientation.Horizontal)
      this.Width += num + 4;
    else
      this.Height += num + 4;
    this.UpdateSize();
  }

  internal void UpdateContentSize(Size size)
  {
    if (this._contentSize >= this._minSize)
      return;
    this._contentSize = !this.Vertical ? size.Height : size.Width;
    if (this._contentSize < this._minSize)
      this._contentSize = this._minSize;
    if (this._contentSize <= this._maxSize || this._maxSize == 0)
      return;
    this._contentSize = this._maxSize;
  }

  public void AddLayoutSystem(LayoutSystemBase newSystem)
  {
    double width = (double) newSystem._workingSize.Width;
    double height = (double) newSystem._workingSize.Height;
    switch (this.Dock)
    {
      case DockStyle.Top:
        this.AddLayoutSystem(this.LayoutSystem, true, newSystem, Orientation.Horizontal);
        break;
      case DockStyle.Bottom:
        this.AddLayoutSystem(this.LayoutSystem, false, newSystem, Orientation.Horizontal);
        break;
      case DockStyle.Left:
        this.AddLayoutSystem(this.LayoutSystem, true, newSystem, Orientation.Vertical);
        break;
      case DockStyle.Right:
        this.AddLayoutSystem(this.LayoutSystem, false, newSystem, Orientation.Vertical);
        break;
    }
  }

  internal ControlLayoutSystem GetLayoutSystem(SplitLayoutSystem layoutSystem)
  {
    foreach (LayoutSystemBase layoutSystem1 in (CollectionBase) layoutSystem.LayoutSystems)
    {
      if (layoutSystem1 is ControlLayoutSystem)
        return (ControlLayoutSystem) layoutSystem1;
      if (layoutSystem1 is SplitLayoutSystem)
      {
        ControlLayoutSystem layoutSystem2 = this.GetLayoutSystem((SplitLayoutSystem) layoutSystem1);
        if (layoutSystem2 != null)
          return layoutSystem2;
      }
    }
    return (ControlLayoutSystem) null;
  }

  private void Application_Idle(object sender, EventArgs e)
  {
    Application.Idle -= new EventHandler(this.Application_Idle);
    bool flag = false;
    while (this.LayoutSystem.Optimize())
      flag = true;
    if (!flag)
      return;
    this.LayoutSystemsChanged(true, true);
  }

  internal void Form_Activated(object sender, EventArgs e)
  {
    IEnumerator enumerator = this._layoutSystems.GetEnumerator();
    try
    {
      do
        ;
      while (enumerator.MoveNext() && (!((LayoutSystemBase) enumerator.Current is ControlLayoutSystem current) || !current.OnActivated()));
    }
    finally
    {
      if (enumerator is IDisposable disposable)
        disposable.Dispose();
    }
  }

  internal void Form_Deactivate(object sender, EventArgs e)
  {
    foreach (LayoutSystemBase layoutSystem in this._layoutSystems)
    {
      if (layoutSystem is ControlLayoutSystem)
        ((ControlLayoutSystem) layoutSystem).OnDeactivated();
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.LayoutSystem.SetDockContainer((DockContainer) null);
      this.LayoutSystem.Dispose();
      this._autoHideManager.Dispose();
      if (this._renderer != null)
      {
        this._renderer.Dispose();
        this._renderer = (RendererBase) null;
      }
      this.Manager = (DockManager) null;
      this._toolTip.GetToolTipText -= new ToolTips.GetToolTipTextEventHandler(this.GetToolTipText);
      this._toolTip.Dispose();
    }
    base.Dispose(disposing);
  }

  public LayoutSystemBase GetLayoutSystemAt(Point position)
  {
    LayoutSystemBase layoutSystemAt = (LayoutSystemBase) null;
    foreach (LayoutSystemBase layoutSystem in this._layoutSystems)
    {
      if (layoutSystem.Bounds.Contains(position) && (!(layoutSystem is ControlLayoutSystem) || !((ControlLayoutSystem) layoutSystem).Collapsed))
      {
        layoutSystemAt = layoutSystem;
        if (layoutSystemAt is ControlLayoutSystem)
          return layoutSystemAt;
      }
    }
    return layoutSystemAt;
  }

  public void CalculateAllMetricsAndLayout()
  {
    this._n = this.DisplayRectangle;
    int hiddenSize = AutoHideManager.GetHiddenSize();
    if (this._autoHideVisible)
    {
      switch (this.Dock)
      {
        case DockStyle.Top:
          this._autoHideBounds = new Rectangle(this._n.Location, new Size(this._n.Width, hiddenSize));
          this._n.Offset(0, hiddenSize);
          this._n.Height -= hiddenSize;
          break;
        case DockStyle.Bottom:
          this._autoHideBounds = new Rectangle(this._n.Left, this._n.Bottom - hiddenSize, this._n.Width, hiddenSize);
          this._n.Height -= hiddenSize;
          break;
        case DockStyle.Left:
          this._autoHideBounds = new Rectangle(this._n.Location, new Size(hiddenSize, this._n.Height));
          this._n.Offset(hiddenSize, 0);
          this._n.Width -= hiddenSize;
          break;
        case DockStyle.Right:
          this._autoHideBounds = new Rectangle(this._n.Right - hiddenSize, this._n.Top, hiddenSize, this._n.Height);
          this._n.Width -= hiddenSize;
          break;
      }
    }
    else
      this._autoHideBounds = Rectangle.Empty;
    if (this._sizable)
    {
      switch (this.Dock)
      {
        case DockStyle.Top:
          this._splitBounds = new Rectangle(this._n.Left, this._n.Bottom - 4, this._n.Width, 4);
          this._n.Height -= 4;
          break;
        case DockStyle.Bottom:
          this._splitBounds = new Rectangle(this._n.Left, this._n.Top, this._n.Width, 4);
          this._n.Offset(0, 4);
          this._n.Height -= 4;
          break;
        case DockStyle.Left:
          this._splitBounds = new Rectangle(this._n.Right - 4, this._n.Top, 4, this._n.Height);
          this._n.Width -= 4;
          break;
        case DockStyle.Right:
          this._splitBounds = new Rectangle(this._n.Left, this._n.Top, 4, this._n.Height);
          this._n.Offset(4, 0);
          this._n.Width -= 4;
          break;
        default:
          this._splitBounds = Rectangle.Empty;
          break;
      }
    }
    else
      this._splitBounds = Rectangle.Empty;
    if (!this._collapsed)
    {
      if (this.Vertical)
      {
        if (this._n.Width > 0)
          this._contentSize = this._n.Width;
      }
      else if (this._n.Height > 0)
        this._contentSize = this._n.Height;
    }
    this.PerformResize((LayoutSystemBase) this._layoutSystem, this._n);
    this.Invalidate();
  }

  internal virtual void LayoutSystemsChanged() => this.LayoutSystemsChanged(false, false);

  internal virtual void LayoutSystemsChanged(bool skipResize, bool skipOptimize)
  {
    this._layoutSystems.Clear();
    this._activeLayoutSystem = (LayoutSystemBase) null;
    this.AddRecursive((LayoutSystemBase) this._layoutSystem);
    if (!skipResize)
      this.UpdateSize();
    if (skipOptimize)
      return;
    Application.Idle += new EventHandler(this.Application_Idle);
  }

  internal void RecreateLayout()
  {
    this.AutoHideManager.Hide(true);
    this._layoutSystem.SetDockContainer((DockContainer) null);
    foreach (LayoutSystemBase layoutSystem in this._layoutSystems)
    {
      if (layoutSystem is ControlLayoutSystem)
        ((ControlLayoutSystem) layoutSystem).Controls.Clear();
    }
    SplitLayoutSystem layoutSystem1 = this._layoutSystem;
    this._layoutSystem = new SplitLayoutSystem();
    layoutSystem1.Dispose();
  }

  internal void LayoutNeeded() => this.CalculateAllMetricsAndLayout();

  protected internal virtual void OnDockingFinished(EventArgs e)
  {
    if (this.DockingFinished != null)
      this.DockingFinished((object) this, e);
    if (this.Manager == null)
      return;
    this.Manager.OnDockingFinished(e);
  }

  protected internal virtual void OnDockingStarted(EventArgs e)
  {
    if (this.DockingStarted != null)
      this.DockingStarted((object) this, e);
    if (this.Manager == null)
      return;
    this.Manager.OnDockingStarted(e);
  }

  protected override void OnDoubleClick(EventArgs e)
  {
    base.OnDoubleClick(e);
    if (this._activeLayoutSystem == null)
      return;
    this._activeLayoutSystem.OnMouseDoubleClick();
  }

  protected override void OnDragOver(DragEventArgs drgevent)
  {
    base.OnDragOver(drgevent);
    Point client = this.PointToClient(new Point(drgevent.X, drgevent.Y));
    if (this._autoHideBounds.Contains(client))
      this._autoHideManager.OnDragOver(client);
    else
      this.GetLayoutSystemAt(client)?.OnDragOver(drgevent);
  }

  protected override void OnFontChanged(EventArgs e)
  {
    base.OnFontChanged(e);
    this.CalculateAllMetricsAndLayout();
  }

  protected override void OnHandleCreated(EventArgs e)
  {
    base.OnHandleCreated(e);
    this.CalculateAllMetricsAndLayout();
  }

  protected override void OnMouseDown(MouseEventArgs e)
  {
    base.OnMouseDown(e);
    if (this._activeLayoutSystem != null)
      this._activeLayoutSystem.OnMouseDown(e);
    else if (this._autoHideBounds.Contains(e.X, e.Y))
    {
      this._autoHideManager.OnMouseDown(e);
    }
    else
    {
      if (!this._splitBounds.Contains(e.X, e.Y) || this.Manager == null || e.Button != MouseButtons.Left)
        return;
      if (this._resizer != null)
        this._resizer.Dispose();
      this._resizer = new DockContainerResizer(this.Manager, this, new Point(e.X, e.Y));
      this._resizer.Cancel += new EventHandler(this.ResizeCancel);
      this._resizer.Commit += new DockContainerResizer.CommitEventHandler(this.ResizeFinish);
    }
  }

  protected override void OnMouseLeave(EventArgs e)
  {
    base.OnMouseLeave(e);
    if (this._activeLayoutSystem != null)
    {
      this._activeLayoutSystem.OnMouseLeave();
      this._activeLayoutSystem = (LayoutSystemBase) null;
    }
    this.Cursor = Cursors.Default;
  }

  protected override void OnMouseMove(MouseEventArgs e)
  {
    base.OnMouseMove(e);
    if (this.Capture)
    {
      if (this._activeLayoutSystem != null)
      {
        this._activeLayoutSystem.OnMouseMove(e);
      }
      else
      {
        if (this._resizer == null)
          return;
        this._resizer.Update(new Point(e.X, e.Y));
      }
    }
    else if (this._autoHideBounds.Contains(e.X, e.Y))
    {
      this._autoHideManager.OnMouseMove(e);
    }
    else
    {
      LayoutSystemBase layoutSystemAt = this.GetLayoutSystemAt(new Point(e.X, e.Y));
      if (layoutSystemAt != null)
      {
        if (this._activeLayoutSystem != null && this._activeLayoutSystem != layoutSystemAt)
          this._activeLayoutSystem.OnMouseLeave();
        layoutSystemAt.OnMouseMove(e);
        this._activeLayoutSystem = layoutSystemAt;
      }
      else
      {
        if (this._activeLayoutSystem != null)
        {
          this._activeLayoutSystem.OnMouseLeave();
          this._activeLayoutSystem = (LayoutSystemBase) null;
        }
        if (this._splitBounds.Contains(e.X, e.Y))
        {
          if (this.Vertical)
            this.Cursor = Cursors.VSplit;
          else
            this.Cursor = Cursors.HSplit;
        }
        else
          this.Cursor = Cursors.Default;
      }
    }
  }

  protected override void OnMouseUp(MouseEventArgs e)
  {
    base.OnMouseUp(e);
    if (this._activeLayoutSystem != null)
      this._activeLayoutSystem.OnMouseUp(e);
    else if (this._resizer != null)
    {
      this._resizer.OnCommit();
    }
    else
    {
      if (!this._autoHideBounds.Contains(e.X, e.Y))
        return;
      this._autoHideManager.OnMouseUp(e);
    }
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    try
    {
      this.WorkingRenderer.StartRenderSession();
      try
      {
        if (!this._collapsed)
          this._layoutSystem.Paint(this.WorkingRenderer, e.Graphics, this.Font);
        if (this._autoHideVisible)
          this._autoHideManager.Paint(e.Graphics, this._autoHideBounds);
        if (!this._sizable || this._collapsed)
          return;
        this.WorkingRenderer.DrawSplitter(e.Graphics, this._splitBounds, this.Dock == DockStyle.Top || this.Dock == DockStyle.Bottom ? Orientation.Horizontal : Orientation.Vertical);
      }
      finally
      {
        this.WorkingRenderer.FinishRenderSession();
      }
    }
    catch (Exception ex)
    {
    }
  }

  protected override void OnPaintBackground(PaintEventArgs pevent)
  {
    this.WorkingRenderer.DrawDockContainerBackground(pevent.Graphics, this.DisplayRectangle);
  }

  protected override void OnResize(EventArgs e)
  {
    base.OnResize(e);
    this.CalculateAllMetricsAndLayout();
  }

  protected internal virtual void OnShowControlContextMenu(ShowControlContextMenuEventArgs e)
  {
    if (this.ShowControlContextMenu != null)
      this.ShowControlContextMenu((object) this, e);
    if (this.Manager == null)
      return;
    this.Manager.OnShowControlContextMenu(e);
  }

  internal DockControl[] GetDockControls()
  {
    ArrayList arrayList = new ArrayList();
    foreach (LayoutSystemBase layoutSystem in this._layoutSystems)
    {
      if (layoutSystem is ControlLayoutSystem)
      {
        foreach (DockControl control in (CollectionBase) ((ControlLayoutSystem) layoutSystem).Controls)
          arrayList.Add((object) control);
      }
    }
    DockControl[] dockControls = new DockControl[arrayList.Count];
    arrayList.CopyTo((Array) dockControls);
    return dockControls;
  }

  internal void Repaint() => this.CalculateAllMetricsAndLayout();

  public bool IsDockLocationValid(DockLocation dockLocation)
  {
    foreach (LayoutSystemBase layoutSystem in this._layoutSystems)
    {
      if (layoutSystem is ControlLayoutSystem && !layoutSystem.IsDockLocationValid(dockLocation))
        return false;
    }
    return true;
  }

  internal void UpdateSize()
  {
    ArrayList list = new ArrayList();
    this.FillLayoutList(list, this._layoutSystem);
    bool flag1 = false;
    bool flag2 = false;
    foreach (ControlLayoutSystem controlLayoutSystem in list)
    {
      if (controlLayoutSystem.Collapsed && this.CanShowCollapsed)
        flag2 = true;
      else
        flag1 = true;
    }
    this._autoHideVisible = flag2;
    this._collapsed = flag2 && !flag1;
    if (this.Dock != DockStyle.None && this.Dock != DockStyle.Fill)
    {
      int num = 0;
      if (this._autoHideVisible)
        num += AutoHideManager.GetHiddenSize();
      if (!this._collapsed & flag1)
        num += this._contentSize + (this.Sizable ? 4 : 0);
      if (this.Vertical)
        this.Width = num;
      else
        this.Height = num;
    }
    this.CalculateAllMetricsAndLayout();
  }

  [DefaultValue(true)]
  public override bool AllowDrop
  {
    get => base.AllowDrop;
    set => base.AllowDrop = value;
  }

  internal Rectangle AutoHideBounds => this._autoHideBounds;

  internal AutoHideManager AutoHideManager => this._autoHideManager;

  [Browsable(false)]
  [DefaultValue(false)]
  public bool AutoHideVisible => this._autoHideVisible;

  [Browsable(false)]
  public override Color BackColor
  {
    get => base.BackColor;
    set => base.BackColor = value;
  }

  [Browsable(false)]
  public override Image BackgroundImage
  {
    get => base.BackgroundImage;
    set => base.BackgroundImage = value;
  }

  [Browsable(false)]
  public bool CanShowCollapsed
  {
    get
    {
      return this.Dock == DockStyle.Left || this.Dock == DockStyle.Top || this.Dock == DockStyle.Right || this.Dock == DockStyle.Bottom;
    }
  }

  [Browsable(false)]
  public bool Collapsed => this._collapsed;

  internal int ContentSize => this._contentSize;

  internal int CurrentSize => this.Vertical ? this._n.Width : this._n.Height;

  [Browsable(false)]
  public override Cursor Cursor
  {
    get => base.Cursor;
    set => base.Cursor = value;
  }

  protected override Size DefaultSize => new Size(0, 0);

  [Browsable(false)]
  public override DockStyle Dock
  {
    get => base.Dock;
    set
    {
      base.Dock = value;
      Orientation orientation = Orientation.Horizontal;
      if (this.Dock == DockStyle.Top || this.Dock == DockStyle.Bottom)
        orientation = Orientation.Vertical;
      if (this._layoutSystem.SplitMode == orientation)
        return;
      this._layoutSystem.SplitMode = orientation;
    }
  }

  [Description("Indicates the type of visual artifacts drawn to the screen to indicate size and position while docking.")]
  [DefaultValue(typeof (DockingHints), "TranslucentFill")]
  [Category("Appearance")]
  public DockingHints DockingHints
  {
    get => this._dockingHints;
    set => this._dockingHints = value;
  }

  [Description("Indicates the method of user interaction during a docking operation.")]
  [DefaultValue(typeof (DockingManager), "Standard")]
  [Category("Behavior")]
  public DockingManager DockingManager
  {
    get => this._dockingManager;
    set => this._dockingManager = value;
  }

  internal bool Empty => this.LayoutSystem.LayoutSystems.Count == 0;

  [Browsable(false)]
  public override Color ForeColor
  {
    get => base.ForeColor;
    set => base.ForeColor = value;
  }

  internal bool FriendDesignMode => this.DesignMode;

  [Browsable(false)]
  public Guid Guid
  {
    get => this._guid;
    set => this._guid = value;
  }

  internal bool HasSingleControlLayoutSystem
  {
    get
    {
      return this.LayoutSystem.LayoutSystems.Count == 1 && this.LayoutSystem.LayoutSystems[0] is ControlLayoutSystem;
    }
  }

  [Browsable(false)]
  public virtual bool IsFloating => false;

  [Browsable(false)]
  public virtual SplitLayoutSystem LayoutSystem
  {
    get => this._layoutSystem;
    set
    {
      if (value == null)
        throw new ArgumentNullException();
      if (this._layoutSystem != null)
        this._layoutSystem.SetDockContainer((DockContainer) null);
      this._layoutSystem = value;
      this._layoutSystem.SetDockContainer(this);
      this.LayoutSystemsChanged();
    }
  }

  internal void SetManager(DockManager value) => this._manager = value;

  [Browsable(false)]
  public DockManager Manager
  {
    get => this._manager;
    set
    {
      if (this._manager == value)
        return;
      if (this._manager != null)
        this._manager.RemoveDockContainer(this);
      this._manager = value;
      if (this._manager == null)
        return;
      this._manager.AddDockContainer(this);
      foreach (DockControl dockControl in this.GetDockControls())
      {
        if (dockControl.Manager != this._manager)
          dockControl.Manager = this._manager;
      }
    }
  }

  [Browsable(false)]
  public DockManager ImplicitManager
  {
    get
    {
      if (this._manager != null)
        return this._manager;
      if (this.Parent is Form parent)
      {
        foreach (object control in (ArrangedElementCollection) parent.Controls)
        {
          if (control is DockContainer dockContainer && dockContainer._manager != null)
            return dockContainer._manager;
        }
      }
      return (DockManager) null;
    }
  }

  [DefaultValue(0)]
  [Description("The largest size this container will allow the user to choose.")]
  [Category("Layout")]
  public int MaxSize
  {
    get => this._maxSize;
    set => this._maxSize = value;
  }

  [Description("The smallest size this container will allow the user to choose.")]
  [DefaultValue(50)]
  [Category("Layout")]
  public int MinSize
  {
    get => this._minSize;
    set => this._minSize = value;
  }

  [Browsable(false)]
  [DefaultValue(typeof (WhidbeyRenderer), null)]
  [Category("Appearance")]
  [Description("The renderer used to calculate object metrics and draw contents.")]
  public virtual RendererBase Renderer
  {
    get => this._workingRenderer;
    set
    {
      this._workingRenderer = value;
      this.LayoutNeeded();
    }
  }

  [DefaultValue(true)]
  [Category("Behavior")]
  [Description("Indicates whether this container will allow the user to resize it.")]
  public bool Sizable
  {
    get => this._sizable;
    set
    {
      this._sizable = value;
      this.CalculateAllMetricsAndLayout();
    }
  }

  [Browsable(false)]
  public override string Text
  {
    get => base.Text;
    set => base.Text = value;
  }

  internal bool Vertical => this.Dock == DockStyle.Left || this.Dock == DockStyle.Right;

  internal RendererBase WorkingRenderer
  {
    get
    {
      if (this._workingRenderer != null)
        return this._workingRenderer;
      if (this._manager != null && this._manager.Renderer != null)
        return this._manager.Renderer;
      if (this._renderer == null)
        this._renderer = (RendererBase) new WhidbeyRenderer();
      return this._renderer;
    }
  }
}
