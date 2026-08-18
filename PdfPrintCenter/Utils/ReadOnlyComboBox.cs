
// Type: Intermech.PdfPrintCenter.Utils.ReadOnlyComboBox




using System.Windows.Forms;


namespace Intermech.PdfPrintCenter.Utils
{
    internal class ReadOnlyComboBox : ComboBox
    {
      public ReadOnlyComboBox() => this.KeyPress += new KeyPressEventHandler(this.OnKeyPress);

      private void OnKeyPress(object sender, KeyPressEventArgs e) => e.KeyChar = char.MinValue;
    }
}
