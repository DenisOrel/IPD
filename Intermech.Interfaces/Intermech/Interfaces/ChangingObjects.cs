
// Type: Intermech.Interfaces.ChangingObjects
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>Коллекция описаний изменяемых объектов</summary>
    [DebuggerDisplay("Count: {Count}; Parent: {Parent}")]
    [Serializable]
    public class ChangingObjects : List<ChangingObject>, ICloneable
    {
      /// <summary>Родительская коллекция описаний изменяемых объектов</summary>
      private ChangingObjects parent;

      /// <summary>Создать пустой экземпляр класса</summary>
      public ChangingObjects()
        : base(0)
      {
      }

      /// <summary>Создать экземпляр класса</summary>
      /// <param name="parent">Родительская коллекция</param>
      public ChangingObjects(ChangingObjects parent)
        : base(0)
      {
        this.parent = parent;
      }

      /// <summary>Родительская коллекция описаний изменяемых объектов</summary>
      public ChangingObjects Parent => this.parent;

      /// <summary>
      /// Отыскать в коллекции (начиная с её корневой записи) описание изменяемого объекта с указанным идентификатором.
      /// Поиск также будет проходить в дочерних коллекциях.
      /// </summary>
      /// <param name="objectID">Уникальный в пределах коллекции идентификатор версии объекта</param>
      /// <returns>null, если описание изменяемого объекта не найдено</returns>
      public virtual ChangingObject FindChangingObjectFromRoot(long objectID)
      {
        if (objectID == 0L)
          return (ChangingObject) null;
        ChangingObjects changingObjects = this;
        while (changingObjects.Parent != null)
          changingObjects = changingObjects.Parent;
        return changingObjects.FindChangingObject(objectID);
      }

      /// <summary>
      /// Отыскать в коллекции описание изменяемого объекта с указанным идентификатором.
      /// Поиск также будет проходить в дочерних коллекциях.
      /// </summary>
      /// <param name="objectID">Уникальный в пределах коллекции идентификатор версии объекта</param>
      /// <returns>null, если описание изменяемого объекта не найдено</returns>
      public virtual ChangingObject FindChangingObject(long objectID)
      {
        if (objectID == 0L)
          return (ChangingObject) null;
        for (int index = 0; index < this.Count; ++index)
        {
          ChangingObject changingObject = this[index].FindChangingObject(objectID);
          if (changingObject != null)
            return changingObject;
        }
        return (ChangingObject) null;
      }

      /// <summary>
      /// Добавить описание изменяемого объекта в коллекцию (самый упрощённый вариант)
      /// </summary>
      /// <param name="objectID">Идентификатор версии изменяемого объекта (уникальный в пределах всей коллекции)</param>
      /// <param name="newObjectID">Идентификатор новой версии изменяемого объекта</param>
      /// <param name="changingAction">Действие, выполняемое над объектом</param>
      /// <param name="applyChanges">Требуется ли применять действие к указанному объекту</param>
      /// <param name="fixApplyChanges">Если флажок равен true, пользователь не сможет изменить значение applyChanges</param>
      /// <returns>Найденное или новое описание удаляемого объекта</returns>
      public virtual ChangingObject Add(
        long objectID,
        long newObjectID,
        ObjectChangingAction changingAction,
        bool applyChanges,
        bool fixApplyChanges)
      {
        ChangingObject changingObjectFromRoot = this.FindChangingObjectFromRoot(objectID);
        if (changingObjectFromRoot != null)
          return changingObjectFromRoot;
        ChangingObject changingObject = new ChangingObject(this, objectID, newObjectID, changingAction, applyChanges, fixApplyChanges);
        this.Add(changingObject);
        return changingObject;
      }

      /// <summary>
      /// Добавить описание изменяемого объекта в коллекцию (упрощённый вариант)
      /// </summary>
      /// <param name="objectID">Идентификатор версии изменяемого объекта (уникальный в пределах всей коллекции)</param>
      /// <param name="newObjectID">Идентификатор новой версии изменяемого объекта</param>
      /// <param name="changingAction">Действие, выполняемое над объектом</param>
      /// <param name="applyChanges">Требуется ли применять действие к указанному объекту</param>
      /// <param name="fixApplyChanges">Если флажок равен true, пользователь не сможет изменить значение applyChanges</param>
      /// <param name="changingNote">Причина изменения объекта</param>
      /// <returns>Найденное или новое описание изменяемого объекта</returns>
      public virtual ChangingObject Add(
        long objectID,
        long newObjectID,
        ObjectChangingAction changingAction,
        bool applyChanges,
        bool fixApplyChanges,
        string changingNote)
      {
        ChangingObject changingObjectFromRoot = this.FindChangingObjectFromRoot(objectID);
        if (changingObjectFromRoot != null)
          return changingObjectFromRoot;
        ChangingObject changingObject = new ChangingObject(this, objectID, newObjectID, changingAction, applyChanges, fixApplyChanges, changingNote);
        this.Add(changingObject);
        return changingObject;
      }

      /// <summary>
      /// Добавить описание изменяемого объекта в коллекцию (полный вариант)
      /// </summary>
      /// <param name="objectID">Идентификатор версии изменяемого объекта (уникальный в пределах всей коллекции)</param>
      /// <param name="newObjectID">Идентификатор новой версии изменяемого объекта</param>
      /// <param name="changingAction">Действие, выполняемое над объектом</param>
      /// <param name="applyChanges">Требуется ли применять действие к указанному объекту</param>
      /// <param name="fixApplyChanges">Если флажок равен true, пользователь не сможет изменить значение applyChanges</param>
      /// <param name="objectType">Идентификатор типа изменяемого объекта</param>
      /// <param name="caption">Заголовок изменяемого объекта</param>
      /// <param name="ownerID">Идентификатор владельца объекта</param>
      /// <param name="chkOutByID">Кем объект взят на изменение</param>
      /// <param name="lcStepID">Шаг жизненного цикла</param>
      /// <param name="versionID">Номер версии объекта</param>
      /// <param name="changingNote">Причина изменения объекта</param>
      /// <returns>Найденное или новое описание изменяемого объекта</returns>
      public virtual ChangingObject Add(
        long objectID,
        long newObjectID,
        ObjectChangingAction changingAction,
        bool applyChanges,
        bool fixApplyChanges,
        int objectType,
        string caption,
        long ownerID,
        long chkOutByID,
        int lcStepID,
        long versionID,
        string changingNote)
      {
        ChangingObject changingObjectFromRoot = this.FindChangingObjectFromRoot(objectID);
        if (changingObjectFromRoot != null)
          return changingObjectFromRoot;
        ChangingObject changingObject = new ChangingObject(this, objectID, newObjectID, changingAction, applyChanges, fixApplyChanges, objectType, caption, ownerID, chkOutByID, lcStepID, versionID, changingNote);
        this.Add(changingObject);
        return changingObject;
      }

      /// <summary>
      /// Полное присваивание другого списка описаний изменяемых объектов
      /// </summary>
      /// <param name="source">Источник</param>
      public virtual void Assign(ChangingObjects source)
      {
        this.Clear();
        if (source == null)
          return;
        this.parent = source.Parent;
        for (int index = 0; index < source.Count; ++index)
          this.Add(source[index].Clone() as ChangingObject);
      }

      /// <summary>
      /// Получение полного списка описаний изменяемых объектов, включая объекты дочерних коллекций
      /// </summary>
      /// <param name="list">Полный список описаний изменяемых объектов, включая объекты дочерних коллекций</param>
      protected virtual void InternalExtractChangingObjects(List<ChangingObject> list)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          list.Add(this[index]);
          this[index].Items.InternalExtractChangingObjects(list);
        }
      }

      /// <summary>
      /// Получение полного списка описаний изменяемых объектов, включая объекты дочерних коллекций
      /// </summary>
      /// <returns>Полный список описаний изменяемых объектов, включая объекты дочерних коллекций</returns>
      public virtual List<ChangingObject> ExtractChangingObjects()
      {
        List<ChangingObject> list = new List<ChangingObject>();
        this.InternalExtractChangingObjects(list);
        return list;
      }

      /// <summary>
      /// Получение полного списка идентификаторов версий изменяемых объектов, включая объекты дочерних коллекций.
      /// В коллекцию попадают идентификаторы только у выделенных для изменения объектов.
      /// </summary>
      /// <param name="list">Полный список идентификаторов версий изменяемых объектов, включая объекты дочерних коллекций</param>
      protected virtual void InternalExtractIDs(List<long> list)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].ApplyChanges)
          {
            long objectId = this[index].ObjectID;
            if (!list.Contains(objectId))
              list.Add(objectId);
            this[index].Items.InternalExtractIDs(list);
          }
        }
      }

      /// <summary>
      /// Получение полного списка идентификаторов версий изменяемых объектов, включая объекты дочерних коллекций.
      /// В коллекцию попадают идентификаторы только у выделенных для изменения объектов.
      /// </summary>
      /// <returns>Полный список идентификаторов версий изменяемых объектов, включая объекты дочерних коллекций</returns>
      public virtual List<long> ExtractIDs()
      {
        List<long> list = new List<long>();
        this.InternalExtractIDs(list);
        return list;
      }

      /// <summary>
      /// Подсчитать, сколько описаний изменяемых объектов выделено
      /// </summary>
      /// <returns>Количество выделенных описаний изменяемых объектов</returns>
      public virtual int SelectedCount()
      {
        List<ChangingObject> changingObjects = this.ExtractChangingObjects();
        int num = 0;
        for (int index = 0; index < changingObjects.Count; ++index)
        {
          if (changingObjects[index].ApplyChanges)
            ++num;
        }
        return num;
      }

      /// <summary>
      /// Отыскивает первого, не равного null, "родителя". Если такого нет, вернёт parObject
      /// </summary>
      /// <returns>Родительский узел, у которого Parent = null</returns>
      public virtual ChangingObjects FindRootParent()
      {
        if (this.Parent == null)
          return this;
        ChangingObjects changingObjects = this;
        ChangingObjects rootParent = this;
        for (; changingObjects.Parent != null; changingObjects = changingObjects.Parent)
          rootParent = changingObjects;
        return rootParent;
      }

      /// <summary>Создать копию экземпляра класса</summary>
      /// <returns>Копия экземпляра класса</returns>
      public object Clone()
      {
        ChangingObjects changingObjects = new ChangingObjects(this.parent);
        changingObjects.Assign(this);
        return (object) changingObjects;
      }
    }
}
