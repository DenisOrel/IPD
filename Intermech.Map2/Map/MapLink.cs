// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapLink
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.ComponentModel;
using System.Drawing;


namespace Intermech.Map
{
    [Serializable]
    public class MapLink : MapStroke, IMapLink, IMapGraphPart, IMapIdentifiablePart
    {
      public const int ChangedLinkUserObject = 1301;
      public const int ChangedFromPort = 1302;
      public const int ChangedToPort = 1303;
      public const int ChangedOrthogonal = 1304;
      public const int ChangedRelinkable = 1305;
      public const int ChangedAbstractLink = 1306;
      public const int ChangedAdjustingStyle = 1310;
      public const int ChangedAvoidsNodes = 1307;
      public const int ChangedLinkUserFlags = 1300;
      public const int ChangedPartID = 1309;
      public const int RelinkableFromHandle = 1024 /*0x0400*/;
      public const int RelinkableToHandle = 1025;
      private const int flagLinkAvoidsNodes = 33554432 /*0x02000000*/;
      private const int flagLinkOrtho = 67108864 /*0x04000000*/;
      private const int flagNoClearPorts = 268435456 /*0x10000000*/;
      private const int flagRelinkable = 134217728 /*0x08000000*/;
      private IMapLink myAbstractLink;
      private MapLinkAdjustingStyle myAdjustingStyle;
      private IMapPort myFromPort;
      private int myPartID;
      private IMapPort myToPort;
      private int myUserFlags;
      private object myUserObject;

      public MapLink()
      {
        this.myFromPort = (IMapPort) null;
        this.myToPort = (IMapPort) null;
        this.myAbstractLink = (IMapLink) null;
        this.myUserFlags = 0;
        this.myUserObject = (object) null;
        this.myPartID = -1;
        this.myAdjustingStyle = MapLinkAdjustingStyle.Calculate;
        this.myAbstractLink = (IMapLink) this;
        this.InternalFlags &= -5;
        this.InternalFlags |= 134217728 /*0x08000000*/;
      }

