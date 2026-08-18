// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapPortLinkEnumerator
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;


namespace Intermech.Map
{
    public struct MapPortLinkEnumerator : IEnumerator, IEnumerable
    {
      private ArrayList myArray;
      private int myIndex;

      internal MapPortLinkEnumerator(ArrayList a)
      {
        this.myArray = a;
        this.myIndex = -1;
        this.Reset();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        MapPortLinkEnumerator enumerator = this;
        enumerator.Reset();
        return (IEnumerator) enumerator;
      }

      public MapPortLinkEnumerator GetEnumerator()
      {
        MapPortLinkEnumerator enumerator = this;
        enumerator.Reset();
        return enumerator;
      }

      object IEnumerator.Current => (object) this.GetCurrent();

      public IMapLink Current => this.GetCurrent();

      private IMapLink GetCurrent()
      {
        if (this.myIndex < 0 || this.myIndex >= this.myArray.Count)
          throw new InvalidOperationException("MapPort.MapPortLinkEnumerator is not at a valid position for the ArrayList");
        return (IMapLink) this.myArray[this.myIndex];
      }

      public bool MoveNext()
      {
        if (this.myIndex + 1 >= this.myArray.Count)
          return false;
        ++this.myIndex;
        return true;
      }

      public void Reset() => this.myIndex = -1;
    }
}
