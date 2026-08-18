
// Type: Intermech.Client.Core.ListItemClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core;

/// <summary>Summary description for ListItemClass.</summary>
public class ListItemClass : IComparable<ListItemClass>
{
  private string name = string.Empty;
  private object tag;

  public string Name
  {
    get => this.name;
    set => this.name = value;
  }

  public object Tag
  {
    get => this.tag;
    set => this.tag = value;
  }

  public ListItemClass(string aName, object aTag)
  {
    this.name = aName;
    this.tag = aTag;
  }

  public int CompareTo(ListItemClass obj)
  {
    return string.Compare(this.name, obj.Name, StringComparison.Ordinal);
  }

  public override string ToString() => this.name;
}
