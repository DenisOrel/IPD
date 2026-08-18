
// Type: Intermech.ComparisonPlugins.PDFComparison.UI.FormHelper




using System;
using System.Windows.Forms;


namespace Intermech.ComparisonPlugins.PDFComparison.UI
{
    public class FormHelper
    {
      public static void CheckEnterFormat(KeyPressEventArgs e)
      {
        char keyChar = e.KeyChar;
        if (char.IsDigit(keyChar) || (int) keyChar == (int) Convert.ToChar((object) Keys.Back) || keyChar == '-' || keyChar == ',')
          return;
        if (keyChar == '.')
          e.KeyChar = ',';
        else
          e.Handled = true;
      }
    }
}
