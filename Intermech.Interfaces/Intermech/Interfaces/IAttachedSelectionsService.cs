
// Type: Intermech.Interfaces.IAttachedSelectionsService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Сервис привязанных выборок</summary>
    public interface IAttachedSelectionsService
    {
      /// <summary>Получить объекты, для которых привязана выборка</summary>
      /// <param name="selectionID">Идентификатор выборки</param>
      /// <param name="objectTypeIDs">Типы объектов</param>
      /// <returns></returns>
      AttachedSelObjectInfo[] GetObjectsForSelection(long selectionID, params int[] objectTypeIDs);

      /// <summary>Получить все привязанные объекты для выборки</summary>
      /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии</param>
      /// <param name="selectionID">Идентификатор выборки</param>
      /// <param name="objectTypeIDs">Типы объектов для которых устанавливается новое значение</param>
      /// <param name="objects">Список привязанных отчетов</param>
      void SetObjectsForSelection(
        Guid sessionGuid,
        long selectionID,
        int[] objectTypeIDs,
        AttachedSelObjectInfo[] objects);

      /// <summary>Открепить выборку от объектов</summary>
      /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии</param>
      /// <param name="selectionID">Идентификатор выборки</param>
      /// <param name="objectIDs">Список привязанных отчетов</param>
      void ExcludeObjects(Guid sessionGuid, long selectionID, long[] objectIDs);
    }
}
