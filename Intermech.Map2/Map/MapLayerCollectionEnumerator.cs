// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapLayerCollectionEnumerator
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;


namespace Intermech.Map
{
    public struct MapLayerCollectionEnumerator : IEnumerator, IEnumerable
    {
      private ArrayList _array;
      private bool _forward;
      private int _index;

      internal MapLayerCollectionEnumerator(ArrayList a, bool forward)
      {
        this._array = a;
        this._forward = forward;
        this._index = -1;
        this.Reset();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        MapLayerCollectionEnumerator enumerator = this;
        enumerator.Reset();
        return (IEnumerator) enumerator;
      }

      public MapLayerCollectionEnumerator GetEnumerator()
      {
        MapLayerCollectionEnumerator enumerator = this;
        enumerator.Reset();
        return enumerator;
      }

      object IEnumerator.Current => (object) this.GetCurrent();

      public MapLayer Current => this.GetCurrent();

      private MapLayer GetCurrent()
      {
        if (this._index < 0 || this._index >= this._array.Count)
          throw new InvalidOperationException("MapLayerCollection.MapLayerCollectionEnumerator is not at a valid position for the ArrayList");
        return (MapLayer) this._array[this._index];
      }

      public bool MoveNext()
      {
        if (this._forward)
        {
          if (this._index + 1 >= this._array.Count)
            return false;
          ++this._index;
          return true;
        }
        if (this._index - 1 < 0)
          return false;
        --this._index;
        return true;
      }

      public void Reset()
      {
        if (this._forward)
          this._index = -1;
        else
          this._index = this._array.Count;
      }
    }
}
