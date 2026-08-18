// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapListGroup
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
    public class MapListGroup : MapGroup
    {
      public const int ChangedSpacing = 2501;
      public const int ChangedAlignment = 2502;
      public const int ChangedLinePen = 2503;
      public const int ChangedBorderPen = 2504;
      public const int ChangedBrush = 2505;
      public const int ChangedCorner = 2506;
      public const int ChangedTopLeftMargin = 2507;
      public const int ChangedBottomRightMargin = 2508;
      public const int ChangedOrientation = 2509;
      private int _alignment;
      private MapShape.MapPenInfo _borderPenInfo;
      private SizeF _bottomRightMargin;
      private MapShape.MapBrushInfo _brushInfo;
      private SizeF _corner;
      private MapShape.MapPenInfo _linePenInfo;
      private Orientation _orientation;
      [NonSerialized]
      private GraphicsPath _path;
      private float _spacing;
      private SizeF _topLeftMargin;

      public MapListGroup()
      {
        this._orientation = Orientation.Vertical;
        this._spacing = 0.0f;
        this._alignment = 2;
        this._linePenInfo = MapShape.GetPenInfo((Pen) null);
        this._borderPenInfo = MapShape.GetPenInfo((Pen) null);
        this._brushInfo = MapShape.GetBrushInfo((Brush) null);
        this._path = (GraphicsPath) null;
        this._corner = new SizeF(0.0f, 0.0f);
        this._topLeftMargin = new SizeF(2f, 2f);
        this._bottomRightMargin = new SizeF(2f, 2f);
        this.InternalFlags &= -17;
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1001:
            base.ChangeValue(e, undo);
            this.ResetPath();
            break;
          case 2501:
            this.Initializing = true;
            this.Spacing = e.GetFloat(undo);
            this.Initializing = false;
            break;
          case 2502:
            this.Alignment = e.GetInt(undo);
            break;
          case 2503:
            this.Initializing = true;
            object obj1 = e.GetValue(undo);
            switch (obj1)
            {
              case Pen _:
                this.LinePen = (Pen) obj1;
                this.Initializing = false;
                return;
              case MapShape.MapPenInfo _:
                this.LinePen = ((MapShape.MapPenInfo) obj1).GetPen();
                break;
            }
            this.Initializing = false;
            break;
          case 2504:
            object obj2 = e.GetValue(undo);
            switch (obj2)
            {
              case Pen _:
                this.BorderPen = (Pen) obj2;
                return;
              case MapShape.MapPenInfo _:
                this.BorderPen = ((MapShape.MapPenInfo) obj2).GetPen();
                return;
              default:
                return;
            }
          case 2505:
            object obj3 = e.GetValue(undo);
            switch (obj3)
            {
              case Brush _:
                this.Brush = (Brush) obj3;
                return;
              case MapShape.MapBrushInfo _:
                this.Brush = ((MapShape.MapBrushInfo) obj3).GetBrush();
                return;
              default:
                return;
            }
          case 2506:
            this.Corner = e.GetSize(undo);
            break;
          case 2507:
            this.Initializing = true;
            this.TopLeftMargin = e.GetSize(undo);
            this.Initializing = false;
            break;
          case 2508:
            this.Initializing = true;
            this.BottomRightMargin = e.GetSize(undo);
            this.Initializing = false;
            break;
          case 2509:
            this.Initializing = true;
            this.Orientation = (Orientation) e.GetInt(undo);
            this.Initializing = false;
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      protected override RectangleF ComputeBounds()
      {
        RectangleF bounds = this.Bounds;
        SizeF topLeftMargin = this.TopLeftMargin;
        SizeF bottomRightMargin = this.BottomRightMargin;
        float val1_1 = 0.0f;
        float val1_2 = 0.0f;
        float val2 = this.Spacing;
        if (this.LinePenInfo != null)
          val2 = Math.Max(this.LinePenInfo.Width, val2);
        if (this.Orientation == Orientation.Vertical)
        {
          foreach (MapObject mapObject in (MapGroup) this)
          {
            if (mapObject != null)
            {
              val1_1 = Math.Max(val1_1, mapObject.Width);
              if (mapObject.CanView())
              {
                if ((double) val1_2 > 0.0)
                  val1_2 += val2;
                val1_2 += mapObject.Height;
              }
            }
          }
        }
        else
        {
          foreach (MapObject mapObject in (MapGroup) this)
          {
            if (mapObject != null)
            {
              val1_2 = Math.Max(val1_2, mapObject.Height);
              if (mapObject.CanView())
              {
                if ((double) val1_1 > 0.0)
                  val1_1 += val2;
                val1_1 += mapObject.Width;
              }
            }
          }
        }
        bounds.Width = val1_1 + topLeftMargin.Width + bottomRightMargin.Width;
        bounds.Height = val1_2 + topLeftMargin.Height + bottomRightMargin.Height;
        return bounds;
      }

      protected override void CopyChildren(MapGroup newgroup, MapCopyDictionary env)
      {
        MapListGroup mapListGroup = (MapListGroup) newgroup;
        if (mapListGroup != null)
          mapListGroup._path = (GraphicsPath) null;
        base.CopyChildren(newgroup, env);
      }

      private void DisposePath(GraphicsPath path)
      {
        if (path == this._path)
          return;
        path.Dispose();
      }

      private GraphicsPath GetPath(float offx, float offy)
      {
        if ((double) offx != 0.0 || (double) offy != 0.0)
        {
          GraphicsPath path = new GraphicsPath(FillMode.Winding);
          MapRoundedRectangle.MakeRoundedRectangularPath(path, offx, offy, this.Bounds, this.Corner);
          return path;
        }
        if (this._path == null)
        {
          this._path = new GraphicsPath(FillMode.Winding);
          MapRoundedRectangle.MakeRoundedRectangularPath(this._path, 0.0f, 0.0f, this.Bounds, this.Corner);
        }
        return this._path;
      }

      public override void LayoutChildren(MapObject childchanged)
      {
        if (this.Initializing)
          return;
        this.ResetPath();
        float left = this.Left;
        float top = this.Top;
        float num1 = 0.0f;
        float num2 = 0.0f;
        float val2 = this.Spacing;
        if (this.LinePenInfo != null)
          val2 = Math.Max(this.LinePenInfo.Width, val2);
        foreach (MapObject mapObject in (MapGroup) this)
        {
          if (mapObject != null && mapObject.CanView())
          {
            num1 = Math.Max(num1, mapObject.Width);
            num2 = Math.Max(num2, mapObject.Height);
          }
        }
        SizeF topLeftMargin = this.TopLeftMargin;
        SizeF bottomRightMargin = this.BottomRightMargin;
        float x = left + topLeftMargin.Width;
        float y = top + topLeftMargin.Height;
        if (this.Orientation == Orientation.Vertical)
        {
          float num3 = y;
          for (int i = 0; i < this.Count; ++i)
          {
            float num4 = num3;
            num3 = Math.Max(num3, this.LayoutItem(i, new RectangleF(x, num3, num1, num2 - num3)));
            if ((double) num3 > (double) num4)
              num3 += val2;
          }
        }
        else
        {
          float num5 = x;
          for (int i = 0; i < this.Count; ++i)
          {
            float num6 = num5;
            num5 = Math.Max(num5, this.LayoutItem(i, new RectangleF(num5, y, num1 - num5, num2)));
            if ((double) num5 > (double) num6)
              num5 += val2;
          }
        }
        this.InvalidBounds = true;
      }

      public virtual float LayoutItem(int i, RectangleF cell)
      {
        if (this.Orientation != Orientation.Vertical)
        {
          float x = cell.X;
          MapObject mapObject = this[i];
          if (mapObject == null || !mapObject.CanView())
          {
            mapObject.Position = new PointF(cell.X, cell.Y);
            return x;
          }
          int alignment = this.Alignment;
          if (alignment <= 16 /*0x10*/)
          {
            switch (alignment - 1)
            {
              case 0:
              case 2:
                PointF pointF1 = new PointF(x, cell.Y + (float) (((double) cell.Height - (double) mapObject.Height) / 2.0));
                mapObject.Position = pointF1;
                return x + mapObject.Width;
              case 1:
              case 3:
                PointF pointF2 = new PointF(x, cell.Y);
                mapObject.Position = pointF2;
                return x + mapObject.Width;
              case 4:
              case 5:
              case 6:
                PointF pointF3 = new PointF(x, cell.Y + (float) (((double) cell.Height - (double) mapObject.Height) / 2.0));
                mapObject.Position = pointF3;
                return x + mapObject.Width;
              case 7:
                PointF pointF4 = new PointF(x, cell.Y + cell.Height - mapObject.Height);
                mapObject.Position = pointF4;
                return x + mapObject.Width;
              default:
                if (alignment == 16 /*0x10*/)
                  goto case 7;
                goto case 4;
            }
          }
          else
          {
            if (alignment <= 64 /*0x40*/)
            {
              if (alignment == 32 /*0x20*/)
              {
                PointF pointF5 = new PointF(x, cell.Y);
                mapObject.Position = pointF5;
                return x + mapObject.Width;
              }
              PointF pointF6 = new PointF(x, cell.Y + (float) (((double) cell.Height - (double) mapObject.Height) / 2.0));
              mapObject.Position = pointF6;
              return x + mapObject.Width;
            }
            if (alignment == 128 /*0x80*/)
            {
              PointF pointF7 = new PointF(x, cell.Y + cell.Height - mapObject.Height);
              mapObject.Position = pointF7;
              return x + mapObject.Width;
            }
            PointF pointF8 = new PointF(x, cell.Y + (float) (((double) cell.Height - (double) mapObject.Height) / 2.0));
            mapObject.Position = pointF8;
            return x + mapObject.Width;
          }
        }
        else
        {
          float y = cell.Y;
          MapObject mapObject = this[i];
          if (mapObject == null || !mapObject.CanView())
          {
            mapObject.Position = new PointF(cell.X, cell.Y);
            return y;
          }
          int alignment = this.Alignment;
          if (alignment <= 16 /*0x10*/)
          {
            switch (alignment - 1)
            {
              case 0:
              case 2:
                PointF pointF9 = new PointF(cell.X + (float) (((double) cell.Width - (double) mapObject.Width) / 2.0), y);
                mapObject.Position = pointF9;
                return y + mapObject.Height;
              case 1:
                PointF pointF10 = new PointF(cell.X, y);
                mapObject.Position = pointF10;
                return y + mapObject.Height;
              case 3:
              case 7:
                PointF pointF11 = new PointF(cell.X + cell.Width - mapObject.Width, y);
                mapObject.Position = pointF11;
                return y + mapObject.Height;
              case 4:
              case 5:
              case 6:
                PointF pointF12 = new PointF(cell.X + (float) (((double) cell.Width - (double) mapObject.Width) / 2.0), y);
                mapObject.Position = pointF12;
                return y + mapObject.Height;
              default:
                if (alignment == 16 /*0x10*/)
                  goto case 1;
                goto case 4;
            }
          }
          else
          {
            if (alignment <= 64 /*0x40*/)
            {
              if (alignment == 64 /*0x40*/)
              {
                PointF pointF13 = new PointF(cell.X + cell.Width - mapObject.Width, y);
                mapObject.Position = pointF13;
                return y + mapObject.Height;
              }
              PointF pointF14 = new PointF(cell.X + (float) (((double) cell.Width - (double) mapObject.Width) / 2.0), y);
              mapObject.Position = pointF14;
              return y + mapObject.Height;
            }
            if (alignment == 128 /*0x80*/)
            {
              PointF pointF15 = new PointF(cell.X + (float) (((double) cell.Width - (double) mapObject.Width) / 2.0), y);
              mapObject.Position = pointF15;
              return y + mapObject.Height;
            }
            PointF pointF16 = new PointF(cell.X, y);
            mapObject.Position = pointF16;
            return y + mapObject.Height;
          }
        }
      }

      public override void Paint(Graphics g, MapView view)
      {
        this.PaintDecoration(g, view);
        base.Paint(g, view);
      }

      public virtual void PaintDecoration(Graphics g, MapView view)
      {
        if (this.Shadowed)
        {
          SizeF shadowOffset = this.GetShadowOffset(view);
          GraphicsPath path = this.GetPath(shadowOffset.Width, shadowOffset.Height);
          if (this.Brush != null)
          {
            Brush shadowBrush = this.GetShadowBrush(view);
            MapShape.DrawPath(g, view, (Pen) null, shadowBrush, path);
          }
          else if (this.BorderPen != null)
          {
            Pen shadowPen = this.GetShadowPen(view, MapShape.GetPenWidth(this.BorderPen));
            MapShape.DrawPath(g, view, shadowPen, (Brush) null, path);
          }
          this.DisposePath(path);
        }
        GraphicsPath path1 = this.GetPath(0.0f, 0.0f);
        MapShape.DrawPath(g, view, this.BorderPen, this.Brush, path1);
        Pen linePen = this.LinePen;
        if (linePen != null)
        {
          float left = this.Left;
          float top = this.Top;
          float width = this.Width;
          float height = this.Height;
          float num1 = Math.Max(MapShape.GetPenWidth(linePen), this.Spacing);
          SizeF topLeftMargin = this.TopLeftMargin;
          SizeF bottomRightMargin = this.BottomRightMargin;
          if (this.Orientation == Orientation.Vertical)
          {
            float num2 = top + topLeftMargin.Height;
            float num3 = 0.0f;
            for (int index = 0; index < this.Count; ++index)
            {
              MapObject mapObject = this[index];
              if (mapObject != null && mapObject.CanView())
              {
                if ((double) num3 > 0.0)
                {
                  MapShape.DrawLine(g, view, linePen, left, (float) ((double) num2 + (double) num3 + (double) num1 / 2.0), left + width, (float) ((double) num2 + (double) num3 + (double) num1 / 2.0));
                  num3 += num1;
                }
                num3 += mapObject.Height;
              }
            }
          }
          else
          {
            float num4 = left + topLeftMargin.Width;
            float num5 = 0.0f;
            for (int index = 0; index < this.Count; ++index)
            {
              MapObject mapObject = this[index];
              if (mapObject != null && mapObject.CanView())
              {
                if ((double) num5 > 0.0)
                {
                  MapShape.DrawLine(g, view, linePen, (float) ((double) num4 + (double) num5 + (double) num1 / 2.0), top, (float) ((double) num4 + (double) num5 + (double) num1 / 2.0), top + height);
                  num5 += num1;
                }
                num5 += mapObject.Width;
              }
            }
          }
        }
        this.DisposePath(path1);
      }

      private void ResetPath()
      {
        if (this._path == null)
          return;
        this._path.Dispose();
        this._path = (GraphicsPath) null;
      }

      [Category("Appearance")]
      [DefaultValue(2)]
      [Description("How each item is positioned along the X axis.")]
      public virtual int Alignment
      {
        get => this._alignment;
        set
        {
          int alignment = this._alignment;
          if (alignment == value)
            return;
          this._alignment = value;
          this.Changed(2502, alignment, (object) null, MapObject.NullRect, value, (object) null, MapObject.NullRect);
        }
      }

      [Category("Appearance")]
      [Description("The pen used to draw an outline for this node.")]
      [DefaultValue(null)]
      public virtual Pen BorderPen
      {
        get => this._borderPenInfo != null ? this._borderPenInfo.GetPen() : (Pen) null;
        set
        {
          MapShape.MapPenInfo borderPenInfo = this._borderPenInfo;
          MapShape.MapPenInfo penInfo = MapShape.GetPenInfo(value);
          if (borderPenInfo == penInfo)
            return;
          this._borderPenInfo = penInfo;
          this.ResetPath();
          this.Changed(2504, 0, (object) borderPenInfo, MapObject.NullRect, 0, (object) penInfo, MapObject.NullRect);
        }
      }

      [Category("Appearance")]
      [TypeConverter(typeof (MapSizeFConverter))]
      [Description("The margin around the text inside the background at the right side and the bottom")]
      public virtual SizeF BottomRightMargin
      {
        get => this._bottomRightMargin;
        set
        {
          SizeF bottomRightMargin = this._bottomRightMargin;
          if (!(bottomRightMargin != value) || (double) value.Width < 0.0 || (double) value.Height < 0.0)
            return;
          this._bottomRightMargin = value;
          this.Changed(2508, 0, (object) null, MapObject.MakeRect(bottomRightMargin), 0, (object) null, MapObject.MakeRect(value));
          this.LayoutChildren((MapObject) null);
        }
      }

      public override RectangleF Bounds
      {
        get => base.Bounds;
        set
        {
          this.ResetPath();
          base.Bounds = value;
        }
      }

      [DefaultValue(null)]
      [Category("Appearance")]
      [Description("The brush used to fill the outline of this shape.")]
      public virtual Brush Brush
      {
        get => this._brushInfo != null ? this._brushInfo.GetBrush() : (Brush) null;
        set
        {
          MapShape.MapBrushInfo brushInfo1 = this._brushInfo;
          MapShape.MapBrushInfo brushInfo2 = MapShape.GetBrushInfo(value);
          if (brushInfo1 == brushInfo2)
            return;
          this._brushInfo = brushInfo2;
          this.Changed(2505, 0, (object) brushInfo1, MapObject.NullRect, 0, (object) brushInfo2, MapObject.NullRect);
        }
      }

      [TypeConverter(typeof (MapSizeFConverter))]
      [Category("Appearance")]
      [Description("The maximum radial width and height of each corner")]
      public virtual SizeF Corner
      {
        get => this._corner;
        set
        {
          SizeF corner = this._corner;
          if (!(corner != value) || (double) value.Width < 0.0 || (double) value.Height < 0.0)
            return;
          this._corner = value;
          this.ResetPath();
          this.Changed(2506, 0, (object) null, MapObject.MakeRect(corner), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [DefaultValue(null)]
      [Description("The pen used to draw lines separating the items.")]
      [Category("Appearance")]
      public virtual Pen LinePen
      {
        get => this._linePenInfo != null ? this._linePenInfo.GetPen() : (Pen) null;
        set
        {
          MapShape.MapPenInfo linePenInfo = this._linePenInfo;
          MapShape.MapPenInfo penInfo = MapShape.GetPenInfo(value);
          if (linePenInfo == penInfo)
            return;
          this._linePenInfo = penInfo;
          this.Changed(2503, 0, (object) linePenInfo, MapObject.NullRect, 0, (object) penInfo, MapObject.NullRect);
          this.LayoutChildren((MapObject) null);
        }
      }

      internal MapShape.MapPenInfo LinePenInfo => this._linePenInfo;

      [Description("How LayoutChildren will position the items.")]
      [Category("Appearance")]
      [DefaultValue(1)]
      public virtual Orientation Orientation
      {
        get => this._orientation;
        set
        {
          Orientation orientation = this._orientation;
          if (orientation == value)
            return;
          this._orientation = value;
          this.Changed(2509, (int) orientation, (object) null, MapObject.NullRect, (int) value, (object) null, MapObject.NullRect);
          this.LayoutChildren((MapObject) null);
        }
      }

      [DefaultValue(0)]
      [Description("The additional vertical distance between items.")]
      [Category("Appearance")]
      public virtual float Spacing
      {
        get => this._spacing;
        set
        {
          float spacing = this._spacing;
          if ((double) spacing == (double) value)
            return;
          this._spacing = value;
          this.Changed(2501, 0, (object) null, MapObject.MakeRect(spacing), 0, (object) null, MapObject.MakeRect(value));
          this.LayoutChildren((MapObject) null);
        }
      }

      [TypeConverter(typeof (MapSizeFConverter))]
      [Category("Appearance")]
      [Description("The margin around the text inside the background at the left side and the top")]
      public virtual SizeF TopLeftMargin
      {
        get => this._topLeftMargin;
        set
        {
          SizeF topLeftMargin = this._topLeftMargin;
          if (!(topLeftMargin != value) || (double) value.Width < 0.0 || (double) value.Height < 0.0)
            return;
          this._topLeftMargin = value;
          this.Changed(2507, 0, (object) null, MapObject.MakeRect(topLeftMargin), 0, (object) null, MapObject.MakeRect(value));
          this.LayoutChildren((MapObject) null);
        }
      }
    }
}
