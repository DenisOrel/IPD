
// Type: Intermech.Controls.BrushSelectionUserControl
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Extensions;
using Intermech.UI;
using Intermech.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Controls;

public class BrushSelectionUserControl : 
  SimpleBaseUserControl,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl,
  IDesignModeControlsContainer,
  IArrowKeysNavigationSupported,
  ILastFocusedControlTracker
{
  private bool _wasInit;
  private ColorButton _checkedButton;
  private Color _hatchBackColor = Color.Empty;
  private string _preSelectedOperationName = string.Empty;
  private BrushStyle.FillTypeEnum _preSelectedFillType;
  private HatchStyle _preSelectedHatchStyle = HatchStyle.Cross;
  private ColorButton _lastTopMostHatchButton;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ColorButton _btnTransparentBrush;
  private ColorButton _btnSolidBrush;
  private ColorButton button3;
  private ColorButton button4;
  private ColorButton button5;
  private ColorButton button6;
  private ColorButton button7;
  private ColorButton button8;
  private ColorButton button9;
  private ColorButton button10;
  private ColorButton button11;
  private ColorButton button12;
  private ColorButton button13;
  private ColorButton button14;
  private ColorButton button15;
  private ColorButton button16;
  private ColorButton button17;
  private ColorButton button18;
  private ColorButton button19;
  private ColorButton button20;
  private ColorButton button21;
  private ColorButton button22;
  private ColorButton button23;
  private ColorButton button24;
  private ColorButton button25;
  private ColorButton button26;
  private ColorButton button27;
  private ColorButton button28;
  private ColorButton button29;
  private ColorButton button30;
  private ColorButton button31;
  private ColorButton button32;
  private ColorButton button33;
  private ColorButton button34;
  private ColorButton button35;
  private ColorButton button36;
  private ColorButton button37;
  private ColorButton button38;
  private ColorButton button39;
  private ColorButton button40;
  private ColorButton button41;
  private ColorButton button42;
  private ColorButton button43;
  private ColorButton button44;
  private ColorButton button45;
  private ColorButton button46;
  private ColorButton button47;
  private ColorButton button48;
  private ColorButton button49;
  private ColorButton button50;
  private ColorButton button51;
  private ColorButton button52;
  private ColorButton button53;
  private ColorButton button54;
  private ColorButton button55;
  private ColorSelectionUserControl _colorSelectionUserControl;
  private Panel _panelFillType;
  private SmoothLabel smoothLabel1;
  private Panel _panelTansparentButton;
  private Panel _panelFillSpacer;
  private Panel _panelSolidFillButton;
  private TableLayoutPanel _panelHatches;
  private ToolTip BrushStyleToolTip;

  public BrushSelectionUserControl()
  {
    this.InitializeComponent();
    if (this._colorSelectionUserControl.OperationName != this._preSelectedOperationName)
      this._colorSelectionUserControl.OperationName = this._preSelectedOperationName;
    if (!this.InDesignMode)
    {
      this._btnTransparentBrush.Click += new EventHandler(this._btnTransparentBrush_Click);
      this._btnTransparentBrush.KeyUp += new KeyEventHandler(this._btnTransparentBrush_KeyUp);
      this._btnTransparentBrush.MouseUp += new MouseEventHandler(this._btnTransparentBrush_MouseUp);
    }
    IEnumerator<BrushStyle> enumerator = BrushStyle.PossibleBrushesStyles.GetEnumerator();
    foreach (ColorButton allButton in this.GetAllButtons())
    {
      BrushStyle brushStyle = enumerator.MoveNext() ? enumerator.Current : throw new Exception($"No brush style for button \"{allButton.Name}\"");
      allButton.Tag = (object) brushStyle;
      if (brushStyle.FillType != BrushStyle.FillTypeEnum.Transparent)
      {
        if (allButton.Color != this._colorSelectionUserControl.Color)
          allButton.Color = this._colorSelectionUserControl.Color;
        if (allButton.HatchBackgroundColor != this.HatchBackColor)
          allButton.HatchBackgroundColor = this.HatchBackColor;
      }
      if (!this.InDesignMode)
        allButton.Click += new EventHandler(this.Button_Click);
    }
    this.SetCheckedButton(this.GetButtonByBrushStyle(BrushStyle.Get(this._preSelectedFillType, this._preSelectedHatchStyle)));
    this._colorSelectionUserControl.AddButtonsTooltipSuffix($".{Environment.NewLine}{Environment.NewLine}Используйте правую клавишу мыши или Ctrl+пробел{Environment.NewLine}для выбора цвета как фона штриховки.");
    this._wasInit = true;
  }

  private void _btnTransparentBrush_KeyUp(object sender, KeyEventArgs e)
  {
    if (!e.Control || e.KeyCode != Keys.Space || !(sender is ColorButton))
      return;
    this.HatchBackColor = Color.Empty;
    this.FireHatchBackColorWasSelected(Color.Empty);
  }

  private void _btnTransparentBrush_MouseUp(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right)
      return;
    this.HatchBackColor = Color.Empty;
    this.FireHatchBackColorWasSelected(Color.Empty);
  }

  private void _btnTransparentBrush_Click(object sender, EventArgs e) => this.Color = Color.Empty;

  private IEnumerable<ColorButton> GetAllButtons()
  {
    yield return this._btnTransparentBrush;
    yield return this._btnSolidBrush;
    foreach (ColorButton hatchButton in this.GetHatchButtons())
      yield return hatchButton;
  }

  private IEnumerable<ColorButton> GetHatchButtons()
  {
    return this._panelHatches.Controls.OfType<ColorButton>().Reverse<ColorButton>();
  }

  private void SetCheckedButton(ColorButton button)
  {
    if (!button.ForceDown)
      button.ForceDown = true;
    if (this._checkedButton == button)
      return;
    if (this._checkedButton != null && this._checkedButton.ForceDown)
      this._checkedButton.ForceDown = false;
    this._checkedButton = button;
  }

  private void Button_Click(object sender, EventArgs e)
  {
    if (!(sender is ColorButton))
      return;
    this.SetCheckedButton((ColorButton) sender);
    this.FireBrushStyleWasSelected(this.SelectedBrushStyle);
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Color), "Black")]
  public Color Color
  {
    [DebuggerStepThrough] get => this._colorSelectionUserControl.Color;
    set
    {
      if (!(this._colorSelectionUserControl.Color != value))
        return;
      this._colorSelectionUserControl.Color = value;
      this.AfterNewColor();
      this.FillTypeControlsVisible = value != Color.Transparent && value != Color.Empty;
    }
  }

  private bool FillTypeControlsVisible
  {
    get => this._panelFillType.Visible;
    set
    {
      if (this._panelFillType.Visible == value)
        return;
      this._panelHatches.Visible = value;
      this._panelSolidFillButton.Visible = value;
      this._panelFillType.Visible = value;
      this._panelFillSpacer.Visible = value;
      this.RecalcHeight();
    }
  }

  private void AfterNewColor()
  {
    this.RecolorButtons();
    if (this.Color.Equals((object) Color.Transparent) || this.Color.Equals((object) Color.Empty))
    {
      this.SetCheckedButton(this._btnTransparentBrush);
    }
    else
    {
      if (this._checkedButton != this._btnTransparentBrush)
        return;
      this.SetCheckedButton(this._btnSolidBrush);
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Color), "")]
  public Color HatchBackColor
  {
    [DebuggerStepThrough] get => this._hatchBackColor;
    set
    {
      if (!(this._hatchBackColor != value))
        return;
      this._hatchBackColor = value;
      if (this._colorSelectionUserControl.SecondColor != value)
        this._colorSelectionUserControl.SecondColor = value;
      this.RecolorButtons();
    }
  }

  private void RecolorButtons()
  {
    if (this._btnSolidBrush.Color != this._colorSelectionUserControl.Color)
      this._btnSolidBrush.Color = this._colorSelectionUserControl.Color;
    if (this._btnSolidBrush.ForeColor != this._colorSelectionUserControl.Color.InvertAsBlackWhite())
      this._btnSolidBrush.ForeColor = this._colorSelectionUserControl.Color.InvertAsBlackWhite();
    foreach (ColorButton hatchButton in this.GetHatchButtons())
    {
      if (hatchButton.Color != this._colorSelectionUserControl.Color)
        hatchButton.Color = this._colorSelectionUserControl.Color;
      if (hatchButton.HatchBackgroundColor != this._hatchBackColor)
        hatchButton.HatchBackgroundColor = this._hatchBackColor;
      hatchButton.UpdateAllGraphics();
    }
  }

  public event BrushSelectionUserControl.BrushStyleWasSelectedDelegate BrushStyleWasSelected;

  protected virtual void FireBrushStyleWasSelected(BrushStyle brushStyle)
  {
    if (this.BrushStyleWasSelected == null)
      return;
    this.BrushStyleWasSelected(brushStyle);
  }

  private ColorButton GetButtonByBrushStyle(BrushStyle brushStyle)
  {
    switch (brushStyle.FillType)
    {
      case BrushStyle.FillTypeEnum.Solid:
        return this._btnSolidBrush;
      case BrushStyle.FillTypeEnum.Hatch:
        return this.GetHatchButtons().First<ColorButton>((Func<ColorButton, bool>) (button => button.HatchStyle == brushStyle.HatchStyle));
      case BrushStyle.FillTypeEnum.Transparent:
        return this._btnTransparentBrush;
      default:
        throw new Exception("Unknown FillType value");
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public BrushStyle SelectedBrushStyle
  {
    get
    {
      if (!this._wasInit)
        return BrushStyle.Get(this._preSelectedFillType, this._preSelectedHatchStyle);
      return this._checkedButton != null && this._checkedButton.Tag != null && this._checkedButton.Tag is BrushStyle ? (BrushStyle) this._checkedButton.Tag : (BrushStyle) null;
    }
    set
    {
      if (value == null)
        value = BrushStyle.Clear;
      this.SetCheckedButton(this.GetButtonByBrushStyle(value));
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue("")]
  public string OperationName
  {
    [DebuggerStepThrough] get
    {
      return !this._wasInit ? this._preSelectedOperationName : this._colorSelectionUserControl.OperationName;
    }
    set
    {
      if (!(this.OperationName != value))
        return;
      if (this._wasInit)
        this._colorSelectionUserControl.OperationName = value;
      else
        this._preSelectedOperationName = value;
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(BrushStyle.FillTypeEnum.Solid)]
  public BrushStyle.FillTypeEnum FillType
  {
    get => !this._wasInit ? this._preSelectedFillType : this.SelectedBrushStyle.FillType;
    set
    {
      if (!this._wasInit)
        this._preSelectedFillType = value;
      if (this.SelectedBrushStyle.FillType == value)
        return;
      this.SelectedBrushStyle = BrushStyle.Get(value);
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(HatchStyle.Cross)]
  public HatchStyle HatchStyle
  {
    get => !this._wasInit ? this._preSelectedHatchStyle : this.SelectedBrushStyle.HatchStyle;
    set
    {
      if (!this._wasInit)
        this._preSelectedHatchStyle = value;
      if (this.SelectedBrushStyle.FillType != BrushStyle.FillTypeEnum.Hatch || this.SelectedBrushStyle.HatchStyle == value)
        return;
      this.SelectedBrushStyle = BrushStyle.Hatches[value];
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public event BrushSelectionUserControl.ColorWasSelectedDelegate ColorWasSelected;

  protected virtual void FireColorWasSelected(Color color)
  {
    if (this.ColorWasSelected == null)
      return;
    this.ColorWasSelected(color);
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public event BrushSelectionUserControl.ColorWasSelectedDelegate HatchBackColorWasSelected;

  protected virtual void FireHatchBackColorWasSelected(Color color)
  {
    if (this.HatchBackColorWasSelected == null)
      return;
    this.HatchBackColorWasSelected(color);
  }

  private void _colorSelectionUserControl_ColorWasSelected(Color color)
  {
    this.AfterNewColor();
    this.FireColorWasSelected(color);
    this.FillTypeControlsVisible = color != Color.Transparent && color != Color.Empty;
  }

  protected void RecalcHeight()
  {
    int num1 = (this.button3.Width + 4) * 7;
    if (this._panelHatches.Height != num1)
      this._panelHatches.Height = num1;
    int num2 = this._panelTansparentButton.Height + this._colorSelectionUserControl.Height + 3;
    if (this._panelFillType.Visible)
      num2 += this._panelFillType.Height + this._panelFillSpacer.Height + this._panelSolidFillButton.Height + this._panelHatches.Height;
    if (this.Height != num2)
      this.Height = num2;
    if (this.Parent == null || !(this.Parent is PopupDropDown))
      return;
    PopupDropDown parent = (PopupDropDown) this.Parent;
    Size clientSize = parent.ClientSize;
    if (clientSize.Height == this.Height)
      return;
    PopupDropDown popupDropDown = parent;
    clientSize = parent.ClientSize;
    Size size = new Size(clientSize.Width, this.Height);
    popupDropDown.ClientSize = size;
  }

  [DefaultValue(typeof (Color), "White")]
  public override Color BackColor
  {
    get => base.BackColor;
    set
    {
      if (!(base.BackColor != value))
        return;
      base.BackColor = value;
      this._colorSelectionUserControl.BackColor = value;
      this._btnTransparentBrush.Color = value;
      this.RecolorButtons();
    }
  }

  private void _colorSelectionUserControl_SizeChanged(object sender, EventArgs e)
  {
    this.RecalcHeight();
  }

  protected override void OnSizeChanged(EventArgs e) => base.OnSizeChanged(e);

  private void _colorSelectionUserControl_SecondColorWasSelected(Color color)
  {
    if (!(this.HatchBackColor != color))
      return;
    this.HatchBackColor = color;
    this.FireHatchBackColorWasSelected(color);
  }

  public void FocusFirstButton()
  {
    switch (this.FillType)
    {
      case BrushStyle.FillTypeEnum.Solid:
        if (this._colorSelectionUserControl.FocusCheckedButton())
          break;
        this._btnSolidBrush.Focus();
        break;
      case BrushStyle.FillTypeEnum.Hatch:
        ColorButton colorButton = this.GetHatchButtons().FirstOrDefault<ColorButton>((Func<ColorButton, bool>) (btn => btn.ForceDown));
        if (colorButton == null)
          break;
        colorButton.Focus();
        break;
      case BrushStyle.FillTypeEnum.Transparent:
        this._btnTransparentBrush.Focus();
        break;
      default:
        throw new Exception("Unknown FillType");
    }
  }

  private void _btnTransparentBrush_OnNavigateToLeft(
    IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    blockDefaultNavigation = true;
    this.NavigateToLeft();
  }

  private void button51_OnNavigateToDown(
    IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    blockDefaultNavigation = true;
    this.NavigateToDown();
  }

  private void button50_OnNavigateToRight(
    IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    blockDefaultNavigation = true;
    this.NavigateToRight();
  }

  private void _btnTransparentBrush_OnNavigateToUp(
    IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    blockDefaultNavigation = true;
    this.NavigateToUp();
  }

  private void button10_Leave(object sender, EventArgs e)
  {
    this._lastTopMostHatchButton = (ColorButton) sender;
  }

  private void _btnSolidBrush_OnNavigateToDown(
    IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    if (this._lastTopMostHatchButton == null || !this._lastTopMostHatchButton.CanFocus)
      return;
    this._lastTopMostHatchButton.Focus();
    blockDefaultNavigation = true;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this._panelFillType = new Panel();
    this.smoothLabel1 = new SmoothLabel();
    this._panelTansparentButton = new Panel();
    this._btnTransparentBrush = new ColorButton();
    this._colorSelectionUserControl = new ColorSelectionUserControl();
    this._btnSolidBrush = new ColorButton();
    this.button3 = new ColorButton();
    this.button11 = new ColorButton();
    this.button19 = new ColorButton();
    this.button27 = new ColorButton();
    this.button35 = new ColorButton();
    this.button43 = new ColorButton();
    this.button51 = new ColorButton();
    this.button52 = new ColorButton();
    this.button53 = new ColorButton();
    this.button54 = new ColorButton();
    this.button55 = new ColorButton();
    this.button48 = new ColorButton();
    this.button47 = new ColorButton();
    this.button46 = new ColorButton();
    this.button45 = new ColorButton();
    this.button44 = new ColorButton();
    this.button36 = new ColorButton();
    this.button37 = new ColorButton();
    this.button38 = new ColorButton();
    this.button39 = new ColorButton();
    this.button40 = new ColorButton();
    this.button41 = new ColorButton();
    this.button49 = new ColorButton();
    this.button50 = new ColorButton();
    this.button42 = new ColorButton();
    this.button34 = new ColorButton();
    this.button33 = new ColorButton();
    this.button32 = new ColorButton();
    this.button31 = new ColorButton();
    this.button30 = new ColorButton();
    this.button29 = new ColorButton();
    this.button28 = new ColorButton();
    this.button20 = new ColorButton();
    this.button21 = new ColorButton();
    this.button22 = new ColorButton();
    this.button23 = new ColorButton();
    this.button24 = new ColorButton();
    this.button25 = new ColorButton();
    this.button26 = new ColorButton();
    this.button18 = new ColorButton();
    this.button17 = new ColorButton();
    this.button16 = new ColorButton();
    this.button15 = new ColorButton();
    this.button14 = new ColorButton();
    this.button13 = new ColorButton();
    this.button12 = new ColorButton();
    this.button4 = new ColorButton();
    this.button5 = new ColorButton();
    this.button6 = new ColorButton();
    this.button7 = new ColorButton();
    this.button8 = new ColorButton();
    this.button9 = new ColorButton();
    this.button10 = new ColorButton();
    this._panelFillSpacer = new Panel();
    this._panelSolidFillButton = new Panel();
    this._panelHatches = new TableLayoutPanel();
    this.BrushStyleToolTip = new ToolTip(this.components);
    this._panelFillType.SuspendLayout();
    this._panelTansparentButton.SuspendLayout();
    this._panelSolidFillButton.SuspendLayout();
    this._panelHatches.SuspendLayout();
    this.SuspendLayout();
    this._panelFillType.BackColor = Color.FromArgb(238, 238, 238);
    this._panelFillType.Controls.Add((Control) this.smoothLabel1);
    this._panelFillType.Dock = DockStyle.Top;
    this._panelFillType.Location = new Point(0, 260);
    this._panelFillType.Name = "_panelFillType";
    this._panelFillType.Size = new Size(260, 22);
    this._panelFillType.TabIndex = 4;
    this.smoothLabel1.AutoSize = true;
    this.smoothLabel1.Font = new Font("Times New Roman", 11.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.smoothLabel1.ForeColor = Color.FromArgb(120, 120, 120);
    this.smoothLabel1.Location = new Point(3, 2);
    this.smoothLabel1.Name = "smoothLabel1";
    this.smoothLabel1.Size = new Size(64 /*0x40*/, 17);
    this.smoothLabel1.TabIndex = 0;
    this.smoothLabel1.Text = "Заливка";
    this.smoothLabel1.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
    this._panelTansparentButton.Controls.Add((Control) this._btnTransparentBrush);
    this._panelTansparentButton.Dock = DockStyle.Top;
    this._panelTansparentButton.Location = new Point(0, 0);
    this._panelTansparentButton.Name = "_panelTansparentButton";
    this._panelTansparentButton.Padding = new Padding(3, 3, 3, 5);
    this._panelTansparentButton.Size = new Size(260, 38);
    this._panelTansparentButton.TabIndex = 0;
    this._btnTransparentBrush.Color = Color.White;
    this._btnTransparentBrush.Dock = DockStyle.Fill;
    this._btnTransparentBrush.DownControl = (Control) this._colorSelectionUserControl;
    this._btnTransparentBrush.Location = new Point(3, 3);
    this._btnTransparentBrush.Name = "_btnTransparentBrush";
    this._btnTransparentBrush.Size = new Size(254, 30);
    this._btnTransparentBrush.TabIndex = 0;
    this._btnTransparentBrush.Text = "Без заливки";
    this.BrushStyleToolTip.SetToolTip((Control) this._btnTransparentBrush, "Отключить заливку.\r\n\r\nИспользуйте правую клавишу мыши, либо ctrl+пробел\r\nдля выбора прозрачного фона у штриховки.");
    this._btnTransparentBrush.UseVisualStyleBackColor = false;
    this._btnTransparentBrush.OnNavigateToUp += new OnNavigateDelegate(this._btnTransparentBrush_OnNavigateToUp);
    this._btnTransparentBrush.OnNavigateToLeft += new OnNavigateDelegate(this._btnTransparentBrush_OnNavigateToLeft);
    this._btnTransparentBrush.OnNavigateToRight += new OnNavigateDelegate(this.button50_OnNavigateToRight);
    this._colorSelectionUserControl.BackColor = Color.White;
    this._colorSelectionUserControl.Color = Color.Red;
    this._colorSelectionUserControl.Dock = DockStyle.Top;
    this._colorSelectionUserControl.DownControl = (Control) this._btnSolidBrush;
    this._colorSelectionUserControl.Location = new Point(0, 38);
    this._colorSelectionUserControl.Name = "_colorSelectionUserControl";
    this._colorSelectionUserControl.Size = new Size(260, 220);
    this._colorSelectionUserControl.TabIndex = 1;
    this._colorSelectionUserControl.TrackLastFocusedChildControl = true;
    this._colorSelectionUserControl.UpControl = (Control) this._btnTransparentBrush;
    this._colorSelectionUserControl.ColorWasSelected += new ColorSelectionUserControl.ColorWasSelectedDelegate(this._colorSelectionUserControl_ColorWasSelected);
    this._colorSelectionUserControl.SecondColorWasSelected += new ColorSelectionUserControl.ColorWasSelectedDelegate(this._colorSelectionUserControl_SecondColorWasSelected);
    this._colorSelectionUserControl.OnNavigateToLeft += new OnNavigateDelegate(this._btnTransparentBrush_OnNavigateToLeft);
    this._colorSelectionUserControl.OnNavigateToRight += new OnNavigateDelegate(this.button50_OnNavigateToRight);
    this._colorSelectionUserControl.SizeChanged += new EventHandler(this._colorSelectionUserControl_SizeChanged);
    this._btnSolidBrush.Color = Color.Black;
    this._btnSolidBrush.Dock = DockStyle.Fill;
    this._btnSolidBrush.DownControl = (Control) this.button3;
    this._btnSolidBrush.ForceDown = true;
    this._btnSolidBrush.ForeColor = Color.White;
    this._btnSolidBrush.Location = new Point(3, 3);
    this._btnSolidBrush.MouseOverColorInnerDark = Color.FromArgb(224 /*0xE0*/, 224 /*0xE0*/, 224 /*0xE0*/);
    this._btnSolidBrush.MouseOverColorInnerLight = Color.FromArgb(224 /*0xE0*/, 224 /*0xE0*/, 224 /*0xE0*/);
    this._btnSolidBrush.MouseOverColorOuter = Color.Gray;
    this._btnSolidBrush.Name = "_btnSolidBrush";
    this._btnSolidBrush.Size = new Size(254, 30);
    this._btnSolidBrush.TabIndex = 0;
    this._btnSolidBrush.Text = "Сплошная заливка";
    this.BrushStyleToolTip.SetToolTip((Control) this._btnSolidBrush, "Использовать сплошную заливку выбранным цветом");
    this._btnSolidBrush.UpControl = (Control) this._colorSelectionUserControl;
    this._btnSolidBrush.UseVisualStyleBackColor = false;
    this._btnSolidBrush.OnNavigateToDown += new OnNavigateDelegate(this._btnSolidBrush_OnNavigateToDown);
    this._btnSolidBrush.OnNavigateToLeft += new OnNavigateDelegate(this._btnTransparentBrush_OnNavigateToLeft);
    this._btnSolidBrush.OnNavigateToRight += new OnNavigateDelegate(this.button50_OnNavigateToRight);
    this.button3.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button3.Color = Color.Black;
    this.button3.Dock = DockStyle.Fill;
    this.button3.DownControl = (Control) this.button11;
    this.button3.HatchBackgroundColor = Color.White;
    this.button3.HatchStyle = HatchStyle.Horizontal;
    this.button3.Location = new Point(2, 2);
    this.button3.Margin = new Padding(2);
    this.button3.Name = "button3";
    this.button3.RightControl = (Control) this.button4;
    this.button3.Size = new Size(28, 28);
    this.button3.TabIndex = 0;
    this.button3.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button3, "Штриховка");
    this.button3.UpControl = (Control) this._btnSolidBrush;
    this.button3.UseVisualStyleBackColor = false;
    this.button3.OnNavigateToLeft += new OnNavigateDelegate(this._btnTransparentBrush_OnNavigateToLeft);
    this.button3.Leave += new EventHandler(this.button10_Leave);
    this.button11.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button11.Color = Color.Black;
    this.button11.Dock = DockStyle.Fill;
    this.button11.DownControl = (Control) this.button19;
    this.button11.HatchBackgroundColor = Color.White;
    this.button11.HatchStyle = HatchStyle.Percent20;
    this.button11.Location = new Point(2, 34);
    this.button11.Margin = new Padding(2);
    this.button11.Name = "button11";
    this.button11.RightControl = (Control) this.button12;
    this.button11.Size = new Size(28, 28);
    this.button11.TabIndex = 8;
    this.button11.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button11, "Штриховка");
    this.button11.UpControl = (Control) this.button3;
    this.button11.UseVisualStyleBackColor = false;
    this.button11.OnNavigateToLeft += new OnNavigateDelegate(this._btnTransparentBrush_OnNavigateToLeft);
    this.button19.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button19.Color = Color.Black;
    this.button19.Dock = DockStyle.Fill;
    this.button19.DownControl = (Control) this.button27;
    this.button19.HatchBackgroundColor = Color.White;
    this.button19.HatchStyle = HatchStyle.Percent80;
    this.button19.Location = new Point(2, 66);
    this.button19.Margin = new Padding(2);
    this.button19.Name = "button19";
    this.button19.RightControl = (Control) this.button20;
    this.button19.Size = new Size(28, 28);
    this.button19.TabIndex = 16 /*0x10*/;
    this.button19.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button19, "Штриховка");
    this.button19.UpControl = (Control) this.button11;
    this.button19.UseVisualStyleBackColor = false;
    this.button19.OnNavigateToLeft += new OnNavigateDelegate(this._btnTransparentBrush_OnNavigateToLeft);
    this.button27.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button27.Color = Color.Black;
    this.button27.Dock = DockStyle.Fill;
    this.button27.DownControl = (Control) this.button35;
    this.button27.HatchBackgroundColor = Color.White;
    this.button27.HatchStyle = HatchStyle.LightVertical;
    this.button27.Location = new Point(2, 98);
    this.button27.Margin = new Padding(2);
    this.button27.Name = "button27";
    this.button27.RightControl = (Control) this.button28;
    this.button27.Size = new Size(28, 28);
    this.button27.TabIndex = 24;
    this.button27.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button27, "Штриховка");
    this.button27.UpControl = (Control) this.button19;
    this.button27.UseVisualStyleBackColor = false;
    this.button27.OnNavigateToLeft += new OnNavigateDelegate(this._btnTransparentBrush_OnNavigateToLeft);
    this.button35.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button35.Color = Color.Black;
    this.button35.Dock = DockStyle.Fill;
    this.button35.DownControl = (Control) this.button43;
    this.button35.HatchBackgroundColor = Color.White;
    this.button35.HatchStyle = HatchStyle.DashedHorizontal;
    this.button35.Location = new Point(2, 130);
    this.button35.Margin = new Padding(2);
    this.button35.Name = "button35";
    this.button35.RightControl = (Control) this.button36;
    this.button35.Size = new Size(28, 28);
    this.button35.TabIndex = 32 /*0x20*/;
    this.button35.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button35, "Штриховка");
    this.button35.UpControl = (Control) this.button27;
    this.button35.UseVisualStyleBackColor = false;
    this.button35.OnNavigateToLeft += new OnNavigateDelegate(this._btnTransparentBrush_OnNavigateToLeft);
    this.button43.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button43.Color = Color.Black;
    this.button43.Dock = DockStyle.Fill;
    this.button43.DownControl = (Control) this.button51;
    this.button43.HatchBackgroundColor = Color.White;
    this.button43.HatchStyle = HatchStyle.Weave;
    this.button43.Location = new Point(2, 162);
    this.button43.Margin = new Padding(2);
    this.button43.Name = "button43";
    this.button43.RightControl = (Control) this.button44;
    this.button43.Size = new Size(28, 28);
    this.button43.TabIndex = 40;
    this.button43.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button43, "Штриховка");
    this.button43.UpControl = (Control) this.button35;
    this.button43.UseVisualStyleBackColor = false;
    this.button43.OnNavigateToLeft += new OnNavigateDelegate(this._btnTransparentBrush_OnNavigateToLeft);
    this.button51.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button51.Color = Color.Black;
    this.button51.Dock = DockStyle.Fill;
    this.button51.HatchBackgroundColor = Color.White;
    this.button51.HatchStyle = HatchStyle.SmallGrid;
    this.button51.Location = new Point(2, 194);
    this.button51.Margin = new Padding(2);
    this.button51.Name = "button51";
    this.button51.RightControl = (Control) this.button52;
    this.button51.Size = new Size(28, 31 /*0x1F*/);
    this.button51.TabIndex = 48 /*0x30*/;
    this.button51.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button51, "Штриховка");
    this.button51.UpControl = (Control) this.button43;
    this.button51.UseVisualStyleBackColor = false;
    this.button51.OnNavigateToDown += new OnNavigateDelegate(this.button51_OnNavigateToDown);
    this.button51.OnNavigateToLeft += new OnNavigateDelegate(this._btnTransparentBrush_OnNavigateToLeft);
    this.button52.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button52.Color = Color.Black;
    this.button52.Dock = DockStyle.Fill;
    this.button52.HatchBackgroundColor = Color.White;
    this.button52.HatchStyle = HatchStyle.SmallCheckerBoard;
    this.button52.LeftControl = (Control) this.button51;
    this.button52.Location = new Point(34, 194);
    this.button52.Margin = new Padding(2);
    this.button52.Name = "button52";
    this.button52.RightControl = (Control) this.button53;
    this.button52.Size = new Size(28, 31 /*0x1F*/);
    this.button52.TabIndex = 49;
    this.button52.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button52, "Штриховка");
    this.button52.UpControl = (Control) this.button44;
    this.button52.UseVisualStyleBackColor = false;
    this.button52.OnNavigateToDown += new OnNavigateDelegate(this.button51_OnNavigateToDown);
    this.button53.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button53.Color = Color.Black;
    this.button53.Dock = DockStyle.Fill;
    this.button53.HatchBackgroundColor = Color.White;
    this.button53.HatchStyle = HatchStyle.LargeCheckerBoard;
    this.button53.LeftControl = (Control) this.button52;
    this.button53.Location = new Point(66, 194);
    this.button53.Margin = new Padding(2);
    this.button53.Name = "button53";
    this.button53.RightControl = (Control) this.button54;
    this.button53.Size = new Size(28, 31 /*0x1F*/);
    this.button53.TabIndex = 50;
    this.button53.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button53, "Штриховка");
    this.button53.UpControl = (Control) this.button45;
    this.button53.UseVisualStyleBackColor = false;
    this.button53.OnNavigateToDown += new OnNavigateDelegate(this.button51_OnNavigateToDown);
    this.button54.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button54.Color = Color.Black;
    this.button54.Dock = DockStyle.Fill;
    this.button54.HatchBackgroundColor = Color.White;
    this.button54.HatchStyle = HatchStyle.OutlinedDiamond;
    this.button54.LeftControl = (Control) this.button53;
    this.button54.Location = new Point(98, 194);
    this.button54.Margin = new Padding(2);
    this.button54.Name = "button54";
    this.button54.RightControl = (Control) this.button55;
    this.button54.Size = new Size(28, 31 /*0x1F*/);
    this.button54.TabIndex = 51;
    this.button54.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button54, "Штриховка");
    this.button54.UpControl = (Control) this.button46;
    this.button54.UseVisualStyleBackColor = false;
    this.button54.OnNavigateToDown += new OnNavigateDelegate(this.button51_OnNavigateToDown);
    this.button55.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button55.Color = Color.Black;
    this.button55.Dock = DockStyle.Fill;
    this.button55.HatchBackgroundColor = Color.White;
    this.button55.HatchStyle = HatchStyle.SolidDiamond;
    this.button55.LeftControl = (Control) this.button54;
    this.button55.Location = new Point(130, 194);
    this.button55.Margin = new Padding(2);
    this.button55.Name = "button55";
    this.button55.RightControl = (Control) this.button48;
    this.button55.Size = new Size(28, 31 /*0x1F*/);
    this.button55.TabIndex = 52;
    this.button55.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button55, "Штриховка");
    this.button55.UpControl = (Control) this.button47;
    this.button55.UseVisualStyleBackColor = false;
    this.button55.OnNavigateToDown += new OnNavigateDelegate(this.button51_OnNavigateToDown);
    this.button48.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button48.Color = Color.Black;
    this.button48.Dock = DockStyle.Fill;
    this.button48.HatchBackgroundColor = Color.White;
    this.button48.HatchStyle = HatchStyle.Shingle;
    this.button48.LeftControl = (Control) this.button47;
    this.button48.Location = new Point(162, 162);
    this.button48.Margin = new Padding(2);
    this.button48.Name = "button48";
    this.button48.RightControl = (Control) this.button49;
    this.button48.Size = new Size(28, 28);
    this.button48.TabIndex = 45;
    this.button48.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button48, "Штриховка");
    this.button48.UpControl = (Control) this.button40;
    this.button48.UseVisualStyleBackColor = false;
    this.button48.OnNavigateToDown += new OnNavigateDelegate(this.button51_OnNavigateToDown);
    this.button47.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button47.Color = Color.Black;
    this.button47.Dock = DockStyle.Fill;
    this.button47.DownControl = (Control) this.button55;
    this.button47.HatchBackgroundColor = Color.White;
    this.button47.HatchStyle = HatchStyle.DottedDiamond;
    this.button47.LeftControl = (Control) this.button46;
    this.button47.Location = new Point(130, 162);
    this.button47.Margin = new Padding(2);
    this.button47.Name = "button47";
    this.button47.RightControl = (Control) this.button48;
    this.button47.Size = new Size(28, 28);
    this.button47.TabIndex = 44;
    this.button47.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button47, "Штриховка");
    this.button47.UpControl = (Control) this.button39;
    this.button47.UseVisualStyleBackColor = false;
    this.button46.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button46.Color = Color.Black;
    this.button46.Dock = DockStyle.Fill;
    this.button46.DownControl = (Control) this.button54;
    this.button46.HatchBackgroundColor = Color.White;
    this.button46.HatchStyle = HatchStyle.DottedGrid;
    this.button46.LeftControl = (Control) this.button45;
    this.button46.Location = new Point(98, 162);
    this.button46.Margin = new Padding(2);
    this.button46.Name = "button46";
    this.button46.RightControl = (Control) this.button47;
    this.button46.Size = new Size(28, 28);
    this.button46.TabIndex = 43;
    this.button46.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button46, "Штриховка");
    this.button46.UpControl = (Control) this.button38;
    this.button46.UseVisualStyleBackColor = false;
    this.button45.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button45.Color = Color.Black;
    this.button45.Dock = DockStyle.Fill;
    this.button45.DownControl = (Control) this.button53;
    this.button45.HatchBackgroundColor = Color.White;
    this.button45.HatchStyle = HatchStyle.Divot;
    this.button45.LeftControl = (Control) this.button44;
    this.button45.Location = new Point(66, 162);
    this.button45.Margin = new Padding(2);
    this.button45.Name = "button45";
    this.button45.RightControl = (Control) this.button46;
    this.button45.Size = new Size(28, 28);
    this.button45.TabIndex = 42;
    this.button45.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button45, "Штриховка");
    this.button45.UpControl = (Control) this.button37;
    this.button45.UseVisualStyleBackColor = false;
    this.button44.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button44.Color = Color.Black;
    this.button44.Dock = DockStyle.Fill;
    this.button44.DownControl = (Control) this.button52;
    this.button44.HatchBackgroundColor = Color.White;
    this.button44.HatchStyle = HatchStyle.Plaid;
    this.button44.LeftControl = (Control) this.button43;
    this.button44.Location = new Point(34, 162);
    this.button44.Margin = new Padding(2);
    this.button44.Name = "button44";
    this.button44.RightControl = (Control) this.button45;
    this.button44.Size = new Size(28, 28);
    this.button44.TabIndex = 41;
    this.button44.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button44, "Штриховка");
    this.button44.UpControl = (Control) this.button36;
    this.button44.UseVisualStyleBackColor = false;
    this.button36.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button36.Color = Color.Black;
    this.button36.Dock = DockStyle.Fill;
    this.button36.DownControl = (Control) this.button44;
    this.button36.HatchBackgroundColor = Color.White;
    this.button36.HatchStyle = HatchStyle.DashedVertical;
    this.button36.LeftControl = (Control) this.button35;
    this.button36.Location = new Point(34, 130);
    this.button36.Margin = new Padding(2);
    this.button36.Name = "button36";
    this.button36.RightControl = (Control) this.button37;
    this.button36.Size = new Size(28, 28);
    this.button36.TabIndex = 33;
    this.button36.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button36, "Штриховка");
    this.button36.UpControl = (Control) this.button28;
    this.button36.UseVisualStyleBackColor = false;
    this.button37.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button37.Color = Color.Black;
    this.button37.Dock = DockStyle.Fill;
    this.button37.DownControl = (Control) this.button45;
    this.button37.HatchBackgroundColor = Color.White;
    this.button37.HatchStyle = HatchStyle.SmallConfetti;
    this.button37.LeftControl = (Control) this.button36;
    this.button37.Location = new Point(66, 130);
    this.button37.Margin = new Padding(2);
    this.button37.Name = "button37";
    this.button37.RightControl = (Control) this.button38;
    this.button37.Size = new Size(28, 28);
    this.button37.TabIndex = 34;
    this.button37.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button37, "Штриховка");
    this.button37.UpControl = (Control) this.button29;
    this.button37.UseVisualStyleBackColor = false;
    this.button38.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button38.Color = Color.Black;
    this.button38.Dock = DockStyle.Fill;
    this.button38.DownControl = (Control) this.button46;
    this.button38.HatchBackgroundColor = Color.White;
    this.button38.HatchStyle = HatchStyle.LargeConfetti;
    this.button38.LeftControl = (Control) this.button37;
    this.button38.Location = new Point(98, 130);
    this.button38.Margin = new Padding(2);
    this.button38.Name = "button38";
    this.button38.RightControl = (Control) this.button39;
    this.button38.Size = new Size(28, 28);
    this.button38.TabIndex = 35;
    this.button38.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button38, "Штриховка");
    this.button38.UpControl = (Control) this.button30;
    this.button38.UseVisualStyleBackColor = false;
    this.button39.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button39.Color = Color.Black;
    this.button39.Dock = DockStyle.Fill;
    this.button39.DownControl = (Control) this.button47;
    this.button39.HatchBackgroundColor = Color.White;
    this.button39.HatchStyle = HatchStyle.ZigZag;
    this.button39.LeftControl = (Control) this.button38;
    this.button39.Location = new Point(130, 130);
    this.button39.Margin = new Padding(2);
    this.button39.Name = "button39";
    this.button39.RightControl = (Control) this.button40;
    this.button39.Size = new Size(28, 28);
    this.button39.TabIndex = 36;
    this.button39.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button39, "Штриховка");
    this.button39.UpControl = (Control) this.button31;
    this.button39.UseVisualStyleBackColor = false;
    this.button40.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button40.Color = Color.Black;
    this.button40.Dock = DockStyle.Fill;
    this.button40.DownControl = (Control) this.button48;
    this.button40.HatchBackgroundColor = Color.White;
    this.button40.HatchStyle = HatchStyle.Wave;
    this.button40.LeftControl = (Control) this.button39;
    this.button40.Location = new Point(162, 130);
    this.button40.Margin = new Padding(2);
    this.button40.Name = "button40";
    this.button40.RightControl = (Control) this.button41;
    this.button40.Size = new Size(28, 28);
    this.button40.TabIndex = 37;
    this.button40.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button40, "Штриховка");
    this.button40.UpControl = (Control) this.button32;
    this.button40.UseVisualStyleBackColor = false;
    this.button41.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button41.Color = Color.Black;
    this.button41.Dock = DockStyle.Fill;
    this.button41.DownControl = (Control) this.button49;
    this.button41.HatchBackgroundColor = Color.White;
    this.button41.HatchStyle = HatchStyle.DiagonalBrick;
    this.button41.LeftControl = (Control) this.button40;
    this.button41.Location = new Point(194, 130);
    this.button41.Margin = new Padding(2);
    this.button41.Name = "button41";
    this.button41.RightControl = (Control) this.button42;
    this.button41.Size = new Size(28, 28);
    this.button41.TabIndex = 38;
    this.button41.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button41, "Штриховка");
    this.button41.UpControl = (Control) this.button33;
    this.button41.UseVisualStyleBackColor = false;
    this.button49.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button49.Color = Color.Black;
    this.button49.Dock = DockStyle.Fill;
    this.button49.HatchBackgroundColor = Color.White;
    this.button49.HatchStyle = HatchStyle.Trellis;
    this.button49.LeftControl = (Control) this.button48;
    this.button49.Location = new Point(194, 162);
    this.button49.Margin = new Padding(2);
    this.button49.Name = "button49";
    this.button49.RightControl = (Control) this.button50;
    this.button49.Size = new Size(28, 28);
    this.button49.TabIndex = 46;
    this.button49.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button49, "Штриховка");
    this.button49.UpControl = (Control) this.button41;
    this.button49.UseVisualStyleBackColor = false;
    this.button49.OnNavigateToDown += new OnNavigateDelegate(this.button51_OnNavigateToDown);
    this.button50.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button50.Color = Color.Black;
    this.button50.Dock = DockStyle.Fill;
    this.button50.HatchBackgroundColor = Color.White;
    this.button50.HatchStyle = HatchStyle.Sphere;
    this.button50.LeftControl = (Control) this.button49;
    this.button50.Location = new Point(226, 162);
    this.button50.Margin = new Padding(2);
    this.button50.Name = "button50";
    this.button50.Size = new Size(32 /*0x20*/, 28);
    this.button50.TabIndex = 47;
    this.button50.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button50, "Штриховка");
    this.button50.UpControl = (Control) this.button42;
    this.button50.UseVisualStyleBackColor = false;
    this.button50.OnNavigateToDown += new OnNavigateDelegate(this.button51_OnNavigateToDown);
    this.button50.OnNavigateToRight += new OnNavigateDelegate(this.button50_OnNavigateToRight);
    this.button42.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button42.Color = Color.Black;
    this.button42.Dock = DockStyle.Fill;
    this.button42.DownControl = (Control) this.button50;
    this.button42.HatchBackgroundColor = Color.White;
    this.button42.HatchStyle = HatchStyle.HorizontalBrick;
    this.button42.LeftControl = (Control) this.button41;
    this.button42.Location = new Point(226, 130);
    this.button42.Margin = new Padding(2);
    this.button42.Name = "button42";
    this.button42.Size = new Size(32 /*0x20*/, 28);
    this.button42.TabIndex = 39;
    this.button42.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button42, "Штриховка");
    this.button42.UpControl = (Control) this.button34;
    this.button42.UseVisualStyleBackColor = false;
    this.button42.OnNavigateToRight += new OnNavigateDelegate(this.button50_OnNavigateToRight);
    this.button34.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button34.Color = Color.Black;
    this.button34.Dock = DockStyle.Fill;
    this.button34.DownControl = (Control) this.button42;
    this.button34.HatchBackgroundColor = Color.White;
    this.button34.HatchStyle = HatchStyle.DashedUpwardDiagonal;
    this.button34.LeftControl = (Control) this.button33;
    this.button34.Location = new Point(226, 98);
    this.button34.Margin = new Padding(2);
    this.button34.Name = "button34";
    this.button34.Size = new Size(32 /*0x20*/, 28);
    this.button34.TabIndex = 31 /*0x1F*/;
    this.button34.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button34, "Штриховка");
    this.button34.UpControl = (Control) this.button26;
    this.button34.UseVisualStyleBackColor = false;
    this.button34.OnNavigateToRight += new OnNavigateDelegate(this.button50_OnNavigateToRight);
    this.button33.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button33.Color = Color.Black;
    this.button33.Dock = DockStyle.Fill;
    this.button33.DownControl = (Control) this.button41;
    this.button33.HatchBackgroundColor = Color.White;
    this.button33.HatchStyle = HatchStyle.DashedDownwardDiagonal;
    this.button33.LeftControl = (Control) this.button32;
    this.button33.Location = new Point(194, 98);
    this.button33.Margin = new Padding(2);
    this.button33.Name = "button33";
    this.button33.RightControl = (Control) this.button34;
    this.button33.Size = new Size(28, 28);
    this.button33.TabIndex = 30;
    this.button33.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button33, "Штриховка");
    this.button33.UpControl = (Control) this.button25;
    this.button33.UseVisualStyleBackColor = false;
    this.button32.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button32.Color = Color.Black;
    this.button32.Dock = DockStyle.Fill;
    this.button32.DownControl = (Control) this.button40;
    this.button32.HatchBackgroundColor = Color.White;
    this.button32.HatchStyle = HatchStyle.DarkHorizontal;
    this.button32.LeftControl = (Control) this.button31;
    this.button32.Location = new Point(162, 98);
    this.button32.Margin = new Padding(2);
    this.button32.Name = "button32";
    this.button32.RightControl = (Control) this.button33;
    this.button32.Size = new Size(28, 28);
    this.button32.TabIndex = 29;
    this.button32.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button32, "Штриховка");
    this.button32.UpControl = (Control) this.button24;
    this.button32.UseVisualStyleBackColor = false;
    this.button31.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button31.Color = Color.Black;
    this.button31.Dock = DockStyle.Fill;
    this.button31.DownControl = (Control) this.button39;
    this.button31.HatchBackgroundColor = Color.White;
    this.button31.HatchStyle = HatchStyle.DarkVertical;
    this.button31.LeftControl = (Control) this.button30;
    this.button31.Location = new Point(130, 98);
    this.button31.Margin = new Padding(2);
    this.button31.Name = "button31";
    this.button31.RightControl = (Control) this.button32;
    this.button31.Size = new Size(28, 28);
    this.button31.TabIndex = 28;
    this.button31.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button31, "Штриховка");
    this.button31.UpControl = (Control) this.button23;
    this.button31.UseVisualStyleBackColor = false;
    this.button30.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button30.Color = Color.Black;
    this.button30.Dock = DockStyle.Fill;
    this.button30.DownControl = (Control) this.button38;
    this.button30.HatchBackgroundColor = Color.White;
    this.button30.HatchStyle = HatchStyle.NarrowHorizontal;
    this.button30.LeftControl = (Control) this.button29;
    this.button30.Location = new Point(98, 98);
    this.button30.Margin = new Padding(2);
    this.button30.Name = "button30";
    this.button30.RightControl = (Control) this.button31;
    this.button30.Size = new Size(28, 28);
    this.button30.TabIndex = 27;
    this.button30.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button30, "Штриховка");
    this.button30.UpControl = (Control) this.button22;
    this.button30.UseVisualStyleBackColor = false;
    this.button29.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button29.Color = Color.Black;
    this.button29.Dock = DockStyle.Fill;
    this.button29.DownControl = (Control) this.button37;
    this.button29.HatchBackgroundColor = Color.White;
    this.button29.HatchStyle = HatchStyle.NarrowVertical;
    this.button29.LeftControl = (Control) this.button28;
    this.button29.Location = new Point(66, 98);
    this.button29.Margin = new Padding(2);
    this.button29.Name = "button29";
    this.button29.RightControl = (Control) this.button30;
    this.button29.Size = new Size(28, 28);
    this.button29.TabIndex = 26;
    this.button29.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button29, "Штриховка");
    this.button29.UpControl = (Control) this.button21;
    this.button29.UseVisualStyleBackColor = false;
    this.button28.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button28.Color = Color.Black;
    this.button28.Dock = DockStyle.Fill;
    this.button28.DownControl = (Control) this.button36;
    this.button28.HatchBackgroundColor = Color.White;
    this.button28.HatchStyle = HatchStyle.LightHorizontal;
    this.button28.LeftControl = (Control) this.button27;
    this.button28.Location = new Point(34, 98);
    this.button28.Margin = new Padding(2);
    this.button28.Name = "button28";
    this.button28.RightControl = (Control) this.button29;
    this.button28.Size = new Size(28, 28);
    this.button28.TabIndex = 25;
    this.button28.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button28, "Штриховка");
    this.button28.UpControl = (Control) this.button20;
    this.button28.UseVisualStyleBackColor = false;
    this.button20.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button20.Color = Color.Black;
    this.button20.Dock = DockStyle.Fill;
    this.button20.DownControl = (Control) this.button28;
    this.button20.HatchBackgroundColor = Color.White;
    this.button20.HatchStyle = HatchStyle.Percent90;
    this.button20.LeftControl = (Control) this.button19;
    this.button20.Location = new Point(34, 66);
    this.button20.Margin = new Padding(2);
    this.button20.Name = "button20";
    this.button20.RightControl = (Control) this.button21;
    this.button20.Size = new Size(28, 28);
    this.button20.TabIndex = 17;
    this.button20.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button20, "Штриховка");
    this.button20.UpControl = (Control) this.button12;
    this.button20.UseVisualStyleBackColor = false;
    this.button21.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button21.Color = Color.Black;
    this.button21.Dock = DockStyle.Fill;
    this.button21.DownControl = (Control) this.button29;
    this.button21.HatchBackgroundColor = Color.White;
    this.button21.HatchStyle = HatchStyle.LightDownwardDiagonal;
    this.button21.LeftControl = (Control) this.button20;
    this.button21.Location = new Point(66, 66);
    this.button21.Margin = new Padding(2);
    this.button21.Name = "button21";
    this.button21.RightControl = (Control) this.button22;
    this.button21.Size = new Size(28, 28);
    this.button21.TabIndex = 18;
    this.button21.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button21, "Штриховка");
    this.button21.UpControl = (Control) this.button13;
    this.button21.UseVisualStyleBackColor = false;
    this.button22.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button22.Color = Color.Black;
    this.button22.Dock = DockStyle.Fill;
    this.button22.DownControl = (Control) this.button30;
    this.button22.HatchBackgroundColor = Color.White;
    this.button22.HatchStyle = HatchStyle.LightUpwardDiagonal;
    this.button22.LeftControl = (Control) this.button21;
    this.button22.Location = new Point(98, 66);
    this.button22.Margin = new Padding(2);
    this.button22.Name = "button22";
    this.button22.RightControl = (Control) this.button23;
    this.button22.Size = new Size(28, 28);
    this.button22.TabIndex = 19;
    this.button22.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button22, "Штриховка");
    this.button22.UpControl = (Control) this.button14;
    this.button22.UseVisualStyleBackColor = false;
    this.button23.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button23.Color = Color.Black;
    this.button23.Dock = DockStyle.Fill;
    this.button23.DownControl = (Control) this.button31;
    this.button23.HatchBackgroundColor = Color.White;
    this.button23.HatchStyle = HatchStyle.DarkDownwardDiagonal;
    this.button23.LeftControl = (Control) this.button22;
    this.button23.Location = new Point(130, 66);
    this.button23.Margin = new Padding(2);
    this.button23.Name = "button23";
    this.button23.RightControl = (Control) this.button24;
    this.button23.Size = new Size(28, 28);
    this.button23.TabIndex = 20;
    this.button23.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button23, "Штриховка");
    this.button23.UpControl = (Control) this.button15;
    this.button23.UseVisualStyleBackColor = false;
    this.button24.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button24.Color = Color.Black;
    this.button24.Dock = DockStyle.Fill;
    this.button24.DownControl = (Control) this.button32;
    this.button24.HatchBackgroundColor = Color.White;
    this.button24.HatchStyle = HatchStyle.DarkUpwardDiagonal;
    this.button24.LeftControl = (Control) this.button23;
    this.button24.Location = new Point(162, 66);
    this.button24.Margin = new Padding(2);
    this.button24.Name = "button24";
    this.button24.RightControl = (Control) this.button25;
    this.button24.Size = new Size(28, 28);
    this.button24.TabIndex = 21;
    this.button24.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button24, "Штриховка");
    this.button24.UpControl = (Control) this.button16;
    this.button24.UseVisualStyleBackColor = false;
    this.button25.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button25.Color = Color.Black;
    this.button25.Dock = DockStyle.Fill;
    this.button25.DownControl = (Control) this.button33;
    this.button25.HatchBackgroundColor = Color.White;
    this.button25.HatchStyle = HatchStyle.WideDownwardDiagonal;
    this.button25.LeftControl = (Control) this.button24;
    this.button25.Location = new Point(194, 66);
    this.button25.Margin = new Padding(2);
    this.button25.Name = "button25";
    this.button25.RightControl = (Control) this.button26;
    this.button25.Size = new Size(28, 28);
    this.button25.TabIndex = 22;
    this.button25.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button25, "Штриховка");
    this.button25.UpControl = (Control) this.button17;
    this.button25.UseVisualStyleBackColor = false;
    this.button26.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button26.Color = Color.Black;
    this.button26.Dock = DockStyle.Fill;
    this.button26.DownControl = (Control) this.button34;
    this.button26.HatchBackgroundColor = Color.White;
    this.button26.HatchStyle = HatchStyle.WideUpwardDiagonal;
    this.button26.LeftControl = (Control) this.button25;
    this.button26.Location = new Point(226, 66);
    this.button26.Margin = new Padding(2);
    this.button26.Name = "button26";
    this.button26.Size = new Size(32 /*0x20*/, 28);
    this.button26.TabIndex = 23;
    this.button26.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button26, "Штриховка");
    this.button26.UpControl = (Control) this.button18;
    this.button26.UseVisualStyleBackColor = false;
    this.button26.OnNavigateToRight += new OnNavigateDelegate(this.button50_OnNavigateToRight);
    this.button18.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button18.Color = Color.Black;
    this.button18.Dock = DockStyle.Fill;
    this.button18.DownControl = (Control) this.button26;
    this.button18.HatchBackgroundColor = Color.White;
    this.button18.HatchStyle = HatchStyle.Percent75;
    this.button18.LeftControl = (Control) this.button17;
    this.button18.Location = new Point(226, 34);
    this.button18.Margin = new Padding(2);
    this.button18.Name = "button18";
    this.button18.Size = new Size(32 /*0x20*/, 28);
    this.button18.TabIndex = 15;
    this.button18.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button18, "Штриховка");
    this.button18.UpControl = (Control) this.button10;
    this.button18.UseVisualStyleBackColor = false;
    this.button18.OnNavigateToRight += new OnNavigateDelegate(this.button50_OnNavigateToRight);
    this.button17.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button17.Color = Color.Black;
    this.button17.Dock = DockStyle.Fill;
    this.button17.DownControl = (Control) this.button25;
    this.button17.HatchBackgroundColor = Color.White;
    this.button17.HatchStyle = HatchStyle.Percent70;
    this.button17.LeftControl = (Control) this.button16;
    this.button17.Location = new Point(194, 34);
    this.button17.Margin = new Padding(2);
    this.button17.Name = "button17";
    this.button17.RightControl = (Control) this.button18;
    this.button17.Size = new Size(28, 28);
    this.button17.TabIndex = 14;
    this.button17.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button17, "Штриховка");
    this.button17.UpControl = (Control) this.button9;
    this.button17.UseVisualStyleBackColor = false;
    this.button16.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button16.Color = Color.Black;
    this.button16.Dock = DockStyle.Fill;
    this.button16.DownControl = (Control) this.button24;
    this.button16.HatchBackgroundColor = Color.White;
    this.button16.HatchStyle = HatchStyle.Percent60;
    this.button16.LeftControl = (Control) this.button15;
    this.button16.Location = new Point(162, 34);
    this.button16.Margin = new Padding(2);
    this.button16.Name = "button16";
    this.button16.RightControl = (Control) this.button17;
    this.button16.Size = new Size(28, 28);
    this.button16.TabIndex = 13;
    this.button16.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button16, "Штриховка");
    this.button16.UpControl = (Control) this.button8;
    this.button16.UseVisualStyleBackColor = false;
    this.button15.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button15.Color = Color.Black;
    this.button15.Dock = DockStyle.Fill;
    this.button15.DownControl = (Control) this.button23;
    this.button15.HatchBackgroundColor = Color.White;
    this.button15.HatchStyle = HatchStyle.Percent50;
    this.button15.LeftControl = (Control) this.button14;
    this.button15.Location = new Point(130, 34);
    this.button15.Margin = new Padding(2);
    this.button15.Name = "button15";
    this.button15.RightControl = (Control) this.button16;
    this.button15.Size = new Size(28, 28);
    this.button15.TabIndex = 12;
    this.button15.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button15, "Штриховка");
    this.button15.UpControl = (Control) this.button7;
    this.button15.UseVisualStyleBackColor = false;
    this.button14.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button14.Color = Color.Black;
    this.button14.Dock = DockStyle.Fill;
    this.button14.DownControl = (Control) this.button22;
    this.button14.HatchBackgroundColor = Color.White;
    this.button14.HatchStyle = HatchStyle.Percent40;
    this.button14.LeftControl = (Control) this.button13;
    this.button14.Location = new Point(98, 34);
    this.button14.Margin = new Padding(2);
    this.button14.Name = "button14";
    this.button14.RightControl = (Control) this.button15;
    this.button14.Size = new Size(28, 28);
    this.button14.TabIndex = 11;
    this.button14.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button14, "Штриховка");
    this.button14.UpControl = (Control) this.button6;
    this.button14.UseVisualStyleBackColor = false;
    this.button13.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button13.Color = Color.Black;
    this.button13.Dock = DockStyle.Fill;
    this.button13.DownControl = (Control) this.button21;
    this.button13.HatchBackgroundColor = Color.White;
    this.button13.HatchStyle = HatchStyle.Percent30;
    this.button13.LeftControl = (Control) this.button12;
    this.button13.Location = new Point(66, 34);
    this.button13.Margin = new Padding(2);
    this.button13.Name = "button13";
    this.button13.RightControl = (Control) this.button14;
    this.button13.Size = new Size(28, 28);
    this.button13.TabIndex = 10;
    this.button13.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button13, "Штриховка");
    this.button13.UpControl = (Control) this.button5;
    this.button13.UseVisualStyleBackColor = false;
    this.button12.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button12.Color = Color.Black;
    this.button12.Dock = DockStyle.Fill;
    this.button12.DownControl = (Control) this.button20;
    this.button12.HatchBackgroundColor = Color.White;
    this.button12.HatchStyle = HatchStyle.Percent25;
    this.button12.LeftControl = (Control) this.button11;
    this.button12.Location = new Point(34, 34);
    this.button12.Margin = new Padding(2);
    this.button12.Name = "button12";
    this.button12.RightControl = (Control) this.button13;
    this.button12.Size = new Size(28, 28);
    this.button12.TabIndex = 9;
    this.button12.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button12, "Штриховка");
    this.button12.UpControl = (Control) this.button4;
    this.button12.UseVisualStyleBackColor = false;
    this.button4.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button4.Color = Color.Black;
    this.button4.Dock = DockStyle.Fill;
    this.button4.DownControl = (Control) this.button12;
    this.button4.HatchBackgroundColor = Color.White;
    this.button4.HatchStyle = HatchStyle.Vertical;
    this.button4.LeftControl = (Control) this.button3;
    this.button4.Location = new Point(34, 2);
    this.button4.Margin = new Padding(2);
    this.button4.Name = "button4";
    this.button4.RightControl = (Control) this.button5;
    this.button4.Size = new Size(28, 28);
    this.button4.TabIndex = 1;
    this.button4.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button4, "Штриховка");
    this.button4.UpControl = (Control) this._btnSolidBrush;
    this.button4.UseVisualStyleBackColor = false;
    this.button4.Leave += new EventHandler(this.button10_Leave);
    this.button5.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button5.Color = Color.Black;
    this.button5.Dock = DockStyle.Fill;
    this.button5.DownControl = (Control) this.button13;
    this.button5.HatchBackgroundColor = Color.White;
    this.button5.HatchStyle = HatchStyle.ForwardDiagonal;
    this.button5.LeftControl = (Control) this.button4;
    this.button5.Location = new Point(66, 2);
    this.button5.Margin = new Padding(2);
    this.button5.Name = "button5";
    this.button5.RightControl = (Control) this.button6;
    this.button5.Size = new Size(28, 28);
    this.button5.TabIndex = 2;
    this.button5.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button5, "Штриховка");
    this.button5.UpControl = (Control) this._btnSolidBrush;
    this.button5.UseVisualStyleBackColor = false;
    this.button5.Leave += new EventHandler(this.button10_Leave);
    this.button6.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button6.Color = Color.Black;
    this.button6.Dock = DockStyle.Fill;
    this.button6.DownControl = (Control) this.button14;
    this.button6.HatchBackgroundColor = Color.White;
    this.button6.HatchStyle = HatchStyle.BackwardDiagonal;
    this.button6.LeftControl = (Control) this.button5;
    this.button6.Location = new Point(98, 2);
    this.button6.Margin = new Padding(2);
    this.button6.Name = "button6";
    this.button6.RightControl = (Control) this.button7;
    this.button6.Size = new Size(28, 28);
    this.button6.TabIndex = 3;
    this.button6.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button6, "Штриховка");
    this.button6.UpControl = (Control) this._btnSolidBrush;
    this.button6.UseVisualStyleBackColor = false;
    this.button6.Leave += new EventHandler(this.button10_Leave);
    this.button7.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button7.Color = Color.Black;
    this.button7.Dock = DockStyle.Fill;
    this.button7.DownControl = (Control) this.button15;
    this.button7.HatchBackgroundColor = Color.White;
    this.button7.LeftControl = (Control) this.button6;
    this.button7.Location = new Point(130, 2);
    this.button7.Margin = new Padding(2);
    this.button7.Name = "button7";
    this.button7.RightControl = (Control) this.button8;
    this.button7.Size = new Size(28, 28);
    this.button7.TabIndex = 4;
    this.button7.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button7, "Штриховка");
    this.button7.UpControl = (Control) this._btnSolidBrush;
    this.button7.UseVisualStyleBackColor = false;
    this.button7.Leave += new EventHandler(this.button10_Leave);
    this.button8.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button8.Color = Color.Black;
    this.button8.Dock = DockStyle.Fill;
    this.button8.DownControl = (Control) this.button16;
    this.button8.HatchBackgroundColor = Color.White;
    this.button8.HatchStyle = HatchStyle.DiagonalCross;
    this.button8.LeftControl = (Control) this.button7;
    this.button8.Location = new Point(162, 2);
    this.button8.Margin = new Padding(2);
    this.button8.Name = "button8";
    this.button8.RightControl = (Control) this.button9;
    this.button8.Size = new Size(28, 28);
    this.button8.TabIndex = 5;
    this.button8.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button8, "Штриховка");
    this.button8.UseVisualStyleBackColor = false;
    this.button8.Leave += new EventHandler(this.button10_Leave);
    this.button9.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button9.Color = Color.Black;
    this.button9.Dock = DockStyle.Fill;
    this.button9.DownControl = (Control) this.button17;
    this.button9.HatchBackgroundColor = Color.White;
    this.button9.HatchStyle = HatchStyle.Percent05;
    this.button9.LeftControl = (Control) this.button8;
    this.button9.Location = new Point(194, 2);
    this.button9.Margin = new Padding(2);
    this.button9.Name = "button9";
    this.button9.RightControl = (Control) this.button10;
    this.button9.Size = new Size(28, 28);
    this.button9.TabIndex = 6;
    this.button9.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button9, "Штриховка");
    this.button9.UseVisualStyleBackColor = false;
    this.button9.Leave += new EventHandler(this.button10_Leave);
    this.button10.BrushType = ColorButton.BrushTypeEnum.Hatch;
    this.button10.Color = Color.Black;
    this.button10.Dock = DockStyle.Fill;
    this.button10.DownControl = (Control) this.button18;
    this.button10.HatchBackgroundColor = Color.White;
    this.button10.HatchStyle = HatchStyle.Percent10;
    this.button10.LeftControl = (Control) this.button9;
    this.button10.Location = new Point(226, 2);
    this.button10.Margin = new Padding(2);
    this.button10.Name = "button10";
    this.button10.Size = new Size(32 /*0x20*/, 28);
    this.button10.TabIndex = 7;
    this.button10.TextImageRelation = TextImageRelation.ImageBeforeText;
    this.BrushStyleToolTip.SetToolTip((Control) this.button10, "Штриховка");
    this.button10.UseVisualStyleBackColor = false;
    this.button10.OnNavigateToRight += new OnNavigateDelegate(this.button50_OnNavigateToRight);
    this.button10.Leave += new EventHandler(this.button10_Leave);
    this._panelFillSpacer.Dock = DockStyle.Top;
    this._panelFillSpacer.Location = new Point(0, 258);
    this._panelFillSpacer.Name = "_panelFillSpacer";
    this._panelFillSpacer.Size = new Size(260, 2);
    this._panelFillSpacer.TabIndex = 6;
    this._panelSolidFillButton.Controls.Add((Control) this._btnSolidBrush);
    this._panelSolidFillButton.Dock = DockStyle.Top;
    this._panelSolidFillButton.Location = new Point(0, 282);
    this._panelSolidFillButton.Name = "_panelSolidFillButton";
    this._panelSolidFillButton.Padding = new Padding(3);
    this._panelSolidFillButton.Size = new Size(260, 36);
    this._panelSolidFillButton.TabIndex = 2;
    this._panelHatches.ColumnCount = 8;
    this._panelHatches.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5f));
    this._panelHatches.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5f));
    this._panelHatches.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5f));
    this._panelHatches.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5f));
    this._panelHatches.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5f));
    this._panelHatches.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5f));
    this._panelHatches.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5f));
    this._panelHatches.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5f));
    this._panelHatches.Controls.Add((Control) this.button55, 4, 6);
    this._panelHatches.Controls.Add((Control) this.button54, 3, 6);
    this._panelHatches.Controls.Add((Control) this.button53, 2, 6);
    this._panelHatches.Controls.Add((Control) this.button52, 1, 6);
    this._panelHatches.Controls.Add((Control) this.button51, 0, 6);
    this._panelHatches.Controls.Add((Control) this.button50, 7, 5);
    this._panelHatches.Controls.Add((Control) this.button49, 6, 5);
    this._panelHatches.Controls.Add((Control) this.button48, 5, 5);
    this._panelHatches.Controls.Add((Control) this.button47, 4, 5);
    this._panelHatches.Controls.Add((Control) this.button46, 3, 5);
    this._panelHatches.Controls.Add((Control) this.button45, 2, 5);
    this._panelHatches.Controls.Add((Control) this.button44, 1, 5);
    this._panelHatches.Controls.Add((Control) this.button43, 0, 5);
    this._panelHatches.Controls.Add((Control) this.button42, 7, 4);
    this._panelHatches.Controls.Add((Control) this.button41, 6, 4);
    this._panelHatches.Controls.Add((Control) this.button40, 5, 4);
    this._panelHatches.Controls.Add((Control) this.button39, 4, 4);
    this._panelHatches.Controls.Add((Control) this.button38, 3, 4);
    this._panelHatches.Controls.Add((Control) this.button37, 2, 4);
    this._panelHatches.Controls.Add((Control) this.button36, 1, 4);
    this._panelHatches.Controls.Add((Control) this.button35, 0, 4);
    this._panelHatches.Controls.Add((Control) this.button34, 7, 3);
    this._panelHatches.Controls.Add((Control) this.button33, 6, 3);
    this._panelHatches.Controls.Add((Control) this.button32, 5, 3);
    this._panelHatches.Controls.Add((Control) this.button31, 4, 3);
    this._panelHatches.Controls.Add((Control) this.button30, 3, 3);
    this._panelHatches.Controls.Add((Control) this.button29, 2, 3);
    this._panelHatches.Controls.Add((Control) this.button28, 1, 3);
    this._panelHatches.Controls.Add((Control) this.button27, 0, 3);
    this._panelHatches.Controls.Add((Control) this.button26, 7, 2);
    this._panelHatches.Controls.Add((Control) this.button25, 6, 2);
    this._panelHatches.Controls.Add((Control) this.button24, 5, 2);
    this._panelHatches.Controls.Add((Control) this.button23, 4, 2);
    this._panelHatches.Controls.Add((Control) this.button22, 3, 2);
    this._panelHatches.Controls.Add((Control) this.button21, 2, 2);
    this._panelHatches.Controls.Add((Control) this.button20, 1, 2);
    this._panelHatches.Controls.Add((Control) this.button19, 0, 2);
    this._panelHatches.Controls.Add((Control) this.button18, 7, 1);
    this._panelHatches.Controls.Add((Control) this.button17, 6, 1);
    this._panelHatches.Controls.Add((Control) this.button16, 5, 1);
    this._panelHatches.Controls.Add((Control) this.button15, 4, 1);
    this._panelHatches.Controls.Add((Control) this.button14, 3, 1);
    this._panelHatches.Controls.Add((Control) this.button13, 2, 1);
    this._panelHatches.Controls.Add((Control) this.button12, 1, 1);
    this._panelHatches.Controls.Add((Control) this.button11, 0, 1);
    this._panelHatches.Controls.Add((Control) this.button10, 7, 0);
    this._panelHatches.Controls.Add((Control) this.button9, 6, 0);
    this._panelHatches.Controls.Add((Control) this.button8, 5, 0);
    this._panelHatches.Controls.Add((Control) this.button7, 4, 0);
    this._panelHatches.Controls.Add((Control) this.button6, 3, 0);
    this._panelHatches.Controls.Add((Control) this.button5, 2, 0);
    this._panelHatches.Controls.Add((Control) this.button4, 1, 0);
    this._panelHatches.Controls.Add((Control) this.button3, 0, 0);
    this._panelHatches.Dock = DockStyle.Top;
    this._panelHatches.Location = new Point(0, 318);
    this._panelHatches.Name = "_panelHatches";
    this._panelHatches.RowCount = 7;
    this._panelHatches.RowStyles.Add(new RowStyle(SizeType.Percent, 14.29f));
    this._panelHatches.RowStyles.Add(new RowStyle(SizeType.Percent, 14.29f));
    this._panelHatches.RowStyles.Add(new RowStyle(SizeType.Percent, 14.29f));
    this._panelHatches.RowStyles.Add(new RowStyle(SizeType.Percent, 14.29f));
    this._panelHatches.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28f));
    this._panelHatches.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28f));
    this._panelHatches.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28f));
    this._panelHatches.Size = new Size(260, 227);
    this._panelHatches.TabIndex = 3;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = Color.White;
    this.Controls.Add((Control) this._panelHatches);
    this.Controls.Add((Control) this._panelSolidFillButton);
    this.Controls.Add((Control) this._panelFillType);
    this.Controls.Add((Control) this._panelFillSpacer);
    this.Controls.Add((Control) this._colorSelectionUserControl);
    this.Controls.Add((Control) this._panelTansparentButton);
    this.Name = nameof (BrushSelectionUserControl);
    this.Size = new Size(260, 545);
    this._panelFillType.ResumeLayout(false);
    this._panelFillType.PerformLayout();
    this._panelTansparentButton.ResumeLayout(false);
    this._panelSolidFillButton.ResumeLayout(false);
    this._panelHatches.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public delegate void BrushStyleWasSelectedDelegate(BrushStyle brushStyle);

  public delegate void ColorWasSelectedDelegate(Color color);
}
