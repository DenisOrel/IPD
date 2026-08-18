
// Type: Intermech.Interfaces.Data.ObjectLocatorResult
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Data
{
    /// <summary>Описывает результат поиска объекта в базе IPS.</summary>
    public sealed class ObjectLocatorResult
    {
      private readonly long objectId;
      private readonly int objectType;

      /// <summary>Создает объект.</summary>
      /// <param name="objectId">Идентификатор версии объекта</param>
      /// <param name="objectType">Идентификатор типа объекта</param>
      /// <exception cref="T:System.ArgumentException">Один из идентификаторов не определен</exception>
      public ObjectLocatorResult(long objectId, int objectType)
      {
        if (objectId == 0L)
          throw new ArgumentException();
        if (objectType == -1)
          throw new ArgumentException();
        this.objectId = objectId;
        this.objectType = objectType;
      }

      /// <summary>Возвращает идентификатор версии найденного объекта.</summary>
      public long ObjectId => this.objectId;

      /// <summary>Возвращает идентификатор типа найденного объекта.</summary>
      public int ObjectType => this.objectType;
    }
}
