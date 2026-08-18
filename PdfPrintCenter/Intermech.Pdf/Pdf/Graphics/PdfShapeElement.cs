// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfShapeElement
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

public abstract class PdfShapeElement : PdfLayoutElement
{
  public RectangleF GetBounds() => this.GetBoundsInternal();

  protected abstract RectangleF GetBoundsInternal();

  protected override PdfLayoutResult Layout(PdfLayoutParams param)
  {
    return param != null ? new ShapeLayouter(this).Layout(param) : throw new ArgumentNullException(nameof (param));
  }
}
