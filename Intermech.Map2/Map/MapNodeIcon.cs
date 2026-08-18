// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapNodeIcon
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.ComponentModel;
using System.Drawing;


namespace Intermech.Map
{
    [Serializable]
    public class MapNodeIcon : MapImage, IMapNodeIconConstraint
    {
      public const int ChangedMaximumIconSize = 2051;
      public const int ChangedMinimumIconSize = 2050;
      private SizeF myMaximumIconSize;
      private SizeF myMinimumIconSize;

      public MapNodeIcon()
      {
        this.myMinimumIconSize = new SizeF(1f, 1f);
        this.myMaximumIconSize = new SizeF(9999f, 9999f);
        this.InternalFlags &= -3;
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 2050:
            this.MinimumIconSize = e.GetSize(undo);
            break;
          case 2051:
            this.MaximumIconSize = e.GetSize(undo);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
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
        IMapNodeIconConstraint constraint = this.Constraint;
        SizeF minimumIconSize = constraint.MinimumIconSize;
        SizeF maximumIconSize = constraint.MaximumIconSize;
        base.DoResize(view, origRect, newPoint, whichHandle, evttype, minimumIconSize, maximumIconSize);
      }

      public override RectangleF Bounds
      {
        get => base.Bounds;
        set
        {
          IMapNodeIconConstraint constraint = this.Constraint;
          SizeF minimumIconSize = constraint.MinimumIconSize;
          SizeF maximumIconSize = constraint.MaximumIconSize;
          float width = value.Width;
          if ((double) width < (double) minimumIconSize.Width)
            width = minimumIconSize.Width;
          else if ((double) width > (double) maximumIconSize.Width)
            width = maximumIconSize.Width;
          float height = value.Height;
          if ((double) height < (double) minimumIconSize.Height)
            height = minimumIconSize.Height;
          else if ((double) height > (double) maximumIconSize.Height)
            height = maximumIconSize.Height;
          base.Bounds = new RectangleF(value.X, value.Y, width, height);
        }
      }

      public virtual IMapNodeIconConstraint Constraint
      {
        get => this.Parent is IMapNodeIconConstraint parent ? parent : (IMapNodeIconConstraint) this;
      }

      [Category("Appearance")]
      [TypeConverter(typeof (MapSizeFConverter))]
      [Description("The maximum size for the icon")]
      public virtual SizeF MaximumIconSize
      {
        get => this.myMaximumIconSize;
        set
        {
          SizeF maximumIconSize = this.myMaximumIconSize;
          if (!(maximumIconSize != value))
            return;
          this.myMaximumIconSize = value;
          this.Changed(2051, 0, (object) null, MapObject.MakeRect(maximumIconSize), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [Category("Appearance")]
      [TypeConverter(typeof (MapSizeFConverter))]
      [Description("The minimum size for the icon")]
      public virtual SizeF MinimumIconSize
      {
        get => this.myMinimumIconSize;
        set
        {
          SizeF minimumIconSize = this.myMinimumIconSize;
          if (!(minimumIconSize != value))
            return;
          this.myMinimumIconSize = value;
          this.Changed(2050, 0, (object) null, MapObject.MakeRect(minimumIconSize), 0, (object) null, MapObject.MakeRect(value));
        }
      }
    }
}
