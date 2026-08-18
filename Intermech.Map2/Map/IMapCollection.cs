using System.Collections;


namespace Intermech.Map
{
    public interface IMapCollection : ICollection, IEnumerable
    {
      void Add(MapObject obj);

      void Clear();

      bool Contains(MapObject obj);

      MapObject[] CopyArray();

      void CopyTo(MapObject[] array, int index);

      void Remove(MapObject obj);

      IEnumerable Backwards { get; }

      bool IsEmpty { get; }
    }
}
