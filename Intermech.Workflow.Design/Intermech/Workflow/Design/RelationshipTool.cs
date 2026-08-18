// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.RelationshipTool
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
public class RelationshipTool(MapView view) : MapToolLinkingNew(view)
{
  private MapLink myLink;
  private WorkflowNode myPredecessor;

  private void MakeTemporaryLink()
  {
    if (this.myLink != null)
      return;
    MapLink mapLink = new MapLink();
    mapLink.Orthogonal = true;
    MapPort mapPort1 = new MapPort();
    mapPort1.Style = MapPortStyle.Rectangle;
    mapPort1.FromSpot = this.Predecessor.Port.FromSpot;
    mapPort1.Bounds = this.Predecessor.Port.Bounds;
    mapLink.FromPort = (IMapPort) mapPort1;
    MapPort mapPort2 = new MapPort();
    mapPort2.Size = new SizeF(1f, 1f);
    mapPort2.Position = this.LastInput.DocPoint;
    mapPort2.ToSpot = 32 /*0x20*/;
    mapLink.ToPort = (IMapPort) mapPort2;
    this.View.Layers.Default.Add((MapObject) mapLink);
    this.myLink = mapLink;
  }

  public MapPort FindNearestPort(PointF pt, WorkflowNode gn) => gn.Port;

  public WorkflowNode Predecessor
  {
    get => this.myPredecessor;
    set => this.myPredecessor = value;
  }
}
