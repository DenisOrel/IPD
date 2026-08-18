
// Type: Intermech.Interfaces.GenericListHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Помощник для титпизированных списков</summary>
    public sealed class GenericListHelper
    {
      /// <summary>Удаление дубликатов из списка</summary>
      /// <remarks>Внимание - список сортируется !!</remarks>
      /// <param name="list"></param>
      /// <returns></returns>
      public static bool MakeUnique<T>(List<T> list)
      {
        return GenericListHelper.MakeUnique<T>(list, (Comparison<T>) null);
      }

      /// <summary>Удаление дубликатов из списка</summary>
      /// <remarks>Внимание - список сортируется !!</remarks>
      /// <param name="list"></param>
      /// <param name="comparison"></param>
      /// <returns></returns>
      public static bool MakeUnique<T>(List<T> list, Comparison<T> comparison)
      {
        if (list == null || list.Count == 0)
          return false;
        if (comparison != null)
          list.Sort(comparison);
        else
          list.Sort();
        for (int index = list.Count - 1; index > 0; --index)
        {
          if ((comparison != null ? comparison(list[index], list[index - 1]) : Comparer<T>.Default.Compare(list[index], list[index - 1])) == 0)
            list.RemoveAt(index);
        }
        return true;
      }

      /// <summary>Сравнение данных из двух списков</summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="aList">Список с данными</param>
      /// <param name="bList">Список - образец с корорым сравниваем</param>
      /// <param name="resultData">Результирующий список с данными</param>
      /// <returns></returns>
      public static bool GetDifference<T>(IList<T> aList, IList<T> bList, out List<T> resultData)
      {
        return GenericListHelper.GetDifference<T>(aList, bList, GenericListHelper.SearchMode.smNotExistInB, out resultData);
      }

      /// <summary>Сравнение данных из двух списков</summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="aList">Список с данными</param>
      /// <param name="bList">Список - образец с корорым сравниваем</param>
      /// <param name="searchMode">Режим сравнения</param>
      /// <param name="resultData">Результирующий список с данными</param>
      /// <returns></returns>
      public static bool GetDifference<T>(
        IList<T> aList,
        IList<T> bList,
        GenericListHelper.SearchMode searchMode,
        out List<T> resultData)
      {
        return GenericListHelper.GetDifference<T>(aList, bList, searchMode, out resultData, (IComparer<T>) null);
      }

      /// <summary>Сравнение данных из двух списков</summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="aList">Список с данными</param>
      /// <param name="bList">Список - образец с корорым сравниваем</param>
      /// <param name="searchMode">Режим сравнения</param>
      /// <param name="resultData">Результирующий список с данными (может быть null)</param>
      /// <param name="comparer"></param>
      /// <returns></returns>
      public static bool GetDifference<T>(
        IList<T> aList,
        IList<T> bList,
        GenericListHelper.SearchMode searchMode,
        out List<T> resultData,
        IComparer<T> comparer)
      {
        resultData = (List<T>) null;
        bool flag = false;
        IList<T> collection1;
        IList<T> collection2;
        switch (searchMode)
        {
          case GenericListHelper.SearchMode.smNotExistInA:
            collection1 = bList;
            collection2 = aList;
            break;
          case GenericListHelper.SearchMode.smNotExistInB:
            collection1 = aList;
            collection2 = bList;
            break;
          case GenericListHelper.SearchMode.smExistInBoth:
            if (aList == null || bList == null)
              return false;
            if (aList.Count > bList.Count)
            {
              collection1 = bList;
              collection2 = aList;
            }
            else
            {
              collection1 = aList;
              collection2 = bList;
            }
            flag = true;
            break;
          default:
            return false;
        }
        if (collection1 == null)
          return false;
        if (collection2 == null || collection2.Count == 0)
        {
          resultData = new List<T>((IEnumerable<T>) collection1);
          return true;
        }
        resultData = new List<T>(collection1.Count);
        List<T> objList = new List<T>((IEnumerable<T>) collection2);
        objList.Sort(comparer);
        foreach (T obj in (IEnumerable<T>) collection1)
        {
          if (objList.BinarySearch(obj, comparer) >= 0)
          {
            if (flag)
              resultData.Add(obj);
          }
          else if (!flag)
            resultData.Add(obj);
        }
        return true;
      }

      /// <summary>Сравнение данных из двух списков</summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="aList">Список с данными</param>
      /// <param name="bList">Список - образец с корорым сравниваем</param>
      /// <returns></returns>
      public static int Compare<T>(IList<T> aList, IList<T> bList)
      {
        return GenericListHelper.Compare<T>(aList, bList, (IComparer<T>) null);
      }

      /// <summary>Сравнение данных из двух списков</summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="aList">Список с данными</param>
      /// <param name="bList">Список - образец с корорым сравниваем</param>
      /// <param name="comparer"></param>
      /// <returns></returns>
      public static int Compare<T>(IList<T> aList, IList<T> bList, IComparer<T> comparer)
      {
        if (aList == null)
          return bList != null ? -1 : 0;
        if (bList == null)
          return 1;
        int num = aList.Count.CompareTo(bList.Count);
        if (num != 0)
          return num;
        comparer = comparer ?? (IComparer<T>) Comparer<T>.Default;
        if (aList.Count > 0)
        {
          for (int index = 0; index < aList.Count; ++index)
          {
            num = comparer.Compare(aList[index], bList[index]);
            if (num != 0)
              return num;
          }
        }
        return num;
      }

      /// <summary>"Разбиение" списка на куски заданной размерности</summary>
      /// <param name="list"></param>
      /// <param name="chankSize">
      /// Размерность куска, на который деляться данные
      /// Если chankSize = 0, возвращаем пустой список
      /// Если chankSize меньше 0, возвращаем оригинальный список
      /// </param>
      /// <param name="strictChankMode">Режим "строгой разамерности". В данном
      /// режиме возвращается все списки строго размерности равной  chankSize (в случае не пустого исходного списка).
      /// Недостающие элементы добавляются как defaulValue</param>
      /// <param name="defaultValue"></param>
      /// <returns></returns>
      public static List<T>[] SplitByChanks<T>(
        IList<T> list,
        int chankSize,
        bool strictChankMode = false,
        T defaultValue = null)
      {
        List<List<T>> objListList1 = new List<List<T>>();
        if (list == null || chankSize == 0)
          return objListList1.ToArray();
        if (chankSize < 0)
        {
          List<List<T>> objListList2 = objListList1;
          if (!(list is List<T> objList))
            objList = new List<T>((IEnumerable<T>) list);
          objListList2.Add(objList);
          return objListList1.ToArray();
        }
        objListList1.Capacity = list.Count % chankSize;
        List<T> objList1 = new List<T>(chankSize);
        foreach (T obj in (IEnumerable<T>) list)
        {
          objList1.Add(obj);
          if (objList1.Count == chankSize)
          {
            objListList1.Add(objList1);
            objList1 = new List<T>(chankSize);
          }
        }
        if (objList1.Count > 0 || objListList1.Count == 0)
        {
          if (strictChankMode && objList1.Count > 0)
          {
            for (int count = objList1.Count; count < chankSize; ++count)
              objList1.Add(defaultValue);
          }
          objListList1.Add(objList1);
        }
        return objListList1.ToArray();
      }

      /// <summary>"Разбиение" списка на куски заданной размерности</summary>
      /// <param name="list"></param>
      /// <param name="chankSize"></param>
      /// <returns></returns>
      public static Dictionary<T, U>[] SplitByChanks<T, U>(IDictionary<T, U> list, int chankSize)
      {
        List<Dictionary<T, U>> dictionaryList = new List<Dictionary<T, U>>();
        if (list == null || chankSize <= 0)
          return dictionaryList.ToArray();
        dictionaryList.Capacity = list.Count % chankSize;
        Dictionary<T, U> dictionary = new Dictionary<T, U>(chankSize);
        foreach (KeyValuePair<T, U> keyValuePair in (IEnumerable<KeyValuePair<T, U>>) list)
        {
          dictionary.Add(keyValuePair.Key, keyValuePair.Value);
          if (dictionary.Count == chankSize)
          {
            dictionaryList.Add(dictionary);
            dictionary = new Dictionary<T, U>(chankSize);
          }
        }
        if (dictionary.Count > 0 || dictionaryList.Count == 0)
          dictionaryList.Add(dictionary);
        return dictionaryList.ToArray();
      }

      /// <summary>Режим поиска / сравнения</summary>
      public enum SearchMode
      {
        /// <summary>Отсутсвует в списке А</summary>
        smNotExistInA,
        /// <summary>Отсутствует в списке B</summary>
        smNotExistInB,
        /// <summary>Присутствует в обоих списках</summary>
        smExistInBoth,
      }
    }
}
