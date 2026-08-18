// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapTriangle
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Intermech.Map
{
    [Serializable]
    public class MapTriangle : MapShape
    {
      public const int ChangedAllPoints = 1434;
      public const int ChangedPointA = 1431;
      public const int ChangedPointB = 1432;
      public const int ChangedPointC = 1433;
      private PointF[] myPoints;

      public MapTriangle()
      {
        this.myPoints = new PointF[3];
        this.InternalFlags |= 512 /*0x0200*/;
        this.myPoints[1] = new PointF(10f, 0.0f);
        this.myPoints[2] = new PointF(5f, 10f);
      }

      public override void AddSelectionHandles(MapSelection sel, MapObject selectedObj)
      {
        if (!this.CanResize() || !this.CanReshape())
        {
          base.AddSelectionHandles(sel, selectedObj);
        }
        else
        {
          sel.RemoveHandles((MapObject) this);
          sel.CreateResizeHandle((MapObject) this, selectedObj, this.A, 8192 /*0x2000*/, true);
          sel.CreateResizeHandle((MapObject) this, selectedObj, this.B, 8193, true);
          sel.CreateResizeHandle((MapObject) this, selectedObj, this.C, 8194, true);
        }
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1431:
            this.A = e.GetPoint(undo);
            break;
          case 1432:
            this.B = e.GetPoint(undo);
            break;
          case 1433:
            this.C = e.GetPoint(undo);
            break;
          case 1434:
            this.myPoints = (PointF[]) e.GetValue(undo);
            this.ResetPath();
            this.InvalidBounds = true;
            this.Changed(1434, 0, (object) null, MapObject.NullRect, 0, (object) null, MapObject.NullRect);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      protected override RectangleF ComputeBounds()
      {
        float x = this.myPoints[0].X;
        float num1 = x;
        if ((double) this.myPoints[1].X < (double) x)
          x = this.myPoints[1].X;
        else if ((double) this.myPoints[1].X > (double) num1)
          num1 = this.myPoints[1].X;
        if ((double) this.myPoints[2].X < (double) x)
          x = this.myPoints[2].X;
        else if ((double) this.myPoints[2].X > (double) num1)
          num1 = this.myPoints[2].X;
        float y = this.myPoints[0].Y;
        float num2 = y;
        if ((double) this.myPoints[1].Y < (double) y)
          y = this.myPoints[1].Y;
        else if ((double) this.myPoints[1].Y > (double) num2)
          num2 = this.myPoints[1].Y;
        if ((double) this.myPoints[2].Y < (double) y)
          y = this.myPoints[2].Y;
        else if ((double) this.myPoints[2].Y > (double) num2)
          num2 = this.myPoints[2].Y;
        return new RectangleF(x, y, num1 - x, num2 - y);
      }

      public override bool ContainsPoint(PointF p)
      {
        return base.ContainsPoint(p) && this.GetPath().IsVisible(p);
      }

      public override void CopyNewValueForRedo(MapChangedEventArgs e)
      {
        if (e.SubHint == 1434)
          e.NewValue = this.myPoints.Clone();
        else
          base.CopyNewValueForRedo(e);
      }

      public override MapObject CopyObject(MapCopyDictionary env)
      {
        MapTriangle mapTriangle = (MapTriangle) base.CopyObject(env);
        if (mapTriangle != null)
          mapTriangle.myPoints = (PointF[]) this.myPoints.Clone();
        return (MapObject) mapTriangle;
      }

      public override void CopyOldValueForUndo(MapChangedEventArgs e)
      {
        if (e.SubHint == 1434)
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
        if (whichHandle >= 8192 /*0x2000*/ && (this.ResizesRealtime || evttype == MapInputState.Finish || evttype == MapInputState.Cancel))
        {
          switch (whichHandle)
          {
            case 8192 /*0x2000*/:
              this.A = newPoint;
              break;
            case 8193:
              this.B = newPoint;
              break;
            case 8194:
              this.C = newPoint;
              break;
          }
        }
        else
          base.DoResize(view, origRect, newPoint, whichHandle, evttype, min, max);
      }

      public override bool GetNearestIntersectionPoint(PointF p1, PointF p2, out PointF result)
      {
        RectangleF bounds = this.Bounds;
        float shift = this.InternalPenWidth / 2f;
        PointF pointF1 = MapShape.ExpandPointOnEdge(this.A, bounds, shift);
        PointF pointF2 = MapShape.ExpandPointOnEdge(this.B, bounds, shift);
        PointF pointF3 = MapShape.ExpandPointOnEdge(this.C, bounds, shift);
        float x = p1.X;
        float y = p1.Y;
        float num1 = 1E+21f;
        PointF pointF4 = new PointF();
        PointF result1;
        if (MapStroke.NearestIntersectionOnLine(pointF1, pointF2, p1, p2, out result1))
        {
          float num2 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num2 < (double) num1)
          {
            num1 = num2;
            pointF4 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF2, pointF3, p1, p2, out result1))
        {
          float num3 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num3 < (double) num1)
          {
            num1 = num3;
            pointF4 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF3, pointF1, p1, p2, out result1))
        {
          float num4 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num4 < (double) num1)
          {
            num1 = num4;
            pointF4 = result1;
          }
        }
        result = pointF4;
        return (double) num1 < 1.0000000200408773E+21;
      }

      public override GraphicsPath MakePath()
      {
        GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);
        graphicsPath.AddLines(this.myPoints);
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
          bool suspendsUpdates = this.SuspendsUpdates;
          if (!suspendsUpdates)
            this.Changing(1434);
          this.SuspendsUpdates = true;
          PointF a = this.A;
          a.X += num1;
          a.Y += num2;
          this.A = a;
          PointF b = this.B;
          b.X += num1;
          b.Y += num2;
          this.B = b;
          PointF c = this.C;
          c.X += num1;
          c.Y += num2;
          this.C = c;
          this.InvalidBounds = false;
          this.SuspendsUpdates = suspendsUpdates;
          if (suspendsUpdates)
            return;
          this.Changed(1434, 0, (object) null, old, 0, (object) null, bounds);
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
          bool suspendsUpdates = this.SuspendsUpdates;
          if (!suspendsUpdates)
            this.Changing(1434);
          this.SuspendsUpdates = true;
          PointF a = this.A;
          a.X = bounds.X + (a.X - old.X) * num3;
          a.Y = bounds.Y + (a.Y - old.Y) * num4;
          this.A = a;
          PointF b = this.B;
          b.X = bounds.X + (b.X - old.X) * num3;
          b.Y = bounds.Y + (b.Y - old.Y) * num4;
          this.B = b;
          PointF c = this.C;
          c.X = bounds.X + (c.X - old.X) * num3;
          c.Y = bounds.Y + (c.Y - old.Y) * num4;
          this.C = c;
          this.InvalidBounds = false;
          this.SuspendsUpdates = suspendsUpdates;
          if (suspendsUpdates)
            return;
          this.Changed(1434, 0, (object) null, old, 0, (object) null, bounds);
        }
      }

      public override void Paint(Graphics g, MapView view)
      {
        if (this.Shadowed)
        {
          SizeF shadowOffset = this.GetShadowOffset(view);
          int length = this.myPoints.Length;
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

      [Description("The first of three points in this triangle.")]
      [Category("Bounds")]
      [TypeConverter(typeof (MapPointFConverter))]
      public PointF A
      {
        get => this.myPoints[0];
        set
        {
          PointF point = this.myPoints[0];
          if (!(point != value))
            return;
          this.ResetPath();
          this.myPoints[0] = value;
          this.InvalidBounds = true;
          this.Changed(1431, 0, (object) null, MapObject.MakeRect(point), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [Description("The second of three points in this triangle.")]
      [Category("Bounds")]
      [TypeConverter(typeof (MapPointFConverter))]
      public PointF B
      {
        get => this.myPoints[1];
        set
        {
          PointF point = this.myPoints[1];
          if (!(point != value))
            return;
          this.ResetPath();
          this.myPoints[1] = value;
          this.InvalidBounds = true;
          this.Changed(1432, 0, (object) null, MapObject.MakeRect(point), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [Description("The third of three points in this triangle.")]
      [Category("Bounds")]
      [TypeConverter(typeof (MapPointFConverter))]
      public PointF C
      {
        get => this.myPoints[2];
        set
        {
          PointF point = this.myPoints[2];
          if (!(point != value))
            return;
          this.ResetPath();
          this.myPoints[2] = value;
          this.InvalidBounds = true;
          this.Changed(1433, 0, (object) null, MapObject.MakeRect(point), 0, (object) null, MapObject.MakeRect(value));
        }
      }
    }
}
