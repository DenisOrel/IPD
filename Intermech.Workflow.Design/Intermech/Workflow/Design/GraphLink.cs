// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.GraphLink
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Map;
using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>A GraphLink is a link whose label is a random number.</summary>
[Serializable]
public class GraphLink : MapLabeledLink
{
  private static Random myRandom = new Random();

  public GraphLink()
  {
    MapText mapText = new MapText();
    mapText.Alignment = 1;
    mapText.Selectable = false;
    mapText.Text = GraphLink.myRandom.Next(100).ToString();
    this.MidLabel = (MapObject) mapText;
  }

  /// <summary>Bring up a GraphLink specific context menu.</summary>
  /// <param name="evt"></param>
  /// <param name="view"></param>
  /// <returns></returns>
  public override bool OnContextClick(MapInputEventArgs evt, MapView view)
  {
    MapContextMenu mapContextMenu = new MapContextMenu(view);
    if (this.CanDelete())
      mapContextMenu.MenuItems.Add(new MenuItem("Delete", new EventHandler(this.Delete_Command)));
    if (mapContextMenu.MenuItems.Count > 0)
      mapContextMenu.MenuItems.Add(new MenuItem("-"));
    mapContextMenu.MenuItems.Add(new MenuItem("Insert Point", new EventHandler(this.InsertPoint_Command)));
    if (this.CanRemovePoint())
      mapContextMenu.MenuItems.Add(new MenuItem("Remove Segment", new EventHandler(this.RemoveSegment_Command)));
    mapContextMenu.MenuItems.Add(new MenuItem("-"));
    mapContextMenu.MenuItems.Add(new MenuItem("Properties", new EventHandler(this.Properties_Command)));
    mapContextMenu.Show((Control) view, evt.ViewPoint);
    return true;
  }

  public void Delete_Command(object sender, EventArgs e)
  {
    MapContextMenu.FindView(sender as MenuItem)?.EditDelete();
  }

  public void InsertPoint_Command(object sender, EventArgs e)
  {
    MapView view = MapContextMenu.FindView(sender as MenuItem);
    if (view == null)
      return;
    PointF docPoint = view.LastInput.DocPoint;
    MapStroke realLink = (MapStroke) this.RealLink;
    int i = realLink.GetSegmentNearPoint(docPoint);
    if (realLink.PointsCount > 3)
    {
      if (i < 1)
        i = 1;
      else if (i >= realLink.PointsCount - 2)
        i = realLink.PointsCount - 3;
    }
    PointF result;
    if (!MapStroke.NearestPointOnLine(realLink.GetPoint(i), realLink.GetPoint(i + 1), docPoint, out result))
      return;
    view.StartTransaction();
    realLink.InsertPoint(i + 1, result);
    if (this.Orthogonal)
      realLink.InsertPoint(i + 1, result);
    realLink.AddSelectionHandles(view.Selection, (MapObject) this);
    view.FinishTransaction("inserted point into link stroke");
  }

  public bool CanRemovePoint() => this.RealLink.PointsCount > (this.Orthogonal ? 6 : 2);

  public void RemoveSegment_Command(object sender, EventArgs e)
  {
    MapView view = MapContextMenu.FindView(sender as MenuItem);
    if (view == null)
      return;
    PointF docPoint = view.LastInput.DocPoint;
    MapStroke realLink = (MapStroke) this.RealLink;
    int segmentNearPoint = realLink.GetSegmentNearPoint(docPoint);
    view.StartTransaction();
    if (this.Orthogonal)
    {
      int i = Math.Min(Math.Max(segmentNearPoint, 2), this.RealLink.PointsCount - 5);
      PointF point1 = realLink.GetPoint(i);
      PointF point2 = realLink.GetPoint(i + 1);
      realLink.RemovePoint(i);
      realLink.RemovePoint(i);
      PointF point3 = realLink.GetPoint(i);
      if ((double) point1.X == (double) point2.X)
        point3.Y = point1.Y;
      else
        point3.X = point1.X;
      realLink.SetPoint(i, point3);
    }
    else
    {
      int i = Math.Min(Math.Max(segmentNearPoint, 1), this.RealLink.PointsCount - 2);
      realLink.RemovePoint(i);
    }
    realLink.AddSelectionHandles(view.Selection, (MapObject) this);
    view.FinishTransaction("removed point from link stroke");
  }

  public void Properties_Command(object sender, EventArgs e)
  {
    MapContextMenu.FindView(sender as MenuItem);
  }
}
