
// Type: Intermech.Interfaces.Dictionary.ExtFinder
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Interfaces.Dictionary
{
    public class ExtFinder
    {
      /// <summary>
      /// Получение результирующей строки с подстановкой окончания
      /// </summary>
      /// <param name="num">Число для проверки окончания</param>
      /// <param name="word">Слово для подстановки</param>
      /// <returns>Строка с результатом</returns>
      public static string GetString(long num, DictWord word)
      {
        string str = string.Empty;
        foreach (DictEnding ending in word.Endings)
        {
          bool flag1 = true;
          foreach (DictRule rule in ending.Rules)
          {
            long num1;
            switch (rule.VOP)
            {
              case DictVOP.Value:
                num1 = num;
                break;
              case DictVOP.Div:
                num1 = num / rule.VOPValue;
                break;
              case DictVOP.Mod:
                num1 = num % rule.VOPValue;
                break;
              default:
                throw new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces_11"), (object) rule.ToString()));
            }
            bool flag2;
            switch (rule.ROP)
            {
              case DictROP.Equal:
                flag2 = num1.Equals(rule.ROPValue1);
                break;
              case DictROP.NotEqual:
                flag2 = !num1.Equals(rule.ROPValue1);
                break;
              case DictROP.More:
                flag2 = num1 > rule.ROPValue1;
                break;
              case DictROP.NotMore:
                flag2 = num1 < rule.ROPValue1;
                break;
              case DictROP.MoreOrEqual:
                flag2 = num1 >= rule.ROPValue1;
                break;
              case DictROP.Less:
                flag2 = num1 < rule.ROPValue1;
                break;
              case DictROP.NotLess:
                flag2 = num1 > rule.ROPValue1;
                break;
              case DictROP.LessOrEqual:
                flag2 = num1 <= rule.ROPValue1;
                break;
              case DictROP.In:
                flag2 = num1 >= rule.ROPValue1 && num1 <= rule.ROPValue2;
                break;
              case DictROP.NotIn:
                flag2 = num1 < rule.ROPValue1 || num1 > rule.ROPValue2;
                break;
              default:
                throw new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces_12"), (object) rule.ToString()));
            }
            flag1 &= flag2;
          }
          if (flag1)
          {
            str = ending.Ending;
            break;
          }
        }
        return str.Equals(string.Empty) ? word.Word : $"{word.Word}{str}";
      }

      /// <summary>
      /// Получение результирующей строки с подстановкой окончания
      /// </summary>
      /// <param name="num">Число для проверки окончания</param>
      /// <param name="lang">Язык со словами для подстановки</param>
      /// <param name="wordName">Слово исходное для сравнения</param>
      /// <returns>Строка с результатом</returns>
      public static string GetString(long num, LangHelper lang, string wordName)
      {
        string empty = string.Empty;
        foreach (DictWord word in lang.Words)
        {
          if (string.Compare(word.Word, wordName, true).Equals(0))
          {
            empty = ExtFinder.GetString(num, word);
            break;
          }
        }
        return empty;
      }
    }
}
