// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTools.PrintReportTools.HtmlWriterTools.Properties.Property
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe


namespace Intermech.PdfPrintCenter.PrintCenterTools.PrintReportTools.HtmlWriterTools.Properties
{
    internal abstract class Property
    {
        public Property(string attributeName, string value)
        {
            this.Name = attributeName.ToLower();
            this.Value = value;
        }

        public string Name { get; private set; }

        public string Value { get; private set; }
    }
}
