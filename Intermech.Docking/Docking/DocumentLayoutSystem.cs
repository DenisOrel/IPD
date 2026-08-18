
// Type: Intermech.Docking.DocumentLayoutSystem
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using Intermech.Docking.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Docking;

public class DocumentLayoutSystem : ControlLayoutSystem
{
  private const int ButtonWidth = 14;
  private const int ButtonHeight = 15;
  private int _scrollOffset;
  private int _maxScroll;
  private Timer _scrollTimer;
  private bool _scrollingEnabled;
  internal TitleButton _scrollLeftButton;
  internal TitleButton _scrollRightButton;
  private TitleButton _closeButton;
  private TitleButton _closeButton2;
  private TitleButton _docListButton;

  public DocumentLayoutSystem()
  {
    this._scrollOffset = 0;
    this._maxScroll = 0;
    this._scrollingEnabled = true;
    this._scrollLeftButton = new TitleButton();
    this._scrollRightButton = new TitleButton();
    this._docListButton = new TitleButton();
    this._closeButton = new TitleButton();
    this._closeButton2 = new TitleButton();
    this._scrollTimer = new Timer();
    this._scrollTimer.Interval = 20;
    this._scrollTimer.Tick += new EventHandler(this.ScrollTimer_Tick);
  }

  public DocumentLayoutSystem(int desiredWidth, int desiredHeight)
    : this()
  {
    this._workingSize = new SizeF((float) desiredWidth, (float) desiredHeight);
  }

  public DocumentLayoutSystem(
    int desiredWidth,
    int desiredHeight,
    DockControl[] controls,
    DockControl selectedControl)
    : this(desiredWidth, desiredHeight)
  {
    this.Controls.AddRange(controls);
    if (selectedControl == null)
      return;
    this.SelectedControl = selectedControl;
  }

  private void FireScrollTimerTick()
  {
    this._scrollTimer.Enabled = true;
    this.ScrollTimer_Tick((object) this._scrollTimer, EventArgs.Empty);
  }

  private void DoScroll(int value)
  {
    this._scrollOffset += value;
    if (this._scrollOffset > this._maxScroll)
    {
      this._scrollOffset = this._maxScroll;
      this.StopScroll();
    }
    if (this._scrollOffset < 0)
    {
      this._scrollOffset = 0;
      this.StopScroll();
    }
    this.Repaint();
  }

  private void SelectDocument(DockControl document)
  {
    if (this._maxScroll <= 0)
      return;
    Rectangle tabBounds = document._tabBounds;
    int num1 = this._tabStripBounds.Right - this.RightPadding;
    int num2 = this._tabStripBounds.Left + this.LeftPadding;
    int num3 = num1 - num2;
    int num4 = 0;
    if (tabBounds.Right > num1)
      num4 = tabBounds.Right - num3 + 30;
    if (tabBounds.Left < num2)
      num4 = tabBounds.Left - num2 - 30;
    if (num4 == 0)
      return;
    this.DoScroll(num4);
  }

  private void ScrollTimer_Tick(object sender, EventArgs e)
  {
    if (this._activeButton == this._scrollLeftButton)
      this.DoScroll(-15);
    else if (this._activeButton == this._scrollRightButton)
      this.DoScroll(15);
    else
      this.StopScroll();
  }

