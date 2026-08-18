
// Type: Intermech.Interfaces.IDBGuid
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс, позволяющий получать глобальный уникальный идентификатор объекта, связи, атрибута, т.п.
    /// </summary>
    public interface IDBGuid
    {
      /// <summary>Guid (только для чтения)</summary>
      Guid GUID { get; }

      /// <summary>
      /// Если true, то это наш системный GUID, =&gt; удалять такой объект нельзя.
      /// </summary>
      bool IsSystemGUID { get; }
    }
}
