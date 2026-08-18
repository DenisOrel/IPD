
// Type: Intermech.Interfaces.MetaDataHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Статический вспомогательный класс. Выполняет роль сверхбыстрого кэша метаданных - работает
    /// "поверх" стандартного кэша метаданных, извлекая из некоторых его таблиц наиболее часто используемые
    /// данные, и размещая их в словарики для быстрого поиска (кэш первого уровня)
    /// </summary>
    public static class MetaDataHelper
    {
      /// <summary>
      /// Объект для потокобезопасного доступа к статическим словарикам класса (если есть необходимость изменить их значение)
      /// </summary>
      public static object SyncRoot
      {
        get => MetaDataHelperService.Instance.SyncRoot;
        set => MetaDataHelperService.Instance.SyncRoot = value;
      }

      /// <summary>
      /// Если с момента последнего обращения к методу SyncMetadata прошло меньше указанного
      /// периода, то обращений к серверу приложений не будет вообще - синхронизация прерывается сразу
      /// </summary>
      public static TimeSpan SyncDelta
      {
        get => MetaDataHelperService.Instance.SyncDelta;
        set => MetaDataHelperService.Instance.SyncDelta = value;
      }

      /// <summary>
      /// Имя модуля, для которого хранится значение счётчика MetaDataGenerationName
      /// </summary>
      public static string MetaDataGenerationModule
      {
        get => MetaDataHelperService.Instance.MetaDataGenerationModule;
      }

      /// <summary>
      /// Имя модуля, для которого хранится значение счётчика MetaDataGenerationName
      /// </summary>
      public static string MetaDataGenerationSection
      {
        get => MetaDataHelperService.Instance.MetaDataGenerationSection;
      }

      /// <summary>
      /// Счётчик, указывающий на поколение метаданных, записанных в СУБД.
      /// Данное значение хранится в системной конфигурации IPS
      /// </summary>
      public static string MetaDataGenerationKey
      {
        get => MetaDataHelperService.Instance.MetaDataGenerationKey;
      }

      /// <summary>Дата и время последней модификации кэша метаданных</summary>
      public static DateTime SyncDateTime => MetaDataHelperService.Instance.SyncDateTime;

      /// <summary>Принудительное обновление содержимого кэша</summary>
      public static bool Forced
      {
        [DebuggerStepThrough] get => MetaDataHelperService.Instance.Forced;
        set => MetaDataHelperService.Instance.Forced = value;
      }

      /// <summary>
      /// Заблокировано ли обновление кэша (наивысший приоритет)
      /// </summary>
      public static bool Locked
      {
        [DebuggerStepThrough] get => MetaDataHelperService.Instance.Locked;
        set => MetaDataHelperService.Instance.Locked = value;
      }

      /// <summary>
      /// В данном словарике хранятся краткие описания типов атрибутов
      /// [ID типа атрибута] =&gt; [IMSAttributeType - описание типа атрибута]
      /// </summary>
      internal static Dictionary<int, IMSAttributeType> AttrTypes
      {
        get => MetaDataHelperService.Instance.AttrTypes;
      }

      /// <summary>
      /// Полный список типов объектов, которые можно считать контекстами редактирования
      /// </summary>
      public static List<int> SpecialContextObjectTypes
      {
        get => MetaDataHelperService.Instance.SpecialContextObjectTypes;
        set => MetaDataHelperService.Instance.SpecialContextObjectTypes = value;
      }

      /// <summary>
      /// Дата и время последней синхронизации словариков, связанных с типами объектов,
      /// типами связей по умолчанию для типов объектов
      /// </summary>
      public static DateTime LastObjectsSyncTime => MetaDataHelperService.Instance.LastObjectsSyncTime;

      /// <summary>
      /// Выполнить полную синхронизацию всех внутренних коллекций с кэшем метаданных
      /// </summary>
      /// <param name="cacheTables">Набор таблиц с кэшем метаданных</param>
      public static void SyncMetadata(DataSet cacheTables)
      {
        MetaDataHelperService.Instance.SyncMetadata(cacheTables, false);
      }

      /// <summary>
      /// Выполнить полную синхронизацию всех внутренних коллекций с кэшем метаданных
      /// </summary>
      /// <param name="cacheTables">Набор таблиц с кэшем метаданных</param>
      /// <param name="forced">true - принудительно синхронизировать</param>
      public static void SyncMetadata(DataSet cacheTables, bool forced)
      {
        MetaDataHelperService.Instance.SyncMetadata(cacheTables, forced);
      }

      /// <summary>
      /// Указать, что содержимое кэша в целом изменилось, сбросить содержимое флажка Forced
      /// </summary>
      public static void Touch() => MetaDataHelperService.Instance.Touch();

      /// <summary>
      /// Выполнить синхронизацию коллекций, связанных с типами объектов, с кэшем метаданных
      /// </summary>
      /// <param name="cacheTables">Набор таблиц с кэшем метаданных</param>
      public static void SyncObjectTypesMetadata(DataSet cacheTables)
      {
        MetaDataHelperService.Instance.SyncObjectTypesMetadata(cacheTables);
      }

      /// <summary>
      /// Выполнить синхронизацию коллекций, связанных с иерархией типов объектов.
      /// Внимание! Перед вызовом этого метода должен быть вызван метод SyncObjectTypesMetadata,
      /// который корректно заполняет коллекции [Guid типа объекта] = [ID типа объекта]
      /// </summary>
      /// <param name="cacheTables">Набор таблиц с кэшем метаданных</param>
      public static void SyncObjectTypesHierarchy(DataSet cacheTables)
      {
        MetaDataHelperService.Instance.SyncObjectTypesHierarchy(cacheTables);
      }

      /// <summary>
      /// Выполнить синхронизацию коллекций, связанных с типами связей, с кэшем метаданных.
      /// </summary>
      /// <param name="cacheTables">Набор таблиц с кэшем метаданных</param>
      public static void SyncRelationTypesMetadata(DataSet cacheTables)
      {
        MetaDataHelperService.Instance.SyncRelationTypesMetadata(cacheTables);
      }

      /// <summary>
      /// Выполнить синхронизацию коллекций, связанных со специальными типами объектов.
      /// Внимание! Перед вызовом этого метода должнs быть вызваны методы SyncObjectTypesMetadata,
      /// SyncSpecialRelationTypes
      /// </summary>
      /// <param name="cacheTables">Набор таблиц с кэшем метаданных</param>
      public static void SyncSpecialObjectTypes(DataSet cacheTables)
      {
        MetaDataHelperService.Instance.SyncSpecialObjectTypes(cacheTables);
      }

      /// <summary>
      /// Выполнить синхронизацию коллекций, связанных со специальными типами связей.
      /// Внимание! Перед вызовом этого метода должен быть вызван метод SyncRelationTypesMetadata,
      /// который корректно заполняет коллекции [Guid типа связи] = [ID типа связи]
      /// </summary>
      /// <param name="cacheTables">Набор таблиц с кэшем метаданных</param>
      public static void SyncSpecialRelationTypes(DataSet cacheTables)
      {
        MetaDataHelperService.Instance.SyncSpecialRelationTypes(cacheTables);
      }

      /// <summary>
      /// Выполнить синхронизацию коллекций, связанных с типами атрибутов, с кэшем метаданных
      /// </summary>
      /// <param name="cacheTables">Набор таблиц с кэшем метаданных</param>
      public static void SyncAttrTypesMetadata(DataSet cacheTables)
      {
        MetaDataHelperService.Instance.SyncAttrTypesMetadata(cacheTables);
      }

      /// <summary>
      /// Выполнить синхронизацию коллекций, связанных со схемами ЖЦ, уровнями продвижения, шагами ЖЦ
      /// </summary>
      /// <param name="cacheTables">Набор таблиц с кэшем метаданных</param>
      public static void SyncLCStepsMetadata(DataSet cacheTables)
      {
        MetaDataHelperService.Instance.SyncLCStepsMetadata(cacheTables);
      }

      /// <summary>Выполнить синхронизацию глобальной коллекции</summary>
      /// <param name="cacheTables">Набор таблиц с кэшем метаданных</param>
      public static void SyncGlobals(DataSet cacheTables)
      {
        MetaDataHelperService.Instance.SyncGlobals(cacheTables);
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном типе объекта
      /// </summary>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <returns>true, если тип объекта существует</returns>
      public static bool ExistsObjectType(int objTypeID)
      {
        return MetaDataHelperService.Instance.ExistsObjectType(objTypeID);
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном типе объекта
      /// </summary>
      /// <param name="objTypeGuid">Guid типа объекта</param>
      /// <returns>true, если тип объекта существует</returns>
      public static bool ExistsObjectType(Guid objTypeGuid)
      {
        return MetaDataHelperService.Instance.ExistsObjectType(objTypeGuid);
      }

      /// <summary>Получить краткую информацию о типе объекта</summary>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <returns>Краткая информация о типе объекта или null</returns>
      public static IMSObjectType GetObjectType(int objTypeID)
      {
        return MetaDataHelperService.Instance.GetObjectType(objTypeID);
      }

      /// <summary>Получить краткую информацию о типе объекта</summary>
      /// <param name="objTypeGuid">Идентификатор типа объекта</param>
      /// <returns>Краткая информация о типе объекта или null</returns>
      public static IMSObjectType GetObjectType(Guid objTypeGuid)
      {
        return MetaDataHelperService.Instance.GetObjectType(objTypeGuid);
      }

      /// <summary>Получить название типа объектов (например, "Детали")</summary>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <returns>Название типа объектов (например, "Детали")</returns>
      public static string GetObjectTypeName(int objTypeID)
      {
        return MetaDataHelperService.Instance.GetObjectTypeName(objTypeID);
      }

      /// <summary>Получить название типа объектов (например, "Детали")</summary>
      /// <param name="objTypeGuid">Идентификатор типа объекта</param>
      /// <returns>Название типа объектов (например, "Детали")</returns>
      public static string GetObjectTypeName(Guid objTypeGuid)
      {
        return MetaDataHelperService.Instance.GetObjectTypeName(objTypeGuid);
      }

      /// <summary>
      /// Получить полное название типа объектов (например, "Изделия\Детали")
      /// </summary>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <returns>Полное название типа объектов (например, "Изделия\Детали")</returns>
      public static string GetObjectTypeFullName(int objTypeID)
      {
        return MetaDataHelperService.Instance.GetObjectTypeFullName(objTypeID);
      }

      /// <summary>
      /// Получить название экземпляра типа объектов (например, "Деталь")
      /// </summary>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <returns>Название экземпляра типа объектов (например, "Деталь")</returns>
      public static string GetObjectName(int objTypeID)
      {
        return MetaDataHelperService.Instance.GetObjectName(objTypeID);
      }

      /// <summary>
      /// Получить название экземпляра типа объектов (например, "Деталь")
      /// </summary>
      /// <param name="objTypeGuid">Идентификатор типа объекта</param>
      /// <returns>Название экземпляра типа объектов (например, "Деталь")</returns>
      public static string GetObjectName(Guid objTypeGuid)
      {
        return MetaDataHelperService.Instance.GetObjectName(objTypeGuid);
      }

      /// <summary>
      /// Получить по наименованию типа объекта его Int32-идентификатор
      /// </summary>
      /// <param name="objTypeName">Наименование типа объекта</param>
      /// <returns>Идентификатор типа объекта. -1 - тип объекта не найден</returns>
      public static int GetObjectTypeIDFromName(string objTypeName)
      {
        return MetaDataHelperService.Instance.GetObjectTypeIDFromName(objTypeName);
      }

      /// <summary>Получить по Guid типа объекта его Int32-идентификатор</summary>
      /// <param name="objTypeGuid">Guid типа объекта</param>
      /// <returns>Идентификатор типа объекта. -1 - тип объекта не найден</returns>
      public static int GetObjectTypeID(Guid objTypeGuid)
      {
        return MetaDataHelperService.Instance.GetObjectTypeID(objTypeGuid);
      }

      /// <summary>
      /// Получить по Int32-идентификатору типа объекта его Guid-идентификатор
      /// </summary>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <returns>Идентификатор типа объекта. Guid.Empty - тип объекта не найден</returns>
      public static Guid GetObjectTypeGuid(int objTypeID)
      {
        return MetaDataHelperService.Instance.GetObjectTypeGuid(objTypeID);
      }

      /// <summary>
      /// Возвращает идентификатор типа объектов по строковому представлению его глобального идентификатора
      /// </summary>
      /// <param name="Guid">Guid типа объекта в виде строки</param>
      public static int GetObjectTypeID(string Guid)
      {
        return MetaDataHelperService.Instance.GetObjectTypeID(Guid);
      }

      /// <summary>Получить по Guid типа связи его Int32-идентификатор</summary>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>Идентификатор типа связи. -1 - тип связи не найден</returns>
      public static int GetRelationTypeID(Guid relTypeGuid)
      {
        return MetaDataHelperService.Instance.GetRelationTypeID(relTypeGuid);
      }

      /// <summary>
      /// Получить по Int32-идентификатору типа связи её Guid-идентификатор
      /// </summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Идентификатор типа связи. Guid.Empty - тип связи не найден</returns>
      public static Guid GetRelationTypeGuid(int relTypeID)
      {
        return MetaDataHelperService.Instance.GetRelationTypeGuid(relTypeID);
      }

      /// <summary>
      /// Возвращает идентификатор типа связи по строковому представлению её глобального идентификатора
      /// </summary>
      /// <param name="Guid">Guid типа связи в виде строки</param>
      public static int GetRelationTypeID(string Guid)
      {
        return MetaDataHelperService.Instance.GetRelationTypeID(Guid);
      }

      /// <summary>
      /// Получить Guid родительского типа объектов для указанного дочернего типа объекта
      /// </summary>
      /// <param name="childTypeGuid">Guid дочернего типа объекта</param>
      /// <returns>Guid родительского типа объектов для указанного дочернего типа объекта или Guid.Empty</returns>
      public static Guid GetObjectTypeParentID(Guid childTypeGuid)
      {
        return MetaDataHelperService.Instance.GetObjectTypeParentID(childTypeGuid);
      }

      /// <summary>
      /// Получить ID родительского типа объектов для указанного дочернего типа объекта
      /// </summary>
      /// <param name="childTypeID">ID дочернего типа объекта</param>
      /// <returns>ID родительского типа объектов для указанного дочернего типа объекта или -1</returns>
      public static int GetObjectTypeParentID(int childTypeID)
      {
        return MetaDataHelperService.Instance.GetObjectTypeParentID(childTypeID);
      }

      /// <summary>
      /// Получить список ID всех родительских объектов для указанного дочернего типа объекта
      /// </summary>
      /// <param name="childTypeGuid">Guid дочернего типа объекта</param>
      /// <returns>Список ID всех родительских объектов для указанного дочернего типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      public static List<int> GetObjectTypeParentsID(Guid childTypeGuid)
      {
        return MetaDataHelperService.Instance.GetObjectTypeParentsID(childTypeGuid);
      }

      /// <summary>
      /// Получить список Guid всех родительских типов объектов для указанного дочернего типа объекта
      /// </summary>
      /// <param name="childTypeID">ID дочернего типа объекта</param>
      /// <returns>Список Guid всех родительских типов объектов для указанного дочернего типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      public static List<Guid> GetObjectTypeParentsGuid(int childTypeID)
      {
        return MetaDataHelperService.Instance.GetObjectTypeParentsGuid(childTypeID);
      }

      /// <summary>
      /// Получить список ID всех родительских объектов для указанного дочернего типа объекта
      /// </summary>
      /// <param name="childTypeID">ID дочернего типа объекта</param>
      /// <returns>Список ID всех родительских объектов для указанного дочернего типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      public static List<int> GetObjectTypeParentsID(int childTypeID)
      {
        return MetaDataHelperService.Instance.GetObjectTypeParentsID(childTypeID);
      }

      /// <summary>
      /// Получить список ID всех родительских объектов для указанного дочернего типа объекта.
      /// Родительские объекты следуют в списке в порядке от самого верхнего типа объекта к дочерним.
      /// </summary>
      /// <param name="childTypeID">ID дочернего типа объекта</param>
      /// <returns>Список ID всех родительских объектов для указанного дочернего типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      public static List<int> GetObjectTypeParentsIDReverse(int childTypeID)
      {
        return MetaDataHelperService.Instance.GetObjectTypeParentsIDReverse(childTypeID);
      }

      /// <summary>
      /// Получить список Guid всех родительских типов объектов для указанного дочернего типа объекта
      /// </summary>
      /// <param name="childTypeGuid">Guid дочернего типа объекта</param>
      /// <returns>Список Guid всех родительских типов объектов для указанного дочернего типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      public static List<Guid> GetObjectTypeParentsGuid(Guid childTypeGuid)
      {
        return MetaDataHelperService.Instance.GetObjectTypeParentsGuid(childTypeGuid);
      }

      /// <summary>
      /// Проверить, является ли тип объекта parentType родительским типом для типа объекта childType
      /// </summary>
      /// <param name="childType">Проверяемый дочерний тип объекта</param>
      /// <param name="parentType">Проверяемый родительский тип объекта (он может быть в любом месте родительской иерархии)</param>
      /// <returns>true, если parentType является родительским типом для childType</returns>
      public static bool IsObjectTypeChildOf(Guid childType, Guid parentType)
      {
        return MetaDataHelperService.Instance.IsObjectTypeChildOf(childType, parentType);
      }

      /// <summary>
      /// Проверить, является ли тип объекта parentType родительским типом для типа объекта c идентификатором childTypeId
      /// </summary>
      /// <param name="childTypeId">Идентификатор проверяемого дочернего типа объекта</param>
      /// <param name="parentType">Проверяемый родительский тип объекта (он может быть в любом месте родительской иерархии)</param>
      /// <returns>true, если parentType является родительским типом для типа с идентификатором childTypeId</returns>
      public static bool IsObjectTypeChildOf(int childTypeId, Guid parentType)
      {
        return MetaDataHelperService.Instance.IsObjectTypeChildOf(childTypeId, parentType);
      }

      /// <summary>
      /// Определить уровень вложенности указанного типа объектов в иерархии. Значение 0 - типы объектов верхнего уровня
      /// </summary>
      /// <param name="objectTypeID">Идентификатор типа объекта</param>
      /// <returns>-1 - тип объекта не найден, 0 - тип верхнего уровня, больше нуля - уровень вложенности в иерархии</returns>
      public static int GetObjectTypeLevel(int objectTypeID)
      {
        return MetaDataHelperService.Instance.GetObjectTypeLevel(objectTypeID);
      }

      /// <summary>
      /// Проверить, является ли тип объекта parentType родительским типом для типа объекта childType
      /// </summary>
      /// <param name="childType">Проверяемый дочерний тип объекта</param>
      /// <param name="parentType">Проверяемый родительский тип объекта (он может быть в любом месте родительской иерархии)</param>
      /// <returns>true, если parentType является родительским типом для childType</returns>
      public static bool IsObjectTypeChildOf(int childType, int parentType)
      {
        return MetaDataHelperService.Instance.IsObjectTypeChildOf(childType, parentType);
      }

      /// <summary>
      /// Получить список ID всех дочерних типов объектов для указанного родительского типа объекта
      /// </summary>
      /// <param name="parentTypeGuid">Guid родительского типа объекта</param>
      /// <returns>Список ID всех дочерних типов объектов для указанного родительского типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      public static List<int> GetObjectTypeChildrenID(Guid parentTypeGuid)
      {
        return MetaDataHelperService.Instance.GetObjectTypeChildrenID(parentTypeGuid);
      }

      /// <summary>
      /// Получить список Guid всех дочерних типов объектов для указанного родительского типа объекта
      /// </summary>
      /// <param name="parentTypeID">ID родительского типа объекта</param>
      /// <returns>Список Guid всех дочерних типов объектов для указанного родительского типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      public static List<Guid> GetObjectTypeChildrenGuid(int parentTypeID)
      {
        return MetaDataHelperService.Instance.GetObjectTypeChildrenGuid(parentTypeID);
      }

      /// <summary>
      /// Получить список ID всех дочерних типов объектов для указанного родительского типа объекта
      /// </summary>
      /// <param name="parentTypeID">ID родительского типа объекта</param>
      /// <returns>Список ID всех дочерних объектов для указанного родительского типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      public static List<int> GetObjectTypeChildrenID(int parentTypeID)
      {
        return MetaDataHelperService.Instance.GetObjectTypeChildrenID(parentTypeID);
      }

      /// <summary>
      /// Получить список Guid всех дочерних типов объектов для указанного родительского типа объекта
      /// </summary>
      /// <param name="parentTypeGuid">Guid родительского типа объекта</param>
      /// <returns>Список Guid всех дочерних типов объектов для указанного родительского типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      public static List<Guid> GetObjectTypeChildrenGuid(Guid parentTypeGuid)
      {
        return MetaDataHelperService.Instance.GetObjectTypeChildrenGuid(parentTypeGuid);
      }

      /// <summary>
      /// Получить рекурсивно список ID всех дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов).
      /// Добавляется также и parentTypeID.
      /// </summary>
      /// <param name="parentTypeID">ID родительского типа объекта</param>
      /// <returns>Список ID всех дочерних объектов для указанного родительского типа объекта (включая унаследованные типы объектов).
      /// В любом случае будет возвращено не пустое значение</returns>
      public static List<int> GetObjectTypeChildrenIDRecursive(int parentTypeID)
      {
        return MetaDataHelperService.Instance.GetObjectTypeChildrenIDRecursive(parentTypeID);
      }

      /// <summary>
      /// Получить рекурсивно список ID всех локальных дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов).
      /// Добавляется также и parentTypeID, даже если он не является локальным типом (в начало списка).
      /// </summary>
      /// <param name="parentTypeID">ID родительского типа объекта</param>
      /// <returns>Список ID всех дочерних локальных типов объектов для указанного родительского типа объекта
      /// В любом случае будет возвращено не пустое значение</returns>
      public static List<int> GetLocalObjectTypeChildrenIDRecursive(int parentTypeID)
      {
        return MetaDataHelperService.Instance.GetLocalObjectTypeChildrenIDRecursive(parentTypeID);
      }

      /// <summary>
      /// Получить рекурсивно список ID всех дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов)
      /// Добавляется также и parentTypeID.
      /// </summary>
      /// <param name="parentTypeIDs">Список Int32-идентификаторов родительских типов объектов</param>
      /// <returns>Список ID всех дочерних объектов для указанных родительских типов объектов (включая унаследованные типы объектов).
      /// В любом случае будет возвращено не пустое значение</returns>
      public static List<int> GetObjectTypeChildrenIDRecursive(IEnumerable<int> parentTypeIDs)
      {
        return MetaDataHelperService.Instance.GetObjectTypeChildrenIDRecursive(parentTypeIDs);
      }

      /// <summary>
      /// Получить рекурсивно список ID всех локальных дочерних типов объектов для указанных родительских типов объектов
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов)
      /// Добавляются также и parentTypeIDs.
      /// </summary>
      /// <param name="parentTypeIDs">Список Int32-идентификаторов родительских типов объектов</param>
      /// <returns>Список ID всех дочерних локальных типов объектов для указанных родительских типов объектов.
      /// В любом случае будет возвращено не пустое значение</returns>
      public static List<int> GetLocalObjectTypeChildrenIDRecursive(IEnumerable<int> parentTypeIDs)
      {
        return MetaDataHelperService.Instance.GetLocalObjectTypeChildrenIDRecursive(parentTypeIDs);
      }

      /// <summary>
      /// Получить рекурсивно список ID всех дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов)
      /// Добавляется также и parentTypeID.
      /// </summary>
      /// <param name="parentTypeGuid">Guid родительского типа объекта</param>
      /// <returns>Список ID всех дочерних объектов для указанного родительского типа объекта (включая унаследованные типы объектов).
      /// В любом случае будет возвращено не пустое значение</returns>
      public static List<int> GetObjectTypeChildrenIDRecursive(Guid parentTypeGuid)
      {
        return MetaDataHelperService.Instance.GetObjectTypeChildrenIDRecursive(parentTypeGuid);
      }

      /// <summary>
      /// Получить рекурсивно список Guid всех дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов)
      /// </summary>
      /// <param name="parentTypeGuid">Guid родительского типа объекта</param>
      /// <returns>Список Guid всех дочерних типов объектов для указанного родительского типа объекта (включая унаследованные типы объектов).
      /// В любом случае будет возвращено не пустое значение</returns>
      public static List<Guid> GetObjectTypeChildrenGuidRecursive(Guid parentTypeGuid)
      {
        return MetaDataHelperService.Instance.GetObjectTypeChildrenGuidRecursive(parentTypeGuid);
      }

      /// <summary>
      /// Получить рекурсивно список Guid всех дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов)
      /// </summary>
      /// <param name="parentTypeGuids">Список Guid идентификаторов родительских типов объектов</param>
      /// <returns>Список Guid всех дочерних объектов для указанных родительских типов объектов (включая унаследованные типы объектов).
      /// В любом случае будет возвращено не пустое значение</returns>
      public static List<Guid> GetObjectTypeChildrenGuidRecursive(IEnumerable<Guid> parentTypeGuids)
      {
        return MetaDataHelperService.Instance.GetObjectTypeChildrenGuidRecursive(parentTypeGuids);
      }

      /// <summary>
      /// Получить рекурсивно список Guid всех дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов)
      /// </summary>
      /// <param name="parentTypeID">Int32-идентификатор родительского типа объекта</param>
      /// <returns>Список Guid всех дочерних типов объектов для указанного родительского типа объекта (включая унаследованные типы объектов).
      /// В любом случае будет возвращено не пустое значение</returns>
      public static List<Guid> GetObjectTypeChildrenGuidRecursive(int parentTypeID)
      {
        return MetaDataHelperService.Instance.GetObjectTypeChildrenGuidRecursive(parentTypeID);
      }

      /// <summary>
      /// Метод получает на вход список допустимых типов объектов. Затем он "раскручивает" их родительские
      /// типы объектов (вверх по иерархии) до абстрактных родительских типов, а затем готовит список
      /// верхних допустимых родительских типов объектов. Метод можно использовать для подготовки списка
      /// типов объектов для окна по выбору объектов из списка допустимых типов, например, в команде
      /// "Добавить в состав"
      /// </summary>
      /// <param name="typeList">Список допустимых типов объектов</param>
      /// <returns>Список допустимых типов объектов верхнего уровня</returns>
      public static List<Guid> GetTopParentEnabledObjectTypesGuid(IEnumerable<Guid> typeList)
      {
        return MetaDataHelperService.Instance.GetTopParentEnabledObjectTypesGuid(typeList);
      }

      /// <summary>
      /// Метод получает на вход список допустимых типов объектов. Затем он "раскручивает" их родительские
      /// типы объектов (вверх по иерархии) до абстрактных родительских типов, а затем готовит список
      /// верхних допустимых родительских типов объектов. Метод можно использовать для подготовки списка
      /// типов объектов для окна по выбору объектов из списка допустимых типов, например, в команде
      /// "Добавить в состав"
      /// </summary>
      /// <param name="typeList">Список допустимых типов объектов</param>
      /// <returns>Список допустимых типов объектов верхнего уровня</returns>
      public static List<int> GetTopParentEnabledObjectTypes(IEnumerable<int> typeList)
      {
        return MetaDataHelperService.Instance.GetTopParentEnabledObjectTypes(typeList);
      }

      /// <summary>Получить список типов объектов верхнего уровня</summary>
      /// <returns>Список типов объектов верхнего уровня</returns>
      public static List<int> GetTopObjectTypesIDs()
      {
        return MetaDataHelperService.Instance.GetTopObjectTypesIDs();
      }

      /// <summary>Получить список типов объектов верхнего уровня</summary>
      /// <returns>Список типов объектов верхнего уровня</returns>
      public static List<Guid> GetTopObjectTypesGuids()
      {
        return MetaDataHelperService.Instance.GetTopObjectTypesGuids();
      }

      /// <summary>
      /// Вернуть нелокальный или абстрактный родительский тип для указанного дочернего типа.
      /// Если дочерний тип является локальным, либо абстрактным, либо типом верхнего уровня,
      /// возвращается он сам. Используется для оптимизации запросов
      /// в коллекции объектов и связей.
      /// </summary>
      /// <param name="childType">Дочерний тип объекта, для которого надо найти родительский тип объекта</param>
      /// <returns>Нелокальный или абстрактный родительский тип для указанного дочернего типа</returns>
      public static int GetTopParentObjectTypeID(int childType)
      {
        return MetaDataHelperService.Instance.GetTopParentObjectTypeID(childType);
      }

      /// <summary>
      /// Попытаться отыскать общий нелокальный или абстрактный родительский тип для указанных типов,
      /// с условием, что они не являются локальными типами. Используется для оптимизации запросов
      /// в коллекции объектов и связей. Если общий тип найти нельзя, возвращается значение
      /// Intermech.Consts.UnknownObjectTypeId
      /// </summary>
      /// <param name="childType1">Первый дочерний тип объекта</param>
      /// <param name="childType2">Второй дочерний тип объекта</param>
      /// <returns>Общий нелокальный или абстрактный родительский тип для указанных типов,
      /// с условием, что они не являются локальными типами. Используется для оптимизации запросов
      /// в коллекции объектов и связей. Если общий тип найти нельзя, возвращается значение
      /// Intermech.Consts.UnknownObjectTypeId</returns>
      public static int GetCommonParentObjectTypeID(int childType1, int childType2)
      {
        return MetaDataHelperService.Instance.GetCommonParentObjectTypeID(childType1, childType2);
      }

      /// <summary>Попытаться отыскать общий родительский тип для указанных типов.
      /// Если общий тип найти нельзя, возвращается Intermech.Consts.UnknownObjectTypeId</summary>
      /// <param name="objectTypes">Перечисление идентификаторов типов объектов</param>
      /// <returns>Общий указанных типов. Если общий тип найти нельзя, возвращается Intermech.Consts.UnknownObjectTypeId</returns>
      public static int GetCommonParentObjectTypeID(IEnumerable<int> objectTypes)
      {
        return MetaDataHelperService.Instance.GetCommonParentObjectTypeID(objectTypes);
      }

      /// <summary>Попытаться отыскать общий родительский тип для указанных объектов.
      /// Если общий тип найти нельзя, возвращается Intermech.Consts.UnknownObjectTypeId</summary>
      /// <param name="objectVersionIDs">Перечисление идентификаторов версий объектов</param>
      /// <returns>Общий указанных типов. Если общий тип найти нельзя, возвращается Intermech.Consts.UnknownObjectTypeId</returns>
      public static int GetCommonParentObjectTypeID(IEnumerable<long> objectVersionIDs)
      {
        return MetaDataHelperService.Instance.GetCommonParentObjectTypeID(objectVersionIDs);
      }

      /// <summary>
      /// Оптимизировать список (удалить вложенные нелокальные дочерние типы объектов, если в списке есть их родительские типы
      /// </summary>
      /// <param name="childObjectTypes">Список дочерних типов объектов для типизированного запроса в коллекцию связей</param>
      /// <returns>Оптимизированный список типов дочерних объектов</returns>
      public static List<int> OptimizeChildObjectTypes(IEnumerable<int> childObjectTypes)
      {
        return MetaDataHelperService.Instance.OptimizeChildObjectTypes(childObjectTypes);
      }

      /// <summary>
      /// Получить Int32-идентификатор типа связи по умолчанию для указанного родительского типа объектов
      /// </summary>
      /// <param name="parentObjectType">Идентификатор родительского типа объектов</param>
      /// <returns>Int32-идентификатор типа связи по умолчанию для указанного родительского типа объектов.
      /// Вернёт -1 - если тип объекта или связи не найден</returns>
      public static int GetDefaultRelationTypeID(int parentObjectType)
      {
        return MetaDataHelperService.Instance.GetDefaultRelationTypeID(parentObjectType);
      }

      /// <summary>
      /// Получить Guid типа связи по умолчанию для указанного родительского типа объектов
      /// </summary>
      /// <param name="parentObjectType">Идентификатор родительского типа объектов</param>
      /// <returns>Guid типа связи по умолчанию для указанного родительского типа объектов.
      /// Вернёт Guid.Empty - если тип оъбекта или связи не найден</returns>
      public static Guid GetDefaultRelationTypeGuid(int parentObjectType)
      {
        return MetaDataHelperService.Instance.GetDefaultRelationTypeGuid(parentObjectType);
      }

      /// <summary>
      /// Получить Int32-идентификатор типа связи по умолчанию для указанного родительского типа объектов
      /// </summary>
      /// <param name="parentObjectTypeGuid">Guid родительского типа объектов</param>
      /// <returns>Int32-идентификатор типа связи по умолчанию для указанного родительского типа объектов.
      /// Вернёт -1 - если тип объекта или связи не найден</returns>
      public static int GetDefaultRelationTypeID(Guid parentObjectTypeGuid)
      {
        return MetaDataHelperService.Instance.GetDefaultRelationTypeID(parentObjectTypeGuid);
      }

      /// <summary>
      /// Получить Guid типа связи по умолчанию для указанного родительского типа объектов
      /// </summary>
      /// <param name="parentObjectTypeGuid">Guid родительского типа объектов</param>
      /// <returns>Guid типа связи по умолчанию для указанного родительского типа объектов.
      /// Вернёт Guid.Empty - если тип оъбекта или связи не найден</returns>
      public static Guid GetDefaultRelationTypeGuid(Guid parentObjectTypeGuid)
      {
        return MetaDataHelperService.Instance.GetDefaultRelationTypeGuid(parentObjectTypeGuid);
      }

      /// <summary>Получить список описаний всех типов объектов</summary>
      /// <returns>Список описаний всех типов объектов</returns>
      public static List<IMSObjectType> GetObjectTypesList()
      {
        return MetaDataHelperService.Instance.GetObjectTypesList();
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном типе связи
      /// </summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>true, если тип связи существует</returns>
      public static bool ExistsRelationType(int relTypeID)
      {
        return MetaDataHelperService.Instance.ExistsRelationType(relTypeID);
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном типе связи
      /// </summary>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>true, если тип связи существует</returns>
      public static bool ExistsRelationType(Guid relTypeGuid)
      {
        return MetaDataHelperService.Instance.ExistsRelationType(relTypeGuid);
      }

      /// <summary>Получить краткую информацию о типе связи</summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Краткая информация о типе связи или null</returns>
      public static IMSRelationType GetRelationType(int relTypeID)
      {
        return MetaDataHelperService.Instance.GetRelationType(relTypeID);
      }

      /// <summary>Получить краткую информацию о типе связи</summary>
      /// <param name="relTypeGuid">Идентификатор типа связи</param>
      /// <returns>Краткая информация о типе связи или null</returns>
      public static IMSRelationType GetRelationType(Guid relTypeGuid)
      {
        return MetaDataHelperService.Instance.GetRelationType(relTypeGuid);
      }

      /// <summary>
      /// Получить название типа связи (например, "Проектная связь")
      /// </summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Название типа связи (например, "")</returns>
      public static string GetRelationTypeName(int relTypeID)
      {
        return MetaDataHelperService.Instance.GetRelationTypeName(relTypeID);
      }

      /// <summary>
      /// Получить название типа связи (например, "Проектная связь")
      /// </summary>
      /// <param name="relTypeGuid">Идентификатор типа связи</param>
      /// <returns>Название типа связи (например, "Проектная связь")</returns>
      public static string GetRelationTypeName(Guid relTypeGuid)
      {
        return MetaDataHelperService.Instance.GetRelationTypeName(relTypeGuid);
      }

      /// <summary>
      /// Получить список всех типов объектов имеющих допустимые типы связей
      /// </summary>
      /// <returns>Список всех типов объектов имеющих допустимые типы связей </returns>
      public static List<int> GetObjectTypesWithApplicabilities()
      {
        return MetaDataHelperService.Instance.GetObjectTypesWithApplicabilities();
      }

      /// <summary>
      /// Получить список всех дочерних типов объектов имеющих допустимые типы связей с родительскими типами
      /// </summary>
      /// <returns></returns>
      public static List<int> GetObjectTypesWithEnterInApplicabilities()
      {
        return MetaDataHelperService.Instance.GetObjectTypesWithEnterInApplicabilities();
      }

      /// <summary>
      /// Получить список допустимых типов связей для указанного родительского типа объектов
      /// </summary>
      /// <param name="objTypeID">Идентификатор родительского типа объектов</param>
      /// <returns>Список допустимых типов связей для указанного родительского типа объектов или null</returns>
      public static List<IMSApplicability> GetObjectTypeApplicabilities(int objTypeID)
      {
        return MetaDataHelperService.Instance.GetObjectTypeApplicabilities(objTypeID);
      }

      /// <summary>
      /// Получить список допустимых типов связей для указанного дочернего типа объектов
      /// </summary>
      /// <param name="partTypeId">Идентификатор дочернего типа объекта</param>
      /// <returns>Список допустимых типов связей для указанного дочернего типа объекта или null</returns>
      public static List<IMSApplicability> GetObjectTypeParentApplicabilities(int partTypeId)
      {
        return MetaDataHelperService.Instance.GetObjectTypeParentApplicabilities(partTypeId);
      }

      /// <summary>
      /// Проверить, может ли указанный дочерний тип объекта входить хотя бы
      /// в один родительский тип хотя бы одним типом связи
      /// </summary>
      /// <param name="partTypeID">id дочернего типа объекта</param>
      /// <returns>true - объект может входить в состав родительского, false - объект не может входить в состав родительского</returns>
      public static bool CanEntersIn(int partTypeID)
      {
        return MetaDataHelperService.Instance.CanEntersIn(partTypeID);
      }

      /// <summary>
      /// Получить список допустимых типов связей для указанного родительского типа объектов
      /// </summary>
      /// <param name="objTypeGuid">Guid родительского типа объектов</param>
      /// <returns>Список допустимых типов связей для указанного родительского типа объектов или null</returns>
      public static List<IMSApplicability> GetObjectTypeApplicabilities(Guid objTypeGuid)
      {
        return MetaDataHelperService.Instance.GetObjectTypeApplicabilities(objTypeGuid);
      }

      /// <summary>
      /// Получить список идентификаторов допустимых типов связей для указанного родительского типа объектов
      /// </summary>
      /// <param name="objTypeID">Идентификатор родительского типа объектов</param>
      /// <returns>Список идентификаторов допустимых типов связей для указанного родительского типа объектов</returns>
      public static List<int> GetApplicabilityRelationTypesID(int objTypeID)
      {
        return MetaDataHelperService.Instance.GetApplicabilityRelationTypesID(objTypeID);
      }

      /// <summary>
      /// Получить список идентификаторов допустимых типов связей для указанного родительского типа объектов
      /// </summary>
      /// <param name="objTypeGuid">Guid родительского типа объектов</param>
      /// <returns>Список идентификаторов допустимых типов связей для указанного родительского типа объектов</returns>
      public static List<int> GetApplicabilityRelationTypesID(Guid objTypeGuid)
      {
        return MetaDataHelperService.Instance.GetApplicabilityRelationTypesID(objTypeGuid);
      }

      /// <summary>
      /// Получить список Guid допустимых типов связей для указанного родительского типа объектов
      /// </summary>
      /// <param name="objTypeID">Идентификатор родительского типа объектов</param>
      /// <returns>Список Guid допустимых типов связей для указанного родительского типа объектов</returns>
      public static List<Guid> GetApplicabilityRelationTypesGuids(int objTypeID)
      {
        return MetaDataHelperService.Instance.GetApplicabilityRelationTypesGuids(objTypeID);
      }

      /// <summary>
      /// Получить список Guid допустимых типов связей для указанного родительского типа объектов
      /// </summary>
      /// <param name="objTypeGuid">Guid родительского типа объектов</param>
      /// <returns>Список Guid допустимых типов связей для указанного родительского типа объектов</returns>
      public static List<Guid> GetApplicabilityRelationTypesGuids(Guid objTypeGuid)
      {
        return MetaDataHelperService.Instance.GetApplicabilityRelationTypesGuids(objTypeGuid);
      }

      /// <summary>
      /// Проверить, допустимо ли включить указанный дочерний тип объекта в указанный
      /// родительский тип объекта по указанному типу связи
      /// </summary>
      /// <param name="parObjTypeID">Родительский тип объекта</param>
      /// <param name="childObjTypeID">Дочерний тип объекта</param>
      /// <param name="relTypeID">Тип связи</param>
      /// <returns>true - такая связь допустима</returns>
      public static bool HasApplicability(int parObjTypeID, int childObjTypeID, int relTypeID)
      {
        return MetaDataHelperService.Instance.HasApplicability(parObjTypeID, childObjTypeID, relTypeID);
      }

      /// <summary>
      /// Проверить, может ли входить в состав указанного родительского типа объекта
      /// хотя бы один дочерний тип объектов хотя бы одним типом связи
      /// </summary>
      /// <param name="parObjTypeGuid">Guid родительского типа объекта</param>
      /// <returns>true - у объекта может быть состав, false - у объекта не может быть состав</returns>
      internal static bool HasApplicability(Guid parObjTypeGuid)
      {
        return MetaDataHelperService.Instance.HasApplicability(parObjTypeGuid);
      }

      /// <summary>
      /// Проверить, может ли входить в состав указанного родительского типа объекта
      /// хотя бы один дочерний тип объектов хотя бы одним типом связи
      /// </summary>
      /// <param name="parObjTypeID">Идентификатор родительского типа объекта</param>
      /// <returns>true - у объекта может быть состав, false - у объекта не может быть состав</returns>
      internal static bool HasApplicability(int parObjTypeID)
      {
        return MetaDataHelperService.Instance.HasApplicability(parObjTypeID);
      }

      /// <summary>
      /// Получить список описаний дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанному типу связи
      /// </summary>
      /// <param name="parObjTypeID">Идентификатор родительского типа объектов</param>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Список описаний дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      public static List<IMSObjectType> GetApplicabilityChildObjectTypes(
        int parObjTypeID,
        int relTypeID)
      {
        return MetaDataHelperService.Instance.GetApplicabilityChildObjectTypes(parObjTypeID, relTypeID);
      }

      /// <summary>
      /// Получить применяемость для указанного дочернего типа объектов в составе указанного
      /// родительского типа объектов по указанному типу связи
      /// Если для childObjTypeID применяемость не найдена, рекурсивно вверх искать применяемость для родительского
      /// </summary>
      /// <param name="parObjTypeID">Идентификатор родительского типа объектов</param>
      /// <param name="childObjTypeID">Идентификатор дочернего типа объектов</param>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Применяемость или null</returns>
      public static IMSApplicability GetApplicability(
        int parObjTypeID,
        int childObjTypeID,
        int relTypeID)
      {
        return MetaDataHelperService.Instance.GetApplicability(parObjTypeID, childObjTypeID, relTypeID);
      }

      /// <summary>
      /// Получить список описаний дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанному типу связи
      /// </summary>
      /// <param name="parObjTypeGuid">Guid родительского типа объектов</param>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>Список описаний дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      public static List<IMSObjectType> GetApplicabilityChildObjectTypes(
        Guid parObjTypeGuid,
        Guid relTypeGuid)
      {
        return MetaDataHelperService.Instance.GetApplicabilityChildObjectTypes(parObjTypeGuid, relTypeGuid);
      }

      /// <summary>
      /// Получить список идентификаторов дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанному типу связи
      /// </summary>
      /// <param name="parObjTypeID">Идентификатор родительского типа объектов</param>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Список идентификаторов дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      public static List<int> GetApplicabilityChildObjectTypesID(int parObjTypeID, int relTypeID)
      {
        return MetaDataHelperService.Instance.GetApplicabilityChildObjectTypesID(parObjTypeID, relTypeID);
      }

      /// <summary>
      /// Получить список идентификаторов дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанным типам связей
      /// </summary>
      /// <param name="parObjTypeID">Идентификатор родительского типа объектов</param>
      /// <param name="relTypeIDs">Идентификаторы типов связей</param>
      /// <returns>Список идентификаторов дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      public static List<int> GetApplicabilityChildObjectTypesID(
        int parObjTypeID,
        IEnumerable<int> relTypeIDs)
      {
        return MetaDataHelperService.Instance.GetApplicabilityChildObjectTypesID(parObjTypeID, relTypeIDs);
      }

      /// <summary>
      /// Получить список идентификаторов дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанному типу связи
      /// </summary>
      /// <param name="parObjTypeGuid">Guid родительского типа объектов</param>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>Список идентификаторов дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      public static List<int> GetApplicabilityChildObjectTypesID(Guid parObjTypeGuid, Guid relTypeGuid)
      {
        return MetaDataHelperService.Instance.GetApplicabilityChildObjectTypesID(parObjTypeGuid, relTypeGuid);
      }

      /// <summary>
      /// Получить список идентификаторов дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанным типам связей
      /// </summary>
      /// <param name="parObjTypeGuid">Guid родительского типа объектов</param>
      /// <param name="relTypeGuids">Guid типов связей</param>
      /// <returns>Список идентификаторов дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      public static List<int> GetApplicabilityChildObjectTypesID(
        Guid parObjTypeGuid,
        IEnumerable<Guid> relTypeGuids)
      {
        return MetaDataHelperService.Instance.GetApplicabilityChildObjectTypesID(parObjTypeGuid, relTypeGuids);
      }

      /// <summary>
      /// Получить список Guid дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанному типу связи
      /// </summary>
      /// <param name="parObjTypeID">Идентификатор родительского типа объектов</param>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Список Guid дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      public static List<Guid> GetApplicabilityChildObjectTypesGuid(int parObjTypeID, int relTypeID)
      {
        return MetaDataHelperService.Instance.GetApplicabilityChildObjectTypesGuid(parObjTypeID, relTypeID);
      }

      /// <summary>
      /// Получить список Guid дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанным типам связей
      /// </summary>
      /// <param name="parObjTypeID">Идентификатор родительского типа объектов</param>
      /// <param name="relTypeIDs">Список идентификаторов типов связей</param>
      /// <returns>Список Guid дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      public static List<Guid> GetApplicabilityChildObjectTypesGuid(
        int parObjTypeID,
        IEnumerable<int> relTypeIDs)
      {
        return MetaDataHelperService.Instance.GetApplicabilityChildObjectTypesGuid(parObjTypeID, relTypeIDs);
      }

      /// <summary>
      /// Получить список Guid дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанному типу связи
      /// </summary>
      /// <param name="parObjTypeGuid">Guid родительского типа объектов</param>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>Список Guid дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      public static List<Guid> GetApplicabilityChildObjectTypesGuid(
        Guid parObjTypeGuid,
        Guid relTypeGuid)
      {
        return MetaDataHelperService.Instance.GetApplicabilityChildObjectTypesGuid(parObjTypeGuid, relTypeGuid);
      }

      /// <summary>
      /// Получить список Guid дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанным типам связей
      /// </summary>
      /// <param name="parObjTypeGuid">Guid родительского типа объектов</param>
      /// <param name="relTypeGuids">Список Guid типов связей</param>
      /// <returns>Список Guid дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      public static List<Guid> GetApplicabilityChildObjectTypesGuid(
        Guid parObjTypeGuid,
        IEnumerable<Guid> relTypeGuids)
      {
        return MetaDataHelperService.Instance.GetApplicabilityChildObjectTypesGuid(parObjTypeGuid, relTypeGuids);
      }

      /// <summary>
      /// Проверить, разрешён ли указанный родительский тип объектов,
      /// если есть списки разрешённых и запрещённых родительских типов объектов.
      /// Метод учитывает иерархию типов объектов для последовательного поиска, в какой
      /// из списков раньше попадёт проверяемый тип объекта, либо его родительские типы
      /// </summary>
      /// <param name="parentObjType">Проверяемый родительский тип объекта</param>
      /// <param name="enabledParents">Список разрешённых родительских типов объектов</param>
      /// <param name="disabledParents">Список запрещённых родительских типов объектов</param>
      /// <param name="defValue">Значение по умолчанию, если информации в списках оказалось недостаточно</param>
      /// <returns>true - применяемость с указанным родительским типом разрешена</returns>
      public static bool IsEnabledParentType(
        int parentObjType,
        IEnumerable<int> enabledParents,
        IEnumerable<int> disabledParents,
        bool defValue)
      {
        return MetaDataHelperService.Instance.IsEnabledParentType(parentObjType, enabledParents, disabledParents, defValue);
      }

      /// <summary>Поддерживает ли указанный тип связи ручную сортировку</summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>true, если указанный тип связи поддерживает ручную сортировку</returns>
      public static bool HasRelationTypeSorting(int relTypeID)
      {
        return MetaDataHelperService.Instance.HasRelationTypeSorting(relTypeID);
      }

      /// <summary>Поддерживает ли указанный тип связи ручную сортировку</summary>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>true, если указанный тип связи поддерживает ручную сортировку</returns>
      public static bool HasRelationTypeSorting(Guid relTypeGuid)
      {
        return MetaDataHelperService.Instance.HasRelationTypeSorting(relTypeGuid);
      }

      /// <summary>
      /// Получить список Int32-идентификаторов типов связей, поддерживающих ручную сортировку
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов связей, поддерживающих ручную сортировку</returns>
      public static List<int> GetSpecialSortingRelationsIDs()
      {
        return MetaDataHelperService.Instance.GetSpecialSortingRelationsIDs();
      }

      /// <summary>
      /// Получить список Guid идентификаторов типов связей, поддерживающих ручную сортировку
      /// </summary>
      /// <returns>Список Guid идентификаторов типов связей, поддерживающих ручную сортировку</returns>
      public static List<Guid> GetSpecialSortingRelationsGuids()
      {
        return MetaDataHelperService.Instance.GetSpecialSortingRelationsGuids();
      }

      /// <summary>
      /// Поддерживает ли указанный тип связи работу с допустимыми заменами
      /// </summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>true, если указанный тип связи поддерживает работу с допустимыми заменами</returns>
      public static bool HasRelationTypeSubstitutes(int relTypeID)
      {
        return MetaDataHelperService.Instance.HasRelationTypeSubstitutes(relTypeID);
      }

      /// <summary>
      /// Поддерживает ли указанный тип связи работу с допустимыми заменами
      /// </summary>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>true, если указанный тип связи поддерживает работу с допустимыми заменами</returns>
      public static bool HasRelationTypeSubstitutes(Guid relTypeGuid)
      {
        return MetaDataHelperService.Instance.HasRelationTypeSubstitutes(relTypeGuid);
      }

      /// <summary>
      /// Получить список Int32-идентификаторов типов связей, позволяющих работу с допустимыми заменами
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов связей, позволяющих работу с допустимыми заменами</returns>
      public static List<int> GetSpecialSubstitutesRelationsIDs()
      {
        return MetaDataHelperService.Instance.GetSpecialSubstitutesRelationsIDs();
      }

      /// <summary>
      /// Получить список Guid идентификаторов типов связей, позволяющих работу с допустимыми заменами
      /// </summary>
      /// <returns>Список Guid идентификаторов типов связей, позволяющих работу с допустимыми заменами</returns>
      public static List<Guid> GetSpecialSubstitutesRelationsGuids()
      {
        return MetaDataHelperService.Instance.GetSpecialSubstitutesRelationsGuids();
      }

      /// <summary>
      /// Поддерживает ли указанный тип связи группирование объектов
      /// </summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>true, если указанный тип связи поддерживает группирование объектов</returns>
      public static bool HasRelationTypeGrouping(int relTypeID)
      {
        return MetaDataHelperService.Instance.HasRelationTypeGrouping(relTypeID);
      }

      /// <summary>
      /// Поддерживает ли указанный тип связи группирование объектов
      /// </summary>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>true, если указанный тип связи поддерживает группирование объектов</returns>
      public static bool HasRelationTypeGrouping(Guid relTypeGuid)
      {
        return MetaDataHelperService.Instance.HasRelationTypeGrouping(relTypeGuid);
      }

      /// <summary>
      /// Получить список Int32-идентификаторов группирующих типов связей
      /// </summary>
      /// <returns>Список Int32-идентификаторов группирующих типов связей</returns>
      public static List<int> GetSpecialGroupingRelationsIDs()
      {
        return MetaDataHelperService.Instance.GetSpecialGroupingRelationsIDs();
      }

      /// <summary>
      /// Получить список Guid идентификаторов группирующих типов связей
      /// </summary>
      /// <returns>Список Guid идентификаторов группирующих типов связей</returns>
      public static List<Guid> GetSpecialGroupingRelationsGuids()
      {
        return MetaDataHelperService.Instance.GetSpecialGroupingRelationsGuids();
      }

      /// <summary>
      /// Проверить, является ли указанный тип связи конфигурируемым
      /// </summary>
      /// <param name="relType">Проверяемый тип связи</param>
      /// <returns>true - тип связи допускает конфигурирование составов</returns>
      public static bool IsPdmConfigurableRelationType(int relType)
      {
        return MetaDataHelperService.Instance.IsPdmConfigurableRelationType(relType);
      }

      /// <summary>
      /// Проверить, является ли указанный тип связи частично конфигурируемым
      /// (в наличии есть атрибут "Контекст конфигуратора составов")
      /// </summary>
      /// <param name="relType">Проверяемый тип связи</param>
      /// <returns>true - тип связи допускает частичное конфигурирование составов</returns>
      public static bool IsPdmPartiallyConfigurableRelationType(int relType)
      {
        return MetaDataHelperService.Instance.IsPdmPartiallyConfigurableRelationType(relType);
      }

      /// <summary>Получить список описаний всех типов связей</summary>
      /// <returns>Список описаний всех типов связей</returns>
      public static List<IMSRelationType> GetRelationTypesList()
      {
        return MetaDataHelperService.Instance.GetRelationTypesList();
      }

      /// <summary>Проверить, является ли тип объектов локальным</summary>
      /// <param name="type">Идентификатор типа объектов</param>
      /// <returns>true - тип объектов является локальным</returns>
      public static bool IsLocalObjectType(int type)
      {
        return MetaDataHelperService.Instance.IsLocalObjectType(type);
      }

      /// <summary>Проверить, является ли тип объектов локальным</summary>
      /// <param name="type">Идентификатор типа объектов</param>
      /// <returns>true - тип объектов является локальным</returns>
      public static bool IsLocalObjectType(Guid type)
      {
        return MetaDataHelperService.Instance.IsLocalObjectType(type);
      }

      /// <summary>
      /// Проверить, есть ли в списке хотя бы один основной или вложенный локальный тип объектов
      /// </summary>
      /// <param name="types">Список идентификаторов типов объектов</param>
      /// <returns>true - найден основной или вложенный локальный тип объектов</returns>
      public static bool HasLocalObjectType(IEnumerable<int> types)
      {
        return MetaDataHelperService.Instance.HasLocalObjectType(types);
      }

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать связи, участвующие в допустимых заменах
      /// </summary>
      /// <param name="objTypeID">Родительский тип объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать связи, участвующие в допустимых заменах</returns>
      public static bool HasObjectTypeSubstRelTypes(int objTypeID)
      {
        return MetaDataHelperService.Instance.HasObjectTypeSubstRelTypes(objTypeID);
      }

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать связи, участвующие в допустимых заменах
      /// </summary>
      /// <param name="objTypeGuid">Guid родительского типа объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать связи, участвующие в допустимых заменах</returns>
      public static bool HasObjectTypeSubstRelTypes(Guid objTypeGuid)
      {
        return MetaDataHelperService.Instance.HasObjectTypeSubstRelTypes(objTypeGuid);
      }

      /// <summary>
      /// Получить список Int32-идентификаторов типов объектов, участвующих в допустимых заменах
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов объектов, участвующих в допустимых заменах</returns>
      public static List<int> GetSubstituteObjectsIDs()
      {
        return MetaDataHelperService.Instance.GetSubstituteObjectsIDs();
      }

      /// <summary>
      /// Получить список Guid идентификаторов типов объектов, участвующих в допустимых заменах
      /// </summary>
      /// <returns>Список Guid идентификаторов типов объектов, участвующих в допустимых заменах</returns>
      public static List<Guid> GetSubstituteObjectsGuids()
      {
        return MetaDataHelperService.Instance.GetSubstituteObjectsGuids();
      }

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать связи, позволяющие выполнять ручную сортировку
      /// </summary>
      /// <param name="objTypeID">Родительский тип объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать связи, позволяющие выполнять ручную сортировку</returns>
      public static bool HasObjectTypeSortingRelTypes(int objTypeID)
      {
        return MetaDataHelperService.Instance.HasObjectTypeSortingRelTypes(objTypeID);
      }

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать связи, позволяющие выполнять ручную сортировку
      /// </summary>
      /// <param name="objTypeGuid">Guid родительского типа объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать связи, позволяющие выполнять ручную сортировку</returns>
      public static bool HasObjectTypeSortingRelTypes(Guid objTypeGuid)
      {
        return MetaDataHelperService.Instance.HasObjectTypeSortingRelTypes(objTypeGuid);
      }

      /// <summary>
      /// Получить список Int32-идентификаторов типов объектов, которые могут содержать связи с сортировкой
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов объектов, которые могут содержать связи с сортировкой</returns>
      public static List<int> GetSortingObjectsIDs()
      {
        return MetaDataHelperService.Instance.GetSortingObjectsIDs();
      }

      /// <summary>
      /// Получить список Guid идентификаторов типов объектов, которые могут содержать связи с сортировкой
      /// </summary>
      /// <returns>Список Guid идентификаторов типов объектов, которые могут содержать связи с сортировкой</returns>
      public static List<Guid> GetSortingObjectsGuids()
      {
        return MetaDataHelperService.Instance.GetSortingObjectsGuids();
      }

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать связи типа "Состав изделия"
      /// </summary>
      /// <param name="objTypeID">Родительский тип объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать связи типа "Состав изделия"</returns>
      public static bool HasObjectTypeDesignedRelType(int objTypeID)
      {
        return MetaDataHelperService.Instance.HasObjectTypeDesignedRelType(objTypeID);
      }

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать связи типа "Состав изделия"
      /// </summary>
      /// <param name="objTypeGuid">Guid родительского типа объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать связи типа "Состав изделия"</returns>
      public static bool HasObjectTypeDesignedRelType(Guid objTypeGuid)
      {
        return MetaDataHelperService.Instance.HasObjectTypeDesignedRelType(objTypeGuid);
      }

      /// <summary>
      /// Получить список Int32-идентификаторов типов объектов, которые могут содержать связи типа "Состав изделия"
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов объектов, которые могут содержать связи типа "Состав изделия"</returns>
      public static List<int> GetDesignedObjectsIDs()
      {
        return MetaDataHelperService.Instance.GetDesignedObjectsIDs();
      }

      /// <summary>
      /// Получить список Guid идентификаторов типов объектов, которые могут содержать связи типа "Состав изделия"
      /// </summary>
      /// <returns>Список Guid идентификаторов типов объектов, которые могут содержать связи типа "Состав изделия"</returns>
      public static List<Guid> GetDesignedObjectsGuids()
      {
        return MetaDataHelperService.Instance.GetDesignedObjectsGuids();
      }

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать группирующие связи и сам является группирующим
      /// </summary>
      /// <param name="objTypeID">Родительский тип объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать группирующие связи и сам является группирующим</returns>
      public static bool HasObjectTypeGroupingRelTypes(int objTypeID)
      {
        return MetaDataHelperService.Instance.HasObjectTypeGroupingRelTypes(objTypeID);
      }

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать группирующие связи и сам является группирующим
      /// </summary>
      /// <param name="objTypeGuid">Guid родительского типа объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать группирующие связи и сам является группирующим</returns>
      public static bool HasObjectTypeGroupingRelTypes(Guid objTypeGuid)
      {
        return MetaDataHelperService.Instance.HasObjectTypeGroupingRelTypes(objTypeGuid);
      }

      /// <summary>
      /// Получить список Int32-идентификаторов группирующих типов объектов
      /// </summary>
      /// <returns>Список Int32-идентификаторов группирующих типов объектов</returns>
      public static List<int> GetSpecialGroupingIDs()
      {
        return MetaDataHelperService.Instance.GetSpecialGroupingIDs();
      }

      /// <summary>
      /// Получить список Guid идентификаторов группирующих типов объектов
      /// </summary>
      /// <returns>Список Guid идентификаторов группирующих типов объектов</returns>
      public static List<Guid> GetSpecialGroupingGuids()
      {
        return MetaDataHelperService.Instance.GetSpecialGroupingGuids();
      }

      /// <summary>
      /// Может ли указанный тип объекта входить в состав группирующих объектов
      /// </summary>
      /// <param name="objTypeID">Тип объекта</param>
      /// <returns>true, если указанный родительский тип объекта может входить в состав группирующих объектов</returns>
      public static bool HasObjectTypeGrouppedRelTypes(int objTypeID)
      {
        return MetaDataHelperService.Instance.HasObjectTypeGrouppedRelTypes(objTypeID);
      }

      /// <summary>
      /// Может ли указанный тип объекта входить в состав группирующих объектов
      /// </summary>
      /// <param name="objTypeGuid">Guid типа объекта</param>
      /// <returns>true, если указанный тип объекта может входить в состав группирующих объектов</returns>
      public static bool HasObjectTypeGrouppedRelTypes(Guid objTypeGuid)
      {
        return MetaDataHelperService.Instance.HasObjectTypeGrouppedRelTypes(objTypeGuid);
      }

      /// <summary>
      /// Получить список Int32-идентификаторов типов объектов, которые могут входить в состав группирующих объектов
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов объектов, которые могут входить в состав группирующих объектов</returns>
      public static List<int> GetSpecialGrouppedIDs()
      {
        return MetaDataHelperService.Instance.GetSpecialGrouppedIDs();
      }

      /// <summary>
      /// Получить список Guid идентификаторов типов объектов, которые могут входить в состав группирующих объектов
      /// </summary>
      /// <returns>Список Guid идентификаторов типов объектов, которые могут входить в состав группирующих объектов</returns>
      public static List<Guid> GetSpecialGrouppedGuids()
      {
        return MetaDataHelperService.Instance.GetSpecialGrouppedGuids();
      }

      /// <summary>
      /// Может ли указанный тип объекта содержать атрибут "Видимость объекта"
      /// </summary>
      /// <param name="objTypeID">Тип объекта</param>
      /// <returns>true, если указанный тип объекта может содержать атрибут "Видимость объекта"</returns>
      public static bool HasObjectTypeVisibilityAttr(int objTypeID)
      {
        return MetaDataHelperService.Instance.HasObjectTypeVisibilityAttr(objTypeID);
      }

      /// <summary>
      /// Может ли указанный тип объекта содержать атрибут "Видимость объекта"
      /// </summary>
      /// <param name="objTypeGuid">Guid типа объекта</param>
      /// <returns>true, если указанный тип объекта может содержать атрибут "Видимость объекта"</returns>
      public static bool HasObjectTypeVisibilityAttr(Guid objTypeGuid)
      {
        return MetaDataHelperService.Instance.HasObjectTypeVisibilityAttr(objTypeGuid);
      }

      /// <summary>
      /// Получить список Int32-идентификаторов типов объектов, которые могут содержать атрибут "Видимость объекта"
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов объектов, которые могут содержать атрибут "Видимость объекта"</returns>
      public static List<int> GetVisibilityObjectsIDs()
      {
        return MetaDataHelperService.Instance.GetVisibilityObjectsIDs();
      }

      /// <summary>
      /// Получить список Guid идентификаторов типов объектов, которые могут содержать атрибут "Видимость объекта"
      /// </summary>
      /// <returns>Список Guid идентификаторов типов объектов, которые могут содержать атрибут "Видимость объекта"</returns>
      public static List<Guid> GetVisibilityObjectsGuids()
      {
        return MetaDataHelperService.Instance.GetVisibilityObjectsGuids();
      }

      /// <summary>
      /// Проверка на необходимость включения версии объектов указанного типа в контекст, при условии что он доступен в сессии
      /// (без проверки на наличие другой версии объекта в контексте)
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="objectType">Тип проверяемого объекта</param>
      /// <param name="customFunc">Кастом функция позволяющая переопределить поведение для определенных типов объектов</param>
      /// <returns>true - данный объект необходимо добавлять в текущий контекст редактирования (без проверки на наличие другой версии объекта в контексте)</returns>
      public static bool MustAppendVersionToEditingContext(
        IUserSession session,
        int objectType,
        Func<EditingContextMode> customFunc = null)
      {
        return MetaDataHelperService.Instance.MustAppendVersionToEditingContext(session, objectType, customFunc);
      }

      /// <summary>
      /// Проверить, является ли указанный тип объектов-контектов упрощённым контекстом
      /// (не меняет содержимое номера группы изенений у контекстных объектов, не может
      /// быть связанным, допускает применение в своём содержимом версий объектов, принадлежащих
      /// другим контекстам редактирования)
      /// </summary>
      /// <param name="contextTypeID">Идентификатор типа объекта-контекста</param>
      /// <returns>true - указанный тип объекта является упрощённым контекстом</returns>
      public static bool IsSimpleEditingContext(int contextTypeID)
      {
        return MetaDataHelperService.Instance.IsSimpleEditingContext(contextTypeID);
      }

      /// <summary>
      /// Является ли указанный тип объекта контекстом редактирования
      /// </summary>
      /// <param name="objTypeID">Тип объекта</param>
      /// <returns>true, если указанный тип объекта является контекстом редактирования</returns>
      public static bool IsObjectTypeEditingContext(int objTypeID)
      {
        return MetaDataHelperService.Instance.IsObjectTypeEditingContext(objTypeID);
      }

      /// <summary>
      /// Является ли указанный тип объекта контекстом редактирования
      /// </summary>
      /// <param name="objTypeGuid">Guid типа объекта</param>
      /// <returns>true, если указанный тип объекта является контекстом редактирования</returns>
      public static bool IsObjectTypeEditingContext(Guid objTypeGuid)
      {
        return MetaDataHelperService.Instance.IsObjectTypeEditingContext(objTypeGuid);
      }

      /// <summary>
      /// Получить список Int32-идентификаторов типов объектов, которые являются контекстами редактирования
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов объектов, которые являются контекстами редактирования</returns>
      public static List<int> GetEditingContextObjectsIDs()
      {
        return MetaDataHelperService.Instance.GetEditingContextObjectsIDs();
      }

      /// <summary>
      /// Получить список Int32-идентификаторов типов объектов верхнего уровня, которые являются контекстами редактирования
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов объектов верхнего уровня, которые являются контекстами редактирования</returns>
      public static List<int> GetEditingContextTopObjectsIDs()
      {
        return MetaDataHelperService.Instance.GetEditingContextTopObjectsIDs();
      }

      /// <summary>
      /// Получить список Guid идентификаторов типов объектов, которые являются контекстами редактирования
      /// </summary>
      /// <returns>Список Guid идентификаторов типов объектов, которые являются контекстами редактирования</returns>
      public static List<Guid> GetEditingContextObjectsGuids()
      {
        return MetaDataHelperService.Instance.GetEditingContextObjectsGuids();
      }

      /// <summary>
      /// Получить список Guid идентификаторов типов объектов верхнего уровня, которые являются контекстами редактирования
      /// </summary>
      /// <returns>Список Guid идентификаторов типов объектов верхнего уровня, которые являются контекстами редактирования</returns>
      public static List<Guid> GetEditingContextTopObjectsGuids()
      {
        return MetaDataHelperService.Instance.GetEditingContextTopObjectsGuids();
      }

      /// <summary>
      /// Проверить, можно ли добавлять указанный тип объекта в контекст редактирования
      /// </summary>
      /// <param name="objTypeGuid">Guid проверяемого типа объекта</param>
      /// <param name="autoMode">Включен ли режим автоматического пополнения</param>
      /// <returns>true - указанный тип объекта допускается добавлять в контекст редактирования</returns>
      public static bool CanAddObjTypeToEditingContext(Guid objTypeGuid, bool autoMode)
      {
        return MetaDataHelperService.Instance.CanAddObjTypeToEditingContext(objTypeGuid, autoMode);
      }

      /// <summary>
      /// Проверить, можно ли добавлять указанный тип объекта в контекст редактирования
      /// </summary>
      /// <param name="objType">Проверяемый тип объекта</param>
      /// <param name="autoMode">Включен ли режим автоматического пополнения</param>
      /// <returns>true - указанный тип объекта допускается добавлять в контекст редактирования</returns>
      public static bool CanAddObjTypeToEditingContext(int objType, bool autoMode)
      {
        return MetaDataHelperService.Instance.CanAddObjTypeToEditingContext(objType, autoMode);
      }

      /// <summary>
      /// Проверить, является ли указанный тип объекта корнем конфигурируемого состава
      /// </summary>
      /// <param name="objType">Проверяемый тип объекта</param>
      /// <returns>true - тип объекта может являться корнем конфигуриемого состава</returns>
      public static bool IsPdmRootObjectType(int objType)
      {
        return MetaDataHelperService.Instance.IsPdmRootObjectType(objType);
      }

      /// <summary>
      /// Проверить, является ли указанный тип объекта конфигурируемым
      /// </summary>
      /// <param name="objType">Проверяемый тип объекта</param>
      /// <returns>true - тип объекта допускает конфигурирование составов</returns>
      public static bool IsPdmConfigurableObjectType(int objType)
      {
        return MetaDataHelperService.Instance.IsPdmConfigurableObjectType(objType);
      }

      /// <summary>
      /// Проверить, может ли указанный тип объекта выступать в роли контекста конфигуратора составов
      /// </summary>
      /// <param name="objType">Проверяемый тип объекта</param>
      /// <returns>true - тип объекта может выступать в роли контекста конфигуратора составов</returns>
      public static bool IsPdmContextableObjectType(int objType)
      {
        return MetaDataHelperService.Instance.IsPdmContextableObjectType(objType);
      }

      /// <summary>
      /// Получить из кэша или из базы данных тип указанной связи. Если не задавать
      /// значение session, то значение будет получено из кэша. Если в кэше значения
      /// нет, вернётся -1. Если задать значение session, то будет выполнено обращение
      /// к базе данных, а новое значение будет помещено в кэш (при необходимости - поверх
      /// старого значения)
      /// </summary>
      /// <param name="session">Сессия, в рамках которой выполняется работа с базой данных</param>
      /// <param name="prjLinkID">Идентификатор связи, тип которой требуется получить</param>
      /// <returns>Идентификатор типа указанной связи или -1</returns>
      public static int GetRelationType4PrjLinkID(IUserSession session, long prjLinkID)
      {
        return MetaDataHelperService.Instance.GetRelationType4PrjLinkID(session, prjLinkID);
      }

      /// <summary>
      /// Получить Int32-идентификатор типа атрибута по его имени, Guid или числовому идентификатору.
      /// Сгенерирует исключение, если в метод засунуть объект некорректного типа
      /// </summary>
      /// <param name="attributeID">Имя атрибута, Guid или числовой идентификатор</param>
      /// <returns>Int32-идентификатор или Intermech.Consts.NavigatorUndefinedAttributeID, если тип атрибута не найден</returns>
      public static int GetAttributeID(object attributeID)
      {
        return MetaDataHelperService.Instance.GetAttributeID(attributeID);
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном типе атрибута
      /// </summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>true, если тип атрибута существует</returns>
      public static bool ExistsAttributeType(int attrTypeID)
      {
        return MetaDataHelperService.Instance.ExistsAttributeType(attrTypeID);
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном типе атрибута
      /// </summary>
      /// <param name="attrTypeGuid">Guid типа атрибута</param>
      /// <returns>true, если тип атрибута существует</returns>
      public static bool ExistsAttributeType(Guid attrTypeGuid)
      {
        return MetaDataHelperService.Instance.ExistsAttributeType(attrTypeGuid);
      }

      /// <summary>Получить краткую информацию о типе атрибута</summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Краткая информация о типе атрибута или null</returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IMSAttributeType GetAttributeType(int attrTypeID)
      {
        return MetaDataHelperService.Instance.GetAttributeType(attrTypeID);
      }

      /// <summary>Хранятся ли в атрибуте системные данные</summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>true, если в атрибуте хранятся системные данные</returns>
      public static bool HasAttributeSystemData(int attrTypeID)
      {
        return MetaDataHelperService.Instance.HasAttributeSystemData(attrTypeID);
      }

      /// <summary>Хранятся ли в атрибуте системные данные</summary>
      /// <param name="attrTypeGuid">Guid типа атрибута</param>
      /// <returns>true, если в атрибуте хранятся системные данные</returns>
      public static bool HasAttributeSystemData(Guid attrTypeGuid)
      {
        return MetaDataHelperService.Instance.HasAttributeSystemData(attrTypeGuid);
      }

      /// <summary>Хранится ли в атрибуте список допустимых значений</summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>true, если в атрибуте хранится список допустимых значений</returns>
      public static bool HasAttributePossibleValues(int attrTypeID)
      {
        return MetaDataHelperService.Instance.HasAttributePossibleValues(attrTypeID);
      }

      /// <summary>Хранится ли в атрибуте список допустимых значений</summary>
      /// <param name="attrTypeGuid">Guid типа атрибута</param>
      /// <returns>true, если в атрибуте хранится список допустимых значений</returns>
      public static bool HasAttributePossibleValues(Guid attrTypeGuid)
      {
        return MetaDataHelperService.Instance.HasAttributePossibleValues(attrTypeGuid);
      }

      /// <summary>Можно ли отображать атрибут</summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>true, если атрибут можно отображать</returns>
      public static bool IsAttributeGridable(int attrTypeID)
      {
        return MetaDataHelperService.Instance.IsAttributeGridable(attrTypeID);
      }

      /// <summary>Можно ли отображать атрибут</summary>
      /// <param name="attrTypeGuid">Guid типа атрибута</param>
      /// <returns>true, если атрибут можно отображать</returns>
      public static bool IsAttributeGridable(Guid attrTypeGuid)
      {
        return MetaDataHelperService.Instance.IsAttributeGridable(attrTypeGuid);
      }

      /// <summary>
      /// Является ли системный атрибут по своей сути атрибутом-ссылкой на объект
      /// </summary>
      /// <param name="attrTypeGuid"> </param>
      /// <returns></returns>
      public static bool IsSystemAttributeSupportsObjectLinks(Guid attrTypeGuid)
      {
        return SystemGUIDs.ObligatoryAttributesAsObjectLinks.IndexOf(attrTypeGuid) >= 0;
      }

      /// <summary>Получить краткую информацию о типе атрибута</summary>
      /// <param name="attrTypeGuid">Идентификатор типа атрибута</param>
      /// <returns>Краткая информация о типе атрибута или null</returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static IMSAttributeType GetAttributeType(Guid attrTypeGuid)
      {
        return MetaDataHelperService.Instance.GetAttributeType(attrTypeGuid);
      }

      /// <summary>Получить название типа атрибута</summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Название типа атрибута</returns>
      public static string GetAttributeTypeName(int attrTypeID)
      {
        return MetaDataHelperService.Instance.GetAttributeTypeName(attrTypeID);
      }

      /// <summary>Получить название типа атрибута</summary>
      /// <param name="attrTypeGuid">Идентификатор типа атрибута</param>
      /// <returns>Название типа атрибута</returns>
      public static string GetAttributeTypeName(Guid attrTypeGuid)
      {
        return MetaDataHelperService.Instance.GetAttributeTypeName(attrTypeGuid);
      }

      /// <summary>
      /// Получить по Guid типа атрибута его Int32-идентификатор
      /// </summary>
      /// <param name="attrTypeGuid">Guid типа атрибута</param>
      /// <returns>Идентификатор типа атрибута. -1 - тип атрибута не найден</returns>
      public static int GetAttributeTypeID(Guid attrTypeGuid)
      {
        return MetaDataHelperService.Instance.GetAttributeTypeID(attrTypeGuid);
      }

      /// <summary>
      /// Получить по Int32-идентификатору типа атрибута его Guid-идентификатор
      /// </summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Идентификатор типа атрибута. Guid.Empty - тип атрибута не найден</returns>
      public static Guid GetAttributeTypeGuid(int attrTypeID)
      {
        return MetaDataHelperService.Instance.GetAttributeTypeGuid(attrTypeID);
      }

      /// <summary>
      /// Возвращает идентификатор типа атрибута по строковому представлению его глобального идентификатора
      /// </summary>
      /// <param name="Guid">Guid типа атрибута в виде строки</param>
      public static int GetAttributeTypeID(string Guid)
      {
        return MetaDataHelperService.Instance.GetAttributeTypeID(Guid);
      }

      /// <summary>
      /// Возвращает идентификатор типа атрибута по его названию
      /// </summary>
      /// <param name="attrName">Название типа атрибута</param>
      public static int GetAttributeByTypeNameID(string attrName)
      {
        return MetaDataHelperService.Instance.GetAttributeByTypeNameID(attrName);
      }

      /// <summary>Возвращает Guid типа атрибута по его названию</summary>
      /// <param name="attrName">Название типа атрибута</param>
      public static Guid GetAttributeByTypeNameGuid(string attrName)
      {
        return MetaDataHelperService.Instance.GetAttributeByTypeNameGuid(attrName);
      }

      /// <summary>Получить список всех типов атрибутов</summary>
      /// <returns>Список всех типов атрибутов</returns>
      public static List<int> GetAttributeTypesIDList()
      {
        return MetaDataHelperService.Instance.GetAttributeTypesIDList();
      }

      /// <summary>Получить список Guid всех типов атрибутов</summary>
      /// <returns>Список Guid всех типов атрибутов</returns>
      public static List<Guid> GetAttributeTypesGuidList()
      {
        return MetaDataHelperService.Instance.GetAttributeTypesGuidList();
      }

      /// <summary>Получить список описаний всех типов атрибутов</summary>
      /// <returns>Список описаний всех типов атрибутов</returns>
      public static List<IMSAttributeType> GetAttributeTypesList()
      {
        return MetaDataHelperService.Instance.GetAttributeTypesList();
      }

      /// <summary>
      /// Получить список описаний атрибута для всех типов объектов, которым он назначен
      /// </summary>
      /// <param name="AttrTypeGuid">Guid типа атрибута</param>
      /// <returns>Список описаний атрибута для всех типов объектов, которым он назначен</returns>
      public static List<IMSAttribute4ObjectType> GetAllAttributes4ObjectTypeList(Guid AttrTypeGuid)
      {
        return MetaDataHelperService.Instance.GetAllAttributes4ObjectTypeList(AttrTypeGuid);
      }

      /// <summary>
      /// Получить список описаний атрибута для всех типов объектов, которым он назначен
      /// </summary>
      /// <param name="AttrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Список описаний атрибута для всех типов объектов, которым он назначен</returns>
      public static List<IMSAttribute4ObjectType> GetAllAttributes4ObjectTypeList(int AttrTypeID)
      {
        return MetaDataHelperService.Instance.GetAllAttributes4ObjectTypeList(AttrTypeID);
      }

      /// <summary>
      /// Получить список описаний всех типов атрибутов для указанного типа объекта
      /// </summary>
      /// <param name="objTypeGuid">Guid типа объекта</param>
      /// <returns>Список описаний всех типов атрибутов для указанного типа объекта</returns>
      public static List<IMSAttribute4ObjectType> GetAttribute4ObjectTypeList(Guid objTypeGuid)
      {
        return MetaDataHelperService.Instance.GetAttribute4ObjectTypeList(objTypeGuid);
      }

      /// <summary>
      /// Получить список описаний всех типов атрибутов для указанного типа объекта
      /// </summary>
      /// <param name="ObjectTypeID">Идентификатор типа объекта</param>
      /// <returns>Список описаний всех типов атрибутов для указанного типа объекта</returns>
      public static List<IMSAttribute4ObjectType> GetAttribute4ObjectTypeList(int ObjectTypeID)
      {
        return MetaDataHelperService.Instance.GetAttribute4ObjectTypeList(ObjectTypeID);
      }

      /// <summary>
      /// Получить описание типа атрибута для указанного типа объекта
      /// </summary>
      /// <param name="ObjectTypeGuid">Guid типа объекта</param>
      /// <param name="AttrTypeGuid">Guid типа атрибута</param>
      /// <returns>Описение типа атрибута для указанного типа объекта, или null</returns>
      public static IMSAttribute4ObjectType GetAttribute4ObjectType(
        Guid ObjectTypeGuid,
        Guid AttrTypeGuid)
      {
        return MetaDataHelperService.Instance.GetAttribute4ObjectType(ObjectTypeGuid, AttrTypeGuid);
      }

      /// <summary>
      /// Получить описание типа атрибута для указанного типа объекта
      /// </summary>
      /// <param name="ObjectTypeID">Идентификатор типа объекта</param>
      /// <param name="AttrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Описение типа атрибута для указанного типа объекта, или null</returns>
      public static IMSAttribute4ObjectType GetAttribute4ObjectType(int ObjectTypeID, int AttrTypeID)
      {
        return MetaDataHelperService.Instance.GetAttribute4ObjectType(ObjectTypeID, AttrTypeID);
      }

      /// <summary>
      /// Получить список описаний атрибута для всех типов связей, которым он назначен
      /// </summary>
      /// <param name="AttrTypeGuid">Guid типа атрибута</param>
      /// <returns>Список описаний атрибута для всех типов связей, которым он назначен</returns>
      public static List<IMSAttribute4RelationType> GetAllAttributes4RelationTypeList(Guid AttrTypeGuid)
      {
        return MetaDataHelperService.Instance.GetAllAttributes4RelationTypeList(AttrTypeGuid);
      }

      /// <summary>
      /// Получить список описаний атрибута для всех типов связей, которым он назначен
      /// </summary>
      /// <param name="AttrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Список описаний атрибута для всех типов связей, которым он назначен</returns>
      public static List<IMSAttribute4RelationType> GetAllAttributes4RelationTypeList(int AttrTypeID)
      {
        return MetaDataHelperService.Instance.GetAllAttributes4RelationTypeList(AttrTypeID);
      }

      /// <summary>
      /// Получить список описаний всех типов атрибутов для указанного типа связи
      /// </summary>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>Список описаний всех типов атрибутов для указанного типа связи</returns>
      public static List<IMSAttribute4RelationType> GetAttribute4RelationTypeList(Guid relTypeGuid)
      {
        return MetaDataHelperService.Instance.GetAttribute4RelationTypeList(relTypeGuid);
      }

      /// <summary>
      /// Получить список описаний всех типов атрибутов для указанного типа связи
      /// </summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Список описаний всех типов атрибутов для указанного типа связи</returns>
      public static List<IMSAttribute4RelationType> GetAttribute4RelationTypeList(int relTypeID)
      {
        return MetaDataHelperService.Instance.GetAttribute4RelationTypeList(relTypeID);
      }

      /// <summary>
      /// Получить описание типа атрибута для указанного типа связи
      /// </summary>
      /// <param name="RelationTypeGuid">Guid типа связи</param>
      /// <param name="AttrTypeGuid">Guid типа атрибута</param>
      /// <returns>Описение типа атрибута для указанного типа связи, или null</returns>
      public static IMSAttribute4RelationType GetAttribute4RelationType(
        Guid RelationTypeGuid,
        Guid AttrTypeGuid)
      {
        return MetaDataHelperService.Instance.GetAttribute4RelationType(RelationTypeGuid, AttrTypeGuid);
      }

      /// <summary>
      /// Получить описание типа атрибута для указанного типа объекта
      /// </summary>
      /// <param name="RelationTypeID">Идентификатор типа объекта</param>
      /// <param name="AttrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Описение типа атрибута для указанного типа объекта, или null</returns>
      public static IMSAttribute4RelationType GetAttribute4RelationType(
        int RelationTypeID,
        int AttrTypeID)
      {
        return MetaDataHelperService.Instance.GetAttribute4RelationType(RelationTypeID, AttrTypeID);
      }

      /// <summary>
      /// Получить список типов объектов, на которые может ссылаться указанный тип атрибута
      /// </summary>
      /// <param name="attrID">Идентификатор типа атрибута</param>
      /// <returns>Список типов объектов, на которые может ссылаться указанный тип атрибута.
      /// Пустой список - допускается ссылка на любой тип объектов,
      /// null - атрибут не является ссылочным</returns>
      public static List<int> GetLinkedObjectTypes(int attrID)
      {
        return MetaDataHelperService.Instance.GetLinkedObjectTypes(attrID);
      }

      /// <summary>
      /// Получить список типов атрибутов, которые могут ссылаться на указанный тип объекта
      /// </summary>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <returns>Список типов атрибутов, которые могут ссылаться на указанный тип объекта</returns>
      public static List<int> GetLinkAttributeTypes(int objTypeID)
      {
        return MetaDataHelperService.Instance.GetLinkAttributeTypes(objTypeID);
      }

      /// <summary>
      /// Получить по Guid группы атрибутов её Int32-идентификатор
      /// </summary>
      /// <param name="attrGroupGuid">Guid типа атрибута</param>
      /// <returns>Идентификатор группы атрибутов. -1 - группа атрибутов не найдена</returns>
      public static int GetAttributeGroupID(Guid attrGroupGuid)
      {
        return MetaDataHelperService.Instance.GetAttributeGroupID(attrGroupGuid);
      }

      /// <summary>
      /// Получить по Int32-идентификатору группы атрибутов её Guid-идентификатор
      /// </summary>
      /// <param name="attrGroupID">Идентификатор типа атрибута</param>
      /// <returns>Идентификатор группы атрибутов. Guid.Empty - группа атрибутов не найдена</returns>
      public static Guid GetAttributeGroupGuid(int attrGroupID)
      {
        return MetaDataHelperService.Instance.GetAttributeGroupGuid(attrGroupID);
      }

      /// <summary>
      /// Возвращает идентификатор группы атрибутов по строковому представлению её глобального идентификатора
      /// </summary>
      /// <param name="Guid">Guid группы атрибутов в виде строки</param>
      public static int GetAttributeGroupID(string Guid)
      {
        return MetaDataHelperService.Instance.GetAttributeGroupID(Guid);
      }

      /// <summary>Получить по Guid группы атрибутов описание группы</summary>
      /// <param name="attrGroupGuid">Guid типа группы атрибутов</param>
      /// <returns>Описание группы атрибутов или null</returns>
      public static IMSAttributeGroup GetAttributeGroup(Guid attrGroupGuid)
      {
        return MetaDataHelperService.Instance.GetAttributeGroup(attrGroupGuid);
      }

      /// <summary>
      /// Получить по строковому Guid группы атрибутов описание группы
      /// </summary>
      /// <param name="Guid">Guid типа группы атрибутов в виде строки</param>
      /// <returns>Описание группы атрибутов или null</returns>
      public static IMSAttributeGroup GetAttributeGroup(string Guid)
      {
        return MetaDataHelperService.Instance.GetAttributeGroup(Guid);
      }

      /// <summary>Получить по ID группы атрибутов описание группы</summary>
      /// <param name="attrGroupID">ID типа группы атрибутов</param>
      /// <returns>Описание группы атрибутов или null</returns>
      public static IMSAttributeGroup GetAttributeGroup(int attrGroupID)
      {
        return MetaDataHelperService.Instance.GetAttributeGroup(attrGroupID);
      }

      /// <summary>
      /// Получить список типов атрибутов для указанной группы атрибутов
      /// </summary>
      /// <param name="guid">Guid группы атрибутов</param>
      /// <returns>Список типов атрибутов для указанной группы атрибутов</returns>
      public static List<int> GetAttributesInGroup(Guid guid)
      {
        return MetaDataHelperService.Instance.GetAttributesInGroup(guid);
      }

      /// <summary>
      /// Получить список типов атрибутов для указанной группы атрибутов
      /// </summary>
      /// <param name="groupID">Идентификатор группы атрибутов: -1 для группы "Все атрибуты", -10 для группы "Назначенные типам" (собираются списки всех id атрибутов, которые назначены типам объектов и типам связей)</param>
      /// <returns>Список типов атрибутов для указанной группы атрибутов</returns>
      public static List<int> GetAttributesInGroup(int groupID)
      {
        return MetaDataHelperService.Instance.GetAttributesInGroup(groupID);
      }

      /// <summary>
      /// Получить список Guid типов атрибутов для указанной группы атрибутов
      /// </summary>
      /// <param name="guid">Guid группы атрибутов</param>
      /// <returns>Список Guid типов атрибутов для указанной группы атрибутов</returns>
      public static List<Guid> GetAttributesInGroupGuids(Guid guid)
      {
        return MetaDataHelperService.Instance.GetAttributesInGroupGuids(guid);
      }

      /// <summary>
      /// Получить список Guid типов атрибутов для указанной группы атрибутов
      /// </summary>
      /// <param name="groupID">Идентификатор группы атрибутов</param>
      /// <returns>Список Guid типов атрибутов для указанной группы атрибутов</returns>
      public static List<Guid> GetAttributesInGroupGuids(int groupID)
      {
        return MetaDataHelperService.Instance.GetAttributesInGroupGuids(groupID);
      }

      /// <summary>
      /// Получить информацию о том, где применяется указанный тип атрибута
      /// </summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Применяемость указанного типа атрибута</returns>
      public static IMSAttributeTypeApplicability GetAttributeTypeApplicability(int attrTypeID)
      {
        return MetaDataHelperService.Instance.GetAttributeTypeApplicability(attrTypeID);
      }

      /// <summary>
      /// Получить информацию о том, где применяется указанный тип атрибута
      /// </summary>
      /// <param name="attrTypeGuid">Уникальный глобальный идентификатор типа атрибута</param>
      /// <returns>Применяемость указанного типа атрибута</returns>
      public static IMSAttributeTypeApplicability GetAttributeTypeApplicability(Guid attrTypeGuid)
      {
        return MetaDataHelperService.Instance.GetAttributeTypeApplicability(attrTypeGuid);
      }

      /// <summary>
      /// Проверить, применяется ли указанный тип атрибута в типах объектов/связей
      /// </summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>true - указанный тип атрибута применяется в типах объектов/связей</returns>
      public static bool IsAttributeInUse(int attrTypeID)
      {
        return MetaDataHelperService.Instance.IsAttributeInUse(attrTypeID);
      }

      /// <summary>
      /// Проверить, применяется ли указанный тип атрибута в типах объектов/связей
      /// </summary>
      /// <param name="attrTypeGuid">Уникальный глобальный идентификатор типа атрибута</param>
      /// <returns>true - указанный тип атрибута применяется в типах объектов/связей</returns>
      public static bool IsAttributeInUse(Guid attrTypeGuid)
      {
        return MetaDataHelperService.Instance.IsAttributeInUse(attrTypeGuid);
      }

      /// <summary>
      /// Получить список идентификаторов типов атрибутов, которые применяются в типах
      /// объектов/связей. Список отсортирован по идентификатору типа атрибута
      /// </summary>
      /// <returns>Список идентификаторов типов атрибутов, которые применяются в типах объектов/связей</returns>
      public static List<int> GetUsedUnsortedAttributesIDs()
      {
        return MetaDataHelperService.Instance.GetUsedUnsortedAttributesIDs();
      }

      /// <summary>
      /// Получить список идентификаторов типов атрибутов, которые применяются в типах
      /// объектов/связей. Список отсортирован по названию типа атрибута
      /// </summary>
      /// <returns>Список идентификаторов типов атрибутов, которые применяются в типах объектов/связей</returns>
      public static List<int> GetUsedSortedAttributesIDs()
      {
        return MetaDataHelperService.Instance.GetUsedSortedAttributesIDs();
      }

      /// <summary>
      /// Получить список описаний типов атрибутов, которые применяются в типах
      /// объектов/связей. Список отсортирован по названию типа атрибута
      /// </summary>
      /// <returns>Список описаний типов атрибутов, которые применяются в типах объектов/связей</returns>
      public static List<IMSAttributeType> GetUsedSortedAttributes()
      {
        return MetaDataHelperService.Instance.GetUsedSortedAttributes();
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанной схеме ЖЦ
      /// </summary>
      /// <param name="schemaID">Идентификатор схемы ЖЦ</param>
      /// <returns>true, если указанная схема ЖЦ существует</returns>
      public static bool ExistsLCSchema(int schemaID)
      {
        return MetaDataHelperService.Instance.ExistsLCSchema(schemaID);
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанной схеме ЖЦ
      /// </summary>
      /// <param name="schemaGuid">Guid схемы ЖЦ</param>
      /// <returns>true, если указанная схема ЖЦ существует</returns>
      public static bool ExistsLCSchema(Guid schemaGuid)
      {
        return MetaDataHelperService.Instance.ExistsLCSchema(schemaGuid);
      }

      /// <summary>Получить краткую информацию о схеме ЖЦ</summary>
      /// <param name="schemaID">Идентификатор схемы ЖЦ</param>
      /// <returns>Краткая информация о схеме ЖЦ или null</returns>
      public static IMSLifeCycleScheme GetLCSchema(int schemaID)
      {
        return MetaDataHelperService.Instance.GetLCSchema(schemaID);
      }

      /// <summary>Получить краткую информацию о схеме ЖЦ</summary>
      /// <param name="schemaGuid">Guid схемы ЖЦ</param>
      /// <returns>Краткая информация о схеме ЖЦ или null</returns>
      public static IMSLifeCycleScheme GetLCSchema(Guid schemaGuid)
      {
        return MetaDataHelperService.Instance.GetLCSchema(schemaGuid);
      }

      /// <summary>Получить название схемы ЖЦ</summary>
      /// <param name="schemaID">Идентификатор схемы ЖЦ</param>
      /// <returns>Название схемы ЖЦ</returns>
      public static string GetLCSchemaName(int schemaID)
      {
        return MetaDataHelperService.Instance.GetLCSchemaName(schemaID);
      }

      /// <summary>Получить название схемы ЖЦ</summary>
      /// <param name="schemaGuid">Guid схемы ЖЦ</param>
      /// <returns>Название схемы ЖЦ</returns>
      public static string GetLCSchemaName(Guid schemaGuid)
      {
        return MetaDataHelperService.Instance.GetLCSchemaName(schemaGuid);
      }

      /// <summary>Получить по Guid схемы ЖЦ её Int32-идентификатор</summary>
      /// <param name="schemaGuid">Guid схемы ЖЦ</param>
      /// <returns>Идентификатор схемы ЖЦ. -1 - схема не найдена</returns>
      public static int GetLCSchemaID(Guid schemaGuid)
      {
        return MetaDataHelperService.Instance.GetLCSchemaID(schemaGuid);
      }

      /// <summary>
      /// Получить по Int32-идентификатору схемы ЖЦ её Guid-идентификатор
      /// </summary>
      /// <param name="schemaID">Идентификатор схемы ЖЦ</param>
      /// <returns>Идентификатор схемы ЖЦ. Guid.Empty - схема ЖЦ не найдена</returns>
      public static Guid GetLCSchemaGuid(int schemaID)
      {
        return MetaDataHelperService.Instance.GetLCSchemaGuid(schemaID);
      }

      /// <summary>
      /// Возвращает идентификатор схемы ЖЦ по строковому представлению её глобального идентификатора
      /// </summary>
      /// <param name="Guid">Guid схемы ЖЦ в виде строки</param>
      public static int GetLCSchemaID(string Guid)
      {
        return MetaDataHelperService.Instance.GetLCSchemaID(Guid);
      }

      /// <summary>Получить список описаний всех схем ЖЦ</summary>
      /// <returns>Список описаний всех схем ЖЦ</returns>
      public static List<IMSLifeCycleScheme> GetLCSchemesList()
      {
        return MetaDataHelperService.Instance.GetLCSchemesList();
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном уровне продвижения
      /// </summary>
      /// <param name="levelID">Идентификатор уровня продвижения</param>
      /// <returns>true, если указанный уровень продвижения существует</returns>
      public static bool ExistsLCLevel(int levelID)
      {
        return MetaDataHelperService.Instance.ExistsLCLevel(levelID);
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном уровне продвижения
      /// </summary>
      /// <param name="levelGuid">Guid уровня продвижения</param>
      /// <returns>true, если  указанный уровень продвижения существует</returns>
      public static bool ExistsLCLevel(Guid levelGuid)
      {
        return MetaDataHelperService.Instance.ExistsLCLevel(levelGuid);
      }

      /// <summary>Получить краткую информацию об уровне продвижения</summary>
      /// <param name="levelID">Идентификатор уровня продвижения</param>
      /// <returns>Краткая информация об уровне провижения или null</returns>
      public static IMSLifeCycleLevel GetLCLevel(int levelID)
      {
        return MetaDataHelperService.Instance.GetLCLevel(levelID);
      }

      /// <summary>Получить краткую информацию об уровне продвижения</summary>
      /// <param name="levelGuid">Guid уровня продвижения</param>
      /// <returns>Краткая информация об уровне продвижения или null</returns>
      public static IMSLifeCycleLevel GetLCLevel(Guid levelGuid)
      {
        return MetaDataHelperService.Instance.GetLCLevel(levelGuid);
      }

      /// <summary>Получить название уровня продвижения</summary>
      /// <param name="levelID">Идентификатор уровня продвижения</param>
      /// <returns>Название уровня продвижения</returns>
      public static string GetLCLevelName(int levelID)
      {
        return MetaDataHelperService.Instance.GetLCLevelName(levelID);
      }

      /// <summary>Получить название уровня продвижения</summary>
      /// <param name="levelGuid">Guid уровня продвижения</param>
      /// <returns>Название уровня продвижения</returns>
      public static string GetLCLevelName(Guid levelGuid)
      {
        return MetaDataHelperService.Instance.GetLCLevelName(levelGuid);
      }

      /// <summary>
      /// Получить по Guid уровня продвижения его Int32-идентификатор
      /// </summary>
      /// <param name="levelGuid">Guid уровня продвижения</param>
      /// <returns>Идентификатор уровня продвижения. -1 - уровень продвижения не найден</returns>
      public static int GetLCLevelID(Guid levelGuid)
      {
        return MetaDataHelperService.Instance.GetLCLevelID(levelGuid);
      }

      /// <summary>
      /// Получить по Int32-идентификатору уровня продвижения его Guid-идентификатор
      /// </summary>
      /// <param name="levelID">Идентификатор уровня продвижения</param>
      /// <returns>Идентификатор уровня продвижения. Guid.Empty - уровень продвижения не найден</returns>
      public static Guid GetLCLevelGuid(int levelID)
      {
        return MetaDataHelperService.Instance.GetLCLevelGuid(levelID);
      }

      /// <summary>
      /// Возвращает идентификатор уровня продвижения по строковому представлению его глобального идентификатора
      /// </summary>
      /// <param name="Guid">Guid уровня продвижения в виде строки</param>
      public static int GetLCLevelID(string Guid) => MetaDataHelperService.Instance.GetLCLevelID(Guid);

      /// <summary>Получить список описаний всех уровней продвижения</summary>
      /// <returns>Список описаний всех уровней продвижения</returns>
      public static List<IMSLifeCycleLevel> GetLCLevelsList()
      {
        return MetaDataHelperService.Instance.GetLCLevelsList();
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном шаге ЖЦ
      /// </summary>
      /// <param name="lcstepID">Идентификатор шага ЖЦ</param>
      /// <returns>true, если указанный шаг ЖЦ существует</returns>
      public static bool ExistsLCStep(int lcstepID)
      {
        return MetaDataHelperService.Instance.ExistsLCStep(lcstepID);
      }

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном шаге ЖЦ
      /// </summary>
      /// <param name="lcstepGuid">Guid шага ЖЦ</param>
      /// <returns>true, если  указанный шаг ЖЦ существует</returns>
      public static bool ExistsLCStep(Guid lcstepGuid)
      {
        return MetaDataHelperService.Instance.ExistsLCStep(lcstepGuid);
      }

      /// <summary>Получить краткую информацию о шаге ЖЦ</summary>
      /// <param name="lcstepID">Идентификатор шага ЖЦ</param>
      /// <returns>Краткая информация о шаге ЖЦ или null</returns>
      public static IMSLifeCycleStep GetLCStep(int lcstepID)
      {
        return MetaDataHelperService.Instance.GetLCStep(lcstepID);
      }

      /// <summary>Получить краткую информацию о шаге ЖЦ</summary>
      /// <param name="lcstepGuid">Guid шага ЖЦ</param>
      /// <returns>Краткая информация о шаге ЖЦ или null</returns>
      public static IMSLifeCycleStep GetLCStep(Guid lcstepGuid)
      {
        return MetaDataHelperService.Instance.GetLCStep(lcstepGuid);
      }

      /// <summary>Получить название шага ЖЦ</summary>
      /// <param name="lcstepID">Идентификатор шага ЖЦ</param>
      /// <returns>Название шага ЖЦ</returns>
      public static string GetLCStepName(int lcstepID)
      {
        return MetaDataHelperService.Instance.GetLCStepName(lcstepID);
      }

      /// <summary>Получить название шага ЖЦ</summary>
      /// <param name="lcstepGuid">Guid шага ЖЦ</param>
      /// <returns>Название шага ЖЦ</returns>
      public static string GetLCStepName(Guid lcstepGuid)
      {
        return MetaDataHelperService.Instance.GetLCStepName(lcstepGuid);
      }

      /// <summary>Получить по Guid шага ЖЦ его Int32-идентификатор</summary>
      /// <param name="lcstepGuid">Guid шага ЖЦ</param>
      /// <returns>Идентификатор шага ЖЦ. -1 - шаг ЖЦ не найден</returns>
      public static int GetLCStepID(Guid lcstepGuid)
      {
        return MetaDataHelperService.Instance.GetLCStepID(lcstepGuid);
      }

      /// <summary>
      /// Получить по Int32-идентификатору шага ЖЦ его Guid-идентификатор
      /// </summary>
      /// <param name="lcstepID">Идентификатор шага ЖЦ</param>
      /// <returns>Идентификатор шага ЖЦ. Guid.Empty - шаг ЖЦ не найден</returns>
      public static Guid GetLCStepGuid(int lcstepID)
      {
        return MetaDataHelperService.Instance.GetLCStepGuid(lcstepID);
      }

      /// <summary>
      /// Возвращает идентификатор шага ЖЦ по строковому представлению его глобального идентификатора
      /// </summary>
      /// <param name="Guid">Guid шага ЖЦ в виде строки</param>
      public static int GetLCStepID(string Guid) => MetaDataHelperService.Instance.GetLCStepID(Guid);

      /// <summary>Получить список описаний всех шагов ЖЦ</summary>
      /// <returns>Список описаний всех шагов ЖЦ</returns>
      public static List<IMSLifeCycleStep> GetLCStepsList()
      {
        return MetaDataHelperService.Instance.GetLCStepsList();
      }

      /// <summary>
      /// Получить по Guid какого-то элемента метаданных его тип
      /// </summary>
      /// <param name="guid">Guid какого-то элемента метаданных</param>
      /// <returns>Тип метаданных для указанного элемента</returns>
      public static IMSGlobals GetGlobalsByGuid(Guid guid)
      {
        return MetaDataHelperService.Instance.GetGlobalsByGuid(guid);
      }

      /// <summary>
      /// Получить по Guid какого-то элемента метаданных его описание
      /// </summary>
      /// <param name="guid">Guid какого-то элемента метаданных</param>
      /// <returns>Описание метаданных для указанного элемента</returns>
      public static IDisplayable GetDisplayableByGuid(Guid guid)
      {
        return MetaDataHelperService.Instance.GetDisplayableByGuid(guid);
      }
    }
}
