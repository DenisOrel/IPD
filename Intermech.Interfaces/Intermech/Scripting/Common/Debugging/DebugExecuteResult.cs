
// Type: Intermech.Scripting.Common.Debugging.DebugExecuteResult
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Scripting.Common.Debugging
{
    /// <summary>Результат выполнения сценария в режиме отладки.</summary>
    [Serializable]
    public struct DebugExecuteResult
    {
      /// <summary>
      /// Возвращаемое значение сценария.
      /// Значение свойства может быть равно null, если у сценария нет возвращаемого значения.
      /// </summary>
      public object ReturnValue { get; set; }

      /// <summary>
      /// Необработанное исключение в процессе выполнения сценария.
      /// Значение свойства может быть равно null.
      /// </summary>
      public Exception Exception { get; set; }

      /// <summary>Отладочный вывод сценария.</summary>
      public string[] DebugOutput { get; set; }
    }
}
