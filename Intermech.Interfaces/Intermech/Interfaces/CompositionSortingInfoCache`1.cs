
// Type: Intermech.Interfaces.CompositionSortingInfoCache`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Кеш элементов вида CompositionSortingInfoItem</summary>
    /// <remarks>Возможно позже, если понадобиться перепишется на динамический поиск по
    /// правилам </remarks>
    public class CompositionSortingInfoCache<T> where T : CompositionSortingInfoItem
    {
      /// <summary>Интерфейс для сортировки элементов кэша</summary>
      private ICompositionSortingComparer<T> _comparer;
      /// <summary>Список элементов состава</summary>
      private readonly List<T> _infoItems = new List<T>();

      /// <summary>Получение правила сортировки связи</summary>
      /// <param name="projTypeId">Ид. типа родительского объекта</param>
      /// <param name="relTypeId">Ид. типа связи</param>
      /// <returns></returns>
      private ChildRelationType GetRuleChildRelationType(int projTypeId, int relTypeId)
      {
        if (this._comparer.SortingRule == null)
          return (ChildRelationType) null;
        int index = this._comparer.SortingRule.IndexOfParentObjectType(projTypeId, true);
        return index == -1 ? (ChildRelationType) null : this._comparer.SortingRule.ParentObjectTypes[index][relTypeId];
      }

      /// <summary>Получение индекса правила сортировки связи</summary>
      /// <param name="projTypeId">Ид. типа родительского объекта</param>
      /// <param name="relTypeId">Ид. типа связи</param>
      /// <param name="defaultValue">Значение по умолчанию (если правило не найдено)</param>
      /// <returns></returns>
      private int GetRuleChildRelationTypeIdx(int projTypeId, int relTypeId, int defaultValue)
      {
        if (this._comparer.SortingRule == null)
          return defaultValue;
        int num = this._comparer.SortingRule.GetObjectTypeVisibleRelations(projTypeId, true).IndexOf(relTypeId);
        return num == -1 ? defaultValue : num;
      }

      /// <summary>Конструктор</summary>
      /// <param name="comparer">Интерфейс с правилами сортировки элементов кэша</param>
      public CompositionSortingInfoCache([NotNull] ICompositionSortingComparer<T> comparer)
      {
        this._comparer = comparer;
      }

      /// <summary>Добавление записи в конец кэша</summary>
      /// <param name="item">Информация о состоянии сортировки элементов состава</param>
      public void AddItem(T item) => this._infoItems.Add(item);

      /// <summary>Добавление записи в начало списка</summary>
      /// <param name="item">Информация о состоянии сортировки элементов состава</param>
      public void AddFirstItem(T item) => this.InsertItem(item, 0);

      /// <summary>Добавление записи в указанную позицию списка</summary>
      /// <param name="item">Информация о состоянии сортировки элементов состава</param>
      /// <param name="index"></param>
      public void InsertItem(T item, int index) => this._infoItems.Insert(index, item);

      /// <summary>Получить предыдущую запись кэша относительно item</summary>
      /// <param name="item"></param>
      /// <returns></returns>
      public T GetPrevObject(T item)
      {
        if ((object) item == null)
          return default (T);
        int num = this._infoItems.IndexOf(item);
        switch (num)
        {
          case -1:
            return default (T);
          case 0:
            return default (T);
          default:
            return this._infoItems[num - 1];
        }
      }

      /// <summary>Получить следующую запись кэша относительно item</summary>
      /// <param name="item"></param>
      /// <returns></returns>
      public T GetNextObject(T item)
      {
        if ((object) item == null)
          return default (T);
        int num = this._infoItems.IndexOf(item);
        if (num == -1)
          return default (T);
        return num != this._infoItems.Count - 1 ? this._infoItems[num + 1] : default (T);
      }

      /// <summary>Поиск по кэшу ближайшей связи по условиям сортировки</summary>
      /// <param name="projTypeId">Ид. типа родительского объекта</param>
      /// <param name="relTypeId">Ид. типа связи</param>
      /// <param name="partTypeId">Ид. типа дочернего объекта</param>
      /// <param name="lookupMode">Режим сравнения при поиске</param>
      /// <returns></returns>
      public T FindClosedObjectRec(
        int projTypeId,
        int relTypeId,
        int partTypeId,
        CompositionSortingLookupMode lookupMode)
      {
        T closedObjectRec = default (T);
        if (projTypeId == -1 || partTypeId == -1 || relTypeId == -1)
          return default (T);
        if (this._comparer.SortingRule == null)
          return default (T);
        ChildRelationType childRelationType = this.GetRuleChildRelationType(projTypeId, relTypeId);
        int childRelationTypeIdx1 = this.GetRuleChildRelationTypeIdx(projTypeId, relTypeId, -1);
        List<T> objList = new List<T>(16 /*0x10*/);
        bool flag1 = false;
        switch (lookupMode)
        {
          case CompositionSortingLookupMode.Less:
          case CompositionSortingLookupMode.LessOnly:
            flag1 = this._comparer.DirectionMode == CompositionSortingDirectionMode.Desc;
            break;
          case CompositionSortingLookupMode.More:
          case CompositionSortingLookupMode.GreaterOnly:
            flag1 = this._comparer.DirectionMode == CompositionSortingDirectionMode.Asc;
            break;
        }
        foreach (T infoItem in this._infoItems)
        {
          int childRelationTypeIdx2 = this.GetRuleChildRelationTypeIdx(projTypeId, infoItem.RelTypeID, -1);
          if (childRelationType == null || childRelationTypeIdx1 == childRelationTypeIdx2)
          {
            if (childRelationType != null)
            {
              int num = childRelationType.CompareTo(infoItem.PartObjType, partTypeId);
              bool flag2 = false;
              switch (lookupMode)
              {
                case CompositionSortingLookupMode.Less:
                  flag2 = num <= 0;
                  break;
                case CompositionSortingLookupMode.More:
                  flag2 = num >= 0;
                  break;
                case CompositionSortingLookupMode.LessOnly:
                  flag2 = num < 0;
                  break;
                case CompositionSortingLookupMode.GreaterOnly:
                  flag2 = num > 0;
                  break;
              }
              if (flag2)
              {
                closedObjectRec = infoItem;
                if (!flag1)
                  objList.Add(closedObjectRec);
                else
                  break;
              }
            }
            else if (infoItem.RelTypeID.Equals(relTypeId) && infoItem.Sorting != -1L)
            {
              closedObjectRec = infoItem;
              if (!flag1)
                objList.Add(closedObjectRec);
              else
                break;
            }
          }
          else
          {
            int num = Math.Sign(childRelationTypeIdx1 - childRelationTypeIdx2);
            bool flag3 = false;
            switch (lookupMode)
            {
              case CompositionSortingLookupMode.Less:
              case CompositionSortingLookupMode.LessOnly:
                flag3 = num > 0;
                break;
              case CompositionSortingLookupMode.More:
              case CompositionSortingLookupMode.GreaterOnly:
                flag3 = num < 0;
                break;
            }
            if (flag3)
            {
              closedObjectRec = infoItem;
              if (!flag1)
                objList.Add(closedObjectRec);
              else
                break;
            }
          }
        }
        if (!flag1 && objList.Count != 0)
          closedObjectRec = objList[objList.Count - 1];
        return closedObjectRec;
      }

      /// <summary>
      /// Принудительная сортировка элемента состава согласно тек. правилу
      /// </summary>
      public void SortItems() => this.SortItems(this._comparer);

      /// <summary>
      /// Принудительная сортировка элементов состава согласно заданных параметрам
      /// </summary>
      /// <param name="comparer">Направление сортировки</param>
      public void SortItems([NotNull] ICompositionSortingComparer<T> comparer)
      {
        this._comparer = comparer;
        this._infoItems.Sort((IComparer<T>) this._comparer);
      }

      /// <summary>Список элементов состава</summary>
      public IList<T> InfoItems => (IList<T>) this._infoItems;
    }
}
