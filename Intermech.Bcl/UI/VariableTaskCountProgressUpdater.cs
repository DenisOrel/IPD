
// Type: Intermech.UI.VariableTaskCountProgressUpdater
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Runtime;
using System;
using System.Diagnostics;


namespace Intermech.UI
{
    /// <summary>
    /// Реализует алгоритм обновления индикатора готовности процесса для случая, когда неизвестно общее количество задач, из которых состоит процесс.
    /// </summary>
    public sealed class VariableTaskCountProgressUpdater : IDynamicProgressUpdater
    {
      private IPercentageProgressSink progressSink;
      private int totalTasks;
      private bool totalTasksLocked;
      private int completedTasks;
      private double percentValue;

      /// <summary>Создает объект.</summary>
      /// <param name="progressSink">Индикатор готовности процесса</param>
      /// <param name="initialTaskCount">Изначально известное количество задач, из которых состоит процесс</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="progressSink" /> не должен быть равен null</exception>
      /// <exception cref="T:ArgumentOutOfRangeException">Параметр <paramref name="initialTaskCount" /> не должен меньше 0</exception>
      public VariableTaskCountProgressUpdater(
        IPercentageProgressSink progressSink,
        int initialTaskCount)
      {
        if (progressSink == null)
          throw new ArgumentNullException(nameof (progressSink));
        if (initialTaskCount < 0)
          throw new ArgumentOutOfRangeException(nameof (initialTaskCount));
        this.progressSink = progressSink;
        this.totalTasks = initialTaskCount;
      }

      /// <summary>Возвращает индикатор готовности процесса.</summary>
      public IPercentageProgressSink ProgressSink
      {
        [DebuggerStepThrough] get => this.progressSink;
      }

      /// <summary>Возвращает количество выполненных задач.</summary>
      public int CompletedTasks
      {
        [DebuggerStepThrough] get => this.completedTasks;
      }

      /// <summary>
      /// Возвращает общее количество задач, из которых состоит процесс.
      /// </summary>
      public int TotalTasks
      {
        [DebuggerStepThrough] get => this.totalTasks;
      }

      /// <summary>
      /// Возвращает признак, что общее количество задач установлено окончательно и больше меняться не будет.
      /// </summary>
      public bool TotalTasksLocked
      {
        [DebuggerStepThrough] get => this.totalTasksLocked;
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
        this.UpdateProgress(value);
      }

      /// <summary>
      /// Увеличивает общее количество задач, из которых состоит процесс, на указанное значение.
      /// </summary>
      /// <param name="value">Приращение количества задач</param>
      /// <exception cref="T:ArgumentOutOfRangeException">Параметр <paramref name="value" /> не должен быть меньше 0</exception>
      /// <exception cref="T:InvalidOperationException">Невозможно изменить общее количество задач, так как оно заблокировано и больше изменяться не должно</exception>
      public void AddTotalTasks(int value)
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException(nameof (value));
        if (this.totalTasksLocked)
          throw PropertyExceptions.PropertyBadValueException((object) this, "TotalTasks", "The property value is locked.");
        if (value == 0)
          return;
        this.totalTasks += value;
      }

      /// <summary>
      /// Блокирует общее количество задач, из которых состоит процесс. Метод используется в том случае, когда общее количество задач установлено окончательно и больше изменяться не должно.
      /// Это позволяет алгоритму точнее обновлять индикатор готовности процесса.
      /// </summary>
      public void LockTotalTasks()
      {
        if (this.totalTasksLocked)
          return;
        this.totalTasksLocked = true;
      }

      private void UpdateProgress(int completedTasksDelta)
      {
        double num1 = 100.0 - this.percentValue;
        if (!this.totalTasksLocked)
          num1 *= 0.6;
        int num2 = this.totalTasks - (this.completedTasks - completedTasksDelta);
        if (!this.totalTasksLocked && num2 < 3)
          num2 = 3;
        this.percentValue += num1 / (double) num2 * (double) completedTasksDelta;
        if (this.percentValue > 100.0 || MathUtils.AlmostEqual(this.percentValue, 100.0))
          this.percentValue = 100.0;
        this.ProgressSink.SetProgress(this.percentValue);
      }
    }
}
