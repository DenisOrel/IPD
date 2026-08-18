
// Type: Intermech.PdfPrintCenter.Utils.Events.ListViewItemDuplicatedEventArgs




using System.Windows.Forms;


namespace Intermech.PdfPrintCenter.Utils.Events
{
    internal class ListViewItemDuplicatedEventArgs
    {
      public ListViewItemDuplicatedEventArgs(ListViewItem addedItem) => this.AddedItem = addedItem;

      public ListViewItem AddedItem { get; private set; }
    }
}
