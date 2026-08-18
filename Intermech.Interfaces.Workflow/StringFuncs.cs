// Decompiled with JetBrains decompiler
// Type: Intermech.StringFuncs
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System.Text.RegularExpressions;

#nullable disable
namespace Intermech;

public class StringFuncs
{
  private static Regex _spaceRegex = new Regex("([\\r\\n]+)\\s+", RegexOptions.Compiled | RegexOptions.Singleline);
  private static Regex _splitRegex = new Regex("(\\W+)", RegexOptions.Compiled | RegexOptions.Singleline);

  public static string UCFirst(string s)
  {
    if (!(s != ""))
      return s;
    char[] charArray = s.ToCharArray();
    bool flag = false;
    int length = charArray.Length;
    for (int index = 0; index < length; ++index)
    {
      int num = char.IsLetter(charArray[index]) ? 1 : 0;
      if (num != 0 && !flag)
        charArray[index] = char.ToUpper(charArray[index]);
      flag = num != 0;
    }
    return new string(charArray);
  }

  public static string ReplaceChar(string s, int index, char replacement)
  {
    char[] charArray = s.ToCharArray();
    charArray[index] = replacement;
    return new string(charArray);
  }

  public static string ReplaceMacros(string s, StringFuncs.GetMacroValueDelegate getValue)
  {
    string str1 = "";
    int num1 = -1;
    int startIndex = 0;
    int num2;
    do
    {
      num2 = s.IndexOf("%", startIndex);
      if (num2 != -1)
      {
        string str2 = str1 + s.Substring(startIndex, num2 - startIndex);
        startIndex = num2 + 1;
        num1 = s.IndexOf("%", startIndex);
        if (num1 != -1)
        {
          startIndex = num1 + 1;
          string Name = s.Substring(num2 + 1, num1 - num2 - 1);
          str1 = !(Name == "") ? str2 + getValue(Name) : str2 + "%";
        }
        else
          str1 = str2 + s.Substring(startIndex - 1);
      }
      else
        str1 += s.Substring(startIndex);
    }
    while (num2 != -1 && num1 != -1);
    return str1;
  }

  public static string WordWrap(string s, int width = 75, bool removeExtraSpaces = true)
  {
    int num = 0;
    string str1 = "";
    if (removeExtraSpaces)
    {
      s = s.Replace('\t', ' ');
      s = StringFuncs._spaceRegex.Replace(s, "$1");
    }
    foreach (string str2 in StringFuncs._splitRegex.Split(s))
    {
      if (str2.Contains("\r"))
        num = 0;
      if (num > 0 && num + str2.Length > width)
      {
        str1 += "\r\n";
        num = 0;
      }
      if (num == 0)
        str2 = str2.TrimStart(' ');
      str1 += str2;
      num += str2.Length;
    }
    return str1;
  }

  public delegate string GetMacroValueDelegate(string Name);
}
