// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Tables.BeginCellLayoutEventArgs
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System.Drawing;


namespace Syncfusion.Pdf.Tables
{
    public class BeginCellLayoutEventArgs : CellLayoutEventArgs
    {
      private bool m_bSkip;

      internal BeginCellLayoutEventArgs(
        PdfGraphics graphics,
        int rowIndex,
        int cellInder,
        RectangleF bounds,
        string value)
        : base(graphics, rowIndex, cellInder, bounds, value)
      {
      }

      public bool Skip
      {
        get => this.m_bSkip;
        set => this.m_bSkip = value;
      }
    }
}
