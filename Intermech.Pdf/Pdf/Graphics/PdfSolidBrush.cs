// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfSolidBrush
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.ColorSpace;
using Syncfusion.Pdf.IO;
using System;


namespace Syncfusion.Pdf.Graphics
{
    public sealed class PdfSolidBrush : PdfBrush
    {
      private bool m_bImmutable;
      private PdfColor m_color;
      private PdfColorSpace m_colorSpace;
      private PdfExtendedColor m_colorspaces;

      private PdfSolidBrush()
        : this(new PdfColor((byte) 0, (byte) 0, (byte) 0))
      {
      }

      public PdfSolidBrush(PdfExtendedColor color)
      {
        PdfColorSpaces colorSpace = color.ColorSpace;
        this.m_colorspaces = color;
        switch (color)
        {
          case PdfCalRGBColor _:
            PdfCalRGBColor pdfCalRgbColor = color as PdfCalRGBColor;
            this.m_color = new PdfColor((byte) pdfCalRgbColor.Red, (byte) pdfCalRgbColor.Green, (byte) pdfCalRgbColor.Blue);
            break;
          case PdfCalGrayColor _:
            PdfCalGrayColor pdfCalGrayColor = color as PdfCalGrayColor;
            this.m_color = new PdfColor((float) (byte) pdfCalGrayColor.Gray);
            this.m_color.Gray = Convert.ToSingle(pdfCalGrayColor.Gray);
            break;
          case PdfLabColor _:
            PdfLabColor pdfLabColor = color as PdfLabColor;
            this.m_color = new PdfColor((byte) pdfLabColor.L, (byte) pdfLabColor.A, (byte) pdfLabColor.B);
            break;
          case PdfICCColor _:
            PdfICCColor pdfIccColor = color as PdfICCColor;
            if (pdfIccColor.ColorSpaces.AlternateColorSpace is PdfCalGrayColorSpace)
            {
              this.m_color = new PdfColor((float) (byte) pdfIccColor.ColorComponents[0]);
              this.m_color.Gray = Convert.ToSingle(pdfIccColor.ColorComponents[0]);
              break;
            }
            if (pdfIccColor.ColorSpaces.AlternateColorSpace is PdfCalRGBColorSpace)
            {
              this.m_color = new PdfColor((byte) pdfIccColor.ColorComponents[0], (byte) pdfIccColor.ColorComponents[1], (byte) pdfIccColor.ColorComponents[2]);
              break;
            }
            if (pdfIccColor.ColorSpaces.AlternateColorSpace is PdfLabColorSpace)
            {
              this.m_color = new PdfColor((byte) pdfIccColor.ColorComponents[0], (byte) pdfIccColor.ColorComponents[1], (byte) pdfIccColor.ColorComponents[2]);
              break;
            }
            if (!(pdfIccColor.ColorSpaces.AlternateColorSpace is PdfDeviceColorSpace))
            {
              this.m_color = new PdfColor((byte) pdfIccColor.ColorComponents[0], (byte) pdfIccColor.ColorComponents[1], (byte) pdfIccColor.ColorComponents[2]);
              break;
            }
            switch ((pdfIccColor.ColorSpaces.AlternateColorSpace as PdfDeviceColorSpace).DeviceColorSpaceType.ToString())
            {
              case "RGB":
                this.m_color = new PdfColor((byte) pdfIccColor.ColorComponents[0], (byte) pdfIccColor.ColorComponents[1], (byte) pdfIccColor.ColorComponents[2]);
                return;
              case "GrayScale":
                this.m_color = new PdfColor((float) (byte) pdfIccColor.ColorComponents[0]);
                this.m_color.Gray = Convert.ToSingle(pdfIccColor.ColorComponents[0]);
                return;
              case "CMYK":
                this.m_color = new PdfColor((float) pdfIccColor.ColorComponents[0], (float) pdfIccColor.ColorComponents[1], (float) pdfIccColor.ColorComponents[2], (float) pdfIccColor.ColorComponents[3]);
                return;
              default:
                return;
            }
          case PdfSeparationColor _:
            this.m_color.Gray = (float) (color as PdfSeparationColor).Tint;
            break;
          case PdfIndexedColor _:
            this.m_color.G = (byte) (color as PdfIndexedColor).SelectColorIndex;
            break;
        }
      }

      public PdfSolidBrush(PdfColor color) => this.m_color = color;

      internal PdfSolidBrush(PdfColor color, bool immutable)
        : this(color)
      {
        this.m_bImmutable = immutable;
      }

      public override PdfBrush Clone() => (PdfBrush) (this.MemberwiseClone() as PdfSolidBrush);

