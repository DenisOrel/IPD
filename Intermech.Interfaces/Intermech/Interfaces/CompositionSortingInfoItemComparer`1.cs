
// Type: Intermech.Interfaces.CompositionSortingInfoItemComparer`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс для сортировки элементов согласно правилу и направлению
    /// </summary>
    public class CompositionSortingInfoItemComparer<T> : ICompositionSortingComparer<T>, IComparer<T> where T : CompositionSortingInfoItem
    {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="sortRule"></param>
      /// <param name="directionMode"></param>
      public CompositionSortingInfoItemComparer(
        [CanBeNull] CompositionsAutosortRule sortingRule,
        CompositionSortingDirectionMode directionMode)
      {
        this.SortingRule = sortingRule;
        this.DirectionMode = directionMode;
      }

      /// <summary>Правило сортировки</summary>
      public CompositionsAutosortRule SortingRule { get; }

      /// <summary>Направление сортировки</summary>
      public CompositionSortingDirectionMode DirectionMode { get; }

      /// <summary>
      /// Реализация IComparer для сортировки согласно заданным параметрам
      /// </summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <returns></returns>
      public int Compare(T x, T y)
      {
        int num1 = 0;
        if (this.SortingRule == null)
          return num1;
        int num2;
        if ((object) x == null)
        {
          if ((object) y == null)
            return num1;
          num2 = -1;
        }
        else if ((object) y == null)
        {
          num2 = 1;
        }
        else
        {
          num2 = this.SortingRule.CompareTo(x.PartObjType, x.RelTypeID, y.RelTypeID, x.PartObjType, y.PartObjType, true);
          if (num2 == 0)
            num2 = x.Sorting.CompareTo(y.Sorting);
        }
        return this.DirectionMode != CompositionSortingDirectionMode.Desc ? num2 : -num2;
      }
    }
}
