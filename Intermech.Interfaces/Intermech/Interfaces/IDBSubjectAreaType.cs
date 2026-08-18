
// Type: Intermech.Interfaces.IDBSubjectAreaType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс предметной области</summary>
    public interface IDBSubjectAreaType
    {
      /// <summary>Символ-идентификатор предметной области.</summary>
      char AreaID { get; }

      /// <summary>Название предметной области</summary>
      string AreaName { get; set; }

      /// <summary>Комментарии</summary>
      string Note { get; set; }

      /// <summary>Удалить предметную область</summary>
      /// <param name="DeleteMode">Зарезервировано.</param>
      /// <returns></returns>
      int Delete(long DeleteMode);

      /// <summary>
      /// Присваивает предметной области новый глобальный идентификатор
      /// </summary>
      void SetGUID(Guid guid);
    }
}
