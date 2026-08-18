// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.ColorSpace.PdfDeviceColorSpace
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;


namespace Syncfusion.Pdf.ColorSpace
{
    public class PdfDeviceColorSpace : PdfColorSpaces
    {
      private PdfColorSpace m_DeviceColorSpaceType;

      public PdfDeviceColorSpace(PdfColorSpace colorspace) => this.m_DeviceColorSpaceType = colorspace;

      public PdfColorSpace DeviceColorSpaceType
      {
        get => this.m_DeviceColorSpaceType;
        set => this.m_DeviceColorSpaceType = value;
      }
    }
}