      protected virtual void AddOrthoPoints(
        PointF startFrom,
        float fromDir,
        PointF endTo,
        float toDir)
      {
        if ((double) fromDir != 0.0 && (double) fromDir != 90.0 && (double) fromDir != 180.0 && (double) fromDir != 270.0 || (double) toDir != 0.0 && (double) toDir != 90.0 && (double) toDir != 180.0 && (double) toDir != 270.0)
          return;
        PointF pointF1 = startFrom;
        PointF pointF2 = endTo;
        float num = this.InternalPenWidth + 1f;
        MapObject mapObject1 = this.FromPort.MapObject;
        IMapNode node1 = this.FromPort.Node;
        RectangleF a1 = node1 == null || node1.MapObject == null ? (mapObject1.Parent == null ? mapObject1.Bounds : mapObject1.Parent.Bounds) : node1.MapObject.Bounds;
        MapObject.InflateRect(ref a1, num, num);
        MapObject mapObject2 = this.ToPort.MapObject;
        IMapNode node2 = this.ToPort.Node;
        RectangleF a2 = node2 == null || node2.MapObject == null ? (mapObject2.Parent == null ? mapObject2.Bounds : mapObject2.Parent.Bounds) : node2.MapObject.Bounds;
        MapObject.InflateRect(ref a2, num, num);
        if (this.AvoidsNodes && this.Document != null)
        {
          MapPositionArray positions = this.Document.GetPositions();
          RectangleF a3 = MapObject.UnionRect(a1, a2);
          MapObject.InflateRect(ref a3, 20f, 20f);
          positions.Propagate(startFrom, fromDir, endTo, toDir, a3);
          int dist = positions.GetDist(endTo.X, endTo.Y);
          if (dist >= int.MaxValue)
          {
            positions.SetAllUnoccupied(int.MaxValue);
            positions.Propagate(startFrom, fromDir, endTo, toDir, positions.Bounds);
            dist = positions.GetDist(endTo.X, endTo.Y);
          }
          if (dist < int.MaxValue && !positions.IsOccupied(endTo.X, endTo.Y))
          {
            this.TraversePositions(positions, endTo.X, endTo.Y, toDir, true);
            PointF point1 = this.GetPoint(2);
            if (this.PointsCount < 4)
            {
              if ((double) fromDir == 0.0 || (double) fromDir == 180.0)
              {
                point1.X = startFrom.X;
                point1.Y = endTo.Y;
              }
              else
              {
                point1.X = endTo.X;
                point1.Y = startFrom.Y;
              }
              this.SetPoint(2, point1);
              this.InsertPoint(3, point1);
              return;
            }
            PointF point2 = this.GetPoint(3);
            if ((double) fromDir == 0.0 || (double) fromDir == 180.0)
            {
              if ((double) point1.X == (double) point2.X)
              {
                float x = (double) fromDir == 0.0 ? Math.Max(point1.X, startFrom.X) : Math.Min(point1.X, startFrom.X);
                this.SetPoint(2, new PointF(x, startFrom.Y));
                this.SetPoint(3, new PointF(x, point2.Y));
                return;
              }
              if ((double) point1.Y == (double) point2.Y)
              {
                float y = startFrom.Y;
                this.SetPoint(2, new PointF(point1.X, y));
                this.SetPoint(3, new PointF(point2.X, y));
                return;
              }
              this.SetPoint(2, new PointF(startFrom.X, point1.Y));
              return;
            }
            if ((double) fromDir != 90.0 && (double) fromDir != 270.0)
              return;
            if ((double) point1.Y == (double) point2.Y)
            {
              float y = (double) fromDir == 90.0 ? Math.Max(point1.Y, startFrom.Y) : Math.Min(point1.Y, startFrom.Y);
              this.SetPoint(2, new PointF(startFrom.X, y));
              this.SetPoint(3, new PointF(point2.X, y));
              return;
            }
            if ((double) point1.X == (double) point2.X)
            {
              float x = startFrom.X;
              this.SetPoint(2, new PointF(x, point1.Y));
              this.SetPoint(3, new PointF(x, point2.Y));
              return;
            }
            this.SetPoint(2, new PointF(point1.X, startFrom.Y));
            return;
          }
        }
        PointF p1;
        PointF p2;
        if ((double) fromDir == 0.0)
        {
          if ((double) pointF2.X > (double) pointF1.X || (double) toDir == 270.0 && (double) pointF2.Y < (double) pointF1.Y && (double) a2.Right > (double) pointF1.X || (double) toDir == 90.0 && (double) pointF2.Y > (double) pointF1.Y && (double) a2.Right > (double) pointF1.X)
          {
            p1 = new PointF(pointF2.X, pointF1.Y);
            p2 = new PointF(pointF2.X, (float) (((double) pointF1.Y + (double) pointF2.Y) / 2.0));
            if ((double) toDir == 180.0)
            {
              p1.X = this.GetMidOrthoPosition(pointF1.X, pointF2.X, false);
              p2.X = p1.X;
              p2.Y = pointF2.Y;
            }
            else if ((double) toDir == 270.0 && (double) pointF2.Y < (double) pointF1.Y || (double) toDir == 90.0 && (double) pointF2.Y >= (double) pointF1.Y)
            {
              p1.X = (double) pointF1.X >= (double) a2.Left ? ((double) pointF1.X >= (double) pointF2.X || (double) pointF1.Y >= (double) a2.Bottom ? a2.Right : this.GetMidOrthoPosition(pointF1.X, pointF2.X, false)) : this.GetMidOrthoPosition(pointF1.X, a2.Left, false);
              p2.X = p1.X;
              p2.Y = pointF2.Y;
            }
            else if ((double) toDir == 0.0 && (double) p1.Y > (double) a2.Top && (double) p1.Y < (double) a2.Bottom)
            {
              p1.X = pointF1.X;
              p1.Y = (double) pointF1.Y >= (double) pointF2.Y ? Math.Max(pointF2.Y, a2.Bottom) : Math.Min(pointF2.Y, a2.Top);
              p2.Y = p1.Y;
            }
          }
          else
          {
            p1 = new PointF(pointF1.X, pointF2.Y);
            p2 = new PointF((float) (((double) pointF1.X + (double) pointF2.X) / 2.0), pointF2.Y);
            if ((double) toDir == 180.0 || (double) toDir == 90.0 && (double) pointF2.Y < (double) a1.Top || (double) toDir == 270.0 && (double) pointF2.Y > (double) a1.Bottom)
            {
              if ((double) pointF2.Y < (double) pointF1.Y && ((double) toDir == 180.0 || (double) toDir == 90.0))
                p1.Y = this.GetMidOrthoPosition(a1.Top, Math.Max(pointF2.Y, a2.Bottom), true);
              else if ((double) pointF2.Y >= (double) pointF1.Y && ((double) toDir == 180.0 || (double) toDir == 270.0))
                p1.Y = this.GetMidOrthoPosition(a1.Bottom, Math.Min(pointF2.Y, a2.Top), true);
              p2.X = pointF2.X;
              p2.Y = p1.Y;
            }
            if ((double) p1.Y > (double) a1.Top && (double) p1.Y < (double) a1.Bottom)
            {
              if ((double) pointF2.X >= (double) a1.Left && (double) pointF2.X <= (double) pointF1.X || (double) pointF1.X <= (double) a2.Right && (double) pointF1.X >= (double) pointF2.X)
              {
                if ((double) toDir == 0.0 || (double) toDir == 180.0)
                {
                  p1 = new PointF(pointF1.X, (float) (((double) pointF1.Y + (double) pointF2.Y) / 2.0));
                  p2 = new PointF(pointF2.X, p1.Y);
                }
                else
                {
                  p1 = new PointF(Math.Max((float) (((double) pointF1.X + (double) pointF2.X) / 2.0), pointF1.X), pointF1.Y);
                  p2 = new PointF(p1.X, pointF2.Y);
                }
              }
              else
              {
                p2.X = pointF2.X;
                p1.Y = (double) toDir == 270.0 || ((double) toDir == 0.0 || (double) toDir == 180.0) && (double) pointF2.Y < (double) pointF1.Y ? Math.Min(pointF2.Y, Math.Min(a1.Top, a2.Top)) : Math.Max(pointF2.Y, Math.Max(a1.Bottom, a2.Bottom));
                p2.Y = p1.Y;
              }
            }
          }
        }
        else if ((double) fromDir == 180.0)
        {
          if ((double) pointF2.X <= (double) pointF1.X || (double) toDir == 270.0 && (double) pointF2.Y < (double) pointF1.Y && (double) a2.Left < (double) pointF1.X || (double) toDir == 90.0 && (double) pointF2.Y > (double) pointF1.Y && (double) a2.Left < (double) pointF1.X)
          {
            p1 = new PointF(pointF2.X, pointF1.Y);
            p2 = new PointF(pointF2.X, (float) (((double) pointF1.Y + (double) pointF2.Y) / 2.0));
            if ((double) toDir == 0.0)
            {
              p1.X = this.GetMidOrthoPosition(pointF1.X, pointF2.X, false);
              p2.X = p1.X;
              p2.Y = pointF2.Y;
            }
            else if ((double) toDir == 270.0 && (double) pointF2.Y < (double) pointF1.Y || (double) toDir == 90.0 && (double) pointF2.Y >= (double) pointF1.Y)
            {
              p1.X = (double) pointF1.X <= (double) a2.Right ? ((double) pointF1.X <= (double) pointF2.X || (double) pointF1.Y >= (double) a2.Bottom ? a2.Left : this.GetMidOrthoPosition(pointF1.X, pointF2.X, false)) : this.GetMidOrthoPosition(pointF1.X, a2.Right, false);
              p2.X = p1.X;
              p2.Y = pointF2.Y;
            }
            else if ((double) toDir == 180.0 && (double) p1.Y > (double) a2.Top && (double) p1.Y < (double) a2.Bottom)
            {
              p1.X = pointF1.X;
              p1.Y = (double) pointF1.Y >= (double) pointF2.Y ? Math.Max(pointF2.Y, a2.Bottom) : Math.Min(pointF2.Y, a2.Top);
              p2.Y = p1.Y;
            }
          }
          else
          {
            p1 = new PointF(pointF1.X, pointF2.Y);
            p2 = new PointF((float) (((double) pointF1.X + (double) pointF2.X) / 2.0), pointF2.Y);
            if ((double) toDir == 0.0 || (double) toDir == 90.0 && (double) pointF2.Y < (double) a1.Top || (double) toDir == 270.0 && (double) pointF2.Y > (double) a1.Bottom)
            {
              if ((double) pointF2.Y < (double) pointF1.Y && ((double) toDir == 0.0 || (double) toDir == 90.0))
                p1.Y = this.GetMidOrthoPosition(a1.Top, Math.Max(pointF2.Y, a2.Bottom), true);
              else if ((double) pointF2.Y >= (double) pointF1.Y && ((double) toDir == 0.0 || (double) toDir == 270.0))
                p1.Y = this.GetMidOrthoPosition(a1.Bottom, Math.Min(pointF2.Y, a2.Top), true);
              p2.X = pointF2.X;
              p2.Y = p1.Y;
            }
            if ((double) p1.Y > (double) a1.Top && (double) p1.Y < (double) a1.Bottom)
            {
              if ((double) pointF2.X >= (double) a1.Left && (double) pointF2.X <= (double) pointF1.X || (double) pointF1.X <= (double) a2.Right && (double) pointF1.X >= (double) pointF2.X)
              {
                if ((double) toDir == 0.0 || (double) toDir == 180.0)
                {
                  p1 = new PointF(pointF1.X, (float) (((double) pointF1.Y + (double) pointF2.Y) / 2.0));
                  p2 = new PointF(pointF2.X, p1.Y);
                }
                else
                {
                  p1 = new PointF(Math.Min((float) (((double) pointF1.X + (double) pointF2.X) / 2.0), pointF1.X), pointF1.Y);
                  p2 = new PointF(p1.X, pointF2.Y);
                }
              }
              else
              {
                p2.X = pointF2.X;
                p1.Y = (double) toDir == 270.0 || ((double) toDir == 0.0 || (double) toDir == 180.0) && (double) pointF2.Y < (double) pointF1.Y ? Math.Min(pointF2.Y, Math.Min(a1.Top, a2.Top)) : Math.Max(pointF2.Y, Math.Max(a1.Bottom, a2.Bottom));
                p2.Y = p1.Y;
              }
            }
          }
        }
        else if ((double) fromDir == 90.0)
        {
          if ((double) pointF2.Y > (double) pointF1.Y || (double) toDir == 180.0 && (double) pointF2.X < (double) pointF1.X && (double) a2.Bottom > (double) pointF1.Y || (double) toDir == 0.0 && (double) pointF2.X > (double) pointF1.X && (double) a2.Bottom > (double) pointF1.Y)
          {
            p1 = new PointF(pointF1.X, pointF2.Y);
            p2 = new PointF((float) (((double) pointF1.X + (double) pointF2.X) / 2.0), pointF2.Y);
            if ((double) toDir == 270.0)
            {
              p1.Y = this.GetMidOrthoPosition(pointF1.Y, pointF2.Y, true);
              p2.X = pointF2.X;
              p2.Y = p1.Y;
            }
            else if ((double) toDir == 180.0 && (double) pointF2.X < (double) pointF1.X || (double) toDir == 0.0 && (double) pointF2.X >= (double) pointF1.X)
            {
              p1.Y = (double) pointF1.Y >= (double) a2.Top ? ((double) pointF1.Y >= (double) pointF2.Y || (double) pointF1.X >= (double) a2.Right ? a2.Bottom : this.GetMidOrthoPosition(pointF1.Y, pointF2.Y, true)) : this.GetMidOrthoPosition(pointF1.Y, a2.Top, true);
              p2.X = pointF2.X;
              p2.Y = p1.Y;
            }
            else if ((double) toDir == 90.0 && (double) p1.X > (double) a2.Left && (double) p1.X < (double) a2.Right)
            {
              p1.X = (double) pointF1.X >= (double) pointF2.X ? Math.Max(pointF2.X, a2.Right) : Math.Min(pointF2.X, a2.Left);
              p1.Y = pointF1.Y;
              p2.X = p1.X;
            }
          }
          else
          {
            p1 = new PointF(pointF2.X, pointF1.Y);
            p2 = new PointF(pointF2.X, (float) (((double) pointF1.Y + (double) pointF2.Y) / 2.0));
            if ((double) toDir == 270.0 || (double) toDir == 0.0 && (double) pointF2.X < (double) a1.Left || (double) toDir == 180.0 && (double) pointF2.X > (double) a1.Right)
            {
              if ((double) pointF2.X < (double) pointF1.X && ((double) toDir == 270.0 || (double) toDir == 0.0))
                p1.X = this.GetMidOrthoPosition(a1.Left, Math.Max(pointF2.X, a2.Right), false);
              else if ((double) pointF2.X >= (double) pointF1.X && ((double) toDir == 270.0 || (double) toDir == 180.0))
                p1.X = this.GetMidOrthoPosition(a1.Right, Math.Min(pointF2.X, a2.Left), false);
              p2.X = p1.X;
              p2.Y = pointF2.Y;
            }
            if ((double) p1.X > (double) a1.Left && (double) p1.X < (double) a1.Right)
            {
              if ((double) pointF2.Y >= (double) a1.Top && (double) pointF2.Y <= (double) pointF1.Y || (double) pointF1.Y <= (double) a2.Bottom && (double) pointF1.Y >= (double) pointF2.Y)
              {
                if ((double) toDir == 0.0 || (double) toDir == 180.0)
                {
                  p1 = new PointF(pointF1.X, Math.Max((float) (((double) pointF1.Y + (double) pointF2.Y) / 2.0), pointF1.Y));
                  p2 = new PointF(pointF2.X, p1.Y);
                }
                else
                {
                  p1 = new PointF((float) (((double) pointF1.X + (double) pointF2.X) / 2.0), pointF1.Y);
                  p2 = new PointF(p1.X, pointF2.Y);
                }
              }
              else
              {
                p1.X = (double) toDir == 180.0 || ((double) toDir == 90.0 || (double) toDir == 270.0) && (double) pointF2.X < (double) pointF1.X ? Math.Min(pointF2.X, Math.Min(a1.Left, a2.Left)) : Math.Max(pointF2.X, Math.Max(a1.Right, a2.Right));
                p2.X = p1.X;
                p2.Y = pointF2.Y;
              }
            }
          }
        }
        else if ((double) pointF2.Y <= (double) pointF1.Y || (double) toDir == 180.0 && (double) pointF2.X < (double) pointF1.X && (double) a2.Top < (double) pointF1.Y || (double) toDir == 0.0 && (double) pointF2.X > (double) pointF1.X && (double) a2.Top < (double) pointF1.Y)
        {
          p1 = new PointF(pointF1.X, pointF2.Y);
          p2 = new PointF((float) (((double) pointF1.X + (double) pointF2.X) / 2.0), pointF2.Y);
          if ((double) toDir == 90.0)
          {
            p1.Y = this.GetMidOrthoPosition(pointF1.Y, pointF2.Y, true);
            p2.X = pointF2.X;
            p2.Y = p1.Y;
          }
          else if ((double) toDir == 180.0 && (double) pointF2.X < (double) pointF1.X || (double) toDir == 0.0 && (double) pointF2.X >= (double) pointF1.X)
          {
            p1.Y = (double) pointF1.Y <= (double) a2.Bottom ? ((double) pointF1.Y <= (double) pointF2.Y || (double) pointF1.X >= (double) a2.Right ? a2.Top : this.GetMidOrthoPosition(pointF1.Y, pointF2.Y, true)) : this.GetMidOrthoPosition(pointF1.Y, a2.Bottom, true);
            p2.X = pointF2.X;
            p2.Y = p1.Y;
          }
          else if ((double) toDir == 270.0 && (double) p1.X > (double) a2.Left && (double) p1.X < (double) a2.Right)
          {
            p1.X = (double) pointF1.X >= (double) pointF2.X ? Math.Max(pointF2.X, a2.Right) : Math.Min(pointF2.X, a2.Left);
            p1.Y = pointF1.Y;
            p2.X = p1.X;
          }
        }
        else
        {
          p1 = new PointF(pointF2.X, pointF1.Y);
          p2 = new PointF(pointF2.X, (float) (((double) pointF1.Y + (double) pointF2.Y) / 2.0));
          if ((double) toDir == 90.0 || (double) toDir == 0.0 && (double) pointF2.X < (double) a1.Left || (double) toDir == 180.0 && (double) pointF2.X > (double) a1.Right)
          {
            if ((double) pointF2.X < (double) pointF1.X && ((double) toDir == 90.0 || (double) toDir == 0.0))
              p1.X = this.GetMidOrthoPosition(a1.Left, Math.Max(pointF2.X, a2.Right), false);
            else if ((double) pointF2.X >= (double) pointF1.X && ((double) toDir == 90.0 || (double) toDir == 180.0))
              p1.X = this.GetMidOrthoPosition(a1.Right, Math.Min(pointF2.X, a2.Left), false);
            p2.X = p1.X;
            p2.Y = pointF2.Y;
          }
          if ((double) p1.X > (double) a1.Left && (double) p1.X < (double) a1.Right)
          {
            if ((double) pointF2.Y >= (double) a1.Top && (double) pointF2.Y <= (double) pointF1.Y || (double) pointF1.Y <= (double) a2.Bottom && (double) pointF1.Y >= (double) pointF2.Y)
            {
              if ((double) toDir == 0.0 || (double) toDir == 180.0)
              {
                p1 = new PointF(pointF1.X, Math.Min((float) (((double) pointF1.Y + (double) pointF2.Y) / 2.0), pointF1.Y));
                p2 = new PointF(pointF2.X, p1.Y);
              }
              else
              {
                p1 = new PointF((float) (((double) pointF1.X + (double) pointF2.X) / 2.0), pointF1.Y);
                p2 = new PointF(p1.X, pointF2.Y);
              }
            }
            else
            {
              p1.X = (double) toDir == 180.0 || ((double) toDir == 90.0 || (double) toDir == 270.0) && (double) pointF2.X < (double) pointF1.X ? Math.Min(pointF2.X, Math.Min(a1.Left, a2.Left)) : Math.Max(pointF2.X, Math.Max(a1.Right, a2.Right));
              p2.X = p1.X;
              p2.Y = pointF2.Y;
            }
          }
        }
        this.AddPoint(p1);
        this.AddPoint(p2);
      }

