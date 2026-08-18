
// Type: Intermech.Controls.LineDashStyleSetupComboBox
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Controls;

[Designer(typeof (LineDashStyleSetupComboBoxDesigner))]
public class LineDashStyleSetupComboBox : 
  CustomComboBox,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent
{
  private LineDashStylesUserControl _lineDashStyleSelectionUserControl;
  private object _lockObject = new object();
  private int _preSelectedLineThickness = 300;
  private const DashStyle DefaultSelectedLineDashStyle = DashStyle.Solid;
  private DashStyle _preSelectedLineDashStyle;
  private static readonly Color DefaultSelectedLineColor = Color.Black;
  private const string DefaultSelectedLineColorName = "Black";
  private Color _preSelectedLineColor = LineDashStyleSetupComboBox.DefaultSelectedLineColor;
  private Pen _linePen;
  private string _preSelectedOperationName = string.Empty;

  public LineDashStyleSetupComboBox()
  {
    base.AllowResizeDropDown = false;
    base.DropDownSizeMode = CustomComboBox.SizeMode.UseComboSize;
    base.DropSize = new Size(220, 244);
    this.Height = 30;
    this.ItemHeight = 30;
  }

  protected override Control CreateDropDownControl()
  {
    this._lineDashStyleSelectionUserControl = new LineDashStylesUserControl();
    if (this._lineDashStyleSelectionUserControl.OperationName != this._preSelectedOperationName)
      this._lineDashStyleSelectionUserControl.OperationName = this._preSelectedOperationName;
    if (this._lineDashStyleSelectionUserControl.LineColor != this._preSelectedLineColor)
      this._lineDashStyleSelectionUserControl.LineColor = this._preSelectedLineColor;
    if (this._lineDashStyleSelectionUserControl.LineThickness != this._preSelectedLineThickness)
      this._lineDashStyleSelectionUserControl.LineThickness = this._preSelectedLineThickness;
    if (this._lineDashStyleSelectionUserControl.SelectedDashStyle != this._preSelectedLineDashStyle)
      this._lineDashStyleSelectionUserControl.SelectedDashStyle = this._preSelectedLineDashStyle;
    this._lineDashStyleSelectionUserControl.BorderStyle = BorderStyle.None;
    this._lineDashStyleSelectionUserControl.OnDashStyleSelected += new LineDashStylesUserControl.OnDashStyleSelectedDelegate(this.LineDashStyleSelected);
    return (Control) this._lineDashStyleSelectionUserControl;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._lineDashStyleSelectionUserControl != null)
      {
        this._lineDashStyleSelectionUserControl.OnDashStyleSelected -= new LineDashStylesUserControl.OnDashStyleSelectedDelegate(this.LineDashStyleSelected);
        this._lineDashStyleSelectionUserControl.Dispose();
        this._lineDashStyleSelectionUserControl = (LineDashStylesUserControl) null;
      }
      this.DisposeObj<Pen>(ref this._linePen);
    }
    base.Dispose(disposing);
  }

  protected void DisposeObj<T>(ref T obj) where T : class, IDisposable
  {
    CommonHelper.SafeDisposeAndNull<T>(this._lockObject, ref obj);
  }

  private void LineDashStyleSelected(LineDashStylesUserControl sender, DashStyle selectedDashStyle)
  {
    this.DisposeObj<Pen>(ref this._linePen);
    this.HideDropDown();
    if (selectedDashStyle != this._preSelectedLineDashStyle)
    {
      this.FireLineDashStyleSelected();
      this._preSelectedLineDashStyle = selectedDashStyle;
    }
    this.Invalidate();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(300)]
  public int LineThickness
  {
    [DebuggerStepThrough] get
    {
      return this._lineDashStyleSelectionUserControl == null ? this._preSelectedLineThickness : this._lineDashStyleSelectionUserControl.LineThickness;
    }
    set
    {
      if (this.LineThickness == value)
        return;
      this._preSelectedLineThickness = value;
      if (this._lineDashStyleSelectionUserControl != null)
        this._lineDashStyleSelectionUserControl.LineThickness = value;
      this.DisposeObj<Pen>(ref this._linePen);
      this.Invalidate();
    }
  }

  public event LineDashStyleSetupComboBox.LineDashStyleSelectedDelegate OnLineDashStyleSelected;

  protected virtual void FireLineDashStyleSelected()
  {
    if (this.OnLineDashStyleSelected == null)
      return;
    this.OnLineDashStyleSelected(this, this.SelectedLineDashStyle);
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
    lock (this._lockObject)
    {
      Rectangle bounds = ea.Bounds;
      bounds.Inflate(-2, -2);
      int num = bounds.Bottom + bounds.Top >> 1;
      ea.Graphics.DrawLine(this.LinePen, bounds.Left + 10, num, this.ClientRectangle.Right - 25, num);
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(DashStyle.Solid)]
  public DashStyle SelectedLineDashStyle
  {
    get
    {
      return this._lineDashStyleSelectionUserControl == null ? this._preSelectedLineDashStyle : this._lineDashStyleSelectionUserControl.SelectedDashStyle;
    }
    set
    {
      if (this.SelectedLineDashStyle == value)
        return;
      if (this._lineDashStyleSelectionUserControl != null)
        this._lineDashStyleSelectionUserControl.SelectedDashStyle = value;
      else
        this._preSelectedLineDashStyle = value;
      this.DisposeObj<Pen>(ref this._linePen);
      this.Invalidate();
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Color), "Black")]
  public Color LineColor
  {
    get
    {
      return this._lineDashStyleSelectionUserControl == null ? this._preSelectedLineColor : this._lineDashStyleSelectionUserControl.LineColor;
    }
    set
    {
      if (!(this.LineColor != value))
        return;
      if (this._lineDashStyleSelectionUserControl != null)
        this._lineDashStyleSelectionUserControl.LineColor = value;
      else
        this._preSelectedLineColor = value;
      this.DisposeObj<Pen>(ref this._linePen);
      this.Invalidate();
    }
  }

  protected Pen LinePen
  {
    get
    {
      return LazyInitializer.EnsureInitialized<Pen>(ref this._linePen, (Func<Pen>) (() => new Pen(this.LineColor, (float) this.LineThickness / 100f)
      {
        DashStyle = this.SelectedLineDashStyle
      }));
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
    if (this._lineDashStyleSelectionUserControl == null)
      return;
    this._lineDashStyleSelectionUserControl.FocusSelectedDashStyle();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue("")]
  public string OperationName
  {
    [DebuggerStepThrough] get
    {
      return this._lineDashStyleSelectionUserControl == null ? this._preSelectedOperationName : this._lineDashStyleSelectionUserControl.OperationName;
    }
    set
    {
      if (!(this.OperationName != value))
        return;
      this._preSelectedOperationName = value;
      if (this._lineDashStyleSelectionUserControl == null)
        return;
      this._lineDashStyleSelectionUserControl.OperationName = value;
    }
  }

  public delegate void LineDashStyleSelectedDelegate(
    LineDashStyleSetupComboBox sender,
    DashStyle selectedDashStyle);
}
