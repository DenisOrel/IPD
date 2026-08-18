// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.WFLinkingNewTool
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Map;
using System;
using System.Drawing;

#nullable disable
namespace Intermech.Workflow.Design;

[Serializable]
public class WFLinkingNewTool : MapToolLinkingNew
{
  private WorkflowNode _startNode;
  private WorkflowNode _lastBKNode;

  public WFLinkingNewTool(MapView v, WorkflowNode startNode)
    : base(v)
  {
    this._startNode = startNode;
  }

  public GraphView View => (GraphView) base.View;

  public override void StartNewLink(IMapPort port, PointF dc)
  {
    base.StartNewLink(this.View.CurrentLinkIsBackward ? (IMapPort) this._startNode.BackwardPort : (IMapPort) this._startNode.Port, dc);
  }

  public override IMapPort PickNearestPort(PointF dc)
  {
    IMapPort mapPort = base.PickNearestPort(dc);
    if (this._lastBKNode != null && (mapPort == null || !object.Equals((object) mapPort.Node, (object) this._lastBKNode)))
    {
      this._lastBKNode.AlignSpots();
      this._lastBKNode = (WorkflowNode) null;
    }
    if (mapPort != null)
    {
      if (object.Equals((object) mapPort.Node, (object) this._startNode))
        mapPort = (IMapPort) null;
      else if (mapPort.Node is WorkflowNode node)
      {
        if (node.View.IsLinked(this._startNode, node))
          return (IMapPort) null;
        this._lastBKNode = (WorkflowNode) null;
        if (this.View.CurrentLinkIsBackward)
        {
          mapPort = (IMapPort) node.BackwardPort;
          this._lastBKNode = node;
        }
        else
          mapPort = (IMapPort) ((MapIconicNode) mapPort.Node).Port;
      }
    }
    return mapPort;
  }

  public override void DoNewLink(IMapPort fromPort, IMapPort toPort)
  {
    base.DoNewLink((IMapPort) this._startNode.Port, toPort);
  }

  protected override IMapLink CreateTemporaryLink(IMapPort fromPort, IMapPort toPort)
  {
    IMapLink temporaryLink = base.CreateTemporaryLink(fromPort, toPort);
    if (!(temporaryLink is WorkflowLink workflowLink) || !this.View.CurrentLinkIsBackward)
      return temporaryLink;
    workflowLink.Backward = true;
    return temporaryLink;
  }

  public override void DoMouseDown()
  {
    if (this.View.PickObject(true, false, this.LastInput.DocPoint, true) is WorkflowNode b && b != this._startNode && !b.View.IsLinked(this._startNode, b))
    {
      MapPort fromPort = this._startNode.Port;
      MapPort toPort;
      if (b.View.CurrentLinkKind == LinkKind.Backward)
      {
        fromPort = (MapPort) this._startNode.BackwardPort;
        toPort = (MapPort) b.BackwardPort;
      }
      else
        toPort = b.Port;
      IMapLink link = this.View.CreateLink((IMapPort) fromPort, (IMapPort) toPort);
      if (link != null)
      {
        this.TransactionResult = "New Relationship";
        this.View.RaiseLinkCreated(link.MapObject);
        this.View.Selection.Select(link.MapObject);
      }
      this.StopTool();
    }
    else
      this.DoCancelMouse();
  }
}
