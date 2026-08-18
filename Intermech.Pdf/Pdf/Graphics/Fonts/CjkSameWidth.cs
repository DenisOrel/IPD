// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Fonts.CjkSameWidth
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.Graphics.Fonts
{
    internal class CjkSameWidth : CjkWidth
    {
      private int m_from;
      private int m_to;
      private int m_width;

      public CjkSameWidth(int from, int to, int width)
      {
        this.m_from = from <= to ? from : throw new ArgumentException("'From' can't be grater than 'to'.");
        this.m_to = to;
        this.m_width = width;
      }

      internal override void AppendToArray(PdfArray arr)
      {
        arr.Add((IPdfPrimitive) new PdfNumber(this.From));
        arr.Add((IPdfPrimitive) new PdfNumber(this.To));
        arr.Add((IPdfPrimitive) new PdfNumber(this.m_width));
      }

      internal override CjkWidth Clone() => this.MemberwiseClone() as CjkWidth;

      internal override int From => this.m_from;

      internal override int this[int index]
      {
        get
        {
          if (index < this.From || index > this.To)
            throw new ArgumentOutOfRangeException(nameof (index), "Index is out of range.");
          return this.m_width;
        }
      }

      internal override int To => this.m_to;
    }
}
