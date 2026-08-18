// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTools.PrintReportTools.HtmlWriterTools.Properties.HtmlProperty
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe


namespace Intermech.PdfPrintCenter.PrintCenterTools.PrintReportTools.HtmlWriterTools.Properties
{
    internal class HtmlProperty : Property
    {
        public HtmlProperty(HtmlAttributes name, string value)
          : base(name.ToString().ToLower(), value)
        {
        }

        public HtmlProperty(string name, string value)
          : base(name, value)
        {
        }
    }
}
