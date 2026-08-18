
// Type: Intermech.Docking.DockControl
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Docking;

[DefaultEvent("Closing")]
[ToolboxItem(false)]
[Designer(typeof (DockControlDesigner))]
public class DockControl : UserControl
{
  protected string _ExtraText;
  private DockManager _manager;
  internal ControlLayoutSystem _layoutSystem;
  internal Rectangle _tabBounds;
  private static Image _defaultImage;
  private Image _workingTabImage;
  private Image _imgListImage;
  private int _imageIndex;
  private bool _showImageInDocumentTab;
  private Intermech.Docking.Rendering.BorderStyle _borderStyle;
  private bool _ignoreFontEvents;
  internal bool _textTrimmed;
  private string _toolTipText;
  private string _tabText;
  private bool _allowClose;
  private bool _hideOnClose;
  private bool _collapsible;
  private bool _persistState;
  private Guid _guid;
  private int _persistId;
  private string _persistString;
  private Size _floatingSize;
  private Point _floatingLocation;
  private bool _floatable;
  internal bool _firstShowed;
  private Control _primaryControl;
  private DockLocation _dockLocation;
  private DockLocation _lastDockLocation;
  private DockLocation _allowedDockLocations;
  private DockState _showHint;
  private DateTime _lastFocused = DateTime.MinValue;
  private int _layoutIndex;
  private Guid _layoutGuid;
  private int _lastIndexInFixedLayoutSystem;

  public event EventHandler BeforeFirstShown;

  public event EventHandler Closed;

  public event CancelEventHandler Closing;

  public event EventHandler AutoHidePopupClosed;

  public event EventHandler AutoHidePopupOpened;

  public event EventHandler DockSituationChanged;

  public DockControl()
  {
    this._manager = (DockManager) null;
    this._layoutSystem = (ControlLayoutSystem) null;
    this._tabBounds = Rectangle.Empty;
    this._workingTabImage = (Image) null;
    this._imgListImage = (Image) null;
    this._imageIndex = -1;
    this._showImageInDocumentTab = false;
    this._borderStyle = Intermech.Docking.Rendering.BorderStyle.None;
    this._ignoreFontEvents = false;
    this._textTrimmed = false;
    this._toolTipText = string.Empty;
    this._tabText = string.Empty;
    this._allowClose = true;
    this._collapsible = true;
    this._persistState = true;
    this._guid = Guid.NewGuid();
    this._persistId = -1;
    this._persistString = string.Empty;
    this._floatingSize = new Size(250, 400);
    this._floatingLocation = new Point(-1, -1);
    this._floatable = true;
    this._firstShowed = false;
    this._primaryControl = (Control) null;
    this._dockLocation = DockLocation.Right;
    this._allowedDockLocations = DockLocation.All;
    this._lastDockLocation = DockLocation.Unknown;
    this._showHint = DockState.Unknown;
    this._layoutIndex = -1;
    this._layoutGuid = Guid.Empty;
    this._lastIndexInFixedLayoutSystem = -1;
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this.SetStyle(ControlStyles.DoubleBuffer, true);
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
    if (DockControl._defaultImage != null)
      return;
    using (Stream manifestResourceStream = typeof (DockControl).Assembly.GetManifestResourceStream("Resources.defdock.png"))
      DockControl._defaultImage = Image.FromStream(manifestResourceStream);
  }

  public DockControl(Control control, string text)
    : this()
  {
    this.Controls.Add(control);
    control.Dock = DockStyle.Fill;
    this.Text = text;
  }

  internal void AssingContainer(DockContainer dockContainer)
  {
    if (dockContainer == null)
      return;
    if (dockContainer.Manager != null && dockContainer.Manager != this.Manager)
      this.Manager = dockContainer.Manager;
    if (dockContainer.IsFloating)
    {
      this._dockLocation = DockLocation.Float;
    }
    else
    {
      switch (dockContainer.Dock)
      {
        case DockStyle.Top:
          this._dockLocation = DockLocation.Top;
          break;
        case DockStyle.Bottom:
          this._dockLocation = DockLocation.Bottom;
          break;
        case DockStyle.Left:
          this._dockLocation = DockLocation.Left;
          break;
        case DockStyle.Right:
          this._dockLocation = DockLocation.Right;
          break;
        case DockStyle.Fill:
          this._dockLocation = !(dockContainer is DocumentContainer) ? DockLocation.Center : DockLocation.Document;
          break;
      }
      this._lastDockLocation = this._dockLocation;
      this._layoutIndex = dockContainer.LayoutSystem.LayoutSystems.IndexOf((LayoutSystemBase) this._layoutSystem);
      this._layoutGuid = this._layoutSystem.Guid;
    }
    this.OnDockSituationChanged(EventArgs.Empty);
  }

