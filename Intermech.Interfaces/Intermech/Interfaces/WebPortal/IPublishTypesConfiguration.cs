
// Type: Intermech.Interfaces.WebPortal.IPublishTypesConfiguration
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>
    /// Интерфейс для работы с конфигурацией публикуемых типов объектов
    /// </summary>
    public interface IPublishTypesConfiguration
    {
      /// <summary>Получить соответствия типов</summary>
      /// <returns></returns>
      Dictionary<string, Guid> ComplianceObjectTypes { get; }

      /// <summary>Перечитать кэш</summary>
      void Reload();

      /// <summary>Сохранить кэш в базу</summary>
      void Save();

      /// <summary>Тип объектов публикуемый?</summary>
      /// <param name="objType">Идентификатор типа объектов</param>
      /// <returns></returns>
      bool IsPublishObjectType(int objType);

      /// <summary>Публиковать весь объект при ссылке на него</summary>
      /// <param name="objType">Идентификатор типа объектов</param>
      /// <returns></returns>
      bool ObjectWithLink(int objType);

      /// <summary>
      /// Установить типу объектов признак публикации всего объекта при ссылке на него
      /// </summary>
      /// <param name="objType">Идентификатор типа объектов</param>
      /// <param name="value">Новое значение</param>
      void SetObjectWithLink(int objType, bool value);

      /// <summary>Список типов объектов, публикация которых разрешена</summary>
      List<int> PublishObjectTypes { get; }

      /// <summary>Список типов связей, публикация которых не запрещена</summary>
      List<int> PublishRelationTypes { get; }

      /// <summary>
      /// Список типов связей, публикация которых должна быть всегда
      /// </summary>
      List<int> AlwaysRelationTypes { get; }

      /// <summary>Удалить тип объектов из числа публикуемых</summary>
      /// <param name="objType">Идентификатор типа объектов</param>
      /// <param name="saveInBase">Сохранить изменения в базе</param>
      /// <returns></returns>
      void RemovePublishObjectType(int objType, bool saveInBase);

      /// <summary>Добавить тип объектов для публикации</summary>
      /// <param name="objType">Идентификатор типа объектов</param>
      /// <param name="saveInBase">Сохранить изменения в базе</param>
      /// <returns></returns>
      void AddPublishObjectType(int objType, bool saveInBase);

      /// <summary>Установить тип передачи связей через портал</summary>
      /// <param name="relationType">Глобальный идентификатор типа связей</param>
      /// <param name="migrateType">Тип передачи</param>
      /// <param name="saveInBase">Сохранить изменения в базе</param>
      void SetRelationMigrateType(Guid relationType, RelationMigrateType migrateType, bool saveInBase);

      /// <summary>Получить тип передачи связей через портал</summary>
      /// <param name="relationType">Глобальный идентификатор типа связей</param>
      /// <returns></returns>
      RelationMigrateType GetRelationMigrateType(Guid relationType);

      /// <summary>Получить настройки формирования состава</summary>
      /// <returns></returns>
      CompositionApplicabilities GetCompositionApplicabilities();

      /// <summary>
      /// Флаг, показывающий, что в настройках есть типы объектов, которые нужно передавать целиком, если на них ссылается
      /// публикуемый объект или связь
      /// </summary>
      bool ObjectWithLinksPresent { get; }
    }
}
