// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.ActivityLink
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Workflow;

public class ActivityLink
{
  public readonly ActivityNode From;
  public readonly ActivityNode To;
  public readonly long ObjectID;
  public readonly LinkKind Kind;
  protected internal GraphData _graphData;

  public ActivityLink(long objectID, LinkKind kind, ActivityNode from, ActivityNode to)
  {
    this.ObjectID = objectID;
    this.Kind = kind;
    this.From = from;
    this.To = to;
  }

  public GraphData GraphData => this._graphData;
}
