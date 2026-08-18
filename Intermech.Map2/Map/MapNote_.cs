// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapNote_
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Drawing;


namespace Intermech.Map
{
    [Serializable]
    public class MapNote_ : MapGroup
    {
      public const int ChangedComment = 3024;
      public const int ChangedStroke = 3025;
      private MapComment _comment;
      private MapStroke _stroke;

      public MapNote_()
      {
        this.InternalFlags &= -17;
        this._stroke = this.CreateStroke();
        this.Add((MapObject) this._stroke);
        this._comment = this.CreateComment();
        this.Add((MapObject) this._comment);
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 3024:
            this.Comment = (MapComment) e.GetValue(undo);
            break;
          case 3025:
            this.Stroke = (MapStroke) e.GetValue(undo);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      protected override void CopyChildren(MapGroup newgroup, MapCopyDictionary env)
      {
        base.CopyChildren(newgroup, env);
        MapNote_ mapNote = (MapNote_) newgroup;
        mapNote._stroke = (MapStroke) env[(object) this._stroke];
        mapNote._comment = (MapComment) env[(object) this._comment];
      }

      public override void Remove(MapObject obj)
      {
        base.Remove(obj);
        if (obj == this._stroke)
        {
          this._stroke = (MapStroke) null;
        }
        else
        {
          if (obj != this._comment)
            return;
          this._comment = (MapComment) null;
        }
      }

      protected virtual MapComment CreateComment()
      {
        return new MapComment() { Text = "" };
      }

      public virtual MapComment Comment
      {
        get => this._comment;
        set
        {
          MapComment comment = this._comment;
          if (comment == value)
            return;
          if (comment != null)
            this.Remove((MapObject) comment);
          this._comment = value;
          if (value != null)
            this.Add((MapObject) value);
          this.Changed(3024, 0, (object) comment, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      protected virtual MapStroke CreateStroke() => new MapStroke();

      public virtual MapStroke Stroke
      {
        get => this._stroke;
        set
        {
          MapStroke stroke = this._stroke;
          if (stroke == value)
            return;
          if (stroke != null)
            this.Remove((MapObject) stroke);
          this._stroke = value;
          if (value != null)
            this.Add((MapObject) value);
          this.Changed(3025, 0, (object) stroke, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      public override RectangleF ExpandPaintBounds(RectangleF rect, MapView view)
      {
        MapObject.InflateRect(ref rect, 2f, 2f);
        return rect;
      }

      public override void Paint(Graphics g, MapView view) => base.Paint(g, view);

      public override void LayoutChildren(MapObject childchanged)
      {
        int num = this.Initializing ? 1 : 0;
      }
    }
}
