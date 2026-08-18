
// Type: Intermech.TextUtils
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech
{
    [Obsolete("Use the class Intermech.Text.TextServices instead of this", true)]
    public static class TextUtils
    {
      private static readonly char[] TrimPatterns = new char[4]
      {
        ' ',
        '\t',
        '\r',
        '\n'
      };
      /// <summary>
      /// Массив выражений для деления текста на отдельные строки.
      /// </summary>
      public static readonly string[] TextSplitPatterns = new string[4]
      {
        Environment.NewLine,
        "\n\r",
        "\n",
        "\r"
      };
      /// <summary>
      /// Массив выражений для деления текстовой строки на отдельные элементы.
      /// </summary>
      public static readonly char[] LineSplitPatterns = new char[1]
      {
        ' '
      };

      public static string Trim(string value) => value?.Trim(TextUtils.TrimPatterns);
    }
}
