
// Type: Intermech.Interfaces.Contexts.EditingContextsLogEntry
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Contexts
{
    /// <summary>
    /// Запись в журнале действий над указанным контекстом редактирования
    /// </summary>
    [Serializable]
    public sealed class EditingContextsLogEntry
    {
      /// <summary>
      /// Код ошибки, возникшей при работе с контекстом редактирования
      /// </summary>
      public EditingContextsLogError ErrorCode;
      /// <summary>
      /// Идентификатор версии объекта, который добавляли/заменяли в контексте редактирования
      /// </summary>
      public long ObjectID;
      /// <summary>Дополнительные данные</summary>
      public object Tag;

      /// <summary>
      /// Создать запись для журнала действий над контекстом редактирования
      /// </summary>
      /// <param name="errorCode">Код ошибки, возникшей при работе с контекстом редактирования</param>
      /// <param name="objectID">Идентификатор версии объекта, который добавляли/заменяли в контексте редактирования</param>
      public EditingContextsLogEntry(EditingContextsLogError errorCode, long objectID)
      {
        this.ErrorCode = errorCode;
        this.ObjectID = objectID;
      }
    }
}
