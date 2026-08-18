// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapToolDragging
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;
using System.Drawing;
using System.Security;
using System.Windows.Forms;


namespace Intermech.Map
{
    [Serializable]
    public class MapToolDragging : MapTool
    {
      private bool myCopiesEffectiveSelection;
      [NonSerialized]
      private MapSelection myDragSelection;
      [NonSerialized]
      private MapObject myDragSelectionOrigObj;
      [NonSerialized]
      private MapSelection myEffectiveSelection;
      private bool myHidesSelectionHandles;
      [NonSerialized]
      private bool myModalDropped;
      [NonSerialized]
      private SizeF myMoveOffset;
      [NonSerialized]
      private bool mySelectionHidden;
      [NonSerialized]
      internal bool mySelectionSet;

      public MapToolDragging(MapView v)
        : base(v)
      {
        this.myCopiesEffectiveSelection = false;
        this.myHidesSelectionHandles = true;
        this.myEffectiveSelection = (MapSelection) null;
        this.myDragSelection = (MapSelection) null;
        this.myDragSelectionOrigObj = (MapObject) null;
        this.myMoveOffset = new SizeF();
        this.mySelectionHidden = false;
        this.myModalDropped = false;
        this.mySelectionSet = false;
        this.myModalDropped = false;
      }

      private bool alreadyDragged(Hashtable draggeds, MapObject o)
      {
        for (MapObject key = o; key != null; key = (MapObject) key.Parent)
        {
          if (draggeds.Contains((object) key))
            return true;
        }
        return false;
      }

      public override bool CanStart()
      {
        if (!this.View.CanMoveObjects() && !this.View.CanCopyObjects() && !this.View.AllowDragOut || this.LastInput.IsContextButton)
          return false;
        Size dragSize = this.DragSize;
        Point viewPoint1 = this.FirstInput.ViewPoint;
        Point viewPoint2 = this.LastInput.ViewPoint;
        if (Math.Abs(viewPoint1.X - viewPoint2.X) <= dragSize.Width / 2 && Math.Abs(viewPoint1.Y - viewPoint2.Y) <= dragSize.Height / 2)
          return false;
        MapObject mapObject = this.View.PickObject(true, false, this.FirstInput.DocPoint, true);
        return mapObject != null && (mapObject.CanMove() || mapObject.CanCopy());
      }

      public virtual void ClearDragSelection()
      {
        if (this.DragSelection == null)
          return;
        foreach (MapObject mapObject in (MapCollection) this.DragSelection)
          mapObject.Remove();
        this.DragSelection = (MapSelection) null;
        this.CurrentObject = this.DragSelectionOriginalObject;
        this.DragSelectionOriginalObject = (MapObject) null;
      }

      public virtual MapSelection ComputeEffectiveSelection(IMapCollection coll, bool move)
      {
        Hashtable draggeds = new Hashtable();
        MapCollection mapCollection1 = (MapCollection) null;
        MapSelection effectiveSelection = new MapSelection((MapView) null);
        MapCollection mapCollection2 = (MapCollection) null;
        foreach (MapObject mapObject1 in (IEnumerable) coll)
        {
          MapObject draggingObject = mapObject1.DraggingObject;
          if (draggingObject != null && (move ? (!draggingObject.CanMove() ? 1 : 0) : (!draggingObject.CanCopy() ? 1 : 0)) == 0 && !this.alreadyDragged(draggeds, draggingObject))
          {
            if (mapCollection1 != null && draggingObject is MapGroup)
            {
              foreach (MapObject key in mapCollection1)
              {
                if (key.IsChildOf(draggingObject))
                {
                  draggeds.Remove((object) key);
                  if (mapCollection2 == null)
                    mapCollection2 = new MapCollection();
                  mapCollection2.Add(key);
                  effectiveSelection.Remove(key);
                }
              }
              if (mapCollection2 != null && !mapCollection2.IsEmpty)
              {
                foreach (MapObject mapObject2 in mapCollection2)
                  mapCollection1.Remove(mapObject2);
                mapCollection2.Clear();
              }
            }
            draggeds.Add((object) draggingObject, (object) draggingObject);
            if (!draggingObject.IsTopLevel)
            {
              if (mapCollection1 == null)
                mapCollection1 = new MapCollection();
              mapCollection1.Add(draggingObject);
            }
            effectiveSelection.Add(draggingObject);
          }
        }
        foreach (MapObject copy in effectiveSelection.CopyArray())
        {
          if (copy is IMapNode mapNode)
          {
            foreach (IMapLink destinationLink in mapNode.DestinationLinks)
            {
              if (!this.alreadyDragged(draggeds, destinationLink.MapObject) && (destinationLink.ToPort == null || this.alreadyDragged(draggeds, destinationLink.ToPort.MapObject)))
              {
                draggeds.Add((object) destinationLink.MapObject, (object) destinationLink.MapObject);
                effectiveSelection.Add(destinationLink.MapObject);
              }
            }
            foreach (IMapLink sourceLink in mapNode.SourceLinks)
            {
              if (!this.alreadyDragged(draggeds, sourceLink.MapObject) && (sourceLink.FromPort == null || this.alreadyDragged(draggeds, sourceLink.FromPort.MapObject)))
              {
                draggeds.Add((object) sourceLink.MapObject, (object) sourceLink.MapObject);
                effectiveSelection.Add(sourceLink.MapObject);
              }
            }
          }
        }
        return effectiveSelection;
      }

