
// Type: Intermech.PdfPrintCenter.Utils.DragData




using System.Collections;
using System.Windows.Forms;


namespace Intermech.PdfPrintCenter.Utils
{
    internal class DragData
    {
      public DragData(Control control, IList selectedNodes)
      {
        this.Control = control;
        this.SelectedNodes = selectedNodes;
      }

      public Control Control { get; private set; }

      public IList SelectedNodes { get; private set; }
    }
}
