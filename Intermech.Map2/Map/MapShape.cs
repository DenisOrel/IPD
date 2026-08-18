// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapShape
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;


namespace Intermech.Map
{
    [Serializable]
    public abstract class MapShape : MapObject
    {
      public const int ChangedBrush = 1102;
      public const int ChangedPen = 1101;
      internal static readonly Brush Brushes_Red = Brushes.Red;
      internal static readonly Brush Brushes_Black = Brushes.Black;
      internal static readonly Brush Brushes_LightGray = Brushes.LightGray;
      internal static readonly Brush Brushes_Gray = Brushes.Gray;
      internal static readonly Brush Brushes_LemonChiffon = Brushes.LemonChiffon;
      internal static readonly Brush Brushes_White = Brushes.White;
      internal static readonly Brush Brushes_Yellow = Brushes.Yellow;
      internal static readonly Brush Brushes_Gold = Brushes.Gold;
      internal static readonly Brush SystemBrushes_Control = SystemBrushes.Control;
      internal static readonly Pen Pens_Red = Pens.Red;
      internal static readonly Pen Pens_Black = Pens.Black;
      internal static readonly Pen Pens_Gray = Pens.Gray;
      internal static readonly Pen Pens_LightGray = Pens.LightGray;
      internal static readonly Pen SystemPens_Control = SystemPens.Control;
      internal static readonly Pen SystemPens_ControlDark = SystemPens.ControlDark;
      internal static readonly Pen SystemPens_ControlDarkDark = SystemPens.ControlDarkDark;
      internal static readonly Pen SystemPens_ControlLightLight = SystemPens.ControlLightLight;
      internal static readonly Pen SystemPens_WindowFrame = SystemPens.WindowFrame;
      internal static int myCounter = 0;
      internal static Hashtable myDrawers = new Hashtable();
      internal static Hashtable myInfos = new Hashtable();
      private MapShape.MapBrushInfo myBrushInfo;
      [NonSerialized]
      internal GraphicsPath myPath;
      private MapShape.MapPenInfo myPenInfo;

