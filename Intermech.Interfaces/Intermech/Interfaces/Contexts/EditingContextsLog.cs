
// Type: Intermech.Interfaces.Contexts.EditingContextsLog
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.Contexts
{
    /// <summary>
    /// Протокол действий с указанным контекстом редактирования
    /// </summary>
    [Serializable]
    public sealed class EditingContextsLog : List<EditingContextsLogEntry>
    {
      /// <summary>Идентификатор версии контекста редактирования</summary>
      public long ContextID;

      /// <summary>
      /// Добавить запись для журнала действий над контекстом редактирования
      /// </summary>
      /// <param name="errorCode">Код ошибки, возникшей при работе с контекстом редактирования</param>
      /// <param name="objectID">Идентификатор версии объекта, который добавляли/заменяли в контексте редактирования</param>
      public void Add(EditingContextsLogError errorCode, long objectID)
      {
        this.Add(new EditingContextsLogEntry(errorCode, objectID));
      }

      /// <summary>Вернуть текст указанной записи журнала</summary>
      /// <param name="entry">Запись журнала</param>
      /// <returns>Текст указанной записи журнала</returns>
      public static string GetEntryText(EditingContextsLogEntry entry)
      {
        return entry == null ? string.Empty : EnumTypeHelper.GetCaption((Enum) entry.ErrorCode);
      }

      /// <summary>Извлечь из журнала событий список версий объектов</summary>
      /// <returns>Список версий объектов</returns>
      public List<long> ExtractVersions()
      {
        List<long> versions = new List<long>(this.Count);
        for (int index = 0; index < this.Count; ++index)
          versions.Add(this[index].ObjectID);
        return versions;
      }
    }
}
