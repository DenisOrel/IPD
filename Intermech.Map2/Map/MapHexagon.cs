// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapHexagon
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Map
{
    [Serializable]
    public class MapHexagon : MapShape
    {
      public const int ChangedDistanceBottom = 1445;
      public const int ChangedDistanceLeft = 1442;
      public const int ChangedDistanceRight = 1443;
      public const int ChangedDistanceTop = 1444;
      public const int ChangedKeepsCrosswiseSymmetry = 1450;
      public const int ChangedKeepsLengthwiseSymmetry = 1449;
      public const int ChangedOrientation = 1446;
      public const int ChangedReshapableCorner = 1448;
      public const int ChangedReshapeBehavior = 1447;
      private const int flagCrosswiseSymmetry = 2097152 /*0x200000*/;
      private const int flagLengthwiseSymmetry = 4194304 /*0x400000*/;
      private const int flagReshapableCorner = 1048576 /*0x100000*/;
      public const int LeftTopPointHandleID = 1028;
      public const int LeftTopSideHandleID = 1026;
      private float myDistanceBottom;
      private float myDistanceLeft;
      private float myDistanceRight;
      private float myDistanceTop;
      private Orientation myOrientation;
      private PointF[] myPoints;
      private MapHexagonReshapeBehavior myReshapeBehavior;
      public const int RightBottomPointHandleID = 1029;
      public const int RightBottomSideHandleID = 1027;

      public MapHexagon()
      {
        this.myPoints = new PointF[6];
        this.myDistanceLeft = 10f;
        this.myDistanceRight = 10f;
        this.myDistanceTop = 10f;
        this.myDistanceBottom = 10f;
        this.myOrientation = Orientation.Horizontal;
        this.myReshapeBehavior = MapHexagonReshapeBehavior.CompleteSymmetry;
        this.InternalFlags |= 512 /*0x0200*/;
        this.InternalFlags |= 7340032 /*0x700000*/;
      }

      public override void AddSelectionHandles(MapSelection sel, MapObject selectedObj)
      {
        base.AddSelectionHandles(sel, selectedObj);
        if (!this.CanReshape() || !this.ReshapableCorner)
          return;
        RectangleF bounds1 = this.Bounds;
        bool flag = this.Orientation == Orientation.Horizontal;
        double distanceLeft = (double) this.DistanceLeft;
        double distanceRight = (double) this.DistanceRight;
        double distanceTop = (double) this.DistanceTop;
        double distanceBottom = (double) this.DistanceBottom;
        bool cross = false;
        bool length = false;
        this.DetermineReshapeBehavior(ref cross, ref length);
        PointF[] points = this.getPoints();
        PointF pointF = new PointF();
        PointF loc1 = !flag ? points[5] : points[1];
        if (sel.CreateResizeHandle((MapObject) this, selectedObj, loc1, 1026, true).MapObject is MapHandle mapObject1)
        {
          mapObject1.Style = MapHandleStyle.Diamond;
          mapObject1.Brush = MapShape.Brushes_Yellow;
          RectangleF bounds2 = mapObject1.Bounds;
          MapObject.InflateRect(ref bounds2, 1f, 1f);
          mapObject1.Bounds = bounds2;
          mapObject1.Cursor = !flag ? Cursors.SizeNS : Cursors.SizeWE;
        }
        PointF loc2 = points[0];
        if (sel.CreateResizeHandle((MapObject) this, selectedObj, loc2, 1028, true).MapObject is MapHandle mapObject2)
        {
          mapObject2.Style = MapHandleStyle.Diamond;
          mapObject2.Brush = MapShape.Brushes_Yellow;
          RectangleF bounds3 = mapObject2.Bounds;
          MapObject.InflateRect(ref bounds3, 1f, 1f);
          mapObject2.Bounds = bounds3;
          if (length)
            mapObject2.Cursor = !flag ? Cursors.SizeNS : Cursors.SizeWE;
        }
        if (cross)
          return;
        PointF loc3 = !flag ? points[2] : points[4];
        if (sel.CreateResizeHandle((MapObject) this, selectedObj, loc3, 1027, true).MapObject is MapHandle mapObject3)
        {
          mapObject3.Style = MapHandleStyle.Diamond;
          mapObject3.Brush = MapShape.Brushes_Yellow;
          RectangleF bounds4 = mapObject3.Bounds;
          MapObject.InflateRect(ref bounds4, 1f, 1f);
          mapObject3.Bounds = bounds4;
          mapObject3.Cursor = !flag ? Cursors.SizeNS : Cursors.SizeWE;
        }
        PointF loc4 = points[3];
        if (!(sel.CreateResizeHandle((MapObject) this, selectedObj, loc4, 1029, true).MapObject is MapHandle mapObject4))
          return;
        mapObject4.Style = MapHandleStyle.Diamond;
        mapObject4.Brush = MapShape.Brushes_Yellow;
        RectangleF bounds5 = mapObject4.Bounds;
        MapObject.InflateRect(ref bounds5, 1f, 1f);
        mapObject4.Bounds = bounds5;
        if (!length)
          return;
        if (flag)
          mapObject4.Cursor = Cursors.SizeWE;
        else
          mapObject4.Cursor = Cursors.SizeNS;
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1442:
            this.DistanceLeft = e.GetFloat(undo);
            break;
          case 1443:
            this.DistanceRight = e.GetFloat(undo);
            break;
          case 1444:
            this.DistanceTop = e.GetFloat(undo);
            break;
          case 1445:
            this.DistanceBottom = e.GetFloat(undo);
            break;
          case 1446:
            this.Orientation = (Orientation) e.GetValue(undo);
            break;
          case 1447:
            this.ReshapeBehavior = (MapHexagonReshapeBehavior) e.GetValue(undo);
            break;
          case 1448:
            this.ReshapableCorner = (bool) e.GetValue(undo);
            break;
          case 1449:
            this.KeepsLengthwiseSymmetry = (bool) e.GetValue(undo);
            break;
          case 1450:
            this.KeepsCrosswiseSymmetry = (bool) e.GetValue(undo);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      public override bool ContainsPoint(PointF p)
      {
        return base.ContainsPoint(p) && this.GetPath().IsVisible(p);
      }

      private void DetermineReshapeBehavior(ref bool cross, ref bool length)
      {
        cross = this.KeepsCrosswiseSymmetry;
        length = this.KeepsLengthwiseSymmetry;
        switch (this.ReshapeBehavior)
        {
          case MapHexagonReshapeBehavior.CrosswiseSymmetry:
            cross = true;
            break;
          case MapHexagonReshapeBehavior.LengthwiseSymmetry:
            length = true;
            break;
          case MapHexagonReshapeBehavior.CompleteSymmetry:
            length = true;
            cross = true;
            break;
        }
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
        bool cross = false;
        bool length = false;
        this.DetermineReshapeBehavior(ref cross, ref length);
        if (whichHandle >= 1026 && whichHandle <= 1029 && (this.ResizesRealtime || evttype == MapInputState.Finish || evttype == MapInputState.Cancel))
        {
          RectangleF bounds = this.Bounds;
          RectangleF rectangleF = this.Bounds;
          bool flag = this.Orientation == Orientation.Horizontal;
          PointF pointF = new PointF();
          float distanceLeft = this.DistanceLeft;
          float distanceRight = this.DistanceRight;
          float distanceTop = this.DistanceTop;
          float distanceBottom = this.DistanceBottom;
          float num1 = this.DistanceLeft;
          float num2 = this.DistanceRight;
          float num3 = this.DistanceTop;
          float num4 = this.DistanceBottom;
          switch (whichHandle)
          {
            case 1026:
              PointF point1 = this.myPoints[0];
              if (flag)
              {
                num1 = newPoint.X - point1.X;
                if ((double) num1 > (double) bounds.Width - (double) Math.Abs(num2))
                  num1 = bounds.Width - Math.Abs(num2);
                if ((double) num1 < 0.0)
                {
                  rectangleF.X = newPoint.X;
                  rectangleF.Y = bounds.Y;
                  if ((double) distanceLeft < 0.0)
                  {
                    if (!cross)
                    {
                      rectangleF.Width = bounds.Width + distanceLeft - num1;
                      rectangleF.Height = bounds.Height;
                      break;
                    }
                    rectangleF.Width = (float) ((double) bounds.Width + (double) distanceLeft * 2.0 - (double) num1 * 2.0);
                    rectangleF.Height = bounds.Height;
                    break;
                  }
                  if (!cross)
                  {
                    rectangleF.Width = bounds.Width - num1;
                    rectangleF.Height = bounds.Height;
                    break;
                  }
                  rectangleF.Width = bounds.Width - num1 * 2f;
                  rectangleF.Height = bounds.Height;
                  break;
                }
                rectangleF.X = point1.X;
                rectangleF.Y = bounds.Y;
                if ((double) distanceLeft < 0.0)
                {
                  if (!cross)
                  {
                    rectangleF.Width = bounds.Width + distanceLeft;
                    rectangleF.Height = bounds.Height;
                    break;
                  }
                  rectangleF.Width = bounds.Width + distanceLeft * 2f;
                  rectangleF.Height = bounds.Height;
                  break;
                }
                rectangleF.Width = bounds.Width;
                rectangleF.Height = bounds.Height;
                break;
              }
              num3 = newPoint.Y - point1.Y;
              if ((double) num3 > (double) bounds.Height - (double) Math.Abs(num4))
                num3 = bounds.Height - Math.Abs(num4);
              if ((double) num3 < 0.0)
              {
                rectangleF.Y = newPoint.Y;
                rectangleF.X = bounds.X;
                if ((double) distanceTop < 0.0)
                {
                  if (!cross)
                  {
                    rectangleF.Height = bounds.Height + distanceTop - num3;
                    rectangleF.Width = bounds.Width;
                    break;
                  }
                  rectangleF.Height = (float) ((double) bounds.Height + (double) distanceTop * 2.0 - (double) num3 * 2.0);
                  rectangleF.Width = bounds.Width;
                  break;
                }
                if (!cross)
                {
                  rectangleF.Height = bounds.Height - num3;
                  rectangleF.Width = bounds.Width;
                  break;
                }
                rectangleF.Height = bounds.Height - num3 * 2f;
                rectangleF.Width = bounds.Width;
                break;
              }
              rectangleF.Y = point1.Y;
              rectangleF.X = bounds.X;
              if ((double) distanceTop < 0.0)
              {
                if (!cross)
                {
                  rectangleF.Height = bounds.Height + distanceTop;
                  rectangleF.Width = bounds.Width;
                  break;
                }
                rectangleF.Height = bounds.Height + distanceTop * 2f;
                rectangleF.Width = bounds.Width;
                break;
              }
              rectangleF.Height = bounds.Height;
              rectangleF.Width = bounds.Width;
              break;
            case 1027:
              PointF point2 = this.myPoints[3];
              if (flag)
              {
                num2 = point2.X - newPoint.X;
                if ((double) num2 > (double) bounds.Width - (double) Math.Abs(num1))
                  num2 = bounds.Width - Math.Abs(num1);
                if ((double) num2 < 0.0)
                {
                  rectangleF.X = bounds.X;
                  rectangleF.Y = bounds.Y;
                  if ((double) distanceRight < 0.0)
                  {
                    rectangleF.Width = bounds.Width + distanceRight - num2;
                    rectangleF.Height = bounds.Height;
                    break;
                  }
                  rectangleF.Width = bounds.Width - num2;
                  rectangleF.Height = bounds.Height;
                  break;
                }
                rectangleF.X = bounds.X;
                rectangleF.Y = bounds.Y;
                if ((double) distanceRight < 0.0)
                {
                  rectangleF.Width = bounds.Width + distanceRight;
                  rectangleF.Height = bounds.Height;
                  break;
                }
                rectangleF.Width = bounds.Width;
                rectangleF.Height = bounds.Height;
                break;
              }
              num4 = point2.Y - newPoint.Y;
              if ((double) num4 > (double) bounds.Height - (double) Math.Abs(num3))
                num4 = bounds.Height - Math.Abs(num3);
              if ((double) num4 < 0.0)
              {
                rectangleF.Y = bounds.Y;
                rectangleF.X = bounds.X;
                if ((double) distanceBottom < 0.0)
                {
                  rectangleF.Height = bounds.Height + distanceBottom - num4;
                  rectangleF.Width = bounds.Width;
                  break;
                }
                rectangleF.Height = bounds.Height - num4;
                rectangleF.Width = bounds.Width;
                break;
              }
              rectangleF.Y = bounds.Y;
              rectangleF.X = bounds.X;
              if ((double) distanceBottom < 0.0)
              {
                rectangleF.Height = bounds.Height + distanceBottom;
                rectangleF.Width = bounds.Width;
                break;
              }
              rectangleF.Height = bounds.Height;
              rectangleF.Width = bounds.Width;
              break;
            case 1028:
              PointF point3 = this.myPoints[1];
              if (flag)
              {
                num1 = point3.X - newPoint.X;
                if ((double) num1 < -((double) bounds.Width - (double) Math.Abs(num2)))
                  num1 = (float) -((double) bounds.Width - (double) Math.Abs(num2));
                if ((double) num1 <= 0.0)
                {
                  rectangleF.X = point3.X;
                  rectangleF.Y = bounds.Y;
                  if ((double) distanceLeft < 0.0)
                  {
                    rectangleF.Width = bounds.Width;
                    rectangleF.Height = bounds.Height;
                  }
                  else if (!cross)
                  {
                    rectangleF.Width = bounds.Width - distanceLeft;
                    rectangleF.Height = bounds.Height;
                  }
                  else
                  {
                    rectangleF.Width = bounds.Width - distanceLeft * 2f;
                    rectangleF.Height = bounds.Height;
                  }
                }
                else
                {
                  rectangleF.X = newPoint.X;
                  rectangleF.Y = bounds.Y;
                  if ((double) distanceLeft < 0.0)
                  {
                    rectangleF.Width = cross ? bounds.Width + num1 * 2f : bounds.Width + num1;
                    rectangleF.Height = bounds.Height;
                  }
                  else if (!cross)
                  {
                    rectangleF.Width = bounds.Width - distanceLeft + num1;
                    rectangleF.Height = bounds.Height;
                  }
                  else
                  {
                    rectangleF.Width = (float) ((double) bounds.Width - (double) distanceLeft * 2.0 + (double) num1 * 2.0);
                    rectangleF.Height = bounds.Height;
                  }
                }
                if (!length)
                {
                  num3 = (double) newPoint.Y >= (double) bounds.Y ? ((double) newPoint.Y <= (double) bounds.Y + (double) bounds.Height ? newPoint.Y - bounds.Y : bounds.Height) : 0.0f;
                  break;
                }
                break;
              }
              num3 = point3.Y - newPoint.Y;
              if ((double) num3 < -((double) bounds.Height - (double) Math.Abs(num4)))
                num3 = (float) -((double) bounds.Height - (double) Math.Abs(num4));
              if ((double) num3 <= 0.0)
              {
                rectangleF.Y = point3.Y;
                rectangleF.X = bounds.X;
                if ((double) distanceTop < 0.0)
                {
                  rectangleF.Height = bounds.Height;
                  rectangleF.Width = bounds.Width;
                }
                else if (!cross)
                {
                  rectangleF.Height = bounds.Height - distanceTop;
                  rectangleF.Width = bounds.Width;
                }
                else
                {
                  rectangleF.Height = bounds.Height - distanceTop * 2f;
                  rectangleF.Width = bounds.Width;
                }
              }
              else
              {
                rectangleF.Y = newPoint.Y;
                rectangleF.X = bounds.X;
                if ((double) distanceTop < 0.0)
                {
                  if (!cross)
                  {
                    rectangleF.Height = bounds.Height + num3;
                    rectangleF.Width = bounds.Width;
                  }
                  else
                  {
                    rectangleF.Height = bounds.Height + num3 * 2f;
                    rectangleF.Width = bounds.Width;
                  }
                }
                else if (!cross)
                {
                  rectangleF.Height = bounds.Height - distanceTop + num3;
                  rectangleF.Width = bounds.Width;
                }
                else
                {
                  rectangleF.Height = (float) ((double) bounds.Height - (double) distanceTop * 2.0 + (double) num3 * 2.0);
                  rectangleF.Width = bounds.Width;
                }
              }
              if (!length)
              {
                num1 = (double) newPoint.X >= (double) bounds.X ? ((double) newPoint.X <= (double) bounds.X + (double) bounds.Width ? newPoint.X - bounds.X : bounds.Width) : 0.0f;
                break;
              }
              break;
            case 1029:
              PointF point4 = this.myPoints[2];
              if (flag)
              {
                num2 = newPoint.X - point4.X;
                if ((double) num2 < -((double) bounds.Width - (double) Math.Abs(num1)))
                  num2 = (float) -((double) bounds.Width - (double) Math.Abs(num1));
                if ((double) num2 < 0.0)
                {
                  if ((double) distanceRight < 0.0)
                  {
                    rectangleF = bounds;
                  }
                  else
                  {
                    rectangleF.X = bounds.X;
                    rectangleF.Y = bounds.Y;
                    rectangleF.Width = bounds.Width - distanceRight;
                    rectangleF.Height = bounds.Height;
                  }
                }
                else
                {
                  rectangleF.X = bounds.X;
                  rectangleF.Y = bounds.Y;
                  if ((double) distanceRight < 0.0)
                  {
                    rectangleF.Width = bounds.Width + num2;
                    rectangleF.Height = bounds.Height;
                  }
                  else
                  {
                    rectangleF.Width = bounds.Width - distanceRight + num2;
                    rectangleF.Height = bounds.Height;
                  }
                }
                if (!length)
                {
                  num4 = (double) newPoint.Y >= (double) bounds.Y ? ((double) newPoint.Y <= (double) bounds.Y + (double) bounds.Height ? bounds.Y + bounds.Height - newPoint.Y : 0.0f) : bounds.Height;
                  break;
                }
                break;
              }
              num4 = newPoint.Y - point4.Y;
              if ((double) num4 < -((double) bounds.Height - (double) Math.Abs(num3)))
                num4 = (float) -((double) bounds.Height - (double) Math.Abs(num3));
              if ((double) num4 < 0.0)
              {
                rectangleF.Y = bounds.Y;
                rectangleF.X = bounds.X;
                if ((double) distanceBottom < 0.0)
                {
                  rectangleF.Height = bounds.Height;
                  rectangleF.Width = bounds.Width;
                }
                else
                {
                  rectangleF.Height = bounds.Height - distanceBottom;
                  rectangleF.Width = bounds.Width;
                }
              }
              else
              {
                rectangleF.Y = bounds.Y;
                rectangleF.X = bounds.X;
                if ((double) distanceBottom < 0.0)
                {
                  rectangleF.Height = bounds.Height + num4;
                  rectangleF.Width = bounds.Width;
                }
                else
                {
                  rectangleF.Height = bounds.Height - distanceBottom + num4;
                  rectangleF.Width = bounds.Width;
                }
              }
              if (!length)
              {
                num2 = (double) newPoint.X >= (double) bounds.X ? ((double) newPoint.X <= (double) bounds.X + (double) bounds.Width ? bounds.X + bounds.Width - newPoint.X : 0.0f) : bounds.Width;
                break;
              }
              break;
          }
          if (cross)
          {
            if (this.Orientation == Orientation.Horizontal)
            {
              if ((double) Math.Abs(num1) > (double) rectangleF.Width / 2.0)
                num1 = rectangleF.Width / 2f * (float) Math.Sign(num1);
              num2 = num1;
            }
            else
            {
              if ((double) Math.Abs(num3) > (double) rectangleF.Height / 2.0)
                num3 = rectangleF.Height / 2f * (float) Math.Sign(num3);
              num4 = num3;
            }
          }
          this.DistanceLeft = num1;
          this.DistanceTop = num3;
          if (!cross)
          {
            this.DistanceRight = num2;
            this.DistanceBottom = num4;
          }
          this.Bounds = rectangleF;
          this.ResetPath();
        }
        else
        {
          RectangleF bounds1 = this.Bounds;
          base.DoResize(view, origRect, newPoint, whichHandle, evttype, min, max);
          RectangleF bounds2 = this.Bounds;
          if (!this.CanReshape())
          {
            float num5 = bounds2.Width / bounds1.Width;
            float num6 = bounds2.Height / bounds1.Height;
            this.DistanceLeft *= num5;
            this.DistanceTop *= num6;
            if (!cross)
            {
              this.DistanceRight *= num5;
              this.DistanceBottom *= num6;
            }
          }
          if (cross)
          {
            if (this.Orientation == Orientation.Vertical)
            {
              this.DistanceRight = this.Bounds.Width - ((double) this.DistanceLeft < 0.0 ? 0.0f : this.DistanceLeft);
              this.DistanceBottom = this.DistanceTop;
            }
            else
            {
              this.DistanceBottom = this.Bounds.Height - ((double) this.DistanceTop < 0.0 ? 0.0f : this.DistanceTop);
              this.DistanceRight = this.DistanceLeft;
            }
          }
          if (length)
          {
            if (this.Orientation == Orientation.Vertical)
            {
              this.DistanceLeft = this.Bounds.Width / 2f;
              this.DistanceRight = this.Bounds.Width / 2f;
            }
            else
            {
              this.DistanceTop = this.Bounds.Height / 2f;
              this.DistanceBottom = this.Bounds.Height / 2f;
            }
          }
          this.ResetPath();
        }
      }

      public override bool GetNearestIntersectionPoint(PointF p1, PointF p2, out PointF result)
      {
        RectangleF bounds = this.Bounds;
        float shift = this.InternalPenWidth / 2f;
        PointF[] points = this.getPoints();
        PointF pointF1 = MapShape.ExpandPointOnEdge(points[0], bounds, shift);
        PointF pointF2 = MapShape.ExpandPointOnEdge(points[1], bounds, shift);
        PointF pointF3 = MapShape.ExpandPointOnEdge(points[2], bounds, shift);
        PointF pointF4 = MapShape.ExpandPointOnEdge(points[3], bounds, shift);
        PointF pointF5 = MapShape.ExpandPointOnEdge(points[4], bounds, shift);
        PointF pointF6 = MapShape.ExpandPointOnEdge(points[5], bounds, shift);
        float x = p1.X;
        float y = p1.Y;
        float num1 = 1E+21f;
        PointF pointF7 = new PointF();
        PointF result1;
        if (MapStroke.NearestIntersectionOnLine(pointF1, pointF2, p1, p2, out result1))
        {
          float num2 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num2 < (double) num1)
          {
            num1 = num2;
            pointF7 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF2, pointF3, p1, p2, out result1))
        {
          float num3 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num3 < (double) num1)
          {
            num1 = num3;
            pointF7 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF3, pointF4, p1, p2, out result1))
        {
          float num4 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num4 < (double) num1)
          {
            num1 = num4;
            pointF7 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF4, pointF5, p1, p2, out result1))
        {
          float num5 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num5 < (double) num1)
          {
            num1 = num5;
            pointF7 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF5, pointF6, p1, p2, out result1))
        {
          float num6 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num6 < (double) num1)
          {
            num1 = num6;
            pointF7 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF6, pointF1, p1, p2, out result1))
        {
          float num7 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num7 < (double) num1)
          {
            num1 = num7;
            pointF7 = result1;
          }
        }
        result = pointF7;
        return (double) num1 < 1.0000000200408773E+21;
      }

      private PointF[] getPoints()
      {
        RectangleF bounds = this.Bounds;
        float num1 = this.DistanceLeft;
        float num2 = this.DistanceRight;
        float num3 = this.DistanceTop;
        float num4 = this.DistanceBottom;
        if (this.Orientation == Orientation.Horizontal)
        {
          if ((double) num3 > (double) bounds.Height)
            num3 = bounds.Height;
          else if ((double) num3 < 0.0)
            num3 = 0.0f;
          if ((double) num4 > (double) bounds.Height)
            num4 = bounds.Height;
          else if ((double) num4 < 0.0)
            num4 = 0.0f;
          if (this.KeepsCrosswiseSymmetry)
          {
            if ((double) num1 < -((double) bounds.Width / 2.0))
              num1 = (float) -((double) bounds.Width / 2.0);
            if ((double) num1 > (double) bounds.Width / 2.0)
              num1 = bounds.Width / 2f;
            if ((double) num2 < -((double) bounds.Width / 2.0))
              num2 = (float) -((double) bounds.Width / 2.0);
            if ((double) num2 > (double) bounds.Width / 2.0)
            {
              float num5 = bounds.Width / 2f;
            }
            if ((double) num1 >= 0.0)
            {
              this.myPoints[0] = new PointF(bounds.X, bounds.Y + num3);
              this.myPoints[1] = new PointF(bounds.X + num1, bounds.Y);
              this.myPoints[2] = new PointF(bounds.X + bounds.Width - num1, bounds.Y);
              this.myPoints[3] = new PointF(bounds.X + bounds.Width, bounds.Y + num3);
              this.myPoints[4] = new PointF(bounds.X + bounds.Width - num1, bounds.Y + bounds.Height);
              this.myPoints[5] = new PointF(bounds.X + num1, bounds.Y + bounds.Height);
            }
            else
            {
              this.myPoints[0] = new PointF(bounds.X - num1, bounds.Y + num3);
              this.myPoints[1] = new PointF(bounds.X, bounds.Y);
              this.myPoints[2] = new PointF(bounds.X + bounds.Width, bounds.Y);
              this.myPoints[3] = new PointF(bounds.X + bounds.Width + num1, bounds.Y + num3);
              this.myPoints[4] = new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height);
              this.myPoints[5] = new PointF(bounds.X, bounds.Y + bounds.Height);
            }
          }
          else
          {
            if ((double) Math.Abs(num1) > (double) bounds.Width)
            {
              num1 = (double) num1 > 0.0 ? bounds.Width : -bounds.Width;
              num2 = 0.0f;
            }
            if ((double) num2 < -((double) bounds.Width - (double) Math.Abs(num1)))
              num2 = (float) -((double) bounds.Width - (double) Math.Abs(num1));
            else if ((double) num2 > (double) bounds.Width - (double) Math.Abs(num1))
              num2 = bounds.Width - Math.Abs(num1);
            if ((double) num1 >= 0.0)
            {
              if ((double) num2 >= 0.0)
              {
                this.myPoints[0] = new PointF(bounds.X, bounds.Y + num3);
                this.myPoints[1] = new PointF(bounds.X + num1, bounds.Y);
                this.myPoints[2] = new PointF(bounds.X + bounds.Width - num2, bounds.Y);
                this.myPoints[3] = new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height - num4);
                this.myPoints[4] = new PointF(bounds.X + bounds.Width - num2, bounds.Y + bounds.Height);
                this.myPoints[5] = new PointF(bounds.X + num1, bounds.Y + bounds.Height);
              }
              else
              {
                this.myPoints[0] = new PointF(bounds.X, bounds.Y + num3);
                this.myPoints[1] = new PointF(bounds.X + num1, bounds.Y);
                this.myPoints[2] = new PointF(bounds.X + bounds.Width, bounds.Y);
                this.myPoints[3] = new PointF(bounds.X + bounds.Width + num2, bounds.Y + bounds.Height - num4);
                this.myPoints[4] = new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height);
                this.myPoints[5] = new PointF(bounds.X + num1, bounds.Y + bounds.Height);
              }
            }
            else if ((double) num2 >= 0.0)
            {
              this.myPoints[0] = new PointF(bounds.X - num1, bounds.Y + num3);
              this.myPoints[1] = new PointF(bounds.X, bounds.Y);
              this.myPoints[2] = new PointF(bounds.X + bounds.Width - num2, bounds.Y);
              this.myPoints[3] = new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height - num4);
              this.myPoints[4] = new PointF(bounds.X + bounds.Width - num2, bounds.Y + bounds.Height);
              this.myPoints[5] = new PointF(bounds.X, bounds.Y + bounds.Height);
            }
            else
            {
              this.myPoints[0] = new PointF(bounds.X - num1, bounds.Y + num3);
              this.myPoints[1] = new PointF(bounds.X, bounds.Y);
              this.myPoints[2] = new PointF(bounds.X + bounds.Width, bounds.Y);
              this.myPoints[3] = new PointF(bounds.X + bounds.Width + num2, bounds.Y + bounds.Height - num4);
              this.myPoints[4] = new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height);
              this.myPoints[5] = new PointF(bounds.X, bounds.Y + bounds.Height);
            }
          }
        }
        else
        {
          if ((double) num1 > (double) bounds.Width)
            num1 = bounds.Width;
          if ((double) num1 < 0.0)
            num1 = 0.0f;
          if ((double) num2 > (double) bounds.Width)
            num2 = bounds.Width;
          if ((double) num2 < 0.0)
            num2 = 0.0f;
          if (this.KeepsCrosswiseSymmetry)
          {
            if ((double) num3 < -((double) bounds.Height / 2.0))
              num3 = (float) -((double) bounds.Height / 2.0);
            if ((double) num3 > (double) bounds.Height / 2.0)
              num3 = bounds.Height / 2f;
            if ((double) num4 < -((double) bounds.Height / 2.0))
              num4 = (float) -((double) bounds.Height / 2.0);
            if ((double) num4 > (double) bounds.Height / 2.0)
            {
              float num6 = bounds.Height / 2f;
            }
            if ((double) num3 >= 0.0)
            {
              this.myPoints[0] = new PointF(bounds.X + num1, bounds.Y);
              this.myPoints[1] = new PointF(bounds.X + bounds.Width, bounds.Y + num3);
              this.myPoints[2] = new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height - num3);
              this.myPoints[3] = new PointF(bounds.X + num1, bounds.Y + bounds.Height);
              this.myPoints[4] = new PointF(bounds.X, bounds.Y + bounds.Height - num3);
              this.myPoints[5] = new PointF(bounds.X, bounds.Y + num3);
            }
            else
            {
              this.myPoints[0] = new PointF(bounds.X + num1, bounds.Y - num3);
              this.myPoints[1] = new PointF(bounds.X + bounds.Width, bounds.Y);
              this.myPoints[2] = new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height);
              this.myPoints[3] = new PointF(bounds.X + num1, bounds.Y + bounds.Height + num3);
              this.myPoints[4] = new PointF(bounds.X, bounds.Y + bounds.Height);
              this.myPoints[5] = new PointF(bounds.X, bounds.Y);
            }
          }
          else
          {
            if ((double) Math.Abs(num3) > (double) bounds.Height)
            {
              num3 = (double) num3 > 0.0 ? bounds.Height : -bounds.Height;
              num4 = 0.0f;
            }
            if ((double) num4 < -((double) bounds.Height - (double) Math.Abs(num3)))
              num4 = (float) -((double) bounds.Height - (double) Math.Abs(num3));
            if ((double) num4 > (double) bounds.Height - (double) Math.Abs(num3))
              num4 = bounds.Height - Math.Abs(num3);
            if ((double) num3 >= 0.0)
            {
              if ((double) num4 >= 0.0)
              {
                this.myPoints[0] = new PointF(bounds.X + num1, bounds.Y);
                this.myPoints[1] = new PointF(bounds.X + bounds.Width, bounds.Y + num3);
                this.myPoints[2] = new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height - num4);
                this.myPoints[3] = new PointF(bounds.X + bounds.Width - num2, bounds.Y + bounds.Height);
                this.myPoints[4] = new PointF(bounds.X, bounds.Y + bounds.Height - num4);
                this.myPoints[5] = new PointF(bounds.X, bounds.Y + num3);
              }
              else
              {
                this.myPoints[0] = new PointF(bounds.X + num1, bounds.Y);
                this.myPoints[1] = new PointF(bounds.X + bounds.Width, bounds.Y + num3);
                this.myPoints[2] = new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height);
                this.myPoints[3] = new PointF(bounds.X + bounds.Width - num2, bounds.Y + bounds.Height + num4);
                this.myPoints[4] = new PointF(bounds.X, bounds.Y + bounds.Height);
                this.myPoints[5] = new PointF(bounds.X, bounds.Y + num3);
              }
            }
            else if ((double) num4 >= 0.0)
            {
              this.myPoints[0] = new PointF(bounds.X + num1, bounds.Y - num3);
              this.myPoints[1] = new PointF(bounds.X + bounds.Width, bounds.Y);
              this.myPoints[2] = new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height - num4);
              this.myPoints[3] = new PointF(bounds.X + bounds.Width - num2, bounds.Y + bounds.Height);
              this.myPoints[4] = new PointF(bounds.X, bounds.Y + bounds.Height - num4);
              this.myPoints[5] = new PointF(bounds.X, bounds.Y);
            }
            else
            {
              this.myPoints[0] = new PointF(bounds.X + num1, bounds.Y - num3);
              this.myPoints[1] = new PointF(bounds.X + bounds.Width, bounds.Y);
              this.myPoints[2] = new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height);
              this.myPoints[3] = new PointF(bounds.X + bounds.Width - num2, bounds.Y + bounds.Height + num4);
              this.myPoints[4] = new PointF(bounds.X, bounds.Y + bounds.Height);
              this.myPoints[5] = new PointF(bounds.X, bounds.Y);
            }
          }
        }
        return this.myPoints;
      }

      public override GraphicsPath MakePath()
      {
        GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);
        graphicsPath.AddLines(this.getPoints());
        graphicsPath.CloseAllFigures();
        return graphicsPath;
      }

      public override void Paint(Graphics g, MapView view)
      {
        SizeF shadowOffset = this.GetShadowOffset(view);
        PointF[] points = this.getPoints();
        if (this.Shadowed)
        {
          int length = points.Length;
          for (int index = 0; index < length; ++index)
          {
            this.myPoints[index].X += shadowOffset.Width;
            this.myPoints[index].Y += shadowOffset.Height;
          }
          if (this.Brush != null)
          {
            Brush shadowBrush = this.GetShadowBrush(view);
            MapShape.DrawPolygon(g, view, (Pen) null, shadowBrush, this.myPoints);
          }
          else if (this.Pen != null)
          {
            Pen shadowPen = this.GetShadowPen(view, this.InternalPenWidth);
            MapShape.DrawPolygon(g, view, shadowPen, (Brush) null, this.myPoints);
          }
          for (int index = 0; index < length; ++index)
          {
            this.myPoints[index].X -= shadowOffset.Width;
            this.myPoints[index].Y -= shadowOffset.Height;
          }
        }
        MapShape.DrawPolygon(g, view, this.Pen, this.Brush, this.myPoints);
      }

      [Category("Appearance")]
      [Description("The distance between the right/bottom point and the Hexagon's bottom border.")]
      [DefaultValue(10f)]
      public virtual float DistanceBottom
      {
        get => this.myDistanceBottom;
        set
        {
          float distanceBottom = this.myDistanceBottom;
          if ((double) distanceBottom == (double) value)
            return;
          this.myDistanceBottom = value;
          if (this.KeepsCrosswiseSymmetry)
            this.DistanceTop = this.Orientation != Orientation.Vertical ? this.Bounds.Height - value : value;
          if (this.KeepsLengthwiseSymmetry && this.Orientation == Orientation.Horizontal)
          {
            this.myDistanceBottom = this.Bounds.Height / 2f;
            this.DistanceTop = this.Bounds.Height / 2f;
          }
          this.ResetPath();
          if ((double) distanceBottom == (double) this.myDistanceBottom)
            return;
          this.Changed(1445, 0, (object) null, MapObject.MakeRect(distanceBottom), 0, (object) null, MapObject.MakeRect(this.myDistanceBottom));
        }
      }

      [Category("Appearance")]
      [DefaultValue(10f)]
      [Description("The distance between the left/top point and the Hexagon's left border.")]
      public virtual float DistanceLeft
      {
        get => this.myDistanceLeft;
        set
        {
          float distanceLeft = this.myDistanceLeft;
          if ((double) distanceLeft == (double) value)
            return;
          this.myDistanceLeft = value;
          if (this.KeepsCrosswiseSymmetry)
            this.DistanceRight = this.Orientation != Orientation.Horizontal ? this.Bounds.Width - value : value;
          if (this.KeepsLengthwiseSymmetry && this.Orientation == Orientation.Vertical)
          {
            this.myDistanceLeft = this.Bounds.Width / 2f;
            this.DistanceRight = this.Bounds.Width / 2f;
          }
          this.ResetPath();
          if ((double) distanceLeft == (double) this.myDistanceLeft)
            return;
          this.Changed(1442, 0, (object) null, MapObject.MakeRect(distanceLeft), 0, (object) null, MapObject.MakeRect(this.myDistanceLeft));
        }
      }

      [Category("Appearance")]
      [DefaultValue(10f)]
      [Description("The distance between the right/bottom point and the Hexagon's right border.")]
      public virtual float DistanceRight
      {
        get => this.myDistanceRight;
        set
        {
          float distanceRight = this.myDistanceRight;
          if ((double) distanceRight == (double) value)
            return;
          this.myDistanceRight = value;
          if (this.KeepsCrosswiseSymmetry)
            this.DistanceLeft = this.Orientation != Orientation.Horizontal ? this.Bounds.Width - value : value;
          if (this.KeepsLengthwiseSymmetry && this.Orientation == Orientation.Vertical)
          {
            this.myDistanceRight = this.Bounds.Width / 2f;
            this.DistanceLeft = this.Bounds.Width / 2f;
          }
          this.ResetPath();
          if ((double) distanceRight == (double) this.myDistanceRight)
            return;
          this.Changed(1443, 0, (object) null, MapObject.MakeRect(distanceRight), 0, (object) null, MapObject.MakeRect(this.myDistanceRight));
        }
      }

      [Description("The distance between the left/top point and the Hexagon's top border.")]
      [Category("Appearance")]
      [DefaultValue(10f)]
      public virtual float DistanceTop
      {
        get => this.myDistanceTop;
        set
        {
          float distanceTop = this.myDistanceTop;
          if ((double) distanceTop == (double) value)
            return;
          this.myDistanceTop = value;
          if (this.KeepsCrosswiseSymmetry)
            this.DistanceBottom = this.Orientation != Orientation.Vertical ? this.Bounds.Height - value : value;
          if (this.KeepsLengthwiseSymmetry && this.Orientation == Orientation.Horizontal)
          {
            this.myDistanceTop = this.Bounds.Height / 2f;
            this.DistanceBottom = this.Bounds.Height / 2f;
          }
          this.ResetPath();
          if ((double) distanceTop == (double) this.myDistanceTop)
            return;
          this.Changed(1444, 0, (object) null, MapObject.MakeRect(distanceTop), 0, (object) null, MapObject.MakeRect(this.myDistanceTop));
        }
      }

      [Description("Whether to maintain symmetry in respect to the crosswise axis.")]
      [Category("Appearance")]
      [DefaultValue(true)]
      public virtual bool KeepsCrosswiseSymmetry
      {
        get => (this.InternalFlags & 2097152 /*0x200000*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 2097152 /*0x200000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
          {
            this.InternalFlags |= 2097152 /*0x200000*/;
            if (this.Orientation == Orientation.Vertical)
            {
              this.DistanceBottom = this.DistanceTop;
              this.DistanceRight = this.Width - ((double) this.DistanceLeft < 0.0 ? 0.0f : this.DistanceLeft);
            }
            else
            {
              this.DistanceRight = this.DistanceLeft;
              this.DistanceBottom = this.Height - ((double) this.DistanceTop < 0.0 ? 0.0f : this.DistanceTop);
            }
          }
          else
            this.InternalFlags &= -2097153;
          this.Changed(1450, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("Whether to maintain symmetry in respect to the lengthwise axis.")]
      [Category("Appearance")]
      [DefaultValue(true)]
      public virtual bool KeepsLengthwiseSymmetry
      {
        get => (this.InternalFlags & 4194304 /*0x400000*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 4194304 /*0x400000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
          {
            this.InternalFlags |= 4194304 /*0x400000*/;
            if (this.Orientation == Orientation.Vertical)
            {
              this.DistanceLeft = this.Bounds.Width / 2f;
              this.DistanceRight = this.Bounds.Width / 2f;
            }
            else
            {
              this.DistanceTop = this.Bounds.Height / 2f;
              this.DistanceBottom = this.Bounds.Height / 2f;
            }
          }
          else
            this.InternalFlags &= -4194305;
          this.Changed(1449, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("Whether the pair of parallel lines run vertically or horizontally")]
      [Category("Appearance")]
      [DefaultValue(0)]
      public virtual Orientation Orientation
      {
        get => this.myOrientation;
        set
        {
          Orientation orientation = this.myOrientation;
          if (orientation == value)
            return;
          this.myOrientation = value;
          if (this.KeepsCrosswiseSymmetry)
          {
            if (value == Orientation.Vertical)
            {
              this.DistanceRight = this.Bounds.Width - ((double) this.DistanceLeft < 0.0 ? 0.0f : this.DistanceLeft);
              this.DistanceBottom = this.DistanceTop;
            }
            else
            {
              this.DistanceBottom = this.Bounds.Height - ((double) this.DistanceTop < 0.0 ? 0.0f : this.DistanceTop);
              this.DistanceRight = this.DistanceLeft;
            }
          }
          if (this.KeepsLengthwiseSymmetry)
          {
            if (value == Orientation.Vertical)
            {
              this.DistanceLeft = this.Bounds.Width / 2f;
              this.DistanceRight = this.Bounds.Width / 2f;
            }
            else
            {
              this.DistanceTop = this.Bounds.Height / 2f;
              this.DistanceBottom = this.Bounds.Height / 2f;
            }
          }
          this.ResetPath();
          this.Changed(1446, 0, (object) orientation, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Category("Behavior")]
      [Description("Whether users can reshape the corner of this resizable object.")]
      [DefaultValue(true)]
      public virtual bool ReshapableCorner
      {
        get => (this.InternalFlags & 1048576 /*0x100000*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 1048576 /*0x100000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 1048576 /*0x100000*/;
          else
            this.InternalFlags &= -1048577;
          this.Changed(1448, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [DefaultValue(3)]
      [Category("Behavior")]
      [Description("What kind of symmetry to maintain when reshaping")]
      public virtual MapHexagonReshapeBehavior ReshapeBehavior
      {
        get => this.myReshapeBehavior;
        set
        {
          MapHexagonReshapeBehavior reshapeBehavior = this.myReshapeBehavior;
          if (reshapeBehavior == value)
            return;
          this.myReshapeBehavior = value;
          this.Changed(1447, 0, (object) reshapeBehavior, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }
    }
}
