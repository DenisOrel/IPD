// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfTemplate
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;
using System.IO;


namespace Syncfusion.Pdf.Graphics
{
    public class PdfTemplate : PdfShapeElement, IPdfWrapper
    {
      private bool m_bIsReadonly;
      internal PdfStream m_content;
      private PdfGraphics m_graphics;
      private PdfResources m_resources;
      private SizeF m_size;
      private bool m_writeTransformation;

      internal PdfTemplate(PdfStream template)
      {
        this.m_writeTransformation = true;
        this.m_content = template != null ? template : throw new ArgumentNullException(nameof (template));
        this.m_size = (PdfCrossTable.Dereference(this.m_content["BBox"]) as PdfArray).ToRectangle().Size;
        this.m_bIsReadonly = true;
      }

      public PdfTemplate(SizeF size)
        : this(size.Width, size.Height)
      {
      }

      internal PdfTemplate(SizeF size, bool writeTransformation)
        : this(size.Width, size.Height)
      {
        this.m_writeTransformation = writeTransformation;
      }

      public PdfTemplate(float width, float height)
      {
        this.m_writeTransformation = true;
        this.m_content = new PdfStream();
        this.SetSize(new SizeF(width, height));
        this.Initialize();
      }

      internal PdfTemplate(SizeF size, MemoryStream stream, PdfDictionary resources)
      {
        this.m_writeTransformation = true;
        if (size == SizeF.Empty)
          throw new ArgumentException("The size of the new PdfTemplate can't be empty.");
        if (stream == null)
          throw new ArgumentNullException(nameof (stream));
        this.m_content = new PdfStream();
        this.SetSize(size);
        this.Initialize();
        stream.WriteTo((Stream) this.m_content.InternalStream);
        if (resources != null)
        {
          this.m_content["Resources"] = (IPdfPrimitive) new PdfDictionary(resources);
          this.m_resources = new PdfResources(resources);
        }
        this.m_bIsReadonly = true;
      }

      internal PdfTemplate(PointF origin, SizeF size, MemoryStream stream, PdfDictionary resources)
      {
        this.m_writeTransformation = true;
        if (size == SizeF.Empty)
          throw new ArgumentException("The size of the new PdfTemplate can't be empty.");
        if (stream == null)
          throw new ArgumentNullException(nameof (stream));
        this.m_content = new PdfStream();
        if ((double) origin.X < 0.0 || (double) origin.Y < 0.0)
          this.SetSize(origin, size);
        else
          this.SetSize(size);
        this.Initialize();
        stream.WriteTo((Stream) this.m_content.InternalStream);
        if (resources != null)
        {
          this.m_content["Resources"] = (IPdfPrimitive) new PdfDictionary(resources);
          this.m_resources = new PdfResources(resources);
        }
        this.m_bIsReadonly = true;
      }

      private void AddSubType()
      {
        this.m_content["Subtype"] = (IPdfPrimitive) this.m_content.GetName("Form");
      }

      private void AddType()
      {
        this.m_content["Type"] = (IPdfPrimitive) this.m_content.GetName("XObject");
      }

      internal void CloneResources(PdfCrossTable crossTable)
      {
        if (this.m_resources == null)
          return;
        PdfDictionary baseDictionary = this.m_resources.Clone(crossTable) as PdfDictionary;
        this.m_resources = new PdfResources(baseDictionary);
        this.m_content["Resources"] = (IPdfPrimitive) baseDictionary;
      }

      protected override void DrawInternal(PdfGraphics graphics)
      {
        if (graphics == null)
          throw new ArgumentNullException(nameof (graphics));
        graphics.DrawPdfTemplate(this, PointF.Empty);
      }

      protected override RectangleF GetBoundsInternal() => new RectangleF(PointF.Empty, this.Size);

      private PdfResources GetResources()
      {
        if (this.m_resources == null)
        {
          this.m_resources = new PdfResources();
          this.m_content["Resources"] = (IPdfPrimitive) this.m_resources;
        }
        return this.m_resources;
      }

      private void Initialize()
      {
        this.AddType();
        this.AddSubType();
      }

      public void Reset()
      {
        if (this.m_resources != null)
        {
          this.m_resources = (PdfResources) null;
          this.m_content.Remove("Resources");
        }
        if (this.m_graphics == null)
          return;
        this.m_graphics.Reset(this.Size);
      }

      public void Reset(SizeF size)
      {
        this.SetSize(size);
        this.Reset();
      }

      private void SetSize(SizeF size)
      {
        this.m_content["BBox"] = (IPdfPrimitive) PdfArray.FromRectangle(new RectangleF(PointF.Empty, size));
        this.m_size = size;
      }

      private void SetSize(PointF origin, SizeF size)
      {
        this.m_content["BBox"] = (IPdfPrimitive) new PdfArray(new float[4]
        {
          origin.X,
          origin.Y,
          size.Width,
          size.Height
        });
        this.m_size = size;
      }

      public PdfGraphics Graphics
      {
        get
        {
          if (this.m_bIsReadonly)
            this.m_graphics = (PdfGraphics) null;
          else if (this.m_graphics == null)
          {
            this.m_graphics = new PdfGraphics(this.Size, new PdfGraphics.GetResources(this.GetResources), this.m_content);
            if (this.m_writeTransformation)
              this.m_graphics.InitializeCoordinates();
          }
          return this.m_graphics;
        }
      }

      public float Height => this.Size.Height;

      public bool ReadOnly => this.m_bIsReadonly;

      public SizeF Size => this.m_size;

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_content;

      public float Width => this.Size.Width;
    }
}
