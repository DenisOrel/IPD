// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.BaseObject
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.GTC.Server.P21;

public abstract class BaseObject : IBaseObject
{
  protected BaseObject()
  {
  }

  protected BaseObject(string keyStr, string paramsStr)
  {
    this.EntityKey = keyStr;
    this.ParamStr = paramsStr;
    this.ParamsArr = this.Split(paramsStr);
  }

  protected string[] ParamsArr { get; private set; }

  protected string ParamStr { get; private set; }

  public abstract void SetParams(IEntityObjects entityObjects);

  public string EntityKey { get; private set; }

  public virtual bool Used { get; set; }

  private string[] Split(string source)
  {
    List<string> source1 = new List<string>();
    bool flag1 = false;
    bool flag2 = false;
    string empty1 = string.Empty;
    for (int index = 0; index < source.Length; ++index)
    {
      char ch = source[index];
      if (!flag1 && !flag2 && ch.Equals('\''))
      {
        flag1 = true;
        empty1 += ch.ToString();
      }
      else if (flag1 && ch.Equals('\''))
      {
        if (index + 3 < source.Length && source[index].Equals('\'') && source[index + 1].Equals('\''))
        {
          empty1 += "''";
          ++index;
        }
        else
        {
          flag1 = false;
          empty1 += ch.ToString();
        }
      }
      else if (!flag2 && !flag1 && ch.Equals('('))
        flag2 = true;
      else if (flag2 && ch.Equals(')'))
        flag2 = false;
      else if (!flag1 && !flag2 && ch.Equals(','))
      {
        source1.Add(empty1.Trim());
        empty1 = string.Empty;
      }
      else
        empty1 += ch.ToString();
    }
    source1.Add(empty1.Trim());
    return source1.Select<string, string>((Func<string, string>) (item =>
    {
      string empty2 = string.Empty;
      string[] strArray = new string[1];
      string str;
      if (item.Length > 2)
      {
        char ch = item[0];
        if (ch.Equals('\''))
        {
          ch = item[item.Length - 1];
          if (ch.Equals('\''))
          {
            str = this.DequoteString(item);
            goto label_5;
          }
        }
      }
      str = item.Equals("$") ? string.Empty : item;
label_5:
      strArray[0] = str;
      return string.Join(empty2, strArray);
    })).ToArray<string>();
  }

  private string DequoteString(string source)
  {
    string str1 = source.Substring(1, source.Length - 2);
    if (str1.Length >= 4)
    {
      while (str1.IndexOf("\\S\\", StringComparison.Ordinal) > 0)
      {
        int index = str1.IndexOf("\\S\\", StringComparison.Ordinal) + 3;
        if (index < str1.Length)
        {
          int num = (int) str1[index];
          string oldValue = "\\S\\" + str1[index].ToString();
          string newValue = ((char) (num + 128 /*0x80*/)).ToString();
          str1 = str1.Replace(oldValue, newValue);
        }
      }
    }
    if (str1.Length >= 5)
    {
      while (str1.IndexOf("\\X\\", StringComparison.Ordinal) > 0)
      {
        int startIndex = str1.IndexOf("\\X\\", StringComparison.Ordinal) + 3;
        if (startIndex + 1 < str1.Length)
        {
          string str2 = str1.Substring(startIndex, 2);
          string oldValue = "\\X\\" + str2;
          string newValue = ((char) Convert.ToInt32(str2, 16 /*0x10*/)).ToString();
          str1 = str1.Replace(oldValue, newValue);
        }
      }
    }
    string sourceString1;
    string newValue1;
    if (str1.Length >= 12)
    {
      for (; str1.IndexOf("\\X2\\", StringComparison.Ordinal) >= 0 && str1.Substring(str1.IndexOf("\\X2\\", StringComparison.Ordinal) + 4, str1.Length - (str1.IndexOf("\\X2\\", StringComparison.Ordinal) + 4)).IndexOf("\\X0\\", StringComparison.Ordinal) >= 4; str1 = str1.Replace($"\\X2\\{sourceString1}\\X0\\", newValue1))
      {
        int startIndex = str1.IndexOf("\\X2\\", StringComparison.Ordinal) + 4;
        int num = str1.Substring(str1.IndexOf("\\X2\\", StringComparison.Ordinal) + 4, str1.Length - (str1.IndexOf("\\X2\\", StringComparison.Ordinal) + 4)).IndexOf("\\X0\\", StringComparison.Ordinal) + startIndex;
        sourceString1 = str1.Substring(startIndex, num - startIndex);
        newValue1 = string.Join(string.Empty, ((IEnumerable<string>) this.SplitInParts(sourceString1, 4).ToArray<string>()).Select<string, int>((Func<string, int>) (x => Convert.ToInt32(x, 16 /*0x10*/))).Select<int, string>(new Func<int, string>(char.ConvertFromUtf32)).ToArray<string>());
      }
    }
    string sourceString2;
    string newValue2;
    if (str1.Length >= 16 /*0x10*/)
    {
      for (; str1.IndexOf("\\X4\\", StringComparison.Ordinal) >= 0 && str1.Substring(str1.IndexOf("\\X4\\", StringComparison.Ordinal) + 4, str1.Length - (str1.IndexOf("\\X4\\", StringComparison.Ordinal) + 4)).IndexOf("\\X0\\", StringComparison.Ordinal) >= 8; str1 = str1.Replace($"\\X4\\{sourceString2}\\X0\\", newValue2))
      {
        int startIndex = str1.IndexOf("\\X4\\", StringComparison.Ordinal) + 4;
        int num = str1.Substring(str1.IndexOf("\\X4\\", StringComparison.Ordinal) + 4, str1.Length - (str1.IndexOf("\\X4\\", StringComparison.Ordinal) + 4)).IndexOf("\\X0\\", StringComparison.Ordinal) + startIndex;
        sourceString2 = str1.Substring(startIndex, num - startIndex);
        newValue2 = string.Join(string.Empty, ((IEnumerable<string>) this.SplitInParts(sourceString2, 8).ToArray<string>()).Select<string, int>((Func<string, int>) (x => Convert.ToInt32(x, 16 /*0x10*/))).Select<int, string>(new Func<int, string>(char.ConvertFromUtf32)).ToArray<string>());
      }
    }
    return str1.Replace("''", "'").Replace("\\\\", "\\").Replace("/IGNORE", string.Empty);
  }

  private IEnumerable<string> SplitInParts(string sourceString, int partLength)
  {
    for (int i = 0; i < sourceString.Length; i += partLength)
      yield return sourceString.Substring(i, Math.Min(partLength, sourceString.Length - i));
  }
}