      public override void AddSelectionHandles(MapSelection sel, MapObject selectedObj)
      {
        if (this.HighlightWhenSelected || !this.CanResize())
        {
          base.AddSelectionHandles(sel, selectedObj);
        }
        else
        {
          sel.RemoveHandles((MapObject) this);
          if (this.PointsCount == 0)
            return;
          int firstPickIndex = this.FirstPickIndex;
          int lastPickIndex = this.LastPickIndex;
          bool flag = this.CanReshape();
          int num = this.Relinkable ? 1 : 0;
          PointF point1 = this.GetPoint(firstPickIndex);
          int handleid1 = num == 0 ? (!flag ? 0 : 8192 /*0x2000*/ + firstPickIndex) : 1024 /*0x0400*/;
          IMapHandle resizeHandle1 = sel.CreateResizeHandle((MapObject) this, selectedObj, point1, handleid1, handleid1 != 0);
          if (handleid1 == 1024 /*0x0400*/ && resizeHandle1.MapObject is MapHandle mapObject1)
          {
            RectangleF bounds = mapObject1.Bounds;
            MapObject.InflateRect(ref bounds, 1f, 1f);
            mapObject1.Bounds = bounds;
            mapObject1.Style = MapHandleStyle.Diamond;
          }
          PointF point2 = this.GetPoint(lastPickIndex);
          int handleid2 = num == 0 ? (!flag ? 0 : 8192 /*0x2000*/ + lastPickIndex) : 1025;
          IMapHandle resizeHandle2 = sel.CreateResizeHandle((MapObject) this, selectedObj, point2, handleid2, handleid2 != 0);
          if (handleid2 == 1025 && resizeHandle2.MapObject is MapHandle mapObject2)
          {
            RectangleF bounds = mapObject2.Bounds;
            MapObject.InflateRect(ref bounds, 1f, 1f);
            mapObject2.Bounds = bounds;
            mapObject2.Style = MapHandleStyle.Diamond;
          }
          for (int i = firstPickIndex + 1; i <= lastPickIndex - 1; ++i)
          {
            PointF point3 = this.GetPoint(i);
            int handleid3 = 8192 /*0x2000*/ + i;
            if (!flag)
              handleid3 = 0;
            else if (this.Orthogonal)
            {
              if (this.PointsCount < 6)
                handleid3 = 0;
              else if (i == firstPickIndex + 1 && this.FromPort != null)
              {
                PointF point4 = this.GetPoint(firstPickIndex);
                if ((double) point4.Y == (double) point3.Y && (double) point4.X != (double) point3.X)
                  handleid3 = 256 /*0x0100*/;
                else if ((double) point4.X == (double) point3.X && (double) point4.Y != (double) point3.Y)
                  handleid3 = 32 /*0x20*/;
                else if ((double) point4.X == (double) point3.X && (double) point4.Y == (double) point3.Y && firstPickIndex + 2 <= lastPickIndex)
                {
                  PointF point5 = this.GetPoint(firstPickIndex + 2);
                  if ((double) point5.Y == (double) point3.Y && (double) point5.X != (double) point3.X)
                    handleid3 = 32 /*0x20*/;
                  else if ((double) point5.X == (double) point3.X && (double) point5.Y != (double) point3.Y)
                    handleid3 = 256 /*0x0100*/;
                }
              }
              else if (i == lastPickIndex - 1 && this.ToPort != null)
              {
                PointF point6 = this.GetPoint(lastPickIndex);
                if ((double) point3.Y == (double) point6.Y && (double) point3.X != (double) point6.X)
                  handleid3 = 64 /*0x40*/;
                else if ((double) point3.X == (double) point6.X && (double) point3.Y != (double) point6.Y)
                  handleid3 = 128 /*0x80*/;
                else if ((double) point6.X == (double) point3.X && (double) point6.Y == (double) point3.Y && lastPickIndex - 2 >= firstPickIndex)
                {
                  PointF point7 = this.GetPoint(lastPickIndex - 2);
                  if ((double) point7.Y == (double) point3.Y && (double) point7.X != (double) point3.X)
                    handleid3 = 128 /*0x80*/;
                  else if ((double) point7.X == (double) point3.X && (double) point7.Y != (double) point3.Y)
                    handleid3 = 64 /*0x40*/;
                }
              }
            }
            sel.CreateResizeHandle((MapObject) this, selectedObj, point3, handleid3, handleid3 != 0);
          }
        }
      }

