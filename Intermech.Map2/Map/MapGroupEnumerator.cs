// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapGroupEnumerator
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;


namespace Intermech.Map
{
    public struct MapGroupEnumerator : IEnumerator, IEnumerable
    {
      private ArrayList myArray;
      private bool myForward;
      private int myIndex;

      internal MapGroupEnumerator(ArrayList a, bool forward)
      {
        this.myArray = a;
        this.myForward = forward;
        this.myIndex = -1;
        this.Reset();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        MapGroupEnumerator enumerator = this;
        enumerator.Reset();
        return (IEnumerator) enumerator;
      }

      public MapGroupEnumerator GetEnumerator()
      {
        MapGroupEnumerator enumerator = this;
        enumerator.Reset();
        return enumerator;
      }

      object IEnumerator.Current => (object) this.GetCurrent();

      public MapObject Current => this.GetCurrent();

      private MapObject GetCurrent()
      {
        if (this.myIndex < 0 || this.myIndex >= this.myArray.Count)
          throw new InvalidOperationException("MapGroup.MapGroupEnumerator is not at a valid position for the ArrayList");
        return (MapObject) this.myArray[this.myIndex];
      }

      public bool MoveNext()
      {
        if (this.myForward)
        {
          if (this.myIndex + 1 >= this.myArray.Count)
            return false;
          ++this.myIndex;
          return true;
        }
        if (this.myIndex - 1 < 0)
          return false;
        --this.myIndex;
        return true;
      }

      public void Reset()
      {
        if (this.myForward)
          this.myIndex = -1;
        else
          this.myIndex = this.myArray.Count;
      }
    }
}
