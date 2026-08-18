// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Fonts.StandardWidthTable
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;

#nullable disable
namespace Syncfusion.Pdf.Graphics.Fonts;

internal class StandardWidthTable : WidthTable
{
  private int[] m_widths;

  internal StandardWidthTable(int[] widths)
  {
    this.m_widths = widths != null ? widths : throw new ArgumentNullException(nameof (widths));
  }

  public override WidthTable Clone()
  {
    StandardWidthTable standardWidthTable = this.MemberwiseClone() as StandardWidthTable;
    standardWidthTable.m_widths = (int[]) this.m_widths.Clone();
    return (WidthTable) standardWidthTable;
  }

  internal override PdfArray ToArray() => new PdfArray(this.m_widths);

  public override int this[int index]
  {
    get
    {
      if (index < 0 || index >= this.m_widths.Length)
        throw new ArgumentOutOfRangeException(nameof (index), "The character is not supported by the font.");
      return this.m_widths[index];
    }
  }

  public int Length => this.m_widths.Length;
}
