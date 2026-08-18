// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.Pdf3DLighting
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class Pdf3DLighting : IPdfWrapper
{
  private PdfDictionary m_dictionary;
  private Pdf3DLightingStyle m_lightingStyle;

  public Pdf3DLighting()
  {
    this.m_dictionary = new PdfDictionary();
    this.Initialize();
  }

  public Pdf3DLighting(Pdf3DLightingStyle style)
    : this()
  {
    this.m_lightingStyle = style;
  }

  private void Dictionary_BeginSave(object sender, SavePdfPrimitiveEventArgs ars) => this.Save();

  protected virtual void Initialize()
  {
    this.m_dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.Dictionary_BeginSave);
    this.m_dictionary.SetProperty("Type", (IPdfPrimitive) new PdfName("3DLightingScheme"));
  }

  protected virtual void Save()
  {
    this.Dictionary.SetProperty("Subtype", (IPdfPrimitive) new PdfName((Enum) this.m_lightingStyle));
  }

  internal PdfDictionary Dictionary => this.m_dictionary;

  public Pdf3DLightingStyle Style
  {
    get => this.m_lightingStyle;
    set => this.m_lightingStyle = value;
  }

  IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
}