      internal override bool MonitorChanges(
        PdfBrush brush,
        PdfStreamWriter streamWriter,
        PdfGraphics.GetResources getResources,
        bool saveChanges,
        PdfColorSpace currentColorSpace)
      {
        if (streamWriter == null)
          throw new ArgumentNullException(nameof (streamWriter));
        if (getResources == null)
          throw new ArgumentNullException(nameof (getResources));
        bool flag1 = false;
        if (brush == null)
        {
          bool flag2 = true;
          streamWriter.SetColorAndSpace(this.m_color, currentColorSpace, false);
          return flag2;
        }
        if (brush == this)
          return flag1;
        if (brush is PdfSolidBrush pdfSolidBrush)
        {
          if (pdfSolidBrush.Color != this.Color || pdfSolidBrush.m_colorSpace != currentColorSpace)
          {
            flag1 = true;
            streamWriter.SetColorAndSpace(this.m_color, currentColorSpace, false);
          }
          return flag1;
        }
        brush.ResetChanges(streamWriter);
        streamWriter.SetColorAndSpace(this.m_color, currentColorSpace, false);
        return true;
      }

      internal override bool MonitorChanges(
        PdfBrush brush,
        PdfStreamWriter streamWriter,
        PdfGraphics.GetResources getResources,
        bool saveChanges,
        PdfColorSpace currentColorSpace,
        bool check)
      {
        if (streamWriter == null)
          throw new ArgumentNullException(nameof (streamWriter));
        if (getResources == null)
          throw new ArgumentNullException(nameof (getResources));
        bool flag1 = false;
        if (brush == null)
        {
          bool flag2 = true;
          streamWriter.SetColorAndSpace(this.m_color, currentColorSpace, false, false);
          return flag2;
        }
        if (brush == this)
          return flag1;
        if (brush is PdfSolidBrush pdfSolidBrush)
        {
          if (pdfSolidBrush.Color != this.Color || pdfSolidBrush.m_colorSpace != currentColorSpace)
          {
            flag1 = true;
            streamWriter.SetColorAndSpace(this.m_color, currentColorSpace, false, false);
          }
          return flag1;
        }
        brush.ResetChanges(streamWriter);
        streamWriter.SetColorAndSpace(this.m_color, currentColorSpace, false);
        return true;
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
        if (streamWriter == null)
          throw new ArgumentNullException(nameof (streamWriter));
        if (getResources == null)
          throw new ArgumentNullException(nameof (getResources));
        bool flag1 = false;
        if (brush == null)
        {
          bool flag2 = true;
          streamWriter.SetColorAndSpace(this.m_color, currentColorSpace, false, false, false);
          return flag2;
        }
        if (brush == this)
          return flag1;
        if (brush is PdfSolidBrush pdfSolidBrush)
        {
          if (pdfSolidBrush.Color != this.Color || pdfSolidBrush.m_colorSpace != currentColorSpace)
          {
            flag1 = true;
            streamWriter.SetColorAndSpace(this.m_color, currentColorSpace, false, false, false);
          }
          return flag1;
        }
        brush.ResetChanges(streamWriter);
        streamWriter.SetColorAndSpace(this.m_color, currentColorSpace, false);
        return true;
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
        if (streamWriter == null)
          throw new ArgumentNullException(nameof (streamWriter));
        if (getResources == null)
          throw new ArgumentNullException(nameof (getResources));
        bool flag1 = false;
        if (brush == null)
        {
          bool flag2 = true;
          streamWriter.SetColorAndSpace(this.m_color, currentColorSpace, false, false, false, false);
          return flag2;
        }
        if (brush == this)
          return flag1;
        if (brush is PdfSolidBrush pdfSolidBrush)
        {
          if (pdfSolidBrush.Color != this.Color || pdfSolidBrush.m_colorSpace != currentColorSpace)
          {
            flag1 = true;
            streamWriter.SetColorAndSpace(this.m_color, currentColorSpace, false, false, false, false);
          }
          return flag1;
        }
        brush.ResetChanges(streamWriter);
        streamWriter.SetColorAndSpace(this.m_color, currentColorSpace, false);
        return true;
      }

      internal override void ResetChanges(PdfStreamWriter streamWriter)
      {
        streamWriter.SetColorAndSpace(new PdfColor((byte) 0, (byte) 0, (byte) 0), PdfColorSpace.RGB, false);
      }

      public PdfColor Color
      {
        get => this.m_color;
        set
        {
          if (this.m_bImmutable)
            throw new ArgumentException("Can't change immutable object.", nameof (Color));
          this.m_color = value;
        }
      }

      internal PdfExtendedColor Colorspaces
      {
        get => this.m_colorspaces;
        set => this.m_colorspaces = value;
      }
    }
}
