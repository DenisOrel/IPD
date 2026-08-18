// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapSubGraphHandle
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Intermech.Map
{
    [Serializable]
    public class MapSubGraphHandle : MapRectangle
    {
      public MapSubGraphHandle()
      {
        this.Size = new SizeF(10f, 10f);
        this.Brush = MapShape.Brushes_Gold;
        this.Pen = MapShape.Pens_Black;
        this.Selectable = false;
        this.Resizable = false;
        this.AutoRescales = false;
      }

      public override GraphicsPath MakePath()
      {
        GraphicsPath graphicsPath = base.MakePath();
        if (this.Parent is MapSubGraph parent)
        {
          RectangleF bounds = this.Bounds;
          if (parent.Collapsible)
          {
            float num1 = bounds.Y + bounds.Height / 2f;
            graphicsPath.StartFigure();
            graphicsPath.AddLine(bounds.X + bounds.Width / 4f, num1, bounds.X + (float) ((double) bounds.Width * 3.0 / 4.0), num1);
            if (!parent.IsExpanded)
            {
              float num2 = bounds.X + bounds.Width / 2f;
              graphicsPath.StartFigure();
              graphicsPath.AddLine(num2, bounds.Y + bounds.Height / 4f, num2, bounds.Y + (float) ((double) bounds.Height * 3.0 / 4.0));
            }
            return graphicsPath;
          }
          graphicsPath.AddEllipse(bounds.X + bounds.Width / 4f, bounds.Y + bounds.Height / 4f, bounds.Width / 2f, bounds.Height / 2f);
        }
        return graphicsPath;
      }

      public override bool OnSingleClick(MapInputEventArgs evt, MapView view)
      {
        if (!(this.Parent is MapSubGraph parent) || !parent.Collapsible)
          return false;
        view?.StartTransaction();
        string tname;
        if (parent.IsExpanded)
        {
          parent.Collapse();
          tname = "Collapsed SubGraph";
        }
        else if (evt.Control)
        {
          parent.ExpandAll();
          tname = "Expanded All SubGraphs";
        }
        else
        {
          parent.Expand();
          tname = "Expanded SubGraph";
        }
        view?.FinishTransaction(tname);
        return true;
      }

      public override void Paint(Graphics g, MapView view)
      {
        base.Paint(g, view);
        this.PaintHandle(g, view);
      }

      protected virtual void PaintHandle(Graphics g, MapView view)
      {
        if (!(this.Parent is MapSubGraph parent))
          return;
        RectangleF bounds = this.Bounds;
        if (parent.Collapsible)
        {
          float num1 = bounds.Y + bounds.Height / 2f;
          MapShape.DrawLine(g, view, this.Pen, bounds.X + bounds.Width / 4f, num1, bounds.X + (float) ((double) bounds.Width * 3.0 / 4.0), num1);
          if (parent.IsExpanded)
            return;
          float num2 = bounds.X + bounds.Width / 2f;
          MapShape.DrawLine(g, view, this.Pen, num2, bounds.Y + bounds.Height / 4f, num2, bounds.Y + (float) ((double) bounds.Height * 3.0 / 4.0));
        }
        else
          MapShape.DrawEllipse(g, view, this.Pen, (Brush) null, bounds.X + bounds.Width / 4f, bounds.Y + bounds.Height / 4f, bounds.Width / 2f, bounds.Height / 2f);
      }
    }
}
