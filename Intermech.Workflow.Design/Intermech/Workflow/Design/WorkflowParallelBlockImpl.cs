// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.WorkflowParallelBlockImpl
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Map;
using System;
using System.Drawing;

#nullable disable
namespace Intermech.Workflow.Design;

internal class WorkflowParallelBlockImpl : MapLink
{
  private PointF FromPoint;
  private PointF ToPoint;
  private PointF[] points = new PointF[5];

  public WorkflowParallelBlockImpl() => this.Resizable = false;

  public override void CalculateStroke()
  {
    IMapPort fromPort = this.FromPort;
    IMapPort toPort = this.ToPort;
    if (fromPort == null || toPort == null)
      return;
    MapObject mapObject1 = fromPort.MapObject;
    MapObject mapObject2 = toPort.MapObject;
    if (mapObject1 == null || mapObject2 == null)
      return;
    MapPort mapPort1 = mapObject1 as MapPort;
    MapPort mapPort2 = mapObject2 as MapPort;
    this.FromPoint = mapPort1.GetSpotLocation(1);
    this.ToPoint = mapPort2.GetSpotLocation(1);
    float num = 30f + Math.Abs(this.FromPoint.X - this.ToPoint.X) / 3f;
    if ((double) this.FromPoint.Y > (double) this.ToPoint.Y)
    {
      this.FromPoint.Y += num;
      this.ToPoint.Y -= num;
    }
    else
    {
      this.ToPoint.Y += num;
      this.FromPoint.Y -= num;
    }
    float x1 = Math.Min(this.FromPoint.X, this.ToPoint.X);
    float y1 = Math.Min(this.FromPoint.Y, this.ToPoint.Y);
    float x2 = Math.Max(this.FromPoint.X, this.ToPoint.X);
    float y2 = Math.Max(this.FromPoint.Y, this.ToPoint.Y);
    this.points[0] = new PointF(x1, y1);
    this.points[1] = new PointF(x2, y1);
    this.points[2] = new PointF(x2, y2);
    this.points[3] = new PointF(x1, y2);
    this.points[4] = new PointF(x1, y1);
    this.SetPoints(this.points);
  }

  private void DrawPointer(Graphics g, float x, float y, bool ltr)
  {
    int num1 = !ltr ? -1 : 1;
    int num2 = 10;
    g.DrawLine(this.Pen, x, y, x - (float) (num1 * num2), y - (float) num2);
    g.DrawLine(this.Pen, x, y, x - (float) (num1 * num2), y + (float) num2);
    g.DrawLine(this.Pen, x, y, x - (float) (num1 * 10), y);
  }

  public override void Paint(Graphics g, MapView view)
  {
    base.Paint(g, view);
    double num = (double) this.ToPoint.X - (double) this.FromPoint.X;
    bool ltr = num > 0.0;
    if ((double) Math.Abs((float) num) <= 30.0)
      return;
    float x = this.points[0].X + (float) (((double) this.points[1].X - (double) this.points[0].X) / 2.0);
    float y1 = this.points[0].Y;
    this.DrawPointer(g, x, y1, ltr);
    float y2 = this.points[2].Y;
    this.DrawPointer(g, x, y2, ltr);
  }
}
