
// Type: Intermech.Controls.LineMenuItem
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

[Designer(typeof (LineDashStyleMenuItemDesigner))]
public class LineMenuItem : 
  ContextMenuItemSurrogate,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IPopupControlHost,
  IPopupMenuItem,
  IArrowKeysNavigationSupported
{
  public new static readonly Color DefaultBackColor = Color.White;
  public new const string DefaultBackColorName = "White";
  public const DashStyle DefaultDashStyle = DashStyle.Solid;
  public const int DefaultLineThickness = 100;
  public static readonly Color DefaultLineColor = Color.Black;
  public const string DefaulLineColorName = "Black";
  protected DashStyle _dashStyle;
  protected int _lineThickness = 100;
  protected Color _lineColor = LineMenuItem.DefaultLineColor;
  public Pen _linePen;

  public LineMenuItem() => base.BackColor = LineMenuItem.DefaultBackColor;

  protected override void Dispose(bool disposing)
  {
    if (disposing)
      this.DisposeObj<Pen>(ref this._linePen);
    base.Dispose(disposing);
  }

  [DefaultValue(typeof (Color), "White")]
  public override Color BackColor
  {
    get => base.BackColor;
    set
    {
      if (value == Color.Empty)
        value = LineMenuItem.DefaultBackColor;
      if (!(base.BackColor != value))
        return;
      base.BackColor = value;
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(DashStyle.Solid)]
  public virtual DashStyle DashStyle
  {
    [DebuggerStepThrough] get => this._dashStyle;
    set
    {
      if (value == DashStyle.Custom)
        value = DashStyle.Solid;
      if (this._dashStyle == value)
        return;
      this._dashStyle = value;
      this.UpdateLinePen();
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(100)]
  public virtual int LineThickness
  {
    [DebuggerStepThrough] get => this._lineThickness;
    set
    {
      if (this._lineThickness == value)
        return;
      this._lineThickness = value;
      this.UpdateLinePen();
    }
  }

  private void UpdateLinePen()
  {
    this.DisposeObj<Pen>(ref this._linePen);
    this.Invalidate();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Color), "Black")]
  public Color LineColor
  {
    [DebuggerStepThrough] get => this._lineColor;
    set
    {
      if (!(this._lineColor != value))
        return;
      this._lineColor = value;
      this.UpdateLinePen();
    }
  }

  private Pen LinePen
  {
    get
    {
      return LazyInitializer.EnsureInitialized<Pen>(ref this._linePen, (Func<Pen>) (() => new Pen(this._lineColor, (float) this._lineThickness / 100f)
      {
        DashStyle = this._dashStyle
      }));
    }
  }

  protected override void PaintContent(
    PaintEventArgs e,
    Brush TextBrush,
    Color bgColor,
    ref string text,
    ref Rectangle textRectangle,
    ref bool drawDefaultText)
  {
    int right = textRectangle.Right;
    Rectangle lineRectangle = this.GetLineRectangle(ref right);
    textRectangle.Width = Math.Min(right - textRectangle.X, textRectangle.Width);
    int num = lineRectangle.Bottom + lineRectangle.Top >> 1;
    if (this.Checked)
    {
      Rectangle rect = new Rectangle(lineRectangle.Left - 3, num - this._lineThickness / 100 - 3, lineRectangle.Width + 6, this._lineThickness / 50 + 6);
      e.Graphics.FillRectangle(this.CheckedBgColorBrush, rect);
      e.Graphics.DrawRectangle(this.CheckedBorderColorPen, rect);
    }
    e.Graphics.DrawLine(this.LinePen, lineRectangle.X, num, lineRectangle.Right, num);
    drawDefaultText = this.DrawText();
    base.PaintContent(e, TextBrush, bgColor, ref text, ref textRectangle, ref drawDefaultText);
  }

  protected virtual bool DrawText() => false;

  protected virtual Rectangle GetLineRectangle(ref int maxTextRight)
  {
    Rectangle clientRectangle = this.ClientRectangle;
    clientRectangle.X += 20;
    clientRectangle.Width -= 40;
    return clientRectangle;
  }

  protected override void OnKeyDown(KeyEventArgs e)
  {
    if ((e.KeyCode == Keys.Space || e.KeyCode == Keys.Return) && !this.Checked)
      this.OnClick(EventArgs.Empty);
    base.OnKeyDown(e);
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected string HiddenText
  {
    get => base.Text;
    set
    {
      value = value ?? string.Empty;
      if (!(base.Text != value))
        return;
      base.Text = value;
      this.Invalidate();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [ReadOnly(true)]
  public override string Text
  {
    get => base.Text;
    set => throw new Exception("Text property is hidden");
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected ContentAlignment HiddenTextAlign
  {
    get => base.TextAlign;
    set
    {
      if (base.TextAlign == value)
        return;
      base.TextAlign = value;
      this.Invalidate();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [ReadOnly(true)]
  public override ContentAlignment TextAlign
  {
    get => base.TextAlign;
    set => throw new Exception("TextAlign property not hidden");
  }
}
