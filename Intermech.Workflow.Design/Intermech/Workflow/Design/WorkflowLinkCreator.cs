// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.WorkflowLinkCreator
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

#nullable disable
namespace Intermech.Workflow.Design;

internal class WorkflowLinkCreator
{
  public static WorkflowLink Create(ActivityLink l) => WorkflowLinkCreator.Create(l.Kind, l);

  public static WorkflowLink Create(LinkKind kind)
  {
    return WorkflowLinkCreator.Create(kind, (ActivityLink) null);
  }

  private static WorkflowLink Create(LinkKind kind, ActivityLink l)
  {
    return kind == LinkKind.ParallelBlock ? (WorkflowLink) new WorkflowParallelBlock(l) : new WorkflowLink(l);
  }
}