  private void LayoutDocuments(RendererBase renderer, Graphics g, Rectangle tabStripBounds)
  {
    List<DockControl> dockControlList = new List<DockControl>();
    int num1 = 250;
    while (true)
    {
      int x = 3;
      foreach (DockControl control in (CollectionBase) this.Controls)
      {
        DrawItemState state = DrawItemState.Default;
        if (this.SelectedControl == control)
        {
          state |= DrawItemState.Selected;
          if (this.DockContainer is DocumentContainer && ((DocumentContainer) this.DockContainer).ActiveDocument == control)
            state |= DrawItemState.Focus;
        }
        Image image = (Image) null;
        if (this.ShowImageInDocumentTab && control.ShowImageInDocumentTab)
          image = control.TabImage;
        string text = control.TabText;
        if (!string.IsNullOrEmpty(control.ExtraText))
          text = control.TabText + DockingConsts.TabTextSeparator + control.ExtraText;
        Size size = renderer.MeasureDocumentStripTab(g, image, text, control.Font, state);
        if (this.DrawIntegralClose && control.Closable)
          size.Width += 13;
        int width = size.Width;
        control._textTrimmed = false;
        if (width > num1)
        {
          width = num1;
          control._textTrimmed = true;
          dockControlList.Add(control);
        }
        control._tabBounds = new Rectangle(x, tabStripBounds.Bottom - renderer.DocumentTabSize, width, renderer.DocumentTabSize);
        x += width - renderer.DocumentTabExtra + 1;
      }
      if (this.Controls.Count != 0)
        x += renderer.DocumentTabExtra;
      int num2 = x + 3;
      this._scrollLeftButton._visible = false;
      this._scrollRightButton._visible = false;
      int num3 = tabStripBounds.Width - this.LeftPadding - this.RightPadding;
      this._maxScroll = num2 - num3;
      if (this._maxScroll < 0)
      {
        if (dockControlList.Count > 0)
        {
          int num4 = (-this._maxScroll - 3) / dockControlList.Count;
          if (num4 > 0)
          {
            num1 += num4;
            dockControlList.Clear();
          }
          else
            break;
        }
        else
          break;
      }
      else
        goto label_26;
    }
    this._maxScroll = 0;
    goto label_28;
label_26:
    if (this._scrollingEnabled && this.Controls.Count > 1)
    {
      this._scrollLeftButton._visible = true;
      this._scrollRightButton._visible = true;
      this._maxScroll += 28;
    }
label_28:
    if (this._scrollOffset > this._maxScroll)
      this._scrollOffset = this._maxScroll;
    this._scrollLeftButton._enabled = this._scrollOffset > 0;
    this._scrollRightButton._enabled = this._scrollOffset < this._maxScroll;
    foreach (DockControl control in (CollectionBase) this.Controls)
    {
      Rectangle tabBounds = control._tabBounds;
      tabBounds.Offset(tabStripBounds.Left + this.LeftPadding - this._scrollOffset, 0);
      control._tabBounds = tabBounds;
    }
    if (!this.DrawIntegralClose || this.SelectedControl == null || !this.SelectedControl.Closable)
      return;
    this._closeButton._visible = true;
    this._closeButton._bounds = this.CalcDocumentCloseButtonBounds(this.SelectedControl._tabBounds);
  }

  private Rectangle CalcDocumentCloseButtonBounds(Rectangle tabBounds)
  {
    return new Rectangle(tabBounds.Right - 17, tabBounds.Top + 2, 14, tabBounds.Height - 3);
  }

  private void PaintDocumentTab(
    RendererBase renderer,
    Graphics gr,
    Font font,
    DockControl dockControl)
  {
    int deltaClose = 0;
    if (this.DrawIntegralClose && dockControl.Closable)
      deltaClose = 13;
    DrawItemState state = DrawItemState.Default;
    if (this.SelectedControl == dockControl)
    {
      state |= DrawItemState.Selected;
      if (this.DockContainer is DocumentContainer && ((DocumentContainer) this.DockContainer).ActiveDocument == dockControl)
        state |= DrawItemState.Focus;
    }
    bool drawSeparator = true;
    if (this.SelectedControl != null && this.Controls.IndexOf(dockControl) == this.Controls.IndexOf(this.SelectedControl) - 1)
      drawSeparator = false;
    Image image = (Image) null;
    if (this.ShowImageInDocumentTab && dockControl.ShowImageInDocumentTab)
      image = dockControl.TabImage;
    string text = dockControl.TabText;
    if (!string.IsNullOrEmpty(dockControl.ExtraText))
      text = dockControl.TabText + DockingConsts.TabTextSeparator + dockControl.ExtraText;
    if ((state & DrawItemState.Focus) == DrawItemState.Focus)
    {
      using (Font font1 = new Font(font, FontStyle.Bold))
        renderer.DrawDocumentStripTab(gr, dockControl._tabBounds, image, text, font1, dockControl.BackColor, dockControl.ForeColor, state, drawSeparator, deltaClose);
    }
    else
      renderer.DrawDocumentStripTab(gr, dockControl._tabBounds, image, text, font, dockControl.BackColor, dockControl.ForeColor, state, drawSeparator, deltaClose);
  }

