// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.GraphInfo
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

#nullable disable
namespace Intermech.Workflow.Design;

public class GraphInfo
{
  public string GraphVal;
  public StrongSignMode StrongMode;

  public GraphInfo(string graphVal, StrongSignMode strongMode)
  {
    this.GraphVal = graphVal;
    this.StrongMode = strongMode;
  }

  public bool StrongSign
  {
    get => this.StrongMode != StrongSignMode.No;
    set
    {
      if (value)
        this.StrongMode = StrongSignMode.Yes;
      else
        this.StrongMode = StrongSignMode.No;
    }
  }
}
