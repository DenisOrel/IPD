// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Tables.PdfColumnCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.Tables
{
    public class PdfColumnCollection : PdfCollection
    {
      internal PdfColumnCollection()
      {
      }

      public void Add(PdfColumn column) => this.List.Add((object) column);

      internal float[] GetWidths(float totalWidth) => this.GetWidths(totalWidth, 0, this.Count - 1);

      internal float[] GetWidths(float totalWidth, int startColumn, int endColumn)
      {
        int length = endColumn - startColumn + 1;
        float[] widths = length <= this.Count ? new float[length] : throw new ArgumentException("The start and end column indices doesn't match.");
        float num1 = 0.0f;
        for (int index = startColumn; index <= endColumn; ++index)
        {
          float width = this[index].Width;
          widths[index - startColumn] = width;
          num1 += width;
        }
        if ((double) totalWidth > 0.0)
        {
          float num2 = totalWidth / num1;
          for (int index = 0; index < length; ++index)
            widths[index] *= num2;
        }
        return widths;
      }

      public PdfColumn this[int index] => this.List[index] as PdfColumn;
    }
}
