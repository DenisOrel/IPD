// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapHandle
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
    public class MapHandle : MapShape, IMapHandle
    {
      [NonSerialized]
      private Cursor myCursor;
      private int myHandleID;
      private MapObject mySelectedObject;
      private MapHandleStyle myStyle;

      public MapHandle()
      {
        this.myHandleID = 0;
        this.mySelectedObject = (MapObject) null;
        this.myStyle = MapHandleStyle.Rectangle;
        this.myCursor = (Cursor) null;
        this.Size = new SizeF(0.0f, 0.0f);
      }

      public override void AddSelectionHandles(MapSelection sel, MapObject selectedObj)
      {
      }

      private void ComputeTrianglePoints(PointF[] v)
      {
        RectangleF bounds = this.Bounds;
        switch (this.Style)
        {
          case MapHandleStyle.TriangleTopLeft:
            v[0].X = bounds.X + bounds.Width / 2f;
            v[0].Y = bounds.Y + bounds.Height;
            v[1].X = bounds.X;
            v[1].Y = bounds.Y;
            v[2].X = bounds.X + bounds.Width;
            v[2].Y = bounds.Y + bounds.Height / 2f;
            break;
          case MapHandleStyle.TriangleTopRight:
            v[0].X = bounds.X;
            v[0].Y = bounds.Y + bounds.Height / 2f;
            v[1].X = bounds.X + bounds.Width;
            v[1].Y = bounds.Y;
            v[2].X = bounds.X + bounds.Width / 2f;
            v[2].Y = bounds.Y + bounds.Height;
            break;
          case MapHandleStyle.TriangleBottomRight:
            v[0].X = bounds.X + bounds.Width / 2f;
            v[0].Y = bounds.Y;
            v[1].X = bounds.X + bounds.Width;
            v[1].Y = bounds.Y + bounds.Height;
            v[2].X = bounds.X;
            v[2].Y = bounds.Y + bounds.Height / 2f;
            break;
          case MapHandleStyle.TriangleBottomLeft:
            v[0].X = bounds.X + bounds.Width;
            v[0].Y = bounds.Y + bounds.Height / 2f;
            v[1].X = bounds.X;
            v[1].Y = bounds.Y + bounds.Height;
            v[2].X = bounds.X + bounds.Width / 2f;
            v[2].Y = bounds.Y;
            break;
          case MapHandleStyle.TriangleMiddleTop:
            v[0].X = bounds.X;
            v[0].Y = bounds.Y + bounds.Height;
            v[1].X = bounds.X + bounds.Width / 2f;
            v[1].Y = bounds.Y;
            v[2].X = bounds.X + bounds.Width;
            v[2].Y = bounds.Y + bounds.Height;
            break;
          case MapHandleStyle.TriangleMiddleRight:
            v[0].X = bounds.X;
            v[0].Y = bounds.Y;
            v[1].X = bounds.X + bounds.Width;
            v[1].Y = bounds.Y + bounds.Height / 2f;
            v[2].X = bounds.X;
            v[2].Y = bounds.Y + bounds.Height;
            break;
          case MapHandleStyle.TriangleMiddleBottom:
            v[0].X = bounds.X + bounds.Width;
            v[0].Y = bounds.Y;
            v[1].X = bounds.X + bounds.Width / 2f;
            v[1].Y = bounds.Y + bounds.Height;
            v[2].X = bounds.X;
            v[2].Y = bounds.Y;
            break;
          case MapHandleStyle.TriangleMiddleLeft:
            v[0].X = bounds.X + bounds.Width;
            v[0].Y = bounds.Y + bounds.Height;
            v[1].X = bounds.X;
            v[1].Y = bounds.Y + bounds.Height / 2f;
            v[2].X = bounds.X + bounds.Width;
            v[2].Y = bounds.Y;
            break;
        }
      }

      public override bool ContainsPoint(PointF p)
      {
        RectangleF bounds = this.Bounds;
        float internalPenWidth = this.InternalPenWidth;
        MapObject.InflateRect(ref bounds, internalPenWidth / 2f, internalPenWidth / 2f);
        if (!MapObject.ContainsRect(bounds, p))
          return false;
        if (this.HandleID != 0)
          return true;
        MapObject.InflateRect(ref bounds, -internalPenWidth, -internalPenWidth);
        return !MapObject.ContainsRect(bounds, p);
      }

      public override MapObject CopyObject(MapCopyDictionary env) => (MapObject) null;

      public virtual Cursor GetCursorForHandle(int id)
      {
        switch (id)
        {
          case 0:
            return (Cursor) null;
          case 1:
            return Cursors.SizeAll;
          case 2:
            return Cursors.SizeNWSE;
          case 3:
          case 5:
          case 6:
          case 7:
            return Cursors.SizeAll;
          case 4:
            return Cursors.SizeNESW;
          case 8:
            return Cursors.SizeNWSE;
          case 16 /*0x10*/:
            return Cursors.SizeNESW;
          case 32 /*0x20*/:
            return Cursors.SizeNS;
          case 64 /*0x40*/:
            return Cursors.SizeWE;
          case 128 /*0x80*/:
            return Cursors.SizeNS;
          case 256 /*0x0100*/:
            return Cursors.SizeWE;
          case 1024 /*0x0400*/:
            return Cursors.Hand;
          case 1025:
            return Cursors.Hand;
          default:
            return Cursors.SizeAll;
        }
      }

      public override GraphicsPath MakePath()
      {
        GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);
        RectangleF bounds = this.Bounds;
        switch (this.Style)
        {
          case MapHandleStyle.None:
            graphicsPath.AddLine(bounds.X, bounds.Y, bounds.X, bounds.Y);
            return graphicsPath;
          case MapHandleStyle.Ellipse:
            graphicsPath.AddEllipse(bounds.X, bounds.Y, bounds.Width, bounds.Height);
            return graphicsPath;
          case MapHandleStyle.Diamond:
            PointF[] points = new PointF[4];
            points[0].X = bounds.X + bounds.Width / 2f;
            points[0].Y = bounds.Y;
            points[1].X = bounds.X + bounds.Width;
            points[1].Y = bounds.Y + bounds.Height / 2f;
            points[2].X = points[0].X;
            points[2].Y = bounds.Y + bounds.Height;
            points[3].X = bounds.X;
            points[3].Y = points[1].Y;
            graphicsPath.AddPolygon(points);
            return graphicsPath;
          case MapHandleStyle.TriangleTopLeft:
          case MapHandleStyle.TriangleTopRight:
          case MapHandleStyle.TriangleBottomRight:
          case MapHandleStyle.TriangleBottomLeft:
          case MapHandleStyle.TriangleMiddleTop:
          case MapHandleStyle.TriangleMiddleRight:
          case MapHandleStyle.TriangleMiddleBottom:
          case MapHandleStyle.TriangleMiddleLeft:
            PointF[] pointFArray = new PointF[3];
            this.ComputeTrianglePoints(pointFArray);
            graphicsPath.AddPolygon(pointFArray);
            return graphicsPath;
          default:
            graphicsPath.AddRectangle(bounds);
            return graphicsPath;
        }
      }

      public override bool OnMouseOver(MapInputEventArgs evt, MapView view)
      {
        MapObject handledObject = this.HandledObject;
        if (handledObject == null || !view.CanResizeObjects() || !handledObject.CanResize() && !handledObject.CanReshape())
          return false;
        Cursor cursor = this.Cursor;
        if (cursor == (Cursor) null)
          cursor = this.GetCursorForHandle(this.HandleID);
        if (cursor == (Cursor) null)
          return false;
        if (view.Cursor != cursor)
          view.Cursor = cursor;
        return true;
      }

      public override void Paint(Graphics g, MapView view)
      {
        RectangleF bounds = this.Bounds;
        switch (this.Style)
        {
          case MapHandleStyle.None:
            break;
          case MapHandleStyle.Ellipse:
            MapShape.DrawEllipse(g, view, this.Pen, this.Brush, bounds.X, bounds.Y, bounds.Width, bounds.Height);
            break;
          case MapHandleStyle.Diamond:
            PointF[] pointFArray1 = view.AllocTempPointArray(4);
            pointFArray1[0].X = bounds.X + bounds.Width / 2f;
            pointFArray1[0].Y = bounds.Y;
            pointFArray1[1].X = bounds.X + bounds.Width;
            pointFArray1[1].Y = bounds.Y + bounds.Height / 2f;
            pointFArray1[2].X = pointFArray1[0].X;
            pointFArray1[2].Y = bounds.Y + bounds.Height;
            pointFArray1[3].X = bounds.X;
            pointFArray1[3].Y = pointFArray1[1].Y;
            MapShape.DrawPolygon(g, view, this.Pen, this.Brush, pointFArray1);
            view.FreeTempPointArray(pointFArray1);
            break;
          case MapHandleStyle.TriangleTopLeft:
          case MapHandleStyle.TriangleTopRight:
          case MapHandleStyle.TriangleBottomRight:
          case MapHandleStyle.TriangleBottomLeft:
          case MapHandleStyle.TriangleMiddleTop:
          case MapHandleStyle.TriangleMiddleRight:
          case MapHandleStyle.TriangleMiddleBottom:
          case MapHandleStyle.TriangleMiddleLeft:
            PointF[] pointFArray2 = view.AllocTempPointArray(3);
            this.ComputeTrianglePoints(pointFArray2);
            MapShape.DrawPolygon(g, view, this.Pen, this.Brush, pointFArray2);
            view.FreeTempPointArray(pointFArray2);
            break;
          default:
            MapShape.DrawRectangle(g, view, this.Pen, this.Brush, bounds.X, bounds.Y, bounds.Width, bounds.Height);
            break;
        }
      }

      [Description("The Cursor to be shown when the mouse is over this handle.")]
      public Cursor Cursor
      {
        get => this.myCursor;
        set => this.myCursor = value;
      }

      public override MapObject SelectionObject => (MapObject) null;

      [Description("The appearance style.")]
      public MapHandleStyle Style
      {
        get => this.myStyle;
        set => this.myStyle = value;
      }

      [Description("Just returns the MapHandle itself.")]
      public MapObject MapObject => (MapObject) this;

      [Description("The object that actually gets the handles.")]
      public MapObject HandledObject => this.SelectedObject.SelectionObject;

      [Description("The selected object that this handle is marking.")]
      public MapObject SelectedObject
      {
        get => this.mySelectedObject;
        set => this.mySelectedObject = value;
      }

      [Description("An identifier for this handle, often a MapObject spot value.")]
      public int HandleID
      {
        get => this.myHandleID;
        set => this.myHandleID = value;
      }
    }
}
