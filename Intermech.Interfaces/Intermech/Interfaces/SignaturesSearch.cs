
// Type: Intermech.Interfaces.SignaturesSearch
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Вспомогательный класс для сигнатурного поиска в массивах
    /// </summary>
    public static class SignaturesSearch
    {
      /// <summary>Буфер 64 Кб для сигнатурного поиска</summary>
      public static int BuffSize = 65536 /*0x010000*/;

      /// <summary>
      /// Определить минимальную длину заданной строки в указанных кодировках, в байтах
      /// </summary>
      /// <param name="encodings">Список кодировок. Если значение не задано, считается, что используется кодировка Encoding.Unicode</param>
      /// <param name="value">Заданная строка</param>
      /// <returns>Минимальная длина строки в указанных кодировках, в байтах</returns>
      public static int GetMinByteCount(IList<Encoding> encodings, string value)
      {
        return SignaturesSearch.InternalGetByteCount(encodings, value, true);
      }

      /// <summary>
      /// Определить максимальную длину заданной строки в указанных кодировках, в байтах
      /// </summary>
      /// <param name="encodings">Список кодировок. Если значение не задано, считается, что используется кодировка Encoding.Unicode</param>
      /// <param name="value">Заданная строка</param>
      /// <returns>Максимальная длина строки в указанных кодировках, в байтах</returns>
      public static int GetMaxByteCount(IList<Encoding> encodings, string value)
      {
        return SignaturesSearch.InternalGetByteCount(encodings, value, false);
      }

      /// <summary>
      /// Определить минимальную/максимальную длину заданной строки в указанных кодировках, в байтах
      /// </summary>
      /// <param name="encodings">Список кодировок. Если значение не задано, считается, что используется кодировка Encoding.Unicode</param>
      /// <param name="value">Заданная строка</param>
      /// <param name="findMin">true - искать минимум, иначе - максимум</param>
      /// <returns>Минимальная/максимальная длина строки в указанных кодировках, в байтах</returns>
      private static int InternalGetByteCount(IList<Encoding> encodings, string value, bool findMin)
      {
        if (string.IsNullOrEmpty(value))
          return 0;
        if (encodings == null || encodings.Count == 0)
          return Encoding.Unicode.GetByteCount(value);
        int val1 = encodings[0].GetByteCount(value);
        for (int index = 1; index < encodings.Count; ++index)
          val1 = findMin ? Math.Min(val1, encodings[index].GetByteCount(value)) : Math.Max(val1, encodings[index].GetByteCount(value));
        return val1;
      }

      /// <summary>
      /// Отыскать минимальную и максимальную длины среди всех сигнатур для всех указанных кодировок
      /// </summary>
      /// <param name="signatures">Список искомых сигнатур</param>
      /// <param name="encodings">Искомые кодировки. Если список не задан, используется Encoding.Unicode</param>
      /// <returns>Минимальная (T1) и максимальная (T2) длины для всех сигнатур среди всех указанных кодировок</returns>
      public static Tuple<int, int> FindMinMaxLengths(
        IList<string> signatures,
        IList<Encoding> encodings)
      {
        if (signatures == null || signatures.Count == 0)
          return new Tuple<int, int>(0, 0);
        bool flag = encodings == null || encodings.Count == 0;
        if (flag)
          encodings = (IList<Encoding>) new List<Encoding>((IEnumerable<Encoding>) new Encoding[1]
          {
            Encoding.Unicode
          });
        int val1_1 = SignaturesSearch.GetMinByteCount(encodings, signatures[0]);
        int val1_2 = flag ? val1_1 : SignaturesSearch.GetMaxByteCount(encodings, signatures[0]);
        for (int index = 1; index < signatures.Count; ++index)
        {
          int minByteCount = SignaturesSearch.GetMinByteCount(encodings, signatures[index]);
          int val2 = flag ? minByteCount : SignaturesSearch.GetMaxByteCount(encodings, signatures[index]);
          val1_1 = Math.Min(val1_1, minByteCount);
          val1_2 = Math.Max(val1_2, val2);
        }
        return new Tuple<int, int>(val1_1, val1_2);
      }

      /// <summary>
      /// Проверить, встречается ли в массиве байт заданная сигнатура в кодировке по умолчанию (Unicode)
      /// </summary>
      /// <param name="buff">Массив байт</param>
      /// <param name="signature">Искомая сигнатура</param>
      /// <returns>true - как минимум одно вхождение указанной сигнатуры найдено</returns>
      public static bool Exists(byte[] buff, string signature)
      {
        return SignaturesSearch.Exists(buff, 0, 0, (IList<string>) new string[1]
        {
          signature
        }, (IList<Encoding>) null, RegexOptions.None);
      }

      /// <summary>
      /// Проверить, встречается ли в массиве байт заданная сигнатура в кодировке по умолчанию (Unicode)
      /// </summary>
      /// <param name="buff">Массив байт</param>
      /// <param name="index">Индекс, с которого начинается поиск в массиве (по умолчанию - 0)</param>
      /// <param name="count">Количество байт, используемых для поиска в массиве. Значение меньше либо равно 0 - используется остаток массива, начиная с index</param>
      /// <param name="signature">Искомая сигнатура</param>
      /// <returns>true - как минимум одно вхождение указанной сигнатуры найдено</returns>
      public static bool Exists(byte[] buff, int index, int count, string signature)
      {
        return SignaturesSearch.Exists(buff, index, count, (IList<string>) new string[1]
        {
          signature
        }, (IList<Encoding>) null, RegexOptions.None);
      }

      /// <summary>
      /// Проверить, встречается ли в массиве байт заданная сигнатура с использованием регулярных выражений в кодировке по умолчанию (Unicode)
      /// </summary>
      /// <param name="buff">Массив байт</param>
      /// <param name="index">Индекс, с которого начинается поиск в массиве (по умолчанию - 0)</param>
      /// <param name="count">Количество байт, используемых для поиска в массиве. Значение меньше либо равно 0 - используется остаток массива, начиная с index</param>
      /// <param name="signature">Искомая сигнатура с использованием регулярных выражений</param>
      /// <param name="options">Опции для регулярного поиска (если None, регулярные выражения не используются)</param>
      /// <returns>true - как минимум одно вхождение указанной сигнатуры с использованием регулярных выражений найдено</returns>
      public static bool Exists(
        byte[] buff,
        int index,
        int count,
        string signature,
        RegexOptions options)
      {
        return SignaturesSearch.Exists(buff, index, count, (IList<string>) new string[1]
        {
          signature
        }, (IList<Encoding>) null, RegexOptions.None);
      }

      /// <summary>
      /// Проверить, встречается ли в массиве байт заданная сигнатура в любой из указанных кодировок
      /// </summary>
      /// <param name="buff">Массив байт</param>
      /// <param name="signature">Искомая сигнатура</param>
      /// <param name="encodings">Искомые кодировки. Если список не задан, используется Encoding.Unicode</param>
      /// <returns>true - как минимум одно вхождение указанной сигнатуры найдено</returns>
      public static bool Exists(byte[] buff, string signature, IList<Encoding> encodings)
      {
        return SignaturesSearch.Exists(buff, 0, 0, (IList<string>) new string[1]
        {
          signature
        }, encodings, RegexOptions.None);
      }

      /// <summary>
      /// Проверить, встречается ли в массиве байт заданная сигнатура в любой из указанных кодировок
      /// </summary>
      /// <param name="buff">Массив байт</param>
      /// <param name="index">Индекс, с которого начинается поиск в массиве (по умолчанию - 0)</param>
      /// <param name="count">Количество байт, используемых для поиска в массиве. Значение меньше либо равно 0 - используется остаток массива, начиная с index</param>
      /// <param name="signature">Искомая сигнатура</param>
      /// <param name="encodings">Искомые кодировки. Если список не задан, используется Encoding.Unicode</param>
      /// <returns>true - как минимум одно вхождение указанной сигнатуры найдено</returns>
      public static bool Exists(
        byte[] buff,
        int index,
        int count,
        string signature,
        IList<Encoding> encodings)
      {
        return SignaturesSearch.Exists(buff, index, count, (IList<string>) new string[1]
        {
          signature
        }, encodings, RegexOptions.None);
      }

      /// <summary>
      /// Проверить, встречается ли в массиве байт любая из заданных сигнатур в любой из указанных кодировок
      /// </summary>
      /// <param name="buff">Массив байт</param>
      /// <param name="signatures">Список искомых сигнатур</param>
      /// <param name="encodings">Искомые кодировки. Если список не задан, используется Encoding.Unicode</param>
      /// <returns>true - как минимум одно вхождение какой-то сигнатуры найдено</returns>
      public static bool Exists(byte[] buff, IList<string> signatures, IList<Encoding> encodings)
      {
        return SignaturesSearch.Exists(buff, 0, 0, signatures, encodings, RegexOptions.None);
      }

      /// <summary>
      /// Проверить, встречается ли в массиве байт любая из заданных сигнатур в любой из указанных кодировок
      /// </summary>
      /// <param name="buff">Массив байт</param>
      /// <param name="index">Индекс, с которого начинается поиск в массиве</param>
      /// <param name="count">Количество байт, используемых для поиска в массиве. Значение меньше либо равно 0 - используется остаток массива, начиная с index</param>
      /// <param name="signatures">Список искомых сигнатур (содержат регулярные выражения, если опции отличаются от значения None)</param>
      /// <param name="encodings">Искомые кодировки. Если список не задан, используется Encoding.Unicode</param>
      /// <param name="options">Если значение опций отличается от None, используется поиск по регулярным выражениям, которые заданы в сигнатурах</param>
      /// <returns>true - как минимум одно вхождение какой-то сигнатуры найдено</returns>
      public static bool Exists(
        byte[] buff,
        int index,
        int count,
        IList<string> signatures,
        IList<Encoding> encodings,
        RegexOptions options)
      {
        if (index < 0)
          throw new ArgumentOutOfRangeException(nameof (index));
        if (buff == null || buff.Length == 0 || signatures == null || signatures.Count == 0)
          return false;
        if (index >= buff.Length)
          throw new ArgumentOutOfRangeException(nameof (index));
        if (count <= 0)
          count = buff.Length - index;
        if (index + count > buff.Length)
          throw new ArgumentOutOfRangeException(nameof (count));
        bool flag = options != 0;
        Tuple<int, int> tuple = !flag ? SignaturesSearch.FindMinMaxLengths(signatures, encodings) : new Tuple<int, int>(0, 0);
        if (!flag && (tuple.Item2 == 0 || count < tuple.Item1))
          return false;
        if ((encodings == null ? 1 : (encodings.Count == 0 ? 1 : 0)) != 0)
          encodings = (IList<Encoding>) new List<Encoding>((IEnumerable<Encoding>) new Encoding[1]
          {
            Encoding.Unicode
          });
        Dictionary<string, Regex> dictionary = new Dictionary<string, Regex>(signatures.Count);
        List<string> stringList = new List<string>(signatures.Count);
        for (int index1 = 0; index1 < signatures.Count; ++index1)
        {
          string signature = signatures[index1];
          if (!dictionary.ContainsKey(signature))
          {
            stringList.Add(signature);
            dictionary[signature] = flag ? new Regex(signature, options) : (Regex) null;
          }
        }
        signatures = (IList<string>) stringList;
        for (int index2 = 0; index2 < encodings.Count; ++index2)
        {
          try
          {
            string input = encodings[index2].GetString(buff, index, count);
            for (int index3 = 0; index3 < signatures.Count; ++index3)
            {
              if (flag)
              {
                if (dictionary[signatures[index3]].IsMatch(input))
                  return true;
              }
              else if (input.IndexOf(signatures[index3]) >= 0)
                return true;
            }
          }
          catch
          {
          }
        }
        return false;
      }

      /// <summary>
      /// Проверить, встречается ли в массиве байт любая из заданных сигнатур в виде регулярных выражений в любой из указанных кодировок
      /// </summary>
      /// <param name="buff">Массив байт</param>
      /// <param name="index">Индекс, с которого начинается поиск в массиве</param>
      /// <param name="count">Количество байт, используемых для поиска в массиве. Значение меньше либо равно 0 - используется остаток массива, начиная с index</param>
      /// <param name="signatures">Список искомых сигнатур в виде регулярных выражений</param>
      /// <param name="encodings">Искомые кодировки. Если список не задан, используется Encoding.Unicode</param>
      /// <returns>true - как минимум одно вхождение какой-то сигнатуры в виде регулярного выражения найдено</returns>
      public static bool Exists(
        byte[] buff,
        int index,
        int count,
        IList<Regex> signatures,
        IList<Encoding> encodings)
      {
        if (index < 0)
          throw new ArgumentOutOfRangeException(nameof (index));
        if (buff == null || buff.Length == 0 || signatures == null || signatures.Count == 0)
          return false;
        if (index >= buff.Length)
          throw new ArgumentOutOfRangeException(nameof (index));
        if (count <= 0)
          count = buff.Length - index;
        if (index + count > buff.Length)
          throw new ArgumentOutOfRangeException(nameof (count));
        if ((encodings == null ? 1 : (encodings.Count == 0 ? 1 : 0)) != 0)
          encodings = (IList<Encoding>) new List<Encoding>((IEnumerable<Encoding>) new Encoding[1]
          {
            Encoding.Unicode
          });
        for (int index1 = 0; index1 < encodings.Count; ++index1)
        {
          try
          {
            string input = encodings[index1].GetString(buff, index, count);
            for (int index2 = 0; index2 < signatures.Count; ++index2)
            {
              if (signatures[index2].IsMatch(input))
                return true;
            }
          }
          catch
          {
          }
        }
        return false;
      }

      /// <summary>Отыскать сигнатуры в файле</summary>
      /// <param name="fileName">Имя файла</param>
      /// <param name="signatures">Список искомых сигнатур</param>
      /// <param name="encodings">Искомые кодировки. Если список не задан, используется Encoding.Unicode</param>
      /// <returns>true - встретилась ли хотя бы одна из сигнатур в любой из кодировок</returns>
      public static bool SignaturesExists(
        string fileName,
        IList<string> signatures,
        IList<Encoding> encodings)
      {
        if (!File.Exists(fileName))
          throw new FileNotFoundException(fileName);
        if (signatures == null)
          throw new ArgumentNullException(nameof (signatures));
        if (signatures.Count == 0)
          throw new ArgumentException(nameof (signatures));
        bool flag = false;
        FileStream fileStream = (FileStream) null;
        if (SignaturesSearch.FindMinMaxLengths(signatures, encodings).Item2 == 0)
          return false;
        try
        {
          fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
          flag = SignaturesSearch.SignaturesExists((Stream) fileStream, signatures, encodings);
        }
        catch
        {
        }
        finally
        {
          fileStream?.Close();
        }
        return flag;
      }

      /// <summary>Отыскать сигнатуры в потоке</summary>
      /// <param name="stream">Поток</param>
      /// <param name="signatures">Список искомых сигнатур</param>
      /// <param name="encodings">Искомые кодировки. Если список не задан, используется Encoding.Unicode</param>
      /// <returns>true - встретилась ли хотя бы одна из сигнатур в любой из кодировок</returns>
      public static bool SignaturesExists(
        Stream stream,
        IList<string> signatures,
        IList<Encoding> encodings)
      {
        if (stream == null)
          throw new ArgumentNullException(nameof (stream));
        if (signatures == null)
          throw new ArgumentNullException(nameof (signatures));
        if (signatures.Count == 0)
          throw new ArgumentException(nameof (signatures));
        bool flag1 = false;
        Tuple<int, int> minMaxLengths = SignaturesSearch.FindMinMaxLengths(signatures, encodings);
        if (minMaxLengths.Item2 == 0)
          return false;
        try
        {
          long num1 = stream.Length - stream.Position;
          long num2 = num1;
          int count = num1 < (long) SignaturesSearch.BuffSize ? Convert.ToInt32(num1) : SignaturesSearch.BuffSize;
          int length1 = minMaxLengths.Item2 * 2;
          byte[] numArray1 = new byte[count];
          byte[] numArray2 = new byte[length1];
          BinaryReader binaryReader = new BinaryReader(stream, Encoding.ASCII);
          bool flag2 = true;
          while (true)
          {
            int num3 = binaryReader.Read(numArray1, 0, count);
            num2 -= (long) num3;
            if (num3 != 0)
            {
              flag1 = SignaturesSearch.Exists(numArray1, 0, num3, signatures, encodings, RegexOptions.None);
              if (!flag1)
              {
                if (flag2)
                {
                  if (num2 > 0L)
                  {
                    if (num3 < count)
                      goto label_23;
                  }
                  else
                    goto label_23;
                }
                if (flag2)
                {
                  Array.Copy((Array) numArray1, numArray1.Length - minMaxLengths.Item2, (Array) numArray2, 0, minMaxLengths.Item2);
                  flag2 = false;
                }
                else
                {
                  int length2 = Math.Min(minMaxLengths.Item2, num3);
                  if (length2 > 0)
                    Array.Copy((Array) numArray1, 0, (Array) numArray2, minMaxLengths.Item2, length2);
                  flag1 = SignaturesSearch.Exists(numArray2, 0, minMaxLengths.Item2 + length2, signatures, encodings, RegexOptions.None);
                  if (!flag1 && num3 >= count)
                  {
                    Array.Clear((Array) numArray2, 0, numArray2.Length);
                    Array.Copy((Array) numArray1, numArray1.Length - minMaxLengths.Item2, (Array) numArray2, 0, minMaxLengths.Item2);
                  }
                  else
                    goto label_20;
                }
              }
              else
                break;
            }
            else
              goto label_23;
          }
          return flag1;
    label_20:
          return flag1;
        }
        catch
        {
        }
    label_23:
        return flag1;
      }
    }
}
