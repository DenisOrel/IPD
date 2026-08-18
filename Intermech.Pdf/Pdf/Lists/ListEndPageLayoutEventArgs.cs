// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Lists.ListEndPageLayoutEventArgs
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;


namespace Syncfusion.Pdf.Lists
{
    public class ListEndPageLayoutEventArgs : EndPageLayoutEventArgs
    {
      private PdfList m_list;

      internal ListEndPageLayoutEventArgs(PdfLayoutResult layoutResult, PdfList list)
        : base(layoutResult)
      {
        this.m_list = list;
      }

      public PdfList List => this.m_list;
    }
}
