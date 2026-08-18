
// Type: Intermech.UI.ProgressSinks
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.UI.Winforms;
using System;
using System.Diagnostics;


namespace Intermech.UI
{
    /// <summary>
    /// Содержит сервисные методы и свойства для работы с индикаторами хода выполнения различных процессов.
    /// </summary>
    public static class ProgressSinks
    {
      private static IProgressSinkDialogService dialogService = (IProgressSinkDialogService) new ProgressSinkDialogService();
      private static NullPercentageProgressSink nullPercentageSink = NullPercentageProgressSink.Default;
      private static NullMasterSlaveProgressSink nullMasterSlaveSink = NullMasterSlaveProgressSink.Default;

      /// <summary>
      /// Возвращает или задает сервис для выполнения процессов с отображением хода выполнения в диалоговом окне.
      /// </summary>
      /// <exception cref="T:ArgumentNullException">Новое значение свойства не должно быть равно null</exception>
      public static IProgressSinkDialogService DialogService
      {
        [DebuggerStepThrough] get => ProgressSinks.dialogService;
        [DebuggerStepThrough] set
        {
          ProgressSinks.dialogService = value != null ? value : throw new ArgumentNullException(nameof (value));
        }
      }

      public static IPercentageProgressSink NullPercentageSink
      {
        [DebuggerStepThrough] get => (IPercentageProgressSink) ProgressSinks.nullPercentageSink;
      }

      public static IMasterSlaveProgressSink ToMasterSlaveSink(this IPercentageProgressSink progressSink)
      {
        return progressSink != null ? (IMasterSlaveProgressSink) new MasterSlaveProgressSinkAdapter(progressSink) : throw new ArgumentNullException(nameof (progressSink));
      }

      public static IMasterSlaveProgressSink NullMasterSlaveSink
      {
        [DebuggerStepThrough] get => (IMasterSlaveProgressSink) ProgressSinks.nullMasterSlaveSink;
      }

      /// <summary>
      /// Создает и возвращает алгоритм обновления индикатора готовности процесса для случая, когда известно общее количество задач, из которых состоит процесс.
      /// </summary>
      /// <param name="progressSink">Индикатор готовности процесса</param>
      /// <param name="totalTaskCount">Общее число задач, из которых состоит процесс</param>
      /// <returns>Алгоритм обновления индикатора готовности процесса</returns>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="progressSink" /> не должен быть равен null</exception>
      /// <exception cref="T:ArgumentOutOfRangeException">Параметр <paramref name="totalTaskCount" /> не должен меньше или равен 0</exception>
      public static IProgressUpdater CreateProgressUpdater(
        IPercentageProgressSink progressSink,
        int totalTaskCount)
      {
        return (IProgressUpdater) new FixedTaskCountProgressUpdater(progressSink, totalTaskCount);
      }

      /// <summary>
      /// Создает и возвращает алгоритм обновления индикатора готовности процесса для случая, когда неизвестно общее количество задач, из которых состоит процесс.
      /// </summary>
      /// <param name="progressSink">Индикатор готовности процесса</param>
      /// <param name="initialTaskCount">Изначально известное количество задач, из которых состоит процесс</param>
      /// <returns>Алгоритм обновления индикатора готовности процесса</returns>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="progressSink" /> не должен быть равен null</exception>
      /// <exception cref="T:ArgumentOutOfRangeException">Параметр <paramref name="initialTaskCount" /> не должен меньше 0</exception>
      public static IDynamicProgressUpdater CreateDynamicProgressUpdater(
        IPercentageProgressSink progressSink,
        int initialTaskCount)
      {
        return (IDynamicProgressUpdater) new VariableTaskCountProgressUpdater(progressSink, initialTaskCount);
      }
    }
}
