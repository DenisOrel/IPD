
// Type: Intermech.Common.StringsHelper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;


namespace Intermech.Common
{
    /// <summary>Вспомогательный статический класс для работы со строками.</summary>
    public static class StringsHelper
    {
      /// <summary>Разделитель в строках с датами.</summary>
      public static char DateSeparator = '/';
      /// Разделитель в строках с датами
      [NotNull]
      public static readonly char[] DateSeparatorArray = new char[1]
      {
        '/'
      };
      /// <summary>Разделители между HEX-значениями.</summary>
      [NotNull]
      public static readonly char[] DividersHexValues = new char[1]
      {
        ' '
      };
      /// <summary>Разделители между словами.</summary>
      [NotNull]
      public static readonly char[] DividersForWords = new char[4]
      {
        ' ',
        '\r',
        '\n',
        '\t'
      };

      /// <summary>Извлечь подстроку из строки line, при этом подстрока МОЖЕТ БЫТЬ ограничена слева разделителем left, а справа подстрока МОЖЕТ БЫТЬ ограничена разделителем
      /// right.</summary>
      /// <param name="line">Исходная строка.</param>
      /// <param name="left">Левый разделитель (необязателен)</param>
      /// <param name="right">Правый разделитель (необязателен)</param>
      /// <returns>Подстрока или String.Empty.</returns>
      [NotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string ExtractFrom([NotNull] string line, [CanBeNull] string left, [CanBeNull] string right)
      {
        int length1 = line.Length;
        if (length1 == 0)
          return string.Empty;
        int num1 = 0;
        if (left == null)
          left = string.Empty;
        else
          num1 = left.Length;
        if (length1 < num1)
          return string.Empty;
        int num2 = 0;
        if (right == null)
          right = string.Empty;
        else
          num2 = right.Length;
        if (num1 == 0 && num2 == 0)
          return line;
        int num3 = num1 > 0 ? line.IndexOf(left, StringComparison.InvariantCultureIgnoreCase) : 0;
        if (num3 < 0)
        {
          left = string.Empty;
          num3 = 0;
          num1 = 0;
        }
        int num4 = num2 > 0 ? line.IndexOf(right, num3 + 1, StringComparison.InvariantCultureIgnoreCase) : -1;
        int length2 = num4 > 0 ? num4 - num3 - num1 : length1 - num3 - num1;
        return length2 <= 0 ? string.Empty : line.Substring(num3 + num1, length2);
      }

      /// <summary>Подсчитать количество вхождений подстроки substring в строке value.</summary>
      /// <param name="value">Изучаемая строка.</param>
      /// <param name="substring">Искомая подстрока.</param>
      /// <returns>Количество вхождений подстроки substring в строку value.</returns>
      public static int ContainsCount([NotNull] string value, [NotNull] string substring)
      {
        int num1 = value.IndexOf(substring, 0, StringComparison.Ordinal);
        if (num1 < 0)
          return 0;
        int num2 = 1;
        int num3;
        for (int startIndex = num1 + substring.Length; startIndex < value.Length; startIndex = num3 + substring.Length)
        {
          num3 = value.IndexOf(substring, startIndex, StringComparison.Ordinal);
          if (num3 >= 0)
            ++num2;
          else
            break;
        }
        return num2;
      }

      /// <summary>Извлечь из строки очередное слово, начиная с указанной позиции, с учётом указанных разделителей слов.</summary>
      /// <param name="line">Строка.</param>
      /// <param name="startIndex">[in,out] Стартовая позиция.</param>
      /// <param name="dividers">Список разделителей (null - получить остаток строки, начиная с позиции startIndex)</param>
      /// <returns>Искомое слово либо String.Empty.</returns>
      [NotNull]
      public static string GetWord([NotNull] string line, ref int startIndex, [CanBeNull] List<char> dividers)
      {
        if (line == string.Empty || startIndex < 0 || startIndex >= line.Length)
          return string.Empty;
        if (dividers == null)
          return line.Substring(startIndex);
        StringBuilder stringBuilder = new StringBuilder(line.Length - startIndex + 1);
        while (startIndex < line.Length && dividers.IndexOf(line[startIndex]) >= 0)
          ++startIndex;
        if (startIndex >= line.Length)
          return string.Empty;
        while (startIndex < line.Length && dividers.IndexOf(line[startIndex]) < 0)
        {
          stringBuilder.Append(line[startIndex]);
          ++startIndex;
        }
        return stringBuilder.ToString();
      }

      /// <summary>Вернуть значение указанного размера (байты) в виде строки, с приведением значения к соответствующим величинам (б, Кб, Мб, Гб)</summary>
      /// <param name="size">Размер (байт)</param>
      /// <returns>Значение размера в виде строки.</returns>
      [NotNull]
      public static string GetSizeString(long size)
      {
        if (size <= 0L)
          return string.Empty;
        if (size < 1024L /*0x0400*/)
          return $"{size:F3} б";
        if (size < 1048576L /*0x100000*/)
          return $"{(double) size / 1024.0:F2} Кб";
        if (size < 1073741824L /*0x40000000*/)
          return $"{(double) size / 1048576.0:F2} Мб";
        return size < 1099511627776L /*0x010000000000*/ ? $"{(double) size / 1073741824.0:F2} Гб" : $"{(double) size / 1099511627776.0:F2} Тб";
      }

      /// <summary>Вернуть значение указанной скорости (байты/сек) в виде строки, с приведением скорости к соответствующим величинам (б/сек, Кб/сек, Мб/сек, Гб/сек)</summary>
      /// <param name="speed">Скорость (байт/сек)</param>
      /// <returns>Значение скорости в виде строки.</returns>
      [NotNull]
      public static string GetSpeedString(double speed)
      {
        if (speed <= 0.0)
          return string.Empty;
        if (speed < 1024.0)
          return $"{speed:F3} байт/сек";
        if (speed < 1048576.0)
          return $"{speed / 1024.0:F2} Кб/сек";
        if (speed < 1073741824.0)
          return $"{speed / 1048576.0:F2} Мб/сек";
        return speed < 1099511627776.0 ? $"{speed / 1073741824.0:F2} Гб/сек" : $"{speed / 1099511627776.0:F2} Тб/сек";
      }

      /// <summary>Вернуть значение указанного интервала времени в виде строки.</summary>
      /// <param name="delta">Временной интервал.</param>
      /// <param name="alignValues">Выравнивать значения (нулями)</param>
      /// <returns>Значение указанного интервала времени в виде строки.</returns>
      [NotNull]
      public static string GetTimeSpanString(TimeSpan delta, bool alignValues)
      {
        StringBuilder stringBuilder = new StringBuilder();
        if (delta.Days > 0)
          stringBuilder.Append($"{delta.Days} дн. ");
        if (delta.Hours > 0)
          stringBuilder.Append(alignValues ? $"{delta.Hours:D2} ч. " : $"{delta.Hours} ч. ");
        if (delta.Minutes > 0)
          stringBuilder.Append(alignValues ? $"{delta.Minutes:D2} мин. " : $"{delta.Minutes} мин. ");
        if (delta.Seconds > 0)
          stringBuilder.Append(alignValues ? $"{delta.Seconds:F3} сек. " : $"{delta.Seconds} сек. ");
        if (delta.Milliseconds > 0)
          stringBuilder.Append(alignValues ? $"{delta.Milliseconds:D4} мс." : $"{delta.Milliseconds} мс.");
        if (stringBuilder.Length == 0)
          stringBuilder.Append("0 сек.");
        return stringBuilder.ToString().Trim();
      }

      /// <summary>Преобразовать строку вида hex hex hex в массив байт.</summary>
      /// <param name="hex">Строка вида hex hex hex.</param>
      /// <returns>Массив байт.</returns>
      [NotNull]
      public static byte[] Hex2Bytes([CanBeNull] string hex)
      {
        if (string.IsNullOrEmpty(hex))
          return Array.Empty<byte>();
        string[] strArray = hex.Split(StringsHelper.DividersHexValues, StringSplitOptions.RemoveEmptyEntries);
        byte[] numArray = new byte[strArray.Length];
        for (int index = 0; index < strArray.Length; ++index)
        {
          byte num = Convert.ToByte(strArray[index], 16 /*0x10*/);
          numArray[index] = num;
        }
        return numArray;
      }

      /// <summary>Преобразовать массив байт в строку вида hex hex hex.</summary>
      /// <param name="val">Массив байт.</param>
      /// <returns>Строка вида hex hex hex.</returns>
      [NotNull]
      public static string Bytes2Hex([CanBeNull] byte[] val)
      {
        if (val == null || val.Length == 0)
          return string.Empty;
        StringBuilder stringBuilder = new StringBuilder();
        foreach (byte num in val)
          stringBuilder.Append(num.ToString("X2", (IFormatProvider) CultureInfo.InvariantCulture));
        return stringBuilder.ToString();
      }

      /// <summary>Из строки вида hex hex hex вернуть обычную Unicode-строку.</summary>
      /// <param name="hex">Строка вида hex hex hex.</param>
      /// <returns>Строковое значение.</returns>
      [NotNull]
      public static string Hex2String([CanBeNull] string hex)
      {
        if (string.IsNullOrEmpty(hex))
          return string.Empty;
        string[] source = hex.Split(StringsHelper.DividersHexValues, StringSplitOptions.RemoveEmptyEntries);
        StringBuilder stringBuilder = new StringBuilder();
        foreach (char ch in ((IEnumerable<string>) source).Select((Func<string, int>) (hexValue => Convert.ToInt32(hexValue, 16 /*0x10*/))).Select((Func<int, char>) (value => (char) value)))
          stringBuilder.Append(ch);
        return stringBuilder.ToString();
      }

      /// <summary>Из строки вида hex hex hex вернуть поток.</summary>
      /// <param name="hex">Строка вида hex hex hex.</param>
      /// <returns>Значение в виде потока.</returns>
      [NotNull]
      public static MemoryStream Hex2Stream([CanBeNull] string hex)
      {
        MemoryStream memoryStream = new MemoryStream();
        if (string.IsNullOrEmpty(hex))
          return memoryStream;
        foreach (byte num in ((IEnumerable<string>) hex.Split(StringsHelper.DividersHexValues, StringSplitOptions.RemoveEmptyEntries)).Select((Func<string, byte>) (hexValue => Convert.ToByte(hexValue, 16 /*0x10*/))))
          memoryStream.WriteByte(num);
        memoryStream.Position = 0L;
        return memoryStream;
      }

      /// <summary>Из строки вида hex hex hex вернуть список найденных идентификаторов.</summary>
      /// <param name="hex">Строка вида hex hex hex.</param>
      /// <returns>Список найденных идентификаторов.</returns>
      [NotNull]
      public static List<string> Hex2Identifiers([CanBeNull] string hex)
      {
        List<string> stringList = new List<string>();
        if (string.IsNullOrEmpty(hex))
          return stringList;
        string[] source = hex.Split(StringsHelper.DividersHexValues, StringSplitOptions.RemoveEmptyEntries);
        StringBuilder stringBuilder = new StringBuilder();
        foreach (int num in ((IEnumerable<string>) source).Select((Func<string, int>) (hexValue => Convert.ToInt32(hexValue, 16 /*0x10*/))))
        {
          if (num < 32 /*0x20*/)
          {
            if (stringBuilder.Length > 0)
            {
              hex = stringBuilder.ToString();
              if (stringList.IndexOf(hex) < 0)
                stringList.Add(hex);
              stringBuilder.Length = 0;
            }
          }
          else
          {
            char ch = (char) num;
            stringBuilder.Append(ch);
          }
        }
        if (stringBuilder.Length > 0)
        {
          hex = stringBuilder.ToString();
          if (stringList.IndexOf(hex) < 0)
            stringList.Add(hex);
          stringBuilder.Length = 0;
        }
        return stringList;
      }

      /// <summary>Преобразовать Int32 в HEX-строку.</summary>
      /// <param name="value">Значение.</param>
      /// <returns>HEX-строка.</returns>
      [NotNull]
      public static string IntToHex(int value)
      {
        return value.ToString("X", (IFormatProvider) CultureInfo.InvariantCulture);
      }

      /// <summary>Преобразовать Int64 в HEX-строку.</summary>
      /// <param name="value">Значение.</param>
      /// <returns>HEX-строка.</returns>
      [NotNull]
      public static string IntToHex(long value)
      {
        return value.ToString("X", (IFormatProvider) CultureInfo.InvariantCulture);
      }

      /// <summary>Преобразовать HEX-строку в Int32.</summary>
      /// <param name="value">HEX-строка.</param>
      /// <returns>Int32.</returns>
      public static int HexToInt32([NotNull] string value)
      {
        int result;
        return !int.TryParse(value, NumberStyles.HexNumber, (IFormatProvider) CultureInfo.InvariantCulture, out result) ? 0 : result;
      }

      /// <summary>Преобразовать HEX-строку в Int64.</summary>
      /// <param name="value">HEX-строка.</param>
      /// <returns>Int64.</returns>
      public static long HexToInt64([NotNull] string value)
      {
        long result;
        return !long.TryParse(value, NumberStyles.HexNumber, (IFormatProvider) CultureInfo.InvariantCulture, out result) ? 0L : result;
      }

      /// <summary>Преобразовать дату в строку для редактирования.</summary>
      /// <param name="value">Значение.</param>
      /// <returns>Строка.</returns>
      [NotNull]
      public static string DateToEditableStr(DateTime value)
      {
        return value.Date.ToString("dd.MM.yyyy", (IFormatProvider) CultureInfo.InvariantCulture);
      }

      /// <summary>Преобразовать строку в дату.</summary>
      /// <param name="value">Значение.</param>
      /// <param name="defValue">Значение по умолчанию.</param>
      /// <returns>Дата или значение по умолчанию в случае ошибки.</returns>
      public static DateTime DateFromEditableStr([NotNull] string value, DateTime defValue = default (DateTime))
      {
        DateTime result;
        return DateTime.TryParseExact(value, "dd.MM.yyyy", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) || DateTime.TryParseExact(value, "dd.M.yyyy", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) || DateTime.TryParseExact(value, "d.MM.yyyy", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) || DateTime.TryParseExact(value, "d.M.yyyy", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) ? result : defValue;
      }

      /// <summary>Преобразовать дату в строку.</summary>
      /// <param name="value">Значение.</param>
      /// <returns>Строка.</returns>
      [NotNull]
      public static string DateToStr(DateTime value)
      {
        return value.Date.ToString("d", (IFormatProvider) CultureInfo.InvariantCulture);
      }

      /// <summary>Преобразовать дату в строку (HEX-кодированную, вида YYYY/MM/DD)</summary>
      /// <param name="value">Значение.</param>
      /// <returns>Строка (HEX-кодированная, вида YYYY/MM/DD)</returns>
      [NotNull]
      public static string DateToStrHex(DateTime value)
      {
        return $"{StringsHelper.IntToHex(value.Year)}{StringsHelper.DateSeparator}{StringsHelper.IntToHex(value.Month)}{StringsHelper.DateSeparator}{StringsHelper.IntToHex(value.Day)}";
      }

      /// <summary>Преобразовать строку в дату.</summary>
      /// <param name="value">Значение.</param>
      /// <returns>Дата.</returns>
      public static DateTime DateFromStr([NotNull] string value)
      {
        return Convert.ToDateTime(value, (IFormatProvider) CultureInfo.InvariantCulture).Date;
      }

      /// <summary>Преобразовать строку (HEX-кодированную, вида YYYY/MM/DD) в дату.</summary>
      /// <param name="value">Значение (HEX-кодированное, вида YYYY/MM/DD)</param>
      /// <returns>Дата (или DateTime.MinValue)</returns>
      public static DateTime DateFromStrHex([CanBeNull] string value)
      {
        string[] strArray = value?.Split(StringsHelper.DateSeparatorArray, StringSplitOptions.None);
        if (strArray != null)
        {
          if (strArray.Length >= 3)
          {
            try
            {
              return new DateTime(StringsHelper.HexToInt32(strArray[0]), StringsHelper.HexToInt32(strArray[1]), StringsHelper.HexToInt32(strArray[2]));
            }
            catch (ArgumentOutOfRangeException ex)
            {
              return DateTime.MinValue;
            }
          }
        }
        return DateTime.MinValue;
      }

      /// <summary>Обрезать часть строки, если её длина превышает указанную величину.</summary>
      /// <param name="value">Строка.</param>
      /// <param name="maxLen">Максимально допустимая длина строки.</param>
      /// <returns>Откорректированная строка.</returns>
      [NotNull]
      public static string TrimString([CanBeNull] string value, int maxLen)
      {
        if (string.IsNullOrEmpty(value) || maxLen <= 0)
          return string.Empty;
        return value.Length > maxLen ? value.Substring(0, maxLen) : value;
      }

      /// <summary>Разделить строку на части указанной максимальной длины.</summary>
      /// <param name="value">Строка.</param>
      /// <param name="maxLen">Максимально допустимая длина фрагмента сети.</param>
      /// <returns>A List{string}</returns>
      [NotNull]
      [ItemNotNull]
      public static List<string> SplitString([CanBeNull] string value, int maxLen)
      {
        List<string> stringList = new List<string>();
        if (string.IsNullOrEmpty(value))
          return stringList;
        if (value.Length <= maxLen)
        {
          stringList.Add(value);
          return stringList;
        }
        string str1 = value.Substring(0, maxLen);
        string str2 = value.Substring(str1.Length);
        stringList.Add(str1);
        string str3;
        for (; str2.Length > 0; str2 = str3.Length < str2.Length ? str2.Substring(str3.Length) : string.Empty)
        {
          str3 = str2.Substring(0, Math.Min(maxLen, str2.Length));
          stringList.Add(str3);
        }
        return stringList;
      }

      /// <summary>Проверить наличие указанного слова в массиве с учётом указанных параметров.</summary>
      /// <param name="words">Массив проверяемых слов.</param>
      /// <param name="word">Искомое слово.</param>
      /// <param name="caseSensitive">true - учитывать регистр букв.</param>
      /// <returns>true - слово найдено в массиве.</returns>
      public static bool Exists([ItemNotNull, CanBeNull] string[] words, [NotNull] string word, bool caseSensitive)
      {
        return words != null && words.Length != 0 && !string.IsNullOrEmpty(word) && ((IEnumerable<string>) words).Any((Func<string, bool>) (t =>
        {
          if (caseSensitive && StringComparer.CurrentCulture.Compare(t, word) == 0)
            return true;
          return !caseSensitive && StringComparer.CurrentCultureIgnoreCase.Compare(t, word) == 0;
        }));
      }

      /// <summary>Проверить наличие подстроки в строке с учётом указанных параметров.</summary>
      /// <param name="value">Строка.</param>
      /// <param name="subString">Искомая подстрока.</param>
      /// <param name="caseSensitive">true - учитывать регистр букв.</param>
      /// <param name="wholeWords">true - искать только вхождение целых слов.</param>
      /// <returns>true, если подстрока найдена.</returns>
      public static bool Exists([NotNull] string value, [NotNull] string subString, bool caseSensitive, bool wholeWords)
      {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(subString))
          return false;
        if (!wholeWords)
          return value.IndexOf(subString, 0, caseSensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase) >= 0;
        string[] words = value.Split(StringsHelper.DividersForWords, StringSplitOptions.RemoveEmptyEntries);
        string[] strArray = subString.Split(StringsHelper.DividersForWords, StringSplitOptions.RemoveEmptyEntries);
        bool flag = true;
        foreach (string word in strArray)
        {
          flag = StringsHelper.Exists(words, word, caseSensitive);
          if (!flag)
            break;
        }
        return flag;
      }

