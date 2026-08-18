
// Type: Intermech.Controls.BrushSetupComboBox
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Controls;

[Designer(typeof (BrushSetupComboBoxDesigner))]
public class BrushSetupComboBox : CustomComboBox
{
  private BrushSelectionUserControl _brushSelectionUserControl;
  private Color _preSelectedColor = Color.Black;
  private Color _preSelectedHatchBackColor = Color.Transparent;
  private BrushStyle.FillTypeEnum _preSelectedFillType;
  private HatchStyle _preSelectedHatchStyle = HatchStyle.Cross;
  private string _preSelectedOperationName = string.Empty;

  public BrushSetupComboBox()
  {
    base.AllowResizeDropDown = false;
    base.DropDownSizeMode = CustomComboBox.SizeMode.UseComboSize;
    base.DropSize = new Size(260, 545);
    this.Height = 30;
    this.ItemHeight = 30;
  }

  protected override Control CreateDropDownControl()
  {
    this._brushSelectionUserControl = new BrushSelectionUserControl();
    if (this._brushSelectionUserControl.Color != this._preSelectedColor)
      this._brushSelectionUserControl.Color = this._preSelectedColor;
    if (this._brushSelectionUserControl.FillType != this._preSelectedFillType)
      this._brushSelectionUserControl.FillType = this._preSelectedFillType;
    if (this._brushSelectionUserControl.HatchStyle != this._preSelectedHatchStyle)
      this._brushSelectionUserControl.HatchStyle = this._preSelectedHatchStyle;
    if (this._brushSelectionUserControl.HatchBackColor != this._preSelectedHatchBackColor)
      this._brushSelectionUserControl.HatchBackColor = this._preSelectedHatchBackColor;
    if (this._brushSelectionUserControl.OperationName != this._preSelectedOperationName)
      this._brushSelectionUserControl.OperationName = this._preSelectedOperationName;
    this._brushSelectionUserControl.BorderStyle = BorderStyle.None;
    this._brushSelectionUserControl.BrushStyleWasSelected += new BrushSelectionUserControl.BrushStyleWasSelectedDelegate(this._brushSelectionUserControl_BrushStyleWasSelected);
    this._brushSelectionUserControl.HatchBackColorWasSelected += new BrushSelectionUserControl.ColorWasSelectedDelegate(this._brushSelectionUserControl_HatchBackColorWasSelected);
    this._brushSelectionUserControl.ColorWasSelected += new BrushSelectionUserControl.ColorWasSelectedDelegate(this._brushSelectionUserControl_ColorWasSelected);
    return (Control) this._brushSelectionUserControl;
  }

  private void _brushSelectionUserControl_HatchBackColorWasSelected(Color color)
  {
    this.Invalidate();
  }

