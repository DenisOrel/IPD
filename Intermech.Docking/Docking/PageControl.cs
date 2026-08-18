
// Type: Intermech.Docking.PageControl
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using Intermech.Docking.Designers;
using Intermech.Docking.Rendering;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.Docking;

[ToolboxItem(true)]
[DefaultEvent("SelectedPageChanged")]
[Designer(typeof (PageControlDesigner))]
[DefaultProperty("TabLayout")]
[ToolboxBitmap(typeof (PageControl))]
[Serializable]
public class PageControl : Control
{
  private Rectangle _tabStripBounds;
  private const int BUTTON_WIDTH = 12;
  private Timer _srollTimer;
  private TitleButton _leftButton;
  private Intermech.Docking.Rendering.BorderStyle _borderStyle;
  private ITabControlRenderer _renderer;
  private PageControl.TabPageCollection _tabPages;
  private TabLayout _tabLayout;
  private TabAlignment _tabAlignment;
  private TabPage _selectedPage;
  private bool _mouseDown;
  private bool _flat;
  private Rectangle _pageArea;
  private const int BUTTON_HEIGHT = 15;
  private TitleButton _rightButton;
  private Rectangle _pageBounds;
  private int _scroll;
  private TitleButton _activeButton;
  private int _maxScroll;
  private ImageList _imageList;

  public event EventHandler SelectedPageChanged;

  public event PageControlCancelEventHandler SelectedPageChanging;

  public PageControl()
  {
    this._borderStyle = Intermech.Docking.Rendering.BorderStyle.Flat;
    this._tabLayout = TabLayout.SingleLineScrollable;
    this._tabAlignment = TabAlignment.Top;
    this._flat = true;
    this._scroll = 0;
    this._maxScroll = 0;
    this._mouseDown = false;
    this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
    this.SetStyle(ControlStyles.Selectable, true);
    this._renderer = (ITabControlRenderer) new TabControlRenderer();
    this._tabPages = new PageControl.TabPageCollection(this);
    this._leftButton = new TitleButton();
    this._rightButton = new TitleButton();
    this._srollTimer = new Timer();
    this._srollTimer.Interval = 20;
    this._srollTimer.Tick += new EventHandler(this.ScrollTimerTick);
  }

  internal void ApplyLayout()
  {
    if (!this.IsHandleCreated)
      return;
    ITabControlRenderer renderer = this.Renderer;
    using (Graphics graphics = this.CreateGraphics())
    {
      renderer.StartRenderSession();
      try
      {
        foreach (TabPage control in (ArrangedElementCollection) this.Controls)
        {
          IntPtr handle = control.Handle;
          control._textTrimmed = false;
          Size size = renderer.MeasureTabControlTab(graphics, control.TabImage, control.Text, this.Font, DrawItemState.Default);
          control._tabWidth = (double) size.Width;
          if (control.MaximumTabWidth != 0 && (double) control.MaximumTabWidth < control._tabWidth)
          {
            control._tabWidth = (double) control.MaximumTabWidth;
            control._textTrimmed = true;
          }
        }
      }
      finally
      {
        renderer.FinishRenderSession();
      }
    }
    this._tabStripBounds = this.DisplayRectangle;
    this._pageArea = this.DisplayRectangle;
    if (this._tabAlignment == TabAlignment.Top)
    {
      this._tabStripBounds.Height = renderer.TabControlTabStripHeight;
      this._pageArea.Offset(0, this._tabStripBounds.Height);
      this._pageArea.Height -= this._tabStripBounds.Height;
    }
    else
    {
      this._tabStripBounds.Height = renderer.TabControlTabStripHeight;
      this._pageArea.Height -= this._tabStripBounds.Height;
      this._tabStripBounds.Offset(0, this._pageArea.Height);
    }
    this._pageBounds = this._pageArea;
    this._pageBounds.Inflate(-renderer.TabControlPadding.Width, -renderer.TabControlPadding.Height);
    switch (this.TabLayout)
    {
      case TabLayout.SingleLineScrollable:
        this.LayoutSingleLineScrollable();
        break;
      case TabLayout.SingleLineFixed:
        this.LayoutSingleLineFixed();
        break;
    }
    this.Invalidate(renderer.ShouldDrawTabControlBackground);
  }

