
// Type: Intermech.Settings.IValueCell
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Settings
{
    public interface IValueCell
    {
      ValueCellState State { get; }

      string Error { get; set; }

      /// <summary>
      /// Сбрасывает признак, что значение ячейки было проверено на допустимость. Автоматически
      /// вызывается при изменении значения в ячейке.
      /// </summary>
      void Invalidate();

      /// <summary>Проверяет значение ячейки на допустимость.</summary>
      void Validate();

      event EventHandler RawValueChanged;

      event EventHandler Invalidated;

      event EventHandler Validating;

      event EventHandler Validated;

      event EventHandler<ErrorTextArgs> GetErrorText;
    }
}
