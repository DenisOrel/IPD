
// Type: Intermech.UI.PercentValueHelper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.UI
{
    internal static class PercentValueHelper
    {
      public static double Normalize(double percentValue)
      {
        if (MathUtils.AlmostZero(percentValue))
          percentValue = 0.0;
        else if (MathUtils.AlmostEqual(percentValue, 100.0))
          percentValue = 100.0;
        return percentValue;
      }

      public static double NormalizeAndCheck(double percentValue)
      {
        percentValue = PercentValueHelper.Normalize(percentValue);
        if (percentValue < 0.0)
          throw new ArgumentOutOfRangeException(nameof (percentValue), "Значение аргумента не должно быть меньше 0.");
        return percentValue <= 100.0 ? percentValue : throw new ArgumentOutOfRangeException(nameof (percentValue), $"Значение аргумента {percentValue} превышает максимально допустимое значение 100.");
      }
    }
}
