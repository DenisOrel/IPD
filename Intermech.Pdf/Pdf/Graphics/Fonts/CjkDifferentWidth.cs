// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Fonts.CjkDifferentWidth
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.Graphics.Fonts
{
    internal class CjkDifferentWidth : CjkWidth
    {
      private int m_from;
      private int[] m_width;

      public CjkDifferentWidth(int from, int[] widths)
      {
        if (widths == null)
          throw new ArgumentNullException(nameof (widths));
        this.m_from = from;
        this.m_width = widths;
      }

      internal override void AppendToArray(PdfArray arr)
      {
        arr.Add((IPdfPrimitive) new PdfNumber(this.From));
        PdfArray element = new PdfArray(this.m_width);
        arr.Add((IPdfPrimitive) element);
      }

      internal override CjkWidth Clone()
      {
        CjkDifferentWidth cjkDifferentWidth = this.MemberwiseClone() as CjkDifferentWidth;
        cjkDifferentWidth.m_width = (int[]) this.m_width.Clone();
        return (CjkWidth) cjkDifferentWidth;
      }

      internal override int From => this.m_from;

      internal override int this[int index]
      {
        get
        {
          if (index < this.From || index > this.To)
            throw new ArgumentOutOfRangeException(nameof (index), "Index is out of range.");
          return this.m_width[index - this.From];
        }
      }

      internal override int To => this.From + this.m_width.Length - 1;
    }
}
