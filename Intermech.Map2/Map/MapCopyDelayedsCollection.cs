// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapCopyDelayedsCollection
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;


namespace Intermech.Map
{
    [Serializable]
    public class MapCopyDelayedsCollection : ICollection, IEnumerable
    {
      private Hashtable myObjects;

      public MapCopyDelayedsCollection() => this.myObjects = new Hashtable();

      public virtual void Add(object obj)
      {
        if (obj == null)
          return;
        this.myObjects[obj] = obj;
      }

      public virtual void Clear() => this.myObjects.Clear();

      public virtual bool Contains(object obj) => obj != null && this.myObjects[obj] == obj;

      public virtual object[] CopyArray()
      {
        object[] objArray = new object[this.Count];
        this.CopyTo((Array) objArray, 0);
        return objArray;
      }

      public virtual void CopyTo(Array array, int index)
      {
        IDictionaryEnumerator enumerator = this.myObjects.GetEnumerator();
        int num = index;
        while (enumerator.MoveNext())
          array.SetValue(enumerator.Key, num++);
      }

      public void CopyTo(MapObject[] array, int index) => this.CopyTo((Array) array, index);

      public virtual IEnumerator GetEnumerator() => this.CopyArray().GetEnumerator();

      public virtual void Remove(object obj)
      {
        if (obj == null)
          return;
        this.myObjects.Remove(obj);
      }

      public virtual int Count => this.myObjects.Count;

      public virtual bool IsEmpty => this.Count == 0;

      public virtual bool IsSynchronized => this.myObjects.IsSynchronized;

      public virtual object SyncRoot => (object) this.myObjects;
    }
}
