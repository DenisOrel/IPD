// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfPageLayer
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf;

public class PdfPageLayer : IPdfWrapper
{
  private bool m_bSaved;
  private bool m_clipPageTemplates;
  private PdfColorSpace m_colorspace;
  private PdfStream m_content;
  internal long m_contentLength;
  private PdfGraphics m_graphics;
  private PdfGraphicsState m_graphicsState;
  private PdfPageLayerCollection m_layer;
  private string m_layerid;
  private string m_name;
  private PdfPageBase m_page;
  internal PdfDictionary m_printOption;
  internal bool m_sublayer;
  internal PdfDictionary m_usage;
  private bool m_visible;
  private PdfPrintState printState;

  public PdfPageLayer(PdfPageBase page)
  {
    this.m_visible = true;
    this.m_page = page != null ? page : throw new ArgumentNullException(nameof (page));
    this.m_clipPageTemplates = true;
    this.m_content = new PdfStream();
  }

  internal PdfPageLayer(PdfPageBase page, PdfStream stream)
  {
    this.m_visible = true;
    if (page == null)
      throw new ArgumentNullException(nameof (page));
    if (stream == null)
      throw new ArgumentNullException(nameof (stream));
    this.m_page = page;
    this.m_content = stream;
  }

  internal PdfPageLayer(PdfPageBase page, bool clipPageTemplates)
    : this(page)
  {
    this.m_clipPageTemplates = clipPageTemplates;
  }

  public PdfPageLayer Add()
  {
    return new PdfPageLayer(this.m_page)
    {
      Name = string.Empty
    };
  }

  private void BeginSaveContent(object sender, SavePdfPrimitiveEventArgs e)
  {
    if (this.m_graphicsState != null)
    {
      this.Graphics.Restore(this.m_graphicsState);
      this.m_graphicsState = (PdfGraphicsState) null;
    }
    this.m_bSaved = true;
  }

  internal void Clear()
  {
    if (this.m_graphics != null)
      this.m_graphics.StreamWriter.Clear();
    if (this.m_content != null)
      this.m_content = (PdfStream) null;
    if (this.m_graphics == null)
      return;
    this.m_graphics = (PdfGraphics) null;
  }

