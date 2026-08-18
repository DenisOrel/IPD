
// Type: Intermech.Client.Core.Show.Net.ShowNew.Shape.TextShape
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Show.Net.Stylus;
using Intermech.Interfaces.Show;
using Syncfusion.Pdf.Graphics;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;


namespace Intermech.Client.Core.Show.Net.ShowNew.Shape;

/// <summary> Summary description for DText.</summary>
[DebuggerDisplay("{_text} {_font.Name} {Stylus.ColorDwg.UInt,h} {Layer.Name}")]
internal sealed class TextShape : BaseShape, IDisposable
{
  private string _text = string.Empty;
  private string _familyNameFont = string.Empty;
  private FontStyle _fontStyle;
  private Font _font;
  private Font _fontMm;
  private PdfTrueTypeFont _fontPdf;
  private double _rotateFont;
  private double _sizeFont;
  private double _widthFont;
  private PointD _insert = PointD.Empty;
  private Matrix _matrix = new Matrix();
  private RectangleF _textBox;
  private RectangleF _textBoxPdf;
  private SizeF[] _arr;
  private static readonly object SyncRoot = new object();

  internal TextShape(ILayer layer, IStylus stylus, double lineWeight)
    : base(layer, stylus, lineWeight)
  {
  }

  public void Dispose()
  {
    this._fontPdf?.Dispose();
    this._fontPdf = (PdfTrueTypeFont) null;
    this._font?.Dispose();
    this._font = (Font) null;
    this._fontMm?.Dispose();
    this._fontMm = (Font) null;
    this._matrix?.Dispose();
    this._matrix = (Matrix) null;
  }

  /// <summary>пересчет положения прямоугольника текста с толщиной</summary>
  /// <param name="textbox">прямоугольник текста</param>
  /// <returns>положения прямоугольника текста с толщиной</returns>
  private RectangleD ExtendBounds(RectangleF textbox, Matrix matrix)
  {
    PointF[] pts = new PointF[4]
    {
      new PointF(textbox.Left, textbox.Top),
      new PointF(textbox.Right, textbox.Top),
      new PointF(textbox.Right, textbox.Bottom),
      new PointF(textbox.Left, textbox.Bottom)
    };
    matrix.TransformPoints(pts);
    double val1_1 = double.MinValue;
    double val1_2 = double.MinValue;
    double num1 = double.MaxValue;
    double num2 = double.MaxValue;
    foreach (PointF pointF in pts)
    {
      val1_1 = Math.Max(val1_1, (double) pointF.X);
      val1_2 = Math.Max(val1_2, (double) pointF.Y);
      num1 = Math.Min(num1, (double) pointF.X);
      num2 = Math.Min(num2, (double) pointF.Y);
    }
    return new RectangleD(num1, num2, val1_1 - num1, val1_2 - num2);
  }

  internal BaseShape InitShort(ConvertStream stream, TextData formt, PointD insert)
  {
    lock (TextShape.SyncRoot)
    {
      this._text = stream.ReadStringCodePage(stream.ReadBytes((int) stream.ReadInt16()), formt.EncodingText);
      FontStyle fontStyle = formt.FontStyle;
      FontFamily family = new FontFamily(formt.FamilyNameFont);
      this._font = new Font(family, 1f, fontStyle, GraphicsUnit.Pixel);
      this._fontMm = new Font(family, 1f, fontStyle, GraphicsUnit.Millimeter);
      PointD empty = PointD.Empty;
      if (formt.FamilyNameFont.IndexOf("GDT", StringComparison.Ordinal) != -1)
      {
        empty.Y = formt.SizeFont / 2.12;
        empty.X = formt.WidthFont != 0.0 ? formt.WidthFont * 2.12 : formt.SizeFont / 2.12;
      }
      else
      {
        empty.Y = formt.SizeFont * 0.7;
        empty.X = formt.WidthFont != 0.0 ? formt.WidthFont * 1.644 : formt.SizeFont / 1.63;
      }
      double rotateFont = formt.RotateFont;
      this._matrix.Translate((float) insert.X, (float) insert.Y);
      this._matrix.Rotate(-(float) rotateFont);
      this._matrix.Scale((float) empty.X, (float) empty.Y);
      this.SetBound(this.ExtendBounds(this._textBox, this._matrix));
    }
    return (BaseShape) this;
  }

