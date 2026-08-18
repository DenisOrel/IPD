// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapSelectionEventArgs
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;


namespace Intermech.Map
{
    [Serializable]
    public class MapSelectionEventArgs : EventArgs
    {
      private MapObject myObject;

      public MapSelectionEventArgs(MapObject obj) => this.myObject = obj;

      public MapObject MapObject => this.myObject;
    }
}
