
// Type: Intermech.Interfaces.DeleteObjectsJobMode
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Режим удаления объектов</summary>
    [Serializable]
    public enum DeleteObjectsJobMode
    {
      /// <summary>
      /// Запросить действие при возникновении ошибки (по умолчанию)
      /// </summary>
      AscOnError,
      /// <summary>
      /// Прерывать удаление при возникновении ошибки (полностью завершать задачу по удалению)
      /// </summary>
      AbortOnError,
      /// <summary>Удалять всё, что можно, игнорируя ошибки</summary>
      IgnoreErrors,
    }
}
