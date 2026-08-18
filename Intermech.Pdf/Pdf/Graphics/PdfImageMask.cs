// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfImageMask
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Drawing.Imaging;


namespace Syncfusion.Pdf.Graphics
{
    public class PdfImageMask : PdfMask
    {
      private PdfBitmap m_imageMask;
      private bool m_softMask;

      public PdfImageMask(PdfBitmap imageMask)
      {
        if (imageMask == null)
          throw new ArgumentNullException(nameof (imageMask));
        switch (imageMask.InternalImage.PixelFormat)
        {
          case PixelFormat.Format1bppIndexed:
            this.m_softMask = false;
            break;
          case PixelFormat.Format8bppIndexed:
            this.m_softMask = true;
            break;
          default:
            throw new ArgumentException(nameof (imageMask), "Image mask should be gray scale or black and white.");
        }
        this.m_imageMask = imageMask;
      }

      public PdfBitmap Mask => this.m_imageMask;

      public bool SoftMask => this.m_softMask;
    }
}