  internal TitleButton GetActiveButton() => this._activeButton;

  private void Scroll(int num)
  {
    this._scroll += num;
    if (this._scroll > this._maxScroll)
    {
      this._scroll = this._maxScroll;
      this.DisableScroll();
    }
    if (this._scroll < 0)
    {
      this._scroll = 0;
      this.DisableScroll();
    }
    switch (this.TabLayout)
    {
      case TabLayout.SingleLineScrollable:
        this.LayoutSingleLineScrollable();
        break;
      case TabLayout.SingleLineFixed:
        this.LayoutSingleLineFixed();
        break;
    }
    this.Invalidate(this._tabStripBounds);
  }

  internal void SetActiveButton(TitleButton newButton)
  {
    if (newButton == this._activeButton)
      return;
    if (this._activeButton != null)
      this.Invalidate(this._tabStripBounds);
    this._activeButton = newButton;
    if (this._activeButton == null)
      return;
    this.Invalidate(this._tabStripBounds);
  }

  private void FitTabs(IList tabs, bool canExpand)
  {
    int width = this._tabStripBounds.Width;
    double num1 = 0.0;
    foreach (TabPage tab in (IEnumerable) tabs)
      num1 += tab._tabWidth;
    if (tabs.Count >= 1)
      num1 -= (double) ((tabs.Count - 1) * this._renderer.TabControlTabExtra);
    if (num1 > (double) width)
    {
      double num2 = num1 - (double) width;
      for (int index = 0; index < tabs.Count; ++index)
      {
        TabPage tab = (TabPage) tabs[index];
        double num3 = index == 0 ? tab._tabWidth : tab._tabWidth - (double) this._renderer.TabControlTabExtra;
        double num4 = num3 / num1;
        double num5 = num3 - num2 * num4;
        tab._textTrimmed = true;
        tab._tabWidth = index == 0 ? num5 : num5 + (double) this._renderer.TabControlTabExtra;
      }
    }
    else
    {
      if (!canExpand || num1 >= (double) width)
        return;
      double num6 = (double) width - num1;
      for (int index = 0; index < tabs.Count; ++index)
      {
        TabPage tab = (TabPage) tabs[index];
        double num7 = index == 0 ? tab._tabWidth : tab._tabWidth - (double) this._renderer.TabControlTabExtra;
        double num8 = num7 / num1;
        double num9 = num7 + num6 * num8;
        tab._tabWidth = index == 0 ? num9 : num9 + (double) this._renderer.TabControlTabExtra;
      }
    }
  }

  private void PaintPage(Graphics g, TabPage page, bool drawSeparator)
  {
    if (!page._tabVisible && !this.DesignMode)
      return;
    DrawItemState state = DrawItemState.Default;
    if (page == this.SelectedPage)
    {
      state |= DrawItemState.Selected;
      if (this.Focused && this.ShowFocusCues)
        state |= DrawItemState.Checked;
    }
    Font font = this.Font;
    if (!page._tabVisible && this.DesignMode)
      font = new Font(font, FontStyle.Italic | FontStyle.Strikeout);
    this.Renderer.DrawTabControlTab(g, page._tabBounds, page.TabImage, page.Text, font, page.BackColor, page.ForeColor, state, drawSeparator, this._tabAlignment, this._flat);
  }

  private TitleButton GetButtonAt(int x, int y)
  {
    if (this._leftButton._visible && this._leftButton._enabled && this._leftButton._bounds.Contains(x, y))
      return this._leftButton;
    return this._rightButton._visible && this._rightButton._enabled && this._rightButton._bounds.Contains(x, y) ? this._rightButton : (TitleButton) null;
  }

  private void ScrollTimerTick(object sender, EventArgs e)
  {
    if (this.GetActiveButton() == this._leftButton)
      this.Scroll(-15);
    else if (this.GetActiveButton() == this._rightButton)
      this.Scroll(15);
    else
      this.DisableScroll();
  }

