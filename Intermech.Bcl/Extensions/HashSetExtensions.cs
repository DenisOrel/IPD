
// Type: Intermech.Extensions.HashSetExtensions
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
    /// <summary>Функции-расширения класса HashSet</summary>
    public static class HashSetExtensions
    {
      /// <summary>Добавить в HashSet набор значений</summary>
      /// <typeparam name="T">Тип элемента HashSet-а</typeparam>
      /// <param name="hashSet">HashSet, в который должно произойти вставка элементов</param>
      /// <param name="enumerator">Енумератор значений, которые должны быть вставлены в HashSet</param>
      /// <returns>HashSet, в который должно произойти вставка элементов</returns>
      [NotNull]
      [CollectionAccess(CollectionAccessType.UpdatedContent)]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static HashSet<T> AddRange<T>([NotNull] this HashSet<T> hashSet, [NotNull] IEnumerable<T> enumerator)
      {
        foreach (T obj in enumerator)
          hashSet.Add(obj);
        Thread.MemoryBarrier();
        return hashSet;
      }

      /// <summary>Преобразует HashSet в массив</summary>
      /// <typeparam name="T">Тип элемента HashSet-а</typeparam>
      /// <param name="hashSet">HashSet, в который должно произойти вставка элементов</param>
      /// <returns>Массив из элементов HashSet-а</returns>
      [NotNull]
      [CollectionAccess(CollectionAccessType.UpdatedContent)]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T[] ToArray<T>([NotNull] this HashSet<T> hashSet) => hashSet.AsArray();
    }
}
