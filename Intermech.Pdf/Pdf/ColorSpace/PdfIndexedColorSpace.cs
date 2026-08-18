// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.ColorSpace.PdfIndexedColorSpace
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Primitives;


namespace Syncfusion.Pdf.ColorSpace
{
    public class PdfIndexedColorSpace : PdfColorSpaces
    {
      private PdfColorSpaces m_basecolorspace = (PdfColorSpaces) new PdfDeviceColorSpace(PdfColorSpace.RGB);
      private byte[] m_indexedColorTable;
      private int m_maxColorIndex;
      private PdfStream m_stream = new PdfStream();

      public PdfIndexedColorSpace()
      {
        this.m_stream.BeginSave += new SavePdfPrimitiveEventHandler(this.Stream_BeginSave);
        this.Initialize();
      }

      private PdfArray CreateInternals()
      {
        PdfArray internals = new PdfArray();
        if (internals == null)
          return internals;
        PdfName element1 = new PdfName("Indexed");
        internals.Add((IPdfPrimitive) element1);
        PdfReferenceHolder element2 = new PdfReferenceHolder((IPdfPrimitive) this.m_stream);
        if (this.m_basecolorspace != null)
        {
          if (this.m_basecolorspace is PdfCalGrayColorSpace)
          {
            PdfReferenceHolder element3 = new PdfReferenceHolder((IPdfWrapper) this.m_basecolorspace);
            internals.Add((IPdfPrimitive) element3);
          }
          else if (this.m_basecolorspace is PdfCalRGBColorSpace)
          {
            PdfReferenceHolder element4 = new PdfReferenceHolder((IPdfWrapper) this.m_basecolorspace);
            internals.Add((IPdfPrimitive) element4);
          }
          else if (this.m_basecolorspace is PdfLabColorSpace)
          {
            PdfReferenceHolder element5 = new PdfReferenceHolder((IPdfWrapper) this.m_basecolorspace);
            internals.Add((IPdfPrimitive) element5);
          }
          else if (this.m_basecolorspace is PdfDeviceColorSpace)
          {
            switch ((this.m_basecolorspace as PdfDeviceColorSpace).DeviceColorSpaceType.ToString())
            {
              case "RGB":
                PdfName element6 = new PdfName("DeviceRGB");
                internals.Add((IPdfPrimitive) element6);
                break;
              case "CMYK":
                PdfName element7 = new PdfName("DeviceCMYK");
                internals.Add((IPdfPrimitive) element7);
                break;
              case "GrayScale":
                PdfName element8 = new PdfName("DeviceGray");
                internals.Add((IPdfPrimitive) element8);
                break;
            }
          }
          internals.Add((IPdfPrimitive) new PdfNumber(this.m_maxColorIndex));
        }
        internals.Add((IPdfPrimitive) element2);
        return internals;
      }

      public byte[] GetProfileData()
      {
        byte[] numArray = new byte[1000];
        return this.m_indexedColorTable;
      }

      private void Initialize()
      {
        lock (PdfColorSpaces.s_syncObject)
        {
          IPdfCache pdfCache = PdfDocument.Cache.Search((IPdfCache) this);
          ((IPdfCache) this).SetInternals(pdfCache != null ? pdfCache.GetInternals() : (IPdfPrimitive) this.CreateInternals());
        }
      }

      protected void Save()
      {
        byte[] buffer = this.m_indexedColorTable != null ? this.m_indexedColorTable : this.GetProfileData();
        this.m_stream.Clear();
        this.m_stream.InternalStream.Write(buffer, 0, buffer.Length);
      }

      private void Stream_BeginSave(object sender, SavePdfPrimitiveEventArgs ars) => this.Save();

      public PdfColorSpaces BaseColorSpace
      {
        get => this.m_basecolorspace;
        set
        {
          this.m_basecolorspace = value;
          this.Initialize();
        }
      }

      public byte[] IndexedColorTable
      {
        get => this.m_indexedColorTable;
        set
        {
          this.m_indexedColorTable = value;
          this.Initialize();
        }
      }

      public int MaxColorIndex
      {
        get => this.m_maxColorIndex;
        set
        {
          this.m_maxColorIndex = value;
          this.Initialize();
        }
      }
    }
}
