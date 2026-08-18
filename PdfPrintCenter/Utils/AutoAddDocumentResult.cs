// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.Utils.AutoAddDocumentResult
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.Utils
{
    internal class AutoAddDocumentResult
    {
        public AutoAddDocumentResult()
        {
            this.OnMinLayout = new List<NodesToPrintQueue>();
            this.NotOnMinLayout = new List<NodesToPrintQueue>();
        }

        public AutoAddDocumentResult(
          List<NodesToPrintQueue> onMinLayout,
          List<NodesToPrintQueue> notOnMinLayout)
        {
            this.OnMinLayout = onMinLayout;
            this.NotOnMinLayout = notOnMinLayout;
        }

        public List<NodesToPrintQueue> NotOnMinLayout { get; private set; }

        public List<NodesToPrintQueue> OnMinLayout { get; private set; }
    }
}
