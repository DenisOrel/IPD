// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfCompositeField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;

#nullable disable
namespace Syncfusion.Pdf;

public class PdfCompositeField : PdfMultipleValueField
{
  private PdfAutomaticField[] m_automaticFields;
  private string m_text;

  public PdfCompositeField() => this.m_text = string.Empty;

  public PdfCompositeField(PdfFont font)
    : base(font)
  {
    this.m_text = string.Empty;
  }

  public PdfCompositeField(PdfFont font, PdfBrush brush)
    : base(font, brush)
  {
    this.m_text = string.Empty;
  }

  public PdfCompositeField(PdfFont font, string text)
    : base(font)
  {
    this.m_text = string.Empty;
    this.Text = text;
  }

  public PdfCompositeField(string text, params PdfAutomaticField[] list)
  {
    this.m_text = string.Empty;
    this.m_automaticFields = list;
    this.Text = text;
  }

  public PdfCompositeField(PdfFont font, PdfBrush brush, string text)
    : base(font, brush)
  {
    this.m_text = string.Empty;
    this.Text = text;
  }

  public PdfCompositeField(PdfFont font, string text, params PdfAutomaticField[] list)
    : base(font)
  {
    this.m_text = string.Empty;
    this.Text = text;
    this.m_automaticFields = list;
  }

  public PdfCompositeField(
    PdfFont font,
    PdfBrush brush,
    string text,
    params PdfAutomaticField[] list)
    : base(font, brush)
  {
    this.m_text = string.Empty;
    this.Text = text;
    this.m_automaticFields = list;
  }

  protected internal override string GetValue(PdfGraphics graphics)
  {
    if (this.m_automaticFields == null || this.m_automaticFields.Length == 0)
      return this.m_text;
    string[] strArray = new string[this.m_automaticFields.Length];
    int num = 0;
    foreach (PdfAutomaticField automaticField in this.m_automaticFields)
      strArray[num++] = automaticField.GetValue(graphics);
    return string.Format(this.m_text, (object[]) strArray);
  }

  public PdfAutomaticField[] AutomaticFields
  {
    get => this.m_automaticFields;
    set => this.m_automaticFields = value;
  }

  public string Text
  {
    get => this.m_text;
    set => this.m_text = value != null ? value : throw new ArgumentNullException(nameof (Text));
  }
}
