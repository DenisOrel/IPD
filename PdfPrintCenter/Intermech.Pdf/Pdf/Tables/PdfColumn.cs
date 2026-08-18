// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Tables.PdfColumn
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;

#nullable disable
namespace Syncfusion.Pdf.Tables;

public class PdfColumn
{
  private const float DefaultWidth = 10f;
  private string m_columnName;
  private PdfStringFormat m_stringFormat;
  private float m_width;

  public PdfColumn()
  {
  }

  internal PdfColumn(float width)
    : this()
  {
    this.Width = width;
  }

  public PdfColumn(string columnName)
  {
    this.m_columnName = columnName;
    this.m_width = 10f;
  }

  public string ColumnName
  {
    get => this.m_columnName;
    set => this.m_columnName = value;
  }

  public PdfStringFormat StringFormat
  {
    get => this.m_stringFormat;
    set => this.m_stringFormat = value;
  }

  public float Width
  {
    get => this.m_width;
    set
    {
      this.m_width = (double) value >= 0.0 ? value : throw new ArgumentException("The width should be a positive number.", nameof (Width));
    }
  }
}
