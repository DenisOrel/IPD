
// Type: Intermech.Data.EntityDb.EntitySet
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Intermech.Data.EntityDb
{
    [Serializable]
    public class EntitySet : HashSet<IEntity>
    {
      public EntitySet()
        : base((IEqualityComparer<IEntity>) EntityIdComparer.Instance)
      {
      }

      public EntitySet(IEnumerable<IEntity> collection)
        : base(collection, (IEqualityComparer<IEntity>) EntityIdComparer.Instance)
      {
      }

      protected EntitySet(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }

      public IEntity TryGetFirstEntity()
      {
        if (this.Count != 0)
        {
          using (IEnumerator<IEntity> enumerator = (IEnumerator<IEntity>) this.GetEnumerator())
          {
            if (enumerator.MoveNext())
              return enumerator.Current;
          }
        }
        return (IEntity) null;
      }

      public List<IEntity> TryGetFirstEntities(int count)
      {
        if (count > this.Count)
          count = this.Count;
        List<IEntity> firstEntities = new List<IEntity>(count);
        if (count > 0)
        {
          using (IEnumerator<IEntity> enumerator = (IEnumerator<IEntity>) this.GetEnumerator())
          {
            for (; count > 0; --count)
            {
              if (enumerator.MoveNext())
                firstEntities.Add(enumerator.Current);
              else
                break;
            }
          }
        }
        return firstEntities;
      }
    }
}