  private void StopScroll()
  {
    this._scrollTimer.Enabled = false;
    this._activeButton = (TitleButton) null;
    this._buttonPressed = false;
    this.InvalidateTitleBar();
  }

  private void LayoutButtons(RendererBase renderer, Graphics g, Rectangle tabStripBounds)
  {
    int y = tabStripBounds.Top + tabStripBounds.Height / 2 - 7;
    int num1 = tabStripBounds.Right - 2;
    if (this.SelectedControl != null && this.SelectedControl.Closable && !this.DrawIntegralClose)
    {
      this._closeButton._visible = true;
      this._closeButton._bounds = new Rectangle(num1 - 14, y, 14, 15);
      num1 -= 15;
      this._closeButton._enabled = this.SelectedControl != null && this.SelectedControl.Closable;
    }
    else
      this._closeButton._visible = false;
    if (this.DocListEnabled)
    {
      this._docListButton._visible = true;
      this._docListButton._bounds = new Rectangle(num1 - 14, y, 14, 15);
      num1 -= 15;
    }
    else
      this._docListButton._visible = false;
    if (this._scrollingEnabled)
    {
      this._scrollRightButton._visible = true;
      this._scrollRightButton._bounds = new Rectangle(num1 - 14, y, 14, 15);
      int num2 = num1 - 15;
      this._scrollLeftButton._visible = true;
      this._scrollLeftButton._bounds = new Rectangle(tabStripBounds.Left + 2, y, 14, 15);
      int num3 = num2 - 15;
    }
    else
    {
      this._scrollRightButton._visible = false;
      this._scrollLeftButton._visible = false;
    }
  }

  protected override void CalculateLayout(
    RendererBase renderer,
    Rectangle bounds,
    bool floating,
    out Rectangle titlebarBounds,
    out Rectangle tabstripBounds,
    out Rectangle clientBounds,
    out Rectangle joinCatchmentBounds)
  {
    titlebarBounds = Rectangle.Empty;
    tabstripBounds = bounds;
    tabstripBounds.Height = renderer.DocumentTabStripSize;
    bounds.Offset(0, renderer.DocumentTabStripSize);
    bounds.Height -= renderer.DocumentTabStripSize;
    clientBounds = bounds;
    joinCatchmentBounds = tabstripBounds;
  }

  protected internal override ControlLayoutSystem CreateNewLayoutSystem()
  {
    return (ControlLayoutSystem) new DocumentLayoutSystem();
  }

  protected internal override ControlLayoutSystem CreateNewLayoutSystem(
    int desiredWidth,
    int desiredHeight,
    DockControl[] controls,
    DockControl selectedControl)
  {
    return (ControlLayoutSystem) new DocumentLayoutSystem(desiredWidth, desiredHeight, controls, selectedControl);
  }

  internal override TitleButton GetButtonAt(int x, int y)
  {
    this._closeButton2._visible = false;
    this._closeButton2._tag = (object) null;
    if (this._closeButton._visible && this._closeButton._enabled && this._closeButton._bounds.Contains(x, y))
      return this._closeButton;
    if (this._docListButton._visible && this._docListButton._enabled && this._docListButton._bounds.Contains(x, y))
      return this._docListButton;
    if (this._scrollLeftButton._visible && this._scrollLeftButton._enabled && this._scrollLeftButton._bounds.Contains(x, y))
      return this._scrollLeftButton;
    if (this._scrollRightButton._visible && this._scrollRightButton._enabled && this._scrollRightButton._bounds.Contains(x, y))
      return this._scrollRightButton;
    if (this.DrawIntegralClose)
    {
      DockControl controlAt = this.GetControlAt(new Point(x, y));
      if (controlAt != null && controlAt.Closable)
      {
        Rectangle rectangle = this.CalcDocumentCloseButtonBounds(controlAt._tabBounds);
        if (rectangle.Contains(x, y))
        {
          this._closeButton2._bounds = rectangle;
          this._closeButton2._visible = true;
          this._closeButton2._tag = (object) controlAt;
          return this._closeButton2;
        }
      }
    }
    return (TitleButton) null;
  }

