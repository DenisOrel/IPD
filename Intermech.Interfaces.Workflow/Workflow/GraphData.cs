// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.GraphData
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;

#nullable disable
namespace Intermech.Workflow;

public class GraphData
{
  protected StringList _data = new StringList();

  public GraphData(string data) => this._data.CommaText = data;

  public bool Empty => this._data.Count == 0;

  public StringListValues Values => this._data.Values;

  protected int GetIntValue(string name)
  {
    string str = this._data.Values[name];
    return str != null ? Convert.ToInt32(str) : 0;
  }

  protected void SetIntValue(string name, int value) => this._data.Values[name] = value.ToString();

  public override string ToString() => this._data.CommaText;
}
