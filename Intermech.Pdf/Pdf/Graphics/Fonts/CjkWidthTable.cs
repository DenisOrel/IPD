// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Fonts.CjkWidthTable
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;
using System.Collections.Generic;


namespace Syncfusion.Pdf.Graphics.Fonts
{
    internal class CjkWidthTable : WidthTable
    {
      private int m_defaultWidth;
      private List<CjkWidth> m_width = new List<CjkWidth>();

      public CjkWidthTable(int defaultWidth) => this.m_defaultWidth = defaultWidth;

      public void Add(CjkWidth widths)
      {
        if (widths == null)
          throw new ArgumentNullException(nameof (widths));
        this.m_width.Add(widths);
      }

      public override WidthTable Clone()
      {
        CjkWidthTable cjkWidthTable = this.MemberwiseClone() as CjkWidthTable;
        cjkWidthTable.m_width = new List<CjkWidth>(this.m_width.Count);
        foreach (CjkWidth cjkWidth in this.m_width)
          cjkWidthTable.m_width.Add(cjkWidth.Clone());
        return (WidthTable) cjkWidthTable;
      }

      internal override PdfArray ToArray()
      {
        PdfArray arr = new PdfArray();
        foreach (CjkWidth cjkWidth in this.m_width)
          cjkWidth.AppendToArray(arr);
        return arr;
      }

      public int DefaultWidth => this.m_defaultWidth;

      public override int this[int index]
      {
        get
        {
          int defaultWidth = this.DefaultWidth;
          foreach (CjkWidth cjkWidth in this.m_width)
          {
            if (index >= cjkWidth.From && index <= cjkWidth.To)
              defaultWidth = cjkWidth[index];
          }
          return defaultWidth;
        }
      }
    }
}