  private void SelectPage(TabPage page, bool activate)
  {
    this.SelectedPage = page;
    if (this.SelectedPage == null)
      return;
    if (activate)
      this.SelectedPage.SelectNextControl((Control) null, true, true, true, true);
    if (this.TabLayout != TabLayout.SingleLineScrollable)
      return;
    Rectangle tabsBounds = this.GetTabsBounds();
    Rectangle tabBounds = page._tabBounds;
    if (tabsBounds.Contains(tabBounds))
      return;
    int num = 0;
    if (tabBounds.Right > tabsBounds.Right)
      num = tabBounds.Right - tabsBounds.Right + 20;
    else if (tabBounds.Left < tabsBounds.Left)
      num = tabBounds.Left - tabsBounds.Left - 20;
    if (num == 0)
      return;
    this.Scroll(num);
  }

  private Rectangle GetTabsBounds()
  {
    Rectangle tabStripBounds = this._tabStripBounds;
    if (this._leftButton._visible)
    {
      tabStripBounds.Offset(12, 0);
      tabStripBounds.Width -= 24;
    }
    return tabStripBounds;
  }

  private void SelectNextPage(int offset, bool activate, bool cicle)
  {
    if (this.SelectedPage == null)
      return;
    int index = this.Controls.IndexOf((Control) this.SelectedPage);
    int num = 0;
    int count = this.Controls.Count;
    while (num++ < count * 2)
    {
      index += offset;
      if (index > count - 1)
        index = cicle ? 0 : count - 1;
      if (index < 0)
        index = cicle ? count - 1 : 0;
      if (((TabPage) this.Controls[index]).TabVisible)
      {
        this.SelectPage((TabPage) this.Controls[index], activate);
        break;
      }
    }
  }

  private void PaintButton(
    Graphics g,
    ITabControlRenderer renderer,
    TitleButton button,
    ButtonType buttonType,
    bool enabled)
  {
    if (!button._visible)
      return;
    DrawItemState state = DrawItemState.Default;
    if (this.GetActiveButton() == button)
    {
      state |= DrawItemState.HotLight;
      if (this._mouseDown)
        state |= DrawItemState.Selected;
    }
    if (!enabled)
      state |= DrawItemState.Disabled;
    renderer.DrawTabControlButton(g, button._bounds, buttonType, state);
  }

  private void CheckEnableScroll(TitleButton button)
  {
    if (button != this._leftButton && button != this._rightButton)
      return;
    this.EnableScroll();
  }

  private void CheckDisableScroll(TitleButton button)
  {
    if (button != this._leftButton && button != this._rightButton)
      return;
    this.DisableScroll();
  }

  private void LayoutSingleLineScrollable()
  {
    int y = this._tabStripBounds.Top + this._tabStripBounds.Height / 2 - 7;
    int num1 = this._tabStripBounds.Right - 2;
    int left = this._tabStripBounds.Left;
    this._rightButton._visible = false;
    this._rightButton._bounds = new Rectangle(num1 - 12, y, 12, 15);
    int num2 = num1 - 12;
    this._leftButton._visible = false;
    this._leftButton._bounds = new Rectangle(left, y, 12, 15);
    foreach (TabPage control in (ArrangedElementCollection) this.Controls)
    {
      if (control._tabVisible || this.DesignMode)
      {
        int width = (int) Math.Round(control._tabWidth, 0);
        control._tabBounds = this._tabAlignment != TabAlignment.Top ? new Rectangle(left, this._tabStripBounds.Top - 1, width, this._renderer.TabControlTabHeight) : new Rectangle(left, this._tabStripBounds.Bottom - this._renderer.TabControlTabHeight, width, this._renderer.TabControlTabHeight);
        left += width - this._renderer.TabControlTabExtra;
      }
    }
    if (this.Controls.Count != 0)
      left += this._renderer.TabControlTabExtra;
    this._maxScroll = left - this._tabStripBounds.Width;
    int num3 = 0;
    if (this._maxScroll < 0)
    {
      this._maxScroll = 0;
    }
    else
    {
      this._leftButton._visible = true;
      this._rightButton._visible = true;
      this._maxScroll += 24;
      num3 = 12;
    }
    if (this._scroll > this._maxScroll)
      this._scroll = this._maxScroll;
    this._leftButton._enabled = this._scroll > 0;
    this._rightButton._enabled = this._scroll < this._maxScroll;
    foreach (TabPage control in (ArrangedElementCollection) this.Controls)
    {
      if (control._tabVisible || this.DesignMode)
      {
        Rectangle tabBounds = control._tabBounds;
        tabBounds.Offset(-(this._scroll - num3), 0);
        control._tabBounds = tabBounds;
      }
    }
  }

