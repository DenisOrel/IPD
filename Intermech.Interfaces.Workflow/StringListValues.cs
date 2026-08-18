// Decompiled with JetBrains decompiler
// Type: Intermech.StringListValues
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech;

[Serializable]
public class StringListValues
{
  private StringList _owner;

  public StringListValues(StringList owner) => this._owner = owner;

  public string this[string key]
  {
    get
    {
      foreach (string str in (List<string>) this._owner)
      {
        if (str.StartsWith(key + "="))
        {
          int startIndex = key.Length + 1;
          return str.Substring(startIndex);
        }
      }
      return (string) null;
    }
    set
    {
      int count = this._owner.Count;
      for (int index = 0; index < count; ++index)
      {
        if (this._owner[index].StartsWith(key + "="))
        {
          this._owner[index] = $"{key}={value}";
          break;
        }
      }
      this._owner.Add($"{key}={value}");
    }
  }

  public void Remove(string key)
  {
    for (int index = this._owner.Count - 1; index >= 0; --index)
    {
      if (this._owner[index].StartsWith(key + "="))
      {
        this._owner.RemoveAt(index);
        break;
      }
    }
  }

  public bool TryGetValue(string Name, ref long Value)
  {
    string s = this[Name];
    return s != null && long.TryParse(s, out Value);
  }

  public bool TryGetValue(string Name, ref bool Value)
  {
    long num = 0;
    if (!this.TryGetValue(Name, ref num))
      return false;
    Value = num != 0L;
    return true;
  }
}
