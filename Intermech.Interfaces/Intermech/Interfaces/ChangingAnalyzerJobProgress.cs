
// Type: Intermech.Interfaces.ChangingAnalyzerJobProgress
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Индикатор выполнения задания по анализу списка изменяемых объектов
    /// </summary>
    [Serializable]
    public enum ChangingAnalyzerJobProgress
    {
      /// <summary>Задание прервано из-за ошибки</summary>
      Error = -2, // 0xFFFFFFFE
      /// <summary>Задание прервано пользователем</summary>
      Cancelled = -1, // 0xFFFFFFFF
      /// <summary>Задание ещё не запущено</summary>
      NotStarted = 0,
      /// <summary>Задание работает</summary>
      Working = 1,
      /// <summary>Задание успешно завершено</summary>
      Completed = 2,
    }
}
