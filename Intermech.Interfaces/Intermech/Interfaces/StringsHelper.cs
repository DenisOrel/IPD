
// Type: Intermech.Interfaces.StringsHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Вспомогательный статический класс для работы со строками
    /// </summary>
    public static class StringsHelper
    {
      /// <summary>Разделитель в строках с датами</summary>
      public static char DateSeparator = '/';
      public static char[] DateSeparatorArray = new char[1]
      {
        '/'
      };
      /// <summary>Разделители между HEX-значениями</summary>
      public static char[] DividersHexValues = new char[1]
      {
        ' '
      };
      /// <summary>Разделители между словами</summary>
      public static char[] DividersForWords = new char[4]
      {
        ' ',
        '\r',
        '\n',
        '\t'
      };

      /// <summary>
      /// Извлечь подстроку из строки line, при этом подстрока МОЖЕТ БЫТЬ ограничена слева разделителем left,
      /// а справа подстрока МОЖЕТ БЫТЬ ограничена разделителем right
      /// </summary>
      /// <param name="line">Исходная строка</param>
      /// <param name="left">Левый разделитель (необязателен)</param>
      /// <param name="right">Правый разделитель (необязателен)</param>
      /// <returns>Подстрока или String.Empty</returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static string ExtractFrom(string line, string left, string right)
      {
        return Intermech.Common.StringsHelper.ExtractFrom(line, left, right);
      }

      /// <summary>
      /// Подсчитать количество вхождений подстроки substring в строке value
      /// </summary>
      /// <param name="value">Изучаемая строка</param>
      /// <param name="substring">Искомая подстрока</param>
      /// <returns>Количество вхождений подстроки substring в строку value</returns>
      public static int ContainsCount(string value, string substring)
      {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(substring))
          return 0;
        int num1 = value.IndexOf(substring, 0);
        if (num1 < 0)
          return 0;
        int num2 = 1;
        int num3;
        for (int startIndex = num1 + substring.Length; startIndex < value.Length; startIndex = num3 + substring.Length)
        {
          num3 = value.IndexOf(substring, startIndex);
          if (num3 >= 0)
            ++num2;
          else
            break;
        }
        return num2;
      }

      /// <summary>
      /// Извлечь из строки очередное слово, начиная с указанной позиции, с учётом указанных разделителей слов
      /// </summary>
      /// <param name="line">Строка</param>
      /// <param name="startIndex">Стартовая позиция</param>
      /// <param name="dividers">Список разделителей (null - получить остаток строки, начиная с позиции startIndex)</param>
      /// <returns>Искомое слово либо String.Empty</returns>
      public static string GetWord(string line, ref int startIndex, List<char> dividers)
      {
        if (string.IsNullOrEmpty(line) || startIndex < 0 || startIndex >= line.Length)
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

      /// <summary>
      /// Вернуть значение указанного размера (байты) в виде строки,
      /// с приведением значения к соответствующим величинам (б, Кб, Мб, Гб)
      /// </summary>
      /// <param name="size">Размер (байт)</param>
      /// <returns>Значение размера в виде строки</returns>
      public static string GetSizeString(long size)
      {
        if (size <= 0L)
          return string.Empty;
        if (size < 1024L /*0x0400*/)
          return string.Format(LocalizationHolder.rm.GetString("Interfaces_756"), (object) size);
        if (size < 1048576L /*0x100000*/)
          return string.Format(LocalizationHolder.rm.GetString("Interfaces_757"), (object) ((double) size / 1024.0));
        if (size < 1073741824L /*0x40000000*/)
          return string.Format(LocalizationHolder.rm.GetString("Interfaces_758"), (object) ((double) size / 1048576.0));
        return size < 1099511627776L /*0x010000000000*/ ? string.Format(LocalizationHolder.rm.GetString("Interfaces_759"), (object) ((double) size / 1073741824.0)) : string.Format(LocalizationHolder.rm.GetString("Interfaces_760"), (object) ((double) size / 1099511627776.0));
      }

      /// <summary>
      /// Вернуть значение указанной скорости (байты/сек) в виде строки,
      /// с приведением скорости к соответствующим величинам (б/сек, Кб/сек, Мб/сек, Гб/сек)
      /// </summary>
      /// <param name="speed">Скорость (байт/сек)</param>
      /// <returns>Значение скорости в виде строки</returns>
      public static string GetSpeedString(double speed)
      {
        if (speed <= 0.0)
          return string.Empty;
        if (speed < 1024.0)
          return string.Format(LocalizationHolder.rm.GetString("Interfaces_761"), (object) speed);
        if (speed < 1048576.0)
          return string.Format(LocalizationHolder.rm.GetString("Interfaces_762"), (object) (speed / 1024.0));
        if (speed < 1073741824.0)
          return string.Format(LocalizationHolder.rm.GetString("Interfaces_763"), (object) (speed / 1048576.0));
        return speed < 1099511627776.0 ? string.Format(LocalizationHolder.rm.GetString("Interfaces_764"), (object) (speed / 1073741824.0)) : string.Format(LocalizationHolder.rm.GetString("Interfaces_765"), (object) (speed / 1099511627776.0));
      }

      /// <summary>
      /// Вернуть значение указанного интервала времени в виде строки
      /// </summary>
      /// <param name="delta">Временной интервал</param>
      /// <param name="alignValues">Выравнивать значения (нулями)</param>
      /// <returns>Значение указанного интервала времени в виде строки</returns>
      public static string GetTimeSpanString(TimeSpan delta, bool alignValues)
      {
        StringBuilder stringBuilder = new StringBuilder();
        if (delta.Days > 0)
          stringBuilder.Append(string.Format(LocalizationHolder.rm.GetString("Interfaces_766"), (object) delta.Days));
        if (delta.Hours > 0)
        {
          if (alignValues)
            stringBuilder.Append(string.Format(LocalizationHolder.rm.GetString("Interfaces_767"), (object) delta.Hours));
          else
            stringBuilder.Append(string.Format(LocalizationHolder.rm.GetString("Interfaces_768"), (object) delta.Hours));
        }
        if (delta.Minutes > 0)
        {
          if (alignValues)
            stringBuilder.Append(string.Format(LocalizationHolder.rm.GetString("Interfaces_769"), (object) delta.Minutes));
          else
            stringBuilder.Append(string.Format(LocalizationHolder.rm.GetString("Interfaces_770"), (object) delta.Minutes));
        }
        if (delta.Seconds > 0)
        {
          if (alignValues)
            stringBuilder.Append(string.Format(LocalizationHolder.rm.GetString("Interfaces_771"), (object) delta.Seconds));
          else
            stringBuilder.Append(string.Format(LocalizationHolder.rm.GetString("Interfaces_772"), (object) delta.Seconds));
        }
        if (delta.Milliseconds > 0)
        {
          if (alignValues)
            stringBuilder.Append(string.Format(LocalizationHolder.rm.GetString("Interfaces_773"), (object) delta.Milliseconds));
          else
            stringBuilder.Append(string.Format(LocalizationHolder.rm.GetString("Interfaces_774"), (object) delta.Milliseconds));
        }
        if (stringBuilder.Length == 0)
          stringBuilder.Append(LocalizationHolder.rm.GetString("Interfaces_775"));
        return stringBuilder.ToString().Trim();
      }

      /// <summary>Преобразовать строку вида hex hex hex в массив байт</summary>
      /// <param name="hex">Строка вида hex hex hex</param>
      /// <returns>Массив байт</returns>
      public static byte[] HEX2Bytes(string hex)
      {
        if (string.IsNullOrEmpty(hex))
          return new byte[0];
        string[] strArray = hex.Split(StringsHelper.DividersHexValues, StringSplitOptions.RemoveEmptyEntries);
        byte[] numArray = new byte[strArray.Length];
        for (int index = 0; index < strArray.Length; ++index)
        {
          byte num = Convert.ToByte(strArray[index], 16 /*0x10*/);
          numArray[index] = num;
        }
        return numArray;
      }

      /// <summary>Преобразовать массив байт в строку вида hex hex hex</summary>
      /// <param name="val">Массив байт</param>
      /// <returns>Строка вида hex hex hex</returns>
      public static string Bytes2HEX(byte[] val)
      {
        if (val == null || val.Length == 0)
          return string.Empty;
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < val.Length; ++index)
          stringBuilder.Append(val[index].ToString("X2", (IFormatProvider) CultureInfo.InvariantCulture));
        return stringBuilder.ToString();
      }

      /// <summary>
      /// Из строки вида hex hex hex вернуть обычную Unicode-строку
      /// </summary>
      /// <param name="hex">Строка вида hex hex hex</param>
      /// <returns>Строковое значение</returns>
      public static string HEX2String(string hex)
      {
        if (string.IsNullOrEmpty(hex))
          return string.Empty;
        string[] strArray = hex.Split(StringsHelper.DividersHexValues, StringSplitOptions.RemoveEmptyEntries);
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < strArray.Length; ++index)
        {
          char int32 = (char) Convert.ToInt32(strArray[index], 16 /*0x10*/);
          stringBuilder.Append(int32);
        }
        return stringBuilder.ToString();
      }

      /// <summary>Из строки вида hex hex hex вернуть поток</summary>
      /// <param name="hex">Строка вида hex hex hex</param>
      /// <returns>Значение в виде потока</returns>
      public static MemoryStream HEX2Stream(string hex)
      {
        MemoryStream memoryStream = new MemoryStream();
        if (string.IsNullOrEmpty(hex))
          return memoryStream;
        string[] strArray = hex.Split(StringsHelper.DividersHexValues, StringSplitOptions.RemoveEmptyEntries);
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < strArray.Length; ++index)
        {
          byte num = Convert.ToByte(strArray[index], 16 /*0x10*/);
          memoryStream.WriteByte(num);
        }
        memoryStream.Position = 0L;
        return memoryStream;
      }

      /// <summary>
      /// Из строки вида hex hex hex вернуть список найденных идентификаторов
      /// </summary>
      /// <param name="hex">Строка вида hex hex hex</param>
      /// <returns>Список найденных идентификаторов</returns>
      public static List<string> HEX2Identifiers(string hex)
      {
        List<string> stringList = new List<string>();
        if (string.IsNullOrEmpty(hex))
          return stringList;
        string[] strArray = hex.Split(StringsHelper.DividersHexValues, StringSplitOptions.RemoveEmptyEntries);
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < strArray.Length; ++index)
        {
          int int32 = Convert.ToInt32(strArray[index], 16 /*0x10*/);
          if (int32 < 32 /*0x20*/)
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
            char ch = (char) int32;
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

      /// <summary>Преобразовать Int32 в HEX-строку</summary>
      /// <param name="value">Значение</param>
      /// <returns>HEX-строка</returns>
      public static string IntToHex(int value)
      {
        return value.ToString("X", (IFormatProvider) CultureInfo.InvariantCulture);
      }

      /// <summary>Преобразовать Int64 в HEX-строку</summary>
      /// <param name="value">Значение</param>
      /// <returns>HEX-строка</returns>
      public static string IntToHex(long value)
      {
        return value.ToString("X", (IFormatProvider) CultureInfo.InvariantCulture);
      }

      /// <summary>Преобразовать HEX-строку в Int32</summary>
      /// <param name="value">HEX-строка</param>
      /// <returns>Int32</returns>
      public static int HexToInt32(string value)
      {
        int result = 0;
        if (!int.TryParse(value, NumberStyles.HexNumber, (IFormatProvider) CultureInfo.InvariantCulture, out result))
          result = 0;
        return result;
      }

      /// <summary>Преобразовать HEX-строку в Int64</summary>
      /// <param name="value">HEX-строка</param>
      /// <returns>Int64</returns>
      public static long HexToInt64(string value)
      {
        long result = 0;
        if (!long.TryParse(value, NumberStyles.HexNumber, (IFormatProvider) CultureInfo.InvariantCulture, out result))
          result = 0L;
        return result;
      }

      /// <summary>Преобразовать дату в строку для редактирования</summary>
      /// <param name="value">Значение</param>
      /// <returns>Строка</returns>
      public static string DateToEditableStr(DateTime value)
      {
        return value.Date.ToString("dd.MM.yyyy", (IFormatProvider) CultureInfo.InvariantCulture);
      }

      /// <summary>Преобразовать строку в дату</summary>
      /// <param name="value">Значение</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>Дата или значение по умолчанию в случае ошибки</returns>
      public static DateTime DateFromEditableStr(string value, DateTime defValue)
      {
        DateTime result = defValue;
        return DateTime.TryParseExact(value, "dd.MM.yyyy", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) || DateTime.TryParseExact(value, "dd.M.yyyy", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) || DateTime.TryParseExact(value, "d.MM.yyyy", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) || DateTime.TryParseExact(value, "d.M.yyyy", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) ? result : defValue;
      }

      /// <summary>Преобразовать дату в строку</summary>
      /// <param name="value">Значение</param>
      /// <returns>Строка</returns>
      public static string DateToStr(DateTime value)
      {
        return value.Date.ToString("d", (IFormatProvider) CultureInfo.InvariantCulture);
      }

      /// <summary>
      /// Преобразовать дату в строку (HEX-кодированную, вида YYYY/MM/DD)
      /// </summary>
      /// <param name="value">Значение</param>
      /// <returns>Строка (HEX-кодированная, вида YYYY/MM/DD)</returns>
      public static string DateToStrHex(DateTime value)
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(StringsHelper.IntToHex(value.Year));
        stringBuilder.Append(StringsHelper.DateSeparator);
        stringBuilder.Append(StringsHelper.IntToHex(value.Month));
        stringBuilder.Append(StringsHelper.DateSeparator);
        stringBuilder.Append(StringsHelper.IntToHex(value.Day));
        return stringBuilder.ToString();
      }

      /// <summary>Преобразовать строку в дату</summary>
      /// <param name="value">Значение</param>
      /// <returns>Дата</returns>
      public static DateTime DateFromStr(string value)
      {
        return Convert.ToDateTime(value, (IFormatProvider) CultureInfo.InvariantCulture).Date;
      }

      /// <summary>
      /// Преобразовать строку (HEX-кодированную, вида YYYY/MM/DD) в дату
      /// </summary>
      /// <param name="value">Значение (HEX-кодированное, вида YYYY/MM/DD)</param>
      /// <returns>Дата (или DateTime.MinValue)</returns>
      public static DateTime DateFromStrHex(string value)
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

      /// <summary>
      /// Обрезать часть строки, если её длина превышает указанную величину
      /// </summary>
      /// <param name="value">Строка</param>
      /// <param name="maxLen">Максимально допустимая длина строки</param>
      /// <returns>Откорректированная строка</returns>
      public static string TrimString(string value, int maxLen)
      {
        if (string.IsNullOrEmpty(value) || maxLen <= 0)
          return string.Empty;
        return value.Length > maxLen ? value.Substring(0, maxLen) : value;
      }

      /// <summary>
      /// Разделить строку на части указанной максимальной длины
      /// </summary>
      /// <param name="value">Строка</param>
      /// <param name="maxLen">Максимально допустимая длина фрагмента сети</param>
      /// <returns></returns>
      public static List<string> SplitString(string value, int maxLen)
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

      /// <summary>
      /// Проверить наличие указанного слова в массиве с учётом указанных параметров
      /// </summary>
      /// <param name="words">Массив проверяемых слов</param>
      /// <param name="word">Искомое слово</param>
      /// <param name="caseSensitive">true - учитывать регистр букв</param>
      /// <returns>true - слово найдено в массиве</returns>
      public static bool Exists(string[] words, string word, bool caseSensitive)
      {
        if (words == null || words.Length == 0 || string.IsNullOrEmpty(word))
          return false;
        for (int index = 0; index < words.Length; ++index)
        {
          if (caseSensitive && StringComparer.CurrentCulture.Compare(words[index], word) == 0 || !caseSensitive && StringComparer.CurrentCultureIgnoreCase.Compare(words[index], word) == 0)
            return true;
        }
        return false;
      }

      /// <summary>
      /// Проверить наличие подстроки в строке с учётом указанных параметров
      /// </summary>
      /// <param name="value">Строка</param>
      /// <param name="subString">Искомая подстрока</param>
      /// <param name="caseSensitive">true - учитывать регистр букв</param>
      /// <param name="wholeWords">true - искать только вхождение целых слов</param>
      /// <returns>true, если подстрока найдена</returns>
      public static bool Exists(string value, string subString, bool caseSensitive, bool wholeWords)
      {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(subString))
          return false;
        if (!wholeWords)
          return value.IndexOf(subString, 0, caseSensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase) >= 0;
        string[] words = value.Split(StringsHelper.DividersForWords, StringSplitOptions.RemoveEmptyEntries);
        string[] strArray = subString.Split(StringsHelper.DividersForWords, StringSplitOptions.RemoveEmptyEntries);
        if (!wholeWords)
        {
          for (int index = 0; index < words.Length; ++index)
          {
            if (caseSensitive && StringComparer.CurrentCulture.Compare(words[index], subString) == 0 || !caseSensitive && StringComparer.CurrentCultureIgnoreCase.Compare(words[index], subString) == 0)
              return true;
          }
          return false;
        }
        bool flag = true;
        for (int index = 0; index < strArray.Length; ++index)
        {
          flag = flag && StringsHelper.Exists(words, strArray[index], caseSensitive);
          if (!flag)
            break;
        }
        return flag;
      }

      /// <summary>
      /// Извлечь второе расширение из имени файла, если оно есть
      /// (например, для "file.ext2.ext1" метод вернёт ".ext2")
      /// </summary>
      /// <param name="fileName">Имя файла</param>
      /// <returns>Второе расширение из имени файла, если оно есть</returns>
      public static string GetSecondFileExtension(string fileName)
      {
        return Path.GetExtension(Path.GetFileNameWithoutExtension(fileName));
      }

      /// <summary>Проверить, является ли расширение файла числом</summary>
      /// <param name="fileName">Имя файла</param>
      /// <returns>true - расширение является числом</returns>
      public static bool IsNumericFileExtension(string fileName)
      {
        string s = StringsHelper.ExtractFrom(Path.GetExtension(fileName), ".", string.Empty).Trim();
        return !string.IsNullOrEmpty(s) && long.TryParse(s, out long _);
      }
    }
}
