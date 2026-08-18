// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.Utils.AddingPagesToLayoutResult
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.Utils
{
    internal class AddingPagesToLayoutResult
    {
        public AddingPagesToLayoutResult()
        {
            this.BadRanges = new List<string>();
            this.PdfWithLayout = (byte[])null;
        }

        public AddingPagesToLayoutResult(params string[] badRanges)
          : this()
        {
            this.BadRanges.AddRange((IEnumerable<string>)badRanges);
        }

        public AddingPagesToLayoutResult(byte[] pdfWithLayout)
          : this()
        {
            this.PdfWithLayout = pdfWithLayout;
        }

        public List<string> BadRanges { get; private set; }

        public byte[] PdfWithLayout { get; private set; }

        public void AddBadRanges(params string[] badRanges)
        {
            this.BadRanges.AddRange((IEnumerable<string>)badRanges);
        }
    }
}
