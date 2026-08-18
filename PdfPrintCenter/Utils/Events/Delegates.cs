// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.Utils.Events.Delegates
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe


namespace Intermech.PdfPrintCenter.Utils.Events
{
    internal static class Delegates
    {
        public delegate void VirtualTreeModifyHandler(object sender, OnModifyVirtualTreeEventArgs e);

        public delegate void ListViewItemDuplicatedHandler(
          object sender,
          ListViewItemDuplicatedEventArgs e);
    }
}