      protected virtual bool AdjustPoints(
        int startIndex,
        PointF newFromPoint,
        int endIndex,
        PointF newToPoint)
      {
        MapLinkAdjustingStyle linkAdjustingStyle = this.AdjustingStyle;
        if (this.Orthogonal)
        {
          if (linkAdjustingStyle == MapLinkAdjustingStyle.Scale)
            return false;
          if (linkAdjustingStyle == MapLinkAdjustingStyle.Stretch)
            linkAdjustingStyle = MapLinkAdjustingStyle.End;
        }
        switch (linkAdjustingStyle - 1)
        {
          case MapLinkAdjustingStyle.Calculate:
            return this.RescalePoints(startIndex, newFromPoint, endIndex, newToPoint);
          case MapLinkAdjustingStyle.Scale:
            return this.StretchPoints(startIndex, newFromPoint, endIndex, newToPoint);
          case MapLinkAdjustingStyle.Stretch:
            return this.ModifyEndPoints(startIndex, newFromPoint, endIndex, newToPoint);
          default:
            return false;
        }
      }

      private void CalculateBezierNoSpot(MapObject fromObj, MapPort from, MapObject toObj, MapPort to)
      {
        this.ClearPoints();
        PointF result1 = fromObj.Center;
        PointF result2 = toObj.Center;
        if (from == null)
        {
          if (!fromObj.GetNearestIntersectionPoint(result2, result1, out result1))
            result1 = fromObj.Center;
        }
        else
          result1 = from.GetFromLinkPoint(this.AbstractLink);
        if (to == null)
        {
          if (!toObj.GetNearestIntersectionPoint(result1, result2, out result2))
            result2 = toObj.Center;
        }
        else
          result2 = to.GetToLinkPoint(this.AbstractLink);
        float num1 = result2.X - result1.X;
        float num2 = result2.Y - result1.Y;
        float curviness = this.Curviness;
        float num3 = Math.Abs(curviness);
        if ((double) curviness < 0.0)
          num3 = -num3;
        float num4 = 0.0f;
        float num5 = 0.0f;
        float num6 = result1.X + num1 / 3f;
        float num7 = result1.Y + num2 / 3f;
        float x1 = num6;
        float num8 = num7;
        float y1;
        if ((double) Math.Abs(num2) < 1.0)
        {
          y1 = (double) num1 <= 0.0 ? num8 + num3 : num8 - num3;
        }
        else
        {
          num4 = -num1 / num2;
          num5 = (float) Math.Sqrt((double) num3 * (double) num3 / ((double) num4 * (double) num4 + 1.0));
          if ((double) curviness < 0.0)
            num5 = -num5;
          x1 = ((double) num2 < 0.0 ? -1f : 1f) * num5 + num6;
          y1 = num4 * (x1 - num6) + num7;
        }
        float num9 = result1.X + (float) (2.0 * (double) num1 / 3.0);
        float num10 = result1.Y + (float) (2.0 * (double) num2 / 3.0);
        float x2 = num9;
        float num11 = num10;
        float y2;
        if ((double) Math.Abs(num2) < 1.0)
        {
          y2 = (double) num1 <= 0.0 ? num11 + num3 : num11 - num3;
        }
        else
        {
          x2 = ((double) num2 < 0.0 ? -1f : 1f) * num5 + num9;
          y2 = num4 * (x2 - num9) + num10;
        }
        this.AddPoint(result1);
        this.AddPoint(x1, y1);
        this.AddPoint(x2, y2);
        this.AddPoint(result2);
        this.SetPoint(0, from.GetFromLinkPoint(this.AbstractLink));
        this.SetPoint(3, to.GetToLinkPoint(this.AbstractLink));
      }

      private void CalculateLineNoSpot(MapObject fromObj, MapPort from, MapObject toObj, MapPort to)
      {
        this.ClearPoints();
        PointF result1 = fromObj.Center;
        PointF result2 = toObj.Center;
        if (from == null)
        {
          if (!fromObj.GetNearestIntersectionPoint(result2, result1, out result1))
            result1 = fromObj.Center;
        }
        else
          result1 = from.GetFromLinkPoint(this.AbstractLink);
        if (to == null)
        {
          if (!toObj.GetNearestIntersectionPoint(result1, result2, out result2))
            result2 = toObj.Center;
        }
        else
          result2 = to.GetToLinkPoint(this.AbstractLink);
        this.AddPoint(result1);
        this.AddPoint(result2);
      }