  public override DockControl GetControlAt(Point position)
  {
    return !this._tabStripBounds.Contains(position) || position.X >= this._tabStripBounds.X + this.LeftPadding && position.X <= this._tabStripBounds.Right - this.RightPadding ? base.GetControlAt(position) : (DockControl) null;
  }

  internal override string GetToolTipText(Point position)
  {
    TitleButton buttonAt = this.GetButtonAt(position.X, position.Y);
    if (buttonAt == this._scrollLeftButton)
      return DockLanguage.ScrollLeftText;
    if (buttonAt == this._scrollRightButton)
      return DockLanguage.ScrollRightText;
    if (buttonAt == this._closeButton || buttonAt == this._closeButton2)
      return DockLanguage.CloseText;
    return buttonAt == this._docListButton ? DockLanguage.DocumentListText : base.GetToolTipText(position);
  }

  internal override void InvalidateTitleBar()
  {
    if (this.DockContainer == null)
      return;
    this.DockContainer.Invalidate(this._tabStripBounds);
  }

  protected internal override void Layout(
    RendererBase renderer,
    Graphics graphics,
    Rectangle bounds,
    bool floating)
  {
    base.Layout(renderer, graphics, bounds, floating);
    this.LayoutButtons(renderer, graphics, this._tabStripBounds);
    this.LayoutDocuments(renderer, graphics, this._tabStripBounds);
    bounds = this._documentBounds;
    Size documentClientPadding = renderer.DocumentClientPadding;
    bounds.Inflate(-documentClientPadding.Width, -documentClientPadding.Height);
    this._layoutInProgress = true;
    try
    {
      int count1 = this.Controls.Count;
      for (int index = 0; index < count1; ++index)
      {
        DockControl control = this.Controls[index];
        if (control.Parent != this.DockContainer)
        {
          if (control.Parent != null)
            DockHelper.DetachControl((Control) control);
          this.DockContainer.Controls.Add((Control) control);
        }
        if (control == this.SelectedControl)
        {
          control.Visible = true;
          control.Bounds = bounds;
        }
      }
      int count2 = this.Controls.Count;
      for (int index = 0; index < count2; ++index)
      {
        if (index < this.Controls.Count)
        {
          DockControl control = this.Controls[index];
          if (control != this.SelectedControl)
            control.Visible = false;
        }
      }
    }
    finally
    {
      this._layoutInProgress = false;
    }
  }

  internal override void OnButtonPress(TitleButton button)
  {
    if (button != this._scrollLeftButton && button != this._scrollRightButton)
      return;
    this.FireScrollTimerTick();
  }

  private void OnDocumentListClick()
  {
    if (!(this.DockContainer is DocumentContainer dockContainer))
      return;
    dockContainer.OnDocumentListClick(this);
  }

  internal override void OnButtonPressed(TitleButton button)
  {
    if (button == this._closeButton)
      this.OnCloseButtonClick((EventArgs) new CancelEventArgs());
    else if (button == this._closeButton2)
    {
      if (!(this._closeButton2._tag is DockControl tag))
        return;
      tag.Close();
    }
    else if (button == this._docListButton)
    {
      this.OnDocumentListClick();
    }
    else
    {
      if (button != this._scrollLeftButton && button != this._scrollRightButton)
        return;
      this.StopScroll();
    }
  }

  protected internal override void OnControlEnter(DockControl control)
  {
    base.OnControlEnter(control);
    if (!(this.DockContainer is DocumentContainer))
      return;
    ((DocumentContainer) this.DockContainer).ActivateDocument(control);
  }

  internal override void OnDockingManagerCommitted(StandardDocker.DockingSite target)
  {
    base.OnDockingManagerCommitted(target);
    if (target == null)
      return;
    int redockType = (int) target._redockType;
  }

