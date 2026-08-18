// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapCollection
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;


namespace Intermech.Map
{
    [Serializable]
    public class MapCollection : IMapCollection, ICollection, IEnumerable
    {
      private ArrayList myObjects;

      public MapCollection() => this.myObjects = new ArrayList();

      public virtual void Add(MapObject obj)
      {
        if (obj == null || this.Contains(obj))
          return;
        this.myObjects.Add((object) obj);
      }

      public virtual void Clear()
      {
        int val1;
        for (int index = this.myObjects.Count; index > 0; index = Math.Min(val1, this.myObjects.Count))
          this.Remove((MapObject) this.myObjects[val1 = index - 1]);
      }

      public virtual bool Contains(MapObject obj)
      {
        return obj != null && this.myObjects.Contains((object) obj);
      }

      public virtual MapObject[] CopyArray()
      {
        MapObject[] array = new MapObject[this.Count];
        this.CopyTo(array, 0);
        return array;
      }

      public virtual void CopyTo(Array array, int index) => this.myObjects.CopyTo(array, index);

      public void CopyTo(MapObject[] array, int index) => this.CopyTo((Array) array, index);

      internal static int fastRemove(ArrayList a, object o)
      {
        int index = -1;
        int count = a.Count;
        if (count > 1000)
          index = a.IndexOf(o, count - 50, 50);
        if (index < 0)
          index = a.IndexOf(o);
        if (index >= 0)
          a.RemoveAt(index);
        return index;
      }

      public virtual MapCollectionEnumerator GetEnumerator()
      {
        return new MapCollectionEnumerator(this.myObjects, true);
      }

      IEnumerable IMapCollection.Backwards => (IEnumerable) this.Backwards;

      public virtual void Remove(MapObject obj)
      {
        if (obj == null)
          return;
        MapCollection.fastRemove(this.myObjects, (object) obj);
      }

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

      public virtual MapCollectionEnumerator Backwards
      {
        get => new MapCollectionEnumerator(this.myObjects, false);
      }

      public virtual int Count => this.myObjects.Count;

      public virtual MapObject First => this.IsEmpty ? (MapObject) null : (MapObject) this.myObjects[0];

      public virtual bool IsEmpty => this.myObjects.Count == 0;

      public virtual bool IsSynchronized => false;

      public virtual MapObject Last
      {
        get => this.IsEmpty ? (MapObject) null : (MapObject) this.myObjects[this.Count - 1];
      }

      public virtual object SyncRoot => (object) this;
    }
}