  private void InitializeGraphics(PdfPageBase page)
  {
    PdfPage page1 = page as PdfPage;
    if (this.m_graphics == null)
    {
      PdfGraphics.GetResources resources = new PdfGraphics.GetResources(this.Page.GetResources);
      PdfArray pdfArray = page.Dictionary.GetValue("MediaBox", "Parent") as PdfArray;
      float floatValue1 = (pdfArray[0] as PdfNumber).FloatValue;
      float floatValue2 = (pdfArray[1] as PdfNumber).FloatValue;
      float floatValue3 = (pdfArray[2] as PdfNumber).FloatValue;
      float floatValue4 = (pdfArray[3] as PdfNumber).FloatValue;
      if (((double) floatValue1 < 0.0 || (double) floatValue2 < 0.0 || (double) floatValue3 < 0.0 || (double) floatValue4 < 0.0) && Math.Floor((double) Math.Abs(floatValue2)) == Math.Floor((double) Math.Abs(page.Size.Height)) && Math.Floor((double) Math.Abs(floatValue3)) == Math.Floor((double) page.Size.Width))
      {
        RectangleF rectangleF = new RectangleF(Math.Min(floatValue1, floatValue3), Math.Min(floatValue2, floatValue4), Math.Max(floatValue1, floatValue3), Math.Max(floatValue2, floatValue4));
        this.m_graphics = new PdfGraphics(new SizeF(rectangleF.Width, rectangleF.Height), resources, this.m_content);
      }
      else
        this.m_graphics = new PdfGraphics(page.Size, resources, this.m_content);
      if (page1 != null)
      {
        PdfSectionCollection parent = page1.Section.Parent;
        if (parent != null)
        {
          this.m_graphics.ColorSpace = parent.Document.ColorSpace;
          this.Colorspace = parent.Document.ColorSpace;
        }
      }
      this.m_content.BeginSave += new SavePdfPrimitiveEventHandler(this.BeginSaveContent);
    }
    this.m_graphicsState = this.m_graphics.Save();
    if (!string.IsNullOrEmpty(this.m_name))
      this.m_content.Write(Encoding.ASCII.GetBytes($"/OC /{this.LayerId} BDC\n"));
    if ((double) page.Origin.X >= 0.0 && (double) page.Origin.Y >= 0.0)
      this.m_graphics.InitializeCoordinates();
    else
      this.m_graphics.InitializeCoordinates(page);
    if (PdfGraphics.TransparencyObject)
      this.m_graphics.SetTransparencyGroup(page);
    if (page.Dictionary.ContainsKey("Rotate"))
    {
      if (!(page.Dictionary["Rotate"] is PdfNumber pdfNumber))
        pdfNumber = PdfCrossTable.Dereference(page.Dictionary["Rotate"]) as PdfNumber;
      if ((double) pdfNumber.FloatValue == 90.0)
      {
        page.Graphics.TranslateTransform(0.0f, page.Size.Height);
        page.Graphics.RotateTransform(-90f);
        page.Graphics.m_clipBounds.Size = new SizeF(page.Size.Height, page.Size.Width);
      }
      else if ((double) pdfNumber.FloatValue == 180.0)
      {
        PdfGraphics graphics = page.Graphics;
        SizeF size = page.Size;
        double width = (double) size.Width;
        size = page.Size;
        double height = (double) size.Height;
        graphics.TranslateTransform((float) width, (float) height);
        page.Graphics.RotateTransform(-180f);
      }
      else if ((double) pdfNumber.FloatValue == 270.0)
      {
        PdfGraphics graphics = page.Graphics;
        SizeF size = page.Size;
        double width1 = (double) size.Width;
        graphics.TranslateTransform((float) width1, 0.0f);
        page.Graphics.RotateTransform(-270f);
        ref RectangleF local = ref page.Graphics.m_clipBounds;
        size = page.Size;
        double height = (double) size.Height;
        size = page.Size;
        double width2 = (double) size.Width;
        SizeF sizeF = new SizeF((float) height, (float) width2);
        local.Size = sizeF;
      }
    }
    if (page1 != null)
    {
      RectangleF actualBounds = page1.Section.GetActualBounds(page1, true);
      PdfMargins margins = page1.Section.PageSettings.Margins;
      if (this.m_clipPageTemplates)
      {
        if ((double) page.Origin.X >= 0.0 && (double) page.Origin.Y >= 0.0)
          this.m_graphics.ClipTranslateMargins(actualBounds);
      }
      else
        this.m_graphics.ClipTranslateMargins(actualBounds.X, actualBounds.Y, margins.Left, margins.Top, margins.Right, margins.Bottom);
    }
    this.m_graphics.SetLayer(this);
    this.m_bSaved = false;
  }

  internal PdfColorSpace Colorspace
  {
    get => this.m_colorspace;
    set => this.m_colorspace = value;
  }

  public PdfGraphics Graphics
  {
    get
    {
      if (this.m_graphics == null || this.m_bSaved)
        this.InitializeGraphics(this.Page);
      return this.m_graphics;
    }
  }

  internal string LayerId
  {
    get => this.m_layerid;
    set => this.m_layerid = value;
  }

  public PdfPageLayerCollection Layers
  {
    get
    {
      if (this.m_layer == null)
        this.m_layer = new PdfPageLayerCollection(this.Page);
      this.m_layer.m_sublayer = true;
      return this.m_layer;
    }
  }

  public string Name
  {
    get => this.m_name;
    set => this.m_name = value;
  }

  public PdfPageBase Page => this.m_page;

  public PdfPrintState PrintState
  {
    get => this.printState;
    set
    {
      this.printState = value;
      if (this.m_printOption == null)
        return;
      if (this.printState.Equals((object) PdfPrintState.AlwaysPrint))
      {
        this.m_printOption.SetProperty(nameof (PrintState), (IPdfPrimitive) new PdfName("ON"));
      }
      else
      {
        if (!this.PrintState.Equals((object) PdfPrintState.NeverPrint))
          return;
        this.m_printOption.SetProperty(nameof (PrintState), (IPdfPrimitive) new PdfName("OFF"));
      }
    }
  }

  IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_content;

  internal bool Visible
  {
    get => this.m_visible;
    set => this.m_visible = value;
  }
}
