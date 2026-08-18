
// Type: Intermech.UserSessionThreadConflictException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;
using System.Runtime.Serialization;


namespace Intermech
{
    [Serializable]
    public class UserSessionThreadConflictException : KernelException
    {
      private const string threadIdField = "threadId";
      private const string conflictIdField = "conflictId";
      private int threadId;
      private Guid conflictId;

      /// <summary>Создает объект.</summary>
      /// <param name="message">Сообщение</param>
      /// <param name="threadId">Идентификатор потока, в котором обнаружен конфликт</param>
      /// <param name="conflictId">Уникальный идентификатор конфликта двух потоков (общее значение у этих потоков)</param>
      public UserSessionThreadConflictException(string message, int threadId, Guid conflictId)
        : base(message)
      {
        this.threadId = threadId;
        this.conflictId = conflictId;
      }

      /// <summary>
      /// Возвращает идентификатор потока, в котором обнаружен конфликт.
      /// </summary>
      public int ThreadId
      {
        [DebuggerStepThrough] get => this.threadId;
      }

      /// <summary>
      /// Возвращает уникальный идентификатор конфликта двух потоков.
      /// Позволяет сопоставить найти в журналах системы stack trace конфликтующих потоков.
      /// </summary>
      public Guid ConflictId
      {
        [DebuggerStepThrough] get => this.conflictId;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="info"></param>
      /// <param name="context"></param>
      protected UserSessionThreadConflictException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.threadId = info.GetInt32(nameof (threadId));
        this.conflictId = (Guid) info.GetValue(nameof (conflictId), typeof (Guid));
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="info"></param>
      /// <param name="context"></param>
      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("threadId", this.threadId);
        info.AddValue("conflictId", (object) this.conflictId);
      }
    }
}