  private void LayoutSingleLineFixed()
  {
    this.FitTabs((IList) this.Controls, false);
    int left = this._tabStripBounds.Left;
    foreach (TabPage control in (ArrangedElementCollection) this.Controls)
    {
      if (control._tabVisible || this.DesignMode)
      {
        int width = (int) Math.Round(control._tabWidth, 0);
        control._tabBounds = this._tabAlignment != TabAlignment.Top ? new Rectangle(left, this._tabStripBounds.Top - 1, width, this._renderer.TabControlTabHeight) : new Rectangle(left, this._tabStripBounds.Bottom - this._renderer.TabControlTabHeight, width, this._renderer.TabControlTabHeight);
        left += width - this._renderer.TabControlTabExtra;
      }
    }
  }

  private void DisableScroll()
  {
    this._srollTimer.Enabled = false;
    this.SetActiveButton((TitleButton) null);
    this._mouseDown = false;
    this.Invalidate(this._tabStripBounds);
  }

  private void EnableScroll()
  {
    this._srollTimer.Enabled = true;
    this.ScrollTimerTick((object) this._srollTimer, EventArgs.Empty);
  }

  protected override Control.ControlCollection CreateControlsInstance()
  {
    return (Control.ControlCollection) new PageControl.TabPageControls(this);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._renderer is IDisposable)
        ((IDisposable) this._renderer).Dispose();
      this._srollTimer.Tick -= new EventHandler(this.ScrollTimerTick);
      this._srollTimer.Dispose();
      this.ImageList = (ImageList) null;
    }
    base.Dispose(disposing);
  }

  public TabPage GetTabPageAt(Point position)
  {
    foreach (TabPage control in (ArrangedElementCollection) this.Controls)
    {
      if ((control._tabVisible || this.DesignMode) && control._tabBounds.Contains(position))
        return control;
    }
    return (TabPage) null;
  }

  protected override bool IsInputKey(Keys keyData)
  {
    switch (keyData)
    {
      case Keys.Left:
      case Keys.Up:
      case Keys.Right:
      case Keys.Down:
        return true;
      default:
        return base.IsInputKey(keyData);
    }
  }

  protected override void OnControlAdded(ControlEventArgs e)
  {
    base.OnControlAdded(e);
    this.ApplyLayout();
    this.PerformLayout();
  }

  protected override void OnControlRemoved(ControlEventArgs e)
  {
    base.OnControlRemoved(e);
    if (this.SelectedPage == e.Control)
    {
      if (this.TabPages.Count != 0)
      {
        this.SelectedPage = this.TabPages[0];
      }
      else
      {
        this._selectedPage = (TabPage) null;
        this.OnSelectedPageChanged(EventArgs.Empty);
      }
    }
    this.ApplyLayout();
    this.PerformLayout();
  }

  protected override void OnFontChanged(EventArgs e)
  {
    this.ApplyLayout();
    this.PerformLayout();
    base.OnFontChanged(e);
  }

  protected override void OnGotFocus(EventArgs e)
  {
    base.OnGotFocus(e);
    this.Invalidate(this.TabStripBounds);
  }

  protected override void OnHandleCreated(EventArgs e)
  {
    base.OnHandleCreated(e);
    this.ApplyLayout();
    this.PerformLayout();
  }

  protected override void OnKeyDown(KeyEventArgs e)
  {
    if (e.KeyCode == Keys.Down)
      return;
    base.OnKeyDown(e);
  }

  protected override void OnLayout(LayoutEventArgs levent)
  {
    if (this.SelectedPage == null)
      return;
    this.SelectedPage.Bounds = this._pageBounds;
  }

  protected override void OnLostFocus(EventArgs e)
  {
    base.OnLostFocus(e);
    this.Invalidate(this.TabStripBounds);
  }

  protected override void OnMouseDown(MouseEventArgs e)
  {
    if (e.Button == MouseButtons.Left)
    {
      if (this.GetActiveButton() != null)
      {
        this._mouseDown = true;
        this.Invalidate(this._tabStripBounds);
        this.CheckEnableScroll(this.GetActiveButton());
        return;
      }
      TabPage tabPageAt = this.GetTabPageAt(new Point(e.X, e.Y));
      if (tabPageAt != null)
      {
        this.Focus();
        if (this.SelectedPage == tabPageAt || !this.TabPages.Contains(tabPageAt))
          return;
        this.SelectPage(tabPageAt, true);
        return;
      }
    }
    base.OnMouseDown(e);
  }

  protected override void OnMouseLeave(EventArgs e)
  {
    this.SetActiveButton((TitleButton) null);
    this._mouseDown = false;
    base.OnMouseLeave(e);
  }

  protected override void OnMouseMove(MouseEventArgs e)
  {
    base.OnMouseMove(e);
    if (this.TabLayout != TabLayout.SingleLineScrollable)
      return;
    this.SetActiveButton(this.GetButtonAt(e.X, e.Y));
  }

  protected override void OnMouseUp(MouseEventArgs e)
  {
    if ((e.Button & MouseButtons.Left) == MouseButtons.Left && this.GetActiveButton() != null)
    {
      this.CheckDisableScroll(this.GetActiveButton());
      this._mouseDown = false;
      this.Invalidate(this._tabStripBounds);
    }
    base.OnMouseUp(e);
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    this.Renderer.StartRenderSession();
    try
    {
      DockControl.PaintBorder((Control) this, e.Graphics, this._borderStyle);
      this._renderer.DrawTabControlTabStripBackground(e.Graphics, this._tabStripBounds, this.BackColor, this._tabAlignment, this._flat);
      Region region = (Region) null;
      if (this.TabLayout == TabLayout.SingleLineScrollable)
      {
        region = e.Graphics.Clip;
        Rectangle tabsBounds = this.GetTabsBounds();
        e.Graphics.SetClip(tabsBounds);
      }
      int count = this.Controls.Count;
      int num = count - 1;
      for (int index = 0; index < count; ++index)
      {
        TabPage control = this.Controls[index] as TabPage;
        bool drawSeparator = true;
        if (this._selectedPage != null && index == this.Controls.IndexOf((Control) this._selectedPage) - 1 || index == num)
          drawSeparator = false;
        this.PaintPage(e.Graphics, control, drawSeparator);
      }
      if (this.SelectedPage != null)
        this.PaintPage(e.Graphics, this.SelectedPage, false);
      if (this.TabLayout == TabLayout.SingleLineScrollable)
        e.Graphics.Clip = region;
      if (this.SelectedPage != null)
        this._renderer.DrawTabControlBackground(e.Graphics, this._pageArea, this.SelectedPage.BackColor, false);
      if (this.TabLayout != TabLayout.SingleLineScrollable)
        return;
      this.PaintButton(e.Graphics, this._renderer, this._rightButton, ButtonType.ScrollRight, this._rightButton._enabled);
      this.PaintButton(e.Graphics, this._renderer, this._leftButton, ButtonType.ScrollLeft, this._leftButton._enabled);
    }
    finally
    {
      this.Renderer.FinishRenderSession();
    }
  }

  protected override void OnResize(EventArgs e)
  {
    this.ApplyLayout();
    base.OnResize(e);
  }

  protected virtual void OnSelectedPageChanged(EventArgs e)
  {
    if (this.SelectedPageChanged == null)
      return;
    this.SelectedPageChanged((object) this, e);
  }

  protected virtual void OnSelectedPageChanging(PageControlCancelEventArgs e)
  {
    if (this.SelectedPageChanging == null)
      return;
    this.SelectedPageChanging((object) this, e);
  }

  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    if (base.ProcessCmdKey(ref msg, keyData))
      return true;
    if (keyData != (Keys.Tab | Keys.Control))
    {
      if (keyData != (Keys.Tab | Keys.Shift | Keys.Control))
        return false;
      this.SelectNextPage(-1, true, true);
      return true;
    }
    this.SelectNextPage(1, true, true);
    return true;
  }

  protected override bool ProcessMnemonic(char charCode)
  {
    foreach (TabPage control in (ArrangedElementCollection) this.Controls)
    {
      if (control.TabVisible && Control.IsMnemonic(charCode, control.Text))
      {
        this.SelectPage(control, true);
        return true;
      }
    }
    return base.ProcessMnemonic(charCode);
  }

  private void ImageList_RecreateHandle(object sender, EventArgs e) => this.ApplyLayout();

  private void ImageList_Disposed(object sender, EventArgs e) => this.ImageList = (ImageList) null;

  private bool ShouldSerializeRenderer() => !(this._renderer is TabControlRenderer);

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Obsolete("Use the TabLayout property instead.")]
  [Browsable(false)]
  public bool AllowScrolling
  {
    get => true;
    set
    {
    }
  }

  [DefaultValue(true)]
  public bool Flat
  {
    get => this._flat;
    set
    {
      if (this._flat == value)
        return;
      this._flat = value;
      this.ApplyLayout();
      this.PerformLayout();
    }
  }

  [Description("The type of border to be drawn around the control.")]
  [Category("Appearance")]
  [DefaultValue(typeof (Intermech.Docking.Rendering.BorderStyle), "Flat")]
  public Intermech.Docking.Rendering.BorderStyle BorderStyle
  {
    get => this._borderStyle;
    set
    {
      this._borderStyle = value;
      this.ApplyLayout();
      this.PerformLayout();
    }
  }

  protected override Size DefaultSize => new Size(300, 200);

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

  [Category("Appearance")]
  [Description("The renderer used to calculate object metrics and draw contents.")]
  [TypeConverter(typeof (TabControlRendererConverter))]
  public ITabControlRenderer Renderer
  {
    get => this._renderer;
    set
    {
      if (value == null)
        throw new ArgumentNullException();
      if (this._renderer is IDisposable)
        ((IDisposable) this._renderer).Dispose();
      this._renderer = value;
      this.ApplyLayout();
      this.PerformLayout();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public TabPage SelectedPage
  {
    get => this._selectedPage;
    set
    {
      if (value == this._selectedPage)
        return;
      if (value == null)
        throw new ArgumentNullException();
      PageControlCancelEventArgs e = this.Controls.Contains((Control) value) ? new PageControlCancelEventArgs(value, value.TabIndex) : throw new ArgumentException("Specified TabPage does not belong to this PageControl.");
      this.OnSelectedPageChanging(e);
      if (!this.Controls.Contains((Control) value) || e.Cancel)
        return;
      this.SetSelectedPage(value);
    }
  }

  [DefaultValue(null)]
  [Category("Appearance")]
  public virtual ImageList ImageList
  {
    get => this._imageList;
    set
    {
      if (this._imageList != null)
      {
        this._imageList.RecreateHandle -= new EventHandler(this.ImageList_RecreateHandle);
        this._imageList.Disposed -= new EventHandler(this.ImageList_Disposed);
      }
      this._imageList = value;
      if (this._imageList != null)
      {
        this._imageList.RecreateHandle += new EventHandler(this.ImageList_RecreateHandle);
        this._imageList.Disposed += new EventHandler(this.ImageList_Disposed);
      }
      this.ApplyLayout();
    }
  }

  [Description("How the tabs of child controls are laid out.")]
  [Category("Behavior")]
  [DefaultValue(typeof (TabLayout), "SingleLineScrollable")]
  public TabLayout TabLayout
  {
    get => this._tabLayout;
    set
    {
      this._tabLayout = value;
      this.ApplyLayout();
      this.PerformLayout();
    }
  }

  [Description("Determines wheter the tabs appear on the top or bottom side of the Control.")]
  [Category("Behavior")]
  [DefaultValue(TabAlignment.Top)]
  public TabAlignment TabAlignment
  {
    get => this._tabAlignment;
    set
    {
      this._tabAlignment = value;
      this.ApplyLayout();
      this.PerformLayout();
    }
  }

  [Category("Behavior")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Description("A collection of TabPage controls belonging to this control.")]
  public PageControl.TabPageCollection TabPages => this._tabPages;

  [Browsable(false)]
  public Rectangle TabStripBounds => this._tabStripBounds;

  [Browsable(false)]
  public override string Text
  {
    get => base.Text;
    set => base.Text = value;
  }

  internal void SetSelectedPage(TabPage page)
  {
    this._selectedPage = page;
    this.ApplyLayout();
    this.SuspendLayout();
    foreach (TabPage tabPage in this.TabPages)
      tabPage.Visible = tabPage == this._selectedPage;
    this.ResumeLayout();
    this.OnSelectedPageChanged(EventArgs.Empty);
  }

  public class TabPageControls : Control.ControlCollection
  {
    private PageControl _owner;

    public TabPageControls(PageControl owner)
      : base((Control) owner)
    {
      this._owner = owner;
    }

    public override void Add(Control value)
    {
      if (!(value is TabPage))
        throw new ArgumentException("Only TabPage controls can be added to a PageControl control.");
      value.Visible = false;
      base.Add(value);
      if (this.Count != 1)
        return;
      this._owner.SelectedPage = (TabPage) value;
    }
  }

  public class TabPageCollection : IList, ICollection, IEnumerable
  {
    private PageControl _parent;

    internal TabPageCollection(PageControl parent) => this._parent = parent;

    public void Add(TabPage tabPage) => this._parent.Controls.Add((Control) tabPage);

    public void AddRange(TabPage[] tabPages)
    {
      this._parent.Controls.AddRange((Control[]) tabPages);
    }

    public void Clear() => this._parent.Controls.Clear();

    public bool Contains(TabPage tabPage) => this._parent.Controls.Contains((Control) tabPage);

    public void CopyTo(TabPage[] array, int index)
    {
      this._parent.Controls.CopyTo((Array) array, index);
    }

    public IEnumerator GetEnumerator()
    {
      TabPage[] array = new TabPage[this.Count];
      this.CopyTo(array, 0);
      return array.GetEnumerator();
    }

    public int IndexOf(TabPage tabPage) => this._parent.Controls.IndexOf((Control) tabPage);

    public void Remove(TabPage tabPage) => this._parent.Controls.Remove((Control) tabPage);

    public void RemoveAt(int index) => this._parent.Controls.RemoveAt(index);

    void ICollection.CopyTo(Array array, int index)
    {
      if (!(array is TabPage[]))
        return;
      this.CopyTo((TabPage[]) array, index);
    }

    bool ICollection.IsSynchronized => false;

    object ICollection.SyncRoot => (object) this;

    int IList.Add(object value)
    {
      if (!(value is TabPage))
        throw new NotSupportedException();
      this._parent.Controls.Add((Control) value);
      return this.IndexOf((TabPage) value);
    }

    bool IList.Contains(object value) => value is TabPage && this.Contains((TabPage) value);

    bool IList.IsFixedSize => false;

    bool IList.IsReadOnly => false;

    object IList.this[int index]
    {
      get => (object) this[index];
      set
      {
      }
    }

    int IList.IndexOf(object value) => value is TabPage ? this.IndexOf((TabPage) value) : -1;

    void IList.Insert(int index, object value) => throw new NotSupportedException();

    void IList.Remove(object value)
    {
      if (!(value is TabPage))
        return;
      this.Remove((TabPage) value);
    }

    public int Count => this._parent.Controls.Count;

    public TabPage this[int index] => (TabPage) this._parent.Controls[index];
  }
}
