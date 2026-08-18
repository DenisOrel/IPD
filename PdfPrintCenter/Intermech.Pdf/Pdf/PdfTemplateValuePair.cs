// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfTemplateValuePair
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;

#nullable disable
namespace Syncfusion.Pdf;

internal class PdfTemplateValuePair
{
  private PdfTemplate m_template;
  private string m_value;

  public PdfTemplateValuePair() => this.m_value = string.Empty;

  public PdfTemplateValuePair(PdfTemplate template, string value)
  {
    this.m_value = string.Empty;
    this.Template = template;
    this.Value = value;
  }

  public PdfTemplate Template
  {
    get => this.m_template;
    set
    {
      this.m_template = value != null ? value : throw new ArgumentNullException(nameof (Template));
    }
  }

  public string Value
  {
    get => this.m_value;
    set
    {
      this.m_value = this.m_value != null ? value : throw new ArgumentNullException(nameof (Value));
    }
  }
}
