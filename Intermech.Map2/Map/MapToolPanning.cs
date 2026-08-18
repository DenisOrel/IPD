// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapToolPanning
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
    public class MapToolPanning : MapTool
    {
      [NonSerialized]
      private bool myActive;
      private bool myAutoPan;
      [NonSerialized]
      private Point myLastViewPoint;
      private bool myModal;
      [NonSerialized]
      private Point myOrigin;
      [NonSerialized]
      private bool myOriginSet;
      [NonSerialized]
      private PaintEventHandler myPaintHandler;

      public MapToolPanning(MapView v)
        : base(v)
      {
        this.myAutoPan = true;
        this.myModal = false;
        this.myActive = false;
        this.myOriginSet = false;
        this.myOrigin = new Point();
        this.myPaintHandler = (PaintEventHandler) null;
      }

      public override bool CanStart()
      {
        MapInputEventArgs lastInput = this.LastInput;
        if (lastInput.Alt || lastInput.Control || lastInput.Shift)
          return false;
        if (this.AutoPan)
          return lastInput.Buttons == MouseButtons.Middle;
        return lastInput.Buttons == MouseButtons.Left && this.View.PickObject(true, false, lastInput.DocPoint, true) == null;
      }

      /// <summary>действия когда клавиша клавиатуры нажата</summary>
      public override void DoKeyDown() => this.StopTool();

      private void DoManualPan()
      {
        PointF docPosition = this.View.DocPosition;
        Size s;
        ref Size local = ref s;
        Point viewPoint = this.LastInput.ViewPoint;
        int width = viewPoint.X - this.myLastViewPoint.X;
        viewPoint = this.LastInput.ViewPoint;
        int height = viewPoint.Y - this.myLastViewPoint.Y;
        local = new Size(width, height);
        SizeF doc = this.View.ConvertViewToDoc(s);
        this.myLastViewPoint = this.LastInput.ViewPoint;
        this.View.DocPosition = new PointF(docPosition.X + doc.Width, docPosition.Y + doc.Height);
      }

      /// <summary>действия когда клавиша мыши нажата</summary>
      public override void DoMouseDown()
      {
        if (this.AutoPan)
          base.DoMouseDown();
        else
          this.Active = true;
      }

      /// <summary>действия когда мышь двигают</summary>
      public override void DoMouseMove()
      {
        if (this.AutoPan)
        {
          if (!this.myOriginSet)
            return;
          Size size = new Size(16 /*0x10*/, 16 /*0x10*/);
          int width = size.Width;
          int height = size.Height;
          int x1 = this.LastInput.ViewPoint.X;
          Point point = this.Origin;
          int x2 = point.X;
          int num1 = x1 - x2;
          point = this.LastInput.ViewPoint;
          int y1 = point.Y;
          point = this.Origin;
          int y2 = point.Y;
          int num2 = y1 - y2;
          if (num1 < -width)
          {
            if (num2 < -height)
              this.View.Cursor = Cursors.PanNW;
            else if (num2 > height)
              this.View.Cursor = Cursors.PanSW;
            else
              this.View.Cursor = Cursors.PanWest;
          }
          else if (num1 > width)
          {
            if (num2 < -height)
              this.View.Cursor = Cursors.PanNE;
            else if (num2 > height)
              this.View.Cursor = Cursors.PanSE;
            else
              this.View.Cursor = Cursors.PanEast;
          }
          else if (num2 < -height)
            this.View.Cursor = Cursors.PanNorth;
          else if (num2 > height)
            this.View.Cursor = Cursors.PanSouth;
          else
            this.View.Cursor = Cursors.NoMove2D;
          this.View.DoAutoPan(this.Origin, this.LastInput.ViewPoint);
        }
        else if (!this.Active)
        {
          if (this.Modal)
            return;
          this.Active = true;
        }
        else
          this.DoManualPan();
      }

      /// <summary>действия когда клавиша мыши отпущена</summary>
      public override void DoMouseUp()
      {
        if (this.AutoPan)
        {
          if (!this.myOriginSet)
          {
            this.Origin = this.LastInput.ViewPoint;
            this.SetPaintingOriginMarker(true);
          }
          else
            this.StopTool();
        }
        else if (this.Modal)
          this.Active = false;
        else
          this.StopTool();
      }

      public override void DoMouseWheel() => this.StopTool();

      private void HandlePaint(object sender, PaintEventArgs evt)
      {
        Cursor noMove2D = Cursors.NoMove2D;
        int width = noMove2D.Size.Width;
        int height = noMove2D.Size.Height;
        noMove2D.Draw(evt.Graphics, this.OriginRect);
      }

      private void SetPaintingOriginMarker(bool b)
      {
        if (b)
        {
          this.myPaintHandler = new PaintEventHandler(this.HandlePaint);
          this.View.Paint += this.myPaintHandler;
          this.View.Invalidate(this.OriginRect);
        }
        else
        {
          if (this.myPaintHandler == null)
            return;
          this.View.Paint -= this.myPaintHandler;
          this.myPaintHandler = (PaintEventHandler) null;
          this.View.Invalidate(this.OriginRect);
        }
      }

      public override void Start()
      {
        if (this.AutoPan)
        {
          this.View.Cursor = Cursors.NoMove2D;
          if (!this.myOriginSet)
            return;
          this.SetPaintingOriginMarker(true);
        }
        else
          this.View.Cursor = Cursors.SizeAll;
      }

      public override void Stop()
      {
        if (this.AutoPan)
        {
          this.myOriginSet = false;
          this.View.StopAutoScroll();
          this.View.Cursor = this.View.DefaultCursor;
          this.SetPaintingOriginMarker(false);
        }
        else
        {
          this.Active = false;
          this.View.Cursor = this.View.DefaultCursor;
        }
      }

      private bool Active
      {
        get => this.myActive;
        set
        {
          if (this.myActive == value)
            return;
          this.myActive = value;
          if (!value)
            return;
          this.myLastViewPoint = this.LastInput.ViewPoint;
        }
      }

      public virtual bool AutoPan
      {
        get => this.myAutoPan;
        set => this.myAutoPan = value;
      }

      public virtual bool Modal
      {
        get => this.myModal;
        set => this.myModal = value;
      }

      public Point Origin
      {
        get => this.myOrigin;
        set
        {
          if (!(this.myOrigin != value))
            return;
          this.myOrigin = value;
          this.myOriginSet = true;
        }
      }

      private Rectangle OriginRect
      {
        get
        {
          Cursor noMove2D = Cursors.NoMove2D;
          int width1 = noMove2D.Size.Width;
          int height1 = noMove2D.Size.Height;
          Point origin = this.Origin;
          int x = origin.X - width1 / 2;
          origin = this.Origin;
          int y = origin.Y - height1 / 2;
          int width2 = width1;
          int height2 = height1;
          return new Rectangle(x, y, width2, height2);
        }
      }
    }
}
