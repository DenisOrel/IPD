// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.ProgressEventArgs
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf
{
    public class ProgressEventArgs
    {
      private int m_current;
      private int m_total;

      private ProgressEventArgs()
      {
      }

      internal ProgressEventArgs(int current, int total)
      {
        if (total <= 0)
          throw new ArgumentOutOfRangeException(nameof (total), "Total is less then or equal to zero.");
        this.m_current = current >= 0 ? current : throw new ArgumentOutOfRangeException(nameof (current), "Current can't be less then zero.");
        this.m_total = total;
      }

      public int Current => this.m_current;

      public float Progress => (float) this.Current / (float) this.Total;

      public int Total => this.m_total;
    }
}
