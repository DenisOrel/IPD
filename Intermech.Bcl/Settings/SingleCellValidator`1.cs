
// Type: Intermech.Settings.SingleCellValidator`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Settings
{
    public abstract class SingleCellValidator<T>
    {
      protected readonly SettingsCell<T> cell;

      public SingleCellValidator(SettingsCell<T> cell)
      {
        this.cell = cell != null ? cell : throw new ArgumentNullException(nameof (cell));
        this.cell.ValidatingCell += new EventHandler(this.ValidateCell);
      }

      private void ValidateCell(object sender, EventArgs e)
      {
        if (e == null)
          throw new ArgumentNullException(nameof (e));
        if (this.cell.State != ValueCellState.Valid)
          return;
        this.OnValidateCell();
      }

      protected abstract void OnValidateCell();
    }
}