  internal BaseShape Init(PointD insert, string text, TextData formt)
  {
    this._text = text;
    this._familyNameFont = formt.FamilyNameFont;
    this._fontStyle = formt.FontStyle;
    this._rotateFont = formt.RotateFont;
    this._sizeFont = formt.SizeFont;
    this._widthFont = formt.WidthFont;
    this._insert = insert;
    float num1 = 50f;
    float emSize = 1f * num1;
    this._matrix.Reset();
    this._matrix.Translate((float) this._insert.X, (float) this._insert.Y);
    this._matrix.Rotate(-(float) this._rotateFont);
    this._matrix.Scale((float) this._widthFont, (float) this._sizeFont);
    this._matrix.Scale(1f / num1, 1f / num1);
    if (this._familyNameFont == "Aharoni")
      this._fontStyle = FontStyle.Bold;
    try
    {
      this._font = new Font(this._familyNameFont, emSize, this._fontStyle, GraphicsUnit.Pixel);
    }
    catch (ArgumentException ex)
    {
      this._fontStyle &= ~FontStyle.Italic;
      this._font = new Font(this._familyNameFont, emSize, this._fontStyle, GraphicsUnit.Pixel);
    }
    this._fontMm = new Font(this._familyNameFont, emSize, this._fontStyle, GraphicsUnit.Millimeter);
    float emHeight = (float) this._font.FontFamily.GetEmHeight(this._font.Style);
    float num2 = (float) this._font.FontFamily.GetCellAscent(this._font.Style) / emHeight;
    float num3 = (float) this._font.FontFamily.GetCellDescent(this._font.Style) / emHeight;
    double num4 = (double) this._font.FontFamily.GetLineSpacing(this._font.Style) / (double) emHeight;
    PointF location = new PointF((float) (-(double) num3 * 0.5) * num1, -num2 * num1);
    this._arr = new SizeF[this._text.Length];
    using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromHwnd(new IntPtr(0)))
    {
      graphics.PageScale = 1f;
      graphics.PageUnit = GraphicsUnit.Pixel;
      float width = graphics.MeasureString("--", this._font).Width;
      SizeF size = graphics.MeasureString(this._text, this._font);
      for (int length = 0; length < this._arr.Length; ++length)
      {
        this._arr[length] = graphics.MeasureString($"-{this._text.Substring(0, length)}-", this._font);
        this._arr[length].Width -= width;
      }
      this._textBox = new RectangleF(location, size);
    }
    this.SetBound(this.ExtendBounds(this._textBox, this._matrix));
    return (BaseShape) this;
  }

  private void InitPdf()
  {
    float emHeight = (float) this._font.FontFamily.GetEmHeight(this._font.Style);
    double num1 = (double) this._font.FontFamily.GetCellAscent(this._font.Style) / (double) emHeight;
    float num2 = (float) this._font.FontFamily.GetCellDescent(this._font.Style) / emHeight;
    double num3 = (double) this._font.FontFamily.GetLineSpacing(this._font.Style) / (double) emHeight;
    this._fontPdf = new PdfTrueTypeFont(new Font(this._familyNameFont, (float) this._font.Height, this._fontStyle, GraphicsUnit.Pixel), true);
    SizeF sizeF = this._fontPdf.MeasureString(this._text);
    this._textBoxPdf = new RectangleF(0.0f, this._fontPdf.Height * (num2 - 1f), sizeF.Width, sizeF.Height);
  }

  private bool IsLine(RectangleF box, Matrix matrix)
  {
    PointF[] pts = new PointF[2]
    {
      new PointF(box.Left, box.Top),
      new PointF(box.Left, box.Bottom)
    };
    matrix.TransformPoints(pts);
    pts[0].X -= pts[1].X;
    pts[0].Y -= pts[1].Y;
    return Math.Sqrt((double) pts[0].X * (double) pts[0].X + (double) pts[0].Y * (double) pts[0].Y) <= 9.9999997473787516E-05;
  }

  internal override void Draw(System.Drawing.Graphics graphics)
  {
    if (!this.Layer.Visible)
      return;
    RectangleF clipBounds = graphics.ClipBounds;
    if (!clipBounds.IntersectsWith(RectangleD.ToRectangleF(this.BoundWeight)))
      return;
    RectangleD rectangleD = this.ExtendBounds(this._textBox, this._matrix);
    rectangleD.Inflate(this.Weight, this.Weight);
    clipBounds = graphics.ClipBounds;
    if (!clipBounds.IntersectsWith(RectangleD.ToRectangleF(rectangleD)))
      return;
    lock (TextShape.SyncRoot)
    {
      GraphicsState gstate = graphics.Save();
      try
      {
        graphics.InterpolationMode = InterpolationMode.High;
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
        graphics.MultiplyTransform(this._matrix);
        if (!this.IsLine(this._textBox, graphics.Transform))
        {
          Font font = graphics.PageUnit == GraphicsUnit.Millimeter ? this._fontMm : this._font;
          for (int startIndex = 0; startIndex < this._arr.Length; ++startIndex)
            graphics.DrawString(this._text.Substring(startIndex, 1), font, (Brush) this.Stylus.SolidBrush, this._textBox.X + this._arr[startIndex].Width, this._textBox.Y);
        }
        else
        {
          Pen pen = this.Stylus.Pen;
          pen.Width = 0.0f;
          graphics.DrawLine(pen, this._textBox.X, this._textBox.Y + this._textBox.Height / 2f, this._textBox.Right, this._textBox.Y + this._textBox.Height / 2f);
        }
      }
      catch (OverflowException ex)
      {
      }
      catch (OutOfMemoryException ex)
      {
      }
      catch (ExternalException ex)
      {
      }
      finally
      {
        graphics.Restore(gstate);
      }
    }
  }

  /// <summary>прорисовка в Pdf</summary>
  /// <param name="graphics">Graphics для рисования PDF</param>
  /// <param name="clipBox">Границы для рисования</param>
  internal override void Draw(PdfGraphics graphics, RectangleD clipBox)
  {
    if (!this.Layer.Visible)
      return;
    lock (TextShape.SyncRoot)
    {
      if (this._fontPdf == null)
        this.InitPdf();
      this.ExtendBounds(this._textBox, this._matrix).Inflate(this.Weight, this.Weight);
      PdfGraphicsState state = graphics.Save();
      try
      {
        graphics.TranslateTransform((float) this._insert.X, (float) this._insert.Y);
        graphics.RotateTransform(-(float) this._rotateFont);
        graphics.ScaleTransform((float) this._widthFont, (float) this._sizeFont);
        graphics.ScaleTransform(0.02f, 0.02f);
        if (!this.IsLine(this._textBox, graphics.Matrix.Matrix))
        {
          for (int startIndex = 0; startIndex < this._arr.Length; ++startIndex)
            graphics.DrawString(this._text.Substring(startIndex, 1), (PdfFont) this._fontPdf, this.Stylus.PdfBrush, this._textBoxPdf.X + this._arr[startIndex].Width, this._textBoxPdf.Y);
        }
        else
        {
          PdfPen pdfPen = this.Stylus.PdfPen;
          pdfPen.Width = 0.0f;
          graphics.DrawLine(pdfPen, this._textBoxPdf.X, this._textBoxPdf.Y + this._textBoxPdf.Height / 2f, this._textBoxPdf.Right, this._textBoxPdf.Y + this._textBoxPdf.Height / 2f);
        }
      }
      catch (OverflowException ex)
      {
      }
      catch (OutOfMemoryException ex)
      {
      }
      catch (ExternalException ex)
      {
      }
      finally
      {
        graphics.Restore(state);
      }
    }
  }
}
