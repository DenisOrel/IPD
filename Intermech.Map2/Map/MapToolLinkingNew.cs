// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapToolLinkingNew
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;


namespace Intermech.Map
{
    [Serializable]
    public class MapToolLinkingNew(MapView v) : MapToolLinking(v)
    {
      public override bool CanStart()
      {
        if (this.FirstInput.IsContextButton || !this.View.CanLinkObjects())
          return false;
        IMapPort mapPort = this.PickPort(this.FirstInput.DocPoint);
        this.OriginalStartPort = mapPort;
        if (mapPort == null)
          return false;
        return this.IsValidFromPort(mapPort) || this.IsValidToPort(mapPort);
      }

      public override void Start()
      {
        base.Start();
        this.StartNewLink(this.OriginalStartPort, this.LastInput.DocPoint);
      }
    }
}