      /// <summary>Извлечь второе расширение из имени файла, если оно есть (например, для "file.ext2.ext1" метод вернёт ".ext2")</summary>
      /// <param name="fileName">Имя файла.</param>
      /// <returns>Второе расширение из имени файла, если оно есть.</returns>
      [CanBeNull]
      public static string GetSecondFileExtension([NotNull, NotWhitespace] string fileName)
      {
        return Path.GetExtension(Path.GetFileNameWithoutExtension(fileName));
      }

      /// <summary>Проверить, является ли расширение файла числом.</summary>
      /// <param name="fileName">Имя файла.</param>
      /// <returns>true - расширение является числом.</returns>
      public static bool IsNumericFileExtension([NotNull, NotWhitespace] string fileName)
      {
        string s = StringsHelper.ExtractFrom(Path.GetExtension(fileName), ".", string.Empty).Trim();
        return !string.IsNullOrEmpty(s) && long.TryParse(s, out long _);
      }

      /// <summary> Возвращает либо первое значение, отличное от null и пустой строки.
      /// Если ни одно из значение не соответствует этому условия, то возвращается последнее переданное значение</summary>
      /// <returns>Первая из переданных строк, отличная от null и string.Empty, либо (если ни одна не соответствует) - последняя переданная</returns>
      [CanBeNull]
      [CanBeEmpty]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string CoalesceNotEmpty([NotNull, ItemCanBeNull] params string[] strings)
      {
        int length = strings.Length;
        string str = (string) null;
        for (int index = 0; index < length; ++index)
        {
          str = strings[index];
          if (string.IsNullOrEmpty(str))
            return str;
        }
        return str;
      }

