// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapPolygon
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;


namespace Intermech.Map
{
    [Serializable]
    public class MapPolygon : MapShape
    {
      public const int ChangedAllPoints = 1412;
      public const int ChangedStyle = 1414;
      public const int ChangedAddPoint = 1401;
      public const int ChangedRemovePoint = 1402;
      public const int ChangedModifiedPoint = 1403;
      private PointF[] _points;
      private int _pointsCount;
      private MapPolygonStyle _style;

      public MapPolygon()
      {
        this._style = MapPolygonStyle.Line;
        this._pointsCount = 0;
        this._points = new PointF[6];
        this.InternalFlags |= 512 /*0x0200*/;
      }

      public virtual int AddPoint(PointF p)
      {
        this.ResetPath();
        int length = this._points.Length;
        if (this._pointsCount >= length)
        {
          PointF[] destinationArray = new PointF[Math.Max(length * 2, this._pointsCount + 1)];
          Array.Copy((Array) this._points, 0, (Array) destinationArray, 0, length);
          this._points = destinationArray;
        }
        int index = this._pointsCount++;
        this._points[index] = p;
        this.InvalidBounds = true;
        this.Changed(1401, index, (object) null, MapObject.MakeRect(p), index, (object) null, MapObject.MakeRect(p));
        return index;
      }

      public int AddPoint(float x, float y) => this.AddPoint(new PointF(x, y));