      public virtual MapSelection CreateDragSelection()
      {
        MapSelection dragSelection = new MapSelection((MapView) null);
        MapRectangle mapRectangle = new MapRectangle();
        mapRectangle.Bounds = this.CurrentObject.Bounds;
        mapRectangle.Visible = false;
        this.View.Layers.Default.Add((MapObject) mapRectangle);
        dragSelection.Add((MapObject) mapRectangle);
        MapCollection coll = new MapCollection();
        foreach (MapObject mapObject in this.EffectiveSelection != null ? (MapCollection) this.EffectiveSelection : (MapCollection) this.Selection)
          coll.Add(mapObject.DraggingObject);
        RectangleF bounds = MapDocument.ComputeBounds((IMapCollection) coll, this.View);
        Bitmap bitmapFromCollection = this.View.GetBitmapFromCollection((IMapCollection) coll, bounds, false);
        MapImage mapImage = new MapImage();
        mapImage.Position = new PointF(bounds.X, bounds.Y);
        mapImage.Image = (Image) bitmapFromCollection;
        this.View.Layers.Default.Add((MapObject) mapImage);
        dragSelection.Add((MapObject) mapImage);
        return dragSelection;
      }

      public override void DoCancelMouse()
      {
        if (this.CurrentObject != null && this.DragSelection == null)
        {
          SizeF a = MapTool.SubtractPoints(this.FirstInput.DocPoint, this.MoveOffset);
          this.View.MoveSelection(this.EffectiveSelection != null ? this.EffectiveSelection : this.Selection, MapTool.SubtractPoints(a, this.CurrentObject.Position), false);
        }
        this.TransactionResult = (string) null;
        this.StopTool();
      }

      public virtual void DoDragDrop(IMapCollection coll, DragDropEffects allow)
      {
        int num = (int) this.View.DoDragDrop((object) coll, allow);
      }

      public virtual void DoDragging(MapInputState evttype)
      {
        if (this.CurrentObject == null)
          return;
        SizeF sizeF1 = MapTool.SubtractPoints(this.LastInput.DocPoint, this.CurrentObject.Position);
        SizeF offset1 = new SizeF(sizeF1.Width - this.MoveOffset.Width, sizeF1.Height - this.MoveOffset.Height);
        bool flag = this.MustBeCopying();
        MapViewSnapStyle gridSnapDrag = this.View.GridSnapDrag;
        if (this.EffectiveSelection == null)
          this.myEffectiveSelection = this.ComputeEffectiveSelection((IMapCollection) this.Selection, !flag);
        if (evttype != MapInputState.Finish)
        {
          bool grid = gridSnapDrag == MapViewSnapStyle.Jump;
          if (flag || !this.View.DragsRealtime)
          {
            this.MakeDragSelection();
            this.View.MoveSelection(this.DragSelection, offset1, grid);
          }
          else
          {
            this.ClearDragSelection();
            this.View.MoveSelection(this.EffectiveSelection, offset1, grid);
          }
        }
        else
        {
          SizeF sizeF2 = new SizeF();
          SizeF offset2;
          if (this.DragSelection != null)
          {
            offset2 = MapTool.SubtractPoints(this.CurrentObject.Position, this.DragSelectionOriginalObject.Position);
            this.ClearDragSelection();
          }
          else
            offset2 = offset1;
          bool grid = gridSnapDrag == MapViewSnapStyle.Jump || gridSnapDrag == MapViewSnapStyle.After;
          if (flag)
          {
            if (this.CopiesEffectiveSelection)
              this.View.CopySelection(this.ComputeEffectiveSelection((IMapCollection) this.Selection, false), offset2, grid);
            else
              this.View.CopySelection(this.Selection, offset2, grid);
          }
          else
          {
            if (this.EffectiveSelection == null)
              this.myEffectiveSelection = this.ComputeEffectiveSelection((IMapCollection) this.Selection, true);
            this.View.MoveSelection(this.EffectiveSelection, offset2, grid);
          }
        }
      }

      /// <summary>действия когда мышь двигают</summary>
      public override void DoMouseMove()
      {
        DragEventArgs dragEventArgs = this.LastInput.DragEventArgs;
        if (dragEventArgs != null)
          dragEventArgs.Effect = !this.MustBeCopying() ? (!this.MustBeMoving() ? (this.MayBeMoving() || this.MayBeCopying() ? DragDropEffects.Move : DragDropEffects.None) : (!this.MayBeMoving() ? DragDropEffects.None : DragDropEffects.Move)) : (!this.MayBeCopying() ? DragDropEffects.None : DragDropEffects.Copy);
        this.DoDragging(MapInputState.Continue);
        this.View.DoAutoScroll(this.LastInput.ViewPoint);
      }

