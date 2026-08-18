// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapToolRubberBanding
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Drawing;


namespace Intermech.Map
{
    [Serializable]
    public class MapToolRubberBanding : MapTool
    {
      [NonSerialized]
      private bool myActive;
      [NonSerialized]
      private Rectangle myBox;
      private bool myModal;

      public MapToolRubberBanding(MapView v)
        : base(v)
      {
        this.myModal = false;
      }

      private void Activate()
      {
        this.myActive = true;
        Point viewPoint = this.FirstInput.ViewPoint;
        int x = viewPoint.X;
        viewPoint = this.FirstInput.ViewPoint;
        int y = viewPoint.Y;
        this.Box = new Rectangle(x, y, 0, 0);
        if (this.FirstInput.Shift || this.Selection.IsEmpty)
          return;
        this.Selection.Clear();
        this.View.Refresh();
      }

      public override bool CanStart()
      {
        if (!this.View.CanSelectObjects() || this.LastInput.IsContextButton)
          return false;
        Size dragSize = this.DragSize;
        Point viewPoint1 = this.FirstInput.ViewPoint;
        Point viewPoint2 = this.LastInput.ViewPoint;
        return (Math.Abs(viewPoint1.X - viewPoint2.X) > dragSize.Width / 2 || Math.Abs(viewPoint1.Y - viewPoint2.Y) > dragSize.Height / 2) && this.View.PickObject(true, false, this.FirstInput.DocPoint, true) == null;
      }

      public virtual Rectangle ComputeRubberBandBox()
      {
        Point viewPoint1 = this.FirstInput.ViewPoint;
        Point viewPoint2 = this.LastInput.ViewPoint;
        return new Rectangle(Math.Min(viewPoint2.X, viewPoint1.X), Math.Min(viewPoint2.Y, viewPoint1.Y), Math.Abs(viewPoint2.X - viewPoint1.X), Math.Abs(viewPoint2.Y - viewPoint1.Y));
      }

      /// <summary>действия когда клавиша мыши нажата</summary>
      public override void DoMouseDown()
      {
        if (!this.CanStart())
          return;
        this.Activate();
      }

      /// <summary>действия когда мышь двигают</summary>
      public override void DoMouseMove()
      {
        if (!this.myActive)
        {
          if (this.Modal)
            return;
          this.Activate();
        }
        else
        {
          Rectangle box1 = this.Box;
          this.Box = this.ComputeRubberBandBox();
          Rectangle box2 = this.Box;
          if (!(box1 != box2))
            return;
          this.View.DrawXorBox(this.Box);
        }
      }

      /// <summary>действия когда клавиша мыши отпущена</summary>
      public override void DoMouseUp()
      {
        if (this.myActive)
        {
          this.Box = this.ComputeRubberBandBox();
          this.DoRubberBand(this.Box);
        }
        this.StopTool();
      }

      public virtual void DoRubberBand(Rectangle box)
      {
        Size dragSize = this.DragSize;
        if (box.Width <= dragSize.Width / 2 && box.Height <= dragSize.Height / 2)
        {
          this.DoSelect(this.LastInput);
          this.DoClick(this.LastInput);
        }
        else
          this.View.SelectInRectangle(this.View.ConvertViewToDoc(box));
      }

      public override void Stop() => this.myActive = false;

      public Rectangle Box
      {
        get => this.myBox;
        set => this.myBox = value;
      }

      public virtual bool Modal
      {
        get => this.myModal;
        set => this.myModal = value;
      }
    }
}
