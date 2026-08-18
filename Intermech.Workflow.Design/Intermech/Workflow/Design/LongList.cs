// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.LongList
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Design;

public class LongList : List<long>
{
  private bool _modified;

  public new void Add(long item)
  {
    base.Add(item);
    this._modified = true;
  }

  public new void RemoveAt(int index)
  {
    base.RemoveAt(index);
    this._modified = true;
  }

  public bool Modified
  {
    get => this._modified;
    set => this._modified = value;
  }
}
