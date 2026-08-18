
// Type: Intermech.Controls.LineThicknessSetupComboBox
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Controls;

[Designer(typeof (LineThicknessSetupComboBoxDesigner))]
public class LineThicknessSetupComboBox : 
  CustomComboBox,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IPopupControlHost,
  IStandartLineThicknessesOwner
{
  private SelectLineThicknessUserControl _lineThicknessSelectionUserControl;
  private object _lockObject = new object();
  private const int DefaultSelectedLineThickness = 100;
  private int _preSelectedLineThickness = 100;
  private static readonly Color DefaultSelectedLineColor = Color.Black;
  private const string DefaultSelectedLineColorName = "Black";
  private Color _preSelectedLineColor = LineThicknessSetupComboBox.DefaultSelectedLineColor;
  private Pen _linePen;
  private string _preSelectedOperationName = string.Empty;
  public static readonly IList<int> DefaultStandartLineThicknesses = (IList<int>) new List<int>(7)
  {
    50,
    100,
    150,
    200,
    300,
    400,
    500
  };
  private Collection<int> _preSelectedStandartLineThicknesses;
  private Decimal _preSelectedCustomThicknessIncrement = 0.2M;
  private Decimal _preSelectedCustomThicknessMinimum = 0.2M;
  private Decimal _preSelectedCustomThicknessMaximum = 15M;

  public LineThicknessSetupComboBox()
  {
    this._preSelectedStandartLineThicknesses = (Collection<int>) new SelectLineThicknessUserControl.StandartLineThicknessesCollection((IStandartLineThicknessesOwner) this, (IList<int>) LineThicknessSetupComboBox.DefaultStandartLineThicknesses.ToList<int>());
    base.AllowResizeDropDown = false;
    base.DropDownSizeMode = CustomComboBox.SizeMode.UseComboSize;
    base.DropSize = new Size(220, 244);
    this.Height = 30;
    this.ItemHeight = 30;
  }

  protected override Control CreateDropDownControl()
  {
    this._lineThicknessSelectionUserControl = new SelectLineThicknessUserControl();
    if (this._lineThicknessSelectionUserControl.LineColor != this._preSelectedLineColor)
      this._lineThicknessSelectionUserControl.LineColor = this._preSelectedLineColor;
    if (this._lineThicknessSelectionUserControl.OperationName != this._preSelectedOperationName)
      this._lineThicknessSelectionUserControl.OperationName = this._preSelectedOperationName;
    if (this._lineThicknessSelectionUserControl.SelectedLineThickness != this._preSelectedLineThickness)
      this._lineThicknessSelectionUserControl.SelectedLineThickness = this._preSelectedLineThickness;
    if (this._lineThicknessSelectionUserControl.CustomThicknessIncrement != this._preSelectedCustomThicknessIncrement)
      this._lineThicknessSelectionUserControl.CustomThicknessIncrement = this._preSelectedCustomThicknessIncrement;
    if (this._lineThicknessSelectionUserControl.CustomThicknessMinimum != this._preSelectedCustomThicknessMinimum)
      this._lineThicknessSelectionUserControl.CustomThicknessMinimum = this._preSelectedCustomThicknessMinimum;
    if (this._lineThicknessSelectionUserControl.CustomThicknessMaximum != this._preSelectedCustomThicknessMaximum)
      this._lineThicknessSelectionUserControl.CustomThicknessMaximum = this._preSelectedCustomThicknessMaximum;
    this._lineThicknessSelectionUserControl.BorderStyle = BorderStyle.None;
    if (this._preSelectedStandartLineThicknesses != this._lineThicknessSelectionUserControl.StandartLineThicknesses && !this._preSelectedStandartLineThicknesses.SequenceEqual<int>((IEnumerable<int>) this._lineThicknessSelectionUserControl.StandartLineThicknesses))
      this._lineThicknessSelectionUserControl.StandartLineThicknesses = this._preSelectedStandartLineThicknesses;
    else
      this._lineThicknessSelectionUserControl.RebuildItems();
    this._lineThicknessSelectionUserControl.LineThicknessSelected += new SelectLineThicknessUserControl.LineThicknessSelectedDelegate(this.LineThicknessSelected);
    return (Control) this._lineThicknessSelectionUserControl;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._lineThicknessSelectionUserControl != null)
      {
        this._lineThicknessSelectionUserControl.LineThicknessSelected -= new SelectLineThicknessUserControl.LineThicknessSelectedDelegate(this.LineThicknessSelected);
        this._lineThicknessSelectionUserControl.Dispose();
        this._lineThicknessSelectionUserControl = (SelectLineThicknessUserControl) null;
      }
      this.DisposeObj<Pen>(ref this._linePen);
    }
    base.Dispose(disposing);
  }

  protected void DisposeObj<T>(ref T obj) where T : class, IDisposable
  {
    CommonHelper.SafeDisposeAndNull<T>(this._lockObject, ref obj);
  }

  private void LineThicknessSelected(SelectLineThicknessUserControl sender, int selectedThickness)
  {
    this.DisposeObj<Pen>(ref this._linePen);
    this.HideDropDown();
    if (selectedThickness != this._preSelectedLineThickness)
    {
      this.FireLineThicknessSelected();
      this._preSelectedLineThickness = selectedThickness;
    }
    this.Invalidate();
  }

  public event LineThicknessSetupComboBox.LineThicknessSelectedDelegate OnLineThicknessSelected;

  protected virtual void FireLineThicknessSelected()
  {
    if (this.OnLineThicknessSelected == null)
      return;
    this.OnLineThicknessSelected(this, this.SelectedLineThickness);
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
      ea.Graphics.DrawLine(this.LinePen, bounds.Left + 50, num, this.ClientRectangle.Right - 25, num);
      bounds.Width = 46;
      string s = $"{((float) this.SelectedLineThickness / 100f).ToString("0.##")} пт";
      StringFormat format = new StringFormat();
      format.Alignment = StringAlignment.Far;
      format.LineAlignment = StringAlignment.Center;
      format.Trimming = StringTrimming.EllipsisCharacter;
      RectangleF layoutRectangle = (RectangleF) bounds;
      using (Brush brush = (Brush) new SolidBrush(this.ForeColor))
        ea.Graphics.DrawString(s, this.Font, brush, layoutRectangle, format);
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(100)]
  public int SelectedLineThickness
  {
    get
    {
      return this._lineThicknessSelectionUserControl == null ? this._preSelectedLineThickness : this._lineThicknessSelectionUserControl.SelectedLineThickness;
    }
    set
    {
      if (this.SelectedLineThickness == value)
        return;
      if (this._lineThicknessSelectionUserControl != null)
        this._lineThicknessSelectionUserControl.SelectedLineThickness = value;
      else
        this._preSelectedLineThickness = value;
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
      return this._lineThicknessSelectionUserControl == null ? this._preSelectedLineColor : this._lineThicknessSelectionUserControl.LineColor;
    }
    set
    {
      if (!(this.LineColor != value))
        return;
      if (this._lineThicknessSelectionUserControl != null)
        this._lineThicknessSelectionUserControl.LineColor = value;
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
      return LazyInitializer.EnsureInitialized<Pen>(ref this._linePen, (Func<Pen>) (() => new Pen(this.LineColor, (float) this.SelectedLineThickness / 100f)));
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
    if (this._lineThicknessSelectionUserControl == null)
      return;
    this._lineThicknessSelectionUserControl.FocusSelectedLineThicknessMenuItem();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue("")]
  public string OperationName
  {
    [DebuggerStepThrough] get
    {
      return this._lineThicknessSelectionUserControl == null ? this._preSelectedOperationName : this._lineThicknessSelectionUserControl.OperationName;
    }
    set
    {
      if (!(this.OperationName != value))
        return;
      if (this._lineThicknessSelectionUserControl != null)
        this._lineThicknessSelectionUserControl.OperationName = value;
      else
        this._preSelectedOperationName = value;
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Editor(typeof (StandartLineThicknessesEditor), typeof (UITypeEditor))]
  public Collection<int> StandartLineThicknesses
  {
    [DebuggerStepThrough] get
    {
      return this._lineThicknessSelectionUserControl == null ? this._preSelectedStandartLineThicknesses : this._lineThicknessSelectionUserControl.StandartLineThicknesses;
    }
    set
    {
      if (value == null)
        throw new Exception("null not supported");
      if (value.Count < 2)
        throw new Exception("must contains at least 2 values");
      if (value.Any<int>((Func<int, bool>) (Thickness => Thickness < 5 || Thickness > 10000)))
        throw new Exception("values must be between 5 and 10000");
      if (value.ContainsDuplicates<int>())
        throw new Exception("duplicates not allowed");
      if (this._preSelectedStandartLineThicknesses == value || this._preSelectedStandartLineThicknesses.SequenceEqual<int>((IEnumerable<int>) value))
        return;
      this.SetStandartLineThicknesses((IEnumerable<int>) value);
    }
  }

  public bool ShouldSerializeStandartLineThicknesses()
  {
    return !this.StandartLineThicknesses.SequenceEqual<int>((IEnumerable<int>) LineThicknessSetupComboBox.DefaultStandartLineThicknesses);
  }

  public void ResetStandartLineThicknesses()
  {
    if (this._lineThicknessSelectionUserControl == null)
    {
      this._preSelectedStandartLineThicknesses.Clear();
      this._preSelectedStandartLineThicknesses.AddRange<int>((IEnumerable<int>) LineThicknessSetupComboBox.DefaultStandartLineThicknesses);
    }
    else
      this._lineThicknessSelectionUserControl.ResetStandartLineThicknesses();
  }

  public virtual void AfterStandartLineThicknessesChanged()
  {
    if (!this._preSelectedStandartLineThicknesses.IsOrdered<int>())
      this.SetStandartLineThicknesses((IEnumerable<int>) this._preSelectedStandartLineThicknesses.OrderBy<int, int>((Func<int, int>) (Thickness => Thickness)).ToArray<int>(), false);
    if (this._lineThicknessSelectionUserControl == null)
      return;
    this._lineThicknessSelectionUserControl.AfterStandartLineThicknessesChanged();
  }

  private void SetStandartLineThicknesses(IEnumerable<int> value, bool checkValueIsOrdered = true)
  {
    if (checkValueIsOrdered && !value.IsOrdered<int>())
      value = (IEnumerable<int>) value.OrderBy<int, int>((Func<int, int>) (Thickness => Thickness));
    if (this._lineThicknessSelectionUserControl != null)
    {
      this._lineThicknessSelectionUserControl.StandartLineThicknesses = new Collection<int>((IList<int>) value.ToList<int>());
    }
    else
    {
      this._preSelectedStandartLineThicknesses.Clear();
      this._preSelectedStandartLineThicknesses.AddRange<int>(value);
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Decimal), "0.2")]
  public Decimal CustomThicknessIncrement
  {
    get
    {
      return this._lineThicknessSelectionUserControl == null ? this._preSelectedCustomThicknessIncrement : this._lineThicknessSelectionUserControl.CustomThicknessIncrement;
    }
    set
    {
      if (!(this.CustomThicknessIncrement != value))
        return;
      this._preSelectedCustomThicknessIncrement = value;
      if (this._lineThicknessSelectionUserControl == null)
        return;
      this._lineThicknessSelectionUserControl.CustomThicknessIncrement = value;
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Decimal), "0.2")]
  public Decimal CustomThicknessMinimum
  {
    get
    {
      return this._lineThicknessSelectionUserControl == null ? this._preSelectedCustomThicknessMinimum : this._lineThicknessSelectionUserControl.CustomThicknessMinimum;
    }
    set
    {
      if (!(this.CustomThicknessMinimum != value))
        return;
      this._preSelectedCustomThicknessMinimum = value;
      if (this._lineThicknessSelectionUserControl == null)
        return;
      this._lineThicknessSelectionUserControl.CustomThicknessMinimum = value;
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Decimal), "15")]
  public Decimal CustomThicknessMaximum
  {
    get
    {
      return this._lineThicknessSelectionUserControl == null ? this._preSelectedCustomThicknessMaximum : this._lineThicknessSelectionUserControl.CustomThicknessMaximum;
    }
    set
    {
      if (!(this.CustomThicknessMaximum != value))
        return;
      this._preSelectedCustomThicknessMaximum = value;
      if (this._lineThicknessSelectionUserControl == null)
        return;
      this._lineThicknessSelectionUserControl.CustomThicknessMaximum = value;
    }
  }

  public delegate void LineThicknessSelectedDelegate(
    LineThicknessSetupComboBox sender,
    int selectedThickness);
}
