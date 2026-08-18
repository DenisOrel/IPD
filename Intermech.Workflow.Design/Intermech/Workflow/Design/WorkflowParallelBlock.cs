// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.WorkflowParallelBlock
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Map;
using System.Drawing;
using System.Drawing.Drawing2D;

#nullable disable
namespace Intermech.Workflow.Design;

public class WorkflowParallelBlock : WorkflowLink
{
  public WorkflowParallelBlock()
  {
  }

  public WorkflowParallelBlock(ActivityLink l)
    : base(l)
  {
  }

  public override MapLink CreateRealLink() => (MapLink) new WorkflowParallelBlockImpl();

  protected override void InitStyles()
  {
    this.ToArrow = false;
    this.Brush = Brushes.Green;
    this.Pen = new Pen(Color.Green, 1f)
    {
      DashStyle = DashStyle.Dash,
      DashPattern = new float[2]{ 10f, 5f }
    };
  }

  protected override void LinkKindChanged()
  {
  }
}
