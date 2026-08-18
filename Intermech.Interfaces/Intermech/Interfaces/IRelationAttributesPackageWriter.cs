
// Type: Intermech.Interfaces.IRelationAttributesPackageWriter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Серверная служба для пакетной записи атрибутов в связи
    /// </summary>
    public interface IRelationAttributesPackageWriter
    {
      /// <summary>
      /// Выполнить запись пакета атрибутов связей в состав указанного родительского объекта
      /// </summary>
      /// <param name="sessionID  ">Уникальный идентификатор сессии, в рамках которой будет выполняться работа с базой данных</param>
      /// <param name="package">Пакет атрибутов связей</param>
      /// <param name="chRels">Список изменённых связей</param>
      /// <returns>true, если информация была успешно сохранена в базе данных</returns>
      bool WriteRelationAttributesPackage(
        Guid sessionID,
        RelationAttributesPackage package,
        out List<long> chRels);

      /// <summary>
      /// Выполнить запись пакетов атрибутов связей в состав указанного родительского объекта
      /// </summary>
      /// <param name="sessionID  ">Уникальный идентификатор сессии, в рамках которой будет выполняться работа с базой данных</param>
      /// <param name="packages">Коллекция идентификаторов версий родительских объектов и пакетов их атрибутов</param>
      /// <param name="chRels">Список изменённых связей</param>
      /// <returns>true, если информация была успешно сохранена в базе данных</returns>
      bool WriteRelationAttributesPackages(
        Guid sessionID,
        Dictionary<long, RelationAttributesPackage> packages,
        out List<long> chRels);
    }
}