      public virtual void CalculateStroke()
      {
        IMapPort fromPort = this.FromPort;
        IMapPort toPort = this.ToPort;
        if (fromPort == null || toPort == null)
          return;
        MapObject mapObject1 = fromPort.MapObject;
        MapObject mapObject2 = toPort.MapObject;
        if (mapObject1 == null || mapObject2 == null)
          return;
        MapPort from = mapObject1 as MapPort;
        MapPort to = mapObject2 as MapPort;
        int pointsCount = this.PointsCount;
        int fromSpot = from != null ? from.FromSpot : 0;
        int toSpot = to != null ? to.ToSpot : 0;
        bool isSelfLoop = this.IsSelfLoop;
        bool orthogonal = this.Orthogonal;
        bool flag1 = this.Style == MapStrokeStyle.Bezier;
        bool flag2 = this.AdjustingStyle == MapLinkAdjustingStyle.Calculate;
        float curviness = this.Curviness;
        bool suspendsUpdates = this.SuspendsUpdates;
        if (!suspendsUpdates)
          this.Changing(1204);
        this.SuspendsUpdates = true;
        if (from == null || to == null || !orthogonal && fromSpot == 0 && toSpot == 0 && !isSelfLoop)
        {
          bool flag3 = false;
          if (!flag2 && pointsCount >= 3)
          {
            PointF result1 = mapObject1.Center;
            PointF result2 = mapObject2.Center;
            if (from == null)
            {
              if (!mapObject1.GetNearestIntersectionPoint(result2, result1, out result1))
                result1 = mapObject1.Center;
            }
            else
              result1 = from.GetFromLinkPoint(this.AbstractLink);
            if (to == null)
            {
              if (!mapObject2.GetNearestIntersectionPoint(result1, result2, out result2))
                result2 = mapObject2.Center;
            }
            else
              result2 = to.GetToLinkPoint(this.AbstractLink);
            flag3 = this.AdjustPoints(0, result1, pointsCount - 1, result2);
          }
          if (!flag3)
          {
            if (flag1)
              this.CalculateBezierNoSpot(mapObject1, from, mapObject2, to);
            else
              this.CalculateLineNoSpot(mapObject1, from, mapObject2, to);
          }
        }
        else
        {
          PointF pointF1 = from.GetFromLinkPoint(this.AbstractLink);
          float num1 = 0.0f;
          float num2 = 0.0f;
          float fromDir = 0.0f;
          if (((orthogonal ? 1 : (fromSpot != 0 ? 1 : 0)) | (isSelfLoop ? 1 : 0)) != 0)
          {
            float endSegmentLength = from.EndSegmentLength;
            fromDir = from.GetFromLinkDir(this.AbstractLink);
            if (isSelfLoop)
            {
              fromDir -= orthogonal ? 90f : 30f;
              if ((double) curviness < 0.0)
                fromDir -= 180f;
              if ((double) fromDir < 0.0)
                fromDir += 360f;
            }
            if (flag1 && pointsCount >= 4)
            {
              endSegmentLength += 15f;
              if (isSelfLoop)
                endSegmentLength += Math.Abs(curviness);
            }
            if ((double) fromDir == 0.0)
              num1 = endSegmentLength;
            else if ((double) fromDir == 90.0)
              num2 = endSegmentLength;
            else if ((double) fromDir == 180.0)
              num1 = -endSegmentLength;
            else if ((double) fromDir == 270.0)
            {
              num2 = -endSegmentLength;
            }
            else
            {
              num1 = endSegmentLength * (float) Math.Cos((double) fromDir * Math.PI / 180.0);
              num2 = endSegmentLength * (float) Math.Sin((double) fromDir * Math.PI / 180.0);
            }
            if (fromSpot == 0 & isSelfLoop)
              pointF1 = from.GetLinkPointFromPoint(new PointF(pointF1.X + num1 * 1000f, pointF1.Y + num2 * 1000f));
          }
          PointF pointF2 = to.GetToLinkPoint(this.AbstractLink);
          float num3 = 0.0f;
          float num4 = 0.0f;
          float toDir = 0.0f;
          if (((orthogonal ? 1 : (toSpot != 0 ? 1 : 0)) | (isSelfLoop ? 1 : 0)) != 0)
          {
            float endSegmentLength = to.EndSegmentLength;
            toDir = to.GetToLinkDir(this.AbstractLink);
            if (isSelfLoop)
            {
              toDir += orthogonal ? 0.0f : 30f;
              if ((double) curviness < 0.0)
                toDir += 180f;
              if ((double) toDir > 360.0)
                toDir -= 360f;
            }
            if (flag1 && pointsCount >= 4)
            {
              endSegmentLength += 15f;
              if (isSelfLoop)
                endSegmentLength += Math.Abs(curviness);
            }
            if ((double) toDir == 0.0)
              num3 = endSegmentLength;
            else if ((double) toDir == 90.0)
              num4 = endSegmentLength;
            else if ((double) toDir == 180.0)
              num3 = -endSegmentLength;
            else if ((double) toDir == 270.0)
            {
              num4 = -endSegmentLength;
            }
            else
            {
              num3 = endSegmentLength * (float) Math.Cos((double) toDir * Math.PI / 180.0);
              num4 = endSegmentLength * (float) Math.Sin((double) toDir * Math.PI / 180.0);
            }
            if (toSpot == 0 & isSelfLoop)
              pointF2 = to.GetLinkPointFromPoint(new PointF(pointF2.X + num3 * 1000f, pointF2.Y + num4 * 1000f));
          }
          PointF pointF3 = pointF1;
          if (((orthogonal ? 1 : (fromSpot != 0 ? 1 : 0)) | (isSelfLoop ? 1 : 0)) != 0)
            pointF3 = new PointF(pointF1.X + num1, pointF1.Y + num2);
          PointF pointF4 = pointF2;
          if (((orthogonal ? 1 : (toSpot != 0 ? 1 : 0)) | (isSelfLoop ? 1 : 0)) != 0)
            pointF4 = new PointF(pointF2.X + num3, pointF2.Y + num4);
          if (!flag2 && !orthogonal && fromSpot == 0 && pointsCount > 3 && this.AdjustPoints(0, pointF1, pointsCount - 2, pointF4))
            this.SetPoint(pointsCount - 1, pointF2);
          else if (!flag2 && !orthogonal && toSpot == 0 && pointsCount > 3 && this.AdjustPoints(1, pointF3, pointsCount - 1, pointF2))
            this.SetPoint(0, pointF1);
          else if (!flag2 && !orthogonal && pointsCount > 4 && this.AdjustPoints(1, pointF3, pointsCount - 2, pointF4))
          {
            this.SetPoint(0, pointF1);
            this.SetPoint(pointsCount - 1, pointF2);
          }
          else if (!flag2 & orthogonal && pointsCount >= 6 && !this.AvoidsNodes && this.AdjustPoints(1, pointF3, pointsCount - 2, pointF4))
          {
            this.SetPoint(0, pointF1);
            this.SetPoint(pointsCount - 1, pointF2);
          }
          else
          {
            this.ClearPoints();
            this.AddPoint(pointF1);
            if (((orthogonal ? 1 : (fromSpot != 0 ? 1 : 0)) | (isSelfLoop ? 1 : 0)) != 0)
              this.AddPoint(pointF3);
            if (orthogonal)
              this.AddOrthoPoints(pointF3, fromDir, pointF4, toDir);
            if (((orthogonal ? 1 : (toSpot != 0 ? 1 : 0)) | (isSelfLoop ? 1 : 0)) != 0)
              this.AddPoint(pointF4);
            this.AddPoint(pointF2);
          }
        }
        this.InvalidBounds = true;
        this.SuspendsUpdates = suspendsUpdates;
        if (suspendsUpdates)
          return;
        RectangleF bounds = this.Bounds;
        this.Changed(1204, 0, (object) null, bounds, 0, (object) null, bounds);
      }

