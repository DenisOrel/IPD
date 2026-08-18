
// Type: Intermech.Interfaces.ChangingObject
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>Описание изменяемого объекта</summary>
    [DebuggerDisplay("ObjectID: {objectID}; NewObjextID: {newObjectID}; Items.Count: {Items.Count}; Caption: {caption}")]
    [Serializable]
    public class ChangingObject : ICloneable
    {
      /// <summary>Родительская коллекция изменяемых объектов</summary>
      private ChangingObjects parent;
      /// <summary>Коллекция дочерних описаний изменяемых объектов</summary>
      private ChangingObjects items;
      /// <summary>
      /// Идентификатор версии изменяемого объекта (уникальный в пределах всей коллекции)
      /// </summary>
      private long objectID;
      /// <summary>
      /// Идентификатор новой версии изменяемого объекта (взятие на изменение, отмена изменений, сохранение изменений, завершение изменений)
      /// </summary>
      private long newObjectID;
      /// <summary>Действие, выполняемое над объектом</summary>
      private ObjectChangingAction changingAction;
      /// <summary>Требуется ли применять действия к указанному объекту</summary>
      private bool applyChanges;
      /// <summary>
      /// Если флажок равен true, пользователь не сможет изменить значение applyChanges
      /// </summary>
      private bool fixApplyChanges;
      /// <summary>Идентификатор типа изменяемого объекта</summary>
      private int objectType = -1;
      /// <summary>Заголовок изменяемого объекта</summary>
      private string caption;
      /// <summary>Идентификатор владельца объекта</summary>
      private long ownerID;
      /// <summary>Кем объект взят на изменение</summary>
      private long chkOutByID;
      /// <summary>Шаг жизненного цикла</summary>
      private int lcStepID = -1;
      /// <summary>Номер версии объекта</summary>
      private long versionID;
      /// <summary>Причина изменения объекта</summary>
      private string changingNote;

      /// <summary>
      /// Создать описание удаляемого объекта (самый упрощённый конструктор)
      /// </summary>
      /// <param name="parent">Родительская коллекция изменяемых объектов</param>
      /// <param name="objectID">Идентификатор версии изменяемых объекта (уникальный в пределах всей коллекции)</param>
      /// <param name="newObjectID">Идентификатор новой версии изменяемого объекта</param>
      /// <param name="changingAction">Действие, выполняемое над объектом</param>
      /// <param name="applyChanged">Требуется ли применять действия к указанному объекту</param>
      /// <param name="fixApplyChanges">Если флажок равен true, пользователь не сможет изменить значение applyChanges</param>
      public ChangingObject(
        ChangingObjects parent,
        long objectID,
        long newObjectID,
        ObjectChangingAction changingAction,
        bool applyChanged,
        bool fixApplyChanges)
        : this(parent, objectID, newObjectID, changingAction, applyChanged, fixApplyChanges, string.Empty)
      {
      }

      /// <summary>
      /// Создать описание изменяемого объекта (упрощённый конструктор)
      /// </summary>
      /// <param name="parent">Родительская коллекция изменяемых объектов</param>
      /// <param name="objectID">Идентификатор версии изменяемых объекта (уникальный в пределах всей коллекции)</param>
      /// <param name="newObjectID">Идентификатор новой версии изменяемого объекта</param>
      /// <param name="changingAction">Действие, выполняемое над объектом</param>
      /// <param name="applyChanged">Требуется ли применять действия к указанному объекту</param>
      /// <param name="fixApplyChanges">Если флажок равен true, пользователь не сможет изменить значение applyChanges</param>
      /// <param name="changingNote">Причина изменения объекта</param>
      public ChangingObject(
        ChangingObjects parent,
        long objectID,
        long newObjectID,
        ObjectChangingAction changingAction,
        bool applyChanged,
        bool fixApplyChanges,
        string changingNote)
        : this(parent, objectID, newObjectID, changingAction, applyChanged, fixApplyChanges, -1, string.Empty, 0L, 0L, -1, 0L, changingNote)
      {
      }

      /// <summary>
      /// Создать описание изменяемого объекта (полная версия конструктора)
      /// </summary>
      /// <param name="parent">Родительская коллекция изменяемых объектов</param>
      /// <param name="objectID">Идентификатор версии изменяемых объекта (уникальный в пределах всей коллекции)</param>
      /// <param name="newObjectID">Идентификатор новой версии изменяемого объекта</param>
      /// <param name="changingAction">Действие, выполняемое над объектом</param>
      /// <param name="applyChanges">Требуется ли применять действия к указанному объекту</param>
      /// <param name="fixApplyChanges">Если флажок равен true, пользователь не сможет изменить значение applyChanges</param>
      /// <param name="objectType">Идентификатор типа изменяемого объекта</param>
      /// <param name="caption">Заголовок изменяемого объекта</param>
      /// <param name="ownerID">Идентификатор владельца объекта</param>
      /// <param name="chkOutByID">Кем объект взят на изменение</param>
      /// <param name="lcStepID">Шаг жизненного цикла</param>
      /// <param name="versionID">Номер версии объекта</param>
      /// <param name="changingNote">Причина изменения объекта</param>
      public ChangingObject(
        ChangingObjects parent,
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
        this.parent = parent;
        this.objectID = objectID;
        this.changingAction = changingAction;
        this.newObjectID = newObjectID;
        this.applyChanges = applyChanges;
        this.fixApplyChanges = fixApplyChanges;
        this.objectType = objectType;
        this.caption = caption;
        this.ownerID = ownerID;
        this.chkOutByID = chkOutByID;
        this.lcStepID = lcStepID;
        this.versionID = versionID;
        this.changingNote = changingNote;
        this.items = new ChangingObjects(parent);
      }

      /// <summary>Родительская коллекция изменяемых объектов</summary>
      public ChangingObjects Parent => this.parent;

      /// <summary>Коллекция дочерних описаний изменяемых объектов</summary>
      public ChangingObjects Items => this.items;

      /// <summary>
      /// Дочернее описание изменяемого объекта с указанным индексом
      /// </summary>
      /// <param name="index">Индекс</param>
      /// <returns>Дочернее описание изменяемого объекта с указанным индексом</returns>
      public ChangingObject this[int index] => this.items[index];

      /// <summary>Количество дочерних описаний изменяемых объектов</summary>
      public int Count => this.items.Count;

      /// <summary>
      /// Идентификатор версии изменяемых объекта (уникальный в пределах всей коллекции)
      /// </summary>
      public long ObjectID
      {
        get => this.objectID;
        set => this.objectID = value;
      }

      /// <summary>
      /// Идентификатор новой версии изменяемого объекта (взятие на изменение, отмена изменений, сохранение изменений, завершение изменений)
      /// </summary>
      public long NewObjectID
      {
        get => this.newObjectID;
        set => this.newObjectID = value;
      }

      /// <summary>Действие, выполняемое над объектом</summary>
      public ObjectChangingAction ChangingAction
      {
        get => this.changingAction;
        set => this.changingAction = value;
      }

      /// <summary>Требуется ли применять действие к указанному объекту</summary>
      public bool ApplyChanges
      {
        get => this.applyChanges;
        set
        {
          if (this.FixApplyChanges)
            return;
          this.applyChanges = value;
        }
      }

      /// <summary>
      /// Если флажок равен true, пользователь не сможет изменить значение applyChanges
      /// </summary>
      public bool FixApplyChanges
      {
        get => this.fixApplyChanges;
        set => this.fixApplyChanges = value;
      }

      /// <summary>Идентификатор типа изменяемого объекта</summary>
      public int ObjectType
      {
        get => this.objectType;
        set => this.objectType = value;
      }

      /// <summary>Заголовок изменяемого объекта</summary>
      public string Caption
      {
        get => this.caption;
        set => this.caption = value;
      }

      /// <summary>Идентификатор владельца объекта</summary>
      public long OwnerID
      {
        get => this.ownerID;
        set => this.ownerID = value;
      }

      /// <summary>Кем объект взят на изменение</summary>
      public long ChkOutByID
      {
        get => this.chkOutByID;
        set => this.chkOutByID = value;
      }

      /// <summary>Шаг жизненного цикла</summary>
      public int LCStepID
      {
        get => this.lcStepID;
        set => this.lcStepID = value;
      }

      /// <summary>Номер версии объекта</summary>
      public long VersionID
      {
        get => this.versionID;
        set => this.versionID = value;
      }

      /// <summary>Причина изменения объекта</summary>
      public string ChangingNote
      {
        get => this.changingNote;
        set => this.changingNote = value;
      }

      /// <summary>
      /// Отыскать в коллекции описание изменяемого объекта с указанным идентификатором.
      /// Поиск также будет проходить в дочерних коллекциях.
      /// </summary>
      /// <param name="objectID">Уникальный в пределах всей коллекции идентификатор версии объекта</param>
      /// <returns>null, если описание изменяемого объекта не найдено</returns>
      public virtual ChangingObject FindChangingObject(long objectID)
      {
        if (objectID == 0L)
          return (ChangingObject) null;
        if (objectID == this.objectID)
          return this;
        for (int index = 0; index < this.items.Count; ++index)
        {
          ChangingObject changingObject = this.items[index].FindChangingObject(objectID);
          if (changingObject != null)
            return changingObject;
        }
        return (ChangingObject) null;
      }

      /// <summary>Сравнить экземпляр класса с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns></returns>
      public override bool Equals(object obj)
      {
        if (!(obj is ChangingObject changingObject))
          return base.Equals(obj);
        return this.objectID == changingObject.objectID && this.changingAction == changingObject.changingAction;
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
        this.Caption = dbObject.Caption;
        this.LCStepID = dbObject.LCStep;
        this.ObjectType = dbObject.ObjectType;
        this.OwnerID = dbObject.OwnerID;
        this.ChkOutByID = dbObject.CheckoutBy;
        this.VersionID = (long) dbObject.VersionID;
      }

      /// <summary>Создать копию экземпляра класса</summary>
      /// <returns>Копия экземпляра класса</returns>
      public object Clone()
      {
        return (object) new ChangingObject(this.Parent, this.ObjectID, this.NewObjectID, this.ChangingAction, this.ApplyChanges, this.FixApplyChanges, this.ObjectType, this.Caption, this.OwnerID, this.ChkOutByID, this.LCStepID, this.VersionID, this.ChangingNote);
      }
    }
}
