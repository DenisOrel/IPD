// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.Pdf3DBackground
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Primitives;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class Pdf3DBackground : IPdfWrapper
{
  private bool m_applyEntire;
  private PdfColor m_backgroundColor;
  private PdfDictionary m_dictionary;
  private const float MaxColourChannelValue = 255f;

  public Pdf3DBackground()
  {
    this.m_dictionary = new PdfDictionary();
    this.Initialize();
  }

  public Pdf3DBackground(PdfColor color)
    : this()
  {
    this.m_backgroundColor = color;
  }

  private void Dictionary_BeginSave(object sender, SavePdfPrimitiveEventArgs ars) => this.Save();

  protected virtual void Initialize()
  {
    this.m_dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.Dictionary_BeginSave);
    this.m_dictionary.SetProperty("Type", (IPdfPrimitive) new PdfName("3DBG"));
  }

  protected virtual void Save()
  {
    this.Dictionary["Subtype"] = (IPdfPrimitive) new PdfName("SC");
    this.Dictionary.SetProperty("CS", (IPdfPrimitive) new PdfName("DeviceRGB"));
    this.Dictionary.SetProperty("EA", (IPdfPrimitive) new PdfBoolean(this.m_applyEntire));
    PdfArray array = new PdfArray();
    array.Insert(0, (IPdfPrimitive) new PdfNumber((float) this.m_backgroundColor.R / (float) byte.MaxValue));
    array.Insert(1, (IPdfPrimitive) new PdfNumber((float) this.m_backgroundColor.G / (float) byte.MaxValue));
    array.Insert(2, (IPdfPrimitive) new PdfNumber((float) this.m_backgroundColor.B / (float) byte.MaxValue));
    this.Dictionary["C"] = (IPdfPrimitive) new PdfArray(array);
  }

  public bool ApplyToEntireAnnotation
  {
    get => this.m_applyEntire;
    set => this.m_applyEntire = value;
  }

  public PdfColor Color
  {
    get => this.m_backgroundColor;
    set => this.m_backgroundColor = value;
  }

  internal PdfDictionary Dictionary => this.m_dictionary;

  IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
}
