
// Type: Intermech.UI.FixedTaskCountProgressUpdater
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.UI
{
    /// <summary>
    /// Реализует алгоритм обновления индикатора готовности процесса для случая, когда известно общее количество задач, из которых состоит процесс.
    /// </summary>
    public sealed class FixedTaskCountProgressUpdater : IProgressUpdater
    {
      private IPercentageProgressSink progressSink;
      private int totalTasks;
      private int completedTasks;
      private double percentFactor;

      /// <summary>Создает объект.</summary>
      /// <param name="progressSink">Индикатор готовности процесса</param>
      /// <param name="totalTaskCount">Общее количество задач, из которых состоит процесс</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="progressSink" /> не должен быть равен null</exception>
      /// <exception cref="T:ArgumentOutOfRangeException">Параметр <paramref name="totalTaskCount" /> не должен меньше или равен 0</exception>
      public FixedTaskCountProgressUpdater(IPercentageProgressSink progressSink, int totalTaskCount)
      {
        if (progressSink == null)
          throw new ArgumentNullException(nameof (progressSink));
        if (totalTaskCount <= 0)
          throw new ArgumentOutOfRangeException(nameof (totalTaskCount));
        this.progressSink = progressSink;
        this.totalTasks = totalTaskCount;
        this.percentFactor = 100.0 / (double) totalTaskCount;
      }

      /// <summary>Возвращает индикатор готовности процесса.</summary>
      public IPercentageProgressSink ProgressSink
      {
        [DebuggerStepThrough] get => this.progressSink;
      }

      /// <summary>
      /// Возвращает общее количество задач, из которых состоит процесс.
      /// </summary>
      public int TotalTasks
      {
        [DebuggerStepThrough] get => this.totalTasks;
      }

      /// <summary>Возвращает количество выполненных задач.</summary>
      public int CompletedTasks
      {
        [DebuggerStepThrough] get => this.completedTasks;
      }

      /// <summary>
      /// Увеличивает количество выполненных задач на указанное значение. Выполнение этого метода вызывает обновление индикатора готовности процесса.
      /// </summary>
      /// <param name="value">Приращение количества задач</param>
      /// <exception cref="T:ArgumentOutOfRangeException">Параметр <paramref name="value" /> не должен быть меньше 0, а результат увеличения количества выполненных задач не должен превышать общее количество задач</exception>
      public void AddCompletedTasks(int value)
      {
        if (value < 0 || this.completedTasks + value > this.totalTasks)
          throw new ArgumentOutOfRangeException(nameof (value));
        if (value == 0)
          return;
        this.completedTasks += value;
        this.progressSink.SetProgress(PercentValueHelper.Normalize((double) this.completedTasks * this.percentFactor));
      }
    }
}
