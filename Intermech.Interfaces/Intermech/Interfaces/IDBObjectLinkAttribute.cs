
// Type: Intermech.Interfaces.IDBObjectLinkAttribute
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс атрибута типа Ссылка на объект</summary>
    public interface IDBObjectLinkAttribute
    {
      /// <summary>
      /// Объект, на который ссылается текущее значение атрибута. Если этот объект взят данным юзером на изменение, то возвращается
      /// рабочая копия объекта.
      /// </summary>
      IDBObject DBObject { get; set; }

      /// <summary>
      /// GUID версии объекта, на который ссылается текущее значение атрибута
      /// </summary>
      Guid DBObjectGUID { get; set; }
    }
}
