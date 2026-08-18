
// Type: Intermech.Interfaces.LifeCycles.IDBLCSchemaInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.LifeCycles
{
    /// <summary>Объект с инфой о схеме жизненного цикла объектов</summary>
    public interface IDBLCSchemaInfo
    {
      /// <summary>Локальный идентификатор схемы ЖЦ</summary>
      int SchemaID { get; }

      /// <summary>Уникальное наименование схемы ЖЦ</summary>
      string Name { get; }

      /// <summary>Комментарии к схеме ЖЦ</summary>
      string Note { get; }

      /// <summary>Глобальный идентификатор схемы ЖЦ</summary>
      Guid GUID { get; }

      /// <summary>
      /// Является ли данная схема ЖЦ схемой по умолчанию.
      /// Схема по умолчанию присваивается вновь создаваемым типам объектов (которые создаются на верхнем уровне иерархии типов).
      /// </summary>
      bool IsDefaultSchema { get; }

      /// <summary>Информация о графической отрисовке схемы</summary>
      byte[] DrawData { get; }

      /// <summary>Структура со свойствами схемы ЖЦ</summary>
      DBLCSchemaProperties SchemaProperties { get; }

      /// <summary>Опции схемы ЖЦ</summary>
      LCSchemaOptions Options { get; }
    }
}
