
// Type: Intermech.UI.IProgressUpdater
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.UI
{
    /// <summary>
    /// Интерфейс алгоритма обновления индикатора готовности процесса для случая, когда известно общее количество задач, из которых состоит процесс.
    /// </summary>
    public interface IProgressUpdater
    {
      /// <summary>Возвращает индикатор готовности процесса.</summary>
      IPercentageProgressSink ProgressSink { get; }

      /// <summary>
      /// Возвращает общее количество задач, из которых состоит процесс.
      /// </summary>
      int TotalTasks { get; }

      /// <summary>Возвращает количество выполненных задач.</summary>
      int CompletedTasks { get; }

      /// <summary>
      /// Увеличивает количество выполненных задач на указанное значение. Выполнение этого метода вызывает обновление индикатора готовности процесса.
      /// </summary>
      /// <param name="value">Приращение количества задач</param>
      /// <exception cref="T:ArgumentOutOfRangeException">Параметр <paramref name="value" /> не должен быть меньше 0, а результат увеличения количества выполненных задач не должен превышать общее количество задач</exception>
      void AddCompletedTasks(int value);
    }
}