  internal override void Paint(RendererBase renderer, Graphics graphics, Font font)
  {
    renderer.DrawDocumentStripBackground(graphics, this._tabStripBounds);
    if (this.SelectedControl != null)
      renderer.DrawDocumentClientBackground(graphics, this._documentBounds, this.SelectedControl.BackColor);
    else
      renderer.DrawDocumentClientBackground(graphics, this._documentBounds, SystemColors.Control);
    Region clip = graphics.Clip;
    Rectangle tabStripBounds = this._tabStripBounds;
    tabStripBounds.X += this.LeftPadding;
    tabStripBounds.Width -= this.LeftPadding;
    tabStripBounds.Width -= this.RightPadding;
    graphics.SetClip(tabStripBounds);
    for (int index = this.Controls.Count - 1; index >= 0; --index)
    {
      DockControl control = this.Controls[index];
      this.PaintDocumentTab(renderer, graphics, control.Font, control);
    }
    if (this.SelectedControl != null)
    {
      this.PaintDocumentTab(renderer, graphics, this.SelectedControl.Font, this.SelectedControl);
      if (this.DrawIntegralClose)
      {
        this.DrawStripButton(graphics, renderer, this._closeButton, ButtonType.Close, this._closeButton._enabled);
        if (this._closeButton2._visible)
          this.DrawStripButton(graphics, renderer, this._closeButton2, ButtonType.Close, this._closeButton2._enabled);
      }
    }
    graphics.Clip = clip;
    if (!this.DrawIntegralClose)
      this.DrawStripButton(graphics, renderer, this._closeButton, ButtonType.Close, this._closeButton._enabled);
    this.DrawStripButton(graphics, renderer, this._docListButton, ButtonType.DocList, this._docListButton._enabled);
    this.DrawStripButton(graphics, renderer, this._scrollRightButton, ButtonType.ScrollRight, this._scrollRightButton._enabled);
    this.DrawStripButton(graphics, renderer, this._scrollLeftButton, ButtonType.ScrollLeft, this._scrollLeftButton._enabled);
  }

  public override bool Collapsed
  {
    get => false;
    set
    {
    }
  }

  protected virtual int LeftPadding => this._scrollLeftButton._visible ? 16 /*0x10*/ : 0;

  protected virtual int RightPadding
  {
    get
    {
      if (this._scrollRightButton._visible)
        return this.Bounds.Right - this._scrollRightButton._bounds.Left;
      if (this._docListButton._visible)
        return this.Bounds.Right - this._docListButton._bounds.Left;
      return !this.DrawIntegralClose && this._closeButton._visible ? this.Bounds.Right - this._closeButton._bounds.Left : 0;
    }
  }

  public bool DocListEnabled
  {
    get => this.DockContainer is DocumentContainer dockContainer && dockContainer.DocListEnabled();
  }

  public bool ScrollingEnabled
  {
    get => this._scrollingEnabled;
    set
    {
      if (this._scrollingEnabled == value)
        return;
      this._scrollingEnabled = value;
      this.Repaint();
    }
  }

  public override DockControl SelectedControl
  {
    get => base.SelectedControl;
    set
    {
      base.SelectedControl = value;
      if (value == null)
        return;
      if (this._mouseDown)
        this._skipMouseUp = true;
      this.SelectDocument(value);
    }
  }

  private bool ShowImageInDocumentTab
  {
    get
    {
      return !(this.DockContainer is DocumentContainer dockContainer) || dockContainer.ShowImageInDocumentTab;
    }
  }

  private bool DrawIntegralClose
  {
    get => this.DockContainer is DocumentContainer dockContainer && dockContainer.IntegralClose;
  }

  internal override DockControl FindLastUsedControl()
  {
    DockControl lastUsedControl = (DockControl) null;
    if (this._controls.Count > 0)
      lastUsedControl = this._controls[0];
    DateTime dateTime = DateTime.MinValue;
    foreach (DockControl control in (CollectionBase) this._controls)
    {
      if (control.LastFocused > dateTime)
      {
        dateTime = control.LastFocused;
        lastUsedControl = control;
      }
    }
    return lastUsedControl;
  }
}
