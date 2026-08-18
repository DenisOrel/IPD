// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapCopyDictionary
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;


namespace Intermech.Map
{
    [Serializable]
    public class MapCopyDictionary : Hashtable
    {
      private MapCopyDelayedsCollection myDelayeds;
      private MapDocument myDestinationDocument;
      private IMapCollection mySourceCollection;

      public MapCopyDictionary()
      {
        this.mySourceCollection = (IMapCollection) null;
        this.myDestinationDocument = (MapDocument) null;
        this.myDelayeds = new MapCopyDelayedsCollection();
      }

      public virtual MapObject Copy(MapObject obj)
      {
        if (obj == null)
          return (MapObject) null;
        if (!(this[(object) obj] is MapObject mapObject))
          mapObject = obj.CopyObject(this);
        return mapObject;
      }

      public virtual MapCopyDelayedsCollection Delayeds => this.myDelayeds;

      public virtual MapDocument DestinationDocument
      {
        get => this.myDestinationDocument;
        set => this.myDestinationDocument = value;
      }

      public override object this[object key]
      {
        get => key == null ? (object) null : base[key];
        set
        {
          if (key == null)
            return;
          base[key] = value;
        }
      }

      public virtual IMapCollection SourceCollection
      {
        get => this.mySourceCollection;
        set => this.mySourceCollection = value;
      }
    }
}
