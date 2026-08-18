
// Type: Intermech.Docking.TabControl
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


namespace Intermech.Docking;

[ToolboxItem(true)]
[DefaultEvent("SelectedPageChanged")]
[Designer(typeof (TabControlDesigner))]
[DefaultProperty("TabLayout")]
[ToolboxBitmap(typeof (TabControl))]
public class TabControl : Control
{
  private Rectangle _tabStripBounds;
  private const int BUTTON_WIDTH = 12;
  private Timer _srollTimer;
  private TitleButton _leftButton;
  private Intermech.Docking.Rendering.BorderStyle _borderStyle;
  private ITabControlRenderer _renderer;
  private TabControl.TabsCollection _tabs;
  private TabLayout _tabLayout;
  private TabAlignment _tabAlignment;
  private Tab _selectedTab;
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

  public event EventHandler SelectedTabChanged;

  public event CancelEventHandler SelectedTabChanging;

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
        foreach (Tab tab in this._tabs)
        {
          tab._textTrimmed = false;
          Size size = renderer.MeasureTabControlTab(graphics, tab.TabImage, tab.Text, this.Font, DrawItemState.Default);
          tab._tabWidth = (double) size.Width;
          if (tab.MaximumTabWidth != 0 && (double) tab.MaximumTabWidth < tab._tabWidth)
          {
            tab._tabWidth = (double) tab.MaximumTabWidth;
            tab._textTrimmed = true;
          }
        }
      }
      finally
      {
        renderer.FinishRenderSession();
      }
    }
    Rectangle displayRectangle = base.DisplayRectangle;
    displayRectangle.Inflate(-1, -1);
    this._tabStripBounds = displayRectangle;
    this._pageArea = displayRectangle;
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

  public void Scroll(int num)
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
    this.ApplyLayout();
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
    foreach (Tab tab in (IEnumerable) tabs)
      num1 += tab._tabWidth;
    if (tabs.Count >= 1)
      num1 -= (double) ((tabs.Count - 1) * this._renderer.TabControlTabExtra);
    if (num1 > (double) width)
    {
      double num2 = num1 - (double) width;
      for (int index = 0; index < tabs.Count; ++index)
      {
        Tab tab = (Tab) tabs[index];
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
        Tab tab = (Tab) tabs[index];
        double num7 = index == 0 ? tab._tabWidth : tab._tabWidth - (double) this._renderer.TabControlTabExtra;
        double num8 = num7 / num1;
        double num9 = num7 + num6 * num8;
        tab._tabWidth = index == 0 ? num9 : num9 + (double) this._renderer.TabControlTabExtra;
      }
    }
  }

  private void PaintPage(Graphics g, Tab tab, bool drawSeparator)
  {
    if (!tab._visible && !this.DesignMode)
      return;
    DrawItemState state = DrawItemState.Default;
    if (tab == this.SelectedTab)
    {
      state |= DrawItemState.Selected;
      if (this.Focused && this.ShowFocusCues)
        state |= DrawItemState.Checked;
    }
    Font font = this.Font;
    if (!tab._visible && this.DesignMode)
      font = new Font(font, FontStyle.Italic);
    this.Renderer.DrawTabControlTab(g, tab._tabBounds, tab.TabImage, tab.Text, font, this.BackColor, this.ForeColor, state, drawSeparator, this._tabAlignment, this._flat);
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

  public void SelectTab(Tab tab)
  {
    this.SelectedTab = tab;
    if (this.TabLayout != TabLayout.SingleLineScrollable)
      return;
    Rectangle tabStripBounds = this._tabStripBounds;
    tabStripBounds.Width -= this._tabStripBounds.Right - this._leftButton._bounds.Left;
    Rectangle rect = tab != null ? tab._tabBounds : Rectangle.Empty;
    if (tab == null || tabStripBounds.Contains(rect))
      return;
    int num = 0;
    if (rect.Right > tabStripBounds.Right)
      num = rect.Right - tabStripBounds.Right + 20;
    else if (rect.Left < tabStripBounds.Left)
      num = rect.Left - tabStripBounds.Left - 20;
    if (num == 0)
      return;
    this.Scroll(num);
  }

  private void SelectNextTab(int offset, bool activate, bool cicle)
  {
    if (this.SelectedTab == null)
      return;
    int index = this._tabs.IndexOf(this.SelectedTab);
    int num = 0;
    int count = this._tabs.Count;
    while (num++ < count * 2)
    {
      index += offset;
      if (index > count - 1)
        index = cicle ? 0 : count - 1;
      if (index < 0)
        index = cicle ? count - 1 : 0;
      if (this._tabs[index]._visible)
      {
        this.SelectTab(this._tabs[index]);
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
    this._rightButton._visible = true;
    this._rightButton._bounds = new Rectangle(num1 - 12, y, 12, 15);
    int num2 = num1 - 12;
    this._leftButton._visible = true;
    this._leftButton._bounds = new Rectangle(num2 - 12, y, 12, 15);
    int num3 = num2 - 12;
    int left = this._tabStripBounds.Left;
    foreach (Tab tab in this._tabs)
    {
      if (tab._visible || this.DesignMode)
      {
        int width = (int) Math.Round(tab._tabWidth, 0);
        tab._tabBounds = this._tabAlignment != TabAlignment.Top ? new Rectangle(left, this._tabStripBounds.Top - 1, width, this._renderer.TabControlTabHeight) : new Rectangle(left, this._tabStripBounds.Bottom - this._renderer.TabControlTabHeight, width, this._renderer.TabControlTabHeight);
        left += width - this._renderer.TabControlTabExtra;
      }
    }
    if (this._tabs.Count != 0)
      left += this._renderer.TabControlTabExtra;
    int num4 = this._leftButton._bounds.Left - this._tabStripBounds.Left;
    this._maxScroll = left - num4;
    if (this._maxScroll < 0)
      this._maxScroll = 0;
    if (this._scroll > this._maxScroll)
      this._scroll = this._maxScroll;
    this._leftButton._enabled = this._scroll > 0;
    this._rightButton._enabled = this._scroll < this._maxScroll;
    foreach (Tab tab in this._tabs)
    {
      if (tab._visible || this.DesignMode)
      {
        Rectangle tabBounds = tab._tabBounds;
        tabBounds.Offset(-this._scroll, 0);
        tab._tabBounds = tabBounds;
      }
    }
  }

  private void LayoutSingleLineFixed()
  {
    this.FitTabs((IList) this._tabs, false);
    int left = this._tabStripBounds.Left;
    foreach (Tab tab in this._tabs)
    {
      if (tab._visible || this.DesignMode)
      {
        int width = (int) Math.Round(tab._tabWidth, 0);
        tab._tabBounds = this._tabAlignment != TabAlignment.Top ? new Rectangle(left, this._tabStripBounds.Top - 1, width, this._renderer.TabControlTabHeight) : new Rectangle(left, this._tabStripBounds.Bottom - this._renderer.TabControlTabHeight, width, this._renderer.TabControlTabHeight);
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

  public TabControl()
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
    this._tabs = new TabControl.TabsCollection(this);
    this._leftButton = new TitleButton();
    this._rightButton = new TitleButton();
    this._srollTimer = new Timer();
    this._srollTimer.Interval = 20;
    this._srollTimer.Tick += new EventHandler(this.ScrollTimerTick);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._renderer is IDisposable)
        ((IDisposable) this._renderer).Dispose();
      foreach (Component tab in this._tabs)
        tab.Dispose();
      this._srollTimer.Dispose();
    }
    base.Dispose(disposing);
  }

  public Tab GetTabPageAt(Point position)
  {
    foreach (Tab tab in this._tabs)
    {
      if (tab._visible && tab._tabBounds.Contains(position))
        return tab;
    }
    return (Tab) null;
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
  }

  protected override void OnKeyDown(KeyEventArgs e)
  {
    if (e.KeyCode == Keys.Down)
      return;
    base.OnKeyDown(e);
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
      Tab tabPageAt = this.GetTabPageAt(new Point(e.X, e.Y));
      if (tabPageAt != null)
      {
        if (this.SelectedTab == tabPageAt)
        {
          this.Focus();
          return;
        }
        this.SelectTab(tabPageAt);
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
        Rectangle tabStripBounds = this._tabStripBounds;
        tabStripBounds.Width -= this._tabStripBounds.Right - this._leftButton._bounds.Left;
        e.Graphics.SetClip(tabStripBounds);
      }
      int count = this._tabs.Count;
      int num1 = count - 1;
      int num2 = this._tabs.IndexOf(this._selectedTab);
      for (int index = 0; index < count; ++index)
      {
        Tab tab = this._tabs[index];
        bool drawSeparator = true;
        if (this._selectedTab != null && (index == num2 - 1 || index == num2) || index == num1)
          drawSeparator = false;
        this.PaintPage(e.Graphics, tab, drawSeparator);
      }
      if (this.SelectedTab != null && num2 != -1)
        this.PaintPage(e.Graphics, this.SelectedTab, false);
      if (this.TabLayout == TabLayout.SingleLineScrollable)
        e.Graphics.Clip = region;
      if (this.SelectedTab != null && num2 != -1)
        this._renderer.DrawTabControlBackground(e.Graphics, this._pageBounds, this.BackColor, false);
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

  protected virtual void OnSeledTabChanged(EventArgs e)
  {
    if (this.SelectedTabChanged == null)
      return;
    this.SelectedTabChanged((object) this, e);
  }

  protected virtual void OnSelectedTabChanging(CancelEventArgs e)
  {
    if (this.SelectedTabChanging == null)
      return;
    this.SelectedTabChanging((object) this, e);
  }

  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    if (base.ProcessCmdKey(ref msg, keyData))
      return true;
    if (keyData != (Keys.Tab | Keys.Control))
    {
      if (keyData != (Keys.Tab | Keys.Shift | Keys.Control))
        return false;
      this.SelectNextTab(-1, true, true);
      return true;
    }
    this.SelectNextTab(1, true, true);
    return true;
  }

  protected override bool ProcessMnemonic(char charCode)
  {
    foreach (Tab tab in this._tabs)
    {
      if (tab._visible && Control.IsMnemonic(charCode, tab.Text))
      {
        this.SelectTab(tab);
        return true;
      }
    }
    return base.ProcessMnemonic(charCode);
  }

  private void ImageList_RecreateHandle(object sender, EventArgs e) => this.ApplyLayout();

  private void ImageList_Disposed(object sender, EventArgs e) => this.ImageList = (ImageList) null;

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

  public override Rectangle DisplayRectangle => this._pageBounds;

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

  private bool ShouldSerializeRenderer()
  {
    return this._renderer != null && this._renderer.ToString() != "Tab";
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Tab SelectedTab
  {
    get => this._selectedTab;
    set
    {
      if (value != null && !this._tabs.Contains(value))
        throw new ArgumentException("Specified Tab does not belong to this TabControl.");
      CancelEventArgs e = new CancelEventArgs();
      this.OnSelectedTabChanging(e);
      if (e.Cancel)
        return;
      this._selectedTab = value;
      this.ApplyLayout();
      this.OnSeledTabChanged(EventArgs.Empty);
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
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [Description("A collection of Tab controls belonging to this control.")]
  public TabControl.TabsCollection Tabs => this._tabs;

  [Browsable(false)]
  public Rectangle TabStripBounds => this._tabStripBounds;

  [Browsable(false)]
  public override string Text
  {
    get => base.Text;
    set => base.Text = value;
  }

  public class TabsCollection : IList, ICollection, IEnumerable
  {
    private ArrayList _tabs;
    private bool _updating;
    private TabControl _owner;

    internal TabsCollection(TabControl owner)
    {
      this._tabs = new ArrayList();
      this._owner = owner;
      this._updating = false;
    }

    public void Add(Tab tab)
    {
      if (this._tabs.IndexOf((object) tab) != -1)
        return;
      this._tabs.Add((object) tab);
      tab._parent = this._owner;
      if (this._updating)
        return;
      this._owner.ApplyLayout();
    }

    public void Insert(int index, Tab tab)
    {
      int index1 = this._tabs.IndexOf((object) tab);
      if (index1 == -1)
      {
        this._tabs.Insert(index, (object) tab);
        tab._parent = this._owner;
        if (this._updating)
          return;
        this._owner.ApplyLayout();
      }
      else
      {
        if (index == index1)
          return;
        object tab1 = this._tabs[index];
        this._tabs[index] = this._tabs[index1];
        this._tabs[index1] = tab1;
        if (this._updating)
          return;
        this._owner.ApplyLayout();
      }
    }

    public void AddRange(Tab[] tabs)
    {
      try
      {
        this._updating = true;
        foreach (Tab tab in tabs)
          this.Add(tab);
        this._owner.ApplyLayout();
      }
      finally
      {
        this._updating = false;
      }
    }

    public void Clear()
    {
      foreach (Tab tab in this._tabs)
        tab._parent = (TabControl) null;
      this._tabs.Clear();
    }

    public bool Contains(Tab tab) => this._tabs.Contains((object) tab);

    public void CopyTo(Tab[] array, int index) => this._tabs.CopyTo((Array) array, index);

    public IEnumerator GetEnumerator()
    {
      Tab[] array = new Tab[this._tabs.Count];
      this.CopyTo(array, 0);
      return array.GetEnumerator();
    }

    public int IndexOf(Tab tab) => this._tabs.IndexOf((object) tab);

    public void Remove(Tab tab)
    {
      tab._parent = (TabControl) null;
      this._tabs.Remove((object) tab);
    }

    public void RemoveAt(int index) => this._tabs.RemoveAt(index);

    internal void MoveTo(Tab tab, int newPos)
    {
      if (this._tabs.Count <= 1)
        return;
      int index = this._tabs.IndexOf((object) tab);
      if (index == -1)
        return;
      this._tabs.RemoveAt(index);
      if (newPos > this._tabs.Count)
        newPos = this._tabs.Count;
      if (newPos < 0)
        newPos = 0;
      this._tabs.Insert(newPos, (object) tab);
      this._owner.ApplyLayout();
      this._owner.Invalidate();
    }

    void ICollection.CopyTo(Array array, int index)
    {
      if (!(array is Tab[]))
        return;
      this.CopyTo((Tab[]) array, index);
    }

    bool ICollection.IsSynchronized => false;

    object ICollection.SyncRoot => (object) this;

    int IList.Add(object value)
    {
      if (!(value is Tab))
        throw new NotSupportedException();
      this.Add((Tab) value);
      return this.IndexOf((Tab) value);
    }

    bool IList.Contains(object value) => value is Tab && this.Contains((Tab) value);

    bool IList.IsFixedSize => false;

    bool IList.IsReadOnly => false;

    object IList.this[int index]
    {
      get => (object) this[index];
      set
      {
      }
    }

    int IList.IndexOf(object value) => value is Tab ? this.IndexOf((Tab) value) : -1;

    void IList.Insert(int index, object value) => this.Insert(index, (Tab) value);

    void IList.Remove(object value)
    {
      if (!(value is Tab))
        return;
      this.Remove((Tab) value);
    }

    public int Count => this._tabs.Count;

    public Tab this[int index] => (Tab) this._tabs[index];
  }
}
