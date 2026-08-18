
// Type: Intermech.Collections.CollectionUtils
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Collections
{
    public static class CollectionUtils
    {
      public static List<T> CreateList<T>(T item)
      {
        return new List<T>() { item };
      }

      public static List<T> CreateList<T>(T item1, T item2)
      {
        return new List<T>() { item1, item2 };
      }

      public static List<T> CreateList<T>(T item1, T item2, T item3)
      {
        return new List<T>() { item1, item2, item3 };
      }

      public static List<T> CreateList<T>(T item1, T item2, T item3, T item4)
      {
        return new List<T>() { item1, item2, item3, item4 };
      }

      public static bool ContentEqual<T>(ICollection<T> collection1, ICollection<T> collection2)
      {
        if (collection1 == null)
          throw new ArgumentNullException(nameof (collection1));
        if (collection2 == null)
          throw new ArgumentNullException(nameof (collection2));
        if (collection1.Count != collection2.Count)
          return false;
        foreach (T obj in (IEnumerable<T>) collection1)
        {
          if (!collection2.Contains(obj))
            return false;
        }
        return true;
      }

      public static void EnsureCapacity<T>(List<T> list, int capacity)
      {
        if (list == null)
          throw new ArgumentNullException(nameof (list));
        if (capacity < 0)
          throw new ArgumentOutOfRangeException(nameof (capacity));
        list.Capacity = Math.Max(list.Capacity, capacity);
      }

      public static void EnsureNewItemsCapacity<T>(List<T> list, int newItemsCount)
      {
        if (list == null)
          throw new ArgumentNullException(nameof (list));
        if (newItemsCount < 0)
          throw new ArgumentOutOfRangeException(nameof (newItemsCount));
        int num1 = list.Capacity - list.Count;
        int num2 = newItemsCount - num1;
        if (num2 <= 0)
          return;
        list.Capacity += num2;
      }

      public static T TryGetFirstItem<T>(IEnumerable<T> collection)
      {
        IEnumerator<T> enumerator = collection != null ? collection.GetEnumerator() : throw new ArgumentNullException(nameof (collection));
        return !enumerator.MoveNext() ? default (T) : enumerator.Current;
      }

      public static T GetFirstItem<T>(IEnumerable<T> collection)
      {
        IEnumerator<T> enumerator = collection != null ? collection.GetEnumerator() : throw new ArgumentNullException(nameof (collection));
        return enumerator.MoveNext() ? enumerator.Current : throw new InvalidOperationException("Collection is empty.");
      }

      public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
      {
        if (collection == null)
          throw new ArgumentNullException(nameof (collection));
        if (items == null)
          throw new ArgumentNullException(nameof (items));
        foreach (T obj in items)
          collection.Add(obj);
      }

      public static int AddSorted<T>(List<T> list, T item)
      {
        int num = list != null ? list.BinarySearch(item) : throw new ArgumentNullException(nameof (list));
        if (num < 0)
          list.Insert(~num, item);
        return num;
      }

      public static int AddSorted<T>(List<T> list, T item, IComparer<T> comparer)
      {
        if (list == null)
          throw new ArgumentNullException(nameof (list));
        if (comparer == null)
          throw new ArgumentNullException(nameof (comparer));
        int num = list.BinarySearch(item, comparer);
        if (num < 0)
          list.Insert(~num, item);
        return num;
      }

      /// <summary>
      /// Добавляет элемент в коллекцию, но только в том случае, если указанный элемент еще не содержится в коллекции.
      /// </summary>
      /// <typeparam name="T">Тип элементов коллекции</typeparam>
      /// <param name="collection">Коллекция элементов</param>
      /// <param name="item">Добавляемый элемент</param>
      /// <returns>true - элемент был добавлен в коллекцию, false - элемент уже был в коллекции</returns>
      public static bool AddNew<T>(ICollection<T> collection, T item)
      {
        if (collection == null)
          throw new ArgumentNullException(nameof (collection));
        if (collection.Contains(item))
          return false;
        collection.Add(item);
        return true;
      }

      /// <summary>
      /// Добавляет элемент в коллекцию, но только в том случае, если указанный элемент еще не содержится в коллекции.
      /// </summary>
      /// <typeparam name="T">Тип элементов коллекции</typeparam>
      /// <param name="collection">Коллекция элементов</param>
      /// <param name="item">Добавляемый элемент</param>
      /// <param name="comparer">Объект для сравнения элементов коллекции</param>
      /// <returns>true - элемент был добавлен в коллекцию, false - элемент уже был в коллекции</returns>
      public static bool AddNew<T>(ICollection<T> collection, T item, IEqualityComparer<T> comparer)
      {
        if (CollectionUtils.Contains<T>((IEnumerable<T>) collection, item, comparer))
          return false;
        collection.Add(item);
        return true;
      }

      public static bool RemoveSorted<T>(List<T> list, T item)
      {
        int index = list != null ? list.BinarySearch(item) : throw new ArgumentNullException(nameof (list));
        if (index < 0)
          return false;
        list.RemoveAt(index);
        return true;
      }

      public static bool RemoveSorted<T>(List<T> list, T item, IComparer<T> comparer)
      {
        if (list == null)
          throw new ArgumentNullException(nameof (list));
        if (comparer == null)
          throw new ArgumentNullException(nameof (comparer));
        int index = list.BinarySearch(item, comparer);
        if (index < 0)
          return false;
        list.RemoveAt(index);
        return true;
      }

      public static void RemoveDuplicates<T>(List<T> list)
      {
        if (list == null)
          throw new ArgumentNullException(nameof (list));
        if (list.Count == 0)
          return;
        CollectionUtils.RemoveDuplicatesCore<T>(list, (IEqualityComparer<T>) EqualityComparer<T>.Default);
      }

      public static void RemoveDuplicates<T>(List<T> list, IEqualityComparer<T> comparer)
      {
        if (list == null)
          throw new ArgumentNullException(nameof (list));
        if (comparer == null)
          throw new ArgumentNullException(nameof (comparer));
        if (list.Count == 0)
          return;
        CollectionUtils.RemoveDuplicatesCore<T>(list, comparer);
      }

      private static void RemoveDuplicatesCore<T>(List<T> list, IEqualityComparer<T> comparer)
      {
        Stack<int> intStack = new Stack<int>();
        for (int index1 = 0; index1 < list.Count; ++index1)
        {
          T y = list[index1];
          for (int index2 = index1 + 1; index2 < list.Count; ++index2)
          {
            if (comparer.Equals(list[index2], y))
              intStack.Push(index2);
          }
          while (intStack.Count != 0)
          {
            int index3 = intStack.Pop();
            list.RemoveAt(index3);
          }
        }
      }

      public static List<T2> ConvertAsList<T1, T2>(
        ICollection<T1> collection,
        Converter<T1, T2> converter)
      {
        if (collection == null)
          throw new ArgumentNullException(nameof (collection));
        if (converter == null)
          throw new ArgumentNullException(nameof (converter));
        List<T2> objList = new List<T2>(collection.Count);
        foreach (T1 input in (IEnumerable<T1>) collection)
          objList.Add(converter(input));
        return objList;
      }

      public static LinkedList<T2> ConvertAsLinkedList<T1, T2>(
        ICollection<T1> collection,
        Converter<T1, T2> converter)
      {
        if (collection == null)
          throw new ArgumentNullException(nameof (collection));
        if (converter == null)
          throw new ArgumentNullException(nameof (converter));
        LinkedList<T2> linkedList = new LinkedList<T2>();
        foreach (T1 input in (IEnumerable<T1>) collection)
          linkedList.AddLast(converter(input));
        return linkedList;
      }

      public static T2[] ConvertAsArray<T1, T2>(ICollection<T1> collection, Converter<T1, T2> converter)
      {
        if (collection == null)
          throw new ArgumentNullException(nameof (collection));
        if (converter == null)
          throw new ArgumentNullException(nameof (converter));
        T2[] objArray = new T2[collection.Count];
        int num = 0;
        foreach (T1 input in (IEnumerable<T1>) collection)
          objArray[num++] = converter(input);
        return objArray;
      }

      public static void Transform<T>(IList<T> list, Converter<T, T> transform)
      {
        if (list == null)
          throw new ArgumentNullException(nameof (list));
        if (transform == null)
          throw new ArgumentNullException(nameof (transform));
        for (int index = 0; index < list.Count; ++index)
          list[index] = transform(list[index]);
      }

      public static void Transform<T>(LinkedList<T> list, Converter<T, T> transform)
      {
        if (list == null)
          throw new ArgumentNullException(nameof (list));
        if (transform == null)
          throw new ArgumentNullException(nameof (transform));
        LinkedListNode<T> linkedListNode;
        for (LinkedListNode<T> node = list.First; node != null; node = linkedListNode.Next)
        {
          linkedListNode = list.AddAfter(node, transform(node.Value));
          list.Remove(node);
        }
      }

      public static LinkedList<T> FindAllAsLinkedList<T>(IEnumerable<T> collection, Predicate<T> match)
      {
        if (collection == null)
          throw new ArgumentNullException(nameof (collection));
        if (match == null)
          throw new ArgumentNullException(nameof (match));
        LinkedList<T> allAsLinkedList = new LinkedList<T>();
        foreach (T obj in collection)
        {
          if (match(obj))
            allAsLinkedList.AddLast(obj);
        }
        return allAsLinkedList;
      }

      public static List<T> FindAllAsList<T>(ICollection<T> collection, Predicate<T> match)
      {
        if (collection == null)
          throw new ArgumentNullException(nameof (collection));
        if (match == null)
          throw new ArgumentNullException(nameof (match));
        List<T> allAsList = new List<T>(collection.Count);
        foreach (T obj in (IEnumerable<T>) collection)
        {
          if (match(obj))
            allAsList.Add(obj);
        }
        return allAsList;
      }

      public static bool Contains<T>(IEnumerable<T> collection, T item)
      {
        if (collection == null)
          throw new ArgumentNullException(nameof (collection));
        foreach (T x in collection)
        {
          if (EqualityComparer<T>.Default.Equals(x, item))
            return true;
        }
        return false;
      }

      public static bool Contains<T>(IEnumerable<T> collection, T item, IEqualityComparer<T> comparer)
      {
        if (collection == null)
          throw new ArgumentNullException(nameof (collection));
        if (comparer == null)
          throw new ArgumentNullException(nameof (comparer));
        foreach (T x in collection)
        {
          if (comparer.Equals(x, item))
            return true;
        }
        return false;
      }

      public static bool Exists<T>(IEnumerable<T> collection, Predicate<T> match)
      {
        if (collection == null)
          throw new ArgumentNullException(nameof (collection));
        if (match == null)
          throw new ArgumentNullException(nameof (match));
        foreach (T obj in collection)
        {
          if (match(obj))
            return true;
        }
        return false;
      }

      public static int IndexOf<T>(IEnumerable<T> collection, Predicate<T> match)
      {
        if (collection == null)
          throw new ArgumentNullException(nameof (collection));
        if (match == null)
          throw new ArgumentNullException(nameof (match));
        int num = 0;
        foreach (T obj in collection)
        {
          if (match(obj))
            return num;
          ++num;
        }
        return -1;
      }

      public static int IndexOf<T>(IEnumerable<T> collection, int startIndex, Predicate<T> match)
      {
        if (collection == null)
          throw new ArgumentNullException(nameof (collection));
        if (startIndex < 0)
          throw new ArgumentOutOfRangeException(nameof (startIndex));
        if (match == null)
          throw new ArgumentNullException(nameof (match));
        int num = 0;
        foreach (T obj in collection)
        {
          if (num >= startIndex && match(obj))
            return num;
          ++num;
        }
        return -1;
      }

      public static int Count<T>(IEnumerable<T> collection, Predicate<T> match)
      {
        if (collection == null)
          throw new ArgumentNullException(nameof (collection));
        if (match == null)
          throw new ArgumentNullException(nameof (match));
        int num = 0;
        foreach (T obj in collection)
        {
          if (match(obj))
            ++num;
        }
        return num;
      }

      public static T Find<T>(IEnumerable<T> collection, Predicate<T> match)
      {
        if (collection == null)
          throw new ArgumentNullException(nameof (collection));
        if (match == null)
          throw new ArgumentNullException(nameof (match));
        foreach (T obj in collection)
        {
          if (match(obj))
            return obj;
        }
        return default (T);
      }

      public static LinkedList<T> ExtractAsLinkedList<T>(IList<T> list, Predicate<T> match)
      {
        if (list == null)
          throw new ArgumentNullException(nameof (list));
        if (match == null)
          throw new ArgumentNullException(nameof (match));
        if (list.IsReadOnly)
          throw new NotSupportedException("Can't extract items from a list object, it's read-only.");
        LinkedList<T> asLinkedList = new LinkedList<T>();
        int index = 0;
        while (index < list.Count)
        {
          if (match(list[index]))
          {
            asLinkedList.AddLast(list[index]);
            list.RemoveAt(index);
          }
          else
            ++index;
        }
        return asLinkedList;
      }

      public static List<T> ExtractAsList<T>(IList<T> list, Predicate<T> match)
      {
        if (list == null)
          throw new ArgumentNullException(nameof (list));
        if (match == null)
          throw new ArgumentNullException(nameof (match));
        List<T> asList = !list.IsReadOnly ? new List<T>(list.Count) : throw new NotSupportedException("Can't extract items from a list object, it's read-only.");
        int index = 0;
        while (index < list.Count)
        {
          if (match(list[index]))
          {
            asList.Add(list[index]);
            list.RemoveAt(index);
          }
          else
            ++index;
        }
        return asList;
      }

      public static T TryExtract<T>(IList<T> list, Predicate<T> match)
      {
        if (list == null)
          throw new ArgumentNullException(nameof (list));
        if (match == null)
          throw new ArgumentNullException(nameof (match));
        if (list.IsReadOnly)
          throw new NotSupportedException("Can't extract items from a list object, it's read-only.");
        for (int index = 0; index < list.Count; ++index)
        {
          if (match(list[index]))
          {
            T obj = list[index];
            list.RemoveAt(index);
            return obj;
          }
        }
        return default (T);
      }

      public static void RemoveAll<T>(IList<T> list, Predicate<T> match)
      {
        if (list == null)
          throw new ArgumentNullException(nameof (list));
        if (match == null)
          throw new ArgumentNullException(nameof (match));
        if (list.IsReadOnly)
          throw new NotSupportedException("Can't remove items from a list object, it's read-only.");
        int index = 0;
        while (index < list.Count)
        {
          if (match(list[index]))
            list.RemoveAt(index);
          else
            ++index;
        }
      }

      public static void RemoveAll<T>(IIndexedCollection<T> collection, Predicate<T> match)
      {
        if (collection == null)
          throw new ArgumentNullException(nameof (collection));
        if (match == null)
          throw new ArgumentNullException(nameof (match));
        if (collection.IsReadOnly)
          throw new NotSupportedException("Can't remove items from a list object, it's read-only.");
        int index = 0;
        while (index < collection.Count)
        {
          if (match(collection[index]))
            collection.RemoveAt(index);
          else
            ++index;
        }
      }

      public static void RemoveAll<T>(ICollection<T> collection, IEnumerable<T> items)
      {
        if (collection == null)
          throw new ArgumentNullException(nameof (collection));
        if (items == null)
          throw new ArgumentNullException(nameof (items));
        if (collection.IsReadOnly)
          throw new NotSupportedException("Can't remove items from a list object, it's read-only.");
        foreach (T obj in items)
          collection.Remove(obj);
      }

      public static TResult FoldLeft<T, TResult>(
        TResult initialValue,
        IEnumerable<T> collection,
        Func<TResult, T, TResult> function)
      {
        if (collection == null)
          throw new ArgumentNullException(nameof (collection));
        if (function == null)
          throw new ArgumentNullException(nameof (function));
        TResult result = initialValue;
        foreach (T obj in collection)
          result = function(result, obj);
        return result;
      }

      public static T[] ToArray<T>(ICollection<T> collection)
      {
        T[] array = collection != null ? new T[collection.Count] : throw new ArgumentNullException(nameof (collection));
        collection.CopyTo(array, 0);
        return array;
      }
    }
}
