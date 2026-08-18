// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapButton
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
    public class MapButton : MapGroup, IMapLabeledNode, IMapActionObject
    {
      public const int ChangedBackground = 2901;
      public const int ChangedIcon = 2902;
      public const int ChangedLabel = 2903;
      public const int ChangedTopLeftMargin = 2904;
      public const int ChangedBottomRightMargin = 2905;
      public const int ChangedActionEnabled = 2906;
      private const int flagActionEnabled = 16777216 /*0x01000000*/;
      private bool myActionActivated;
      private MapObject myBack;
      private SizeF myBottomRightMargin;
      private MapObject myIcon;
      private MapText myLabel;
      private SizeF myTopLeftMargin;

      public event MapInputEventHandler Action;

      public MapButton()
      {
        this.myBack = (MapObject) null;
        this.myIcon = (MapObject) null;
        this.myLabel = (MapText) null;
        this.myTopLeftMargin = new SizeF(3f, 2f);
        this.myBottomRightMargin = new SizeF(2f, 3f);
        this.myActionActivated = false;
        this.InternalFlags &= -17;
        this.InternalFlags |= 16777216 /*0x01000000*/;
        this.myBack = this.CreateBackground();
        this.Add(this.myBack);
        this.myIcon = this.CreateIcon();
        this.Add(this.myIcon);
        this.myLabel = this.CreateLabel();
        this.Add((MapObject) this.myLabel);
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 2901:
            this.Background = (MapObject) e.GetValue(undo);
            break;
          case 2902:
            this.Icon = (MapObject) e.GetValue(undo);
            break;
          case 2903:
            this.Label = (MapText) e.GetValue(undo);
            break;
          case 2904:
            this.Initializing = true;
            this.TopLeftMargin = e.GetSize(undo);
            this.Initializing = false;
            break;
          case 2905:
            this.Initializing = true;
            this.BottomRightMargin = e.GetSize(undo);
            this.Initializing = false;
            break;
          case 2906:
            this.ActionEnabled = (bool) e.GetValue(undo);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      protected override void CopyChildren(MapGroup newgroup, MapCopyDictionary env)
      {
        base.CopyChildren(newgroup, env);
        MapButton mapButton = (MapButton) newgroup;
        mapButton.myBack = (MapObject) env[(object) this.myBack];
        mapButton.myIcon = (MapObject) env[(object) this.myIcon];
        mapButton.myLabel = (MapText) env[(object) this.myLabel];
        mapButton.Action = (MapInputEventHandler) null;
        mapButton.myActionActivated = false;
      }

      protected virtual MapObject CreateBackground()
      {
        MapRectangle background = new MapRectangle();
        background.Selectable = false;
        background.Pen = (Pen) null;
        background.Brush = MapShape.SystemBrushes_Control;
        return (MapObject) background;
      }

      protected virtual MapObject CreateIcon() => (MapObject) null;

      protected virtual MapText CreateLabel()
      {
        MapText label = new MapText();
        label.Selectable = false;
        return label;
      }

      public override RectangleF ExpandPaintBounds(RectangleF rect, MapView view)
      {
        MapObject.InflateRect(ref rect, 2f, 2f);
        return rect;
      }

      public override void LayoutChildren(MapObject childchanged)
      {
        if (this.Initializing)
          return;
        MapObject background = this.Background;
        MapText label = this.Label;
        MapObject icon = this.Icon;
        if (icon != null && label != null)
          icon.SetSpotLocation(64 /*0x40*/, (MapObject) label, 256 /*0x0100*/);
        if (background == null)
          return;
        RectangleF bounds = this.Bounds;
        if (label != null)
          bounds = label.Bounds;
        else if (icon != null)
          bounds = icon.Bounds;
        if (icon != null && label != null)
        {
          bounds.X -= icon.Width;
          bounds.Width += icon.Width;
          if ((double) icon.Height > (double) label.Height)
          {
            bounds.Y -= (float) (((double) icon.Height - (double) label.Height) / 2.0);
            bounds.Height = icon.Height;
          }
        }
        SizeF topLeftMargin = this.TopLeftMargin;
        SizeF bottomRightMargin = this.BottomRightMargin;
        bounds.X -= topLeftMargin.Width;
        bounds.Width += topLeftMargin.Width + bottomRightMargin.Width;
        bounds.Y -= topLeftMargin.Height;
        bounds.Height += topLeftMargin.Height + bottomRightMargin.Height;
        background.Bounds = bounds;
      }

      public virtual void OnAction(MapView view, MapInputEventArgs e)
      {
        if (this.Action == null)
          return;
        this.Action((object) this, e);
      }

      public virtual void OnActionAdjusted(MapView view, MapInputEventArgs e)
      {
      }

      public override void Paint(Graphics g, MapView view)
      {
        base.Paint(g, view);
        this.PaintButton(g, view);
      }

      protected virtual void PaintButton(Graphics g, MapView view)
      {
        RectangleF bounds = this.Bounds;
        Pen pen1;
        Pen pen2;
        Pen pen3;
        Pen pen4;
        if (this.ActionActivated)
        {
          pen1 = MapShape.SystemPens_ControlDarkDark;
          pen2 = MapShape.SystemPens_ControlLightLight;
          pen3 = MapShape.SystemPens_ControlDark;
          pen4 = MapShape.SystemPens_Control;
        }
        else
        {
          pen1 = MapShape.SystemPens_ControlLightLight;
          pen2 = MapShape.SystemPens_ControlDarkDark;
          pen3 = MapShape.SystemPens_Control;
          pen4 = MapShape.SystemPens_ControlDark;
        }
        PointF[] pointFArray = view.AllocTempPointArray(3);
        PointF pointF1 = new PointF(bounds.X + 0.5f, bounds.Y + 0.5f);
        PointF pointF2 = new PointF((float) ((double) bounds.X + (double) bounds.Width - 0.5), bounds.Y + 0.5f);
        PointF pointF3 = new PointF(bounds.X + 0.5f, (float) ((double) bounds.Y + (double) bounds.Height - 0.5));
        PointF pointF4 = new PointF((float) ((double) bounds.X + (double) bounds.Width - 0.5), (float) ((double) bounds.Y + (double) bounds.Height - 0.5));
        pointFArray[0] = pointF2;
        pointFArray[1] = pointF1;
        pointFArray[2] = pointF3;
        MapShape.DrawLines(g, view, pen3, pointFArray);
        pointFArray[0].Y -= 0.5f;
        pointFArray[1] = pointF4;
        pointFArray[2].X -= 0.5f;
        MapShape.DrawLines(g, view, pen4, pointFArray);
        --pointF1.X;
        --pointF1.Y;
        ++pointF2.X;
        --pointF2.Y;
        ++pointF4.X;
        ++pointF4.Y;
        --pointF3.X;
        ++pointF3.Y;
        pointFArray[0] = pointF2;
        pointFArray[1] = pointF1;
        pointFArray[2] = pointF3;
        MapShape.DrawLines(g, view, pen1, pointFArray);
        pointFArray[0].Y -= 0.5f;
        pointFArray[1] = pointF4;
        pointFArray[2].X -= 0.5f;
        MapShape.DrawLines(g, view, pen2, pointFArray);
        view.FreeTempPointArray(pointFArray);
      }

      public override void Remove(MapObject obj)
      {
        base.Remove(obj);
        if (obj == this.myLabel)
          this.myLabel = (MapText) null;
        else if (obj == this.myBack)
        {
          this.myBack = (MapObject) null;
        }
        else
        {
          if (obj != this.myIcon)
            return;
          this.myIcon = (MapObject) null;
        }
      }

      [Description("Whether the button appears depressed")]
      [Category("Appearance")]
      [DefaultValue(false)]
      public virtual bool ActionActivated
      {
        get => this.myActionActivated;
        set
        {
          if (this.myActionActivated == value)
            return;
          this.myActionActivated = value;
          this.InvalidateViews();
        }
      }

      [DefaultValue(true)]
      [Description("Whether the user can click on this button to perform an action")]
      [Category("Behavior")]
      public virtual bool ActionEnabled
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
          this.Changed(2906, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      public virtual MapObject Background
      {
        get => this.myBack;
        set
        {
          MapObject back = this.myBack;
          if (back == value)
            return;
          if (back != null)
            this.Remove(back);
          this.myBack = value;
          if (value != null)
          {
            value.Selectable = false;
            this.InsertBefore((MapObject) null, value);
          }
          this.Changed(2901, 0, (object) back, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Category("Appearance")]
      [Description("The margin around the icon and label inside the background at the right side and the bottom")]
      [TypeConverter(typeof (MapSizeFConverter))]
      public virtual SizeF BottomRightMargin
      {
        get => this.myBottomRightMargin;
        set
        {
          SizeF bottomRightMargin = this.myBottomRightMargin;
          if (!(bottomRightMargin != value))
            return;
          this.myBottomRightMargin = value;
          this.Changed(2905, 0, (object) null, MapObject.MakeRect(bottomRightMargin), 0, (object) null, MapObject.MakeRect(value));
          this.LayoutChildren((MapObject) null);
        }
      }

      public virtual MapObject Icon
      {
        get => this.myIcon;
        set
        {
          MapObject icon = this.myIcon;
          if (icon == value)
            return;
          if (icon != null)
            this.Remove(icon);
          this.myIcon = value;
          if (value != null)
          {
            value.Selectable = false;
            this.Add(value);
          }
          this.Changed(2902, 0, (object) icon, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      public virtual MapText Label
      {
        get => this.myLabel;
        set
        {
          MapText label = this.myLabel;
          if (label == value)
            return;
          if (label != null)
            this.Remove((MapObject) label);
          this.myLabel = value;
          if (value != null)
          {
            value.Selectable = false;
            this.Add((MapObject) value);
          }
          this.Changed(2903, 0, (object) label, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("The text string for the button")]
      [Category("Appearance")]
      [DefaultValue("")]
      public virtual string Text
      {
        get => this.Label != null ? this.Label.Text : "";
        set
        {
          if (this.Label == null)
            return;
          this.Label.Text = value;
        }
      }

      [Category("Appearance")]
      [Description("The margin around the icon and label inside the background at the left side and the top")]
      [TypeConverter(typeof (MapSizeFConverter))]
      public virtual SizeF TopLeftMargin
      {
        get => this.myTopLeftMargin;
        set
        {
          SizeF topLeftMargin = this.myTopLeftMargin;
          if (!(topLeftMargin != value))
            return;
          this.myTopLeftMargin = value;
          this.Changed(2904, 0, (object) null, MapObject.MakeRect(topLeftMargin), 0, (object) null, MapObject.MakeRect(value));
          this.LayoutChildren((MapObject) null);
        }
      }
    }
}
