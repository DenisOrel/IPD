
// Type: Intermech.Interfaces.DeletingObjects
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>Коллекция описаний удаляемых объектов</summary>
    [DebuggerDisplay("Count: {Count}; Parent: {Parent}")]
    [Serializable]
    public class DeletingObjects : List<DeletingObject>, ICloneable
    {
      /// <summary>Родительская коллекция описаний удаляемых объектов</summary>
      private DeletingObjects parent;

      /// <summary>Создать пустой экземпляр класса</summary>
      public DeletingObjects()
        : base(0)
      {
      }

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="parent">Родительская коллекция</param>
      public DeletingObjects(DeletingObjects parent)
        : base(0)
      {
        this.parent = parent;
      }

      /// <summary>Родительская коллекция описаний удаляемых объектов</summary>
      public DeletingObjects Parent
      {
        [DebuggerStepThrough] get => this.parent;
      }

      /// <summary>
      /// Отыскать в коллекции (начиная с её корневой записи) описание удаляемого объекта с указанным идентификатором.
      /// Поиск также будет проходить в дочерних коллекциях.
      /// </summary>
      /// <param name="objectID">Уникальный в пределах коллекции идентификатор версии объекта</param>
      /// <returns>null, если описание удаляемого объекта не найдено</returns>
      public virtual DeletingObject FindDeletingObjectFromRoot(long objectID)
      {
        if (objectID == 0L)
          return (DeletingObject) null;
        DeletingObjects deletingObjects = this;
        while (deletingObjects.Parent != null)
          deletingObjects = deletingObjects.Parent;
        return deletingObjects.FindDeletingObject(objectID);
      }

      /// <summary>
      /// Отыскать в коллекции описание удаляемого объекта с указанным идентификатором.
      /// Поиск также будет проходить в дочерних коллекциях.
      /// </summary>
      /// <param name="objectID">Уникальный в пределах коллекции идентификатор версии объекта</param>
      /// <returns>null, если описание удаляемого объекта не найдено</returns>
      public virtual DeletingObject FindDeletingObject(long objectID)
      {
        if (objectID == 0L)
          return (DeletingObject) null;
        for (int index = 0; index < this.Count; ++index)
        {
          DeletingObject deletingObject = this[index].FindDeletingObject(objectID);
          if (deletingObject != null)
            return deletingObject;
        }
        return (DeletingObject) null;
      }

      /// <summary>
      /// Добавить описание удаляемого объекта в коллекцию (самый упрощённый вариант)
      /// </summary>
      /// <param name="weight">"Весовой" коэффициент (по умолчанию - 0)</param>
      /// <param name="id">Идентификатор удаляемого объекта</param>
      /// <param name="objectID">Идентификатор версии удаляемого объекта (уникальный в пределах всей коллекции)</param>
      /// <param name="removeObject">Требуется ли удалять указанный объект</param>
      /// <returns>Найденное или новое описание удаляемого объекта</returns>
      public virtual DeletingObject Add(long weight, long id, long objectID, bool removeObject)
      {
        DeletingObject deletingObjectFromRoot = this.FindDeletingObjectFromRoot(objectID);
        if (deletingObjectFromRoot != null)
          return deletingObjectFromRoot;
        DeletingObject deletingObject = new DeletingObject(this, weight, id, objectID, removeObject);
        this.Add(deletingObject);
        return deletingObject;
      }

      /// <summary>
      /// Добавить описание удаляемого объекта в коллекцию (упрощённый вариант)
      /// </summary>
      /// <param name="weight">"Весовой" коэффициент (по умолчанию - 0)</param>
      /// <param name="id">Идентификатор удаляемого объекта</param>
      /// <param name="objectID">Идентификатор версии удаляемого объекта (уникальный в пределах всей коллекции)</param>
      /// <param name="removeObject">Требуется ли удалять указанный объект</param>
      /// <param name="removeNote">Причина удаления объекта</param>
      /// <returns>Найденное или новое описание удаляемого объекта</returns>
      public virtual DeletingObject Add(
        long weight,
        long id,
        long objectID,
        bool removeObject,
        string removeNote)
      {
        DeletingObject deletingObjectFromRoot = this.FindDeletingObjectFromRoot(objectID);
        if (deletingObjectFromRoot != null)
          return deletingObjectFromRoot;
        DeletingObject deletingObject = new DeletingObject(this, weight, id, objectID, removeObject, removeNote);
        this.Add(deletingObject);
        return deletingObject;
      }

      /// <summary>
      /// Добавить описание удаляемого объекта в коллекцию (полный вариант)
      /// </summary>
      /// <param name="weight">"Весовой" коэффициент (по умолчанию - 0)</param>
      /// <param name="id">Идентификатор удаляемого объекта</param>
      /// <param name="objectID">Идентификатор версии удаляемого объекта (уникальный в пределах всей коллекции)</param>
      /// <param name="removeObject">Требуется ли удалять указанный объект</param>
      /// <param name="objectType">Идентификатор типа удаляемого объекта</param>
      /// <param name="caption">Заголовок удаляемого объекта</param>
      /// <param name="ownerID">Идентификатор владельца объекта</param>
      /// <param name="chkOutByID">Кем объект взят на изменение</param>
      /// <param name="lcStepID">Шаг жизненного цикла</param>
      /// <param name="removeNote">Причина удаления объекта</param>
      /// <returns>Найденное или новое описание удаляемого объекта</returns>
      public virtual DeletingObject Add(
        long weight,
        long id,
        long objectID,
        bool removeObject,
        int objectType,
        string caption,
        long ownerID,
        long chkOutByID,
        int lcStepID,
        string removeNote)
      {
        DeletingObject deletingObjectFromRoot = this.FindDeletingObjectFromRoot(objectID);
        if (deletingObjectFromRoot != null)
          return deletingObjectFromRoot;
        DeletingObject deletingObject = new DeletingObject(this, weight, id, objectID, removeObject, objectType, caption, ownerID, chkOutByID, lcStepID, removeNote);
        this.Add(deletingObject);
        return deletingObject;
      }

      /// <summary>
      /// Добавить описание удаляемого объекта в коллекцию (полный вариант)
      /// </summary>
      /// <param name="weight">"Весовой" коэффициент (по умолчанию - 0)</param>
      /// <param name="id">Идентификатор удаляемого объекта</param>
      /// <param name="objectID">Идентификатор версии удаляемого объекта (уникальный в пределах всей коллекции)</param>
      /// <param name="removeObject">Требуется ли удалять указанный объект</param>
      /// <param name="objectType">Идентификатор типа удаляемого объекта</param>
      /// <param name="caption">Заголовок удаляемого объекта</param>
      /// <param name="ownerID">Идентификатор владельца объекта</param>
      /// <param name="chkOutByID">Кем объект взят на изменение</param>
      /// <param name="lcStepID">Шаг жизненного цикла</param>
      /// <param name="prjLinkID">Идентификатор связи (если объект удаляется из состава)</param>
      /// <param name="versionNo">Номер версии</param>
      /// <param name="baseVersion">Признак базовой версии</param>
      /// <param name="removeNote">Причина удаления объекта</param>
      /// <returns>Найденное или новое описание удаляемого объекта</returns>
      public virtual DeletingObject Add(
        long weight,
        long id,
        long objectID,
        bool removeObject,
        int objectType,
        string caption,
        long ownerID,
        long chkOutByID,
        int lcStepID,
        long prjLinkID,
        long versionNo,
        bool baseVersion,
        string removeNote)
      {
        DeletingObject deletingObjectFromRoot = this.FindDeletingObjectFromRoot(objectID);
        if (deletingObjectFromRoot != null)
          return deletingObjectFromRoot;
        DeletingObject deletingObject = new DeletingObject(this, weight, id, objectID, removeObject, objectType, caption, ownerID, chkOutByID, lcStepID, prjLinkID, versionNo, baseVersion, removeNote);
        this.Add(deletingObject);
        return deletingObject;
      }

      /// <summary>
      /// Полное присваивание другого списка описаний удаляемых объектов
      /// </summary>
      /// <param name="source">Источник</param>
      public virtual void Assign(DeletingObjects source)
      {
        this.Clear();
        if (source == null)
          return;
        this.parent = source.Parent;
        for (int index = 0; index < source.Count; ++index)
          this.Add(source[index].Clone() as DeletingObject);
      }

      /// <summary>
      /// Получение полного списка описаний удаляемых объектов, включая объекты дочерних коллекций
      /// </summary>
      /// <param name="list">Полный список описаний удаляемых объектов, включая объекты дочерних коллекций</param>
      protected virtual void InternalExtractDeletingObjects(List<DeletingObject> list)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (!list.Contains(this[index]))
          {
            list.Add(this[index]);
            this[index].Items.InternalExtractDeletingObjects(list);
          }
        }
      }

      /// <summary>
      /// Получение полного списка описаний удаляемых объектов, включая объекты дочерних коллекций
      /// </summary>
      /// <returns>Полный список описаний удаляемых объектов, включая объекты дочерних коллекций</returns>
      public virtual List<DeletingObject> ExtractDeletingObjects()
      {
        List<DeletingObject> list = new List<DeletingObject>();
        this.InternalExtractDeletingObjects(list);
        return list;
      }

      /// <summary>
      /// Получение полного списка описаний удаляемых объектов, включая объекты дочерних коллекций.
      /// В коллекцию попадают только положительные идентификаторы.
      /// </summary>
      /// <param name="list">Полный список описаний удаляемых объектов, включая объекты дочерних коллекций</param>
      protected virtual void InternalExtractAbsDeletingObjects(List<DeletingObject> list)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          DeletingObject deletingObject = this[index].Clone() as DeletingObject;
          deletingObject.ObjectID = Math.Abs(deletingObject.ObjectID);
          this[index].Items.InternalExtractAbsDeletingObjects(list);
          if (deletingObject.ObjectID == 0L || !list.Contains(deletingObject))
            list.Add(deletingObject);
        }
      }

      /// <summary>
      /// Получение полного списка описаний удаляемых объектов, включая объекты дочерних коллекций.
      /// В коллекцию попадают только положительные идентификаторы.
      /// </summary>
      /// <returns>Полный список описаний удаляемых объектов, включая объекты дочерних коллекций</returns>
      public virtual List<DeletingObject> ExtractAbsDeletingObjects()
      {
        List<DeletingObject> list = new List<DeletingObject>();
        this.InternalExtractAbsDeletingObjects(list);
        return list;
      }

      /// <summary>
      /// Получение полного списка идентификаторов версий удаляемых объектов, включая объекты дочерних коллекций.
      /// В коллекцию попадают только положительные идентификаторы, причём только у выделенных
      /// для удаления объектов.
      /// </summary>
      /// <param name="list">Полный список идентификаторов версий удаляемых объектов, включая объекты дочерних коллекций</param>
      protected virtual void InternalExtractAbsIDs(List<long> list)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].RemoveObject)
          {
            long objectId = this[index].ObjectID;
            if (!list.Contains(objectId))
              list.Add(objectId);
            if (objectId < 0L)
            {
              long num = Math.Abs(objectId);
              if (!list.Contains(num))
                list.Add(num);
            }
            this[index].Items.InternalExtractAbsIDs(list);
          }
        }
      }

      /// <summary>
      /// Получение полного списка идентификаторов версий удаляемых объектов, включая объекты дочерних коллекций.
      /// В коллекцию попадают только положительные идентификаторы, причём только у выделенных
      /// для удаления объектов.
      /// </summary>
      /// <returns>Полный список идентификаторов версий удаляемых объектов, включая объекты дочерних коллекций</returns>
      public virtual List<long> ExtractAbsIDs()
      {
        List<long> list = new List<long>();
        this.InternalExtractAbsIDs(list);
        return list;
      }

      /// <summary>
      /// Подсчитать, сколько описаний удаляемых объектов выделено
      /// </summary>
      /// <returns>Количество выделенных описаний удаляемых объектов</returns>
      public virtual int SelectedCount()
      {
        List<DeletingObject> deletingObjects = this.ExtractDeletingObjects();
        int num = 0;
        for (int index = 0; index < deletingObjects.Count; ++index)
        {
          if (deletingObjects[index].RemoveObject)
            ++num;
        }
        return num;
      }

      /// <summary>
      /// Отыскивает первого, не равного null, "родителя". Если такого нет, вернёт parObject
      /// </summary>
      /// <returns>Родительский узел, у которого Parent = null</returns>
      public virtual DeletingObjects FindRootParent()
      {
        if (this.Parent == null)
          return this;
        DeletingObjects deletingObjects = this;
        DeletingObjects rootParent = this;
        for (; deletingObjects.Parent != null; deletingObjects = deletingObjects.Parent)
          rootParent = deletingObjects;
        return rootParent;
      }

      /// <summary>Создать копию экземпляра класса</summary>
      /// <returns>Копия экземпляра класса</returns>
      public object Clone()
      {
        DeletingObjects deletingObjects = new DeletingObjects(this.parent);
        deletingObjects.Assign(this);
        return (object) deletingObjects;
      }

      /// <summary>
      /// Выполнить сортировку коллекции и её дочерних элементов
      /// </summary>
      public new virtual void Sort()
      {
        base.Sort();
        for (int index = 0; index < this.Count; ++index)
          this[index].Items.Sort();
      }

      /// <summary>
      /// Выполнить сортировку коллекции и её дочерних элементов
      /// </summary>
      /// <param name="comparer">Сравниватель</param>
      public new virtual void Sort(IComparer<DeletingObject> comparer)
      {
        base.Sort(comparer);
        for (int index = 0; index < this.Count; ++index)
          this[index].Items.Sort(comparer);
      }

      /// <summary>
      /// Переместить базовые версии в конец списка (очерёдность их расположения меняться не будет)
      /// </summary>
      public virtual void BaseVersionsDown()
      {
        if (this.Count == 0)
          return;
        List<DeletingObject> collection = new List<DeletingObject>();
        for (int index = this.Count - 1; index >= 0; --index)
        {
          if (this[index].BaseVersion)
          {
            collection.Insert(0, this[index]);
            this.RemoveAt(index);
          }
        }
        this.AddRange((IEnumerable<DeletingObject>) collection);
        for (int index = 0; index < this.Count; ++index)
          this[index].Items.BaseVersionsDown();
      }
    }
}
