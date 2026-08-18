
// Type: Intermech.Search.ObjectListFilters.ObjectListFilter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Search.Utilities;
using System;


namespace Intermech.Search.ObjectListFilters
{
    [Serializable]
    public sealed class ObjectListFilter
    {
      public static readonly ObjectListFilter AllObjectsFilter = new ObjectListFilter()
      {
        ID = 0,
        Guid = Guid.Empty,
        Name = "Все объекты",
        ObjectTypeIds = new int[0],
        IsSystem = true
      };

      public static ObjectListFilter DefaultFilter => ObjectListFilter.AllObjectsFilter;

      public ObjectListFilter(
        long selectionVersionID,
        Guid selectionVersionGuid,
        string name,
        int[] objectTypeIds)
      {
        if (ObjectHelper.IsUnknownObjectVersionID(selectionVersionID))
          throw new ArgumentException();
        if (selectionVersionGuid == Guid.Empty)
          throw new ArgumentException();
        this.ID = selectionVersionID;
        this.Guid = selectionVersionGuid;
        this.Name = name;
        this.ObjectTypeIds = objectTypeIds;
        this.IsSystem = SystemGUIDs.IsSystemGUID(this.Guid);
      }

      private ObjectListFilter()
      {
      }

      public long ID { get; private set; }

      public Guid Guid { get; private set; }

      public string Name { get; private set; }

      public int[] ObjectTypeIds { get; private set; }

      public bool IsSystem { get; private set; }

      public override bool Equals(object obj)
      {
        if (this == obj)
          return true;
        return obj is ObjectListFilter objectListFilter && this.ID == objectListFilter.ID;
      }

      public override int GetHashCode() => (int) this.ID;
    }
}