      public override void AddSelectionHandles(MapSelection sel, MapObject selectedObj)
      {
        if (!this.CanResize() || !this.CanReshape())
        {
          base.AddSelectionHandles(sel, selectedObj);
        }
        else
        {
          sel.RemoveHandles((MapObject) this);
          int num = this.PointsCount - 1;
          for (int i = 0; i <= num; ++i)
          {
            PointF point = this.GetPoint(i);
            sel.CreateResizeHandle((MapObject) this, selectedObj, point, 8192 /*0x2000*/ + i, true);
          }
        }
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1401:
            if (!undo)
            {
              int oldInt = e.OldInt;
              RectangleF newRect = e.NewRect;
              double x = (double) newRect.X;
              newRect = e.NewRect;
              double y = (double) newRect.Y;
              PointF p = new PointF((float) x, (float) y);
              this.InsertPoint(oldInt, p);
              break;
            }
            this.RemovePoint(e.OldInt);
            break;
          case 1402:
            if (!undo)
            {
              this.RemovePoint(e.OldInt);
              break;
            }
            int oldInt1 = e.OldInt;
            RectangleF oldRect1 = e.OldRect;
            double x1 = (double) oldRect1.X;
            oldRect1 = e.OldRect;
            double y1 = (double) oldRect1.Y;
            PointF p1 = new PointF((float) x1, (float) y1);
            this.InsertPoint(oldInt1, p1);
            break;
          case 1403:
            if (!undo)
            {
              int oldInt2 = e.OldInt;
              RectangleF newRect = e.NewRect;
              double x2 = (double) newRect.X;
              newRect = e.NewRect;
              double y2 = (double) newRect.Y;
              PointF p2 = new PointF((float) x2, (float) y2);
              this.SetPoint(oldInt2, p2);
              break;
            }
            int oldInt3 = e.OldInt;
            RectangleF oldRect2 = e.OldRect;
            double x3 = (double) oldRect2.X;
            oldRect2 = e.OldRect;
            double y3 = (double) oldRect2.Y;
            PointF p3 = new PointF((float) x3, (float) y3);
            this.SetPoint(oldInt3, p3);
            break;
          case 1412:
            this.SetPoints((PointF[]) e.GetValue(undo));
            break;
          case 1414:
            this.Style = (MapPolygonStyle) e.GetValue(undo);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      public virtual void ClearPoints()
      {
        this.Changing(1412);
        this.ResetPath();
        this._pointsCount = 0;
        this.InvalidBounds = true;
        this.Changed(1412, 0, (object) null, MapObject.NullRect, 0, (object) null, MapObject.NullRect);
      }

      protected override RectangleF ComputeBounds()
      {
        int pointsCount = this.PointsCount;
        if (pointsCount <= 0)
        {
          PointF position = this.Position;
          return new RectangleF(position.X, position.Y, 0.0f, 0.0f);
        }
        PointF point1 = this.GetPoint(0);
        float num1 = point1.X;
        float num2 = point1.Y;
        float val1_1 = point1.X;
        float val1_2 = point1.Y;
        if (this.Style == MapPolygonStyle.Bezier)
        {
          for (int i = 3; i < this._pointsCount; i += 3)
          {
            PointF point2 = this.GetPoint(i - 3);
            PointF point3 = this.GetPoint(i - 2);
            if (i + 3 >= this._pointsCount)
              i = this._pointsCount - 1;
            PointF point4 = this.GetPoint(i - 1);
            PointF point5 = this.GetPoint(i);
            PointF b1 = point3;
            PointF b2 = point4;
            PointF b3 = point5;
            RectangleF rectangleF = MapStroke.BezierBounds(point2, b1, b2, b3);
            num1 = Math.Min(num1, rectangleF.X);
            num2 = Math.Min(num2, rectangleF.Y);
            val1_1 = Math.Max(val1_1, rectangleF.X + rectangleF.Width);
            val1_2 = Math.Max(val1_2, rectangleF.Y + rectangleF.Height);
          }
        }
        else
        {
          for (int i = 1; i < pointsCount; ++i)
          {
            PointF point6 = this.GetPoint(i);
            num1 = Math.Min(num1, point6.X);
            num2 = Math.Min(num2, point6.Y);
            val1_1 = Math.Max(val1_1, point6.X);
            val1_2 = Math.Max(val1_2, point6.Y);
          }
        }
        return new RectangleF(num1, num2, val1_1 - num1, val1_2 - num2);
      }

      public override bool ContainsPoint(PointF p)
      {
        if (!base.ContainsPoint(p))
          return false;
        GraphicsPath path = this.GetPath(0.0f, 0.0f);
        int num = path.IsVisible(p) ? 1 : 0;
        this.DisposePath(path);
        return num != 0;
      }

      public override void CopyNewValueForRedo(MapChangedEventArgs e)
      {
        if (e.SubHint == 1412)
        {
          PointF[] pointFArray = this.CopyPointsArray();
          e.NewValue = (object) pointFArray;
        }
        else
          base.CopyNewValueForRedo(e);
      }

      public override MapObject CopyObject(MapCopyDictionary env)
      {
        MapPolygon mapPolygon = (MapPolygon) base.CopyObject(env);
        if (mapPolygon != null)
          mapPolygon._points = (PointF[]) this._points.Clone();
        return (MapObject) mapPolygon;
      }

      public override void CopyOldValueForUndo(MapChangedEventArgs e)
      {
        if (e.SubHint == 1412)
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

      [Description("A copy of the array of points in this polygon.")]
      [Category("Appearance")]
      public virtual PointF[] CopyPointsArray()
      {
        PointF[] destinationArray = new PointF[this._pointsCount];
        Array.Copy((Array) this._points, 0, (Array) destinationArray, 0, this._pointsCount);
        return destinationArray;
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
          this.SetPoint(whichHandle - 8192 /*0x2000*/, newPoint);
        else
          base.DoResize(view, origRect, newPoint, whichHandle, evttype, min, max);
      }

      public override bool GetNearestIntersectionPoint(PointF p1, PointF p2, out PointF result)
      {
        RectangleF bounds = this.Bounds;
        float shift = this.InternalPenWidth / 2f;
        float num1 = 1E+21f;
        PointF pointF1 = new PointF();
        if (this.Style == MapPolygonStyle.Bezier)
        {
          for (int i = 3; i < this._pointsCount; i += 3)
          {
            PointF point1 = this.GetPoint(i - 3);
            PointF point2 = this.GetPoint(i - 2);
            if (i + 3 >= this._pointsCount)
              i = this._pointsCount - 1;
            PointF point3 = this.GetPoint(i - 1);
            PointF point4 = this.GetPoint(i);
            PointF b1 = point2;
            PointF b2 = point3;
            PointF b3 = point4;
            PointF p1_1 = p1;
            PointF p2_1 = p2;
            PointF pointF2;
            ref PointF local = ref pointF2;
            if (MapStroke.BezierNearestIntersectionOnLine(point1, b1, b2, b3, p1_1, p2_1, out local))
            {
              float num2 = (float) (((double) pointF2.X - (double) p1.X) * ((double) pointF2.X - (double) p1.X) + ((double) pointF2.Y - (double) p1.Y) * ((double) pointF2.Y - (double) p1.Y));
              if ((double) num2 < (double) num1)
              {
                num1 = num2;
                pointF1 = pointF2;
              }
            }
          }
        }
        else
        {
          for (int i = 0; i < this.PointsCount; ++i)
          {
            PointF result1;
            if (MapStroke.NearestIntersectionOnLine(MapShape.ExpandPointOnEdge(this.GetPoint(i), bounds, shift), MapShape.ExpandPointOnEdge(this.GetPoint(i + 1 < this.PointsCount ? i + 1 : 0), bounds, shift), p1, p2, out result1))
            {
              float num3 = (float) (((double) result1.X - (double) p1.X) * ((double) result1.X - (double) p1.X) + ((double) result1.Y - (double) p1.Y) * ((double) result1.Y - (double) p1.Y));
              if ((double) num3 < (double) num1)
              {
                num1 = num3;
                pointF1 = result1;
              }
            }
          }
        }
        result = pointF1;
        return (double) num1 < 1.0000000200408773E+21;
      }

      private GraphicsPath GetPath(float offx, float offy)
      {
        GraphicsPath path;
        if ((double) offx != 0.0 || (double) offy != 0.0)
        {
          path = new GraphicsPath(FillMode.Winding);
        }
        else
        {
          if (this.myPath != null)
            return this.myPath;
          path = new GraphicsPath(FillMode.Winding);
          this.myPath = path;
        }
        int pointsCount = this.PointsCount;
        PointF[] points = new PointF[pointsCount];
        for (int i = 0; i < pointsCount; ++i)
        {
          PointF point = this.GetPoint(i);
          points[i].X = point.X + offx;
          points[i].Y = point.Y + offy;
        }
        bool flag = this.Style == MapPolygonStyle.Bezier;
        if (flag && pointsCount % 3 != 1)
        {
          MapObject.Trace($"Polygon has wrong number of points: {pointsCount.ToString((IFormatProvider) NumberFormatInfo.InvariantInfo)}; should have 3n+1 points");
          flag = false;
        }
        if (flag)
          path.AddBeziers(points);
        else
          path.AddLines(points);
        path.CloseAllFigures();
        return path;
      }

      public virtual PointF GetPoint(int i)
      {
        return i >= 0 && i < this._pointsCount ? this._points[i] : throw new ArgumentOutOfRangeException("MapPolygon.GetPoint given an invalid index");
      }

      public virtual void InsertPoint(int i, PointF p)
      {
        if (i < 0)
          throw new ArgumentOutOfRangeException("MapPolygon.InsertPoint given an invalid index, less than zero");
        if (i > this._pointsCount)
          i = this._pointsCount;
        this.ResetPath();
        int length = this._points.Length;
        if (this._pointsCount >= length)
        {
          PointF[] destinationArray = new PointF[Math.Max(length * 2, this._pointsCount + 1)];
          Array.Copy((Array) this._points, 0, (Array) destinationArray, 0, length);
          this._points = destinationArray;
        }
        if (this._pointsCount > i)
          Array.Copy((Array) this._points, i, (Array) this._points, i + 1, this._pointsCount - i);
        ++this._pointsCount;
        this._points[i] = p;
        this.InvalidBounds = true;
        this.Changed(1401, i, (object) null, MapObject.MakeRect(p), i, (object) null, MapObject.MakeRect(p));
      }

      public override GraphicsPath MakePath() => (GraphicsPath) this.GetPath(0.0f, 0.0f).Clone();

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
            this.Changing(1412);
          this.SuspendsUpdates = true;
          for (int i = 0; i < this.PointsCount; ++i)
          {
            PointF point = this.GetPoint(i);
            float x = point.X + num1;
            float y = point.Y + num2;
            this.SetPoint(i, new PointF(x, y));
          }
          this.InvalidBounds = false;
          this.SuspendsUpdates = suspendsUpdates;
          if (suspendsUpdates)
            return;
          this.Changed(1412, 0, (object) null, old, 0, (object) null, bounds);
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
            this.Changing(1412);
          this.SuspendsUpdates = true;
          for (int i = 0; i < this.PointsCount; ++i)
          {
            PointF point = this.GetPoint(i);
            float x = bounds.X + (point.X - old.X) * num3;
            float y = bounds.Y + (point.Y - old.Y) * num4;
            this.SetPoint(i, new PointF(x, y));
          }
          this.InvalidBounds = false;
          this.SuspendsUpdates = suspendsUpdates;
          if (suspendsUpdates)
            return;
          this.Changed(1412, 0, (object) null, old, 0, (object) null, bounds);
        }
      }

