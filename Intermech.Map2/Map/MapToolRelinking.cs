// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapToolRelinking
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Drawing;


namespace Intermech.Map
{
    [Serializable]
    public class MapToolRelinking : MapToolLinking
    {
      [NonSerialized]
      private bool mySelectionHidden;

      public MapToolRelinking(MapView v)
        : base(v)
      {
        this.mySelectionHidden = false;
      }

      public override bool CanStart()
      {
        if (this.FirstInput.IsContextButton || !this.View.CanLinkObjects())
          return false;
        IMapHandle mapHandle = this.PickRelinkHandle(this.FirstInput.DocPoint);
        if (mapHandle == null)
          return false;
        if (mapHandle.HandleID == 1024 /*0x0400*/)
        {
          this.CurrentObject = mapHandle.HandledObject;
          IMapLink mapLink1 = mapHandle.SelectedObject as IMapLink;
          if (mapLink1 is MapLink)
          {
            MapLink mapLink2 = (MapLink) mapLink1;
            if (mapLink2.AbstractLink != null)
              mapLink1 = mapLink2.AbstractLink;
          }
          if (mapLink1 == null)
            return false;
          this.Link = mapLink1;
          this.OriginalEndPort = this.Link.FromPort;
          return true;
        }
        if (mapHandle.HandleID != 1025)
          return false;
        this.CurrentObject = mapHandle.HandledObject;
        IMapLink mapLink3 = mapHandle.SelectedObject as IMapLink;
        if (mapLink3 is MapLink)
        {
          MapLink mapLink4 = (MapLink) mapLink3;
          if (mapLink4.AbstractLink != null)
            mapLink3 = mapLink4.AbstractLink;
        }
        if (mapLink3 == null)
          return false;
        this.Link = mapLink3;
        this.OriginalEndPort = this.Link.ToPort;
        return true;
      }

      public virtual IMapHandle PickRelinkHandle(PointF dc)
      {
        return this.View.PickObject(false, true, dc, true) as IMapHandle;
      }

      public override void Start()
      {
        base.Start();
        MapObject currentObject = this.CurrentObject;
        if (currentObject != null && this.Selection.GetHandleCount(currentObject) > 0)
        {
          this.mySelectionHidden = true;
          currentObject.RemoveSelectionHandles(this.Selection);
        }
        this.StartRelink(this.Link, this.OriginalEndPort, this.LastInput.DocPoint);
      }

      public override void Stop()
      {
        if (this.mySelectionHidden)
        {
          this.mySelectionHidden = false;
          MapObject currentObject = this.CurrentObject;
          if (currentObject != null && currentObject.Document == this.View.Document)
          {
            if (!this.Selection.Contains(this.Link.MapObject))
              this.Selection.Add(this.Link.MapObject);
            else
              currentObject.AddSelectionHandles(this.Selection, this.Link.MapObject);
          }
        }
        this.CurrentObject = (MapObject) null;
        base.Stop();
      }
    }
}
