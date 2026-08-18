// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Tables.LightTableBeginPageLayoutEventArgs
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System.Drawing;


namespace Syncfusion.Pdf.Tables
{
    public class LightTableBeginPageLayoutEventArgs : BeginPageLayoutEventArgs
    {
      private int m_startRow;

      internal LightTableBeginPageLayoutEventArgs(RectangleF bounds, PdfPage page, int startRow)
        : base(bounds, page)
      {
        this.m_startRow = startRow;
      }

      public int StartRowIndex => this.m_startRow;
    }
}