      protected MapShape()
      {
        this.myPenInfo = MapShape.GetPenInfo(MapShape.Pens_Black);
        this.myBrushInfo = MapShape.GetBrushInfo((Brush) null);
        this.myPath = (GraphicsPath) null;
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1001:
            base.ChangeValue(e, undo);
            this.ResetPath();
            break;
          case 1101:
            object obj1 = e.GetValue(undo);
            switch (obj1)
            {
              case Pen _:
                this.Pen = (Pen) obj1;
                return;
              case MapShape.MapPenInfo _:
                this.Pen = ((MapShape.MapPenInfo) obj1).GetPen();
                return;
              default:
                return;
            }
          case 1102:
            object obj2 = e.GetValue(undo);
            switch (obj2)
            {
              case Brush _:
                this.Brush = (Brush) obj2;
                return;
              case MapShape.MapBrushInfo _:
                this.Brush = ((MapShape.MapBrushInfo) obj2).GetBrush();
                return;
              default:
                return;
            }
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      internal static void CleanInfos()
      {
        if (MapShape.myCounter++ < 100)
          return;
        MapShape.myCounter = 0;
        GC.Collect();
        ArrayList arrayList = new ArrayList();
        IDictionaryEnumerator enumerator1 = MapShape.myDrawers.GetEnumerator();
        while (enumerator1.MoveNext())
        {
          MapShape.WeakHashRef key = (MapShape.WeakHashRef) enumerator1.Key;
          if (!key.IsAlive)
            arrayList.Add((object) key);
        }
        foreach (MapShape.WeakHashRef key in arrayList)
          MapShape.myDrawers.Remove((object) key);
        arrayList.Clear();
        IDictionaryEnumerator enumerator2 = MapShape.myInfos.GetEnumerator();
        while (enumerator2.MoveNext())
        {
          MapShape.WeakHashRef key = (MapShape.WeakHashRef) enumerator2.Key;
          if (!key.IsAlive)
            arrayList.Add((object) key);
        }
        foreach (MapShape.WeakHashRef key in arrayList)
          MapShape.myInfos.Remove((object) key);
        arrayList.Clear();
      }

      public override bool ContainedByRectangle(RectangleF r)
      {
        RectangleF bounds = this.Bounds;
        float internalPenWidth = this.InternalPenWidth;
        MapObject.InflateRect(ref bounds, internalPenWidth / 2f, internalPenWidth / 2f);
        return (double) r.Width > 0.0 && (double) r.Height > 0.0 && (double) bounds.Width >= 0.0 && (double) bounds.Height >= 0.0 && (double) bounds.X >= (double) r.X && (double) bounds.Y >= (double) r.Y && (double) bounds.X + (double) bounds.Width <= (double) r.X + (double) r.Width && (double) bounds.Y + (double) bounds.Height <= (double) r.Y + (double) r.Height;
      }

      public override bool ContainsPoint(PointF p)
      {
        RectangleF bounds = this.Bounds;
        float internalPenWidth = this.InternalPenWidth;
        MapObject.InflateRect(ref bounds, internalPenWidth / 2f, internalPenWidth / 2f);
        return MapObject.ContainsRect(bounds, p);
      }

      public override MapObject CopyObject(MapCopyDictionary env)
      {
        MapShape mapShape = (MapShape) base.CopyObject(env);
        if (mapShape != null)
          mapShape.myPath = (GraphicsPath) null;
        return (MapObject) mapShape;
      }

      internal void DisposePath(GraphicsPath path)
      {
        if (path == this.myPath)
          return;
        path.Dispose();
      }

      public static void DrawBezier(
        Graphics g,
        MapView view,
        Pen pen,
        float x1,
        float y1,
        float x2,
        float y2,
        float x3,
        float y3,
        float x4,
        float y4)
      {
        if (pen == null)
          return;
        g.DrawBezier(pen, x1, y1, x2, y2, x3, y3, x4, y4);
      }

      public static void DrawEllipse(
        Graphics g,
        MapView view,
        Pen pen,
        Brush brush,
        float x,
        float y,
        float width,
        float height)
      {
        if (brush != null)
          g.FillEllipse(brush, x, y, width, height);
        if (pen == null)
          return;
        g.DrawEllipse(pen, x, y, width, height);
      }

      public static void DrawLine(
        Graphics g,
        MapView view,
        Pen pen,
        float x1,
        float y1,
        float x2,
        float y2)
      {
        if (pen == null)
          return;
        g.DrawLine(pen, x1, y1, x2, y2);
      }

      public static void DrawLines(Graphics g, MapView view, Pen pen, PointF[] points)
      {
        if (pen == null)
          return;
        g.DrawLines(pen, points);
      }

      internal static void DrawPath(
        Graphics g,
        MapView view,
        Pen pen,
        Brush brush,
        GraphicsPath path)
      {
        if (brush != null)
          g.FillPath(brush, path);
        if (pen == null)
          return;
        g.DrawPath(pen, path);
      }

      public static void DrawPie(
        Graphics g,
        MapView view,
        Pen pen,
        Brush brush,
        float x,
        float y,
        float width,
        float height,
        float startangle,
        float sweepangle)
      {
        if (brush != null)
          g.FillPie(brush, x, y, width, height, startangle, sweepangle);
        if (pen == null)
          return;
        g.DrawPie(pen, x, y, width, height, startangle, sweepangle);
      }

      public static void DrawPolygon(Graphics g, MapView view, Pen pen, Brush brush, PointF[] points)
      {
        if (brush != null)
          g.FillPolygon(brush, points);
        if (pen == null)
          return;
        g.DrawPolygon(pen, points);
      }

      public static void DrawRectangle(
        Graphics g,
        MapView view,
        Pen pen,
        Brush brush,
        float x,
        float y,
        float width,
        float height)
      {
        if (brush != null)
          g.FillRectangle(brush, x, y, width, height);
        if (pen == null)
          return;
        g.DrawRectangle(pen, x, y, width, height);
      }

      public override RectangleF ExpandPaintBounds(RectangleF rect, MapView view)
      {
        if (this.Pen != null)
        {
          float num = Math.Max(Math.Max(this.InternalPenWidth, 1f), this.PenInfo.MiterLimit + 1f);
          MapObject.InflateRect(ref rect, num, num);
        }
        if (this.Shadowed)
        {
          SizeF shadowOffset = this.GetShadowOffset(view);
          if ((double) shadowOffset.Width < 0.0)
          {
            rect.X += shadowOffset.Width;
            rect.Width -= shadowOffset.Width;
          }
          else
            rect.Width += shadowOffset.Width;
          if ((double) shadowOffset.Height < 0.0)
          {
            rect.Y += shadowOffset.Height;
            rect.Height -= shadowOffset.Height;
            return rect;
          }
          rect.Height += shadowOffset.Height;
        }
        return rect;
      }

      public static PointF ExpandPointOnEdge(PointF p, RectangleF rect, float shift)
      {
        if ((double) p.X <= (double) rect.X)
          p.X -= shift;
        else if ((double) p.X >= (double) rect.X + (double) rect.Width)
          p.X += shift;
        if ((double) p.Y <= (double) rect.Y)
        {
          p.Y -= shift;
          return p;
        }
        if ((double) p.Y >= (double) rect.Y + (double) rect.Height)
          p.Y += shift;
        return p;
      }

      internal static MapShape.MapBrushInfo GetBrushInfo(Brush b)
      {
        if (b == null)
          return (MapShape.MapBrushInfo) null;
        lock (MapShape.myDrawers)
        {
          MapShape.WeakHashRef key1 = new MapShape.WeakHashRef((object) b);
          WeakReference drawer = (WeakReference) MapShape.myDrawers[(object) key1];
          MapShape.MapBrushInfo target1 = (MapShape.MapBrushInfo) null;
          if (drawer != null)
          {
            if (drawer.IsAlive)
              target1 = drawer.Target as MapShape.MapBrushInfo;
            else
              MapShape.myDrawers.Remove((object) key1);
          }
          if (target1 == null)
          {
            target1 = new MapShape.MapBrushInfo();
            if (!target1.SetBrush(b))
              return target1;
            MapShape.WeakHashRef key2 = new MapShape.WeakHashRef((object) target1);
            WeakReference info = (WeakReference) MapShape.myInfos[(object) key2];
            if (info != null)
            {
              if (info.IsAlive)
              {
                if (info.Target is MapShape.MapBrushInfo target2)
                  return target2;
              }
              else
                MapShape.myInfos.Remove((object) key2);
            }
            MapShape.myDrawers[(object) key1] = (object) new WeakReference((object) target1);
            MapShape.myInfos[(object) key2] = (object) key2;
            MapShape.CleanInfos();
          }
          return target1;
        }
      }

      public override bool GetNearestIntersectionPoint(PointF p1, PointF p2, out PointF result)
      {
        RectangleF bounds = this.Bounds;
        float internalPenWidth = this.InternalPenWidth;
        MapObject.InflateRect(ref bounds, internalPenWidth / 2f, internalPenWidth / 2f);
        return MapObject.GetNearestIntersectionPoint(bounds, p1, p2, out result);
      }

      protected GraphicsPath GetPath()
      {
        if (this.myPath == null)
          this.myPath = this.MakePath();
        return this.myPath;
      }

      internal static MapShape.MapPenInfo GetPenInfo(Pen p)
      {
        if (p == null)
          return (MapShape.MapPenInfo) null;
        lock (MapShape.myDrawers)
        {
          MapShape.WeakHashRef key1 = new MapShape.WeakHashRef((object) p);
          WeakReference drawer = (WeakReference) MapShape.myDrawers[(object) key1];
          MapShape.MapPenInfo target1 = (MapShape.MapPenInfo) null;
          if (drawer != null)
          {
            if (drawer.IsAlive)
              target1 = drawer.Target as MapShape.MapPenInfo;
            else
              MapShape.myDrawers.Remove((object) key1);
          }
          if (target1 == null)
          {
            target1 = new MapShape.MapPenInfo();
            target1.SetPen(p);
            MapShape.WeakHashRef key2 = new MapShape.WeakHashRef((object) target1);
            WeakReference info = (WeakReference) MapShape.myInfos[(object) key2];
            if (info != null)
            {
              if (info.IsAlive)
              {
                if (info.Target is MapShape.MapPenInfo target2)
                  return target2;
              }
              else
                MapShape.myInfos.Remove((object) key2);
            }
            MapShape.myDrawers[(object) key1] = (object) new WeakReference((object) target1);
            MapShape.myInfos[(object) key2] = (object) key2;
            MapShape.CleanInfos();
          }
          return target1;
        }
      }

      internal static float GetPenWidth(Pen pen) => pen == null ? 0.0f : pen.Width;

      public static float GetPenWidth(Pen pen, MapView view)
      {
        if (pen == null)
          return 0.0f;
        float width = MapShape.GetPenInfo(pen).Width;
        return (double) width == 0.0 && view != null && (double) view.DocScale > 0.0 ? 1f / view.DocScale : width;
      }

      public virtual GraphicsPath MakePath()
      {
        GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);
        graphicsPath.AddRectangle(this.Bounds);
        return graphicsPath;
      }

