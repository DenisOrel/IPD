
// Type: Intermech.Interfaces.ICompositionsAutomaticSortingSession
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Compositions;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс сессии расчета / назначения атрибута сортировки согласно правилам сортировки и отображениям составов
    /// </summary>
    /// <remarks>Не забываем каждый раз при получении сессии ее освобождать!!</remarks>
    public interface ICompositionsAutomaticSortingSession
    {
      /// <summary>
      /// Загрузка состава родительских объектов, связи которого будет нумероваться
      /// </summary>
      /// <remarks></remarks>
      /// <param name="objectIDs">Ид. версии объекта, состав которого получается</param>
      /// <param name="session">Пользовательская сессия</param>
      void PrefetchObjectComposition(IEnumerable<long> objectIDs, object session);

      /// <summary>
      /// Загрузка состава родительских объектов, связи которого будет нумероваться
      /// </summary>
      /// <remarks>Рекомендуется вызывать перед обработкой связей
      /// для заранее известных объектов</remarks>
      /// <param name="objectIDs">Описание объектов, состав которых получается</param>
      /// <param name="session">Пользовательская сессия</param>
      void PrefetchObjectComposition(IEnumerable<ObjInfoItem> objectIDs, object session);

      /// <summary>Назначение атрибута сортировки согласно установкам</summary>
      /// <remarks>Режим обработки - связь добавляется в конец списка</remarks>
      /// <param name="relationId">Ид. связи</param>
      /// <param name="session">Пользовательская сессия</param>
      void ProceedRelation(long relationId, object session);

      /// <summary>Назначение атрибута сортировки согласно установкам</summary>
      /// <remarks>Режим обработки - связь добавляется в конец списка</remarks>
      /// <param name="relationIDs">Ид. связи</param>
      /// <param name="session">Пользовательская сессия</param>
      void ProceedRelation(IEnumerable<long> relationIDs, object session);

      /// <summary>Назначение атрибута сортировки согласно установкам</summary>
      /// <remarks>Режим обработки - связь добавляется в конец списка. Если известны все параметры связи,
      /// рекомендуется передавать CompositionSortingProjInfo</remarks>
      /// <param name="relationIDs">Ид. связи</param>
      /// <param name="targetMode">Режимы обработки / назначения сортировки</param>
      /// <param name="targetRelationId">Связь - target</param>
      /// <param name="session">Пользовательская сессия</param>
      void ProceedRelation(
        IEnumerable<long> relationIDs,
        CompositionTargetMode targetMode,
        long targetRelationId,
        object session);

      /// <summary>Назначение атрибута сортировки согласно установкам</summary>
      /// <remarks>Режим обработки - связь добавляется в конец списка</remarks>
      /// <param name="relationInfo">Описание параметров связи</param>
      /// <param name="session">Пользовательская сессия</param>
      void ProceedRelation(CompositionSortingProjInfo relationInfo, object session);

      /// <summary>Назначение атрибута сортировки согласно установкам</summary>
      /// <remarks>Режим обработки - связь добавляется в конец списка</remarks>
      /// <param name="relationInfo">Описание параметров связи</param>
      /// <param name="session">Пользовательская сессия</param>
      void ProceedRelation(
        IEnumerable<CompositionSortingProjInfo> relationInfo,
        object session);

      /// <summary>Назначение атрибута сортировки согласно установкам</summary>
      /// <remarks>Режим обработки - связь добавляется в конец списка</remarks>
      /// <param name="relationInfo">Описание параметров связи</param>
      /// <param name="targetMode">Режимы обработки / назначения сортировки</param>
      /// <param name="targetRelationId">Связь - target</param>
      /// <param name="session">Пользовательская сессия</param>
      void ProceedRelation(
        IEnumerable<CompositionSortingProjInfo> relationInfo,
        CompositionTargetMode targetMode,
        long targetRelationId,
        object session);

      /// <summary>Назначение атрибута сортировки согласно установкам</summary>
      /// <remarks>Режим обработки - связь добавляется в конец списка</remarks>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="targetMode">Режимы обработки / назначения сортировки</param>
      /// <param name="sortingParams">Параметры назначения</param>
      void ProceedRelation(
        object session,
        CompositionTargetMode targetMode,
        CompositionSortingParams sortingParams);
    }
}