  private void _brushSelectionUserControl_ColorWasSelected(Color color) => this.Invalidate();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this._brushSelectionUserControl != null)
    {
      this._brushSelectionUserControl.BrushStyleWasSelected -= new BrushSelectionUserControl.BrushStyleWasSelectedDelegate(this._brushSelectionUserControl_BrushStyleWasSelected);
      this._brushSelectionUserControl.HatchBackColorWasSelected -= new BrushSelectionUserControl.ColorWasSelectedDelegate(this._brushSelectionUserControl_HatchBackColorWasSelected);
      this._brushSelectionUserControl.ColorWasSelected -= new BrushSelectionUserControl.ColorWasSelectedDelegate(this._brushSelectionUserControl_ColorWasSelected);
      this._brushSelectionUserControl.Dispose();
      this._brushSelectionUserControl = (BrushSelectionUserControl) null;
    }
    base.Dispose(disposing);
  }

  private void _brushSelectionUserControl_BrushStyleWasSelected(BrushStyle brushStyle)
  {
    this.HideDropDown();
    this.Invalidate();
  }

  /// <summary>Indicates if drop-down is resizable.</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  [ReadOnly(true)]
  public new bool AllowResizeDropDown
  {
    get => base.AllowResizeDropDown;
    set => throw new Exception("AllowResizeDropDown is hidden");
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  [ReadOnly(true)]
  public new Size ControlSize
  {
    get => base.ControlSize;
    set => throw new Exception("ControlSize is hidden");
  }

  /// <summary>Indicates current sizing mode</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  [ReadOnly(true)]
  public new CustomComboBox.SizeMode DropDownSizeMode
  {
    get => base.DropDownSizeMode;
    set => throw new Exception("DropDownSizeMode is hidden");
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  [ReadOnly(true)]
  public new Size DropSize
  {
    get => base.DropSize;
    set => throw new Exception("DropSize is hidden");
  }

  /// <summary>Actual drop-down control itself</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  [ReadOnly(true)]
  public new Control DropDownControl
  {
    get => base.DropDownControl;
    set => throw new Exception("DropDownControl is hidden");
  }

  protected override void OnDrawItem(DrawItemEventArgs ea)
  {
    base.OnDrawItem(ea);
    if (ea.Index != -1)
      return;
    Rectangle bounds = ea.Bounds;
    bounds.Inflate(-2, -2);
    if (this.SelectedBrushStyle == BrushStyle.Clear)
    {
      using (Pen pen = new Pen(Color.Black))
      {
        RectangleF layoutRectangle = new RectangleF((float) bounds.X, (float) bounds.Y, (float) bounds.Width, (float) bounds.Height);
        StringFormat format = new StringFormat();
        format.Alignment = StringAlignment.Center;
        format.LineAlignment = StringAlignment.Center;
        format.Trimming = StringTrimming.EllipsisCharacter;
        ea.Graphics.DrawRectangle(pen, bounds);
        ea.Graphics.DrawString("Без заливки", this.Font, Brushes.Black, layoutRectangle, format);
      }
    }
    else
    {
      using (Pen pen = new Pen(this.Color))
      {
        using (Brush brush = this.SelectedBrushStyle.CreateBrush(this.Color, this.HatchBackColor))
        {
          ea.Graphics.FillRectangle(brush, bounds);
          ea.Graphics.DrawRectangle(pen, bounds);
        }
      }
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Color), "Black")]
  public Color Color
  {
    get
    {
      return this._brushSelectionUserControl == null ? this._preSelectedColor : this._brushSelectionUserControl.Color;
    }
    set
    {
      if (!(this.Color != value))
        return;
      if (this._brushSelectionUserControl != null)
        this._brushSelectionUserControl.Color = value;
      else
        this._preSelectedColor = value;
      this.Invalidate();
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Color), "Transparent")]
  public Color HatchBackColor
  {
    get
    {
      return this._brushSelectionUserControl == null ? this._preSelectedHatchBackColor : this._brushSelectionUserControl.HatchBackColor;
    }
    set
    {
      if (!(this.HatchBackColor != value))
        return;
      if (this._brushSelectionUserControl != null)
        this._brushSelectionUserControl.HatchBackColor = value;
      else
        this._preSelectedHatchBackColor = value;
      this.Invalidate();
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(BrushStyle.FillTypeEnum.Solid)]
  public BrushStyle.FillTypeEnum FillType
  {
    get
    {
      return this._brushSelectionUserControl == null ? this._preSelectedFillType : this._brushSelectionUserControl.FillType;
    }
    set
    {
      if (this.FillType == value)
        return;
      if (this._brushSelectionUserControl == null)
        this._preSelectedFillType = value;
      else
        this._brushSelectionUserControl.FillType = value;
      this.Invalidate();
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(HatchStyle.Cross)]
  public HatchStyle HatchStyle
  {
    get
    {
      return this._brushSelectionUserControl == null ? this._preSelectedHatchStyle : this._brushSelectionUserControl.HatchStyle;
    }
    set
    {
      if (this.HatchStyle == value)
        return;
      if (this._brushSelectionUserControl == null)
        this._preSelectedHatchStyle = value;
      else
        this._brushSelectionUserControl.HatchStyle = value;
      if (this.FillType != BrushStyle.FillTypeEnum.Hatch)
        return;
      this.Invalidate();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public BrushStyle SelectedBrushStyle
  {
    get
    {
      return this._brushSelectionUserControl == null ? BrushStyle.Get(this._preSelectedFillType, this._preSelectedHatchStyle) : this._brushSelectionUserControl.SelectedBrushStyle;
    }
  }

  protected override void OnMeasureItem(MeasureItemEventArgs e)
  {
    base.OnMeasureItem(e);
    e.ItemHeight = 30;
  }

  protected override void OnDropDown(EventArgs e)
  {
    base.OnDropDown(e);
    this.Invalidate();
  }

  protected override void FireOnAutoFocus(EventArgs e)
  {
    base.FireOnAutoFocus(e);
    if (this._brushSelectionUserControl == null)
      return;
    this._brushSelectionUserControl.FocusFirstButton();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue("")]
  public string OperationName
  {
    [DebuggerStepThrough] get
    {
      return this._brushSelectionUserControl == null ? this._preSelectedOperationName : this._brushSelectionUserControl.OperationName;
    }
    set
    {
      if (!(this.OperationName != value))
        return;
      if (this._brushSelectionUserControl != null)
        this._brushSelectionUserControl.OperationName = value;
      else
        this._preSelectedOperationName = value;
    }
  }
}
