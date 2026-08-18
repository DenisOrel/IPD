
// Type: Intermech.Interfaces.LifeCycles.IDBLCSchema
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.LifeCycles
{
    /// <summary>Схема жизненного цикла объектов</summary>
    public interface IDBLCSchema
    {
      /// <summary>Локальный идентификатор схемы ЖЦ</summary>
      int SchemaID { get; }

      /// <summary>Уникальное наименование схемы ЖЦ</summary>
      string Name { get; set; }

      /// <summary>Комментарии к схеме ЖЦ</summary>
      string Note { get; set; }

      /// <summary>Глобальный идентификатор схемы ЖЦ</summary>
      Guid GUID { get; set; }

      /// <summary>
      /// Является ли данная схема ЖЦ схемой по умолчанию.
      /// Схема по умолчанию присваивается вновь создаваемым типам объектов (которые создаются на верхнем уровне иерархии типов).
      /// </summary>
      bool IsDefaultSchema { get; set; }

      /// <summary>Информация о графической отрисовке схемы</summary>
      byte[] DrawData { get; set; }

      /// <summary>Удаляет схему ЖЦ</summary>
      int Delete(long deleteMode);

      /// <summary>Структура со свойствами схемы ЖЦ</summary>
      DBLCSchemaProperties SchemaProperties { get; set; }

      /// <summary>Опции схемы ЖЦ</summary>
      LCSchemaOptions Options { get; set; }

      /// <summary>
      /// Возвращает коллекцию шагов жизненного цикла данной схемы
      /// </summary>
      /// <returns></returns>
      IDBLifecycleStepCollection GetStepsCollection();
    }
}
