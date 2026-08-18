// Decompiled with JetBrains decompiler
// Type: Intermech.HtmlUtils
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech;

public class HtmlUtils
{
  private static List<string> unaryTags = new List<string>((IEnumerable<string>) new string[14]
  {
    "area",
    "base",
    "br",
    "dd",
    "dt",
    "hr",
    "img",
    "input",
    "li",
    "link",
    "meta",
    "p",
    "param",
    "font"
  });

  /// <summary>Closes all unclosed HTML tags</summary>
  /// <param name="p"></param>
  /// <returns></returns>
  public static string CloseTags(string s)
  {
    int num = 0;
    List<string> stringList = new List<string>();
    MatchCollection matchCollection = new Regex("<([/{A-z}]+)\\s?[^>]*?>", RegexOptions.Singleline).Matches(s.ToLower());
    if (matchCollection.Count > 0)
    {
      foreach (Match match in matchCollection)
      {
        if (match.Success && match.Groups.Count > 1)
        {
          string str1 = match.Groups[1].ToString();
          if (HtmlUtils.unaryTags.IndexOf(str1) == -1)
          {
            if (str1.Length > 0 && str1[0] == '/')
            {
              if (stringList.Count > 0)
              {
                string str2 = str1.Remove(0, 1);
                int index;
                for (index = stringList.Count - 1; index >= 0 && stringList[index] != str2; --index)
                  ++num;
                if (index >= 0)
                  stringList.RemoveRange(index, stringList.Count - index);
              }
            }
            else
              stringList.Add(str1);
          }
        }
      }
      int startIndex1 = matchCollection[matchCollection.Count - 1].Index + matchCollection[matchCollection.Count - 1].Length;
      int startIndex2 = s.IndexOf("<", startIndex1);
      if (startIndex2 != -1)
        s = s.Remove(startIndex2);
    }
    string str = "";
    foreach (object obj in stringList)
      str = $"</{obj}>\r\n" + str;
    s += str;
    return s;
  }

  public static string nl2br(string s)
  {
    StringBuilder stringBuilder = new StringBuilder(s);
    stringBuilder.Replace("\r\n", "\u0001\u0002\u0003");
    stringBuilder.Replace("\n", "\u0001\u0002\u0003");
    stringBuilder.Replace("\u0001\u0002\u0003", "<br />\r\n");
    return stringBuilder.ToString();
  }
}
