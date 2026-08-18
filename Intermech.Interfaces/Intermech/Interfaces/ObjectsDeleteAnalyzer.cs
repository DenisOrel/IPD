
// Type: Intermech.Interfaces.ObjectsDeleteAnalyzer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Абстрактный анализатор списка удаляемых объектов.</summary>
    public class ObjectsDeleteAnalyzer : LongLifeObject, IObjectsDeleteAnalyzer
    {
      /// <summary>
      /// Уникальный идентификатор анализатора
      /// (по данному идентификатору происходит регистрация и
      /// удаление анализатора в службе анализаторов)
      /// </summary>
      private Guid guid = Guid.NewGuid();

      /// <summary>
      /// Уникальный идентификатор анализатора
      /// (по данному идентификатору происходит регистрация и
      /// удаление анализатора в службе анализаторов)
      /// </summary>
      public virtual Guid Guid => this.guid;

      /// <summary>
      /// Выполнить анализ удаляемых объектов, при необходимости добавить в граф
      /// дополнительные идентификаторы версий объектов, которые тоже требуется удалить.
      /// На верхнем уровне - первоначальный список удаляемых версий объектов
      /// </summary>
      /// <param name="session">Сессия, в рамках которой выполняется анализ</param>
      /// <param name="deletingObjects">Список удаляемых версий объектов</param>
      /// <param name="options">Параметры</param>
      /// <returns>Количество добавленных к удалению объектов</returns>
      public virtual int Analyze(
        IUserSession session,
        DeletingObjects deletingObjects,
        DeleteAnalyzerOptions options)
      {
        return 0;
      }

      /// <summary>Выполнить анализ всех версий в списке объектов</summary>
      /// <param name="session">Сессия</param>
      /// <param name="deletingObjects">Список удаляемых объектов</param>
      /// <param name="options">Параметры</param>
      /// <returns>Количество найденных версий</returns>
      public virtual int AnalyzeAllVersions(
        IUserSession session,
        DeletingObjects deletingObjects,
        DeleteAnalyzerOptions options)
      {
        if (session == null || deletingObjects == null || (options & DeleteAnalyzerOptions.FindAllVersions) == DeleteAnalyzerOptions.None)
          return 0;
        int num = 0;
        List<DeletingObject> deletingObjects1 = deletingObjects.ExtractDeletingObjects();
        for (int index1 = 0; index1 < deletingObjects1.Count; ++index1)
        {
          DeletingObject deletingObject = deletingObjects1[index1];
          List<long> objectIdVersions = session.GetObjectIDVersions(deletingObject.ObjectID);
          if (objectIdVersions != null)
          {
            for (int index2 = 0; index2 < objectIdVersions.Count; ++index2)
            {
              if (deletingObjects.FindDeletingObjectFromRoot(objectIdVersions[index2]) == null)
              {
                deletingObject.Items.FindRootParent().Add(0L, 0L, objectIdVersions[index2], false, LocalizationHolder.rm.GetString("Interfaces_70")).LoadDescription(session);
                ++num;
              }
            }
          }
        }
        return num;
      }
    }
}
