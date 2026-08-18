
// Type: Intermech.Interfaces.Compositions.CompositionObjects
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>Коллекция описаний объектов состава</summary>
    [DebuggerDisplay("Count: {Count}; Parent: {Parent}")]
    [Serializable]
    public class CompositionObjects : List<CompositionObject>, ICloneable
    {
      /// <summary>Родительская коллекция описаний объектов состава</summary>
      private CompositionObjects parent;

      /// <summary>Создать пустой экземпляр класса</summary>
      public CompositionObjects()
        : base(0)
      {
      }

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="parent">Родительская коллекция</param>
      public CompositionObjects(CompositionObjects parent)
        : base(0)
      {
        this.parent = parent;
      }

      /// <summary>Родительская коллекция описаний объектов состава</summary>
      public CompositionObjects Parent
      {
        [DebuggerStepThrough] get => this.parent;
      }

      /// <summary>
      /// Отыскать в коллекции (начиная с её корневой записи) описание объекта состава с указанным идентификатором связи.
      /// Поиск также будет проходить в дочерних коллекциях.
      /// </summary>
      /// <param name="F_PRJLINK_ID">Уникальный в пределах коллекции идентификатор связи</param>
      /// <returns>null, если описание объекта состава не найдено</returns>
      public virtual CompositionObject FindRelationFromRoot(long F_PRJLINK_ID)
      {
        if (F_PRJLINK_ID == 0L)
          return (CompositionObject) null;
        CompositionObjects compositionObjects = this;
        while (compositionObjects.Parent != null)
          compositionObjects = compositionObjects.Parent;
        return compositionObjects.FindRelation(F_PRJLINK_ID);
      }

      /// <summary>
      /// Отыскать в коллекции описание объекта состава с указанным идентификатором связи.
      /// Поиск также будет проходить в дочерних коллекциях.
      /// </summary>
      /// <param name="F_PRJLINK_ID">Уникальный в пределах коллекции идентификатор связи</param>
      /// <returns>null, если описание объекта состава не найдено</returns>
      public virtual CompositionObject FindRelation(long F_PRJLINK_ID)
      {
        if (F_PRJLINK_ID == 0L)
          return (CompositionObject) null;
        for (int index = 0; index < this.Count; ++index)
        {
          CompositionObject relation = this[index].FindRelation(F_PRJLINK_ID);
          if (relation != null)
            return relation;
        }
        return (CompositionObject) null;
      }

      /// <summary>Добавить описание объекта состава в коллекцию</summary>
      /// <param name="obj">Описание объекта состава</param>
      /// <returns>Описание объекта состава</returns>
      public virtual CompositionObject Add(CompositionObject obj)
      {
        if (obj != null)
        {
          base.Add(obj);
          obj.Parent = this;
        }
        return obj;
      }

      /// <summary>
      /// Полное присваивание другого списка описаний объектов состава
      /// </summary>
      /// <param name="source">Источник</param>
      public virtual void Assign(CompositionObjects source)
      {
        this.Clear();
        if (source == null)
          return;
        this.parent = source.Parent;
        for (int index = 0; index < source.Count; ++index)
          this.Add(source[index].Clone() as CompositionObject);
      }

      /// <summary>
      /// Получение полного списка описаний объектов состава, включая объекты состава дочерних коллекций
      /// </summary>
      /// <param name="list">Полный список описаний объектов состава, включая объекты состава дочерних коллекций</param>
      protected virtual void InternalExtractRelations(List<CompositionObject> list)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          list.Add(this[index]);
          this[index].Items.InternalExtractRelations(list);
        }
      }

      /// <summary>
      /// Получение полного списка описаний объектов состава, включая объекты состава дочерних коллекций
      /// </summary>
      /// <returns>Полный список описаний объектов состава, включая объекты состава дочерних коллекций</returns>
      public virtual List<CompositionObject> ExtractRelations()
      {
        List<CompositionObject> list = new List<CompositionObject>();
        this.InternalExtractRelations(list);
        return list;
      }

      /// <summary>
      /// Отыскивает первого, не равного null, "родителя". Если такого нет, вернёт parObject
      /// </summary>
      /// <returns>Родительский узел, у которого Parent = null</returns>
      public virtual CompositionObjects FindRootParent()
      {
        if (this.Parent == null)
          return this;
        CompositionObjects compositionObjects = this;
        CompositionObjects rootParent = this;
        for (; compositionObjects.Parent != null; compositionObjects = compositionObjects.Parent)
          rootParent = compositionObjects;
        return rootParent;
      }

      /// <summary>Создать копию экземпляра класса</summary>
      /// <returns>Копия экземпляра класса</returns>
      public object Clone()
      {
        CompositionObjects compositionObjects = new CompositionObjects(this.parent);
        compositionObjects.Assign(this);
        return (object) compositionObjects;
      }

      /// <summary>
      /// Выполнить сортировку коллекции и её дочерних элементов
      /// </summary>
      /// <param name="comparer">Сравниватель</param>
      public new virtual void Sort(IComparer<CompositionObject> comparer)
      {
        base.Sort(comparer);
        for (int index = 0; index < this.Count; ++index)
          this[index].Items.Sort(comparer);
      }
    }
}
