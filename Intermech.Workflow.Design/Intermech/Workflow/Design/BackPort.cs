// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.BackPort
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
public class BackPort : MapPort
{
  private int _lastDir;
  public bool _updating;
  public double weight;
  private string tooltip = "";

  public BackPort() => this.EndSegmentLength = 50f;

  public override string GetToolTip(MapView view)
  {
    return $"{this.weight.ToString()}, POINTS: {this.tooltip} , LC={this.LinksCount.ToString()}";
  }

  public void UpdateSpot(WorkflowLink l)
  {
    this._lastDir = 0;
    if (!l.Orthogonal)
      return;
    MapNode node = this.Node as MapNode;
    MapNode mapNode = l.FromNode != node ? l.FromNode as MapNode : l.ToNode as MapNode;
    if (node == null || mapNode == null)
      return;
    float num1 = node.Top + node.Height / 2f;
    double left = (double) node.Left;
    double num2 = (double) node.Width / 2.0;
    PointF pointF1 = new PointF();
    PointF pointF2 = new PointF();
    int num3 = 1;
    if (l.RealLink.PointsCount <= 5)
      return;
    PointF pointF3;
    PointF point1;
    int num4;
    int num5;
    if (l.FromNode == node)
    {
      pointF3 = l.RealLink.GetPoint(2);
      point1 = l.RealLink.GetPoint(4);
      num4 = 2;
      num5 = 4;
    }
    else
    {
      pointF3 = l.RealLink.GetPoint(4);
      point1 = l.RealLink.GetPoint(2);
      num4 = 4;
      num5 = 2;
      num3 = -1;
    }
    this.tooltip = $"({pointF3.X} - {pointF3.Y}) ";
    this.tooltip += $"({point1.X} - {point1.Y})";
    int num6 = 1;
    if ((double) node.Left > (double) mapNode.Left)
      num6 = -1;
    PointF pointF4 = new PointF();
    for (int i = num4; (num4 > num5 ? (i >= num5 ? 1 : 0) : (i <= num5 ? 1 : 0)) != 0; i += num3)
    {
      PointF point2 = l.RealLink.GetPoint(i);
      if ((double) pointF4.X > 0.0 && (double) pointF4.Y == (double) point2.Y)
      {
        pointF3 = point2;
        break;
      }
      pointF4 = point2;
    }
    int spot;
    if ((double) pointF3.Y > (double) node.Top - (double) l.RealLink.ToArrowShaftLength && (double) pointF3.Y < (double) node.Top + (double) node.Height + (double) l.RealLink.ToArrowShaftLength)
    {
      if ((double) mapNode.Left < (double) node.Left)
      {
        this._lastDir = 180;
        spot = 256 /*0x0100*/;
      }
      else
      {
        this._lastDir = 360;
        spot = 64 /*0x40*/;
      }
    }
    else if ((double) pointF3.Y > (double) node.Top)
    {
      this._lastDir = 90;
      spot = 128 /*0x80*/;
    }
    else
    {
      this._lastDir = 270;
      spot = 32 /*0x20*/;
    }
    switch (spot)
    {
      case 0:
        return;
      case 32 /*0x20*/:
      case 128 /*0x80*/:
        this.weight = (double) pointF3.Y <= (double) num1 ? (double) pointF3.Y : 1000000.0 - (double) pointF3.Y;
        this.weight = (double) num6 * this.weight;
        break;
      default:
        this.weight = (double) pointF3.X;
        break;
    }
    this._updating = true;
    try
    {
      this.FromSpot = spot;
      this.ToSpot = spot;
      PointF spotLocation = node.GetSpotLocation(spot);
      if (spotLocation.Equals((object) this.GetSpotLocation(spot)))
        return;
      this.SetSpotLocation(spot, spotLocation);
    }
    finally
    {
      this._updating = false;
    }
  }

  public override PointF GetToLinkPoint(IMapLink link) => base.GetToLinkPoint(link);

  public override PointF GetFromLinkPoint(IMapLink link) => base.GetFromLinkPoint(link);

  public override float GetFromLinkDir(IMapLink link)
  {
    return this._lastDir != 0 ? (float) this._lastDir : base.GetFromLinkDir(link);
  }

  public override float GetToLinkDir(IMapLink link)
  {
    return this._lastDir != 0 ? (float) this._lastDir : base.GetToLinkDir(link);
  }

  public override void OnLinkChanged(
    IMapLink l,
    int subhint,
    int oldI,
    object oldVal,
    RectangleF oldRect,
    int newI,
    object newVal,
    RectangleF newRect)
  {
    base.OnLinkChanged(l, subhint, oldI, oldVal, oldRect, newI, newVal, newRect);
    if (subhint != 1709 && subhint != 1710)
      return;
    (this.Node as WorkflowNode).AlignSpots();
  }
}