      public override void Paint(Graphics g, MapView view)
      {
        if (this.Shadowed)
        {
          SizeF shadowOffset = this.GetShadowOffset(view);
          if (this.Brush != null)
          {
            Brush shadowBrush = this.GetShadowBrush(view);
            if (shadowBrush != null)
            {
              GraphicsPath path = this.GetPath(shadowOffset.Width, shadowOffset.Height);
              MapShape.DrawPath(g, view, (Pen) null, shadowBrush, path);
              this.DisposePath(path);
            }
          }
          else if (this.Pen != null)
          {
            Pen shadowPen = this.GetShadowPen(view, this.InternalPenWidth);
            if (shadowPen != null)
            {
              GraphicsPath path = this.GetPath(shadowOffset.Width, shadowOffset.Height);
              MapShape.DrawPath(g, view, shadowPen, (Brush) null, path);
              this.DisposePath(path);
            }
          }
        }
        GraphicsPath path1 = this.GetPath(0.0f, 0.0f);
        MapShape.DrawPath(g, view, this.Pen, this.Brush, path1);
        this.DisposePath(path1);
      }

      public virtual void RemovePoint(int i)
      {
        if (i < 0 || i >= this._pointsCount)
          return;
        this.ResetPath();
        PointF point = this._points[i];
        if (this._pointsCount > i + 1)
          Array.Copy((Array) this._points, i + 1, (Array) this._points, i, this._pointsCount - i - 1);
        --this._pointsCount;
        this.InvalidBounds = true;
        this.Changed(1402, i, (object) null, MapObject.MakeRect(point), i, (object) null, MapObject.MakeRect(point));
      }

