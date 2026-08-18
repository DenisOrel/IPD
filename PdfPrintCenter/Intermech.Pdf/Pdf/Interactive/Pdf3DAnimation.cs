// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.Pdf3DAnimation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class Pdf3DAnimation : IPdfWrapper
{
  private PdfDictionary m_dictionary;
  private int m_playCount;
  private float m_timeMultiplier;
  private PDF3DAnimationType m_type;

  public Pdf3DAnimation()
  {
    this.m_dictionary = new PdfDictionary();
    this.Initialize();
  }

  public Pdf3DAnimation(PDF3DAnimationType type)
    : this()
  {
    this.m_type = type;
  }

  private void Dictionary_BeginSave(object sender, SavePdfPrimitiveEventArgs ars) => this.Save();

  protected virtual void Initialize()
  {
    this.m_dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.Dictionary_BeginSave);
    this.m_dictionary.SetProperty("Type", (IPdfPrimitive) new PdfName("3DAnimationStyle"));
  }

  protected virtual void Save()
  {
    this.Dictionary["Subtype"] = (IPdfPrimitive) new PdfName((Enum) this.m_type);
    this.Dictionary.SetProperty("PC", (IPdfPrimitive) new PdfNumber(this.m_playCount));
    this.Dictionary.SetProperty("TM", (IPdfPrimitive) new PdfNumber(this.m_timeMultiplier));
  }

  internal PdfDictionary Dictionary => this.m_dictionary;

  public int PlayCount
  {
    get => this.m_playCount;
    set => this.m_playCount = value;
  }

  IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;

  public float TimeMultiplier
  {
    get => this.m_timeMultiplier;
    set => this.m_timeMultiplier = value;
  }

  public PDF3DAnimationType Type
  {
    get => this.m_type;
    set => this.m_type = value;
  }
}