      protected void ResetPath()
      {
        if (this.myPath == null)
          return;
        this.myPath.Dispose();
        this.myPath = (GraphicsPath) null;
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

      [Category("Appearance")]
      [Description("The brush used to fill the outline of this shape.")]
      public virtual Brush Brush
      {
        get => this.myBrushInfo != null ? this.myBrushInfo.GetBrush() : (Brush) null;
        set
        {
          MapShape.MapBrushInfo brushInfo1 = this.myBrushInfo;
          MapShape.MapBrushInfo brushInfo2 = MapShape.GetBrushInfo(value);
          if (brushInfo1 == brushInfo2)
            return;
          this.myBrushInfo = brushInfo2;
          this.Changed(1102, 0, (object) brushInfo1, MapObject.NullRect, 0, (object) brushInfo2, MapObject.NullRect);
        }
      }

      internal float InternalPenWidth => this.PenInfo != null ? this.PenInfo.Width : 0.0f;

      [Category("Appearance")]
      [Description("The pen used to draw the outline of this shape.")]
      public virtual Pen Pen
      {
        get => this.myPenInfo != null ? this.myPenInfo.GetPen() : (Pen) null;
        set
        {
          MapShape.MapPenInfo penInfo1 = this.myPenInfo;
          MapShape.MapPenInfo penInfo2 = MapShape.GetPenInfo(value);
          if (penInfo1 == penInfo2)
            return;
          this.myPenInfo = penInfo2;
          this.Changed(1101, 0, (object) penInfo1, MapObject.NullRect, 0, (object) penInfo2, MapObject.NullRect);
          if (this.Parent == null)
            return;
          this.Parent.InvalidatePaintBounds();
        }
      }

      internal MapShape.MapPenInfo PenInfo => this.myPenInfo;

      [Serializable]
      internal sealed class MapBrushInfo
      {
        [NonSerialized]
        internal Brush myBrush;
        internal Color myColor;
        internal Color myForeColor;
        internal HatchStyle myHatchStyle;
        internal Image myImage;
        internal string myTypeName;
        internal WrapMode myWrapMode;

        internal MapBrushInfo()
        {
        }

        public override bool Equals(object obj)
        {
          return obj is MapShape.MapBrushInfo mapBrushInfo && this.myTypeName == mapBrushInfo.myTypeName && this.myColor == mapBrushInfo.myColor && this.myForeColor == mapBrushInfo.myForeColor && this.myHatchStyle == mapBrushInfo.myHatchStyle && this.myImage == mapBrushInfo.myImage && this.myWrapMode == mapBrushInfo.myWrapMode;
        }

        public Brush GetBrush()
        {
          if (this.myBrush == null)
            this.myBrush = !(this.myTypeName == "SolidBrush") ? (!(this.myTypeName == "HatchBrush") ? (!(this.myTypeName == "TextureBrush") ? MapShape.Brushes_Gray : (Brush) new TextureBrush(this.myImage, this.myWrapMode)) : (Brush) new HatchBrush(this.myHatchStyle, this.myForeColor, this.myColor)) : (Brush) new SolidBrush(this.myColor);
          return this.myBrush;
        }

        public override int GetHashCode()
        {
          return (int) ((WrapMode) ((HatchStyle) (this.myTypeName.GetHashCode() ^ this.myColor.GetHashCode() ^ this.myForeColor.GetHashCode()) ^ this.myHatchStyle ^ (this.myImage != null ? (HatchStyle) this.myImage.GetHashCode() : HatchStyle.Horizontal)) ^ this.myWrapMode);
        }

        public bool SetBrush(Brush b)
        {
          this.myBrush = b;
          switch (b)
          {
            case SolidBrush _:
              SolidBrush solidBrush = (SolidBrush) b;
              this.myTypeName = "SolidBrush";
              this.myColor = solidBrush.Color;
              break;
            case HatchBrush _:
              HatchBrush hatchBrush = (HatchBrush) b;
              this.myTypeName = "HatchBrush";
              this.myColor = hatchBrush.BackgroundColor;
              this.myForeColor = hatchBrush.ForegroundColor;
              this.myHatchStyle = hatchBrush.HatchStyle;
              break;
            case TextureBrush _:
              TextureBrush textureBrush = (TextureBrush) b;
              this.myTypeName = "TextureBrush";
              this.myImage = textureBrush.Image;
              this.myWrapMode = textureBrush.WrapMode;
              break;
            default:
              this.myTypeName = "";
              this.myImage = (Image) null;
              return false;
          }
          return true;
        }

        public override string ToString()
        {
          return "BrushInfo: " + this.myTypeName + " " + this.myColor.ToString();
        }
      }

      [Serializable]
      internal sealed class MapPenInfo
      {
        internal PenAlignment myAlignment;
        internal Color myColor;
        internal DashCap myDashCap;
        internal float myDashOffset;
        internal float[] myDashPattern;
        internal DashStyle myDashStyle;
        internal LineCap myEndCap;
        internal LineJoin myLineJoin;
        internal float myMiterLimit;
        [NonSerialized]
        internal Pen myPen;
        internal LineCap myStartCap;
        internal float myWidth;

        internal MapPenInfo()
        {
        }

        public override bool Equals(object obj)
        {
          if (!(obj is MapShape.MapPenInfo mapPenInfo))
            return false;
          bool flag = this.myColor == mapPenInfo.myColor && (double) this.myWidth == (double) mapPenInfo.myWidth && this.myDashStyle == mapPenInfo.myDashStyle && this.myDashCap == mapPenInfo.myDashCap && (double) this.myDashOffset == (double) mapPenInfo.myDashOffset && this.myAlignment == mapPenInfo.myAlignment && this.myEndCap == mapPenInfo.myEndCap && this.myStartCap == mapPenInfo.myStartCap && this.myLineJoin == mapPenInfo.myLineJoin && (double) this.myMiterLimit == (double) mapPenInfo.myMiterLimit;
          if (flag && this.myDashStyle == DashStyle.Custom)
          {
            if (this.myDashPattern == null && mapPenInfo.myDashPattern == null)
              return true;
            if (this.myDashPattern == null || mapPenInfo.myDashPattern == null || this.myDashPattern.Length != mapPenInfo.myDashPattern.Length)
              return false;
            for (int index = 0; index < this.myDashPattern.Length; ++index)
            {
              if ((double) this.myDashPattern[index] != (double) mapPenInfo.myDashPattern[index])
                return false;
            }
          }
          return flag;
        }

        public override int GetHashCode()
        {
          int hashCode = (int) ((DashStyle) (this.myColor.GetHashCode() ^ this.myWidth.GetHashCode()) ^ this.myDashStyle ^ (DashStyle) this.myDashCap ^ (DashStyle) this.myDashOffset.GetHashCode() ^ (DashStyle) this.myAlignment ^ (DashStyle) this.myEndCap ^ (DashStyle) this.myStartCap ^ (DashStyle) this.myLineJoin ^ (DashStyle) this.myMiterLimit.GetHashCode());
          if (this.myDashStyle == DashStyle.Custom && this.myDashPattern != null)
            hashCode ^= this.myDashPattern.GetHashCode();
          return hashCode;
        }

        public Pen GetPen()
        {
          if (this.myPen == null)
          {
            this.myPen = new Pen(this.myColor, this.myWidth);
            this.myPen.DashStyle = this.myDashStyle;
            this.myPen.DashCap = this.myDashCap;
            this.myPen.DashOffset = this.myDashOffset;
            if (this.myDashStyle == DashStyle.Custom)
              this.myPen.DashPattern = this.myDashPattern;
            this.myPen.Alignment = this.myAlignment;
            this.myPen.EndCap = this.myEndCap;
            this.myPen.StartCap = this.myStartCap;
            this.myPen.LineJoin = this.myLineJoin;
            this.myPen.MiterLimit = this.myMiterLimit;
          }
          return this.myPen;
        }

        public bool SetPen(Pen p)
        {
          this.myPen = p;
          try
          {
            this.myColor = p.Color;
          }
          catch (Exception ex)
          {
            this.myColor = Color.Black;
          }
          this.myWidth = p.Width;
          this.myDashStyle = p.DashStyle;
          this.myDashCap = p.DashCap;
          this.myDashOffset = p.DashOffset;
          if (this.myDashStyle == DashStyle.Custom)
            this.myDashPattern = p.DashPattern;
          this.myAlignment = p.Alignment;
          this.myEndCap = p.EndCap;
          this.myStartCap = p.StartCap;
          this.myLineJoin = p.LineJoin;
          this.myMiterLimit = p.MiterLimit;
          return true;
        }

        public override string ToString()
        {
          string str1 = "PenInfo: " + this.myColor.ToString() + " width " + this.myWidth.ToString((IFormatProvider) NumberFormatInfo.InvariantInfo) + " align " + this.myAlignment.ToString() + " dashstyle " + this.myDashStyle.ToString() + " dashcap " + this.myDashCap.ToString() + " dashoffset " + this.myDashOffset.ToString((IFormatProvider) NumberFormatInfo.InvariantInfo);
          if (this.myDashStyle == DashStyle.Custom && this.myDashPattern != null)
          {
            string str2 = str1 + " dashpattern{";
            for (int index = 0; index < this.myDashPattern.Length; ++index)
            {
              if (index > 0)
                str2 += ", ";
              str2 += this.myDashPattern[index].ToString((IFormatProvider) NumberFormatInfo.InvariantInfo);
            }
            str1 = str2 + "}";
          }
          return str1 + " endcap " + this.myEndCap.ToString() + " startcap " + this.myStartCap.ToString() + " join " + this.myLineJoin.ToString() + " miterlim " + this.myMiterLimit.ToString((IFormatProvider) NumberFormatInfo.InvariantInfo);
        }

        public Color Color => this.myColor;

        public float MiterLimit => this.myMiterLimit;

        public float Width => this.myWidth;
      }

      internal sealed class WeakHashRef : WeakReference
      {
        private int myHashCode;
        private bool myHashed;

        internal WeakHashRef(object target)
          : base(target)
        {
          this.myHashed = false;
          this.myHashCode = 0;
          if (target == null)
            throw new ArgumentNullException("WeakHashRef created with null Target");
        }

        public override bool Equals(object obj)
        {
          MapShape.WeakHashRef weakHashRef = obj as MapShape.WeakHashRef;
          return !this.IsAlive ? this.myHashed && weakHashRef != null && weakHashRef.myHashed && this.myHashCode == weakHashRef.myHashCode : (weakHashRef != null ? this.Target.Equals(weakHashRef.Target) : this.Target == obj);
        }

        public override int GetHashCode()
        {
          if (!this.myHashed)
          {
            this.myHashed = true;
            this.myHashCode = this.Target.GetHashCode();
          }
          return this.myHashCode;
        }
      }
    }
}
