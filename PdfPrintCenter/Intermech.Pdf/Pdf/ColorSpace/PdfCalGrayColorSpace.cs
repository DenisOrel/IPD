// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.ColorSpace.PdfCalGrayColorSpace
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;

#nullable disable
namespace Syncfusion.Pdf.ColorSpace;

public class PdfCalGrayColorSpace : PdfColorSpaces, IPdfWrapper
{
  private double[] m_blackPoint;
  private double m_gama = 1.0;
  private double[] m_whitePoint = new double[3]
  {
    0.9505,
    1.0,
    1.089
  };

  public PdfCalGrayColorSpace() => this.Initialize();

  private PdfArray CreateInternals()
  {
    PdfArray internals = new PdfArray();
    if (internals != null)
    {
      PdfName element1 = new PdfName("CalGray");
      internals.Add((IPdfPrimitive) element1);
      PdfDictionary element2 = new PdfDictionary();
      element2.SetProperty("WhitePoint", (IPdfPrimitive) new PdfArray(this.m_whitePoint));
      element2.SetProperty("Gamma", (IPdfPrimitive) new PdfNumber(this.m_gama));
      if (this.m_blackPoint != null)
        element2.SetProperty("BlackPoint", (IPdfPrimitive) new PdfArray(this.m_blackPoint));
      internals.Add((IPdfPrimitive) element2);
    }
    return internals;
  }

  private void Initialize()
  {
    lock (PdfColorSpaces.s_syncObject)
    {
      IPdfCache pdfCache = PdfDocument.Cache.Search((IPdfCache) this);
      ((IPdfCache) this).SetInternals(pdfCache != null ? pdfCache.GetInternals() : (IPdfPrimitive) this.CreateInternals());
    }
  }

  public double[] BlackPoint
  {
    get => this.m_blackPoint;
    set
    {
      this.m_blackPoint = value == null || value.Length == 3 ? value : throw new ArgumentOutOfRangeException(nameof (BlackPoint), "BlackPoint array must have 3 values.");
      this.Initialize();
    }
  }

  public double Gamma
  {
    get => this.m_gama;
    set
    {
      this.m_gama = value;
      this.Initialize();
    }
  }

  public double[] WhitePoint
  {
    get => this.m_whitePoint;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (WhitePoint), "WhitePoint array cannot be null.");
      this.m_whitePoint = value.Length == 3 ? value : throw new ArgumentOutOfRangeException(nameof (WhitePoint), "WhitePoint array must have 3 values.");
      this.Initialize();
    }
  }
}
