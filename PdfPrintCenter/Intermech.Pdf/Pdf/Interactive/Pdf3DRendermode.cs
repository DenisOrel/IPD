// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.Pdf3DRendermode
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Primitives;
using System;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class Pdf3DRendermode : IPdfWrapper
{
  private PdfColor m_auxilaryColor;
  private float m_creaseValue;
  private PdfDictionary m_dictionary;
  private PdfColor m_faceColor;
  private float m_opacity;
  private Pdf3DRenderStyle m_style;

  public Pdf3DRendermode()
  {
    this.m_dictionary = new PdfDictionary();
    this.Initialize();
  }

  public Pdf3DRendermode(Pdf3DRenderStyle style)
    : this()
  {
    this.m_style = style;
  }

  private void Dictionary_BeginSave(object sender, SavePdfPrimitiveEventArgs ars) => this.Save();

  protected virtual void Initialize()
  {
    this.m_dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.Dictionary_BeginSave);
    this.m_dictionary.SetProperty("Type", (IPdfPrimitive) new PdfName("3DRenderMode"));
  }

  protected virtual void Save()
  {
    this.Dictionary["Subtype"] = (IPdfPrimitive) new PdfName((Enum) this.m_style);
    PdfArray array1 = new PdfArray();
    array1.Insert(0, (IPdfPrimitive) new PdfName("DeviceRGB"));
    array1.Insert(1, (IPdfPrimitive) new PdfNumber((float) this.m_auxilaryColor.R / (float) byte.MaxValue));
    array1.Insert(2, (IPdfPrimitive) new PdfNumber((float) this.m_auxilaryColor.G / (float) byte.MaxValue));
    array1.Insert(3, (IPdfPrimitive) new PdfNumber((float) this.m_auxilaryColor.B / (float) byte.MaxValue));
    this.Dictionary["AC"] = (IPdfPrimitive) new PdfArray(array1);
    PdfArray array2 = new PdfArray();
    array2.Insert(0, (IPdfPrimitive) new PdfName("DeviceRGB"));
    array2.Insert(1, (IPdfPrimitive) new PdfNumber((float) this.m_faceColor.R / (float) byte.MaxValue));
    array2.Insert(2, (IPdfPrimitive) new PdfNumber((float) this.m_faceColor.G / (float) byte.MaxValue));
    array2.Insert(3, (IPdfPrimitive) new PdfNumber((float) this.m_faceColor.B / (float) byte.MaxValue));
    this.Dictionary["FC"] = (IPdfPrimitive) new PdfArray(array2);
    this.Dictionary.SetProperty("O", (IPdfPrimitive) new PdfNumber(this.m_opacity));
    this.Dictionary.SetProperty("CV", (IPdfPrimitive) new PdfNumber(this.m_creaseValue));
  }

  public PdfColor AuxilaryColor
  {
    get => this.m_auxilaryColor;
    set => this.m_auxilaryColor = value;
  }

  public float CreaseValue
  {
    get => this.m_creaseValue;
    set => this.m_creaseValue = value;
  }

  internal PdfDictionary Dictionary => this.m_dictionary;

  public PdfColor FaceColor
  {
    get => this.m_faceColor;
    set => this.m_faceColor = value;
  }

  public float Opacity
  {
    get => this.m_opacity;
    set => this.m_opacity = value;
  }

  public Pdf3DRenderStyle Style
  {
    get => this.m_style;
    set => this.m_style = value;
  }

  IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
}
