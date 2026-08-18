// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapRedStroke
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Intermech.Map
{
    /// <summary>ломаная линия для RedLines с пересчёт положения объектов относительно элемента в документе</summary>
    [Serializable]
    public class MapRedStroke : MapShape, IMapRelativePosition, IMapTime, IMapToolTipText
    {
      private PointF[] _points;
      private int _pointsCount;
      /// <summary>сложный объект с  IDs  состовляющеми документ</summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private IMapRelative _relative;
      /// <summary>ID элемента базового элемента</summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private string _relativeId;
      /// <summary>получить базовую точку элемента</summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private SizeF _baseOffsetId = SizeF.Empty;
      public const int ChangedAllPoints = 1412;
      public const int ChangedAddPoint = 1401;
      public const int ChangedRemovePoint = 1402;
      public const int ChangedModifiedPoint = 1403;
      public const int ChangedModificationTime = 1616;
      /// <summary>дата создания примитива</summary>
      private DateTime _createTime = DateTime.Now;
      /// <summary>дата последнего изменения примитива</summary>
      private DateTime _modificationTime = DateTime.Now;
      public const int ChangedToolTipText = 1618;
      private string myToolTipText;

      public MapRedStroke()
      {
        this._pointsCount = 0;
        this._points = new PointF[6];
        this.InternalFlags |= 512 /*0x0200*/;
      }

      public override void Dispose()
      {
        this._points = (PointF[]) null;
        this._relativeId = (string) null;
        this._relative = (IMapRelative) null;
        base.Dispose();
      }

      /// <summary>сложный объект с  IDs  состовляющеми документ</summary>
      public IMapRelative Relative
      {
        get => this._relative;
        set => this._relative = value;
      }

      /// <summary>ID элемента базового элемента</summary>
      public string RelativeId
      {
        get => this._relativeId;
        set
        {
          string relativeId = this._relativeId;
          this._relativeId = value;
          this._baseOffsetId = this._relativeId == null || this.Relative == null ? SizeF.Empty : new SizeF(this.Relative.GetBasePoint(this._relativeId));
        }
      }

      /// <summary>получить базовую точку элемента</summary>
      public PointF BasePoint => this._baseOffsetId.ToPointF();

      [Description("Whether users can see this object.")]
      [Category("Behavior")]
      [DefaultValue(true)]
      public override bool Visible
      {
        get
        {
          bool visible = true;
          if (this.Relative != null && this.Relative.CheckElementId(this.RelativeId))
            visible = this.Relative.GetVisible(this.RelativeId);
          if (visible == base.Visible)
            return visible;
          bool skipsUndoManager = this.SkipsUndoManager;
          this.SkipsUndoManager = true;
          base.Visible = visible;
          this.SkipsUndoManager = skipsUndoManager;
          return visible;
        }
        set
        {
        }
      }

      /// <summary>проверить поменялась ли базовая точка элемента</summary>
      /// <returns>true - если смещение базовой точки поменялось</returns>
      private bool CheckOffsetThis()
      {
        if (this.Relative == null || this.RelativeId == null)
          return false;
        SizeF sizeF = new SizeF(this.Relative.GetBasePoint(this.RelativeId));
        SizeF offset = sizeF - this._baseOffsetId;
        if (offset == SizeF.Empty)
          return false;
        bool skipsUndoManager = this.SkipsUndoManager;
        this.SkipsUndoManager = true;
        this._baseOffsetId = sizeF;
        this.OffsetThis(offset);
        this.SkipsUndoManager = skipsUndoManager;
        return true;
      }

      /// <summary>сместить объект в указанную сторону </summary>
      /// <param name="offset">смещение базовой точки</param>
      private void OffsetThis(SizeF offset)
      {
        RectangleF bounds = base.Bounds;
        bounds.Offset(offset.Width, offset.Height);
        base.Bounds = bounds;
      }

      public override RectangleF Bounds
      {
        get
        {
          this.CheckOffsetThis();
          return base.Bounds;
        }
        set => base.Bounds = value;
      }

      /// <summary>дата создания примитива</summary>
      public DateTime CreateTime
      {
        get => this._createTime;
        set => this._createTime = value;
      }

      /// <summary>дата последнего изменения примитива</summary>
      public DateTime ModificationTime
      {
        get => this._modificationTime;
        set
        {
          DateTime modificationTime = this._modificationTime;
          if (!(modificationTime != value))
            return;
          this._modificationTime = value;
          this.Changed(1616, 0, (object) modificationTime, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
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
        this.ModificationTime = DateTime.Now;
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
              this.InsertPoint(e.OldInt, new PointF(e.NewRect.X, e.NewRect.Y));
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
            this.InsertPoint(e.OldInt, new PointF(e.OldRect.X, e.OldRect.Y));
            break;
          case 1403:
            if (!undo)
            {
              this.SetPoint(e.OldInt, new PointF(e.NewRect.X, e.NewRect.Y));
              break;
            }
            this.SetPoint(e.OldInt, new PointF(e.OldRect.X, e.OldRect.Y));
            break;
          case 1412:
            this.SetPoints((PointF[]) e.GetValue(undo));
            break;
          case 1616:
            this._modificationTime = (DateTime) e.GetValue(undo);
            break;
          case 1618:
            this.Initializing = true;
            this.ToolTipText = (string) e.GetValue(undo);
            this.Initializing = false;
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      /// <summary>сформировать сведения о примитиве</summary>
      /// <returns>сведения о примитиве</returns>
      public string GenerateToolTipText()
      {
        this.ToolTipText = this.Layer == null || this.Layer.Identifier == null ? (string) null : this.Layer.Identifier.ToString();
        return this.ToolTipText;
      }

      /// <summary>сведения о примитиве </summary>
      [Description("A string to be displayed in a tooltip.")]
      public string ToolTipText
      {
        get => this.myToolTipText;
        set
        {
          if (!(this.myToolTipText != value))
            return;
          this.myToolTipText = value;
        }
      }

      public override string GetToolTip(MapView view) => this.ToolTipText;

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
        for (int i = 1; i < pointsCount; ++i)
        {
          PointF point2 = this.GetPoint(i);
          num1 = Math.Min(num1, point2.X);
          num2 = Math.Min(num2, point2.Y);
          val1_1 = Math.Max(val1_1, point2.X);
          val1_2 = Math.Max(val1_2, point2.Y);
        }
        return new RectangleF(num1, num2, val1_1 - num1, val1_2 - num2);
      }

      public override bool ContainsPoint(PointF p) => this.GetSegmentNearPoint(p) >= 0;

      public int GetSegmentNearPoint(PointF pnt)
      {
        RectangleF bounds = this.Bounds;
        float num = Math.Max(this.InternalPenWidth, 1f) + 3f;
        if ((double) pnt.X >= (double) bounds.X - (double) num && (double) pnt.X <= (double) bounds.X + (double) bounds.Width + (double) num && (double) pnt.Y >= (double) bounds.Y - (double) num && (double) pnt.Y <= (double) bounds.Y + (double) bounds.Height + (double) num)
        {
          int pointsCount = this.PointsCount;
          if (pointsCount <= 1)
            return -1;
          float fuzz = num - 1.5f;
          for (int i = 0; i < pointsCount - 1; ++i)
          {
            if (MapStroke.LineContainsPoint(this.GetPoint(i), this.GetPoint(i + 1), fuzz, pnt))
              return i;
          }
        }
        return -1;
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
        MapRedStroke mapRedStroke = (MapRedStroke) base.CopyObject(env);
        if (mapRedStroke != null)
          mapRedStroke._points = (PointF[]) this._points.Clone();
        return (MapObject) mapRedStroke;
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
        float num1 = 1E+21f;
        PointF pointF = new PointF();
        for (int i = 0; i < this.PointsCount - 1; ++i)
        {
          PointF result1;
          if (MapStroke.NearestIntersectionOnLine(this.GetPoint(i), this.GetPoint(i + 1), p1, p2, out result1))
          {
            float num2 = (float) (((double) result1.X - (double) p1.X) * ((double) result1.X - (double) p1.X) + ((double) result1.Y - (double) p1.Y) * ((double) result1.Y - (double) p1.Y));
            if ((double) num2 < (double) num1)
            {
              num1 = num2;
              pointF = result1;
            }
          }
        }
        result = pointF;
        return (double) num1 < 1.0000000200408773E+21;
      }

      private GraphicsPath GetPath(float offx, float offy)
      {
        GraphicsPath path;
        if ((double) offx != 0.0 || (double) offy != 0.0)
        {
          path = new GraphicsPath();
        }
        else
        {
          if (this.myPath != null)
            return this.myPath;
          path = new GraphicsPath();
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
        if (points.Length != 0)
          path.AddLines(points);
        return path;
      }

      public virtual PointF GetLastPoint() => this.GetPoint(this._pointsCount - 1);

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
        GraphicsPath path = this.GetPath(0.0f, 0.0f);
        MapShape.DrawPath(g, view, this.Pen, (Brush) null, path);
        this.DisposePath(path);
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

      [Description("The number of points in this Array Points.")]
      [Category("Appearance")]
      public virtual int PointsCount => this._pointsCount;
    }
}
