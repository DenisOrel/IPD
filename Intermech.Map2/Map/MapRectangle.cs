// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapRectangle
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Drawing;


namespace Intermech.Map
{
    [Serializable]
    public class MapRectangle : MapShape
    {
      public override void Paint(Graphics g, MapView view)
      {
        RectangleF bounds = this.Bounds;
        if (this.Shadowed)
        {
          SizeF shadowOffset = this.GetShadowOffset(view);
          if (this.Brush != null)
          {
            Brush shadowBrush = this.GetShadowBrush(view);
            MapShape.DrawRectangle(g, view, (Pen) null, shadowBrush, bounds.X + shadowOffset.Width, bounds.Y + shadowOffset.Height, bounds.Width, bounds.Height);
          }
          else if (this.Pen != null)
          {
            Pen shadowPen = this.GetShadowPen(view, this.InternalPenWidth);
            MapShape.DrawRectangle(g, view, shadowPen, (Brush) null, bounds.X + shadowOffset.Width, bounds.Y + shadowOffset.Height, bounds.Width, bounds.Height);
          }
        }
        MapShape.DrawRectangle(g, view, this.Pen, this.Brush, bounds.X, bounds.Y, bounds.Width, bounds.Height);
      }
    }
}
