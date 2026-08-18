// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.Utils.Events.ListViewItemDuplicatedEventArgs
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using System.Windows.Forms;


namespace Intermech.PdfPrintCenter.Utils.Events
{
    internal class ListViewItemDuplicatedEventArgs
    {
        public ListViewItemDuplicatedEventArgs(ListViewItem addedItem) => this.AddedItem = addedItem;

        public ListViewItem AddedItem { get; private set; }
    }
}