  protected virtual void OnDockSituationChanged(EventArgs e)
  {
    if (this.DockSituationChanged == null)
      return;
    this.DockSituationChanged((object) this, e);
  }

  public void SetFloatingValues(Size A_0, Point A_1, DockLocation A_2)
  {
    this._floatingSize = A_0;
    this._floatingLocation = A_1;
    this._dockLocation = A_2;
  }

  internal static void PaintBorder(Control target, Graphics g, Intermech.Docking.Rendering.BorderStyle borderStyle)
  {
    if (borderStyle == Intermech.Docking.Rendering.BorderStyle.None)
      return;
    Rectangle rectangle = new Rectangle(0, 0, target.Width, target.Height);
    if (borderStyle == Intermech.Docking.Rendering.BorderStyle.Flat)
    {
      --rectangle.Width;
      --rectangle.Height;
      g.DrawRectangle(SystemPens.ControlDark, rectangle);
    }
    else
    {
      Border3DStyle style;
      switch (borderStyle - 1)
      {
        case Intermech.Docking.Rendering.BorderStyle.None:
          style = Border3DStyle.Flat;
          break;
        case Intermech.Docking.Rendering.BorderStyle.Flat:
          style = Border3DStyle.Raised;
          break;
        case Intermech.Docking.Rendering.BorderStyle.RaisedThick:
          style = Border3DStyle.RaisedInner;
          break;
        case Intermech.Docking.Rendering.BorderStyle.RaisedThin:
          style = Border3DStyle.Sunken;
          break;
        default:
          style = Border3DStyle.SunkenOuter;
          break;
      }
      ControlPaint.DrawBorder3D(g, rectangle, style);
    }
  }

  public virtual void Activated()
  {
    ControlLayoutSystem.DockControlCollection controls = this._layoutSystem.Controls;
    int index = controls.IndexOf(this);
    if (this._firstShowed)
      return;
    this._firstShowed = true;
    this.OnBeforeFirstShown(EventArgs.Empty);
    if (this.LayoutSystem != null || index >= controls.Count)
      return;
    controls[index].Activate();
  }

  public virtual void Deactivated()
  {
  }

  private void FocusControl(Control c)
  {
    if (c.IsDisposed)
      return;
    c.Focus();
  }

  public void Activate()
  {
    if (this.LayoutSystem == null || this.Parent == null)
      return;
    ControlLayoutSystem.DockControlCollection controls = this._layoutSystem.Controls;
    int index = controls.IndexOf(this);
    if (this.LayoutSystem.SelectedControl != this)
    {
      if (this.Manager != null)
      {
        CancelEventArgs args = new CancelEventArgs();
        this.Manager.OnDockControlActivating(this, args);
        if (args.Cancel)
          return;
      }
      this.LayoutSystem.SelectedControl = this;
    }
    if (this.LayoutSystem == null)
    {
      if (index >= controls.Count)
        return;
      controls[index].Activate();
    }
    else
    {
      if (this.IsFloating)
        ((FloatingDockContainer) this.LayoutSystem.DockContainer).ActivateForm();
      this.Parent.GetContainerControl().ActiveControl = this.ActiveControl;
      if (!this.ContainsFocus)
      {
        if (this.PrimaryControl != null)
          this.FocusControl(this.PrimaryControl);
        else
          this.SelectNextControl((Control) this, true, true, true, true);
        if (!this.ContainsFocus)
        {
          if (this.Controls.Count == 1)
            this.FocusControl(this.Controls[0]);
          else
            this.Focus();
        }
      }
      else if (this.PrimaryControl != null)
        this.FocusControl(this.PrimaryControl);
      if (this.Manager == null)
        return;
      this.Manager.OnDockControlActivated(this);
    }
  }

  private void CheckManager()
  {
    if (this._manager == null || this._manager._dockContainers.Count == 0)
      throw new InvalidOperationException("No DockManager is associated with this DockControl.");
  }

