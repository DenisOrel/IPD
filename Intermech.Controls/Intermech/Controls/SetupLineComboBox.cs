
// Type: Intermech.Controls.SetupLineComboBox
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
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Controls;

[Designer(typeof (SetupLineComboBoxDesigner))]
public class SetupLineComboBox : 
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
  private SetupLineUserControl _setupLineUserControl;
  private object _lockObject = new object();
  private int _preSelectedLineThickness = 300;
  private const DashStyle DefaultSelectedLineDashStyle = DashStyle.Solid;
  private DashStyle _preSelectedLineDashStyle;
  private static readonly Color DefaultSelectedLineColor = Color.Black;
  private const string DefaultSelectedLineColorName = "Black";
  private Color _preSelectedLineColor = SetupLineComboBox.DefaultSelectedLineColor;
  private Pen _linePen;
  private string _preSelectedOperationName = string.Empty;
  private Collection<int> _preSelectedStandartLineThicknesses;

  public SetupLineComboBox()
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
    this._setupLineUserControl = new SetupLineUserControl();
    if (this._setupLineUserControl.OperationName != this._preSelectedOperationName)
      this._setupLineUserControl.OperationName = this._preSelectedOperationName;
    if (this._setupLineUserControl.LineColor != this._preSelectedLineColor)
      this._setupLineUserControl.LineColor = this._preSelectedLineColor;
    if (this._setupLineUserControl.LineThickness != this._preSelectedLineThickness)
      this._setupLineUserControl.LineThickness = this._preSelectedLineThickness;
    if (this._setupLineUserControl.DashStyle != this._preSelectedLineDashStyle)
      this._setupLineUserControl.DashStyle = this._preSelectedLineDashStyle;
    this._setupLineUserControl.BorderStyle = BorderStyle.None;
    if (this._preSelectedStandartLineThicknesses != this._setupLineUserControl.StandartLineThicknesses && !this._preSelectedStandartLineThicknesses.SequenceEqual<int>((IEnumerable<int>) this._setupLineUserControl.StandartLineThicknesses))
      this._setupLineUserControl.StandartLineThicknesses = this._preSelectedStandartLineThicknesses;
    this._setupLineUserControl.OnLineChanged += new SetupLineUserControl.SetupLineNotifyEvent(this._setupLineUserControl_OnLineChanged);
    return (Control) this._setupLineUserControl;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._setupLineUserControl != null)
      {
        this._setupLineUserControl.OnLineChanged -= new SetupLineUserControl.SetupLineNotifyEvent(this._setupLineUserControl_OnLineChanged);
        this._setupLineUserControl.Dispose();
        this._setupLineUserControl = (SetupLineUserControl) null;
      }
      this.DisposeObj<Pen>(ref this._linePen);
    }
    base.Dispose(disposing);
  }

  protected void DisposeObj<T>(ref T obj) where T : class, IDisposable
  {
    CommonHelper.SafeDisposeAndNull<T>(this._lockObject, ref obj);
  }

  private void _setupLineUserControl_OnLineChanged(SetupLineUserControl sender)
  {
    this.DisposeObj<Pen>(ref this._linePen);
    this.HideDropDown();
    if (this._preSelectedLineColor != sender.LineColor)
      this._preSelectedLineColor = sender.LineColor;
    if (this._preSelectedLineDashStyle != sender.DashStyle)
      this._preSelectedLineDashStyle = sender.DashStyle;
    if (this._preSelectedLineThickness != sender.LineThickness)
      this._preSelectedLineThickness = sender.LineThickness;
    this.FireOnLineChanged();
    this.Invalidate();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(100)]
  public int LineThickness
  {
    [DebuggerStepThrough] get
    {
      return this._setupLineUserControl == null ? this._preSelectedLineThickness : this._setupLineUserControl.LineThickness;
    }
    set
    {
      if (this.LineThickness == value)
        return;
      this._preSelectedLineThickness = value;
      if (this._setupLineUserControl != null)
        this._setupLineUserControl.LineThickness = value;
      this.DisposeObj<Pen>(ref this._linePen);
      this.Invalidate();
    }
  }

  public event SetupLineComboBox.OnLineChangedDelegate OnLineChanged;

  protected virtual void FireOnLineChanged()
  {
    if (this.OnLineChanged == null)
      return;
    this.OnLineChanged(this);
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
      return this._setupLineUserControl == null ? this._preSelectedLineDashStyle : this._setupLineUserControl.DashStyle;
    }
    set
    {
      if (this.SelectedLineDashStyle == value)
        return;
      if (this._setupLineUserControl != null)
        this._setupLineUserControl.DashStyle = value;
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
      return this._setupLineUserControl == null ? this._preSelectedLineColor : this._setupLineUserControl.LineColor;
    }
    set
    {
      if (!(this.LineColor != value))
        return;
      if (this._setupLineUserControl != null)
        this._setupLineUserControl.LineColor = value;
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
    if (this._setupLineUserControl == null)
      return;
    this._setupLineUserControl.AutoFocus();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue("")]
  public string OperationName
  {
    [DebuggerStepThrough] get
    {
      return this._setupLineUserControl == null ? this._preSelectedOperationName : this._setupLineUserControl.OperationName;
    }
    set
    {
      if (!(this.OperationName != value))
        return;
      this._preSelectedOperationName = value;
      if (this._setupLineUserControl == null)
        return;
      this._setupLineUserControl.OperationName = value;
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Editor(typeof (StandartLineThicknessesEditor), typeof (UITypeEditor))]
  public Collection<int> StandartLineThicknesses
  {
    [DebuggerStepThrough] get
    {
      return this._setupLineUserControl == null ? this._preSelectedStandartLineThicknesses : this._setupLineUserControl.StandartLineThicknesses;
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
    if (this._setupLineUserControl == null)
    {
      this._preSelectedStandartLineThicknesses.Clear();
      this._preSelectedStandartLineThicknesses.AddRange<int>((IEnumerable<int>) LineThicknessSetupComboBox.DefaultStandartLineThicknesses);
    }
    else
      this._setupLineUserControl.ResetStandartLineThicknesses();
  }

  public virtual void AfterStandartLineThicknessesChanged()
  {
    if (this._preSelectedStandartLineThicknesses.IsOrdered<int>())
      return;
    this.SetStandartLineThicknesses((IEnumerable<int>) this._preSelectedStandartLineThicknesses.OrderBy<int, int>((Func<int, int>) (Thickness => Thickness)).ToArray<int>(), false);
  }

  private void SetStandartLineThicknesses(IEnumerable<int> value, bool checkValueIsOrdered = true)
  {
    if (checkValueIsOrdered && !value.IsOrdered<int>())
      value = (IEnumerable<int>) value.OrderBy<int, int>((Func<int, int>) (Thickness => Thickness));
    if (this._setupLineUserControl != null)
    {
      this._setupLineUserControl.StandartLineThicknesses = new Collection<int>((IList<int>) value.ToList<int>());
    }
    else
    {
      this._preSelectedStandartLineThicknesses.Clear();
      this._preSelectedStandartLineThicknesses.AddRange<int>(value);
    }
  }

  public delegate void OnLineChangedDelegate(SetupLineComboBox sender);
}
