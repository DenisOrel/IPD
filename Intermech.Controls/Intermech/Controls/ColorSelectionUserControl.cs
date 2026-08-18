
// Type: Intermech.Controls.ColorSelectionUserControl
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Controls.Properties;
using Intermech.Extensions;
using Intermech.UI;
using Intermech.Windows.Forms;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Controls;

[DefaultEvent("ColorWasSelected")]
public class ColorSelectionUserControl : 
  SimpleBaseUserControl,
  IArrowKeysNavigationSupported,
  IFocusFromDirection
{
  public static readonly Color DefaultColor = Color.Black;
  public const string DefaultColorName = "Name";
  private ColorButton[] _standartColorButtons;
  private HashSet<Color> _standartColors;
  private Color _color = ColorSelectionUserControl.DefaultColor;
  private ColorSelectionUserControl.LastUsedColorsConfig _lastColors;
  private string _operationName = string.Empty;
  private bool _wasInit;
  private Color _secondColor = Color.Empty;
  private static ConcurrentDictionary<string, ColorSelectionUserControl.LastUsedColorsConfig> _LastColorConfigs = new ConcurrentDictionary<string, ColorSelectionUserControl.LastUsedColorsConfig>();
  private static Func<string, Color[]> LoadFromExternal;
  private static Action<string, Color[]> SaveToExternal;
  private ColorButton _lastActiveLastUsedColorButton;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ColorButton _btnOtherColors;
  private TableLayoutPanel _tableLayoutPanel;
  private ColorButton _colorButton72;
  private ColorButton _colorButton73;
  private ColorButton _colorButton74;
  private ColorButton _colorButton75;
  private ColorButton _colorButton76;
  private ColorButton _colorButton77;
  private ColorButton _colorButton78;
  private ColorButton _colorButton79;
  private ColorButton _colorButton80;
  private Panel panel1;
  private ColorButton _colorButton85;
  private ColorButton _colorButton81;
  private ColorButton _colorButton82;
  private ColorButton _colorButton83;
  private ColorButton _colorButton84;
  private Panel panel2;
  private ColorButton _colorButton90;
  private ColorButton _colorButton86;
  private ColorButton _colorButton87;
  private ColorButton _colorButton88;
  private ColorButton _colorButton89;
  private Panel panel3;
  private ColorButton _colorButton95;
  private ColorButton _colorButton91;
  private ColorButton _colorButton92;
  private ColorButton _colorButton93;
  private ColorButton _colorButton94;
  private Panel panel4;
  private ColorButton _colorButton100;
  private ColorButton _colorButton96;
  private ColorButton _colorButton97;
  private ColorButton _colorButton98;
  private ColorButton _colorButton99;
  private Panel panel5;
  private ColorButton _colorButton104;
  private Panel panel6;
  private ColorButton _colorButton110;
  private ColorButton _colorButton106;
  private ColorButton _colorButton109;
  private Panel panel7;
  private ColorButton _colorButton115;
  private ColorButton _colorButton111;
  private ColorButton _colorButton112;
  private ColorButton _colorButton113;
  private ColorButton _colorButton114;
  private Panel panel8;
  private ColorButton _colorButton120;
  private ColorButton _colorButton116;
  private ColorButton _colorButton117;
  private ColorButton _colorButton118;
  private ColorButton _colorButton119;
  private Panel panel9;
  private ColorButton _colorButton125;
  private ColorButton _colorButton121;
  private ColorButton _colorButton122;
  private ColorButton _colorButton123;
  private ColorButton _colorButton124;
  private Panel panel10;
  private ColorButton _colorButton130;
  private ColorButton _colorButton126;
  private ColorButton _colorButton127;
  private ColorButton _colorButton128;
  private ColorButton _colorButton129;
  private ColorButton _colorButton131;
  private ColorButton _colorButton132;
  private ColorButton _colorButton133;
  private ColorButton _colorButton134;
  private ColorButton _colorButton135;
  private ColorButton _colorButton136;
  private ColorButton _colorButton137;
  private ColorButton _colorButton138;
  private ColorButton _colorButton139;
  private ColorButton _colorButton140;
  private Panel _panelOtherColors;
  private ColorDialog _colorDialog;
  public ColorButton _colorButton71;
  private ColorButton colorButton4;
  private ColorButton colorButton3;
  private ColorButton colorButton2;
  private ColorButton _colorButton105;
  private Panel _panelColorsHeader;
  private SmoothLabel _labelStandartColors;
  private Panel _panelLastUsed;
  private SmoothLabel _labelLastColors;
  private TableLayoutPanel _tableLayoutLastUsed;
  private ColorButton _btnLastUsed7;
  private ColorButton _btnLastUsed6;
  private ColorButton _btnLastUsed8;
  private ColorButton _btnLastUsed10;
  private ColorButton _btnLastUsed9;
  private ColorButton _btnLastUsed2;
  private ColorButton _btnLastUsed1;
  private ColorButton _btnLastUsed3;
  private ColorButton _btnLastUsed5;
  private ColorButton _btnLastUsed4;
  private ToolTip _colorsToolTip;
  private Panel _dividerOtherColors;
  private ColorButton _colorButton108;
  private ColorButton _colorButton107;
  private ColorButton colorButton5;

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Color), "Name")]
  public Color Color
  {
    [DebuggerStepThrough] get => this._color;
    set
    {
      if (value == Color.Empty)
        value = ColorSelectionUserControl.DefaultColor;
      if (!(this._color != value))
        return;
      this._color = value;
      if (!this._wasInit)
        return;
      this.AllColorButtons.InvokeForAll<ColorButton>((Action<ColorButton>) (btn => btn.ForceDown = btn.Color.Equals((object) value)));
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue("")]
  public string OperationName
  {
    [DebuggerStepThrough] get => this._operationName;
    set
    {
      value = value ?? string.Empty;
      if (!(this._operationName != value))
        return;
      this._operationName = value;
      if (this.InDesignMode)
        return;
      if (this._lastColors != null)
      {
        this._lastColors.Changed -= new Action<ColorSelectionUserControl.LastUsedColorsConfig>(this.LastUsedColorsChangeHandler);
        this._lastColors = (ColorSelectionUserControl.LastUsedColorsConfig) null;
      }
      this._lastColors = ColorSelectionUserControl.GetOperationColors(this._operationName, new Action<ColorSelectionUserControl.LastUsedColorsConfig>(this.LastUsedColorsChangeHandler));
      this.RecalSizes();
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.components != null)
        this.components.Dispose();
      if (this._lastColors != null)
      {
        this._lastColors.Changed -= new Action<ColorSelectionUserControl.LastUsedColorsConfig>(this.LastUsedColorsChangeHandler);
        this._lastColors = (ColorSelectionUserControl.LastUsedColorsConfig) null;
      }
    }
    base.Dispose(disposing);
  }

  private void LastUsedColorsChangeHandler(
    ColorSelectionUserControl.LastUsedColorsConfig lastUsedColorsConfig)
  {
    this.RecalSizes();
  }

  public ColorSelectionUserControl()
  {
    this.InitializeComponent();
    this._standartColorButtons = this._tableLayoutPanel.Controls.OfType<ColorButton>().Concat<ColorButton>(this.GetGradientColorButtons()).ToArray<ColorButton>(70);
    this._standartColors = new HashSet<Color>(((IEnumerable<ColorButton>) this._standartColorButtons).Select<ColorButton, Color>((Func<ColorButton, Color>) (button => button.Color)));
    this.AllColorButtons.InvokeForAll<ColorButton>((Action<ColorButton>) (btn => btn.ForceDown = btn.Color.Equals((object) this._color)));
    this._wasInit = true;
  }

  private void ColorSelectionUserControl_Load(object sender, EventArgs e)
  {
    this.RecalSizes();
    this.AllColorButtons.InvokeForAll<ColorButton>((Action<ColorButton>) (btn =>
    {
      btn.Click += new EventHandler(this.btn_Click);
      btn.MouseUp += new MouseEventHandler(this.btn_MouseUp);
      btn.KeyUp += new KeyEventHandler(this.btn_KeyUp);
    }));
  }

  private void btn_MouseUp(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right || !(sender is ColorButton))
      return;
    this.FireSecondColorWasSelected(((ColorButton) sender).Color);
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public event ColorSelectionUserControl.ColorWasSelectedDelegate ColorWasSelected;

  protected virtual void FireColorWasSelected(Color color)
  {
    if (this.ColorWasSelected == null)
      return;
    this.ColorWasSelected(color);
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public event ColorSelectionUserControl.ColorWasSelectedDelegate SecondColorWasSelected;

  protected virtual void FireSecondColorWasSelected(Color color)
  {
    if (this.SecondColorWasSelected == null)
      return;
    this.SecondColorWasSelected(color);
  }

  private void btn_Click(object sender, EventArgs e)
  {
    if (!(sender is ColorButton))
      return;
    this.Color = ((ColorButton) sender).Color;
    this.FireColorWasSelected(this.Color);
  }

  private void _tableLayoutStandartColors_SizeChanged(object sender, EventArgs e)
  {
    this.RecalSizes();
  }

  private IEnumerable<ColorButton> LastUsedButtons
  {
    [DebuggerStepThrough] get
    {
      yield return this._btnLastUsed1;
      yield return this._btnLastUsed2;
      yield return this._btnLastUsed3;
      yield return this._btnLastUsed4;
      yield return this._btnLastUsed5;
      yield return this._btnLastUsed6;
      yield return this._btnLastUsed7;
      yield return this._btnLastUsed8;
      yield return this._btnLastUsed9;
      yield return this._btnLastUsed10;
    }
  }

  public void AddButtonsTooltipSuffix(string suffix)
  {
    foreach (ColorButton allColorButton in this.AllColorButtons)
      this._colorsToolTip.SetToolTip((Control) allColorButton, this._colorsToolTip.GetToolTip((Control) allColorButton) + suffix);
    this._colorsToolTip.SetToolTip((Control) this._btnOtherColors, this._colorsToolTip.GetToolTip((Control) this._btnOtherColors) + suffix);
  }

  private IEnumerable<ColorButton> AllColorButtons
  {
    [DebuggerStepThrough] get
    {
      return ((IEnumerable<ColorButton>) this._standartColorButtons).Concat<ColorButton>(this.LastUsedButtons);
    }
  }

  private IEnumerable<ColorButton> GetGradientColorButtons()
  {
    for (int panelNum = 1; panelNum < 11; ++panelNum)
    {
      if (this._tableLayoutPanel.GetControlFromPosition(panelNum, 1) is Panel controlFromPosition)
      {
        foreach (ColorButton gradientColorButton in controlFromPosition.Controls.OfType<ColorButton>())
          yield return gradientColorButton;
      }
    }
  }

  private void RecalSizes()
  {
    this._tableLayoutPanel.SuspendLayout();
    try
    {
      int top = this._colorButton71.Margin.Top;
      int bottom = this._colorButton71.Margin.Bottom;
      if ((double) this._tableLayoutPanel.RowStyles[0].Height != (double) (this._colorButton71.Width + top + bottom))
        this._tableLayoutPanel.RowStyles[0].Height = (float) (this._colorButton71.Width + top + bottom);
      if ((double) this._tableLayoutPanel.RowStyles[2].Height != (double) this._tableLayoutPanel.RowStyles[0].Height)
        this._tableLayoutPanel.RowStyles[2].Height = this._tableLayoutPanel.RowStyles[0].Height;
      foreach (Control gradientColorButton in this.GetGradientColorButtons())
        gradientColorButton.Height = this._colorButton71.Width;
      int num1 = this._colorButton71.Width * 7 + (top + bottom) * 3;
      if (num1 != this._tableLayoutPanel.Height)
        this._tableLayoutPanel.Height = num1;
      bool flag = this._lastColors != null && this._lastColors.ColorsCount > 0;
      if (this._panelLastUsed.Visible != flag)
      {
        this._tableLayoutLastUsed.Visible = flag;
        this._panelLastUsed.Visible = flag;
      }
      if (this._tableLayoutLastUsed.Visible)
      {
        if (this._tableLayoutLastUsed.Height != this._colorButton71.Width + top + bottom)
          this._tableLayoutLastUsed.Height = this._colorButton71.Width + top + bottom;
        int index = 0;
        foreach (ColorButton lastUsedButton in this.LastUsedButtons)
        {
          lastUsedButton.Visible = index < this._lastColors.ColorsCount;
          if (lastUsedButton.Visible)
            lastUsedButton.Color = this._lastColors.GetColor(index);
          ++index;
        }
      }
      int num2 = this._panelColorsHeader.Height + this._tableLayoutPanel.Height + this._panelOtherColors.Height + this._dividerOtherColors.Height;
      if (flag)
        num2 += this._panelLastUsed.Height + this._tableLayoutLastUsed.Height;
      if (this.Height != num2)
        this.Height = num2;
    }
    finally
    {
      this._tableLayoutPanel.ResumeLayout(true);
    }
    if (this.Parent == null || !(this.Parent is PopupDropDown))
      return;
    PopupDropDown parent = (PopupDropDown) this.Parent;
    if (parent.ClientSize.Height == this.Height)
      return;
    parent.ClientSize = new Size(parent.ClientSize.Width, this.Height);
  }

  private void _btnOtherColors_Click(object sender, EventArgs e)
  {
    this._colorDialog.Color = this._color;
    if (this._colorDialog.ShowDialog((IWin32Window) this) != DialogResult.OK)
      return;
    if (this._lastColors != null && !this._lastColors.Contains(this._colorDialog.Color) && !this._standartColors.Contains(this._colorDialog.Color))
      this._lastColors.AddNewColor(this._colorDialog.Color);
    this.Color = this._colorDialog.Color;
    this.FireColorWasSelected(this.Color);
  }

  private void _btnOtherColors_MouseUp(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right)
      return;
    this.SelectCustomSecondColor();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Color), "")]
  public Color SecondColor
  {
    get => this._secondColor;
    set
    {
      if (!(this._secondColor != value))
        return;
      this._secondColor = value;
    }
  }

  public bool FocusCheckedButton()
  {
    Control control = (Control) this.AllColorButtons.FirstOrDefault<ColorButton>((Func<ColorButton, bool>) (btn => btn.ForceDown));
    if (control == null)
      return false;
    control.Focus();
    return true;
  }

  private void btn_KeyUp(object sender, KeyEventArgs e)
  {
    if (!e.Control || e.KeyCode != Keys.Space || !(sender is ColorButton))
      return;
    this.FireSecondColorWasSelected(((ColorButton) sender).Color);
  }

  private void _btnOtherColors_KeyUp(object sender, KeyEventArgs e)
  {
    if (!e.Control || e.KeyCode != Keys.Space)
      return;
    this.SelectCustomSecondColor();
  }

  private void SelectCustomSecondColor()
  {
    this._colorDialog.Color = this._secondColor;
    if (this._colorDialog.ShowDialog((IWin32Window) this) != DialogResult.OK)
      return;
    if (this._lastColors != null && !this._lastColors.Contains(this._colorDialog.Color) && !this._standartColors.Contains(this._colorDialog.Color))
      this._lastColors.AddNewColor(this._colorDialog.Color);
    this.SecondColor = this._colorDialog.Color;
    this.FireSecondColorWasSelected(this._colorDialog.Color);
  }

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    if (this._lastColors != null || this.InDesignMode)
      return;
    this._lastColors = ColorSelectionUserControl.GetOperationColors(this._operationName, new Action<ColorSelectionUserControl.LastUsedColorsConfig>(this.LastUsedColorsChangeHandler));
    this.RecalSizes();
  }

  public static void InitLastColorsLoadSave(
    Func<string, Color[]> loadFromExternal,
    Action<string, Color[]> saveToExternal)
  {
    ColorSelectionUserControl.LoadFromExternal = ColorSelectionUserControl.LoadFromExternal == null ? loadFromExternal : throw new Exception("LastUsedColorsServiceManager already initialized");
    ColorSelectionUserControl.SaveToExternal = saveToExternal;
  }

  internal static ColorSelectionUserControl.LastUsedColorsConfig GetOperationColors(
    string operation,
    Action<ColorSelectionUserControl.LastUsedColorsConfig> ChangesHandler)
  {
    ColorSelectionUserControl.LastUsedColorsConfig orAdd = ColorSelectionUserControl._LastColorConfigs.GetOrAdd(operation, (Func<string, ColorSelectionUserControl.LastUsedColorsConfig>) (opName =>
    {
      Color[] colors = ColorSelectionUserControl.LoadFromExternal != null ? ColorSelectionUserControl.LoadFromExternal(opName) : (Color[]) null;
      ColorSelectionUserControl.LastUsedColorsConfig operationColors = new ColorSelectionUserControl.LastUsedColorsConfig(opName, colors);
      operationColors.Changed += new Action<ColorSelectionUserControl.LastUsedColorsConfig>(ColorSelectionUserControl.OnLastUsedColorsConfigChanged);
      return operationColors;
    }));
    orAdd.Changed += ChangesHandler;
    return orAdd;
  }

  private static void OnLastUsedColorsConfigChanged(
    ColorSelectionUserControl.LastUsedColorsConfig lastUsedColorsConfig)
  {
    if (ColorSelectionUserControl.SaveToExternal == null)
      return;
    ColorSelectionUserControl.SaveToExternal(lastUsedColorsConfig.OperationName, lastUsedColorsConfig.Colors.ToArray<Color>(lastUsedColorsConfig.ColorsCount));
  }

  private void _colorButton131_OnNavigateToDown(
    IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    if (sender.DownControl == null)
      return;
    if (!this._panelLastUsed.Visible)
    {
      if (this._btnOtherColors.CanFocus)
        this._btnOtherColors.Focus();
      blockDefaultNavigation = true;
    }
    else
    {
      if (sender.DownControl.CanFocus)
        return;
      blockDefaultNavigation = true;
      this.LastUsedButtons.LastOrDefault<ColorButton>((Func<ColorButton, bool>) (btn => btn.CanFocus))?.Focus();
    }
  }

  private void _btnOtherColors_OnNavigateToUp(
    IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    if (sender.UpControl == null)
      return;
    blockDefaultNavigation = true;
    if (!this._panelLastUsed.Visible)
    {
      if (!this._colorButton131.CanFocus)
        return;
      this._colorButton131.Focus();
    }
    else if (this._lastActiveLastUsedColorButton == null)
      this.LastUsedButtons.LastOrDefault<ColorButton>((Func<ColorButton, bool>) (btn => btn.CanFocus))?.Focus();
    else
      this._lastActiveLastUsedColorButton.Focus();
  }

  private void _colorButton71_OnNavigateToLeft(
    IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    blockDefaultNavigation = true;
    this.NavigateToLeft();
  }

  private void _colorButton72_OnNavigateToUp(
    IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    blockDefaultNavigation = true;
    this.NavigateToUp();
  }

  private void _colorButton80_OnNavigateToRight(
    IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    blockDefaultNavigation = true;
    this.NavigateToRight();
  }

  private void _btnOtherColors_OnNavigateToDown(
    IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    blockDefaultNavigation = true;
    this.NavigateToDown();
  }

  public IEnumerable<Control> LeftMostControls
  {
    [DebuggerStepThrough] get
    {
      yield return (Control) this._colorButton71;
      yield return (Control) this._colorButton85;
      yield return (Control) this._colorButton81;
      yield return (Control) this._colorButton82;
      yield return (Control) this._colorButton83;
      yield return (Control) this._colorButton84;
      yield return (Control) this._colorButton131;
      yield return (Control) this._btnLastUsed1;
      yield return (Control) this._btnOtherColors;
    }
  }

  public IEnumerable<Control> TopMostControls
  {
    [DebuggerStepThrough] get
    {
      yield return (Control) this._colorButton71;
      yield return (Control) this._colorButton72;
      yield return (Control) this._colorButton73;
      yield return (Control) this._colorButton74;
      yield return (Control) this._colorButton75;
      yield return (Control) this._colorButton76;
      yield return (Control) this._colorButton77;
      yield return (Control) this._colorButton78;
      yield return (Control) this._colorButton79;
      yield return (Control) this._colorButton80;
    }
  }

  public IEnumerable<Control> RightMostControls
  {
    [DebuggerStepThrough] get
    {
      yield return (Control) this._colorButton80;
      yield return (Control) this._colorButton130;
      yield return (Control) this._colorButton126;
      yield return (Control) this._colorButton127;
      yield return (Control) this._colorButton128;
      yield return (Control) this._colorButton129;
      yield return (Control) this._colorButton140;
      yield return (Control) this._btnLastUsed10;
      yield return (Control) this._btnOtherColors;
    }
  }

  public IEnumerable<Control> BottomMostControls
  {
    [DebuggerStepThrough] get
    {
      yield return (Control) this._btnOtherColors;
    }
  }

  private void _btnLastUsed1_Leave(object sender, EventArgs e)
  {
    this._lastActiveLastUsedColorButton = (ColorButton) sender;
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this._tableLayoutPanel = new TableLayoutPanel();
    this._colorButton71 = new ColorButton();
    this._colorButton85 = new ColorButton();
    this._colorButton81 = new ColorButton();
    this._colorButton82 = new ColorButton();
    this._colorButton83 = new ColorButton();
    this._colorButton84 = new ColorButton();
    this._colorButton131 = new ColorButton();
    this._btnLastUsed1 = new ColorButton();
    this._btnOtherColors = new ColorButton();
    this._btnLastUsed2 = new ColorButton();
    this._btnLastUsed3 = new ColorButton();
    this._btnLastUsed4 = new ColorButton();
    this._btnLastUsed5 = new ColorButton();
    this._btnLastUsed6 = new ColorButton();
    this._btnLastUsed7 = new ColorButton();
    this._btnLastUsed8 = new ColorButton();
    this._btnLastUsed9 = new ColorButton();
    this._btnLastUsed10 = new ColorButton();
    this._colorButton140 = new ColorButton();
    this._colorButton139 = new ColorButton();
    this._colorButton138 = new ColorButton();
    this._colorButton137 = new ColorButton();
    this._colorButton136 = new ColorButton();
    this._colorButton135 = new ColorButton();
    this._colorButton134 = new ColorButton();
    this._colorButton133 = new ColorButton();
    this._colorButton132 = new ColorButton();
    this._colorButton89 = new ColorButton();
    this._colorButton94 = new ColorButton();
    this._colorButton99 = new ColorButton();
    this._colorButton104 = new ColorButton();
    this._colorButton109 = new ColorButton();
    this._colorButton114 = new ColorButton();
    this._colorButton119 = new ColorButton();
    this._colorButton124 = new ColorButton();
    this._colorButton129 = new ColorButton();
    this._colorButton128 = new ColorButton();
    this._colorButton123 = new ColorButton();
    this._colorButton118 = new ColorButton();
    this._colorButton113 = new ColorButton();
    this._colorButton108 = new ColorButton();
    this.colorButton2 = new ColorButton();
    this._colorButton98 = new ColorButton();
    this._colorButton93 = new ColorButton();
    this._colorButton88 = new ColorButton();
    this._colorButton87 = new ColorButton();
    this._colorButton92 = new ColorButton();
    this._colorButton97 = new ColorButton();
    this.colorButton3 = new ColorButton();
    this._colorButton107 = new ColorButton();
    this.colorButton5 = new ColorButton();
    this._colorButton112 = new ColorButton();
    this._colorButton117 = new ColorButton();
    this._colorButton122 = new ColorButton();
    this._colorButton127 = new ColorButton();
    this._colorButton126 = new ColorButton();
    this._colorButton121 = new ColorButton();
    this._colorButton116 = new ColorButton();
    this._colorButton111 = new ColorButton();
    this._colorButton106 = new ColorButton();
    this.colorButton4 = new ColorButton();
    this._colorButton96 = new ColorButton();
    this._colorButton91 = new ColorButton();
    this._colorButton86 = new ColorButton();
    this._colorButton90 = new ColorButton();
    this._colorButton95 = new ColorButton();
    this._colorButton100 = new ColorButton();
    this._colorButton105 = new ColorButton();
    this._colorButton110 = new ColorButton();
    this._colorButton115 = new ColorButton();
    this._colorButton120 = new ColorButton();
    this._colorButton125 = new ColorButton();
    this._colorButton130 = new ColorButton();
    this._colorButton80 = new ColorButton();
    this._colorButton79 = new ColorButton();
    this._colorButton78 = new ColorButton();
    this._colorButton77 = new ColorButton();
    this._colorButton76 = new ColorButton();
    this._colorButton75 = new ColorButton();
    this._colorButton74 = new ColorButton();
    this._colorButton73 = new ColorButton();
    this._colorButton72 = new ColorButton();
    this.panel1 = new Panel();
    this.panel2 = new Panel();
    this.panel3 = new Panel();
    this.panel4 = new Panel();
    this.panel5 = new Panel();
    this.panel6 = new Panel();
    this.panel7 = new Panel();
    this.panel8 = new Panel();
    this.panel9 = new Panel();
    this.panel10 = new Panel();
    this._panelOtherColors = new Panel();
    this._colorDialog = new ColorDialog();
    this._panelColorsHeader = new Panel();
    this._labelStandartColors = new SmoothLabel();
    this._panelLastUsed = new Panel();
    this._labelLastColors = new SmoothLabel();
    this._tableLayoutLastUsed = new TableLayoutPanel();
    this._colorsToolTip = new ToolTip(this.components);
    this._dividerOtherColors = new Panel();
    this._tableLayoutPanel.SuspendLayout();
    this._colorButton107.SuspendLayout();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.panel3.SuspendLayout();
    this.panel4.SuspendLayout();
    this.panel5.SuspendLayout();
    this.panel6.SuspendLayout();
    this.panel7.SuspendLayout();
    this.panel8.SuspendLayout();
    this.panel9.SuspendLayout();
    this.panel10.SuspendLayout();
    this._panelOtherColors.SuspendLayout();
    this._panelColorsHeader.SuspendLayout();
    this._panelLastUsed.SuspendLayout();
    this._tableLayoutLastUsed.SuspendLayout();
    this.SuspendLayout();
    this._tableLayoutPanel.ColumnCount = 11;
    this._tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 3f));
    this._tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
    this._tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
    this._tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
    this._tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
    this._tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
    this._tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
    this._tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
    this._tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
    this._tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
    this._tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
    this._tableLayoutPanel.Controls.Add((Control) this._colorButton71, 1, 0);
    this._tableLayoutPanel.Controls.Add((Control) this._colorButton72, 2, 0);
    this._tableLayoutPanel.Controls.Add((Control) this._colorButton73, 3, 0);
    this._tableLayoutPanel.Controls.Add((Control) this._colorButton74, 4, 0);
    this._tableLayoutPanel.Controls.Add((Control) this._colorButton75, 5, 0);
    this._tableLayoutPanel.Controls.Add((Control) this._colorButton76, 6, 0);
    this._tableLayoutPanel.Controls.Add((Control) this._colorButton77, 7, 0);
    this._tableLayoutPanel.Controls.Add((Control) this._colorButton78, 8, 0);
    this._tableLayoutPanel.Controls.Add((Control) this._colorButton79, 9, 0);
    this._tableLayoutPanel.Controls.Add((Control) this._colorButton80, 10, 0);
    this._tableLayoutPanel.Controls.Add((Control) this.panel1, 1, 1);
    this._tableLayoutPanel.Controls.Add((Control) this.panel2, 2, 1);
    this._tableLayoutPanel.Controls.Add((Control) this.panel3, 3, 1);
    this._tableLayoutPanel.Controls.Add((Control) this.panel4, 4, 1);
    this._tableLayoutPanel.Controls.Add((Control) this.panel5, 5, 1);
    this._tableLayoutPanel.Controls.Add((Control) this.panel6, 6, 1);
    this._tableLayoutPanel.Controls.Add((Control) this.panel7, 7, 1);
    this._tableLayoutPanel.Controls.Add((Control) this.panel8, 8, 1);
    this._tableLayoutPanel.Controls.Add((Control) this.panel9, 9, 1);
    this._tableLayoutPanel.Controls.Add((Control) this.panel10, 10, 1);
    this._tableLayoutPanel.Controls.Add((Control) this._colorButton131, 1, 2);
    this._tableLayoutPanel.Controls.Add((Control) this._colorButton132, 2, 2);
    this._tableLayoutPanel.Controls.Add((Control) this._colorButton133, 3, 2);
    this._tableLayoutPanel.Controls.Add((Control) this._colorButton134, 4, 2);
    this._tableLayoutPanel.Controls.Add((Control) this._colorButton135, 5, 2);
    this._tableLayoutPanel.Controls.Add((Control) this._colorButton136, 6, 2);
    this._tableLayoutPanel.Controls.Add((Control) this._colorButton137, 7, 2);
    this._tableLayoutPanel.Controls.Add((Control) this._colorButton138, 8, 2);
    this._tableLayoutPanel.Controls.Add((Control) this._colorButton139, 9, 2);
    this._tableLayoutPanel.Controls.Add((Control) this._colorButton140, 10, 2);
    this._tableLayoutPanel.Dock = DockStyle.Top;
    this._tableLayoutPanel.Location = new Point(0, 22);
    this._tableLayoutPanel.Name = "_tableLayoutPanel";
    this._tableLayoutPanel.RowCount = 3;
    this._tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
    this._tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this._tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
    this._tableLayoutPanel.Size = new Size(243, 150);
    this._tableLayoutPanel.TabIndex = 0;
    this._tableLayoutPanel.SizeChanged += new EventHandler(this._tableLayoutStandartColors_SizeChanged);
    this._colorButton71.Color = Color.White;
    this._colorButton71.Dock = DockStyle.Fill;
    this._colorButton71.DownControl = (Control) this._colorButton85;
    this._colorButton71.Location = new Point(3, 3);
    this._colorButton71.Margin = new Padding(0, 3, 3, 0);
    this._colorButton71.Name = "_colorButton71";
    this._colorButton71.RightControl = (Control) this._colorButton72;
    this._colorButton71.Size = new Size(21, 21);
    this._colorButton71.TabIndex = 0;
    this._colorsToolTip.SetToolTip((Control) this._colorButton71, "Белый");
    this._colorButton71.UseVisualStyleBackColor = false;
    this._colorButton71.OnNavigateToUp += new OnNavigateDelegate(this._colorButton72_OnNavigateToUp);
    this._colorButton71.OnNavigateToLeft += new OnNavigateDelegate(this._colorButton71_OnNavigateToLeft);
    this._colorButton85.Borders = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton85.Color = Color.FromArgb(242, 242, 242);
    this._colorButton85.Dock = DockStyle.Top;
    this._colorButton85.DownControl = (Control) this._colorButton81;
    this._colorButton85.Location = new Point(0, 3);
    this._colorButton85.Name = "_colorButton85";
    this._colorButton85.RightControl = (Control) this._colorButton90;
    this._colorButton85.Size = new Size(21, 20);
    this._colorButton85.TabIndex = 0;
    this._colorsToolTip.SetToolTip((Control) this._colorButton85, "Белый, более тёмный оттенок 5%");
    this._colorButton85.UpControl = (Control) this._colorButton71;
    this._colorButton85.UseVisualStyleBackColor = false;
    this._colorButton85.OnNavigateToLeft += new OnNavigateDelegate(this._colorButton71_OnNavigateToLeft);
    this._colorButton81.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton81.Color = Color.FromArgb(216, 216, 216);
    this._colorButton81.Dock = DockStyle.Top;
    this._colorButton81.DownControl = (Control) this._colorButton82;
    this._colorButton81.Location = new Point(0, 23);
    this._colorButton81.Name = "_colorButton81";
    this._colorButton81.RightControl = (Control) this._colorButton86;
    this._colorButton81.Size = new Size(21, 20);
    this._colorButton81.TabIndex = 1;
    this._colorsToolTip.SetToolTip((Control) this._colorButton81, "Белый, более тёмный оттенок 15%");
    this._colorButton81.UpControl = (Control) this._colorButton85;
    this._colorButton81.UseVisualStyleBackColor = false;
    this._colorButton81.OnNavigateToLeft += new OnNavigateDelegate(this._colorButton71_OnNavigateToLeft);
    this._colorButton82.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton82.Color = Color.FromArgb(191, 191, 191);
    this._colorButton82.Dock = DockStyle.Top;
    this._colorButton82.DownControl = (Control) this._colorButton83;
    this._colorButton82.Location = new Point(0, 43);
    this._colorButton82.Name = "_colorButton82";
    this._colorButton82.RightControl = (Control) this._colorButton87;
    this._colorButton82.Size = new Size(21, 20);
    this._colorButton82.TabIndex = 2;
    this._colorsToolTip.SetToolTip((Control) this._colorButton82, "Белый, более тёмный оттенок 25%");
    this._colorButton82.UpControl = (Control) this._colorButton81;
    this._colorButton82.UseVisualStyleBackColor = false;
    this._colorButton82.OnNavigateToLeft += new OnNavigateDelegate(this._colorButton71_OnNavigateToLeft);
    this._colorButton83.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton83.Color = Color.FromArgb(165, 165, 164);
    this._colorButton83.Dock = DockStyle.Top;
    this._colorButton83.DownControl = (Control) this._colorButton84;
    this._colorButton83.Location = new Point(0, 63 /*0x3F*/);
    this._colorButton83.Name = "_colorButton83";
    this._colorButton83.RightControl = (Control) this._colorButton88;
    this._colorButton83.Size = new Size(21, 20);
    this._colorButton83.TabIndex = 3;
    this._colorsToolTip.SetToolTip((Control) this._colorButton83, "Белый, более тёмный оттенок 35%");
    this._colorButton83.UpControl = (Control) this._colorButton82;
    this._colorButton83.UseVisualStyleBackColor = false;
    this._colorButton83.OnNavigateToLeft += new OnNavigateDelegate(this._colorButton71_OnNavigateToLeft);
    this._colorButton84.Borders = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton84.Color = Color.FromArgb((int) sbyte.MaxValue, (int) sbyte.MaxValue, 126);
    this._colorButton84.Dock = DockStyle.Top;
    this._colorButton84.DownControl = (Control) this._colorButton131;
    this._colorButton84.Location = new Point(0, 83);
    this._colorButton84.Name = "_colorButton84";
    this._colorButton84.RightControl = (Control) this._colorButton89;
    this._colorButton84.Size = new Size(21, 20);
    this._colorButton84.TabIndex = 4;
    this._colorsToolTip.SetToolTip((Control) this._colorButton84, "Белый, более тёмный оттенок 50%");
    this._colorButton84.UpControl = (Control) this._colorButton83;
    this._colorButton84.UseVisualStyleBackColor = false;
    this._colorButton84.OnNavigateToLeft += new OnNavigateDelegate(this._colorButton71_OnNavigateToLeft);
    this._colorButton131.Color = Color.FromArgb(192 /*0xC0*/, 0, 0);
    this._colorButton131.Dock = DockStyle.Fill;
    this._colorButton131.DownControl = (Control) this._btnLastUsed1;
    this._colorButton131.Location = new Point(3, 129);
    this._colorButton131.Margin = new Padding(0, 3, 3, 0);
    this._colorButton131.Name = "_colorButton131";
    this._colorButton131.RightControl = (Control) this._colorButton132;
    this._colorButton131.Size = new Size(21, 21);
    this._colorButton131.TabIndex = 20;
    this._colorsToolTip.SetToolTip((Control) this._colorButton131, "Тёмно-красный");
    this._colorButton131.UpControl = (Control) this._colorButton84;
    this._colorButton131.UseVisualStyleBackColor = false;
    this._colorButton131.OnNavigateToDown += new OnNavigateDelegate(this._colorButton131_OnNavigateToDown);
    this._colorButton131.OnNavigateToLeft += new OnNavigateDelegate(this._colorButton71_OnNavigateToLeft);
    this._btnLastUsed1.Color = Color.White;
    this._btnLastUsed1.Dock = DockStyle.Fill;
    this._btnLastUsed1.DownControl = (Control) this._btnOtherColors;
    this._btnLastUsed1.Location = new Point(3, 3);
    this._btnLastUsed1.Margin = new Padding(0, 3, 3, 0);
    this._btnLastUsed1.Name = "_btnLastUsed1";
    this._btnLastUsed1.RightControl = (Control) this._btnLastUsed2;
    this._btnLastUsed1.Size = new Size(21, 21);
    this._btnLastUsed1.TabIndex = 30;
    this._colorsToolTip.SetToolTip((Control) this._btnLastUsed1, "Последний использованный нестандартный цвет");
    this._btnLastUsed1.UpControl = (Control) this._colorButton131;
    this._btnLastUsed1.UseVisualStyleBackColor = false;
    this._btnLastUsed1.OnNavigateToLeft += new OnNavigateDelegate(this._colorButton71_OnNavigateToLeft);
    this._btnLastUsed1.Leave += new EventHandler(this._btnLastUsed1_Leave);
    this._btnOtherColors.Color = Color.FromArgb(253, 253, 253);
    this._btnOtherColors.Dock = DockStyle.Fill;
    this._btnOtherColors.Image = (Image) Resources.gnome_colors_icon_2;
    this._btnOtherColors.ImageAlign = ContentAlignment.MiddleLeft;
    this._btnOtherColors.Location = new Point(3, 3);
    this._btnOtherColors.Name = "_btnOtherColors";
    this._btnOtherColors.Size = new Size(237, 27);
    this._btnOtherColors.TabIndex = 0;
    this._btnOtherColors.Text = " &Другие цвета...";
    this._btnOtherColors.TextAlign = ContentAlignment.MiddleLeft;
    this._btnOtherColors.TextImageRelation = TextImageRelation.ImageBeforeText;
    this._btnOtherColors.UpControl = (Control) this._btnLastUsed1;
    this._btnOtherColors.UseVisualStyleBackColor = false;
    this._btnOtherColors.OnNavigateToUp += new OnNavigateDelegate(this._btnOtherColors_OnNavigateToUp);
    this._btnOtherColors.OnNavigateToDown += new OnNavigateDelegate(this._btnOtherColors_OnNavigateToDown);
    this._btnOtherColors.OnNavigateToLeft += new OnNavigateDelegate(this._colorButton71_OnNavigateToLeft);
    this._btnOtherColors.OnNavigateToRight += new OnNavigateDelegate(this._colorButton80_OnNavigateToRight);
    this._btnOtherColors.Click += new EventHandler(this._btnOtherColors_Click);
    this._btnOtherColors.KeyUp += new KeyEventHandler(this._btnOtherColors_KeyUp);
    this._btnOtherColors.MouseUp += new MouseEventHandler(this._btnOtherColors_MouseUp);
    this._btnLastUsed2.Color = Color.White;
    this._btnLastUsed2.Dock = DockStyle.Fill;
    this._btnLastUsed2.DownControl = (Control) this._btnOtherColors;
    this._btnLastUsed2.LeftControl = (Control) this._btnLastUsed1;
    this._btnLastUsed2.Location = new Point(27, 3);
    this._btnLastUsed2.Margin = new Padding(0, 3, 3, 0);
    this._btnLastUsed2.Name = "_btnLastUsed2";
    this._btnLastUsed2.RightControl = (Control) this._btnLastUsed3;
    this._btnLastUsed2.Size = new Size(21, 21);
    this._btnLastUsed2.TabIndex = 31 /*0x1F*/;
    this._colorsToolTip.SetToolTip((Control) this._btnLastUsed2, "Последний использованный нестандартный цвет");
    this._btnLastUsed2.UpControl = (Control) this._colorButton132;
    this._btnLastUsed2.UseVisualStyleBackColor = false;
    this._btnLastUsed2.Leave += new EventHandler(this._btnLastUsed1_Leave);
    this._btnLastUsed3.Color = Color.White;
    this._btnLastUsed3.Dock = DockStyle.Fill;
    this._btnLastUsed3.DownControl = (Control) this._btnOtherColors;
    this._btnLastUsed3.LeftControl = (Control) this._btnLastUsed2;
    this._btnLastUsed3.Location = new Point(51, 3);
    this._btnLastUsed3.Margin = new Padding(0, 3, 3, 0);
    this._btnLastUsed3.Name = "_btnLastUsed3";
    this._btnLastUsed3.RightControl = (Control) this._btnLastUsed4;
    this._btnLastUsed3.Size = new Size(21, 21);
    this._btnLastUsed3.TabIndex = 32 /*0x20*/;
    this._colorsToolTip.SetToolTip((Control) this._btnLastUsed3, "Последний использованный нестандартный цвет");
    this._btnLastUsed3.UpControl = (Control) this._colorButton133;
    this._btnLastUsed3.UseVisualStyleBackColor = false;
    this._btnLastUsed3.Leave += new EventHandler(this._btnLastUsed1_Leave);
    this._btnLastUsed4.Color = Color.White;
    this._btnLastUsed4.Dock = DockStyle.Fill;
    this._btnLastUsed4.DownControl = (Control) this._btnOtherColors;
    this._btnLastUsed4.LeftControl = (Control) this._btnLastUsed3;
    this._btnLastUsed4.Location = new Point(75, 3);
    this._btnLastUsed4.Margin = new Padding(0, 3, 3, 0);
    this._btnLastUsed4.Name = "_btnLastUsed4";
    this._btnLastUsed4.RightControl = (Control) this._btnLastUsed5;
    this._btnLastUsed4.Size = new Size(21, 21);
    this._btnLastUsed4.TabIndex = 33;
    this._colorsToolTip.SetToolTip((Control) this._btnLastUsed4, "Последний использованный нестандартный цвет");
    this._btnLastUsed4.UpControl = (Control) this._colorButton134;
    this._btnLastUsed4.UseVisualStyleBackColor = false;
    this._btnLastUsed4.Leave += new EventHandler(this._btnLastUsed1_Leave);
    this._btnLastUsed5.Color = Color.White;
    this._btnLastUsed5.Dock = DockStyle.Fill;
    this._btnLastUsed5.DownControl = (Control) this._btnOtherColors;
    this._btnLastUsed5.LeftControl = (Control) this._btnLastUsed4;
    this._btnLastUsed5.Location = new Point(99, 3);
    this._btnLastUsed5.Margin = new Padding(0, 3, 3, 0);
    this._btnLastUsed5.Name = "_btnLastUsed5";
    this._btnLastUsed5.RightControl = (Control) this._btnLastUsed6;
    this._btnLastUsed5.Size = new Size(21, 21);
    this._btnLastUsed5.TabIndex = 34;
    this._colorsToolTip.SetToolTip((Control) this._btnLastUsed5, "Последний использованный нестандартный цвет");
    this._btnLastUsed5.UpControl = (Control) this._colorButton135;
    this._btnLastUsed5.UseVisualStyleBackColor = false;
    this._btnLastUsed5.Leave += new EventHandler(this._btnLastUsed1_Leave);
    this._btnLastUsed6.Color = Color.White;
    this._btnLastUsed6.Dock = DockStyle.Fill;
    this._btnLastUsed6.DownControl = (Control) this._btnOtherColors;
    this._btnLastUsed6.LeftControl = (Control) this._btnLastUsed5;
    this._btnLastUsed6.Location = new Point(123, 3);
    this._btnLastUsed6.Margin = new Padding(0, 3, 3, 0);
    this._btnLastUsed6.Name = "_btnLastUsed6";
    this._btnLastUsed6.RightControl = (Control) this._btnLastUsed7;
    this._btnLastUsed6.Size = new Size(21, 21);
    this._btnLastUsed6.TabIndex = 35;
    this._colorsToolTip.SetToolTip((Control) this._btnLastUsed6, "Последний использованный нестандартный цвет");
    this._btnLastUsed6.UpControl = (Control) this._colorButton136;
    this._btnLastUsed6.UseVisualStyleBackColor = false;
    this._btnLastUsed6.Leave += new EventHandler(this._btnLastUsed1_Leave);
    this._btnLastUsed7.Color = Color.White;
    this._btnLastUsed7.Dock = DockStyle.Fill;
    this._btnLastUsed7.DownControl = (Control) this._btnOtherColors;
    this._btnLastUsed7.LeftControl = (Control) this._btnLastUsed6;
    this._btnLastUsed7.Location = new Point(147, 3);
    this._btnLastUsed7.Margin = new Padding(0, 3, 3, 0);
    this._btnLastUsed7.Name = "_btnLastUsed7";
    this._btnLastUsed7.RightControl = (Control) this._btnLastUsed8;
    this._btnLastUsed7.Size = new Size(21, 21);
    this._btnLastUsed7.TabIndex = 36;
    this._colorsToolTip.SetToolTip((Control) this._btnLastUsed7, "Последний использованный нестандартный цвет");
    this._btnLastUsed7.UpControl = (Control) this._colorButton137;
    this._btnLastUsed7.UseVisualStyleBackColor = false;
    this._btnLastUsed7.Leave += new EventHandler(this._btnLastUsed1_Leave);
    this._btnLastUsed8.Color = Color.White;
    this._btnLastUsed8.Dock = DockStyle.Fill;
    this._btnLastUsed8.DownControl = (Control) this._btnOtherColors;
    this._btnLastUsed8.LeftControl = (Control) this._btnLastUsed7;
    this._btnLastUsed8.Location = new Point(171, 3);
    this._btnLastUsed8.Margin = new Padding(0, 3, 3, 0);
    this._btnLastUsed8.Name = "_btnLastUsed8";
    this._btnLastUsed8.RightControl = (Control) this._btnLastUsed9;
    this._btnLastUsed8.Size = new Size(21, 21);
    this._btnLastUsed8.TabIndex = 37;
    this._colorsToolTip.SetToolTip((Control) this._btnLastUsed8, "Последний использованный нестандартный цвет");
    this._btnLastUsed8.UpControl = (Control) this._colorButton138;
    this._btnLastUsed8.UseVisualStyleBackColor = false;
    this._btnLastUsed8.Leave += new EventHandler(this._btnLastUsed1_Leave);
    this._btnLastUsed9.Color = Color.White;
    this._btnLastUsed9.Dock = DockStyle.Fill;
    this._btnLastUsed9.DownControl = (Control) this._btnOtherColors;
    this._btnLastUsed9.LeftControl = (Control) this._btnLastUsed8;
    this._btnLastUsed9.Location = new Point(195, 3);
    this._btnLastUsed9.Margin = new Padding(0, 3, 3, 0);
    this._btnLastUsed9.Name = "_btnLastUsed9";
    this._btnLastUsed9.RightControl = (Control) this._btnLastUsed10;
    this._btnLastUsed9.Size = new Size(21, 21);
    this._btnLastUsed9.TabIndex = 38;
    this._colorsToolTip.SetToolTip((Control) this._btnLastUsed9, "Последний использованный нестандартный цвет");
    this._btnLastUsed9.UpControl = (Control) this._colorButton139;
    this._btnLastUsed9.UseVisualStyleBackColor = false;
    this._btnLastUsed9.Leave += new EventHandler(this._btnLastUsed1_Leave);
    this._btnLastUsed10.Color = Color.White;
    this._btnLastUsed10.Dock = DockStyle.Fill;
    this._btnLastUsed10.DownControl = (Control) this._btnOtherColors;
    this._btnLastUsed10.LeftControl = (Control) this._btnLastUsed9;
    this._btnLastUsed10.Location = new Point(219, 3);
    this._btnLastUsed10.Margin = new Padding(0, 3, 3, 0);
    this._btnLastUsed10.Name = "_btnLastUsed10";
    this._btnLastUsed10.Size = new Size(21, 21);
    this._btnLastUsed10.TabIndex = 39;
    this._colorsToolTip.SetToolTip((Control) this._btnLastUsed10, "Последний использованный нестандартный цвет");
    this._btnLastUsed10.UpControl = (Control) this._colorButton140;
    this._btnLastUsed10.UseVisualStyleBackColor = false;
    this._btnLastUsed10.OnNavigateToRight += new OnNavigateDelegate(this._colorButton80_OnNavigateToRight);
    this._btnLastUsed10.Leave += new EventHandler(this._btnLastUsed1_Leave);
    this._colorButton140.Color = Color.FromArgb(112 /*0x70*/, 48 /*0x30*/, 160 /*0xA0*/);
    this._colorButton140.Dock = DockStyle.Fill;
    this._colorButton140.DownControl = (Control) this._btnLastUsed10;
    this._colorButton140.LeftControl = (Control) this._colorButton139;
    this._colorButton140.Location = new Point(219, 129);
    this._colorButton140.Margin = new Padding(0, 3, 3, 0);
    this._colorButton140.Name = "_colorButton140";
    this._colorButton140.Size = new Size(21, 21);
    this._colorButton140.TabIndex = 29;
    this._colorsToolTip.SetToolTip((Control) this._colorButton140, "Лиловый");
    this._colorButton140.UpControl = (Control) this._colorButton129;
    this._colorButton140.UseVisualStyleBackColor = false;
    this._colorButton140.OnNavigateToDown += new OnNavigateDelegate(this._colorButton131_OnNavigateToDown);
    this._colorButton140.OnNavigateToRight += new OnNavigateDelegate(this._colorButton80_OnNavigateToRight);
    this._colorButton139.Color = Color.FromArgb(0, 32 /*0x20*/, 96 /*0x60*/);
    this._colorButton139.Dock = DockStyle.Fill;
    this._colorButton139.DownControl = (Control) this._btnLastUsed9;
    this._colorButton139.LeftControl = (Control) this._colorButton138;
    this._colorButton139.Location = new Point(195, 129);
    this._colorButton139.Margin = new Padding(0, 3, 3, 0);
    this._colorButton139.Name = "_colorButton139";
    this._colorButton139.RightControl = (Control) this._colorButton140;
    this._colorButton139.Size = new Size(21, 21);
    this._colorButton139.TabIndex = 28;
    this._colorsToolTip.SetToolTip((Control) this._colorButton139, "Тёмно-синий");
    this._colorButton139.UpControl = (Control) this._colorButton124;
    this._colorButton139.UseVisualStyleBackColor = false;
    this._colorButton139.OnNavigateToDown += new OnNavigateDelegate(this._colorButton131_OnNavigateToDown);
    this._colorButton138.Color = Color.FromArgb(0, 112 /*0x70*/, 192 /*0xC0*/);
    this._colorButton138.Dock = DockStyle.Fill;
    this._colorButton138.DownControl = (Control) this._btnLastUsed8;
    this._colorButton138.LeftControl = (Control) this._colorButton137;
    this._colorButton138.Location = new Point(171, 129);
    this._colorButton138.Margin = new Padding(0, 3, 3, 0);
    this._colorButton138.Name = "_colorButton138";
    this._colorButton138.RightControl = (Control) this._colorButton139;
    this._colorButton138.Size = new Size(21, 21);
    this._colorButton138.TabIndex = 27;
    this._colorsToolTip.SetToolTip((Control) this._colorButton138, "Синий");
    this._colorButton138.UpControl = (Control) this._colorButton119;
    this._colorButton138.UseVisualStyleBackColor = false;
    this._colorButton138.OnNavigateToDown += new OnNavigateDelegate(this._colorButton131_OnNavigateToDown);
    this._colorButton137.Color = Color.FromArgb(0, 176 /*0xB0*/, 240 /*0xF0*/);
    this._colorButton137.Dock = DockStyle.Fill;
    this._colorButton137.DownControl = (Control) this._btnLastUsed7;
    this._colorButton137.LeftControl = (Control) this._colorButton136;
    this._colorButton137.Location = new Point(147, 129);
    this._colorButton137.Margin = new Padding(0, 3, 3, 0);
    this._colorButton137.Name = "_colorButton137";
    this._colorButton137.RightControl = (Control) this._colorButton138;
    this._colorButton137.Size = new Size(21, 21);
    this._colorButton137.TabIndex = 26;
    this._colorsToolTip.SetToolTip((Control) this._colorButton137, "Светло-синий");
    this._colorButton137.UpControl = (Control) this._colorButton114;
    this._colorButton137.UseVisualStyleBackColor = false;
    this._colorButton137.OnNavigateToDown += new OnNavigateDelegate(this._colorButton131_OnNavigateToDown);
    this._colorButton136.Color = Color.FromArgb(0, 176 /*0xB0*/, 80 /*0x50*/);
    this._colorButton136.Dock = DockStyle.Fill;
    this._colorButton136.DownControl = (Control) this._btnLastUsed6;
    this._colorButton136.LeftControl = (Control) this._colorButton135;
    this._colorButton136.Location = new Point(123, 129);
    this._colorButton136.Margin = new Padding(0, 3, 3, 0);
    this._colorButton136.Name = "_colorButton136";
    this._colorButton136.RightControl = (Control) this._colorButton137;
    this._colorButton136.Size = new Size(21, 21);
    this._colorButton136.TabIndex = 25;
    this._colorsToolTip.SetToolTip((Control) this._colorButton136, "Зелёный");
    this._colorButton136.UpControl = (Control) this._colorButton109;
    this._colorButton136.UseVisualStyleBackColor = false;
    this._colorButton136.OnNavigateToDown += new OnNavigateDelegate(this._colorButton131_OnNavigateToDown);
    this._colorButton135.Color = Color.FromArgb(146, 208 /*0xD0*/, 80 /*0x50*/);
    this._colorButton135.Dock = DockStyle.Fill;
    this._colorButton135.DownControl = (Control) this._btnLastUsed5;
    this._colorButton135.LeftControl = (Control) this._colorButton134;
    this._colorButton135.Location = new Point(99, 129);
    this._colorButton135.Margin = new Padding(0, 3, 3, 0);
    this._colorButton135.Name = "_colorButton135";
    this._colorButton135.RightControl = (Control) this._colorButton136;
    this._colorButton135.Size = new Size(21, 21);
    this._colorButton135.TabIndex = 24;
    this._colorsToolTip.SetToolTip((Control) this._colorButton135, "Светло-зелёный");
    this._colorButton135.UpControl = (Control) this._colorButton104;
    this._colorButton135.UseVisualStyleBackColor = false;
    this._colorButton135.OnNavigateToDown += new OnNavigateDelegate(this._colorButton131_OnNavigateToDown);
    this._colorButton134.Color = Color.Yellow;
    this._colorButton134.Dock = DockStyle.Fill;
    this._colorButton134.DownControl = (Control) this._btnLastUsed4;
    this._colorButton134.LeftControl = (Control) this._colorButton133;
    this._colorButton134.Location = new Point(75, 129);
    this._colorButton134.Margin = new Padding(0, 3, 3, 0);
    this._colorButton134.Name = "_colorButton134";
    this._colorButton134.RightControl = (Control) this._colorButton135;
    this._colorButton134.Size = new Size(21, 21);
    this._colorButton134.TabIndex = 23;
    this._colorsToolTip.SetToolTip((Control) this._colorButton134, "Жёлтый");
    this._colorButton134.UpControl = (Control) this._colorButton99;
    this._colorButton134.UseVisualStyleBackColor = false;
    this._colorButton134.OnNavigateToDown += new OnNavigateDelegate(this._colorButton131_OnNavigateToDown);
    this._colorButton133.Color = Color.FromArgb((int) byte.MaxValue, 192 /*0xC0*/, 0);
    this._colorButton133.Dock = DockStyle.Fill;
    this._colorButton133.DownControl = (Control) this._btnLastUsed3;
    this._colorButton133.LeftControl = (Control) this._colorButton132;
    this._colorButton133.Location = new Point(51, 129);
    this._colorButton133.Margin = new Padding(0, 3, 3, 0);
    this._colorButton133.Name = "_colorButton133";
    this._colorButton133.RightControl = (Control) this._colorButton134;
    this._colorButton133.Size = new Size(21, 21);
    this._colorButton133.TabIndex = 22;
    this._colorsToolTip.SetToolTip((Control) this._colorButton133, "Оранжевый");
    this._colorButton133.UpControl = (Control) this._colorButton94;
    this._colorButton133.UseVisualStyleBackColor = false;
    this._colorButton133.OnNavigateToDown += new OnNavigateDelegate(this._colorButton131_OnNavigateToDown);
    this._colorButton132.Dock = DockStyle.Fill;
    this._colorButton132.DownControl = (Control) this._btnLastUsed2;
    this._colorButton132.LeftControl = (Control) this._colorButton131;
    this._colorButton132.Location = new Point(27, 129);
    this._colorButton132.Margin = new Padding(0, 3, 3, 0);
    this._colorButton132.Name = "_colorButton132";
    this._colorButton132.RightControl = (Control) this._colorButton133;
    this._colorButton132.Size = new Size(21, 21);
    this._colorButton132.TabIndex = 21;
    this._colorsToolTip.SetToolTip((Control) this._colorButton132, "Красный");
    this._colorButton132.UpControl = (Control) this._colorButton89;
    this._colorButton132.UseVisualStyleBackColor = false;
    this._colorButton132.OnNavigateToDown += new OnNavigateDelegate(this._colorButton131_OnNavigateToDown);
    this._colorButton89.Borders = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton89.Color = Color.FromArgb(12, 12, 12);
    this._colorButton89.Dock = DockStyle.Top;
    this._colorButton89.DownControl = (Control) this._colorButton132;
    this._colorButton89.LeftControl = (Control) this._colorButton84;
    this._colorButton89.Location = new Point(0, 83);
    this._colorButton89.Name = "_colorButton89";
    this._colorButton89.RightControl = (Control) this._colorButton94;
    this._colorButton89.Size = new Size(21, 20);
    this._colorButton89.TabIndex = 4;
    this._colorsToolTip.SetToolTip((Control) this._colorButton89, "Чёрный, более светлый оттенок 5%");
    this._colorButton89.UpControl = (Control) this._colorButton88;
    this._colorButton89.UseVisualStyleBackColor = false;
    this._colorButton94.Borders = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton94.Color = Color.FromArgb(23, 22, 22);
    this._colorButton94.Dock = DockStyle.Top;
    this._colorButton94.DownControl = (Control) this._colorButton133;
    this._colorButton94.LeftControl = (Control) this._colorButton89;
    this._colorButton94.Location = new Point(0, 83);
    this._colorButton94.Name = "_colorButton94";
    this._colorButton94.RightControl = (Control) this._colorButton99;
    this._colorButton94.Size = new Size(21, 20);
    this._colorButton94.TabIndex = 4;
    this._colorsToolTip.SetToolTip((Control) this._colorButton94, "Серый 25%, более тёмный оттенок 90%");
    this._colorButton94.UpControl = (Control) this._colorButton93;
    this._colorButton94.UseVisualStyleBackColor = false;
    this._colorButton99.Borders = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton99.Color = Color.FromArgb(34, 42, 53);
    this._colorButton99.Dock = DockStyle.Top;
    this._colorButton99.DownControl = (Control) this._colorButton134;
    this._colorButton99.LeftControl = (Control) this._colorButton94;
    this._colorButton99.Location = new Point(0, 83);
    this._colorButton99.Name = "_colorButton99";
    this._colorButton99.RightControl = (Control) this._colorButton104;
    this._colorButton99.Size = new Size(21, 20);
    this._colorButton99.TabIndex = 4;
    this._colorsToolTip.SetToolTip((Control) this._colorButton99, "Сизый, более тёмный оттенок 50%");
    this._colorButton99.UpControl = (Control) this._colorButton98;
    this._colorButton99.UseVisualStyleBackColor = false;
    this._colorButton104.Borders = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton104.Color = Color.FromArgb(30, 78, 121);
    this._colorButton104.Dock = DockStyle.Top;
    this._colorButton104.DownControl = (Control) this._colorButton135;
    this._colorButton104.LeftControl = (Control) this._colorButton99;
    this._colorButton104.Location = new Point(0, 83);
    this._colorButton104.Name = "_colorButton104";
    this._colorButton104.RightControl = (Control) this._colorButton109;
    this._colorButton104.Size = new Size(21, 20);
    this._colorButton104.TabIndex = 4;
    this._colorsToolTip.SetToolTip((Control) this._colorButton104, "Синий, более тёмный оттенок 50%");
    this._colorButton104.UpControl = (Control) this.colorButton2;
    this._colorButton104.UseVisualStyleBackColor = false;
    this._colorButton109.Borders = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton109.Color = Color.FromArgb(131, 60, 11);
    this._colorButton109.Dock = DockStyle.Top;
    this._colorButton109.DownControl = (Control) this._colorButton136;
    this._colorButton109.LeftControl = (Control) this._colorButton104;
    this._colorButton109.Location = new Point(0, 83);
    this._colorButton109.Name = "_colorButton109";
    this._colorButton109.RightControl = (Control) this._colorButton114;
    this._colorButton109.Size = new Size(21, 20);
    this._colorButton109.TabIndex = 4;
    this._colorsToolTip.SetToolTip((Control) this._colorButton109, "Оранжевый, более тёмный оттенок 50%");
    this._colorButton109.UpControl = (Control) this._colorButton108;
    this._colorButton109.UseVisualStyleBackColor = false;
    this._colorButton114.Borders = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton114.Color = Color.FromArgb(82, 82, 82);
    this._colorButton114.Dock = DockStyle.Top;
    this._colorButton114.DownControl = (Control) this._colorButton137;
    this._colorButton114.LeftControl = (Control) this._colorButton109;
    this._colorButton114.Location = new Point(0, 83);
    this._colorButton114.Name = "_colorButton114";
    this._colorButton114.RightControl = (Control) this._colorButton119;
    this._colorButton114.Size = new Size(21, 20);
    this._colorButton114.TabIndex = 4;
    this._colorsToolTip.SetToolTip((Control) this._colorButton114, "Серый 50%, более тёмный оттенок 50%");
    this._colorButton114.UpControl = (Control) this._colorButton113;
    this._colorButton114.UseVisualStyleBackColor = false;
    this._colorButton119.Borders = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton119.Color = Color.FromArgb((int) sbyte.MaxValue, 96 /*0x60*/, 0);
    this._colorButton119.Dock = DockStyle.Top;
    this._colorButton119.DownControl = (Control) this._colorButton138;
    this._colorButton119.LeftControl = (Control) this._colorButton114;
    this._colorButton119.Location = new Point(0, 83);
    this._colorButton119.Name = "_colorButton119";
    this._colorButton119.RightControl = (Control) this._colorButton124;
    this._colorButton119.Size = new Size(21, 20);
    this._colorButton119.TabIndex = 4;
    this._colorsToolTip.SetToolTip((Control) this._colorButton119, "Золотистый, более тёмный оттенок 50%");
    this._colorButton119.UpControl = (Control) this._colorButton118;
    this._colorButton119.UseVisualStyleBackColor = false;
    this._colorButton124.Borders = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton124.Color = Color.FromArgb(31 /*0x1F*/, 56, 100);
    this._colorButton124.Dock = DockStyle.Top;
    this._colorButton124.DownControl = (Control) this._colorButton139;
    this._colorButton124.LeftControl = (Control) this._colorButton119;
    this._colorButton124.Location = new Point(0, 83);
    this._colorButton124.Name = "_colorButton124";
    this._colorButton124.RightControl = (Control) this._colorButton129;
    this._colorButton124.Size = new Size(21, 20);
    this._colorButton124.TabIndex = 4;
    this._colorsToolTip.SetToolTip((Control) this._colorButton124, "Синий, более тёмный оттенок 50%");
    this._colorButton124.UpControl = (Control) this._colorButton123;
    this._colorButton124.UseVisualStyleBackColor = false;
    this._colorButton129.Borders = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton129.Color = Color.FromArgb(55, 86, 35);
    this._colorButton129.Dock = DockStyle.Top;
    this._colorButton129.DownControl = (Control) this._colorButton140;
    this._colorButton129.LeftControl = (Control) this._colorButton124;
    this._colorButton129.Location = new Point(0, 83);
    this._colorButton129.Name = "_colorButton129";
    this._colorButton129.Size = new Size(21, 20);
    this._colorButton129.TabIndex = 4;
    this._colorsToolTip.SetToolTip((Control) this._colorButton129, "Зелёный, более тёмный оттенок 50%");
    this._colorButton129.UpControl = (Control) this._colorButton128;
    this._colorButton129.UseVisualStyleBackColor = false;
    this._colorButton129.OnNavigateToRight += new OnNavigateDelegate(this._colorButton80_OnNavigateToRight);
    this._colorButton128.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton128.Color = Color.FromArgb(83, 129, 53);
    this._colorButton128.Dock = DockStyle.Top;
    this._colorButton128.DownControl = (Control) this._colorButton129;
    this._colorButton128.LeftControl = (Control) this._colorButton123;
    this._colorButton128.Location = new Point(0, 63 /*0x3F*/);
    this._colorButton128.Name = "_colorButton128";
    this._colorButton128.Size = new Size(21, 20);
    this._colorButton128.TabIndex = 3;
    this._colorsToolTip.SetToolTip((Control) this._colorButton128, "Зелёный, более тёмный оттенок 25%");
    this._colorButton128.UpControl = (Control) this._colorButton127;
    this._colorButton128.UseVisualStyleBackColor = false;
    this._colorButton128.OnNavigateToRight += new OnNavigateDelegate(this._colorButton80_OnNavigateToRight);
    this._colorButton123.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton123.Color = Color.FromArgb(47, 84, 150);
    this._colorButton123.Dock = DockStyle.Top;
    this._colorButton123.DownControl = (Control) this._colorButton124;
    this._colorButton123.LeftControl = (Control) this._colorButton118;
    this._colorButton123.Location = new Point(0, 63 /*0x3F*/);
    this._colorButton123.Name = "_colorButton123";
    this._colorButton123.RightControl = (Control) this._colorButton128;
    this._colorButton123.Size = new Size(21, 20);
    this._colorButton123.TabIndex = 3;
    this._colorsToolTip.SetToolTip((Control) this._colorButton123, "Синий, более тёмный оттенок 25%");
    this._colorButton123.UpControl = (Control) this._colorButton122;
    this._colorButton123.UseVisualStyleBackColor = false;
    this._colorButton118.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton118.Color = Color.FromArgb(191, 144 /*0x90*/, 0);
    this._colorButton118.Dock = DockStyle.Top;
    this._colorButton118.DownControl = (Control) this._colorButton119;
    this._colorButton118.LeftControl = (Control) this._colorButton113;
    this._colorButton118.Location = new Point(0, 63 /*0x3F*/);
    this._colorButton118.Name = "_colorButton118";
    this._colorButton118.RightControl = (Control) this._colorButton123;
    this._colorButton118.Size = new Size(21, 20);
    this._colorButton118.TabIndex = 3;
    this._colorsToolTip.SetToolTip((Control) this._colorButton118, "Золотистый, более тёмный оттенок 25%");
    this._colorButton118.UpControl = (Control) this._colorButton117;
    this._colorButton118.UseVisualStyleBackColor = false;
    this._colorButton113.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton113.Color = Color.FromArgb(123, 123, 123);
    this._colorButton113.Dock = DockStyle.Top;
    this._colorButton113.DownControl = (Control) this._colorButton114;
    this._colorButton113.LeftControl = (Control) this._colorButton108;
    this._colorButton113.Location = new Point(0, 63 /*0x3F*/);
    this._colorButton113.Name = "_colorButton113";
    this._colorButton113.RightControl = (Control) this._colorButton118;
    this._colorButton113.Size = new Size(21, 20);
    this._colorButton113.TabIndex = 3;
    this._colorsToolTip.SetToolTip((Control) this._colorButton113, "Серый 50%, более тёмный оттенок 25%");
    this._colorButton113.UpControl = (Control) this._colorButton112;
    this._colorButton113.UseVisualStyleBackColor = false;
    this._colorButton108.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton108.Color = Color.FromArgb(197, 90, 17);
    this._colorButton108.Dock = DockStyle.Top;
    this._colorButton108.DownControl = (Control) this._colorButton109;
    this._colorButton108.LeftControl = (Control) this.colorButton2;
    this._colorButton108.Location = new Point(0, 63 /*0x3F*/);
    this._colorButton108.Name = "_colorButton108";
    this._colorButton108.RightControl = (Control) this._colorButton113;
    this._colorButton108.Size = new Size(21, 20);
    this._colorButton108.TabIndex = 3;
    this._colorsToolTip.SetToolTip((Control) this._colorButton108, "Оранжевый, более тёмный оттенок 25%");
    this._colorButton108.UpControl = (Control) this._colorButton107;
    this._colorButton108.UseVisualStyleBackColor = false;
    this.colorButton2.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this.colorButton2.Color = Color.FromArgb(46, 117, 181);
    this.colorButton2.Dock = DockStyle.Top;
    this.colorButton2.DownControl = (Control) this._colorButton104;
    this.colorButton2.LeftControl = (Control) this._colorButton98;
    this.colorButton2.Location = new Point(0, 63 /*0x3F*/);
    this.colorButton2.Name = "colorButton2";
    this.colorButton2.RightControl = (Control) this._colorButton108;
    this.colorButton2.Size = new Size(21, 20);
    this.colorButton2.TabIndex = 3;
    this._colorsToolTip.SetToolTip((Control) this.colorButton2, "Синий, более тёмный оттенок 25%");
    this.colorButton2.UpControl = (Control) this.colorButton3;
    this.colorButton2.UseVisualStyleBackColor = false;
    this._colorButton98.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton98.Color = Color.FromArgb(50, 63 /*0x3F*/, 79);
    this._colorButton98.Dock = DockStyle.Top;
    this._colorButton98.DownControl = (Control) this._colorButton99;
    this._colorButton98.LeftControl = (Control) this._colorButton93;
    this._colorButton98.Location = new Point(0, 63 /*0x3F*/);
    this._colorButton98.Name = "_colorButton98";
    this._colorButton98.RightControl = (Control) this.colorButton2;
    this._colorButton98.Size = new Size(21, 20);
    this._colorButton98.TabIndex = 3;
    this._colorsToolTip.SetToolTip((Control) this._colorButton98, "Сизый, более тёмный оттенок 25%");
    this._colorButton98.UpControl = (Control) this._colorButton97;
    this._colorButton98.UseVisualStyleBackColor = false;
    this._colorButton93.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton93.Color = Color.FromArgb(58, 56, 56);
    this._colorButton93.Dock = DockStyle.Top;
    this._colorButton93.DownControl = (Control) this._colorButton94;
    this._colorButton93.LeftControl = (Control) this._colorButton88;
    this._colorButton93.Location = new Point(0, 63 /*0x3F*/);
    this._colorButton93.Name = "_colorButton93";
    this._colorButton93.RightControl = (Control) this._colorButton98;
    this._colorButton93.Size = new Size(21, 20);
    this._colorButton93.TabIndex = 3;
    this._colorsToolTip.SetToolTip((Control) this._colorButton93, "Серый 25%, более тёмный оттенок 75%");
    this._colorButton93.UpControl = (Control) this._colorButton92;
    this._colorButton93.UseVisualStyleBackColor = false;
    this._colorButton88.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton88.Color = Color.FromArgb(38, 38, 38);
    this._colorButton88.Dock = DockStyle.Top;
    this._colorButton88.DownControl = (Control) this._colorButton89;
    this._colorButton88.LeftControl = (Control) this._colorButton83;
    this._colorButton88.Location = new Point(0, 63 /*0x3F*/);
    this._colorButton88.Name = "_colorButton88";
    this._colorButton88.RightControl = (Control) this._colorButton93;
    this._colorButton88.Size = new Size(21, 20);
    this._colorButton88.TabIndex = 3;
    this._colorsToolTip.SetToolTip((Control) this._colorButton88, "Чёрный, более светлый оттенок 15%");
    this._colorButton88.UpControl = (Control) this._colorButton87;
    this._colorButton88.UseVisualStyleBackColor = false;
    this._colorButton87.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton87.Color = Color.FromArgb(63 /*0x3F*/, 63 /*0x3F*/, 63 /*0x3F*/);
    this._colorButton87.Dock = DockStyle.Top;
    this._colorButton87.DownControl = (Control) this._colorButton88;
    this._colorButton87.LeftControl = (Control) this._colorButton82;
    this._colorButton87.Location = new Point(0, 43);
    this._colorButton87.Name = "_colorButton87";
    this._colorButton87.RightControl = (Control) this._colorButton92;
    this._colorButton87.Size = new Size(21, 20);
    this._colorButton87.TabIndex = 2;
    this._colorsToolTip.SetToolTip((Control) this._colorButton87, "Чёрный, более светлый оттенок 25%");
    this._colorButton87.UpControl = (Control) this._colorButton86;
    this._colorButton87.UseVisualStyleBackColor = false;
    this._colorButton92.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton92.Color = Color.FromArgb(117, 112 /*0x70*/, 112 /*0x70*/);
    this._colorButton92.Dock = DockStyle.Top;
    this._colorButton92.DownControl = (Control) this._colorButton93;
    this._colorButton92.LeftControl = (Control) this._colorButton87;
    this._colorButton92.Location = new Point(0, 43);
    this._colorButton92.Name = "_colorButton92";
    this._colorButton92.RightControl = (Control) this._colorButton97;
    this._colorButton92.Size = new Size(21, 20);
    this._colorButton92.TabIndex = 2;
    this._colorsToolTip.SetToolTip((Control) this._colorButton92, "Серый 25%, более тёмный оттенок 50%");
    this._colorButton92.UpControl = (Control) this._colorButton91;
    this._colorButton92.UseVisualStyleBackColor = false;
    this._colorButton97.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton97.Color = Color.FromArgb(132, 150, 176 /*0xB0*/);
    this._colorButton97.Dock = DockStyle.Top;
    this._colorButton97.DownControl = (Control) this._colorButton98;
    this._colorButton97.LeftControl = (Control) this._colorButton92;
    this._colorButton97.Location = new Point(0, 43);
    this._colorButton97.Name = "_colorButton97";
    this._colorButton97.RightControl = (Control) this.colorButton3;
    this._colorButton97.Size = new Size(21, 20);
    this._colorButton97.TabIndex = 2;
    this._colorsToolTip.SetToolTip((Control) this._colorButton97, "Сизый, более светлый оттенок 40%");
    this._colorButton97.UpControl = (Control) this._colorButton96;
    this._colorButton97.UseVisualStyleBackColor = false;
    this.colorButton3.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this.colorButton3.Color = Color.FromArgb(156, 195, 229);
    this.colorButton3.Dock = DockStyle.Top;
    this.colorButton3.DownControl = (Control) this.colorButton2;
    this.colorButton3.LeftControl = (Control) this._colorButton97;
    this.colorButton3.Location = new Point(0, 43);
    this.colorButton3.Name = "colorButton3";
    this.colorButton3.RightControl = (Control) this._colorButton107;
    this.colorButton3.Size = new Size(21, 20);
    this.colorButton3.TabIndex = 2;
    this._colorsToolTip.SetToolTip((Control) this.colorButton3, "Синий, более светлый оттенок 40%");
    this.colorButton3.UpControl = (Control) this.colorButton4;
    this.colorButton3.UseVisualStyleBackColor = false;
    this._colorButton107.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton107.Color = Color.FromArgb(244, 177, 131);
    this._colorButton107.Controls.Add((Control) this.colorButton5);
    this._colorButton107.Dock = DockStyle.Top;
    this._colorButton107.DownControl = (Control) this._colorButton108;
    this._colorButton107.LeftControl = (Control) this.colorButton3;
    this._colorButton107.Location = new Point(0, 43);
    this._colorButton107.Name = "_colorButton107";
    this._colorButton107.RightControl = (Control) this._colorButton112;
    this._colorButton107.Size = new Size(21, 20);
    this._colorButton107.TabIndex = 2;
    this._colorsToolTip.SetToolTip((Control) this._colorButton107, "Оранжевый, более светлый оттенок 40%");
    this._colorButton107.UpControl = (Control) this._colorButton106;
    this._colorButton107.UseVisualStyleBackColor = false;
    this.colorButton5.Anchor = AnchorStyles.Left | AnchorStyles.Right;
    this.colorButton5.Borders = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.colorButton5.Color = Color.FromArgb(131, 60, 11);
    this.colorButton5.Location = new Point(0, 35);
    this.colorButton5.Name = "colorButton5";
    this.colorButton5.Size = new Size(20, 18);
    this.colorButton5.TabIndex = 4;
    this.colorButton5.UseVisualStyleBackColor = false;
    this._colorButton112.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton112.Color = Color.FromArgb(201, 201, 201);
    this._colorButton112.Dock = DockStyle.Top;
    this._colorButton112.DownControl = (Control) this._colorButton113;
    this._colorButton112.LeftControl = (Control) this._colorButton107;
    this._colorButton112.Location = new Point(0, 43);
    this._colorButton112.Name = "_colorButton112";
    this._colorButton112.RightControl = (Control) this._colorButton117;
    this._colorButton112.Size = new Size(21, 20);
    this._colorButton112.TabIndex = 2;
    this._colorsToolTip.SetToolTip((Control) this._colorButton112, "Серый 50%, более светлый оттенок 40%");
    this._colorButton112.UpControl = (Control) this._colorButton111;
    this._colorButton112.UseVisualStyleBackColor = false;
    this._colorButton117.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton117.Color = Color.FromArgb((int) byte.MaxValue, 217, 101);
    this._colorButton117.Dock = DockStyle.Top;
    this._colorButton117.DownControl = (Control) this._colorButton118;
    this._colorButton117.LeftControl = (Control) this._colorButton112;
    this._colorButton117.Location = new Point(0, 43);
    this._colorButton117.Name = "_colorButton117";
    this._colorButton117.RightControl = (Control) this._colorButton122;
    this._colorButton117.Size = new Size(21, 20);
    this._colorButton117.TabIndex = 2;
    this._colorsToolTip.SetToolTip((Control) this._colorButton117, "Золотистый, более светлый оттенок 40%");
    this._colorButton117.UpControl = (Control) this._colorButton116;
    this._colorButton117.UseVisualStyleBackColor = false;
    this._colorButton122.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton122.Color = Color.FromArgb(142, 170, 219);
    this._colorButton122.Dock = DockStyle.Top;
    this._colorButton122.DownControl = (Control) this._colorButton123;
    this._colorButton122.LeftControl = (Control) this._colorButton117;
    this._colorButton122.Location = new Point(0, 43);
    this._colorButton122.Name = "_colorButton122";
    this._colorButton122.RightControl = (Control) this._colorButton127;
    this._colorButton122.Size = new Size(21, 20);
    this._colorButton122.TabIndex = 2;
    this._colorsToolTip.SetToolTip((Control) this._colorButton122, "Синий, более светлый оттенок 40%");
    this._colorButton122.UpControl = (Control) this._colorButton121;
    this._colorButton122.UseVisualStyleBackColor = false;
    this._colorButton127.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton127.Color = Color.FromArgb(168, 208 /*0xD0*/, 141);
    this._colorButton127.Dock = DockStyle.Top;
    this._colorButton127.DownControl = (Control) this._colorButton128;
    this._colorButton127.LeftControl = (Control) this._colorButton122;
    this._colorButton127.Location = new Point(0, 43);
    this._colorButton127.Name = "_colorButton127";
    this._colorButton127.Size = new Size(21, 20);
    this._colorButton127.TabIndex = 2;
    this._colorsToolTip.SetToolTip((Control) this._colorButton127, "Зелёный, более светлый оттенок 40%");
    this._colorButton127.UpControl = (Control) this._colorButton126;
    this._colorButton127.UseVisualStyleBackColor = false;
    this._colorButton127.OnNavigateToRight += new OnNavigateDelegate(this._colorButton80_OnNavigateToRight);
    this._colorButton126.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton126.Color = Color.FromArgb(197, 224 /*0xE0*/, 179);
    this._colorButton126.Dock = DockStyle.Top;
    this._colorButton126.DownControl = (Control) this._colorButton127;
    this._colorButton126.LeftControl = (Control) this._colorButton121;
    this._colorButton126.Location = new Point(0, 23);
    this._colorButton126.Name = "_colorButton126";
    this._colorButton126.Size = new Size(21, 20);
    this._colorButton126.TabIndex = 1;
    this._colorsToolTip.SetToolTip((Control) this._colorButton126, "Зелёный, более светлый оттенок 60%");
    this._colorButton126.UpControl = (Control) this._colorButton130;
    this._colorButton126.UseVisualStyleBackColor = false;
    this._colorButton126.OnNavigateToRight += new OnNavigateDelegate(this._colorButton80_OnNavigateToRight);
    this._colorButton121.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton121.Color = Color.FromArgb(180, 198, 231);
    this._colorButton121.Dock = DockStyle.Top;
    this._colorButton121.DownControl = (Control) this._colorButton122;
    this._colorButton121.LeftControl = (Control) this._colorButton116;
    this._colorButton121.Location = new Point(0, 23);
    this._colorButton121.Name = "_colorButton121";
    this._colorButton121.RightControl = (Control) this._colorButton126;
    this._colorButton121.Size = new Size(21, 20);
    this._colorButton121.TabIndex = 1;
    this._colorsToolTip.SetToolTip((Control) this._colorButton121, "Синий, более светлый оттенок 60%");
    this._colorButton121.UpControl = (Control) this._colorButton125;
    this._colorButton121.UseVisualStyleBackColor = false;
    this._colorButton116.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton116.Color = Color.FromArgb(254, 229, 153);
    this._colorButton116.Dock = DockStyle.Top;
    this._colorButton116.DownControl = (Control) this._colorButton117;
    this._colorButton116.LeftControl = (Control) this._colorButton111;
    this._colorButton116.Location = new Point(0, 23);
    this._colorButton116.Name = "_colorButton116";
    this._colorButton116.RightControl = (Control) this._colorButton121;
    this._colorButton116.Size = new Size(21, 20);
    this._colorButton116.TabIndex = 1;
    this._colorsToolTip.SetToolTip((Control) this._colorButton116, "Золотистый, более светлый оттенок 60%");
    this._colorButton116.UpControl = (Control) this._colorButton120;
    this._colorButton116.UseVisualStyleBackColor = false;
    this._colorButton111.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton111.Color = Color.FromArgb(219, 219, 219);
    this._colorButton111.Dock = DockStyle.Top;
    this._colorButton111.DownControl = (Control) this._colorButton112;
    this._colorButton111.LeftControl = (Control) this._colorButton106;
    this._colorButton111.Location = new Point(0, 23);
    this._colorButton111.Name = "_colorButton111";
    this._colorButton111.RightControl = (Control) this._colorButton116;
    this._colorButton111.Size = new Size(21, 20);
    this._colorButton111.TabIndex = 1;
    this._colorsToolTip.SetToolTip((Control) this._colorButton111, "Серый 50%, более светлый оттенок 60%");
    this._colorButton111.UpControl = (Control) this._colorButton115;
    this._colorButton111.UseVisualStyleBackColor = false;
    this._colorButton106.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton106.Color = Color.FromArgb(247, 203, 172);
    this._colorButton106.Dock = DockStyle.Top;
    this._colorButton106.DownControl = (Control) this._colorButton107;
    this._colorButton106.LeftControl = (Control) this.colorButton4;
    this._colorButton106.Location = new Point(0, 23);
    this._colorButton106.Name = "_colorButton106";
    this._colorButton106.RightControl = (Control) this._colorButton111;
    this._colorButton106.Size = new Size(21, 20);
    this._colorButton106.TabIndex = 1;
    this._colorsToolTip.SetToolTip((Control) this._colorButton106, "Оранжевый, более светлый оттенок 60%");
    this._colorButton106.UpControl = (Control) this._colorButton110;
    this._colorButton106.UseVisualStyleBackColor = false;
    this.colorButton4.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this.colorButton4.Color = Color.FromArgb(189, 215, 238);
    this.colorButton4.Dock = DockStyle.Top;
    this.colorButton4.DownControl = (Control) this.colorButton3;
    this.colorButton4.LeftControl = (Control) this._colorButton96;
    this.colorButton4.Location = new Point(0, 23);
    this.colorButton4.Name = "colorButton4";
    this.colorButton4.RightControl = (Control) this._colorButton106;
    this.colorButton4.Size = new Size(21, 20);
    this.colorButton4.TabIndex = 1;
    this._colorsToolTip.SetToolTip((Control) this.colorButton4, "Синий, более светлый оттенок 60%");
    this.colorButton4.UpControl = (Control) this._colorButton105;
    this.colorButton4.UseVisualStyleBackColor = false;
    this._colorButton96.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton96.Color = Color.FromArgb(173, 185, 202);
    this._colorButton96.Dock = DockStyle.Top;
    this._colorButton96.DownControl = (Control) this._colorButton97;
    this._colorButton96.LeftControl = (Control) this._colorButton91;
    this._colorButton96.Location = new Point(0, 23);
    this._colorButton96.Name = "_colorButton96";
    this._colorButton96.RightControl = (Control) this.colorButton4;
    this._colorButton96.Size = new Size(21, 20);
    this._colorButton96.TabIndex = 1;
    this._colorsToolTip.SetToolTip((Control) this._colorButton96, "Сизый, более светлый оттенок 60%");
    this._colorButton96.UpControl = (Control) this._colorButton100;
    this._colorButton96.UseVisualStyleBackColor = false;
    this._colorButton91.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton91.Color = Color.FromArgb(174, 171, 171);
    this._colorButton91.Dock = DockStyle.Top;
    this._colorButton91.DownControl = (Control) this._colorButton92;
    this._colorButton91.LeftControl = (Control) this._colorButton86;
    this._colorButton91.Location = new Point(0, 23);
    this._colorButton91.Name = "_colorButton91";
    this._colorButton91.RightControl = (Control) this._colorButton96;
    this._colorButton91.Size = new Size(21, 20);
    this._colorButton91.TabIndex = 1;
    this._colorsToolTip.SetToolTip((Control) this._colorButton91, "Серый 25%, более тёмный оттенок 25%");
    this._colorButton91.UpControl = (Control) this._colorButton95;
    this._colorButton91.UseVisualStyleBackColor = false;
    this._colorButton86.Borders = AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton86.Color = Color.FromArgb(89, 89, 89);
    this._colorButton86.Dock = DockStyle.Top;
    this._colorButton86.DownControl = (Control) this._colorButton87;
    this._colorButton86.LeftControl = (Control) this._colorButton81;
    this._colorButton86.Location = new Point(0, 23);
    this._colorButton86.Name = "_colorButton86";
    this._colorButton86.RightControl = (Control) this._colorButton91;
    this._colorButton86.Size = new Size(21, 20);
    this._colorButton86.TabIndex = 1;
    this._colorsToolTip.SetToolTip((Control) this._colorButton86, "Чёрный, более светлый оттенок 35%");
    this._colorButton86.UpControl = (Control) this._colorButton90;
    this._colorButton86.UseVisualStyleBackColor = false;
    this._colorButton90.Borders = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton90.Color = Color.FromArgb((int) sbyte.MaxValue, (int) sbyte.MaxValue, (int) sbyte.MaxValue);
    this._colorButton90.Dock = DockStyle.Top;
    this._colorButton90.DownControl = (Control) this._colorButton86;
    this._colorButton90.LeftControl = (Control) this._colorButton85;
    this._colorButton90.Location = new Point(0, 3);
    this._colorButton90.Name = "_colorButton90";
    this._colorButton90.RightControl = (Control) this._colorButton95;
    this._colorButton90.Size = new Size(21, 20);
    this._colorButton90.TabIndex = 0;
    this._colorsToolTip.SetToolTip((Control) this._colorButton90, "Чёрный, более светлый оттенок 50%");
    this._colorButton90.UpControl = (Control) this._colorButton72;
    this._colorButton90.UseVisualStyleBackColor = false;
    this._colorButton95.Borders = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton95.Color = Color.FromArgb(208 /*0xD0*/, 206, 206);
    this._colorButton95.Dock = DockStyle.Top;
    this._colorButton95.DownControl = (Control) this._colorButton91;
    this._colorButton95.LeftControl = (Control) this._colorButton90;
    this._colorButton95.Location = new Point(0, 3);
    this._colorButton95.Name = "_colorButton95";
    this._colorButton95.RightControl = (Control) this._colorButton100;
    this._colorButton95.Size = new Size(21, 20);
    this._colorButton95.TabIndex = 0;
    this._colorsToolTip.SetToolTip((Control) this._colorButton95, "Серый 25%, более тёмный оттенок 10%");
    this._colorButton95.UpControl = (Control) this._colorButton73;
    this._colorButton95.UseVisualStyleBackColor = false;
    this._colorButton100.Borders = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton100.Color = Color.FromArgb(214, 220, 228);
    this._colorButton100.Dock = DockStyle.Top;
    this._colorButton100.DownControl = (Control) this._colorButton96;
    this._colorButton100.LeftControl = (Control) this._colorButton95;
    this._colorButton100.Location = new Point(0, 3);
    this._colorButton100.Name = "_colorButton100";
    this._colorButton100.RightControl = (Control) this._colorButton105;
    this._colorButton100.Size = new Size(21, 20);
    this._colorButton100.TabIndex = 0;
    this._colorsToolTip.SetToolTip((Control) this._colorButton100, "Сизый, более светлый оттенок 80%");
    this._colorButton100.UpControl = (Control) this._colorButton74;
    this._colorButton100.UseVisualStyleBackColor = false;
    this._colorButton105.Borders = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton105.Color = Color.FromArgb(222, 235, 246);
    this._colorButton105.Dock = DockStyle.Top;
    this._colorButton105.DownControl = (Control) this.colorButton4;
    this._colorButton105.LeftControl = (Control) this._colorButton100;
    this._colorButton105.Location = new Point(0, 3);
    this._colorButton105.Name = "_colorButton105";
    this._colorButton105.RightControl = (Control) this._colorButton110;
    this._colorButton105.Size = new Size(21, 20);
    this._colorButton105.TabIndex = 0;
    this._colorsToolTip.SetToolTip((Control) this._colorButton105, "Синий, более светлый оттенок 80%");
    this._colorButton105.UpControl = (Control) this._colorButton75;
    this._colorButton105.UseVisualStyleBackColor = false;
    this._colorButton110.Borders = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton110.Color = Color.FromArgb(251, 229, 213);
    this._colorButton110.Dock = DockStyle.Top;
    this._colorButton110.DownControl = (Control) this._colorButton106;
    this._colorButton110.LeftControl = (Control) this._colorButton105;
    this._colorButton110.Location = new Point(0, 3);
    this._colorButton110.Name = "_colorButton110";
    this._colorButton110.RightControl = (Control) this._colorButton115;
    this._colorButton110.Size = new Size(21, 20);
    this._colorButton110.TabIndex = 0;
    this._colorsToolTip.SetToolTip((Control) this._colorButton110, "Оранжевый, более светлый оттенок 80%");
    this._colorButton110.UpControl = (Control) this._colorButton76;
    this._colorButton110.UseVisualStyleBackColor = false;
    this._colorButton115.Borders = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton115.Color = Color.FromArgb(237, 237, 237);
    this._colorButton115.Dock = DockStyle.Top;
    this._colorButton115.DownControl = (Control) this._colorButton111;
    this._colorButton115.LeftControl = (Control) this._colorButton110;
    this._colorButton115.Location = new Point(0, 3);
    this._colorButton115.Name = "_colorButton115";
    this._colorButton115.RightControl = (Control) this._colorButton120;
    this._colorButton115.Size = new Size(21, 20);
    this._colorButton115.TabIndex = 0;
    this._colorsToolTip.SetToolTip((Control) this._colorButton115, "Серый 50%, более светлый оттенок 80%");
    this._colorButton115.UpControl = (Control) this._colorButton77;
    this._colorButton115.UseVisualStyleBackColor = false;
    this._colorButton120.Borders = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton120.Color = Color.FromArgb((int) byte.MaxValue, 242, 204);
    this._colorButton120.Dock = DockStyle.Top;
    this._colorButton120.DownControl = (Control) this._colorButton116;
    this._colorButton120.LeftControl = (Control) this._colorButton115;
    this._colorButton120.Location = new Point(0, 3);
    this._colorButton120.Name = "_colorButton120";
    this._colorButton120.RightControl = (Control) this._colorButton125;
    this._colorButton120.Size = new Size(21, 20);
    this._colorButton120.TabIndex = 0;
    this._colorsToolTip.SetToolTip((Control) this._colorButton120, "Золотистый, более светлый оттенок 80%");
    this._colorButton120.UpControl = (Control) this._colorButton78;
    this._colorButton120.UseVisualStyleBackColor = false;
    this._colorButton125.Borders = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton125.Color = Color.FromArgb(217, 226, 243);
    this._colorButton125.Dock = DockStyle.Top;
    this._colorButton125.DownControl = (Control) this._colorButton121;
    this._colorButton125.LeftControl = (Control) this._colorButton120;
    this._colorButton125.Location = new Point(0, 3);
    this._colorButton125.Name = "_colorButton125";
    this._colorButton125.RightControl = (Control) this._colorButton130;
    this._colorButton125.Size = new Size(21, 20);
    this._colorButton125.TabIndex = 0;
    this._colorsToolTip.SetToolTip((Control) this._colorButton125, "Синий, более светлый оттенок 80%");
    this._colorButton125.UpControl = (Control) this._colorButton79;
    this._colorButton125.UseVisualStyleBackColor = false;
    this._colorButton130.Borders = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._colorButton130.Color = Color.FromArgb(226, 239, 217);
    this._colorButton130.Dock = DockStyle.Top;
    this._colorButton130.DownControl = (Control) this._colorButton126;
    this._colorButton130.LeftControl = (Control) this._colorButton125;
    this._colorButton130.Location = new Point(0, 3);
    this._colorButton130.Name = "_colorButton130";
    this._colorButton130.Size = new Size(21, 20);
    this._colorButton130.TabIndex = 0;
    this._colorsToolTip.SetToolTip((Control) this._colorButton130, "Зелёный, более светлый оттенок 80%");
    this._colorButton130.UpControl = (Control) this._colorButton80;
    this._colorButton130.UseVisualStyleBackColor = false;
    this._colorButton130.OnNavigateToRight += new OnNavigateDelegate(this._colorButton80_OnNavigateToRight);
    this._colorButton80.Color = Color.FromArgb(112 /*0x70*/, 173, 71);
    this._colorButton80.Dock = DockStyle.Fill;
    this._colorButton80.DownControl = (Control) this._colorButton130;
    this._colorButton80.LeftControl = (Control) this._colorButton79;
    this._colorButton80.Location = new Point(219, 3);
    this._colorButton80.Margin = new Padding(0, 3, 3, 0);
    this._colorButton80.Name = "_colorButton80";
    this._colorButton80.Size = new Size(21, 21);
    this._colorButton80.TabIndex = 9;
    this._colorsToolTip.SetToolTip((Control) this._colorButton80, "Зелёный");
    this._colorButton80.UseVisualStyleBackColor = false;
    this._colorButton80.OnNavigateToUp += new OnNavigateDelegate(this._colorButton72_OnNavigateToUp);
    this._colorButton80.OnNavigateToRight += new OnNavigateDelegate(this._colorButton80_OnNavigateToRight);
    this._colorButton79.Color = Color.FromArgb(68, 114, 196);
    this._colorButton79.Dock = DockStyle.Fill;
    this._colorButton79.DownControl = (Control) this._colorButton125;
    this._colorButton79.LeftControl = (Control) this._colorButton78;
    this._colorButton79.Location = new Point(195, 3);
    this._colorButton79.Margin = new Padding(0, 3, 3, 0);
    this._colorButton79.Name = "_colorButton79";
    this._colorButton79.RightControl = (Control) this._colorButton80;
    this._colorButton79.Size = new Size(21, 21);
    this._colorButton79.TabIndex = 8;
    this._colorsToolTip.SetToolTip((Control) this._colorButton79, "Синий");
    this._colorButton79.UseVisualStyleBackColor = false;
    this._colorButton79.OnNavigateToUp += new OnNavigateDelegate(this._colorButton72_OnNavigateToUp);
    this._colorButton78.Color = Color.FromArgb((int) byte.MaxValue, 192 /*0xC0*/, 0);
    this._colorButton78.Dock = DockStyle.Fill;
    this._colorButton78.DownControl = (Control) this._colorButton120;
    this._colorButton78.LeftControl = (Control) this._colorButton77;
    this._colorButton78.Location = new Point(171, 3);
    this._colorButton78.Margin = new Padding(0, 3, 3, 0);
    this._colorButton78.Name = "_colorButton78";
    this._colorButton78.RightControl = (Control) this._colorButton79;
    this._colorButton78.Size = new Size(21, 21);
    this._colorButton78.TabIndex = 7;
    this._colorsToolTip.SetToolTip((Control) this._colorButton78, "Золотистый");
    this._colorButton78.UseVisualStyleBackColor = false;
    this._colorButton78.OnNavigateToUp += new OnNavigateDelegate(this._colorButton72_OnNavigateToUp);
    this._colorButton77.Color = Color.FromArgb(165, 165, 165);
    this._colorButton77.Dock = DockStyle.Fill;
    this._colorButton77.DownControl = (Control) this._colorButton115;
    this._colorButton77.LeftControl = (Control) this._colorButton76;
    this._colorButton77.Location = new Point(147, 3);
    this._colorButton77.Margin = new Padding(0, 3, 3, 0);
    this._colorButton77.Name = "_colorButton77";
    this._colorButton77.RightControl = (Control) this._colorButton78;
    this._colorButton77.Size = new Size(21, 21);
    this._colorButton77.TabIndex = 6;
    this._colorsToolTip.SetToolTip((Control) this._colorButton77, "Серый 50%");
    this._colorButton77.UseVisualStyleBackColor = false;
    this._colorButton77.OnNavigateToUp += new OnNavigateDelegate(this._colorButton72_OnNavigateToUp);
    this._colorButton76.Color = Color.FromArgb(237, 125, 49);
    this._colorButton76.Dock = DockStyle.Fill;
    this._colorButton76.DownControl = (Control) this._colorButton110;
    this._colorButton76.LeftControl = (Control) this._colorButton75;
    this._colorButton76.Location = new Point(123, 3);
    this._colorButton76.Margin = new Padding(0, 3, 3, 0);
    this._colorButton76.Name = "_colorButton76";
    this._colorButton76.RightControl = (Control) this._colorButton77;
    this._colorButton76.Size = new Size(21, 21);
    this._colorButton76.TabIndex = 5;
    this._colorsToolTip.SetToolTip((Control) this._colorButton76, "Оранжевый");
    this._colorButton76.UseVisualStyleBackColor = false;
    this._colorButton76.OnNavigateToUp += new OnNavigateDelegate(this._colorButton72_OnNavigateToUp);
    this._colorButton75.Color = Color.FromArgb(91, 155, 213);
    this._colorButton75.Dock = DockStyle.Fill;
    this._colorButton75.DownControl = (Control) this._colorButton105;
    this._colorButton75.LeftControl = (Control) this._colorButton74;
    this._colorButton75.Location = new Point(99, 3);
    this._colorButton75.Margin = new Padding(0, 3, 3, 0);
    this._colorButton75.Name = "_colorButton75";
    this._colorButton75.RightControl = (Control) this._colorButton76;
    this._colorButton75.Size = new Size(21, 21);
    this._colorButton75.TabIndex = 4;
    this._colorsToolTip.SetToolTip((Control) this._colorButton75, "Синий");
    this._colorButton75.UseVisualStyleBackColor = false;
    this._colorButton75.OnNavigateToUp += new OnNavigateDelegate(this._colorButton72_OnNavigateToUp);
    this._colorButton74.Color = Color.FromArgb(68, 84, 106);
    this._colorButton74.Dock = DockStyle.Fill;
    this._colorButton74.DownControl = (Control) this._colorButton100;
    this._colorButton74.LeftControl = (Control) this._colorButton73;
    this._colorButton74.Location = new Point(75, 3);
    this._colorButton74.Margin = new Padding(0, 3, 3, 0);
    this._colorButton74.Name = "_colorButton74";
    this._colorButton74.RightControl = (Control) this._colorButton75;
    this._colorButton74.Size = new Size(21, 21);
    this._colorButton74.TabIndex = 3;
    this._colorsToolTip.SetToolTip((Control) this._colorButton74, "Сизый");
    this._colorButton74.UseVisualStyleBackColor = false;
    this._colorButton74.OnNavigateToUp += new OnNavigateDelegate(this._colorButton72_OnNavigateToUp);
    this._colorButton73.Color = Color.FromArgb(231, 230, 230);
    this._colorButton73.Dock = DockStyle.Fill;
    this._colorButton73.DownControl = (Control) this._colorButton95;
    this._colorButton73.LeftControl = (Control) this._colorButton72;
    this._colorButton73.Location = new Point(51, 3);
    this._colorButton73.Margin = new Padding(0, 3, 3, 0);
    this._colorButton73.Name = "_colorButton73";
    this._colorButton73.RightControl = (Control) this._colorButton74;
    this._colorButton73.Size = new Size(21, 21);
    this._colorButton73.TabIndex = 2;
    this._colorsToolTip.SetToolTip((Control) this._colorButton73, "Серый 25%");
    this._colorButton73.UseVisualStyleBackColor = false;
    this._colorButton73.OnNavigateToUp += new OnNavigateDelegate(this._colorButton72_OnNavigateToUp);
    this._colorButton72.Color = Color.Black;
    this._colorButton72.Dock = DockStyle.Fill;
    this._colorButton72.DownControl = (Control) this._colorButton90;
    this._colorButton72.LeftControl = (Control) this._colorButton71;
    this._colorButton72.Location = new Point(27, 3);
    this._colorButton72.Margin = new Padding(0, 3, 3, 0);
    this._colorButton72.Name = "_colorButton72";
    this._colorButton72.RightControl = (Control) this._colorButton73;
    this._colorButton72.Size = new Size(21, 21);
    this._colorButton72.TabIndex = 1;
    this._colorsToolTip.SetToolTip((Control) this._colorButton72, "Чёрный");
    this._colorButton72.UseVisualStyleBackColor = false;
    this._colorButton72.OnNavigateToUp += new OnNavigateDelegate(this._colorButton72_OnNavigateToUp);
    this.panel1.Controls.Add((Control) this._colorButton84);
    this.panel1.Controls.Add((Control) this._colorButton83);
    this.panel1.Controls.Add((Control) this._colorButton82);
    this.panel1.Controls.Add((Control) this._colorButton81);
    this.panel1.Controls.Add((Control) this._colorButton85);
    this.panel1.Dock = DockStyle.Fill;
    this.panel1.Location = new Point(3, 24);
    this.panel1.Margin = new Padding(0);
    this.panel1.Name = "panel1";
    this.panel1.Padding = new Padding(0, 3, 3, 0);
    this.panel1.Size = new Size(24, 102);
    this.panel1.TabIndex = 10;
    this.panel2.Controls.Add((Control) this._colorButton89);
    this.panel2.Controls.Add((Control) this._colorButton88);
    this.panel2.Controls.Add((Control) this._colorButton87);
    this.panel2.Controls.Add((Control) this._colorButton86);
    this.panel2.Controls.Add((Control) this._colorButton90);
    this.panel2.Dock = DockStyle.Fill;
    this.panel2.Location = new Point(27, 24);
    this.panel2.Margin = new Padding(0);
    this.panel2.Name = "panel2";
    this.panel2.Padding = new Padding(0, 3, 3, 0);
    this.panel2.Size = new Size(24, 102);
    this.panel2.TabIndex = 11;
    this.panel3.Controls.Add((Control) this._colorButton94);
    this.panel3.Controls.Add((Control) this._colorButton93);
    this.panel3.Controls.Add((Control) this._colorButton92);
    this.panel3.Controls.Add((Control) this._colorButton91);
    this.panel3.Controls.Add((Control) this._colorButton95);
    this.panel3.Dock = DockStyle.Fill;
    this.panel3.Location = new Point(51, 24);
    this.panel3.Margin = new Padding(0);
    this.panel3.Name = "panel3";
    this.panel3.Padding = new Padding(0, 3, 3, 0);
    this.panel3.Size = new Size(24, 102);
    this.panel3.TabIndex = 12;
    this.panel4.Controls.Add((Control) this._colorButton99);
    this.panel4.Controls.Add((Control) this._colorButton98);
    this.panel4.Controls.Add((Control) this._colorButton97);
    this.panel4.Controls.Add((Control) this._colorButton96);
    this.panel4.Controls.Add((Control) this._colorButton100);
    this.panel4.Dock = DockStyle.Fill;
    this.panel4.Location = new Point(75, 24);
    this.panel4.Margin = new Padding(0);
    this.panel4.Name = "panel4";
    this.panel4.Padding = new Padding(0, 3, 3, 0);
    this.panel4.Size = new Size(24, 102);
    this.panel4.TabIndex = 13;
    this.panel5.Controls.Add((Control) this._colorButton104);
    this.panel5.Controls.Add((Control) this.colorButton2);
    this.panel5.Controls.Add((Control) this.colorButton3);
    this.panel5.Controls.Add((Control) this.colorButton4);
    this.panel5.Controls.Add((Control) this._colorButton105);
    this.panel5.Dock = DockStyle.Fill;
    this.panel5.Location = new Point(99, 24);
    this.panel5.Margin = new Padding(0);
    this.panel5.Name = "panel5";
    this.panel5.Padding = new Padding(0, 3, 3, 0);
    this.panel5.Size = new Size(24, 102);
    this.panel5.TabIndex = 14;
    this.panel6.Controls.Add((Control) this._colorButton109);
    this.panel6.Controls.Add((Control) this._colorButton108);
    this.panel6.Controls.Add((Control) this._colorButton107);
    this.panel6.Controls.Add((Control) this._colorButton106);
    this.panel6.Controls.Add((Control) this._colorButton110);
    this.panel6.Dock = DockStyle.Fill;
    this.panel6.Location = new Point(123, 24);
    this.panel6.Margin = new Padding(0);
    this.panel6.Name = "panel6";
    this.panel6.Padding = new Padding(0, 3, 3, 0);
    this.panel6.Size = new Size(24, 102);
    this.panel6.TabIndex = 15;
    this.panel7.Controls.Add((Control) this._colorButton114);
    this.panel7.Controls.Add((Control) this._colorButton113);
    this.panel7.Controls.Add((Control) this._colorButton112);
    this.panel7.Controls.Add((Control) this._colorButton111);
    this.panel7.Controls.Add((Control) this._colorButton115);
    this.panel7.Dock = DockStyle.Fill;
    this.panel7.Location = new Point(147, 24);
    this.panel7.Margin = new Padding(0);
    this.panel7.Name = "panel7";
    this.panel7.Padding = new Padding(0, 3, 3, 0);
    this.panel7.Size = new Size(24, 102);
    this.panel7.TabIndex = 16 /*0x10*/;
    this.panel8.Controls.Add((Control) this._colorButton119);
    this.panel8.Controls.Add((Control) this._colorButton118);
    this.panel8.Controls.Add((Control) this._colorButton117);
    this.panel8.Controls.Add((Control) this._colorButton116);
    this.panel8.Controls.Add((Control) this._colorButton120);
    this.panel8.Dock = DockStyle.Fill;
    this.panel8.Location = new Point(171, 24);
    this.panel8.Margin = new Padding(0);
    this.panel8.Name = "panel8";
    this.panel8.Padding = new Padding(0, 3, 3, 0);
    this.panel8.Size = new Size(24, 102);
    this.panel8.TabIndex = 17;
    this.panel9.Controls.Add((Control) this._colorButton124);
    this.panel9.Controls.Add((Control) this._colorButton123);
    this.panel9.Controls.Add((Control) this._colorButton122);
    this.panel9.Controls.Add((Control) this._colorButton121);
    this.panel9.Controls.Add((Control) this._colorButton125);
    this.panel9.Dock = DockStyle.Fill;
    this.panel9.Location = new Point(195, 24);
    this.panel9.Margin = new Padding(0);
    this.panel9.Name = "panel9";
    this.panel9.Padding = new Padding(0, 3, 3, 0);
    this.panel9.Size = new Size(24, 102);
    this.panel9.TabIndex = 18;
    this.panel10.Controls.Add((Control) this._colorButton129);
    this.panel10.Controls.Add((Control) this._colorButton128);
    this.panel10.Controls.Add((Control) this._colorButton127);
    this.panel10.Controls.Add((Control) this._colorButton126);
    this.panel10.Controls.Add((Control) this._colorButton130);
    this.panel10.Dock = DockStyle.Fill;
    this.panel10.Location = new Point(219, 24);
    this.panel10.Margin = new Padding(0);
    this.panel10.Name = "panel10";
    this.panel10.Padding = new Padding(0, 3, 3, 0);
    this.panel10.Size = new Size(24, 102);
    this.panel10.TabIndex = 19;
    this._panelOtherColors.Controls.Add((Control) this._btnOtherColors);
    this._panelOtherColors.Dock = DockStyle.Top;
    this._panelOtherColors.Location = new Point(0, 220);
    this._panelOtherColors.Name = "_panelOtherColors";
    this._panelOtherColors.Padding = new Padding(3);
    this._panelOtherColors.Size = new Size(243, 33);
    this._panelOtherColors.TabIndex = 40;
    this._colorDialog.AnyColor = true;
    this._colorDialog.FullOpen = true;
    this._panelColorsHeader.BackColor = Color.FromArgb(238, 238, 238);
    this._panelColorsHeader.Controls.Add((Control) this._labelStandartColors);
    this._panelColorsHeader.Dock = DockStyle.Top;
    this._panelColorsHeader.Location = new Point(0, 0);
    this._panelColorsHeader.Name = "_panelColorsHeader";
    this._panelColorsHeader.Size = new Size(243, 22);
    this._panelColorsHeader.TabIndex = 31 /*0x1F*/;
    this._labelStandartColors.AutoSize = true;
    this._labelStandartColors.Font = new Font("Times New Roman", 11.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this._labelStandartColors.ForeColor = Color.FromArgb(120, 120, 120);
    this._labelStandartColors.Location = new Point(3, 2);
    this._labelStandartColors.Name = "_labelStandartColors";
    this._labelStandartColors.Size = new Size(147, 17);
    this._labelStandartColors.TabIndex = 0;
    this._labelStandartColors.Text = "Стандартные цвета";
    this._labelStandartColors.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
    this._panelLastUsed.BackColor = Color.FromArgb(238, 238, 238);
    this._panelLastUsed.Controls.Add((Control) this._labelLastColors);
    this._panelLastUsed.Dock = DockStyle.Top;
    this._panelLastUsed.Location = new Point(0, 174);
    this._panelLastUsed.Name = "_panelLastUsed";
    this._panelLastUsed.Size = new Size(243, 22);
    this._panelLastUsed.TabIndex = 32 /*0x20*/;
    this._panelLastUsed.Visible = false;
    this._labelLastColors.AutoSize = true;
    this._labelLastColors.Font = new Font("Times New Roman", 11.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this._labelLastColors.ForeColor = Color.FromArgb(120, 120, 120);
    this._labelLastColors.Location = new Point(3, 2);
    this._labelLastColors.Name = "_labelLastColors";
    this._labelLastColors.Size = new Size(129, 17);
    this._labelLastColors.TabIndex = 0;
    this._labelLastColors.Text = "Последние цвета";
    this._labelLastColors.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
    this._tableLayoutLastUsed.ColumnCount = 11;
    this._tableLayoutLastUsed.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 3f));
    this._tableLayoutLastUsed.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
    this._tableLayoutLastUsed.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
    this._tableLayoutLastUsed.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
    this._tableLayoutLastUsed.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
    this._tableLayoutLastUsed.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
    this._tableLayoutLastUsed.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
    this._tableLayoutLastUsed.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
    this._tableLayoutLastUsed.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
    this._tableLayoutLastUsed.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
    this._tableLayoutLastUsed.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
    this._tableLayoutLastUsed.Controls.Add((Control) this._btnLastUsed1, 1, 0);
    this._tableLayoutLastUsed.Controls.Add((Control) this._btnLastUsed2, 2, 0);
    this._tableLayoutLastUsed.Controls.Add((Control) this._btnLastUsed3, 3, 0);
    this._tableLayoutLastUsed.Controls.Add((Control) this._btnLastUsed4, 4, 0);
    this._tableLayoutLastUsed.Controls.Add((Control) this._btnLastUsed5, 5, 0);
    this._tableLayoutLastUsed.Controls.Add((Control) this._btnLastUsed6, 6, 0);
    this._tableLayoutLastUsed.Controls.Add((Control) this._btnLastUsed7, 7, 0);
    this._tableLayoutLastUsed.Controls.Add((Control) this._btnLastUsed8, 8, 0);
    this._tableLayoutLastUsed.Controls.Add((Control) this._btnLastUsed9, 9, 0);
    this._tableLayoutLastUsed.Controls.Add((Control) this._btnLastUsed10, 10, 0);
    this._tableLayoutLastUsed.Dock = DockStyle.Top;
    this._tableLayoutLastUsed.Location = new Point(0, 196);
    this._tableLayoutLastUsed.Name = "_tableLayoutLastUsed";
    this._tableLayoutLastUsed.RowCount = 1;
    this._tableLayoutLastUsed.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this._tableLayoutLastUsed.Size = new Size(243, 24);
    this._tableLayoutLastUsed.TabIndex = 33;
    this._tableLayoutLastUsed.Visible = false;
    this._dividerOtherColors.Dock = DockStyle.Top;
    this._dividerOtherColors.Location = new Point(0, 172);
    this._dividerOtherColors.Name = "_dividerOtherColors";
    this._dividerOtherColors.Size = new Size(243, 2);
    this._dividerOtherColors.TabIndex = 34;
    this._dividerOtherColors.Visible = false;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = Color.White;
    this.Controls.Add((Control) this._panelOtherColors);
    this.Controls.Add((Control) this._tableLayoutLastUsed);
    this.Controls.Add((Control) this._panelLastUsed);
    this.Controls.Add((Control) this._dividerOtherColors);
    this.Controls.Add((Control) this._tableLayoutPanel);
    this.Controls.Add((Control) this._panelColorsHeader);
    this.Name = nameof (ColorSelectionUserControl);
    this.Size = new Size(243, 258);
    this.Load += new EventHandler(this.ColorSelectionUserControl_Load);
    this._tableLayoutPanel.ResumeLayout(false);
    this._colorButton107.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel3.ResumeLayout(false);
    this.panel4.ResumeLayout(false);
    this.panel5.ResumeLayout(false);
    this.panel6.ResumeLayout(false);
    this.panel7.ResumeLayout(false);
    this.panel8.ResumeLayout(false);
    this.panel9.ResumeLayout(false);
    this.panel10.ResumeLayout(false);
    this._panelOtherColors.ResumeLayout(false);
    this._panelColorsHeader.ResumeLayout(false);
    this._panelColorsHeader.PerformLayout();
    this._panelLastUsed.ResumeLayout(false);
    this._panelLastUsed.PerformLayout();
    this._tableLayoutLastUsed.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public delegate void ColorWasSelectedDelegate(Color color);

  internal class LastUsedColorsConfig
  {
    private string _operationName;
    private const int MaxCount = 10;
    private List<Color> _colors = new List<Color>(10);

    internal string OperationName
    {
      [DebuggerStepThrough] get => this._operationName;
    }

    internal bool Contains(Color color) => this._colors.Contains(color);

    internal LastUsedColorsConfig(string operationName, Color[] colors = null)
    {
      this._operationName = operationName;
      if (colors == null || colors.Length == 0)
        return;
      this._colors.AddRange((IEnumerable<Color>) colors);
    }

    public IEnumerable<Color> Colors
    {
      [DebuggerStepThrough] get => (IEnumerable<Color>) this._colors;
    }

    internal Color GetColor(int index) => this._colors[index];

    public int ColorsCount
    {
      [DebuggerStepThrough] get => this._colors.Count;
    }

    public void AddNewColor(Color newColor)
    {
      int index = this._colors.IndexOf(newColor);
      switch (index)
      {
        case -1:
          if (this._colors.Count == 10)
          {
            this._colors.RemoveAt(9);
            break;
          }
          break;
        case 0:
          return;
        default:
          this._colors.RemoveAt(index);
          break;
      }
      this._colors.Insert(0, newColor);
      this.FireChanged();
    }

    public event Action<ColorSelectionUserControl.LastUsedColorsConfig> Changed;

    private void FireChanged()
    {
      if (this.Changed == null)
        return;
      this.Changed(this);
    }
  }
}
