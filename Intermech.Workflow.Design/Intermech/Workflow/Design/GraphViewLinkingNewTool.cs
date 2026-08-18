// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.GraphViewLinkingNewTool
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
public class GraphViewLinkingNewTool(MapView v) : MapToolLinkingNew(v)
{
  private MapPort _myLastNearestPort;

  public override IMapPort PickNearestPort(PointF dc)
  {
    IMapPort mapPort = base.PickNearestPort(dc);
    if (this.EndPort != null && this.EndPort.MapObject is MapPort mapObject)
    {
      if (mapPort != null)
      {
        this._myLastNearestPort = mapObject;
        this._myLastNearestPort.Style = MapPortStyle.Rectangle;
        this._myLastNearestPort.Pen = ((GraphView) this.View).PortHighlightPen;
        this._myLastNearestPort.Brush = ((GraphView) this.View).PortHighlightBrush;
      }
      else if (this._myLastNearestPort != null)
      {
        this._myLastNearestPort.Style = MapPortStyle.None;
        this._myLastNearestPort = (MapPort) null;
      }
    }
    return mapPort;
  }

  public override void Stop()
  {
    base.Stop();
    if (this._myLastNearestPort == null)
      return;
    this._myLastNearestPort.Style = MapPortStyle.None;
    this._myLastNearestPort = (MapPort) null;
  }
}
