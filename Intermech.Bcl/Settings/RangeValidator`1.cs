
// Type: Intermech.Settings.RangeValidator`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Localization;
using System;


namespace Intermech.Settings
{
    public class RangeValidator<T> : SingleCellValidator<T> where T : IComparable<T>
    {
      private readonly T minValue;
      private readonly T maxValue;

      public RangeValidator(SettingsCell<T> cell, T minValue, T maxValue)
        : base(cell)
      {
        this.minValue = minValue.CompareTo(maxValue) != 1 ? minValue : throw new ArgumentOutOfRangeException(nameof (minValue));
        this.maxValue = maxValue;
      }

      protected override void OnValidateCell()
      {
        T rawValue = this.cell.RawValue;
        if (this.minValue.CompareTo(rawValue) == 1)
        {
          this.cell.Error = LocalizationHolder.rm.GetString("SR_813");
        }
        else
        {
          if (this.maxValue.CompareTo(rawValue) != -1)
            return;
          this.cell.Error = LocalizationHolder.rm.GetString("SR_814");
        }
      }
    }
}
