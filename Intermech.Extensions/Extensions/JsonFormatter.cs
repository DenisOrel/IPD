// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.JsonFormatter
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Linq;
using System.Text;

#nullable disable
namespace Intermech.Extensions;

public static class JsonFormatter
{
  private const string INDENT_STRING = "    ";

  [NotNull]
  public static string IndentJSON([NotNull] this string str)
  {
    int count = 0;
    bool flag1 = false;
    StringBuilder sb = new StringBuilder();
    for (int index = 0; index < str.Length; ++index)
    {
      char ch = str[index];
      switch (ch)
      {
        case '"':
          sb.Append(ch);
          bool flag2 = false;
          int num = index;
          while (num > 0 && str[--num] == '\\')
            flag2 = !flag2;
          if (!flag2)
          {
            flag1 = !flag1;
            break;
          }
          break;
        case ',':
          sb.Append(ch);
          if (!flag1)
          {
            sb.AppendLine();
            Enumerable.Range(0, count).InvokeForAll<int>((Action<int>) (item => sb.Append("    ")));
            break;
          }
          break;
        case ':':
          sb.Append(ch);
          if (!flag1)
          {
            sb.Append(" ");
            break;
          }
          break;
        case '[':
        case '{':
          sb.Append(ch);
          if (!flag1)
          {
            sb.AppendLine();
            Enumerable.Range(0, ++count).InvokeForAll<int>((Action<int>) (item => sb.Append("    ")));
            break;
          }
          break;
        case ']':
        case '}':
          if (!flag1)
          {
            sb.AppendLine();
            Enumerable.Range(0, --count).InvokeForAll<int>((Action<int>) (item => sb.Append("    ")));
          }
          sb.Append(ch);
          break;
        default:
          sb.Append(ch);
          break;
      }
    }
    return sb.ToString();
  }
}
