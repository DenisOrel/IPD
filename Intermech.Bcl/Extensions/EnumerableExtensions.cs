
// Type: Intermech.Extensions.EnumerableExtensions
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;


namespace Intermech.Extensions
{
    /// <summary>Методы-расширения класса IEnumerable-T</summary>
    public static class EnumerableExtensions
    {
      /// <summary>Минимальная базовая вместимость списка для хранения последовательности</summary>
      public const int MinimumZeroCapacity = 16 /*0x10*/;
      /// <summary>Минимальная базовая вместимость списка для хранения последовательности</summary>
      public const int DefaultListCapacity = 16 /*0x10*/;

      [CollectionAccess(CollectionAccessType.None)]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool IsEmpty<T>([NotNull] this IEnumerable<T> enumerable)
      {
        int result;
        return enumerable.TryGetCount(out result) && result == 0 || !enumerable.Any();
      }

      [CollectionAccess(CollectionAccessType.None)]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool NotEmpty<T>([NotNull] this IEnumerable<T> enumerable)
      {
        int result;
        return enumerable.TryGetCount(out result) && result > 0 || enumerable.Any();
      }

      /// <summary>Обычный Select + проверка на null всех результатов
      /// Больно уж часто используется, вынес как отдельный метод расширения</summary>
      [NotNull]
      [LinqTunnel]
      [CollectionAccess(CollectionAccessType.Read)]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<TResult> SelectNotNull<TSource, TResult>(
        [NotNull] this IEnumerable<TSource> enumerable,
        [NotNull] Func<TSource, TResult?> selector)
        where TResult : struct
      {
        return enumerable.Select(selector).Where((Func<TResult?, bool>) (result => result.HasValue)).Select((Func<TResult?, TResult>) (result => result.Value));
      }

      /// <summary>Обычный Select + проверка на null всех результатов
      /// Больно уж часто используется, вынес как отдельный метод расширения</summary>
      [NotNull]
      [ItemNotNull]
      [CollectionAccess(CollectionAccessType.Read)]
      [LinqTunnel]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<TResult> SelectNotNull<TSource, TResult>(
        [NotNull] this IEnumerable<TSource> enumerable,
        [NotNull] Func<TSource, TResult> selector)
        where TResult : class
      {
        return enumerable.Select(selector).Where((Func<TResult, bool>) (result => (object) result != null));
      }

      /// <summary>Вызов метода для всех значений</summary>
      [NotNull]
      [ItemCanBeNull]
      [CollectionAccess(CollectionAccessType.Read)]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IEnumerable<T> InvokeForAll<T>([NotNull, ItemCanBeNull] this IEnumerable<T> enumerable, [NotNull, InstantHandle] Action<T> handler)
      {
        foreach (T obj in enumerable)
          handler(obj);
        return enumerable;
      }

      /// <summary>Преобразование в массив с заданием начальной вместимости массива и получение реально заполненных элементов</summary>
      /// <param name="enumerable">Перечисление</param>
      /// <param name="capacity">[in,out] вместимость массива (начальное значение может быть как меньше, так и больше финальной
      /// вместимости массива, но чем точнее будет указан, тем быстрее отработает метод)</param>
      /// <param name="count">[out] число реально внесённых в массив элементов (меньше либо равно вместимости массива)</param>
      /// <returns>Массив</returns>
      [NotNull]
      [CollectionAccess(CollectionAccessType.Read)]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private static T[] ToArray<T>([NotNull] this IEnumerable<T> enumerable, ref int capacity, out int count)
      {
        switch (enumerable)
        {
          case ICollection<T> objs1:
            count = objs1.Count;
            if (count == 0)
            {
              T[] array = Array.Empty<T>();
              capacity = 0;
              return array;
            }
            T[] array1 = new T[objs1.Count];
            objs1.CopyTo(array1, 0);
            capacity = array1.Length;
            count = array1.Length;
            return array1;
          case ICollection collection:
            count = collection.Count;
            if (count == 0)
            {
              T[] array2 = Array.Empty<T>();
              capacity = 0;
              return array2;
            }
            T[] array3 = new T[collection.Count];
            collection.CopyTo((Array) array3, 0);
            capacity = array3.Length;
            count = array3.Length;
            return array3;
          case IReadOnlyCollection<T> objs2:
            count = objs2.Count;
            if (count == 0)
            {
              T[] array4 = Array.Empty<T>();
              capacity = 0;
              return array4;
            }
            T[] array5 = new T[objs2.Count];
            int num1 = 0;
            foreach (T obj in (IEnumerable<T>) objs2)
              array5[num1++] = obj;
            capacity = array5.Length;
            count = array5.Length;
            return array5;
          default:
            if (capacity <= 0 && enumerable is ICapacity capacity1)
              capacity = capacity1.Capacity;
            if (capacity <= 0)
              capacity = 16 /*0x10*/;
            T[] sourceArray = new T[capacity];
            int num2 = 0;
            foreach (T obj in enumerable)
            {
              if (num2 == capacity)
              {
                T[] destinationArray = new T[checked (num2 + num2 << 1)];
                Array.Copy((Array) sourceArray, 0, (Array) destinationArray, 0, capacity);
                sourceArray = destinationArray;
                capacity = sourceArray.Length;
              }
              sourceArray[num2++] = obj;
            }
            Thread.MemoryBarrier();
            count = num2;
            return sourceArray;
        }
      }

