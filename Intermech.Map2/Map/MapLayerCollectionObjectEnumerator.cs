// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapLayerCollectionObjectEnumerator
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;


namespace Intermech.Map
{
    public struct MapLayerCollectionObjectEnumerator : IEnumerator, IEnumerable
    {
      private ArrayList myArray;
      private bool myForward;
      private int myIndex;
      private bool myEnumeratorValid;
      private MapLayerEnumerator myEnumerator;

      internal MapLayerCollectionObjectEnumerator(ArrayList a, bool forward)
      {
        this.myArray = a;
        this.myForward = forward;
        this.myIndex = -1;
        this.myEnumerator = new MapLayerEnumerator(a, true);
        this.myEnumeratorValid = false;
        this.Reset();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        MapLayerCollectionObjectEnumerator enumerator = this;
        enumerator.Reset();
        return (IEnumerator) enumerator;
      }

      public MapLayerCollectionObjectEnumerator GetEnumerator()
      {
        MapLayerCollectionObjectEnumerator enumerator = this;
        enumerator.Reset();
        return enumerator;
      }

      object IEnumerator.Current => (object) this.GetCurrent();

      public MapObject Current => this.GetCurrent();

      private MapObject GetCurrent()
      {
        if (!this.myEnumeratorValid)
          throw new InvalidOperationException("MapLayerCollectionObjectEnumerator is not at a valid position for the ArrayList of Layers");
        return this.myEnumerator.Current;
      }

      public bool MoveNext()
      {
        if (this.myEnumeratorValid)
        {
          if (this.myEnumerator.MoveNext())
            return true;
          this.myEnumeratorValid = false;
        }
        if (this.myForward)
        {
          while (this.myIndex + 1 < this.myArray.Count)
          {
            ++this.myIndex;
            this.myEnumerator = ((MapLayer) this.myArray[this.myIndex]).GetEnumerator();
            this.myEnumeratorValid = true;
            if (this.myEnumerator.MoveNext())
              return true;
          }
          return false;
        }
        while (this.myIndex - 1 >= 0)
        {
          --this.myIndex;
          this.myEnumerator = ((MapLayer) this.myArray[this.myIndex]).Backwards;
          this.myEnumeratorValid = true;
          if (this.myEnumerator.MoveNext())
            return true;
        }
        return false;
      }

      public void Reset()
      {
        this.myIndex = !this.myForward ? this.myArray.Count : -1;
        this.myEnumeratorValid = false;
      }
    }
}
