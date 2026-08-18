
// Type: Intermech.Interfaces.MyObjectElement
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс, хранящий некоторую информацию о версии объекта.
    /// Поле Value - (Int64) - идентификатор версии объекта
    /// </summary>
    public class MyObjectElement : 
      MyElement,
      IAssignable,
      ICloneable,
      IDatabaseSync,
      IComparable,
      IComparable<MyObjectElement>
    {
      /// <summary>Идентификатор типа объекта</summary>
      public int ObjectType = -1;

      /// <summary>Создать незаполненный экземпляр класса</summary>
      public MyObjectElement()
      {
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
      /// </summary>
      public MyObjectElement(object source) => this.Assign(source);

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="objectID">Идентификатор версии объекта (будет записан в поле Value)</param>
      /// <param name="caption">Заголовок объекта</param>
      /// <param name="tag">Дополнительное значение</param>
      /// <param name="objectType">Тип объекта</param>
      public MyObjectElement(long objectID, string caption, object tag, int objectType)
        : base((object) objectID, caption, tag)
      {
        this.ObjectType = objectType;
      }

      /// <summary>Идентификатор версии объекта</summary>
      public virtual long ObjectID
      {
        [DebuggerStepThrough] get => this.Value is long ? (long) this.Value : 0L;
        set => this.Value = (object) value;
      }

      /// <summary>Очистить поля класса</summary>
      public override void Clear()
      {
        base.Clear();
        this.ObjectType = -1;
      }

      /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
      /// <param name="source">Объект-источник</param>
      public override void Assign(object source)
      {
        if (this == source)
          return;
        base.Assign(source);
        if (!(source is MyObjectElement myObjectElement))
          return;
        this.ObjectType = myObjectElement.ObjectType;
      }

      /// <summary>
      /// Выполнить синхронизацию внутренних коллекций с базой данных
      /// </summary>
      /// <param name="session">Ссылка на сессию, в рамках которой выполняется работа с базой данных и сервером приложений</param>
      public virtual void SyncObjectsData(IUserSession session)
      {
        if (session == null || !(this.Value is long))
          return;
        long objectID = (long) this.Value;
        IDBObject dbObject = (IDBObject) null;
        if (objectID != 0L)
          dbObject = session.GetObject(objectID, false);
        if (dbObject != null)
        {
          this.Caption = dbObject.Caption;
          this.ObjectType = dbObject.ObjectType;
        }
        else
          this.Clear();
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public virtual int CompareTo(object obj) => this.CompareTo(obj as MyObjectElement);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(MyObjectElement other)
      {
        if (other == null)
          return 1;
        int num = MetaDataHelper.GetObjectTypeName(this.ObjectType).CompareTo(MetaDataHelper.GetObjectTypeName(other.ObjectType));
        return num != 0 ? num : this.Caption.ToUpperInvariant().CompareTo(other.Caption.ToUpperInvariant());
      }

      /// <summary>Создать точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public override object Clone()
      {
        MyObjectElement myObjectElement = new MyObjectElement();
        myObjectElement.Assign((object) this);
        return (object) myObjectElement;
      }

      /// <summary>Сравнить экземпляр класса с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        return obj is MyObjectElement myObjectElement && this.ObjectID == myObjectElement.ObjectID;
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode() => this.ObjectID.GetHashCode();
    }
}
