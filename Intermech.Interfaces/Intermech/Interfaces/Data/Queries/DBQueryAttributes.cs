
// Type: Intermech.Interfaces.Data.Queries.DBQueryAttributes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Interfaces.Data.Queries
{
    public class DBQueryAttributes : 
      ICollection<DBQueryAttribute>,
      IEnumerable<DBQueryAttribute>,
      IEnumerable,
      ICloneable
    {
      private List<DBQueryAttribute> attributes;

      public DBQueryAttributes() => this.attributes = new List<DBQueryAttribute>(8);

      public DBQueryAttributes(IEnumerable<DBQueryAttribute> source)
      {
        this.attributes = source != null ? new List<DBQueryAttribute>(source) : throw new ArgumentNullException(nameof (source));
      }

      public List<DBQueryAttribute> ToList()
      {
        return new List<DBQueryAttribute>((IEnumerable<DBQueryAttribute>) this.attributes);
      }

      public int TryGetIndex(DBQueryAttribute attribute)
      {
        if (attribute == null)
          throw new ArgumentNullException(nameof (attribute));
        return this.attributes.FindIndex((Predicate<DBQueryAttribute>) (existingAttribute => object.Equals((object) existingAttribute.Item1, (object) attribute.Item1) && existingAttribute.Item2 == attribute.Item2));
      }

      public int GetIndex(DBQueryAttribute attribute)
      {
        int index = this.TryGetIndex(attribute);
        return index >= 0 ? index : throw new InvalidOperationException($"Атрибут '{attribute.Item1}' не был добавлен в параметры запроса.");
      }

      public DBQueryAttributes Clone()
      {
        return new DBQueryAttributes((IEnumerable<DBQueryAttribute>) this.attributes);
      }

      object ICloneable.Clone() => (object) this.Clone();

      public void Clear() => this.attributes.Clear();

      public int Add(DBQueryAttribute attribute)
      {
        int index = attribute != null ? this.TryGetIndex(attribute) : throw new ArgumentNullException(nameof (attribute));
        if (index >= 0)
        {
          DBQueryAttribute attribute1 = this.attributes[index];
          if (attribute.Item3 != attribute1.Item3)
            throw new InvalidOperationException($"Атрибут #{attribute.Item1} уже добавлен в коллекцию атрибутов запроса с другим значением свойства ColumnContent ({attribute.Item3} != {attribute1.Item3}).");
        }
        else
        {
          index = this.attributes.Count;
          this.attributes.Add(attribute);
        }
        return index;
      }

      void ICollection<DBQueryAttribute>.Add(DBQueryAttribute attribute) => this.Add(attribute);

      public void AddRange(DBQueryAttributes source)
      {
        if (source == null)
          throw new ArgumentNullException(nameof (source));
        foreach (DBQueryAttribute attribute in source.attributes)
          this.Add(attribute);
      }

      public void AddRange(IEnumerable<DBQueryAttribute> source)
      {
        if (source == null)
          throw new ArgumentNullException(nameof (source));
        foreach (DBQueryAttribute attribute in source)
          this.Add(attribute);
      }

      public bool Contains(DBQueryAttribute attribute) => this.TryGetIndex(attribute) >= 0;

      public bool Remove(DBQueryAttribute attribute)
      {
        int index = this.TryGetIndex(attribute);
        int num = index >= 0 ? 1 : 0;
        if (num == 0)
          return num != 0;
        this.attributes.RemoveAt(index);
        return num != 0;
      }

      public void CopyTo(DBQueryAttribute[] array, int arrayIndex)
      {
        this.attributes.CopyTo(array, arrayIndex);
      }

      public IEnumerator<DBQueryAttribute> GetEnumerator()
      {
        return (IEnumerator<DBQueryAttribute>) this.attributes.GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.attributes.GetEnumerator();

      public int Count => this.attributes.Count;

      public bool IsReadOnly => false;
    }
}