  public void ReplaceTo(DockControl target)
  {
    target._layoutGuid = this._layoutGuid;
    target._persistId = -1;
    target._persistString = string.Empty;
    target._dockLocation = this._dockLocation;
    target._lastDockLocation = this._lastDockLocation;
    target._lastIndexInFixedLayoutSystem = this._lastIndexInFixedLayoutSystem;
    ControlLayoutSystem layoutSystem = this._layoutSystem;
    ControlLayoutSystem.DockControlCollection controls = this._layoutSystem.Controls;
    int index = controls.IndexOf(this);
    DockManager manager = this.Manager;
    manager?.Lock();
    try
    {
      controls.Remove(this);
      controls.Insert(index, target);
    }
    finally
    {
      manager?.UnLock();
    }
    layoutSystem.SelectedControl = target;
    target.Manager = this.Manager;
    target.Activate();
    this.Dispose();
  }

  public void Close()
  {
    if (!this.IsInContainer)
      return;
    DocumentClosingEventArgs e1 = new DocumentClosingEventArgs(this, false);
    DocumentContainer dockContainer = this._layoutSystem.DockContainer as DocumentContainer;
    if (this._layoutSystem.DockContainer is DocumentContainer)
      ((DocumentContainer) this._layoutSystem.DockContainer).OnDocumentClosing(e1);
    if (e1.Cancel)
      return;
    CancelEventArgs e2 = new CancelEventArgs();
    this.OnClosing(e2);
    if (e2.Cancel)
      return;
    if (this._manager != null && this.ContainsFocus)
      this._manager.OnDockControlDeactivated(this);
    if (this.IsFloating && this._layoutSystem.DockContainer.HasSingleControlLayoutSystem && this._layoutSystem.Controls.Count == 1)
      ((FloatingDockContainer) this._layoutSystem.DockContainer).HideForm();
    else
      DockHelper.DetachDockControl(this);
    dockContainer?.OnDocumentClosed(this);
    this.OnClosed(EventArgs.Empty);
    if (this.HideOnClose)
      return;
    this.Dispose();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._layoutSystem != null)
        DockHelper.DetachDockControl(this);
      if (this._workingTabImage != null)
        this._workingTabImage.Dispose();
      if (this._imgListImage != null)
        this._imgListImage = (Image) null;
      if (this.Manager != null)
        this.Manager = (DockManager) null;
    }
    base.Dispose(disposing);
  }

  public void Float() => this.Float(this.FloatingBounds);

  public void Float(Rectangle bounds)
  {
    bool flag = false;
    this.CheckManager();
    if (this.IsFloating)
      throw new InvalidOperationException("The dockable window is already floating.");
    Size size = this._floatingSize;
    if (this._layoutSystem != null)
    {
      size = new Size((int) this._layoutSystem._workingSize.Width, (int) this._layoutSystem._workingSize.Height);
      if (this._layoutSystem is DocumentLayoutSystem)
        flag = true;
      DockHelper.DetachDockControl(this);
    }
    ControlLayoutSystem layoutSystem;
    if (flag)
    {
      DockControl[] controls = new DockControl[1]{ this };
      layoutSystem = (ControlLayoutSystem) new DocumentLayoutSystem(size.Width, size.Height, controls, this);
    }
    else
    {
      DockControl[] controls = new DockControl[1]{ this };
      layoutSystem = new ControlLayoutSystem(size.Width, size.Height, controls, this);
    }
    FloatingDockContainer floatingDockContainer = this._manager.CreateFloatingDockContainer();
    floatingDockContainer.LayoutSystem.LayoutSystems.Add((LayoutSystemBase) layoutSystem);
    floatingDockContainer.SetWindowPos(bounds, true, true);
  }

  public static DockControl FromForm(Form form)
  {
    DockControl dockControl = new DockControl();
    form.Visible = false;
    form.TopLevel = false;
    form.FormBorderStyle = FormBorderStyle.None;
    dockControl.Controls.Add((Control) form);
    dockControl.Text = form.Text;
    form.Dock = DockStyle.Fill;
    form.Visible = true;
    return dockControl;
  }

  public Rectangle FloatingBounds
  {
    get
    {
      if (this._floatingLocation.X == -1 && this._floatingLocation.Y == -1)
      {
        Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
        this._floatingLocation = new Point(workingArea.X + workingArea.Width / 2 - this._floatingSize.Width / 2, workingArea.Y + workingArea.Height / 2 - this._floatingSize.Height / 2);
      }
      return new Rectangle(this._floatingLocation, this._floatingSize);
    }
  }

  protected internal virtual void OnBeforeFirstShown(EventArgs e)
  {
    if (this.BeforeFirstShown == null)
      return;
    this.BeforeFirstShown((object) this, e);
  }

  public virtual void OnClosed(EventArgs e)
  {
    if (this.Closed == null)
      return;
    this.Closed((object) this, e);
  }

  protected internal virtual void OnClosing(CancelEventArgs e)
  {
    if (this.Closing == null)
      return;
    this.Closing((object) this, e);
  }

  internal void CheckClose(CancelEventArgs cea) => this.OnClosing(cea);

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string PersistString
  {
    get
    {
      this._persistString = this.GetPersistString();
      return this._persistString;
    }
    set => this._persistString = value;
  }

  internal int PersistId
  {
    get => this._persistId;
    set => this._persistId = value;
  }

  protected virtual string GetPersistString() => this._persistString;

  protected override void OnFontChanged(EventArgs e)
  {
    base.OnFontChanged(e);
    if (this._layoutSystem == null || this.IgnoreFontEvents || this._layoutSystem._layoutInProgress)
      return;
    this._layoutSystem.Repaint();
  }

  protected override void OnMouseDown(MouseEventArgs e)
  {
    base.OnMouseDown(e);
    this.Activate();
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    DockControl.PaintBorder((Control) this, e.Graphics, this._borderStyle);
  }

  protected internal virtual void OnAutoHidePopupClosed(EventArgs e)
  {
    if (this.AutoHidePopupClosed == null)
      return;
    this.AutoHidePopupClosed((object) this, e);
  }

  protected internal virtual void OnAutoHidePopupOpened(EventArgs e)
  {
    if (this.AutoHidePopupOpened == null)
      return;
    this.AutoHidePopupOpened((object) this, e);
  }

  protected override void WndProc(ref Message m)
  {
    if (m.Msg != 33)
    {
      base.WndProc(ref m);
    }
    else
    {
      base.WndProc(ref m);
      if (this.ContainsFocus)
        return;
      this.Activate();
    }
  }

  public new void Show() => this.Show(this._manager);

  public void Show(DockOpenOrder openOrder) => this.Show(this._manager, openOrder);

  public void Show(DockManager manager) => this.Show(manager, this.DefaultShowLocation);

  public void Show(DockManager manager, DockOpenOrder openOrder)
  {
    this.Show(manager, this.DefaultShowLocation, openOrder);
  }

  public void Show(DockManager manager, DockState dockState)
  {
    this.ShowProc(manager, dockState, DockOpenOrder.DefaultOpenOrder);
  }

  public void Show(DockManager manager, DockState dockState, DockOpenOrder openOrder)
  {
    this.ShowProc(manager, dockState, openOrder);
  }

  private void ShowProc(DockManager manager, DockState dockState, DockOpenOrder openOrder)
  {
    if (this._manager != manager)
      this.Manager = manager;
    this.OpenProc(DockHelper.DockStateToLocation(dockState), DockHelper.IsDockStateAutoHide(dockState), openOrder);
  }

  private DockState DefaultShowLocation
  {
    get
    {
      if (this.ShowHint != DockState.Unknown)
        return this.ShowHint;
      if ((this.AllowedStates & DockLocation.Document) != DockLocation.Unknown)
        return DockState.Document;
      if ((this.AllowedStates & DockLocation.Right) != DockLocation.Unknown)
        return DockState.DockRight;
      if ((this.AllowedStates & DockLocation.Left) != DockLocation.Unknown)
        return DockState.DockLeft;
      if ((this.AllowedStates & DockLocation.Bottom) != DockLocation.Unknown)
        return DockState.DockBottom;
      if ((this.AllowedStates & DockLocation.Top) != DockLocation.Unknown)
        return DockState.DockTop;
      return (this.AllowedStates & DockLocation.Float) != DockLocation.Unknown ? DockState.Float : DockState.DockRight;
    }
  }

  public void Open() => this.Open(this._dockLocation, false);

  public void Open(DockOpenOrder openOrder) => this.Open(this._dockLocation, false, openOrder);

  public void Open(DockLocation dockLocation, bool autoHide, Guid layoutGuid)
  {
    this._layoutGuid = layoutGuid;
    this.Open(dockLocation, autoHide);
  }

  public void Open(
    DockLocation dockLocation,
    bool autoHide,
    Guid layoutGuid,
    DockOpenOrder openOrder)
  {
    this._layoutGuid = layoutGuid;
    this.Open(dockLocation, autoHide, openOrder);
  }

  public void Open(DockLocation dockLocation, bool autoHide)
  {
    this.OpenProc(dockLocation, autoHide, DockOpenOrder.DefaultOpenOrder);
  }

  public void Open(DockLocation dockLocation, bool autoHide, DockOpenOrder openOrder)
  {
    this.OpenProc(dockLocation, autoHide, openOrder);
  }

  private void OpenProc(DockLocation dockLocation, bool autoHide, DockOpenOrder openOrder)
  {
    ControlLayoutSystem controlLayoutSystem = this._layoutSystem;
    this._dockLocation = dockLocation;
    if (this._lastDockLocation == DockLocation.Unknown)
      this._lastDockLocation = dockLocation;
    if (controlLayoutSystem == null)
    {
      controlLayoutSystem = this.LastControlLayoutSystem;
      if (!controlLayoutSystem.Controls.Contains(this))
      {
        if (openOrder == DockOpenOrder.NearRight && controlLayoutSystem.SelectedControl != null)
        {
          int num = controlLayoutSystem.Controls.IndexOf(controlLayoutSystem.SelectedControl);
          if (num != -1 && num != controlLayoutSystem.Controls.Count - 1)
            controlLayoutSystem.Controls.Insert(num + 1, this);
          else
            controlLayoutSystem.Controls.Add(this);
        }
        else
          controlLayoutSystem.Controls.Add(this);
      }
    }
    controlLayoutSystem.SelectedControl = this;
    if (controlLayoutSystem.SelectedControl != this)
      return;
    if (this.IsFloating)
    {
      ((FloatingDockContainer) this._layoutSystem.DockContainer).ShowForm();
    }
    else
    {
      if (autoHide || !this._layoutSystem.Collapsed || !this._layoutSystem.DockContainer.CanShowCollapsed)
        return;
      this._layoutSystem.DockContainer.AutoHideManager.PopupDockControl(this, true, true);
    }
  }

  public void PerformDock()
  {
    ControlLayoutSystem controlLayoutSystem = this.LastControlLayoutSystem;
    if (controlLayoutSystem == this._layoutSystem)
      return;
    DockHelper.DetachDockControl(this);
    int index = this.LastIndexInFixedLayoutSystem;
    if (index < 0)
      index = 0;
    else if (index > controlLayoutSystem.Controls.Count)
      index = controlLayoutSystem.Controls.Count;
    controlLayoutSystem.Controls.Insert(index, this);
  }

  public void PerformDock(ControlLayoutSystem layoutSystem) => this.PerformDock(layoutSystem, 0);

  public void PerformDock(ControlLayoutSystem layoutSystem, int index)
  {
    if (this._layoutSystem != layoutSystem)
    {
      DockHelper.DetachDockControl(this);
      layoutSystem.Controls.Insert(index, this);
    }
    else
      layoutSystem.Controls.SetChildIndex(this, index);
    layoutSystem.SelectedControl = this;
  }

  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    if (this._layoutSystem != null)
    {
      switch (keyData)
      {
        case Keys.Prior | Keys.Control:
          int index1 = this._layoutSystem.Controls.IndexOf(this) - 1;
          if (index1 < 0)
            index1 = this._layoutSystem.Controls.Count - 1;
          this._layoutSystem.Controls[index1].Open();
          this._layoutSystem.Controls[index1].Activate();
          return true;
        case Keys.Next | Keys.Control:
          int index2 = this._layoutSystem.Controls.IndexOf(this) + 1;
          if (index2 >= this._layoutSystem.Controls.Count)
            index2 = 0;
          this._layoutSystem.Controls[index2].Open();
          this._layoutSystem.Controls[index2].Activate();
          return true;
        case Keys.F4 | Keys.Control:
          if (this.Closable)
            this.Close();
          return true;
        default:
          if (keyData == (Keys.OemMinus | Keys.Alt) && this._layoutSystem.IsInContainer)
          {
            this._layoutSystem.DockContainer.OnShowControlContextMenu(new ShowControlContextMenuEventArgs(this, new Point(0, 0)));
            return true;
          }
          if (keyData == Keys.Escape && this.Manager != null && this.IsFloating && this.Manager.OwnerForm != null)
          {
            this.Manager.OwnerForm.Activate();
            return true;
          }
          break;
      }
    }
    return base.ProcessCmdKey(ref msg, keyData);
  }

  private bool ShouldSerializeTabText() => this._tabText.Length != 0 && this._tabText != this.Text;

  public bool IsDockLocationValid(DockLocation dockLocation)
  {
    return BaseDocker.IsDockLocationValid(dockLocation, this._allowedDockLocations);
  }

  private string UpdateText(string value)
  {
    if (string.IsNullOrEmpty(value))
      return value;
    value = value.Replace('\n', ' ');
    return value;
  }

  public override Color BackColor
  {
    get => base.BackColor;
    set
    {
      base.BackColor = value;
      if (this._layoutSystem == null || this._layoutSystem.DockContainer == null)
        return;
      this._layoutSystem.DockContainer.Invalidate(this._layoutSystem.Bounds);
    }
  }

  [Category("Appearance")]
  [DefaultValue(typeof (Intermech.Docking.Rendering.BorderStyle), "None")]
  [Description("The type of border to be drawn around the control.")]
  public Intermech.Docking.Rendering.BorderStyle BorderStyle
  {
    get => this._borderStyle;
    set
    {
      this._borderStyle = value;
      this.PerformLayout();
      this.Invalidate();
    }
  }

  [DefaultValue(typeof (Control), null)]
  [Category("Behavior")]
  [Description("The control that will be focused when the window is activated.")]
  public Control PrimaryControl
  {
    get => this._primaryControl;
    set
    {
      this._primaryControl = value;
      while (this._primaryControl is IContainerControl primaryControl && primaryControl.ActiveControl != null)
        this._primaryControl = primaryControl.ActiveControl;
    }
  }

  [Category("Docking")]
  [DefaultValue(false)]
  [Description("Indicates the content will be hidden instead of being closed.")]
  public bool HideOnClose
  {
    get => this._hideOnClose;
    set => this._hideOnClose = value;
  }

  [Category("Docking")]
  [DefaultValue(true)]
  [Description("Indicates whether this control will be closable by the user.")]
  public bool Closable
  {
    get => this._allowClose;
    set
    {
      this._allowClose = value;
      if (this._layoutSystem == null)
        return;
      this._layoutSystem.Repaint();
    }
  }

  [DefaultValue(true)]
  [Description("Indicates whether the user will be able to put this control in to auto-hide mode.")]
  [Category("Docking")]
  public bool Collapsible
  {
    get => this._collapsible;
    set
    {
      this._collapsible = value;
      if (this._layoutSystem == null)
        return;
      this._layoutSystem.Repaint();
    }
  }

  public override Rectangle DisplayRectangle
  {
    get
    {
      Rectangle displayRectangle = base.DisplayRectangle;
      switch (this._borderStyle)
      {
        case Intermech.Docking.Rendering.BorderStyle.Flat:
        case Intermech.Docking.Rendering.BorderStyle.RaisedThin:
        case Intermech.Docking.Rendering.BorderStyle.SunkenThin:
          displayRectangle.Inflate(-1, -1);
          return displayRectangle;
        case Intermech.Docking.Rendering.BorderStyle.RaisedThick:
        case Intermech.Docking.Rendering.BorderStyle.SunkenThick:
          displayRectangle.Inflate(-2, -2);
          return displayRectangle;
        default:
          return displayRectangle;
      }
    }
  }

  [Browsable(false)]
  public override DockStyle Dock
  {
    get => base.Dock;
    set => base.Dock = value;
  }

  [Category("Docking")]
  [Description("Gets or sets a value indicating in which area of the DockManager the content allowed to show.")]
  [DefaultValue(DockLocation.All)]
  [Editor(typeof (DockLocationsEditor), typeof (UITypeEditor))]
  public DockLocation AllowedStates
  {
    get => this._allowedDockLocations;
    set
    {
      if (this._allowedDockLocations == value)
        return;
      this._allowedDockLocations = value;
    }
  }

  [Category("Docking")]
  [Description("The desired docking state when first showing.")]
  [DefaultValue(DockState.Unknown)]
  public DockState ShowHint
  {
    get => this._showHint;
    set
    {
      if (this._showHint == value)
        return;
      this._showHint = value;
    }
  }

  public DockLocation DockLocation => this._dockLocation;

  private FloatingDockContainer FloatingDockContainer
  {
    get => this._layoutSystem.DockContainer as FloatingDockContainer;
  }

  [DefaultValue(typeof (Point), "-1, -1")]
  [Browsable(false)]
  public Point FloatingLocation
  {
    get => this._floatingLocation;
    set
    {
      this._floatingLocation = value;
      if (!this.IsFloating || !(this.FloatingDockContainer.GetLocation() != this._floatingLocation))
        return;
      this.FloatingDockContainer.SetLocation(this._floatingLocation);
    }
  }

  [DefaultValue(true)]
  [Category("Docking")]
  [Description("Indicates whether the user will be able to float the DockControl.")]
  public bool Floatable
  {
    get => this._floatable;
    set => this._floatable = value;
  }

  [Description("Indicates the default size this control will assume when floating on its own.")]
  [DefaultValue(typeof (Size), "250, 400")]
  [Category("Layout")]
  public Size FloatingSize
  {
    get => this._floatingSize;
    set
    {
      if (value.Width <= 0 || value.Height <= 0)
        throw new ArgumentOutOfRangeException();
      this._floatingSize = value;
      if (!this.IsFloating || !(this.FloatingDockContainer.GetSize() != this._floatingSize))
        return;
      this.FloatingDockContainer.SetSize(this._floatingSize);
    }
  }

  public override Color ForeColor
  {
    get => base.ForeColor;
    set
    {
      base.ForeColor = value;
      if (this._layoutSystem == null || this._layoutSystem.DockContainer == null)
        return;
      this._layoutSystem.DockContainer.Invalidate(this._tabBounds);
    }
  }

  public Guid Guid
  {
    get => this._guid;
    set => this._guid = value;
  }

  private bool ShouldSerializeGuid() => true;

  internal bool IgnoreFontEvents
  {
    get => this._ignoreFontEvents;
    set => this._ignoreFontEvents = value;
  }

  [Browsable(false)]
  public bool IsFloating => this.IsInContainer && this._layoutSystem.DockContainer.IsFloating;

  [Browsable(false)]
  public bool IsInContainer
  {
    get => this._layoutSystem != null && this._layoutSystem.DockContainer != null;
  }

  [Browsable(false)]
  public bool IsOpen
  {
    get
    {
      bool isOpen = this.IsInContainer && this._layoutSystem != null && this._layoutSystem.SelectedControl == this;
      if (isOpen && this._layoutSystem.Collapsed)
        isOpen = this._layoutSystem.IsPoppedUp;
      if (isOpen && this.IsFloating)
        isOpen = ((FloatingDockContainer) this._layoutSystem.DockContainer).GetVisible();
      return isOpen;
    }
  }

  internal ControlLayoutSystem LastControlLayoutSystem
  {
    get
    {
      this.CheckManager();
      bool isDocument = false;
      if (this._layoutSystem is DocumentLayoutSystem)
        isDocument = true;
      return DockHelper.FindOrCreateLayoutSystem(this.Manager, this._lastDockLocation, this._layoutIndex, this._layoutGuid, isDocument, false);
    }
  }

  internal int LastIndexInFixedLayoutSystem
  {
    get => this._lastIndexInFixedLayoutSystem;
    set => this._lastIndexInFixedLayoutSystem = value;
  }

  [Browsable(false)]
  public ControlLayoutSystem LayoutSystem => this._layoutSystem;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public DockManager Manager
  {
    get => this._manager;
    set
    {
      if (this._manager != null)
        this._manager.RemoveDockControl(this);
      this._manager = value;
      if (this._manager == null)
        return;
      this._manager.AddDockControl(this);
      if (this._layoutSystem == null)
        return;
      this._layoutSystem.Repaint();
    }
  }

  [Category("Behavior")]
  [Description("Indicates whether the location of the DockControl will be included in layout serialization.")]
  [DefaultValue(true)]
  public bool PersistState
  {
    get => this._persistState;
    set => this._persistState = value;
  }

  public Rectangle TabBounds => this._tabBounds;

  [Description("The image displayed for this control on docking tabs.")]
  [AmbientValue(typeof (Image), null)]
  [Category("Appearance")]
  [DefaultValue(null)]
  public Image TabImage
  {
    get
    {
      if (this._imageIndex == -1)
        return this._workingTabImage;
      if (this._imgListImage == null)
      {
        ImageList imageList = this.ImageList;
        if (imageList != null && this._imageIndex >= 0 && this._imageIndex < imageList.Images.Count)
          this._imgListImage = imageList.Images[this._imageIndex];
      }
      return this._imgListImage;
    }
    set
    {
      this._workingTabImage = value;
      this._imgListImage = (Image) null;
      this._imageIndex = -1;
      if (this._layoutSystem == null)
        return;
      this._layoutSystem.Repaint();
    }
  }

  [Browsable(false)]
  public DockContainer DockContainer
  {
    get => this._layoutSystem != null ? this._layoutSystem.DockContainer : (DockContainer) null;
  }

  private bool ShouldSerializeTabImage() => this._imageIndex > 0;

  [Browsable(false)]
  public virtual ImageList ImageList
  {
    get => this._manager != null ? this._manager.ImageList : (ImageList) null;
  }

  [DefaultValue(-1)]
  [Category("Image")]
  [TypeConverter(typeof (ImageIndexConverter))]
  [Description("Gets or sets the index value of the image assigned to the control.")]
  [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof (UITypeEditor))]
  public int TabImageIndex
  {
    get => this._imageIndex;
    set
    {
      if (this._imageIndex == value)
        return;
      this._imageIndex = value;
      this._imgListImage = (Image) null;
      if (this._layoutSystem == null)
        return;
      this._layoutSystem.Repaint();
    }
  }

  [Localizable(true)]
  [Category("Appearance")]
  [Description("The text to display on the tab for the DockControl. This can be different to the standard text.")]
  public string TabText
  {
    get => this._tabText.Length == 0 ? this.Text : this._tabText;
    set
    {
      value = value != null ? this.UpdateText(value) : throw new ArgumentNullException();
      if (!(this._tabText != value))
        return;
      this._tabText = value;
      if (this._layoutSystem == null)
        return;
      this._layoutSystem.Repaint();
    }
  }

  [Browsable(true)]
  [Category("Appearance")]
  [Description("Control Text")]
  [Localizable(true)]
  [Bindable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public override string Text
  {
    get => base.Text;
    set
    {
      value = this.UpdateText(value);
      if (!(base.Text != value))
        return;
      base.Text = value;
      if (this._layoutSystem != null)
        this._layoutSystem.Repaint();
      if (!this.IsFloating || !this._layoutSystem.DockContainer.HasSingleControlLayoutSystem || this._layoutSystem.SelectedControl != this)
        return;
      ((FloatingDockContainer) this._layoutSystem.DockContainer).e();
    }
  }

  [Browsable(true)]
  [Category("Appearance")]
  [Description("Control Extra Text")]
  [Localizable(true)]
  [Bindable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public virtual string ExtraText
  {
    get => this._ExtraText;
    set
    {
      this._ExtraText = value;
      if (this._layoutSystem != null)
        this._layoutSystem.Repaint();
      if (!this.IsFloating || !this._layoutSystem.DockContainer.HasSingleControlLayoutSystem || this._layoutSystem.SelectedControl != this)
        return;
      ((FloatingDockContainer) this._layoutSystem.DockContainer).e();
    }
  }

  [Localizable(false)]
  [DefaultValue(false)]
  [Description("Show image in DocumentMode tab.")]
  [Category("Appearance")]
  public bool ShowImageInDocumentTab
  {
    get => this._showImageInDocumentTab;
    set
    {
      if (this._showImageInDocumentTab == value)
        return;
      this._showImageInDocumentTab = value;
      if (this._layoutSystem == null)
        return;
      this._layoutSystem.Repaint();
    }
  }

  [Localizable(true)]
  [DefaultValue("")]
  [Description("Gets or sets the text that appears as a ToolTip for the control tab.")]
  [Category("Appearance")]
  public string ToolTipText
  {
    get => this._toolTipText;
    set => this._toolTipText = value != null ? value : throw new ArgumentNullException();
  }

  private bool ShouldSerializeToolTipText() => this._toolTipText.Length > 0;

  internal Image WorkingTabImage => this.TabImage ?? DockControl._defaultImage;

  public virtual string HelpID => "649";

  internal DateTime LastFocused
  {
    get => this._lastFocused;
    set => this._lastFocused = DateTime.Now;
  }
}
