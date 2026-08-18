// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapBasicNode
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
    public class MapBasicNode : MapNode
    {
      public const int ChangedLabelSpot = 2101;
      public const int ChangedShape = 2102;
      public const int ChangedLabel = 2103;
      public const int ChangedPort = 2104;
      public const int ChangedMiddleLabelMargin = 2105;
      public const int ChangedAutoResizes = 2106;
      private const int flagAutoResizes = 16777216 /*0x01000000*/;
      private static readonly SizeF DefaultPortSize = new SizeF(7f, 7f);
      private static readonly SizeF DefaultShapeMargin = new SizeF(7f, 7f);
      private MapText _label;
      private int _labelSpot;
      private SizeF _middleLabelMargin;
      private MapPort _port;
      private MapShape _shape;

      public MapBasicNode()
      {
        this._shape = (MapShape) null;
        this._label = (MapText) null;
        this._port = (MapPort) null;
        this._labelSpot = 32 /*0x20*/;
        this._middleLabelMargin = new SizeF(20f, 10f);
        this.InternalFlags |= 16908288 /*0x01020000*/;
        this._port = this.CreatePort();
        this._shape = this.CreateShape(this._port);
        this.Add((MapObject) this._shape);
        this.Add((MapObject) this._port);
        if (this._port != null)
          this._port.PortObject = (MapObject) this._shape;
        this.PropertiesDelegatedToSelectionObject = true;
        this.Initializing = false;
        this.LayoutChildren((MapObject) null);
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 2101:
            this.setLabelSpot(e.GetInt(undo), true);
            break;
          case 2102:
            this.Shape = (MapShape) e.GetValue(undo);
            break;
          case 2103:
            this.Label = (MapText) e.GetValue(undo);
            break;
          case 2104:
            this.Port = (MapPort) e.GetValue(undo);
            break;
          case 2105:
            this.Initializing = true;
            this.MiddleLabelMargin = e.GetSize(undo);
            this.Initializing = false;
            break;
          case 2106:
            this.setAutoResizes((bool) e.GetValue(undo), true);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      protected override void CopyChildren(MapGroup newgroup, MapCopyDictionary env)
      {
        base.CopyChildren(newgroup, env);
        MapBasicNode mapBasicNode = (MapBasicNode) newgroup;
        mapBasicNode._shape = (MapShape) env[(object) this._shape];
        mapBasicNode._port = (MapPort) env[(object) this._port];
        mapBasicNode._label = (MapText) env[(object) this._label];
      }

      protected virtual MapText CreateLabel(string name)
      {
        MapText label = new MapText();
        label.Text = name;
        label.Selectable = false;
        return label;
      }

      protected virtual MapPort CreatePort()
      {
        MapPort port = new MapPort();
        port.Style = MapPortStyle.Ellipse;
        port.FromSpot = 0;
        port.ToSpot = 0;
        port.Size = MapBasicNode.DefaultPortSize;
        return port;
      }

      protected virtual MapShape CreateShape(MapPort p)
      {
        MapEllipse shape = new MapEllipse();
        SizeF size = p.Size;
        shape.Size = new SizeF(size.Width + 2f * MapBasicNode.DefaultShapeMargin.Width, size.Height + 2f * MapBasicNode.DefaultShapeMargin.Height);
        shape.Selectable = false;
        shape.Resizable = false;
        shape.Reshapable = false;
        shape.Brush = MapShape.Brushes_White;
        return (MapShape) shape;
      }

      public override void LayoutChildren(MapObject childchanged)
      {
        if (this.Initializing)
          return;
        MapShape shape = this.Shape;
        if (shape == null)
          return;
        MapText label = this.Label;
        if (label != null)
        {
          if (this.LabelSpot == 1)
          {
            PointF center = shape.Center;
            SizeF middleLabelMargin = this.MiddleLabelMargin;
            if (this.AutoResizes)
            {
              float width = label.Width + middleLabelMargin.Width;
              float height = label.Height + middleLabelMargin.Height;
              shape.Bounds = new RectangleF(center.X - width / 2f, center.Y - height / 2f, width, height);
            }
            else
            {
              float width = Math.Max(shape.Width - (middleLabelMargin.Width + middleLabelMargin.Width), 0.0f);
              float val2 = Math.Max(shape.Height - (middleLabelMargin.Height + middleLabelMargin.Height), 0.0f);
              label.Width = width;
              label.WrappingWidth = width;
              label.UpdateSize();
              float height = Math.Min(label.Height, val2);
              float x = shape.Left + middleLabelMargin.Width;
              float y = (float) ((double) shape.Top + (double) middleLabelMargin.Height + ((double) val2 - (double) height) / 2.0);
              label.Bounds = new RectangleF(x, y, width, height);
            }
            label.Alignment = 1;
            label.Center = center;
            if (this.Port != null)
              this.Port.Bounds = shape.Bounds;
          }
          else
          {
            label.Alignment = this.SpotOpposite(this.LabelSpot);
            label.SetSpotLocation(this.SpotOpposite(this.LabelSpot), (MapObject) shape, this.LabelSpot);
          }
        }
        if (this.Port == null)
          return;
        this.Port.SetSpotLocation(1, (MapObject) shape, 1);
      }

      public virtual void OnAutoResizesChanged(bool old)
      {
        MapText label = this.Label;
        if (label == null)
          return;
        label.Wrapping = old;
        label.Clipping = old;
      }

      public virtual void OnLabelSpotChanged(int old)
      {
        if (this.Port != null)
        {
          if (this.LabelSpot == 1)
          {
            this.Port.Style = MapPortStyle.None;
            this.Resizable = false;
          }
          else if (old == 1)
          {
            this.Port.Style = MapPortStyle.Ellipse;
            RectangleF rectangleF1;
            ref RectangleF local1 = ref rectangleF1;
            double x1 = (double) this.Shape.Center.X;
            SizeF sizeF = MapBasicNode.DefaultPortSize;
            double num1 = (double) sizeF.Width / 2.0;
            double x2 = x1 - num1;
            PointF center = this.Shape.Center;
            double y1 = (double) center.Y;
            sizeF = MapBasicNode.DefaultPortSize;
            double num2 = (double) sizeF.Height / 2.0;
            double y2 = y1 - num2;
            sizeF = MapBasicNode.DefaultPortSize;
            double width1 = (double) sizeF.Width;
            sizeF = MapBasicNode.DefaultPortSize;
            double height1 = (double) sizeF.Height;
            local1 = new RectangleF((float) x2, (float) y2, (float) width1, (float) height1);
            RectangleF rectangleF2;
            ref RectangleF local2 = ref rectangleF2;
            center = this.Shape.Center;
            double num3 = (double) center.X - (double) rectangleF1.Width / 2.0;
            sizeF = MapBasicNode.DefaultShapeMargin;
            double width2 = (double) sizeF.Width;
            double x3 = num3 - width2;
            double num4 = (double) this.Shape.Center.Y - (double) rectangleF1.Height / 2.0;
            sizeF = MapBasicNode.DefaultShapeMargin;
            double height2 = (double) sizeF.Height;
            double y3 = num4 - height2;
            double width3 = (double) rectangleF1.Width;
            sizeF = MapBasicNode.DefaultShapeMargin;
            double num5 = 2.0 * (double) sizeF.Width;
            double width4 = width3 + num5;
            double height3 = (double) rectangleF1.Height;
            sizeF = MapBasicNode.DefaultShapeMargin;
            double num6 = 2.0 * (double) sizeF.Height;
            double height4 = height3 + num6;
            local2 = new RectangleF((float) x3, (float) y3, (float) width4, (float) height4);
            this.Shape.Bounds = rectangleF2;
            this.Port.Bounds = rectangleF1;
          }
        }
        this.LayoutChildren((MapObject) this.Label);
      }

      public override void Remove(MapObject obj)
      {
        base.Remove(obj);
        if (obj == this._shape)
          this._shape = (MapShape) null;
        else if (obj == this._label)
        {
          this._label = (MapText) null;
        }
        else
        {
          if (obj != this._port)
            return;
          this._port = (MapPort) null;
        }
      }

      private void setAutoResizes(bool b, bool undoing)
      {
        bool flag = (this.InternalFlags & 16777216 /*0x01000000*/) != 0;
        if (flag == b)
          return;
        if (b)
          this.InternalFlags |= 16777216 /*0x01000000*/;
        else
          this.InternalFlags &= -16777217;
        this.Changed(2106, 0, (object) flag, MapObject.NullRect, 0, (object) b, MapObject.NullRect);
        if (undoing)
          return;
        this.OnAutoResizesChanged(flag);
      }

      private void setLabelSpot(int spot, bool undoing)
      {
        int labelSpot = this._labelSpot;
        if (labelSpot == spot)
          return;
        this._labelSpot = spot;
        this.Changed(2101, labelSpot, (object) null, MapObject.NullRect, spot, (object) null, MapObject.NullRect);
        if (undoing)
          return;
        this.OnLabelSpotChanged(labelSpot);
      }

      [Description("Whether the background changes size as the text changes")]
      [Category("Behavior")]
      [DefaultValue(true)]
      public virtual bool AutoResizes
      {
        get => (this.InternalFlags & 16777216 /*0x01000000*/) != 0;
        set => this.setAutoResizes(value, false);
      }

      [Description("The Brush used by the shape")]
      [Category("Appearance")]
      public Brush Brush
      {
        get => this.Shape.Brush;
        set => this.Shape.Brush = value;
      }

      public override MapText Label
      {
        get => this._label;
        set
        {
          MapText label = this._label;
          if (label == value)
            return;
          if (label != null)
            this.Remove((MapObject) label);
          this._label = value;
          if (value != null)
            this.Add((MapObject) value);
          this.Changed(2103, 0, (object) label, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [DefaultValue(32 /*0x20*/)]
      [Description("The spot at which any label is positioned relative to the shape")]
      [Category("Appearance")]
      public virtual int LabelSpot
      {
        get => this._labelSpot;
        set => this.setLabelSpot(value, false);
      }

      [TypeConverter(typeof (MapSizeFConverter))]
      [Description("The margin of the shape around the label, when the LabelSpot is Middle")]
      [Category("Appearance")]
      public virtual SizeF MiddleLabelMargin
      {
        get => this._middleLabelMargin;
        set
        {
          SizeF middleLabelMargin = this._middleLabelMargin;
          if (!(middleLabelMargin != value))
            return;
          this._middleLabelMargin = value;
          this.Changed(2105, 0, (object) null, MapObject.MakeRect(middleLabelMargin), 0, (object) null, MapObject.MakeRect(value));
          this.LayoutChildren((MapObject) null);
        }
      }

      [Category("Appearance")]
      [Description("The Pen used by the shape")]
      public Pen Pen
      {
        get => this.Shape.Pen;
        set => this.Shape.Pen = value;
      }

      public virtual MapPort Port
      {
        get => this._port;
        set
        {
          MapPort port = this._port;
          if (port == value)
            return;
          if (port != null)
            this.Remove((MapObject) port);
          this._port = value;
          if (value != null)
            this.Add((MapObject) value);
          this.Changed(2104, 0, (object) port, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          if (value == null || value.PortObject != null)
            return;
          value.PortObject = (MapObject) this.Shape;
        }
      }

      public override MapObject SelectionObject
      {
        get => this.Shape != null ? (MapObject) this.Shape : (MapObject) this;
      }

      public virtual MapShape Shape
      {
        get => this._shape;
        set
        {
          MapShape shape = this._shape;
          if (shape == value)
            return;
          this.CopyPropertiesFromSelectionObject((MapObject) shape, (MapObject) value);
          if (shape != null)
            this.Remove((MapObject) shape);
          this._shape = value;
          if (value != null)
            this.InsertBefore((MapObject) null, (MapObject) value);
          this.Changed(2102, 0, (object) shape, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          if (this.Port == null || this.Port.PortObject != shape)
            return;
          this.Port.PortObject = (MapObject) value;
        }
      }

      public override string Text
      {
        get => this.Label != null ? this.Label.Text : "";
        set
        {
          if (value == null)
            this.Remove((MapObject) this._label);
          else if (this.Label == null)
          {
            this._label = this.CreateLabel(value);
            this.Add((MapObject) this._label);
          }
          else
            this.Label.Text = value;
        }
      }
    }
}
