// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapToolResizing
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Drawing;


namespace Intermech.Map
{
    [Serializable]
    public class MapToolResizing : MapTool
    {
      private SizeF myMaximumSize;
      private SizeF myMinimumSize;
      [NonSerialized]
      private RectangleF myOriginalBounds;
      [NonSerialized]
      private PointF myOriginalPoint;
      [NonSerialized]
      private IMapHandle myResizeHandle;
      [NonSerialized]
      private MapObject mySelectedObject;
      [NonSerialized]
      private bool mySelectionHidden;

      public MapToolResizing(MapView v)
        : base(v)
      {
        this.myMinimumSize = new SizeF(0.01f, 0.01f);
        this.myMaximumSize = new SizeF(1E+21f, 1E+21f);
        this.myResizeHandle = (IMapHandle) null;
        this.mySelectionHidden = false;
        this.mySelectedObject = (MapObject) null;
      }

      public override bool CanStart()
      {
        if (this.FirstInput.IsContextButton || !this.View.CanResizeObjects())
          return false;
        IMapHandle mapHandle = this.PickResizeHandle(this.FirstInput.DocPoint);
        if (mapHandle == null || mapHandle.HandledObject == null)
          return false;
        return mapHandle.HandledObject.CanResize() || mapHandle.HandledObject.CanReshape();
      }

      public override void DoCancelMouse()
      {
        if (this.CurrentObject != null)
          this.CurrentObject.DoResize(this.View, this.OriginalBounds, this.OriginalPoint, this.ResizeHandle.HandleID, MapInputState.Cancel, this.MinimumSize, this.MaximumSize);
        this.TransactionResult = (string) null;
        this.StopTool();
      }

      /// <summary>действия когда мышь двигают</summary>
      public override void DoMouseMove() => this.DoResizing(MapInputState.Continue);

      /// <summary>действия когда клавиша мыши отпущена</summary>
      public override void DoMouseUp()
      {
        this.DoResizing(MapInputState.Finish);
        this.TransactionResult = "Resize";
        this.StopTransaction();
        this.View.RaiseObjectResized(this.CurrentObject);
        this.View.OnViewChanged();
        this.StopTool();
      }

      public virtual void DoResizing(MapInputState evttype)
      {
        if (this.CurrentObject == null)
          return;
        MapInputEventArgs lastInput = this.LastInput;
        switch (this.View.GridSnapResize)
        {
          case MapViewSnapStyle.Jump:
            lastInput.DocPoint = this.View.FindNearestGridPoint(lastInput.DocPoint);
            lastInput.ViewPoint = this.View.ConvertDocToView(lastInput.DocPoint);
            break;
          case MapViewSnapStyle.After:
            if (evttype != MapInputState.Finish)
              break;
            goto case MapViewSnapStyle.Jump;
        }
        this.CurrentObject.DoResize(this.View, this.OriginalBounds, lastInput.DocPoint, this.ResizeHandle.HandleID, evttype, this.MinimumSize, this.MaximumSize);
      }

      public virtual IMapHandle PickResizeHandle(PointF dc)
      {
        return this.View.PickObject(false, true, dc, true) as IMapHandle;
      }

      public override void Start()
      {
        IMapHandle mapHandle = this.PickResizeHandle(this.FirstInput.DocPoint);
        if (mapHandle == null)
          return;
        MapObject handledObject = mapHandle.HandledObject;
        if (handledObject == null)
          return;
        this.CurrentObject = handledObject;
        this.StartTransaction();
        if (this.Selection.GetHandleCount(handledObject) > 0)
        {
          this.mySelectionHidden = true;
          this.mySelectedObject = mapHandle.SelectedObject;
          handledObject.RemoveSelectionHandles(this.Selection);
        }
        this.ResizeHandle = mapHandle;
        this.OriginalBounds = handledObject.Bounds;
        this.OriginalPoint = mapHandle.MapObject.GetSpotLocation(1);
      }

      public override void Stop()
      {
        if (this.mySelectionHidden)
        {
          this.mySelectionHidden = false;
          MapObject currentObject = this.CurrentObject;
          if (currentObject != null && currentObject.Document == this.View.Document)
          {
            if (!this.Selection.Contains(this.mySelectedObject))
              this.Selection.Add(this.mySelectedObject);
            else
              currentObject.AddSelectionHandles(this.Selection, this.mySelectedObject);
          }
        }
        this.mySelectedObject = (MapObject) null;
        this.CurrentObject = (MapObject) null;
        this.ResizeHandle = (IMapHandle) null;
        this.StopTransaction();
      }

      public virtual SizeF MaximumSize
      {
        get => this.myMaximumSize;
        set => this.myMaximumSize = value;
      }

      public virtual SizeF MinimumSize
      {
        get => this.myMinimumSize;
        set => this.myMinimumSize = value;
      }

      public RectangleF OriginalBounds
      {
        get => this.myOriginalBounds;
        set => this.myOriginalBounds = value;
      }

      public PointF OriginalPoint
      {
        get => this.myOriginalPoint;
        set => this.myOriginalPoint = value;
      }

      public IMapHandle ResizeHandle
      {
        get => this.myResizeHandle;
        set => this.myResizeHandle = value;
      }
    }
}
