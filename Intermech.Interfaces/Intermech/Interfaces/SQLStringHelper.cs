
// Type: Intermech.Interfaces.SQLStringHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Text;


namespace Intermech.Interfaces
{
    public static class SQLStringHelper
    {
      /// <summary>Таблица преобразования русских символов в латинские</summary>
      private static Dictionary<char, string> _TransliterateTable = new Dictionary<char, string>();

      static SQLStringHelper()
      {
        SQLStringHelper._TransliterateTable.Add('Ё', "E");
        SQLStringHelper._TransliterateTable.Add('ё', "e");
        SQLStringHelper._TransliterateTable.Add('Й', "J");
        SQLStringHelper._TransliterateTable.Add('й', "j");
        SQLStringHelper._TransliterateTable.Add('Ц', "TS");
        SQLStringHelper._TransliterateTable.Add('ц', "ts");
        SQLStringHelper._TransliterateTable.Add('У', "U");
        SQLStringHelper._TransliterateTable.Add('у', "u");
        SQLStringHelper._TransliterateTable.Add('К', "K");
        SQLStringHelper._TransliterateTable.Add('к', "k");
        SQLStringHelper._TransliterateTable.Add('Е', "E");
        SQLStringHelper._TransliterateTable.Add('е', "e");
        SQLStringHelper._TransliterateTable.Add('Н', "N");
        SQLStringHelper._TransliterateTable.Add('н', "n");
        SQLStringHelper._TransliterateTable.Add('Г', "G");
        SQLStringHelper._TransliterateTable.Add('г', "g");
        SQLStringHelper._TransliterateTable.Add('Ш', "SH");
        SQLStringHelper._TransliterateTable.Add('ш', "sh");
        SQLStringHelper._TransliterateTable.Add('Щ', "SCH");
        SQLStringHelper._TransliterateTable.Add('щ', "sch");
        SQLStringHelper._TransliterateTable.Add('З', "Z");
        SQLStringHelper._TransliterateTable.Add('з', "z");
        SQLStringHelper._TransliterateTable.Add('Х', "KH");
        SQLStringHelper._TransliterateTable.Add('х', "kh");
        SQLStringHelper._TransliterateTable.Add('Ъ', "");
        SQLStringHelper._TransliterateTable.Add('ъ', "");
        SQLStringHelper._TransliterateTable.Add('Ф', "F");
        SQLStringHelper._TransliterateTable.Add('ф', "f");
        SQLStringHelper._TransliterateTable.Add('Ы', "Y");
        SQLStringHelper._TransliterateTable.Add('ы', "y");
        SQLStringHelper._TransliterateTable.Add('В', "V");
        SQLStringHelper._TransliterateTable.Add('в', "v");
        SQLStringHelper._TransliterateTable.Add('А', "A");
        SQLStringHelper._TransliterateTable.Add('а', "a");
        SQLStringHelper._TransliterateTable.Add('П', "P");
        SQLStringHelper._TransliterateTable.Add('п', "p");
        SQLStringHelper._TransliterateTable.Add('Р', "R");
        SQLStringHelper._TransliterateTable.Add('р', "r");
        SQLStringHelper._TransliterateTable.Add('О', "O");
        SQLStringHelper._TransliterateTable.Add('о', "o");
        SQLStringHelper._TransliterateTable.Add('Л', "L");
        SQLStringHelper._TransliterateTable.Add('л', "l");
        SQLStringHelper._TransliterateTable.Add('Д', "D");
        SQLStringHelper._TransliterateTable.Add('д', "d");
        SQLStringHelper._TransliterateTable.Add('Ж', "ZH");
        SQLStringHelper._TransliterateTable.Add('ж', "zh");
        SQLStringHelper._TransliterateTable.Add('Э', "E");
        SQLStringHelper._TransliterateTable.Add('э', "e");
        SQLStringHelper._TransliterateTable.Add('Я', "JA");
        SQLStringHelper._TransliterateTable.Add('я', "ja");
        SQLStringHelper._TransliterateTable.Add('Ч', "CH");
        SQLStringHelper._TransliterateTable.Add('ч', "ch");
        SQLStringHelper._TransliterateTable.Add('С', "S");
        SQLStringHelper._TransliterateTable.Add('с', "s");
        SQLStringHelper._TransliterateTable.Add('М', "M");
        SQLStringHelper._TransliterateTable.Add('м', "m");
        SQLStringHelper._TransliterateTable.Add('И', "I");
        SQLStringHelper._TransliterateTable.Add('и', "i");
        SQLStringHelper._TransliterateTable.Add('Т', "T");
        SQLStringHelper._TransliterateTable.Add('т', "t");
        SQLStringHelper._TransliterateTable.Add('Ь', "");
        SQLStringHelper._TransliterateTable.Add('ь', "");
        SQLStringHelper._TransliterateTable.Add('Б', "B");
        SQLStringHelper._TransliterateTable.Add('б', "b");
        SQLStringHelper._TransliterateTable.Add('Ю', "JU");
        SQLStringHelper._TransliterateTable.Add('ю', "ju");
      }

      /// <summary>
      /// Функция пробразует символы кириллицы в строке в латинский транслит
      /// </summary>
      /// <param name="val">Исходная строка с кириллицей</param>
      /// <returns>Транслитерированная строка</returns>
      public static string Translit(string val)
      {
        if (val == null || val.Length == 0)
          return val;
        StringBuilder stringBuilder = new StringBuilder(val.Length);
        for (int index = 0; index < val.Length; ++index)
        {
          string str;
          if (SQLStringHelper._TransliterateTable.TryGetValue(val[index], out str))
            stringBuilder.Append(str);
          else
            stringBuilder.Append(val[index]);
        }
        return stringBuilder.ToString();
      }

      /// <summary>
      /// Корректировка значений фильтов / условий (только для LIKE)
      /// </summary>
      /// <param name="data"></param>
      /// <returns></returns>
      public static string QuoteLikeString(string data)
      {
        char[] chArray = new char[4]{ '*', '%', '[', ']' };
        if (string.IsNullOrEmpty(data) || data.IndexOfAny(chArray) == -1)
          return data;
        char[] charArray = data.ToCharArray();
        int length = data.Length;
        StringBuilder stringBuilder = new StringBuilder(length + 6);
        for (int index = 0; index < length; ++index)
        {
          char ch = charArray[index];
          if (Array.IndexOf<char>(chArray, ch) != -1)
          {
            stringBuilder.Append('[');
            stringBuilder.Append(ch);
            stringBuilder.Append(']');
          }
          else
            stringBuilder.Append(ch);
        }
        return stringBuilder.ToString();
      }
    }
}