      /// <summary>действия когда клавиша мыши отпущена</summary>
      public override void DoMouseUp()
      {
        this.myModalDropped = true;
        if (this.MustBeCopying())
        {
          this.DoDragging(MapInputState.Finish);
          this.TransactionResult = "Copy Selection";
          this.StopTransaction();
          this.View.RaiseSelectionCopied();
        }
        else
        {
          this.DoDragging(MapInputState.Finish);
          this.TransactionResult = "Move Selection";
          this.StopTransaction();
          this.View.RaiseSelectionMoved();
        }
        this.StopTool();
      }

      public virtual void MakeDragSelection()
      {
        if (this.DragSelection != null)
          return;
        this.DragSelectionOriginalObject = this.CurrentObject;
        this.DragSelection = this.CreateDragSelection();
        if (this.DragSelection == null || this.DragSelection.IsEmpty)
        {
          this.DragSelectionOriginalObject = (MapObject) null;
          this.DragSelection = (MapSelection) null;
        }
        else
        {
          this.View.MoveSelection(this.EffectiveSelection != null ? this.EffectiveSelection : this.Selection, MapTool.SubtractPoints(MapTool.SubtractPoints(this.FirstInput.DocPoint, this.MoveOffset), this.DragSelectionOriginalObject.Position), false);
          if (this.CurrentObject.View == this.View)
            return;
          this.CurrentObject = this.DragSelection.Primary;
        }
      }

      public virtual bool MayBeCopying()
      {
        if (!this.LastInput.Shift && this.View.CanInsertObjects())
        {
          foreach (MapObject mapObject in (MapCollection) this.Selection)
          {
            if (mapObject.CanCopy())
              return true;
          }
        }
        return false;
      }

      public virtual bool MayBeMoving()
      {
        if (!this.LastInput.Control && this.View.CanMoveObjects())
        {
          foreach (MapObject mapObject in (MapCollection) this.Selection)
          {
            if (mapObject.CanMove())
              return true;
          }
        }
        return false;
      }

      public virtual bool MustBeCopying()
      {
        int num = this.LastInput.Control ? 1 : 0;
        bool flag = this.View.CanInsertObjects();
        return num != 0 && flag;
      }

      public virtual bool MustBeMoving()
      {
        int num = this.LastInput.Shift ? 1 : 0;
        bool flag = this.View.CanMoveObjects();
        return num != 0 && flag;
      }

      public override void Start()
      {
        if (!this.mySelectionSet)
        {
          this.CurrentObject = this.View.PickObject(true, false, this.FirstInput.DocPoint, true);
          if (this.CurrentObject == null)
            return;
          this.MoveOffset = MapTool.SubtractPoints(this.FirstInput.DocPoint, this.CurrentObject.Position);
        }
        this.StartTransaction();
        if (!this.mySelectionSet && !this.Selection.Contains(this.CurrentObject))
        {
          if (this.FirstInput.Shift || this.FirstInput.Control)
            this.Selection.Add(this.CurrentObject);
          else
            this.Selection.Select(this.CurrentObject);
        }
        if (this.HidesSelectionHandles)
        {
          this.mySelectionHidden = true;
          this.Selection.RemoveAllSelectionHandles();
        }
        if (this.mySelectionSet || !this.View.AllowDragOut)
          return;
        this.myModalDropped = false;
        try
        {
          this.Selection.HotSpot = MapTool.SubtractPoints(this.LastInput.DocPoint, this.Selection.Primary.Position);
          this.DoDragDrop((IMapCollection) this.Selection, DragDropEffects.All);
        }
        catch (SecurityException ex)
        {
          MapObject.Trace("MapToolDragging Start: " + ex.ToString());
        }
        finally
        {
          if (!this.myModalDropped)
            this.DoCancelMouse();
          else
            this.StopTool();
          this.Selection.HotSpot = new SizeF();
        }
      }

      public override void Stop()
      {
        this.View.StopAutoScroll();
        if (this.mySelectionHidden)
        {
          this.mySelectionHidden = false;
          this.Selection.AddAllSelectionHandles();
        }
        this.ClearDragSelection();
        this.myEffectiveSelection = (MapSelection) null;
        this.MoveOffset = new SizeF();
        this.CurrentObject = (MapObject) null;
        this.mySelectionSet = false;
        this.StopTransaction();
      }

      public virtual bool CopiesEffectiveSelection
      {
        get => this.myCopiesEffectiveSelection;
        set => this.myCopiesEffectiveSelection = value;
      }

      public MapSelection DragSelection
      {
        get => this.myDragSelection;
        set => this.myDragSelection = value;
      }

      public MapObject DragSelectionOriginalObject
      {
        get => this.myDragSelectionOrigObj;
        set => this.myDragSelectionOrigObj = value;
      }

      public MapSelection EffectiveSelection => this.myEffectiveSelection;

      public virtual bool HidesSelectionHandles
      {
        get => this.myHidesSelectionHandles;
        set => this.myHidesSelectionHandles = value;
      }

      public SizeF MoveOffset
      {
        get => this.myMoveOffset;
        set => this.myMoveOffset = value;
      }
    }
}