      /// <summary>Преобразование в массив с заданием начальной вместимости массива и получение реально заполненных элементов</summary>
      /// <param name="enumerable">Перечисление</param>
      /// <param name="capacity">Оценка финальной вместимости массива (может быть как меньше, так и больше финальной
      /// вместимости массива, но чем точнее будет указан, тем быстрее отработает метод)</param>
      /// <param name="count">[out] число реально внесённых в массив элементов (меньше или равно вместимости массива)</param>
      /// <returns>Массив</returns>
      [NotNull]
      [CollectionAccess(CollectionAccessType.Read)]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T[] ToArray<T>([NotNull] this IEnumerable<T> enumerable, int capacity, out int count)
      {
        return enumerable.ToArray(ref capacity, out count);
      }

      /// <summary>Преобразование в массив с заданием ожидаемого числа элементов, на большом числе элементов будет работать
      /// несколько быстрее, чем ToArray() без параметров Размер массива (Length) всегда будет равен числу реально
      /// взятых из последовательности элементов. capacity при этом может быть как меньше, так и больше финальной
      /// вместимости массива, но чем точнее будет указан, тем быстрее отработает метод</summary>
      /// <param name="enumerable">Перечисление</param>
      /// <param name="capacity">(Опционально) Рекомендованная начальная вместимости массива</param>
      /// <returns>Массив</returns>
      [NotNull]
      [CollectionAccess(CollectionAccessType.Read)]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T[] AsArray<T>([NotNull] this IEnumerable<T> enumerable, int capacity = -1)
      {
        return enumerable is T[] objArray ? objArray : enumerable.ToArray(ref capacity, out int _);
      }

      /// <summary>Преобразование в массив с заданием ожидаемого числа элементов, на большом числе элементов будет работать
      /// несколько быстрее, чем ToArray() без параметров. Размер массива (Length) всегда будет равен числу реально
      /// взятых из последовательности элементов. capacity при этом может быть как меньше, так и больше финальной
      /// вместимости массива, но чем точнее будет указан, тем быстрее отработает метод</summary>
      /// <param name="enumerable">Перечисление</param>
      /// <param name="capacity">Базовый размер массива. Сначала создаётся массив данного размера,
      /// потом заполняется элементами перечисления, каждый раз когда в процессе заполнения происходит
      /// переполнение массива, его размер увеличивается в полтора раза. В конце итоговый размер массива
      /// сравнивается с числом фактически добавленных элементов и, если требуется, размер массива
      /// уменьшается до этого числа. Чем точнее и ближе указана базовая вместимости массива, чем меньше
      /// операций изменения размера массива будет происходить во время преобразования</param>
      /// <returns>Массив</returns>
      [NotNull]
      [CollectionAccess(CollectionAccessType.Read)]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T[] ToArray<T>([NotNull] this IEnumerable<T> enumerable, int capacity)
      {
        int count;
        T[] array = enumerable.ToArray(ref capacity, out count);
        if (array.Length > count)
        {
          Array.Resize(ref array, count);
          Thread.MemoryBarrier();
        }
        return Intermech.Diagnostics.Check.Result.NotNull(array);
      }

