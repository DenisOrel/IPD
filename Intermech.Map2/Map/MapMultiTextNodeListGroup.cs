// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapMultiTextNodeListGroup
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Map
{
    [Serializable]
    internal sealed class MapMultiTextNodeListGroup : MapListGroup
    {
      public override void Changed(
        int subhint,
        int oldI,
        object oldVal,
        RectangleF oldRect,
        int newI,
        object newVal,
        RectangleF newRect)
      {
        base.Changed(subhint, oldI, oldVal, oldRect, newI, newVal, newRect);
        switch (subhint)
        {
          case 1051:
            this.Parent?.LayoutChildren((MapObject) null);
            break;
          case 1052:
            if (!(this.Parent is MapMultiTextNode parent))
              break;
            parent.RemoveOnlyPorts(oldI);
            break;
        }
      }

      public override float LayoutItem(int i, RectangleF cell)
      {
        if (this.MTN == null || (double) this.MTN.ItemWidth <= 0.0)
          return base.LayoutItem(i, cell);
        float itemWidth = this.MTN.ItemWidth;
        float y = cell.Y;
        MapObject mapObject = this[i];
        if (mapObject != null)
        {
          if (mapObject.CanView())
          {
            mapObject.Bounds = new RectangleF(cell.X, cell.Y, itemWidth, mapObject.Height);
            return y + mapObject.Height;
          }
          mapObject.Position = new PointF(cell.X, cell.Y);
        }
        return y;
      }

      public MapMultiTextNode MTN => (MapMultiTextNode) this.Parent;

      public override Orientation Orientation
      {
        get => Orientation.Vertical;
        set
        {
        }
      }
    }
}
