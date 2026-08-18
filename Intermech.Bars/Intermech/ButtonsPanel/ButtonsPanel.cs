
// Type: Intermech.ButtonsPanel.ButtonsPanel
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Intermech.ButtonsPanel
{
    [DefaultEvent("ButtonClick")]
    [Designer(typeof (ButtonsPanelDesigner))]
    public class ButtonsPanel : UserControl
    {
      private ToolTip _toolTip;
      private PanelButtonCollection _buttons;
      private int _buttonSpacing;
      private int _fitHeight;
      private ButtonsPanelRenderer _renderer;
      private ImageList _imageList;
      private bool _bPressed;
      internal bool _bShowScroll;
      [AccessedThroughProperty("tmrScroll")]
      private Timer _tmrScroll;
      private IContainer components;
      private PanelButton _hoverButton;
      private Size _knownImageSize;
      private bool _layoutInvalid;
      private const int SCROLLAMOUNT = 15;
      internal Rectangle _scrollDownBounds;
      private bool _scrollDownHover;
      private int _scrollOffset;
      internal Rectangle _scrollUpBounds;
      private bool _scrollUpHover;
      internal StringFormat _buttonTextFormat;
      private ButtonHighlightType _highlightType;
      private int _idealHeight;
      private ButtonLayoutType _layoutType;
      internal int _internalButtonSelected;
      private Color _paneLeftColor;
      private Color _paneRightColor;
      private bool _flat;

      public event Intermech.ButtonsPanel.ButtonsPanel.ButtonClickEventHandler ButtonClick;

      public ButtonsPanel()
      {
        this._layoutInvalid = false;
        this._internalButtonSelected = -1;
        this._layoutType = ButtonLayoutType.TextRight;
        this._highlightType = ButtonHighlightType.ImageAndText;
        this._buttonSpacing = 4;
        this._buttons = new PanelButtonCollection(this);
        this.InitializeComponent();
        this.Initialize();
        this.InitializeFormat();
        this._paneLeftColor = SystemColors.Control;
        this._paneRightColor = SystemColors.ControlLightLight;
        this.SetStyle(ControlStyles.DoubleBuffer, true);
      }

      [DebuggerStepThrough]
      private void InitializeComponent() => this.components = (IContainer) new System.ComponentModel.Container();

      private void Initialize()
      {
        this._toolTip = new ToolTip();
        this.tmrScroll = new Timer();
        this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        this.SetStyle(ControlStyles.DoubleBuffer, true);
        this.SetStyle(ControlStyles.Selectable, false);
        this._renderer = new ButtonsPanelRenderer();
      }

      internal void InitializeFormat()
      {
        if (this._buttonTextFormat != null)
          this._buttonTextFormat.Dispose();
        this._buttonTextFormat = new StringFormat();
        if (this._layoutType == ButtonLayoutType.TextBelow)
        {
          this._buttonTextFormat.Alignment = StringAlignment.Center;
          this._buttonTextFormat.LineAlignment = StringAlignment.Near;
        }
        else
        {
          this._buttonTextFormat.Alignment = StringAlignment.Near;
          this._buttonTextFormat.LineAlignment = StringAlignment.Center;
        }
        this._buttonTextFormat.Trimming = StringTrimming.EllipsisCharacter;
      }

      private void CalculateLayout(Graphics g)
      {
        if (this._imageList != null)
          this._knownImageSize = this._imageList.ImageSize;
        Rectangle rectangle1 = this.DisplayRectangle;
        this._bShowScroll = false;
        this._fitHeight = 0;
        int num1 = this._buttonSpacing / 2;
        if (num1 < 5)
          num1 = 5;
        int num2 = 0;
        foreach (PanelButton button in (CollectionBase) this._buttons)
        {
          if (this.ButtonVisible(button))
          {
            num1 += button.GetHeight(g);
            num1 += this._buttonSpacing;
            ++num2;
          }
        }
        if (num2 != 0)
          num1 -= this._buttonSpacing / 2;
        this._fitHeight = num1;
        if (num1 > rectangle1.Height)
        {
          rectangle1.X = rectangle1.Right - SystemInformation.VerticalScrollBarWidth - 3;
          rectangle1.Width = SystemInformation.VerticalScrollBarWidth;
          rectangle1.Y += 3;
          rectangle1.Height = SystemInformation.VerticalScrollBarThumbHeight;
          this._scrollUpBounds = rectangle1;
          rectangle1 = this.DisplayRectangle;
          rectangle1.X = rectangle1.Right - SystemInformation.VerticalScrollBarWidth - 3;
          rectangle1.Width = SystemInformation.VerticalScrollBarWidth;
          rectangle1.Y = rectangle1.Bottom - SystemInformation.VerticalScrollBarThumbHeight - 3;
          rectangle1.Height = SystemInformation.VerticalScrollBarThumbHeight;
          this._scrollDownBounds = rectangle1;
          this._bShowScroll = true;
          this._idealHeight = num1;
          rectangle1 = this.DisplayRectangle;
          if (this._scrollOffset > this._idealHeight - rectangle1.Height)
            this._scrollOffset = this._idealHeight - rectangle1.Height;
        }
        else
          this._scrollOffset = 0;
        int num3 = rectangle1.Top + this._buttonSpacing / 2;
        if (num3 < rectangle1.Top + 5)
          num3 = rectangle1.Top + 5;
        int y = num3 - this._scrollOffset;
        int count = this._buttons.Count;
        for (int Index = 0; Index < count; ++Index)
        {
          PanelButton button = this._buttons[Index];
          if (this.ButtonVisible(button))
          {
            int height = button.GetHeight(g);
            Rectangle displayRectangle = this.DisplayRectangle;
            Rectangle rectangle2 = new Rectangle(1, y, displayRectangle.Width - 3, height);
            rectangle1 = rectangle2;
            this._buttons[Index]._outerBounds = rectangle1;
            Size imageSize;
            if (this._layoutType == ButtonLayoutType.TextBelow)
            {
              if (this._imageList != null)
              {
                rectangle1 = button._outerBounds;
                rectangle2 = this.DisplayRectangle;
                imageSize = this._imageList.ImageSize;
                rectangle1.X = rectangle2.Width / 2 - imageSize.Width / 2;
                rectangle1.Y += 2;
                rectangle1.Width = imageSize.Width;
                rectangle1.Height = imageSize.Height;
                button._imageBounds = rectangle1;
                rectangle1 = button._outerBounds;
                rectangle1.Y += imageSize.Height + 7;
                rectangle1.Height -= imageSize.Height + 7;
              }
              rectangle2 = this.DisplayRectangle;
              SizeF layoutArea = new SizeF((float) (rectangle2.Width - 2), 100f);
              SizeF sizeF = g.MeasureString(button.Text, this.Font, layoutArea, this._buttonTextFormat);
              rectangle1.X = rectangle2.Width / 2 - Convert.ToInt32(sizeF.Width) / 2 + 1;
              rectangle1.Width = Convert.ToInt32(sizeF.Width) + 2;
              rectangle1.Height = Convert.ToInt32(sizeF.Height) + 1;
              button._textBounds = rectangle1;
            }
            else
            {
              if (this.HasImage(button))
              {
                rectangle1.X = 5;
                imageSize = this._imageList.ImageSize;
                rectangle1.Y += rectangle1.Height / 2 - imageSize.Height / 2;
                rectangle1.Width = imageSize.Width;
                rectangle1.Height = imageSize.Height;
                button._imageBounds = rectangle1;
                rectangle1 = button._outerBounds;
                rectangle1.X += imageSize.Width + 7;
                rectangle1.Width -= imageSize.Height + 7;
              }
              button._textBounds = rectangle1;
            }
            if (this._highlightType == ButtonHighlightType.ImageAndText)
            {
              button._selectionBounds = button._outerBounds;
            }
            else
            {
              rectangle1 = button._imageBounds;
              rectangle1.Inflate(2, 2);
              button._selectionBounds = rectangle1;
            }
            y += height + this._buttonSpacing + 1;
          }
        }
      }

      public void ApplyLayout()
      {
        using (Graphics g = Graphics.FromHwnd(this.Handle))
        {
          this.CalculateLayout(g);
          this._layoutInvalid = false;
        }
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
        {
          this._renderer.Dispose();
          if (this.components != null)
            this.components.Dispose();
        }
        base.Dispose(disposing);
      }

      internal void DoMouseMove(MouseEventArgs e)
      {
        if (this.DesignMode)
        {
          if (e.Button != MouseButtons.Left)
            return;
          IDesignerHost service1 = (IDesignerHost) this.GetService(typeof (IDesignerHost));
          IComponentChangeService service2 = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
          if (this._internalButtonSelected == -1)
            return;
          int Index1 = -1;
          int num = this._buttons.Count - 1;
          for (int Index2 = 0; Index2 <= num; ++Index2)
          {
            Rectangle outerBounds = this._buttons[Index2]._outerBounds;
            if (outerBounds.Contains(e.X, e.Y))
            {
              if (Index2 == this._internalButtonSelected)
                return;
              if (Index2 < this._internalButtonSelected)
              {
                Index1 = e.Y >= outerBounds.Y + outerBounds.Height / 2 ? Index2 + 1 : Index2;
                break;
              }
              if (Index2 > this._internalButtonSelected)
              {
                Index1 = e.Y <= outerBounds.Y + outerBounds.Height / 2 ? Index2 - 1 : Index2;
                break;
              }
              break;
            }
          }
          if (Index1 == -1)
            this.Invalidate();
          else if (Index1 == this._internalButtonSelected)
          {
            this.Invalidate();
          }
          else
          {
            PanelButton button = this._buttons[this._internalButtonSelected];
            this._buttons.Remove(button);
            this._buttons.Insert(Index1, button);
            this._internalButtonSelected = Index1;
          }
        }
        else
        {
          PanelButton panelButton = (PanelButton) null;
          if (this._bShowScroll)
          {
            if (this._scrollUpBounds.Contains(e.X, e.Y))
            {
              this._scrollUpHover = true;
              this._hoverButton = (PanelButton) null;
              this.Invalidate();
              return;
            }
            if (this._scrollDownBounds.Contains(e.X, e.Y))
            {
              this._scrollDownHover = true;
              this._hoverButton = (PanelButton) null;
              this.Invalidate();
              return;
            }
            if (this._scrollUpHover || this._scrollDownHover)
            {
              this._scrollUpHover = false;
              this._scrollDownHover = false;
              this.Invalidate();
            }
          }
          foreach (PanelButton button in (CollectionBase) this._buttons)
          {
            if (this.ButtonVisible(button) && button._outerBounds.Contains(e.X, e.Y))
            {
              panelButton = button;
              break;
            }
          }
          if (panelButton == this._hoverButton)
            return;
          if (this._hoverButton != null)
            this.InvalidateButton(this._hoverButton);
          this._hoverButton = panelButton;
          if (this._hoverButton != null)
          {
            this.InvalidateButton(this._hoverButton);
            this._toolTip.SetToolTip((Control) this, this._hoverButton._toolTipText);
          }
          else
            this._toolTip.SetToolTip((Control) this, string.Empty);
        }
      }

      private void DrawButtons(Graphics g, Brush textBrush, ArrayList selectedComponents)
      {
        g.Clip = new Region(this.DisplayRectangle);
        int count = this._buttons.Count;
        for (int Index = 0; Index < count; ++Index)
        {
          PanelButton button = this._buttons[Index];
          bool flag;
          if (this.DesignMode && selectedComponents != null)
            flag = selectedComponents.Contains((object) button);
          else if (button.Visible)
            flag = false;
          else
            continue;
          if (((this._hoverButton != button ? 0 : (button.Enabled ? 1 : 0)) | (flag ? 1 : 0)) != 0)
          {
            if (this._bPressed)
              this._renderer.DrawButtonHighlight(g, button._selectionBounds, false, ButtonsPanelRenderer.HighlightMode.Pushed);
            else
              this._renderer.DrawButtonHighlight(g, button._selectionBounds, false, ButtonsPanelRenderer.HighlightMode.Hot);
          }
          else if (button.Checked)
            this._renderer.DrawButtonHighlight(g, button._selectionBounds, false, ButtonsPanelRenderer.HighlightMode.Checked);
          if (this.HasImage(button))
          {
            Rectangle imageBounds1 = button._imageBounds;
            if (!object.Equals((object) imageBounds1, (object) Rectangle.Empty))
            {
              if (button.Enabled)
                g.DrawImage(this._imageList.Images[button.ImageIndex], imageBounds1);
              else
                this._renderer.DrawImageDisabled(g, this._imageList.Images[button.ImageIndex], imageBounds1, this.BackColor);
            }
            else
            {
              Rectangle imageBounds2 = button._imageBounds;
            }
          }
          if (!button._visible)
          {
            using (Font font = new Font(this.Font, FontStyle.Strikeout))
              g.DrawString(button.Text, font, textBrush, ConvertHelper.ToRectangleF(button._textBounds), this._buttonTextFormat);
          }
          else if (button.Enabled)
            g.DrawString(button.Text, this.Font, textBrush, ConvertHelper.ToRectangleF(button._textBounds), this._buttonTextFormat);
          else
            g.DrawString(button.Text, this.Font, SystemBrushes.ControlDark, ConvertHelper.ToRectangleF(button._textBounds), this._buttonTextFormat);
        }
        if (this._bShowScroll)
        {
          if (this._scrollOffset == 0)
            ControlPaint.DrawScrollButton(g, this._scrollUpBounds, ScrollButton.Up, ButtonState.Flat | ButtonState.Inactive);
          else if (this._scrollUpHover)
          {
            if (this._bPressed)
              ControlPaint.DrawScrollButton(g, this._scrollUpBounds, ScrollButton.Up, ButtonState.Pushed);
            else
              ControlPaint.DrawScrollButton(g, this._scrollUpBounds, ScrollButton.Up, ButtonState.Normal);
          }
          else
            ControlPaint.DrawScrollButton(g, this._scrollUpBounds, ScrollButton.Up, ButtonState.Flat);
          if (this._scrollOffset == this._idealHeight - this.DisplayRectangle.Height)
            ControlPaint.DrawScrollButton(g, this._scrollDownBounds, ScrollButton.Down, ButtonState.Flat | ButtonState.Inactive);
          else if (this._scrollDownHover)
          {
            if (this._bPressed)
              ControlPaint.DrawScrollButton(g, this._scrollDownBounds, ScrollButton.Down, ButtonState.Pushed);
            else
              ControlPaint.DrawScrollButton(g, this._scrollDownBounds, ScrollButton.Down, ButtonState.Normal);
          }
          else
            ControlPaint.DrawScrollButton(g, this._scrollDownBounds, ScrollButton.Down, ButtonState.Flat);
        }
        g.Clip = new Region(this.DisplayRectangle);
      }

      private bool ButtonVisible(PanelButton button) => this.DesignMode || button.Visible;

      public PanelButton GetButtonAt(Point p)
      {
        foreach (PanelButton button in (CollectionBase) this._buttons)
        {
          if (this.ButtonVisible(button) && button._outerBounds.Contains(p))
            return button;
        }
        return (PanelButton) null;
      }

      public PanelButton GetButtonAt(int x, int y) => this.GetButtonAt(new Point(x, y));

      public PanelButton AddButton(string text, int imageIndex, string toolTip)
      {
        PanelButton button = new PanelButton();
        button.Text = text;
        button.ImageIndex = imageIndex;
        button._toolTipText = toolTip;
        this.Buttons.Add(button);
        return button;
      }

      private bool HasImage(PanelButton button)
      {
        return this._imageList != null && button.ImageIndex >= 0 && button.ImageIndex < this._imageList.Images.Count;
      }

      [DebuggerStepThrough]
      internal void InvalidateButton(PanelButton button)
      {
        if (!this.ButtonVisible(button))
          return;
        Rectangle outerBounds = button._outerBounds;
        outerBounds.Inflate(2, 2);
        this.Invalidate(outerBounds);
      }

      internal void InvalidateLayout()
      {
        this._layoutInvalid = true;
        this.Invalidate();
      }

      protected override void OnFontChanged(EventArgs e)
      {
        base.OnFontChanged(e);
        this.InvalidateLayout();
      }

      private void OnImageListHandleRecreated(object sender, EventArgs e) => this.InvalidateLayout();

      protected override void OnMouseDown(MouseEventArgs e)
      {
        base.OnMouseDown(e);
        if (this.DesignMode)
        {
          foreach (PanelButton button in (CollectionBase) this._buttons)
          {
            if (button._outerBounds.Contains(e.X, e.Y))
            {
              ((ISelectionService) this.GetService(typeof (ISelectionService))).SetSelectedComponents((ICollection) new ArrayList()
              {
                (object) button
              });
              this._internalButtonSelected = this._buttons.IndexOf(button);
              return;
            }
          }
        }
        if (this._scrollUpHover || this._scrollDownHover)
        {
          this._bPressed = true;
          this.tmrScroll.Interval = 300;
          this.tmrScroll.Enabled = true;
          this.PerformScroll();
        }
        if (this._hoverButton == null)
          return;
        this._bPressed = true;
        this.InvalidateButton(this._hoverButton);
      }

      protected override void OnMouseLeave(EventArgs e)
      {
        base.OnMouseLeave(e);
        if (this._hoverButton != null)
        {
          this.InvalidateButton(this._hoverButton);
          this._hoverButton = (PanelButton) null;
        }
        if (!this._scrollUpHover && !this._scrollDownHover)
          return;
        this._scrollUpHover = false;
        this._scrollDownHover = false;
        this.Invalidate();
      }

      protected override void OnMouseMove(MouseEventArgs e)
      {
        base.OnMouseMove(e);
        this.DoMouseMove(e);
      }

      protected override void OnMouseUp(MouseEventArgs e)
      {
        base.OnMouseUp(e);
        if (this.DesignMode || !this._bPressed)
          return;
        if (this._hoverButton != null)
        {
          this._bPressed = false;
          this.InvalidateButton(this._hoverButton);
          if (!this._hoverButton.Enabled || e.Button != MouseButtons.Left)
            return;
          if (this.ButtonClick != null)
            this.ButtonClick((object) this, new PanelButtonClickEventArgs(this._hoverButton));
          this._hoverButton.OnClick();
        }
        else
        {
          this._bPressed = false;
          this.Invalidate();
          if (!this.tmrScroll.Enabled)
            return;
          this.tmrScroll.Enabled = false;
        }
      }

      protected override void OnPaint(PaintEventArgs e)
      {
        ArrayList selectedComponents = (ArrayList) null;
        if (this._layoutInvalid)
        {
          this.CalculateLayout(e.Graphics);
          this._layoutInvalid = false;
        }
        if (this._imageList != null && this._imageList.ImageSize != this._knownImageSize)
          this.CalculateLayout(e.Graphics);
        if (this.DesignMode)
          selectedComponents = new ArrayList(((ISelectionService) this.GetService(typeof (ISelectionService))).GetSelectedComponents());
        ControlPaint.DrawBorder3D(e.Graphics, this.ClientRectangle, Border3DStyle.SunkenOuter);
        using (SolidBrush textBrush = new SolidBrush(this.ForeColor))
          this.DrawButtons(e.Graphics, (Brush) textBrush, selectedComponents);
      }

      protected override void OnPaintBackground(PaintEventArgs pevent)
      {
        Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
        if (this._flat)
        {
          using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
            pevent.Graphics.FillRectangle((Brush) solidBrush, rect);
        }
        else
        {
          using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, this._paneLeftColor, this._paneRightColor, LinearGradientMode.Horizontal))
            pevent.Graphics.FillRectangle((Brush) linearGradientBrush, rect);
        }
      }

      protected override void OnResize(EventArgs e)
      {
        base.OnResize(e);
        this.InvalidateLayout();
      }

      private void PerformScroll()
      {
        Rectangle displayRectangle = this.DisplayRectangle;
        if (this._scrollUpHover)
        {
          this._scrollOffset -= 15;
          if (this._scrollOffset <= 0)
            this._scrollOffset = 0;
        }
        else if (this._scrollDownHover)
        {
          this._scrollOffset += 15;
          if (this._scrollOffset > this._idealHeight - displayRectangle.Height)
            this._scrollOffset = this._idealHeight - displayRectangle.Height;
        }
        if (!this._scrollUpHover && !this._scrollDownHover)
          return;
        this.InvalidateLayout();
      }

      private void tmrScroll_Tick(object sender, EventArgs e)
      {
        if (this.tmrScroll.Interval == 300)
        {
          this.tmrScroll.Enabled = false;
          this.tmrScroll.Interval = 50;
          this.tmrScroll.Enabled = true;
        }
        this.PerformScroll();
      }

      [DefaultValue(typeof (ImageList), null)]
      public ImageList ImageList
      {
        get => this._imageList;
        set
        {
          if (this._imageList != null)
            this._imageList.RecreateHandle -= new EventHandler(this.OnImageListHandleRecreated);
          this._imageList = value;
          if (this._imageList != null)
            this._imageList.RecreateHandle += new EventHandler(this.OnImageListHandleRecreated);
          this.InvalidateLayout();
        }
      }

      [Browsable(false)]
      private Timer tmrScroll
      {
        get => this._tmrScroll;
        set
        {
          if (this._tmrScroll != null)
            this._tmrScroll.Tick -= new EventHandler(this.tmrScroll_Tick);
          this._tmrScroll = value;
          if (this._tmrScroll == null)
            return;
          this._tmrScroll.Tick += new EventHandler(this.tmrScroll_Tick);
        }
      }

      [Browsable(false)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      public PanelButtonCollection Buttons => this._buttons;

      [DefaultValue(4)]
      public int ButtonSpacing
      {
        get => this._buttonSpacing;
        set
        {
          if (this._buttonSpacing == value)
            return;
          this._buttonSpacing = value >= 0 && value <= 20 ? value : throw new ArgumentException("Button spacing must be a value between 0 and 20.");
          this.InvalidateLayout();
        }
      }

      [Browsable(false)]
      public int FitHeight => this._fitHeight;

      [DefaultValue(false)]
      public bool Flat
      {
        get => this._flat;
        set
        {
          if (this._flat == value)
            return;
          this._flat = value;
          this.Invalidate();
        }
      }

      [DefaultValue(ButtonHighlightType.ImageAndText)]
      public ButtonHighlightType HighlightType
      {
        get => this._highlightType;
        set
        {
          if (this._highlightType == value)
            return;
          this._highlightType = value;
          this.Invalidate();
        }
      }

      [DefaultValue(ButtonLayoutType.TextRight)]
      public ButtonLayoutType LayoutType
      {
        get => this._layoutType;
        set
        {
          if (this._layoutType == value)
            return;
          this._layoutType = value;
          this.InitializeFormat();
          this.InvalidateLayout();
        }
      }

      public delegate void ButtonClickEventHandler(object sender, PanelButtonClickEventArgs e);
    }
}
