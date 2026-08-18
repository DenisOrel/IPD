
// Type: Intermech.Interfaces.IMetaDataHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Contexts;
using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    public interface IMetaDataHelper
    {
      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном типе объекта
      /// </summary>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <returns>true, если тип объекта существует</returns>
      bool ExistsObjectType(int objTypeID);

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном типе объекта
      /// </summary>
      /// <param name="objTypeGuid">Guid типа объекта</param>
      /// <returns>true, если тип объекта существует</returns>
      bool ExistsObjectType(Guid objTypeGuid);

      /// <summary>Получить краткую информацию о типе объекта</summary>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <returns>Краткая информация о типе объекта или null</returns>
      IMSObjectType GetObjectType(int objTypeID);

      /// <summary>Получить краткую информацию о типе объекта</summary>
      /// <param name="objTypeGuid">Идентификатор типа объекта</param>
      /// <returns>Краткая информация о типе объекта или null</returns>
      IMSObjectType GetObjectType(Guid objTypeGuid);

      /// <summary>Получить название типа объектов (например, "Детали")</summary>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <returns>Название типа объектов (например, "Детали")</returns>
      string GetObjectTypeName(int objTypeID);

      /// <summary>Получить название типа объектов (например, "Детали")</summary>
      /// <param name="objTypeGuid">Идентификатор типа объекта</param>
      /// <returns>Название типа объектов (например, "Детали")</returns>
      string GetObjectTypeName(Guid objTypeGuid);

      /// <summary>
      /// Получить полное название типа объектов (например, "Изделия\Детали")
      /// </summary>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <returns>Полное название типа объектов (например, "Изделия\Детали")</returns>
      string GetObjectTypeFullName(int objTypeID);

      /// <summary>
      /// Получить название экземпляра типа объектов (например, "Деталь")
      /// </summary>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <returns>Название экземпляра типа объектов (например, "Деталь")</returns>
      string GetObjectName(int objTypeID);

      /// <summary>
      /// Получить название экземпляра типа объектов (например, "Деталь")
      /// </summary>
      /// <param name="objTypeGuid">Идентификатор типа объекта</param>
      /// <returns>Название экземпляра типа объектов (например, "Деталь")</returns>
      string GetObjectName(Guid objTypeGuid);

      /// <summary>Получить по Guid типа объекта его Int32-идентификатор</summary>
      /// <param name="objTypeGuid">Guid типа объекта</param>
      /// <returns>Идентификатор типа объекта. -1 - тип объекта не найден</returns>
      int GetObjectTypeID(Guid objTypeGuid);

      /// <summary>
      /// Получить по Int32-идентификатору типа объекта его Guid-идентификатор
      /// </summary>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <returns>Идентификатор типа объекта. Guid.Empty - тип объекта не найден</returns>
      Guid GetObjectTypeGuid(int objTypeID);

      /// <summary>
      /// Возвращает идентификатор типа объектов по строковому представлению его глобального идентификатора
      /// </summary>
      /// <param name="Guid">Guid типа объекта в виде строки</param>
      int GetObjectTypeID(string Guid);

      /// <summary>Получить по Guid типа связи его Int32-идентификатор</summary>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>Идентификатор типа связи. -1 - тип связи не найден</returns>
      int GetRelationTypeID(Guid relTypeGuid);

      /// <summary>
      /// Получить по Int32-идентификатору типа связи её Guid-идентификатор
      /// </summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Идентификатор типа связи. Guid.Empty - тип связи не найден</returns>
      Guid GetRelationTypeGuid(int relTypeID);

      /// <summary>
      /// Возвращает идентификатор типа связи по строковому представлению её глобального идентификатора
      /// </summary>
      /// <param name="Guid">Guid типа связи в виде строки</param>
      int GetRelationTypeID(string Guid);

      /// <summary>
      /// Получить Guid родительского типа объектов для указанного дочернего типа объекта
      /// </summary>
      /// <param name="childTypeGuid">Guid дочернего типа объекта</param>
      /// <returns>Guid родительского типа объектов для указанного дочернего типа объекта или Guid.Empty</returns>
      Guid GetObjectTypeParentID(Guid childTypeGuid);

      /// <summary>
      /// Получить ID родительского типа объектов для указанного дочернего типа объекта
      /// </summary>
      /// <param name="childTypeID">ID дочернего типа объекта</param>
      /// <returns>ID родительского типа объектов для указанного дочернего типа объекта или -1</returns>
      int GetObjectTypeParentID(int childTypeID);

      /// <summary>
      /// Получить список ID всех родительских объектов для указанного дочернего типа объекта
      /// </summary>
      /// <param name="childTypeGuid">Guid дочернего типа объекта</param>
      /// <returns>Список ID всех родительских объектов для указанного дочернего типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      List<int> GetObjectTypeParentsID(Guid childTypeGuid);

      /// <summary>
      /// Получить список Guid всех родительских типов объектов для указанного дочернего типа объекта
      /// </summary>
      /// <param name="childTypeID">ID дочернего типа объекта</param>
      /// <returns>Список Guid всех родительских типов объектов для указанного дочернего типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      List<Guid> GetObjectTypeParentsGuid(int childTypeID);

      /// <summary>
      /// Получить список ID всех родительских объектов для указанного дочернего типа объекта
      /// </summary>
      /// <param name="childTypeID">ID дочернего типа объекта</param>
      /// <returns>Список ID всех родительских объектов для указанного дочернего типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      List<int> GetObjectTypeParentsID(int childTypeID);

      /// <summary>
      /// Получить список ID всех родительских объектов для указанного дочернего типа объекта.
      /// Родительские объекты следуют в списке в порядке от самого верхнего типа объекта к дочерним.
      /// </summary>
      /// <param name="childTypeID">ID дочернего типа объекта</param>
      /// <returns>Список ID всех родительских объектов для указанного дочернего типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      List<int> GetObjectTypeParentsIDReverse(int childTypeID);

      /// <summary>
      /// Получить список Guid всех родительских типов объектов для указанного дочернего типа объекта
      /// </summary>
      /// <param name="childTypeGuid">Guid дочернего типа объекта</param>
      /// <returns>Список Guid всех родительских типов объектов для указанного дочернего типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      List<Guid> GetObjectTypeParentsGuid(Guid childTypeGuid);

      /// <summary>
      /// Проверить, является ли тип объекта parentType родительским типом для типа объекта childType
      /// </summary>
      /// <param name="childType">Проверяемый дочерний тип объекта</param>
      /// <param name="parentType">Проверяемый родительский тип объекта (он может быть в любом месте родительской иерархии)</param>
      /// <returns>true, если parentType является родительским типом для childType</returns>
      bool IsObjectTypeChildOf(Guid childType, Guid parentType);

      /// <summary>
      /// Определить уровень вложенности указанного типа объектов в иерархии. Значение 0 - типы объектов верхнего уровня
      /// </summary>
      /// <param name="objectTypeID">Идентификатор типа объекта</param>
      /// <returns>-1 - тип объекта не найден, 0 - тип верхнего уровня, больше нуля - уровень вложенности в иерархии</returns>
      int GetObjectTypeLevel(int objectTypeID);

      /// <summary>
      /// Проверить, является ли тип объекта parentType родительским типом для типа объекта childType
      /// </summary>
      /// <param name="childType">Проверяемый дочерний тип объекта</param>
      /// <param name="parentType">Проверяемый родительский тип объекта (он может быть в любом месте родительской иерархии)</param>
      /// <returns>true, если parentType является родительским типом для childType</returns>
      bool IsObjectTypeChildOf(int childType, int parentType);

      /// <summary>
      /// Получить список ID всех дочерних типов объектов для указанного родительского типа объекта
      /// </summary>
      /// <param name="parentTypeGuid">Guid родительского типа объекта</param>
      /// <returns>Список ID всех дочерних типов объектов для указанного родительского типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      List<int> GetObjectTypeChildrenID(Guid parentTypeGuid);

      /// <summary>
      /// Получить список Guid всех дочерних типов объектов для указанного родительского типа объекта
      /// </summary>
      /// <param name="parentTypeID">ID родительского типа объекта</param>
      /// <returns>Список Guid всех дочерних типов объектов для указанного родительского типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      List<Guid> GetObjectTypeChildrenGuid(int parentTypeID);

      /// <summary>
      /// Получить список ID всех дочерних типов объектов для указанного родительского типа объекта
      /// </summary>
      /// <param name="parentTypeID">ID родительского типа объекта</param>
      /// <returns>Список ID всех дочерних объектов для указанного родительского типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      List<int> GetObjectTypeChildrenID(int parentTypeID);

      /// <summary>
      /// Получить список Guid всех дочерних типов объектов для указанного родительского типа объекта
      /// </summary>
      /// <param name="parentTypeGuid">Guid родительского типа объекта</param>
      /// <returns>Список Guid всех дочерних типов объектов для указанного родительского типа объекта.
      /// В любом случае будет возвращено не пустое значение</returns>
      List<Guid> GetObjectTypeChildrenGuid(Guid parentTypeGuid);

      /// <summary>
      /// Получить рекурсивно список ID всех дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов).
      /// Добавляется также и parentTypeID.
      /// </summary>
      /// <param name="parentTypeID">ID родительского типа объекта</param>
      /// <returns>Список ID всех дочерних объектов для указанного родительского типа объекта (включая унаследованные типы объектов).
      /// В любом случае будет возвращено не пустое значение</returns>
      List<int> GetObjectTypeChildrenIDRecursive(int parentTypeID);

      /// <summary>
      /// Получить рекурсивно список ID всех локальных дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов).
      /// Добавляется также и parentTypeID, даже если он не является локальным типом (в начало списка).
      /// </summary>
      /// <param name="parentTypeID">ID родительского типа объекта</param>
      /// <returns>Список ID всех дочерних локальных типов объектов для указанного родительского типа объекта
      /// В любом случае будет возвращено не пустое значение</returns>
      List<int> GetLocalObjectTypeChildrenIDRecursive(int parentTypeID);

      /// <summary>
      /// Получить рекурсивно список ID всех дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов)
      /// Добавляется также и parentTypeID.
      /// </summary>
      /// <param name="parentTypeIDs">Список Int32-идентификаторов родительских типов объектов</param>
      /// <returns>Список ID всех дочерних объектов для указанных родительских типов объектов (включая унаследованные типы объектов).
      /// В любом случае будет возвращено не пустое значение</returns>
      List<int> GetObjectTypeChildrenIDRecursive(IEnumerable<int> parentTypeIDs);

      /// <summary>
      /// Получить рекурсивно список ID всех локальных дочерних типов объектов для указанных родительских типов объектов
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов)
      /// Добавляются также и parentTypeIDs.
      /// </summary>
      /// <param name="parentTypeIDs">Список Int32-идентификаторов родительских типов объектов</param>
      /// <returns>Список ID всех дочерних локальных типов объектов для указанных родительских типов объектов.
      /// В любом случае будет возвращено не пустое значение</returns>
      List<int> GetLocalObjectTypeChildrenIDRecursive(IEnumerable<int> parentTypeIDs);

      /// <summary>
      /// Получить рекурсивно список ID всех дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов)
      /// Добавляется также и parentTypeID.
      /// </summary>
      /// <param name="parentTypeGuid">Guid родительского типа объекта</param>
      /// <returns>Список ID всех дочерних объектов для указанного родительского типа объекта (включая унаследованные типы объектов).
      /// В любом случае будет возвращено не пустое значение</returns>
      List<int> GetObjectTypeChildrenIDRecursive(Guid parentTypeGuid);

      /// <summary>
      /// Получить рекурсивно список Guid всех дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов)
      /// </summary>
      /// <param name="parentTypeGuid">Guid родительского типа объекта</param>
      /// <returns>Список Guid всех дочерних типов объектов для указанного родительского типа объекта (включая унаследованные типы объектов).
      /// В любом случае будет возвращено не пустое значение</returns>
      List<Guid> GetObjectTypeChildrenGuidRecursive(Guid parentTypeGuid);

      /// <summary>
      /// Получить рекурсивно список Guid всех дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов)
      /// </summary>
      /// <param name="parentTypeGuids">Список Guid идентификаторов родительских типов объектов</param>
      /// <returns>Список Guid всех дочерних объектов для указанных родительских типов объектов (включая унаследованные типы объектов).
      /// В любом случае будет возвращено не пустое значение</returns>
      List<Guid> GetObjectTypeChildrenGuidRecursive(IEnumerable<Guid> parentTypeGuids);

      /// <summary>
      /// Получить рекурсивно список Guid всех дочерних типов объектов для указанного родительского типа объекта
      /// (иерархия полностью "раскручивается" вниз по дереву типов объектов)
      /// </summary>
      /// <param name="parentTypeID">Int32-идентификатор родительского типа объекта</param>
      /// <returns>Список Guid всех дочерних типов объектов для указанного родительского типа объекта (включая унаследованные типы объектов).
      /// В любом случае будет возвращено не пустое значение</returns>
      List<Guid> GetObjectTypeChildrenGuidRecursive(int parentTypeID);

      /// <summary>
      /// Метод получает на вход список допустимых типов объектов. Затем он "раскручивает" их родительские
      /// типы объектов (вверх по иерархии) до абстрактных родительских типов, а затем готовит список
      /// верхних допустимых родительских типов объектов. Метод можно использовать для подготовки списка
      /// типов объектов для окна по выбору объектов из списка допустимых типов, например, в команде
      /// "Добавить в состав"
      /// </summary>
      /// <param name="typeList">Список допустимых типов объектов</param>
      /// <returns>Список допустимых типов объектов верхнего уровня</returns>
      List<Guid> GetTopParentEnabledObjectTypesGuid(IEnumerable<Guid> typeList);

      /// <summary>
      /// Метод получает на вход список допустимых типов объектов. Затем он "раскручивает" их родительские
      /// типы объектов (вверх по иерархии) до абстрактных родительских типов, а затем готовит список
      /// верхних допустимых родительских типов объектов. Метод можно использовать для подготовки списка
      /// типов объектов для окна по выбору объектов из списка допустимых типов, например, в команде
      /// "Добавить в состав"
      /// </summary>
      /// <param name="typeList">Список допустимых типов объектов</param>
      /// <returns>Список допустимых типов объектов верхнего уровня</returns>
      List<int> GetTopParentEnabledObjectTypes(IEnumerable<int> typeList);

      /// <summary>Получить список типов объектов верхнего уровня</summary>
      /// <returns>Список типов объектов верхнего уровня</returns>
      List<int> GetTopObjectTypesIDs();

      /// <summary>Получить список типов объектов верхнего уровня</summary>
      /// <returns>Список типов объектов верхнего уровня</returns>
      List<Guid> GetTopObjectTypesGuids();

      /// <summary>
      /// Вернуть нелокальный или абстрактный родительский тип для указанного дочернего типа.
      /// Если дочерний тип является локальным, либо абстрактным, либо типом верхнего уровня,
      /// возвращается он сам. Используется для оптимизации запросов
      /// в коллекции объектов и связей.
      /// </summary>
      /// <param name="childType">Дочерний тип объекта, для которого надо найти родительский тип объекта</param>
      /// <returns>Нелокальный или абстрактный родительский тип для указанного дочернего типа</returns>
      int GetTopParentObjectTypeID(int childType);

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
      int GetCommonParentObjectTypeID(int childType1, int childType2);

      /// <summary>Попытаться отыскать общий родительский тип для указанных типов.
      /// Если общий тип найти нельзя, возвращается Intermech.Consts.UnknownObjectTypeId</summary>
      /// <param name="objectTypes">Перечисление идентификаторов типов объектов</param>
      /// <returns>Общий указанных типов. Если общий тип найти нельзя, возвращается Intermech.Consts.UnknownObjectTypeId</returns>
      int GetCommonParentObjectTypeID(IEnumerable<int> objectTypes);

      /// <summary>Попытаться отыскать общий родительский тип для указанных объектов.
      /// Если общий тип найти нельзя, возвращается Intermech.Consts.UnknownObjectTypeId</summary>
      /// <param name="objectVersionIDs">Перечисление идентификаторов версий объектов</param>
      /// <returns>Общий указанных типов. Если общий тип найти нельзя, возвращается Intermech.Consts.UnknownObjectTypeId</returns>
      int GetCommonParentObjectTypeID(IEnumerable<long> objectVersionIDs);

      /// <summary>
      /// Оптимизировать список (удалить вложенные нелокальные дочерние типы объектов, если в списке есть их родительские типы
      /// </summary>
      /// <param name="childObjectTypes">Список дочерних типов объектов для типизированного запроса в коллекцию связей</param>
      /// <returns>Оптимизированный список типов дочерних объектов</returns>
      List<int> OptimizeChildObjectTypes(IEnumerable<int> childObjectTypes);

      /// <summary>
      /// Получить Int32-идентификатор типа связи по умолчанию для указанного родительского типа объектов
      /// </summary>
      /// <param name="parentObjectType">Идентификатор родительского типа объектов</param>
      /// <returns>Int32-идентификатор типа связи по умолчанию для указанного родительского типа объектов.
      /// Вернет -1 - если тип объекта или связи не найден</returns>
      int GetDefaultRelationTypeID(int parentObjectType);

      /// <summary>
      /// Получить Guid типа связи по умолчанию для указанного родительского типа объектов
      /// </summary>
      /// <param name="parentObjectType">Идентификатор родительского типа объектов</param>
      /// <returns>Guid типа связи по умолчанию для указанного родительского типа объектов.
      /// Вернет Guid.Empty - если тип объекта или связи не найден</returns>
      Guid GetDefaultRelationTypeGuid(int parentObjectType);

      /// <summary>
      /// Получить Int32-идентификатор типа связи по умолчанию для указанного родительского типа объектов
      /// </summary>
      /// <param name="parentObjectTypeGuid">Guid родительского типа объектов</param>
      /// <returns>Int32-идентификатор типа связи по умолчанию для указанного родительского типа объектов.
      /// Вернет -1 - если тип объекта или связи не найден</returns>
      int GetDefaultRelationTypeID(Guid parentObjectTypeGuid);

      /// <summary>
      /// Получить Guid типа связи по умолчанию для указанного родительского типа объектов
      /// </summary>
      /// <param name="parentObjectTypeGuid">Guid родительского типа объектов</param>
      /// <returns>Guid типа связи по умолчанию для указанного родительского типа объектов.
      /// Вернет Guid.Empty - если тип объекта или связи не найден</returns>
      Guid GetDefaultRelationTypeGuid(Guid parentObjectTypeGuid);

      /// <summary>Получить список описаний всех типов объектов</summary>
      /// <returns>Список описаний всех типов объектов</returns>
      List<IMSObjectType> GetObjectTypesList();

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном типе связи
      /// </summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>true, если тип связи существует</returns>
      bool ExistsRelationType(int relTypeID);

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном типе связи
      /// </summary>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>true, если тип связи существует</returns>
      bool ExistsRelationType(Guid relTypeGuid);

      /// <summary>Получить краткую информацию о типе связи</summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Краткая информация о типе связи или null</returns>
      IMSRelationType GetRelationType(int relTypeID);

      /// <summary>Получить краткую информацию о типе связи</summary>
      /// <param name="relTypeGuid">Идентификатор типа связи</param>
      /// <returns>Краткая информация о типе связи или null</returns>
      IMSRelationType GetRelationType(Guid relTypeGuid);

      /// <summary>
      /// Получить название типа связи (например, "Проектная связь")
      /// </summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Название типа связи (например, "")</returns>
      string GetRelationTypeName(int relTypeID);

      /// <summary>
      /// Получить название типа связи (например, "Проектная связь")
      /// </summary>
      /// <param name="relTypeGuid">Идентификатор типа связи</param>
      /// <returns>Название типа связи (например, "Проектная связь")</returns>
      string GetRelationTypeName(Guid relTypeGuid);

      /// <summary>
      /// Получить список всех типов объектов имеющих допустимые типы связей
      /// </summary>
      /// <returns>Список всех типов объектов имеющих допустимые типы связей </returns>
      List<int> GetObjectTypesWithApplicabilities();

      /// <summary>
      /// Получить список всех дочерних типов объектов имеющих допустимые типы связей с родительскими типами
      /// </summary>
      /// <returns></returns>
      List<int> GetObjectTypesWithEnterInApplicabilities();

      /// <summary>
      /// Получить список допустимых типов связей для указанного родительского типа объектов
      /// </summary>
      /// <param name="objTypeID">Идентификатор родительского типа объектов</param>
      /// <returns>Список допустимых типов связей для указанного родительского типа объектов или null</returns>
      List<IMSApplicability> GetObjectTypeApplicabilities(int objTypeID);

      /// <summary>
      /// Проверить, может ли указанный дочерний тип объекта входить хотя бы
      /// в один родительский тип хотя бы одним типом связи
      /// </summary>
      /// <param name="partTypeID">id дочернего типа объекта</param>
      /// <returns>true - объект может входить в состав родительского, false - объект не может входить в состав родительского</returns>
      bool CanEntersIn(int partTypeID);

      /// <summary>
      /// Получить список допустимых типов связей для указанного родительского типа объектов
      /// </summary>
      /// <param name="objTypeGuid">Guid родительского типа объектов</param>
      /// <returns>Список допустимых типов связей для указанного родительского типа объектов или null</returns>
      List<IMSApplicability> GetObjectTypeApplicabilities(Guid objTypeGuid);

      /// <summary>
      /// Получить список допустимых типов связей для указанного дочернего типа объектов
      /// </summary>
      /// <param name="partTypeID">Идентификатор дочернего типа объекта</param>
      /// <returns>Список допустимых типов связей для указанного дочернего типа объекта или null</returns>
      List<IMSApplicability> GetObjectTypeParentApplicabilities(int partTypeId);

      /// <summary>
      /// Получить список идентификаторов допустимых типов связей для указанного родительского типа объектов
      /// </summary>
      /// <param name="objTypeID">Идентификатор родительского типа объектов</param>
      /// <returns>Список идентификаторов допустимых типов связей для указанного родительского типа объектов</returns>
      List<int> GetApplicabilityRelationTypesID(int objTypeID);

      /// <summary>
      /// Получить список идентификаторов допустимых типов связей для указанного родительского типа объектов
      /// </summary>
      /// <param name="objTypeGuid">Guid родительского типа объектов</param>
      /// <returns>Список идентификаторов допустимых типов связей для указанного родительского типа объектов</returns>
      List<int> GetApplicabilityRelationTypesID(Guid objTypeGuid);

      /// <summary>
      /// Получить список Guid допустимых типов связей для указанного родительского типа объектов
      /// </summary>
      /// <param name="objTypeID">Идентификатор родительского типа объектов</param>
      /// <returns>Список Guid допустимых типов связей для указанного родительского типа объектов</returns>
      List<Guid> GetApplicabilityRelationTypesGuids(int objTypeID);

      /// <summary>
      /// Получить список Guid допустимых типов связей для указанного родительского типа объектов
      /// </summary>
      /// <param name="objTypeGuid">Guid родительского типа объектов</param>
      /// <returns>Список Guid допустимых типов связей для указанного родительского типа объектов</returns>
      List<Guid> GetApplicabilityRelationTypesGuids(Guid objTypeGuid);

      /// <summary>
      /// Проверить, допустимо ли включить указанный дочерний тип объекта в указанный
      /// родительский тип объекта по указанному типу связи
      /// </summary>
      /// <param name="parObjTypeID">Родительский тип объекта</param>
      /// <param name="childObjTypeID">Дочерний тип объекта</param>
      /// <param name="relTypeID">Тип связи</param>
      /// <returns>true - такая связь допустима</returns>
      bool HasApplicability(int parObjTypeID, int childObjTypeID, int relTypeID);

      /// <summary>
      /// Проверить, может ли входить в состав указанного родительского типа объекта
      /// хотя бы один дочерний тип объектов хотя бы одним типом связи
      /// </summary>
      /// <param name="parObjTypeGuid">Guid родительского типа объекта</param>
      /// <returns>true - у объекта может быть состав, false - у объекта не может быть состав</returns>
      bool HasApplicability(Guid parObjTypeGuid);

      /// <summary>
      /// Проверить, может ли входить в состав указанного родительского типа объекта
      /// хотя бы один дочерний тип объектов хотя бы одним типом связи
      /// </summary>
      /// <param name="parObjTypeID">Идентификатор родительского типа объекта</param>
      /// <returns>true - у объекта может быть состав, false - у объекта не может быть состав</returns>
      bool HasApplicability(int parObjTypeID);

      /// <summary>
      /// Получить список описаний дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанному типу связи
      /// </summary>
      /// <param name="parObjTypeID">Идентификатор родительского типа объектов</param>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Список описаний дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      List<IMSObjectType> GetApplicabilityChildObjectTypes(int parObjTypeID, int relTypeID);

      /// <summary>
      /// Получить применяемость для указанного дочернего типа объектов в составе указанного
      /// родительского типа объектов по указанному типу связи
      /// Если для childObjTypeID применяемость не найдена, рекурсивно вверх искать применяемость для родительского
      /// </summary>
      /// <param name="parObjTypeID">Идентификатор родительского типа объектов</param>
      /// <param name="childObjTypeID">Идентификатор дочернего типа объектов</param>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Применяемость или null</returns>
      IMSApplicability GetApplicability(int parObjTypeID, int childObjTypeID, int relTypeID);

      /// <summary>
      /// Получить список описаний дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанному типу связи
      /// </summary>
      /// <param name="parObjTypeGuid">Guid родительского типа объектов</param>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>Список описаний дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      List<IMSObjectType> GetApplicabilityChildObjectTypes(Guid parObjTypeGuid, Guid relTypeGuid);

      /// <summary>
      /// Получить список идентификаторов дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанному типу связи
      /// </summary>
      /// <param name="parObjTypeID">Идентификатор родительского типа объектов</param>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Список идентификаторов дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      List<int> GetApplicabilityChildObjectTypesID(int parObjTypeID, int relTypeID);

      /// <summary>
      /// Получить список идентификаторов дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанным типам связей
      /// </summary>
      /// <param name="parObjTypeID">Идентификатор родительского типа объектов</param>
      /// <param name="relTypeIDs">Идентификаторы типов связей</param>
      /// <returns>Список идентификаторов дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      List<int> GetApplicabilityChildObjectTypesID(int parObjTypeID, IEnumerable<int> relTypeIDs);

      /// <summary>
      /// Получить список идентификаторов дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанному типу связи
      /// </summary>
      /// <param name="parObjTypeGuid">Guid родительского типа объектов</param>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>Список идентификаторов дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      List<int> GetApplicabilityChildObjectTypesID(Guid parObjTypeGuid, Guid relTypeGuid);

      /// <summary>
      /// Получить список идентификаторов дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанным типам связей
      /// </summary>
      /// <param name="parObjTypeGuid">Guid родительского типа объектов</param>
      /// <param name="relTypeGuids">Guid типов связей</param>
      /// <returns>Список идентификаторов дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      List<int> GetApplicabilityChildObjectTypesID(Guid parObjTypeGuid, IEnumerable<Guid> relTypeGuids);

      /// <summary>
      /// Получить список Guid дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанному типу связи
      /// </summary>
      /// <param name="parObjTypeID">Идентификатор родительского типа объектов</param>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Список Guid дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      List<Guid> GetApplicabilityChildObjectTypesGuid(int parObjTypeID, int relTypeID);

      /// <summary>
      /// Получить список Guid дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанным типам связей
      /// </summary>
      /// <param name="parObjTypeID">Идентификатор родительского типа объектов</param>
      /// <param name="relTypeIDs">Список идентификаторов типов связей</param>
      /// <returns>Список Guid дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      List<Guid> GetApplicabilityChildObjectTypesGuid(int parObjTypeID, IEnumerable<int> relTypeIDs);

      /// <summary>
      /// Получить список Guid дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанному типу связи
      /// </summary>
      /// <param name="parObjTypeGuid">Guid родительского типа объектов</param>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>Список Guid дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      List<Guid> GetApplicabilityChildObjectTypesGuid(Guid parObjTypeGuid, Guid relTypeGuid);

      /// <summary>
      /// Получить список Guid дочерних типов объектов, которые можно включать в состав указанных
      /// родительских типов объектов по указанным типам связей
      /// </summary>
      /// <param name="parObjTypeGuid">Guid родительского типа объектов</param>
      /// <param name="relTypeGuids">Список Guid типов связей</param>
      /// <returns>Список Guid дочерних типов объектов, которые можно включать в состав указанных или null</returns>
      List<Guid> GetApplicabilityChildObjectTypesGuid(
        Guid parObjTypeGuid,
        IEnumerable<Guid> relTypeGuids);

      /// <summary>
      /// Проверить, разрешен ли указанный родительский тип объектов,
      /// если есть списки разрешенных и запрещенных родительских типов объектов.
      /// Метод учитывает иерархию типов объектов для последовательного поиска, в какой
      /// из списков раньше попадет проверяемый тип объекта, либо его родительские типы
      /// </summary>
      /// <param name="parentObjType">Проверяемый родительский тип объекта</param>
      /// <param name="enabledParents">Список разрешенных родительских типов объектов</param>
      /// <param name="disabledParents">Список запрещенных родительских типов объектов</param>
      /// <param name="defValue">Значение по умолчанию, если информации в списках оказалось недостаточно</param>
      /// <returns>true - применяемость с указанным родительским типом разрешена</returns>
      bool IsEnabledParentType(
        int parentObjType,
        IEnumerable<int> enabledParents,
        IEnumerable<int> disabledParents,
        bool defValue);

      /// <summary>Поддерживает ли указанный тип связи ручную сортировку</summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>true, если указанный тип связи поддерживает ручную сортировку</returns>
      bool HasRelationTypeSorting(int relTypeID);

      /// <summary>Поддерживает ли указанный тип связи ручную сортировку</summary>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>true, если указанный тип связи поддерживает ручную сортировку</returns>
      bool HasRelationTypeSorting(Guid relTypeGuid);

      /// <summary>
      /// Получить список Int32-идентификаторов типов связей, поддерживающих ручную сортировку
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов связей, поддерживающих ручную сортировку</returns>
      List<int> GetSpecialSortingRelationsIDs();

      /// <summary>
      /// Получить список Guid идентификаторов типов связей, поддерживающих ручную сортировку
      /// </summary>
      /// <returns>Список Guid идентификаторов типов связей, поддерживающих ручную сортировку</returns>
      List<Guid> GetSpecialSortingRelationsGuids();

      /// <summary>
      /// Поддерживает ли указанный тип связи работу с допустимыми заменами
      /// </summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>true, если указанный тип связи поддерживает работу с допустимыми заменами</returns>
      bool HasRelationTypeSubstitutes(int relTypeID);

      /// <summary>
      /// Поддерживает ли указанный тип связи работу с допустимыми заменами
      /// </summary>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>true, если указанный тип связи поддерживает работу с допустимыми заменами</returns>
      bool HasRelationTypeSubstitutes(Guid relTypeGuid);

      /// <summary>
      /// Получить список Int32-идентификаторов типов связей, позволяющих работу с допустимыми заменами
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов связей, позволяющих работу с допустимыми заменами</returns>
      List<int> GetSpecialSubstitutesRelationsIDs();

      /// <summary>
      /// Получить список Guid идентификаторов типов связей, позволяющих работу с допустимыми заменами
      /// </summary>
      /// <returns>Список Guid идентификаторов типов связей, позволяющих работу с допустимыми заменами</returns>
      List<Guid> GetSpecialSubstitutesRelationsGuids();

      /// <summary>
      /// Поддерживает ли указанный тип связи группирование объектов
      /// </summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>true, если указанный тип связи поддерживает группирование объектов</returns>
      bool HasRelationTypeGrouping(int relTypeID);

      /// <summary>
      /// Поддерживает ли указанный тип связи группирование объектов
      /// </summary>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>true, если указанный тип связи поддерживает группирование объектов</returns>
      bool HasRelationTypeGrouping(Guid relTypeGuid);

      /// <summary>
      /// Получить список Int32-идентификаторов группирующих типов связей
      /// </summary>
      /// <returns>Список Int32-идентификаторов группирующих типов связей</returns>
      List<int> GetSpecialGroupingRelationsIDs();

      /// <summary>
      /// Получить список Guid идентификаторов группирующих типов связей
      /// </summary>
      /// <returns>Список Guid идентификаторов группирующих типов связей</returns>
      List<Guid> GetSpecialGroupingRelationsGuids();

      /// <summary>
      /// Проверить, является ли указанный тип связи конфигурируемым
      /// </summary>
      /// <param name="relType">Проверяемый тип связи</param>
      /// <returns>true - тип связи допускает конфигурирование составов</returns>
      bool IsPdmConfigurableRelationType(int relType);

      /// <summary>
      /// Проверить, является ли указанный тип связи частично конфигурируемым
      /// (в наличии есть атрибут "Контекст конфигуратора составов")
      /// </summary>
      /// <param name="relType">Проверяемый тип связи</param>
      /// <returns>true - тип связи допускает частичное конфигурирование составов</returns>
      bool IsPdmPartiallyConfigurableRelationType(int relType);

      /// <summary>Получить список описаний всех типов связей</summary>
      /// <returns>Список описаний всех типов связей</returns>
      List<IMSRelationType> GetRelationTypesList();

      /// <summary>Проверить, является ли тип объектов локальным</summary>
      /// <param name="type">Идентификатор типа объектов</param>
      /// <returns>true - тип объектов является локальным</returns>
      bool IsLocalObjectType(int type);

      /// <summary>Проверить, является ли тип объектов локальным</summary>
      /// <param name="type">Идентификатор типа объектов</param>
      /// <returns>true - тип объектов является локальным</returns>
      bool IsLocalObjectType(Guid type);

      /// <summary>
      /// Проверить, есть ли в списке хотя бы один основной или вложенный локальный тип объектов
      /// </summary>
      /// <param name="types">Список идентификаторов типов объектов</param>
      /// <returns>true - найден основной или вложенный локальный тип объектов</returns>
      bool HasLocalObjectType(IEnumerable<int> types);

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать связи, участвующие в допустимых заменах
      /// </summary>
      /// <param name="objTypeID">Родительский тип объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать связи, участвующие в допустимых заменах</returns>
      bool HasObjectTypeSubstRelTypes(int objTypeID);

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать связи, участвующие в допустимых заменах
      /// </summary>
      /// <param name="objTypeGuid">Guid родительского типа объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать связи, участвующие в допустимых заменах</returns>
      bool HasObjectTypeSubstRelTypes(Guid objTypeGuid);

      /// <summary>
      /// Получить список Int32-идентификаторов типов объектов, участвующих в допустимых заменах
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов объектов, участвующих в допустимых заменах</returns>
      List<int> GetSubstituteObjectsIDs();

      /// <summary>
      /// Получить список Guid идентификаторов типов объектов, участвующих в допустимых заменах
      /// </summary>
      /// <returns>Список Guid идентификаторов типов объектов, участвующих в допустимых заменах</returns>
      List<Guid> GetSubstituteObjectsGuids();

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать связи, позволяющие выполнять ручную сортировку
      /// </summary>
      /// <param name="objTypeID">Родительский тип объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать связи, позволяющие выполнять ручную сортировку</returns>
      bool HasObjectTypeSortingRelTypes(int objTypeID);

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать связи, позволяющие выполнять ручную сортировку
      /// </summary>
      /// <param name="objTypeGuid">Guid родительского типа объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать связи, позволяющие выполнять ручную сортировку</returns>
      bool HasObjectTypeSortingRelTypes(Guid objTypeGuid);

      /// <summary>
      /// Получить список Int32-идентификаторов типов объектов, которые могут содержать связи с сортировкой
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов объектов, которые могут содержать связи с сортировкой</returns>
      List<int> GetSortingObjectsIDs();

      /// <summary>
      /// Получить список Guid идентификаторов типов объектов, которые могут содержать связи с сортировкой
      /// </summary>
      /// <returns>Список Guid идентификаторов типов объектов, которые могут содержать связи с сортировкой</returns>
      List<Guid> GetSortingObjectsGuids();

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать связи типа "Состав изделия"
      /// </summary>
      /// <param name="objTypeID">Родительский тип объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать связи типа "Состав изделия"</returns>
      bool HasObjectTypeDesignedRelType(int objTypeID);

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать связи типа "Состав изделия"
      /// </summary>
      /// <param name="objTypeGuid">Guid родительского типа объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать связи типа "Состав изделия"</returns>
      bool HasObjectTypeDesignedRelType(Guid objTypeGuid);

      /// <summary>
      /// Получить список Int32-идентификаторов типов объектов, которые могут содержать связи типа "Состав изделия"
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов объектов, которые могут содержать связи типа "Состав изделия"</returns>
      List<int> GetDesignedObjectsIDs();

      /// <summary>
      /// Получить список Guid идентификаторов типов объектов, которые могут содержать связи типа "Состав изделия"
      /// </summary>
      /// <returns>Список Guid идентификаторов типов объектов, которые могут содержать связи типа "Состав изделия"</returns>
      List<Guid> GetDesignedObjectsGuids();

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать группирующие связи и сам является группирующим
      /// </summary>
      /// <param name="objTypeID">Родительский тип объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать группирующие связи и сам является группирующим</returns>
      bool HasObjectTypeGroupingRelTypes(int objTypeID);

      /// <summary>
      /// Может ли указанный родительский тип объекта содержать группирующие связи и сам является группирующим
      /// </summary>
      /// <param name="objTypeGuid">Guid родительского типа объекта</param>
      /// <returns>true, если указанный родительский тип объекта может содержать группирующие связи и сам является группирующим</returns>
      bool HasObjectTypeGroupingRelTypes(Guid objTypeGuid);

      /// <summary>
      /// Получить список Int32-идентификаторов группирующих типов объектов
      /// </summary>
      /// <returns>Список Int32-идентификаторов группирующих типов объектов</returns>
      List<int> GetSpecialGroupingIDs();

      /// <summary>
      /// Получить список Guid идентификаторов группирующих типов объектов
      /// </summary>
      /// <returns>Список Guid идентификаторов группирующих типов объектов</returns>
      List<Guid> GetSpecialGroupingGuids();

      /// <summary>
      /// Может ли указанный тип объекта входить в состав группирующих объектов
      /// </summary>
      /// <param name="objTypeID">Тип объекта</param>
      /// <returns>true, если указанный родительский тип объекта может входить в состав группирующих объектов</returns>
      bool HasObjectTypeGrouppedRelTypes(int objTypeID);

      /// <summary>
      /// Может ли указанный тип объекта входить в состав группирующих объектов
      /// </summary>
      /// <param name="objTypeGuid">Guid типа объекта</param>
      /// <returns>true, если указанный тип объекта может входить в состав группирующих объектов</returns>
      bool HasObjectTypeGrouppedRelTypes(Guid objTypeGuid);

      /// <summary>
      /// Получить список Int32-идентификаторов типов объектов, которые могут входить в состав группирующих объектов
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов объектов, которые могут входить в состав группирующих объектов</returns>
      List<int> GetSpecialGrouppedIDs();

      /// <summary>
      /// Получить список Guid идентификаторов типов объектов, которые могут входить в состав группирующих объектов
      /// </summary>
      /// <returns>Список Guid идентификаторов типов объектов, которые могут входить в состав группирующих объектов</returns>
      List<Guid> GetSpecialGrouppedGuids();

      /// <summary>
      /// Может ли указанный тип объекта содержать атрибут "Видимость объекта"
      /// </summary>
      /// <param name="objTypeID">Тип объекта</param>
      /// <returns>true, если указанный тип объекта может содержать атрибут "Видимость объекта"</returns>
      bool HasObjectTypeVisibilityAttr(int objTypeID);

      /// <summary>
      /// Может ли указанный тип объекта содержать атрибут "Видимость объекта"
      /// </summary>
      /// <param name="objTypeGuid">Guid типа объекта</param>
      /// <returns>true, если указанный тип объекта может содержать атрибут "Видимость объекта"</returns>
      bool HasObjectTypeVisibilityAttr(Guid objTypeGuid);

      /// <summary>
      /// Получить список Int32-идентификаторов типов объектов, которые могут содержать атрибут "Видимость объекта"
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов объектов, которые могут содержать атрибут "Видимость объекта"</returns>
      List<int> GetVisibilityObjectsIDs();

      /// <summary>
      /// Получить список Guid идентификаторов типов объектов, которые могут содержать атрибут "Видимость объекта"
      /// </summary>
      /// <returns>Список Guid идентификаторов типов объектов, которые могут содержать атрибут "Видимость объекта"</returns>
      List<Guid> GetVisibilityObjectsGuids();

      /// <summary>
      /// Проверка на необходимость включения версии объектов указанного типа в контекст, при условии что он доступен в сессии
      /// (без проверки на наличие другой версии объекта в контексте)
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="objectType">Тип проверяемого объекта</param>
      /// <param name="customFunc">Кастом функция позволяющая переопределить поведение для определенных типов объектов</param>
      /// <returns>true - данный объект необходимо добавлять в текущий контекст редактирования (без проверки на наличие другой версии объекта в контексте)</returns>
      bool MustAppendVersionToEditingContext(
        IUserSession session,
        int objectType,
        Func<EditingContextMode> customFunc = null);

      /// <summary>
      /// Проверить, является ли указанный тип объектов-контекстов упрощенным контекстом
      /// (не меняет содержимое номера группы изменений у контекстных объектов, не может
      /// быть связанным, допускает применение в своем содержимом версий объектов, принадлежащих
      /// другим контекстам редактирования)
      /// </summary>
      /// <param name="contextTypeID">Идентификатор типа объекта-контекста</param>
      /// <returns>true - указанный тип объекта является упрощенным контекстом</returns>
      bool IsSimpleEditingContext(int contextTypeID);

      /// <summary>
      /// Является ли указанный тип объекта контекстом редактирования
      /// </summary>
      /// <param name="objTypeID">Тип объекта</param>
      /// <returns>true, если указанный тип объекта является контекстом редактирования</returns>
      bool IsObjectTypeEditingContext(int objTypeID);

      /// <summary>
      /// Является ли указанный тип объекта контекстом редактирования
      /// </summary>
      /// <param name="objTypeGuid">Guid типа объекта</param>
      /// <returns>true, если указанный тип объекта является контекстом редактирования</returns>
      bool IsObjectTypeEditingContext(Guid objTypeGuid);

      /// <summary>
      /// Получить список Int32-идентификаторов типов объектов, которые являются контекстами редактирования
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов объектов, которые являются контекстами редактирования</returns>
      List<int> GetEditingContextObjectsIDs();

      /// <summary>
      /// Получить список Int32-идентификаторов типов объектов верхнего уровня, которые являются контекстами редактирования
      /// </summary>
      /// <returns>Список Int32-идентификаторов типов объектов верхнего уровня, которые являются контекстами редактирования</returns>
      List<int> GetEditingContextTopObjectsIDs();

      /// <summary>
      /// Получить список Guid идентификаторов типов объектов, которые являются контекстами редактирования
      /// </summary>
      /// <returns>Список Guid идентификаторов типов объектов, которые являются контекстами редактирования</returns>
      List<Guid> GetEditingContextObjectsGuids();

      /// <summary>
      /// Получить список Guid идентификаторов типов объектов верхнего уровня, которые являются контекстами редактирования
      /// </summary>
      /// <returns>Список Guid идентификаторов типов объектов верхнего уровня, которые являются контекстами редактирования</returns>
      List<Guid> GetEditingContextTopObjectsGuids();

      /// <summary>
      /// Проверить, можно ли добавлять указанный тип объекта в контекст редактирования
      /// </summary>
      /// <param name="objTypeGuid">Guid проверяемого типа объекта</param>
      /// <param name="autoMode">Включен ли режим автоматического пополнения</param>
      /// <returns>true - указанный тип объекта допускается добавлять в контекст редактирования</returns>
      bool CanAddObjTypeToEditingContext(Guid objTypeGuid, bool autoMode);

      /// <summary>
      /// Проверить, можно ли добавлять указанный тип объекта в контекст редактирования
      /// </summary>
      /// <param name="objType">Проверяемый тип объекта</param>
      /// <param name="autoMode">Включен ли режим автоматического пополнения</param>
      /// <returns>true - указанный тип объекта допускается добавлять в контекст редактирования</returns>
      bool CanAddObjTypeToEditingContext(int objType, bool autoMode);

      /// <summary>
      /// Проверить, является ли указанный тип объекта корнем конфигурируемого состава
      /// </summary>
      /// <param name="objType">Проверяемый тип объекта</param>
      /// <returns>true - тип объекта может являться корнем конфигурируемого состава</returns>
      bool IsPdmRootObjectType(int objType);

      /// <summary>
      /// Проверить, является ли указанный тип объекта конфигурируемым
      /// </summary>
      /// <param name="objType">Проверяемый тип объекта</param>
      /// <returns>true - тип объекта допускает конфигурирование составов</returns>
      bool IsPdmConfigurableObjectType(int objType);

      /// <summary>
      /// Проверить, может ли указанный тип объекта выступать в роли контекста конфигуратора составов
      /// </summary>
      /// <param name="objType">Проверяемый тип объекта</param>
      /// <returns>true - тип объекта может выступать в роли контекста конфигуратора составов</returns>
      bool IsPdmContextableObjectType(int objType);

      /// <summary>
      /// Получить из кэша или из базы данных тип указанной связи. Если не задавать
      /// значение session, то значение будет получено из кэша. Если в кэше значения
      /// нет, Вернется -1. Если задать значение session, то будет выполнено обращение
      /// к базе данных, а новое значение будет помещено в кэш (при необходимости - поверх
      /// старого значения)
      /// </summary>
      /// <param name="session">Сессия, в рамках которой выполняется работа с базой данных</param>
      /// <param name="prjLinkID">Идентификатор связи, тип которой требуется получить</param>
      /// <returns>Идентификатор типа указанной связи или -1</returns>
      int GetRelationType4PrjLinkID(IUserSession session, long prjLinkID);

      /// <summary>
      /// Получить Int32-идентификатор типа атрибута по его имени, Guid или числовому идентификатору.
      /// Сгенерирует исключение, если в метод засунуть объект некорректного типа
      /// </summary>
      /// <param name="attributeID">Имя атрибута, Guid или числовой идентификатор</param>
      /// <returns>Int32-идентификатор или Intermech.Consts.NavigatorUndefinedAttributeID, если тип атрибута не найден</returns>
      int GetAttributeID(object attributeID);

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном типе атрибута
      /// </summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>true, если тип атрибута существует</returns>
      bool ExistsAttributeType(int attrTypeID);

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном типе атрибута
      /// </summary>
      /// <param name="attrTypeGuid">Guid типа атрибута</param>
      /// <returns>true, если тип атрибута существует</returns>
      bool ExistsAttributeType(Guid attrTypeGuid);

      /// <summary>Получить краткую информацию о типе атрибута</summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Краткая информация о типе атрибута или null</returns>
      IMSAttributeType GetAttributeType(int attrTypeID);

      /// <summary>Хранятся ли в атрибуте системные данные</summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>true, если в атрибуте хранятся системные данные</returns>
      bool HasAttributeSystemData(int attrTypeID);

      /// <summary>Хранятся ли в атрибуте системные данные</summary>
      /// <param name="attrTypeGuid">Guid типа атрибута</param>
      /// <returns>true, если в атрибуте хранятся системные данные</returns>
      bool HasAttributeSystemData(Guid attrTypeGuid);

      /// <summary>Хранится ли в атрибуте список допустимых значений</summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>true, если в атрибуте хранится список допустимых значений</returns>
      bool HasAttributePossibleValues(int attrTypeID);

      /// <summary>Хранится ли в атрибуте список допустимых значений</summary>
      /// <param name="attrTypeGuid">Guid типа атрибута</param>
      /// <returns>true, если в атрибуте хранится список допустимых значений</returns>
      bool HasAttributePossibleValues(Guid attrTypeGuid);

      /// <summary>Можно ли отображать атрибут</summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>true, если атрибут можно отображать</returns>
      bool IsAttributeGridable(int attrTypeID);

      /// <summary>Можно ли отображать атрибут</summary>
      /// <param name="attrTypeGuid">Guid типа атрибута</param>
      /// <returns>true, если атрибут можно отображать</returns>
      bool IsAttributeGridable(Guid attrTypeGuid);

      /// <summary>Получить краткую информацию о типе атрибута</summary>
      /// <param name="attrTypeGuid">Идентификатор типа атрибута</param>
      /// <returns>Краткая информация о типе атрибута или null</returns>
      IMSAttributeType GetAttributeType(Guid attrTypeGuid);

      /// <summary>Получить название типа атрибута</summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Название типа атрибута</returns>
      string GetAttributeTypeName(int attrTypeID);

      /// <summary>Получить название типа атрибута</summary>
      /// <param name="attrTypeGuid">Идентификатор типа атрибута</param>
      /// <returns>Название типа атрибута</returns>
      string GetAttributeTypeName(Guid attrTypeGuid);

      /// <summary>
      /// Получить по Guid типа атрибута его Int32-идентификатор
      /// </summary>
      /// <param name="attrTypeGuid">Guid типа атрибута</param>
      /// <returns>Идентификатор типа атрибута. -1 - тип атрибута не найден</returns>
      int GetAttributeTypeID(Guid attrTypeGuid);

      /// <summary>
      /// Получить по Int32-идентификатору типа атрибута его Guid-идентификатор
      /// </summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Идентификатор типа атрибута. Guid.Empty - тип атрибута не найден</returns>
      Guid GetAttributeTypeGuid(int attrTypeID);

      /// <summary>
      /// Возвращает идентификатор типа атрибута по строковому представлению его глобального идентификатора
      /// </summary>
      /// <param name="Guid">Guid типа атрибута в виде строки</param>
      int GetAttributeTypeID(string Guid);

      /// <summary>
      /// Возвращает идентификатор типа атрибута по его названию
      /// </summary>
      /// <param name="attrName">Название типа атрибута</param>
      int GetAttributeByTypeNameID(string attrName);

      /// <summary>Возвращает Guid типа атрибута по его названию</summary>
      /// <param name="attrName">Название типа атрибута</param>
      Guid GetAttributeByTypeNameGuid(string attrName);

      /// <summary>Получить список всех типов атрибутов</summary>
      /// <returns>Список всех типов атрибутов</returns>
      List<int> GetAttributeTypesIDList();

      /// <summary>Получить список Guid всех типов атрибутов</summary>
      /// <returns>Список Guid всех типов атрибутов</returns>
      List<Guid> GetAttributeTypesGuidList();

      /// <summary>Получить список описаний всех типов атрибутов</summary>
      /// <returns>Список описаний всех типов атрибутов</returns>
      List<IMSAttributeType> GetAttributeTypesList();

      /// <summary>
      /// Получить список описаний атрибута для всех типов объектов, которым он назначен
      /// </summary>
      /// <param name="AttrTypeGuid">Guid типа атрибута</param>
      /// <returns>Список описаний атрибута для всех типов объектов, которым он назначен</returns>
      List<IMSAttribute4ObjectType> GetAllAttributes4ObjectTypeList(Guid AttrTypeGuid);

      /// <summary>
      /// Получить список описаний атрибута для всех типов объектов, которым он назначен
      /// </summary>
      /// <param name="AttrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Список описаний атрибута для всех типов объектов, которым он назначен</returns>
      List<IMSAttribute4ObjectType> GetAllAttributes4ObjectTypeList(int AttrTypeID);

      /// <summary>
      /// Получить список описаний всех типов атрибутов для указанного типа объекта
      /// </summary>
      /// <param name="objTypeGuid">Guid типа объекта</param>
      /// <returns>Список описаний всех типов атрибутов для указанного типа объекта</returns>
      List<IMSAttribute4ObjectType> GetAttribute4ObjectTypeList(Guid objTypeGuid);

      /// <summary>
      /// Получить список описаний всех типов атрибутов для указанного типа объекта
      /// </summary>
      /// <param name="ObjectTypeID">Идентификатор типа объекта</param>
      /// <returns>Список описаний всех типов атрибутов для указанного типа объекта</returns>
      List<IMSAttribute4ObjectType> GetAttribute4ObjectTypeList(int ObjectTypeID);

      /// <summary>
      /// Получить описание типа атрибута для указанного типа объекта
      /// </summary>
      /// <param name="ObjectTypeGuid">Guid типа объекта</param>
      /// <param name="AttrTypeGuid">Guid типа атрибута</param>
      /// <returns>Описание типа атрибута для указанного типа объекта, или null</returns>
      IMSAttribute4ObjectType GetAttribute4ObjectType(Guid ObjectTypeGuid, Guid AttrTypeGuid);

      /// <summary>
      /// Получить описание типа атрибута для указанного типа объекта
      /// </summary>
      /// <param name="ObjectTypeID">Идентификатор типа объекта</param>
      /// <param name="AttrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Описание типа атрибута для указанного типа объекта, или null</returns>
      IMSAttribute4ObjectType GetAttribute4ObjectType(int ObjectTypeID, int AttrTypeID);

      /// <summary>
      /// Получить список описаний атрибута для всех типов связей, которым он назначен
      /// </summary>
      /// <param name="AttrTypeGuid">Guid типа атрибута</param>
      /// <returns>Список описаний атрибута для всех типов связей, которым он назначен</returns>
      List<IMSAttribute4RelationType> GetAllAttributes4RelationTypeList(Guid AttrTypeGuid);

      /// <summary>
      /// Получить список описаний атрибута для всех типов связей, которым он назначен
      /// </summary>
      /// <param name="AttrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Список описаний атрибута для всех типов связей, которым он назначен</returns>
      List<IMSAttribute4RelationType> GetAllAttributes4RelationTypeList(int AttrTypeID);

      /// <summary>
      /// Получить список описаний всех типов атрибутов для указанного типа связи
      /// </summary>
      /// <param name="relTypeGuid">Guid типа связи</param>
      /// <returns>Список описаний всех типов атрибутов для указанного типа связи</returns>
      List<IMSAttribute4RelationType> GetAttribute4RelationTypeList(Guid relTypeGuid);

      /// <summary>
      /// Получить список описаний всех типов атрибутов для указанного типа связи
      /// </summary>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <returns>Список описаний всех типов атрибутов для указанного типа связи</returns>
      List<IMSAttribute4RelationType> GetAttribute4RelationTypeList(int relTypeID);

      /// <summary>
      /// Получить описание типа атрибута для указанного типа связи
      /// </summary>
      /// <param name="RelationTypeGuid">Guid типа связи</param>
      /// <param name="AttrTypeGuid">Guid типа атрибута</param>
      /// <returns>Описание типа атрибута для указанного типа связи, или null</returns>
      IMSAttribute4RelationType GetAttribute4RelationType(Guid RelationTypeGuid, Guid AttrTypeGuid);

      /// <summary>
      /// Получить описание типа атрибута для указанного типа объекта
      /// </summary>
      /// <param name="RelationTypeID">Идентификатор типа объекта</param>
      /// <param name="AttrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Описание типа атрибута для указанного типа объекта, или null</returns>
      IMSAttribute4RelationType GetAttribute4RelationType(int RelationTypeID, int AttrTypeID);

      /// <summary>
      /// Получить список типов объектов, на которые может ссылаться указанный тип атрибута
      /// </summary>
      /// <param name="attrID">Идентификатор типа атрибута</param>
      /// <returns>Список типов объектов, на которые может ссылаться указанный тип атрибута.
      /// Пустой список - допускается ссылка на любой тип объектов,
      /// null - атрибут не является ссылочным</returns>
      List<int> GetLinkedObjectTypes(int attrID);

      /// <summary>
      /// Получить список типов атрибутов, которые могут ссылаться на указанный тип объекта
      /// </summary>
      /// <param name="objTypeID">Идентификатор типа объекта</param>
      /// <returns>Список типов атрибутов, которые могут ссылаться на указанный тип объекта</returns>
      List<int> GetLinkAttributeTypes(int objTypeID);

      /// <summary>
      /// Получить по Guid группы атрибутов её Int32-идентификатор
      /// </summary>
      /// <param name="attrGroupGuid">Guid типа атрибута</param>
      /// <returns>Идентификатор группы атрибутов. -1 - группа атрибутов не найдена</returns>
      int GetAttributeGroupID(Guid attrGroupGuid);

      /// <summary>
      /// Получить по Int32-идентификатору группы атрибутов её Guid-идентификатор
      /// </summary>
      /// <param name="attrGroupID">Идентификатор типа атрибута</param>
      /// <returns>Идентификатор группы атрибутов. Guid.Empty - группа атрибутов не найдена</returns>
      Guid GetAttributeGroupGuid(int attrGroupID);

      /// <summary>
      /// Возвращает идентификатор группы атрибутов по строковому представлению её глобального идентификатора
      /// </summary>
      /// <param name="Guid">Guid группы атрибутов в виде строки</param>
      int GetAttributeGroupID(string Guid);

      /// <summary>Получить по Guid группы атрибутов описание группы</summary>
      /// <param name="attrGroupGuid">Guid типа группы атрибутов</param>
      /// <returns>Описание группы атрибутов или null</returns>
      IMSAttributeGroup GetAttributeGroup(Guid attrGroupGuid);

      /// <summary>
      /// Получить по строковому Guid группы атрибутов описание группы
      /// </summary>
      /// <param name="Guid">Guid типа группы атрибутов в виде строки</param>
      /// <returns>Описание группы атрибутов или null</returns>
      IMSAttributeGroup GetAttributeGroup(string Guid);

      /// <summary>Получить по ID группы атрибутов описание группы</summary>
      /// <param name="attrGroupID">ID типа группы атрибутов</param>
      /// <returns>Описание группы атрибутов или null</returns>
      IMSAttributeGroup GetAttributeGroup(int attrGroupID);

      /// <summary>
      /// Получить список типов атрибутов для указанной группы атрибутов
      /// </summary>
      /// <param name="guid">Guid группы атрибутов</param>
      /// <returns>Список типов атрибутов для указанной группы атрибутов</returns>
      List<int> GetAttributesInGroup(Guid guid);

      /// <summary>
      /// Получить список типов атрибутов для указанной группы атрибутов
      /// </summary>
      /// <param name="groupID">Идентификатор группы атрибутов: -1 для группы "Все атрибуты", -10 для группы "Назначенные типам" (собираются списки всех id атрибутов, которые назначены типам объектов и типам связей)</param>
      /// <returns>Список типов атрибутов для указанной группы атрибутов</returns>
      List<int> GetAttributesInGroup(int groupID);

      /// <summary>
      /// Получить список Guid типов атрибутов для указанной группы атрибутов
      /// </summary>
      /// <param name="guid">Guid группы атрибутов</param>
      /// <returns>Список Guid типов атрибутов для указанной группы атрибутов</returns>
      List<Guid> GetAttributesInGroupGuids(Guid guid);

      /// <summary>
      /// Получить список Guid типов атрибутов для указанной группы атрибутов
      /// </summary>
      /// <param name="groupID">Идентификатор группы атрибутов</param>
      /// <returns>Список Guid типов атрибутов для указанной группы атрибутов</returns>
      List<Guid> GetAttributesInGroupGuids(int groupID);

      /// <summary>
      /// Получить информацию о том, где применяется указанный тип атрибута
      /// </summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>Применяемость указанного типа атрибута</returns>
      IMSAttributeTypeApplicability GetAttributeTypeApplicability(int attrTypeID);

      /// <summary>
      /// Получить информацию о том, где применяется указанный тип атрибута
      /// </summary>
      /// <param name="attrTypeGuid">Уникальный глобальный идентификатор типа атрибута</param>
      /// <returns>Применяемость указанного типа атрибута</returns>
      IMSAttributeTypeApplicability GetAttributeTypeApplicability(Guid attrTypeGuid);

      /// <summary>
      /// Проверить, применяется ли указанный тип атрибута в типах объектов/связей
      /// </summary>
      /// <param name="attrTypeID">Идентификатор типа атрибута</param>
      /// <returns>true - указанный тип атрибута применяется в типах объектов/связей</returns>
      bool IsAttributeInUse(int attrTypeID);

      /// <summary>
      /// Проверить, применяется ли указанный тип атрибута в типах объектов/связей
      /// </summary>
      /// <param name="attrTypeGuid">Уникальный глобальный идентификатор типа атрибута</param>
      /// <returns>true - указанный тип атрибута применяется в типах объектов/связей</returns>
      bool IsAttributeInUse(Guid attrTypeGuid);

      /// <summary>
      /// Получить список идентификаторов типов атрибутов, которые применяются в типах
      /// объектов/связей. Список отсортирован по идентификатору типа атрибута
      /// </summary>
      /// <returns>Список идентификаторов типов атрибутов, которые применяются в типах объектов/связей</returns>
      List<int> GetUsedUnsortedAttributesIDs();

      /// <summary>
      /// Получить список идентификаторов типов атрибутов, которые применяются в типах
      /// объектов/связей. Список отсортирован по названию типа атрибута
      /// </summary>
      /// <returns>Список идентификаторов типов атрибутов, которые применяются в типах объектов/связей</returns>
      List<int> GetUsedSortedAttributesIDs();

      /// <summary>
      /// Получить список описаний типов атрибутов, которые применяются в типах
      /// объектов/связей. Список отсортирован по названию типа атрибута
      /// </summary>
      /// <returns>Список описаний типов атрибутов, которые применяются в типах объектов/связей</returns>
      List<IMSAttributeType> GetUsedSortedAttributes();

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанной схеме ЖЦ
      /// </summary>
      /// <param name="schemaID">Идентификатор схемы ЖЦ</param>
      /// <returns>true, если указанная схема ЖЦ существует</returns>
      bool ExistsLCSchema(int schemaID);

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанной схеме ЖЦ
      /// </summary>
      /// <param name="schemaGuid">Guid схемы ЖЦ</param>
      /// <returns>true, если указанная схема ЖЦ существует</returns>
      bool ExistsLCSchema(Guid schemaGuid);

      /// <summary>Получить краткую информацию о схеме ЖЦ</summary>
      /// <param name="schemaID">Идентификатор схемы ЖЦ</param>
      /// <returns>Краткая информация о схеме ЖЦ или null</returns>
      IMSLifeCycleScheme GetLCSchema(int schemaID);

      /// <summary>Получить краткую информацию о схеме ЖЦ</summary>
      /// <param name="schemaGuid">Guid схемы ЖЦ</param>
      /// <returns>Краткая информация о схеме ЖЦ или null</returns>
      IMSLifeCycleScheme GetLCSchema(Guid schemaGuid);

      /// <summary>Получить название схемы ЖЦ</summary>
      /// <param name="schemaID">Идентификатор схемы ЖЦ</param>
      /// <returns>Название схемы ЖЦ</returns>
      string GetLCSchemaName(int schemaID);

      /// <summary>Получить название схемы ЖЦ</summary>
      /// <param name="schemaGuid">Guid схемы ЖЦ</param>
      /// <returns>Название схемы ЖЦ</returns>
      string GetLCSchemaName(Guid schemaGuid);

      /// <summary>Получить по Guid схемы ЖЦ её Int32-идентификатор</summary>
      /// <param name="schemaGuid">Guid схемы ЖЦ</param>
      /// <returns>Идентификатор схемы ЖЦ. -1 - схема не найдена</returns>
      int GetLCSchemaID(Guid schemaGuid);

      /// <summary>
      /// Получить по Int32-идентификатору схемы ЖЦ её Guid-идентификатор
      /// </summary>
      /// <param name="schemaID">Идентификатор схемы ЖЦ</param>
      /// <returns>Идентификатор схемы ЖЦ. Guid.Empty - схема ЖЦ не найдена</returns>
      Guid GetLCSchemaGuid(int schemaID);

      /// <summary>
      /// Возвращает идентификатор схемы ЖЦ по строковому представлению её глобального идентификатора
      /// </summary>
      /// <param name="Guid">Guid схемы ЖЦ в виде строки</param>
      int GetLCSchemaID(string Guid);

      /// <summary>Получить список описаний всех схем ЖЦ</summary>
      /// <returns>Список описаний всех схем ЖЦ</returns>
      List<IMSLifeCycleScheme> GetLCSchemesList();

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном уровне продвижения
      /// </summary>
      /// <param name="levelID">Идентификатор уровня продвижения</param>
      /// <returns>true, если указанный уровень продвижения существует</returns>
      bool ExistsLCLevel(int levelID);

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном уровне продвижения
      /// </summary>
      /// <param name="levelGuid">Guid уровня продвижения</param>
      /// <returns>true, если  указанный уровень продвижения существует</returns>
      bool ExistsLCLevel(Guid levelGuid);

      /// <summary>Получить краткую информацию об уровне продвижения</summary>
      /// <param name="levelID">Идентификатор уровня продвижения</param>
      /// <returns>Краткая информация об уровне продвижения или null</returns>
      IMSLifeCycleLevel GetLCLevel(int levelID);

      /// <summary>Получить краткую информацию об уровне продвижения</summary>
      /// <param name="levelGuid">Guid уровня продвижения</param>
      /// <returns>Краткая информация об уровне продвижения или null</returns>
      IMSLifeCycleLevel GetLCLevel(Guid levelGuid);

      /// <summary>Получить название уровня продвижения</summary>
      /// <param name="levelID">Идентификатор уровня продвижения</param>
      /// <returns>Название уровня продвижения</returns>
      string GetLCLevelName(int levelID);

      /// <summary>Получить название уровня продвижения</summary>
      /// <param name="levelGuid">Guid уровня продвижения</param>
      /// <returns>Название уровня продвижения</returns>
      string GetLCLevelName(Guid levelGuid);

      /// <summary>
      /// Получить по Guid уровня продвижения его Int32-идентификатор
      /// </summary>
      /// <param name="levelGuid">Guid уровня продвижения</param>
      /// <returns>Идентификатор уровня продвижения. -1 - уровень продвижения не найден</returns>
      int GetLCLevelID(Guid levelGuid);

      /// <summary>
      /// Получить по Int32-идентификатору уровня продвижения его Guid-идентификатор
      /// </summary>
      /// <param name="levelID">Идентификатор уровня продвижения</param>
      /// <returns>Идентификатор уровня продвижения. Guid.Empty - уровень продвижения не найден</returns>
      Guid GetLCLevelGuid(int levelID);

      /// <summary>
      /// Возвращает идентификатор уровня продвижения по строковому представлению его глобального идентификатора
      /// </summary>
      /// <param name="Guid">Guid уровня продвижения в виде строки</param>
      int GetLCLevelID(string Guid);

      /// <summary>Получить список описаний всех уровней продвижения</summary>
      /// <returns>Список описаний всех уровней продвижения</returns>
      List<IMSLifeCycleLevel> GetLCLevelsList();

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном шаге ЖЦ
      /// </summary>
      /// <param name="lcstepId">Идентификатор шага ЖЦ</param>
      /// <returns>true, если указанный шаг ЖЦ существует</returns>
      bool ExistsLCStep(int lcstepId);

      /// <summary>
      /// Проверить, существует ли в кэше информация об указанном шаге ЖЦ
      /// </summary>
      /// <param name="lcstepGuid">Guid шага ЖЦ</param>
      /// <returns>true, если  указанный шаг ЖЦ существует</returns>
      bool ExistsLCStep(Guid lcstepGuid);

      /// <summary>Получить краткую информацию о шаге ЖЦ</summary>
      /// <param name="lcstepID">Идентификатор шага ЖЦ</param>
      /// <returns>Краткая информация о шаге ЖЦ или null</returns>
      IMSLifeCycleStep GetLCStep(int lcstepID);

      /// <summary>Получить краткую информацию о шаге ЖЦ</summary>
      /// <param name="lcstepGuid">Guid шага ЖЦ</param>
      /// <returns>Краткая информация о шаге ЖЦ или null</returns>
      IMSLifeCycleStep GetLCStep(Guid lcstepGuid);

      /// <summary>Получить название шага ЖЦ</summary>
      /// <param name="lcstepID">Идентификатор шага ЖЦ</param>
      /// <returns>Название шага ЖЦ</returns>
      string GetLCStepName(int lcstepID);

      /// <summary>Получить название шага ЖЦ</summary>
      /// <param name="lcstepGuid">Guid шага ЖЦ</param>
      /// <returns>Название шага ЖЦ</returns>
      string GetLCStepName(Guid lcstepGuid);

      /// <summary>Получить по Guid шага ЖЦ его Int32-идентификатор</summary>
      /// <param name="lcstepGuid">Guid шага ЖЦ</param>
      /// <returns>Идентификатор шага ЖЦ. -1 - шаг ЖЦ не найден</returns>
      int GetLCStepID(Guid lcstepGuid);

      /// <summary>
      /// Получить по Int32-идентификатору шага ЖЦ его Guid-идентификатор
      /// </summary>
      /// <param name="lcstepID">Идентификатор шага ЖЦ</param>
      /// <returns>Идентификатор шага ЖЦ. Guid.Empty - шаг ЖЦ не найден</returns>
      Guid GetLCStepGuid(int lcstepID);

      /// <summary>
      /// Возвращает идентификатор шага ЖЦ по строковому представлению его глобального идентификатора
      /// </summary>
      /// <param name="Guid">Guid шага ЖЦ в виде строки</param>
      int GetLCStepID(string Guid);

      /// <summary>Получить список описаний всех шагов ЖЦ</summary>
      /// <returns>Список описаний всех шагов ЖЦ</returns>
      List<IMSLifeCycleStep> GetLCStepsList();

      /// <summary>
      /// Получить по Guid какого-то элемента метаданных его тип
      /// </summary>
      /// <param name="guid">Guid какого-то элемента метаданных</param>
      /// <returns>Тип метаданных для указанного элемента</returns>
      IMSGlobals GetGlobalsByGuid(Guid guid);

      /// <summary>
      /// Получить по Guid какого-то элемента метаданных его описание
      /// </summary>
      /// <param name="guid">Guid какого-то элемента метаданных</param>
      /// <returns>Описание метаданных для указанного элемента</returns>
      IDisplayable GetDisplayableByGuid(Guid guid);
    }
}
