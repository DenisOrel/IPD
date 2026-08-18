// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfTilingBrush
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System.Collections;
using System.Drawing;


namespace Syncfusion.Pdf.Graphics
{
    public sealed class PdfTilingBrush : PdfBrush, IPdfWrapper
    {
      private RectangleF m_box;
      private PdfStream m_brushStream;
      private bool m_bStroking;
      private PdfGraphics m_graphics;
      private PointF m_location;
      private PdfPage m_page;
      private PdfResources m_resources;

      public PdfTilingBrush(RectangleF rectangle)
      {
        this.m_brushStream = new PdfStream();
        this.m_resources = new PdfResources();
        this.m_brushStream[nameof (Resources)] = (IPdfPrimitive) this.m_resources;
        this.SetBox(rectangle);
        this.SetObligatoryFields();
      }

      public PdfTilingBrush(SizeF size)
        : this(new RectangleF(PointF.Empty, size))
      {
      }

      public PdfTilingBrush(RectangleF rectangle, PdfPage page)
      {
        this.m_page = page;
        this.m_brushStream = new PdfStream();
        this.m_resources = new PdfResources();
        this.m_brushStream[nameof (Resources)] = (IPdfPrimitive) this.m_resources;
        this.SetBox(rectangle);
        this.SetObligatoryFields();
        this.Graphics.ColorSpace = page.Document.ColorSpace;
      }

      public PdfTilingBrush(SizeF size, PdfPage page)
        : this(new RectangleF(PointF.Empty, size), page)
      {
      }

      internal PdfTilingBrush(RectangleF rectangle, PdfPage page, PointF location)
      {
        this.m_page = page;
        this.m_location = location;
        this.m_brushStream = new PdfStream();
        this.m_resources = new PdfResources();
        this.m_brushStream[nameof (Resources)] = (IPdfPrimitive) this.m_resources;
        this.SetBox(rectangle);
        this.SetObligatoryFields();
      }

      public override PdfBrush Clone()
      {
        PdfTilingBrush pdfTilingBrush = new PdfTilingBrush(this.Rectangle, this.m_page, this.Location);
        pdfTilingBrush.m_brushStream.Data = this.m_brushStream.Data;
        pdfTilingBrush.m_resources = new PdfResources((PdfDictionary) this.m_resources);
        pdfTilingBrush.m_brushStream["Resources"] = (IPdfPrimitive) pdfTilingBrush.m_resources;
        return (PdfBrush) pdfTilingBrush;
      }

      private PdfResources GetResources() => this.Resources;

      internal override bool MonitorChanges(
        PdfBrush brush,
        PdfStreamWriter streamWriter,
        PdfGraphics.GetResources getResources,
        bool saveChanges,
        PdfColorSpace currentColorSpace)
      {
        bool flag = false;
        if (brush != this)
        {
          streamWriter.SetColorSpace("Pattern", this.m_bStroking);
          PdfName name = getResources().GetName((IPdfWrapper) this);
          streamWriter.SetColourWithPattern((IList) null, name, this.m_bStroking);
          flag = true;
        }
        return flag;
      }

      internal override bool MonitorChanges(
        PdfBrush brush,
        PdfStreamWriter streamWriter,
        PdfGraphics.GetResources getResources,
        bool saveChanges,
        PdfColorSpace currentColorSpace,
        bool check)
      {
        bool flag = false;
        if (brush != this)
        {
          streamWriter.SetColorSpace("Pattern", this.m_bStroking);
          PdfName name = getResources().GetName((IPdfWrapper) this);
          streamWriter.SetColourWithPattern((IList) null, name, this.m_bStroking);
          flag = true;
        }
        return flag;
      }

      internal override bool MonitorChanges(
        PdfBrush brush,
        PdfStreamWriter streamWriter,
        PdfGraphics.GetResources getResources,
        bool saveChanges,
        PdfColorSpace currentColorSpace,
        bool check,
        bool iccbased)
      {
        bool flag = false;
        if (brush != this)
        {
          streamWriter.SetColorSpace("Pattern", this.m_bStroking);
          PdfName name = getResources().GetName((IPdfWrapper) this);
          streamWriter.SetColourWithPattern((IList) null, name, this.m_bStroking);
          flag = true;
        }
        return flag;
      }

      internal override bool MonitorChanges(
        PdfBrush brush,
        PdfStreamWriter streamWriter,
        PdfGraphics.GetResources getResources,
        bool saveChanges,
        PdfColorSpace currentColorSpace,
        bool check,
        bool iccbased,
        bool indexed)
      {
        bool flag = false;
        if (brush != this)
        {
          streamWriter.SetColorSpace("Pattern", this.m_bStroking);
          PdfName name = getResources().GetName((IPdfWrapper) this);
          streamWriter.SetColourWithPattern((IList) null, name, this.m_bStroking);
          flag = true;
        }
        return flag;
      }

      internal override void ResetChanges(PdfStreamWriter streamWriter)
      {
      }

      private void SetBox(RectangleF box)
      {
        this.m_box = box;
        this.m_brushStream["BBox"] = (IPdfPrimitive) PdfArray.FromRectangle(this.m_box);
      }

      private void SetObligatoryFields()
      {
        this.m_brushStream["PatternType"] = (IPdfPrimitive) new PdfNumber(1);
        this.m_brushStream["PaintType"] = (IPdfPrimitive) new PdfNumber(1);
        this.m_brushStream["TilingType"] = (IPdfPrimitive) new PdfNumber(1);
        this.m_brushStream["XStep"] = (IPdfPrimitive) new PdfNumber(this.m_box.Right - this.m_box.Left);
        this.m_brushStream["YStep"] = (IPdfPrimitive) new PdfNumber(this.m_box.Bottom - this.m_box.Top);
        if (this.m_page == null)
          return;
        this.m_brushStream["Matrix"] = (IPdfPrimitive) new PdfArray(new float[6]
        {
          1f,
          0.0f,
          0.0f,
          1f,
          this.m_location.X,
          this.m_page.Size.Height % this.Rectangle.Size.Height - this.Location.Y
        });
      }

      public PdfGraphics Graphics
      {
        get
        {
          if (this.m_graphics == null)
          {
            this.m_graphics = new PdfGraphics(this.Size, new PdfGraphics.GetResources(this.GetResources), this.m_brushStream);
            this.m_graphics.InitializeCoordinates();
          }
          return this.m_graphics;
        }
      }

      internal PointF Location
      {
        get => this.m_location;
        set => this.m_location = value;
      }

      public RectangleF Rectangle => this.m_box;

      internal PdfResources Resources => this.m_resources;

      public SizeF Size => this.m_box.Size;

      internal bool Stroking
      {
        get => this.m_bStroking;
        set => this.m_bStroking = value;
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_brushStream;
    }
}
