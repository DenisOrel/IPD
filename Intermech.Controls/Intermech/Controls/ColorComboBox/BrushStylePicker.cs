
// Type: Intermech.Controls.ColorComboBox.BrushStylePicker
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Extensions;
using OfficePickers.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Controls.ColorComboBox;

[DefaultEvent("SelectedHatchChanged")]
[ToolboxItem(true)]
[Description("Provides hatch picker control that could be used in a model or non-model form.")]
public class BrushStylePicker : UserControl
{
  private Color _color = Color.Black;
  private HatchStyle _lastHatch = HatchStyle.Cross;
  /// <summary>
  /// Parent form when this control is inside a context menu form
  /// </summary>
  private ContextMenuForm _contextForm;
  /// <summary>
  /// Parent control, when on of the Show(Control parent ...) is called.
  /// </summary>
  private Control _parentControl;
  /// <summary>Buttons rectangle definitions.</summary>
  private BrushStylePicker.BrushStyleButton[] buttons;
  /// <summary>Hot Track brush style</summary>
  private BrushStyle _hotTrackBrushStyle;
  /// <summary>selected brush style</summary>
  private BrushStyle _selectedBrushStyle;
  public readonly int PreferredHeight;
  public readonly int PreferredWidth;
  private const int ColsInOneRow = 5;
  private const int OuterHorizontalIdent = 15;
  private const int OuterVerticalIdent = 12;
  private const int ButtonWidth = 20;
  private const int ButtonHeight = 15;
  private const int InnerHorizontalIdent = 5;
  private const int InnerVerticalIdent = 5;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Occurs when the value of the Color property changes.</summary>
  [Category("Behavior")]
  [Description("Occurs when the value of the Hatch property changes.")]
  public event EventHandler SelectedBrushStyleChanged;

