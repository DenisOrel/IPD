// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfFieldActions
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfFieldActions : IPdfWrapper
{
  private PdfAnnotationActions m_annotationActions;
  private PdfJavaScriptAction m_calculate;
  private PdfDictionary m_dictionary = new PdfDictionary();
  private PdfJavaScriptAction m_format;
  private PdfJavaScriptAction m_keyPressed;
  private PdfJavaScriptAction m_validate;

  public PdfFieldActions(PdfAnnotationActions annotationActions)
  {
    this.m_annotationActions = annotationActions != null ? annotationActions : throw new ArgumentNullException("annotationActrions");
  }

  public PdfJavaScriptAction Calculate
  {
    get => this.m_calculate;
    set
    {
      if (this.m_calculate == value)
        return;
      this.m_calculate = value;
      this.m_dictionary.SetProperty("C", (IPdfWrapper) this.m_calculate);
    }
  }

  public PdfJavaScriptAction Format
  {
    get => this.m_format;
    set
    {
      if (this.m_format == value)
        return;
      this.m_format = value;
      this.m_dictionary.SetProperty("F", (IPdfWrapper) this.m_format);
    }
  }

  public PdfAction GotFocus
  {
    get => this.m_annotationActions.GotFocus;
    set => this.m_annotationActions.GotFocus = value;
  }

  public PdfJavaScriptAction KeyPressed
  {
    get => this.m_keyPressed;
    set
    {
      if (this.m_keyPressed == value)
        return;
      this.m_keyPressed = value;
      this.m_dictionary.SetProperty("K", (IPdfWrapper) this.m_keyPressed);
    }
  }

  public PdfAction LostFocus
  {
    get => this.m_annotationActions.LostFocus;
    set => this.m_annotationActions.LostFocus = value;
  }

  public PdfAction MouseDown
  {
    get => this.m_annotationActions.MouseDown;
    set => this.m_annotationActions.MouseDown = value;
  }

  public PdfAction MouseEnter
  {
    get => this.m_annotationActions.MouseEnter;
    set => this.m_annotationActions.MouseEnter = value;
  }

  public PdfAction MouseLeave
  {
    get => this.m_annotationActions.MouseLeave;
    set => this.m_annotationActions.MouseLeave = value;
  }

  public PdfAction MouseUp
  {
    get => this.m_annotationActions.MouseUp;
    set => this.m_annotationActions.MouseUp = value;
  }

  IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;

  public PdfJavaScriptAction Validate
  {
    get => this.m_validate;
    set
    {
      if (this.m_validate == value)
        return;
      this.m_validate = value;
      this.m_dictionary.SetProperty("V", (IPdfWrapper) this.m_validate);
    }
  }
}
