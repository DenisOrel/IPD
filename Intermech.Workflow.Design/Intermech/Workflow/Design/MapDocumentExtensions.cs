// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.MapDocumentExtensions
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Map;

#nullable disable
namespace Intermech.Workflow.Design;

public static class MapDocumentExtensions
{
  public static WorkflowNode FindNode(this MapDocument doc, ActivityKind kind)
  {
    foreach (MapObject mapObject in doc)
    {
      if (mapObject is WorkflowNode node && node.ActivityKind == kind)
        return node;
    }
    return (WorkflowNode) null;
  }
}
