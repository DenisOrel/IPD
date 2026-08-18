// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.Utils.PageInterval
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe


namespace Intermech.PdfPrintCenter.Utils
{
    internal class PageInterval
    {
        public PageInterval(int begin, int end)
        {
            this.Begin = begin;
            this.End = end;
        }

        public int Begin { get; private set; }

        public int End { get; private set; }

        public override string ToString()
        {
            return this.Begin == this.End ? this.Begin.ToString() : $"{this.Begin}-{this.End}";
        }
    }
}
