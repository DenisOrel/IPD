
// Type: Intermech.Controls.SpellCheck.Utility
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Collections.Generic;
using System.Globalization;


namespace Intermech.Controls.SpellCheck;

internal sealed class Utility
{
  public static string AddPrefix(string word, Struct.AffixRule rule)
  {
    foreach (Struct.AffixEntry affixEntry in (List<Struct.AffixEntry>) rule.AffixEntries)
    {
      if (word.Length >= affixEntry.ConditionCount)
      {
        int num1 = 0;
        int num2 = affixEntry.ConditionCount - 1;
        for (int index1 = 0; index1 <= num2; ++index1)
        {
          int index2 = (int) word[index1];
          if ((affixEntry.Condition[index2] & 1 << index1) == 1 << index1)
            ++num1;
          else
            break;
        }
        if (num1 == affixEntry.ConditionCount)
        {
          string str = word.Substring(affixEntry.StripCharacters.Length);
          return affixEntry.AddCharacters + str;
        }
      }
    }
    return word;
  }

  public static string AddSuffix(string word, Struct.AffixRule rule)
  {
    foreach (Struct.AffixEntry affixEntry in (List<Struct.AffixEntry>) rule.AffixEntries)
    {
      if (word.Length >= affixEntry.ConditionCount)
      {
        int num1 = 0;
        int num2 = affixEntry.ConditionCount - 1;
        for (int index1 = 0; index1 <= num2; ++index1)
        {
          int index2 = (int) word[word.Length - (affixEntry.ConditionCount - index1)];
          if ((affixEntry.Condition[index2] & 1 << index1) == 1 << index1)
            ++num1;
          else
            break;
        }
        if (num1 == affixEntry.ConditionCount)
        {
          int length = word.Length - affixEntry.StripCharacters.Length;
          return word.Substring(0, length) + affixEntry.AddCharacters;
        }
      }
    }
    return word;
  }

  public static void EncodeConditions(string conditionText, Struct.AffixEntry entry)
  {
    int num1 = entry.Condition.Length - 1;
    for (int index = 0; index <= num1; ++index)
      entry.Condition[index] = 0;
    if (conditionText == ".")
    {
      entry.ConditionCount = 0;
    }
    else
    {
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      int num2 = 0;
      char[] chArray = new char[201];
      int index1 = 0;
      string str = conditionText;
      int index2 = 0;
      for (int length = str.Length; index2 < length; ++index2)
      {
        char ch = str[index2];
        if (ch == '[')
          flag2 = true;
        else if ((ch == '^' & flag2 ? 1 : 0) != 0)
          flag1 = true;
        else if (ch == ']')
          flag3 = true;
        else if (flag2)
        {
          chArray[index1] = ch;
          ++index1;
        }
        else
          flag3 = true;
        if (flag3)
        {
          if (flag2)
          {
            if (flag1)
            {
              int num3 = entry.Condition.Length - 1;
              for (int index3 = 0; index3 <= num3; ++index3)
                entry.Condition[index3] |= 1 << num2;
              int num4 = index1 - 1;
              for (int index4 = 0; index4 <= num4; ++index4)
              {
                int index5 = (int) chArray[index4];
                entry.Condition[index5] &= ~(1 << num2);
              }
            }
            else
            {
              int num5 = index1 - 1;
              for (int index6 = 0; index6 <= num5; ++index6)
              {
                int index7 = (int) chArray[index6];
                entry.Condition[index7] |= 1 << num2;
              }
            }
            flag2 = false;
            flag1 = false;
            index1 = 0;
          }
          else if (ch == '.')
          {
            int num6 = entry.Condition.Length - 1;
            for (int index8 = 0; index8 <= num6; ++index8)
              entry.Condition[index8] |= 1 << num2;
          }
          else
          {
            int index9 = (int) ch;
            entry.Condition[index9] |= 1 << num2;
          }
          flag3 = false;
          ++num2;
        }
      }
      entry.ConditionCount = num2;
    }
  }

  public static void EncodeRule(string ruleText, ref Struct.PhoneticRule rule)
  {
    int num1 = rule.Condition.Length - 1;
    for (int index = 0; index <= num1; ++index)
      rule.Condition[index] = 0;
    bool flag1 = false;
    bool flag2 = false;
    char[] chArray = new char[201];
    int index1 = 0;
    string str = ruleText;
    int index2 = 0;
    for (int length = str.Length; index2 < length; ++index2)
    {
      char ch = str[index2];
      switch ((char) ((uint) ch - 36U))
      {
        case char.MinValue:
          rule.EndOnly = true;
          break;
        case '\u0004':
          flag1 = true;
          break;
        case '\u0005':
          flag2 = true;
          break;
        case '\t':
          ++rule.ConsumeCount;
          break;
        case '\f':
        case '\r':
        case '\u000E':
        case '\u000F':
        case '\u0010':
        case '\u0011':
        case '\u0012':
        case '\u0013':
        case '\u0014':
        case '\u0015':
          rule.Priority = int.Parse(ch.ToString((IFormatProvider) CultureInfo.CurrentUICulture));
          break;
        case '\u0018':
          rule.ReplaceMode = true;
          break;
        case ':':
          rule.BeginningOnly = true;
          break;
        default:
          if (flag1)
          {
            chArray[index1] = ch;
            ++index1;
            break;
          }
          flag2 = true;
          break;
      }
      if (flag2)
      {
        if (flag1)
        {
          int num2 = index1 - 1;
          for (int index3 = 0; index3 <= num2; ++index3)
          {
            int index4 = (int) chArray[index3];
            rule.Condition[index4] |= 1 << rule.ConditionCount;
          }
          flag1 = false;
          index1 = 0;
        }
        else
        {
          int index5 = (int) ch;
          rule.Condition[index5] |= 1 << rule.ConditionCount;
        }
        flag2 = false;
        ++rule.ConditionCount;
      }
    }
  }

  public static string RemovePrefix(string word, Struct.AffixEntry entry)
  {
    int num1 = word.Length - entry.AddCharacters.Length;
    if ((num1 <= 0 || num1 + entry.StripCharacters.Length < entry.ConditionCount || !word.StartsWith(entry.AddCharacters) ? 0 : 1) != 0)
    {
      string str1 = word.Substring(entry.AddCharacters.Length);
      string str2 = entry.StripCharacters + str1;
      int num2 = 0;
      int num3 = entry.ConditionCount - 1;
      for (int index1 = 0; index1 <= num3; ++index1)
      {
        int index2 = (int) str2[index1];
        if ((entry.Condition[index2] & 1 << index1) == 1 << index1)
          ++num2;
      }
      if (num2 == entry.ConditionCount)
        return str2;
    }
    return word;
  }

  public static string RemoveSuffix(string word, Struct.AffixEntry entry)
  {
    int length = word.Length - entry.AddCharacters.Length;
    if ((length <= 0 || length + entry.StripCharacters.Length < entry.ConditionCount || !word.EndsWith(entry.AddCharacters) ? 0 : 1) != 0)
    {
      string str = word.Substring(0, length) + entry.StripCharacters;
      int num1 = 0;
      int num2 = entry.ConditionCount - 1;
      for (int index1 = 0; index1 <= num2; ++index1)
      {
        int index2 = (int) str[str.Length - (entry.ConditionCount - index1)];
        if ((entry.Condition[index2] & 1 << index1) == 1 << index1)
          ++num1;
      }
      if (num1 == entry.ConditionCount)
        return str;
    }
    return word;
  }
}
