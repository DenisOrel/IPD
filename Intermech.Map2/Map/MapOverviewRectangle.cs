// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapOverviewRectangle
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Drawing;


namespace Intermech.Map
{
    [Serializable]
    public class MapOverviewRectangle : MapRectangle
    {
      [NonSerialized]
      private bool myChanging;

      public MapOverviewRectangle()
      {
        this.myChanging = false;
        this.Selectable = false;
        this.Resizable = false;
        this.Pen = new Pen(Color.DarkCyan, 0.0f);
      }

      public override void Paint(Graphics g, MapView view)
      {
        MapView observedView = this.ObservedView;
        base.Paint(g, view);
      }

      public override void AddSelectionHandles(MapSelection sel, MapObject selectedObj)
      {
      }

      public override PointF ComputeMove(PointF origLoc, PointF newLoc)
      {
        if (this.ObservedView != null)
        {
          PointF documentTopLeft = this.ObservedView.DocumentTopLeft;
          SizeF documentSize = this.ObservedView.DocumentSize;
          if ((double) newLoc.X + (double) this.Width > (double) documentTopLeft.X + (double) documentSize.Width)
            newLoc.X = documentTopLeft.X + documentSize.Width - this.Width;
          if ((double) newLoc.X < (double) documentTopLeft.X)
            newLoc.X = documentTopLeft.X;
          if ((double) newLoc.Y + (double) this.Height > (double) documentTopLeft.Y + (double) documentSize.Height)
            newLoc.Y = documentTopLeft.Y + documentSize.Height - this.Height;
          if ((double) newLoc.Y < (double) documentTopLeft.Y)
            newLoc.Y = documentTopLeft.Y;
          if (this.ObservedView.ShowsNegativeCoordinates)
            return newLoc;
          if ((double) newLoc.X < 0.0)
            newLoc.X = 0.0f;
          if ((double) newLoc.Y < 0.0)
            newLoc.Y = 0.0f;
        }
        return newLoc;
      }

      public override bool ContainsPoint(PointF p)
      {
        RectangleF bounds = this.Bounds;
        float num = 4f / this.View.DocScale;
        MapObject.InflateRect(ref bounds, num, num);
        if (!MapObject.ContainsRect(bounds, p))
          return false;
        MapObject.InflateRect(ref bounds, -2f * num, -2f * num);
        return !MapObject.ContainsRect(bounds, p);
      }

      protected override void OnBoundsChanged(RectangleF old)
      {
        base.OnBoundsChanged(old);
        if (this.ObservedView == null || this.myChanging)
          return;
        this.myChanging = true;
        this.ObservedView.DocPosition = this.Position;
        this.myChanging = false;
      }

      public override void OnGotSelection(MapSelection sel)
      {
      }

      public void UpdateRectFromView()
      {
        if (this.ObservedView == null || this.myChanging)
          return;
        this.myChanging = true;
        this.Bounds = this.ObservedView.DocExtent;
        if (this.View != null)
          this.View.ScrollRectangleToVisible(this.Bounds);
        this.myChanging = false;
      }

      public MapView ObservedView => this.View is MapOverview view ? view.Observed : (MapView) null;
    }
}
