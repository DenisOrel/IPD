
// Type: Intermech.Interfaces.DeletingObject
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>Описание удаляемого объекта</summary>
    [DebuggerDisplay("[{Level}:{Weight}/{WeightGlobal}] ObjectID: {objectID}; RemoveObject: {removeObject}; Items.Count: {Items.Count}; Caption: {caption}")]
    [Serializable]
    public class DeletingObject : ICloneable, IComparable, IComparable<DeletingObject>
    {
      /// <summary>"Весовой" коэффициент удаляемой версии объекта</summary>
      private long weight;
      /// <summary>Родительская коллекция удаляемых объектов</summary>
      private DeletingObjects parent;
      /// <summary>Коллекция дочерних описаний удаляемых объектов</summary>
      private DeletingObjects items;
      /// <summary>Идентификатор удаляемого объекта</summary>
      private long id;
      /// <summary>
      /// Идентификатор версии удаляемого объекта (уникальный в пределах всей коллекции)
      /// </summary>
      private long objectID;
      /// <summary>Требуется ли удалять указанный объект</summary>
      private bool removeObject;
      /// <summary>Идентификатор типа удаляемого объекта</summary>
      private int objectType = -1;
      /// <summary>Заголовок удаляемого объекта</summary>
      private string caption;
      /// <summary>Идентификатор владельца объекта</summary>
      private long ownerID;
      /// <summary>Кем объект взят на изменение</summary>
      private long chkOutByID;
      /// <summary>Шаг жизненного цикла</summary>
      private int lcStepID = -1;
      /// <summary>
      /// Идентификаторы связей (если объект удаляется из составов)
      /// </summary>
      private List<long> prjLinkIDs = new List<long>();
      /// <summary>Номер версии</summary>
      private long versionNo;
      /// <summary>Является ли версия базовой</summary>
      private bool baseVersion;
      /// <summary>Причина удаления объекта</summary>
      private string removeNote;

      /// <summary>
      /// Создать описание удаляемого объекта (самый упрощённый конструктор)
      /// </summary>
      /// <param name="parent">Родительская коллекция удаляемых объектов</param>
      /// <param name="weight">"Весовой" коэффициент (по умолчанию - 0)</param>
      /// <param name="id">Идентификатор удаляемого объекта</param>
      /// <param name="objectID">Идентификатор версии удаляемого объекта (уникальный в пределах всей коллекции)</param>
      /// <param name="removeObject">Требуется ли удалять указанный объект</param>
      public DeletingObject(
        DeletingObjects parent,
        long weight,
        long id,
        long objectID,
        bool removeObject)
        : this(parent, weight, id, objectID, removeObject, string.Empty)
      {
      }

      /// <summary>
      /// Создать описание удаляемого объекта (упрощённый конструктор)
      /// </summary>
      /// <param name="parent">Родительская коллекция удаляемых объектов</param>
      /// <param name="weight">"Весовой" коэффициент (по умолчанию - 0)</param>
      /// <param name="id">Идентификатор удаляемого объекта</param>
      /// <param name="objectID">Идентификатор версии удаляемого объекта (уникальный в пределах всей коллекции)</param>
      /// <param name="removeObject">Требуется ли удалять указанный объект</param>
      /// <param name="removeNote">Причина удаления объекта</param>
      public DeletingObject(
        DeletingObjects parent,
        long weight,
        long id,
        long objectID,
        bool removeObject,
        string removeNote)
        : this(parent, weight, id, objectID, removeObject, -1, string.Empty, 0L, 0L, -1, 0L, 0L, false, removeNote)
      {
      }

      /// <summary>
      /// Создать описание удаляемого объекта (полная версия конструктора)
      /// </summary>
      /// <param name="parent">Родительская коллекция удаляемых объектов</param>
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
      public DeletingObject(
        DeletingObjects parent,
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
        : this(parent, weight, id, objectID, removeObject, objectType, caption, ownerID, chkOutByID, lcStepID, 0L, 0L, false, removeNote)
      {
      }

      /// <summary>
      /// Создать описание удаляемого объекта (полная версия конструктора)
      /// </summary>
      /// <param name="parent">Родительская коллекция удаляемых объектов</param>
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
      /// <param name="baseVersion">Является ли версия базовой</param>
      /// <param name="removeNote">Причина удаления объекта</param>
      public DeletingObject(
        DeletingObjects parent,
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
        this.parent = parent;
        this.weight = weight;
        this.id = id;
        this.objectID = objectID;
        this.removeObject = removeObject;
        this.objectType = objectType;
        this.caption = caption;
        this.ownerID = ownerID;
        this.chkOutByID = chkOutByID;
        this.lcStepID = lcStepID;
        if (prjLinkID != 0L && !this.prjLinkIDs.Contains(prjLinkID))
          this.prjLinkIDs.Add(prjLinkID);
        this.versionNo = versionNo;
        this.baseVersion = baseVersion;
        this.removeNote = removeNote;
        this.items = new DeletingObjects(parent);
      }

      /// <summary>"Весовой" коэффициент удаляемой версии объекта</summary>
      public long Weight
      {
        [DebuggerStepThrough] get => this.weight;
        set => this.weight = value;
      }

      /// <summary>
      /// Глобальный "Весовой" коэффициент удаляемой версии объекта в графе
      /// </summary>
      public long WeightGlobal => this.Weight - (long) this.Level;

      /// <summary>Родительская коллекция удаляемых объектов</summary>
      public DeletingObjects Parent
      {
        [DebuggerStepThrough] get => this.parent;
      }

      /// <summary>Уровень вложенности элемента</summary>
      public int Level
      {
        get
        {
          int level = 0;
          for (DeletingObjects parent = this.Parent; parent != null; parent = parent.Parent)
            ++level;
          return level;
        }
      }

      /// <summary>Коллекция дочерних описаний удаляемых объектов</summary>
      public DeletingObjects Items
      {
        [DebuggerStepThrough] get => this.items;
      }

      /// <summary>
      /// Дочернее описание удаляемого объекта с указанным индексом
      /// </summary>
      /// <param name="index">Индекс</param>
      /// <returns>Дочернее описание удаляемого объекта с указанным индексом</returns>
      public DeletingObject this[int index]
      {
        [DebuggerStepThrough] get => this.items[index];
      }

      /// <summary>Количество дочерних описаний удаляемых объектов</summary>
      public int Count
      {
        [DebuggerStepThrough] get => this.items.Count;
      }

      /// <summary>Идентификатор удаляемого объекта</summary>
      public long ID
      {
        [DebuggerStepThrough] get => this.id;
        set => this.id = value;
      }

      /// <summary>
      /// Идентификатор версии удаляемого объекта (уникальный в пределах всей коллекции)
      /// </summary>
      public long ObjectID
      {
        [DebuggerStepThrough] get => this.objectID;
        set => this.objectID = value;
      }

      /// <summary>Требуется ли удалять указанный объект</summary>
      public bool RemoveObject
      {
        [DebuggerStepThrough] get => this.removeObject;
        set => this.removeObject = value;
      }

      /// <summary>Идентификатор типа удаляемого объекта</summary>
      public int ObjectType
      {
        [DebuggerStepThrough] get => this.objectType;
        set => this.objectType = value;
      }

      /// <summary>Заголовок удаляемого объекта</summary>
      public string Caption
      {
        [DebuggerStepThrough] get => this.caption;
        set => this.caption = value;
      }

      /// <summary>Идентификатор владельца объекта</summary>
      public long OwnerID
      {
        [DebuggerStepThrough] get => this.ownerID;
        set => this.ownerID = value;
      }

      /// <summary>Кем объект взят на изменение</summary>
      public long ChkOutByID
      {
        [DebuggerStepThrough] get => this.chkOutByID;
        set => this.chkOutByID = value;
      }

      /// <summary>Шаг жизненного цикла</summary>
      public int LCStepID
      {
        [DebuggerStepThrough] get => this.lcStepID;
        set => this.lcStepID = value;
      }

      /// <summary>
      /// Идентификаторы связей (если объект удаляется из состава)
      /// </summary>
      public List<long> PrjLinkIDs
      {
        [DebuggerStepThrough] get => this.prjLinkIDs;
      }

      /// <summary>Номер версии объекта</summary>
      public long VersionNo
      {
        [DebuggerStepThrough] get => this.versionNo;
        set => this.versionNo = value;
      }

      /// <summary>Является ли версия базовой</summary>
      public bool BaseVersion
      {
        [DebuggerStepThrough] get => this.baseVersion;
        set => this.baseVersion = value;
      }

      /// <summary>Причина удаления объекта</summary>
      public string RemoveNote
      {
        [DebuggerStepThrough] get => this.removeNote;
        set => this.removeNote = value;
      }

      /// <summary>
      /// Отыскать в коллекции описание удаляемого объекта с указанным идентификатором.
      /// Поиск также будет проходить в дочерних коллекциях.
      /// </summary>
      /// <param name="objectID">Уникальный в пределах всей коллекции идентификатор версии объекта</param>
      /// <returns>null, если описание удаляемого объекта не найдено</returns>
      public virtual DeletingObject FindDeletingObject(long objectID)
      {
        if (objectID == 0L)
          return (DeletingObject) null;
        if (objectID == this.objectID)
          return this;
        for (int index = 0; index < this.items.Count; ++index)
        {
          DeletingObject deletingObject = this.items[index].FindDeletingObject(objectID);
          if (deletingObject != null)
            return deletingObject;
        }
        return (DeletingObject) null;
      }

      /// <summary>Сравнить экземпляр класса с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true - объекты полностью идентичны</returns>
      public override bool Equals(object obj)
      {
        return !(obj is DeletingObject deletingObject) ? base.Equals(obj) : this.objectID == deletingObject.objectID;
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode() => this.objectID.GetHashCode();

      /// <summary>
      /// Загрузить описание объекта из базы данных (если оно не загружено)
      /// </summary>
      /// <param name="session">Сессия</param>
      public virtual void LoadDescription(IUserSession session)
      {
        if (session == null || this.LCStepID != -1 && this.ObjectType != -1 && this.OwnerID != 0L)
          return;
        IDBObject dbObject = session.GetObject(this.ObjectID);
        this.ID = dbObject.ID;
        this.Caption = dbObject.Caption;
        this.LCStepID = dbObject.LCStep;
        this.ObjectType = dbObject.ObjectType;
        this.OwnerID = dbObject.OwnerID;
        this.ChkOutByID = dbObject.CheckoutBy;
        this.VersionNo = (long) dbObject.VersionID;
        this.BaseVersion = dbObject.IsBaseVersion;
      }

      /// <summary>Создать копию экземпляра класса</summary>
      /// <returns>Копия экземпляра класса</returns>
      public object Clone()
      {
        DeletingObject deletingObject = new DeletingObject(this.Parent, this.Weight, this.ID, this.ObjectID, this.RemoveObject, this.ObjectType, this.Caption, this.OwnerID, this.ChkOutByID, this.LCStepID, 0L, this.VersionNo, this.BaseVersion, this.RemoveNote);
        if (this.prjLinkIDs.Count > 0)
        {
          for (int index = 0; index < this.prjLinkIDs.Count; ++index)
            deletingObject.PrjLinkIDs.Add(this.prjLinkIDs[index]);
        }
        return (object) deletingObject;
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as DeletingObject);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(DeletingObject other)
      {
        return other == null ? -1 : this.WeightGlobal.CompareTo(other.WeightGlobal);
      }
    }
}
