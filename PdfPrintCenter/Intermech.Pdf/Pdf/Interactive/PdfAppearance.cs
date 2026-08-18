// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfAppearance
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Primitives;
using System;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfAppearance : IPdfWrapper
{
  private PdfAnnotation m_annotation;
  private PdfDictionary m_dictionary = new PdfDictionary();
  private PdfTemplate m_templateMouseHover;
  private PdfTemplate m_templateNormal;
  private PdfTemplate m_templatePressed;

  public PdfAppearance(PdfAnnotation annotation)
  {
    this.m_annotation = annotation;
    PdfGraphics graphics = this.Normal.Graphics;
  }

  internal PdfTemplate GetNormalTemplate() => this.m_templateNormal;

  internal PdfTemplate GetPressedTemplate() => this.m_templatePressed;

  public PdfTemplate MouseHover
  {
    get
    {
      if (this.m_templateMouseHover == null)
      {
        this.m_templateMouseHover = new PdfTemplate(this.m_annotation.Size);
        this.m_dictionary.SetProperty("R", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_templateMouseHover));
      }
      return this.m_templateMouseHover;
    }
    set
    {
      if (this.m_templateMouseHover == value)
        return;
      this.m_templateMouseHover = value;
      this.m_dictionary.SetProperty("R", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_templateMouseHover));
    }
  }

  public PdfTemplate Normal
  {
    get
    {
      if (this.m_templateNormal == null)
      {
        this.m_templateNormal = new PdfTemplate(this.m_annotation.Size);
        this.m_dictionary.SetProperty("N", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_templateNormal));
      }
      return this.m_templateNormal;
    }
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (Normal));
      if (this.m_templateNormal == value)
        return;
      this.m_templateNormal = value;
      this.m_dictionary.SetProperty("N", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_templateNormal));
    }
  }

  public PdfTemplate Pressed
  {
    get
    {
      if (this.m_templatePressed == null)
      {
        this.m_templatePressed = new PdfTemplate(this.m_annotation.Size);
        this.m_dictionary.SetProperty("D", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_templatePressed));
      }
      return this.m_templatePressed;
    }
    set
    {
      if (value == this.m_templatePressed)
        return;
      this.m_templatePressed = value;
      this.m_dictionary.SetProperty("D", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_templatePressed));
    }
  }

  IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
}