      public virtual void SetPoint(int i, PointF p)
      {
        PointF p1 = i >= 0 && i < this._pointsCount ? this._points[i] : throw new ArgumentOutOfRangeException("MapPolygon.SetPoint given an invalid index");
        if (!(p1 != p))
          return;
        this.ResetPath();
        this._points[i] = p;
        this.InvalidBounds = true;
        this.Changed(1403, i, (object) null, MapObject.MakeRect(p1), i, (object) null, MapObject.MakeRect(p));
      }

      public virtual void SetPoints(PointF[] points)
      {
        this.Changing(1412);
        this.ResetPath();
        int length = points.Length;
        if (length > this._points.Length)
          this._points = new PointF[length];
        Array.Copy((Array) points, 0, (Array) this._points, 0, length);
        this._pointsCount = length;
        this.InvalidBounds = true;
        this.Changed(1412, 0, (object) null, MapObject.NullRect, 0, (object) null, MapObject.NullRect);
      }

      [Category("Appearance")]
      [Description("The number of points in this polygon.")]
      public virtual int PointsCount => this._pointsCount;

      [DefaultValue(0)]
      [Description("The kind of line or curve drawn using this polygon's points.")]
      [Category("Appearance")]
      public virtual MapPolygonStyle Style
      {
        get => this._style;
        set
        {
          MapPolygonStyle style = this._style;
          if (style == value)
            return;
          this._style = value;
          this.ResetPath();
          this.Changed(1414, 0, (object) style, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }
    }
}
