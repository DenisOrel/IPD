// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapNodeLinkEnumerator
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;


namespace Intermech.Map
{
    public struct MapNodeLinkEnumerator : IEnumerator, IEnumerable
    {
      private MapNode myNode;
      private MapNode.Search mySearch;
      private ArrayList myArray;
      private int myIndex;

      internal MapNodeLinkEnumerator(MapNode n, MapNode.Search s)
      {
        this.myNode = n;
        this.mySearch = s;
        this.myArray = (ArrayList) null;
        this.myIndex = -1;
        this.Reset();
      }

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this;

      public MapNodeLinkEnumerator GetEnumerator() => this;

      object IEnumerator.Current => (object) this.GetCurrent();

      public IMapLink Current => this.GetCurrent();

      private IMapLink GetCurrent()
      {
        if (this.myIndex < 0 || this.myIndex >= this.myArray.Count)
          throw new InvalidOperationException("MapNode.MapNodeLinkEnumerator is not at a valid position for the ArrayList");
        return (IMapLink) this.myArray[this.myIndex];
      }

      public bool MoveNext()
      {
        if (this.myIndex + 1 < this.myArray.Count)
        {
          ++this.myIndex;
          return true;
        }
        this.myNode.myParts = this.myArray;
        return false;
      }

      public void Reset()
      {
        this.myArray = this.myNode.findAll(this.mySearch);
        this.myIndex = -1;
      }
    }
}
