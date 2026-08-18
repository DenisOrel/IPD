
// Type: Intermech.Controls.SelectLineThicknessMenuItem
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
using System.Windows.Forms;


namespace Intermech.Controls;

[DefaultEvent("OnLineThicknessSelected")]
public class SelectLineThicknessMenuItem : 
  LineThicknessMenuItem,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IPopupControlHost,
  IPopupMenuItem,
  IArrowKeysNavigationSupported,
  IStandartLineThicknessesOwner
{
  private SelectLineThicknessUserControl _lineThicknessSelectionUserControl;
  private string _preSelectedOperationName = string.Empty;
  private Decimal _preSelectedCustomThicknessIncrement = 0.2M;
  private Decimal _preSelectedCustomThicknessMinimum = 0.2M;
  private Decimal _preSelectedCustomThicknessMaximum = 15M;
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
  private int _textWidth;

  public SelectLineThicknessMenuItem()
  {
    this.HasDropDownControl = true;
    this._preSelectedStandartLineThicknesses = (Collection<int>) new SelectLineThicknessUserControl.StandartLineThicknessesCollection((IStandartLineThicknessesOwner) this, (IList<int>) LineThicknessSetupComboBox.DefaultStandartLineThicknesses.ToList<int>());
    this._stringFormat.Alignment = StringAlignment.Near;
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

  protected override void UpdateText()
  {
    this.HiddenText = $"Толщина: {((float) this.LineThickness / 100f).ToString("0.##")} пт";
  }

  protected override Control CreateDropDownControl()
  {
    this._lineThicknessSelectionUserControl = new SelectLineThicknessUserControl();
    if (this._lineThicknessSelectionUserControl.LineColor != this.LineColor)
      this._lineThicknessSelectionUserControl.LineColor = this.LineColor;
    if (this._lineThicknessSelectionUserControl.OperationName != this._preSelectedOperationName)
      this._lineThicknessSelectionUserControl.OperationName = this._preSelectedOperationName;
    if (this._lineThicknessSelectionUserControl.DashStyle != this.DashStyle)
      this._lineThicknessSelectionUserControl.DashStyle = this.DashStyle;
    if (this._lineThicknessSelectionUserControl.SelectedLineThickness != this.LineThickness)
      this._lineThicknessSelectionUserControl.SelectedLineThickness = this.LineThickness;
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

  private void LineThicknessSelected(SelectLineThicknessUserControl sender, int selectedThickness)
  {
    this.DisposeObj<Pen>(ref this._linePen);
    if (selectedThickness != this.LineThickness)
    {
      this.LineThickness = selectedThickness;
      this.FireLineThicknessSelected();
    }
    this.HideDropDown();
    this.Invalidate();
  }

  public event SelectLineThicknessMenuItem.LineThicknessSelectedDelegate OnLineThicknessSelected;

  protected virtual void FireLineThicknessSelected()
  {
    if (this.OnLineThicknessSelected == null)
      return;
    this.OnLineThicknessSelected(this, this.LineThickness);
  }

  protected override bool DrawText() => true;

  protected override Rectangle GetLineRectangle(ref int maxTextRight)
  {
    Rectangle clientRectangle = this.ClientRectangle;
    clientRectangle.X += this._textWidth + 5;
    clientRectangle.Width -= this._textWidth + 25;
    return clientRectangle;
  }

  protected override void PaintContent(
    PaintEventArgs e,
    Brush TextBrush,
    Color bgColor,
    ref string text,
    ref Rectangle textRectangle,
    ref bool drawDefaultText)
  {
    this._textWidth = (int) e.Graphics.MeasureString(this.Text, this.Font, textRectangle.Width, this._stringFormat).Width;
    base.PaintContent(e, TextBrush, bgColor, ref text, ref textRectangle, ref drawDefaultText);
  }

  public delegate void LineThicknessSelectedDelegate(
    SelectLineThicknessMenuItem sender,
    int selectedThickness);
}
