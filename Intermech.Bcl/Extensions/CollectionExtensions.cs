
// Type: Intermech.Extensions.CollectionExtensions
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;


namespace Intermech.Extensions
{
    /// <summary>Расширение для ICollection</summary>
    /// <remarks>Для новых функций желательно писать тесты</remarks>
    public static class CollectionExtensions
    {
      /// <summary>Добавить в коллекцию перечисление элементов</summary>
      [NotNull]
      [CollectionAccess(CollectionAccessType.UpdatedContent)]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static ICollection<T> AddRange<T>([NotNull] this ICollection<T> collection, [NotNull] IEnumerable<T> values)
      {
        int result1;
        int result2;
        if (values.TryGetCount(out result1) && collection.TryGetCapacity(out result2))
        {
          int capacity = collection.Count + result1;
          if (result2 < capacity)
            collection.TrySetCapacity(capacity);
        }
        foreach (T obj in values)
          collection.Add(obj);
        Thread.MemoryBarrier();
        return collection;
      }

      /// <summary>Удалить из коллекции перечисление элементов</summary>
      [NotNull]
      [CollectionAccess(CollectionAccessType.UpdatedContent)]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static ICollection<T> RemoveRange<T>([NotNull] this ICollection<T> collection, [NotNull] IEnumerable<T> values)
      {
        foreach (T obj in values.AsArray())
          collection.Remove(obj);
        Thread.MemoryBarrier();
        return collection;
      }

      /// <summary>Добавление элемента в коллекцию с контролем уникальности</summary>
      [NotNull]
      [CollectionAccess(CollectionAccessType.UpdatedContent)]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static ICollection<T> SafeAdd<T>([NotNull] this ICollection<T> collection, [CanBeNull] T item)
      {
        if (!collection.Contains(item))
          collection.Add(item);
        Thread.MemoryBarrier();
        return collection;
      }

      /// <summary>Добавление элементов в коллекцию с контролем уникальности</summary>
      [NotNull]
      [CollectionAccess(CollectionAccessType.UpdatedContent)]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static ICollection<T> SafeAddRange<T>([NotNull] this ICollection<T> collection, [NotNull] IEnumerable<T> items)
      {
        foreach (T obj in items)
        {
          if (!collection.Contains(obj))
            collection.Add(obj);
        }
        Thread.MemoryBarrier();
        return collection;
      }
    }
}
