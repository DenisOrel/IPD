
// Type: Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings.SheetPanelLocation




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
