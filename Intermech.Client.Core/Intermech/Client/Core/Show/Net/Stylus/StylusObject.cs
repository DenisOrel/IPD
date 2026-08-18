
// Type: Intermech.Client.Core.Show.Net.Stylus.StylusObject
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Syncfusion.Pdf.Graphics;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Intermech.Client.Core.Show.Net.Stylus;

/// <summary>список перьев для линий(по цвету ACAD) </summary>
[DebuggerDisplay("{ColorDwg.UInt,h} {ColorPen} {Weight}")]
internal class StylusObject : IStylus, IDisposable
{
  /// <summary>цвет которым рисовать</summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Color _colorPen;

  /// <summary>создать экземпляр пера</summary>
  /// <param name="colorDwg">цвет ACAD</param>
  internal StylusObject(DwgColor colorDwg)
  {
    this.ColorDwg = colorDwg;
    this.SetNewPen(this._colorPen = this.ColorDwg.GdiColor);
  }

  /// <summary>установить новый цвет</summary>
  /// <param name="newColor">новый цвет</param>
  internal void SetNewPen(Color newColor)
  {
    PdfColor color = new PdfColor(newColor);
    this.PdfBrush = (PdfBrush) new PdfSolidBrush(color);
    this.PdfPen = new PdfPen(color)
    {
      LineJoin = PdfLineJoin.Miter,
      LineCap = PdfLineCap.Round
    };
    this.SolidBrush?.Dispose();
    this.SolidBrush = new SolidBrush(newColor);
    this.Pen?.Dispose();
    this.Pen = new Pen(newColor)
    {
      LineJoin = LineJoin.Miter,
      EndCap = LineCap.Round,
      StartCap = LineCap.Round
    };
  }

  /// <summary> Clean up any resources being used. </summary>
  public void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }

  ~StylusObject() => this.Dispose(false);

  /// <summary> Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  private void Dispose(bool disposing)
  {
    if (disposing)
      return;
    this.Pen?.Dispose();
    this.Pen = (Pen) null;
    this.SolidBrush?.Dispose();
    this.SolidBrush = (SolidBrush) null;
    this.PdfPen = (PdfPen) null;
    this.PdfBrush = (PdfBrush) null;
  }

  /// <summary>цвет ACAD указанный в чертеже</summary>
  public DwgColor ColorDwg { get; }

  /// <summary>цвет которым рисовать</summary>
  public Color ColorPen
  {
    get => this._colorPen;
    set
    {
      if (!(this._colorPen != value))
        return;
      this.SetNewPen(this._colorPen = value);
    }
  }

  /// <summary>дополнительная толщина пера(мм)</summary>
  public double Weight { get; set; }

  /// <summary>перо GDI+</summary>
  public Pen Pen { get; private set; }

  /// <summary>заливка GDI+</summary>
  public SolidBrush SolidBrush { get; private set; }

  /// <summary>перо PDF</summary>
  public PdfPen PdfPen { get; private set; }

  /// <summary>заливка PDF</summary>
  public PdfBrush PdfBrush { get; private set; }
}
