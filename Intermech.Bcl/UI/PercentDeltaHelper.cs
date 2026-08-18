
// Type: Intermech.UI.PercentDeltaHelper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.UI
{
    internal static class PercentDeltaHelper
    {
      public static double NormalizeAndCheck(double currentPercentValue, double percentDelta)
      {
        if (percentDelta <= 0.0)
          throw new ArgumentOutOfRangeException(nameof (percentDelta));
        double x = currentPercentValue + percentDelta;
        if (MathUtils.AlmostEqual(x, 100.0))
          percentDelta += 100.0 - x;
        else if (x > 100.0)
          throw new ArgumentOutOfRangeException(nameof (percentDelta), $"Значение аргумента {percentDelta} превышает максимально допустимое значение {percentDelta - (x - 100.0)}.");
        return percentDelta;
      }
    }
}
