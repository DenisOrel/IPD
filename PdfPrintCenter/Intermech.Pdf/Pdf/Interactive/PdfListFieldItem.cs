// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfListFieldItem
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfListFieldItem : IPdfWrapper
{
  private int c_textIndex;
  private int c_valueIndex;
  private PdfArray m_array;
  private string m_text;
  private string m_value;

  public PdfListFieldItem()
  {
    this.c_textIndex = 1;
    this.m_text = string.Empty;
    this.m_value = string.Empty;
    this.m_array = new PdfArray();
    this.Initialize(this.m_text, this.m_value);
  }

  public PdfListFieldItem(string text, string value)
  {
    this.c_textIndex = 1;
    this.m_text = string.Empty;
    this.m_value = string.Empty;
    this.m_array = new PdfArray();
    this.Initialize(text, value);
  }

  private void Initialize(string text, string value)
  {
    if (this.c_valueIndex < this.c_textIndex)
    {
      this.m_array.Add((IPdfPrimitive) new PdfString(value));
      this.m_array.Add((IPdfPrimitive) new PdfString(text));
    }
    else
    {
      this.m_array.Add((IPdfPrimitive) new PdfString(text));
      this.m_array.Add((IPdfPrimitive) new PdfString(value));
    }
    this.m_text = text;
    this.m_value = value;
  }

  IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_array;

  public string Text
  {
    get => this.m_text;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (Text));
      if (!(this.m_text != value))
        return;
      this.m_text = value;
      ((PdfString) this.m_array[this.c_textIndex]).Value = this.m_text;
    }
  }

  public string Value
  {
    get => this.m_value;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (Value));
      if (!(this.m_value != value))
        return;
      this.m_value = value;
      ((PdfString) this.m_array[this.c_valueIndex]).Value = this.m_value;
    }
  }
}
