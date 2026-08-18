// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.Utils.PrintParameters
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings;


namespace Intermech.PdfPrintCenter.Utils
{
    internal class PrintParameters
    {
        public PrintParameters(
          short copies,
          string printerName,
          IPdfPageProducer layout,
          bool fitToPage)
        {
            this.Copies = copies;
            this.PrinterName = printerName;
            this.Layout = layout;
            this.FitToPage = fitToPage;
        }

        public short Copies { get; private set; }

        public IPdfPageProducer Layout { get; private set; }

        public string PrinterName { get; private set; }

        public bool FitToPage { get; private set; }
    }
}