      [CollectionAccess(CollectionAccessType.Read)]
      [ContractAnnotation("enumerable:null => null; => notnull")]
      [CanBeNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static List<T> AsList<T>([CanBeNull] this IEnumerable<T> enumerable, int capacity = -1)
      {
        return enumerable.ToList(capacity);
      }

      /// <summary>Преобразование в массив с заданием ожидаемого числа элементов, на большом числе элементов будет работать
      /// несколько быстрее, чем ToList() без параметров</summary>
      /// <param name="enumerable">Перечисление</param>
      /// <param name="capacity">Оценка финальной вместимости массива (может быть как меньше, так и больше финальной
      /// вместимости массива, но чем точнее будет указан, тем быстрее отработает метод)</param>
      /// <returns>Список</returns>
      [ContractAnnotation("enumerable:null => null; => notnull")]
      [CanBeNull]
      [CollectionAccess(CollectionAccessType.Read)]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static List<T> ToList<T>([CanBeNull] this IEnumerable<T> enumerable, int capacity)
      {
        if (enumerable == null)
          return (List<T>) null;
        int result;
        if (enumerable.TryGetCount(out result) && capacity < result)
          capacity = result;
        if (capacity == -1 && enumerable is ICapacity capacity1)
          capacity = capacity1.Capacity;
        List<T> list = capacity >= 0 ? new List<T>(capacity) : new List<T>();
        list.AddRange(enumerable);
        Thread.MemoryBarrier();
        return list;
      }

      /// <summary>Если это возможно - рассчитать рекомендуемую вместимость внутреннего массива, который должен использоваться
      /// коллекцией для хранения всех элементов последовательности</summary>
      /// <param name="enumeration">Последовательность</param>
      /// <param name="baseCapacity">(Optional) Минимальная вместимость. Результат не может быть меньше этого значения</param>
      /// <returns>Рекомендуемая вместимость</returns>
      [CollectionAccess(CollectionAccessType.None)]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int GetRecommendedCapacity<T>([CanBeNull, NoEnumeration] this IEnumerable<T> enumeration, int baseCapacity = 16 /*0x10*/)
      {
        if (enumeration != null)
        {
          switch (enumeration)
          {
            case List<T> objList:
              return Math.Max(baseCapacity, objList.Capacity);
            case ArrayList arrayList:
              return Math.Max(baseCapacity, arrayList.Capacity);
            case CollectionBase collectionBase:
              return Math.Max(baseCapacity, collectionBase.Capacity);
            case SortedList sortedList:
              return Math.Max(baseCapacity, sortedList.Capacity);
            case ICapacity capacity:
              return Math.Max(baseCapacity, capacity.Capacity);
            default:
              int result;
              if (enumeration.TryGetCount(out result))
                return Math.Max(baseCapacity, result);
              break;
          }
        }
        return Math.Max(baseCapacity, 16 /*0x10*/);
      }

      /// <summary>Если это возможно - рассчитать рекомендуемую вместимость внутреннего массива, который должен использоваться
      /// коллекцией для хранения всех элементов последовательности</summary>
      /// <param name="enumeration">Последовательность</param>
      /// <returns>Размерность коллекции если она найдена, иначе null</returns>
      [CollectionAccess(CollectionAccessType.None)]
      [ContractAnnotation("enumeration: Null => null")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int? TryGetCapacity<T>([CanBeNull, NoEnumeration] this IEnumerable<T> enumeration)
      {
        int result;
        return !enumeration.TryGetCapacity(out result) ? new int?() : new int?(result);
      }

      /// <summary>Если это возможно - рассчитать рекомендуемую вместимость внутреннего массива, который должен использоваться
      /// коллекцией для хранения всех элементов последовательности</summary>
      /// <param name="enumeration">Последовательность</param>
      /// <param name="result">[out] вместимость коллекции если она найдена, иначе -1</param>
      /// <returns>True, если вместимость удалось найти, иначе false</returns>
      [CollectionAccess(CollectionAccessType.None)]
      [ContractAnnotation("enumeration: Null => false")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool TryGetCapacity<T>([CanBeNull, NoEnumeration] this IEnumerable<T> enumeration, out int result)
      {
        if (enumeration != null)
        {
          switch (enumeration)
          {
            case List<T> objList:
              result = objList.Capacity;
              return true;
            case ArrayList arrayList:
              result = arrayList.Capacity;
              return true;
            case CollectionBase collectionBase:
              result = collectionBase.Capacity;
              return true;
            case SortedList sortedList:
              result = sortedList.Capacity;
              return true;
            case ICapacity capacity:
              result = capacity.Capacity;
              return true;
            default:
              if (enumeration.TryGetCount(out result))
                return true;
              break;
          }
        }
        result = -1;
        return false;
      }

      /// <summary>Попытаться установить вместимость коллекции</summary>
      [CollectionAccess(CollectionAccessType.None)]
      [ContractAnnotation("enumeration: Null => false")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool TrySetCapacity<T>([CanBeNull, NoEnumeration] this IEnumerable<T> enumeration, int capacity)
      {
        if (enumeration != null)
        {
          switch (enumeration)
          {
            case List<T> objList:
              objList.Capacity = capacity;
              return true;
            case ArrayList arrayList:
              arrayList.Capacity = capacity;
              return true;
            case CollectionBase collectionBase:
              collectionBase.Capacity = capacity;
              return true;
            case SortedList sortedList:
              sortedList.Capacity = capacity;
              return true;
          }
        }
        return false;
      }

      /// <summary>Попытаться получиться кол-во элементов последовательности преобразовывая её к интерфейсам ICollection,
      /// ICollection_T и IReadOnlyCollection_T</summary>
      /// <param name="enumeration">Последовательность</param>
      /// <returns>Число элементов или null если число найти не удалось</returns>
      [CanBeNull]
      [CollectionAccess(CollectionAccessType.None)]
      [ContractAnnotation("enumeration: Null => null")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int? TryGetCountOrCapacity<T>([CanBeNull, NoEnumeration] this IEnumerable<T> enumeration)
      {
        int result;
        return !enumeration.TryGetCountOrCapacity(out result) ? new int?() : new int?(result);
      }

      /// <summary>Попытаться получиться кол-во элементов последовательности преобразовывая её к интерфейсам ICollection,
      /// ICollection_T и IReadOnlyCollection_T</summary>
      /// <typeparam name="T">Generic type parameter</typeparam>
      /// <param name="enumeration">Последовательность</param>
      /// <param name="result">[out] число элементов или вместимость из ICapacityOwner, если число найти не удалось,
      /// иначе -1</param>
      /// <returns>True, если число элементов или вместимость из ICapacityOwner удалось найти, иначе false</returns>
      [CollectionAccess(CollectionAccessType.None)]
      [ContractAnnotation("enumeration: Null => false")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool TryGetCountOrCapacity<T>([CanBeNull, NoEnumeration] this IEnumerable<T> enumeration, out int result)
      {
        if (enumeration != null)
        {
          if (enumeration.TryGetCount(out result))
            return true;
          if (enumeration is ICapacity capacity)
          {
            result = capacity.Capacity;
            return true;
          }
        }
        result = -1;
        return false;
      }

      /// <summary>Попытаться получиться кол-во элементов последовательности преобразовывая её к интерфейсам ICollection,
      /// ICollection_T и IReadOnlyCollection_T</summary>
      /// <param name="enumeration">Последовательность</param>
      /// <returns>Число элементов или null если число найти не удалось</returns>
      [CanBeNull]
      [CollectionAccess(CollectionAccessType.None)]
      [ContractAnnotation("enumeration: Null => null")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int? TryGetCount<T>([CanBeNull, NoEnumeration] this IEnumerable<T> enumeration)
      {
        int result;
        return !enumeration.TryGetCount(out result) ? new int?() : new int?(result);
      }

      /// <summary>Попытаться получиться кол-во элементов последовательности преобразовывая её к интерфейсам ICollection,
      /// ICollection_T и IReadOnlyCollection_T</summary>
      /// <typeparam name="T">Generic type parameter</typeparam>
      /// <param name="enumeration">Последовательность</param>
      /// <param name="result">[out] Число элементов если число удалось найти, иначе -1</param>
      /// <returns>True, если число элементов удалось найти, иначе false</returns>
      [CollectionAccess(CollectionAccessType.None)]
      [ContractAnnotation("enumeration: Null => false")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool TryGetCount<T>([CanBeNull, NoEnumeration] this IEnumerable<T> enumeration, out int result, bool force = false)
      {
        switch (enumeration)
        {
          case ICollection collection:
            result = collection.Count;
            return true;
          case ICollection<T> objs1:
            result = objs1.Count;
            return true;
          case IReadOnlyCollection<T> objs2:
            result = objs2.Count;
            return true;
          default:
            result = -1;
            return false;
        }
      }

      /// <summary>Попытаться получиться кол-во элементов последовательности преобразовывая её к интерфейсам ICollection,
      /// ICollection_T и IReadOnlyCollection_T</summary>
      /// <typeparam name="T">Generic type parameter</typeparam>
      /// <param name="enumeration">Последовательность</param>
      /// <param name="result">[out] Число элементов если число удалось найти, иначе -1</param>
      /// <returns>True, если число элементов удалось найти, иначе false</returns>
      [CollectionAccess(CollectionAccessType.None)]
      [ContractAnnotation("enumeration: Null => false")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int GetCount<T>([NotNull] this IEnumerable<T> enumeration)
      {
        switch (enumeration)
        {
          case ICollection collection:
            return collection.Count;
          case ICollection<T> objs1:
            return objs1.Count;
          case IReadOnlyCollection<T> objs2:
            return objs2.Count;
          default:
            return enumeration.Count();
        }
      }
    }
}
