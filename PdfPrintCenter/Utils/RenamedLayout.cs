// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.Utils.RenamedLayout
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe


namespace Intermech.PdfPrintCenter.Utils
{
    internal class RenamedLayout
    {
        public RenamedLayout(string oldName, string newName)
        {
            this.OldName = oldName;
            this.NewName = newName;
        }

        public string OldName { get; set; }

        public string NewName { get; set; }
    }
}
