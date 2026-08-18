
// Type: Intermech.Interfaces.IImportedObjectInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс на импротированный объект</summary>
    public interface IImportedObjectInfo
    {
      /// <summary>ID версии объекта</summary>
      long ObjectID { get; }

      /// <summary>ID объекта</summary>
      long ID { get; }

      /// <summary>Ошибка при импорте объекта</summary>
      Exception ImportMessage { get; }
    }
}