      public override void Changed(
        int subhint,
        int oldI,
        object oldVal,
        RectangleF oldRect,
        int newI,
        object newVal,
        RectangleF newRect)
      {
        if (this.SuspendsUpdates)
          return;
        base.Changed(subhint, oldI, oldVal, oldRect, newI, newVal, newRect);
        if (subhint != 1203 && subhint != 1201 && subhint != 1202 && subhint != 1204 && subhint != 1206 && subhint != 1205)
          return;
        this.AbstractLink.OnPortChanged((IMapPort) null, subhint, oldI, oldVal, oldRect, newI, newVal, newRect);
        this.PortsOnLinkChanged(subhint, oldI, oldVal, oldRect, newI, newVal, newRect);
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1300:
            this.UserFlags = e.GetInt(undo);
            break;
          case 1301:
            this.UserObject = e.GetValue(undo);
            break;
          case 1302:
            this.FromPort = (IMapPort) e.GetValue(undo);
            break;
          case 1303:
            this.ToPort = (IMapPort) e.GetValue(undo);
            break;
          case 1304:
            this.setOrthogonal((bool) e.GetValue(undo), true);
            break;
          case 1305:
            this.Relinkable = (bool) e.GetValue(undo);
            break;
          case 1306:
            this.AbstractLink = (IMapLink) e.GetValue(undo);
            break;
          case 1307:
            this.setAvoidsNodes((bool) e.GetValue(undo), true);
            break;
          case 1309:
            this.PartID = e.GetInt(undo);
            break;
          case 1310:
            this.AdjustingStyle = (MapLinkAdjustingStyle) e.GetInt(undo);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      public override MapObject CopyObject(MapCopyDictionary env)
      {
        MapLink mapLink = (MapLink) base.CopyObject(env);
        if (mapLink != null)
        {
          env.Delayeds.Add((object) this);
          mapLink.myAbstractLink = (IMapLink) env.Copy(this.myAbstractLink.MapObject);
          mapLink.myFromPort = (IMapPort) null;
          mapLink.myToPort = (IMapPort) null;
          mapLink.myPartID = -1;
        }
        return (MapObject) mapLink;
      }

      public override void CopyObjectDelayed(MapCopyDictionary env, MapObject newobj)
      {
        base.CopyObjectDelayed(env, newobj);
        if (!(newobj is MapLink mapLink))
          return;
        IMapPort fromPort = this.FromPort;
        IMapPort toPort = this.ToPort;
        IMapPort mapPort1 = env[(object) fromPort] as IMapPort;
        IMapPort mapPort2 = env[(object) toPort] as IMapPort;
        IMapLink abstractLink = mapLink.AbstractLink;
        if (mapLink.Movable || (fromPort == null || mapPort1 != null) && (toPort == null || mapPort2 != null))
        {
          mapLink.myFromPort = mapPort1;
          mapLink.myToPort = mapPort2;
          mapPort1?.AddDestinationLink(abstractLink);
          mapPort2?.AddSourceLink(abstractLink);
        }
        else
          abstractLink.MapObject.Remove();
      }

      public override void DoResize(
        MapView view,
        RectangleF origRect,
        PointF newPoint,
        int whichHandle,
        MapInputState evttype,
        SizeF min,
        SizeF max)
      {
        if (!this.ResizesRealtime && evttype != MapInputState.Finish && evttype != MapInputState.Cancel)
          return;
        int i1 = this.FirstPickIndex + 1;
        int i2 = this.LastPickIndex - 1;
        switch (whichHandle)
        {
          case 32 /*0x20*/:
            PointF point1 = this.GetPoint(i1 - 1);
            this.SetPoint(i1, new PointF(point1.X, newPoint.Y));
            PointF point2 = this.GetPoint(i1 + 2);
            this.SetPoint(i1 + 1, new PointF(point2.X, newPoint.Y));
            break;
          case 64 /*0x40*/:
            PointF point3 = this.GetPoint(i2 - 2);
            this.SetPoint(i2 - 1, new PointF(newPoint.X, point3.Y));
            point3 = this.GetPoint(i2 + 1);
            this.SetPoint(i2, new PointF(newPoint.X, point3.Y));
            break;
          case 128 /*0x80*/:
            PointF point4 = this.GetPoint(i2 - 2);
            this.SetPoint(i2 - 1, new PointF(point4.X, newPoint.Y));
            PointF point5 = this.GetPoint(i2 + 1);
            this.SetPoint(i2, new PointF(point5.X, newPoint.Y));
            break;
          case 256 /*0x0100*/:
            PointF point6 = this.GetPoint(i1 - 1);
            this.SetPoint(i1, new PointF(newPoint.X, point6.Y));
            PointF point7 = this.GetPoint(i1 + 2);
            this.SetPoint(i1 + 1, new PointF(newPoint.X, point7.Y));
            break;
          default:
            int pointsCount = this.PointsCount;
            if (pointsCount < 2 || whichHandle < 8192 /*0x2000*/)
              break;
            int i3 = whichHandle - 8192 /*0x2000*/;
            PointF point8 = this.GetPoint(i3);
            if (this.Orthogonal)
            {
              PointF point9 = this.GetPoint(i3 - 1);
              PointF point10 = this.GetPoint(i3 + 1);
              if ((double) point9.X == (double) point8.X && (double) point8.Y == (double) point10.Y)
              {
                this.SetPoint(i3 - 1, new PointF(newPoint.X, point9.Y));
                this.SetPoint(i3 + 1, new PointF(point10.X, newPoint.Y));
              }
              else if ((double) point9.Y == (double) point8.Y && (double) point8.X == (double) point10.X)
              {
                this.SetPoint(i3 - 1, new PointF(point9.X, newPoint.Y));
                this.SetPoint(i3 + 1, new PointF(newPoint.X, point10.Y));
              }
              else if ((double) point9.X == (double) point8.X && (double) point8.X == (double) point10.X)
              {
                this.SetPoint(i3 - 1, new PointF(newPoint.X, point9.Y));
                this.SetPoint(i3 + 1, new PointF(newPoint.X, point10.Y));
              }
              else if ((double) point9.Y == (double) point8.Y && (double) point8.Y == (double) point10.Y)
              {
                this.SetPoint(i3 - 1, new PointF(point9.X, newPoint.Y));
                this.SetPoint(i3 + 1, new PointF(point10.X, newPoint.Y));
              }
            }
            this.SetPoint(i3, newPoint);
            if (pointsCount < 3)
              break;
            if (i3 == 1 && this.FromPort != null && this.FromPort.MapObject is MapPort mapObject1)
              this.SetPoint(0, mapObject1.GetFromLinkPoint(this.AbstractLink));
            if (i3 != pointsCount - 2 || this.ToPort == null || !(this.ToPort.MapObject is MapPort mapObject2))
              break;
            this.SetPoint(pointsCount - 1, mapObject2.GetToLinkPoint(this.AbstractLink));
            break;
        }
      }

      protected virtual float GetMidOrthoPosition(float fromPosition, float toPosition, bool vertical)
      {
        return (float) (((double) fromPosition + (double) toPosition) / 2.0);
      }

      public IMapNode GetOtherNode(IMapNode n) => MapLink.GetOtherNode((IMapLink) this, n);

      public static IMapNode GetOtherNode(IMapLink l, IMapNode n)
      {
        if (l.FromPort.Node == n)
          return l.ToPort.Node;
        return l.ToPort.Node == n ? l.FromPort.Node : (IMapNode) null;
      }

      public IMapPort GetOtherPort(IMapPort p) => MapLink.GetOtherPort((IMapLink) this, p);

      public static IMapPort GetOtherPort(IMapLink l, IMapPort p)
      {
        if (l.FromPort == p)
          return l.ToPort;
        return l.ToPort == p ? l.FromPort : (IMapPort) null;
      }

      protected virtual bool ModifyEndPoints(
        int startIndex,
        PointF newFromPoint,
        int endIndex,
        PointF newToPoint)
      {
        if (this.Orthogonal)
        {
          PointF point1 = this.GetPoint(startIndex + 1);
          PointF point2 = this.GetPoint(startIndex + 2);
          if ((double) point1.X == (double) point2.X && (double) point1.Y != (double) point2.Y)
            this.SetPoint(startIndex + 1, new PointF(point1.X, newFromPoint.Y));
          else if ((double) point1.Y == (double) point2.Y)
            this.SetPoint(startIndex + 1, new PointF(newFromPoint.X, point1.Y));
          PointF point3 = this.GetPoint(endIndex - 1);
          point2 = this.GetPoint(endIndex - 2);
          if ((double) point3.X == (double) point2.X && (double) point3.Y != (double) point2.Y)
            this.SetPoint(endIndex - 1, new PointF(point3.X, newToPoint.Y));
          else if ((double) point3.Y == (double) point2.Y)
            this.SetPoint(endIndex - 1, new PointF(newToPoint.X, point3.Y));
        }
        this.SetPoint(startIndex, newFromPoint);
        this.SetPoint(endIndex, newToPoint);
        return true;
      }

      protected override void OnLayerChanged(MapLayer oldlayer, MapLayer newlayer, MapObject mainObj)
      {
        base.OnLayerChanged(oldlayer, newlayer, mainObj);
        if (newlayer == null && !this.NoClearPorts && (mainObj is IMapLink || !this.IsChildOf(mainObj)))
        {
          IMapLink abstractLink = this.AbstractLink;
          this.FromPort?.RemoveLink(abstractLink);
          this.ToPort?.RemoveLink(abstractLink);
        }
        else
        {
          if (newlayer == null)
            return;
          IMapLink abstractLink = this.AbstractLink;
          this.FromPort?.AddDestinationLink(abstractLink);
          this.ToPort?.AddSourceLink(abstractLink);
        }
      }

      public virtual void OnPortChanged(
        IMapPort port,
        int subhint,
        int oldI,
        object oldVal,
        RectangleF oldRect,
        int newI,
        object newVal,
        RectangleF newRect)
      {
        if (port == null)
          return;
        switch (subhint)
        {
          case 1302:
          case 1303:
            if (oldVal != newVal || this.AdjustingStyle == MapLinkAdjustingStyle.Calculate || this.AdjustingStyle == MapLinkAdjustingStyle.Scale && this.Orthogonal)
              this.CalculateStroke();
            this.PortsOnLinkChanged(subhint, oldI, oldVal, oldRect, newI, newVal, newRect);
            break;
          case 1702:
            break;
          case 1703:
            break;
          default:
            if (port.MapObject is MapPort mapObject && mapObject == this.FromPort && this.PointsCount > 0)
            {
              PointF fromLinkPoint = mapObject.GetFromLinkPoint(this.AbstractLink);
              PointF point = this.GetPoint(0);
              if ((double) fromLinkPoint.X >= (double) point.X - 0.5 && (double) fromLinkPoint.X <= (double) point.X + 0.5 && (double) fromLinkPoint.Y >= (double) point.Y - 0.5 && (double) fromLinkPoint.Y <= (double) point.Y + 0.5)
                break;
              this.CalculateStroke();
              break;
            }
            if (mapObject != null && mapObject == this.ToPort && this.PointsCount >= 2)
            {
              PointF toLinkPoint = mapObject.GetToLinkPoint(this.AbstractLink);
              PointF point = this.GetPoint(this.PointsCount - 1);
              if ((double) toLinkPoint.X >= (double) point.X - 0.5 && (double) toLinkPoint.X <= (double) point.X + 0.5 && (double) toLinkPoint.Y >= (double) point.Y - 0.5 && (double) toLinkPoint.Y <= (double) point.Y + 0.5)
                break;
              this.CalculateStroke();
              break;
            }
            this.CalculateStroke();
            break;
        }
      }

      public virtual void PortsOnLinkChanged(
        int subhint,
        int oldI,
        object oldVal,
        RectangleF oldRect,
        int newI,
        object newVal,
        RectangleF newRect)
      {
        if (this.FromPort != null)
          this.FromPort.OnLinkChanged((IMapLink) this, subhint, oldI, oldVal, oldRect, newI, newVal, newRect);
        if (this.ToPort == null)
          return;
        this.ToPort.OnLinkChanged((IMapLink) this, subhint, oldI, oldVal, oldRect, newI, newVal, newRect);
      }

      protected virtual bool RescalePoints(
        int startIndex,
        PointF newFromPoint,
        int endIndex,
        PointF newToPoint)
      {
        PointF point1 = this.GetPoint(startIndex);
        PointF point2 = this.GetPoint(endIndex);
        if (point1 != newFromPoint || point2 != newToPoint)
        {
          double x1 = (double) point1.X;
          double y1 = (double) point1.Y;
          double x2 = (double) point2.X;
          double y2 = (double) point2.Y;
          double num1 = x1;
          double num2 = x2 - num1;
          double num3 = y2 - y1;
          double num4 = Math.Sqrt(num2 * num2 + num3 * num3);
          if (num4 < 1.0)
            num4 = 1.0;
          double num5;
          if (num2 == 0.0)
          {
            num5 = num3 >= 0.0 ? Math.PI / 2.0 : -1.0 * Math.PI / 2.0;
          }
          else
          {
            num5 = Math.Atan(num3 / Math.Abs(num2));
            if (num2 < 0.0)
              num5 = Math.PI - num5;
          }
          double x3 = (double) newFromPoint.X;
          double y3 = (double) newFromPoint.Y;
          double x4 = (double) newToPoint.X;
          double y4 = (double) newToPoint.Y;
          double num6 = x3;
          double num7 = x4 - num6;
          double num8 = y4 - y3;
          double num9 = Math.Sqrt(num7 * num7 + num8 * num8);
          double num10;
          if (num7 == 0.0)
          {
            num10 = num8 >= 0.0 ? Math.PI / 2.0 : -1.0 * Math.PI / 2.0;
          }
          else
          {
            num10 = Math.Atan(num8 / Math.Abs(num7));
            if (num7 < 0.0)
              num10 = Math.PI - num10;
          }
          double num11 = num4;
          double num12 = num9 / num11;
          double num13 = num10 - num5;
          this.SetPoint(startIndex, newFromPoint);
          for (int i = startIndex + 1; i < endIndex; ++i)
          {
            PointF point3 = this.GetPoint(i);
            double num14 = (double) point3.X - x1;
            double num15 = (double) point3.Y - y1;
            double num16 = Math.Sqrt(num14 * num14 + num15 * num15);
            if (num16 < 1.0)
              num16 = 1.0;
            double num17;
            if (num14 == 0.0)
            {
              num17 = num15 >= 0.0 ? Math.PI / 2.0 : -1.0 * Math.PI / 2.0;
            }
            else
            {
              num17 = Math.Atan(num15 / Math.Abs(num14));
              if (num14 < 0.0)
                num17 = Math.PI - num17;
            }
            double num18 = num17 + num13;
            double num19 = num16 * num12;
            double x5 = x3 + num19 * Math.Cos(num18);
            double y5 = y3 + num19 * Math.Sin(num18);
            this.SetPoint(i, new PointF((float) x5, (float) y5));
          }
          this.SetPoint(endIndex, newToPoint);
        }
        return true;
      }

      private void setAvoidsNodes(bool avoid, bool undoing)
      {
        bool oldVal = (this.InternalFlags & 33554432 /*0x02000000*/) != 0;
        if (oldVal == avoid)
          return;
        if (avoid)
          this.InternalFlags |= 33554432 /*0x02000000*/;
        else
          this.InternalFlags &= -33554433;
        this.Changed(1307, 0, (object) oldVal, MapObject.NullRect, 0, (object) avoid, MapObject.NullRect);
        this.PortsOnLinkChanged(1307, 0, (object) oldVal, MapObject.NullRect, 0, (object) avoid, MapObject.NullRect);
        if (!(!undoing & avoid))
          return;
        this.ClearPoints();
        this.CalculateStroke();
      }

      private void setOrthogonal(bool ortho, bool undoing)
      {
        bool oldVal = (this.InternalFlags & 67108864 /*0x04000000*/) != 0;
        if (oldVal == ortho)
          return;
        if (ortho)
          this.InternalFlags |= 67108864 /*0x04000000*/;
        else
          this.InternalFlags &= -67108865;
        this.Changed(1304, 0, (object) oldVal, MapObject.NullRect, 0, (object) ortho, MapObject.NullRect);
        this.PortsOnLinkChanged(1304, 0, (object) oldVal, MapObject.NullRect, 0, (object) ortho, MapObject.NullRect);
        if (!(!undoing & ortho))
          return;
        this.ClearPoints();
        this.CalculateStroke();
      }

      protected virtual bool StretchPoints(
        int startIndex,
        PointF newFromPoint,
        int endIndex,
        PointF newToPoint)
      {
        PointF point1 = this.GetPoint(startIndex);
        PointF point2 = this.GetPoint(endIndex);
        if (point1 != newFromPoint || point2 != newToPoint)
        {
          float x1 = point1.X;
          float y1 = point1.Y;
          float x2 = point2.X;
          float y2 = point2.Y;
          float num1 = (float) (((double) x2 - (double) x1) * ((double) x2 - (double) x1) + ((double) y2 - (double) y1) * ((double) y2 - (double) y1));
          float x3 = newFromPoint.X;
          float y3 = newFromPoint.Y;
          float x4 = newToPoint.X;
          float y4 = newToPoint.Y;
          float num2 = 0.0f;
          float num3 = 1f;
          if ((double) x4 - (double) x3 != 0.0)
            num2 = (float) (((double) y4 - (double) y3) / ((double) x4 - (double) x3));
          if ((double) num2 != 0.0)
            num3 = (float) Math.Sqrt(1.0 + 1.0 / ((double) num2 * (double) num2));
          this.SetPoint(startIndex, newFromPoint);
          for (int i = startIndex + 1; i < endIndex; ++i)
          {
            PointF point3 = this.GetPoint(i);
            float x5 = point3.X;
            float y5 = point3.Y;
            float num4 = 0.5f;
            if ((double) num1 != 0.0)
              num4 = (float) (((double) x1 - (double) x5) * ((double) x1 - (double) x2) + ((double) y1 - (double) y5) * ((double) y1 - (double) y2)) / num1;
            float num5 = x1 + num4 * (x2 - x1);
            float num6 = y1 + num4 * (y2 - y1);
            float num7 = (float) Math.Sqrt(((double) x5 - (double) num5) * ((double) x5 - (double) num5) + ((double) y5 - (double) num6) * ((double) y5 - (double) num6));
            if ((double) y5 < (double) num2 * ((double) x5 - (double) num5) + (double) num6)
              num7 = -num7;
            if ((double) num2 > 0.0)
              num7 = -num7;
            float x6 = x3 + num4 * (x4 - x3);
            float num8 = y3 + num4 * (y4 - y3);
            if ((double) num2 != 0.0)
            {
              float x7 = x6 + num7 / num3;
              float y6 = num8 - (x7 - x6) / num2;
              this.SetPoint(i, new PointF(x7, y6));
            }
            else
              this.SetPoint(i, new PointF(x6, num8 + num7));
          }
          this.SetPoint(endIndex, newToPoint);
        }
        return true;
      }

      private void TraversePositions(
        MapPositionArray positions,
        float px,
        float py,
        float dir,
        bool first)
      {
        SizeF cellSize = positions.CellSize;
        int dist = positions.GetDist(px, py);
        float x1 = px;
        float y1 = py;
        float x2 = x1;
        float y2 = y1;
        if ((double) dir == 0.0)
          x2 += cellSize.Width;
        else if ((double) dir == 90.0)
          y2 += cellSize.Height;
        else if ((double) dir == 180.0)
          x2 -= cellSize.Width;
        else
          y2 -= cellSize.Height;
        for (; dist > 1 && positions.GetDist(x2, y2) == dist - 1; --dist)
        {
          x1 = x2;
          y1 = y2;
          if ((double) dir == 0.0)
            x2 += cellSize.Width;
          else if ((double) dir == 90.0)
            y2 += cellSize.Height;
          else if ((double) dir == 180.0)
            x2 -= cellSize.Width;
          else
            y2 -= cellSize.Height;
        }
        if (first)
        {
          if (dist > 1)
          {
            if ((double) dir == 180.0 || (double) dir == 0.0)
              x1 = (float) (Math.Floor((double) x1 / (double) cellSize.Width) * (double) cellSize.Width + (double) cellSize.Width / 2.0);
            else
              y1 = (float) (Math.Floor((double) y1 / (double) cellSize.Height) * (double) cellSize.Height + (double) cellSize.Height / 2.0);
          }
        }
        else
        {
          x1 = (float) (Math.Floor((double) x1 / (double) cellSize.Width) * (double) cellSize.Width + (double) cellSize.Width / 2.0);
          y1 = (float) (Math.Floor((double) y1 / (double) cellSize.Height) * (double) cellSize.Height + (double) cellSize.Height / 2.0);
        }
        if (dist > 1)
        {
          float dir1 = dir;
          float num1 = x1;
          float num2 = y1;
          if ((double) dir == 0.0)
          {
            dir1 = 90f;
            num2 += cellSize.Height;
          }
          else if ((double) dir == 90.0)
          {
            dir1 = 180f;
            num1 -= cellSize.Width;
          }
          else if ((double) dir == 180.0)
          {
            dir1 = 270f;
            num2 -= cellSize.Height;
          }
          else if ((double) dir == 270.0)
          {
            dir1 = 0.0f;
            num1 += cellSize.Width;
          }
          if (positions.GetDist(num1, num2) == dist - 1)
          {
            this.TraversePositions(positions, num1, num2, dir1, false);
          }
          else
          {
            float num3 = x1;
            float num4 = y1;
            if ((double) dir == 0.0)
            {
              dir1 = 270f;
              num4 -= cellSize.Height;
            }
            else if ((double) dir == 90.0)
            {
              dir1 = 0.0f;
              num3 += cellSize.Width;
            }
            else if ((double) dir == 180.0)
            {
              dir1 = 90f;
              num4 += cellSize.Height;
            }
            else if ((double) dir == 270.0)
            {
              dir1 = 180f;
              num3 -= cellSize.Width;
            }
            if (positions.GetDist(num3, num4) == dist - 1)
              this.TraversePositions(positions, num3, num4, dir1, false);
          }
        }
        this.AddPoint(x1, y1);
      }

      public virtual void Unlink() => this.AbstractLink.MapObject.Remove();

      [Description("The object acting as the IMapLink.")]
      public virtual IMapLink AbstractLink
      {
        get => this.myAbstractLink;
        set
        {
          IMapLink abstractLink = this.myAbstractLink;
          if (abstractLink == value || value == null)
            return;
          IMapPort fromPort = this.FromPort;
          fromPort?.RemoveLink(abstractLink);
          IMapPort toPort = this.ToPort;
          toPort?.RemoveLink(abstractLink);
          this.myAbstractLink = value;
          fromPort?.AddDestinationLink(value);
          toPort?.AddSourceLink(value);
          this.Changed(1306, 0, (object) abstractLink, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("How CalculateStroke behaves.")]
      [Category("Behavior")]
      [DefaultValue(0)]
      public virtual MapLinkAdjustingStyle AdjustingStyle
      {
        get => this.myAdjustingStyle;
        set
        {
          MapLinkAdjustingStyle adjustingStyle = this.myAdjustingStyle;
          if (adjustingStyle == value)
            return;
          this.myAdjustingStyle = value;
          this.Changed(1310, (int) adjustingStyle, (object) null, MapObject.NullRect, (int) value, (object) null, MapObject.NullRect);
        }
      }

      [Description("Whether an Orthogonal link tries to avoid crossing over any nodes.")]
      [DefaultValue(false)]
      [Category("Appearance")]
      public virtual bool AvoidsNodes
      {
        get => (this.InternalFlags & 33554432 /*0x02000000*/) != 0;
        set => this.setAvoidsNodes(value, false);
      }

      public override int FirstPickIndex
      {
        get
        {
          return !(this.FromPort is MapPort fromPort) || this.PointsCount <= 2 || fromPort.FromSpot == 0 && !this.Orthogonal ? 0 : 1;
        }
      }

      [Description("The node that the link is coming from.")]
      public virtual IMapNode FromNode => this.FromPort?.Node;

      [Description("The port that the link is coming from.")]
      [DefaultValue(null)]
      public virtual IMapPort FromPort
      {
        get => this.myFromPort;
        set
        {
          IMapPort fromPort = this.myFromPort;
          if (fromPort == value)
            return;
          IMapLink abstractLink = this.AbstractLink;
          if (fromPort != null && abstractLink.ToPort != fromPort)
            fromPort.RemoveLink(abstractLink);
          this.myFromPort = value;
          value?.AddDestinationLink(abstractLink);
          this.Changed(1302, 0, (object) fromPort, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          abstractLink.OnPortChanged(value, 1302, 0, (object) fromPort, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("Returns itself as a MapObject.")]
      public MapObject MapObject
      {
        get => (MapObject) this;
        set
        {
        }
      }

      public virtual bool IsSelfLoop => this.FromPort == this.ToPort && this.FromPort != null;

      public override int LastPickIndex
      {
        get
        {
          int pointsCount = this.PointsCount;
          if (pointsCount == 0)
            return 0;
          return !(this.ToPort is MapPort toPort) || pointsCount <= 2 || toPort.ToSpot == 0 && !this.Orthogonal ? pointsCount - 1 : pointsCount - 2;
        }
      }

      internal bool NoClearPorts
      {
        get => (this.InternalFlags & 268435456 /*0x10000000*/) != 0;
        set
        {
          if (value)
            this.InternalFlags |= 268435456 /*0x10000000*/;
          else
            this.InternalFlags &= -268435457 /*0xEFFFFFFF*/;
        }
      }

      [Category("Appearance")]
      [DefaultValue(false)]
      [Description("Whether the segments of the link are always horizontal and vertical.")]
      public virtual bool Orthogonal
      {
        get => (this.InternalFlags & 67108864 /*0x04000000*/) != 0;
        set => this.setOrthogonal(value, false);
      }

      [Category("Ownership")]
      [Description("The unique ID of this part in its document.")]
      public int PartID
      {
        get => this.myPartID;
        set
        {
          int partId = this.myPartID;
          if (partId == value)
            return;
          this.myPartID = value;
          this.Changed(1309, partId, (object) null, MapObject.NullRect, value, (object) null, MapObject.NullRect);
        }
      }

      [DefaultValue(true)]
      [Category("Behavior")]
      [Description("Whether the user may reconnect this link to another port.")]
      public virtual bool Relinkable
      {
        get => (this.InternalFlags & 134217728 /*0x08000000*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 134217728 /*0x08000000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 134217728 /*0x08000000*/;
          else
            this.InternalFlags &= -134217729;
          this.Changed(1305, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      public override MapStrokeStyle Style
      {
        get => this.IsSelfLoop && !this.Orthogonal ? MapStrokeStyle.Bezier : base.Style;
        set => base.Style = value;
      }

      [Description("The node that the link is going to.")]
      public virtual IMapNode ToNode => this.ToPort?.Node;

      [DefaultValue(null)]
      [Description("The port that the link is going to.")]
      public virtual IMapPort ToPort
      {
        get => this.myToPort;
        set
        {
          IMapPort toPort = this.myToPort;
          if (toPort == value)
            return;
          IMapLink abstractLink = this.AbstractLink;
          if (toPort != null && abstractLink.FromPort != toPort)
            toPort.RemoveLink(abstractLink);
          this.myToPort = value;
          value?.AddSourceLink(abstractLink);
          this.Changed(1303, 0, (object) toPort, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          abstractLink.OnPortChanged(value, 1303, 0, (object) toPort, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("An integer value associated with this link.")]
      [DefaultValue(0)]
      public virtual int UserFlags
      {
        get => this.myUserFlags;
        set
        {
          int userFlags = this.myUserFlags;
          if (userFlags == value)
            return;
          this.myUserFlags = value;
          this.Changed(1300, userFlags, (object) null, MapObject.NullRect, value, (object) null, MapObject.NullRect);
        }
      }

      [DefaultValue(null)]
      [Description("An object associated with this link.")]
      public virtual object UserObject
      {
        get => this.myUserObject;
        set
        {
          object userObject = this.myUserObject;
          if (userObject == value)
            return;
          this.myUserObject = value;
          this.Changed(1301, 0, userObject, MapObject.NullRect, 0, value, MapObject.NullRect);
        }
      }
    }
}
