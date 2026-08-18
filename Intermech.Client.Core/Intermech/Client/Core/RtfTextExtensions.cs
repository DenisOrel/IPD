
// Type: Intermech.Client.Core.RtfTextExtensions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Text;


namespace Intermech.Client.Core;

public static class RtfTextExtensions
{
  public const string RtfBoldTemplate = "\\b {0}: \\b0 ";
  public const string RtfNewLineTemplate = "\\line ";

  public static string GetRtfUnicodeEscapedString(this string s, bool bold = false)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (char ch in s)
    {
      if (ch == '\\' || ch == '{' || ch == '}')
        stringBuilder.Append("\\" + ch.ToString());
      else if (ch <= '\u007F')
      {
        if (ch == '\n')
          stringBuilder.Append("\\line ");
        else
          stringBuilder.Append(ch);
      }
      else
        stringBuilder.Append($"\\u{(object) Convert.ToUInt32(ch)}?");
    }
    return !bold ? stringBuilder.ToString() : $"\\b {stringBuilder}: \\b0 ";
  }
}
