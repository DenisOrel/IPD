// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapRedCircle
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
    /// <summary>окружность для RedLines с пересчёт положения объектов относительно элемента в документе</summary>
    [Serializable]
    public class MapRedCircle : MapShape, IMapRelativePosition, IMapTime, IMapToolTipText
    {
      /// <summary>сложный объект с  IDs  состовляющеми документ</summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private IMapRelative _relative;
      /// <summary>ID элемента базового элемента</summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private string _relativeId;
      /// <summary>получить базовую точку элемента</summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private SizeF _baseOffsetId = SizeF.Empty;
      /// <summary>смещение от базовой точки элемента к базовой точки этого MapObject</summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private SizeF _offset = SizeF.Empty;
      public const int ChangedModificationTime = 1616;
      /// <summary>дата создания примитива</summary>
      private DateTime _createTime = DateTime.Now;
      /// <summary>дата последнего изменения примитива</summary>
      private DateTime _modificationTime = DateTime.Now;
      public const int ChangedToolTipText = 1618;
      private string myToolTipText;

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
          this._baseOffsetId = this.RelativeId == null || this.Relative == null ? SizeF.Empty : new SizeF(this.Relative.GetBasePoint(this.RelativeId));
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
        set
        {
          RectangleF rectangleF = value;
          float val1 = rectangleF.Width / 2f;
          float val2 = rectangleF.Height / 2f;
          float num1 = Math.Max(val1, val2);
          float num2 = rectangleF.X + val1;
          float num3 = rectangleF.Y + val2;
          rectangleF = new RectangleF(num2 - num1, num3 - num1, num1 * 2f, num1 * 2f);
          base.Bounds = rectangleF;
        }
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

      public override void Dispose()
      {
        this._relativeId = (string) null;
        this._relative = (IMapRelative) null;
        base.Dispose();
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1616:
            this.ModificationTime = (DateTime) e.GetValue(undo);
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

      public override void AddSelectionHandles(MapSelection sel, MapObject selectedObj)
      {
        base.AddSelectionHandles(sel, selectedObj);
        IMapRelative relative = this.Relative;
      }

      public override void RemoveSelectionHandles(MapSelection sel) => base.RemoveSelectionHandles(sel);

      /// <summary>проверка:  точка попала внутрь объекта ?</summary>
      /// <param name="p">проверяемая точка</param>
      /// <returns>true -точка попала внутрь объекта</returns>
      public override bool ContainsPoint(PointF p)
      {
        RectangleF bounds = this.Bounds;
        float num1 = bounds.Width / 2f;
        float num2 = bounds.Height / 2f;
        if ((double) num1 == 0.0 || (double) num2 == 0.0)
          return false;
        float num3 = bounds.X + num1;
        float num4 = (p.X - num3) / num1;
        if ((double) Math.Abs(num4) > 1.5)
          return false;
        float num5 = bounds.Y + num2;
        float num6 = (p.Y - num5) / num2;
        return (double) Math.Abs(num6) <= 1.5 && (double) Math.Abs((float) ((double) num4 * (double) num4 + (double) num6 * (double) num6 - 1.0)) <= 0.2;
      }

      protected override RectangleF ComputeBounds() => this.Bounds;

      public override bool GetNearestIntersectionPoint(PointF p1, PointF p2, out PointF result)
      {
        RectangleF bounds = this.Bounds;
        float num = this.InternalPenWidth / 2f;
        MapObject.InflateRect(ref bounds, num, num);
        return MapRedCircle.NearestIntersectionOnCircle(bounds, p1, p2, out result);
      }

      public override GraphicsPath MakePath()
      {
        GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);
        RectangleF bounds = this.Bounds;
        graphicsPath.AddEllipse(bounds.X, bounds.Y, bounds.Width, bounds.Height);
        return graphicsPath;
      }

      /// <summary>Самое близкое Пересечение На Дуге</summary>
      /// <param name="rect"></param>
      /// <param name="p1"></param>
      /// <param name="p2"></param>
      /// <param name="result"></param>
      /// <param name="startAngle"></param>
      /// <param name="sweepAngle"></param>
      /// <returns></returns>
      public static bool NearestIntersectionOnArc(
        RectangleF rect,
        PointF p1,
        PointF p2,
        out PointF result,
        float startAngle,
        float sweepAngle)
      {
        float num1 = rect.Width / 2f;
        float num2 = rect.Height / 2f;
        float num3 = rect.X + num1;
        float num4 = rect.Y + num2;
        float num5;
        float num6;
        if ((double) sweepAngle < 0.0)
        {
          num5 = startAngle + sweepAngle;
          num6 = -sweepAngle;
        }
        else
        {
          num5 = startAngle;
          num6 = sweepAngle;
        }
        if ((double) p1.X != (double) p2.X)
        {
          float num7 = (double) p1.X <= (double) p2.X ? (float) (((double) p2.Y - (double) p1.Y) / ((double) p2.X - (double) p1.X)) : (float) (((double) p1.Y - (double) p2.Y) / ((double) p1.X - (double) p2.X));
          float num8 = (float) ((double) p1.Y - (double) num4 - (double) num7 * ((double) p1.X - (double) num3));
          float num9 = (float) Math.Sqrt((double) num1 * (double) num1 * ((double) num7 * (double) num7) + (double) num2 * (double) num2 - (double) num8 * (double) num8);
          float x1 = (float) ((-((double) num1 * (double) num1 * (double) num7 * (double) num8) + (double) num1 * (double) num2 * (double) num9) / ((double) num2 * (double) num2 + (double) num1 * (double) num1 * ((double) num7 * (double) num7))) + num3;
          float x2 = (float) ((-((double) num1 * (double) num1 * (double) num7 * (double) num8) - (double) num1 * (double) num2 * (double) num9) / ((double) num2 * (double) num2 + (double) num1 * (double) num1 * ((double) num7 * (double) num7))) + num3;
          float y1 = num7 * (x1 - num3) + num8 + num4;
          float y2 = num7 * (x2 - num3) + num8 + num4;
          float angle1 = MapStroke.GetAngle(x1 - num3, y1 - num4);
          float angle2 = MapStroke.GetAngle(x2 - num3, y2 - num4);
          if ((double) angle1 < (double) num5)
            angle1 += 360f;
          if ((double) angle2 < (double) num5)
            angle2 += 360f;
          if ((double) angle1 > (double) num5 + (double) num6)
            angle1 -= 360f;
          if ((double) angle2 > (double) num5 + (double) num6)
            angle2 -= 360f;
          bool flag1 = (double) angle1 >= (double) num5 && (double) angle1 <= (double) num5 + (double) num6;
          bool flag2 = (double) angle2 >= (double) num5 && (double) angle2 <= (double) num5 + (double) num6;
          if (flag1 & flag2)
          {
            result = (double) Math.Abs((float) (((double) p1.X - (double) x1) * ((double) p1.X - (double) x1))) + (double) Math.Abs((float) (((double) p1.Y - (double) y1) * ((double) p1.Y - (double) y1))) >= (double) (Math.Abs((float) (((double) p1.X - (double) x2) * ((double) p1.X - (double) x2))) + Math.Abs((float) (((double) p1.Y - (double) y2) * ((double) p1.Y - (double) y2)))) ? new PointF(x2, y2) : new PointF(x1, y1);
            return true;
          }
          if (flag1 && !flag2)
          {
            result = new PointF(x1, y1);
            return true;
          }
          if (!flag1 & flag2)
          {
            result = new PointF(x2, y2);
            return true;
          }
          result = new PointF();
          return false;
        }
        float num10 = (float) Math.Sqrt((double) num2 * (double) num2 - (double) num2 * (double) num2 / ((double) num1 * (double) num1) * (((double) p1.X - (double) num3) * ((double) p1.X - (double) num3)));
        float y3 = num4 + num10;
        float y4 = num4 - num10;
        float angle3 = MapStroke.GetAngle(p1.X - num3, y3 - num4);
        float angle4 = MapStroke.GetAngle(p1.X - num3, y4 - num4);
        if ((double) angle3 < (double) num5)
          angle3 += 360f;
        if ((double) angle4 < (double) num5)
          angle4 += 360f;
        if ((double) angle3 > (double) num5 + (double) num6)
          angle3 -= 360f;
        if ((double) angle4 > (double) num5 + (double) num6)
          angle4 -= 360f;
        bool flag3 = (double) angle3 >= (double) num5 && (double) angle3 <= (double) num5 + (double) num6;
        bool flag4 = (double) angle4 >= (double) num5 && (double) angle4 <= (double) num5 + (double) num6;
        if (flag3 & flag4)
        {
          result = (double) Math.Abs(y3 - p1.Y) >= (double) Math.Abs(y4 - p1.Y) ? new PointF(p1.X, y4) : new PointF(p1.X, y3);
          return true;
        }
        if (flag3 && !flag4)
        {
          result = new PointF(p1.X, y3);
          return true;
        }
        if (!flag3 & flag4)
        {
          result = new PointF(p1.X, y4);
          return true;
        }
        result = new PointF();
        return false;
      }

      public static bool NearestIntersectionOnCircle(
        RectangleF rect,
        PointF p1,
        PointF p2,
        out PointF result)
      {
        if ((double) rect.Width == 0.0)
          return MapStroke.NearestIntersectionOnLine(new PointF(rect.X, rect.Y), new PointF(rect.X, rect.Y + rect.Height), p1, p2, out result);
        if ((double) rect.Height == 0.0)
          return MapStroke.NearestIntersectionOnLine(new PointF(rect.X, rect.Y), new PointF(rect.X + rect.Width, rect.Y), p1, p2, out result);
        float num1 = rect.Width / 2f;
        float num2 = rect.Height / 2f;
        float num3 = rect.X + num1;
        float num4 = rect.Y + num2;
        if ((double) p1.X != (double) p2.X)
        {
          float num5 = (double) p1.X <= (double) p2.X ? (float) (((double) p2.Y - (double) p1.Y) / ((double) p2.X - (double) p1.X)) : (float) (((double) p1.Y - (double) p2.Y) / ((double) p1.X - (double) p2.X));
          float num6 = (float) ((double) p1.Y - (double) num4 - (double) num5 * ((double) p1.X - (double) num3));
          if ((double) num1 * (double) num1 * ((double) num5 * (double) num5) + (double) num2 * (double) num2 - (double) num6 * (double) num6 < 0.0)
          {
            result = new PointF();
            return false;
          }
          float num7 = (float) Math.Sqrt((double) num1 * (double) num1 * ((double) num5 * (double) num5) + (double) num2 * (double) num2 - (double) num6 * (double) num6);
          float x1 = (float) ((-((double) num1 * (double) num1 * (double) num5 * (double) num6) + (double) num1 * (double) num2 * (double) num7) / ((double) num2 * (double) num2 + (double) num1 * (double) num1 * ((double) num5 * (double) num5))) + num3;
          float x2 = (float) ((-((double) num1 * (double) num1 * (double) num5 * (double) num6) - (double) num1 * (double) num2 * (double) num7) / ((double) num2 * (double) num2 + (double) num1 * (double) num1 * ((double) num5 * (double) num5))) + num3;
          float y1 = num5 * (x1 - num3) + num6 + num4;
          float y2 = num5 * (x2 - num3) + num6 + num4;
          result = (double) Math.Abs((float) (((double) p1.X - (double) x1) * ((double) p1.X - (double) x1))) + (double) Math.Abs((float) (((double) p1.Y - (double) y1) * ((double) p1.Y - (double) y1))) >= (double) (Math.Abs((float) (((double) p1.X - (double) x2) * ((double) p1.X - (double) x2))) + Math.Abs((float) (((double) p1.Y - (double) y2) * ((double) p1.Y - (double) y2)))) ? new PointF(x2, y2) : new PointF(x1, y1);
        }
        else
        {
          double num8 = (double) num2 * (double) num2;
          float num9 = num1 * num1;
          float num10 = p1.X - num3;
          float d = (float) (num8 - num8 / (double) num9 * ((double) num10 * (double) num10));
          if ((double) d < 0.0)
          {
            result = new PointF();
            return false;
          }
          float num11 = (float) Math.Sqrt((double) d);
          float y3 = num4 + num11;
          float y4 = num4 - num11;
          result = (double) Math.Abs(y3 - p1.Y) >= (double) Math.Abs(y4 - p1.Y) ? new PointF(p1.X, y4) : new PointF(p1.X, y3);
        }
        return true;
      }

      public override void Paint(Graphics g, MapView view)
      {
        RectangleF bounds = this.Bounds;
        if (this.Shadowed)
        {
          SizeF shadowOffset = this.GetShadowOffset(view);
          if (this.Brush != null)
          {
            Brush shadowBrush = this.GetShadowBrush(view);
            MapShape.DrawEllipse(g, view, (Pen) null, shadowBrush, bounds.X + shadowOffset.Width, bounds.Y + shadowOffset.Height, bounds.Width, bounds.Height);
          }
          else if (this.Pen != null)
          {
            Pen shadowPen = this.GetShadowPen(view, this.InternalPenWidth);
            MapShape.DrawEllipse(g, view, shadowPen, (Brush) null, bounds.X + shadowOffset.Width, bounds.Y + shadowOffset.Height, bounds.Width, bounds.Height);
          }
        }
        MapShape.DrawEllipse(g, view, this.Pen, this.Brush, bounds.X, bounds.Y, bounds.Width, bounds.Height);
      }
    }
}
