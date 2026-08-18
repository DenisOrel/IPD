// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapRoundedRectangle
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
    public class MapRoundedRectangle : MapRectangle
    {
      public const int ChangedCorner = 1421;
      public const int ChangedStyle = 1422;
      private SizeF myCorner;
      private RectangleStyle _style;

      public MapRoundedRectangle()
      {
        this.myCorner = new SizeF(10f, 10f);
        this._style = RectangleStyle.BoxBluntPoint;
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
        RectangleF bounds = this.Bounds;
        SizeF corner = this.Corner;
        if (this._style == RectangleStyle.Box)
        {
          MapRoundedRectangle.MakeRoundedRectangularPath(path, offx, offy, bounds, new SizeF(0.0f, 0.0f));
          return path;
        }
        if (this._style == RectangleStyle.BoxFacet)
        {
          MapRoundedRectangle.MakeFacetRectangularPath(path, offx, offy, bounds, corner);
          return path;
        }
        MapRoundedRectangle.MakeRoundedRectangularPath(path, offx, offy, bounds, corner);
        return path;
      }

      public override GraphicsPath MakePath() => (GraphicsPath) this.GetPath(0.0f, 0.0f).Clone();

      internal static void MakeFacetRectangularPath(
        GraphicsPath path,
        float offx,
        float offy,
        RectangleF rect,
        SizeF corner)
      {
        if ((double) corner.Width > (double) rect.Width / 2.0)
          corner.Width = rect.Width / 2f;
        if ((double) corner.Height > (double) rect.Height / 2.0)
          corner.Height = rect.Height / 2f;
        rect.X += offx;
        rect.Y += offy;
        float x = rect.X;
        float num1 = x + rect.Width;
        float num2 = x + corner.Width;
        float num3 = num1 - corner.Width;
        float y = rect.Y;
        float num4 = y + rect.Height;
        float num5 = num4 - corner.Height;
        float num6 = y + corner.Height;
        path.StartFigure();
        path.AddLine(x, num6, num2, y);
        if ((double) num2 != (double) num3)
          path.AddLine(num2, y, num3, y);
        path.AddLine(num3, y, num1, num6);
        if ((double) num6 != (double) num5)
          path.AddLine(num1, num6, num1, num5);
        path.AddLine(num1, num5, num3, num4);
        if ((double) num3 != (double) num2)
          path.AddLine(num3, num4, num2, num4);
        path.AddLine(num2, num4, x, num5);
        if ((double) num5 != (double) num6)
          path.AddLine(x, num5, x, num6);
        path.CloseFigure();
      }

      internal static void MakeRoundedRectangularPath(
        GraphicsPath path,
        float offx,
        float offy,
        RectangleF rect,
        SizeF corner)
      {
        if ((double) corner.Width > (double) rect.Width / 2.0)
          corner.Width = rect.Width / 2f;
        if ((double) corner.Height > (double) rect.Height / 2.0)
          corner.Height = rect.Height / 2f;
        rect.X += offx;
        rect.Y += offy;
        float width = corner.Width * 2f;
        float height = corner.Height * 2f;
        if ((double) width > 0.0 && (double) height > 0.0)
        {
          float x = rect.X;
          float y = rect.Y;
          float num1 = x + rect.Width;
          float num2 = y + rect.Height;
          float x2 = x + width;
          float num3 = y + height;
          float num4 = num1 - width;
          float num5 = num2 - height;
          path.AddArc(num4, y, width, height, 270f, 90f);
          if ((double) num3 < (double) num5)
            path.AddLine(num1, num3, num1, num5);
          path.AddArc(num4, num5, width, height, 0.0f, 90f);
          if ((double) x2 < (double) num4)
            path.AddLine(num4, num2, x2, num2);
          path.AddArc(x, num5, width, height, 90f, 90f);
          if ((double) num3 < (double) num5)
            path.AddLine(x, num5, x, num3);
          path.AddArc(x, y, width, height, 180f, 90f);
        }
        else
          path.AddRectangle(rect);
        path.CloseAllFigures();
      }

      public override void Paint(Graphics g, MapView view)
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
          else if (this.Pen != null)
          {
            Pen shadowPen = this.GetShadowPen(view, this.InternalPenWidth);
            MapShape.DrawPath(g, view, shadowPen, (Brush) null, path);
          }
          this.DisposePath(path);
        }
        GraphicsPath path1 = this.GetPath(0.0f, 0.0f);
        MapShape.DrawPath(g, view, this.Pen, this.Brush, path1);
        this.DisposePath(path1);
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1421:
            this.Corner = e.GetSize(undo);
            break;
          case 1422:
            this.Style = (RectangleStyle) e.GetValue(undo);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      /// <summary>Максимальную ширину и высоту фаски каждого угла</summary>
      [TypeConverter(typeof (MapSizeFConverter))]
      [Description("The maximum radial width and height of each corner")]
      [Category("Appearance")]
      public virtual SizeF Corner
      {
        get => this.myCorner;
        set
        {
          SizeF corner = this.myCorner;
          if (!(corner != value) || (double) value.Width < 0.0 || (double) value.Height < 0.0)
            return;
          this.myCorner = value;
          this.ResetPath();
          this.Changed(1421, 0, (object) null, MapObject.MakeRect(corner), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      /// <summary>стили рамки</summary>
      public virtual RectangleStyle Style
      {
        get => this._style;
        set
        {
          RectangleStyle style = this._style;
          if (style == value)
            return;
          this._style = value;
          this.ResetPath();
          this.Changed(1422, 0, (object) style, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }
    }
}