  /// <summary>Gets or sets the selected color from the HatchPicker</summary>
  [Category("Data")]
  [Description("The color of the Hatch")]
  [DefaultValue(typeof (Color), "System.Drawing.Color.Black")]
  public Color Color
  {
    get => this._color;
    set
    {
      this._color = value;
      this.Refresh();
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public BrushStyle SelectedBrushStyle
  {
    [DebuggerStepThrough] get => this._selectedBrushStyle;
    set
    {
      if (this._selectedBrushStyle == value)
        return;
      this._selectedBrushStyle = value;
      this.FireSelectedBrushStyleChanged(EventArgs.Empty);
    }
  }

  [Category("Data")]
  [Description("FillType")]
  [DefaultValue(typeof (BrushStyle.FillTypeEnum), "Intermech.Controls.ColorComboBox.BrushStyle.FillTypeEnum.Solid")]
  public BrushStyle.FillTypeEnum FillType
  {
    [DebuggerStepThrough] get
    {
      return this._selectedBrushStyle == null ? BrushStyle.FillTypeEnum.Transparent : this._selectedBrushStyle.FillType;
    }
    set
    {
      if (this._selectedBrushStyle != null && this._selectedBrushStyle.FillType == value)
        return;
      switch (value)
      {
        case BrushStyle.FillTypeEnum.Solid:
          this._selectedBrushStyle = BrushStyle.Solid;
          break;
        case BrushStyle.FillTypeEnum.Transparent:
          this._selectedBrushStyle = BrushStyle.Clear;
          break;
        default:
          this._selectedBrushStyle = BrushStyle.Hatches[this._lastHatch];
          break;
      }
      this.FireSelectedBrushStyleChanged(EventArgs.Empty);
    }
  }

  [Category("Data")]
  [Description("HatchStyle")]
  [DefaultValue(typeof (HatchStyle), "System.Drawing.Drawing2D.HatchStyle.Cross")]
  public HatchStyle Hatch
  {
    [DebuggerStepThrough] get
    {
      return this._selectedBrushStyle == null ? HatchStyle.Cross : this._selectedBrushStyle.HatchStyle;
    }
    set
    {
      if (this._selectedBrushStyle != null && this._selectedBrushStyle.HatchStyle == value)
        return;
      this._selectedBrushStyle = BrushStyle.Hatches[value];
      this.FireSelectedBrushStyleChanged(EventArgs.Empty);
    }
  }

  private Rectangle ButtonIndexToRect(int buttonIndex)
  {
    return new Rectangle(15 + buttonIndex % 5 * 25, 12 + buttonIndex / 5 * 20, 20, 15);
  }

  /// <summary>
  /// Initialized a new instance of the HatchPicker in order to provide
  /// color picker control that could be used in a model or non-model form.
  /// </summary>
  public BrushStylePicker()
  {
    int buttonIndex = 0;
    this.buttons = BrushStyle.PossibleBrushesStyles.Select<BrushStyle, BrushStylePicker.BrushStyleButton>((Func<BrushStyle, BrushStylePicker.BrushStyleButton>) (possibleBrushesStyle => new BrushStylePicker.BrushStyleButton(possibleBrushesStyle, this.ButtonIndexToRect(buttonIndex++)))).ToArray<BrushStylePicker.BrushStyleButton>(BrushStyle.BrushStylesCount);
    this.PreferredWidth = this.buttons[Math.Min(4, this.buttons.Length - 1)].DrawRect.Right + 15;
    this.PreferredHeight = this.buttons[this.buttons.Length - 1].DrawRect.Bottom + 12;
    this.InitializeComponent();
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this.SetStyle(ControlStyles.UserPaint, true);
  }

  /// <summary>
  /// Initialized a new instance of the HatchPicker in order to provide
  /// color picker control that could be used in a model or non-model form.
  /// </summary>
  /// <param name="startingColor">Starting color to the HatchPicker control</param>
  public BrushStylePicker(BrushStyle brushStyle)
    : this()
  {
    this._selectedBrushStyle = brushStyle;
  }

  /// <summary>
  /// Opens the control inside a context menu in the specified location
  /// relative to the specified control.
  /// </summary>
  /// <param name="left">Parent control coordinates left location of the control</param>
  /// <param name="top">Parent control coordinates top location of the control</param>
  /// <param name="parent">Parent control to place the control at</param>
  public void Show(Control parent, int left, int top) => this.Show(parent, new Point(left, top));

  /// <summary>
  /// Opens the control inside a context menu in the specified location
  /// </summary>
  /// <param name="left">Screen coordinates left location of the control</param>
  /// <param name="top">Screen coordinates top location of the control</param>
  public void Show(int left, int top) => this.Show(new Point(left, top));

  /// <summary>
  /// Opens the control inside a context menu in the specified location
  /// </summary>
  /// <param name="startLocation">Screen coordinates location of the control</param>
  public void Show(Point startLocation)
  {
    this._contextForm = new ContextMenuForm();
    this._contextForm.SetContainingControl((Control) this);
    this._contextForm.Height = this.PreferredHeight;
    this._contextForm.Show((Control) this, startLocation, this.PreferredWidth);
  }

  /// <summary>
  /// Opens the control inside a context menu in the specified location
  /// </summary>
  /// <param name="startLocation">Screen coordinates location of the control</param>
  /// <param name="parent">Parent control to place the control at</param>
  public void Show(Control parent, Point startLocation)
  {
    this._parentControl = parent;
    ContextMenuForm contextMenuForm = new ContextMenuForm();
    contextMenuForm.SetContainingControl((Control) this);
    contextMenuForm.Height = this.PreferredHeight;
    this._contextForm = contextMenuForm;
    contextMenuForm.Show(parent, startLocation, this.PreferredWidth);
  }

  /// <summary>Fires the HatchPicker.SelectedColorChanged event</summary>
  /// <param name="e"></param>
  public void FireSelectedBrushStyleChanged(EventArgs e)
  {
    if (this._selectedBrushStyle != null && this._selectedBrushStyle.FillType == BrushStyle.FillTypeEnum.Hatch)
      this._lastHatch = this._selectedBrushStyle.HatchStyle;
    this.Refresh();
    if (this.SelectedBrushStyleChanged == null)
      return;
    this.SelectedBrushStyleChanged((object) this, e);
  }

  /// <summary>
  /// Overrides, when mouse move - allow the hot-track look-and-feel
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseMove(MouseEventArgs e)
  {
    base.OnMouseMove(e);
    ((IEnumerable<BrushStylePicker.BrushStyleButton>) this.buttons).InvokeForFirst<BrushStylePicker.BrushStyleButton>((Predicate<BrushStylePicker.BrushStyleButton>) (button => button.DrawRect.Contains(e.Location)), (Action<BrushStylePicker.BrushStyleButton>) (button => this._hotTrackBrushStyle = button.BrushStyle));
    this.Refresh();
  }

  /// <summary>Overrides, when click on, handles color selection.</summary>
  /// <param name="e"></param>
  protected override void OnMouseClick(MouseEventArgs e)
  {
    base.OnMouseClick(e);
    ((IEnumerable<BrushStylePicker.BrushStyleButton>) this.buttons).InvokeForFirst<BrushStylePicker.BrushStyleButton>((Predicate<BrushStylePicker.BrushStyleButton>) (button => button.DrawRect.Contains(e.Location)), (Action<BrushStylePicker.BrushStyleButton>) (button =>
    {
      this.SelectedBrushStyle = button.BrushStyle;
      if (this._contextForm != null)
        this._contextForm.Hide();
      this._contextForm = (ContextMenuForm) null;
    }));
    this.Refresh();
  }

  protected override void OnMouseLeave(EventArgs e)
  {
    base.OnMouseLeave(e);
    this._hotTrackBrushStyle = (BrushStyle) null;
    this.Refresh();
  }

  /// <summary>Override, paint background to white</summary>
  /// <param name="e"></param>
  protected override void OnPaintBackground(PaintEventArgs pevent)
  {
    using (Brush brush = (Brush) new SolidBrush(CustomColors.ColorPickerBackgroundDocked))
      pevent.Graphics.FillRectangle(brush, pevent.ClipRectangle);
  }

  /// <summary>Overrides, paint all buttons</summary>
  /// <param name="e"></param>
  protected override void OnPaint(PaintEventArgs e)
  {
    using (Brush brush1 = (Brush) new SolidBrush(CustomColors.ButtonHoverLight))
    {
      using (Brush brush2 = (Brush) new SolidBrush(CustomColors.ButtonHoverDark))
      {
        using (Brush brush3 = (Brush) new SolidBrush(CustomColors.SelectedAndHover))
        {
          using (Pen pen1 = new Pen(CustomColors.SelectedBorder))
          {
            using (Pen pen2 = new Pen(CustomColors.ButtonBorder))
            {
              foreach (BrushStylePicker.BrushStyleButton button in this.buttons)
              {
                bool flag1 = this._hotTrackBrushStyle == button.BrushStyle;
                bool flag2 = this._selectedBrushStyle == button.BrushStyle;
                bool flag3 = flag2 & flag1;
                using (Brush brush4 = button.BrushStyle.CreateBrush(this._color))
                {
                  Rectangle rect;
                  ref Rectangle local = ref rect;
                  int x = button.DrawRect.Left - 3;
                  Rectangle drawRect = button.DrawRect;
                  int y = drawRect.Top - 3;
                  drawRect = button.DrawRect;
                  int width = drawRect.Width + 6;
                  drawRect = button.DrawRect;
                  int height = drawRect.Height + 6;
                  local = new Rectangle(x, y, width, height);
                  if (flag3)
                  {
                    e.Graphics.FillRectangle(brush3, rect);
                    e.Graphics.DrawRectangle(pen1, rect);
                  }
                  else if (flag1)
                  {
                    e.Graphics.FillRectangle(brush1, rect);
                    e.Graphics.DrawRectangle(pen1, rect);
                  }
                  else if (flag2)
                  {
                    e.Graphics.FillRectangle(brush2, rect);
                    e.Graphics.DrawRectangle(pen1, rect);
                  }
                  e.Graphics.FillRectangle(brush4, button.DrawRect);
                  e.Graphics.DrawRectangle(pen2, button.DrawRect);
                }
              }
            }
          }
        }
      }
    }
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
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BorderStyle = BorderStyle.FixedSingle;
    this.Name = nameof (BrushStylePicker);
  }

  private class BrushStyleButton
  {
    public readonly BrushStyle BrushStyle;
    public readonly Rectangle DrawRect;

    public BrushStyleButton(BrushStyle brushStyle, Rectangle drawRect)
    {
      this.BrushStyle = brushStyle;
      this.DrawRect = drawRect;
    }
  }
}
