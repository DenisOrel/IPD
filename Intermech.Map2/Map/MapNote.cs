// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapNote
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
    public class MapNote : MapGroup, IMapLabeledNode, IMapIdentifiablePart
    {
      public const int ChangedTopLeftMargin = 2301;
      public const int ChangedBottomRightMargin = 2302;
      public const int ChangedPartID = 2303 /*0x08FF*/;
      private SizeF myBottomRightMargin;
      private SizeF myTopLeftMargin;
      private MapObject myBack;
      private MapText myLabel;
      private int myPartID = -1;

      public MapNote()
      {
        this.myTopLeftMargin = new SizeF(4f, 2f);
        this.myBottomRightMargin = new SizeF(4f, 2f);
        this.InternalFlags &= -17;
        this.myBack = this.CreateBackground();
        this.Add(this.myBack);
        this.myLabel = this.CreateLabel();
        this.Add((MapObject) this.myLabel);
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 2301:
            this.Initializing = true;
            this.TopLeftMargin = e.GetSize(undo);
            this.Initializing = false;
            break;
          case 2302:
            this.Initializing = true;
            this.BottomRightMargin = e.GetSize(undo);
            this.Initializing = false;
            break;
          case 2303 /*0x08FF*/:
            this.PartID = e.GetInt(undo);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      protected override void CopyChildren(MapGroup newgroup, MapCopyDictionary env)
      {
        base.CopyChildren(newgroup, env);
        MapNote mapNote = (MapNote) newgroup;
        mapNote.myPartID = -1;
        mapNote.myBack = (MapObject) env[(object) this.myBack];
        mapNote.myLabel = (MapText) env[(object) this.myLabel];
      }

      protected virtual MapObject CreateBackground()
      {
        MapRectangle background = new MapRectangle();
        background.Shadowed = true;
        background.Selectable = false;
        background.Pen = MapShape.Pens_LightGray;
        background.Brush = MapShape.Brushes_LemonChiffon;
        return (MapObject) background;
      }

      protected virtual MapText CreateLabel()
      {
        MapText label = new MapText();
        label.Selectable = false;
        label.Multiline = true;
        label.Editable = true;
        this.Editable = true;
        return label;
      }

      public override void DoBeginEdit(MapView view)
      {
        if (this.Label == null)
          return;
        this.Label.DoBeginEdit(view);
      }

      public override void LayoutChildren(MapObject childchanged)
      {
        if (this.Initializing)
          return;
        MapText label = this.Label;
        if (label == null)
          return;
        MapObject background = this.Background;
        if (background == null)
          return;
        SizeF topLeftMargin = this.TopLeftMargin;
        SizeF bottomRightMargin = this.BottomRightMargin;
        background.Bounds = new RectangleF(label.Left - topLeftMargin.Width, label.Top - topLeftMargin.Height, label.Width + topLeftMargin.Width + bottomRightMargin.Width, label.Height + topLeftMargin.Height + bottomRightMargin.Height);
      }

      public override void Remove(MapObject obj)
      {
        base.Remove(obj);
        if (obj == this.myLabel)
        {
          this.myLabel = (MapText) null;
        }
        else
        {
          if (obj != this.myBack)
            return;
          this.myBack = (MapObject) null;
        }
      }

      public MapObject Background => this.myBack;

      [Description("The margin around the text inside the background at the right side and the bottom")]
      [Category("Appearance")]
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
          this.Changed(2302, 0, (object) null, MapObject.MakeRect(bottomRightMargin), 0, (object) null, MapObject.MakeRect(value));
          this.LayoutChildren((MapObject) null);
        }
      }

      [Description("The unique ID of this part in its document.")]
      [Category("Ownership")]
      public int PartID
      {
        get => this.myPartID;
        set
        {
          int partId = this.myPartID;
          if (partId == value)
            return;
          this.myPartID = value;
          this.Changed(2303 /*0x08FF*/, partId, (object) null, MapObject.NullRect, value, (object) null, MapObject.NullRect);
        }
      }

      public override bool Shadowed
      {
        get => this.Background != null ? this.Background.Shadowed : base.Shadowed;
        set
        {
          if (this.Background != null)
            this.Background.Shadowed = value;
          else
            base.Shadowed = value;
        }
      }

      [Category("Appearance")]
      [Description("The margin around the text inside the background at the left side and the top")]
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
          this.Changed(2301, 0, (object) null, MapObject.MakeRect(topLeftMargin), 0, (object) null, MapObject.MakeRect(value));
          this.LayoutChildren((MapObject) null);
        }
      }

      public virtual MapText Label => this.myLabel;

      public virtual string Text
      {
        get => this.Label.Text;
        set => this.Label.Text = value;
      }
    }
}
