// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapLabeledLink
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
    public class MapLabeledLink : MapGroup, IMapLink, IMapGraphPart, IMapIdentifiablePart
    {
      public const int ChangedLink = 1311;
      public const int ChangedFromLabel = 1312;
      public const int ChangedMidLabel = 1313;
      public const int ChangedToLabel = 1314;
      public const int ChangedFromLabelCentered = 1315;
      public const int ChangedMidLabelCentered = 1316;
      public const int ChangedToLabelCentered = 1317;
      private const bool DEFAULT_ARROW_FILLED = true;
      private const float DEFAULT_ARROW_LENGTH = 10f;
      private const float DEFAULT_ARROW_SHAFT_LENGTH = 8f;
      private const float DEFAULT_ARROW_WIDTH = 8f;
      private const int flagFromLabelCentered = 16777216 /*0x01000000*/;
      private const int flagMidLabelCentered = 33554432 /*0x02000000*/;
      private const int flagToLabelCentered = 67108864 /*0x04000000*/;
      private MapObject myFromLabel;
      private MapObject myMidLabel;
      private MapLink myRealLink;
      private MapObject myToLabel;

      public MapLabeledLink()
      {
        this.myRealLink = (MapLink) null;
        this.myFromLabel = (MapObject) null;
        this.myMidLabel = (MapObject) null;
        this.myToLabel = (MapObject) null;
        this.InternalFlags &= -5;
        MapLink realLink = this.CreateRealLink();
        if (realLink == null)
          return;
        realLink.Selectable = false;
        this.RealLink = realLink;
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1311:
            this.RealLink = (MapLink) e.GetValue(undo);
            break;
          case 1312:
            this.FromLabel = (MapObject) e.GetValue(undo);
            break;
          case 1313:
            this.MidLabel = (MapObject) e.GetValue(undo);
            break;
          case 1314:
            this.ToLabel = (MapObject) e.GetValue(undo);
            break;
          case 1315:
            this.FromLabelCentered = (bool) e.GetValue(undo);
            break;
          case 1316:
            this.MidLabelCentered = (bool) e.GetValue(undo);
            break;
          case 1317:
            this.ToLabelCentered = (bool) e.GetValue(undo);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      protected override void CopyChildren(MapGroup newgroup, MapCopyDictionary env)
      {
        base.CopyChildren(newgroup, env);
        MapLabeledLink mapLabeledLink = (MapLabeledLink) newgroup;
        mapLabeledLink.myRealLink = (MapLink) env[(object) this.myRealLink];
        mapLabeledLink.myFromLabel = (MapObject) env[(object) this.myFromLabel];
        mapLabeledLink.myMidLabel = (MapObject) env[(object) this.myMidLabel];
        mapLabeledLink.myToLabel = (MapObject) env[(object) this.myToLabel];
      }

      public virtual MapLink CreateRealLink() => new MapLink();

      public IMapNode GetOtherNode(IMapNode n) => MapLink.GetOtherNode((IMapLink) this, n);

      public IMapPort GetOtherPort(IMapPort p) => MapLink.GetOtherPort((IMapLink) this, p);

      public override void LayoutChildren(MapObject childchanged)
      {
        if (this.Initializing)
          return;
        MapLink realLink = this.RealLink;
        if (realLink == null)
          return;
        int pointsCount = realLink.PointsCount;
        if (pointsCount < 2)
          return;
        MapObject fromLabel = this.FromLabel;
        if (fromLabel != null)
        {
          PointF point1 = realLink.GetPoint(0);
          PointF point2 = realLink.GetPoint(1);
          if (pointsCount == 2)
            this.PositionEndLabel(fromLabel, false, point1, point1, point2);
          else
            this.PositionEndLabel(fromLabel, false, point1, point2, realLink.GetPoint(2));
        }
        this.LayoutMidLabel(childchanged);
        MapObject toLabel = this.ToLabel;
        if (toLabel == null)
          return;
        PointF point3 = realLink.GetPoint(pointsCount - 1);
        PointF point4 = realLink.GetPoint(pointsCount - 2);
        if (pointsCount == 2)
          this.PositionEndLabel(toLabel, true, point3, point3, point4);
        else
          this.PositionEndLabel(toLabel, true, point3, point4, realLink.GetPoint(pointsCount - 3));
      }

      protected virtual void LayoutMidLabel(MapObject childchanged)
      {
        MapObject midLabel = this.MidLabel;
        if (midLabel == null)
          return;
        MapLink realLink = this.RealLink;
        int pointsCount = realLink.PointsCount;
        if (pointsCount < 2)
          return;
        if (realLink.Style == MapStrokeStyle.Bezier && pointsCount < 7)
        {
          PointF v;
          PointF w;
          MapStroke.BezierMidPoint(realLink.GetPoint(0), realLink.GetPoint(1), realLink.GetPoint(pointsCount - 2), realLink.GetPoint(pointsCount - 1), out v, out w);
          this.PositionMidLabel(midLabel, v, w);
        }
        else
        {
          int i = pointsCount / 2;
          if (pointsCount % 2 == 0)
          {
            PointF point1 = realLink.GetPoint(i - 1);
            PointF point2 = realLink.GetPoint(i);
            this.PositionMidLabel(midLabel, point1, point2);
          }
          else
          {
            PointF point3 = realLink.GetPoint(i - 1);
            PointF point4 = realLink.GetPoint(i);
            PointF point5 = realLink.GetPoint(i + 1);
            double num1 = (double) point4.X - (double) point3.X;
            float num2 = point4.Y - point3.Y;
            float num3 = point5.X - point4.X;
            float num4 = point5.Y - point4.Y;
            if (num1 * num1 + (double) num2 * (double) num2 >= (double) num3 * (double) num3 + (double) num4 * (double) num4)
              this.PositionMidLabel(midLabel, point3, point4);
            else
              this.PositionMidLabel(midLabel, point4, point5);
          }
        }
      }

      protected override void MoveChildren(RectangleF old)
      {
        bool initializing = this.Initializing;
        this.Initializing = true;
        base.MoveChildren(old);
        this.Initializing = initializing;
      }

      public virtual void OnPortChanged(
        IMapPort port,
        int subhint,
        int oldI,
        object oldVal,
        RectangleF oldRect,
        int newI,
        object newVal,
        RectangleF newRect)
      {
        if (this.RealLink != null)
          this.RealLink.OnPortChanged(port, subhint, oldI, oldVal, oldRect, newI, newVal, newRect);
        this.LayoutChildren(port == null ? (MapObject) null : port.MapObject);
      }

      public override MapObject Pick(PointF p, bool selectableOnly)
      {
        if (this.CanView())
        {
          foreach (MapObject backward in this.Backwards)
          {
            MapObject mapObject = backward.Pick(p, selectableOnly);
            if (mapObject != null)
              return mapObject;
          }
        }
        return (MapObject) null;
      }

      protected virtual void PositionEndLabel(
        MapObject lab,
        bool atEnd,
        PointF a,
        PointF b,
        PointF c)
      {
        if (!atEnd && this.FromLabelCentered || atEnd && this.ToLabelCentered)
        {
          if ((double) a.X == (double) b.X)
          {
            if ((double) a.Y < (double) b.Y)
              lab.SetSpotLocation(32 /*0x20*/, a);
            else
              lab.SetSpotLocation(128 /*0x80*/, a);
          }
          else if ((double) a.Y == (double) b.Y)
          {
            if ((double) a.X < (double) b.X)
              lab.SetSpotLocation(256 /*0x0100*/, a);
            else
              lab.SetSpotLocation(64 /*0x40*/, a);
          }
          else if ((double) a.X < (double) b.X)
          {
            if ((double) a.Y < (double) b.Y)
              lab.SetSpotLocation(2, a);
            else
              lab.SetSpotLocation(16 /*0x10*/, a);
          }
          else if ((double) a.Y < (double) b.Y)
            lab.SetSpotLocation(8, a);
          else
            lab.SetSpotLocation(4, a);
        }
        else if ((double) a.X < (double) b.X)
        {
          if ((double) b.Y <= (double) c.Y)
            lab.SetSpotLocation(16 /*0x10*/, a);
          else
            lab.SetSpotLocation(2, a);
        }
        else if ((double) a.X > (double) b.X)
        {
          if ((double) b.Y <= (double) c.Y)
            lab.SetSpotLocation(8, a);
          else
            lab.SetSpotLocation(4, a);
        }
        else if ((double) a.Y < (double) b.Y)
        {
          if ((double) b.X <= (double) c.X)
            lab.SetSpotLocation(4, a);
          else
            lab.SetSpotLocation(2, a);
        }
        else if ((double) a.Y > (double) b.Y)
        {
          if ((double) b.X <= (double) c.X)
            lab.SetSpotLocation(8, a);
          else
            lab.SetSpotLocation(16 /*0x10*/, a);
        }
        else if ((double) b.X <= (double) c.X)
        {
          if ((double) b.Y <= (double) c.Y)
            lab.SetSpotLocation(16 /*0x10*/, b);
          else
            lab.SetSpotLocation(2, b);
        }
        else if ((double) b.Y <= (double) c.Y)
          lab.SetSpotLocation(8, b);
        else
          lab.SetSpotLocation(4, b);
      }

      protected virtual void PositionMidLabel(MapObject lab, PointF a, PointF b)
      {
        PointF newp = new PointF((float) (((double) a.X + (double) b.X) / 2.0), (float) (((double) a.Y + (double) b.Y) / 2.0));
        int spot = 1;
        if (!this.MidLabelCentered)
          spot = (double) a.X >= (double) b.X ? ((double) Math.Abs(a.Y - b.Y) >= 1.0 ? ((double) a.Y >= (double) b.Y ? 4 : 2) : 32 /*0x20*/) : ((double) Math.Abs(a.Y - b.Y) >= 1.0 ? ((double) a.Y >= (double) b.Y ? 8 : 16 /*0x10*/) : 128 /*0x80*/);
        lab.SetSpotLocation(spot, newp);
      }

      public override void Remove(MapObject obj)
      {
        if (obj == null)
          return;
        if (obj == this.RealLink)
          this.RealLink = (MapLink) null;
        else if (obj == this.FromLabel)
          this.FromLabel = (MapObject) null;
        else if (obj == this.MidLabel)
        {
          this.MidLabel = (MapObject) null;
        }
        else
        {
          if (obj != this.ToLabel)
            return;
          this.ToLabel = (MapObject) null;
        }
      }

      public virtual void Unlink() => this.Layer?.Remove((MapObject) this);

      [DefaultValue(0)]
      [Description("How CalculateStroke behaves.")]
      [Category("Behavior")]
      public virtual MapLinkAdjustingStyle AdjustingStyle
      {
        get => this.RealLink.AdjustingStyle;
        set => this.RealLink.AdjustingStyle = value;
      }

      [DefaultValue(false)]
      [Category("Appearance")]
      [Description("Whether an Orthogonal link tries to avoid crossing over any nodes.")]
      public bool AvoidsNodes
      {
        get => this.RealLink.AvoidsNodes;
        set => this.RealLink.AvoidsNodes = value;
      }

      [Description("The brush used to fill any arrowhead.")]
      [Category("Appearance")]
      public Brush Brush
      {
        get => this.RealLink.Brush;
        set => this.RealLink.Brush = value;
      }

      [DefaultValue(10f)]
      [Category("Appearance")]
      [Description("How rounded corners are for strokes of style RoundedLine")]
      public float Curviness
      {
        get => this.RealLink.Curviness;
        set => this.RealLink.Curviness = value;
      }

      [Category("Appearance")]
      [DefaultValue(false)]
      [Description("Whether an arrow is drawn at the start of this stroke.")]
      public bool FromArrow
      {
        get => this.RealLink.FromArrow;
        set => this.RealLink.FromArrow = value;
      }

      [Category("Appearance")]
      [DefaultValue(true)]
      [Description("Whether the arrowhead is filled with the stroke's brush")]
      public bool FromArrowFilled
      {
        get => this.RealLink.FromArrowFilled;
        set => this.RealLink.FromArrowFilled = value;
      }

      [Category("Appearance")]
      [DefaultValue(10f)]
      [Description("The length of the arrowhead at the start of this stroke, along the shaft from the end point to the widest point.")]
      public float FromArrowLength
      {
        get => this.RealLink.FromArrowLength;
        set => this.RealLink.FromArrowLength = value;
      }

      [Description("The length of the arrow along the shaft at the start of this stroke.")]
      [DefaultValue(8f)]
      [Category("Appearance")]
      public float FromArrowShaftLength
      {
        get => this.RealLink.FromArrowShaftLength;
        set => this.RealLink.FromArrowShaftLength = value;
      }

      [Description("The general shape of the arrowhead at the start of this stroke.")]
      [Category("Appearance")]
      [DefaultValue(0)]
      public MapStrokeArrowheadStyle FromArrowStyle
      {
        get => this.RealLink.FromArrowStyle;
        set => this.RealLink.FromArrowStyle = value;
      }

      [Category("Appearance")]
      [DefaultValue(8f)]
      [Description("The width at its widest point of the arrowhead at the start of this stroke.")]
      public float FromArrowWidth
      {
        get => this.RealLink.FromArrowWidth;
        set => this.RealLink.FromArrowWidth = value;
      }

      [Description("The label object associated with the source end of the link.")]
      [Category("Labels")]
      public virtual MapObject FromLabel
      {
        get => this.myFromLabel;
        set
        {
          MapObject fromLabel = this.myFromLabel;
          if (fromLabel == value)
            return;
          if (fromLabel != null)
            base.Remove(fromLabel);
          this.myFromLabel = value;
          if (value != null)
          {
            this.Add(value);
            if (value == this.MidLabel)
            {
              this.myMidLabel = (MapObject) null;
              this.Changed(1313, 0, (object) value, MapObject.NullRect, 0, (object) null, MapObject.NullRect);
            }
            else if (value == this.ToLabel)
            {
              this.myToLabel = (MapObject) null;
              this.Changed(1314, 0, (object) value, MapObject.NullRect, 0, (object) null, MapObject.NullRect);
            }
          }
          this.Changed(1312, 0, (object) fromLabel, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [DefaultValue(false)]
      [Category("Labels")]
      [Description("Whether the label at the start (or source end) of the link is positioned on top of the stroke")]
      public virtual bool FromLabelCentered
      {
        get => (this.InternalFlags & 16777216 /*0x01000000*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 16777216 /*0x01000000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 16777216 /*0x01000000*/;
          else
            this.InternalFlags &= -16777217;
          this.Changed(1315, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          this.LayoutChildren(this.FromLabel);
        }
      }

      [Description("The node that the link is coming from.")]
      public virtual IMapNode FromNode => this.myRealLink.FromNode;

      [Description("The port that the link is coming from.")]
      [DefaultValue(null)]
      public virtual IMapPort FromPort
      {
        get => this.myRealLink.FromPort;
        set => this.myRealLink.FromPort = value;
      }

      [Description("Returns itself as a MapObject.")]
      public MapObject MapObject
      {
        get => (MapObject) this;
        set
        {
        }
      }

      [DefaultValue(false)]
      [Category("Appearance")]
      [Description("Whether a highlight is shown along the path of this stroke.")]
      public bool Highlight
      {
        get => this.RealLink.Highlight;
        set => this.RealLink.Highlight = value;
      }

      [Category("Appearance")]
      [DefaultValue(null)]
      [Description("The pen used to draw the highlight.")]
      public Pen HighlightPen
      {
        get => this.RealLink.HighlightPen;
        set => this.RealLink.HighlightPen = value;
      }

      [DefaultValue(0)]
      [Category("Appearance")]
      [Description("The width of the pen used to highlight the stroke.")]
      public float HighlightPenWidth
      {
        get => this.RealLink.HighlightPenWidth;
        set => this.RealLink.HighlightPenWidth = value;
      }

      [Description("Whether the highlight is shown when this stroke becomes selected.")]
      [Category("Behavior")]
      [DefaultValue(false)]
      public bool HighlightWhenSelected
      {
        get => this.RealLink.HighlightWhenSelected;
        set => this.RealLink.HighlightWhenSelected = value;
      }

      [Category("Labels")]
      [Description("The label object associated with the middle of the link.")]
      public virtual MapObject MidLabel
      {
        get => this.myMidLabel;
        set
        {
          MapObject midLabel = this.myMidLabel;
          if (midLabel == value)
            return;
          if (midLabel != null)
            base.Remove(midLabel);
          this.myMidLabel = value;
          if (value != null)
          {
            this.Add(value);
            if (value == this.FromLabel)
            {
              this.myFromLabel = (MapObject) null;
              this.Changed(1312, 0, (object) value, MapObject.NullRect, 0, (object) null, MapObject.NullRect);
            }
            else if (value == this.ToLabel)
            {
              this.myToLabel = (MapObject) null;
              this.Changed(1314, 0, (object) value, MapObject.NullRect, 0, (object) null, MapObject.NullRect);
            }
          }
          this.Changed(1313, 0, (object) midLabel, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("Whether the label at the middle of the link is positioned on top of the stroke")]
      [Category("Labels")]
      [DefaultValue(false)]
      public virtual bool MidLabelCentered
      {
        get => (this.InternalFlags & 33554432 /*0x02000000*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 33554432 /*0x02000000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 33554432 /*0x02000000*/;
          else
            this.InternalFlags &= -33554433;
          this.Changed(1316, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          this.LayoutChildren(this.MidLabel);
        }
      }

      [Description("The Orthogonal property of the RealLink.")]
      [Category("Appearance")]
      [DefaultValue(false)]
      public bool Orthogonal
      {
        get => this.RealLink.Orthogonal;
        set => this.RealLink.Orthogonal = value;
      }

      [Category("Ownership")]
      [Description("The unique ID of this part in its document.")]
      public virtual int PartID
      {
        get => this.RealLink.PartID;
        set => this.RealLink.PartID = value;
      }

      [Category("Appearance")]
      [Description("The pen used to draw the stroke.")]
      public Pen Pen
      {
        get => this.RealLink.Pen;
        set => this.RealLink.Pen = value;
      }

      [Category("Appearance")]
      [DefaultValue(0)]
      [Description("The width of the pen used to draw the stroke.")]
      public float PenWidth
      {
        get => this.RealLink.PenWidth;
        set => this.RealLink.PenWidth = value;
      }

      [Description("The MapLink object in this group.")]
      public virtual MapLink RealLink
      {
        get => this.myRealLink;
        set
        {
          MapLink realLink = this.myRealLink;
          if (realLink == value)
            return;
          if (realLink != null)
          {
            realLink.AbstractLink = (IMapLink) realLink;
            base.Remove((MapObject) realLink);
          }
          this.myRealLink = value;
          if (value != null)
          {
            this.Add((MapObject) value);
            value.AbstractLink = (IMapLink) this;
          }
          this.Changed(1311, 0, (object) realLink, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("The Relinkable property of the RealLink.")]
      [DefaultValue(true)]
      [Category("Behavior")]
      public bool Relinkable
      {
        get => this.RealLink.Relinkable;
        set => this.RealLink.Relinkable = value;
      }

      [DefaultValue(true)]
      [Category("Behavior")]
      [Description("The Reshapable property of the RealLink.")]
      public override bool Reshapable
      {
        get => this.RealLink.Reshapable;
        set => this.RealLink.Reshapable = value;
      }

      [Description("The Resizable property of the RealLink.")]
      [Category("Behavior")]
      [DefaultValue(true)]
      public override bool Resizable
      {
        get => this.RealLink.Resizable;
        set => this.RealLink.Resizable = value;
      }

      public override MapObject SelectionObject => (MapObject) this.RealLink;

      public override bool Shadowed
      {
        get => this.SelectionObject.Shadowed;
        set => this.SelectionObject.Shadowed = value;
      }

      [Description("The Style property of the RealLink.")]
      [DefaultValue(0)]
      [Category("Appearance")]
      public virtual MapStrokeStyle Style
      {
        get => this.RealLink.Style;
        set => this.RealLink.Style = value;
      }

      [DefaultValue(false)]
      [Description("Whether an arrow is drawn at the end of this stroke.")]
      [Category("Appearance")]
      public bool ToArrow
      {
        get => this.RealLink.ToArrow;
        set => this.RealLink.ToArrow = value;
      }

      [Description("Whether the arrowhead is filled with the stroke's brush")]
      [DefaultValue(true)]
      [Category("Appearance")]
      public bool ToArrowFilled
      {
        get => this.RealLink.ToArrowFilled;
        set => this.RealLink.ToArrowFilled = value;
      }

      [Description("The length of the arrow at the end of this stroke, along the shaft from the end point to the widest point.")]
      [Category("Appearance")]
      [DefaultValue(10f)]
      public float ToArrowLength
      {
        get => this.RealLink.ToArrowLength;
        set => this.RealLink.ToArrowLength = value;
      }

      [Category("Appearance")]
      [Description("The length of the arrow along the shaft at the end of this stroke.")]
      [DefaultValue(8f)]
      public float ToArrowShaftLength
      {
        get => this.RealLink.ToArrowShaftLength;
        set => this.RealLink.ToArrowShaftLength = value;
      }

      [Category("Appearance")]
      [DefaultValue(0)]
      [Description("The general shape of the arrowhead at the end of this stroke.")]
      public MapStrokeArrowheadStyle ToArrowStyle
      {
        get => this.RealLink.ToArrowStyle;
        set => this.RealLink.ToArrowStyle = value;
      }

      [Category("Appearance")]
      [Description("The width of the arrowhead at the widest point.")]
      [DefaultValue(8f)]
      public float ToArrowWidth
      {
        get => this.RealLink.ToArrowWidth;
        set => this.RealLink.ToArrowWidth = value;
      }

      [Category("Labels")]
      [Description("The label object associated with the destination end of the link.")]
      public virtual MapObject ToLabel
      {
        get => this.myToLabel;
        set
        {
          MapObject toLabel = this.myToLabel;
          if (toLabel == value)
            return;
          if (toLabel != null)
            base.Remove(toLabel);
          this.myToLabel = value;
          if (value != null)
          {
            this.Add(value);
            if (value == this.MidLabel)
            {
              this.myMidLabel = (MapObject) null;
              this.Changed(1313, 0, (object) value, MapObject.NullRect, 0, (object) null, MapObject.NullRect);
            }
            else if (value == this.FromLabel)
            {
              this.myFromLabel = (MapObject) null;
              this.Changed(1312, 0, (object) value, MapObject.NullRect, 0, (object) null, MapObject.NullRect);
            }
          }
          this.Changed(1314, 0, (object) toLabel, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [DefaultValue(false)]
      [Description("Whether the label at the destination end of the link is positioned on top of the stroke")]
      [Category("Labels")]
      public virtual bool ToLabelCentered
      {
        get => (this.InternalFlags & 67108864 /*0x04000000*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 67108864 /*0x04000000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 67108864 /*0x04000000*/;
          else
            this.InternalFlags &= -67108865;
          this.Changed(1317, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          this.LayoutChildren(this.ToLabel);
        }
      }

      [Description("The node that the link is going to.")]
      public virtual IMapNode ToNode => this.myRealLink.ToNode;

      [DefaultValue(null)]
      [Description("The port that the link is going to.")]
      public virtual IMapPort ToPort
      {
        get => this.myRealLink.ToPort;
        set => this.myRealLink.ToPort = value;
      }

      [Description("An integer value associated with this port.")]
      [DefaultValue(0)]
      public virtual int UserFlags
      {
        get => this.myRealLink.UserFlags;
        set => this.myRealLink.UserFlags = value;
      }

      [Description("An object associated with this port.")]
      [DefaultValue(null)]
      public virtual object UserObject
      {
        get => this.myRealLink.UserObject;
        set => this.myRealLink.UserObject = value;
      }
    }
}
