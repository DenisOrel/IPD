// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.NewAttrsDictionary
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Editors;

internal class NewAttrsDictionary : Dictionary<string, List<int>>
{
  internal bool ContainsKey(object key) => key != null && this.ContainsKey(key.ToString());

  internal int KeyPosition(string key)
  {
    int num = -1;
    if (this.ContainsKey((object) key))
    {
      foreach (string key1 in this.Keys)
      {
        ++num;
        string strB = key;
        if (string.Compare(key1, strB) == 0)
          return num;
      }
    }
    return num;
  }

  internal int KeyPosition(object key) => this.KeyPosition(key.ToString());
}
