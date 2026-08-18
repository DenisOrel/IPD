
// Type: Intermech.Interfaces.SpecHandleAttributeEventArgs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Аргументы для события специальной обработки значений атрибута при операциях импорта
    /// </summary>
    public class SpecHandleAttributeEventArgs
    {
      /// <summary>Пользовательская сессия</summary>
      public IUserSession Session;
      /// <summary>Идентификатор версии объекта/связи</summary>
      public IDBAttributable Attributable;
      /// <summary>Идентификатор версии объекта/связи</summary>
      public long AttributableID;
      /// <summary>Тип объекта/связи</summary>
      public int TypeID;
      /// <summary>Идентификатор атрибута</summary>
      public int AttributeID;
      /// <summary>Глобальный идентификатор атрибута</summary>
      public Guid AttributeGuid;
      /// <summary>Флаг того, что подписчик обработал значение</summary>
      public bool Handled;
      /// <summary>Значение атрибута</summary>
      public AttributeRecord Value;
      /// <summary>
      /// Если атрибут не создается, то не обновлять значение атрибута
      /// </summary>
      public bool NotUpdate;
      /// <summary>
      /// Возможно атрибут будет заливатся в другой атрибут. Если обработчик определит такой случай сюда нужно записать
      /// новый идентификатор атрибута
      /// </summary>
      public int NewAttributeID;
      /// <summary>Признак того, что объект/связь новые</summary>
      public bool IsNewObject;
      /// <summary>Флаг того, что узел является владельцем объекта/связи</summary>
      public bool IsOwner;
      /// <summary>Дополнительные данные</summary>
      public object Tag;

      /// <summary>конструктор</summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="attributable">Идентификатор версии объекта/связи</param>
      /// <param name="typeID">Тип объекта/связи</param>
      /// <param name="attrID">Идентификатор атрибута</param>
      /// <param name="attrGuid">Глобальный идентификатор атрибута</param>
      /// <param name="val">Значение атрибута</param>
      public SpecHandleAttributeEventArgs(
        IUserSession session,
        long attributableID,
        IDBAttributable attributable,
        int typeID,
        int attrID,
        Guid attrGuid,
        AttributeRecord val,
        bool isNewObject,
        bool isOwner)
      {
        this.AttributableID = attributableID;
        this.Attributable = attributable;
        this.Session = session;
        this.TypeID = typeID;
        this.AttributeID = attrID;
        this.AttributeGuid = attrGuid;
        this.Handled = false;
        this.NotUpdate = false;
        this.Value = val;
        this.NewAttributeID = 0;
        this.IsNewObject = isNewObject;
        this.IsOwner = isOwner;
      }
    }
}
