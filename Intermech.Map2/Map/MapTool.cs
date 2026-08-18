// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapTool
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
    public abstract class MapTool : IMapTool
    {
      private Size _dragSize;
      [NonSerialized]
      private MapObject _currentObject;
      [NonSerialized]
      private string _stopTransactionName;
      [NonSerialized]
      private MapView _view;

      protected MapTool(MapView view)
      {
        this._view = view != null ? view : throw new ArgumentNullException("Every MapTool must have a non-null MapView.");
        this._currentObject = (MapObject) null;
        this._dragSize = SystemInformation.DragSize;
        this._stopTransactionName = (string) null;
      }

      public virtual bool CanStart() => true;

      public virtual void DoCancelMouse() => this.StopTool();

      public virtual bool DoClick(MapInputEventArgs evt)
      {
        return evt.DoubleClick ? this.View.DoDoubleClick(evt) : this.View.DoSingleClick(evt);
      }

      /// <summary>действия когда клавиша клавиатуры нажата</summary>
      public virtual void DoKeyDown()
      {
        if (this.LastInput.Key != Keys.Escape)
          return;
        this.DoCancelMouse();
      }

      /// <summary>действия когда клавиша мыши нажата</summary>
      public virtual void DoMouseDown()
      {
      }

      public virtual void DoMouseHover()
      {
      }

      /// <summary>действия когда мышь двигают</summary>
      public virtual void DoMouseMove()
      {
      }

      /// <summary>действия когда клавиша мыши отпущена</summary>
      public virtual void DoMouseUp() => this.StopTool();

      public virtual void DoMouseWheel()
      {
      }

      public virtual void DoSelect(MapInputEventArgs evt)
      {
        this.CurrentObject = this.View.PickObject(true, false, evt.DocPoint, true);
        if (this.CurrentObject != null)
        {
          if (evt.Control)
            this.Selection.Toggle(this.CurrentObject);
          else if (evt.Shift)
            this.Selection.Add(this.CurrentObject);
          else
            this.Selection.Select(this.CurrentObject);
        }
        else
        {
          if (evt.Control || evt.Shift)
            return;
          this.Selection.Clear();
        }
      }

      public virtual void Start()
      {
      }

      public bool StartTransaction()
      {
        this.TransactionResult = (string) null;
        return this._view.StartTransaction();
      }

      public virtual void Stop()
      {
      }

      public void StopTool()
      {
        if (this._view.Tool != this)
          return;
        this._view.Tool = (IMapTool) null;
      }

      public bool StopTransaction()
      {
        return this.TransactionResult == null ? this.View.AbortTransaction() : this.View.FinishTransaction(this.TransactionResult);
      }

      public MapObject CurrentObject
      {
        get => this._currentObject;
        set => this._currentObject = value;
      }

      internal Size DragSize
      {
        get => this._dragSize;
        set => this._dragSize = value;
      }

      public MapInputEventArgs FirstInput => this._view.FirstInput;

      public MapInputEventArgs LastInput => this._view.LastInput;

      public MapSelection Selection => this._view.Selection;

      public string TransactionResult
      {
        get => this._stopTransactionName;
        set => this._stopTransactionName = value;
      }

      public MapView View
      {
        get => this._view;
        set
        {
          if (value == null)
            return;
          this._view = value;
        }
      }

      public static SizeF SubtractPoints(PointF a, PointF b) => new SizeF(a.X - b.X, a.Y - b.Y);

      public static SizeF SubtractPoints(PointF a, SizeF b) => new SizeF(a.X - b.Width, a.Y - b.Height);

      public static SizeF SubtractPoints(SizeF a, PointF b) => new SizeF(a.Width - b.X, a.Height - b.Y);
    }
}
