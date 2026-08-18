// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapToolAction
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;


namespace Intermech.Map
{
    [Serializable]
    public class MapToolAction : MapTool
    {
      [NonSerialized]
      private IMapActionObject myActionObject;

      public MapToolAction(MapView v)
        : base(v)
      {
        this.myActionObject = (IMapActionObject) null;
      }

      public override bool CanStart()
      {
        return !this.FirstInput.IsContextButton && this.PickActionObject() != null;
      }

      /// <summary>действия когда мышь двигают</summary>
      public override void DoMouseMove()
      {
        if (this.ActionObject == null)
          return;
        this.ActionObject.OnActionAdjusted(this.View, this.LastInput);
      }

      /// <summary>действия когда клавиша мыши отпущена</summary>
      public override void DoMouseUp()
      {
        if (this.ActionObject != null && this.ActionObject == this.PickActionObject())
          this.ActionObject.OnAction(this.View, this.LastInput);
        this.StopTool();
      }

      public virtual IMapActionObject PickActionObject()
      {
        for (MapObject mapObject = this.View.PickObject(true, false, this.LastInput.DocPoint, false); mapObject != null; mapObject = (MapObject) mapObject.Parent)
        {
          if (mapObject is IMapActionObject mapActionObject && mapActionObject.ActionEnabled)
          {
            this.CurrentObject = mapObject;
            return mapActionObject;
          }
        }
        return (IMapActionObject) null;
      }

      public override void Start()
      {
        this.ActionObject = this.PickActionObject();
        if (this.ActionObject == null)
          this.StopTool();
        else
          this.ActionObject.ActionActivated = true;
      }

      public override void Stop()
      {
        if (this.ActionObject != null)
          this.ActionObject.ActionActivated = false;
        this.ActionObject = (IMapActionObject) null;
        this.CurrentObject = (MapObject) null;
      }

      public IMapActionObject ActionObject
      {
        get => this.myActionObject;
        set => this.myActionObject = value;
      }
    }
}
