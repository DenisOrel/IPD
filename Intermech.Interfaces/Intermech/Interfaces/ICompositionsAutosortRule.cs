
// Type: Intermech.Interfaces.ICompositionsAutosortRule
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс, описывающий правило сортировки и отображения составов, позволяющее управлять видимостью типов связей
    /// </summary>
    public interface ICompositionsAutosortRule : IXMLStoredClass
    {
      /// <summary>
      /// Идентификатор версии объекта "Настройки роли", в атрибуте которого
      /// хранится экземпляр указанного правила
      /// </summary>
      long ObjectID { get; set; }

      /// <summary>
      /// Уникальный идентификатор правила сортировки составов
      /// (совпадает с Guid версии объекта "Настройки роли")
      /// </summary>
      Guid Guid { get; set; }

      /// <summary>
      /// Заголовок объекта "Конфигурации роли", в атрибуте
      /// которого хранится экземпляр указанного правила
      /// </summary>
      string Name { get; set; }

      /// <summary>
      /// Использовать события для фильтрации списков типов связей
      /// </summary>
      bool UseEvents { get; set; }

      /// <summary>
      /// Список родительских типов объектов, составы которых будут сортироваться
      /// </summary>
      List<ParentObjectType> ParentObjectTypes { get; }

      /// <summary>
      /// Перегенерировать стартовые значения атрибута "Сортировка" у всей коллекции дочерних типов объектов
      /// </summary>
      void GenerateStartSortingValues();

      /// <summary>
      /// Выполнить синхронизацию внутренних коллекций с кэшем метаданных
      /// </summary>
      /// <param name="session">Сессия, в рамках которой выполняется работа с кэшем метаданных</param>
      void SyncMetadata(IUserSession session);

      /// <summary>
      /// Загрузить информацию из атрибута "" указанного объекта типа "Настройки роли"
      /// </summary>
      /// <param name="session">Сессия, в рамках которой выполняется работа с базой данных</param>
      /// <param name="objectID">Идентификатор версии объекта типа "Настройки роли"</param>
      /// <param name="throwException">Генерировать исключение, если возникнут проблемы при загрузке информации</param>
      void Load(IUserSession session, long objectID, bool throwException);

      /// <summary>
      /// Записать информацию в атрибута "" указанного объекта типа "Настройки роли"
      /// </summary>
      /// <param name="session">Сессия, в рамках которой выполняется работа с базой данных</param>
      /// <param name="objectID">Идентификатор версии объекта типа "Настройки роли"</param>
      /// <param name="throwException">Генерировать исключение, если возникнут проблемы при сохранении информации</param>
      void Save(IUserSession session, long objectID, bool throwException);

      /// <summary>
      /// Получить список видимых связей для указанного типа объекта.
      /// </summary>
      /// <param name="ObjectType">Тип объекта</param>
      /// <param name="returnDefault">Если true, то вернуть тип связи по умолчанию, если нет видимых типов связей</param>
      /// <returns>Cписок видимых связей для указанного типа объекта</returns>
      List<int> GetObjectTypeVisibleRelations(int ObjectType, bool returnDefault);

      /// <summary>
      /// Получить список видимых связей для указанного типа объекта.
      /// </summary>
      /// <param name="ObjectTypeGuid">Тип объекта</param>
      /// <param name="returnDefault">Если true, то вернуть тип связи по умолчанию, если нет видимых типов связей</param>
      /// <returns>Cписок видимых связей для указанного типа объекта</returns>
      List<int> GetObjectTypeVisibleRelations(Guid ObjectTypeGuid, bool returnDefault);

      /// <summary>
      /// Получить список видимых связей для указанного типа объекта.
      /// </summary>
      /// <param name="ObjectType">Тип объекта</param>
      /// <param name="returnDefault">Если true, то вернуть тип связи по умолчанию, если нет видимых типов связей</param>
      /// <returns>Cписок видимых связей для указанного типа объекта</returns>
      List<Guid> GetObjectTypeVisibleRelationsGuids(int ObjectType, bool returnDefault);

      /// <summary>
      /// Получить список видимых связей для указанного типа объекта.
      /// </summary>
      /// <param name="ObjectTypeGuid">Тип объекта</param>
      /// <param name="returnDefault">Если true, то вернуть тип связи по умолчанию, если нет видимых типов связей</param>
      /// <returns>Cписок видимых связей для указанного типа объекта</returns>
      List<Guid> GetObjectTypeVisibleRelationsGuids(Guid ObjectTypeGuid, bool returnDefault);

      /// <summary>
      /// Разрешено ли отображать выборки и классификаторы для указанного родительского типа объекта
      /// </summary>
      /// <param name="ObjectType">Родительский тип объекта</param>
      /// <param name="defaultValue">Значение по умолчанию, если тип не найден в коллекции</param>
      /// <returns>Разрешено ли отображать выборки и классификаторы для указанного родительского типа объекта</returns>
      bool AreSelectionsAndClassifiersEnabled(int ObjectType, bool defaultValue = true);

      /// <summary>
      /// Установить признак разрешения отображения выборок и классификаторов для указанного родительского типа объекта
      /// </summary>
      /// <param name="ObjectType">Родительский тип объекта</param>
      /// <param name="value">Разрешено ли отображать выборки и классификаторы для указанного родительского типа объекта</param>
      void SetSelectionsAndClassifiersEnabled(int ObjectType, bool value);
    }
}
