
// Type: Intermech.Controls.SelectLineDashStyleMenuItem
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Controls;

[DefaultEvent("OnLineDashStyleSelected")]
public class SelectLineDashStyleMenuItem : LineDashStyleMenuItem
{
  private int _textWidth;
  private LineDashStylesUserControl _lineDashStyleSelectionUserControl;
  private string _preSelectedOperationName = string.Empty;

  public SelectLineDashStyleMenuItem()
  {
    this.HiddenText = "Штрихи:";
    this.HasDropDownControl = true;
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

  protected override Control CreateDropDownControl()
  {
    this._lineDashStyleSelectionUserControl = new LineDashStylesUserControl();
    if (this._lineDashStyleSelectionUserControl.OperationName != this._preSelectedOperationName)
      this._lineDashStyleSelectionUserControl.OperationName = this._preSelectedOperationName;
    if (this._lineDashStyleSelectionUserControl.LineColor != this.LineColor)
      this._lineDashStyleSelectionUserControl.LineColor = this.LineColor;
    if (this._lineDashStyleSelectionUserControl.LineThickness != this.LineThickness)
      this._lineDashStyleSelectionUserControl.LineThickness = this.LineThickness;
    if (this._lineDashStyleSelectionUserControl.SelectedDashStyle != this.DashStyle)
      this._lineDashStyleSelectionUserControl.SelectedDashStyle = this.DashStyle;
    this._lineDashStyleSelectionUserControl.BorderStyle = BorderStyle.None;
    this._lineDashStyleSelectionUserControl.OnDashStyleSelected += new LineDashStylesUserControl.OnDashStyleSelectedDelegate(this.LineDashStyleSelected);
    return (Control) this._lineDashStyleSelectionUserControl;
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

  public event SelectLineDashStyleMenuItem.LineDashStyleSelectedDelegate OnLineDashStyleSelected;

  protected virtual void FireLineDashStyleSelected()
  {
    if (this.OnLineDashStyleSelected == null)
      return;
    this.OnLineDashStyleSelected(this, this.DashStyle);
  }

  private void LineDashStyleSelected(LineDashStylesUserControl sender, DashStyle selectedDashStyle)
  {
    this.DisposeObj<Pen>(ref this._linePen);
    this.HideDropDown();
    if (selectedDashStyle == this.DashStyle)
      return;
    this.FireLineDashStyleSelected();
    this.DashStyle = selectedDashStyle;
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
    SelectLineDashStyleMenuItem sender,
    DashStyle selectedDashStyle);
}
