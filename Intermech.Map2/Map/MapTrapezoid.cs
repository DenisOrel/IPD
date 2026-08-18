// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapTrapezoid
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
    public class MapTrapezoid : MapShape
    {
      public const int PointAHandleID = 1034;
      public const int PointBHandleID = 1035;
      public const int PointCHandleID = 1036;
      public const int PointDHandleID = 1037;
      public const int ChangedPointA = 1460;
      public const int ChangedPointB = 1461;
      public const int ChangedPointC = 1462;
      public const int ChangedPointD = 1463;
      public const int ChangedMultiplePoints = 1464;
      public const int ChangedOrientation = 1465;
      private Orientation _orientation;
      private PointF[] _points;

      public MapTrapezoid()
      {
        this._points = new PointF[4];
        this._orientation = Orientation.Horizontal;
        this.InternalFlags |= 512 /*0x0200*/;
        this._points[1] = new PointF(8f, 0.0f);
        this._points[2] = new PointF(10f, 10f);
        this._points[3] = new PointF(0.0f, 10f);
      }

      public override void AddSelectionHandles(MapSelection sel, MapObject selectedObj)
      {
        if (!this.CanResize() || !this.CanReshape())
        {
          base.AddSelectionHandles(sel, selectedObj);
        }
        else
        {
          int orientation = (int) this.Orientation;
          PointF pointF1 = new PointF();
          PointF pointF2 = new PointF();
          PointF pointF3 = new PointF();
          PointF pointF4 = new PointF();
          double x1 = (double) this.A.X;
          PointF pointF5 = this.B;
          double x2 = (double) pointF5.X;
          bool flag1 = x1 <= x2;
          pointF5 = this.A;
          double y1 = (double) pointF5.Y;
          pointF5 = this.D;
          double y2 = (double) pointF5.Y;
          bool flag2 = y1 <= y2;
          sel.RemoveHandles((MapObject) this);
          PointF pointF6;
          PointF pointF7;
          PointF pointF8;
          PointF pointF9;
          if (flag1 & flag2)
          {
            pointF6 = this.A;
            pointF7 = this.B;
            pointF8 = this.D;
            pointF9 = this.C;
          }
          else if (!flag1 & flag2)
          {
            pointF6 = this.B;
            pointF7 = this.A;
            pointF8 = this.C;
            pointF9 = this.D;
          }
          else if (flag1 && !flag2)
          {
            pointF6 = this.D;
            pointF7 = this.C;
            pointF8 = this.A;
            pointF9 = this.B;
          }
          else
          {
            pointF6 = this.C;
            pointF7 = this.D;
            pointF8 = this.B;
            pointF9 = this.A;
          }
          PointF loc = new PointF((float) (((double) pointF6.X + (double) pointF8.X) / 2.0), (float) (((double) pointF6.Y + (double) pointF8.Y) / 2.0));
          sel.CreateResizeHandle((MapObject) this, selectedObj, loc, 256 /*0x0100*/, true);
          loc = new PointF((float) (((double) pointF7.X + (double) pointF9.X) / 2.0), (float) (((double) pointF7.Y + (double) pointF9.Y) / 2.0));
          sel.CreateResizeHandle((MapObject) this, selectedObj, loc, 64 /*0x40*/, true);
          loc = new PointF((float) (((double) pointF6.X + (double) pointF7.X) / 2.0), (float) (((double) pointF6.Y + (double) pointF7.Y) / 2.0));
          sel.CreateResizeHandle((MapObject) this, selectedObj, loc, 32 /*0x20*/, true);
          loc = new PointF((float) (((double) pointF8.X + (double) pointF9.X) / 2.0), (float) (((double) pointF8.Y + (double) pointF9.Y) / 2.0));
          sel.CreateResizeHandle((MapObject) this, selectedObj, loc, 128 /*0x80*/, true);
          sel.CreateResizeHandle((MapObject) this, selectedObj, this.A, 1034, true);
          sel.CreateResizeHandle((MapObject) this, selectedObj, this.B, 1035, true);
          sel.CreateResizeHandle((MapObject) this, selectedObj, this.C, 1036, true);
          sel.CreateResizeHandle((MapObject) this, selectedObj, this.D, 1037, true);
        }
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1460:
            this.A = e.GetPoint(undo);
            break;
          case 1461:
            this.B = e.GetPoint(undo);
            break;
          case 1462:
            this.C = e.GetPoint(undo);
            break;
          case 1463:
            this.D = e.GetPoint(undo);
            break;
          case 1464:
            this.SetPoints((PointF[]) e.GetValue(undo));
            break;
          case 1465:
            this.Orientation = (Orientation) e.GetValue(undo);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      protected override RectangleF ComputeBounds()
      {
        float x = this._points[0].X;
        float num1 = x;
        if ((double) this._points[1].X < (double) x)
          x = this._points[1].X;
        else if ((double) this._points[1].X > (double) num1)
          num1 = this._points[1].X;
        if ((double) this._points[2].X < (double) x)
          x = this._points[2].X;
        else if ((double) this._points[2].X > (double) num1)
          num1 = this._points[2].X;
        if ((double) this._points[3].X < (double) x)
          x = this._points[3].X;
        else if ((double) this._points[3].X > (double) num1)
          num1 = this._points[3].X;
        float y = this._points[0].Y;
        float num2 = y;
        if ((double) this._points[1].Y < (double) y)
          y = this._points[1].Y;
        else if ((double) this._points[1].Y > (double) num2)
          num2 = this._points[1].Y;
        if ((double) this._points[2].Y < (double) y)
          y = this._points[2].Y;
        else if ((double) this._points[2].Y > (double) num2)
          num2 = this._points[2].Y;
        if ((double) this._points[3].Y < (double) y)
          y = this._points[3].Y;
        else if ((double) this._points[3].Y > (double) num2)
          num2 = this._points[3].Y;
        return new RectangleF(x, y, num1 - x, num2 - y);
      }

      public override RectangleF ComputeResize(
        RectangleF origRect,
        PointF newPoint,
        int handle,
        SizeF min,
        SizeF max,
        bool reshape)
      {
        if (handle <= 16 /*0x10*/)
          return base.ComputeResize(origRect, newPoint, handle, min, max, reshape);
        float x1 = origRect.X;
        float y1 = origRect.Y;
        float num1 = origRect.X + origRect.Width;
        float num2 = origRect.Y + origRect.Height;
        RectangleF resize = origRect;
        switch (handle)
        {
          case 32 /*0x20*/:
            float num3;
            if (this.Orientation == Orientation.Horizontal)
            {
              num3 = 0.0f;
            }
            else
            {
              PointF pointF = this.A;
              double y2 = (double) pointF.Y;
              pointF = this.B;
              double y3 = (double) pointF.Y;
              num3 = Math.Abs((float) (y2 - y3)) / 2f;
            }
            resize.Y = Math.Max(newPoint.Y - num3, num2 - max.Height);
            resize.Y = Math.Min(resize.Y, num2 - min.Height);
            resize.Height = num2 - resize.Y;
            if ((double) resize.Height <= 0.0)
              resize.Height = 1f;
            return resize;
          case 64 /*0x40*/:
            float num4;
            if (this.Orientation == Orientation.Horizontal)
            {
              PointF pointF = this.B;
              double x2 = (double) pointF.X;
              pointF = this.C;
              double x3 = (double) pointF.X;
              num4 = Math.Abs((float) (x2 - x3)) / 2f;
            }
            else
              num4 = 0.0f;
            resize.Width = Math.Min(newPoint.X + num4 - x1, max.Width);
            resize.Width = Math.Max(resize.Width, min.Width);
            break;
          case 128 /*0x80*/:
            float num5;
            if (this.Orientation == Orientation.Horizontal)
            {
              num5 = 0.0f;
            }
            else
            {
              PointF pointF = this.C;
              double y4 = (double) pointF.Y;
              pointF = this.D;
              double y5 = (double) pointF.Y;
              num5 = Math.Abs((float) (y4 - y5)) / 2f;
            }
            resize.Height = Math.Min(newPoint.Y - y1, max.Height);
            resize.Height = Math.Max(resize.Height, min.Height);
            return resize;
          case 256 /*0x0100*/:
            float num6;
            if (this.Orientation == Orientation.Horizontal)
            {
              PointF pointF = this.A;
              double x4 = (double) pointF.X;
              pointF = this.D;
              double x5 = (double) pointF.X;
              num6 = Math.Abs((float) (x4 - x5)) / 2f;
            }
            else
              num6 = 0.0f;
            resize.X = Math.Max(newPoint.X - num6, num1 - max.Width);
            resize.X = Math.Min(resize.X, num1 - min.Width);
            resize.Width = num1 - resize.X;
            if ((double) resize.Width <= 0.0)
              resize.Width = 1f;
            return resize;
        }
        return resize;
      }

      public override bool ContainsPoint(PointF p)
      {
        return base.ContainsPoint(p) && this.GetPath().IsVisible(p);
      }

      public override void CopyNewValueForRedo(MapChangedEventArgs e)
      {
        if (e.SubHint == 1464)
          e.NewValue = this._points.Clone();
        else
          base.CopyNewValueForRedo(e);
      }

      public override MapObject CopyObject(MapCopyDictionary env)
      {
        MapTrapezoid mapTrapezoid = (MapTrapezoid) base.CopyObject(env);
        if (mapTrapezoid != null)
          mapTrapezoid._points = (PointF[]) this._points.Clone();
        return (MapObject) mapTrapezoid;
      }

      public override void CopyOldValueForUndo(MapChangedEventArgs e)
      {
        if (e.SubHint == 1464)
        {
          if (e.IsBeforeChanging)
            return;
          MapChangedEventArgs beforeChangingEdit = e.FindBeforeChangingEdit();
          if (beforeChangingEdit == null)
            return;
          e.OldValue = beforeChangingEdit.NewValue;
        }
        else
          base.CopyOldValueForUndo(e);
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
        bool flag1 = this.Orientation == Orientation.Horizontal;
        if (whichHandle < 1034 || whichHandle > 1037 || !this.CanReshape() || !this.ResizesRealtime && evttype != MapInputState.Finish && evttype != MapInputState.Cancel)
        {
          if (flag1 && (whichHandle == 256 /*0x0100*/ || whichHandle == 64 /*0x40*/) && this.CanReshape() && (this.ResizesRealtime || evttype == MapInputState.Finish || evttype == MapInputState.Cancel))
          {
            PointF pointF1 = new PointF();
            PointF pointF2 = new PointF();
            PointF pointF3 = new PointF();
            PointF pointF4 = new PointF();
            bool flag2 = (double) this.A.X <= (double) this.B.X;
            bool flag3 = true;
            switch (whichHandle)
            {
              case 64 /*0x40*/:
                if (flag2)
                {
                  pointF1 = this.B;
                  pointF2 = this.C;
                  pointF3 = this.A;
                  pointF4 = this.D;
                }
                else
                {
                  pointF1 = this.A;
                  pointF2 = this.D;
                  pointF3 = this.B;
                  pointF4 = this.C;
                }
                flag3 = false;
                break;
              case 256 /*0x0100*/:
                if (flag2)
                {
                  pointF1 = this.A;
                  pointF2 = this.D;
                  pointF3 = this.B;
                  pointF4 = this.C;
                }
                else
                {
                  pointF1 = this.B;
                  pointF2 = this.C;
                  pointF3 = this.A;
                  pointF4 = this.D;
                }
                flag3 = true;
                break;
            }
            float num = pointF1.X - pointF2.X;
            pointF1.X = newPoint.X + num / 2f;
            pointF2.X = newPoint.X - num / 2f;
            if (flag3)
            {
              if ((double) pointF1.X > (double) pointF3.X)
              {
                pointF1.X = pointF3.X;
                pointF2.X = pointF1.X - num;
              }
              if ((double) pointF2.X > (double) pointF4.X)
              {
                pointF2.X = pointF4.X;
                pointF1.X = pointF2.X + num;
              }
            }
            else
            {
              if ((double) pointF1.X <= (double) pointF3.X)
              {
                pointF1.X = pointF3.X;
                pointF2.X = pointF1.X - num;
              }
              if ((double) pointF2.X < (double) pointF4.X)
              {
                pointF2.X = pointF4.X;
                pointF1.X = pointF2.X + num;
              }
            }
            if (flag3)
            {
              if (flag2)
              {
                this.A = pointF1;
                this.D = pointF2;
              }
              else
              {
                this.B = pointF1;
                this.C = pointF2;
              }
            }
            else if (flag2)
            {
              this.B = pointF1;
              this.C = pointF2;
            }
            else
            {
              this.A = pointF1;
              this.D = pointF2;
            }
          }
          else if (flag1 || whichHandle != 32 /*0x20*/ && whichHandle != 128 /*0x80*/ || !this.CanReshape() || !this.ResizesRealtime && evttype != MapInputState.Finish && evttype != MapInputState.Cancel)
          {
            base.DoResize(view, origRect, newPoint, whichHandle, evttype, min, max);
          }
          else
          {
            PointF pointF5 = new PointF();
            PointF pointF6 = new PointF();
            PointF pointF7 = new PointF();
            PointF pointF8 = new PointF();
            bool flag4 = (double) this.A.Y <= (double) this.D.Y;
            bool flag5 = true;
            switch (whichHandle)
            {
              case 32 /*0x20*/:
                PointF pointF9;
                PointF pointF10;
                PointF pointF11;
                PointF pointF12;
                if (flag4)
                {
                  pointF9 = this.A;
                  pointF10 = this.B;
                  pointF11 = this.D;
                  pointF12 = this.C;
                }
                else
                {
                  pointF9 = this.D;
                  pointF10 = this.C;
                  pointF11 = this.A;
                  pointF12 = this.B;
                }
                bool flag6 = true;
                float num1 = pointF9.Y - pointF10.Y;
                pointF9.Y = newPoint.Y + num1 / 2f;
                pointF10.Y = newPoint.Y - num1 / 2f;
                if (flag6)
                {
                  if ((double) pointF9.Y > (double) pointF11.Y)
                  {
                    pointF9.Y = pointF11.Y;
                    pointF10.Y = pointF9.Y - num1;
                  }
                  if ((double) pointF10.Y > (double) pointF12.Y)
                  {
                    pointF10.Y = pointF12.Y;
                    pointF9.Y = pointF10.Y + num1;
                  }
                }
                else
                {
                  if ((double) pointF9.Y < (double) pointF11.Y)
                  {
                    pointF9.Y = pointF11.Y;
                    pointF10.Y = pointF9.Y - num1;
                  }
                  if ((double) pointF10.Y < (double) pointF12.Y)
                  {
                    pointF10.Y = pointF12.Y;
                    pointF9.Y = pointF10.Y + num1;
                  }
                }
                if (flag6)
                {
                  if (flag4)
                  {
                    this.A = pointF9;
                    this.B = pointF10;
                    return;
                  }
                  this.D = pointF9;
                  this.C = pointF10;
                  return;
                }
                if (flag4)
                {
                  this.D = pointF9;
                  this.C = pointF10;
                  return;
                }
                this.A = pointF9;
                this.B = pointF10;
                return;
              case 128 /*0x80*/:
                if (flag4)
                {
                  pointF5 = this.D;
                  pointF6 = this.C;
                  pointF7 = this.A;
                  pointF8 = this.B;
                }
                else
                {
                  pointF5 = this.A;
                  pointF6 = this.B;
                  pointF7 = this.D;
                  pointF8 = this.C;
                }
                flag5 = false;
                break;
            }
            float num2 = pointF5.Y - pointF6.Y;
            pointF5.Y = newPoint.Y + num2 / 2f;
            pointF6.Y = newPoint.Y - num2 / 2f;
            if (flag5)
            {
              if ((double) pointF5.Y > (double) pointF7.Y)
              {
                pointF5.Y = pointF7.Y;
                pointF6.Y = pointF5.Y - num2;
              }
              if ((double) pointF6.Y > (double) pointF8.Y)
              {
                pointF6.Y = pointF8.Y;
                pointF5.Y = pointF6.Y + num2;
              }
            }
            else
            {
              if ((double) pointF5.Y < (double) pointF7.Y)
              {
                pointF5.Y = pointF7.Y;
                pointF6.Y = pointF5.Y - num2;
              }
              if ((double) pointF6.Y < (double) pointF8.Y)
              {
                pointF6.Y = pointF8.Y;
                pointF5.Y = pointF6.Y + num2;
              }
            }
            if (flag5)
            {
              if (flag4)
              {
                this.A = pointF5;
                this.B = pointF6;
              }
              else
              {
                this.D = pointF5;
                this.C = pointF6;
              }
            }
            else if (flag4)
            {
              this.D = pointF5;
              this.C = pointF6;
            }
            else
            {
              this.A = pointF5;
              this.B = pointF6;
            }
          }
        }
        else
        {
          PointF a = this.A;
          PointF b = this.B;
          PointF c = this.C;
          PointF d = this.D;
          switch (whichHandle)
          {
            case 1034:
              PointF pointF13 = newPoint;
              if (!flag1)
              {
                if ((double) pointF13.Y > (double) d.Y)
                  pointF13.Y = d.Y;
              }
              else if ((double) pointF13.X > (double) b.X)
                pointF13.X = b.X;
              this.A = pointF13;
              break;
            case 1035:
              PointF pointF14 = newPoint;
              if (!flag1)
              {
                if ((double) pointF14.Y > (double) c.Y)
                  pointF14.Y = c.Y;
                this.B = pointF14;
                break;
              }
              if ((double) pointF14.X < (double) a.X)
                pointF14.X = a.X;
              this.B = pointF14;
              break;
            case 1036:
              PointF pointF15 = newPoint;
              if (!flag1)
              {
                if ((double) pointF15.Y < (double) b.Y)
                  pointF15.Y = b.Y;
                this.C = pointF15;
                break;
              }
              if ((double) pointF15.X < (double) d.X)
                pointF15.X = d.X;
              this.C = pointF15;
              break;
            case 1037:
              PointF pointF16 = newPoint;
              if (!flag1)
              {
                if ((double) pointF16.Y < (double) a.Y)
                  pointF16.Y = a.Y;
                this.D = pointF16;
                break;
              }
              if ((double) pointF16.X > (double) c.X)
                pointF16.X = c.X;
              this.D = pointF16;
              break;
          }
        }
      }

      public override bool GetNearestIntersectionPoint(PointF p1, PointF p2, out PointF result)
      {
        RectangleF bounds = this.Bounds;
        float shift = this.InternalPenWidth / 2f;
        PointF pointF1 = MapShape.ExpandPointOnEdge(this._points[0], bounds, shift);
        PointF pointF2 = MapShape.ExpandPointOnEdge(this._points[1], bounds, shift);
        PointF pointF3 = MapShape.ExpandPointOnEdge(this._points[2], bounds, shift);
        PointF pointF4 = MapShape.ExpandPointOnEdge(this._points[3], bounds, shift);
        float x = p1.X;
        float y = p1.Y;
        float num1 = 1E+21f;
        PointF pointF5 = new PointF();
        PointF result1;
        if (MapStroke.NearestIntersectionOnLine(pointF1, pointF2, p1, p2, out result1))
        {
          float num2 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num2 < (double) num1)
          {
            num1 = num2;
            pointF5 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF2, pointF3, p1, p2, out result1))
        {
          float num3 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num3 < (double) num1)
          {
            num1 = num3;
            pointF5 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF3, pointF4, p1, p2, out result1))
        {
          float num4 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num4 < (double) num1)
          {
            num1 = num4;
            pointF5 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF4, pointF1, p1, p2, out result1))
        {
          float num5 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num5 < (double) num1)
          {
            num1 = num5;
            pointF5 = result1;
          }
        }
        result = pointF5;
        return (double) num1 < 1.0000000200408773E+21;
      }

      public override GraphicsPath MakePath()
      {
        GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);
        graphicsPath.AddLines(this._points);
        graphicsPath.CloseAllFigures();
        return graphicsPath;
      }

      protected override void OnBoundsChanged(RectangleF old)
      {
        base.OnBoundsChanged(old);
        SizeF size = this.Size;
        if ((double) old.Width == (double) size.Width && (double) old.Height == (double) size.Height)
        {
          RectangleF bounds = this.Bounds;
          float num1 = bounds.X - old.X;
          float num2 = bounds.Y - old.Y;
          if ((double) num1 == 0.0 && (double) num2 == 0.0)
            return;
          this.Changing(1464);
          bool suspendsUpdates = this.SuspendsUpdates;
          this.SuspendsUpdates = true;
          PointF a = this.A;
          a.X += num1;
          a.Y += num2;
          PointF b = this.B;
          b.X += num1;
          b.Y += num2;
          PointF c = this.C;
          c.X += num1;
          c.Y += num2;
          PointF d = this.D;
          d.X += num1;
          d.Y += num2;
          this.A = a;
          this.B = b;
          this.C = c;
          this.D = d;
          this.InvalidBounds = false;
          this.SuspendsUpdates = suspendsUpdates;
          if (suspendsUpdates)
            return;
          this.Changed(1464, 0, (object) null, old, 0, (object) null, bounds);
        }
        else
        {
          RectangleF bounds = this.Bounds;
          float num3 = 1f;
          if ((double) old.Width != 0.0)
            num3 = bounds.Width / old.Width;
          float num4 = 1f;
          if ((double) old.Height != 0.0)
            num4 = bounds.Height / old.Height;
          this.Changing(1464);
          bool suspendsUpdates = this.SuspendsUpdates;
          this.SuspendsUpdates = true;
          PointF a = this.A;
          a.X = bounds.X + (a.X - old.X) * num3;
          a.Y = bounds.Y + (a.Y - old.Y) * num4;
          PointF b = this.B;
          b.X = bounds.X + (b.X - old.X) * num3;
          b.Y = bounds.Y + (b.Y - old.Y) * num4;
          PointF c = this.C;
          c.X = bounds.X + (c.X - old.X) * num3;
          c.Y = bounds.Y + (c.Y - old.Y) * num4;
          PointF d = this.D;
          d.X = bounds.X + (d.X - old.X) * num3;
          d.Y = bounds.Y + (d.Y - old.Y) * num4;
          this.A = a;
          this.B = b;
          this.C = c;
          this.D = d;
          this.InvalidBounds = false;
          this.SuspendsUpdates = suspendsUpdates;
          if (suspendsUpdates)
            return;
          this.Changed(1464, 0, (object) null, old, 0, (object) null, bounds);
        }
      }

      public override void Paint(Graphics g, MapView view)
      {
        SizeF shadowOffset = this.GetShadowOffset(view);
        if (this.Shadowed)
        {
          int length = this._points.Length;
          for (int index = 0; index < length; ++index)
          {
            this._points[index].X += shadowOffset.Width;
            this._points[index].Y += shadowOffset.Height;
          }
          if (this.Brush != null)
          {
            Brush shadowBrush = this.GetShadowBrush(view);
            MapShape.DrawPolygon(g, view, (Pen) null, shadowBrush, this._points);
          }
          else if (this.Pen != null)
          {
            Pen shadowPen = this.GetShadowPen(view, this.InternalPenWidth);
            MapShape.DrawPolygon(g, view, shadowPen, (Brush) null, this._points);
          }
          for (int index = 0; index < length; ++index)
          {
            this._points[index].X -= shadowOffset.Width;
            this._points[index].Y -= shadowOffset.Height;
          }
        }
        MapShape.DrawPolygon(g, view, this.Pen, this.Brush, this._points);
      }

      public virtual void SetPoints(PointF[] points)
      {
        if (points == null || points.Length != 4)
          throw new ArgumentOutOfRangeException("Trapezoids always have four points");
        if (!(points[0] != this._points[0]) && !(points[1] != this._points[1]) && !(points[2] != this._points[2]) && !(points[3] != this._points[3]))
          return;
        this.Changing(1464);
        this.ResetPath();
        Array.Copy((Array) points, 0, (Array) this._points, 0, 4);
        this.InvalidBounds = true;
        this.Changed(1464, 0, (object) null, MapObject.NullRect, 0, (object) null, MapObject.NullRect);
      }

      [Description("The first point in this trapezoid.")]
      [TypeConverter(typeof (MapPointFConverter))]
      [Category("Bounds")]
      public PointF A
      {
        get => this._points[0];
        set
        {
          PointF point = this._points[0];
          if (!(point != value))
            return;
          this._points[0] = value;
          PointF pointF = new PointF();
          if (this.Orientation == Orientation.Horizontal)
          {
            PointF b = this.B;
            if ((double) this._points[0].X > (double) this.B.X)
              b.X = this._points[0].X;
            b.Y = this._points[0].Y;
            this.B = b;
          }
          else
          {
            PointF d = this.D;
            if ((double) this._points[0].Y > (double) this.D.Y)
              d.Y = this._points[0].Y;
            d.X = this._points[0].X;
            this.D = d;
          }
          this.InvalidBounds = true;
          this.ResetPath();
          this.Changed(1460, 0, (object) null, MapObject.MakeRect(point), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [Category("Bounds")]
      [Description("The second point in this trapezoid.")]
      [TypeConverter(typeof (MapPointFConverter))]
      public PointF B
      {
        get => this._points[1];
        set
        {
          PointF point = this._points[1];
          if (!(point != value))
            return;
          this._points[1] = value;
          PointF pointF = new PointF();
          if (this.Orientation == Orientation.Horizontal)
          {
            PointF a = this.A;
            if ((double) this._points[1].X < (double) this.A.X)
              a.X = this._points[1].X;
            a.Y = this._points[1].Y;
            this.A = a;
          }
          else
          {
            PointF c = this.C;
            if ((double) this._points[1].Y > (double) this.C.Y)
              c.Y = this._points[1].Y;
            c.X = this._points[1].X;
            this.C = c;
          }
          this.InvalidBounds = true;
          this.ResetPath();
          this.Changed(1461, 0, (object) null, MapObject.MakeRect(point), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [TypeConverter(typeof (MapPointFConverter))]
      [Category("Bounds")]
      [Description("The third point in this trapezoid.")]
      public PointF C
      {
        get => this._points[2];
        set
        {
          PointF point = this._points[2];
          if (!(point != value))
            return;
          this._points[2] = value;
          PointF pointF = new PointF();
          if (this.Orientation == Orientation.Horizontal)
          {
            PointF d = this.D;
            if ((double) this._points[2].X < (double) this.D.X)
              d.X = this._points[2].X;
            d.Y = this._points[2].Y;
            this.D = d;
          }
          else
          {
            PointF b = this.B;
            if ((double) this._points[2].Y < (double) this.B.Y)
              b.Y = this._points[2].Y;
            b.X = this._points[2].X;
            this.B = b;
          }
          this.InvalidBounds = true;
          this.ResetPath();
          this.Changed(1462, 0, (object) null, MapObject.MakeRect(point), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [TypeConverter(typeof (MapPointFConverter))]
      [Category("Bounds")]
      [Description("The fourth point in this trapezoid.")]
      public PointF D
      {
        get => this._points[3];
        set
        {
          PointF point = this._points[3];
          if (!(point != value))
            return;
          this._points[3] = value;
          PointF pointF = new PointF();
          if (this.Orientation == Orientation.Horizontal)
          {
            PointF c = this.C;
            if ((double) this._points[3].X > (double) this.C.X)
              c.X = this._points[3].X;
            c.Y = this._points[3].Y;
            this.C = c;
          }
          else
          {
            PointF a = this.A;
            if ((double) this._points[3].Y < (double) this.A.Y)
              a.Y = this._points[3].Y;
            a.X = this._points[3].X;
            this.A = a;
          }
          this.InvalidBounds = true;
          this.ResetPath();
          this.Changed(1463, 0, (object) null, MapObject.MakeRect(point), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [Category("Appearance")]
      [DefaultValue(0)]
      [Description("Whether the prominent pair of verticies point vertically or horizontally")]
      public virtual Orientation Orientation
      {
        get => this._orientation;
        set
        {
          Orientation orientation = this._orientation;
          if (orientation == value)
            return;
          this._orientation = value;
          RectangleF bounds = this.Bounds;
          this.A = new PointF(bounds.X, bounds.Y);
          this.B = new PointF(bounds.X + bounds.Width, bounds.Y);
          this.C = new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height);
          this.D = new PointF(bounds.X, bounds.Y + bounds.Height);
          this.Changed(1465, 0, (object) orientation, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          this.ResetPath();
        }
      }
    }
}
