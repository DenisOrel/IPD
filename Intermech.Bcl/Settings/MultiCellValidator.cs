
// Type: Intermech.Settings.MultiCellValidator
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Settings
{
    public class MultiCellValidator
    {
      private readonly ISettingsCell[] cells;

      public MultiCellValidator(params ISettingsCell[] cells)
      {
        this.cells = cells != null ? cells : throw new ArgumentNullException(nameof (cells));
        foreach (ISettingsCell cell in cells)
          cell.ValidatingGroup += new EventHandler(this.OnCellValidating);
      }

      private void OnCellValidating(object sender, EventArgs e)
      {
        bool flag = true;
        foreach (IValueCell cell in this.cells)
        {
          if (cell.State != ValueCellState.Valid)
          {
            flag = false;
            break;
          }
        }
        if (!flag)
          return;
        this.OnValidateCells();
      }

      protected virtual void OnValidateCells()
      {
      }
    }
}
