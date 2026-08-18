// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.KeyWordsList
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.AVS;

public class KeyWordsList : List<string>
{
  private KeyWordsSchema schema;

  public KeyWordsList()
  {
  }

  public KeyWordsList(KeyWordsSchema schema) => this.schema = schema;

  public KeyWordsList Clone()
  {
    KeyWordsList keyWordsList = new KeyWordsList();
    keyWordsList.AddRange((IEnumerable<string>) this);
    return keyWordsList;
  }

  public KeyWordsList RevertList()
  {
    KeyWordsList keyWordsList = new KeyWordsList();
    keyWordsList.AddRange((IEnumerable<string>) this.OrderByDescending<string, string>((Func<string, string>) (k => k)));
    return keyWordsList;
  }

  public new string this[int index]
  {
    get => base[index];
    set
    {
      if (this.schema == null)
        return;
      string str = base[index];
      base[index] = value;
      if (!this.Contains(value + "~d"))
        return;
      if (this.schema.Parent != null && this.schema.Parent.KeyWords.Contains(value))
        this.RemoveAt(index);
      this.Remove(value + "~d");
    }
  }
}
