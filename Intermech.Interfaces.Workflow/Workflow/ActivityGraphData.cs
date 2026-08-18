// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.ActivityGraphData
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Workflow;

public class ActivityGraphData(string data) : GraphData(data)
{
  public int X
  {
    get => this.GetIntValue(nameof (X));
    set => this.SetIntValue(nameof (X), value);
  }

  public int Y
  {
    get => this.GetIntValue(nameof (Y));
    set => this.SetIntValue(nameof (Y), value);
  }
}
