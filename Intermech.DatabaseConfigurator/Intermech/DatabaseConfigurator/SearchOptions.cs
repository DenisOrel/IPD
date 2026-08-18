// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.SearchOptions
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.DatabaseConfigurator;

public class SearchOptions
{
  private string text = string.Empty;
  private bool caseSensitive;
  private bool thisNodeOnly = true;
  private List<string> history = new List<string>();

  public string Text
  {
    get => this.text;
    set => this.text = value;
  }

  public bool CaseSensitive
  {
    get => this.caseSensitive;
    set => this.caseSensitive = value;
  }

  public bool ThisNodeOnly
  {
    get => this.thisNodeOnly;
    set => this.thisNodeOnly = value;
  }

  public List<string> History => this.history;

  public SearchOptions()
  {
  }

  public SearchOptions(string aText, bool aCaseSensitive, bool aThisNodeOnly)
  {
    this.text = aText;
    this.caseSensitive = aCaseSensitive;
    this.thisNodeOnly = aThisNodeOnly;
  }
}
