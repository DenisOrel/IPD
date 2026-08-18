
// Type: Intermech.Interfaces.ClassifiedError
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Структура для передачи текущего результата классификации
    /// </summary>
    [Serializable]
    public class ClassifiedError
    {
      /// <summary>ID объекта</summary>
      public long ObjectID { get; }

      /// <summary>Его имя</summary>
      public string ObjectName { get; }

      /// <summary>Сообщение</summary>
      public Exception Exception { get; }

      /// <summary>Флаг окончания классификации</summary>
      public bool FullClassified { get; }

      public ClassifiedError(
        long objectID,
        string objectName,
        Exception exception,
        bool fullClassified = false)
      {
        this.ObjectID = objectID;
        this.ObjectName = objectName;
        this.Exception = exception;
        this.FullClassified = fullClassified;
      }

      public ClassifiedError(bool fullClassified)
        : this(0L, string.Empty, (Exception) null, fullClassified)
      {
      }
    }
}