      /// <summary> Возвращает либо первое значение, отличное от null и пустой строки (пробелы не считаются значащими).
      /// Если ни одно из значение не соответствует этому условия, то возвращается последнее переданное значение</summary>
      /// <returns>Первая из переданных строк, непустая (пробелы не считаются значащими) отличная от null и string.Empty,
      /// либо (если ни одна не соответствует) - последняя переданная</returns>
      [CanBeNull]
      [CanBeEmpty]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string CoalesceNotWhitespace([NotNull, ItemCanBeNull] params string[] strings)
      {
        int length = strings.Length;
        string str = (string) null;
        for (int index = 0; index < length; ++index)
        {
          str = strings[index];
          if (!string.IsNullOrWhiteSpace(str))
            return str;
        }
        return str;
      }

      [NotNull]
      public static string MergeNotEmpty(char delimiter, [NotNull, ItemCanBeNull] params string[] strings)
      {
        bool flag = true;
        StringBuilder stringBuilder = new StringBuilder(256 /*0x0100*/);
        foreach (string str in strings)
        {
          if (string.IsNullOrEmpty(str))
          {
            if (!flag)
              stringBuilder.Append(delimiter);
            else
              flag = false;
            stringBuilder.Append(str);
          }
        }
        return stringBuilder.ToString();
      }

      [NotNull]
      public static string MergeNotWhitespace(char delimiter, [NotNull, ItemCanBeNull] params string[] strings)
      {
        bool flag = true;
        StringBuilder stringBuilder = new StringBuilder(256 /*0x0100*/);
        foreach (string str in strings)
        {
          if (string.IsNullOrWhiteSpace(str))
          {
            if (!flag)
              stringBuilder.Append(delimiter);
            else
              flag = false;
            stringBuilder.Append(str);
          }
        }
        return stringBuilder.ToString();
      }
    }
}
