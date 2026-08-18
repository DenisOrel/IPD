
// Type: Intermech.Text.TextServices
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Intermech.Text
{
    /// <summary>
    /// Сервисы и утилиты, используемые при работе с текстом и строками.
    /// </summary>
    public static class TextServices
    {
      private static readonly char[] trimPatterns = new char[4]
      {
        ' ',
        '\t',
        '\r',
        '\n'
      };
      private static readonly char[] wordsSplitPatterns = new char[1]
      {
        ' '
      };
      private static readonly string[] textLinesSplitPatterns = new string[4]
      {
        Environment.NewLine,
        "\n\r",
        "\n",
        "\r"
      };
      [ThreadStatic]
      private static StringBuilderPoolSelector stringBuilderPoolSelector;

      /// <summary>
      /// Удаляет из начала и конца строки пробелы и аналогичные им символы, а также символы переноса строк.
      /// </summary>
      /// <param name="value">Строковое значение</param>
      /// <returns>Очищенное строковое значение</returns>
      public static string Trim(string value) => value?.Trim(TextServices.TrimPatterns);

      /// <summary>
      /// Возвращает массив символов, удаляемых из строковых значений при выполнении метода Trim.
      /// </summary>
      public static char[] TrimPatterns
      {
        [DebuggerStepThrough] get => TextServices.trimPatterns;
      }

      /// <summary>
      /// Возвращает массив образцов для деления текстовой строки на отдельные слова.
      /// </summary>
      public static char[] WordsSplitPatterns
      {
        [DebuggerStepThrough] get => TextServices.wordsSplitPatterns;
      }

      /// <summary>
      /// Возвращает массив образцов для деления многострочного текста на отдельные строки.
      /// </summary>
      public static string[] TextLinesSplitPatterns
      {
        [DebuggerStepThrough] get => TextServices.textLinesSplitPatterns;
      }

      /// <summary>
      /// Разбивает текст на отдельные строки, используя указанный разделитель строк.
      /// </summary>
      /// <param name="text">Однострочный или многострочный текст</param>
      /// <param name="delimiter">Разделитель строк в тексте</param>
      /// <returns>Перечислитель фрагментов текста, каждый из которых описывает отдельную строку без символов разделителя строк</returns>
      /// <exception cref="T:ArgumentException">Параметры <paramref name="text" />, <paramref name="delimiter" /> не должны быть равны null. Параметр <paramref name="delimiter" /> не должен быть равен пустой строке.</exception>
      public static IEnumerable<StringView> EnumerateTextLines(this string text, string delimiter)
      {
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        return !string.IsNullOrEmpty(delimiter) ? TextServices.EnumerateTextLinesCore(text, delimiter) : throw new ArgumentException("Разделитель строк не может быть равен пустой строке.", nameof (delimiter));
      }

      private static IEnumerable<StringView> EnumerateTextLinesCore(string text, string delimiter)
      {
        int startIndex = 0;
        int pos = text.IndexOf(delimiter, startIndex);
        if (pos >= 0)
        {
          do
          {
            yield return new StringView(startIndex, pos - startIndex);
            startIndex = pos + delimiter.Length;
            pos = text.IndexOf(delimiter, startIndex);
          }
          while (pos >= 0);
          yield return new StringView(startIndex, text.Length - startIndex);
        }
        else
          yield return new StringView(0, text.Length);
      }

      /// <summary>Возвращает или задает пул объектов StringBuilder.</summary>
      public static StringBuilderPoolSelector StringBuilderPool
      {
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          if (TextServices.stringBuilderPoolSelector == null)
            TextServices.stringBuilderPoolSelector = new StringBuilderPoolSelector();
          return TextServices.stringBuilderPoolSelector;
        }
        [DebuggerStepThrough] set => TextServices.stringBuilderPoolSelector = value;
      }
    }
}
