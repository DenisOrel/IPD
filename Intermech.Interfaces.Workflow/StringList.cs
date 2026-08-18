// Decompiled with JetBrains decompiler
// Type: Intermech.StringList
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech;

[Serializable]
public class StringList : List<string>
{
  public char QuoteChar = '"';
  public char Delimiter = ',';
  private StringListValues _values;
  private static StringList _tempSL = new StringList();

  public StringList()
  {
  }

  public StringList(StringList prototype) => this.AddRange((IEnumerable<string>) prototype);

  public string Text
  {
    get => string.Join("\r\n", this.ToArray());
    set
    {
      this.Clear();
      value = value.Replace("\r\n", "\n");
      string[] array = value.Split(new string[2]
      {
        "\r",
        "\n"
      }, StringSplitOptions.None);
      if (array[array.Length - 1] == "")
        Array.Resize<string>(ref array, array.Length - 1);
      this.AddRange((IEnumerable<string>) array);
    }
  }

  public string CommaText
  {
    get
    {
      string commaText = "";
      if (this.Count == 1 && this[0] == "")
      {
        commaText = this.QuoteChar.ToString() + this.QuoteChar.ToString();
      }
      else
      {
        foreach (string str1 in (List<string>) this)
        {
          string str2 = str1.IndexOfAny(new char[3]
          {
            ' ',
            this.QuoteChar,
            this.Delimiter
          }) == -1 ? str1 : $"\"{str1.Replace("\"", "\"\"")}\"";
          if (commaText != "")
            commaText += ",";
          commaText += str2;
        }
      }
      return commaText;
    }
    set
    {
      this.Clear();
      bool flag = false;
      StringBuilder stringBuilder = new StringBuilder(value);
      for (int index = 0; index < stringBuilder.Length; ++index)
      {
        if ((int) stringBuilder[index] == (int) this.QuoteChar)
          flag = !flag;
        else if (!flag && value[index] == ',')
          stringBuilder[index] = '\u0001';
      }
      stringBuilder.Replace('\u0001'.ToString(), "\r\n");
      string str1 = stringBuilder.ToString();
      string[] separator = new string[1]{ "\r\n" };
      foreach (string str2 in str1.Split(separator, StringSplitOptions.None))
      {
        if (str2.Length > 0 && str2[0] == '"')
          str2 = str2.Remove(0, 1);
        if (str2.Length > 0 && str2[str2.Length - 1] == '"')
          str2 = str2.Remove(str2.Length - 1, 1);
        this.Add(str2.Replace("\"\"", "\""));
      }
    }
  }

  public StringListValues Values
  {
    get
    {
      if (this._values == null)
        this._values = new StringListValues(this);
      return this._values;
    }
  }

  public override bool Equals(object obj)
  {
    return obj is StringList stringList && this.Count == stringList.Count && this.Text == stringList.Text;
  }

  public override int GetHashCode() => base.GetHashCode();

  public void SaveToFile(string fn)
  {
    StreamWriter streamWriter = new StreamWriter(fn, false);
    try
    {
      streamWriter.Write(this.Text);
    }
    finally
    {
      streamWriter.Close();
    }
  }

  public static string StringToCommaText(string s)
  {
    StringList._tempSL.Text = s;
    return StringList._tempSL.CommaText;
  }

  public static string CommaTextToString(string s)
  {
    StringList._tempSL.CommaText = s;
    return StringList._tempSL.Text;
  }

  public static string ObjectArrayToCommaText(object[] array)
  {
    StringList stringList = new StringList();
    foreach (object obj in array)
    {
      string str = obj == null || DBNull.Value.Equals(obj) ? "@null@" : StringList.StringToCommaText(Convert.ToString(obj, (IFormatProvider) CultureInfo.InvariantCulture));
      stringList.Add(str);
    }
    return stringList.CommaText;
  }

  public static object[] CommaTextToObjectArray(string s)
  {
    StringList stringList = new StringList();
    stringList.CommaText = s;
    List<object> objectList = new List<object>();
    foreach (string s1 in (List<string>) stringList)
    {
      if (s1 == "@null@")
        objectList.Add((object) DBNull.Value);
      else
        objectList.Add((object) StringList.CommaTextToString(s1));
    }
    return objectList.ToArray();
  }
}
