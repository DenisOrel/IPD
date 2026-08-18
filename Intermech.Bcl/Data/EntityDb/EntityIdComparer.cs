
// Type: Intermech.Data.EntityDb.EntityIdComparer
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Data.EntityDb
{
    [Serializable]
    internal sealed class EntityIdComparer : IComparer<IEntity>, IEqualityComparer<IEntity>
    {
      private static readonly EntityIdComparer instance = new EntityIdComparer();

      public int Compare(IEntity x, IEntity y) => x.UniqueId.CompareTo(y.UniqueId);

      public bool Equals(IEntity x, IEntity y) => x.UniqueId == y.UniqueId;

      public int GetHashCode(IEntity obj) => obj.UniqueId.GetHashCode();

      public override int GetHashCode() => 0;

      public override bool Equals(object obj) => obj is EntityIdComparer || base.Equals(obj);

      public static EntityIdComparer Instance => EntityIdComparer.instance;
    }
}
