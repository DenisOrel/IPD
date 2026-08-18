// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings.SheetPanelLocation
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using System.Windows.Forms;


namespace Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings
{
    internal class SheetPanelLocation
    {
        public SheetPanelLocation(Panel panel, ListViewItem listViewItem, FormatLocation formatLocation)
        {
            this.Panel = panel;
            this.ListViewItem = listViewItem;
            this.FormatLocation = formatLocation;
        }

        public ListViewItem ListViewItem { get; set; }

        public Panel Panel { get; set; }

        public FormatLocation FormatLocation { get; set; }

        public void MovePanelFormat(int left, int top)
        {
            this.ListViewItem.SubItems[0].Text = $"{left}; {top}";
            this.FormatLocation.Left = left;
            this.FormatLocation.Top = top;
        }
    }
}
